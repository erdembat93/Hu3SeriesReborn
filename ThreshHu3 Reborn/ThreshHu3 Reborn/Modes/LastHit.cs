﻿using EloBuddy;
using EloBuddy.SDK;

namespace TreshHu3Reborn.Modes
{
    public sealed class LastHit : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LastHit);
        }

        public override void Execute()
        {

        }
    }
}
