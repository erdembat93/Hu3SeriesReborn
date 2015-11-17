using System.Linq;
using EloBuddy;
using EloBuddy.SDK;

using Settings = DrMundoHu3Reborn.Config.Modes.LaneClear;

namespace DrMundoHu3Reborn.Modes
{
    public sealed class LastHit : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit);
        }

        public override void Execute()
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
        }
    }
}
