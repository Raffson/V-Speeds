using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using V_Speeds;

namespace V_Speeds_Tester
{
    [TestClass]
    public class V_Calculator_Tester
    {
        private readonly V_Calculator vcalc = new();

        // The following order is used for profile parameters:  lsa, cl, bf, rc, cd, rtr, gw, thr, clg, rfc
        private void SetProfile(V_Calculator vcalc, int profile)
        {
            var ap = AircraftProfile.Indexer[profile];
            vcalc.Lsa = (double)ap[0];
            vcalc.Cl = (double)ap[1];
            vcalc.Bf = (double)ap[2];
            vcalc.Rc = (double)ap[3];
            vcalc.Cd = (double)ap[4];
            vcalc.Rtr = (double)ap[5];
            vcalc.Thr = (double)ap[7];
            vcalc.Clg = (double)ap[8];
            vcalc.Rfc = (double)ap[9];
        }

        private void RunScenario(double gw, double qfe, double oat, double rl, double vs, double exprun, double expv1)
        {
            vcalc.Gw = gw;
            vcalc.Qfe = qfe;
            vcalc.Oat = oat;
            vcalc.Rl = rl;
            Assert.AreEqual(vs, Converter.mps2kts(vcalc.CalcVs().Item1), 1.0);   // Check Vs
            double result = vcalc.CalcNeededRunway();
            double diff = exprun - result;
            double tolerance = exprun * -0.1; // 10% tolerance for overestimations...
            Assert.IsTrue(diff < 10, $"Dv is underestimated by more than 10m: {exprun}m expected but got {result}m");
            Assert.IsTrue(diff > tolerance, $"Dv is underestimated by more than 10%: {exprun}m expected but got {result}m");
            Assert.AreEqual(expv1, Converter.mps2kts(vcalc.CalcV1().Item1), 3);
        }

        [TestMethod]
        public void DCS_F16C_23775lbs_Tester() // Tests for F16
        {
            double weight = Converter.lbs2kgs(23775.0);
            double qfe = Converter.inHg2pa(30.05);
            double oat = Converter.celc2kel(20.0);
            double expv2 = 161.0;

            SetProfile(vcalc, 8);
            double expDv = 340.0;
            var ranges = new (double, double)[] { (1600, 126), (1800, 134), (2400, 148) };
            foreach ((var range, var expv1) in ranges) RunScenario(weight, qfe, oat, range, expv2, expDv, expv1);
            
            SetProfile(vcalc, 9);
            expDv = 610.0;
            ranges = new (double, double)[] { (1600, 124), (1800, 133), (2400, 152) };
            foreach ((var range, var expv1) in ranges) RunScenario(weight, qfe, oat, range, expv2, expDv, expv1);


            SetProfile(vcalc, 8);
            RunScenario(weight, Converter.inHg2pa(29.92), Converter.celc2kel(15.0), 2455.0, expv2, 335.0, 149.0);
            RunScenario(weight, Converter.inHg2pa(28.56), Converter.celc2kel(17.0), 2475.0, expv2, 375.0, 150.0);
            RunScenario(weight, Converter.inHg2pa(24.49), Converter.celc2kel(9.0),  3657.9, expv2, 525.0, 167.0);
            RunScenario(weight, Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 1343.5, expv2, 470.0, 110.0);
            RunScenario(weight, Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 1504.8, expv2, 385.0, 123.0);

            SetProfile(vcalc, 9);
            RunScenario(weight, Converter.inHg2pa(29.92), Converter.celc2kel(15.0), 2455.0, expv2, 600.0, 153.0);
            RunScenario(weight, Converter.inHg2pa(28.56), Converter.celc2kel(17.0), 2475.0, expv2, 675.0, 153.0);
            RunScenario(weight, Converter.inHg2pa(24.49), Converter.celc2kel(9.0),  3657.9, expv2, 950.0, 170.0);
            RunScenario(weight, Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 1343.5, expv2, 870.0, 104.0);
            RunScenario(weight, Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 1504.8, expv2, 695.0, 118.0);
        }

        [TestMethod]
        public void DCS_F16C_39857lbs_Tester() // Tests for F16
        {
            double weight = Converter.lbs2kgs(39857.0);
            double qfe = Converter.inHg2pa(30.05);
            double oat = Converter.celc2kel(20.0);
            double expv2 = 208.0;
            double cd = 0.124;

            SetProfile(vcalc, 8);
            vcalc.Cd = cd;
            double expDv = 1030.0;
            var ranges = new (double, double)[] { (1650, 123), (1800, 129), (2400, 152) };
            foreach ((var range, var expv1) in ranges) RunScenario(weight, qfe, oat, range, expv2, expDv, expv1);

            SetProfile(vcalc, 9);
            vcalc.Cd = cd;
            expDv = 2035.0;
            ranges = new (double, double)[] { (1650, 116), (1800, 123), (2400, 142) };
            foreach ((var range, var expv1) in ranges) RunScenario(weight, qfe, oat, range, expv2, expDv, expv1);


            // For short fields with not enough runway I created the same same atmospheric conditions at tonopah
            // expected runway distances have been confirmed...

            SetProfile(vcalc, 8); // Full AB
            vcalc.Cd = cd;
            RunScenario(weight, Converter.inHg2pa(29.92), Converter.celc2kel(15.0), 2455.0, expv2, 1025.0, 152.0);
            RunScenario(weight, Converter.inHg2pa(28.56), Converter.celc2kel(17.0), 2475.0, expv2, 1145.0, 150.0);
            RunScenario(weight, Converter.inHg2pa(24.49), Converter.celc2kel(9.0),  3657.9, expv2, 1540.0, 172.0);
            RunScenario(weight, Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 1343.5, expv2, 1465.0, 98.0);
            RunScenario(weight, Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 3657.9, expv2, 1465.0, 174.0);
            RunScenario(weight, Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 1504.8, expv2, 1195.0, 113.0);
            RunScenario(weight, Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 2475.0, expv2, 1195.0, 148.0);

            SetProfile(vcalc, 9); // Full MIL
            vcalc.Cd = cd;
            RunScenario(weight, Converter.inHg2pa(29.92), Converter.celc2kel(15.0), 2455.0, expv2, 2020.0, 143.0);
            RunScenario(weight, Converter.inHg2pa(28.56), Converter.celc2kel(17.0), 2475.0, expv2, 2305.0, 141.0);
            RunScenario(weight, Converter.inHg2pa(24.49), Converter.celc2kel(9.0),  3657.9, expv2, 3200.0, 158.0);
            RunScenario(weight, Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 1343.5, expv2, 3025.0, 91.0);
            RunScenario(weight, Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 3657.9, expv2, 3025.0, 160.0);
            RunScenario(weight, Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 1504.8, expv2, 2420.0, 103.0);
            RunScenario(weight, Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 2475.0, expv2, 2420.0, 139.0);
        }


        // Feet-Meter: 12001ft = 3657.9m
        //             4408ft  = 1343.5m
        //             4937ft  = 1504.8m
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
        //
        //
        // known cases for testing, speeds in KEAS:
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
    }
}