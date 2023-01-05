using UnityEditor;

namespace Platformer2D_Task
{    
    public interface IGameController
    {
        void Restart();

        void Stop();
        
        void Pause();

        void Resume();
    }
}
