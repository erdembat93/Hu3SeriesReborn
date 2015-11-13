using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Rendering;
using Settings = TristanaHu3Reborn.Config.Modes.Draw;
using Misc = TristanaHu3Reborn.Config.Modes.Misc;


namespace TristanaHu3Reborn
{
    public static class Program
    {
        // ReSharper disable once MemberCanBePrivate.Global
        public const string ChampName = "Tristana";

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
            Interrupter.OnInterruptableSpell += Interrupter_OnInterruptableSpell;
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
                new Circle { Color = Settings.colorR, BorderWidth = Settings._widthR, Radius = SpellManager.R.Range }.Draw(Player.Instance.Position);
            }
        }

        static void Gapcloser_OnGapcloser(AIHeroClient sender, Gapcloser.GapcloserEventArgs e)
        {
            if (sender.IsEnemy && sender.IsInRange(Player.Instance, SpellManager.R.Range) && Player.Instance.Distance(e.End) < SpellManager.R.Range && Misc.RGap)
            {
                SpellManager.R.Cast(sender);
            }
        }


        static void Interrupter_OnInterruptableSpell(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs e)
        {
            if (sender.IsValidTarget(SpellManager.R.Range) && e.DangerLevel >= DangerLevel.High && Misc.RInt)
            {
                SpellManager.R.Cast(sender);
            }
        }
    }
}
