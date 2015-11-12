using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using Settings = EzrealHu3.Config.Modes.Misc;
using Configs = EzrealHu3.Config.Modes.AutoHarass;

namespace EzrealHu3.Modes
{
    public sealed class PermaActive : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return true;
        }

        public override void Execute()
        {
            if (Settings.UseR && R.IsReady())
            {
                foreach (var hero in EntityManager.Heroes.Enemies.Where(e => e.IsVisible))
                {
                    if (hero.HasBuffOfType(BuffType.Stun) || hero.HasBuffOfType(BuffType.Knockup) ||
                        hero.HasBuffOfType(BuffType.Snare))
                    {
                        R.Cast(hero);
                    }
                }

                var target = TargetSelector.GetTarget(Settings.maxR, DamageType.Physical);
                if (target == null || target.IsZombie || target.HasUndyingBuff()) return;

                if (!target.IsInRange(Player.Instance, Settings.minR) &&
                    target.Health <= SpellDamage.GetRealDamage(SpellSlot.R, target) && target.Health >= Settings.MinHealthR)
                {
                    R.Cast(target);
                }
            }

            if ((Configs.UseQ && Q.IsReady()) || (Configs.UseW && W.IsReady()))
            {
                var target = TargetSelector.GetTarget(Q.Range, DamageType.Physical);
                if (target == null || target.IsZombie || target.HasUndyingBuff()) return;

                if (target.IsValidTarget(Q.Range))
                {
                    Q.Cast(target);
                }
                if (target.IsValidTarget(W.Range))
                {
                    W.Cast(target);
                }
            }
        }
    }
}