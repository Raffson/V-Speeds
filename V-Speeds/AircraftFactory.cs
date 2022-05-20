using V_Speeds.Aircrafts;

namespace V_Speeds
{
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

    public static class AircraftFactory
    {
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
