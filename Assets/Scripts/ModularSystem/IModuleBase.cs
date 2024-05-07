using System.Collections;
using Utilities;

namespace ModularSystem
{
    public interface IModuleBase
    {
        public IModuleControlBase Parent { get; set; }
        public ModuleState State {get; set;}
        public int Priority { get; set; }

        IEnumerator IE_SetState(ModuleState state);

        IModuleControlBase GetParent();

        IEnumerator SetParent(IModuleControlBase parent);

        string GetName();

        IEnumerator IE_Deinitialize(); 
        IEnumerator IE_Initialize();
        IEnumerator IE_PostInitialize();
        IEnumerator IE_Activate();
        void Deactivate();
        IEnumerator IE_Disable();
        IEnumerator IE_Pause();
        IEnumerator IE_Resume();
        IEnumerator IE_Restart();

        bool Tick();

        bool FixedTick();

        bool LateTick();
    }
}