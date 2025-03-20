using System.Collections;
using UnityEngine;

public class SpeedBuffSkill : Skill
{
    protected override void ApplyBuff()
    {
        base.ApplyBuff();
     PlayerMovements.Instance.PlayerSpeed += damage;
        RemoveBuff();
    }
    IEnumerator RemoveBuff()
    {
        yield return new WaitForSeconds(skillDuration);
        PlayerMovements.Instance.PlayerSpeed -= damage;
    }
}
