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

using System;
using System.Collections.Generic;
using System.Linq;

using Sandbox;

namespace ProjectWarfare.Notifications
{
    /// <summary>
    /// Main class for working with notifications
    /// </summary>
    public partial class NotificationStack
    {
        public static NotificationStack Instance { get; set; }

        private Stack<NotificationData> Stack { get; set; } = new();

        public int Count
        {
            get => Stack.Count;
        }

        public IEnumerable<NotificationData> All
        {
            get => Stack.AsEnumerable();
        }

        public NotificationStack()
        {
            Instance = this;
        }

        [ClientRpc]
        public static void AddNotification(string libraryName, byte[] data)
        {
            Type notificationDataType = Library.Get<NotificationData>(libraryName);

            if (notificationDataType == null)
            {
                return;
            }

            NotificationData notificationData = Library.Create<NotificationData>(notificationDataType);

            notificationData.Read(data);

            Instance?.Stack.Push(notificationData);
        }

        public static void Push<T>(To to, T notificationData) where T : NotificationData
        {
            if (Host.IsServer)
            {
                AddNotification(to, notificationData.Name, notificationData.Write());
            }
            else
            {
                Instance?.Stack.Push(notificationData);
            }
        }

        public static void Push<T>(T notificationData) where T : NotificationData => Push(To.Everyone, notificationData);

        public NotificationData Pop() => Stack.Pop();
    }
}
