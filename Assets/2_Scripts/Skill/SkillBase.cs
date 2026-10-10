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

    private KeyType keyType;
    private SkillDataSO data;
    private bool isCooltime;
    public bool IsCooltime => isCooltime;

    private Player player;
    private PlayerAnimation anim;

    private void Awake()
    {
        player = GetComponent<Player>();
        anim = GetComponent<PlayerAnimation>();
    }

    public void Init(KeyType key, SkillDataSO skillData)
    {
        keyType = key;
        data = skillData;
        UIManager.Instance.SetSkillSlot(keyType, data);
    }

    private void Update()
    {
        var key = StringToKeyCode(keyType.ToString()); 
        if (Input.GetKeyDown(key)) TryUseSkill();
    }

    private KeyCode StringToKeyCode(string key)
    {
        switch (key)
        {
            case "Q": return KeyCode.Q;
            case "W": return KeyCode.W;
            case "E": return KeyCode.E;
            case "R": return KeyCode.R;
            case "D": return KeyCode.D;
            case "F": return KeyCode.F;
            default: return KeyCode.None;
        }
    }

    public void TryUseSkill()
    {
        if (isCooltime) return;
        if (player.CurState == State.Attack) return;
        if (player.CurState == State.Dead) return;
        OnSkillUsed();
    }

    protected virtual void OnSkillUsed()
    {
        UIManager.Instance.StartCooldown(keyType);
        StartCoroutine(SkillRoutine());
        StartCoroutine(CooldownRoutine());
    }

    protected virtual IEnumerator SkillRoutine()
    {    
        anim.DoSkillAnimation(keyType);
        yield return null;
    }

    private IEnumerator CooldownRoutine()
    {
        isCooltime = true;
        yield return new WaitForSeconds(data.CoolTime);
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