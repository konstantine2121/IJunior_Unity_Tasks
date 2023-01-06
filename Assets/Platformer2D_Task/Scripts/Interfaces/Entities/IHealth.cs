using System;

namespace Platformer2D_Task
{
    public interface IHealth :  IDamageTaker
    {
        event Action<IHealth, float> ValueChanged;
        event Action<IHealth, float> Died;
        event Action<IHealth, bool> InvulnerabilityChanged;

        float Value { get; }

        float MaxValue { get; }

        bool Invulnerability { get; }
    }
}
