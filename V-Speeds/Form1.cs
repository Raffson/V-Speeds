using System.Collections.Immutable;

namespace V_Speeds
{
    public partial class Form1 : Form
    {
        private readonly V_Calculator vcalc = new V_Calculator();

        // mapping numericUpDowns to their setter-function for model, combobox + converters to ensure metric data is passed...
        private readonly Dictionary<NumericUpDown, BaseDelegate> model_map;

        // mapping unit to a delegate with lastSelectedIndex, respective numericUpDown, Conversion functions
        private readonly Dictionary<ComboBox, BaseDelegate> unit_map = new Dictionary<ComboBox, BaseDelegate>();

        // list of inputs to be (un)locked if profile is selected...
        private readonly NumericUpDown[] fixed_inputs;

        // list of inputs to overwrite profile is selected...
        private readonly NumericUpDown[] profile_inputs;


        private int lastProfileIndex = 0;

        public Form1()
        {
            System.Globalization.CultureInfo customCulture = 
                (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            customCulture.NumberFormat.NumberGroupSeparator = ",";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            
            InitializeComponent();

            fixed_inputs = new NumericUpDown[] { lsa_in, cl_in, bf_in, rtr_in };
            profile_inputs = new NumericUpDown[] { lsa_in, cl_in, bf_in, csa_in, cd_in, rtr_in, gw_in, thr_in };

            apSelect.SelectedIndex = 0;
            apSelect.SelectedIndexChanged += new System.EventHandler(ProfileChanged);

            model_map = new Dictionary<NumericUpDown, BaseDelegate> {
                    { gw_in,  new WeightDelegate(gw_in, vcalc.SetGw, weightUnit) },
                    { oat_in, new TemperatureDelegate(oat_in, vcalc.SetOat, oatUnit) },
                    { qfe_in, new PressureDelegate(qfe_in, vcalc.SetQfe, qfeUnit) },
                    { lsa_in, new AreaDelegate(lsa_in, vcalc.SetLsa, lsaUnit) },
                    { cl_in,  new UnitlessDelegate(cl_in, vcalc.SetCl) },
                    { thr_in, new ForceDelegate(thr_in, vcalc.SetThr, thrUnit) },
                    { bf_in,  new ForceDelegate(bf_in, vcalc.SetBf, bfUnit) },
                    { rl_in,  new DistanceDelegate(rl_in, vcalc.SetRl, rlUnit) },
                    { csa_in, new AreaDelegate(csa_in, vcalc.SetCsa, csaUnit) },
                    { cd_in,  new UnitlessDelegate(cd_in, vcalc.SetCd) },
                    { rtr_in, new UnitlessDelegate(rtr_in, vcalc.SetRtr) }
                };
            var units = new ComboBox[] { weightUnit, oatUnit, qfeUnit, lsaUnit, thrUnit, bfUnit, rlUnit, csaUnit };
            var inputs = new NumericUpDown[] { gw_in, oat_in, qfe_in, lsa_in, thr_in, bf_in, rl_in, csa_in };
            var pairs = Enumerable.Zip(units, inputs, (key, value) => new { key, value });
            foreach (var pair in pairs)
            {
                unit_map.Add(pair.key, model_map[pair.value]);
                pair.key.SelectedIndex = 0;
                pair.key.SelectedIndexChanged += new System.EventHandler(UnitChanged);
            }
        }

        private void ProfileChanged(object? sender, EventArgs e)
        {
            ComboBox? cb = sender as ComboBox;
            if (cb == null || cb.SelectedIndex == lastProfileIndex) return;
            lastProfileIndex = cb.SelectedIndex;
            bool enabled = cb.SelectedIndex == 0;
            foreach (var input in fixed_inputs) input.Enabled = enabled; // (un)lock
            if (cb.SelectedIndex > 0) // locked, now load!
            {
                var ap = AircraftProfile.Indexer[cb.SelectedIndex];
                ImmutableArray<decimal>.Enumerator iter = ap.GetEnumerator();
                Queue<int> backups = new Queue<int>(); // for backing up the units...
                foreach (var input in profile_inputs)
                {
                    if (model_map[input].Unit != null) // change unit only if there is one...
                    {
                        backups.Enqueue(model_map[input].Unit.SelectedIndex);
                        model_map[input].LastIndex = 0; // to prevent converters...
                        model_map[input].Unit.SelectedIndex = 0; // set metric, triggers "UnitChanged"...
                    }
                    iter.MoveNext();
                    input.Value = iter.Current;
                    if (model_map[input].Unit != null) 
                        model_map[input].Unit.SelectedIndex = backups.Dequeue(); // restore unit...
                }
            }
        }

        private void NumericUpDown_Focus(object? sender, EventArgs e)
        {
            NumericUpDown? nud = sender as NumericUpDown;
            if (nud != null) nud.Select(0, nud.ToString().Length);
        }

        private void CalcV1(object? sender, EventArgs e)
        {
            (double eas, double tas) = vcalc.CalcV1();
            v1eas_output.Text = Converter.mps2kts(eas).ToString("N2");
            v1tas_output.Text = Converter.mps2kts(tas).ToString("N2");
        }

        private void CalcV2(object? sender, EventArgs e)
        {
            (double eas, double tas) = vcalc.CalcVs();
            vs_eas_output.Text = Converter.mps2kts(eas).ToString("N2");
            vs_tas_output.Text = Converter.mps2kts(tas).ToString("N2");
        }

        private void UnitChanged(object? sender, EventArgs e)
        {
            ComboBox? cb = sender as ComboBox;
            if (cb == null || cb.SelectedIndex == unit_map[cb].LastIndex) return; // no change in selection, thus nothing to do...
            NumericUpDown input = unit_map[cb].Input;
            Func<decimal, decimal> f1 = unit_map[cb].I2M; // imperial to metric
            Func<decimal, decimal> f2 = unit_map[cb].M2I; // metric to imperial
            input.ValueChanged -= new System.EventHandler(UpdateModel); // disable model update since the underlying value stays the same
            input.Value = cb.SelectedIndex == 0 ? f1(input.Value) : f2(input.Value);
            unit_map[cb].LastIndex = cb.SelectedIndex;
            input.ValueChanged += new System.EventHandler(UpdateModel); // re-enable model update...
        }

        private void UpdateModel(object? sender, EventArgs e)
        {
            NumericUpDown? nud = sender as NumericUpDown;
            if (nud == null) return;
            double nudval = (double)nud.Value;
            if (model_map[nud].Unit is null) model_map[nud].Setter(nudval);
            else
            {
                Func<double, double> f1 = model_map[nud].M2SI;
                Func<double, double> f2 = model_map[nud].I2SI;
                nudval = model_map[nud].Unit.SelectedIndex == 0 ? f1(nudval) : f2(nudval);
                model_map[nud].Setter(nudval);
            }
        }
    }
}
