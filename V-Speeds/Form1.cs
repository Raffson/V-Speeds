using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace V_Speeds
{
    public partial class Form1 : Form
    {
        private readonly V_Calculator vcalc = new V_Calculator();

        private readonly Dictionary<string, int> unit_indices = new Dictionary<string, int> { {"weightUnit", 0}, {"oatUnit", 0}, {"qfeUnit", 0}, {"lsaUnit", 0},
                                                                                     {"thrUnit", 0}, {"bfUnit", 0}, {"rlUnit", 0}, {"csaUnit", 0} };

        public Form1()
        {
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            customCulture.NumberFormat.NumberGroupSeparator = ",";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            InitializeComponent();
            ComboBox[] units = new ComboBox[] { weightUnit, oatUnit, qfeUnit, lsaUnit, thrUnit, bfUnit, rlUnit, csaUnit};
            foreach (var unit in units)
            {
                unit.SelectedIndex = 0;
                unit.SelectedIndexChanged += new System.EventHandler(UnitChanged);
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
            if (cb.SelectedIndex == unit_indices[cb.Name]) return;
            var fi = new Dictionary<string, NumericUpDown> { { "thrUnit", thr_input }, { "bfUnit", bf_input } }; //force inputs...
            var ai = new Dictionary<string, NumericUpDown> { { "lsaUnit", lsa_input }, { "csaUnit", csa_input } }; //area inputs...
            switch (cb.Name)
            {
                case "weightUnit":
                    gw_input.Value = cb.SelectedIndex == 0 ? Converter.lbs2kgs(gw_input.Value) : Converter.kgs2lbs(gw_input.Value);
                    break;
                case "oatUnit":
                    oat_input.Value = cb.SelectedIndex == 0 ? Converter.fahr2celc(oat_input.Value) : Converter.celc2fahr(oat_input.Value);
                    break;
                case "qfeUnit":
                    qfe_input.Value = cb.SelectedIndex == 0 ? Converter.inHg2mbar(qfe_input.Value) : Converter.mbar2inHg(qfe_input.Value);
                    break;
                case "lsaUnit":
                case "csaUnit":
                    ai[cb.Name].Value = cb.SelectedIndex == 0 ? Converter.sqft2sqm(ai[cb.Name].Value) : Converter.sqm2sqft(ai[cb.Name].Value);
                    break;
                case "thrUnit":
                case "bfUnit":
                    fi[cb.Name].Value = cb.SelectedIndex == 0 ? Converter.lbf2newton(fi[cb.Name].Value) : Converter.newton2lbf(fi[cb.Name].Value);
                    break;
                case "rlUnit":
                    rl_input.Value = cb.SelectedIndex == 0 ? Converter.ft2m(rl_input.Value) : Converter.m2ft(rl_input.Value);
                    break;
                default:
                    break;
            }
            unit_indices[cb.Name] = cb.SelectedIndex;
        }

        private void UpdateModel(object sender, EventArgs e)
        {
            NumericUpDown nud = sender as NumericUpDown;
            double nudval = (double)nud.Value;
            switch (nud.Name)
            {
                case "gw_input":
                    vcalc.Gw = weightUnit.SelectedIndex == 0 ? nudval : Converter.lbs2kgs(nudval);
                    break;
                case "oat_input":
                    vcalc.Oat = oatUnit.SelectedIndex == 0 ? Converter.celc2kel(nudval) : Converter.fahr2kel(nudval);
                    break;
                case "qfe_input":
                    vcalc.Qfe = qfeUnit.SelectedIndex == 0 ? Converter.mbar2pa(nudval) : Converter.inHg2pa(nudval);
                    break;
                case "lsa_input":
                    vcalc.Lsa = lsaUnit.SelectedIndex == 0 ? nudval : Converter.sqft2sqm(nudval);
                    break;
                case "cl_input":
                    vcalc.Cl = nudval;
                    break;
                case "thr_input":
                    vcalc.Thr = thrUnit.SelectedIndex == 0 ? nudval : Converter.lbf2newton(nudval);
                    break;
                case "bf_input":
                    vcalc.Bf = bfUnit.SelectedIndex == 0 ? nudval : Converter.lbf2newton(nudval);
                    break;
                case "rl_input":
                    vcalc.Rl = rlUnit.SelectedIndex == 0 ? nudval : Converter.ft2m(nudval);
                    break;
                case "csa_input":
                    vcalc.Csa = csaUnit.SelectedIndex == 0 ? nudval : Converter.sqft2sqm(nudval);
                    break;
                case "cd_input":
                    vcalc.Cd = nudval;
                    break;
                case "rtr_input":
                    vcalc.Rtr = nudval;
                    break;
                default:
                    break;
            }
        }
    }
}
