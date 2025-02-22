using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAIScript : MonoBehaviour
{
    public NavMeshAgent Agent;
    public Transform Target;

    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Target = GameObject.Find("Jelly The Wizard").transform;
    }

    private void Update()
    {
        if (Vector3.Distance(Agent.transform.position, Target.transform.position) > 1)
        {
            Agent.SetDestination(Target.position);
        }
    }
}
