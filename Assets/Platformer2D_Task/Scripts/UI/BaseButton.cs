using System;

namespace Platformer2D_Task.UI
{
    public abstract class BaseButton
    {
        public Action<BaseButton> Clicked;
        
        public abstract string Text { get; set; }
    }
}
