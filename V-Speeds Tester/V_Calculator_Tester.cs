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
        private void SetProfile(int profile)
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

        private void RunScenario(double gw, double qfe, double oat, double rl, double vs, double expDv, double expv1, int ap, double cd = 0)
        {
            SetProfile(ap);
            vcalc.Gw = gw;
            vcalc.Qfe = qfe;
            vcalc.Oat = oat;
            vcalc.Rl = rl;
            vcalc.Cd = cd > 0 ? cd : vcalc.Cd;
            Assert.AreEqual(vs, Converter.mps2kts(vcalc.CalcVs().Item1), 1.0);   // Check Vs
            double result = vcalc.CalcNeededRunway();
            double diff = expDv - result;
            double tolerance = expDv * -0.08; // 8% tolerance for overestimations...
            Assert.IsTrue(diff < 10, $"Dv is underestimated by more than 10m: {expDv}m expected but got {result}m\n{vcalc}");
            Assert.IsTrue(diff > tolerance, $"Dv is overestimated by more than 8%: {expDv}m expected but got {result}m\n{vcalc}");
            double v1 = Converter.mps2kts(vcalc.CalcV1().Item1);
            Assert.AreEqual(expv1, v1, 3, $"\n{vcalc}");
        }

        [TestMethod]
        public void DCS_F16C_23775lbs_Tester() // Tests for F16 at 23775lbs, CD = 0.1 (default)
        {
            double weight = Converter.lbs2kgs(23775.0);
            double expv2 = 161.0;
            var data = new(double qfe, double oat, double rl, double expDv, double expV1, int ap)[] { 
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1600.0, 340.0, 126.0, 8),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1800.0, 340.0, 134.0, 8),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 2400.0, 340.0, 148.0, 8),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1600.0, 610.0, 124.0, 9),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1800.0, 610.0, 133.0, 9),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 2400.0, 610.0, 152.0, 9),

                (Converter.inHg2pa(29.92), Converter.celc2kel(15.0), 2455.0, 335.0, 149.0, 8),
                (Converter.inHg2pa(28.56), Converter.celc2kel(17.0), 2475.0, 375.0, 150.0, 8),
                (Converter.inHg2pa(24.49), Converter.celc2kel(9.0),  3657.9, 525.0, 167.0, 8),
                (Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 1343.5, 470.0, 110.0, 8),
                (Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 1504.8, 385.0, 123.0, 8),

                (Converter.inHg2pa(29.92), Converter.celc2kel(15.0), 2455.0, 600.0, 153.0, 9),
                (Converter.inHg2pa(28.56), Converter.celc2kel(17.0), 2475.0, 675.0, 153.0, 9),
                (Converter.inHg2pa(24.49), Converter.celc2kel(9.0),  3657.9, 950.0, 170.0, 9),
                (Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 1343.5, 870.0, 104.0, 9),
                (Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 1504.8, 695.0, 118.0, 9),
            };
            foreach (var (qfe, oat, rl, expDv, expV1, ap) in data) RunScenario(weight, qfe, oat, rl, expv2, expDv, expV1, ap);
        }

        [TestMethod]
        public void DCS_F16C_39857lbs_Tester() // Tests for F16 at 23775lbs, CD = 0.124
        {
            double weight = Converter.lbs2kgs(39857.0);
            double expv2 = 208.0;
            double cd = 0.124;

            // For short fields with not enough runway I created the same same atmospheric conditions at tonopah
            // expected runway distances have been confirmed...
            var data = new (double qfe, double oat, double rl, double expDv, double expV1, int ap)[] {
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1650.0, 1030.0, 123.0, 8),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1800.0, 1030.0, 129.0, 8),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 2400.0, 1030.0, 152.0, 8),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1650.0, 2035.0, 116.0, 9),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1800.0, 2035.0, 123.0, 9),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 2400.0, 2035.0, 142.0, 9),

                (Converter.inHg2pa(29.92), Converter.celc2kel(15.0), 2455.0, 1025.0, 152.0, 8),
                (Converter.inHg2pa(28.56), Converter.celc2kel(17.0), 2475.0, 1145.0, 150.0, 8),
                (Converter.inHg2pa(24.49), Converter.celc2kel(9.0),  3657.9, 1540.0, 172.0, 8),
                (Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 1343.5, 1465.0, 98.0,  8),
                (Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 3657.9, 1465.0, 174.0, 8),
                (Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 1504.8, 1195.0, 113.0, 8),
                (Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 2475.0, 1195.0, 148.0, 8),

                (Converter.inHg2pa(29.92), Converter.celc2kel(15.0), 2455.0, 2020.0, 143.0, 9),
                (Converter.inHg2pa(28.56), Converter.celc2kel(17.0), 2475.0, 2305.0, 141.0, 9),
                (Converter.inHg2pa(24.49), Converter.celc2kel(9.0),  3657.9, 3200.0, 158.0, 9),
                (Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 1343.5, 3025.0, 91.0,  9),
                (Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 3657.9, 3025.0, 160.0, 9),
                (Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 1504.8, 2420.0, 103.0, 9),
                (Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 2475.0, 2420.0, 139.0, 9),
            };
            foreach (var (qfe, oat, rl, expDv, expV1, ap) in data) RunScenario(weight, qfe, oat, rl, expv2, expDv, expV1, ap, cd);
        }

        [TestMethod]
        public void DCS_F18C_30955lbs_Tester() // Tests for F18 at 30955lbs, CD = 0.11 (default)
        {
            double weight = Converter.lbs2kgs(30955.0);
            double expv2 = 146.0;
            var data = new (double qfe, double oat, double rl, double expDv, double expV1, int ap)[] {
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1600.0, 305.0, 89.0,  10),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1800.0, 305.0, 99.0,  10),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 2400.0, 305.0, 119.0, 10),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1600.0, 420.0, 120.0, 11),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1800.0, 420.0, 127.0, 11),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 2400.0, 420.0, 145.0, 11),

                (Converter.inHg2pa(29.92), Converter.celc2kel(15.0), 2455.0, 300.0, 119.0, 10),
                (Converter.inHg2pa(28.56), Converter.celc2kel(17.0), 2475.0, 325.0, 124.0, 10),
                (Converter.inHg2pa(24.49), Converter.celc2kel(9.0),  3657.9, 420.0, 150.0, 10),
                (Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 1343.5, 400.0, 77.0,  10),
                (Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 1504.8, 340.0, 87.0,  10),

                (Converter.inHg2pa(29.92), Converter.celc2kel(15.0), 2455.0, 405.0, 147.0, 11),
                (Converter.inHg2pa(28.56), Converter.celc2kel(17.0), 2475.0, 450.0, 148.0, 11),
                (Converter.inHg2pa(24.49), Converter.celc2kel(9.0),  3657.9, 600.0, 167.0, 11),
                (Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 1343.5, 570.0, 101.0, 11),
                (Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 1504.8, 475.0, 113.0, 11),
            };
            foreach (var (qfe, oat, rl, expDv, expV1, ap) in data) RunScenario(weight, qfe, oat, rl, expv2, expDv, expV1, ap);
        }

        [TestMethod]
        public void DCS_F18C_49110lbs_Tester() // Tests for F18 at 49110lbs, CD = 0.127
        {
            double weight = Converter.lbs2kgs(49110.0);
            double expv2 = 184.0;
            double cd = 0.127;
            var data = new (double qfe, double oat, double rl, double expDv, double expV1, int ap)[] {
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1650.0, 780.0,  88.0,  10),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1800.0, 780.0,  96.0,  10),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 2400.0, 780.0,  118.0, 10),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1650.0, 1175.0, 108.0, 11),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1800.0, 1175.0, 116.0, 11),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 2400.0, 1175.0, 137.0, 11),

                (Converter.inHg2pa(29.92), Converter.celc2kel(15.0), 2455.0, 765.0,  118.0, 10),
                (Converter.inHg2pa(28.56), Converter.celc2kel(17.0), 2475.0, 860.0,  120.0, 10),
                (Converter.inHg2pa(24.49), Converter.celc2kel(9.0),  3657.9, 1120.0, 147.0, 10),
                (Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 1343.5, 1055.0, 74.0,  10),
                (Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 1504.8, 880.0,  85.0,  10),

                (Converter.inHg2pa(29.92), Converter.celc2kel(15.0), 2455.0, 1145.0, 139.0, 11),
                (Converter.inHg2pa(28.56), Converter.celc2kel(17.0), 2475.0, 1320.0, 136.0, 11),
                (Converter.inHg2pa(24.49), Converter.celc2kel(9.0),  3657.9, 1720.0, 157.0, 11),
                //(Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 1343.5, 0.0, 0.0, 11), // <- not enough runway, need to test...
                (Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 1504.8, 1350.0, 103.0, 11),
            };
            foreach (var (qfe, oat, rl, expDv, expV1, ap) in data) RunScenario(weight, qfe, oat, rl, expv2, expDv, expV1, ap, cd);
        }

        [TestMethod]
        public void DCS_A10_32948lbs_Tester() // Tests for A10 at 32948lbs, CD = 0.083 (default)
        {
            double weight = Converter.lbs2kgs(32948.0);
            double expv2 = 137.0;
            var data = new (double qfe, double oat, double rl, double expDv, double expV1, int ap)[] {
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1650.0, 705.0,  120.0,  1),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1800.0, 705.0,  125.0,  1),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 2400.0, 705.0,  142.0,  1),

                (Converter.inHg2pa(29.92), Converter.celc2kel(15.0), 2455.0, 665.0,  142.0, 1),
                (Converter.inHg2pa(28.56), Converter.celc2kel(17.0), 2475.0, 760.0,  143.0, 1),
                (Converter.inHg2pa(24.49), Converter.celc2kel(9.0),  3657.9, 970.0,  154.0, 1),
                //(Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 1343.5, 1055.0, 0.0,  1), // not tested yet...
                //(Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 1504.8, 880.0,  0.0,  1), // not tested yet...
            };
            foreach (var (qfe, oat, rl, expDv, expV1, ap) in data) RunScenario(weight, qfe, oat, rl, expv2, expDv, expV1, ap);
        }

        [TestMethod]
        public void DCS_A10_47093lbs_Tester() // Tests for A10 at 47093lbs, CD = 0.116
        {
            // Fails because CalcThrust is not accurate for its engines, interesting how lighter weight passes through though...
            double weight = Converter.lbs2kgs(47093.0);
            double expv2 = 163.0;
            double cd = 0.116;
            var data = new (double qfe, double oat, double rl, double expDv, double expV1, int ap)[] {
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1650.0, 1720.0,  110.0,  1),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1800.0, 1720.0,  115.0,  1),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 2400.0, 1720.0,  135.0,  1),

                (Converter.inHg2pa(29.92), Converter.celc2kel(15.0), 2455.0, 1600.0,  137.0, 1),
                (Converter.inHg2pa(28.56), Converter.celc2kel(17.0), 2475.0, 1890.0,  132.0, 1),
                (Converter.inHg2pa(24.49), Converter.celc2kel(9.0),  3657.9, 2240.0,  145.0, 1),
                //(Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 1343.5, 1055.0, 0.0,  1), // not tested yet...
                //(Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 1504.8, 880.0,  0.0,  1), // not tested yet...
            };
            foreach (var (qfe, oat, rl, expDv, expV1, ap) in data) RunScenario(weight, qfe, oat, rl, expv2, expDv, expV1, ap, cd);
        }

        // Feet-Meter: 12001ft = 3657.9m
        //             4408ft  = 1343.5m
        //             4937ft  = 1504.8m
        //
        // GW = 47093Lbs => Vs = 163; Dv = +/-1600m
        //  CD = 0.116; CLG = 0.55; RFC = 0.034;
        //      RL = 2455m => V1 = 136-139
        //
        // known cases for testing, speeds in KEAS:
        // A10 : OAT = 20C; QFE = 30.05 inHg; RC = 4
        //  GW = 32948Lbs; CD = 0.083  => Vs = 137;
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
        // Data template:
        //  (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1600.0, 0.0, 0.0,  10),
        //  (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1800.0, 0.0, 0.0,  10),
        //  (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 2400.0, 0.0, 0.0, 10),
        //  (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1600.0, 0.0, 0.0, 11),
        //  (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1800.0, 0.0, 0.0, 11),
        //  (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 2400.0, 0.0, 0.0, 11),
        //
        //  (Converter.inHg2pa(29.92), Converter.celc2kel(15.0), 2455.0, 0.0, 0.0, 10),
        //  (Converter.inHg2pa(28.56), Converter.celc2kel(17.0), 2475.0, 0.0, 0.0, 10),
        //  (Converter.inHg2pa(24.49), Converter.celc2kel(9.0),  3657.9, 0.0, 0.0, 10),
        //  (Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 1343.5, 0.0, 0.0,  10),
        //  (Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 1504.8, 0.0, 0.0,  10),
        //
        //  (Converter.inHg2pa(29.92), Converter.celc2kel(15.0), 2455.0, 0.0, 0.0, 11),
        //  (Converter.inHg2pa(28.56), Converter.celc2kel(17.0), 2475.0, 0.0, 0.0, 11),
        //  (Converter.inHg2pa(24.49), Converter.celc2kel(9.0),  3657.9, 0.0, 0.0, 11),
        //  (Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 1343.5, 0.0, 0.0, 11),
        //  (Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 1504.8, 0.0, 0.0, 11),
    }
}