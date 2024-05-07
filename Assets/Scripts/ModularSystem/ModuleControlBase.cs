using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilities;

namespace ModularSystem
{
    public class ModuleControlBase : MonoBehaviour, IModuleControlBase
    {
        public string ControlBaseName;
        
        public Dictionary<Type, IModuleBase> ModuleTypesDictionary { get; set; }


        [SerializeField]
        private List<MonoBehaviour> _modules = new();
        
        public List<IModuleBase> Modules
        {
            get => _modules.Cast<IModuleBase>().ToList();
            set => _modules = value.Cast<MonoBehaviour>().ToList();
        }
        
        private void Awake()
        {
            StartCoroutine(SetupModules());
        }
        
        public IEnumerator SetupModules()
        {
            ModuleTypesDictionary = new Dictionary<Type, IModuleBase>();
            yield return StartCoroutine(RegisterAllModules());
        
        }
        
        public IEnumerator RegisterAllModules()
        {
            Modules.RemoveAll(module => module == null);

            Modules = Modules.OrderBy(m => m.Priority).ToList();
            _modules = Modules.Select(m => m as MonoBehaviour).Where(m => m != null).ToList();
       

            if (Modules.Count > 0)
            {
                foreach (var module in Modules)
                {
                    if(module.State != ModuleState.Activated) yield return StartCoroutine(ArrangeModule(module));

                }
            }
      

        }
        
        private IEnumerator ArrangeModule(IModuleBase module)
        {
            
            ModuleTypesDictionary[module.GetType()] = module;
            if (!Application.isPlaying) yield break;
            yield return StartCoroutine(module.SetParent(this)); 
            yield return StartCoroutine(module.IE_Initialize());
            yield return StartCoroutine(module.IE_PostInitialize());
            yield return StartCoroutine(module.IE_SetState(ModuleState.Activated)); // with this, IE_Activate is also called
        }
        
        public IEnumerator IE_ActivateModules()
        {
            foreach (var module in Modules)
            {
                if (module != null) yield return module.IE_Activate();
            }
        }
    
        public IEnumerator IE_DeactivateModules()
        {
            foreach (var module in Modules)
            {
                module?.Deactivate();
            }

            yield return null;
        }
        
        private void Update()
        {
            foreach (var module in Modules)
            {
                if (module != null)  module.Tick();
            }
        }

        private void FixedUpdate()
        {
            foreach(var module in Modules)
            {
                if (module != null)  module.FixedTick();
            }
        }

        private void LateUpdate()
        {
            foreach (var module in Modules)
            {
                if (module != null)  module.LateTick();
            }
        }

        public void AddModule(IModuleBase moduleBase)
        {
            if (moduleBase != null)
            {
                Modules.Add(moduleBase);
                Debug.Log($"New module {moduleBase.GetName()} added to {ControlBaseName}.");
            }
            
        }
        
        public T GetModule<T>() where T : class, IModuleBase
        {
            if (ModuleTypesDictionary.TryGetValue(typeof(T), out IModuleBase module))
            {
                return module as T;
            }

            return null;
        }
        
        public IEnumerator IE_ResetAllModules()
        {
            for (int i = Modules.Count - 1; i >= 0; i--)
            {
                if (Modules[i] != null)
                {
                    Debug.Log("Resetting module: " + Modules[i].GetName());
                    yield return Modules[i].IE_Restart();
                }
            }
            
        }
    }
}