using UnityEngine;

public class EnemyGFXController : MonoBehaviour
{
    [SerializeField] private MeleeEnemyController ec;

    public void SetECIsAttackingFalse()
    {
        ec.isAttacking = false;
    }
}
