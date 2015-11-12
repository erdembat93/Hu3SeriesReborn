using EloBuddy.SDK.Events;

namespace YasuoHu3Reborn.EvadePlus
{
    internal class Program
    {
        public static SkillshotDetector _skillshotDetector;
        public static EvadePlus Evade;

        public static void Initialize()
        {
            Loading.OnLoadingComplete += delegate
            {
                _skillshotDetector = new SkillshotDetector();
                Evade = new EvadePlus(_skillshotDetector);
                EvadeMenu.CreateMenu();
            };
        }
    }
}
