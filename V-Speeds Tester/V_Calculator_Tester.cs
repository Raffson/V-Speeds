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

        // The following order is used for profile parameters:  lsa, cl, bf, rc, cd, rtr, thr, clg, rfc
        private void SetProfile(int profile)
        {
            var ap = AircraftProfile.Indexer[profile];
            vcalc.Lsa = (double)ap[0];
            vcalc.Cl = (double)ap[1];
            vcalc.Bf = (double)ap[2];
            vcalc.Rc = (double)ap[3];
            vcalc.Cd = (double)ap[4];
            vcalc.Rtr = (double)ap[5];
            vcalc.Thr = (double)ap[6];
            vcalc.Clg = (double)ap[7];
            vcalc.Rfc = (double)ap[8];
        }

        private void RunScenario(double gw, double qfe, double oat, double rl, double vs, double expDv, double expv1, int ap, double cd = 0)
        {
            SetProfile(ap);
            vcalc.Gw = gw;
            vcalc.Qfe = qfe;
            vcalc.Oat = oat;
            vcalc.Rl = rl;
            vcalc.Cd = cd > 0 ? cd : vcalc.Cd;
            Assert.AreEqual(vs, Converter.mps2kts(vcalc.CalcVs().Item1), 1.0);   // Check Vs, assume no thrust
            double nr = vcalc.CalcNeededRunway(); // testdata considers distances where no thrust is assumed for Vs
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
                sumUnder += Math.Abs(diff);
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
            System.Diagnostics.Debug.WriteLine($"  Estimate error = {diff:N2} for profile {ap}");
        }

        private void PrintStats()
        {
            System.Diagnostics.Debug.WriteLine($"{underestimates} underestimations with an average error of {(sumUnder / underestimates):N2}m");
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
                (Converter.inHg2pa(25.46), Converter.celc2kel(11.0), 3063.5, 460.0, 163.0, 8),

                (Converter.inHg2pa(29.92), Converter.celc2kel(15.0), 2455.0, 600.0, 153.0, 9),
                (Converter.inHg2pa(28.56), Converter.celc2kel(17.0), 2475.0, 675.0, 152.0, 9),
                (Converter.inHg2pa(24.49), Converter.celc2kel(9.0),  3657.9, 905.0, 170.0, 9),
                (Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 1343.5, 870.0, 104.0, 9),
                (Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 1504.8, 695.0, 118.0, 9),
                (Converter.inHg2pa(26.74), Converter.celc2kel(14.0), 1859.3, 765.0, 131.0, 9),
                (Converter.inHg2pa(25.46), Converter.celc2kel(11.0), 3063.5, 840.0, 161.0, 9),
            };
            foreach (var (qfe, oat, rl, expDv, expV1, ap) in data) RunScenario(weight, qfe, oat, rl, expv2, expDv, expV1, ap);
            PrintStats();
        }

        [TestMethod]
        public void DCS_F16C_39857lbs_Tester() // Tests for F16 at 39857lbs, CD = 0.126
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
                (Converter.inHg2pa(24.49), Converter.celc2kel(9.0),  3657.9, 1540.0, 168.0, 8), // V1 is high...
                (Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 1343.5, 1465.0, 98.0,  8),
                (Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 3657.9, 1465.0, 174.0, 8), // V1 is high...
                (Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 1504.8, 1195.0, 113.0, 8),
                (Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 2475.0, 1195.0, 149.0, 8),
                (Converter.inHg2pa(26.74), Converter.celc2kel(14.0), 1859.3, 1300.0, 125.0, 8),
                (Converter.inHg2pa(25.46), Converter.celc2kel(11.0), 3063.5, 1420.0, 159.0, 8), // V1 is okay here...

                (Converter.inHg2pa(29.92), Converter.celc2kel(15.0), 2455.0, 2020.0, 143.0, 9),
                (Converter.inHg2pa(28.56), Converter.celc2kel(17.0), 2475.0, 2305.0, 141.0, 9),
                (Converter.inHg2pa(24.49), Converter.celc2kel(9.0),  3657.9, 3200.0, 158.0, 9), // V1 is high...
                (Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 1343.5, 3025.0, 91.0,  9),
                (Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 3657.9, 3025.0, 160.0, 9), // V1 is high...
                (Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 1504.8, 2420.0, 103.0, 9),
                (Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 2475.0, 2420.0, 139.0, 9),
                (Converter.inHg2pa(26.74), Converter.celc2kel(14.0), 1859.3, 2635.0, 114.0, 9),
                (Converter.inHg2pa(25.46), Converter.celc2kel(11.0), 3063.5, 2900.0, 147.0, 9), // V1 is okay here...
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
                (Converter.inHg2pa(25.46), Converter.celc2kel(11.0), 3645.4, 395.0, 145.0, 10),
                (Converter.inHg2pa(28.01), Converter.celc2kel(16.0), 3063.5, 340.0, 135.0, 10),

                (Converter.inHg2pa(29.92), Converter.celc2kel(15.0), 2455.0, 405.0, 147.0, 11),
                (Converter.inHg2pa(28.56), Converter.celc2kel(17.0), 2475.0, 450.0, 147.0, 11),
                (Converter.inHg2pa(24.49), Converter.celc2kel(9.0),  3657.9, 600.0, 165.0, 11),
                (Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 1343.5, 570.0, 100.0, 11),
                (Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 1504.8, 475.0, 113.0, 11),
                (Converter.inHg2pa(26.74), Converter.celc2kel(14.0), 1859.3, 525.0, 125.0, 11),
                (Converter.inHg2pa(25.46), Converter.celc2kel(11.0), 3645.4, 565.0, 162.0, 11),
                (Converter.inHg2pa(28.01), Converter.celc2kel(16.0), 3063.5, 485.0, 155.0, 11),
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
                (Converter.inHg2pa(25.46), Converter.celc2kel(11.0), 3645.4, 1035.0, 147.0, 10),
                (Converter.inHg2pa(28.01), Converter.celc2kel(16.0), 3063.5, 890.0,  136.0, 10),

                (Converter.inHg2pa(29.92), Converter.celc2kel(15.0), 2455.0, 1145.0, 139.0, 11),
                (Converter.inHg2pa(28.56), Converter.celc2kel(17.0), 2475.0, 1320.0, 136.0, 11),
                (Converter.inHg2pa(24.49), Converter.celc2kel(9.0),  3657.9, 1720.0, 154.0, 11),
                (Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 1504.8, 1350.0, 103.0, 11),
                (Converter.inHg2pa(26.74), Converter.celc2kel(14.0), 1859.3, 1475.0, 112.0, 11),
                (Converter.inHg2pa(25.46), Converter.celc2kel(11.0), 3645.4, 1580.0, 156.0, 11),
                (Converter.inHg2pa(28.01), Converter.celc2kel(16.0), 3063.5, 1360.0, 150.0, 11),
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
                (Converter.inHg2pa(25.46), Converter.celc2kel(11.0), 3645.4, 905.0,  155.0, 1),
                (Converter.inHg2pa(28.01), Converter.celc2kel(16.0), 3063.5, 795.0,  151.0, 1),
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
                (Converter.inHg2pa(25.46), Converter.celc2kel(11.0), 3645.4, 2250.0,  151.0, 1),
                (Converter.inHg2pa(28.01), Converter.celc2kel(16.0), 3063.5, 1945.0,  146.0, 1),
            };
            foreach (var (qfe, oat, rl, expDv, expV1, ap) in data) RunScenario(weight, qfe, oat, rl, expv2, expDv, expV1, ap, cd);
            PrintStats();
        }

        [TestMethod]
        public void DCS_F14B_52660lbs_Tester() // Tests for F14B at 52660lbs, CD = 0.058 (default)
        {
            double weight = Converter.lbs2kgs(52660.0);
            double expv2 = 131.0;
            var data = new (double qfe, double oat, double rl, double expDv, double expV1, int ap)[] {
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1800.0, 260.0, 132.0, 4),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 2400.0, 260.0, 160.0, 4),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1800.0, 450.0, 133.0, 5),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 2400.0, 450.0, 156.0, 5),

                (Converter.inHg2pa(29.92), Converter.celc2kel(15.0), 2455.0, 255.0, 165.0, 4),
                (Converter.inHg2pa(28.56), Converter.celc2kel(17.0), 2475.0, 300.0, 160.0, 4),
                (Converter.inHg2pa(24.49), Converter.celc2kel(9.0),  3657.9, 365.0, 192.0, 4),
                (Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 1343.5, 350.0, 102.0, 4),
                (Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 1504.8, 310.0, 119.0, 4),
                (Converter.inHg2pa(26.74), Converter.celc2kel(14.0), 1859.3, 335.0, 130.0, 4),
                (Converter.inHg2pa(25.46), Converter.celc2kel(11.0), 3063.5, 340.0, 176.0, 4),

                (Converter.inHg2pa(29.92), Converter.celc2kel(15.0), 2455.0, 430.0, 158.0, 5),
                (Converter.inHg2pa(28.56), Converter.celc2kel(17.0), 2475.0, 450.0, 155.0, 5),
                (Converter.inHg2pa(24.49), Converter.celc2kel(9.0),  3657.9, 575.0, 185.0, 5),
                (Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 1343.5, 585.0, 100.0, 5),
                (Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 1504.8, 505.0, 115.0, 5),
                (Converter.inHg2pa(26.74), Converter.celc2kel(14.0), 1859.3, 540.0, 130.0, 5),
                (Converter.inHg2pa(25.46), Converter.celc2kel(11.0), 3063.5, 580.0, 170.0, 5),
            };
            foreach (var (qfe, oat, rl, expDv, expV1, ap) in data) RunScenario(weight, qfe, oat, rl, expv2, expDv, expV1, ap);
            PrintStats(); 
        }

        [TestMethod]
        public void DCS_F14B_74384lbs_Tester() // Tests for F14B at 74384lbs, CD = 0.064
        {
            double weight = Converter.lbs2kgs(74384.0);
            double expv2 = 156.0;
            double cd = 0.064;
            var data = new (double qfe, double oat, double rl, double expDv, double expV1, int ap)[] {
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1800.0, 545.0, 119.0, 4),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 2400.0, 545.0, 142.0, 4),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1800.0, 925.0, 117.0, 5),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 2400.0, 925.0, 138.0, 5),

                (Converter.inHg2pa(29.92), Converter.celc2kel(15.0), 2455.0, 525.0, 144.0, 4),
                (Converter.inHg2pa(28.56), Converter.celc2kel(17.0), 2475.0, 560.0, 142.0, 4),
                (Converter.inHg2pa(24.49), Converter.celc2kel(9.0),  3657.9, 760.0, 165.0, 4),
                (Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 1343.5, 680.0, 90.0,  4),
                (Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 1504.8, 610.0, 105.0, 4),
                (Converter.inHg2pa(26.74), Converter.celc2kel(14.0), 1859.3, 660.0, 117.0, 4),
                (Converter.inHg2pa(25.46), Converter.celc2kel(11.0), 3063.5, 660.0, 155.0, 4),

                (Converter.inHg2pa(29.92), Converter.celc2kel(15.0), 2455.0, 860.0,  140.0, 5),
                (Converter.inHg2pa(28.56), Converter.celc2kel(17.0), 2475.0, 920.0,  138.0, 5),
                (Converter.inHg2pa(24.49), Converter.celc2kel(9.0),  3657.9, 1165.0, 160.0, 5),
                (Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 1343.5, 1120.0, 87.0,  5),
                (Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 1504.8, 950.0,  100.0, 5),
                (Converter.inHg2pa(26.74), Converter.celc2kel(14.0), 1859.3, 1015.0, 114.0, 5),
                (Converter.inHg2pa(25.46), Converter.celc2kel(11.0), 3063.5, 1095.0, 150.0, 5),
            };
            foreach (var (qfe, oat, rl, expDv, expV1, ap) in data) RunScenario(weight, qfe, oat, rl, expv2, expDv, expV1, ap, cd);
            PrintStats();
        }

        [TestMethod]
        public void DCS_F14A_50706lbs_Tester() // Tests for F14A at 50706lbs, CD = 0.058 (default)
        {
            double weight = Converter.lbs2kgs(50706.0);
            double expv2 = 129.0;
            var data = new (double qfe, double oat, double rl, double expDv, double expV1, int ap)[] {
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1800.0, 360.0, 141.0, 2),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 2400.0, 360.0, 169.0, 2),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1800.0, 570.0, 131.0, 3),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 2400.0, 570.0, 157.0, 3),

                (Converter.inHg2pa(29.92), Converter.celc2kel(15.0), 2455.0, 330.0, 170.0, 2),
                (Converter.inHg2pa(28.56), Converter.celc2kel(17.0), 2475.0, 400.0, 168.0, 2),
                (Converter.inHg2pa(24.49), Converter.celc2kel(9.0),  3657.9, 470.0, 197.0, 2),
                (Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 1343.5, 445.0, 107.0, 2),
                (Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 1504.8, 410.0, 123.0, 2),
                (Converter.inHg2pa(26.74), Converter.celc2kel(14.0), 1859.3, 440.0, 139.0, 2),
                (Converter.inHg2pa(25.46), Converter.celc2kel(11.0), 3063.5, 465.0, 185.0, 2),

                (Converter.inHg2pa(29.92), Converter.celc2kel(15.0), 2455.0, 545.0, 162.0, 3),
                (Converter.inHg2pa(28.56), Converter.celc2kel(17.0), 2475.0, 625.0, 155.0, 3),
                (Converter.inHg2pa(24.49), Converter.celc2kel(9.0),  3657.9, 790.0, 181.0, 3),
                (Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 1343.5, 750.0, 100.0, 3),
                (Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 1504.8, 640.0, 116.0, 3),
                (Converter.inHg2pa(26.74), Converter.celc2kel(14.0), 1859.3, 695.0, 129.0, 3),
                (Converter.inHg2pa(25.46), Converter.celc2kel(11.0), 3063.5, 745.0, 168.0, 3),
            };
            foreach (var (qfe, oat, rl, expDv, expV1, ap) in data) RunScenario(weight, qfe, oat, rl, expv2, expDv, expV1, ap);
            PrintStats();
        }

        [TestMethod]
        public void DCS_F14A_73440lbs_Tester() // Tests for F14A at 73440lbs, CD = 0.064
        {
            double weight = Converter.lbs2kgs(73440.0);
            double expv2 = 155.0;
            double cd = 0.064;
            var data = new (double qfe, double oat, double rl, double expDv, double expV1, int ap)[] {
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1800.0, 750.0,  125.0, 2),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 2400.0, 750.0,  144.0, 2),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1800.0, 1255.0, 116.0, 3),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 2400.0, 1255.0, 135.0, 3), // V1 low?

                (Converter.inHg2pa(29.92), Converter.celc2kel(15.0), 2455.0, 690.0,  147.0, 2),
                (Converter.inHg2pa(28.56), Converter.celc2kel(17.0), 2475.0, 775.0,  145.0, 2),
                (Converter.inHg2pa(24.49), Converter.celc2kel(9.0),  3657.9, 995.0,  167.0, 2),
                (Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 1343.5, 1025.0, 92.0,  2),
                (Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 1504.8, 865.0,  109.0, 2),
                (Converter.inHg2pa(26.74), Converter.celc2kel(14.0), 1859.3, 935.0,  121.0, 2),
                (Converter.inHg2pa(25.46), Converter.celc2kel(11.0), 3063.5, 1005.0, 154.0, 2),

                (Converter.inHg2pa(29.92), Converter.celc2kel(15.0), 2455.0, 1190.0, 137.0, 3),
                (Converter.inHg2pa(28.56), Converter.celc2kel(17.0), 2475.0, 1280.0, 133.0, 3), // V1 low?
                (Converter.inHg2pa(24.49), Converter.celc2kel(9.0),  3657.9, 1655.0, 153.0, 3),
                (Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 1343.5, 1650.0, 89.0,  3),
                (Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 1504.8, 1430.0, 101.0, 3),
                (Converter.inHg2pa(26.74), Converter.celc2kel(14.0), 1859.3, 1560.0, 111.0, 3),
                (Converter.inHg2pa(25.46), Converter.celc2kel(11.0), 3063.5, 1670.0, 143.0, 3), // V1 low
            };
            foreach (var (qfe, oat, rl, expDv, expV1, ap) in data) RunScenario(weight, qfe, oat, rl, expv2, expDv, expV1, ap, cd);
            PrintStats();
        }

        [TestMethod]
        public void DCS_F15C_36764lbs_Tester() // Tests for F15C at 36764lbs, CD = 0.085 (default)
        {
            double weight = Converter.lbs2kgs(36764.0);
            double expv2 = 159.0;
            var data = new (double qfe, double oat, double rl, double expDv, double expV1, int ap)[] {
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1800.0, 320.0, 150.0, 6),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 2400.0, 320.0, 180.0, 6),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1800.0, 530.0, 150.0, 7),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 2400.0, 530.0, 180.0, 7),

                (Converter.inHg2pa(29.92), Converter.celc2kel(15.0), 2455.0, 310.0, 180.0, 6),
                (Converter.inHg2pa(28.56), Converter.celc2kel(17.0), 2475.0, 350.0, 180.0, 6),
                (Converter.inHg2pa(24.49), Converter.celc2kel(9.0),  3657.9, 445.0, 215.0, 6),
                (Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 1343.5, 425.0, 110.0, 6),
                (Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 1504.8, 360.0, 130.0, 6),
                (Converter.inHg2pa(26.74), Converter.celc2kel(14.0), 1859.3, 385.0, 150.0, 6),
                (Converter.inHg2pa(25.46), Converter.celc2kel(11.0), 3063.5, 420.0, 200.0, 6),

                (Converter.inHg2pa(29.92), Converter.celc2kel(15.0), 2455.0, 510.0, 183.0, 7),
                (Converter.inHg2pa(28.56), Converter.celc2kel(17.0), 2475.0, 570.0, 180.0, 7), 
                (Converter.inHg2pa(24.49), Converter.celc2kel(9.0),  3657.9, 750.0, 210.0, 7),
                (Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 1343.5, 715.0, 112.0, 7),
                (Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 1504.8, 590.0, 135.0, 7),
                (Converter.inHg2pa(26.74), Converter.celc2kel(14.0), 1859.3, 640.0, 150.0, 7),
                (Converter.inHg2pa(25.46), Converter.celc2kel(11.0), 3063.5, 695.0, 195.0, 7),
            };
            foreach (var (qfe, oat, rl, expDv, expV1, ap) in data) RunScenario(weight, qfe, oat, rl, expv2, expDv, expV1, ap);
            PrintStats();
        }

        [TestMethod]
        public void DCS_F15C_59639lbs_Tester() // Tests for F15C at 59639lbs, CD = 0.097
        {
            double weight = Converter.lbs2kgs(59639.0);
            double expv2 = 202.0;
            double cd = 0.097;
            var data = new (double qfe, double oat, double rl, double expDv, double expV1, int ap)[] {
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1800.0, 900.0,  130.0, 6),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 2400.0, 900.0,  155.0, 6),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1800.0, 1580.0, 127.0, 7),
                (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 2400.0, 1580.0, 150.0, 7),

                (Converter.inHg2pa(29.92), Converter.celc2kel(15.0), 2455.0, 890.0,  159.0, 6),
                (Converter.inHg2pa(28.56), Converter.celc2kel(17.0), 2475.0, 1000.0, 155.0, 6),
                (Converter.inHg2pa(24.49), Converter.celc2kel(9.0),  3657.9, 1315.0, 185.0, 6),
                (Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 1343.5, 1240.0, 100.0, 6),
                (Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 1504.8, 1035.0, 115.0, 6),
                (Converter.inHg2pa(26.74), Converter.celc2kel(14.0), 1859.3, 1115.0, 130.0, 6),
                (Converter.inHg2pa(25.46), Converter.celc2kel(11.0), 3063.5, 1220.0, 170.0, 6),

                (Converter.inHg2pa(29.92), Converter.celc2kel(15.0), 2455.0, 1575.0, 153.0, 7),
                (Converter.inHg2pa(28.56), Converter.celc2kel(17.0), 2475.0, 1810.0, 150.0, 7), 
                (Converter.inHg2pa(24.49), Converter.celc2kel(9.0),  3657.9, 2410.0, 170.0, 7), // beware, if tires blow, friction is higher => more runway needed!!!
                // (Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 1343.5, 0.0, 95.0, 7), // not enough runway...
                // (Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 1504.8, 0.0, 115.0, 7), // not enough runway...
                (Converter.inHg2pa(26.74), Converter.celc2kel(14.0), 1859.3, 2050.0, 125.0, 7),
                (Converter.inHg2pa(25.46), Converter.celc2kel(11.0), 3063.5, 2290.0, 160.0, 7),
            };
            foreach (var (qfe, oat, rl, expDv, expV1, ap) in data) RunScenario(weight, qfe, oat, rl, expv2, expDv, expV1, ap, cd);
            PrintStats();
        }

        // TODO: - Gather testdata with a density ratio larger than 1 and see what happens with our predicitions...
        //
        // Feet-Meter: 12001ft = 3657.9m
        //             4408ft  = 1343.5m
        //             4937ft  = 1504.8m
        //             6100ft  = 1859.3m
        //             11960ft = 3645.4m
        //             10051ft = 3063.5m
        //
        // Data template:
        //  (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1800.0, 0.0, 0.0, X),
        //  (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 2400.0, 0.0, 0.0, X),
        //  (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 1800.0, 0.0, 0.0, X),
        //  (Converter.inHg2pa(30.05), Converter.celc2kel(20.0), 2400.0, 0.0, 0.0, X),

        //  (Converter.inHg2pa(29.92), Converter.celc2kel(15.0), 2455.0, 0.0, 0.0, X),
        //  (Converter.inHg2pa(28.56), Converter.celc2kel(17.0), 2475.0, 0.0, 0.0, X),
        //  (Converter.inHg2pa(24.49), Converter.celc2kel(9.0),  3657.9, 0.0, 0.0, X),
        //  (Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 1343.5, 0.0, 0.0, X),
        //  (Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 1504.8, 0.0, 0.0, X),
        //  (Converter.inHg2pa(26.74), Converter.celc2kel(14.0), 1859.3, 0.0, 0.0, X),
        //  (Converter.inHg2pa(25.46), Converter.celc2kel(11.0), 3063.5, 0.0, 0.0, X),

        //  (Converter.inHg2pa(29.92), Converter.celc2kel(15.0), 2455.0, 0.0, 0.0, X),
        //  (Converter.inHg2pa(28.56), Converter.celc2kel(17.0), 2475.0, 0.0, 0.0, X), 
        //  (Converter.inHg2pa(24.49), Converter.celc2kel(9.0),  3657.9, 0.0, 0.0, X),
        //  (Converter.inHg2pa(25.15), Converter.celc2kel(10.0), 1343.5, 0.0, 0.0, X),
        //  (Converter.inHg2pa(28.00), Converter.celc2kel(16.0), 1504.8, 0.0, 0.0, X),
        //  (Converter.inHg2pa(26.74), Converter.celc2kel(14.0), 1859.3, 0.0, 0.0, X),
        //  (Converter.inHg2pa(25.46), Converter.celc2kel(11.0), 3063.5, 0.0, 0.0, X),
    }
}