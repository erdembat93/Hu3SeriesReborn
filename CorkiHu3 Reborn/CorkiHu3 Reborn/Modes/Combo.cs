using EloBuddy;
using EloBuddy.SDK;

using Settings = AddonTemplate.Config.Modes.Combo;

namespace AddonTemplate.Modes
{
    public sealed class Combo : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
        }

        public override void Execute()
        {
            var target = TargetSelector.GetTarget(R.Range, DamageType.Physical);
            if (target == null || target.IsZombie || target.HasUndyingBuff()) return;

            if (Settings.UseQ && Q.IsReady() && target.IsValidTarget(Q.Range))
            {
                Q.Cast();
            }

            if (Settings.UseE && E.IsReady() && target.IsValidTarget(E.Range) && Player.Instance.IsFacing(target))
            {
                E.Cast();
            }

            if (Settings.UseR && R.IsReady() && target.IsValidTarget(R.Range) &&
                Player.Instance.ManaPercent <= Settings.ManaR)
            {
                R.Cast();
            }

            if (Settings.UseW && W.IsReady() && target.IsValidTarget(W.Range) && target.CountEnemiesInRange(600) <= 2 &&
                target.HealthPercent >= Player.Instance.HealthPercent && Player.Instance.IsFacing(target))
            {
                W.Cast(Player.Instance.Position.Extend(Game.CursorPos, W.Range).To3D());
            }
        }
    }
}
}
