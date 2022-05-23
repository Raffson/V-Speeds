namespace V_Speeds
{
    /// <summary>
    ///     A custom container to make life easier in Form1, bundeling all components together for accessing or transforming the relevant data.
    /// </summary>
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
        private readonly Func<double, double>? _si2m;
        private readonly Func<double, double>? _si2i;

        /// <summary>
        ///     Constructor for initializing the different "delegates".
        /// </summary>
        /// <param name="input">The <see cref="NumericUpDown"/> input component of the form.</param>
        /// <param name="property">The property that corresponds with <paramref name="input"/>.</param>
        /// <param name="increment">The increment values depending on the unit.</param>
        /// <param name="unit">The <see cref="ComboBox"/> unit component of the form.</param>
        /// <param name="i2m">A function converting the unit from imperial to metric.</param>
        /// <param name="m2i">A function converting the unit from metric to imperial.</param>
        /// <param name="m2si">A function converting the unit from metric to SI (e.g. millibar to Pascal).</param>
        /// <param name="i2si">A function converting the unit from imperial to SI.</param>
        /// <param name="si2m">A function converting the unit from SI to metric (e.g. Pascal to millbar).</param>
        /// <param name="si2i">A function converting the unit from SI to imperial.</param>
        public BaseDelegate(NumericUpDown input,
                            string property,
                            (decimal, decimal) increment,
                            ComboBox? unit = null,
                            Func<decimal, decimal>? i2m = null,
                            Func<decimal, decimal>? m2i = null,
                            Func<double, double>? m2si = null,
                            Func<double, double>? i2si = null,
                            Func<double, double>? si2m = null,
                            Func<double, double>? si2i = null)
        {
            _index = 0;
            _input = input;
            _property = property;
            _unit = unit;
            _i2m = i2m;
            _m2i = m2i;
            _m2si = m2si;
            _i2si = i2si;
            _si2m = si2m;
            _si2i = si2i;
            _increment = increment;
        }

        /// <summary>
        ///     Property indicating the last selected index of the unit (0 is metric, 1 is imperial).
        /// </summary>
        public int LastIndex { get => _index; set => _index = value; }

        /// <summary>
        ///     Property for the input component of Form1.
        /// </summary>
        public NumericUpDown Input => _input;

        /// <summary>
        ///     Property for the metric-to-imperial converter.
        /// </summary>
        public Func<decimal, decimal>? M2I => _m2i;

        /// <summary>
        ///     Property for the imperial-to-metric converter.
        /// </summary>
        public Func<decimal, decimal>? I2M => _i2m;

        /// <summary>
        ///     Property indicating the corresponding property of the model.
        /// </summary>
        public string Property => _property;

        /// <summary>
        ///     Property for the unit component of Form1.
        /// </summary>
        public ComboBox? Unit => _unit;

        /// <summary>
        ///     Property for the metric-to-SI converter.
        /// </summary>
        public Func<double, double>? M2SI => _m2si;

        /// <summary>
        ///     Property for the imperial-to-SI converter.
        /// </summary>
        public Func<double, double>? I2SI => _i2si;

        /// <summary>
        ///     Property for the SI-to-metric converter.
        /// </summary>
        public Func<double, double>? SI2M => _si2m;

        /// <summary>
        ///     Property for the SI-to-imperial converter.
        /// </summary>
        public Func<double, double>? SI2I => _si2i;

        /// <summary>
        ///     Property indicating which increment should be used, either for metric or imperial.
        /// </summary>
        public decimal Increment => _index == 0 ? _increment.metric : _increment.imperial;

    }

    /// <summary>
    ///     Container for any input that's based on weight.
    /// </summary>
    internal class WeightDelegate : BaseDelegate
    {
        public WeightDelegate(NumericUpDown input, string property, ComboBox unit, (decimal, decimal) increment) : 
            base(input, property, increment, unit, Converter.lbs2kgs, Converter.kgs2lbs,
                                                   Converter.do_nothing, Converter.lbs2kgs,
                                                   Converter.do_nothing, Converter.kgs2lbs) { }
    }

    /// <summary>
    ///     Container for any input that's based on temperature.
    /// </summary>
    internal class TemperatureDelegate : BaseDelegate
    {
        public TemperatureDelegate(NumericUpDown input, string property, ComboBox unit, (decimal, decimal) increment) :
            base(input, property, increment, unit, Converter.fahr2celc, Converter.celc2fahr,
                                                   Converter.celc2kel, Converter.fahr2kel,
                                                   Converter.kel2celc, Converter.kel2fahr) { }
    }

    /// <summary>
    ///     Container for any input that's based on pressure.
    /// </summary>
    internal class PressureDelegate : BaseDelegate
    {
        public PressureDelegate(NumericUpDown input, string property, ComboBox unit, (decimal, decimal) increment) :
            base(input, property, increment, unit, Converter.inHg2mbar, Converter.mbar2inHg,
                                                   Converter.mbar2pa, Converter.inHg2pa,
                                                   Converter.pa2mbar, Converter.pa2inHg) { }
    }

    /// <summary>
    ///     Container for any input that's based on an area.
    /// </summary>
    internal class AreaDelegate : BaseDelegate
    {
        public AreaDelegate(NumericUpDown input, string property, ComboBox unit, (decimal, decimal) increment) :
            base(input, property, increment, unit, Converter.sqft2sqm, Converter.sqm2sqft,
                                                   Converter.do_nothing, Converter.sqft2sqm,
                                                   Converter.do_nothing, Converter.sqm2sqft) { }
    }

    /// <summary>
    ///     Container for any input that's based no unit.
    /// </summary>
    internal class UnitlessDelegate : BaseDelegate
    {
        public UnitlessDelegate(NumericUpDown input, string property, decimal increment) : base(input, property, (increment, 0m)) { }
    }

    /// <summary>
    ///     Container for any input that's based on force.
    /// </summary>
    internal class ForceDelegate : BaseDelegate
    {
        public ForceDelegate(NumericUpDown input, string property, ComboBox unit, (decimal, decimal) increment) :
            base(input, property, increment, unit, Converter.lbf2newton, Converter.newton2lbf,
                                                   Converter.do_nothing, Converter.lbf2newton,
                                                   Converter.do_nothing, Converter.newton2lbf) { }
    }

    /// <summary>
    ///     Container for any input that's based on distance.
    /// </summary>
    internal class DistanceDelegate : BaseDelegate
    {
        public DistanceDelegate(NumericUpDown input, string property, ComboBox unit, (decimal, decimal) increment) :
            base(input, property, increment, unit, Converter.ft2m, Converter.m2ft,
                                                   Converter.do_nothing, Converter.ft2m,
                                                   Converter.do_nothing, Converter.m2ft) { }
    }
}
