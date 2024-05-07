using System;
using System.Collections;
using UnityEngine;
using Utilities;

namespace ModularSystem
{
    public class ModuleBase : MonoBehaviour, IModuleBase
    {
        [field: SerializeField]
        public int Priority { get; set; }

        [field: SerializeField]
        protected string moduleName;

        [field: SerializeField]
        public ModuleState State { get; set; }
        public Action<ModuleState> OnStateChangeBegin { get; set; }
        public Action<ModuleState> OnStateChangeEnd { get; set; }
        
        [field: SerializeField]
        public IModuleControlBase Parent { get; set; }
        
        public virtual IEnumerator IE_Activate()
        {
            gameObject.SetActive(true);
            yield return null;
        }
        
        public virtual void Deactivate()
        {
            gameObject.SetActive(false);
        }
        
        public IEnumerator IE_SetState(ModuleState state)
        {
            OnStateChangeBegin?.Invoke(State);
            State = state;

            switch (State)
            {
                case ModuleState.Uninitialized:
                    yield return StartCoroutine(IE_Deinitialize());
                    break;
                case ModuleState.Initialized:
                    yield return StartCoroutine(IE_Initialize());
                    break;
                case ModuleState.PostInitialized:
                    yield return StartCoroutine(IE_PostInitialize());
                    break;
                case ModuleState.Activated:
                    yield return StartCoroutine(IE_Activate());
                    break;
                case ModuleState.Deactivated:
                    Deactivate();
                    break;
                case ModuleState.Disabled:
                    yield return StartCoroutine(IE_Disable());
                    break;
                case ModuleState.Paused:
                    yield return StartCoroutine(IE_Pause());
                    break;
                case ModuleState.Resumed:
                    yield return StartCoroutine(IE_Resume());
                    break;
                case ModuleState.Restarted:
                    yield return StartCoroutine(IE_Restart());
                    break;
                default:
                    break;
            }

            OnStateChangeEnd?.Invoke(State);
        }
        
        public IModuleControlBase GetParent()
        {
            return this.Parent;
        }

        public IEnumerator SetParent(IModuleControlBase parent)
        {
            Parent = parent;
            yield return null;
        
        }

        public string GetName()
        {
            return moduleName;
        }

        public virtual IEnumerator IE_Initialize()
        {
            yield return null;
        }

        public virtual IEnumerator IE_PostInitialize()
        {
            yield return null;
        }

        public virtual IEnumerator IE_Deinitialize()
        {
            yield return null;
        }

        public virtual IEnumerator IE_Disable()
        {
            yield return null;
        }

        public virtual IEnumerator IE_Pause()
        {
            yield return null;
        }

        public virtual IEnumerator IE_Resume()
        {
            yield return null;
        }

        public virtual IEnumerator IE_Restart()
        {
            yield return null;
        }

        public virtual bool Tick()
        {
            return State == ModuleState.Activated;
        }


        public virtual bool FixedTick()
        {
            return State == ModuleState.Activated;
        }

        public virtual bool LateTick()
        {
            return State == ModuleState.Activated;
        }
        
        
    }
}