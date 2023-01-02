namespace Platformer2D_Task.UI
{
    public interface IScoreBar
    {
        public void RegisterCollector(BoxCollector health);

        public void UnregisterCollector();
    }
}
