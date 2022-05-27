using V_Speeds.ConstantsAndConverters;
using V_Speeds.Model.Aircrafts;
using V_Speeds.ObserverPattern;
using System.Reflection;

namespace V_Speeds
{
    public partial class Form1 : Form, IMyObserver<V_Calculator>
    {
        private readonly V_Calculator vcalc = new();

        // mapping numericUpDowns to their setter-function for model, combobox + converters to ensure metric data is passed
        private readonly Dictionary<NumericUpDown, BaseDelegate> model_map;

        // mapping unit to a delegate with lastSelectedIndex, respective numericUpDown, Conversion functions
        private readonly Dictionary<ComboBox, BaseDelegate> unit_map;

        // mapping Property names to their inputs
        private readonly Dictionary<string, NumericUpDown> prop_map;

        // list of inputs to be (un)locked when profile is selected
        private readonly NumericUpDown[] fixed_inputs;

        // keep track of last selected profile to prevent unnecessary updates to the model
        private int lastProfileIndex = 0;

        // keep track of last controller input to prevent unnecessary updates to the view
        private NumericUpDown? lastControllerInput = null;

        // keep track of the last loaded assembly for external aircraft
        private Assembly? extDLL = null;

        // keep track of the last selected type from extDLL
        private Type? extType = null;

        private void InitializeDictionaries(out Dictionary<NumericUpDown, BaseDelegate> model_map,
                                            out Dictionary<ComboBox, BaseDelegate> unit_map,
                                            out Dictionary<string, NumericUpDown> prop_map)
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
            // Build unit_map
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
            // Build prop_map
            prop_map = new();
            foreach(var pair in model_map) prop_map.Add(pair.Value.Property, pair.Key);
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
            InitializeDictionaries(out model_map, out unit_map, out prop_map);

            foreach(var aircraft in Enum.GetValues<AircraftType>()) apSelect.Items.Add(aircraft.DisplayName());
            apSelect.SelectedIndex = 0;
            apSelect.SelectedIndexChanged += new EventHandler(ProfileChanged);

            vcalc.Subscribe(this);
        }

        private Type? SelectExternalAircraft(List<Type?> validTypes)
        {
            MessageBox.Show("Multiple aircraft were found in the supplied DLL file.\nPlease choose a specific aircraft in the next pop-up screen...",
                "Multiple aircraft found!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            
            Form prompt = new()
            {
                Text = "Select an aircraft...",
                Width = 400,
                Height = 200
            };
            
            ComboBox inputBox = new()
            {
                Left = 50,
                Top = 50,
                DataSource = validTypes,
                Width = 300,
            };
            
            Button confirmation = new() { Text = "Continue", Left = 50, Top = 100, AutoSize = true, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            
            Button cancel = new() { Text = "Cancel", Left = 200, Top = 100, AutoSize = true, DialogResult = DialogResult.Cancel };
            cancel.Click += (sender, e) => { prompt.Close(); };
            
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(cancel);
            prompt.Controls.Add(inputBox);
            var result = prompt.ShowDialog();
            if (result == DialogResult.OK) return (inputBox.SelectedItem as Type);
            else if (result == DialogResult.Cancel) return vcalc.Craft.GetType(); // type of current aircraft?
            else return null;
        }

        private void LoadExternalDLL()
        {
            OpenFileDialog ofd = new()
            {
                InitialDirectory = Environment.CurrentDirectory,
                Filter = "DLL files (*.dll)|*.dll",
                FilterIndex = 2,
                RestoreDirectory = true
            };

            if (ofd.ShowDialog() == DialogResult.OK && ofd.FileName is string fname && fname != string.Empty)
            {
                // load assembly and check if it's a valid DLL...
                extDLL = Assembly.LoadFrom(fname);
                var validTypes = new List<Type?>();
                foreach(var type in extDLL.GetTypes())
                {
                    if (type.BaseType == typeof(Aircraft) || type.BaseType == typeof(AircraftAB))
                        validTypes.Add(type);
                }

                if (validTypes.Count > 1) // choose one...
                    extType = SelectExternalAircraft(validTypes);
                else if (validTypes.Count == 1)
                    extType = validTypes.First();

                if (extType is not null)
                {
                    if (extType != vcalc.Craft.GetType())
                    {
                        object? instance = Activator.CreateInstance(extType);
                        if (instance is Aircraft ac)
                        {
                            vcalc.Craft = ac;
                            dllName.Text = extType.Name;
                            dllName.Visible = true;
                        }
                        else apSelect.SelectedIndex = lastProfileIndex; // FUBAR in this case, revert to last selected index...
                    }
                    else apSelect.SelectedIndex = lastProfileIndex; // cancelled in SelectExternalAircraft, revert to last selected index...
                }
                else
                {
                    MessageBox.Show("No valid aircraft were found in the specified DLL.\nCancelling operation!",
                        "No aircraft found!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    apSelect.SelectedIndex = lastProfileIndex;  // no valid type was found, revert to last selected index...
                }
            }
            else apSelect.SelectedIndex = lastProfileIndex;  // cancelled FileDialog, revert to last selected index...
        }

        private void ProfileChanged(object? sender, EventArgs e)
        {
            if (sender is not ComboBox cb || cb.SelectedIndex == lastProfileIndex) return;
            dllName.Visible = false;
            // Load... Observer Pattern takes care of the rest...
            if (cb.SelectedIndex > 0 && cb.SelectedItem is string aircraftString)
            {
                var actype = aircraftString.AircraftTypeFromString();
                if (actype == AircraftType.External)
                    LoadExternalDLL();
                else
                    vcalc.Craft = AircraftFactory.CreateAircraft(actype);
            }
            else vcalc.Craft = new Aircraft(vcalc.Craft);
            abcb.Checked = false;
            abcb.Visible = vcalc.Craft.HasAfterburner();
            bool enabled = cb.SelectedIndex == 0;
            foreach (var input in fixed_inputs) input.Enabled = enabled; // (un)Lock
            lastProfileIndex = cb.SelectedIndex;
        }

        private void ResetProfile(object sender, EventArgs e)
        {
            if (apSelect.SelectedItem is string aircraftString)
            {
                var actype = aircraftString.AircraftTypeFromString();
                if (actype != AircraftType.External)
                {
                    vcalc.Craft = AircraftFactory.CreateAircraft(actype);
                    abcb.Checked = false;
                }
                else if (extType is not null)
                {
                    if (Activator.CreateInstance(extType) is Aircraft ac)
                        vcalc.Craft = ac;
                }
                // else should not be the case, would mean something got seriously FUBAR...
                else MessageBox.Show("Can't reset external profile! No type was registered...", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else MessageBox.Show("Can't reset profile! Selected profile is invalid...", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void NumericUpDown_Focus(object? sender, EventArgs e)
        {
            if (sender is NumericUpDown nud) nud.Select(0, nud.ToString().Length);
        }

        private void UnitChanged(object? sender, EventArgs e)
        {
            if (sender is not ComboBox cb || cb.SelectedIndex == unit_map[cb].LastIndex) return; // no change in selection, thus nothing to do...
            if (unit_map[cb].I2M is not Func<decimal, decimal> i2m || unit_map[cb].M2I is not Func<decimal, decimal> m2i) return; // FUBAR?
            NumericUpDown input = unit_map[cb].Input;
            input.ValueChanged -= new EventHandler(UpdateModel); // disable model update since the underlying value stays the same
            input.Value = cb.SelectedIndex == 0 ? i2m(input.Value) : m2i(input.Value);
            unit_map[cb].LastIndex = cb.SelectedIndex;
            input.Increment = unit_map[cb].Increment;
            input.ValueChanged += new EventHandler(UpdateModel); // re-enable model update...
        }

        private void UpdateModel(object? sender, EventArgs e)
        {
            if (sender is not NumericUpDown nud) return;
            double nudval = (double)nud.Value;
            lastControllerInput = nud;
            if (model_map[nud].Unit is ComboBox unit && model_map[nud].M2SI is Func<double, double> m2si
                                                     && model_map[nud].I2SI is Func<double, double> i2si)
                vcalc[model_map[nud].Property] = unit.SelectedIndex == 0 ? m2si(nudval) : i2si(nudval);
            else vcalc[model_map[nud].Property] = nudval;
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
            if (vcalc.Craft is IAfterburnable ac)
            {
                ac.AB = abcb.Checked;
                (abcb.Text, abcb.ForeColor) = abcb.Checked ? ("AB", Color.Red) : ("MIL", Color.FromKnownColor(KnownColor.HotTrack));
            }
            // else FUBAR...
        }

        // Observer Pattern Stuff
        public void Update(V_Calculator vc) // update entire view
        {
            foreach ( var (input, dlgt) in model_map.Select(x => (x.Key, x.Value)) )
            {
                if (vc[dlgt.Property] is double value)
                {
                    input.ValueChanged -= new EventHandler(UpdateModel); // no model update needed now...
                    if (dlgt.Unit is ComboBox unit && dlgt.SI2M is Func<double, double> si2m && dlgt.SI2I is Func<double, double> si2i)
                    {
                        input.Value = unit.SelectedIndex == 0 ? (decimal)si2m(value) : (decimal)si2i(value);
                    }
                    else input.Value = (decimal)value;
                    input.ValueChanged += new EventHandler(UpdateModel); // re-enable model update...
                }
            }
        }

        public void Update(string property)
        {
            //System.Diagnostics.Debug.WriteLine(property);
            if (vcalc[property] is double value && lastControllerInput != prop_map[property])
            {
                //System.Diagnostics.Debug.WriteLine(property);
                var input = prop_map[property];
                var dlgt = model_map[input];
                input.ValueChanged -= new EventHandler(UpdateModel); // no model update needed now...
                if (dlgt.Unit is ComboBox unit && dlgt.SI2M is Func<double, double> si2m && dlgt.SI2I is Func<double, double> si2i)
                {
                    input.Value = unit.SelectedIndex == 0 ? (decimal)si2m(value) : (decimal)si2i(value);
                }
                else input.Value = (decimal)value;
                input.ValueChanged += new EventHandler(UpdateModel); // re-enable model update...
            }
            lastControllerInput = null;
        }
    }
}
