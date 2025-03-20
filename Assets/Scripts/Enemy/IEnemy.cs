using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static EnemyAIScript;

public interface IEnemyState
{
    void EnterState(EnemyAIScript Enemy);
    void UpdateState(EnemyAIScript Enemy);
    void ExitState(EnemyAIScript Enemy);
}

/* ------------------------------------------------------------------------------------------------- */

public class IdleState : IEnemyState
{

    private float IdleStateTimer;
    public void EnterState(EnemyAIScript Enemy)
    {
        IdleStateTimer = 0;
    }

    public void UpdateState(EnemyAIScript Enemy)
    {
        IdleStateTimer += Time.deltaTime;
        if (Vector3.Distance(Enemy.transform.position, Enemy.PlayerRef.position) < Enemy.DetectionRadius)
        {
            Enemy.SwitchState(new ChaseState());
            return;
        }
        else if (IdleStateTimer <= 2)
        {
            IdleStateTimer = 0;
            Enemy.SwitchState(new PatrolState());
        }
    }

    public void ExitState(EnemyAIScript Enemy)
    {
        //Debug.Log("Idle: Çýkýlýyor...");
    }
}

/* ------------------------------------------------------------------------------------------------- */

public class ChaseState : IEnemyState
{
    private NavMeshHit Hit;
    public void EnterState(EnemyAIScript Enemy)
    {
        //Debug.Log("Chase: Baþlýyor...");
    }

    public void UpdateState(EnemyAIScript Enemy)
    {
        if (NavMesh.SamplePosition(Enemy.PlayerRef.position, out Hit, 5f, NavMesh.AllAreas) && Hit.mask == (1 << NavMesh.GetAreaFromName("Deneme")))
        {
            Enemy.Agent.SetDestination(Enemy.PlayerRef.position);
            if (Enemy.Agent.remainingDistance < Enemy.AttackRange)
            {
                Enemy.SwitchState(new AttackState());
            }
            return;
        }

        Enemy.SwitchState(new AlertState());
    }

    public void ExitState(EnemyAIScript Enemy)
    {
        //Debug.Log("Chase: Bitiyor...");
    }
}
/* ------------------------------------------------------------------------------------------------- */
public class AttackState : IEnemyState
{
    public void EnterState(EnemyAIScript Enemy)
    {
        //Debug.Log("Attack: Baþlýyor...");
    }

    public void UpdateState(EnemyAIScript Enemy)
    {
        if (Vector3.Distance(Enemy.transform.position, Enemy.PlayerRef.position) > Enemy.ChaseDistance)
        {
            Enemy.SwitchState(new ChaseState());
            return;
        }
        
        Enemy.EnemySkillAttack();
    }

    public void ExitState(EnemyAIScript Enemy)
    {
        //Debug.Log("Attack: Bitiyor...");
    }

}

public class PatrolState : IEnemyState
{
    private float WaitTime;
    private float WaitDuration;

    public void EnterState(EnemyAIScript Enemy)
    {
        WaitTime = 0f;
        WaitDuration = Random.Range(2f, 5f);
        Enemy.RandomPatrolPoint();
    }

    public void UpdateState(EnemyAIScript Enemy)
    {
        if (Vector3.Distance(Enemy.transform.position, Enemy.PlayerRef.position) < Enemy.DetectionRadius)
        {
            Enemy.SwitchState(new ChaseState());
            return;
        }

        if (!Enemy.Agent.pathPending && Enemy.Agent.remainingDistance <= 3.2f)
        {
            WaitTime += Time.deltaTime;
            if (WaitTime > WaitDuration)
            {
                WaitTime = 0f;
                WaitDuration = Random.Range(2f, 5f);
                Enemy.RandomPatrolPoint();
            }
        }
    }

    public void ExitState(EnemyAIScript Enemy)
    {
        //Debug.Log("Patrol: Bitiyor...");
    }
}

public class AlertState : IEnemyState
{
    public void EnterState(EnemyAIScript Enemy)
    {
        Debug.Log("Alert: Baþlýyor...");
    }

    public void UpdateState(EnemyAIScript Enemy)
    {
        Debug.Log("Alert: GOOO BRRRRRRRR");

        Vector3 direction = (Enemy.PlayerRef.position - Enemy.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        Enemy.transform.rotation = Quaternion.Slerp(Enemy.transform.rotation, lookRotation, Time.deltaTime * 5f);

        if (Enemy.CanSeePlayer() || Vector3.Distance(Enemy.transform.position, Enemy.PlayerRef.position) < Enemy.DetectionRadius)
        {
            Enemy.Agent.SetDestination(Enemy.PlayerRef.position);

            if (Enemy.Agent.remainingDistance < Enemy.AttackRange)
            {
                Enemy.SwitchState(new AttackState());
                return;
            }

            NavMeshHit hit;
            if (NavMesh.SamplePosition(Enemy.PlayerRef.position, out hit, 5f, NavMesh.AllAreas) && hit.mask == (1 << NavMesh.GetAreaFromName("Deneme")))
            {
                Enemy.SwitchState(new ChaseState());
                return;
            }
            return;
        }

        Enemy.SwitchState(new PatrolState());
    }

    public void ExitState(EnemyAIScript Enemy)
    {
        Debug.Log("Alert: Bitiyor...");
    }
}

public class RepositionState : IEnemyState
{
    public void EnterState(EnemyAIScript Enemy)
    {

    }

    public void UpdateState(EnemyAIScript Enemy)
    {

    }

    public void ExitState(EnemyAIScript Enemy)
    {

    }
}

/* ------------------------------------------------------------------------------------------------- */