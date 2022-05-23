namespace V_Speeds.Model.Aircrafts
{
    /// <summary>
    ///     Enum representing the different aircraft profiles.
    /// </summary>
    public enum AircraftType
    {
        Custom = 0,     // 0
        DCS_A10 = 1,    // 1
        DCS_F14A,       // 2
        DCS_F14B,       // 3
        DCS_F15C,       // 4
        DCS_F16C_blk50, // 5
        DCS_F18C,       // 6


        External = int.MaxValue
    }

    /// <summary>
    ///     Factory for creating aircraft.
    /// </summary>
    public static class AircraftFactory
    {
        /// <summary>
        ///     Creates an aircraft of the given type.
        /// </summary>
        /// <param name="t">The type of the aircraft to be created, refer to <see cref="AircraftType"/> for the different types.</param>
        /// <returns>
        ///     The created aircraft depending on the provided type.<br></br>
        ///     If the type is invalid, a default <see cref="Aircraft"/> is returned.
        /// </returns>
        public static Aircraft CreateAircraft(AircraftType t)
        {
            return t switch
            {
                AircraftType.DCS_A10 => new DCS_A10(),
                AircraftType.DCS_F14A => new DCS_F14A(),
                AircraftType.DCS_F14B => new DCS_F14B(),
                AircraftType.DCS_F15C => new DCS_F15C(),
                AircraftType.DCS_F16C_blk50 => new DCS_F16C_blk50(),
                AircraftType.DCS_F18C => new DCS_F18C(),
                AircraftType.External => new Aircraft(), // this will change once i know what to do exactly...
                _ => new Aircraft(),
            };
        }
    }
}
