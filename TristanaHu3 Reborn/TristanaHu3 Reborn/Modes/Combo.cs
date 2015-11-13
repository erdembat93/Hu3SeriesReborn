using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using Settings = TristanaHu3Reborn.Config.Modes.Combo;
using Configs = TristanaHu3Reborn.Config.Modes.Misc;

namespace TristanaHu3Reborn.Modes
{
    public sealed class Combo : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
        }

        public override void Execute()
        {
            var target = TargetSelector.GetTarget(1200, DamageType.Physical);
            if (target == null || target.IsZombie || target.HasUndyingBuff()) return;

            Orbwalker.ForcedTarget = null;

            
            if (Settings.UseE && E.IsReady() && target.IsValidTarget(E.Range) && !Config.Modes.ModesMenu["dont e" + target.ChampionName].Cast<CheckBox>().CurrentValue)
            {
                E.Cast(target);
            }

            if (Settings.UseQ && target.IsValidTarget(Player.Instance.AttackRange))
            {
                Q.Cast();
            }

            if (Settings.UseW && W.IsReady() && target.IsValidTarget(1200) && target.CountEnemiesInRange(700) <= 2 &&
                (target.HealthPercent < (Player.Instance.HealthPercent - 10)) &&
                !target.IsInRange(Player.Instance, Player.Instance.AttackRange))
            {
                var castpos = Player.Instance.Position.Extend(target.Position, W.Range).To3D();
                if (!castpos.Tower())
                {
                    W.Cast(castpos);
                }
            }
        }
    }
}