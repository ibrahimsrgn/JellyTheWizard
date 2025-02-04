using DG.Tweening;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public enum SkillType
    {
        SingleTarget,
        AEO,
        Buff
    }
    public SkillType type;
    public Transform target;
    public void UseSkill()
    {
        switch (type)
        {
            case SkillType.SingleTarget:
                ShootSingleTargetSkill(target);
                break;
            case SkillType.AEO:
                ShootAEOSkill(target);
                break;
            case SkillType.Buff:
                Buff();
                break;
        }
    }
    private void ShootSingleTargetSkill(Transform target)
    {
        transform.DOMove(target.position, 1);
        transform.LookAt(target.position);
    }
    private void ShootAEOSkill(Transform target)
    {
        transform.DOMove(target.position, 0);
    }
    private void Buff()
    {
        //Buff
    }
}
