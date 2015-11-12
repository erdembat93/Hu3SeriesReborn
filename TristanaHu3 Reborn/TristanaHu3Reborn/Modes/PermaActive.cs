using EloBuddy;
using EloBuddy.SDK;

namespace AddonTemplate.Modes
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

            if (R.IsReady())
            {
                var stacks = target.GetBuffCount("tristanaecharge");
                if (stacks >= 1)
                {
                    var erdamage = (SpellDamage.GetRealDamage(SpellSlot.E, target)*((0.30*stacks) + 1) +
                                    SpellDamage.GetRealDamage(SpellSlot.R, target));

                    if (target.Health <= erdamage)
                    {
                        R.Cast(target);
                    }
                }
            }
        }
    }
}
