using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using Color = System.Drawing.Color;

// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable InconsistentNaming
// ReSharper disable MemberHidesStaticFromOuterClass

namespace CaitlynHu3Reborn
{
    public static class Config
    {
        private const string MenuName = "CaitlynHu3 Reborn";

        private static readonly Menu Menu;

        static Config()
        {
            // Initialize the menu
            Menu = MainMenu.AddMenu(MenuName, MenuName.ToLower());
            Menu.AddGroupLabel("CaitlynHu3 Reborn");
            Menu.AddLabel("Made By: MarioGK", 50);

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
                LaneClear.Initialize();
                Menu.AddSeparator();
                LastHit.Initialize();

                DrawMenu = Menu.AddSubMenu("Draw");
                Draw.Initialize();
            }

            public static void Initialize()
            {
            }

            public static class Combo
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useE;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }

                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }

                static Combo()
                {
                    // Initialize the menu values
                    ModesMenu.AddGroupLabel("Combo");
                    _useQ = ModesMenu.Add("comboQ", new CheckBox("Use Smart Q"));
                    _useE = ModesMenu.Add("comboE", new CheckBox("Use Smart E to safety"));
                }

                public static void Initialize()
                {
                }
            }

            public static class Harass
            {
                private static readonly CheckBox _useQ;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }

                static Harass()
                {
                    // Initialize the menu values
                    ModesMenu.AddGroupLabel("Harass");
                    _useQ = ModesMenu.Add("harassQ", new CheckBox("Use Q"));
                }

                public static void Initialize()
                {
                }
            }

            public static class LaneClear
            {
                private static readonly CheckBox _useQ;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }

                static LaneClear()
                {
                    // Initialize the menu values
                    ModesMenu.AddGroupLabel("LaneClear");
                    _useQ = ModesMenu.Add("laneQ", new CheckBox("Use Q(Crappy atm)"));
                }

                public static void Initialize()
                {
                }
            }

            public static class LastHit
            {
                private static readonly CheckBox _useQ;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }

                static LastHit()
                {
                    // Initialize the menu values
                    ModesMenu.AddGroupLabel("LastHit");
                    _useQ = ModesMenu.Add("lastQ", new CheckBox("Use Smart Q Last Hit"));
                }

                public static void Initialize()
                {
                }
            }

            public static class Misc
            {
                private static readonly CheckBox _autoQ;
                private static readonly CheckBox _autoW;

                public static bool UseQCC
                {
                    get { return _autoQ.CurrentValue; }
                }

                public static bool UseWCC
                {
                    get { return _autoW.CurrentValue; }
                }
                
                static Misc()
                {
                    // Initialize the menu values
                    ModesMenu.AddGroupLabel("Misc");
                    _autoQ = ModesMenu.Add("autoQ", new CheckBox("Auto Q if target is CC'ed"));
                    _autoW = ModesMenu.Add("autoW", new CheckBox("Auto W if target is CC'ed"));
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