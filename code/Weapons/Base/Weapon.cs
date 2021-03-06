using Castles.UI;
using Sandbox;
using Sandbox.UI;

namespace Castles.Weapons.Base
{
	public partial class Weapon : BaseWeapon
	{
		public virtual string PrintName => "Base Castles Weapon";
		public virtual string AttackSound => "rust_pistol.shoot";
		public virtual string WorldModel => "weapons/rust_pistol/rust_pistol.vmdl";
		public virtual int? HoldType => null; 

		[Net, Predicted]
		public TimeSince TimeSinceDeployed { get; set; }
		
		public override void ActiveStart( Entity ent )
		{
			base.ActiveStart( ent );
			
			Owner.LastAttackerWeapon = this;
			TimeSinceDeployed = 0;
		}
		
		public override void Spawn()
		{
			base.Spawn();

			SetModel( WorldModel );
		}

		public override void CreateHudElements()
		{
			CrosshairPanel = new GunCrosshair();
			CrosshairCanvas.SetCrosshair( CrosshairPanel );
		}
		
		public override void CreateViewModel()
		{
			Host.AssertClient();

			if ( string.IsNullOrEmpty( ViewModelPath ) )
				return;

			ViewModelEntity = new ViewModel
			{
				Position = Position,
				Owner = Owner,
				EnableViewmodelRendering = true
			};
			
			ViewModelEntity.SetModel( ViewModelPath );
		}
		
		public override void SimulateAnimator( PawnAnimator anim )
		{
			if ( HoldType != null )
			{
				anim.SetParam( "holdtype", HoldType.Value );
				anim.SetParam( "aimat_weight", 1.0f );
			}
			else
			{
				base.SimulateAnimator( anim );
			}
		}
	}
}
