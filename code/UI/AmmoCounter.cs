using Castles.Weapons.Base;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Castles.UI
{
	public class AmmoCounter : Panel
	{
		public static AmmoCounter Instance { get; set; }
		public Label AmmoText { get; set; }
		
		public AmmoCounter()
		{
			AmmoText = Add.Label( "AMMO", "text-green-500" );
			
			StyleSheet.Load( "/ui/AmmoCounter.scss" );
			StyleSheet.Load( "/ui/Tailwind.scss" );

			Instance = this;
		}

		public override void Tick()
		{
			base.Tick();
			
			var player = Local.Pawn as GamePlayer;
			if ( player == null ) return;

			var currentWeapon = player.ActiveChild as ProjectileWeapon;
			if ( currentWeapon == null ) return;

			int totalAmmo = player.CurrentWeaponAmmo;
			int clipSize = currentWeapon.AmmoClip;

			AmmoText.Text = $"{clipSize}/{totalAmmo}";
		}
	}
}
