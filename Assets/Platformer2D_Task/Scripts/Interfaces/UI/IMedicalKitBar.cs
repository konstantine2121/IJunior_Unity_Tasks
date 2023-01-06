using UnityEngine.UIElements;

namespace Platformer2D_Task.UI
{
    public interface IMedicalKitBar
    {
        Button MedicalKitButton { get; }

        void RegisterCollector(IMedicalKitCollector collector);

        void UnregisterCollector();

        void RegisterHealTarget(IHealth healTarget);

        void UnregisterHealTarget();
    }
}
