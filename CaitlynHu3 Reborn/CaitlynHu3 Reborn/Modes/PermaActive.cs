using System;
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

        private int _lastW;

        public override void Execute()
        {
            var targetAutoAttack = TargetSelector.GetTarget(Player.Instance.AttackRange * 2, DamageType.Physical);
            if (targetAutoAttack != null)
            {
                if (targetAutoAttack.HasBuff("caitlynyordletrapinternal") && Settings.UseAA)
                {
                    Player.IssueOrder(GameObjectOrder.AttackUnit, targetAutoAttack);
                }
            }

            if (Settings.UseQCC && Q.IsReady())
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

            if (Settings.UseWCC && _lastW + 1500 > Environment.TickCount && W.IsReady())
            {
                var target = TargetSelector.GetTarget(W.Range, DamageType.Physical);
                if (target != null)
                {
                    if (target.HasBuffOfType(BuffType.Stun) || target.HasBuffOfType(BuffType.Snare) ||
                        target.HasBuffOfType(BuffType.Knockup))
                    {
                        W.Cast(target);
                        _lastW = Environment.TickCount;
                    }
                }
            }
        }
    }
}
