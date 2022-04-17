using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Warfare.UI
{
    public partial class Hud : HudEntity<RootPanel>
    {
        public Label MainText;

        public static Hud Instance { get; set; }

        public Hud()
        {
            Instance = this;

            RootPanel.StyleSheet.Load("ui/Hud.scss");
            MainText = RootPanel.Add.Label("For client:\n1 - Generic notification, 2 - Hint notification, 3 - Error notification", "hud-hint-text");

            RootPanel.AddChild<Notifications.NotificationWrapper>();
        }

        [Event.Hotload]
        public static void OnHotload()
        {
            Instance?.Delete();

            if (Host.IsClient)
            {
                _ = new Hud();
            }
        }
    }
}
