using System;
using System.Windows.Forms;

namespace V_Speeds
{
    internal abstract class UnitDelegate // delegate is perhaps not the right name, but it'll have to do for now...
    {
        private int index;
        public readonly NumericUpDown Input;
        public readonly Func<decimal, decimal> M2I;
        public readonly Func<decimal, decimal> I2M;
        public UnitDelegate(NumericUpDown input,
                Func<decimal, decimal> i2m,
                Func<decimal, decimal> m2i)
        {
            index = 0;
            Input = input;
            I2M = i2m;
            M2I = m2i;
        }

        public int LastIndex { get => index; set => index = value; }
    }

    internal class WeightDelegate : UnitDelegate
    {
        public WeightDelegate(NumericUpDown input) : base(input, Converter.lbs2kgs, Converter.kgs2lbs) { }
    }

    internal class TemperatureDelegate : UnitDelegate
    {
        public TemperatureDelegate(NumericUpDown input) : base(input, Converter.fahr2celc, Converter.celc2fahr) { }
    }

    internal class PressureDelegate : UnitDelegate
    {
        public PressureDelegate(NumericUpDown input) : base(input, Converter.inHg2mbar, Converter.mbar2inHg) { }
    }

    internal class AreaDelegate : UnitDelegate
    {
        public AreaDelegate(NumericUpDown input) : base(input, Converter.sqft2sqm, Converter.sqm2sqft) { }
    }

    internal class ForceDelegate : UnitDelegate
    {
        public ForceDelegate(NumericUpDown input) : base(input, Converter.lbf2newton, Converter.newton2lbf) { }
    }

    internal class DistanceDelegate : UnitDelegate
    {
        public DistanceDelegate(NumericUpDown input) : base(input, Converter.ft2m, Converter.m2ft) { }
    }
}
