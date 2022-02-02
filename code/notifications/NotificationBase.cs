using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Notifications {

	/// <summary>
	/// Base class of notification panel
	/// </summary>
	public class NotificationBase : Panel
	{
		/// <summary>
		/// How long notification will active
		/// by default notification will shown for 6 seconds
		/// </summary>
		private const float showTime = 6.0f;
		private float timeStamp = Sandbox.Time.Delta;

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
		
		public NotificationBase()
		{
			Style.Dirty();
			NotificationShape = Add.Label( " ", "shape" );
			Title = Add.Label( "Notification Title", "title" );
			Message = Add.Label( "Notification text here", "message" );
		}

		public override void Tick()
		{
			base.Tick();

			//TODO: it will be nice to use TimeSince struct to not check game ticks by hand
			if ( Sandbox.Time.Now - timeStamp < showTime )
			{
				timeStamp = Sandbox.Time.Delta;
			}
			else
			{
				Sandbox.Event.Run( "NotificationManager.DeleteNotification", this );
			}
		}
	}
}
