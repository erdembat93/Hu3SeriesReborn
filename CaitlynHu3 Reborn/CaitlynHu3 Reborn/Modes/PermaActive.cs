using EloBuddy;
using EloBuddy.SDK;

using Settings = AddonTemplate.Config.Modes.Misc;

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
            var target = TargetSelector.GetTarget(Q.Range, DamageType.Physical);
            if (target.HasUndyingBuff() || target.IsZombie || target == null) return;

            if (Settings.UseQCC)
            {
                if (target.HasBuffOfType(BuffType.Stun) || target.HasBuffOfType(BuffType.Snare) ||
                    target.HasBuffOfType(BuffType.Knockup))
                {
                    Q.Cast(target);
                }
            }

            if (Settings.UseWCC)
            {
                if (target.HasBuffOfType(BuffType.Stun) || target.HasBuffOfType(BuffType.Snare) ||
                    target.HasBuffOfType(BuffType.Knockup))
                {
                    W.Cast(target);
                }
            }
        }
    }
}
