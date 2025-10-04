using UnityEngine;

namespace Abilities
{
    public class SpeedAb : Ability
    {
        [SerializeField] private float speedAdd;
        
        public override void UseAbility()
        {
            if (!player) return;
            isUsed = true;
            player.AddSpeed(speedAdd);
        }

        public override void RemoveAbility()
        {
            isUsed = false;
            player.AddSpeed(-speedAdd);
        }
    }
}
