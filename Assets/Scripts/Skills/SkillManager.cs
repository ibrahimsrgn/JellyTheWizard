using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillTree
{
    fire,//0-1-2:+0
    air,//3-4-5:+3
    telekinesis//6-7-8:+6
}
public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance;
    [SerializeField] private GameObject[] skills;
    [SerializeField] public Transform target;
    [SerializeField] private Transform firePoint;
    private int skillIndex = 0;
    public SkillTree skillTree;
    public Skill.SkillType skillType;

    private Dictionary<int, float> skillCooldowns = new();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        for(int i = 0; i < skills.Length; i++)
        {
            skillCooldowns.Add(i, 0f);
        }
    }
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            UseSkill(0 + skillIndex);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            UseSkill(1 + skillIndex);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            UseSkill(2 + skillIndex);
        }
    }
    public void UseSkill(int skillIndex)
    {
        if (skillCooldowns[skillIndex]>0f)
        {
            Debug.Log("Skill is on cooldown!");
            return;
        }
        Skill _skill= skills[skillIndex].GetComponent<Skill>();
        GameObject skillPrefab=null;

        if (_skill.type==Skill.SkillType.Buff)
        {
            Vector3 pos=transform.position;
            pos.z -= 0.7f;
             skillPrefab = Instantiate(skills[skillIndex], pos, transform.rotation);
            target = transform;
            skillType = Skill.SkillType.Buff;
        }
        else if (_skill.type == Skill.SkillType.SingleTarget)
        {
                target = DeadLockEnemy.Instance.selectedEnemy;
             skillPrefab = Instantiate(skills[skillIndex], firePoint.position, transform.rotation);
            skillType = Skill.SkillType.SingleTarget;
        }
        else if (_skill.type == Skill.SkillType.AOE)
        {
                target = DeadLockEnemy.Instance.selectedEnemy;
             skillPrefab = Instantiate(skills[skillIndex], firePoint.position, transform.rotation);
            skillType= Skill.SkillType.AOE;
        }
        Skill skill= skillPrefab?.GetComponent<Skill>();
        skill.target = target;
        skill.UseSkill(skillType);
        skillCooldowns[skillIndex]=skill.skillCooldown;
        StartCoroutine(Cooldown(_skill.skillCooldown, skillIndex));
    }

    public void ChangeSkillTree(int skillInt)
    {
        switch (skillInt)
        {
            case 1:
                skillIndex = 0;
                skillTree = SkillTree.fire;
                break;
            case 2:
                skillIndex = 3;
                skillTree = SkillTree.air;
                break;
            case 3:
                skillIndex = 6;
                skillTree = SkillTree.telekinesis;
                break;
        }
    }

    private IEnumerator Cooldown(float timer,int skillIndex)
    {
        while (skillCooldowns[skillIndex] > 0)
        {
            skillCooldowns[skillIndex] -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        skillCooldowns[skillIndex] = 0;
    }

}
