namespace V_Speeds.Model.Aircrafts
{
    public interface IAfterburnable
    {
        bool AB { get; set; }
        double ThrAB { get; set; }
        double RcAB { get; set; }
    }
}
