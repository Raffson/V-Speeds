namespace V_Speeds
{
    public static class Constants
    {
        /// <summary>
        ///     Universal Gas Constant
        /// </summary>
        public const double ugc = 8.31446261815324;

        /// <summary>
        ///     Molar mass of air
        /// </summary>
        public const double mmair = 28.9645 / 1000;

        /// <summary>
        ///     1G at poles (m/s2), just for some extra wiggle room, considering no elevation
        /// </summary>
        public const double g = 9.83;

        /// <summary>
        ///     Standard air density at sea-level
        /// </summary>
        public const double p0 = 101325 * mmair / (ugc * 288.15);
    }
}