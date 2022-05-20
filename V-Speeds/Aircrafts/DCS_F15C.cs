﻿namespace V_Speeds.Aircrafts
{
    internal class DCS_F15C : AircraftAB
    {
        public DCS_F15C(double gw = 12701.0)
        {
            Gw = gw;
            Lsa = 56.5;
            Cl = 0.71;
            Bf = 61700;
            Rc = 2.5;
            RcAB = 2;
            Cd = 0.085;
            Rtr = 0;
            Thr = 128000;
            ThrAB = 200000;
            Clg = 0.071;
            Rfc = 0.08;
        }
    }
}
