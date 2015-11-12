using System.Linq;
using EloBuddy;
using EloBuddy.SDK;

using Settings = AddonTemplate.Config.Modes.LaneClear;

namespace AddonTemplate.Modes
{
    public sealed class JungleClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear);
        }

        public override void Execute()
        {
            if (Q.IsReady() && Settings.UseQ)
            {
                var minionq =
                    EntityManager.MinionsAndMonsters.GetJungleMonsters().FirstOrDefault(
                        m => m.IsValidTarget(Q.Range));

                if (minionq != null)
                {
                    Q.Cast(minionq);
                }
            }

            if (Program.CanW)
            {
                W.Cast();
            }
        }
    }
}
