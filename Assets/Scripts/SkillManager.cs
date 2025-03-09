using UnityEngine;

public enum SkillTree
{
    fire,//0-1-2:+0
    air,//3-4-5:+3
    telekinesis//6-7-8:+6
}
public class SkillManager : MonoBehaviour
{
    [SerializeField] private GameObject[] skills;
    [SerializeField] public Transform target;
    [SerializeField] private Transform firePoint;
    private int skillIndex = 0;
    public SkillTree skillTree;
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
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            ChangeSkillTree(SkillTree.fire);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            ChangeSkillTree(SkillTree.air);

        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            ChangeSkillTree(SkillTree.telekinesis);
        }
    }
    public void UseSkill(int skillIndex)
    {
        if(DeadLockEnemy.Instance.selectedEnemy != null)
        target = DeadLockEnemy.Instance.selectedEnemy;
        GameObject skillPrefab = Instantiate(skills[skillIndex], firePoint.position, Quaternion.identity);
        Skill skill = skillPrefab.GetComponent<Skill>();
        skill.target = target;
        skill.UseSkill();
        Debug.Log(skill);
    }

    public void ChangeSkillTree(SkillTree _skillTree)
    {
        skillTree = _skillTree;
        switch (skillTree)
        {
            case SkillTree.fire:
                skillIndex = 0;
                break;
                case SkillTree.air:
                skillIndex = 3;
                break;
                case SkillTree.telekinesis:
                skillIndex = 6;
                break;
        }
        Debug.Log(skillTree);
    }

}
