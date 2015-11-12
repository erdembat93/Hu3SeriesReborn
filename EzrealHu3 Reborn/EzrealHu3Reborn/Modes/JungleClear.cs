using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using Settings = EzrealHu3.Config.Modes.LaneClear;

namespace EzrealHu3.Modes
{
    public sealed class JungleClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear);
        }

        public override void Execute()
        {
            var jungleMonsters =
                EntityManager.MinionsAndMonsters.GetJungleMonsters()
                    .OrderByDescending(m => m.Health)
                    .FirstOrDefault(m => m.IsValidTarget(Q.Range));
            if (jungleMonsters == null) return;
            if (Settings.UseQ && Q.IsReady() && Settings.ManaLane <= Player.Instance.ManaPercent)
            {
                Q.Cast(jungleMonsters);
            }
        }
    }
}
