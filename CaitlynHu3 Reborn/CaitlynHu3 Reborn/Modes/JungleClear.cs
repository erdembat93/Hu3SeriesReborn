using System.Linq;
using EloBuddy.SDK;
using Settings = AddonTemplate.Config.Modes.LaneClear;

namespace AddonTemplate.Modes
{
    public sealed class JungleClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear);
        }

        public override void Execute()
        {
            var minion =
                EntityManager.MinionsAndMonsters.GetJungleMonsters().FirstOrDefault(m => m.IsValidTarget(Q.Range));
            if (minion == null) return;

            if (Settings.UseQ && Q.IsReady() && minion.IsValidTarget(Q.Range))
            {
                Q.Cast(minion);
            }
        }
    }
}
