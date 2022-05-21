namespace V_Speeds
{
    internal static class ExtensionMethods
    {
        internal static string DisplayName(this AircraftType e)
        {
            return e switch
            {
                AircraftType.Custom => "Custom...",
                AircraftType.DCS_A10 => "DCS A-10 (A/C/C-II)",
                AircraftType.DCS_F14A => "DCS F-14A",
                AircraftType.DCS_F14B => "DCS F-14B",
                AircraftType.DCS_F15C => "DCS F-15C",
                AircraftType.DCS_F16C_blk50 => "DCS F-16C blk50",
                AircraftType.DCS_F18C => "DCS F-18C",
                AircraftType.External => "External DLL",
                _ => "Unknown..."
            };
        }

        internal static AircraftType AircraftTypeFromString(this string s)
        {
            return s switch
            {
                "Custom..." => AircraftType.Custom,
                "DCS A-10 (A/C/C-II)" => AircraftType.DCS_A10,
                "DCS F-14A" => AircraftType.DCS_F14A,
                "DCS F-14B" => AircraftType.DCS_F14B,
                "DCS F-15C" => AircraftType.DCS_F15C,
                "DCS F-16C blk50" => AircraftType.DCS_F16C_blk50,
                "DCS F-18C" => AircraftType.DCS_F18C,
                "External DLL" => AircraftType.External,
                _ => AircraftType.Custom
            };
        }
    }
}
