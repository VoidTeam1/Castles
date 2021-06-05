using Sandbox;

namespace Castles
{
	public class SpectatorPlayer : Player
	{

		public override void Respawn()
		{
			SetModel( "models/ghost.vmdl" );
			
			Controller = new NoclipController();
			Animator = new StandardPlayerAnimator();
			Camera = new FirstPersonCamera();
			
			EnableDrawing = false;
			EnableHideInFirstPerson = true;
			EnableShadowInFirstPerson = true;
			EnableAllCollisions = false;

			base.Respawn();
		}
		
	}
}
