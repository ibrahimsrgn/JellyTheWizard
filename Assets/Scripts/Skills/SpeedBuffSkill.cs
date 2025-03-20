using System.Collections;
using UnityEngine;

public class SpeedBuffSkill : Skill
{
    protected override void ApplyBuff()
    {
        base.ApplyBuff();
        Debug.Log("1");
    // PlayerMovements.Instance.PlayerSpeed += damage;
        RemoveBuff();
    }
    IEnumerator RemoveBuff()
    {
        yield return new WaitForSeconds(skillDuration);
      //  PlayerMovements.Instance.PlayerSpeed -= damage;
    }
}
