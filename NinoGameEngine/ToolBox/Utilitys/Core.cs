using NinoGameEngine.NinoCore.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinoGameEngine.ToolBox.Utilitys.Game
{
    public abstract class Core : CoreInterface
    {
        public Core()
        {
            NinoBase.AddTokGameLoop(this);
        }

        void CoreInterface.OnClose() => OnClose();
        void CoreInterface.OnUpdate() => OnUpdate();


        public abstract void OnUpdate();
        public abstract void OnClose();
    }
}
