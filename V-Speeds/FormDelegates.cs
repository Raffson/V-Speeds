using System;
using System.Windows.Forms;

namespace V_Speeds
{
    internal interface IUnitDelegate
    {
        int LastIndex { get; set; }
        NumericUpDown Input { get; }
        Func<decimal, decimal> M2I { get; }
        Func<decimal, decimal> I2M { get;  }
    }

    internal interface IModelDelegate
    {
        Action<double> Setter { get; }
        ComboBox Unit { get; }
        Func<double, double> M2SI { get; }
        Func<double, double> I2SI { get; }

    }

    internal abstract class BaseDelegate : IUnitDelegate, IModelDelegate // delegate is perhaps not the right name, but it'll have to do for now...
    {
        private int _index;
        private readonly NumericUpDown _input;
        private readonly Action<double> _setter;
        private readonly ComboBox? _unit;
        private readonly Func<decimal, decimal>? _m2i;
        private readonly Func<decimal, decimal>? _i2m;
        private readonly Func<double, double>? _m2si;
        private readonly Func<double, double>? _i2si;

        public BaseDelegate(NumericUpDown input,
                            Action<double> setter,
                            ComboBox? unit = null,
                            Func<decimal, decimal>? i2m = null,
                            Func<decimal, decimal>? m2i = null,
                            Func<double, double>? m2si = null,
                            Func<double, double>? i2si = null)
        {
            _index = 0;
            _input = input;
            _setter = setter;
            _unit = unit;
            _i2m = i2m;
            _m2i = m2i;
            _m2si = m2si;
            _i2si = i2si;
        }

        public int LastIndex { get => _index; set => _index = value; }

        public NumericUpDown Input => _input;

        public Func<decimal, decimal> M2I => _m2i;

        public Func<decimal, decimal> I2M => _i2m;

        public Action<double> Setter => _setter;

        public ComboBox Unit => _unit;

        public Func<double, double> M2SI => _m2si;

        public Func<double, double> I2SI => _i2si;
    }

    internal class WeightDelegate : BaseDelegate
    {
        public WeightDelegate(NumericUpDown input, Action<double> setter, ComboBox unit) : 
            base(input, setter, unit, Converter.lbs2kgs, Converter.kgs2lbs, Converter.do_nothing, Converter.lbs2kgs) { }
    }

    internal class TemperatureDelegate : BaseDelegate
    {
        public TemperatureDelegate(NumericUpDown input, Action<double> setter, ComboBox unit) :
            base(input, setter, unit, Converter.fahr2celc, Converter.celc2fahr, Converter.celc2kel, Converter.fahr2kel) { }
    }

    internal class PressureDelegate : BaseDelegate
    {
        public PressureDelegate(NumericUpDown input, Action<double> setter, ComboBox unit) :
            base(input, setter, unit, Converter.inHg2mbar, Converter.mbar2inHg, Converter.mbar2pa, Converter.inHg2pa) { }
    }

    internal class AreaDelegate : BaseDelegate
    {
        public AreaDelegate(NumericUpDown input, Action<double> setter, ComboBox unit) :
            base(input, setter, unit, Converter.sqft2sqm, Converter.sqm2sqft, Converter.do_nothing, Converter.sqft2sqm) { }
    }

    internal class UnitlessDelegate : BaseDelegate
    {
        public UnitlessDelegate(NumericUpDown input, Action<double> setter) : base(input, setter) { }
    }

    internal class ForceDelegate : BaseDelegate
    {
        public ForceDelegate(NumericUpDown input, Action<double> setter, ComboBox unit) :
            base(input, setter, unit, Converter.lbf2newton, Converter.newton2lbf, Converter.do_nothing, Converter.lbf2newton) { }
    }

    internal class DistanceDelegate : BaseDelegate
    {
        public DistanceDelegate(NumericUpDown input, Action<double> setter, ComboBox unit) :
            base(input, setter, unit, Converter.ft2m, Converter.m2ft, Converter.do_nothing, Converter.ft2m) { }
    }
}
