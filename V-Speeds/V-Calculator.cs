using System;

namespace V_Speeds
{
    class V_Calculator
    {
        public const double igc = 8.3144598; // ideal gas constant
        public const double mmair = 28.97 / 1000; // molecular mass of air
        public const double g = 9.83; // 1G at poles (m/s2), just for some extra wiggle room, considering no elevation (except pressure)...
        public const double p0 = 101325 * mmair / (igc * 288.15); // standard sea density


        private double _gw, _oat, _qfe, _lsa, _cl, _thr, _bf, _rl, _csa, _cd, _rtr;


        public V_Calculator(double gw = 1000.0, double oat = 288.15, double qfe = 101325, double lsa = 10.0, double cl = 1.0,
            double thr = 1000.0, double bf = 500.0, double rl = 2500.0, double csa = 1.0, double cd = 0.1, double rtr = 0.0)
        {
            Gw = gw;
            Oat = oat;
            Qfe = qfe;
            Lsa = lsa;
            Cl = cl;
            Thr = thr;
            Bf = bf;
            Rl = rl;
            Csa = csa;
            Cd = cd;
            Rtr = rtr;
        }

        public double Gw { get => _gw; set => _gw = value; }
        public double Oat { get => _oat; set => _oat = value; }
        public double Qfe { get => _qfe; set => _qfe = value; }
        public double Lsa { get => _lsa; set => _lsa = value; }
        public double Cl { get => _cl; set => _cl = value; }
        public double Thr { get => _thr; set => _thr = value; }
        public double Bf { get => _bf; set => _bf = value; }
        public double Rl { get => _rl; set => _rl = value; }
        public double Csa { get => _csa; set => _csa = value; }
        public double Cd { get => _cd; set => _cd = value; }
        public double Rtr { get => _rtr; set => _rtr = value; }

        // internal setter-functions for Form1
        internal void SetGw(double value) => Gw = value;
        internal void SetOat(double value) => Oat = value;
        internal void SetQfe(double value) => Qfe = value;
        internal void SetLsa(double value) => Lsa = value;
        internal void SetCl(double value) => Cl = value;
        internal void SetThr(double value) => Thr = value;
        internal void SetBf(double value) => Bf = value;
        internal void SetRl(double value) => Rl = value;
        internal void SetCsa(double value) => Csa = value;
        internal void SetCd(double value) => Cd = value;
        internal void SetRtr(double value) => Rtr = value;

        public double CalcV1()
        {
            //ignore change in mass => extra safety margin
            //ignore wind => extra safety margin (headwind would increase IAS and decrease GS, thus less distance travelled on runway...)
            //ignore speedbrakes => more safety margin because longer braking distance...
            //using EAS to approximate IAS...
            double p = _qfe * mmair / (igc * _oat);
            double easfactor = Math.Sqrt(p / p0);
            bool stop = false;
            double t = 0.1;   // time interval 0.1 seconds
            double tas = 0.0; // assuming no headwind (extra safety) => tas = gs
            double rwl = _rl; // how much runway do we have left...
            while( !stop )
            {
                double eas = tas * easfactor;
                double drag = Math.Pow(eas, 2) * p * _csa * _cd / 2;
                double acc = (_thr - drag) / _gw;
                rwl -= (tas*t + acc*Math.Pow(t, 2)/2);
                double rwl2 = rwl - (tas * 5); // allow for 5 seconds of "rollout" (engine cooldown) time before actual braking takes effect...
                double totalbrake = _bf + drag + (_thr * _rtr);
                double dec = totalbrake / _gw;
                double tntb = tas / dec; // time needed to brake...
                double bdist = (tas * tntb) - (dec * Math.Pow(tntb, 2) / 2);
                if (bdist > rwl2) stop = true;
                tas += (acc * t);
                //Console.WriteLine(String.Format("TAS={0}, RWL={1}, EAS={2}, DRAG={3}, ACC={4}, TOTBR={5}, DEC={6}, TNTB={7}, BDIST={8}",
                //    tas, rwl, eas, drag, acc, totalbrake, dec, tntb, bdist));
            }
            return ((tas * easfactor) - 5); // 5kts safety margin
        }

        public double CalcV2()
        {
            double force = _gw * g;
            double p = _qfe * mmair / (igc * _oat);
            double result = Math.Sqrt(2*force/(p*_lsa*_cl));
            return result;
        }
    }
}
