using V_Speeds.Model;
using V_Speeds.Model.Aircrafts;
using V_Speeds.ObserverPattern;

namespace V_Speeds
{
    public class V_Calculator : IMyObservable<V_Calculator>
    {
        private readonly List<IMyObserver<V_Calculator>> _observers = new();

        private Airfield _field = new();
        private Aircraft _acft = new();


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

        public void Subscribe(IMyObserver<V_Calculator> observer)
        {
            // Check whether observer is already registered. If not, add it
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
                observer.Update(this);
            }
        }

        public void Unsubscribe(IMyObserver<V_Calculator> observer)
        {
            if(_observers.Contains(observer)) _observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in _observers) observer.Update(this);
        }

        public override string ToString() // for debugging purposes...
        {
            return $"V-Calculator config:\n" +
                $"GW  = {Gw}\n" +
                $"OAT = {Oat}\n" +
                $"QFE = {Qfe}\n" +
                $"LSA = {Lsa}\n" +
                $"CL  = {Cl}\n" +
                $"CLG = {Clg}\n" +
                $"THR = {Thr}\n" +
                $"BF  = {Bf}\n" +
                $"RL  = {Rl}\n" +
                $"RC  = {Rc}\n" +
                $"CD  = {Cd}\n" +
                $"RTR = {Rtr}\n" +
                $"RFC = {Rfc}\n";
        }

        public Airfield Field { get => _field; set => _field = value; }
        public Aircraft Craft { get => _acft; set => _acft = value; }

        public object? this[string propertyName]
        {
            get
            {
                if (this.GetType().GetProperty(propertyName) is System.Reflection.PropertyInfo pi)
                    return pi.GetValue(this, null);
                return null;
            }

            set
            {
                if (this.GetType().GetProperty(propertyName) is System.Reflection.PropertyInfo pi)
                    pi.SetValue(this, value, null);
            }
        }

        public double Gw { get => Craft.Gw; set => Craft.Gw = value; }
        public double Oat { get => Field.Oat; set => Field.Oat = Math.Abs(value); }
        public double Qfe { get => Field.Qfe; set => Field.Qfe = Math.Abs(value); }
        public double Lsa { get => Craft.Lsa; set => Craft.Lsa = value; }
        public double Cl { get => Craft.Cl; set => Craft.Cl = value; }
        public double Clg { get => Craft.Clg; set => Craft.Clg = value; }
        public double Thr { get => Craft.Thr; set => Craft.Thr = value; }
        public double Bf { get => Craft.Bf; set => Craft.Bf = value; }
        public double Rl { get => Field.Rl; set => Field.Rl = Math.Abs(value); }
        public double Rc { get => Craft.Rc; set => Craft.Rc = value; }
        public double Cd { get => Craft.Cd; set => Craft.Cd = value; }
        public double Rtr { get => Craft.Rtr; set => Craft.Rtr = value; }
        public double Rfc { get => Craft.Rfc; set => Craft.Rfc = value; }


        // Expecting v0 in m/s, acc in m/s² and time in seconds
        //  return distance travelled in meters
        public static double CalcDistance(double v0, double a, double time) => (v0 * time) + (a * Math.Pow(time, 2) / 2); // need a better spot for this function...


        /// <summary>
        ///     Calculates V1, i.e. the point of no return...
        /// </summary>
        /// <returns>A tuple containing V1 in EAS and TAS respectively.</returns>
        public (double eas, double tas) CalcV1()
        {
            //ignore change in mass => extra safety margin
            //ignore wind => extra safety margin (headwind would increase IAS and decrease GS, thus less distance travelled on runway)
            //ignore speedbrakes and drag in general during brake => more safety margin because longer braking distance
            //using EAS to approximate IAS
            double p = Field.LocalDensity();
            System.Diagnostics.Debug.WriteLine($"Density ratio = {p / Constants.p0}");
            double t = 0.1;   // time interval 0.1 seconds
            double tas = 0.0; // assuming no headwind (extra safety) => tas = gs
            double rwl = Field.Rl; // how much runway do we have left
            double avgacc = Craft.ProjectedAcceleration(tas, p);
            double avgdec = Craft.ProjectedDeceleration(tas, p);
            double rc = Field.Rl < 1500 ? Rc + (1500 - Field.Rl) * 0.003 : Rc; // shorter runway tend to get overshot...
            while (true)
            {
                double acc = Craft.ProjectedAcceleration(tas, p);
                double dec = Craft.ProjectedDeceleration(tas + acc, p); // 1 second ahead
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
                double bdist = Field.Rl - (Field.Rl / (avgacc / avgdec + 1)); // = RLb

                if (bdist > rwl2) break; // meaning we can't stop anymore
                rwl -= CalcDistance(tas, acc, t);
                tas += (acc * t);
            }
            return (Converter.TAS2EAS(tas, p), tas);
        }


        /// <summary>
        ///     Calculates Vs, i.e. the minimum airspeed required to maintain level flight for the given aircraft.
        ///     <br></br>TODO: introduce parameter for degrees of pitch...
        /// </summary>
        /// <param name="idle">Indicates whether we should account for full thrust.</param>
        /// <returns>A tuple containing Vs in EAS and TAS respectively.</returns>
        public (double eas, double tas) CalcVs(bool idle = true)
        {
            double p = Field.LocalDensity();
            // if idle is false and TWR too high, force will become negative -> make sure ze return 0 instead of NaN
            double force = Math.Max(0, Craft.RequiredForce(Constants.g) - (idle ? 0 : (Math.Sin(10 * Math.PI / 180) * Craft.Thrust(0, p))));
            double tas = Math.Sqrt(2 * force / (p * Lsa * Cl));
            return (Converter.TAS2EAS(tas, p), tas);
        }

        /// <summary>
        ///     Calculates the estimated runway needed to reach Vs for the given aircraft.
        /// </summary>
        /// <param name="idle">Indicates wheter we should account for full thrust.</param>
        /// <returns>The (estimated) required runway in meters.</returns>
        public double CalcNeededRunway(bool idle = true) // fullthrust false for tests, because the data was gathered that way
        {
            double vs = CalcVs(idle).tas; // TAS!!!
            double dist = 0;
            double p = Field.LocalDensity();
            double t = 0.1;   // time interval 0.1 seconds
            double tas = 0.0; // assuming no headwind (extra safety) => tas = gs
            while (tas <= vs)
            {
                double acc = Craft.ProjectedAcceleration(tas, p);
                if (acc < 0.001) return double.NaN; // means we can't reach Vs, could cause an infinite loop
                //System.Diagnostics.Debug.WriteLine(acc);
                dist += CalcDistance(tas, acc, t);
                tas += (acc * t);
            }
            // at this point we have an excess in distance, the larger the acceleration, the bigger the error...
            double diff = tas - vs;
            double accVs = Craft.ProjectedAcceleration(vs, p);
            double time = diff / accVs;
            dist -= CalcDistance(vs, accVs, time);
            return dist;
        }

        /// <summary>
        ///     Calculates the Maximum Take-Off Weight (MTOW) for the given aircraft and airfield.
        /// </summary>
        /// <returns>MTOW in kgs.</returns>
        public double CalcMTOW() // must gather testdata...
        {
            int mtow = 0;
            double gw_backup = Gw;
            int last;
            int incrementer = 32;  // start with increment of 32kg = 5 less calls to CalcNeededRunway, what aircraft that weighs less than 32kgs anyway...
            double tolerance = Field.Rl * 0.01; // Aim for a gross weight that needs 1% less Dv than actual runway length
            int count = 0;
            while (true)
            {
                count++;
                last = mtow;
                mtow += incrementer;
                Gw = (double)mtow;
                double dist = CalcNeededRunway(false);  // assuming full thrust
                if (double.IsNaN(dist) || dist > Field.Rl)
                {
                    if (incrementer == 1) return last > 0 ? last : double.NaN; // means we're stuck in a loop, 'last' should be our best estimate...
                    mtow = last;
                    incrementer = 1;
                    continue;
                }
                else if (Field.Rl - dist < tolerance) break;
                else if (dist < Field.Rl && double.IsNaN(dist))
                {
                    mtow = -1;
                    break;
                }
                incrementer <<= 1;
            }
            System.Diagnostics.Debug.WriteLine($"{count} times called CalcNeededRunway...");
            Gw = gw_backup;
            return mtow > 0 ? mtow : double.NaN;

            // MTOW testdata F16:
            //  10C; 25.15inHg; 4408ft;  CD = 0.095  => 30000lbs
            //  9C;  24.49inHg; 12001ft; CD = 0.144  => 42250lbs
            //  16C; 28.00inHg; 4937ft;  CD = 0.144  => 33000lbs
            //  16C; 28.00inHg; 4937ft;  CD = 0.126; CL = 1.18  => 37500lbs (14° AoA...)
            //
            // MTOW testdata F18:
            //  10C; 25.15inHg; 4408ft;  CD = 0.150  => 46500lbs
            //  16C; 28.00inHg; 4937ft;  CD = 0.165  => 52200lbs (tested with 49655lbs at mesquite towards the hill, cleared the hill...)
            //
            // MTOW testdata A10:
            //  10C; 25.15inHg; 4408ft;  CD = 0.080  => 38500lbs
            //  10C; 25.15inHg; 4408ft;  CD = 0.080 CL = 1.21  => 41000lbs
            //  9C;  24.49inHg; 12001ft; CD = 0.116  => 56000lbs (serious overweight though...)
            //  16C; 28.00inHg; 4937ft;  CD = 0.116  => 43100lbs (can't clear hill at mesquite so beware...)
            //  16C; 28.00inHg; 4937ft;  CD = 0.116; CL = 1.21  => 46750lbs (very close at mesquite with the hil...)
        }
    }
}
