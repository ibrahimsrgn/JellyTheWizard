using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static EnemyController;

public interface IEnemyState
{
    void EnterState(NavMeshAIScript Enemy);
    void UpdateState(NavMeshAIScript Enemy);
    void ExitState(NavMeshAIScript Enemy);
}

public class IdleState : IEnemyState
{
    public void EnterState(NavMeshAIScript Enemy)
    {
        Debug.Log("Idle: Bekliyor...");
    }

    public void UpdateState(NavMeshAIScript Enemy)
    {
        if (Vector3.Distance(Enemy.transform.position, Enemy.PlayerRef.position) < Enemy.DetectionRadius)
        {
            Enemy.SwitchState(new ChaseState());
        }
    }

    public void ExitState(NavMeshAIScript Enemy)
    {
        Debug.Log("Idle: Çýkýlýyor...");
    }
}

public class ChaseState : IEnemyState
{
    public void EnterState(NavMeshAIScript Enemy)
    {
        Debug.Log("Chase: Baþlýyor...");
    }

    public void UpdateState(NavMeshAIScript Enemy)
    {
        Enemy.Agent.SetDestination(Enemy.PlayerRef.position);
        if (Enemy.Agent.remainingDistance < 3f)
        {
            Enemy.SwitchState(new AttackState());
        }
    }

    public void ExitState(NavMeshAIScript Enemy)
    {
        Debug.Log("Chase: Bitiyor...");
    }
}

public class AttackState : IEnemyState
{
    public void EnterState(NavMeshAIScript Enemy)
    {
        Debug.Log("Attack: Baþlýyor...");
    }

    public void UpdateState(NavMeshAIScript Enemy)
    {
        Debug.Log("bam bam bam");
        if (Vector3.Distance(Enemy.transform.position, Enemy.PlayerRef.position) > 4f)
        {
            Enemy.SwitchState(new ChaseState());
        }
    }

    public void ExitState(NavMeshAIScript Enemy)
    {
        Debug.Log("Attack: Bitiyor...");
    }
}