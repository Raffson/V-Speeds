namespace V_Speeds.Aircrafts
{
    internal class DCS_F14A : Aircraft
    {
        public DCS_F14A()
        {
            Lsa = 94;
            Cl = 0.89;
            Bf = 79000;
            Rc = 3; // 2
            Cd = 0.058;
            Rtr = 0;
            Thr = 98000; // 153000
            Clg = 0.0;
            Rfc = 0.0;
        }

        public override bool HasAfterburner() => true;
    }
}
