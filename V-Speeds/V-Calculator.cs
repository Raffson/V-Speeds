namespace V_Speeds
{
    class V_Calculator
    {
        public const double igc = 8.3144598; // ideal gas constant
        public const double mmair = 28.97 / 1000; // molecular mass of air
        public const double g = 9.83; // 1G at poles (m/s2), just for some extra wiggle room, considering no elevation (except pressure)...
        public const double p0 = 101325 * mmair / (igc * 288.15); // standard sea density


        private double _gw, _oat, _qfe, _lsa, _cl, _clg, _thr, _bf, _rl, _rc, _cd, _rtr, _rfc;


        public V_Calculator(double gw = 1000.0, double oat = 288.15, double qfe = 101325, double lsa = 10.0, double cl = 1.0, double clg = 0.5,
            double thr = 1000.0, double bf = 500.0, double rl = 2500.0, double rc = 2.0, double cd = 0.1, double rtr = 0.0, double rfc = 0.05)
        {
            // all members should stay positve, except Cl and Cd... thus setters will use absolute value
            Gw = gw;    // gross weight:         kgs
            Oat = oat;  // outside air temp:     Kelvin
            Qfe = qfe;  // local pressure:       Pascal
            Lsa = lsa;  // lifting surface area: m²
            Cl = cl;    // lift coefficient:     no unit
            Clg = clg;  // CL at mounting angle: no unit
            Thr = thr;  // thrust:               Newton
            Bf = bf;    // brake force:          Newton
            Rl = rl;    // runway length:        m
            Rc = rc;    // reaction time:        sec (accounting for engine spooldown, deployment of reversers, etc.)
            Cd = cd;    // drag coefficient:     no unit
            Rtr = rtr;  // reverse thrust ratio: no unit
            Rfc = rfc;  // Rolling friction co.: no unit
        }
        
        public double Gw { get => _gw; set => _gw = Math.Abs(value); }
        public double Oat { get => _oat; set => _oat = Math.Abs(value); }
        public double Qfe { get => _qfe; set => _qfe = Math.Abs(value); }
        public double Lsa { get => _lsa; set => _lsa = Math.Abs(value); }
        public double Cl { get => _cl; set => _cl = value; }
        public double Clg { get => _clg; set => _clg = value; }
        public double Thr { get => _thr; set => _thr = Math.Abs(value); }
        public double Bf { get => _bf; set => _bf = Math.Abs(value); }
        public double Rl { get => _rl; set => _rl = Math.Abs(value); }
        public double Rc { get => _rc; set => _rc = Math.Abs(value); }
        public double Cd { get => _cd; set => _cd = value; }
        public double Rtr { get => _rtr; set => _rtr = Math.Abs(value); }
        public double Rfc { get => _rfc; set => _rfc = Math.Abs(value); }

        // internal setter-functions for Form1
        internal void SetGw(double value) => Gw = value;
        internal void SetOat(double value) => Oat = value;
        internal void SetQfe(double value) => Qfe = value;
        internal void SetLsa(double value) => Lsa = value;
        internal void SetCl(double value) => Cl = value;
        internal void SetClg(double value) => Clg = value;
        internal void SetThr(double value) => Thr = value;
        internal void SetBf(double value) => Bf = value;
        internal void SetRl(double value) => Rl = value;
        internal void SetRc(double value) => Rc = value;
        internal void SetCd(double value) => Cd = value;
        internal void SetRtr(double value) => Rtr = value;
        internal void SetRfc(double value) => Rfc = value;

        private double TAS2EAS(double tas, double rho) => tas * Math.Sqrt(rho / p0);

        public (double, double) CalcV1()
        {
            //ignore change in mass => extra safety margin
            //ignore wind => extra safety margin (headwind would increase IAS and decrease GS, thus less distance travelled on runway...)
            //ignore speedbrakes and drag in general during brake => more safety margin because longer braking distance...
            //using EAS to approximate IAS...
            double p = _qfe * mmair / (igc * _oat);
            double t = 0.1;   // time interval 0.1 seconds
            double tas = 0.0; // assuming no headwind (extra safety) => tas = gs
            double rwl = _rl; // how much runway do we have left...
            while( true )
            {
                // Account for reduced normal force when airspeed increases => reduced braking efficiency
                // Account for friction when rolling down the runway
                // TODO 1: account for variations in thrust depending on atmosphere... currently no clue where to start -_-
                // TODO 2: after some extra testing, seems like shorter runways are a problem for a lightweight F18 in afterburner
                //          when aborting, thrust remains present for a longer time compared to other aircraft,
                //          the effect on a heavier F18 is obviously less...
                double lift = Math.Pow(tas, 2) * p * _lsa * _clg / 2;
                double fg = _gw * g;
                double fn = fg - lift;
                double ff = fn * _rfc;
                double drag = Math.Pow(tas, 2) * p * _lsa * _cd / 2 + ff; // drag and friction, since friction is also a form of "drag"
                //System.Diagnostics.Debug.WriteLine(Math.Pow(tas, 2) * p * _csa * _cd / 2 + "  " + drag + "  " + ff);
                double acc = (_thr*0.9 - drag) / _gw; // be more conservative with thrust, 90% of rated thrust <- this has to change!...
                double brakeforce = _bf * Math.Sqrt(fn / fg) + ff; // account for weight on wheels, reduced efficiency for reduced weight...
                double totalbrake = brakeforce + _thr * (_rtr - 0.08); // _thr * 0.08 to estimate idle thrust <- Add idle thrust parameter???
                double dec = totalbrake / _gw; // we're basically aiming for the average deceleration...

                // the part below makes sense, but still only using an average estimate for deceleration...
                double ptas = tas + _rc*acc; // TAS 'rc' second ahead
                double tntb = ptas / dec; // time needed to brake... 
                double bdist = (ptas * tntb) - (dec * Math.Pow(tntb, 2) / 2); // braking distance...
                double rwl2 = rwl - (ptas * _rc + acc * Math.Pow(_rc, 2) / 2); // look 'rc' ahead...
                if (bdist > rwl2) break; // meaning we can't stop anymore...
                rwl -= (tas * t + acc * Math.Pow(t, 2) / 2);
                tas += (acc * t);
                /*System.Diagnostics.Debug.WriteLine(String.Format("TAS={0}, RWL={1}, EAS={2}, DRAG={3}, ACC={4}, TOTBR={5}, DEC={6}, TNTB={7}, BDIST={8}",
                    tas, rwl, eas, drag, acc, totalbrake, dec, tntb, bdist));*/
            }
            return (TAS2EAS(tas, p), tas);
        }

        // Minimum airspeed required to maintain level flight for a certain configuration
        public (double, double) CalcVs()
        {
            double force = _gw * g;
            double p = _qfe * mmair / (igc * _oat);
            double tas = Math.Sqrt(2*force/(p*_lsa*_cl));
            return (TAS2EAS(tas, p), tas);
        }

        // Required runway to reach the minimum airspeed for level flight for a certain configuration
        // After some preliminary tests it seems this is a slight overestimation, which isn't a bad thing necessarily...
        //  tests done with F18 in DCS...
        public double CalcNeededRunway()
        {
            (_, double vs) = CalcVs();
            double dist = 0;
            // Need an estimation of acceleration like V1...
            //  what follows is very similar to the code in CalcV1
            //  the major difference is CalcV1 uses fg & fn which we don't need here...
            double p = _qfe * mmair / (igc * _oat);
            double t = 0.1;   // time interval 0.1 seconds
            double tas = 0.0; // assuming no headwind (extra safety) => tas = gs
            while (true)
            {
                // Account for reduced normal force when airspeed increases => reduced braking efficiency
                // Account for friction when rolling down the runway
                // TODO: account for variations in thrust depending on atmosphere... currently no clue where to start -_-
                double lift = Math.Pow(tas, 2) * p * _lsa * _clg / 2;
                double fg = _gw * g;
                double fn = fg - lift;
                double ff = fn * _rfc;
                double drag = Math.Pow(tas, 2) * p * _lsa * _cd / 2 + ff; // drag and friction, since friction is also a form of "drag"
                double acc = (_thr * 0.9 - drag) / _gw; // be more conservative with thrust, 90% of rated thrust <- this has to change!...
                if (tas >= vs) break;
                dist += (tas * t + acc * Math.Pow(t, 2) / 2);
                tas += (acc * t);
            }
            return dist;
        }
    }
}
