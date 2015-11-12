using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;

using Settings = YasuoHu3Reborn.Config.Modes.Combo;

namespace YasuoHu3Reborn.Modes
{
    public sealed class Combo : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
        }

        public override void Execute()
        {
            var target = TargetSelector.GetTarget(Player.Instance.HasQ3() ? 1100 : 480, DamageType.Physical);
            if (target == null) return;

            if (Settings.UseE && SpellManager.E.IsReady() && !target.IsValidTarget(Player.Instance.AttackRange))
            {
                if (target.IsValidTarget(E.Range) && target.CanE() && !target.IsInRange(Player.Instance, Player.Instance.AttackRange) && !target.GetAfterEPos().Tower())
                {
                    SpellManager.E.Cast(target);
                }
            }

            if (Settings.UseEGap && SpellManager.E.IsReady())
            {
                var minion =
                    ObjectManager.Get<Obj_AI_Minion>()
                        .OrderByDescending(m => m.Distance(Player.Instance.Position))
                        .FirstOrDefault(
                            m =>
                                m.IsValidTarget(SpellManager.E.Range)
                                && (m.Distance(target) < Player.Instance.Distance(target))
                                && Player.Instance.IsFacing(m) && m.CanE() && !m.GetAfterEPos().Tower());

                if (minion != null && !target.IsInRange(Player.Instance, Player.Instance.AttackRange))
                {
                    SpellManager.E.Cast(minion);
                }
            }

            if (Settings.UseQ && SpellManager.Q.IsReady() && target.IsValidTarget(SpellManager.Q.Range) && !Player.Instance.IsDashing())
            {
                SpellManager.Q.Cast(target);
            }

            if (Settings.UseR && SpellManager.R.IsReady())
            {
                var enemies =
                    ObjectManager.Get<AIHeroClient>()
                        .Where(x => x.IsValidTarget(SpellManager.R.Range))
                        .Where(x => x.HasBuffOfType(BuffType.Knockup) || x.HasBuffOfType(BuffType.Knockback));

                var enemy = enemies as IList<AIHeroClient> ?? enemies.ToList();

                if (enemy.Count() >= Settings.MinR && Player.Instance.CountEnemiesInRange(SpellManager.R.Range) <= Settings.MinR)
                {
                    Core.DelayAction(() => SpellManager.R.Cast(), Settings.DelayR);
                }
            }
        }
    }
}

