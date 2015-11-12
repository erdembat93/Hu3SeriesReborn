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
            if (target.HasUndyingBuff() || target.IsZombie || target == null) return;

            if (Settings.UseQ && Q.IsReady() && target.IsValidTarget(Q.Range))
            {
                Q.Cast();
            }
        }
    }
}
