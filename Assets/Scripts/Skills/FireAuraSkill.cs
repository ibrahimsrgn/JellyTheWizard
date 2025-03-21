using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAuraSkill : Skill
{
    public float radius = 5;
    protected override void ApplyBuff()
    {
        base.ApplyBuff();
        StartCoroutine(CastAreaDmg());
    }
    IEnumerator CastAreaDmg()
    {
        float timePassed = 0;
        while (timePassed < skillDuration)
        {

            Collider[] enemies = Physics.OverlapSphere(transform.position, radius);
            foreach (Collider enemy in enemies)
            {
                enemy.gameObject.GetComponent<HealthManager>()?.ApplyDamage(damage);
            }
        }
        timePassed += skillSpeed;
        yield return new WaitForSeconds(skillSpeed);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, radius);
    }

}
