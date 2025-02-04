using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField] private GameObject[] skills;
    [SerializeField] public Transform target;
    [SerializeField] private Transform firePoint;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            UseSkill();
        }
    }
    public void UseSkill()
    {
        GameObject skillPrefab = Instantiate(skills[0], firePoint.position, Quaternion.identity);
        Skill skill = skillPrefab.GetComponent<Skill>();
        skill.target = target;
        skill.UseSkill();
    }


}
