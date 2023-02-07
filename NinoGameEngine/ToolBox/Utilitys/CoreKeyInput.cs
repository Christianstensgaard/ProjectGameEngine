using NinoGameEngine.NinoCore.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinoGameEngine.ToolBox.Utilitys.Game
{
    public abstract class CoreKeyInput:KeyInputInterface, CoreInterface
    {
        public CoreKeyInput()
        {
            NinoBase.AddToKeyListener(this);
            NinoBase.AddTokGameLoop(this);
        }

        void CoreInterface.OnClose() => OnClose();
        void KeyInputInterface.OnKeyDown(KeyEventArgs key) => OnKeyDown(key);
        void KeyInputInterface.OnKeyUp(KeyEventArgs key) => OnKeyUp(key);
        void CoreInterface.OnUpdate() => OnUpdate();

        public abstract void OnKeyDown(KeyEventArgs key);
        public abstract void OnKeyUp(KeyEventArgs key);
        public abstract void OnUpdate();
        public abstract void OnClose();





    }
}
