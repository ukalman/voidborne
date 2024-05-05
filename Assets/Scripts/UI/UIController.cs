using System.Collections.Generic;
using Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private List<Transform> layers = new List<Transform>();

        #endregion

        #endregion

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreUISignals.Instance.OnClosePanel += OnClosePanel;
            CoreUISignals.Instance.OnOpenPanel += OnOpenPanel;
            CoreUISignals.Instance.OnCloseAllPanels += OnCloseAllPanels;
        }

        [Button("Close All Panels")]
        private void OnCloseAllPanels()
        {
            foreach (var layer in layers)
            {
                if (layer.childCount <= 0) return;
#if UNITY_EDITOR
                DestroyImmediate(layer.GetChild(0).gameObject);
#else
                Destroy(layer.GetChild(0).gameObject);
#endif
            }
        }

        [Button("Open Panel")]
        private void OnOpenPanel(UIPanelTypes panelType, int value)
        {
            OnClosePanel(value);
            var prefab = Resources.Load<GameObject>($"UIPanels/{panelType}Panel");
            Debug.Log(prefab); 
            if (prefab == null)
            {
                Debug.LogError("Failed to load prefab from Resources: " + $"UIPanels/{panelType}Panel");
                return;
            }
            Instantiate(prefab, layers[value]);
        }

        [Button("Close Panel")]
        private void OnClosePanel(int value)
        {
            if (layers[value].childCount <= 0) return;

#if UNITY_EDITOR
            DestroyImmediate(layers[value].GetChild(0).gameObject);
#else
                Destroy(layers[value].GetChild(0).gameObject);
#endif
        }

        private void UnSubscribeEvents()
        {
            CoreUISignals.Instance.OnClosePanel -= OnClosePanel;
            CoreUISignals.Instance.OnOpenPanel -= OnOpenPanel;
            CoreUISignals.Instance.OnCloseAllPanels -= OnCloseAllPanels;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}