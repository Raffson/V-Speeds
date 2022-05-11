using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using V_Speeds;

namespace V_Speeds_Tester
{
    [TestClass]
    public class V_Calculator_Tester
    {
        private readonly V_Calculator vcalc = new();

        private int underestimates = 0;
        private double sumUnder = 0; // sum of underestimation error percentages
        private double smallestUnder = double.PositiveInfinity;
        private double largestUnder = 0.0;
        private int overestimates = 0;
        private double sumOver = 0; // sum of overestimation error percentages
        private double smallestOver = double.PositiveInfinity;
        private double largestOver = 0.0;

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
            double nr = vcalc.CalcNeededRunway();
            double diff = nr - expDv;
            double tolerance = expDv * 0.1; // 10% tolerance for overestimations, may want to narrow this down but i'll need engine-profiles...
            Assert.IsTrue(diff > -10, $"Dv is underestimated by more than 10m: {expDv}m expected but got {nr}m\n{vcalc}");
            Assert.IsTrue(diff < tolerance, $"Dv is overestimated by more than 10%: {expDv}m expected but got {nr}m\n{vcalc}");
            double v1 = Converter.mps2kts(vcalc.CalcV1().Item1);
            tolerance = 3 + (rl / 3000); // 3 kts tolerance + 1 kts for every 3000m of runway
            Assert.AreEqual(expv1, v1, tolerance, $"\n{vcalc}");
            if( nr < expDv )
            {
                underestimates += 1;
                sumUnder += (Math.Abs(diff) / expDv);
                smallestUnder = Math.Abs(diff) < smallestUnder ? Math.Abs(diff) : smallestUnder;
                largestUnder = Math.Abs(diff) > largestUnder ? Math.Abs(diff) : largestUnder;
            }
            else
            {
                overestimates += 1;
                sumOver += (Math.Abs(diff) / expDv);
                smallestOver = Math.Abs(diff) < smallestOver ? Math.Abs(diff) : smallestOver;
                largestOver = Math.Abs(diff) > largestOver ? Math.Abs(diff) : largestOver;
            }
            System.Diagnostics.Debug.WriteLine($"  Estimate error = {diff}");
        }

        private void PrintStats()
        {
            System.Diagnostics.Debug.WriteLine($"{underestimates} underestimations with an average error of {(100 * sumUnder / underestimates):N2}%");
            System.Diagnostics.Debug.WriteLine($"{overestimates} overestimations with an average error of {(100 * sumOver / overestimates):N2}%");
            System.Diagnostics.Debug.WriteLine($"Largest / Smallest Underestimate: {largestUnder:N2} / {smallestUnder:N2}");
            System.Diagnostics.Debug.WriteLine($"Largest / Smallest Overestimate: {largestOver:N2} / {smallestOver:N2}");
        }

        [TestMethod]
        public void DCS_F16C_23775lbs_Tester() // Tests for F16 at 23775lbs, CD = 0.095 (default)
        {
            double weight = Converter.lbs2kgs(23775.0);
            double expv2 = 161.0;
            var data = new(double qfe, double oat, double rl, double expDv, double expV1, int ap)[] { 
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1600.0, 340.0, 126.0, 8),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1800.0, 340.0, 134.0, 8),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 2400.0, 340.0, 148.0, 8),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1600.0, 610.0, 124.0, 9),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1800.0, 610.0, 132.0, 9),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 2400.0, 610.0, 151.0, 9),

                (Converter.inHg2pa(29.92), Converter.celc2kel(15.0), 2455.0, 335.0, 149.0, 8),
                (Converter.inHg2pa(28.56), Converter.celc2kel(17.0), 2475.0, 375.0, 150.0, 8),
                (Converter.inHg2pa(24.49), Converter.celc2kel(9.0),  3657.9, 500.0, 167.0, 8),
                (Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 1343.5, 470.0, 109.0, 8),
                (Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 1504.8, 385.0, 122.0, 8),
                (Converter.inHg2pa(26.74), Converter.celc2kel(14.0), 1859.3, 425.0, 135.0, 8),

                (Converter.inHg2pa(29.92), Converter.celc2kel(15.0), 2455.0, 600.0, 153.0, 9),
                (Converter.inHg2pa(28.56), Converter.celc2kel(17.0), 2475.0, 675.0, 152.0, 9),
                (Converter.inHg2pa(24.49), Converter.celc2kel(9.0),  3657.9, 905.0, 170.0, 9),
                (Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 1343.5, 870.0, 104.0, 9),
                (Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 1504.8, 695.0, 118.0, 9),
                (Converter.inHg2pa(26.74), Converter.celc2kel(14.0), 1859.3, 765.0, 131.0, 9),
            };
            foreach (var (qfe, oat, rl, expDv, expV1, ap) in data) RunScenario(weight, qfe, oat, rl, expv2, expDv, expV1, ap);
            PrintStats();
        }

        [TestMethod]
        public void DCS_F16C_39857lbs_Tester() // Tests for F16 at 23775lbs, CD = 0.12
        {
            double weight = Converter.lbs2kgs(39857.0);
            double expv2 = 208.0;
            double cd = 0.126;

            var data = new (double qfe, double oat, double rl, double expDv, double expV1, int ap)[] {
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1650.0, 1030.0, 123.0, 8),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1800.0, 1030.0, 129.0, 8),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 2400.0, 1030.0, 151.0, 8),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1650.0, 2040.0, 115.0, 9),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1800.0, 2040.0, 121.0, 9),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 2400.0, 2040.0, 142.0, 9),

                (Converter.inHg2pa(29.92), Converter.celc2kel(15.0), 2455.0, 1025.0, 152.0, 8),
                (Converter.inHg2pa(28.56), Converter.celc2kel(17.0), 2475.0, 1145.0, 150.0, 8),
                (Converter.inHg2pa(24.49), Converter.celc2kel(9.0),  3657.9, 1540.0, 172.0, 8),
                (Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 1343.5, 1465.0, 98.0,  8),
                (Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 3657.9, 1465.0, 174.0, 8),
                (Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 1504.8, 1195.0, 113.0, 8),
                (Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 2475.0, 1195.0, 149.0, 8),
                (Converter.inHg2pa(26.74), Converter.celc2kel(14.0), 1859.3, 1300.0, 125.0, 8),

                (Converter.inHg2pa(29.92), Converter.celc2kel(15.0), 2455.0, 2020.0, 143.0, 9),
                (Converter.inHg2pa(28.56), Converter.celc2kel(17.0), 2475.0, 2305.0, 141.0, 9),
                (Converter.inHg2pa(24.49), Converter.celc2kel(9.0),  3657.9, 3200.0, 158.0, 9),
                (Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 1343.5, 3025.0, 91.0,  9),
                (Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 3657.9, 3025.0, 160.0, 9),
                (Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 1504.8, 2420.0, 103.0, 9),
                (Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 2475.0, 2420.0, 139.0, 9),
                (Converter.inHg2pa(26.74), Converter.celc2kel(14.0), 1859.3, 2635.0, 114.0, 9),
            };
            foreach (var (qfe, oat, rl, expDv, expV1, ap) in data) RunScenario(weight, qfe, oat, rl, expv2, expDv, expV1, ap, cd);
            PrintStats();
        }

        [TestMethod]
        public void DCS_F18C_30955lbs_Tester() // Tests for F18 at 30955lbs, CD = 0.12 (default)
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

                (Converter.inHg2pa(29.92), Converter.celc2kel(15.0), 2455.0, 300.0, 120.0, 10),
                (Converter.inHg2pa(28.56), Converter.celc2kel(17.0), 2475.0, 325.0, 123.0, 10),
                (Converter.inHg2pa(24.49), Converter.celc2kel(9.0),  3657.9, 420.0, 148.0, 10),
                (Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 1343.5, 400.0, 74.0,  10),
                (Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 1504.8, 340.0, 87.0,  10),
                (Converter.inHg2pa(26.74), Converter.celc2kel(14.0), 1859.3, 365.0, 101.0, 10),

                (Converter.inHg2pa(29.92), Converter.celc2kel(15.0), 2455.0, 405.0, 147.0, 11),
                (Converter.inHg2pa(28.56), Converter.celc2kel(17.0), 2475.0, 450.0, 147.0, 11),
                (Converter.inHg2pa(24.49), Converter.celc2kel(9.0),  3657.9, 600.0, 165.0, 11),
                (Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 1343.5, 570.0, 100.0, 11),
                (Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 1504.8, 475.0, 113.0, 11),
                (Converter.inHg2pa(26.74), Converter.celc2kel(14.0), 1859.3, 525.0, 125.0, 11),
            };
            foreach (var (qfe, oat, rl, expDv, expV1, ap) in data) RunScenario(weight, qfe, oat, rl, expv2, expDv, expV1, ap);
            PrintStats();
        }

        [TestMethod]
        public void DCS_F18C_49110lbs_Tester() // Tests for F18 at 49110lbs, CD = 0.15
        {
            double weight = Converter.lbs2kgs(49110.0);
            double expv2 = 184.0;
            double cd = 0.15;
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
                (Converter.inHg2pa(26.74), Converter.celc2kel(14.0), 1859.3, 975.0,  97.0,  10),

                (Converter.inHg2pa(29.92), Converter.celc2kel(15.0), 2455.0, 1145.0, 139.0, 11),
                (Converter.inHg2pa(28.56), Converter.celc2kel(17.0), 2475.0, 1320.0, 136.0, 11),
                (Converter.inHg2pa(24.49), Converter.celc2kel(9.0),  3657.9, 1720.0, 154.0, 11),
                (Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 1504.8, 1350.0, 103.0, 11),
                (Converter.inHg2pa(26.74), Converter.celc2kel(14.0), 1859.3, 1475.0, 112.0, 11),
            };
            foreach (var (qfe, oat, rl, expDv, expV1, ap) in data) RunScenario(weight, qfe, oat, rl, expv2, expDv, expV1, ap, cd);
            PrintStats();
        }

        [TestMethod]
        public void DCS_A10_32948lbs_Tester() // Tests for A10 at 32948lbs, CD = 0.08 (default)
        {
            double weight = Converter.lbs2kgs(32948.0);
            double expv2 = 137.0;
            var data = new (double qfe, double oat, double rl, double expDv, double expV1, int ap)[] {
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1650.0, 705.0,  120.0, 1),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1800.0, 705.0,  125.0, 1),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 2400.0, 705.0,  142.0, 1),

                (Converter.inHg2pa(29.92), Converter.celc2kel(15.0), 2455.0, 665.0,  142.0, 1),
                (Converter.inHg2pa(28.56), Converter.celc2kel(17.0), 2475.0, 760.0,  143.0, 1),
                (Converter.inHg2pa(24.49), Converter.celc2kel(9.0),  3657.9, 970.0,  154.0, 1),
                (Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 1343.5, 940.0,  99.0,  1),
                (Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 1504.8, 785.0,  112.0, 1),
                (Converter.inHg2pa(26.74), Converter.celc2kel(14.0), 1859.3, 850.0,  123.0, 1),
            };
            foreach (var (qfe, oat, rl, expDv, expV1, ap) in data) RunScenario(weight, qfe, oat, rl, expv2, expDv, expV1, ap);
            PrintStats();
        }

        [TestMethod]
        public void DCS_A10_47093lbs_Tester() // Tests for A10 at 47093lbs, CD = 0.116
        {
            // Fails because CalcThrust is not accurate for its engines, interesting how lighter weight passes through though...
            // Currently we have a fitting...
            double weight = Converter.lbs2kgs(47093.0);
            double expv2 = 163.0;
            double cd = 0.116;
            var data = new (double qfe, double oat, double rl, double expDv, double expV1, int ap)[] {
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1650.0, 1720.0,  110.0, 1),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1800.0, 1720.0,  115.0, 1),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 2400.0, 1720.0,  135.0, 1),

                (Converter.inHg2pa(29.92), Converter.celc2kel(15.0), 2455.0, 1600.0,  137.0, 1),
                (Converter.inHg2pa(28.56), Converter.celc2kel(17.0), 2475.0, 1890.0,  132.0, 1),
                (Converter.inHg2pa(24.49), Converter.celc2kel(9.0),  3657.9, 2240.0,  145.0, 1),
                (Converter.inHg2pa(26.74), Converter.celc2kel(14.0), 1859.3, 2110.0,  110.0, 1),
            };
            foreach (var (qfe, oat, rl, expDv, expV1, ap) in data) RunScenario(weight, qfe, oat, rl, expv2, expDv, expV1, ap, cd);
            PrintStats();
        }

        // Feet-Meter: 12001ft = 3657.9m
        //             4408ft  = 1343.5m
        //             4937ft  = 1504.8m
        //             6100ft  = 1859.3m
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