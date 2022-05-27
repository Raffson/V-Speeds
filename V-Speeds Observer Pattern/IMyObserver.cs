namespace V_Speeds.ObserverPattern
{
    /// <summary>
    ///     Interface for Observers in Observer Pattern, specifically for receiving notifications of changed properties.
    /// </summary>
    /// <typeparam name="T">The type of the object to be observed, i.e. the Subject/Observable.</typeparam>
    public interface IMyObserver<T>
    {
        /// <summary>
        ///     Updates the observer's view. Could also be used to push updates through to other observers.
        /// </summary>
        /// <param name="value">The type of the Subject, i.e. the Observable object.</param>
        void Update(T value);

        /// <summary>
        ///     Updates the observer's view for a specific property. Could also be used to push updates through to other observers.
        /// </summary>
        /// <param name="property">The name of the property that has changed.</param>
        void Update(string property);
    }
}
