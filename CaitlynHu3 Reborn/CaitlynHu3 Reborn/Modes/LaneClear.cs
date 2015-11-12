using System.Linq;
using EloBuddy.SDK;

using Settings = AddonTemplate.Config.Modes.LaneClear;

namespace AddonTemplate.Modes
{
    public sealed class LaneClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear);
        }

        public override void Execute()
        {
            var minion = EntityManager.MinionsAndMonsters.GetLaneMinions().FirstOrDefault(m => m.IsValidTarget(Q.Range));
            if (minion == null) return;

            if (Q.IsReady() && Settings.UseQ && minion.IsValidTarget(Q.Range))
            {
                Q.Cast(minion);
            }
        }
    }
}