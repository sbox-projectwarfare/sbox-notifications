using System.IO;
using Sandbox;

namespace Warfare.Notifications
{
    public partial class NotificationData
    {
        public string Title { get; set; } = "Notification Title";

        public string Message { get; set; } = "Notification text here";

        protected virtual void WriteData(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(Title);
            binaryWriter.Write(Message);
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
            Title = binaryReader.ReadString();
            Message = binaryReader.ReadString();
        }

        public static NotificationData Read(byte[] bytes)
        {
            using MemoryStream memoryStream = new(bytes);
            using BinaryReader binaryReader = new(memoryStream);

            NotificationData notificationData = new();
            notificationData.ReadData(binaryReader);
            return notificationData;
        }
    }
}
