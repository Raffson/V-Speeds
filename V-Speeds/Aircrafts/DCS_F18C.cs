using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V_Speeds.Aircrafts
{
    internal class DCS_F18C : Aircraft
    {
        public DCS_F18C(double gw = 10433.0)
        {
            Gw = gw;
            Lsa = 38;
            Cl = 1.05;
            Bf = 50000;
            Rc = 3; // 4
            Cd = 0.12;
            Rtr = 0;
            Thr = 105000; // 146000
            Clg = 0.55;
            Rfc = 0.033;
        }

        public override bool HasAfterburner() => true;
    }
}
