using System.Text;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Castles.UI
{
	public partial class DebugHUD : Panel
	{
		public Label Label { get; set; }

		public DebugHUD()
		{
			StyleSheet.Load( "/ui/DebugHUD.scss" );

			Label = Add.Label();
		}

		public string GetDebugText()
		{
			var builder = new StringBuilder();
			
			var player = Local.Pawn as Player;
			if ( player == null ) return "";

			var client = Local.Client;
			if ( client == null ) return "";

			builder.AppendLine( "Castles Debug Stats" );
			builder.AppendLine();

			bool isGamePlayer = player is GamePlayer;
			builder.AppendLine( "Is spectator: " + (!isGamePlayer ? "Yes" : "No") );

			if ( player is GamePlayer gamePlayer )
			{
				builder.AppendLine( "Team: " + gamePlayer.Team );
			}
			else
			{
				
			}
			
			return builder.ToString();
		}

		public override void Tick()
		{
			Label.Text = GetDebugText();
		}
	}
}
