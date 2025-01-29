using System.Linq;
using UnityEngine;

public class DeadLockEnemy : MonoBehaviour
{
    public static DeadLockEnemy Instance { get; private set; }
    [SerializeField]private GameObject DeadLockDetecter;

    private void Awake()
    {
        Instance = this;
    }

    public void DeadLockSpawner()
    {
        Instantiate(DeadLockDetecter, Vector3.zero, Quaternion.identity);
    }
}
