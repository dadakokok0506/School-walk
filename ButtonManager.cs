using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonManager : MonoBehaviour
{
    public Button button1;
    public Button button2;
    public Button button3;

    public GameObject loadingScreen; // 加载界面的 GameObject
    public Slider loadingBar; // 加载进度条

    public float delayTime = 1.0f; // 人为延迟时间

    public Button loadButton; public Button loadButton2; public Button loadButton3;

    public float loadTime = 3.0f;


    void Start()
    {
        // 给每个按钮添加点击事件
        button1.onClick.AddListener(() => GoToScene("SchoolSceneDay"));
        button2.onClick.AddListener(() => GoToScene("SchoolSceneNight"));
        button3.onClick.AddListener(() => GoToScene("SchoolSceneAbandoned"));
        loadButton.onClick.AddListener(StartLoading);
        // 确保加载界面在开始时是隐藏的
        loadingScreen.SetActive(false);
    }
    void StartLoading()
    {
        // 启动加载协程
        StartCoroutine(LoadProgress());
    }

    IEnumerator LoadProgress()
    {
        float timer = 0.0f;
        float progress = 0.0f;

        // 在加载时间内更新进度条
        while (timer < loadTime)
        {
            timer += Time.deltaTime;
            progress = Mathf.Clamp01(timer / loadTime);
            loadingBar.value = progress;
            yield return null;
        }
       
        // 加载完成后，可以执行其他操作，比如切换场景等
        Debug.Log("加载完成！");
    }


IEnumerator LoadSceneAsync(string sceneName)
    {
        // 异步加载目标场景
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        // 当目标场景还未加载完成时
        while (!operation.isDone)
        {
            // 更新加载进度条
            float progress = Mathf.Clamp01(operation.progress / 0.9f); // 0.9 是加载完成的进度
            loadingBar.value = progress;
            Debug.Log("滑块移动");
            yield return null;
        }
    }
    IEnumerator DelayedLoadScene(string sceneName)
    {
        // 等待人为延迟时间
        yield return new WaitForSeconds(delayTime);

        // 异步加载目标场景
        StartCoroutine(LoadSceneAsync(sceneName));
    }


    void GoToScene(string sceneName)
    {
        // 启用加载界面
        loadingScreen.SetActive(true);

        // 延迟一段时间再加载场景
        StartCoroutine(DelayedLoadScene(sceneName));
    }
    
}
