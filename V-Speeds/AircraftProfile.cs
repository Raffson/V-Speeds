using System.Collections.Immutable;

namespace V_Speeds
{
    // The following order is used for profile parameters:  lsa, cl, bf, rc, cd, rtr, thr, clg, rfc
    // for gw, thr & lsa, use gross weight & rated thrust (reference wikipedia for example)
    // the rest can be found in config files of simulators or by experimental determination...
    public static class AircraftProfile
    {
        static private int counter = 1; // for dictionary 'Indexer'...

        // lift coefficient at approx 10 degrees AOA, guestimation according to tests in DCS
        // all these profiles still need rigorous testing, F16 & F18 are getting close though
        

        static readonly ImmutableArray<decimal> DCS_A10 =
            ImmutableArray.Create<decimal>(new decimal[] { 47m, 1.03m, 63000m, 4m, 0.08m, 0m, 63750m, 0.61m, 0.037m });

        // CL for approx 8 degrees AoA instead of 9-10
        // for some reason friction needs to be ignored for a fit -_-
        static readonly ImmutableArray<decimal> DCS_F14A_AB =
            ImmutableArray.Create<decimal>(new decimal[] { 94m, 0.89m, 79000m, 2m, 0.058m, 0m, 153000m, 0m, 0m });
        static readonly ImmutableArray<decimal> DCS_F14A_MIL =
            ImmutableArray.Create<decimal>(new decimal[] { 94m, 0.89m, 79000m, 3m, 0.058m, 0m, 98000m, 0m, 0m });

        // CL for approx 8 degrees AoA instead of 9-10
        // for some reason friction needs to be ignored for a fit -_-
        static readonly ImmutableArray<decimal> DCS_F14B_AB =
            ImmutableArray.Create<decimal>(new decimal[] { 94m, 0.89m, 79000m, 2m, 0.058m, 0m, 213000m, 0m, 0m });
        static readonly ImmutableArray<decimal> DCS_F14B_MIL =
            ImmutableArray.Create<decimal>(new decimal[] { 94m, 0.89m, 79000m, 3m, 0.058m, 0m, 135000m, 0m, 0m });

        static readonly ImmutableArray<decimal> DCS_F15C_AB =
            ImmutableArray.Create<decimal>(new decimal[] { 56.5m, 0.71m, 61700m, 2m, 0.085m, 0m, 202400m, 0.32m, 0.08m });
        static readonly ImmutableArray<decimal> DCS_F15C_MIL =
            ImmutableArray.Create<decimal>(new decimal[] { 56.5m, 0.71m, 61700m, 2m, 0.085m, 0m, 130000m, 0.32m, 0.08m });

        static readonly ImmutableArray<decimal> DCS_F16blk50_AB =
            ImmutableArray.Create<decimal>(new decimal[] { 28m, 0.9m, 51000m, 2.5m, 0.095m, 0m, 114000m, 0.58m, 0.047m });
        static readonly ImmutableArray<decimal> DCS_F16blk50_MIL =
            ImmutableArray.Create<decimal>(new decimal[] { 28m, 0.9m, 51000m, 4m, 0.095m, 0m, 67000m, 0.58m, 0.047m });

        static readonly ImmutableArray<decimal> DCS_F18C_AB =
            ImmutableArray.Create<decimal>(new decimal[] { 38m, 1.05m, 50000m, 4m, 0.12m, 0m, 146000m, 0.55m, 0.033m });
        static readonly ImmutableArray<decimal> DCS_F18C_MIL =
            ImmutableArray.Create<decimal>(new decimal[] { 38m, 1.05m, 50000m, 3m, 0.12m, 0m, 105000m, 0.55m, 0.033m });


        public static readonly ImmutableDictionary<int, ImmutableArray<decimal>> Indexer =
            ImmutableDictionary.ToImmutableDictionary<int, ImmutableArray<decimal>>(new Dictionary<int, ImmutableArray<decimal>> {
                { counter++, DCS_A10 },             // 1
                { counter++, DCS_F14A_AB },         // 2
                { counter++, DCS_F14A_MIL },        // 3
                { counter++, DCS_F14B_AB },         // 4
                { counter++, DCS_F14B_MIL },        // 5
                { counter++, DCS_F15C_AB },         // 6
                { counter++, DCS_F15C_MIL },        // 7
                { counter++, DCS_F16blk50_AB },     // 8
                { counter++, DCS_F16blk50_MIL },    // 9
                { counter++, DCS_F18C_AB },         // 10
                { counter++, DCS_F18C_MIL },        // 11
            });

        // known cases for testing, speeds in KEAS:
        // F16 : OAT = 20C; QFE = 30.05 inHg; RC = 4 for MIL, RC = 2.5 for AB; CLG = 0.58
        //  GW = 23775Lbs; CD = 0.11  => Vs = 161; Dv (AB, MIL) = (+/-340m, +/-610m)
        //      RL = 1600m => V1 (AB, MIL) = (124-129, 124-127)   <-  OK!
        //      RL = 1800m => V1 (AB, MIL) = (132-135, 132-135)   <-  OK!
        //      RL = 2400m => V1 (AB, MIL) = (148-150, 152-154)   <-  OK!
        //      RL = 2475m; QFE = 28.56 => V1 (AB, MIL) = (149-152, 152-155)   <-  OK!
        //                  OAT = 17°C     Dv (AB, MIL) = (+/-375m, +/-675m)
        //      RL = 12001ft; QFE = 24.49 => V1 (AB, MIL) = (164-173, 168-175)   <-  OK!
        //                    OAT = 9°C      Dv (AB, MIL) = (+/-525m, +/-950m)
        //      RL = 4408ft; QFE = 25.15 => V1 (AB, MIL) = (110-111, 104-105)   <-  OK!
        //                    OAT = 10°C    Dv (AB, MIL) = (+/-470m, +/-870m)
        //      RL = 4937ft; QFE = 28.00 => V1 (AB, MIL) = (125, 120)   <-  OK!
        //                   OAT = 16°C     Dv (AB, MIL) = (+/-385m, +/-695m)
        //  GW = 39857Lbs; CD = 0.14  => Vs = 208; Dv (AB, MIL) = (+/-1033m, +/-2035m)
        //      RL = 1650m => V1 (AB, MIL) = (123-125, 115-119)   <-  OK!
        //      RL = 1800m => V1 (AB, MIL) = (129-131, 120-125)   <-  OK!
        //      RL = 2400m => V1 (AB, MIL) = (151-154, 142-143)   <-  OK!
        //      RL = 2475m; QFE = 28.56 => V1 (AB, MIL) = (150-152, 140-143)   <-  OK!
        //                  OAT = 17°C     Dv (AB, MIL) = (+/-1145m, +/-2305m)
        //      RL = 12001ft; QFE = 24.49 => V1 (AB, MIL) = (170-175, 156-160)   <-  OK!
        //                    OAT = 9°C      Dv (AB, MIL) = (+/-1540m, +/-3200m)
        //      RL = 4408ft; QFE = 25.15 => V1 (AB, MIL) = (100, 91)   <-  OK! MIL slightly too low...
        //                    OAT = 10°C    Dv (AB, MIL) = (+/-1210m, NER)  <- OK!
        //                       for CL = 1.08 => Vs = 190  <- ^^^^^  ^^^ -> MIL not enough runway... over/under-estimated???
        //      RL = 4937ft; QFE = 28.00 => V1 (AB, MIL) = (115, 105)   <-  OK!
        //                   OAT = 16°C     Dv (AB, MIL) = (+/-1180m, NER)  <- AB overestimated at 1250m
        //                                                            ^^^ -> MIL not enough runway... over/under-estimated???
        //
        // F18 : OAT = 20C; QFE = 30.05 inHg; RC = 3 for MIL, RC = 4 for AB
        //  GW = 30955Lbs; CD = 0.195  => Vs = 146; Dv (AB, MIL) = (+/-305m, +/-420m)
        //      RL = 1600m => V1 (AB, MIL) = (87-94, 118-123)   <-  OK!
        //      RL = 1800m => V1 (AB, MIL) = (96-102, 126-130)   <-  OK!
        //      RL = 2400m => V1 (AB, MIL) = (116-123, 142-149)   <-  OK!
        //      RL = 2475m; QFE = 28.56 => V1 (AB, MIL) = (121-128, 145-151)   <-  OK!
        //                  OAT = 17°C     Dv (AB, MIL) = (+/-325m, +/-450m)
        //      RL = 12001ft; QFE = 24.49 => V1 (AB, MIL) = (148-155, 170)   <-  OK!
        //                    OAT = 9°C      Dv (AB, MIL) = (+/-420m, +/-600m)
        //      RL = 4408ft; QFE = 25.15 => V1 (AB, MIL) = (76-82, 100-105)   <-  OK!
        //                   OAT = 10°C     Dv (AB, MIL) = (+/-400m, +/-570m)
        //      RL = 4937ft; QFE = 28.00 => V1 (AB, MIL) = (88, 115)   <-  OK!
        //                   OAT = 16°C     Dv (AB, MIL) = (+/-340m, +/-475m)   <- OK!
        //  GW = 49110Lbs; CD = 0.201  => Vs = 184; Dv (AB, MIL) = (+/-780m, +/-1175m)
        //      RL = 1650m => V1 (AB, MIL) = (90, 111)   <-  OK!
        //      RL = 1800m => V1 (AB, MIL) = (96, 116)   <-  OK!
        //      RL = 2400m => V1 (AB, MIL) = (119, 137)   <-  OK!
        //      RL = 2475m; QFE = 28.56 => V1 (AB, MIL) = (118-122, 137)   <-  OK!
        //                  OAT = 17°C     Dv (AB, MIL) = (+/-860m, +/-1320m)
        //      RL = 12001ft; QFE = 24.49 => V1 (AB, MIL) = (147-150, 159)   <-  OK! MIL is a close one though...
        //                    OAT = 9°C      Dv (AB, MIL) = (+/-1120m, +/-1710m)  <- AB & MIL overestimated at 1215m & 1855m
        //      RL = 4408ft; QFE = 25.15 => V1 (AB, MIL) = (78-80, 92)   <-  OK!
        //                   OAT = 10°C     Dv (AB, MIL) = (+/-1055m, NER)  <- AB being overestimated at 1155m,
        //                                                            ^^^ -> MIL not enough runway... over/under-estimated???
        //      RL = 4937ft; QFE = 28.00 => V1 (AB, MIL) = (87, 103)   <-  OK!
        //                   OAT = 16°C     Dv (AB, MIL) = (+/-880m, +/-1350m)  <- AB & MIL overestimated at 935m & 1410m
        //
        // A10 : OAT = 20C; QFE = 30.05 inHg; RC = 4
        //  GW = 32948Lbs; CD = 0.08  => Vs = 137; Dv = +/-705m  <- Should be correct...
        //      RL = 1650m => V1 = 119-123   <-  OK!
        //      RL = 1800m => V1 = 124-128   <-  OK-ish! slightly high...
        //      RL = 2400m => V1 = 140-144   <-  OK!
        //      RL = 2475m; QFE = 28.56 => V1 = 140-146   <-  OK!
        //                  OAT = 17°C     Dv = +/-760m  <- Should be correct
        //      RL = 12001ft; QFE = 24.49 => V1 = 152-157   <-  OK!
        //                    OAT = 9°C      Dv = +/-970m  <- Should be correct
        //      RL = 4408ft; QFE = 25.15 => V1 = ---   <-  NOK!
        //                   OAT = 10°C     Dv = +/-...m
        //      RL = 4937ft; QFE = 28.00 => V1 = ---   <-  NOK!
        //                   OAT = 16°C      Dv = +/-...m
        //  GW = 47093Lbs; CD = 0.116  => Vs = 164; Dv = +/-1715m  <- Should be correct, underestimated at 1685m!!!
        //      RL = 1650m => V1 = 109-115  <-  OK!
        //      RL = 1800m => V1 = 114-119   <-  OK!
        //      RL = 2400m => V1 = 134-138  <-  OK!
        //      RL = 2475m; QFE = 28.56 => V1 = 131-136   <-  OK!
        //                  OAT = 17°C     Dv = +/-1890m  <- Should be correct
        //      RL = 12001ft; QFE = 24.49 => V1 = 144-148   <-  OK!
        //                    OAT = 9°C      Dv = +/-2240m <- Overestimated at 2680m
        //      RL = 4408ft; QFE = 25.15 => V1 = ---   <-  NOK!
        //                   OAT = 10°C     Dv = +/-...m
        //      RL = 4937ft; QFE = 28.00 => V1 = ---   <-  NOK!
        //                   OAT = 16°C     Dv = +/-...m
        //
        //
        // PLANE : OAT = --C; QFE = --.-- inHg; RC = - for MIL, RC = - for AB
        //  GW = ----Lbs; CD = -.--  => Vs = ---; Dv (AB, MIL) = (+/-...m, +/-...m)
        //      RL = 1600m => V1 (AB, MIL) = (, )   <-  NOK!
        //      RL = 1800m => V1 (AB, MIL) = (, )   <-  NOK!
        //      RL = 2400m => V1 (AB, MIL) = (, )   <-  NOK!
        //      RL = 2475m; QFE = 28.56 => V1 (AB, MIL) = (, )   <-  NOK!
        //                  OAT = 17°C     Dv (AB, MIL) = (+/-...m, +/-...m)
        //      RL = 2455m; QFE = 29.92 => V1 (AB, MIL) = (, )   <-  NOK!
        //                  OAT = 15°C     Dv (AB, MIL) = (+/-...m, +/-...m)
        //      RL = 12001ft; QFE = 24.49 => V1 (AB, MIL) = (, )   <-  NOK!
        //                    OAT = 9°C      Dv (AB, MIL) = (+/-...m, +/-..m)
        //      RL = 4408ft; QFE = 25.15 => V1 (AB, MIL) = (, )   <-  NOK!
        //                   OAT = 10°C     Dv (AB, MIL) = (+/-...m, +/-...m)
        //      RL = 4937ft; QFE = 28.00 => V1 (AB, MIL) = (, )   <-  NOK!
        //                   OAT = 16°C     Dv (AB, MIL) = (+/-...m, +/-...m)   <- NOK!
        //
        //
        //
        // Debug data A10, 15C 29.92inHg <=> densr = 1.0000 -> FF = 3300pph per engine
        //                 17C 28.56inHg <=> densr = 0.9479 -> FF = 3100pphpe ~ 0.9394 of RT (Rated Thrust)
        //                 14C 26.74inHg <=> densr = 0.8968 -> FF = 2950pphpe ~ 0.8939 of RT
        //                 9C  24.49inHg <=> densr = 0.8359 -> FF = 2800pphpe ~ 0.8485 of RT
        // GW = 32948Lbs; RC = 4; => Vs = 137; Dv = +/-665m
        //  CD = 0.08; CLG = 0.61; RFC = 0.034;
        //      RL = 2455m => V1 = 142-143
        // GW = 47093Lbs => Vs = 163; Dv = +/-1600m
        //  CD = 0.116; CLG = 0.55; RFC = 0.034;
        //      RL = 2455m => V1 = 136-139
        //
        //
        // Debug data F16, 15C 29.92inHg <=> densr = 1.0000 -> FF = 10200pph
        //                 17C 28.56inHg <=> densr = 0.9479 -> FF = 11000pph ~ 
        //                 14C 26.74inHg <=> densr = 0.8968 -> FF = 10250pph ~
        //                 9C  24.49inHg <=> densr = 0.8359 -> FF = 8250pph  ~
        // GW = 23775Lbs; RC MIL/AB = 4/2.5; => Vs = 161; Dv MIL/AB  = +/-600m / +/-335m
        //  CD = 0.095; CLG = 0.58; RFC = 0.043;
        //      RL = 2455m => V1 MIL/AB = 153-154 / 149-150
        // GW = 39857Lbs => Vs = 208; Dv MIL/AB = +/-2020m / +/-1025m (MIL Underestimated at 1990m)
        //  CD = 0.116; CLG = 0.58; RFC = 0.043;
        //      RL = 2455m => V1 MIL/AB = 144-145 / 152-154
        //
        // F16 Conclusion: Engine power overestimated?  70kN for MIL? 119kN for AB? RFC = 0.04; CD 0.1 & 0.124
        //
        //
        // Debug data F18, 15C 29.92inHg <=> densr = 1.0000 -> FF = 10250pph per engine
        //                 17C 28.56inHg <=> densr = 0.9479 -> FF = 9600pphpe ~ 0.9366 of RT
        //                 14C 26.74inHg <=> densr = 0.8968 -> FF = 9300pphpe ~ 0.9073 of RT
        //                 9C  24.49inHg <=> densr = 0.8359 -> FF = 8500pphpe ~ 0.8293 of RT
        // GW = 30955Lbs; RC MIL/AB = 3/4; => Vs = 146; Dv MIL/AB  = +/-405m / +/-300m
        //  CD = 0.195; CLG = 0.55; RFC = 0.012;
        //      RL = 2455m => V1 MIL/AB = 147-149 / 119-120
        // GW = 49110Lbs => Vs = 184; Dv MIL/AB = +/-1145m / +/-765m
        //  CD = 0.201; CLG = 0.55; RFC = 0.012;
        //      RL = 2455m => V1 MIL/AB = 138-143 / 121-123

    }
}
