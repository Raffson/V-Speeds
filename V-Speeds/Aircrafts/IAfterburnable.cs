using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V_Speeds.Aircrafts
{
    public interface IAfterburnable
    {
        bool AB { get; set; }
        double ThrAB { get; set; }
        double RcAB { get; set; }
    }
}
