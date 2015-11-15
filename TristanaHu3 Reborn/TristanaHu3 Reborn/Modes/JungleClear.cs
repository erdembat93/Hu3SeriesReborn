﻿using System.Linq;
using EloBuddy;
using EloBuddy.SDK;

using Settings = TristanaHu3Reborn.Config.Modes.LaneClear;

namespace TristanaHu3Reborn.Modes
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
            if (minion == null) return;

            if (minion.IsValidTarget(E.Range) && Settings.UseE)
            {
                E.Cast(minion);
            }

            if (minion.HasBuff("tristanaecharge"))
            {
                Q.Cast();
                Orbwalker.ForcedTarget = minion;
            }
        }
    }
}