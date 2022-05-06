namespace V_Speeds
{
    class V_Calculator
    {
        [System.Diagnostics.Conditional("CONTRACTS_FULL")]
        public static void Require(bool condition) => System.Diagnostics.Contracts.Contract.Requires(condition);

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

        // Expecting thrust in Newton, density in density in kg/m³
        //  return estimated thrust depending on density
        public static double CalcThrust(double thrust, double density)
        {
            // Having troubles with airports at different altitudes to determine the needed runway to reach Vs
            // sealevel is ok
            // nevada at 5500ft is being overestimated
            // soganlug at 1500ft is being underestimated
            //      -> this has more or less been resolved for the F16,
            //         F18 is being underestimated in terms of thrust, less runway needed than calculated...
            // dynamic pressure also plays a role, more air for the engine means more thrust...
            // -> if we ignore this, it should mean more safety margin, lower V1 and longer runway estimate
            //      i guess i'll leave it for now...
            double densr = density / p0;
            //double thrcoeff = Math.Min(1, Math.Pow(densr, 1 + Math.Pow(densr, 5))); // Good estimate so far...
            //thrcoeff = Math.Min(1, Math.Pow(densr, Math.Pow((0.6 + densr), Math.Pow(1.4, densr))));

            // An even better estimate, however when density becomes lower this will fail miserably,
            // looking at this function it shows that it will massively overestimate thrust when densr < 0.8
            // for now we'll start using a different function when densr < 0.7609,
            // i.e. the point where lowcoeff and highcoeff intersect...
            double lowcoeff = Math.Min(1, Math.Pow(densr, Math.Pow((0.5 + densr), Math.Pow(0.5, densr))));
            double highcoeff = Math.Min(1, Math.Pow(densr, Math.Pow((0.3 + densr), Math.Pow(3, densr))));
            double thrcoeff = Math.Min(lowcoeff, highcoeff);
            return thrust * Math.Min(1, (thrcoeff));
        }

        // Expecting tas in m/s and density in kg/m³, tas and density MUST BE POSITIVE!
        //  return projected acceleration in m/s²
        private double ProjectedAcceleration(double tas, double density)
        {
            // TODO: A better model for thrust, perhaps using the general thrust equation...
            double thrust = CalcThrust(_thr, density);
            double fn = Math.Max(0, CalcForce(_gw, g) - CalcLiftForce(tas, density, _lsa, _clg));
            double drag = CalcDragForce(tas, density, _lsa, _cd) + CalcFrictionForce(fn, _rfc);
            double acc = (thrust - drag) / _gw;
            return acc;
        }

        // Expecting tas in m/s and density in kg/m³, tas and density MUST BE POSITIVE!
        //  return projected deceleration in m/s²
        private double ProjectedDeceleration(double tas, double density)
        {
            double fg = CalcForce(_gw, g);
            double fn = Math.Max(0, fg - CalcLiftForce(tas, density, _lsa, _clg));
            double ff = CalcFrictionForce(fn, _rfc); // friction while rolling down the runway
            double brakecoeff = Math.Pow(Math.Sin(Math.PI * fn / fg / 2), 1 / 2.0); // how much weight is still on the wheels
            //System.Diagnostics.Debug.WriteLine(Converter.mps2kts(tas) + "  " + brakecoeff);
            double brakeforce = _bf * brakecoeff + ff; // account for weight on wheels, reduced efficiency for reduced weight
            double totalbrake = brakeforce + CalcThrust(_thr, density) * (_rtr - 0.08); // _thr * 0.08 for idle thrust <- Add idle thrust parameter???
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
            System.Diagnostics.Debug.WriteLine(p / p0);
            double t = 0.1;   // time interval 0.1 seconds
            double tas = 0.0; // assuming no headwind (extra safety) => tas = gs
            double rwl = _rl; // how much runway do we have left
            double avgacc = ProjectedAcceleration(tas, p);
            double avgdec = ProjectedDeceleration(tas, p);
            double rc = _rl < 1500 ? _rc + (1500 - _rl) * 0.003 : _rc; // shorter runway tend to get overshot...
            while( true )
            {
                double acc = ProjectedAcceleration(tas, p);
                double dec = ProjectedDeceleration(tas+acc, p); // 1 second ahead
                avgacc = (avgacc + acc) / 2;
                avgdec = (avgdec + dec) / 2;
                //System.Diagnostics.Debug.WriteLine(Converter.mps2kts(tas) + "  " + avgacc*_gw + "  " + avgdec * _gw);

                // start thinking in terms of energy... Fa * RLa = Fb * RLb and RL = RLa + RLb + RLrc
                //  where Fa is the net thrust, RLa the distance covered during acceleration,
                //      Fb is the net brakeforce, RLb the distance covered during braking
                //      RL is the total runway length and RLrc is the distance covered during reaction
                //  we can simplify by using 'acc' and 'dec' cause the mass stays the same,
                //  thus the ratio of forces is equal to ratio of 'acc' and 'dec'
                double rwl2 = rwl - CalcDistance(tas, acc, rc); // look 'rc' ahead
                double bdist = _rl - (_rl / (avgacc / avgdec + 1)); // = RLb

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
            while (tas <= vs)
            {
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
