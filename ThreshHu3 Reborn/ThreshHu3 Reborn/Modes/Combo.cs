using System.Linq;
using EloBuddy;
using EloBuddy.SDK;

// Using the config like this makes your life easier, trust me
using Settings = TreshHu3Reborn.Config.Modes.Combo;

namespace TreshHu3Reborn.Modes
{
    public sealed class Combo : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
        }

        public override void Execute()
        {
            var target = TargetSelector.GetTarget(Q.Range, DamageType.Physical);
            if (target == null) return;

            if (Settings.UseQ && Q.IsReady() && target.IsValidTarget(Q.Range))
            {
                Q.Cast(target);
            }

            if (Settings.UseE && E.IsReady() && target.IsValidTarget(E.Range))
            {
                if (Settings.ModeE == 0)
                {
                    E.Cast(target);
                    Chat.Print("Use E Push");
                }
                if (Settings.ModeE == 1)
                {
                    E.Cast(Player.Instance.Position.Shorten(target.Position, E.Range));
                    Chat.Print("Use E Pull");
                }
            }

            if (Settings.UseW && W.IsReady())
            {
                var ally =
                    EntityManager.Heroes.Allies.OrderByDescending(h => h.Health)
                        .FirstOrDefault(a => a.IsValidTarget(W.Range));

                if (ally != null)
                {
                    W.Cast(ally);
                }
            }

            if (Settings.UseR && R.IsReady() && Player.Instance.CountEnemiesInRange(R.Range) >= Settings.MinR)
            {
                R.Cast();
            }
        }
    }
}
