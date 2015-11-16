using EloBuddy;
using EloBuddy.SDK;

namespace CaitlynHu3Reborn
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
                            25 + 1.2f*Player.Instance.TotalAttackDamage,
                            70 + 1.3f*Player.Instance.TotalAttackDamage,
                            115 + 1.4f*Player.Instance.TotalAttackDamage,
                            160 + 1.5f*Player.Instance.TotalAttackDamage,
                            205 + 1.6f*Player.Instance.TotalAttackDamage
                        }[spellLevel];
                    break;

                case SpellSlot.W:

                    damage = new float[] {0, 0, 0, 0, 0}[spellLevel] + 0.0f*Player.Instance.TotalMagicalDamage;
                    break;

                case SpellSlot.E:

                    damage = new float[] {70, 110, 150, 190, 230}[spellLevel] + 0.8f*Player.Instance.TotalMagicalDamage;
                    break;

                case SpellSlot.R:

                    damage = new float[] { 250, 475, 700 }[spellLevel] + 2.0f * Player.Instance.TotalAttackDamage;
                    break;
            }

            if (damage <= 0)
            {
                return 0;
            }

            return Player.Instance.CalculateDamageOnUnit(target, damageType, damage) - 30;
        }
    }
}