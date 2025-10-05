using System;
using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private static readonly int AttackSpawnEnemies = Animator.StringToHash("SpawnEnemies");
    
    [SerializeField] private Transform leftPlatform, rightPlatform, leftEdge, rightEdge;
    [SerializeField] private float moveSpeed, spawnForce;
    [SerializeField] private int enemyCount;
    [SerializeField] private GameObject enemyPref;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Animator anim;
    
    private bool _isFlying;

    private void Start()
    {
        anim.SetTrigger(AttackSpawnEnemies);
    }

    public void DoSpawnEnemyAttack()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        var enemies = enemyCount;

        while (enemies > 0)
        {
            enemies--;
            
            var go = Instantiate(enemyPref,  spawnPoint.position, spawnPoint.rotation);

            if (go.TryGetComponent(out EnemyController ec))
            {
                ec.GetWaypoint(0).transform.position = leftEdge.position;
                ec.GetWaypoint(0).transform.rotation = leftEdge.rotation;
                ec.GetWaypoint(1).transform.position = rightEdge.position;
                ec.GetWaypoint(1).transform.rotation = rightEdge.rotation;
            }
            
            yield return new WaitForSeconds(.2f);
        }
    }
}
