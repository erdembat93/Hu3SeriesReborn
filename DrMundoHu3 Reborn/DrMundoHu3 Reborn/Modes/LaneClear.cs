using System.Linq;
using EloBuddy;
using EloBuddy.SDK;

using Settings = DrMundoHu3Reborn.Config.Modes.LaneClear;

namespace DrMundoHu3Reborn.Modes
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
                EntityManager.MinionsAndMonsters.GetLaneMinions()
                    .FirstOrDefault(m => m.IsValidTarget(Q.Range) && m.IsEnemy);

            if (minion != null)
            {
                if (Settings.UseQ && Q.IsReady())
                {
                    var minionQ =
                        EntityManager.MinionsAndMonsters.GetLaneMinions()
                            .FirstOrDefault(
                                m =>
                                    m.IsValidTarget(Q.Range) && m.IsEnemy &&
                                    m.Health < SpellDamage.GetRealDamage(SpellSlot.Q, m));
                    if (minionQ != null)
                    {
                        Q.Cast(minionQ);
                    }
                }
                if (Settings.UseW && W.IsReady() && minion.IsValidTarget(W.Range) &&
                    !Player.Instance.HasBuff("burningagony"))
                {
                    {
                        W.Cast();
                    }
                }
                if (Settings.UseE && E.IsReady() && minion.IsValidTarget(E.Range))
                {
                    {
                        E.Cast();
                    }
                }
            }
        }
    }
}
