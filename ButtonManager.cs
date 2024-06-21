using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonManager : MonoBehaviour
{
    public Button button1;
    public Button button2;
    public Button button3;

    public GameObject loadingScreen; // ���ؽ���� GameObject
    public Slider loadingBar; // ���ؽ�����

    public float delayTime = 1.0f; // ��Ϊ�ӳ�ʱ��

    public Button loadButton; public Button loadButton2; public Button loadButton3;

    public float loadTime = 3.0f;


    void Start()
    {
        // ��ÿ����ť��ӵ���¼�
        button1.onClick.AddListener(() => GoToScene("SchoolSceneDay"));
        button2.onClick.AddListener(() => GoToScene("SchoolSceneNight"));
        button3.onClick.AddListener(() => GoToScene("SchoolSceneAbandoned"));
        loadButton.onClick.AddListener(StartLoading);
        // ȷ�����ؽ����ڿ�ʼʱ�����ص�
        loadingScreen.SetActive(false);
    }
    void StartLoading()
    {
        // ��������Э��
        StartCoroutine(LoadProgress());
    }

    IEnumerator LoadProgress()
    {
        float timer = 0.0f;
        float progress = 0.0f;

        // �ڼ���ʱ���ڸ��½�����
        while (timer < loadTime)
        {
            timer += Time.deltaTime;
            progress = Mathf.Clamp01(timer / loadTime);
            loadingBar.value = progress;
            yield return null;
        }
       
        // ������ɺ󣬿���ִ�����������������л�������
        Debug.Log("������ɣ�");
    }


IEnumerator LoadSceneAsync(string sceneName)
    {
        // �첽����Ŀ�곡��
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        // ��Ŀ�곡����δ�������ʱ
        while (!operation.isDone)
        {
            // ���¼��ؽ�����
            float progress = Mathf.Clamp01(operation.progress / 0.9f); // 0.9 �Ǽ�����ɵĽ���
            loadingBar.value = progress;
            Debug.Log("�����ƶ�");
            yield return null;
        }
    }
    IEnumerator DelayedLoadScene(string sceneName)
    {
        // �ȴ���Ϊ�ӳ�ʱ��
        yield return new WaitForSeconds(delayTime);

        // �첽����Ŀ�곡��
        StartCoroutine(LoadSceneAsync(sceneName));
    }


    void GoToScene(string sceneName)
    {
        // ���ü��ؽ���
        loadingScreen.SetActive(true);

        // �ӳ�һ��ʱ���ټ��س���
        StartCoroutine(DelayedLoadScene(sceneName));
    }
    
}
