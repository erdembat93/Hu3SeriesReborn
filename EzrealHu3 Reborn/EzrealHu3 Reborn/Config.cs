using System.Data.SqlClient;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using Color = System.Drawing.Color;

// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable InconsistentNaming
// ReSharper disable MemberHidesStaticFromOuterClass

namespace EzrealHu3
{
    public static class Config
    {
        private const string MenuName = "EzrealHu3 2.0";

        private static readonly Menu Menu;

        static Config()
        {
            // Initialize the menu
            Menu = MainMenu.AddMenu(MenuName, MenuName.ToLower());
            Menu.AddGroupLabel("EzrealHu3 2.0");
            Menu.AddSeparator();
            Menu.AddGroupLabel("Made By: MarioGK");

            // Initialize the modes
            Modes.Initialize();
        }

        public static void Initialize()
        {
        }

        public static class Modes
        {
            private static readonly Menu ModesMenu, DrawMenu;

            static Modes()
            {
                ModesMenu = Menu.AddSubMenu("Modes");

                Combo.Initialize();
                Menu.AddSeparator();
                Harass.Initialize();
                Menu.AddSeparator();
                AutoHarass.Initialize();
                Menu.AddSeparator();
                LaneClear.Initialize();
                Menu.AddSeparator();
                LastHit.Initialize();
                Menu.AddSeparator();
                Misc.Initialize();

                DrawMenu = Menu.AddSubMenu("Draw");
                Draw.Initialize();
            }

            public static void Initialize()
            {
            }

            public static class Combo
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                private static readonly CheckBox _useE;
                private static readonly CheckBox _useR;
                private static readonly Slider _minR;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }

                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }

                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }

                public static bool UseR
                {
                    get { return _useR.CurrentValue; }
                }

                public static int MinR
                {
                    get { return _minR.CurrentValue; }
                }

                static Combo()
                {
                    // Initialize the menu values
                    ModesMenu.AddGroupLabel("Combo");
                    _useQ = ModesMenu.Add("comboQ", new CheckBox("Use Q"));
                    _useW = ModesMenu.Add("comboW", new CheckBox("Use W"));
                    _useE = ModesMenu.Add("comboE", new CheckBox("Use E Smart", false));
                    _useR = ModesMenu.Add("comboR", new CheckBox("Use R on multiple enemies", false));
                    _minR = ModesMenu.Add("minR", new Slider("Min Enemies to use R", 2, 0, 5));
                }

                public static void Initialize()
                {
                }
            }

            public static class Harass
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                private static readonly Slider _manaQ;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }

                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }

                public static int ManaHarass
                {
                    get { return _manaQ.CurrentValue; }
                }

                static Harass()
                {
                    // Initialize the menu values
                    ModesMenu.AddGroupLabel("Harass");
                    _useQ = ModesMenu.Add("harassQ", new CheckBox("Use Q"));
                    _useW = ModesMenu.Add("harassW", new CheckBox("Use W"));
                    _manaQ = ModesMenu.Add("manaHaras", new Slider("Min Mana To Haras", 30));
                }

                public static void Initialize()
                {
                }
            }

            public static class AutoHarass
            {
                private static readonly CheckBox _autouseQ;
                private static readonly CheckBox _autouseW;
                private static readonly Slider _automanaQ;

                public static bool UseQ
                {
                    get { return _autouseQ.CurrentValue; }
                }

                public static bool UseW
                {
                    get { return _autouseW.CurrentValue; }
                }

                public static int ManaAutoHarass
                {
                    get { return _automanaQ.CurrentValue; }
                }

                static AutoHarass()
                {
                    // Initialize the menu values
                    ModesMenu.AddGroupLabel("Harass");
                    _autouseQ = ModesMenu.Add("autoharassQ", new CheckBox("Use Q"));
                    _autouseW = ModesMenu.Add("autoharassW", new CheckBox("Use W"));
                    _automanaQ = ModesMenu.Add("automanaHaras", new Slider("Min Mana To Haras", 30));
                }

                public static void Initialize()
                {
                }
            }

            public static class LaneClear
            {
                private static readonly CheckBox _useQ;
                private static readonly Slider _manaLane;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }

                public static int ManaLane
                {
                    get { return _manaLane.CurrentValue; }
                }

                static LaneClear()
                {
                    ModesMenu.AddGroupLabel("LaneClear");
                    _useQ = ModesMenu.Add("laneQ", new CheckBox("Use Q"));
                    _manaLane = ModesMenu.Add("manaLane", new Slider("Min Mana To Use Lane Clear" , 30));
                }

                public static void Initialize()
                {
                }
            }

            public static class LastHit
            {
                private static readonly CheckBox _useQ;
                private static readonly Slider _manaLast;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }

                public static int ManaLast
                {
                    get { return _manaLast.CurrentValue; }
                }

                static LastHit()
                {
                    // Initialize the menu values
                    ModesMenu.AddGroupLabel("LastHit");
                    _useQ = ModesMenu.Add("lastQ", new CheckBox("Use Q"));
                    _manaLast = ModesMenu.Add("manaLast", new Slider("Min Mana To Use Lane Clear", 30));
                }

                public static void Initialize()
                {
                }
            }

            public static class Misc
            {
                private static readonly CheckBox _fleeE;
                private static readonly CheckBox _stunR;
                private static readonly CheckBox _gapE;
                private static readonly CheckBox _ksR;
                private static readonly Slider _minKsR;
                private static readonly Slider _maxKsR;
                private static readonly Slider _minHealthKsR;

                public static bool UseE
                {
                    get { return _fleeE.CurrentValue; }
                }

                public static bool UseR
                {
                    get { return _stunR.CurrentValue; }
                }

                public static bool GapE
                {
                    get { return _gapE.CurrentValue; }
                }

                public static bool KSR
                {
                    get { return _ksR.CurrentValue; }
                }

                public static int minR
                {
                    get { return _minKsR.CurrentValue; }
                }

                public static int maxR
                {
                    get { return _maxKsR.CurrentValue; }
                }

                public static int MinHealthR
                {
                    get { return _minHealthKsR.CurrentValue; }
                }

                static Misc()
                {
                    // Initialize the menu values
                    ModesMenu.AddGroupLabel("Misc");
                    _fleeE = ModesMenu.Add("fleeE", new CheckBox("Use E to Flee ?"));
                    _stunR = ModesMenu.Add("stunUlt", new CheckBox("Use R when target is CC`ed ?"));
                    _gapE = ModesMenu.Add("gapE", new CheckBox("Use E on Gapcloser ?"));
                    ModesMenu.AddGroupLabel("KS");
                    _ksR = ModesMenu.Add("ksR", new CheckBox("Use R to KS"));
                    _minKsR = ModesMenu.Add("ksminR", new Slider("Min Range to KS with R", 600, 300, 2000));
                    _maxKsR = ModesMenu.Add("ksmaxR", new Slider("Max Range to KS with R", 1500, 300, 2000));
                    _minHealthKsR = ModesMenu.Add("kshealthR", new Slider("Min Health to KS with R", 200, 0, 650));

                }

                public static void Initialize()
                {
                }
            }

            public static class Draw
            {
                private static readonly CheckBox _drawHealth;
                private static readonly CheckBox _drawQ;
                private static readonly CheckBox _drawW;
                private static readonly CheckBox _drawE;
                private static readonly CheckBox _drawR;

                private static readonly CheckBox _drawReady;

                public static bool DrawHealth
                {
                    get { return _drawHealth.CurrentValue; }
                }

                public static bool DrawQ
                {
                    get { return _drawQ.CurrentValue; }
                }

                public static bool DrawW
                {
                    get { return _drawW.CurrentValue; }
                }

                public static bool DrawE
                {
                    get { return _drawE.CurrentValue; }
                }

                public static bool DrawR
                {
                    get { return _drawR.CurrentValue; }
                }

                public static bool DrawReady
                {
                    get { return _drawReady.CurrentValue; }
                }


                public static Color colorHealth
                {
                    get { return DrawMenu.GetColor("colorHealth"); }
                }

                public static Color colorQ
                {
                    get { return DrawMenu.GetColor("colorQ"); }
                }

                public static Color colorW
                {
                    get { return DrawMenu.GetColor("colorW"); }
                }

                public static Color colorE
                {
                    get { return DrawMenu.GetColor("colorE"); }
                }

                public static Color colorR
                {
                    get { return DrawMenu.GetColor("colorR"); }
                }

                public static float _widthQ
                {
                    get { return DrawMenu.GetWidth("widthQ"); }
                }

                public static float _widthW
                {
                    get { return DrawMenu.GetWidth("widthW"); }
                }

                public static float _widthE
                {
                    get { return DrawMenu.GetWidth("widthE"); }
                }

                public static float _widthR
                {
                    get { return DrawMenu.GetWidth("widthR"); }
                }

                static Draw()
                {
                    DrawMenu.AddGroupLabel("Draw");
                    _drawReady = DrawMenu.Add("drawReady", new CheckBox("Draw Only If The Spells Are Ready.", false));
                    DrawMenu.AddSeparator();
                    DrawMenu.AddLabel("Reload is required to aply the changes made in the damage indicator");
                    _drawHealth = DrawMenu.Add("drawHealth", new CheckBox("Draw Damage in HealthBar"));
                    DrawMenu.AddColorItem("colorHealth");
                    DrawMenu.AddSeparator();
                    //Q
                    _drawQ = DrawMenu.Add("drawQ", new CheckBox("Draw Q"));
                    DrawMenu.AddColorItem("colorQ");
                    DrawMenu.AddWidthItem("widthQ");
                    //W
                    _drawW = DrawMenu.Add("drawW", new CheckBox("Draw W"));
                    DrawMenu.AddColorItem("colorW");
                    DrawMenu.AddWidthItem("widthW");
                    //E
                    _drawE = DrawMenu.Add("drawE", new CheckBox("Draw E"));
                    DrawMenu.AddColorItem("colorE");
                    DrawMenu.AddWidthItem("widthE");
                    //R
                    _drawR = DrawMenu.Add("drawR", new CheckBox("Draw R"));
                    DrawMenu.AddColorItem("colorR");
                    DrawMenu.AddWidthItem("widthR");
                }

                public static void Initialize()
                {
                }
            }
        }
    }
}