using System;
using EloBuddy;
using EloBuddy.SDK;

// Using the config like this makes your life easier, trust me
using Settings = AddonTemplate.Config.Modes.Combo;

namespace AddonTemplate.Modes
{
    public sealed class Combo : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
        }

        private static int _lastRCast;

        public override void Execute()
        {
            var target = TargetSelector.GetTarget(Q.Range, DamageType.Physical);
            if (target == null || target.IsZombie || target.HasUndyingBuff()) return;

            if (Q.IsReady() && Settings.UseQ && target.IsValidTarget(Q.Range) && !target.IsInRange(Player.Instance, Player.Instance.AttackRange))
            {
                Q.Cast(target);
            }

            if (Program.CanW && Settings.UseW)
            {
                W.Cast();
            }

            if (E.IsReady() && Settings.UseE && target.IsValidTarget(E.Range) && (target.HealthPercent > Player.Instance.HealthPercent || target.IsInRange(Player.Instance, Player.Instance.AttackRange)))
            {
                E.Cast(target);
            }

            if (R.IsReady() && Settings.UseR && target.IsValidTarget(R.Range) && target.HealthPercent <= 50 && _lastRCast < Environment.TickCount + Settings.DelayR)
            {
                R.Cast(target);
                _lastRCast = Environment.TickCount;
            }
        }
    }
}
