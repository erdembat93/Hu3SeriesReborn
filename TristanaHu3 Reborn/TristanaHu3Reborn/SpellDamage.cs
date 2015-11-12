using EloBuddy;
using EloBuddy.SDK;

namespace AddonTemplate
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

                    damage = new float[] { 0, 0, 0, 0, 0 }[spellLevel] + 0.0f * Player.Instance.TotalMagicalDamage;
                    break;

                case SpellSlot.W:

                    damage = new float[] { 80, 105, 130, 155, 180 }[spellLevel] + 0.5f * Player.Instance.TotalMagicalDamage;
                    break;

                case SpellSlot.E:

                    damage =
                        new float[]
                        {
                            60 + (0.50f*Player.Instance.TotalAttackDamage),
                            70 + (0.65f*Player.Instance.TotalAttackDamage),
                            80 + (0.80f*Player.Instance.TotalAttackDamage),
                            90 + (0.95f*Player.Instance.TotalAttackDamage),
                            100 + (1.1f*Player.Instance.TotalAttackDamage)
                        }[spellLevel] +
                        0.75f*Player.Instance.TotalMagicalDamage;
                    break;

                case SpellSlot.R:

                    damage = new float[] { 300, 400, 500 }[spellLevel] + 1f * Player.Instance.TotalMagicalDamage;
                    break;
            }

            if (damage <= 0)
            {
                return 0;
            }

            return Player.Instance.CalculateDamageOnUnit(target, damageType, damage) - 20;
        }
    }
}