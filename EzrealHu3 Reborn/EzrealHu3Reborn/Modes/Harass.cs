using EloBuddy;
using EloBuddy.SDK;
using Settings = EzrealHu3.Config.Modes.Harass;

namespace EzrealHu3.Modes
{
    public sealed class Harass : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass);
        }

        public override void Execute()
        {
            {
                var target = TargetSelector.GetTarget(Q.Range, DamageType.Physical);
                if (target == null || target.IsZombie || target.HasUndyingBuff()) return;

                if (Settings.UseQ && Q.IsReady() && target.IsValidTarget(Q.Range) && Settings.ManaHarass <= Player.Instance.ManaPercent)
                {
                    Q.Cast(target);
                }

                if (Settings.UseW && W.IsReady() && target.IsValidTarget(W.Range) && Settings.ManaHarass <= Player.Instance.ManaPercent)
                {
                    W.Cast(target);
                }
            }
        }
    }
}
