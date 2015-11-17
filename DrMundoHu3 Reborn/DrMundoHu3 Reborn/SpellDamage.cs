﻿using EloBuddy;
using EloBuddy.SDK;

namespace DrMundoHu3Reborn
{
    public static class SpellDamage
    {
        public static float GetTotalDamage(AIHeroClient target)
        {
            // Auto attack
            var damage = Player.Instance.GetAutoAttackDamage(target);

            // Q
            if (SpellManager.Q.IsReady())
            {
                damage += SpellManager.Q.GetRealDamage(target);
            }

            // W
            if (SpellManager.W.IsReady())
            {
                damage += SpellManager.W.GetRealDamage(target);
            }

            // E
            if (SpellManager.E.IsReady())
            {
                damage += SpellManager.E.GetRealDamage(target);
            }

            // R
            if (SpellManager.R.IsReady())
            {
                damage += SpellManager.R.GetRealDamage(target);
            }

            return damage;
        }

        public static float GetRealDamage(this Spell.SpellBase spell, Obj_AI_Base target)
        {
            return spell.Slot.GetRealDamage(target);
        }

        public static float GetRealDamage(this SpellSlot slot, Obj_AI_Base target)
        {
            // Helpers
            var spellLevel = Player.Instance.Spellbook.GetSpell(slot).Level;
            const DamageType damageType = DamageType.Magical;
            float damage = 0;

            // Validate spell level
            if (spellLevel == 0)
            {
                return 0;
            }
            spellLevel--;

            switch (slot)
            {
                case SpellSlot.Q:

                    damage =
                        new float[]
                        {
                            80 + 0.15f*(target.HealthPercent),
                            130 + 0.18f*(target.HealthPercent),
                            180 + 0.21f*(target.HealthPercent),
                            230 + 0.23f*(target.HealthPercent),
                            280 + 0.25f*(target.HealthPercent)
                        }[spellLevel] +
                        0.0f*Player.Instance.TotalMagicalDamage;
                    break;

                case SpellSlot.W:

                    damage = new float[] { 35, 50, 65, 80, 95 }[spellLevel] + 0.1f * Player.Instance.TotalMagicalDamage;
                    break;

                case SpellSlot.E:

                    damage = new float[] { 40, 55, 70, 85, 100 }[spellLevel] + Player.Instance.GetAutoAttackDamage(target);
                    break;

                case SpellSlot.R:

                    damage = new float[] { 0, 0, 0 }[spellLevel] + 0.0f * Player.Instance.TotalMagicalDamage;
                    break;
            }

            if (damage <= 0)
            {
                return 0;
            }

            return Player.Instance.CalculateDamageOnUnit(target, damageType, damage) - 10;
        }
    }
}