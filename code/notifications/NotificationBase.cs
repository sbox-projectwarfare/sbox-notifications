using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Notifications {

	/// <summary>
	/// Base class of notification panel
	/// </summary>
	public class NotificationBase : Panel
	{
		/// <summary>
		/// Title for your notification
		/// </summary>
		public Label Title;

		/// <summary>
		/// Notification message under the title
		/// </summary>
		public Label Message;

		// Just to draw a UI shape in left from text
		private Label NotificationShape;

		/// <summary>
		/// How long notification will active
		/// by default notification will shown for 6 seconds
		/// </summary>
		private const float showTime = 6.0f;
		private float timeStamp = Sandbox.Time.Delta;

		public NotificationBase()
		{
			NotificationShape = Add.Label( " ", "shape" );
			Title = Add.Label( "Notification Title", "title" );
			Message = Add.Label( "Notification text here", "message" );
		}

		public override void Tick()
		{
			base.Tick();

			if ( Sandbox.Time.Now - timeStamp < showTime )
			{
				SetClass( "active", true );
				timeStamp = Sandbox.Time.Delta; // update ticks
			}
			else
			{
				// Hide notification
				SetClass( "unactive", true );

				Log.Info( "Notifications Library: Clearing notification from memory..." );
				Delete();
			}
		}
	}
}
