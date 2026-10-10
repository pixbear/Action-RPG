using System.Collections;
using UnityEngine;

public class SkillBase : MonoBehaviour
{   
    public enum KeyType
    {
        Q,
        W,
        E,
        R,
        D,
        F
    }

    public KeyType Key;
    public SkillDataSO Data;
    private bool isCooltime;
    public bool IsCooltime => isCooltime;

    private Player player;
    private PlayerAnimationController anim;

    private void Awake()
    {
        player = GetComponent<Player>();
        anim = GetComponent<PlayerAnimationController>();
    }

    private void Start()
    {
        UIManager.Instance.SetSkillSlot(Key, Data);
    }

    public void TryUseSkill()
    {
        if (isCooltime) return;
        if (player.CurState == Player.State.Attack) return;
        if (player.CurState == Player.State.Dead) return;
        OnSkillUsed();
    }

    protected virtual void OnSkillUsed()
    {
        UIManager.Instance.StartCooldown(Key);
        StartCoroutine(SkillRoutine());
        StartCoroutine(CooldownRoutine());
    }

    protected virtual IEnumerator SkillRoutine()
    {    
        anim.DoSkillAnimation(Key);
        yield return null;
    }

    private IEnumerator CooldownRoutine()
    {
        isCooltime = true;
        yield return new WaitForSeconds(Data.CoolTime);
        isCooltime = false;
    }

    protected Vector3 GetAimDir()
    {
        Vector3 aimDir = Vector3.zero;
        RaycastHit hit;
        LayerMask layer = LayerMask.GetMask("Ground");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
        {
            aimDir = new Vector3(hit.point.x, transform.position.y, hit.point.z) - transform.position;
        }
        return aimDir.normalized;
    }
}