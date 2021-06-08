using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Castles.UI
{
	public class GoldHUD : Panel
	{
		public Label GoldLabel { get; set; }
		
		public GoldHUD()
		{
			StyleSheet.Load( "/ui/GoldHUD.scss" );

			GoldLabel = Add.Label("GOLD");
		}

		public override void Tick()
		{
			var pawn = Local.Pawn;
			if ( pawn == null ) return;
			if ( pawn is not GamePlayer gamePlayer ) return;

			GoldLabel.Text = $"{gamePlayer.Gold} gold";
		}
	}
}
