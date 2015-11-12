using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using SharpDX;
using YasuoHu3Reborn.EvadePlus;
using YasuoHu3Reborn.EvadePlus.SkillshotTypes;
using Color = System.Drawing.Color;

using Settings = YasuoHu3Reborn.Config.Modes.Evade;

namespace YasuoHu3Reborn
{
    class Evader
    {
        public static int WallCastT;
        public static Vector2 YasuoWallCastedPos;
        public static int WDelay;
        public static GameObject Wall;
        public static Geometry.Polygon.Rectangle WallPolygon;
        private static int _resetWall;

        public static void Init()
        {
            GameObject.OnCreate += GameObject_OnCreate;
            Game.OnTick += delegate { UpdateTask(); };
            Obj_AI_Base.OnProcessSpellCast += Obj_AI_Base_OnProcessSpellCast;
            Drawing.OnDraw += Drawing_OnDraw;
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if (WallPolygon != null)
            {
                WallPolygon.DrawPolygon(Color.AliceBlue);
            }
        }

        private static void GameObject_OnCreate(GameObject sender, EventArgs args)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(
                        sender.Name, "_w_windwall.\\.troy",
                        System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            {
                Wall = sender;
            }
        }

        private static void Obj_AI_Base_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.IsValid && sender.Team == ObjectManager.Player.Team && args.SData.Name == "YasuoWMovingWall")
            {
                YasuoWallCastedPos = sender.ServerPosition.To2D();
                _resetWall = Environment.TickCount + 4000;
            }
        }

        public static void UpdateTask()
        {
            if (EvadePlus.Program.Evade == null) return;
            EvadePlus.Program.Evade.CacheSkillshots();
            if (_resetWall - Environment.TickCount > 3400 && Wall != null)
            {
                var level = Player.GetSpell(SpellSlot.W).Level;
                var wallWidth = (300 + 50 * level);
                var wallDirection = (Wall.Position.To2D() - YasuoWallCastedPos).Normalized().Perpendicular();
                var wallStart = Wall.Position.To2D() + wallWidth / 2 * wallDirection;
                var wallEnd = wallStart - wallWidth * wallDirection;
                WallPolygon = new Geometry.Polygon.Rectangle(wallStart, wallEnd, 75);
            }
            if (_resetWall < Environment.TickCount)
            {
                Wall = null;
                WallPolygon = null;
            }
            if (Wall != null && YasuoWallCastedPos.IsValid() && WallPolygon != null)
            {
                foreach (var activeSkillshot in EvadePlus.Program.Evade.SkillshotDetector.ActiveSkillshots.Where(a => (a is LinearMissileSkillshot) && EvadeMenu.IsSkillshotW(a)))
                {
                    if (WallPolygon.IsInside(activeSkillshot.GetPosition()))
                    {
                        activeSkillshot.IsValid = false;
                    }
                }
            }

            EvadePlus.Program.Evade.CacheSkillshots();

            if (EvadePlus.Program.Evade.IsHeroInDanger(Player.Instance))
            {
                if (Settings.UseW && SpellManager.W.IsReady())
                {
                    foreach (var activeSkillshot in EvadePlus.Program.Evade.SkillshotDetector.ActiveSkillshots.Where(a => a is LinearMissileSkillshot && EvadeMenu.IsSkillshotW(a)))
                    {
                        if (activeSkillshot.ToPolygon().IsInside(Player.Instance))
                        {
                            Player.CastSpell(SpellSlot.W, activeSkillshot.GetPosition());
                            WDelay = Environment.TickCount + 500;
                            return;
                        }
                    }
                }

                if (WDelay > Environment.TickCount) return;

                if (Settings.UseE && SpellManager.E.IsReady())
                {
                    foreach (
                        var source in
                            EntityManager.MinionsAndMonsters.EnemyMinions.Where(
                                a => a.Team != Player.Instance.Team && a.Distance(Player.Instance) < 475 && a.CanE()))
                    {
                        if(source.GetAfterEPos().IsUnderTower()) continue;
                        if (EvadePlus.Program.Evade.IsPointSafe(source.GetAfterEPos().To2D()))
                        {
                            int count = 0;
                            for (int i = 0; i < 10; i += 47)
                            {
                                if (!EvadePlus.Program.Evade.IsPointSafe(Player.Instance.Position.Extend(source.GetAfterEPos(), i)))
                                {
                                    count ++;
                                }
                            }
                            if (count > 3) continue;
                            Player.CastSpell(SpellSlot.E, source);
                            break;
                        }
                    }
                    foreach (
                        var source in
                            EntityManager.Heroes.Enemies.Where(
                                a => a.IsEnemy && a.Distance(Player.Instance) < 475 && a.CanE()))
                    {
                        if (source.GetAfterEPos().IsUnderTower()) continue;
                        if (EvadePlus.Program.Evade.IsPointSafe(source.GetAfterEPos().To2D()))
                        {
                            int count = 0;
                            for (int i = 0; i < 10; i += 47)
                            {
                                if (!EvadePlus.Program.Evade.IsPointSafe(Player.Instance.Position.Extend(source.GetAfterEPos(), i)))
                                {
                                    count ++;
                                }
                            }
                            if (count > 3) continue;
                            Player.CastSpell(SpellSlot.E, source);
                            break;
                        }
                    }
                }
            }
        }
    }
}
