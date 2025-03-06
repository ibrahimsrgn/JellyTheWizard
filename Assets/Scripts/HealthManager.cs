using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private float Health = 100f;

    public void ApplyDamage(float damage)
    {
        Health -= damage;
        Debug.Log("Health: " + Health);
        if (Health <= 0)
        {
            Destroy(gameObject);
            if (DeadLockEnemy.Instance.selectedEnemy == gameObject.transform)
            {
                DeadLockEnemy.Instance.LockOnCamera.gameObject.SetActive(false);
            }
        }
    }
}
