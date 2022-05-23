namespace V_Speeds.Model.Aircrafts
{
    /// <summary>
    ///     Interface for aircraft with an afterburner.
    /// </summary>
    public interface IAfterburnable
    {
        /// <summary>
        ///     Property representing the state of the afterburner.
        /// </summary>
        bool AB { get; set; }

        /// <summary>
        ///     Property representing the thrust force of the afterburner, expected in Newton.
        /// </summary>
        double ThrAB { get; set; }

        /// <summary>
        ///     Property representing the reaction time associated with the afterburner, expected in seconds.
        /// </summary>
        double RcAB { get; set; }
    }
}
