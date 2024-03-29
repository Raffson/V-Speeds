﻿namespace V_Speeds.Model.Aircrafts
{
    public class AircraftAB : Aircraft, IAfterburnable
    {
        /// <summary>
        ///     Boolean field to indicate if you wish to use afterburner parameters
        /// </summary>
        private bool _ab = false;

        /// <summary>
        ///     The thrust force of the afterburner, expected in Newtons
        /// </summary>
        private double _thrAB = 1500.0;

        /// <summary>
        ///     The reaction time considering afterburner, expected in seconds.
        /// </summary>
        private double _rcAB = 2.0;


        /// <summary>
        ///     Property for the state of the afterburner.
        /// </summary>
        public bool AB
        {
            get => _ab;
            set
            {
                _ab = value;
                Notify("Thr");
                Notify("Rc");
            }
        }

        /// <summary>
        ///     Property for the thrust force of the afterburner, expected in Newtons.<br></br>
        ///     Setter takes absolute value and notifies observers.
        /// </summary>
        public double ThrAB
        {
            get => _thrAB;
            set
            {
                _thrAB = value;
                if (AB) Notify("Thr"); // for Aircraft observers
                Notify("ThrAB");    // for AircraftAB observers
            }
        }

        /// <summary>
        ///     Property for the reaction time associated with the afterburner on, expected in seconds.<br></br>
        ///     Setter takes absolute value and notifies observers.
        /// </summary>
        public double RcAB
        {
            get => _rcAB;
            set
            {
                _rcAB = value;
                if (AB) Notify("Rc"); // for Aircraft observers
                Notify("RcAB"); // for AircraftAB observers
            }
        }


        /// <summary>
        ///     Overriding Thr property to make it behave the same as an aircraft with no afterburner.
        /// </summary>
        public override sealed double Thr
        {
            get => AB ? ThrAB : base.Thr;
            set
            {
                if( AB ) ThrAB = value;
                else base.Thr = value;
            }
        }


        /// <summary>
        ///     Overriding Rc property to make it behave the same as an aircraft with no afterburner.
        /// </summary>
        public override sealed double Rc
        {
            get => AB ? RcAB : base.Rc;
            set
            {
                if( AB ) RcAB = value;
                else base.Rc = value;
            }
        }

        /// <summary>
        ///     Returns a boolean value indicating whether or not this aircraft has an afterburner.
        ///     Aircraft derived from this class will always return true and won't be able to override.
        /// </summary>
        /// <returns><c>true</c> if the aircraft has afterburner, otherwise <c>false</c>.</returns>
        public override sealed bool HasAfterburner() => true;

    }
}
