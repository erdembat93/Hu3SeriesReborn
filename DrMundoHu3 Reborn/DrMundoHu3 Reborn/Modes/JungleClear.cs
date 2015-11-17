using System.Linq;
using EloBuddy;
using EloBuddy.SDK;

using Settings = DrMundoHu3Reborn.Config.Modes.LaneClear;

namespace DrMundoHu3Reborn.Modes
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
                EntityManager.MinionsAndMonsters.GetJungleMonsters()
                    .FirstOrDefault(m => m.IsValidTarget(Q.Range) && m.IsEnemy);

            if (minion != null)
            {
                if (Settings.UseQ && Q.IsReady() && minion.IsValidTarget(Q.Range))
                {
                    Q.Cast(minion);
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
