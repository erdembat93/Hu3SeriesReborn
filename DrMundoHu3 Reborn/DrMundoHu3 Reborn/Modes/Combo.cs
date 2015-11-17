using EloBuddy;
using EloBuddy.SDK;

using Settings = DrMundoHu3Reborn.Config.Modes.Combo;

namespace DrMundoHu3Reborn.Modes
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

            if (target != null)
            {
                if (Settings.UseQ && Q.IsReady() && target.IsValidTarget(Q.Range))
                {
                    {
                        Q.Cast(target);
                    }
                }
                if (Settings.UseW && W.IsReady() && target.IsValidTarget(W.Range) && !Player.Instance.HasBuff("burningagony"))
                {
                    {
                        W.Cast();
                    }
                }
                if (Settings.UseE && E.IsReady() && target.IsValidTarget(E.Range))
                {
                    {
                        E.Cast();
                    }
                }
            }
        }
    }
}
