namespace Beobachter
{
    public interface IObserver
    {
        void AenderungIstEingetreten(IObservable quelle);
    }
}