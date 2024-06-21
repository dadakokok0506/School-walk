using UnityEngine;

public class DoorController1 : MonoBehaviour
{
    public Transform player;  // ��Ҷ���
    public Transform door;    // �Ŷ���
    public float distToOpen = 1f;  // �����Ŷ����ľ���
    private Animator anim;
    private bool isOpen = false; // �ŵ�ǰ�Ƿ��Ǵ�״̬

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float dist = Vector3.Distance(player.position, door.position); // �����������֮��ľ���

        if (dist < distToOpen && !isOpen) // ����ҿ����������ǹر�״̬ʱ
        {
            anim.SetTrigger("open1"); // ִ�д��ŵĶ���
            isOpen = true; // ������״̬Ϊ��
        }
        else if (dist >= distToOpen && isOpen) // �����Զ���������Ǵ�״̬ʱ
        {
            anim.SetTrigger("close1"); // ִ�йر��ŵĶ���
            isOpen = false; // ������״̬Ϊ�ر�
        }
    }
}
