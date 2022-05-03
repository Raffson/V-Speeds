namespace V_Speeds
{
    class V_Calculator
    {
        public const double igc = 8.3144598; // ideal gas constant
        public const double mmair = 28.97 / 1000; // molecular mass of air
        public const double g = 9.83; // 1G at poles (m/s2), just for some extra wiggle room, considering no elevation (except pressure)
        public const double p0 = 101325 * mmair / (igc * 288.15); // standard air density at sea-level


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

        // Expecting tas in m/s and density in kg/m³
        //  return EAS in m/s
        public static double TAS2EAS(double tas, double density) => tas * Math.Sqrt(density / p0);

        // Expecting press in Pascal and temp in Kelvin
        //  return density in kg/m³
        public static double CalcDensity(double press, double temp) => press * mmair / (igc * temp);

        // Expecting tas in m/s,density in kg/m³, lsa in m²
        //  return lift force in Newton
        // This function can also be used to calculate drag, the only difference is the coefficient
        public static double CalcLiftForce(double tas, double density, double lsa, double coeff) => Math.Pow(tas, 2) * density * lsa * coeff / 2;

        // Same as GetLiftForce but just for the sake of readabily
        //  return drag force in Newton
        public static double CalcDragForce(double tas, double density, double lsa, double coeff) => CalcLiftForce(tas, density, lsa, coeff);

        // Expecting fn in Newtons
        //  return friction force in Newton
        public static double CalcFrictionForce(double fn, double coeff) => fn * Math.Abs(coeff);

        // Expecting weight in kgs, acc in m/s²
        //  return force in Newton
        public static double CalcForce(double weight, double acc) => weight * acc;

        // Expecting v0 in m/s, acc in m/s² and time in seconds
        //  return distance travelled in meters
        public static double CalcDistance(double v0, double a, double time) => (v0 * time) + (a * Math.Pow(time, 2) / 2);

        // Expecting tas in m/s and density in kg/m³, tas and density MUST BE POSITIVE!
        //  return projected acceleration in m/s²
        private double ProjectedAcceleration(double tas, double density)
        {
            // TODO: A better model for thrust, perhaps using the general thrust equation...
            double densfactor = Math.Min(1, Math.Pow(density / p0, 2.0 / 3.0)); // efficiency factor wrt air density
            double thrust = _thr * densfactor;
            double fn = Math.Max(0, CalcForce(_gw, g) - CalcLiftForce(tas, density, _lsa, _clg));
            double drag = CalcDragForce(tas, density, _lsa, _cd) + CalcFrictionForce(fn, _rfc);
            double acc = (thrust - drag) / _gw;
            //System.Diagnostics.Debug.WriteLine(Converter.mps2kts(tas) + "  " + acc);
            return acc;
        }

        // Expecting tas in m/s and density in kg/m³, tas and density MUST BE POSITIVE!
        //  return projected deceleration in m/s²
        private double ProjectedDeceleration(double tas, double density)
        {
            // TODO: account for variations in thrust depending on atmosphere, specifically for "idle thrust"
            double fg = CalcForce(_gw, g);
            double fn = Math.Max(0, fg - CalcLiftForce(tas, density, _lsa, _clg));
            double ff = CalcFrictionForce(fn, _rfc); // friction while rolling down the runway
            double brakecoeff = Math.Sqrt(fn / fg); // how much weight is still on the wheels
            double brakeforce = _bf * brakecoeff + ff; // account for weight on wheels, reduced efficiency for reduced weight
            double totalbrake = brakeforce + _thr * (_rtr - 0.08); // _thr * 0.08 to estimate idle thrust <- Add idle thrust parameter???
            double dec = totalbrake / _gw; // we're basically aiming for the average deceleration
            return dec;
        }

        // Calculates V1, returns a tuple containing (EAS, TAS)
        public (double, double) CalcV1()
        {
            //ignore change in mass => extra safety margin
            //ignore wind => extra safety margin (headwind would increase IAS and decrease GS, thus less distance travelled on runway)
            //ignore speedbrakes and drag in general during brake => more safety margin because longer braking distance
            //using EAS to approximate IAS
            double p = CalcDensity(_qfe, _oat);
            double t = 0.1;   // time interval 0.1 seconds
            double tas = 0.0; // assuming no headwind (extra safety) => tas = gs
            double rwl = _rl; // how much runway do we have left
            while( true )
            {
                // TODO 1: account for variations in thrust depending on atmosphere... currently no clue where to start -_-
                // TODO 2: be more precise in brake phase, currently we're aiming for an average
                //          we should "integrate" like we do with the acceleration, however that's a whole lot extra CPU time...

                double acc = ProjectedAcceleration(tas, p);
                double dec = ProjectedDeceleration(tas, p);
                //System.Diagnostics.Debug.WriteLine(acc + "  " + dec);

                // the part below makes sense, but still only using an average estimate for deceleration...
                // also, we're aiming 'rc' seconds ahead but by then 'dec' will be even lower
                //  -> since braking efficiency increases as speed decreases, the current (over)estimate may be accurate after all, average-wise that is...
                double ptas = tas + _rc*acc; // TAS 'rc' second ahead
                double tntb = ptas / dec; // time needed to brake from predicted speed
                double bdist = CalcDistance(ptas, -dec, tntb); // braking distance
                double rwl2 = rwl - CalcDistance(ptas, acc, _rc); // look 'rc' ahead
                if (bdist > rwl2) break; // meaning we can't stop anymore
                rwl -= CalcDistance(tas, acc, t);
                tas += (acc * t);
            }
            return (TAS2EAS(tas, p), tas);
        }

        // Minimum airspeed required to maintain level flight for a certain configuration
        //  returns a tuple containing (EAS, TAS)
        public (double, double) CalcVs()
        {
            double force = CalcForce(_gw,  g);
            double p = CalcDensity(_qfe, _oat);
            double tas = Math.Sqrt(2 * force / (p * _lsa * _cl));
            return (TAS2EAS(tas, p), tas);
        }

        // Required runway to reach the minimum airspeed for level flight for a certain configuration
        //  returns the runway needed in meters to reach Vs
        public double CalcNeededRunway()
        {
            (_, double vs) = CalcVs();
            double dist = 0;
            double p = CalcDensity(_qfe, _oat);
            double t = 0.1;   // time interval 0.1 seconds
            double tas = 0.0; // assuming no headwind (extra safety) => tas = gs
            while (true)
            {
                if (tas >= vs) break;
                double acc = ProjectedAcceleration(tas, p);
                if (acc < 0.001) return double.NaN; // means we can't reach Vs, could cause an infinite loop
                //System.Diagnostics.Debug.WriteLine(acc);
                dist += CalcDistance(tas, acc, t);
                tas += (acc * t);
            }
            return dist;
        }
    }
}
