using System.Linq;
using EloBuddy.SDK;
using Settings = YasuoHu3Reborn.Config.Modes.LaneClear;

namespace YasuoHu3Reborn.Modes
{
    public sealed class JungleClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear);
        }

        public override void Execute()
        {
            var minion = EntityManager.MinionsAndMonsters.GetJungleMonsters().FirstOrDefault();

            if (Settings.UseQ && minion.IsValidTarget(Q.Range) && Q.IsReady())
            {
                Q.Cast(minion);
            }

            if (Settings.UseE && minion.IsValidTarget(E.Range) && E.IsReady())
            {
                E.Cast(minion);
            }
        }
    }
}
