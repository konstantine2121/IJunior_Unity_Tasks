namespace Platformer2D_Task.UI
{
    public interface IGameView : IBaseMenuView
    {
        IPlayerHpBar PlayerHpBar { get; }

        IScoreBar ScoreBar { get; }
    }
}
