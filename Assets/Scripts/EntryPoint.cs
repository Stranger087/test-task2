using addressable;
using di;
using game;
using ui;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    private void Awake()
    {
        var uiManagerPrefab = AddressableUtils.LoadImmediately<GameObject>(AddressableKeys.UiManager);

        var uiManagerGo = Instantiate(uiManagerPrefab);
        DontDestroyOnLoad(uiManagerGo);
        var uiManager = uiManagerGo.GetComponent<UiManager>();

        GlobalContext.BindInstance(uiManager);
        GlobalContext.BindInstance(new MetaGameLogic());

        uiManager.Initialize();
        uiManager.ShowMainScreen();
    }
}