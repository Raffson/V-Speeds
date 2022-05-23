namespace V_Speeds.Model.Aircrafts
{
    /// <summary>
    ///     Container class for DCS F-14A by Heatblur
    /// </summary>
    internal class DCS_F14A : AircraftAB
    {
        public DCS_F14A(double gw = 18198.0)
        {
            Gw = Math.Abs(gw);
            Lsa = 94;
            Cl = 0.89;
            Bf = 79000;
            Rc = 3;
            RcAB = 2;
            Cd = 0.058;
            Rtr = 0;
            Thr = 98000;
            ThrAB = 153000;
            Clg = 0.0;
            Rfc = 0.0;
        }
    }
}
