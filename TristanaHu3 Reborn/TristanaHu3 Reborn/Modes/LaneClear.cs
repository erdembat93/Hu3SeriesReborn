using System.Linq;
using EloBuddy;
using EloBuddy.SDK;

using Settings = TristanaHu3Reborn.Config.Modes.LaneClear;

namespace TristanaHu3Reborn.Modes
{
    public sealed class LaneClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear);
        }

        public override void Execute()
        {
            var minion =
                EntityManager.MinionsAndMonsters.EnemyMinions
                    .FirstOrDefault(m => m.IsValidTarget(Player.Instance.AttackRange));
            if (minion != null)
            {

                if (minion.IsValidTarget(E.Range) && Settings.UseE && E.IsReady())
                {
                    E.Cast(minion);
                }

                if (minion.IsValidTarget(Q.Range) && Settings.UseQ && Q.IsReady())
                {
                    Q.IsReady();
                }

                if (minion.HasBuff("tristanaecharge"))
                {
                    Orbwalker.ForcedTarget = minion;
                }
            }

            var tower = EntityManager.Turrets.Enemies.FirstOrDefault(t => !t.IsDead && t.IsValidTarget(E.Range));
            if (tower == null) return;

            if (tower.IsValidTarget(E.Range) && Settings.UseE && E.IsReady())
            {
                E.Cast(tower);
            }

            if (tower.IsValidTarget(Q.Range) && Settings.UseQ && Q.IsReady())
            {
                Q.Cast();
            }
        }
    }
}
