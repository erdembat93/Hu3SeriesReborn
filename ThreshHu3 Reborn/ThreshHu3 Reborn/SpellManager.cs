using EloBuddy;
using EloBuddy.SDK;

namespace TreshHu3Reborn
{
    public static class SpellManager
    {
        public static Spell.Active Q { get; private set; }
        public static Spell.Active W { get; private set; }
        public static Spell.Active E { get; private set; }
        public static Spell.Targeted R { get; private set; }

        static SpellManager()
        {
            Q = new Spell.Active(SpellSlot.Q, 1000);
            W = new Spell.Active(SpellSlot.W, 800);
            E = new Spell.Active(SpellSlot.E, 700);
            R = new Spell.Targeted(SpellSlot.R, 600);
        }

        public static void Initialize()
        {
        }
    }
}
