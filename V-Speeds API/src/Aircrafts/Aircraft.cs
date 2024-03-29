﻿using V_Speeds.ConstantsAndConverters;
using V_Speeds.ObserverPattern;

namespace V_Speeds.Model.Aircrafts
{
    public class Aircraft : IMyObservable<Aircraft>
    {
        /// <summary>
        ///     List of observers to be notified if a property changes.
        /// </summary>
        private readonly List<IMyObserver<Aircraft>> _observers = new();

        // private fields...
        private double _gw = 1000.0, _lsa = 10.0, _cl = 1.0, _clg = 0.1, _thr = 1000.0, _bf = 500.0, _rc = 2.0, _cd = 0.1, _rtr = 0.0, _rfc = 0.05;

        public Aircraft() { }

        public Aircraft(Aircraft other)
        {
            _gw = other._gw;
            _lsa = other._lsa;
            _cl = other._cl;
            _clg = other._clg;
            _thr = other._thr;
            _bf = other._bf;
            _rc = other._rc;
            _cd = other._cd;
            _rtr = other._rtr;
            _rfc = other._rfc;
        }

        /// <summary>
        ///     Property for gross weight of the aircraft, expected in kgs.<br></br>
        ///     Setter takes the absolute value and notifies observers.
        /// </summary>
        public double Gw
        {
            get => _gw;
            set
            {
                _gw = Math.Abs(value);
                Notify("Gw");
            }
        }

        /// <summary>
        ///     Property for the wing area of the aircraft (Lifting Surface Area), expected in m².<br></br>
        ///     Setter takes the absolute value and notifies observers.
        /// </summary>
        public double Lsa
        {
            get => _lsa;
            set
            {
                _lsa = Math.Abs(value);
                Notify("Lsa");
            }
        }

        /// <summary>
        ///     Property for the lift coefficient of the aircraft, unitless.<br></br>
        ///     Setter notifies observers.
        /// </summary>
        public double Cl
        {
            get => _cl;
            set
            {
                _cl = value;
                Notify("Cl");
            }
        }

        /// <summary>
        ///     Property for the lift coefficient of the aircraft at the angle of incidence considering takeoff configuration, unitless.
        ///     Setter notifies observers.
        /// </summary>
        public double Clg
        {
            get => _clg;
            set
            {
                _clg = value;
                Notify("Clg");
            }
        }

        /// <summary>
        ///     Property for the rated nominal thrust of the aircraft at standard atmosphere, expected in Newton.<br></br>
        ///     Setter takes the absolute value and notifies observers.
        /// </summary>
        public virtual double Thr
        {
            get => _thr;
            set
            {
                _thr = Math.Abs(value);
                Notify("Thr");
            }
        }

        /// <summary>
        ///     Property for the brakeforce of the aircraft, expected in Newton.<br></br>
        ///     Setter takes the absolute value and notifies observers.
        /// </summary>
        public double Bf
        {
            get => _bf;
            set
            {
                _bf = Math.Abs(value);
                Notify("Bf");
            }
        }

        /// <summary>
        ///     Property for the reaction time of the aircraft such as engine spooldown, activation of reversers, etc., expected in seconds.<br></br>
        ///     Setter takes the absolute value and notifies observers.
        /// </summary>
        public virtual double Rc
        {
            get => _rc;
            set
            {
                _rc = Math.Abs(value);
                Notify("Rc");
            }
        }

        /// <summary>
        ///     Property for the drag coefficient of the aircraft, unitless.<br></br>
        ///     Setter notifies observers.
        /// </summary>
        public double Cd
        {
            get => _cd;
            set
            {
                _cd = value;
                Notify("Cd");
            }
        }

        /// <summary>
        ///     Property for the reverse thrust ratio of the aircraft, unitless.<br></br>
        ///     Setter takes the absolute value, ensures 0 <= Rtr <= 1 and notifies observers.
        /// </summary>
        public double Rtr
        {
            get => _rtr;
            set
            {
                _rtr = Math.Min(1, Math.Abs(value));
                Notify("Rtr");
            }
        }

        /// <summary>
        ///     Property for the rolling friction coefficient of the aircraft, unitless.<br></br>
        ///     Setter takes the absolute value and notifies observers.
        /// </summary>
        public double Rfc
        {
            get => _rfc;
            set
            {
                _rfc = Math.Abs(value);
                Notify("Rfc");
            }
        }

        /// <summary>
        ///     Virtual function to indicate whether or not the aircraft has an afterburner.
        /// </summary>
        /// <returns><c>true</c> if the aircraft has afterburner, otherwise <c>false</c>.</returns>
        public virtual bool HasAfterburner() => false;


        ///
        /// <summary>
        ///     Returns the lift force generated by the aircraft for a given speed in a given fluid density.<br></br>
        ///      It can also be used to calculate drag since it's the same formula except for the coefficient.<br></br>
        ///      Method is virtual so external aircraft could override.
        /// </summary>
        ///
        /// <param name="tas">True Air Speed expected in m/s (meters per second) and positive!</param>
        /// <param name="density">The density of the fluid (in kg/m³) in which the aircraft is moving, must be positve.</param>
        /// <param name="coeff">The coefficient to be used for the formula, if it's null, Cl is chosen as default.</param>
        ///
        /// <returns>
        ///    The lift force in Newtons
        /// </returns>
        public virtual double LiftForce(double tas, double density, double? coeff = null)
            => Math.Pow(tas, 2) * density * Lsa * (coeff is null ? Cl : (double)coeff) / 2;

        ///
        /// <summary>
        ///     Returns the drag force generated by the aircraft for a given speed in a given fluid density.<br></br>
        ///      It uses LiftForce with Cd, so this function is bascically for readablity.<br></br>
        ///      Method is virtual so external aircraft could override.
        /// </summary>
        ///
        /// <param name="tas">True Air Speed expected in m/s (meters per second) and positive!</param>
        /// <param name="density">The density of the fluid (in kg/m³) in which the aircraft is moving, must be positve.</param>
        ///
        /// <returns>
        ///    The drag force in Newtons
        /// </returns>
        public virtual double DragForce(double tas, double density) => LiftForce(tas, density, Cd);

        ///
        /// <summary>
        ///     Returns the friction force of the aircraft given the normal force.
        /// </summary>
        ///
        /// <param name="fn">The presumed normal force in Newton exerted on the plane.</param>
        ///
        /// <returns>
        ///    The friction force in Newtons
        /// </returns>
        public double FrictionForce(double fn) => fn * Math.Abs(Rfc);

        ///
        /// <summary>
        ///     Returns the required force to attain the given acceleration.
        /// </summary>
        ///
        /// <param name="acc">The desired acceleration, expected in m/s².</param>
        ///
        /// <returns>
        ///    The required force in Newtons
        /// </returns>
        public double RequiredForce(double acc) => Gw * acc;

        ///
        /// <summary>
        ///     Returns the estimated thrust according to the true airspeed and the density of the fluid in which the aircraft is moving.<br></br>
        ///      Method is virtual so external aircraft could override.
        /// </summary>
        ///
        /// <param name="tas">True Air Speed expected in m/s (meters per second) and positive!</param>
        /// <param name="density">The density of the fluid (in kg/m³) in which the aircraft is moving, must be positve.</param>
        ///
        /// <returns>
        ///    The estimated thrust force in Newtons
        /// </returns>
        public virtual double Thrust(double tas, double density)
        {
            // dynamic pressure also plays a role, more air for the engine means more thrust...
            // -> if we ignore this, it should mean more safety margin, lower V1 and longer runway estimate
            //      i guess i'll leave it for now...
            double densr = density / Constants.p0;
            //double thrcoeff = Math.Min(1, Math.Pow(densr, 1 + Math.Pow(densr, 5))); // Good estimate so far...
            //thrcoeff = Math.Min(1, Math.Pow(densr, Math.Pow((0.6 + densr), Math.Pow(1.4, densr))));

            // Combine functions to make a general estimate...
            //double lowcoeff = Math.Min(1, Math.Pow(densr, Math.Pow((0.5 + densr), Math.Pow(0.5, densr))));
            //double highcoeff = Math.Min(1, Math.Pow(densr, Math.Pow((0.35 + densr), Math.Pow(1.6, densr))));
            //double thrcoeff = Math.Min(lowcoeff, highcoeff);

            double lowcoeff = Math.Max(Math.Pow(Math.Log(densr + 0.98), 0.6365), 1.75 * (densr - 0.02));
            lowcoeff = Math.Min(lowcoeff, Math.Sin((0.55 * densr - 0.1) * Math.PI / 1.7) + 0.3);
            double highcoeff = Math.Pow(densr, Math.Pow(0.09 + densr, Math.Pow(3.25, densr)));
            double thrcoeff = Math.Min(lowcoeff, highcoeff);

            //double thrcoeff = Math.Min(1, Math.Pow(densr, Math.Pow(0.09 + densr, Math.Pow(3.25, densr))));

            // Would need some input parameter to somehow make a better thrust estimation for a specific aircraft...
            // if nothing is provided, fall back on some default model...
            // -> provide input as points for interpolation?

            return Thr * Math.Min(1, thrcoeff);
        }

        ///
        /// <summary>
        ///     Returns the projected acceleration of the aircraft, minding drag and friction force in the equation.<br></br>
        ///      Method is virtual so external aircraft could override.
        /// </summary>
        ///
        /// <param name="tas">True Air Speed expected in m/s (meters per second) and positve!</param>
        /// <param name="density">The density of the fluid (in kg/m³) in which the aircraft is moving, must be postive!</param>
        ///
        /// <returns>
        ///     The the projected acceleration in m/s²
        /// </returns>
        public virtual double ProjectedAcceleration(double tas, double density)
        {
            // TODO: A better model for thrust, perhaps using the general thrust equation...
            double thrust = Thrust(tas, density);
            double fn = Math.Max(0, RequiredForce(Constants.g) - LiftForce(tas, density, Clg));
            double drag = DragForce(tas, density) + FrictionForce(fn);
            double acc = Math.Max(0, thrust - drag) / Gw;
            return acc;
        }

        ///
        /// <summary>
        ///     Returns the projected deceleration of the aircraft, 
        ///     keeping in mind that less weight on the wheels results in less effective braking.<br></br>
        ///      Method is virtual so external aircraft could override.
        /// </summary>
        ///
        /// <param name="tas">True Air Speed expected in m/s (meters per second) and positve!</param>
        /// <param name="density">The density of the fluid (in kg/m³) in which the aircraft is moving, must be postive!</param>
        ///
        /// <returns>
        ///     The the projected deceleration in m/s²
        /// </returns>
        public virtual double ProjectedDeceleration(double tas, double density)
        {
            double fg = RequiredForce(Constants.g);
            double fn = Math.Max(0, fg - LiftForce(tas, density, Clg));
            double ff = FrictionForce(fn); // friction while rolling down the runway
            double brakecoeff = Math.Pow(Math.Sin(Math.PI * fn / fg / 2), 1 / 2.0); // how much weight is still on the wheels
            //System.Diagnostics.Debug.WriteLine(Converter.mps2kts(tas) + "  " + brakecoeff);
            double brakeforce = _bf * brakecoeff + ff; // account for weight on wheels, reduced efficiency for reduced weight
            double totalbrake = brakeforce + Thrust(tas, density) * (Rtr - 0.08); // _thr * 0.08 for idle thrust <- Add idle thrust parameter???
            double dec = Math.Max(0, totalbrake) / Gw; // we're basically aiming for the average deceleration
            return dec;
        }

        // Observer Pattern Stuff
        public void Subscribe(IMyObserver<Aircraft> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
                observer.Update(this);
            }
        }

        public void Unsubscribe(IMyObserver<Aircraft> observer)
        {
            if (_observers.Contains(observer)) _observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in _observers) observer.Update(this);
        }

        public void Notify(string property)
        {
            foreach (var observer in _observers) observer.Update(property);
        }
    }
}
