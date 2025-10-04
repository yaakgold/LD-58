using UnityEngine;

namespace Abilities
{
    public class JumpAb : Ability
    {
        [SerializeField] private float jumpAdd;
        
        public override void UseAbility()
        {
            if (!player) return;
            isUsed = true;
            player.AddJumpForce(jumpAdd);
        }

        public override void RemoveAbility()
        {
            isUsed = false;
            player.AddJumpForce(-jumpAdd);
        }
    }
}
