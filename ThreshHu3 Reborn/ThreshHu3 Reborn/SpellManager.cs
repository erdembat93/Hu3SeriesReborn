using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace TreshHu3Reborn
{
    public static class SpellManager
    {
        public static Spell.Skillshot Q { get; private set; }
        public static Spell.Active Q2 { get; private set; }
        public static Spell.Skillshot W { get; private set; }
        public static Spell.Skillshot E { get; private set; }
        public static Spell.Active R { get; private set; }

        static SpellManager()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1000, SkillShotType.Linear, 550, 1900, 70)
            {
                MinimumHitChance = HitChance.High
            };
            Q2 = new Spell.Active(SpellSlot.Q, 1500);
            W = new Spell.Skillshot(SpellSlot.Q, 940, SkillShotType.Circular, 350, 1600, 40);
            E = new Spell.Skillshot(SpellSlot.E, 400, SkillShotType.Linear, 350, int.MaxValue, 120);
            R = new Spell.Active(SpellSlot.R, 430);
        }

        public static void Initialize()
        {
        }
    }
}
