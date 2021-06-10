using Castles.Weapons.Base;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Castles.UI
{
	public class Hotbar : Panel
	{
		private Label currentHealth;
		private Label maxHealth;
		private Label currentAmmo;
		private Label maxAmmo;
		
		public Hotbar()
		{
			currentAmmo = new Label();
			maxAmmo = new Label();
			
			var left = Add.Panel( "side-bg" );
			left.AddClass( "side-left" );
			
			var hpPanel = left.Add.Panel( "bar-item" );
			hpPanel.AddClass( "bar-item--hp" );
			var hpLeft = hpPanel.Add.Panel( "bar-left" );
			hpLeft.Add.Image(); // TODO: Add icon for health
			hpLeft.Add.Label( "HEALTH", "bar-title" );

			var hpRight = hpPanel.Add.Panel( "bar-right" );
			currentHealth = hpRight.Add.Label("100", "bar-current");
			maxHealth = hpRight.Add.Label( "/100", "bar-maximum" ); ;
			
			Add.Panel( "center-bg" );
			Add.Panel( "side-bg" );
			
			StyleSheet.Load( "UI/Hotbar.scss" );
		}

		/*
		private Panel CreateBar(Label current, Label max, string name)
		{
			var mainPanel = Add.Panel( "bar-item" );
			mainPanel.AddClass( $"{name}-bar" );
			
			var left = mainPanel.Add.Panel( "bar-left" );
			left.Add.Image(); // TODO: Add icon for health/armor
			left.Add.Label( $"{name.ToUpper()}", "bar-title" );

			var right = mainPanel.Add.Panel( "bar-right" );
			current = right.Add.Label("100", "bar-current");
			max = right.Add.Label( "/100", "bar-maximum" ); ;

			return mainPanel;
		}*/
		
		public override void Tick()
		{
			base.Tick();
			
			var player = Local.Pawn as GamePlayer;
			if ( player == null ) return;
			
			var hp = player.Health;
			var maxHP = 100; // TODO: Change this to an property later

			currentHealth.Text = hp.ToString();
			maxHealth.Text = "/" + maxHP;
			
			var currentWeapon = player.ActiveChild as ProjectileWeapon;
			if ( currentWeapon == null ) return;

			var totalAmmo = player.CurrentWeaponAmmo;
			var clipAmount = currentWeapon.AmmoClip;

			currentAmmo.Text = clipAmount.ToString();
			maxAmmo.Text = totalAmmo.ToString();
		}
	}


}
