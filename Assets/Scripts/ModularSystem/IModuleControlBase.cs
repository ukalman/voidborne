using System;
using System.Collections;
using System.Collections.Generic;

namespace ModularSystem
{
    public interface IModuleControlBase
    {
        
        
        List<IModuleBase> Modules { get; set; }
        Dictionary<Type, IModuleBase> ModuleTypesDictionary { get; set; }

        void AddModule(IModuleBase moduleBase);
        
        T GetModule<T>() where T : class, IModuleBase;

        IEnumerator IE_ActivateModules();

        IEnumerator IE_DeactivateModules();

        IEnumerator IE_ResetAllModules();
    }
}