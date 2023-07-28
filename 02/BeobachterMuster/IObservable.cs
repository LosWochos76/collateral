public interface IObservable
{
    public void BeobachterAnmelden(IObserver b);
    public void BeobachterAbmelden(IObserver b);
    public void BeobachterInformieren();
}