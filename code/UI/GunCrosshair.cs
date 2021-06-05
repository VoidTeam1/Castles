using Castles.Weapons.Base;
using Sandbox;
using Sandbox.UI;

namespace Castles.UI
{
	public class GunCrosshair : Panel
	{
		public GunCrosshair()
		{
			StyleSheet.Load( "/ui/GunCrosshair.scss" );
		}

		float scale = 1;

		public override void Tick()
		{
			base.Tick();

			var player = Local.Pawn as GamePlayer;
			if ( player == null ) return;
			
			var weapon = player.LastAttackerWeapon as Weapon;

			if ( weapon is ProjectileWeapon projectileWeapon )
			{
				scale = scale.LerpTo(projectileWeapon.CalculateSpread(), Time.Delta * 10);
			}
			
			Style.Width = (40 * scale).Clamp(20, 300);
			Style.Height = (40 * scale).Clamp(20, 210);
			Style.Dirty();

			// scale = scale.LerpTo( 1, Time.Delta * 5 );

		}
	}
}
