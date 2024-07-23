using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadingSceneManager : MonoBehaviour
{
    [SerializeField] private string SceneName;
    [SerializeField] private GameObject loadingScene;
    [SerializeField] private Slider loadingSlider;
    public static LoadingSceneManager Instance { get; set; }


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
       
    }
    public void playNewGameLoadingScene()
    {
        StartCoroutine(LoadSceneNewGame(SceneName));
    }
    private IEnumerator LoadSceneNewGame(string sceneName)
    {
        yield return new WaitForSeconds(1f);
        loadingScene.SetActive(true);
        loadingSlider.value = 0;

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName);
        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01((loadOperation.progress / 0.9f));
            loadingSlider.value = progressValue;
            yield return null;
        }
        if (!SaveManager.Instance.isLoading)
        {
            loadingScene.SetActive(false);
            yield return new WaitForSeconds(1f);
            TimeLineManager.Instance.NewGameTimeLine();
        }
    }

    public IEnumerator LoadSceneFromLoadData(string sceneName, int slotNumber)
    {
        yield return new WaitForSeconds(1f);
        loadingScene.SetActive(true);
        loadingSlider.value = 0;

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName);
        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01((loadOperation.progress / 0.9f));
            loadingSlider.value = progressValue;
            yield return null;
        }
        if (loadOperation.isDone)
        {
            yield return new WaitForSeconds(0.25f);
            loadingScene.gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
            SaveManager.Instance.LoadGame(slotNumber);
            yield return new WaitForSeconds(2f);
            loadingScene.gameObject.SetActive(false);

        }
    }
}
