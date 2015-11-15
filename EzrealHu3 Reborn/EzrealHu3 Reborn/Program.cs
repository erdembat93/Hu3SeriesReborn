using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using Settings = EzrealHu3.Config.Modes.Draw;
using Misc = EzrealHu3.Config.Modes.Misc;


namespace EzrealHu3
{
    public static class Program
    {
        // ReSharper disable once MemberCanBePrivate.Global
        public const string ChampName = "Ezreal";

        public static void Main(string[] args)
        {
            Loading.OnLoadingComplete += OnLoadingComplete;
        }

        private static void OnLoadingComplete(EventArgs args)
        {
            if (Player.Instance.ChampionName != ChampName)
            {
                return;
            }

            Config.Initialize();
            SpellManager.Initialize();
            ModeManager.Initialize();
            DamageIndicator.Initialize(SpellDamage.GetTotalDamage);

            Drawing.OnDraw += OnDraw;

            Gapcloser.OnGapcloser += Gapcloser_OnGapcloser;
        }

        private static void OnDraw(EventArgs args)
        {
            if (Settings.DrawReady ? SpellManager.Q.IsReady() : Settings.DrawQ)
            {
                new Circle { Color = Settings.colorQ, BorderWidth = Settings._widthQ, Radius = SpellManager.Q.Range }.Draw(Player.Instance.Position);
            }

            if (Settings.DrawReady ? SpellManager.W.IsReady() : Settings.DrawW)
            {
                new Circle { Color = Settings.colorW, BorderWidth = Settings._widthW, Radius = SpellManager.W.Range }.Draw(Player.Instance.Position);
            }

            if (Settings.DrawReady ? SpellManager.E.IsReady() : Settings.DrawE)
            {
                new Circle { Color = Settings.colorE, BorderWidth = Settings._widthE, Radius = SpellManager.E.Range }.Draw(Player.Instance.Position);
            }

            if (Settings.DrawReady ? SpellManager.R.IsReady() : Settings.DrawR)
            {
                new Circle { Color = Settings.colorR, BorderWidth = Settings._widthR, Radius = Misc.minR }.Draw(Player.Instance.Position);
                new Circle { Color = Settings.colorR, BorderWidth = Settings._widthR, Radius = Misc.maxR }.Draw(Player.Instance.Position);
            }
        }

        private static void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (Misc.GapE && SpellManager.E.IsReady())
            {
                if (sender.IsEnemy && sender.IsVisible && Player.Instance.Distance(e.End) < 100)
                {
                    SpellManager.E.Cast(Player.Instance.Position.Shorten(sender.Position, SpellManager.E.Range));
                }
            }
        }
    }
}
