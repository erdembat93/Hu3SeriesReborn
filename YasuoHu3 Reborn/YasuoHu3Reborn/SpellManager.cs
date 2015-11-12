using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace YasuoHu3Reborn
{
    public static class SpellManager
    {
        public static Spell.Skillshot Q { get { return Player.Instance.HasQ3() ? Q2 : Q1; } }
        public static Spell.Skillshot W { get; private set; }
        public static Spell.Targeted E { get; private set; }
        public static Spell.Targeted R { get; private set; }

        static SpellManager()
        {
            W = new Spell.Skillshot(SpellSlot.W, 400,SkillShotType.Linear, 400, int.MaxValue);
            E = new Spell.Targeted(SpellSlot.E, 475);
            R = new Spell.Targeted(SpellSlot.R, 1200);
        }

        public static void Initialize()
        {
        }

        private static readonly Spell.Skillshot Q1 = new Spell.Skillshot(SpellSlot.Q, 450, SkillShotType.Linear, 250, GetQDelay(), 1)
        {
            AllowedCollisionCount = int.MaxValue
        };
        private static readonly Spell.Skillshot Q2 = new Spell.Skillshot(SpellSlot.Q, 900, SkillShotType.Linear, 300, 1200, 50)
        {
            AllowedCollisionCount = int.MaxValue
        };      

        //ty Fluxy
        public static int GetQDelay()
        {
            return (int)(1 / (1 / 0.5 * Player.Instance.AttackSpeedMod));
        }
    }
}
