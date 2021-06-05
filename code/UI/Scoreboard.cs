using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Castles.UI
{
	public class Scoreboard : Panel
	{
		public Scoreboard()
		{
			StyleSheet.Load( "/ui/Scoreboard.scss" );

			Add.Label( "CASTLES SCOREBOARD", "title" );
			
			var team = Add.Panel("team");
			team.Add.Label( "TEAM RED", "team-header--red" );
			
			var teamContent = team.Add.Panel( "team-content" );
			teamContent.Add.Label( "PLAYERNAME", "team-entry--red" );
		}

		public override void Tick()
		{
			base.Tick();
			
			SetClass( "open", Local.Client.Input.Down( InputButton.Score ) );
		}
	}
}
