using Sandbox;

namespace Castles.Weapons.Base
{
	public partial class Projectile : ModelEntity
	{
		public ProjectileWeapon Weapon { get; set; }
		
		public float ProjectileVelocity { get; set; } = 600f;
		public float PenetrationThickness => 100f;
		public float TotalPenetration { get; set; }
		
		public string ProjectileModel { get; set; } = "weapons/shells/pistol_shell.vmdl";
		public TimeSince TimeSinceShot { get; set; }


		public void Shoot( CastlesPlayer owner, ProjectileWeapon weapon, float spread = 0f )
		{
			Owner = owner;
			Weapon = weapon;

			SetModel( ProjectileModel );

			var fSpread = spread;
			var rotSpread = Rotation.From( Rand.Float( -fSpread, fSpread ), Rand.Float( -fSpread, fSpread ), Rand.Float( -fSpread, fSpread ) );

			Position = Owner.EyePos;
			Rotation = Owner.EyeRot * rotSpread;
			
			TimeSinceShot = 0;
		}
		
		private float CalculateBulletSpeed()
		{
			return ProjectileVelocity * 10 - TotalPenetration * 3;
		}

		private float CalculateBulletDrop()
		{
			return Weapon.ProjectileDrop * TimeSinceShot + TotalPenetration * 0.1f;
		}

		private float CalculateDamage()
		{
			return Weapon.BaseDamage;
		}

		private bool CheckPenetration(TraceResult tr)
		{
			Vector3 castStart = tr.EndPos;
			Vector3 castEnd = castStart + Rotation.Forward * PenetrationThickness;
			
			var thicknessTrace = Trace.Ray( castEnd, castStart )
				.UseHitboxes()
				.Ignore( Owner )
				.Ignore( this )
				.Size( 1.0f )
				.Run();

			float wallThickness = thicknessTrace.EndPos.Distance( castStart );
			if ( wallThickness < PenetrationThickness && (castStart - thicknessTrace.EndPos).LengthSquared > 3 )
			{
				thicknessTrace.Surface.DoBulletImpact( thicknessTrace );

				TotalPenetration += wallThickness;
				Position = thicknessTrace.EndPos;

				return true;
			}

			return false;
		}
		

		[Event.Tick]
		public virtual void Tick()
		{
			if ( !IsAuthority ) return;
			
			var velocity = Rotation.Forward * CalculateBulletSpeed();
			
			var start = Position;
			var end = start + velocity * Time.Delta;
			
			bool inWater = Physics.TestPointContents( start, CollisionLayer.Water );

			var tr = Trace.Ray( start, end )
				.UseHitboxes()
				.HitLayer( CollisionLayer.Water, !inWater )
				.Ignore( Owner )
				.Ignore( this )
				.Size( 1.0f )
				.Run();

			if ( Weapon != null )
			{
				end = end + Vector3.Down * CalculateBulletDrop();
			}

			if ( tr.Hit )
			{
				tr.Surface.DoBulletImpact( tr );
				bool penetrated = CheckPenetration( tr );

				if ( !IsServer ) return;
				
				if ( tr.Entity.IsValid() )
				{
					var damageInfo = DamageInfo.FromBullet( tr.EndPos, tr.Direction * 200, CalculateDamage() )
						.UsingTraceResult( tr )
						.WithAttacker( Owner )
						.WithWeapon( this );

					tr.Entity.TakeDamage( damageInfo );
				}
				
				if ( !penetrated )
				{
					Delete();
				}
			}
			else
			{
				Position = end;
			}
		}
	}
}
