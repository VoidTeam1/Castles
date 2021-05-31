using Sandbox;

namespace Castles.Weapons.Base
{
	public partial class CastlesViewModel : BaseViewModel
	{
		public override void PostCameraSetup( ref CameraSetup camSetup )
		{
			base.PostCameraSetup( ref camSetup );

			AddCameraEffects( ref camSetup );
		}

		private void AddCameraEffects( ref CameraSetup camSetup )
		{
			Rotation = Local.Pawn.EyeRot;

			// TODO: Add custom camera effects here
		}
	}
}
