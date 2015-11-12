using EloBuddy;
using EloBuddy.SDK;

using Settings = CaitlynHu3Reborn.Config.Modes.Misc;

namespace CaitlynHu3Reborn.Modes
{
    public sealed class PermaActive : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return true;
        }

        public override void Execute()
        {
            if (Settings.UseQCC)
            {
                var target = TargetSelector.GetTarget(Q.Range, DamageType.Physical);

                if (target != null)
                {
                    if (target.HasBuffOfType(BuffType.Stun) || target.HasBuffOfType(BuffType.Snare) ||
                        target.HasBuffOfType(BuffType.Knockup))
                    {
                        Q.Cast(target);
                    }
                }
            }

            if (Settings.UseWCC)
            {
                var target = TargetSelector.GetTarget(W.Range, DamageType.Physical);
                if (target != null)
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
}
