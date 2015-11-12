using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using Settings = EzrealHu3.Config.Modes.LastHit;

namespace EzrealHu3.Modes
{
    public sealed class LastHit : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit);
        }

        public override void Execute()
        {
            var laneMinion =
                EntityManager.MinionsAndMonsters.GetLaneMinions()
                    .OrderByDescending(m => m.Health)
                    .FirstOrDefault(m => m.IsValidTarget(Q.Range) && m.Health <= SpellDamage.GetRealDamage(SpellSlot.Q, m));
            if (laneMinion == null) return;

            if (Settings.UseQ && Q.IsReady() && Settings.ManaLast <= Player.Instance.ManaPercent && !laneMinion.IsInRange(Player.Instance, Player.Instance.GetAutoAttackRange()))
            {
                Q.Cast(laneMinion);
            }
        }
    }
}
