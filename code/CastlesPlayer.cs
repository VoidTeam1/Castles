using Castles.Weapons;
using Sandbox;

namespace Castles
{
	public partial class CastlesPlayer : Player
	{
		public CastlesPlayer()
		{
			Inventory = new PlayerInventory( this );
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

			base.Respawn();

			Inventory.Add( new Pistol(), true );
		}
		
		/// <summary>
		/// Called every tick, clientside and serverside.
		/// </summary>
		public override void Simulate( Client cl )
		{
			base.Simulate( cl );
			
			if ( Input.ActiveChild != null )
			{
				ActiveChild = Input.ActiveChild;
			}

			// This simulates an active weapon
			SimulateActiveChild( cl, ActiveChild );
		}

		/// <summary>
		/// Called when the player is killed.
		/// </summary>
		public override void OnKilled()
		{
			base.OnKilled();
			
			EnableDrawing = false;
		}
	}
}
