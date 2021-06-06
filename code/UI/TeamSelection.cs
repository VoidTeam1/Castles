using System.Collections.Generic;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Castles.UI
{
	public class TeamSelection : Panel
	{
		private List<Label> Teams;
		
		public TeamSelection()
		{
			Add.Panel( "background" );
			
			var contentPanel = Add.Panel( "content" );
			contentPanel.Add.Label( "CHOOSE YOUR TEAM", "title" );
			
			var teamsPanel = contentPanel.Add.Panel( "teams" );
			
			foreach ( var team in Team.All )
			{
				var teamLabel = new Label { Text = team.Members.Count.ToString() };
				Teams.Add( teamLabel );
				AddTeamPanel( team.Name, teamLabel, teamsPanel);
			}

			var specPanel = contentPanel.Add.Panel( "spectator" );
			specPanel.Add.Label( "JOIN AS SPECTATOR", "spectator-text" );
			
			StyleSheet.Load( "UI/TeamSelection.scss" );
		}

		private void AddTeamPanel( string name, Label count, Panel parent )
		{
			var team = parent.Add.Panel( "team" );
			var title = team.Add.Panel( "team-title" );
			title.Add.Label( "TEAM" );
			title.Add.Label( $"{name.ToUpper()}", $"team-title--{name}");

			var box = team.Add.Panel( "team-box" );
			box.AddClass( $"team-box--{name}" );
			box.Add.Label( $"{count.Text} / 4", "team-box-count" );
		}

		private void Update()
		{
			foreach ( var label in Teams )
			{
				
			}
		}
	}
}
