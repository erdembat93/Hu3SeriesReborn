using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;

namespace YasuoHu3Reborn
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

        public static bool HasQ3(this AIHeroClient target)
        {
            return target.IsMe && target.HasBuff("YasuoQ3W");
        }

        public static bool CanE(this Obj_AI_Base target)
        {
            return !target.HasBuff("YasuoDashWrapper");
        }

        public static Vector3 GetAfterEPos(this Obj_AI_Base unit)
        {
            return Player.Instance.Position.Extend(Prediction.Position.PredictUnitPosition(unit, 250), 475).To3D();
        }

        public static bool IsKnockedUp(this Obj_AI_Base unit)
        {
            return unit.HasBuffOfType(BuffType.Knockback) || unit.HasBuffOfType(BuffType.Knockup);
        }

        public static bool IsUnderTower(this Vector3 v)
        {
            return EntityManager.Turrets.Enemies.Where(a => a.Health > 0 && !a.IsDead).Any(a => a.Distance(v) < 950);
        }

        public static bool Tower(this Vector3 pos)
        {
            return EntityManager.Turrets.Enemies.Where(t => !t.IsDead).Any(d => d.Distance(pos) < 950);
        }
    }
}