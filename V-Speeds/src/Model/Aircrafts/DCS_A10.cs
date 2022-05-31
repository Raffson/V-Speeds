namespace V_Speeds.Model.Aircrafts
{
    /// <summary>
    ///     Container class for DCS A-10 (A/C/C-II) by Eagle Dynamics
    /// </summary>
    internal class DCS_A10 : Aircraft
    {
        public DCS_A10(double gw = 11321.0)
        {
            Gw = Math.Abs(gw);
            Lsa = 47;
            Cl = 1.03;
            Bf = 63000;
            Rc = 4;
            Cd = 0.08;
            Rtr = 0;
            Thr = 63750;
            Clg = 0.61;
            Rfc = 0.037;
        }
    }
}
