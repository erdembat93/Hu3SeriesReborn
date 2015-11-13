using System.Linq;
using EloBuddy;
using EloBuddy.SDK;

namespace TristanaHu3Reborn.Modes
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
                EntityManager.MinionsAndMonsters.EnemyMinions
                    .FirstOrDefault(m => m.IsValidTarget(Player.Instance.AttackRange));
            if (minion == null) return;

            if (minion.HasBuff("tristanaecharge"))
            {
                Orbwalker.ForcedTarget = minion;
            }
        }
    }
}