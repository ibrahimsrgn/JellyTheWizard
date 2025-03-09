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
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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
        Skill _skill= skills[skillIndex].GetComponent<Skill>();
        Debug.Log(_skill.type);
        if (_skill.type==Skill.SkillType.Buff)
        {
            GameObject skillPrefab = Instantiate(skills[skillIndex], firePoint.position, Quaternion.identity);
            Skill skill = skillPrefab.GetComponent<Skill>();
            skill.target = transform;
            skill.UseSkill(Skill.SkillType.Buff);
            Debug.Log(skill);
            Destroy(skillPrefab, skill.skillDuration);

        }
        else if (_skill.type == Skill.SkillType.SingleTarget)
        {
            if (DeadLockEnemy.Instance.selectedEnemy != null)
                target = DeadLockEnemy.Instance.selectedEnemy;
            GameObject skillPrefab = Instantiate(skills[skillIndex], firePoint.position, Quaternion.identity);
            Skill skill = skillPrefab.GetComponent<Skill>();
            skill.target = target;
            skill.UseSkill(Skill.SkillType.SingleTarget);
            Debug.Log(skill);
            Destroy(skillPrefab, skill.skillDuration);
        }
        else if (_skill.type == Skill.SkillType.AOE)
        {
            if (DeadLockEnemy.Instance.selectedEnemy != null)
                target = DeadLockEnemy.Instance.selectedEnemy;
            GameObject skillPrefab = Instantiate(skills[skillIndex], firePoint.position, Quaternion.identity);
            Skill skill = skillPrefab.GetComponent<Skill>();
            skill.target = target;
            skill.UseSkill(Skill.SkillType.AOE);
            Debug.Log(skill);
            Destroy(skillPrefab, skill.skillDuration);
        }
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
        Debug.Log(skillTree);
    }

}
