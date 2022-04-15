/*
 * Notification manager script code is under MIT License
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

using System.Collections.Generic;

using Sandbox;

using Warfare.UI.Notifications;

#pragma warning disable IDE0051

namespace Warfare.Notifications
{

    /// <summary>
    /// Types of available notifications
    /// </summary>
    public enum NotificationType
    {
        Generic,
        Hint,
        Error
    }

    /// <summary>
    /// Main class for working with notifications
    /// </summary>
    public partial class NotificationManager
    {
        public static NotificationManager Instance { get; set; }

        private List<BaseNotification> NotificationList { get; set; } = new();

        /// <summary>
        /// How much panels will be shown on the screen
        /// </summary>
        private const int NOTIFICATION_LIMIT = 6;

        public NotificationManager()
        {
            Instance = this;
        }

        [Event("NotificationManager.DeleteNotification")]
        private void OnDeleteNotification(BaseNotification notification)
        {
            Assert.NotNull(notification);

            foreach (BaseNotification notificationFromList in NotificationList)
            {
                if (notification == notificationFromList)
                {
                    NotificationList.Remove(notificationFromList); // delete it from list and itself
                }
            }
        }

        /// <summary>
        /// Returns an instance of panel from enum.
        /// Should be updated for custom panel type
        /// </summary>
        private static BaseNotification GetTypeFromEnum(NotificationType type) => type switch
        {
            NotificationType.Generic => new GenericNotification(),
            NotificationType.Hint => new HintNotification(),
            NotificationType.Error => new ErrorNotification(),
            _ => null
        };

        [ClientRpc]
        public static void ShowNotification(NotificationType type, byte[] data)
        {
            BaseNotification baseNotification = GetTypeFromEnum(type);
            baseNotification.Data = NotificationData.Read(data);

            Assert.NotNull(baseNotification);

            Instance?.NotificationList.Add(baseNotification);

            UI.Hud.Instance?.RootPanel.AddChild(baseNotification);
        }
    }
}
