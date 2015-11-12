using System;
using System.Drawing.Printing;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace YasuoHu3Reborn
{
    public static class SpellManager
    {
        public static Spell.Skillshot Q { get; private set; }
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
            Game.OnTick += Game_OnTick;
        }

        private static void Game_OnTick(EventArgs args)
        {
            Q = new Spell.Skillshot(SpellSlot.Q, Player.Instance.HasQ3() ? (uint) 1070 : 470, SkillShotType.Linear,
                Player.Instance.HasQ3() ? (int) GetQDelay : (int) GetQ2Delay, int.MaxValue, 65);
        }

        public static double GetQDelay
        {
            get { return 0.4f * (1 - Math.Min((Player.Instance.AttackSpeedMod - 1) * 0.58f, 0.66f)); }
        }

        public static float GetQ2Delay
        {
            get { return 0.5f * (1 - Math.Min((Player.Instance.AttackSpeedMod - 1) * 0.58f, 0.66f)); }
        }
    }
}
