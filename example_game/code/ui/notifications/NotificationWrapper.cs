/*
 * Notification wrapper code is under MIT License
 *
 * Copyright (c) 2022 S&box Project Warfare
 * 
 * Author: Val Zubko (5FB5)
 * Contributors: Alf21
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

using Sandbox;
using Sandbox.UI;

using Warfare.Notifications;

namespace Warfare.UI.Notifications
{
    public partial class NotificationWrapper : Panel
    {
        /// <summary>
        /// How much panels will be shown on the screen
        /// </summary>
        public const int NOTIFICATION_LIMIT = 6;

        public NotificationWrapper()
        {
            AddClass("notification-wrapper");
        }

        public override void Tick()
        {
            base.Tick();

            NotificationStack notificationStack = NotificationStack.Instance;

            for (int i = ChildrenCount - 1; i < NOTIFICATION_LIMIT - 1; i++)
            {
                if (notificationStack.Count == 0)
                {
                    break;
                }

                NotificationData notificationData = notificationStack.Pop();

                Notification baseNotification = TypeLibrary.Create<Notification>(notificationData.NotificationName);
                baseNotification.Data = notificationData;

                AddChild(baseNotification);

                baseNotification.IsDisplayed = true;
            }
        }
    }
}
