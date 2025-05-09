using System.Threading.Tasks;
using Core;
using Library.Scripts.Modules.Tools;
using Library.Scripts.Modules.Ui;
using Modules.Actor;
using UnityEngine;

namespace Library.Scripts.Core
{
    public class CommonComponents : MonoBehaviour
    {
        #region Instance

        private static CommonComponents _instance;

        public static CommonComponents Instance
        {
            get
            {
                if (_instance == null)
                {
                    LoadInstance();
                }

                return _instance;
            }

            set { _instance = value; }
        }

        #endregion

        [SerializeField] private UiCanvas _uiCanvas;

        [SerializeField] private TouchInputController _touchInputController;
        
        [SerializeField] private ActorBaseController _actorBaseController;
        [SerializeField] private GameStateController _gameStateController;

        public static TouchInputController TouchInputController => _instance._touchInputController;
        public static UiCanvas UiCanvas => _instance._uiCanvas;
        public static ActorBaseController ActorBaseController => _instance._actorBaseController;
        public static GameStateController GameStateController => _instance._gameStateController;

        public static void LoadInstance()
        {
            if (_instance != null) return;
            _instance = LocalAssetLoader.InstantiateAsset<CommonComponents>("CommonComponents", null, true).Result;
            DontDestroyOnLoad(_instance.gameObject);
        }

        public async Task Init(EnterPoint enterPoint)
        {
            Debug.Log($"Init");
            _uiCanvas.Init(enterPoint.LoadWindowList);
            //_gameObjectController.Init();
        }

        public async Task LoadData()
        {
            await Task.WhenAll(

            );
        }

        public void InitGlobal()
        {
            Debug.Log($"Init Global");
            _actorBaseController.Init();
            _gameStateController.Init();
            _touchInputController.Init();
        }

        public void FreeControllers()
        {
            _actorBaseController.Free();
            _uiCanvas.Destruct();
            //_gameObjectController.FreeController();
            _touchInputController.Free();
        }
    }
}
