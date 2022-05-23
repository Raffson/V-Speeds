namespace V_Speeds.ObserverPattern
{
    /// <summary>
    ///     Interface for Subjects in Observer Pattern, specifically to make objects notify their observers of changes to their properties.
    /// </summary>
    /// <typeparam name="T">The type of the observable object, usually the same type as the implementing class.</typeparam>
    public interface IMyObservable<T>
    {
        /// <summary>
        ///     Adds an observer to the list of observers to be notified of changes to properties.
        /// </summary>
        /// <param name="observer">The observer to be added.</param>
        void Subscribe(IMyObserver<T> observer);

        /// <summary>
        ///     Deletes an observer (if present) from the list of observers.
        /// </summary>
        /// <param name="observer">The observer to be deleted.</param>
        void Unsubscribe(IMyObserver<T> observer);

        /// <summary>
        ///     Notifies all observers of changes to the entire object, i.e. instances of IMyObserver should call <c>Update(this)</c>.
        /// </summary>
        void Notify();

        /// <summary>
        ///     Notifies all observers of changes to a specific property of the object, i.e. instances of IMyObserver should call <c>Update(property)</c>.
        /// </summary>
        /// <param name="property">The name of the property that has changed.</param>
        void Notify(string property);
    }
}
