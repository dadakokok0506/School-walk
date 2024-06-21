using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
    public Slider slider;
    public Text text;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        SceneMgr.sceneMgr.UIEvent = OnSceneLoading;
        if(SceneMgr.sceneMgr.OnLoadingComplete!=null)
        SceneMgr.sceneMgr.OnLoadingComplete();
    }

    private void OnSceneLoading(float disProgress)
    {
        anim.Play("loadImage", -1, disProgress);
        slider.value = disProgress;
        text.text = ((int)(slider.value * 100)) + "%";
    }
}
