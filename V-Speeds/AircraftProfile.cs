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
            ImmutableArray.Create<decimal>(new decimal[] { 47m, 1.03m, 63000m, 2m, 0.121m, 0m, 11321m, 68500m, 0.667m, 0.05m });

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
            ImmutableArray.Create<decimal>(new decimal[] { 28m, 0.9m, 53900m, 2.5m, 0.11m, 0m, 8573m, 124000m, 0.58m, 0.043m });
        static readonly ImmutableArray<decimal> DCS_F16blk50_MIL =
            ImmutableArray.Create<decimal>(new decimal[] { 28m, 0.9m, 53900m, 4m, 0.11m, 0m, 8573m, 72750m, 0.58m, 0.043m });

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

        // known cases for testing, speeds in KEAS: 
        // F16 : OAT = 20C; QFE = 30.05 inHg; RC = 4 for MIL, RC = 2.5 for AB; CLG = 0.58
        //  GW = 23775Lbs; CD = 0.11  => Vs = 161; Dv (AB, MIL) = (+/-340m, +/-610m)
        //      RL = 1600m => V1 (AB, MIL) = (124-129, 124-127)   <-  OK!
        //      RL = 1800m => V1 (AB, MIL) = (132-135, 132-135)   <-  OK!
        //      RL = 2400m => V1 (AB, MIL) = (148-149, 152-153)   <-  OK!
        //      RL = 2475m; QFE = 28.56 => V1 (AB, MIL) = (149-151, 152-155)   <-  OK!
        //                  OAT = 17°C     Dv (AB, MIL) = (+/-375m, +/-675m)
        //      RL = 12001ft; QFE = 24.49 => V1 (AB, MIL) = (165-173, 168-175)   <-  OK!
        //                    OAT = 9°C      Dv (AB, MIL) = (+/-525m, +/-950m)
        //  GW = 39857Lbs; CD = 0.140  => Vs = 208; Dv (AB, MIL) = (+/-1033m, +/-2035m)
        //      RL = 1650m => V1 (AB, MIL) = (123-125, 115-119)   <-  OK!
        //      RL = 1800m => V1 (AB, MIL) = (129-131, 121-125)   <-  OK!
        //      RL = 2400m => V1 (AB, MIL) = (151-154, 142-143)   <-  OK!
        //      RL = 2475m; QFE = 28.56 => V1 (AB, MIL) = (150-152, 140-143)   <-  OK!
        //                  OAT = 17°C     Dv (AB, MIL) = (+/-1185m, +/-2395m)
        //      RL = 12001ft; QFE = 24.49 => V1 (AB, MIL) = (170-175, 156-160)   <-  OK!
        //                    OAT = 9°C      Dv (AB, MIL) = (+/-1540m, +/-3200m)

        // F18 : OAT = 20C; QFE = 30.05 inHg; RC = 2 for MIL, RC = 3 for AB
        //  GW = ...Lbs; CD = ...  => Vs = ...; Dv (AB, MIL) = (+/-...m, +/-...m)
        //      RL = 1600m => V1 (AB, MIL) = (, )   <-  OK!
        //      RL = 2400m => V1 (AB, MIL) = (, )   <-  OK!
        //  GW = ...Lbs; CD = ...  => Vs = ...; Dv (AB, MIL) = (+/-...m, +/-...m)
        //      RL = 1650m => V1 (AB, MIL) = (, )   <-  OK!
        //      RL = 2400m => V1 (AB, MIL) = (, )   <-  OK!
        //
        //
        // A10 : OAT = 20C; QFE = 30.05 inHg; RC = 2
        //  GW = 32948Lbs; CD = 0.121  => Vs = 137; Dv = +/-697m  <- Should be correct...
        //      RL = 1650m => V1 = 123   <-  OK!
        //      RL = 2400m => V1 = 144   <-  OK!
        //  GW = 46381Lbs; CD = 0.145  => Vs = 162; Dv = +/-1647m  <- Should be correct...
        //      RL = 1650m => V1 = 116  <-  OK!
        //      RL = 2400m => V1 = 138  <-  OK!
    }
}
