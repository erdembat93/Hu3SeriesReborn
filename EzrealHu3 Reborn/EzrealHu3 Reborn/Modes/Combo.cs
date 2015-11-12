using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using Settings = EzrealHu3.Config.Modes.Combo;

namespace EzrealHu3.Modes
{
    public sealed class Combo : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
        }

        public override void Execute()
        {
            var target = TargetSelector.GetTarget(Q.Range, DamageType.Physical);
            if (target == null || target.IsZombie || target.HasUndyingBuff()) return;

            if (Settings.UseE && E.IsReady() && Player.Instance.CountEnemiesInRange(800) <= 2 && Player.Instance.ManaPercent >= 60 || target.HealthPercent <= 20)
            {
                E.Cast(Player.Instance.Position.Extend(Game.CursorPos, E.Range).To3D());
            }

            if (Settings.UseQ && Q.IsReady() && target.IsValidTarget(Q.Range))
            {
                Q.Cast(target);
            }

            if (Settings.UseW && W.IsReady() && target.IsValidTarget(W.Range))
            {
                W.Cast(target);
            }

            if (Settings.UseR && R.IsReady() && Player.Instance.CountEnemiesInRange(SpellManager.W.Range) <= 2)
            {
                var heroes = EntityManager.Heroes.Enemies;
                foreach (var hero in EntityManager.Heroes.Enemies.Where(hero => !hero.IsDead && hero.IsVisible && hero.IsInRange(Player.Instance, R.Range)))
                {
                    var collision = new List<AIHeroClient>();
                    collision.Clear();
                    foreach (var colliHero in heroes.Where(colliHero => !colliHero.IsDead && colliHero.IsVisible && colliHero.IsInRange(hero, 3000)))
                    {
                        if (Prediction.Position.Collision.LinearMissileCollision(colliHero, Player.Instance.Position.To2D(), Player.Instance.Position.Extend(hero.Position.To2D(), 1500),
                            SpellManager.R.Speed, SpellManager.R.Width, SpellManager.R.CastDelay))
                        {
                            collision.Add(colliHero);
                        }
                        if (collision.Count >= Settings.MinR)
                        {
                            R.Cast(hero);
                        }
                    }
                }
            }
        }
    }
}
