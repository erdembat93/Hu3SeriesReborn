﻿using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using Settings = AddonTemplate.Config.Modes.LaneClear;

namespace AddonTemplate.Modes
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
                    .FirstOrDefault(m => m.IsValidTarget(Player.Instance.AttackRange));
            if (minion.IsValidTarget(E.Range) && Settings.UseE)
            {
                E.Cast(minion);
            }
            if (minion.HasBuff("tristanaecharge"))
            {
                Orbwalker.ForcedTarget = minion;
            }
        }
    }
}
