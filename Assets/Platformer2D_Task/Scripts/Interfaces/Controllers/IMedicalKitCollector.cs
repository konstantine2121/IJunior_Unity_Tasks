using System;

namespace Platformer2D_Task
{
    public interface IMedicalKitCollector
    {
        event Action<int> NumberOfKitsChanged;

        int MedicalKits { get; }

        int MaxMedicalKits { get; }

        float UseMedicalKit();
    }
}
