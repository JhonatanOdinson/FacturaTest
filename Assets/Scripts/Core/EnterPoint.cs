using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Library.Scripts.Modules.Ui;
using UnityEngine;

namespace Library.Scripts.Core {
  public class EnterPoint : MonoBehaviour {
    private static bool _isInitGC = false;
    private bool _initProcess = false;
    private bool _destructProcess;
    [SerializeField] private List<WindowData> _loadWindowList = new List<WindowData>();
    [SerializeField] private SceneComponents _sceneComponents;
    [SerializeField] private Transform _actorContainer;
    public Action OnEnterPointInited;
    
    public SceneComponents SceneComponentsRef => _sceneComponents;
    public IEnumerable<WindowData> LoadWindowList => _loadWindowList;
    public Transform ActorContainer => _actorContainer;
    public bool DestructProcess => _destructProcess;

    async void Start() {
      if(_isInitGC) return;
      await Init();
    }

    private void Update() {
      if (!_isInitGC) return;
    }

    public async Task Init() {
      if(_initProcess) return;
      _initProcess = true;
      GameDirector.SetEnterPoint(this);
      if (!_isInitGC)
        await InitGlobalControllers();
      await CommonComponents.Instance.Init(this);
      _sceneComponents?.Init();
     OnEnterPointInited?.Invoke();
      //SceneLoader.Instance.OnLoadStart += Destruct;
    }

    private async Task InitGlobalControllers() {
      CommonComponents.LoadInstance();
      await CommonComponents.Instance.LoadData();
      CommonComponents.Instance.InitGlobal();
      _isInitGC = true;
    }
    

    public void OnApplicationQuit() {
      if(!Application.isEditor) return;
      Destruct(); //call unsubscribe
    }

    private void Destruct() {
      if(_destructProcess) return;
      _destructProcess = true;
      StopAllCoroutines();
      CommonComponents.Instance.FreeControllers();
      SceneComponentsRef.Destruct();
    }
  }
}
