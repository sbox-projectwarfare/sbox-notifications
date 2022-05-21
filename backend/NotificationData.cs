/*
 * Notification data script code is under MIT License
 *
 * Copyright (c) 2022 s&box MilitaryRP
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

using System;
using System.IO;

using Sandbox;

using Warfare.UI.Notifications;

namespace Warfare.Notifications
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class NotificationDataAttribute : LibraryAttribute
    {
        public Type NotificationType { get; set; }

        public NotificationDataAttribute(string name, Type notificationType) : base("pw_notificationdata_" + name)
        {
            NotificationType = notificationType;
        }
    }

    [NotificationData("base", typeof(Notification)), Hammer.Skip]
    public partial class NotificationData
    {
        public string Title { get; set; }

        public string Message { get; set; }

        public string Name { get; set; }

        public string NotificationName { get; set; }

        public NotificationData()
        {
            LibraryAttribute libraryAttribute = TypeLibrary.GetDescription(GetType());

            Name = libraryAttribute.Name;

            if (libraryAttribute is NotificationDataAttribute notificationDataAttribute)
            {
                LibraryAttribute attribute = TypeLibrary.GetDescription(notificationDataAttribute.NotificationType);

                if (attribute is NotificationAttribute)
                {
                    NotificationName = attribute.Name;
                }
            }
        }

        protected virtual void WriteData(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(Name);
            binaryWriter.Write(NotificationName);
            binaryWriter.Write(Title ?? "");
            binaryWriter.Write(Message ?? "");
        }

        public byte[] Write()
        {
            using MemoryStream memoryStream = new();

            using (BinaryWriter binaryWriter = new(memoryStream))
            {
                WriteData(binaryWriter);
            }

            memoryStream.Flush();

            return memoryStream.GetBuffer();
        }

        protected virtual void ReadData(BinaryReader binaryReader)
        {
            Name = binaryReader.ReadString();
            NotificationName = binaryReader.ReadString();

            Title = binaryReader.ReadString();

            if (string.IsNullOrEmpty(Title))
            {
                Title = null;
            }

            Message = binaryReader.ReadString();

            if (string.IsNullOrEmpty(Message))
            {
                Message = null;
            }
        }

        public void Read(byte[] bytes)
        {
            using MemoryStream memoryStream = new(bytes);
            using BinaryReader binaryReader = new(memoryStream);

            ReadData(binaryReader);
        }
    }
}
