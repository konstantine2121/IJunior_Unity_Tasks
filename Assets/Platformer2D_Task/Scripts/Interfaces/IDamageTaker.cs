namespace Platformer2D_Task
{
    public interface IDamageTaker
    {
        void TakeHeal(float heal);

        void TakeDamage(float damage);

        void Kill();
    }
}