using System.Collections;
using UnityEngine;

namespace Abilities
{
    public class MagicMissileAb : Ability
    {
        [SerializeField] private GameObject missilePrefab;
        [SerializeField] private float timeBetweenShots;
        [SerializeField] private LayerMask layerMask;

        private bool _isCanceled;
        
        public override void UseAbility()
        {
            _isCanceled = false;
            var hit = Physics2D.CircleCast(transform.position, 200, Vector2.right * 200, 200, layerMask);

            if (!hit.collider)
            {
                StartCoroutine(TimerBetweenShots());
                return;
            }
        
            var target = hit.collider.gameObject;
            
            var go = Instantiate(missilePrefab, transform.position, Quaternion.identity);

            if (go.TryGetComponent(out Missile missile))
            {
                missile.target = target;
            }

            StartCoroutine(TimerBetweenShots());
        }

        public override void RemoveAbility()
        {
            _isCanceled = true;
        }

        private IEnumerator TimerBetweenShots()
        {
            yield return new WaitForSeconds(timeBetweenShots);

            if (_isCanceled) yield break;
            UseAbility();
        }
    }
}
