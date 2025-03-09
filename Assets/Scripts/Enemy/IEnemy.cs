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
    public void EnterState(EnemyAIScript Enemy)
    {
        Debug.Log("Idle: Bekliyor...");
    }

    public void UpdateState(EnemyAIScript Enemy)
    {
        if (Vector3.Distance(Enemy.transform.position, Enemy.PlayerRef.position) < Enemy.DetectionRadius)
        {
            Enemy.SwitchState(new ChaseState());
        }
    }

    public void ExitState(EnemyAIScript Enemy)
    {
        Debug.Log("Idle: Çýkýlýyor...");
    }
}

/* ------------------------------------------------------------------------------------------------- */

public class ChaseState : IEnemyState
{
    public void EnterState(EnemyAIScript Enemy)
    {
        Debug.Log("Chase: Baþlýyor...");
    }

    public void UpdateState(EnemyAIScript Enemy)
    {
        Enemy.Agent.SetDestination(Enemy.PlayerRef.position);
        if (Enemy.Agent.remainingDistance < 3f)
        {
            Enemy.SwitchState(new AttackState());
        }
    }

    public void ExitState(EnemyAIScript Enemy)
    {
        Debug.Log("Chase: Bitiyor...");
    }
}
/* ------------------------------------------------------------------------------------------------- */
public class AttackState : IEnemyState
{
    public void EnterState(EnemyAIScript Enemy)
    {
        Debug.Log("Attack: Baþlýyor...");
    }

    public void UpdateState(EnemyAIScript Enemy)
    {
        Debug.Log("bam bam bam");
        if (Vector3.Distance(Enemy.transform.position, Enemy.PlayerRef.position) > Enemy.NavMeshStoppingDistance)
        {
            Enemy.SwitchState(new ChaseState());
        }
        else
        {
            
        }
    }

    public void ExitState(EnemyAIScript Enemy)
    {
        Debug.Log("Attack: Bitiyor...");
    }

}

/* ------------------------------------------------------------------------------------------------- */