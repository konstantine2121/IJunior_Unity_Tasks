namespace Platformer2D_Task.UI
{
    public interface IScoreBar
    {
        void RegisterCollector(BoxCollector health);

        void UnregisterCollector();
    }
}
