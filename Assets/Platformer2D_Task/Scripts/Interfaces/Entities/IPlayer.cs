namespace Platformer2D_Task
{
    public interface IPlayer : IDamageTargetTypeContainer
    {
        IHealth Health { get; }

        BoxCollector BoxCollector { get; }

        MedicalKitCollector MedicalKitCollector { get; }
    }
}
