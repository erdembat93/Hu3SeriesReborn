using EloBuddy;
using EloBuddy.SDK;

using Settings = TristanaHu3Reborn.Config.Modes.Combo;

namespace TristanaHu3Reborn.Modes
{
    public sealed class PermaActive : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return true;
        }

        public override void Execute()
        {
            var target = TargetSelector.GetTarget(R.Range, DamageType.Physical);
            if (target == null || target.IsZombie || target.HasUndyingBuff()) return;

            if (R.IsReady() && Settings.UseR)
            {
                var stacks = target.GetBuffCount("tristanaecharge");
                if (stacks >= 1)
                {
                    if (target.Health <= (SpellDamage.GetRealDamage(SpellSlot.E, target)*((0.30*stacks) + 1) +
                                          SpellDamage.GetRealDamage(SpellSlot.R, target)))
                    {
                        R.Cast(target);
                    }
                }
            }

            if (R.IsReady() && Settings.UseRTower)
            {
                if (target.GetAfterRPos().AllyTower())
                {
                    R.Cast(target);
                }
            }
        }
    }
}
