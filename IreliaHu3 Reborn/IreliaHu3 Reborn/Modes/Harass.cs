using EloBuddy;
using EloBuddy.SDK;
using Settings = AddonTemplate.Config.Modes.Harass;

namespace AddonTemplate.Modes
{
    public sealed class Harass : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass);
        }

        public override void Execute()
        {
            var target = TargetSelector.GetTarget(Q.Range, DamageType.Physical);
            if (target == null || target.IsZombie || target.HasUndyingBuff()) return;

            if (Q.IsReady() && Settings.UseQ && target.IsValidTarget(Q.Range))
            {
                Q.Cast(target);
            }

            if (Program.CanW && Settings.UseW && (target.HealthPercent > Player.Instance.HealthPercent || target.IsInRange(Player.Instance, Player.Instance.AttackRange)))
            {
                W.Cast();
            }

            if (E.IsReady() && Settings.UseE && target.IsValidTarget(E.Range))
            {
                E.Cast(target);
            }
        }
    }
}
