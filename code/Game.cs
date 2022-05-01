/*
 * This is an example game to show you how notification library works
 * It shows some test cases of using it in main game's script
 * And on client
 *
 */

using System;
using System.Linq;

using Sandbox;

using ProjectWarfare.Notifications;
using ProjectWarfare.UI;

//
// You don't need to put things in a namespace, but it doesn't hurt.
//
namespace ProjectWarfare
{
    /// <summary>
    /// This is your game class. This is an entity that is created serverside when
    /// the game starts, and is replicated to the client.
    ///
    /// You can use this to create things like HUDs and declare which player class
    /// to use for spawned players.
    /// </summary>
    public partial class MyGame : Game
    {
        public MyGame() : base()
        {
            if (IsClient)
            {
                _ = new NotificationStack();
                _ = new Hud();
            }
        }

        /// <summary>
        /// A client has joined the server. Make them a pawn to play with
        /// </summary>
        public override void ClientJoined(Client client)
        {
            base.ClientJoined(client);

            // Create a pawn for this client to play with
            Pawn pawn = new();
            client.Pawn = pawn;

            // chose a random one
            SpawnPoint randomSpawnPoint = All.OfType<SpawnPoint>().OrderBy(x => Guid.NewGuid()).FirstOrDefault();

            // if it exists, place the pawn there
            if (randomSpawnPoint != null)
            {
                Transform tx = randomSpawnPoint.Transform;
                tx.Position += Vector3.Up * 50.0f; // raise it up
                pawn.Transform = tx;
            }

            pawn.Spawn();
        }
    }
}
