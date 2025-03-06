using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
public class NavMeshAIScript : MonoBehaviour
{

    public float PatrolRadius;
    public NavMeshAgent Agent;
    public Transform PlayerRef;
    public float FieldOfViewAngle = 110f;
    public float DetectionRadius = 10f;

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