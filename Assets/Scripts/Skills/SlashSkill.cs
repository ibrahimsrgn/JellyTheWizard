using Unity.VisualScripting;
using UnityEngine;

public class SlashSkill : Skill
{
    protected override void StartAOESkill(Transform targett)
    {
        startPosition = transform.position;
        isMoving = true;
        canMove = true;
        t = 0f;
        target = targett;
    }
    private void OnTriggerEnter(Collider other)
    {
        Vector3 hitPosition = transform.position;
        Vector3 hitNormal = Vector3.up;

        //Do damage & knockback
        if (other.gameObject.GetComponent<HealthManager>() != null)
        {
            other.gameObject.GetComponent<HealthManager>().ApplyDamage(damage);
            bool destroy = false;
            if(target!=null&&other.gameObject==target.gameObject)
            {
                destroy = true;
            }
            GetComponent<ProjectileEffectsSpawner>().RemoteCollisionTrigger(hitPosition, hitNormal, destroy);
        }
        else
        {
            Debug.Log("1");
            GetComponent<ProjectileEffectsSpawner>().RemoteCollisionTrigger(hitPosition, hitNormal, true);
        }
    }
}
