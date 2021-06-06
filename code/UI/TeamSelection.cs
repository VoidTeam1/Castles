using System.Collections.Generic;
using System.Linq;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Castles.UI
{
	public class TeamSelection : Panel
	{
		private Dictionary<string, Label> Teams = new();
		
		public TeamSelection()
		{
			Add.Panel( "background" );
			
			var contentPanel = Add.Panel( "content" );
			contentPanel.Add.Label( "CHOOSE YOUR TEAM", "title" );
			
			var teamsPanel = contentPanel.Add.Panel( "teams" );
			
			foreach ( var team in Team.All )
			{
				Teams[team.Name] = AddTeamPanel( team.Name, team.Members.Count, teamsPanel);
			}

			var specPanel = contentPanel.Add.Panel( "spectator" );
			specPanel.Add.Button( "JOIN AS SPECTATOR", "spectator-text", CloseMenu);

			PlayerScoreboard.PlayerJoinedTeam += ( _, _ ) =>
			{
				Update();
			};
			
			PlayerScoreboard.PlayerLeftTeam += ( _, _ ) =>
			{
				Update();
			};
			
			StyleSheet.Load( "UI/TeamSelection.scss" );
		}

		private Label AddTeamPanel( string name, int count, Panel parent )
		{
			var team = parent.Add.Button( "", "team", () => SelectTeam( name ));
			var title = team.Add.Panel( "team-title" );
			title.Add.Label( "TEAM " );
			title.Add.Label( $"{name.ToUpper()}", $"team-title--{name}");

			var box = team.Add.Panel( "team-box" );
			box.AddClass( $"team-box--{name}" );
			return box.Add.Label( $"{count} / 4", "team-box-count" );
		}

		private void SelectTeam(string teamName)
		{
			ConsoleSystem.Run( $"join_team {teamName}" );
			CloseMenu();
		}

		private void CloseMenu()
		{
			Style.Display = DisplayMode.None;
		}
		
		private void Update()
		{
			foreach ( var teamPair in Teams )
			{
				var team = Team.All.FirstOrDefault( x => x.Name == teamPair.Key );
				teamPair.Value.Text = $"{team.Members.Count} / 4";
			}
		}
	}
}
