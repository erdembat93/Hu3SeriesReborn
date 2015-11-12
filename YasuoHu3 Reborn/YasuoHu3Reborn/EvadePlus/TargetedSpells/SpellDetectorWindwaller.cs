using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace YasuoHu3Reborn.EvadePlus.TargetedSpells
{
    class SpellDetectorWindwaller
    {
        public static void Init()
        {
            Config.Modes.EvaderMenu.AddGroupLabel("Targeted Skills");
            foreach (var enemy in EntityManager.Heroes.Enemies)
            {
                Config.Modes.EvaderMenu.AddSeparator();
                Config.Modes.EvaderMenu.AddLabel(enemy.ChampionName);
                foreach (var spell in TargetSpellDatabase.Spells.Where(a => a.Type == SpellType.Targeted && a.ChampionName == enemy.ChampionName.ToLower()))
                {
                    Config.Modes.EvaderMenu.Add(spell.Name + "/e", new CheckBox(spell.Name + ": " + spell.Spellslot));
                }
            }

            Obj_AI_Base.OnProcessSpellCast += Obj_AI_Base_OnProcessSpellCast;
        }

        private static void Obj_AI_Base_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.IsAlly || !(sender is AIHeroClient) || args.Target == null || !args.Target.IsMe || Player.GetSpell(SpellSlot.W).State != SpellState.Ready) return;
            var spell = TargetSpellDatabase.GetByName(args.SData.Name);
            if (spell != null && Config.Modes.EvaderMenu[spell.Name + "/e"] != null && Config.Modes.EvaderMenu[spell.Name + "/e"].Cast<CheckBox>().CurrentValue)
            {
                Core.DelayAction(delegate { Player.CastSpell(SpellSlot.W, sender.Position); },
                    (int)
                        ((Player.Instance.Distance(sender) - 100/args.SData.MissileSpeed > 0
                            ? args.SData.MissileSpeed
                            : 2000)*1000 > 1
                            ? (Player.Instance.Distance(sender) - 100/args.SData.MissileSpeed > 0
                                ? args.SData.MissileSpeed
                                : 2000)*1000
                            : 0));
            }
        }
    }
}
