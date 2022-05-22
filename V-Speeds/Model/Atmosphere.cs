namespace V_Speeds.Model
{
    /// <summary>
    ///     A class representing (as the name suggests) an atmosphere.
    /// </summary>
    public class Atmosphere
    {
        /// <summary>
        ///     Temperature of the atmosphere in Kelvin
        /// </summary>
        double _temp;

        /// <summary>
        ///     Pressure of the atmosphere in Pascal
        /// </summary>
        double _press;

        /// <summary>
        ///     Molar mass of the atmosphere in kg/mol
        /// </summary>
        double _mmass;

        /// <summary>
        ///     Constructs an Atmosphere object given the temperature, pressure and molar mass.
        /// </summary>
        /// <param name="temp">Temperature of the new atmosphere in Kelvin</param>
        /// <param name="press">Pressure of the new atmosphere in Pascal</param>
        /// <param name="mmass">Molar mass of the new atmosphere in kg/mol</param>
        public Atmosphere(double temp = 288.15, double press = 101325.0, double mmass = Constants.mmair)
        {
            Temp = temp;
            Press = press;
            Mmass = mmass;
        }

        /// <summary>
        ///     Property for temperature, expected in Kelvin.<br></br>
        ///     Setter takes the absolute value.
        /// </summary>
        public double Temp { get => _temp; set => _temp = Math.Abs(value); }

        /// <summary>
        ///     Property for pressure, expected in Pascal.<br></br>
        ///     Setter takes the absolute value.
        /// </summary>
        public double Press { get => _press; set => _press = Math.Abs(value); }

        /// <summary>
        ///     Property for molar mass, expected in kg/mol.<br></br>
        ///     Setter takes the absolute value.
        /// </summary>
        public double Mmass { get => _mmass; set => _mmass = Math.Abs(value); }

        /// <summary>
        ///     Returns the density of the atmosphere considering temperature, pressure and molar mass.
        /// </summary>
        /// <returns>Denisty of the atmosphere in kg/m³</returns>
        public double Density() => Press * Mmass / (Constants.ugc * Temp);
    }
}