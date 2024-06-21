using UnityEngine;
using UnityEngine.UI;

public class ShowTextAtLocation3 : MonoBehaviour
{
    public Text displayText;
    public float displayDuration = 3f; // 显示时间

    void Start()
    {
        // 确保在开始时隐藏文本显示
        if (displayText != null)
        {
            displayText.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 如果玩家进入了碰撞体
        {
            ShowCustomText("您已到地理教室！");
        }
    }

    public void ShowCustomText(string text)
    {
        if (displayText != null)
        {
            displayText.text = text;
            displayText.gameObject.SetActive(true); // 激活文本显示

            // 在 displayDuration 秒后调用 HideText 方法
            Invoke("HideText", displayDuration);
        }
    }

    void HideText()
    {
        if (displayText != null)
        {
            displayText.gameObject.SetActive(false); // 隐藏文本显示
        }
    }
}
