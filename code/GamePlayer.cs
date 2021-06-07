using System.Linq;
using Castles.UI;
using Castles.Weapons;
using Castles.Weapons.Base;
using Sandbox;
using Steamworks;

namespace Castles
{
	public partial class GamePlayer : Player
	{
		[Net]
		public Team Team { get; set; }

		public DamageInfo LastDamage { get; set; }

		public bool Alive => Health > 0;

		public GamePlayer()
		{
			Inventory = new PlayerInventory( this );
		}

		public GamePlayer(Team team)
		{
			Inventory = new PlayerInventory( this );
			Team = team;
		}
		
		/// <summary>
		/// Called when the player spawns.
		/// </summary>
		public override void Respawn()
		{
			SetModel( "models/citizen/citizen.vmdl" );

			// TODO: Probably use custom controllers later
			Controller = new WalkController();
			Animator = new StandardPlayerAnimator();
			Camera = new FirstPersonCamera();
			
			EnableAllCollisions = true;
			EnableDrawing = true;
			EnableHideInFirstPerson = true;
			EnableShadowInFirstPerson = true;
			EnableAllCollisions = true;

			base.Respawn();

			Inventory.Add( new Shotgun(), true );
			// Inventory.Add( new Pistol(), true );

			GiveAmmo( AmmoType.Pistol, 120 );
			GiveAmmo( AmmoType.SemiAuto, 120 );
			GiveAmmo( AmmoType.Buckshot, 120 );

		}
		
		/// <summary>
		/// Called every tick, clientside and serverside.
		/// </summary>
		public override void Simulate( Client cl )
		{
			base.Simulate( cl );
			if ( !Alive ) return;
			
			if ( Input.ActiveChild != null )
			{
				ActiveChild = Input.ActiveChild;
			}

			// This simulates an active weapon
			SimulateActiveChild( cl, ActiveChild );
		}

		public override void TakeDamage( DamageInfo info )
		{
			LastDamage = info;

			// hack - hitbox 5 is head
			if ( info.HitboxIndex == 5 )
			{
				info.Damage *= 2.5f;
			}

			base.TakeDamage( info );
		}

		/// <summary>
		/// Called when the player is killed.
		/// </summary>
		public override void OnKilled()
		{
			base.OnKilled();
			
			EnableDrawing = false;
			EnableAllCollisions = false;

			BecomeRagdollOnClient( LastDamage.Force, GetHitboxBone( LastDamage.HitboxIndex ) );
			Camera = new SpectateRagdollCamera();

			ClearAmmo();
		}

		/// <summary>
		/// Gets the player's team from server.
		/// </summary>
		[ClientRpc]
		public void ReceiveTeamData( string name )
		{
			Host.AssertClient();
			
			Team = Team.All.FirstOrDefault( x => x.Name == name );
			Event.Run( "Castles.TeamReceived", Team );
		}

	}
}
