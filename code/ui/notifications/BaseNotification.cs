/*
 * Class code is under MIT License
 *
 * Copyright (c) 2022 s&box MilitaryRP
 * Author: Val Zubko (5FB5)
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 *
*/

using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Warfare.UI.Notifications
{
	/// <summary>
	/// Base class of notification panel
	/// </summary>
	public class BaseNotification : Panel
	{
		/// <summary>
		/// How long notification will active
		/// by default notification will shown for 4.7 seconds
		/// </summary>
		private float showTime = 4.7f;

		/// <summary>
		/// Title for your notification
		/// </summary>
		public Label Title { get; set; }

		/// <summary>
		/// Notification message under the title
		/// </summary>
		public Label Message { get; set; }

		// Just to draw a UI shape in left from text
		public Label NotificationShape { get; set; }

		public BaseNotification()
		{
			Style.Dirty();
			NotificationShape = Add.Label(" ", "shape");
			Title = Add.Label("Notification Title", "title");
			Message = Add.Label("Notification text here", "message");
		}

		public override void Tick()
		{
			base.Tick();

			if (showTime > 0)
			{
				showTime -= Sandbox.Time.Delta;
			}
			else
			{
				Sandbox.Event.Run("NotificationManager.DeleteNotification", this);
			}
		}
	}
}
