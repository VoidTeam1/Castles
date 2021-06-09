using System.Linq;
using System.Numerics;
using Sandbox;
using Sandbox.Hammer;

namespace Castles.MapEntities
{
	[Library("castles_payment", Description = "Payment for castles upgrades.")]
	public partial class PaymentEntity : ModelEntity
	{
		[Property("payment_type", Title = "Payment Type")]
		public PaymentType PaymentType { get; set; }

		[Property( "max_level", Title = "Max Level" )]
		public int MaxLevel { get; set; } = 1;
		public int CurrentLevel { get; set; } = 0;
		
		private bool IsSetup { get; set; }
		private Vector3 RayPosition { get; set; }

		public bool Pay(GamePlayer player)
		{
			int price = GetUpgradePrice();
			
			if ( CurrentLevel == MaxLevel ) return false;
			if ( !player.CanAfford( price ) ) return false;

			player.TakeGold( price );
			CurrentLevel++;

			foreach ( var ent in All.Where( x => x is IUpgradeable upgradeable && upgradeable.ActivatedBy == EntityName ).Cast<IUpgradeable>() )
			{
				ent.Upgrade( CurrentLevel );
			}

			return true;
		}
		
		public void Setup()
		{
			var ray = Trace.Ray( Position, Position * Vector3.Down * 1000 )
				.WorldOnly()
				.Run();

			if ( !ray.Hit )
			{
				Log.Error( $"Failed to setup payment entity {EntityName}!" );
				return;
			}

			RayPosition = ray.EndPos;
			IsSetup = true;
		}

		[Event.Frame]
		public void Frame()
		{
			var pawn = Local.Pawn as GamePlayer;
			if (pawn == null) return;

			if ( !IsSetup )
			{
				Setup();
			}

			DebugOverlay.Circle( RayPosition + Vector3.Up * 1, Rotation.From(90, 0, 0), 25, Color.Green );
			
			var distance = (Position - pawn.Position).LengthSquared;
			if ( distance < 40000 )
			{
				DebugOverlay.Text( RayPosition + Vector3.Up * 30,  "Press E to upgrade", Color.White );
			}
		}

		public override void Simulate( Client cl )
		{
			base.Simulate( cl );

			if ( IsClient ) return;

			var pawn = cl.Pawn as GamePlayer;
			if ( pawn == null ) return;

			if ( !Input.Pressed( InputButton.Use ) ) return;
				
			var distance = (Position - pawn.Position).LengthSquared;

			if ( distance < 20000 )
			{
				Pay( pawn );
			}

		}

		public int GetUpgradePrice()
		{
			// TODO: Different price calculations
			return CurrentLevel * 10;
		}
	}
}
