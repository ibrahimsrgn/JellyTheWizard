using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAIScript : MonoBehaviour
{
    [HideInInspector] public NavMeshAgent Agent;
    public Transform PlayerRef;
    [Header("Navmesh Settings")]
    public float FieldOfViewAngle = 110f;
    public float DetectionRadius = 10f;
    public float PatrolRadius;
    public float ChaseDistance;

    [Header("Enemy Settings")]
    public int AttackDamage;
    public float AttackSpeed;
    public float AttackRange;
    private bool EnemyCanAttack = true;
    public GameObject EnemySkill;

    private IEnemyState CurrentState;

    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        PlayerRef = GameObject.Find("Jelly The Wizard").transform;
        CurrentState = new IdleState();
        CurrentState.EnterState(this);
    }

    private void Update()
    {
        CurrentState.UpdateState(this);
    }

    public void SwitchState(IEnemyState NewState)
    {
        CurrentState.ExitState(this);
        CurrentState = NewState;
        CurrentState.EnterState(this);
    }

    public void EnemySkillAttack()
    {
        if (EnemyCanAttack)
        {
            EnemyCanAttack = false;
            StartCoroutine(AttackToPlayer());
        }
    }

    private IEnumerator AttackToPlayer()
    {
        yield return new WaitForSeconds(AttackSpeed);
        GameObject SkillPrefab = Instantiate(EnemySkill, transform.position, Quaternion.identity);
        Skill Skill = SkillPrefab.GetComponent<Skill>();
        Skill.damage = AttackDamage;
        Skill.target = PlayerRef;

        Skill.UseSkill(Skill.SkillType.SingleTarget);
        EnemyCanAttack = true;
    }

    public void RandomPatrolPoint()
    {
        Vector3 RandomDirection = Random.insideUnitSphere * PatrolRadius;
        RandomDirection += transform.position;

        NavMeshHit Hit;
        if (NavMesh.SamplePosition(RandomDirection, out Hit, PatrolRadius, NavMesh.AllAreas))
        {
            Agent.SetDestination(Hit.position);
        }
    }

    public bool CanSeePlayer()
    {
        Vector3 PlayerDirection = (PlayerRef.position - transform.position).normalized;
        float Angle = Vector3.Angle(PlayerDirection, transform.forward);

        if (Angle < FieldOfViewAngle / 2 && Vector3.Distance(transform.position, PlayerRef.position) < DetectionRadius)
        {
            return true;
        }
        return false;
    }
}
