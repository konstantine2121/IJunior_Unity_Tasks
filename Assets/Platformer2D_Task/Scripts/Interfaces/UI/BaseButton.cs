using System;

namespace Platformer2D_Task
{
    public abstract class BaseButton
    {
        public Action<BaseButton> Clicked;
        
        string Text { get; set; }
    }
}
