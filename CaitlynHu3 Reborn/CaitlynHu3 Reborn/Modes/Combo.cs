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

            if (Settings.UseE && E.IsReady() && target.IsInRange(Player.Instance, 390) &&
                target.IsValidTarget(E.Range) && target.IsFacing(Player.Instance))
            {
                E.Cast(target);
            }

            if (Settings.UseQ && Q.IsReady() && target.IsValidTarget(Q.Range) &&
                (!target.IsInRange(Player.Instance, Player.Instance.AttackRange) ||
                 target.HasBuffOfType(BuffType.Slow)))
            {
                Q.Cast(target);
            }

            if (W.IsReady() && target.IsValidTarget(W.Range))
            {
                W.Cast(target);
            }

            if (Player.Instance.CanAttack)
            {
                var targetAutoAttack = TargetSelector.GetTarget(Player.Instance.AttackRange * 2, DamageType.Physical);
                if (targetAutoAttack == null) return;

                if (targetAutoAttack.HasBuff("caitlynyordletrapinternal"))
                {
                    Player.IssueOrder(GameObjectOrder.AttackUnit, targetAutoAttack);
                }
            }
        }
    }
}