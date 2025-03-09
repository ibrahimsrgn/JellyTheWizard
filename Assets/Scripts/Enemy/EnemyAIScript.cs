using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAIScript : MonoBehaviour
{
    [HideInInspector] public NavMeshAgent Agent;
    [HideInInspector] public Transform PlayerRef;
    [Header("Navmesh Settings")]
    public float FieldOfViewAngle = 110f;
    public float DetectionRadius = 10f;
    public float PatrolRadius;
    public float NavMeshStoppingDistance;

    [Header("Enemy Settings")]
    public float AttackDamage;
    public float AttackSpeed;
    public float AttackRange;

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
}

/*private void RandomPatrolPoint()
    {
        Vector3 RandomDirection = Random.insideUnitSphere * PatrolRadius;
        RandomDirection += transform.position;

        NavMeshHit Hit;
        if (NavMesh.SamplePosition(RandomDirection, out Hit, PatrolRadius, NavMesh.AllAreas))
        {
            Agent.SetDestination(Hit.position);
        }
    }

    private bool CanSeePlayer()
    {
        Vector3 PlayerDirection = (PlayerRef.position - transform.position).normalized;
        float Angle = Vector3.Angle(PlayerDirection, transform.forward);

        if (Angle < FieldOfViewAngle / 2 && Vector3.Distance(transform.position, PlayerRef.position) < DetectionRadius)
        {
            return true;
        }
        return false;
    }*/