namespace V_Speeds
{
    class V_Calculator
    {
        public const double igc = 8.3144598; // ideal gas constant
        public const double mmair = 28.97 / 1000; // molecular mass of air
        public const double g = 9.83; // 1G at poles (m/s2), just for some extra wiggle room, considering no elevation (except pressure)...
        public const double p0 = 101325 * mmair / (igc * 288.15); // standard sea density


        private double _gw, _oat, _qfe, _lsa, _cl, _thr, _bf, _rl, _csa, _cd, _rtr;


        public V_Calculator(double gw = 1000.0, double oat = 288.15, double qfe = 101325, double lsa = 10.0, double cl = 1.0,
            double thr = 1000.0, double bf = 500.0, double rl = 2500.0, double csa = 1.0, double cd = 0.1, double rtr = 0.0)
        {
            // all members should stay positve, except Cl and Cd... thus setters will use absolute value
            Gw = gw;    // gross weight:         kgs
            Oat = oat;  // outside air temp:     Kelvin
            Qfe = qfe;  // local pressure:       Pascal
            Lsa = lsa;  // lifting surface area: m²
            Cl = cl;    // lift coefficient:     no unit
            Thr = thr;  // thrust:               Newton
            Bf = bf;    // brake force:          Newton
            Rl = rl;    // runway length:        m
            Csa = csa;  // cross sectional area: m²  (seen from the front)
            Cd = cd;    // drag coefficient:     no unit
            Rtr = rtr;  // reverse thrust ratio: no unit
        }
        
        public double Gw { get => _gw; set => _gw = Math.Abs(value); }
        public double Oat { get => _oat; set => _oat = Math.Abs(value); }
        public double Qfe { get => _qfe; set => _qfe = Math.Abs(value); }
        public double Lsa { get => _lsa; set => _lsa = Math.Abs(value); }
        public double Cl { get => _cl; set => _cl = value; }
        public double Thr { get => _thr; set => _thr = Math.Abs(value); }
        public double Bf { get => _bf; set => _bf = Math.Abs(value); }
        public double Rl { get => _rl; set => _rl = Math.Abs(value); }
        public double Csa { get => _csa; set => _csa = Math.Abs(value); }
        public double Cd { get => _cd; set => _cd = value; }
        public double Rtr { get => _rtr; set => _rtr = Math.Abs(value); }

        // internal setter-functions for Form1
        internal void SetGw(double value) => Gw = value;
        internal void SetOat(double value) => Oat = value;
        internal void SetQfe(double value) => Qfe = value;
        internal void SetLsa(double value) => Lsa = value;
        internal void SetCl(double value) => Cl = value;
        internal void SetThr(double value) => Thr = value;
        internal void SetBf(double value) => Bf = value;
        internal void SetRl(double value) => Rl = value;
        internal void SetCsa(double value) => Csa = value;
        internal void SetCd(double value) => Cd = value;
        internal void SetRtr(double value) => Rtr = value;

        private double TAS2EAS(double tas, double rho)
        {
            return tas * Math.Sqrt(rho / p0);
        }

        public (double, double) CalcV1()
        {
            //ignore change in mass => extra safety margin
            //ignore wind => extra safety margin (headwind would increase IAS and decrease GS, thus less distance travelled on runway...)
            //ignore speedbrakes and drag in general during brake => more safety margin because longer braking distance...
            //using EAS to approximate IAS...
            double p = _qfe * mmair / (igc * _oat);
            double t = 0.1;   // time interval 0.1 seconds
            double rc = 2.0;  // reaction time, accounting for engine spooldown, deployment of reversers, etc.
            double tas = 0.0; // assuming no headwind (extra safety) => tas = gs
            double rwl = _rl; // how much runway do we have left...
            while( true )
            {
                // Currently considering F-16C block 50 (DCS) only since there seems to be a problem here...
                //  for some reason the lighter the aircraft, the likelier it'll overshoots the runway
                //  it seems as if the brakeforce is limited, my current guess is the following:
                //      theoretically lighter aircraft should stop faster because of F=m*a,
                //      BUT! a lighter aircraft requires less lift, thus less speed
                //      consequentially the weight on the wheels reduces faster with lighter aircraft,
                //      this in turn means that brake effectiveness will reduce because of insufficient grip (less weight on wheels)...
                //  all of this suggests we might need som extra variables,
                //  but for now we'll test with hardcoded values considering F-16 only...
                // So what do we need exactly....
                //  - CL for AoA when aircraft is level on the ground (call it CLg),
                //    relates to the angle of incidence but flaps should be considered...
                //      -> will guestimate this with 0.52
                //  - Somehow account for the changing brakeforce, slower aircraft = better braking...
                //      -> relates to weight and CLg which we'll use to approximate lift,
                //        which in turn tells us how much weight is still on the wheels
                //  - Account for tire-friction during acceleration,
                //    depends on Fn (normal force) which becomes smaller as we speed up and more lift is generated...
                //    -> Add FRC (Friction Roll Coefficient), assume 0.043 (data from lua file, average of coeffients)
                //  SO LET'S DO THIS...
                double clg = 0.52;
                double frc = 0.043;
                double lift = Math.Pow(tas, 2) * p * _lsa * clg / 2;
                double fg = _gw * g;
                double fn = fg - lift;
                double ff = fn * frc;
                double drag = Math.Pow(tas, 2) * p * _csa * _cd / 2 + ff; // drag and friction, since friction is also a form of "drag"
                double acc = (_thr*0.9 - drag) / _gw; // be more conservative with thrust, 90% of rated thrust...
                double brakeforce = _bf * Math.Sqrt(fn / fg) + ff; // account for weight on wheels, reduced efficiency for reduced weight...
                double totalbrake = brakeforce + (_thr * _rtr) - (_thr * 0.08); // _thr * 0.08 to estimate idle thrust
                double dec = totalbrake / _gw;

                // the part below makes sense, although we need to account for changing brakeforce due to the above...
                double ptas = tas + rc*acc; // TAS 'rc' second ahead
                double tntb = ptas / dec; // time needed to brake... 
                double bdist = (ptas * tntb) - (dec * Math.Pow(tntb, 2) / 2); // braking distance...
                double rwl2 = rwl - (ptas * rc + acc * Math.Pow(rc, 2) / 2); // look 'rc' ahead...
                //System.Diagnostics.Debug.WriteLine(ptas * 1.943844 + "  " + bdist + "  " + rwl + "  " + rwl2);
                if (bdist > rwl2) {
                    System.Diagnostics.Debug.WriteLine((lift / fg).ToString("N8") + "  " + (fn / fg).ToString("N8"));
                    System.Diagnostics.Debug.WriteLine(tas * 1.943844 + " " + ptas * 1.943844 + "  " + acc + "  " + dec); 
                    break; } // meaning we can't stop anymore...
                rwl -= (tas * t + acc * Math.Pow(t, 2) / 2);
                tas += (acc * t);
                /*Console.WriteLine(String.Format("TAS={0}, RWL={1}, EAS={2}, DRAG={3}, ACC={4}, TOTBR={5}, DEC={6}, TNTB={7}, BDIST={8}",
                    tas, rwl, eas, drag, acc, totalbrake, dec, tntb, bdist));*/
            }
            return (TAS2EAS(tas, p), tas);
        }

        public (double, double) CalcVs()
        {
            double force = _gw * g;
            double p = _qfe * mmair / (igc * _oat);
            double tas = Math.Sqrt(2*force/(p*_lsa*_cl));
            return (TAS2EAS(tas, p), tas);
        }
    }
}
