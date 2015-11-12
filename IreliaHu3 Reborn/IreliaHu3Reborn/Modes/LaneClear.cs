using System.Linq;
using EloBuddy;
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
            if (Q.IsReady() && Settings.UseQ)
            {
                var minionq =
                    EntityManager.MinionsAndMonsters.EnemyMinions.FirstOrDefault(
                        m => m.IsValidTarget(Q.Range) && m.Health <= SpellDamage.GetRealDamage(SpellSlot.Q, m));

                if (minionq != null)
                {
                    Q.Cast(minionq);
                }
            }

            if (Program.CanW)
            {
                var minionw =
                   EntityManager.MinionsAndMonsters.EnemyMinions.FirstOrDefault(
                       m => m.IsValidTarget(Player.Instance.AttackRange + Player.Instance.BoundingRadius));

                if (minionw != null)
                {
                    W.Cast();
                }
            }
        }
    }
}
