using EloBuddy;
using EloBuddy.SDK;

using Settings = TristanaHu3Reborn.Config.Modes.Misc;

namespace TristanaHu3Reborn.Modes
{
    public sealed class Flee : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee);
        }

        public override void Execute()
        {
            var target = TargetSelector.GetTarget(R.Range, DamageType.Physical);
            if (target == null || target.IsZombie || target.HasUndyingBuff()) return;

            if (Settings.UseRFlee && Player.Instance.HealthPercent <= 10 && Player.Instance.HealthPercent < target.HealthPercent)
            {
                R.Cast(target);
            }

            if (Settings.UseWFlee && W.IsReady())
            {
                W.Cast(Player.Instance.Position.Extend(Game.CursorPos, W.Range).To3D());
            }
        }
    }
}
