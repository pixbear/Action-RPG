using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator anim;
    public Animator Anim => anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
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