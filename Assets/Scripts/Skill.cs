using DG.Tweening;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public enum SkillType
    {
        SingleTarget,
        AOE, // AEO yerine AOE düzeltilmiş
        Buff
    }

    public SkillType type;
    public Transform target;
    public float skillSpeed = 5f; // Hız kontrolü için

    private bool isMoving = false;
    private Vector3 startPosition;
    private float t = 0f;

    public void UseSkill()
    {
        if (target == null)
        {
            Debug.LogWarning("Target is null! Cannot use skill.");
            return;
        }

        switch (type)
        {
            case SkillType.SingleTarget:
                StartSingleTargetSkill(target);
                break;
            case SkillType.AOE:
                StartAOESkill(target);
                break;
            case SkillType.Buff:
                ApplyBuff();
                break;
        }
    }

    private void StartSingleTargetSkill(Transform target)
    {
        startPosition = transform.position;
        isMoving = true;
        t = 0f;
    }

    private void StartAOESkill(Transform target)
    {
        startPosition = target.position;
        isMoving = true;
        t = 0f;
    }

    private void ApplyBuff()
    {
        // TODO BUFF
    }

    private void Update()
    {
        if (isMoving && target != null)
        {
            t += Time.deltaTime * skillSpeed;
            transform.position = Vector3.Lerp(startPosition, target.position, t);
            transform.LookAt(target.position);

            if (t >= 1f)
            {
                isMoving = false;
            }
        }
    }
}
