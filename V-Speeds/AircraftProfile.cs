using System;
using System.Collections.Generic;

using ProfileTuple = System.Tuple<decimal, decimal, decimal, decimal, decimal, decimal>; // lsa, cl, bf, csa, cd, rtr

namespace V_Speeds
{
    internal static class AircraftProfile
    {
        // lift coefficient at approx 10 degrees AOA, guestimation according to tests in DCS
        // csa & cd probably still wrong...
        static readonly ProfileTuple F16blk50 = new ProfileTuple(28, (decimal)0.9, 53900, 1, (decimal)0.1, 0);

        public static readonly Dictionary<int, ProfileTuple> Indexer = new Dictionary<int, ProfileTuple> {
            { 1, F16blk50 }
        };
    }
}
