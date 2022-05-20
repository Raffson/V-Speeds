namespace V_Speeds
{
    /// <summary>
    ///     Static class for making conversions
    /// </summary>
    public static class Converter
    {
        public static double do_nothing(double val) => val;
        public static double lbs2kgs(double lbs) => lbs * 0.45359237;
        public static double kgs2lbs(double kgs) => kgs / 0.45359237;
        public static double celc2kel(double c) => c + 273.15;
        public static double fahr2kel(double f) => (f + 459.67) * 5 / 9;
        public static double celc2fahr(double c) => c * 9 / 5 + 32;
        public static double fahr2celc(double f) => (f - 32) * 5 / 9;
        public static double mbar2pa(double mb) => mb * 100;
        public static double inHg2pa(double inHg) => inHg * 3386.39;
        public static double mbar2inHg(double mb) => mb / 33.8639;
        public static double inHg2mbar(double inHg) => inHg * 33.8639;
        public static double sqft2sqm(double sqft) => sqft * 0.09290304;
        public static double sqm2sqft(double sqm) => sqm / 0.09290304;
        public static double mps2kts(double mps) => mps * 1.94384449;
        public static double newton2lbf(double newton) => newton * 0.224808943;
        public static double lbf2newton(double lbf) => lbf / 0.224808943;
        public static double m2ft(double m) => m * 3.2808399;
        public static double ft2m(double ft) => ft / 3.2808399;
        public static decimal lbs2kgs(decimal lbs) => (decimal)((double)lbs * 0.45359237);
        public static decimal kgs2lbs(decimal kgs) => (decimal)((double)kgs / 0.45359237);
        public static decimal celc2fahr(decimal c) => (decimal)((double)c * 9 / 5 + 32);
        public static decimal fahr2celc(decimal f) => (decimal)((double)(f - 32) * 5 / 9);
        public static decimal mbar2inHg(decimal mb) => (decimal)((double)mb / 33.8639);
        public static decimal inHg2mbar(decimal inHg) => (decimal)((double)inHg * 33.8639);
        public static decimal sqft2sqm(decimal sqft) => (decimal)((double)sqft * 0.09290304);
        public static decimal sqm2sqft(decimal sqm) => (decimal)((double)sqm / 0.09290304);
        public static decimal newton2lbf(decimal newton) => (decimal)((double)newton * 0.224808943);
        public static decimal lbf2newton(decimal lbf) => (decimal)((double)lbf / 0.224808943);
        public static decimal m2ft(decimal m) => (decimal)((double)m * 3.2808399);
        public static decimal ft2m(decimal ft) => (decimal)((double)ft / 3.2808399);

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
