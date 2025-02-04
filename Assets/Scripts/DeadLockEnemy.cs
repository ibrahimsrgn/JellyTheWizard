using System.Linq;
using UnityEngine;

public class DeadLockEnemy : MonoBehaviour
{
    private float DistanceToEnemy;
    public static DeadLockEnemy Instance { get; private set; }
    private Ray DeadLockRay;
    
    private void Awake()
    {
        Instance = this;
    }

    public void DeadLocker()
    {
        Vector3 center = transform.position;
        float radius = 5f;
        LayerMask layerMask = LayerMask.GetMask("Enemy");

        Collider[] hitColliders = Physics.OverlapSphere(center, radius, layerMask);
        DistanceToEnemy = 25f;
        foreach (Collider col in hitColliders)
        {
            DeadLockRay = new Ray(transform.position, col.transform.position - transform.position);
            if (Physics.Raycast(DeadLockRay, out RaycastHit hit, DistanceToEnemy))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    DistanceToEnemy = hit.distance;
                }
            }
        }
        Debug.Log("Distance to Enemy: " + DistanceToEnemy);
    }
}
