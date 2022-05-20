﻿namespace V_Speeds
{
    internal abstract class BaseDelegate // delegate is perhaps not the right name, but it'll have to do for now...
    {
        private int _index;
        private readonly NumericUpDown _input;
        private readonly string _property;
        private readonly (decimal metric, decimal imperial) _increment;
        private readonly ComboBox? _unit;
        private readonly Func<decimal, decimal>? _m2i;
        private readonly Func<decimal, decimal>? _i2m;
        private readonly Func<double, double>? _m2si;
        private readonly Func<double, double>? _i2si;

        public BaseDelegate(NumericUpDown input,
                            string property,
                            (decimal, decimal) increment,
                            ComboBox? unit = null,
                            Func<decimal, decimal>? i2m = null,
                            Func<decimal, decimal>? m2i = null,
                            Func<double, double>? m2si = null,
                            Func<double, double>? i2si = null)
        {
            _index = 0;
            _input = input;
            _property = property;
            _unit = unit;
            _i2m = i2m;
            _m2i = m2i;
            _m2si = m2si;
            _i2si = i2si;
            _increment = increment;
        }

        public int LastIndex { get => _index; set => _index = value; }

        public NumericUpDown Input => _input;

        public Func<decimal, decimal> M2I => _m2i;

        public Func<decimal, decimal> I2M => _i2m;

        public string Property => _property;

        public ComboBox Unit => _unit;

        public Func<double, double> M2SI => _m2si;

        public Func<double, double> I2SI => _i2si;

        public decimal Increment => _index == 0 ? _increment.metric : _increment.imperial;
    }

    internal class WeightDelegate : BaseDelegate
    {
        public WeightDelegate(NumericUpDown input, string property, ComboBox unit, (decimal, decimal) increment) : 
            base(input, property, increment, unit, Converter.lbs2kgs, Converter.kgs2lbs, Converter.do_nothing, Converter.lbs2kgs) { }
    }

    internal class TemperatureDelegate : BaseDelegate
    {
        public TemperatureDelegate(NumericUpDown input, string property, ComboBox unit, (decimal, decimal) increment) :
            base(input, property, increment, unit, Converter.fahr2celc, Converter.celc2fahr, Converter.celc2kel, Converter.fahr2kel) { }
    }

    internal class PressureDelegate : BaseDelegate
    {
        public PressureDelegate(NumericUpDown input, string property, ComboBox unit, (decimal, decimal) increment) :
            base(input, property, increment, unit, Converter.inHg2mbar, Converter.mbar2inHg, Converter.mbar2pa, Converter.inHg2pa) { }
    }

    internal class AreaDelegate : BaseDelegate
    {
        public AreaDelegate(NumericUpDown input, string property, ComboBox unit, (decimal, decimal) increment) :
            base(input, property, increment, unit, Converter.sqft2sqm, Converter.sqm2sqft, Converter.do_nothing, Converter.sqft2sqm) { }
    }

    internal class UnitlessDelegate : BaseDelegate
    {
        public UnitlessDelegate(NumericUpDown input, string property, decimal increment) : base(input, property, (increment, 0m)) { }
    }

    internal class ForceDelegate : BaseDelegate
    {
        public ForceDelegate(NumericUpDown input, string property, ComboBox unit, (decimal, decimal) increment) :
            base(input, property, increment, unit, Converter.lbf2newton, Converter.newton2lbf, Converter.do_nothing, Converter.lbf2newton) { }
    }

    internal class DistanceDelegate : BaseDelegate
    {
        public DistanceDelegate(NumericUpDown input, string property, ComboBox unit, (decimal, decimal) increment) :
            base(input, property, increment, unit, Converter.ft2m, Converter.m2ft, Converter.do_nothing, Converter.ft2m) { }
    }
}
