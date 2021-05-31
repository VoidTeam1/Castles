using Sandbox;

namespace Castles
{
	partial class CastlesPlayer : Player
	{
		/// <summary>
		/// Called when the player spawns.
		/// </summary>
		public override void Respawn()
		{
			SetModel( "models/citizen/citizen.vmdl" );
			
			// TODO: Probably use custom controllers later
			Controller = new WalkController();
			Animator = new StandardPlayerAnimator();
			Camera = new ThirdPersonCamera();
			
			EnableAllCollisions = true;
			EnableDrawing = true;
			EnableHideInFirstPerson = true;
			EnableShadowInFirstPerson = true;

			base.Respawn();
		}
		
		/// <summary>
		/// Called every tick, clientside and serverside.
		/// </summary>
		public override void Simulate( Client cl )
		{
			base.Simulate( cl );

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
