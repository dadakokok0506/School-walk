using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMgr : MonoBehaviour
{
    #region 单例
    public static SceneMgr sceneMgr;
    private void Start()
    {
        sceneMgr = this;
        DontDestroyOnLoad(gameObject);
        dic = new Dictionary<SceneMode, string>();
        dic.Add(SceneMode.Scene1, "Scene1");
        dic.Add(SceneMode.Scene2, "Scene2");
        dic.Add(SceneMode.Scene3, "Scene3");
    }
    #endregion

    /// <summary>
    /// 场景
    /// </summary>
    public enum SceneMode
    {
        Scene1,
        Scene2,
        Scene3
    };
    /// <summary>
    /// 当前场景
    /// </summary>
    public SceneMode TargetScene;

    public Dictionary<SceneMode, string> dic;


    public Action<float> UIEvent;
    public Action OnLoadingComplete;
    //====
    //public Slider slider;
    //public Text text;
    //public Animator anim;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            SceneLoadAsync(TargetScene);


        if (Input.GetKeyDown(KeyCode.B))
            SceneMgr.sceneMgr.SceneLoadAsync(SceneMode.Scene1,() => 
            { 
                Debug.Log("当前场景:" + SceneManager.GetActiveScene().name);
                Debug.Log("当前场景物体:" + GameObject.Find("1").name);
            }
            );
    }

    public void SceneLoadAsync(SceneMode sceneMode,Action action = null)
    {
        SceneManager.LoadScene("Loading");
        OnLoadingComplete = ()=> { StartCoroutine(SceneLoadAsyncIE(sceneMode, action)); };
        //StartCoroutine(SceneLoadAsyncIE(sceneMode,action));
    }

    IEnumerator SceneLoadAsyncIE(SceneMode sceneMode, Action action)
    {
        float disProgress = 0;
        float currentProgress = 0;
        if (dic[sceneMode] == null || !dic.ContainsKey(sceneMode))yield break;
        AsyncOperation ao = SceneManager.LoadSceneAsync(dic[sceneMode]);
        ao.allowSceneActivation = false;//是否允许加载完毕立即跳转到下一个场景
        while (currentProgress < 0.9f)
        {
            currentProgress = ao.progress;
            while (disProgress < currentProgress)
            {
                disProgress += 0.01f;
                if(UIEvent!=null)
                    UIEvent(disProgress);
                yield return null;
            }
        }
        while (disProgress <= 1)
        {
            disProgress += 0.01f;
            if (UIEvent != null)
                UIEvent(disProgress);
            yield return null;
        }
        ao.allowSceneActivation = true;
        OnLoadingComplete = null;
        while (!ao.isDone)
            yield return null;
        if (action != null) action();
        yield return null;
    }
}
