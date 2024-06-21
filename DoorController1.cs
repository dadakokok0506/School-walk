using UnityEngine;

public class DoorController1 : MonoBehaviour
{
    public Transform player;  // 玩家对象
    public Transform door;    // 门对象
    public float distToOpen = 1f;  // 触发门动作的距离
    private Animator anim;
    private bool isOpen = false; // 门当前是否是打开状态

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float dist = Vector3.Distance(player.position, door.position); // 计算玩家与门之间的距离

        if (dist < distToOpen && !isOpen) // 当玩家靠近门且门是关闭状态时
        {
            anim.SetTrigger("open1"); // 执行打开门的动画
            isOpen = true; // 更新门状态为打开
        }
        else if (dist >= distToOpen && isOpen) // 当玩家远离门且门是打开状态时
        {
            anim.SetTrigger("close1"); // 执行关闭门的动画
            isOpen = false; // 更新门状态为关闭
        }
    }
}
