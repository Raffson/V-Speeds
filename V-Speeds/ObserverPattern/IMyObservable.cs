namespace V_Speeds.ObserverPattern
{
    public interface IMyObservable<T>
    {
        void Subscribe(IMyObserver<T> observer);
        void Unsubscribe(IMyObserver<T> observer);
        void Notify();
        void Notify(string property);
    }
}
