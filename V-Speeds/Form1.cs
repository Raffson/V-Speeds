﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace V_Speeds
{
    public partial class Form1 : Form
    {
        private class MyIndex // 'int'-wrapper for use with tuple, as I can't change an 'int' value in after tuple was made...
        {
            private int index;

            public MyIndex(int i = 0) => index = i;

            public int Index { get => index; set => index = value; }
        }

        private readonly V_Calculator vcalc = new V_Calculator();

        // mapping unitname to tuple with lastSelectedIndex, respective numericUpDown, Conversion functions -> see constructor
        private readonly Dictionary<ComboBox, (MyIndex, NumericUpDown, Func<decimal, decimal>, Func<decimal, decimal>)> unit_map;

        // mapping numericUpDowns to their setter-function for model, combobox + converters to ensure metric data is passed...
        private readonly Dictionary<NumericUpDown, (Action<double>, ComboBox, Func<double, double>, Func<double, double>)> model_map;

        // list of inputs to be (un)locked if profile is selected...
        private readonly NumericUpDown[] fixed_inputs;


        private int lastProfileIndex = 0;

        public Form1()
        {
            System.Globalization.CultureInfo customCulture = 
                (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            customCulture.NumberFormat.NumberGroupSeparator = ",";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            
            InitializeComponent();
            fixed_inputs = new NumericUpDown[] { lsa_input, cl_input, bf_input, csa_input, cd_input, rtr_input };
            apSelect.SelectedIndex = 0;
            apSelect.SelectedIndexChanged += new System.EventHandler(ProfileChanged);
            unit_map = new Dictionary<ComboBox, (MyIndex, NumericUpDown, Func<decimal, decimal>, Func<decimal, decimal>)> {
                    { weightUnit, (new MyIndex(), gw_input, Converter.lbs2kgs, Converter.kgs2lbs) },
                    { oatUnit, (new MyIndex(), oat_input, Converter.fahr2celc, Converter.celc2fahr) },
                    { qfeUnit, (new MyIndex(), qfe_input, Converter.inHg2mbar, Converter.mbar2inHg) },
                    { lsaUnit, (new MyIndex(), lsa_input, Converter.sqft2sqm, Converter.sqm2sqft) },
                    { thrUnit, (new MyIndex(), thr_input, Converter.lbf2newton, Converter.newton2lbf) },
                    { bfUnit, (new MyIndex(), bf_input, Converter.lbf2newton, Converter.newton2lbf) },
                    { rlUnit, (new MyIndex(), rl_input, Converter.ft2m, Converter.m2ft) },
                    { csaUnit, (new MyIndex(), csa_input, Converter.sqft2sqm, Converter.sqm2sqft) }
                };
            foreach (var entry in unit_map)
            {
                entry.Key.SelectedIndex = 0;
                entry.Key.SelectedIndexChanged += new System.EventHandler(UnitChanged);
            }
            model_map = new Dictionary<NumericUpDown, (Action<double>, ComboBox, Func<double, double>, Func<double, double>)> {
                    { gw_input, (vcalc.SetGw, weightUnit, Converter.do_nothing, Converter.lbs2kgs) },
                    { oat_input, (vcalc.SetOat, oatUnit, Converter.celc2kel, Converter.fahr2kel) },
                    { qfe_input, (vcalc.SetQfe, qfeUnit, Converter.mbar2pa, Converter.inHg2pa) },
                    { lsa_input, (vcalc.SetLsa, lsaUnit, Converter.do_nothing, Converter.sqft2sqm) },
                    { cl_input, (vcalc.SetCl, null, null, null) },
                    { thr_input, (vcalc.SetThr, thrUnit, Converter.do_nothing, Converter.lbf2newton) },
                    { bf_input, (vcalc.SetBf, bfUnit, Converter.do_nothing, Converter.lbf2newton) },
                    { rl_input, (vcalc.SetRl, rlUnit, Converter.do_nothing, Converter.ft2m) },
                    { csa_input, (vcalc.SetCsa, csaUnit, Converter.do_nothing, Converter.sqft2sqm) },
                    { cd_input, (vcalc.SetCd, null, null, null) },
                    { rtr_input, (vcalc.SetRtr, null, null, null) }
                };
        }

        private void ProfileChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedIndex == lastProfileIndex) return;
            lastProfileIndex = cb.SelectedIndex;
            if (cb.SelectedIndex == 0) // unlock
                foreach (var input in fixed_inputs) input.Enabled = true;
            else // lock & load
            {
                //vcalc.SetProfile(AircraftProfile.Indexer[cb.SelectedIndex]);
                foreach (var input in fixed_inputs)
                {
                    // update actual value in input, must first set units to metric...
                    if (model_map[input].Item2 != null)
                    {
                        unit_map[model_map[input].Item2].Item1.Index = 0; // to prevent converters...
                        model_map[input].Item2.SelectedIndex = 0; // set metric, triggers "UnitChanged"...
                    }
                    input.Enabled = false;
                }
                var profile = AircraftProfile.Indexer[cb.SelectedIndex];
                (lsa_input.Value, cl_input.Value, bf_input.Value, csa_input.Value, cd_input.Value, rtr_input.Value) = profile;
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
            if (cb.SelectedIndex == unit_map[cb].Item1.Index) return; // no change in selection, thus nothing to do...
            NumericUpDown input = unit_map[cb].Item2;
            Func<decimal, decimal> f1 = unit_map[cb].Item3; // imperial to metric
            Func<decimal, decimal> f2 = unit_map[cb].Item4; // metric to imperial
            input.ValueChanged -= new System.EventHandler(UpdateModel); // disable model update since the underlying value stays the same
            unit_map[cb].Item2.Value = cb.SelectedIndex == 0 ? f1(input.Value) : f2(input.Value);
            unit_map[cb].Item1.Index = cb.SelectedIndex;
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
