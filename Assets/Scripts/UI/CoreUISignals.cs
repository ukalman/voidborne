using Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class CoreUISignals : MonoSingleton<CoreUISignals>
    {
        public UnityAction<UIPanelTypes, int> OnOpenPanel = delegate { };
        public UnityAction<int> OnClosePanel = delegate { };
        public UnityAction OnCloseAllPanels = delegate { };
        
    }
}