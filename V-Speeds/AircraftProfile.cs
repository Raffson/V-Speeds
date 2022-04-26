using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace V_Speeds
{
    // The following order is used for profile parameters:  lsa, cl, bf, csa, cd, rtr, gw, thr
    // for gw & thr, use gross weight & nominal thrust (reference wikipedia for example)
    internal static class AircraftProfile
    {
        static private int counter = 1;

        // lift coefficient at approx 10 degrees AOA, guestimation according to tests in DCS
        // csa & cd probably still wrong, matter of fact they change according to loadout...
        static readonly ImmutableArray<decimal> F16blk50_AB = 
            ImmutableArray.Create<decimal>(new decimal[]{ 28m, 0.9m, 53900m, 1m, 0.1m, 0m, 12020m, 131000m });
        static readonly ImmutableArray<decimal> F16blk50_MIL = 
            ImmutableArray.Create<decimal>(new decimal[]{ 28m, 0.9m, 53900m, 1m, 0.1m, 0m, 12020m, 76310 });

        public static readonly ImmutableDictionary<int, ImmutableArray<decimal>> Indexer =
            ImmutableDictionary.ToImmutableDictionary<int, ImmutableArray<decimal>>(new Dictionary<int, ImmutableArray<decimal>> {
                { counter++, F16blk50_AB },
                { counter++, F16blk50_MIL }
            });
    }
}
