using System.Collections.Immutable;

namespace V_Speeds
{
    // The following order is used for profile parameters:  lsa, cl, bf, rc, cd, rtr, gw, thr, clg, rfc
    // for gw, thr & lsa, use gross weight & nominal thrust (reference wikipedia for example)
    // the rest can be found in config files of simulators or by experimental determination...
    internal static class AircraftProfile
    {
        /*
            F-16C blk50 (AB) = 1
            F-16C blk50 (MIL) = 2
            A-10C = 3
            F-14A = 4
            F-14B = 5
            F-15C = 6
        */
        static private int counter = 1;

        // lift coefficient at approx 10 degrees AOA, guestimation according to tests in DCS
        // all these profiles still need rigorous testing, F16 is getting close though
        static readonly ImmutableArray<decimal> DCS_A10C =
            ImmutableArray.Create<decimal>(new decimal[] { 47m, 1.03m, 70000m, 2m, 0.15m, 0m, 11321m, 80646m, 0.75m, 0.04m });

        static readonly ImmutableArray<decimal> DCS_F14A_AB =
            ImmutableArray.Create<decimal>(new decimal[] { 52.5m, 1m, 60000m, 2m, 0.1m, 0m, 19090m, 185800m, 0.5m, 0.04m });
        static readonly ImmutableArray<decimal> DCS_F14A_MIL =
            ImmutableArray.Create<decimal>(new decimal[] { 52.5m, 1m, 60000m, 2m, 0.1m, 0m, 19090m, 109000m, 0.5m, 0.04m });

        static readonly ImmutableArray<decimal> DCS_F14B_AB =
            ImmutableArray.Create<decimal>(new decimal[] { 52.5m, 1m, 60000m, 2m, 0.1m, 0m, 19976m, 250000m, 0.5m, 0.04m });
        static readonly ImmutableArray<decimal> DCS_F14B_MIL =
            ImmutableArray.Create<decimal>(new decimal[] { 52.5m, 1m, 60000m, 2m, 0.1m, 0m, 19976m, 147800m, 0.5m, 0.04m });

        static readonly ImmutableArray<decimal> DCS_F15C_AB =
            ImmutableArray.Create<decimal>(new decimal[] { 56.5m, 0.7m, 61700m, 2m, 0.085m, 0m, 12701m, 202400m, 0.1m, 0.08m });
        static readonly ImmutableArray<decimal> DCS_F15C_MIL =
            ImmutableArray.Create<decimal>(new decimal[] { 56.5m, 0.7m, 61700m, 2m, 0.085m, 0m, 12701m, 130000m, 0.1m, 0.08m });

        static readonly ImmutableArray<decimal> DCS_F16blk50_AB =
            ImmutableArray.Create<decimal>(new decimal[] { 28m, 0.9m, 53900m, 2m, 0.1m, 0m, 8573m, 125000m, 0.52m, 0.043m });
        static readonly ImmutableArray<decimal> DCS_F16blk50_MIL =
            ImmutableArray.Create<decimal>(new decimal[] { 28m, 0.9m, 53900m, 2m, 0.1m, 0m, 8573m, 73310m, 0.52m, 0.043m });

        static readonly ImmutableArray<decimal> DCS_F18C_AB =
            ImmutableArray.Create<decimal>(new decimal[] { 38m, 1.05m, 52900m, 3m, 0.1m, 0m, 10433m, 158000m, 0.5m, 0.03m });
        static readonly ImmutableArray<decimal> DCS_F18C_MIL =
            ImmutableArray.Create<decimal>(new decimal[] { 38m, 1.05m, 52900m, 2m, 0.1m, 0m, 10433m, 107000m, 0.5m, 0.03m });


        public static readonly ImmutableDictionary<int, ImmutableArray<decimal>> Indexer =
            ImmutableDictionary.ToImmutableDictionary<int, ImmutableArray<decimal>>(new Dictionary<int, ImmutableArray<decimal>> {
                { counter++, DCS_A10C },
                { counter++, DCS_F14A_AB },
                { counter++, DCS_F14A_MIL },
                { counter++, DCS_F14B_AB },
                { counter++, DCS_F14B_MIL },
                { counter++, DCS_F15C_AB },
                { counter++, DCS_F15C_MIL },
                { counter++, DCS_F16blk50_AB },
                { counter++, DCS_F16blk50_MIL },
                { counter++, DCS_F18C_AB },
                { counter++, DCS_F18C_MIL },
            });
    }
}
