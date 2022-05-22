namespace V_Speeds
{
    /// <summary>
    ///     Static class for making conversions
    /// </summary>
    public static class Converter
    {
        public static double do_nothing(double val) => val;
        public static double lbs2kgs(double lbs) => lbs * 0.45359237;
        public static double kgs2lbs(double kgs) => kgs / lbs2kgs(1.0);
        public static double celc2kel(double c) => Math.Max(0, c + 273.15);
        public static double fahr2kel(double f) => celc2kel(fahr2celc(f));
        public static double kel2celc(double k) => Math.Max(-273.15, k - 273.15);
        public static double kel2fahr(double k) => celc2fahr(kel2celc(k));
        public static double celc2fahr(double c) => c * 9 / 5 + 32;
        public static double fahr2celc(double f) => (f - 32) * 5 / 9;
        public static double mbar2pa(double mb) => mb * 100;
        public static double pa2mbar(double pa) => pa / mbar2pa(1.0);
        public static double inHg2pa(double inHg) => mbar2pa(inHg2mbar(inHg));
        public static double pa2inHg(double pa) => mbar2inHg(pa2mbar(pa));
        public static double mbar2inHg(double mb) => mb / inHg2mbar(1.0);
        public static double inHg2mbar(double inHg) => inHg * 33.8639;
        public static double sqft2sqm(double sqft) => sqft * 0.09290304;
        public static double sqm2sqft(double sqm) => sqm / sqft2sqm(1.0);
        public static double mps2kts(double mps) => mps * 1.94384449;
        public static double newton2lbf(double newton) => newton * 0.224808943;
        public static double lbf2newton(double lbf) => lbf / newton2lbf(1.0);
        public static double m2ft(double m) => m * 3.2808399;
        public static double ft2m(double ft) => ft / m2ft(1.0);
        public static decimal lbs2kgs(decimal lbs) => (decimal)lbs2kgs((double)lbs);
        public static decimal kgs2lbs(decimal kgs) => (decimal)kgs2lbs((double)kgs);
        public static decimal celc2fahr(decimal c) => (decimal)celc2fahr((double)c);
        public static decimal fahr2celc(decimal f) => (decimal)fahr2celc((double)f);
        public static decimal mbar2inHg(decimal mb) => (decimal)mbar2inHg((double)mb);
        public static decimal inHg2mbar(decimal inHg) => (decimal)inHg2mbar((double)inHg);
        public static decimal sqft2sqm(decimal sqft) => (decimal)sqft2sqm((double)sqft);
        public static decimal sqm2sqft(decimal sqm) => (decimal)sqm2sqft((double)sqm);
        public static decimal newton2lbf(decimal newton) => (decimal)newton2lbf((double)newton);
        public static decimal lbf2newton(decimal lbf) => (decimal)lbf2newton((double)lbf);
        public static decimal m2ft(decimal m) => (decimal)m2ft((double)m);
        public static decimal ft2m(decimal ft) => (decimal)ft2m((double)ft);

        /// <summary>
        ///     Converts True Air Speed to Equivalent Air Speed,
        ///     which should be close to Indicated Air Speed (IAS) and Calibrated Air Speed (CAS)
        /// </summary>
        /// <param name="tas">True Air Speed expected in your unit of choice</param>
        /// <param name="density">The density of the fluid (in kg/m³) in which the aircraft is moving, must be positve or NaN is returned.</param>
        /// <returns>Equivalent Air Speed in the same unit as <paramref name="tas"/></returns>
        public static double TAS2EAS(double tas, double density) => tas * Math.Sqrt(density / Constants.p0);


    }
}
