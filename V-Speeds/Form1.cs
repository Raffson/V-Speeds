namespace V_Speeds
{
    public partial class Form1 : Form
    {
        private readonly V_Calculator vcalc = new();

        // mapping numericUpDowns to their setter-function for model, combobox + converters to ensure metric data is passed...
        private readonly Dictionary<NumericUpDown, BaseDelegate> model_map;

        // mapping unit to a delegate with lastSelectedIndex, respective numericUpDown, Conversion functions
        private readonly Dictionary<ComboBox, BaseDelegate> unit_map;

        // list of inputs to be (un)locked when profile is selected...
        private readonly NumericUpDown[] fixed_inputs;

        // list of inputs to overwrite when profile is selected...
        private readonly NumericUpDown[] profile_inputs;


        private int lastProfileIndex = 0;

        private void InitializeDictionaries(out Dictionary<NumericUpDown, BaseDelegate> model_map, out Dictionary<ComboBox, BaseDelegate> unit_map)
        {
            model_map = new Dictionary<NumericUpDown, BaseDelegate> {
                    { gw_in,  new WeightDelegate(gw_in, "Gw", weightUnit, (100m, 100m)) },
                    { oat_in, new TemperatureDelegate(oat_in, "Oat", oatUnit, (0.1m, 0.1m)) },
                    { qfe_in, new PressureDelegate(qfe_in, "Qfe", qfeUnit, (1m, 0.01m)) },
                    { lsa_in, new AreaDelegate(lsa_in, "Lsa", lsaUnit, (1m, 1m)) },
                    { cl_in,  new UnitlessDelegate(cl_in, "Cl", 0.001m) },
                    { thr_in, new ForceDelegate(thr_in, "Thr", thrUnit, (1000m, 1000m)) },
                    { bf_in,  new ForceDelegate(bf_in, "Bf", bfUnit, (100m, 100m)) },
                    { rl_in,  new DistanceDelegate(rl_in, "Rl", rlUnit, (50m, 100m)) },
                    { rc_in,  new UnitlessDelegate(rc_in, "Rc", 0.1m) },
                    { cd_in,  new UnitlessDelegate(cd_in, "Cd", 0.001m) },
                    { rtr_in, new UnitlessDelegate(rtr_in, "Rtr", 0.01m) },
                    { clg_in, new UnitlessDelegate(clg_in, "Clg", 0.001m) },
                    { rfc_in, new UnitlessDelegate(rfc_in, "Rfc", 0.001m) },
                };
            // Link unitmap...
            unit_map = new();
            var units = new ComboBox[] { weightUnit, oatUnit, qfeUnit, lsaUnit, thrUnit, bfUnit, rlUnit };
            var inputs = new NumericUpDown[] { gw_in, oat_in, qfe_in, lsa_in, thr_in, bf_in, rl_in };
            var pairs = Enumerable.Zip(units, inputs, (key, value) => new { key, value });
            foreach (var pair in pairs)
            {
                unit_map.Add(pair.key, model_map[pair.value]);
                pair.key.SelectedIndex = 0;
                pair.key.SelectedIndexChanged += new EventHandler(UnitChanged);
            }
        }

        public Form1()
        {
            System.Globalization.CultureInfo customCulture = 
                (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            customCulture.NumberFormat.NumberGroupSeparator = ",";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            
            InitializeComponent();

            fixed_inputs = new NumericUpDown[] { lsa_in, cl_in, bf_in, rtr_in, clg_in, rfc_in };
            profile_inputs = new NumericUpDown[] { lsa_in, cl_in, bf_in, rc_in, cd_in, rtr_in, thr_in, clg_in, rfc_in };
            InitializeDictionaries(out model_map, out unit_map);

            foreach(var aircraft in Enum.GetValues<AircraftType>()) apSelect.Items.Add(aircraft.DisplayName());
            apSelect.SelectedIndex = 0;
            apSelect.SelectedIndexChanged += new EventHandler(ProfileChanged);
        }

        private void ProfileChanged(object? sender, EventArgs e)
        {
            if (sender is not ComboBox cb || cb.SelectedIndex == lastProfileIndex) return;
            lastProfileIndex = cb.SelectedIndex;
            bool enabled = cb.SelectedIndex == 0;
            foreach (var input in fixed_inputs) input.Enabled = enabled; // (un)Lock
            // & Load...
            var aircraftString = (string)cb.SelectedItem;
            vcalc.Craft = AircraftFactory.CreateAircraft(aircraftString.AircraftTypeFromString());
            vcalc.Gw = (double)gw_in.Value;
            abcb.Checked = false;
            abcb.Visible = vcalc.Craft.HasAfterburner();

            int backup = 0; // for backing up the unit...
            foreach (var input in profile_inputs)
            {
                input.ValueChanged -= new EventHandler(UpdateModel); // no model update needed now...

                if (model_map[input].Unit is not null) // change unit only if there is one...
                {
                    backup = model_map[input].Unit.SelectedIndex;
                    model_map[input].LastIndex = 0; // to prevent converters in "UnitChanged"
                    model_map[input].Unit.SelectedIndex = 0; // set metric, triggers "UnitChanged"
                }
                input.Value = (decimal)(double)vcalc[model_map[input].Property]; // triggers "UpdateModel"
                if (model_map[input].Unit is not null) 
                    model_map[input].Unit.SelectedIndex = backup; // restore unit...

                input.ValueChanged += new EventHandler(UpdateModel); // re-enable model update...
            }
        }

        private void NumericUpDown_Focus(object? sender, EventArgs e)
        {
            if (sender is NumericUpDown nud) nud.Select(0, nud.ToString().Length);
        }

        private void UnitChanged(object? sender, EventArgs e)
        {
            if (sender is not ComboBox cb || cb.SelectedIndex == unit_map[cb].LastIndex) return; // no change in selection, thus nothing to do...
            NumericUpDown input = unit_map[cb].Input;
            input.ValueChanged -= new EventHandler(UpdateModel); // disable model update since the underlying value stays the same
            input.Value = cb.SelectedIndex == 0 ? unit_map[cb].I2M(input.Value) : unit_map[cb].M2I(input.Value);
            unit_map[cb].LastIndex = cb.SelectedIndex;
            input.Increment = unit_map[cb].Increment;
            input.ValueChanged += new EventHandler(UpdateModel); // re-enable model update...
        }

        private void UpdateModel(object? sender, EventArgs e)
        {
            if (sender is not NumericUpDown nud) return;
            double nudval = (double)nud.Value;
            if (model_map[nud].Unit is null) vcalc[model_map[nud].Property] = nudval;
            else vcalc[model_map[nud].Property] = model_map[nud].Unit.SelectedIndex == 0 ? 
                    model_map[nud].M2SI(nudval) : model_map[nud].I2SI(nudval);
        }

        private void DoCalculations(object sender, EventArgs e)
        {
            double ceil = 0; // after debugging, round numbers up (+1 and truncate)...
            
            (double eas, double tas) = vcalc.CalcV1();
            v1eas_output.Text = $"{(Converter.mps2kts(eas) + ceil):N2}";
            v1tas_output.Text = $"{(Converter.mps2kts(tas) + ceil):N2}";

            (eas, tas) = vcalc.CalcVs(false); // account for full power
            vs_eas_output.Text = $"{(Converter.mps2kts(eas) + ceil):N2} - ";
            vs_tas_output.Text = $"{(Converter.mps2kts(tas) + ceil):N2} - ";
            
            (eas, tas) = vcalc.CalcVs(); // consider no thrust
            vs_eas_output.Text += $"{(Converter.mps2kts(eas) + ceil):N2}";
            vs_tas_output.Text += $"{(Converter.mps2kts(tas) + ceil):N2}";

            double dvFT = vcalc.CalcNeededRunway(false); // Provide Dv wrt Vs at full thrust, will show a lower number!!!
            double dvIT = vcalc.CalcNeededRunway(true);  // Provide Dv wrt Vs at idle thrust, will show a higher number!!!
            double mtow = vcalc.CalcMTOW();
            
            dv_m_output.Text = $"{(dvFT + ceil):N2} - {(dvIT + ceil):N2}";
            dv_ft_output.Text = $"{(Converter.m2ft(dvFT) + ceil):N2} - {(Converter.m2ft(dvIT) + ceil):N2}";
            
            (dv_m_output.ForeColor, dv_ft_output.ForeColor) = dvIT > vcalc.Rl || double.IsNaN(dvIT) ? (Color.Red, Color.Red) : (Color.Black, Color.Black);
            
            mtow_kg_output.Text = $"{mtow:N0}";
            mtow_lbs_output.Text = $"{Converter.kgs2lbs(mtow):N0}";
            
            if (double.IsNaN(dvIT) && double.IsNaN(dvFT))
                MessageBox.Show("Given configuration can't reach Vs!", "WARNING!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void AfterburnerToggle(object? sender, EventArgs e)
        {
            if (vcalc.Craft is not Aircrafts.IAfterburnable ac) return; // FUBAR...
            ac.AB = abcb.Checked;
            (abcb.Text, abcb.ForeColor) = abcb.Checked ? ("AB", Color.Red) : ("MIL", Color.FromKnownColor(KnownColor.HotTrack));
        }
    }
}
