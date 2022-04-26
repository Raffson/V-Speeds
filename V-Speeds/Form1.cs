﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace V_Speeds
{
    public partial class Form1 : Form
    {
        private readonly V_Calculator vcalc = new V_Calculator();

        // mapping unit to a delegate with lastSelectedIndex, respective numericUpDown, Conversion functions
        private readonly Dictionary<ComboBox, UnitDelegate> unit_map;

        // mapping numericUpDowns to their setter-function for model, combobox + converters to ensure metric data is passed...
        private readonly Dictionary<NumericUpDown, (Action<double>, ComboBox, Func<double, double>, Func<double, double>)> model_map;

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
            profile_inputs = new NumericUpDown[] { lsa_in, cl_in, bf_in, csa_in, cd_in, rtr_in };
            apSelect.SelectedIndex = 0;
            apSelect.SelectedIndexChanged += new System.EventHandler(ProfileChanged);
            unit_map = new Dictionary<ComboBox, UnitDelegate> {
                    { weightUnit, new WeightDelegate(gw_in) },
                    { oatUnit, new TemperatureDelegate(oat_in) },
                    { qfeUnit, new PressureDelegate(qfe_in) },
                    { lsaUnit, new AreaDelegate(lsa_in) },
                    { thrUnit, new ForceDelegate(thr_in) },
                    { bfUnit,  new ForceDelegate(bf_in) },
                    { rlUnit,  new DistanceDelegate(rl_in) },
                    { csaUnit, new AreaDelegate(csa_in) }
                };
            foreach (var entry in unit_map)
            {
                entry.Key.SelectedIndex = 0;
                entry.Key.SelectedIndexChanged += new System.EventHandler(UnitChanged);
            }
            model_map = new Dictionary<NumericUpDown, (Action<double>, ComboBox, Func<double, double>, Func<double, double>)> {
                    { gw_in, (vcalc.SetGw, weightUnit, Converter.do_nothing, Converter.lbs2kgs) },
                    { oat_in, (vcalc.SetOat, oatUnit, Converter.celc2kel, Converter.fahr2kel) },
                    { qfe_in, (vcalc.SetQfe, qfeUnit, Converter.mbar2pa, Converter.inHg2pa) },
                    { lsa_in, (vcalc.SetLsa, lsaUnit, Converter.do_nothing, Converter.sqft2sqm) },
                    { cl_in, (vcalc.SetCl, null, null, null) },
                    { thr_in, (vcalc.SetThr, thrUnit, Converter.do_nothing, Converter.lbf2newton) },
                    { bf_in, (vcalc.SetBf, bfUnit, Converter.do_nothing, Converter.lbf2newton) },
                    { rl_in, (vcalc.SetRl, rlUnit, Converter.do_nothing, Converter.ft2m) },
                    { csa_in, (vcalc.SetCsa, csaUnit, Converter.do_nothing, Converter.sqft2sqm) },
                    { cd_in, (vcalc.SetCd, null, null, null) },
                    { rtr_in, (vcalc.SetRtr, null, null, null) }
                };
        }

        private void ProfileChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedIndex == lastProfileIndex) return;
            lastProfileIndex = cb.SelectedIndex;
            bool enabled = cb.SelectedIndex == 0 ? true : false;
            foreach (var input in fixed_inputs) input.Enabled = enabled; // (un)lock
            if (cb.SelectedIndex > 0) // locked, now load!
            {
                Queue<int> backups = new Queue<int>();
                foreach (var input in profile_inputs)
                {
                    if (model_map[input].Item2 != null) // change unit only if there is one...
                    {
                        backups.Enqueue(model_map[input].Item2.SelectedIndex);
                        unit_map[model_map[input].Item2].LastIndex = 0; // to prevent converters...
                        model_map[input].Item2.SelectedIndex = 0; // set metric, triggers "UnitChanged"...
                    }
                }
                var profile = AircraftProfile.Indexer[cb.SelectedIndex];
                (lsa_in.Value, cl_in.Value, bf_in.Value, csa_in.Value, cd_in.Value, rtr_in.Value) = profile;
                foreach (var input in profile_inputs) // restore units...
                    if (model_map[input].Item2 != null)
                        model_map[input].Item2.SelectedIndex = backups.Dequeue();
            }
        }

        private void NumericUpDown_Focus(object sender, EventArgs e)
        {
            NumericUpDown nud = sender as NumericUpDown;
            nud.Select(0, nud.ToString().Length);
        }

        private void CalcV1(object sender, EventArgs e)
        {
            v1_output.Text = Converter.mps2kts(vcalc.CalcV1()).ToString("N2");
        }

        private void CalcV2(object sender, EventArgs e)
        {
            v2_output.Text = Converter.mps2kts(vcalc.CalcV2()).ToString("N2");
        }

        private void UnitChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedIndex == unit_map[cb].LastIndex) return; // no change in selection, thus nothing to do...
            NumericUpDown input = unit_map[cb].Input;
            Func<decimal, decimal> f1 = unit_map[cb].I2M; // imperial to metric
            Func<decimal, decimal> f2 = unit_map[cb].M2I; // metric to imperial
            input.ValueChanged -= new System.EventHandler(UpdateModel); // disable model update since the underlying value stays the same
            input.Value = cb.SelectedIndex == 0 ? f1(input.Value) : f2(input.Value);
            unit_map[cb].LastIndex = cb.SelectedIndex;
            input.ValueChanged += new System.EventHandler(UpdateModel); // re-enable model update...
        }

        private void UpdateModel(object sender, EventArgs e)
        {
            NumericUpDown nud = sender as NumericUpDown;
            double nudval = (double)nud.Value;
            if (model_map[nud].Item2 is null) model_map[nud].Item1(nudval);
            else
            {
                Func<double, double> f1 = model_map[nud].Item3;
                Func<double, double> f2 = model_map[nud].Item4;
                nudval = model_map[nud].Item2.SelectedIndex == 0 ? f1(nudval) : f2(nudval);
                model_map[nud].Item1(nudval);
            }
        }
    }
}
