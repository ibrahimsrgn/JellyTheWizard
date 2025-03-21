using System;
using System.Collections;
using System.Linq;
using Unity.Cinemachine;
using UnityEngine;

public class DeadLockEnemy : MonoBehaviour
{
    public Transform selectedEnemy;
    public CinemachineCamera LockOnCamera;
    private float DistanceToEnemy;
    public float Radius = 5f;
    public static DeadLockEnemy Instance { get; private set; }
    private Ray DeadLockRay;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Instance = this;
    }

    public void DeadLocker()
    {
        Vector3 center = transform.position;
        LayerMask layerMask = LayerMask.GetMask("Enemy");

        Collider[] hitColliders = Physics.OverlapSphere(center, Radius, layerMask);
        if (hitColliders.Length == 0)
        {
            DeadUnlocker();
            return;
        }
        DistanceToEnemy = Radius;
        foreach (Collider col in hitColliders)
        {
            DeadLockRay = new Ray(transform.position, col.transform.position - transform.position);
            if (Physics.Raycast(DeadLockRay, out RaycastHit hit, DistanceToEnemy))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    selectedEnemy = hit.transform;
                    DistanceToEnemy = hit.distance;
                    LockOnCamera.LookAt = hit.transform;
                    LockOnCamera.gameObject.SetActive(true);
                    StartCoroutine(DeadLockChecker(col));
                }
                //else if (LockOnCamera.LookAt == hit.transform) DeadNextlocker(hit.distance);
            }
        }
    }

    private void DeadNextlocker(float MinDistance)
    {
        DistanceToEnemy = Radius;
        if (Physics.Raycast(DeadLockRay, out RaycastHit hit, DistanceToEnemy) && DistanceToEnemy > MinDistance)
        {
            if (hit.collider.CompareTag("Enemy") && LockOnCamera.LookAt != hit.transform)
            {
                selectedEnemy = hit.transform;
                DistanceToEnemy = hit.distance;
                LockOnCamera.LookAt = hit.transform;
                LockOnCamera.gameObject.SetActive(true);
            }
        }
    }

    public void DeadUnlocker()
    {
        LockOnCamera.gameObject.SetActive(false);
        LockOnCamera.LookAt = null;
    }

    public IEnumerator DeadLockChecker(Collider col)
    {
        while (LockOnCamera.LookAt != null)
        {
            DeadLockRay = new Ray(transform.position, col.transform.position - transform.position);
            if (Physics.Raycast(DeadLockRay, out RaycastHit hit, 25))
            {
                if (!hit.collider.CompareTag("Enemy"))
                {
                    DeadUnlocker();
                }
            }
            else
            {
                DeadUnlocker();
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
