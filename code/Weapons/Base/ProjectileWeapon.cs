using Sandbox;

namespace Castles.Weapons.Base
{
	public partial class ProjectileWeapon : CastlesWeapon
	{
		public override string PrintName => "Base Projectile Weapon";
		public virtual AmmoType AmmoType => AmmoType.Pistol;
		
		public virtual int MagSize => 8;
		public virtual float ReloadTime => 3.0f;
		public virtual float Spread => 0.8f;
		public virtual float SpreadResetTime => 1f;
		public virtual float SpreadMoveMultiplier => 2f;
		public virtual float ProjectileDrop => 5f;
		public virtual float BaseDamage => 25f;

		public virtual string MuzzleFlashParticle => "particles/pistol_muzzleflash.vpcf";
		public virtual string MuzzleFlashAttachment => "muzzle";

		public virtual string ProjectileModel => "weapons/shells/pistol_shell.vmdl";
		public virtual float ProjectileVelocity => 600f;
		
		[Net, Predicted]
		public int AmmoClip { get; set; }
		
		[Net, Predicted]
		public bool IsReloading { get; set; }
		
		[Net, Predicted]
		public TimeSince TimeSinceReload { get; set; }

		[Net, Predicted]
		public TimeSince TimeSinceSpreadReset { get; set; }
		
		
		public override void Simulate( Client owner ) 
		{
			if ( TimeSinceDeployed < 0.6f )
				return;

			if ( !IsReloading )
			{
				base.Simulate( owner );
			}

			if ( IsReloading && TimeSinceReload > ReloadTime )
			{
				//OnReloadFinish();
			}
		}
		
		public override void AttackPrimary()
		{
			TimeSincePrimaryAttack = 0;
			TimeSinceSecondaryAttack = 0;
			
			ShootEffects();

			PlaySound( AttackSound );
			ShootBullet();
		}
		
		[ClientRpc]
		protected virtual void ShootEffects()
		{
			Host.AssertClient();

			Particles.Create( MuzzleFlashParticle, EffectEntity, MuzzleFlashAttachment );

			if ( IsLocalPawn )
			{
				_ = new Sandbox.ScreenShake.Perlin();
			}

			ViewModelEntity?.SetAnimBool( "fire", true );
			CrosshairPanel?.OnEvent( "fire" );
		}
		
		/// <summary>
		/// Shoots a single bullet
		/// </summary>
		public virtual void ShootBullet()
		{
			if (IsServer)
			{
				bool shouldApplySpread = ShouldApplySpread();

				var projectile = new Projectile
				{
					ProjectileModel = ProjectileModel, 
					ProjectileVelocity = ProjectileVelocity
				};

				projectile.Shoot( Owner as CastlesPlayer, this, shouldApplySpread ? CalculateSpread() : 0f );

				TimeSinceSpreadReset = 0;
			}
		}
		
		/// <summary>
		/// Returns if a weapon should apply spread
		/// </summary>
		public virtual bool ShouldApplySpread()
		{
			return TimeSinceSpreadReset < SpreadResetTime || CalculateSpread() > Spread;
		}

		/// <summary>
		/// Calculates the current weapon spread multiplier
		/// </summary>
		public virtual float CalculateSpread()
		{
			float spread = Spread;
			var walkController = ((Owner as CastlesPlayer)?.Controller as WalkController);
			bool isDucking = walkController != null && walkController.Duck.IsActive;

			if ( Owner.Velocity.Length > 0 && !isDucking )
			{
				spread *= SpreadMoveMultiplier;
			}

			if ( Owner.Velocity.Length < 1 && isDucking )
			{
				spread *= 0.8f;
			}
			
			return spread;
		}
	}
}
