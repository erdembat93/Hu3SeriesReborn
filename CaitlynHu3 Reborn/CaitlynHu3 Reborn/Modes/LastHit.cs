using System.Linq;
using EloBuddy;
using EloBuddy.SDK;

using Settings = CaitlynHu3Reborn.Config.Modes.LastHit;

namespace CaitlynHu3Reborn.Modes
{
    public sealed class LastHit : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit);
        }

        public override void Execute()
        {
            var minion =
                EntityManager.MinionsAndMonsters.GetLaneMinions()
                    .FirstOrDefault(
                        m => m.IsValidTarget(Q.Range) && m.Health <= SpellDamage.GetRealDamage(SpellSlot.Q, m));
            if (minion == null) return;

            if (Q.IsReady() && !minion.IsInRange(Player.Instance, Player.Instance.AttackRange) && Settings.UseQ &&
                minion.IsValidTarget(Q.Range))
            {
                Q.Cast(minion);
            }
        }
    }
}
