using Sandbox;

namespace Castles.Weapons.Base
{
	public partial class CastlesWeapon : BaseWeapon
	{
		public virtual string PrintName => "Base Castles Weapon";
		public virtual string AttackSound => "rust_pistol.shoot";

		[Net, Predicted]
		public TimeSince TimeSinceDeployed { get; set; }
		
		public override void ActiveStart( Entity ent )
		{
			base.ActiveStart( ent );

			TimeSinceDeployed = 0;
		}
		
		public override void CreateViewModel()
		{
			Host.AssertClient();

			if ( string.IsNullOrEmpty( ViewModelPath ) )
				return;
			
			Log.Info( "Creating viewmodel" );

			ViewModelEntity = new CastlesViewModel
			{
				Position = Position,
				Owner = Owner,
				EnableViewmodelRendering = true
			};
			
			ViewModelEntity.SetModel( ViewModelPath );
		}
	}
}
