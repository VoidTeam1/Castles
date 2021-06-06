
using Castles.UI;
using Castles.Weapons.Base;
using Sandbox;
using Sandbox.UI;
using Steamworks;

namespace Castles
{
	/// <summary>
	/// This is your game class. This is an entity that is created serverside when
	/// the game starts, and is replicated to the client. 
	/// 
	/// You can use this to create things like HUDs and declare which player class
	/// to use for spawned players.
	/// 
	/// Your game needs to be registered (using [Library] here) with the same name 
	/// as your game addon. If it isn't then we won't be able to find it.
	/// </summary>
	[Library( "castles" )]
	public partial class Game : Sandbox.Game
	{
		public Game()
		{
			if ( IsServer )
			{
				_ = new HUD();
			}

			if ( IsClient )
			{
				
			}
		}

		/// <summary>
		/// A client has joined the server. Make them a pawn to play with
		/// </summary>
		public override void ClientJoined( Client client )
		{
			base.ClientJoined( client );

			var player = new SpectatorPlayer();
			client.Pawn = player;

			player.Respawn();
			
			// TODO: Proper team selection later
			Rand.FromArray(Team.All.ToArray()).Join( client );
		}
		
		/// <summary>
		/// An entity, which is a pawn, and has a client, has been killed.
		/// </summary>
		public override void OnKilled( Client client, Entity pawn )
		{
			Host.AssertServer();

			Log.Info( $"{client.Name} was killed" );

			if ( pawn.LastAttacker != null )
			{
				var attackerClient = pawn.LastAttacker.GetClientOwner();

				if ( attackerClient != null )
				{
					var lastWeapon = attackerClient.Pawn.LastAttackerWeapon as Weapon;
					OnKilledMessage( attackerClient.SteamId, attackerClient.Name, client.SteamId, client.Name, lastWeapon?.PrintName ?? "Projectile" );
				}
				else
				{
					OnKilledMessage( (ulong)pawn.LastAttacker.NetworkIdent, pawn.LastAttacker.ToString(), client.SteamId, client.Name, "killed" );
				}
			}
			else
			{
				OnKilledMessage( 0, client.Name, client.SteamId, "", "died" );
			}
		}
	}
}
