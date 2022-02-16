using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

[Library]
public partial class GameUi: HudEntity<RootPanel> 
{
	public GameUi()
	{
		if ( !IsClient )
			return;

		RootPanel.StyleSheet.Load( "ExampleGameUi.scss" );
		RootPanel.AddChild<MainPanel>();
	}

	public class MainPanel : Panel
	{
		public Label MainText;

		public MainPanel()
		{
			MainText = Add.Label( "For client:\n1 - Generic notification, 2 - Hint notification, 3 - Error notification", "text" );
		}
	}
}

