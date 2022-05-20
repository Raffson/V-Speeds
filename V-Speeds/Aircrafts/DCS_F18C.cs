namespace V_Speeds.Aircrafts
{
    internal class DCS_F18C : AircraftAB
    {
        public DCS_F18C(double gw = 10433.0)
        {
            Gw = gw;
            Lsa = 38;
            Cl = 1.05;
            Bf = 50000;
            Rc = 3;
            RcAB = 4;
            Cd = 0.12;
            Rtr = 0;
            Thr = 105000;
            ThrAB = 146000;
            Clg = 0.55;
            Rfc = 0.033;
        }
    }
}
