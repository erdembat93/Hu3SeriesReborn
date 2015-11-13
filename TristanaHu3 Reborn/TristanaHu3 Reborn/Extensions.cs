using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

namespace TristanaHu3Reborn
{
    public static class Extensions
    {
        public static bool HasUndyingBuff(this AIHeroClient target)
        {
            // Various buffs
            if (target.Buffs.Any(
                b => b.IsValid() &&
                     (b.DisplayName == "Chrono Shift" /* Zilean R */||
                      b.DisplayName == "JudicatorIntervention" /* Kayle R */||
                      b.DisplayName == "Undying Rage" /* Tryndamere R */)))
            {
                return true;
            }

            // Poppy R
            if (target.ChampionName == "Poppy")
            {
                if (EntityManager.Heroes.Allies.Any(o => !o.IsMe && o.Buffs.Any(b => b.Caster.NetworkId == target.NetworkId && b.IsValid() && b.DisplayName == "PoppyDITarget")))
                {
                    return true;
                }
            }

            return target.IsInvulnerable;
        }

        public static bool HasSpellShield(this AIHeroClient target)
        {
            // Various spellshields
            return target.HasBuffOfType(BuffType.SpellShield) || target.HasBuffOfType(BuffType.SpellImmunity);
        }

        public static float TotalShieldHealth(this Obj_AI_Base target)
        {
            return target.Health + target.AllShield + target.AttackShield + target.MagicShield;
        }

        public static int GetStunDuration(this Obj_AI_Base target)
        {
            return (int)(target.Buffs.Where(b => b.IsActive && Game.Time < b.EndTime &&
                                          (b.Type == BuffType.Charm ||
                                           b.Type == BuffType.Knockback ||
                                           b.Type == BuffType.Stun ||
                                           b.Type == BuffType.Suppression ||
                                           b.Type == BuffType.Snare)).Aggregate(0f, (current, buff) => Math.Max(current, buff.EndTime)) - Game.Time) * 1000;
        }

        public static bool IsPassiveReady(this AIHeroClient target)
        {
            return target.IsMe && target.HasBuff("XerathAscended2OnHit");
        }

        public static bool Tower(this Vector3 pos)
        {
            return EntityManager.Turrets.Enemies.Where(t => !t.IsDead).Any(d => d.Distance(pos) < 950);
        }

        public static bool AllyTower(this Vector3 pos)
        {
            return EntityManager.Turrets.Allies.Where(t => !t.IsDead).Any(d => d.Distance(pos) < 950);
        }

        public static Vector3 RPos(this Obj_AI_Base unit)
        {
            return unit.Position.Extend(Prediction.Position.PredictUnitPosition(unit, 550), 1000).To3D();
        }
    }
}