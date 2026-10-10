using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Player player;



    private void Awake()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        player.onStateChange += Set;
    }

    private void OnDisable()
    {
        player.onStateChange -= Set;
    }

    public void Set(Player.State state)
    {
        switch (state)
        {
            case Player.State.Idle:
                anim.SetBool("isMove", false);
                break;
            case Player.State.Move:
                anim.SetBool("isMove", true);
                break;
            case Player.State.Attack:
                anim.SetTrigger("Attack");
                break;
            case Player.State.Dead:
                anim.SetTrigger("Dead");
                break;
        }
    }

    public void DoSkillAnimation(SkillBase.KeyType type)
    {
        anim.SetTrigger(string.Format("{0}Skill", type.ToString()));
    }
}