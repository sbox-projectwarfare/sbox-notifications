﻿using Sandbox;

using Warfare.Notifications;

namespace Warfare
{
    public partial class Pawn : AnimEntity
    {
        public static Model PlayerModel { get; set; } = Model.Load("models/sbox_props/watermelon/watermelon.vmdl");

        /// <summary>
        /// Called when the entity is first created
        /// </summary>
        ///
        public override void Spawn()
        {
            base.Spawn();

            Model = PlayerModel;
            EnableDrawing = true;
            EnableHideInFirstPerson = true;
            EnableShadowInFirstPerson = true;
        }

        /// <summary>
        /// Called every tick, clientside and serverside.
        /// </summary>
        public override void Simulate(Client cl)
        {
            base.Simulate(cl);

            if (IsServer)
            {
                using (Prediction.Off())
                {
                    if (Input.Released(InputButton.Slot1))
                    {
                        Log.Info("Pressed Slot 1 key on server");

                        NotificationManager.ShowNotification(NotificationType.Generic, new NotificationData()
                        {
                            Message = "Notification created by player on server!"
                        }.Write());
                    }
                }

                return;
            }

            if (Input.Released(InputButton.Slot2))
            {
                Log.Info("Pressed Slot 2 key on client");
                NotificationManager.ShowNotification(NotificationType.Hint, new NotificationData()
                {
                    Message = "Hint created by player on client!"
                }.Write());
            }

            if (Input.Released(InputButton.Slot3))
            {
                Log.Info("Pressed Slot 3 key");
                NotificationManager.ShowNotification(NotificationType.Error, new NotificationData()
                {
                    Message = "Error created by player on client!"
                }.Write());
            }
        }
    }
}