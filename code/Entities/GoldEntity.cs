using System.Linq;
using Castles.Extensions;
using Sandbox;
using Sandbox.Hammer;

namespace Castles.Entities
{
	[Library("gold")]
	public class GoldEntity : ModelEntity
	{
		public static SoundEvent PickupSound = new ( "sounds/studs/blue_pickup.vsnd" );
		public float RotationSpeed { get; set; } = 8f;
		public int GoldAmount { get; set; } = 1;

		public GoldEntity()
		{
			if ( !IsServer ) return;
			
			SetModel( "models/lego_stud/lego_stud.vmdl" );
			SetupPhysicsFromModel( PhysicsMotionType.Dynamic );
			PhysicsBody.GravityScale = 0f;

			Rotation = Rotation.From(0, 0, 90);
			CollisionGroup = CollisionGroup.Trigger;
			EnableSolidCollisions = false;
			EnableTouch = true;
			PhysicsBody.GravityScale = 0f;
		}
		
		[Event.Tick]
		public void Tick()
		{
			if ( IsClient ) return;
			Rotation *= Rotation.FromPitch( RotationSpeed );
		}

		public override void StartTouch(Entity entity)
		{
			base.StartTouch( entity );
			
			if ( entity is not GamePlayer gamePlayer ) return;
			if ( IsServer )
			{
				Delete();
				gamePlayer.AddGold( GoldAmount );
			}
			
			PlaySound( PickupSound.Name );
		}
	}
}
