using Unity.VisualScripting;
using UnityEngine;

public class SlashSkill : Skill
{
    protected override void StartAOESkill(Transform target)
    {
       startPosition = transform.position;
        isMoving = true;
        canMove = true;
        t = 0f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != target&&other.gameObject!=SkillManager.Instance.gameObject)
        {
            //Do damage & knockback
            if (other.gameObject.GetComponent<HealthManager>() != null)
            {
                other.gameObject.GetComponent<HealthManager>().ApplyDamage(damage);
            }
        }
        else if (other.gameObject == target)
        {
            Debug.Log("1");
            //Fire remoteCollisionTrigger & destroy
            Vector3 hitPosition = transform.position; // Çarpma noktası: Merminin konumu
            Vector3 hitNormal = Vector3.up; // Örnek bir yön belirle (İstersen değiştir)

            GetComponent<ProjectileEffectsSpawner>().RemoteCollisionTrigger(hitPosition, hitNormal);
        }
    }
}
