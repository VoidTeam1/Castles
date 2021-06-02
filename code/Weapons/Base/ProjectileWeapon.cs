using System;
using Castles.UI;
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
		public virtual float MaxSpread => 10f;
		public virtual float SpraySpreadMultiplier => 1f;

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
		public TimeSince TimeSinceSprayStart { get; set; }

		public override void Simulate( Client owner ) 
		{
			if ( TimeSinceSpreadReset > SpreadResetTime )
			{
				TimeSinceSprayStart = 0;
			}

			if ( TimeSinceDeployed < 0.6f )
				return;

			if ( !IsReloading )
			{
				base.Simulate( owner );
			}

			if ( IsReloading && TimeSinceReload > ReloadTime )
			{
				OnReloadFinish();
			}
		}

		public override void Reload()
		{
			if ( IsReloading ) return;
			if ( AmmoClip >= MagSize ) return;

			if ( ((CastlesPlayer) Owner).CurrentWeaponAmmo < 1 ) return;

			TimeSinceReload = 0;
			IsReloading = true;

			StartReloadEffects();
		}
		
		public virtual void OnReloadFinish()
		{
			IsReloading = false;

			if ( Owner is CastlesPlayer player )
			{
				var ammo = player.TakeAmmo( AmmoType, MagSize - AmmoClip );
				if ( ammo == 0 )
					return;

				AmmoClip += ammo;
			}
		}
		
		[ClientRpc]
		public virtual void StartReloadEffects()
		{
			ViewModelEntity?.SetAnimBool( "reload", true );
			(Owner as AnimEntity)?.SetAnimBool( "b_reload", true );
		}
		
		public override void AttackPrimary()
		{
			if ( !TakeAmmo() ) return;
			
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
				var projectile = new Projectile
				{
					ProjectileModel = ProjectileModel, 
					ProjectileVelocity = ProjectileVelocity
				};

				projectile.Shoot( Owner as CastlesPlayer, this, CalculateSpread() );
			}
			
			TimeSinceSpreadReset = 0;
		}
		
		/// <summary>
		/// Returns if a weapon should apply spread
		/// </summary>
		public virtual bool ShouldApplySpread()
		{
			return TimeSinceSpreadReset < SpreadResetTime;
		}

		/// <summary>
		/// Calculates the current weapon spread multiplier
		/// </summary>
		public virtual float CalculateSpread()
		{
			float spread = Spread;
			var walkController = ((Owner as CastlesPlayer)?.Controller as WalkController);
			bool isDucking = walkController != null && walkController.Duck.IsActive;
			
			if ( !ShouldApplySpread() )
			{
				spread = 0.2f;
			}

			if ( Owner.Velocity.Length > 0 && !isDucking )
			{
				spread += (SpreadMoveMultiplier / 100) * Owner.Velocity.Length;
			}

			if ( Owner.Velocity.Length < 1 && isDucking )
			{
				spread *= 0.8f;
			}

			if ( TimeSinceSprayStart > 0.1f )
			{
				spread *= Math.Max( (TimeSinceSprayStart + 1) * SpraySpreadMultiplier - 0.5f, 1 );
			}

			spread = spread.Clamp( 0, MaxSpread );

			return spread;
		}

		public bool TakeAmmo( int amount = 1 )
		{
			if ( AmmoClip < amount )
				return false;

			AmmoClip -= amount;
			return true;
		}
	}
}
