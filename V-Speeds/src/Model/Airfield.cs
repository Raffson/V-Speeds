﻿using V_Speeds.ObserverPattern;

namespace V_Speeds.Model
{
    /// <summary>
    ///     A class respresenting (as the name suggests) an airfield.
    /// </summary>
    public class Airfield : IMyObservable<Airfield>, IMyObserver<Atmosphere>
    {
        /// <summary>
        ///     List of observers to be notified if a property changes.
        /// </summary>
        private readonly List<IMyObserver<Airfield>> _observers = new();

        /// <summary>
        ///     Runway length, expected in meters.
        /// </summary>
        private double _rl = 2500.0; // airfield can have more runways though -_-

        /// <summary>
        ///     Atmospheric condition at airfield
        /// </summary>
        private readonly Atmosphere _atmos = new();


        /// <summary>
        ///     Constructs an airfield object given an atmosphere and a runway length
        /// </summary>
        /// <param name="atmos">The atmospheric condition at the airfield.</param>
        /// <param name="rl">The length of the runway at the field</param>
        public Airfield(double rl = 2500.0, Atmosphere? atmos = null)
        {
            _atmos = atmos is null ? _atmos : atmos;
            Rl = rl;
            _atmos.Subscribe(this);
        }

        /// <summary>
        ///     Property for accessing the runway length, expected in meters.<br></br>
        ///     Setter takes the absolute value.
        /// </summary>
        public double Rl
        {
            get => _rl;
            set
            {
                _rl = Math.Abs(value);
                Notify("Rl");
            }
        }

        /// <summary>
        ///     Property for accessing the airfield's outside air temperature (OAT), expected in Kelvin.
        /// </summary>
        public double Oat { get => Atmosphere.Temp; set => Atmosphere.Temp = value; }

        /// <summary>
        ///     Property for accessing the airfield's local pressure (QFE), expected in Pascal.
        /// </summary>
        public double Qfe { get => Atmosphere.Press; set => Atmosphere.Press = value; }

        /// <summary>
        ///     Get-accessor for the airfield's atmosphere, useful in case you wish to change the molar mass of the atmosphere.
        ///     However this shouldn't be something you want to do 
        ///     unless you suddenly find yourself on a different planet with a completely different atmosphere. 
        /// </summary>
        public Atmosphere Atmosphere => _atmos;

        /// <summary>
        ///     Get the atmosphere's density at the airfield.
        /// </summary>
        /// <returns>The atmosphere's density in kg/m³</returns>
        public double LocalDensity() => Atmosphere.Density();


        // Observer Pattern Stuff
        public void Subscribe(IMyObserver<Airfield> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
                observer.Update(this);
            }
        }

        public void Unsubscribe(IMyObserver<Airfield> observer)
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

        public void Update(Atmosphere value)
        {
            Notify("Oat");
            Notify("Qfe");
        }

        public void Update(string property)
        {
            if (property == "Temp") property = "Oat";
            else if (property == "Press") property = "Qfe";
            Notify(property);
        }
    }
}
