using Sandbox;

namespace Castles
{
	public class SpectatorPlayer : Player
	{

		public override void Respawn()
		{
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
