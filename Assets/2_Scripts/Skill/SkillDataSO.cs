using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "ScriptableObjects/SkillDataSO", order = 1)]
public class SkillDataSO : ScriptableObject
{
    public Sprite Icon;
    public string Name;
    public string Desc;
    public int CoolTime;
}