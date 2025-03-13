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
    public float skillDuration = 5f; // Süre kontrolü için
    public float skillCooldown = 1f; // Süre kontrolü için
    public int damage;

    private bool isMoving = false;
    private bool canMove = false;
    private bool inCooldown = false;
    private Vector3 startPosition;
    private float t = 0f;
    private void Start()
    {
        Destroy(gameObject, skillDuration);
    }
    public void UseSkill(SkillType skillType)
    {
        if (target == null)
        {
            Debug.LogWarning("Shooting without target.");
        }
        type = skillType;
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
        canMove = true;
        t = 0f;
    }

    private void StartAOESkill(Transform target)
    {
        transform.position = target.position;
        canMove = false;
        t = 0f;
    }

    private void ApplyBuff()
    {
        canMove = false;
        transform.SetParent(target);
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
        if (canMove && target == null)
        {
            transform.position += transform.forward * (skillSpeed * Time.deltaTime);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (type == SkillType.Buff) return;
        if (collision.gameObject.GetComponent<HealthManager>() != null)
        {
            collision.gameObject.GetComponent<HealthManager>().ApplyDamage(damage);
        }

    }
    private void OnParticleCollision(GameObject other)
    {
        if (type == SkillType.Buff) return;
        if (other.GetComponent<HealthManager>() != null)
        {
            other.GetComponent<HealthManager>().ApplyDamage(damage);
        }
    }
}
