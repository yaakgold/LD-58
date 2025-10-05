using UnityEngine;

public class MeleeAttackController : MonoBehaviour
{
    public MeleeEnemyController mec;
    
    public void EndAttack()
    {
        mec.isAttacking = false;
    }
}
