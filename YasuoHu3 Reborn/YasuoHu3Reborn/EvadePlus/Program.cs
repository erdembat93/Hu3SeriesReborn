using EloBuddy.SDK.Events;

namespace YasuoHu3Reborn.EvadePlus
{
    internal static class Program
    {
        private static SkillshotDetector _skillshotDetector;
        private static EvadePlus _evade;

        public static void Initialize()
        {
            Loading.OnLoadingComplete += delegate
            {
                _skillshotDetector = new SkillshotDetector();
                _evade = new EvadePlus(_skillshotDetector);
                EvadeMenu.CreateMenu();
            };
        }
    }
}
