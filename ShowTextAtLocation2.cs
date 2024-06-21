using UnityEngine;
using UnityEngine.UI;

public class ShowTextAtLocation3 : MonoBehaviour
{
    public Text displayText;
    public float displayDuration = 3f; // ��ʾʱ��

    void Start()
    {
        // ȷ���ڿ�ʼʱ�����ı���ʾ
        if (displayText != null)
        {
            displayText.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // �����ҽ�������ײ��
        {
            ShowCustomText("���ѵ�������ң�");
        }
    }

    public void ShowCustomText(string text)
    {
        if (displayText != null)
        {
            displayText.text = text;
            displayText.gameObject.SetActive(true); // �����ı���ʾ

            // �� displayDuration ������ HideText ����
            Invoke("HideText", displayDuration);
        }
    }

    void HideText()
    {
        if (displayText != null)
        {
            displayText.gameObject.SetActive(false); // �����ı���ʾ
        }
    }
}
