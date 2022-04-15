/*
 * This is an example game to show you how notification library works
 * It shows some test cases of using it in main game's script 
 * And on client
 *
 */

using System;
using System.Linq;

using Sandbox;

using Warfare.Notifications;

//
// You don't need to put things in a namespace, but it doesn't hurt.
//
namespace Warfare
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
		public NotificationManager NotificationManager;
		
		public MyGame() : base()
		{
			// Initialize notification manager
			NotificationManager = new NotificationManager();

			// Simple test game's UI
			_ = new Hud();
		}

		/// <summary>
		/// A client has joined the server. Make them a pawn to play with
		/// </summary>
		public override void ClientJoined(Client client)
		{
			base.ClientJoined(client);

			// Create a pawn for this client to play with
			var pawn = new Pawn();
			client.Pawn = pawn;

			// Get all of the spawnpoints
			var spawnpoints = All.OfType<SpawnPoint>();

			// chose a random one
			var randomSpawnPoint = spawnpoints.OrderBy(x => Guid.NewGuid()).FirstOrDefault();

			// if it exists, place the pawn there
			if (randomSpawnPoint != null)
			{
				var tx = randomSpawnPoint.Transform;
				tx.Position += Vector3.Up * 50.0f; // raise it up
				pawn.Transform = tx;
			}

			pawn.Spawn();
		}
	}
}
