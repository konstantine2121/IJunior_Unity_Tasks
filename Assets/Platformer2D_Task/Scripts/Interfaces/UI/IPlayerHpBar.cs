namespace Platformer2D_Task.UI
{
    public interface IPlayerHpBar
    {
        public void RegisterHealth(IHealth health);

        public void UnregisterHealth();
    }
}
