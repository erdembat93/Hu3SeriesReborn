using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace CaitlynHu3Reborn
{
    public static class SpellManager
    {
        public static Spell.Skillshot Q { get; private set; }
        public static Spell.Skillshot W { get; private set; }
        public static Spell.Skillshot E { get; private set; }
        public static Spell.Targeted R { get; private set; }

        static SpellManager()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1230, SkillShotType.Linear, 550, 2000, 60);
            W = new Spell.Skillshot(SpellSlot.W, 800, SkillShotType.Circular, 350, int.MaxValue, 30);
            E = new Spell.Skillshot(SpellSlot.E, 950, SkillShotType.Linear, 250, 1600, 80);
            R = new Spell.Targeted(SpellSlot.R, 2000);
        }

        public static void Initialize()
        {
        }
    }
}
