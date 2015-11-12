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

            if (Settings.UseE && E.IsReady() && target.IsValidTarget(E.Range))
            {
                E.Cast(target);
            }

            if (Settings.UseQ && E.IsReady() && target.IsValidTarget(Q.Range) && target.HasBuff("tristanaecharge"))
            {
                Q.Cast();
            }
        }
    }
}
