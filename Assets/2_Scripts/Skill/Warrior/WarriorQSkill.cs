using System.Collections;
using UnityEngine;

public class WarriorQSkill : SkillBase
{
    [SerializeField] Axe axe;

    protected override IEnumerator SkillRoutine()
    {
        base.SkillRoutine();
        axe.SetDir(GetAimDir());
        yield return new WaitForSeconds(1f);

        while (Vector3.Distance(transform.position, axe.transform.position) > 0.1f)
        {
            var dir = (axe.transform.position - transform.position).normalized;
            axe.SetDir(dir);
            yield return null;
        }
    }
}
