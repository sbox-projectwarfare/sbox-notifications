using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Warfare.Notifications
{
	public partial class Hud : HudEntity<RootPanel> 
	{
		public Label MainText;

		public static Hud Instance { get; set; }

		public Hud() : base()
		{
			Instance = this;

			if (!IsClient)
			{
				return;
			}

			RootPanel.StyleSheet.Load("Hud.scss");
			MainText = RootPanel.Add.Label("For client:\n1 - Generic notification, 2 - Hint notification, 3 - Error notification", "hud-hint-text");
		}

		[Event.Hotload]
		public static void OnHotload()
		{
			Instance?.Delete();

			_ = new Hud();
		}
	}
}
