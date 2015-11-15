using EloBuddy;
using EloBuddy.SDK;

using Settings = CaitlynHu3Reborn.Config.Modes.Combo;

namespace CaitlynHu3Reborn.Modes
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
            if (target == null) return;

            if (Settings.UseQ && Q.IsReady() && !target.IsInRange(Player.Instance, Player.Instance.AttackRange) &&
                target.IsValidTarget(Q.Range))
            {
                Q.Cast(target);
            }

            if (Settings.UseE && E.IsReady() && target.IsInRange(Player.Instance, 350) &&
                target.IsValidTarget(E.Range))
            {
                E.Cast(target);
            }
        }
    }
}