namespace Beobachter
{
    public interface IObservable
    {
        void RegistriereBeobachter(IObserver b);
        void EntferneBeobachter(IObserver b);
        void BenachrichtigeAlleBeobachter();
    }
}