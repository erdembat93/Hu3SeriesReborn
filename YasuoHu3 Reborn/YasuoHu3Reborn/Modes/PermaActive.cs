using EloBuddy;
using EloBuddy.SDK;

using Settings = YasuoHu3Reborn.Config.Modes.Misc;

namespace YasuoHu3Reborn.Modes
{
    public sealed class PermaActive : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return true;
        }

        public override void Execute()
        {
            var target = TargetSelector.GetTarget(480, DamageType.Physical);
            if (target == null) return;

            if (Settings.AutoQ && Q.IsReady() && !Player.Instance.HasQ3())
            {
                Q.Cast(target);
            }
        }
    }
}
