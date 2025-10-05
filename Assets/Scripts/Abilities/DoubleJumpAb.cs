namespace Abilities
{
    public class DoubleJumpAb : Ability
    {
        public override void UseAbility()
        {
            if (!player) return;
            isUsed = true;
            player.AddJump();
        }

        public override void RemoveAbility()
        {
            isUsed = false;
            player.RemoveJump();
        }
    }
}
