﻿using System.Linq;
using EloBuddy;
using EloBuddy.SDK;

using Settings = AddonTemplate.Config.Modes.LastHit;

namespace AddonTemplate.Modes
{
    public sealed class LastHit : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit);
        }

        public override void Execute()
        {
            if (Q.IsReady() && Settings.UseQ && Player.Instance.ManaPercent >= Settings.ManaLast)
            {
                var minionq =
                    EntityManager.MinionsAndMonsters.EnemyMinions.FirstOrDefault(
                        m => m.IsValidTarget(Q.Range) && m.Health < SpellDamage.GetRealDamage(SpellSlot.Q, m));

                if (minionq != null)
                {
                    Q.Cast(minionq);
                }
            }
        }
    }
}
