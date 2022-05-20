﻿namespace V_Speeds.Aircrafts
{
    internal class DCS_F14B : AircraftAB
    {
        public DCS_F14B(double gw = 19838.0)
        {
            Gw = gw;
            Lsa = 94;
            Cl = 0.89;
            Bf = 79000;
            Rc = 3;
            RcAB = 2;
            Cd = 0.058;
            Rtr = 0;
            Thr = 135000;
            ThrAB = 213000;
            Clg = 0.0;
            Rfc = 0.0;
        }
    }
}
