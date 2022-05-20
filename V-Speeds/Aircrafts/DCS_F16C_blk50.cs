namespace V_Speeds.Aircrafts
{
    internal class DCS_F16C_blk50 : AircraftAB
    {
        public DCS_F16C_blk50(double gw = 8573.0)
        {
            Gw = Math.Abs(gw);
            Lsa = 28;
            Cl = 0.9;
            Bf = 51000;
            Rc = 4;
            RcAB = 2.5;
            Cd = 0.095;
            Rtr = 0;
            Thr = 67000;
            ThrAB = 114000;
            Clg = 0.58;
            Rfc = 0.047;
        }
    }
}
