using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace DrMundoHu3Reborn
{
    public static class SpellManager
    {
        public static Spell.Skillshot Q { get; private set; }
        public static Spell.Active W { get; private set; }
        public static Spell.Active E { get; private set; }
        public static Spell.Active R { get; private set; }

        static SpellManager()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 980, SkillShotType.Linear, 260, 1850, 70);
            W = new Spell.Active(SpellSlot.W, 260);
            E = new Spell.Active(SpellSlot.E, (uint)Player.Instance.AttackRange + (uint)Player.Instance.BoundingRadius);
            R = new Spell.Active(SpellSlot.R);
        }

        public static void Initialize()
        {
        }
    }
}
