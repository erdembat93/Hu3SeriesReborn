﻿using System.Drawing.Printing;
using EloBuddy;
using EloBuddy.SDK;

using Settings = TristanaHu3Reborn.Config.Modes.Combo;

namespace TristanaHu3Reborn.Modes
{
    public sealed class PermaActive : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return true;
        }

        public override void Execute()
        {
            var target = TargetSelector.GetTarget(R.Range, DamageType.Physical);
            if (target == null || target.IsZombie || target.HasUndyingBuff()) return;

            if (R.IsReady() && Settings.UseR)
            {
                var stacks = target.GetBuffCount("tristanaecharge");
                if (stacks > 0)
                {
                    if (target.Health <= (SpellDamage.GetRealDamage(SpellSlot.E, target)*((0.29*stacks) + 1) +
                                          SpellDamage.GetRealDamage(SpellSlot.R, target)))
                    {
                        R.Cast(target);
                    }
                }
            }

            if (R.IsReady() && Settings.UseR)
            {
                if (target.Health <= (SpellDamage.GetRealDamage(SpellSlot.R, target)) && target.Health > Player.Instance.TotalAttackDamage)
                {
                    R.Cast(target);
                }
            }

            if (R.IsReady() && Settings.UseRTower)
            {
                if (target.RPos().AllyTower())
                {
                    R.Cast(target);
                }
            }
        }
    }
}
