namespace V_Speeds.Aircrafts
{
    internal class DCS_F16C_blk50 : Aircraft
    {
        public DCS_F16C_blk50()
        {
            Lsa = 28;
            Cl = 0.9;
            Bf = 51000;
            Rc = 4; // 2.5
            Cd = 0.095;
            Rtr = 0;
            Thr = 67000; // 114000
            Clg = 0.58;
            Rfc = 0.047;
        }

        public override bool HasAfterburner() => true;
    }
}
