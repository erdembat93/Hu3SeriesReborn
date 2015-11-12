using EloBuddy;
using EloBuddy.SDK;

// Using the config like this makes your life easier, trust me
using Settings = AddonTemplate.Config.Modes.Combo;
using Configs = AddonTemplate.Config.Modes.Misc;

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
            if (target == null || target.IsZombie || target.HasUndyingBuff()) return;

            Orbwalker.ForcedTarget = null;

            if (Settings.UseE && E.IsReady() && target.IsValidTarget(E.Range) && !Configs.Enemies)
            {
                E.Cast(target);
            }

            if (Settings.UseQ && E.IsReady() && target.IsValidTarget(Player.Instance.AttackRange))
            {
                Q.Cast();
            }
        }
    }
}
