using System;
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
            var target = TargetSelector.GetTarget(Q.Range, DamageType.Physical);
            if (target.HasUndyingBuff() || target.IsZombie || target == null) return;

            if (Settings.UseQ && Q.IsReady() && !target.IsInRange(Player.Instance, Player.Instance.AttackRange) &&
                target.IsValidTarget(Q.Range))
            {
                Q.Cast();
            }
        }
    }
}
