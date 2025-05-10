using System;
using System.Collections.Generic;
using System.Linq;
using Modules.UI.Interface;
using Modules.UI.Window;
using Unity.VisualScripting;
using UnityEngine;

namespace Library.Scripts.Modules.Ui
{
   public class UiCanvas : MonoBehaviour
   {
      [SerializeField] private Canvas _canvasRef;
      [SerializeField] private Transform _windowContainer;
      [SerializeField] private RectTransform _canvasRect;
      [SerializeField] private CanvasGroup _canvasGroup;

      [SerializeField] private List<WindowBase> _windowList = new();

      public event Action OnUpdateWindow; 

      public void Init(IEnumerable<WindowData> windowList)
      {
         foreach (WindowData windowData in windowList)
         {
            var window = Instantiate(windowData.GetWindowRef.LoadFromPrefab(true).Result, _windowContainer);
            window.Init();
            _windowList.Add(window);
         }
      }

      public T GetWindow<T>() where T : WindowBase {
         return (T) _windowList.First(e => e.GetType() == typeof(T));
      }
      
      private void FixedUpdate()
      {
         //OnUpdateWindow?.Invoke();
      }

      public static Vector2 WorldToUISpace(Camera cam/*,RectTransform parent*/, Vector3 worldPos) {
         return cam.WorldToScreenPoint(worldPos);
         
        /* Vector3 screenPos = cam.WorldToScreenPoint(worldPos);
         Vector2 movePos = Vector2.zero;
         RectTransformUtility.ScreenPointToLocalPointInRectangle(parent, screenPos,
            null, out movePos);
         return movePos;*/
      }
      
      public void Destruct()
      {
         foreach (WindowBase windowBase in _windowList)
         {
            windowBase.Destruct();
         }
         _windowList.Clear();
      }
   }
}
