﻿using EloBuddy;
using EloBuddy.SDK;

namespace CaitlynHu3Reborn.Modes
{
    public sealed class Flee : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Flee);
        }

        public override void Execute()
        {
            if (E.IsReady())
            {
                E.Cast(Player.Instance.Position.Shorten(Game.CursorPos, E.Range));
            }
        }
    }
}
