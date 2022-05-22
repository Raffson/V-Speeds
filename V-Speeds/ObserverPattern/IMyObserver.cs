namespace V_Speeds.ObserverPattern
{
    public interface IMyObserver<T>
    {
        void Update(T value);
        void Update(string property);
    }
}
