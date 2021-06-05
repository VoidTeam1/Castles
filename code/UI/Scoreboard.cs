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
		}

		public override void Tick()
		{
			base.Tick();
			
			SetClass( "open", Local.Client.Input.Down( InputButton.Score ) );
		}
	}
}
