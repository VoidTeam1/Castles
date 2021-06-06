using System.Collections.Generic;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace Castles.UI
{
	public class Scoreboard : Panel
	{
		// TODO: Make a class ScoreboardEntry and save each entry in .. idk what 
		private Dictionary<string, Panel> scoreboard;

		public Scoreboard()
		{
			StyleSheet.Load( "/ui/Scoreboard.scss" );

			Add.Label( "CASTLES SCOREBOARD", "title" );

			var teams = Add.Panel( "teams" );

			foreach ( var team in Team.All )
			{
				scoreboard[team.Name] = AddTeam( teams, team );
			}

			PlayerScoreboard.PlayerJoinedTeam += ( client, teamName ) =>
			{
				var teamPanel = scoreboard[teamName];
				AddPlayer( teamPanel, client.Name, teamName );
			};
			
			
			/* TODO: make this work
			PlayerScoreboard.PlayerLeftTeam += ( client, teamName ) =>
			{
				var teamPanel = scoreboard[teamName];
				AddPlayer( teamPanel, client.Name, teamName );
			};
			*/
		}

		private Panel AddTeam(Panel parent, Team team)
		{
			var teamPanel = Add.Panel("team");
			teamPanel.Add.Label( $"TEAM {team.Name}", $"team-header--{team.Name}" );
			var teamContent = teamPanel.Add.Panel( "team-content" );

			foreach ( var player in team.Members )
			{
				AddPlayer( teamContent, player.GetClientOwner().Name, player.Team.Name );
			}

			return teamContent;
		}

		private void AddPlayer(Panel parent, string playerName, string teamName)
		{
			parent.Add.Label(playerName, $"team-entry--{teamName}" );
		}

		public override void Tick()
		{
			base.Tick();
			
			SetClass( "open", Local.Client.Input.Down( InputButton.Score ) );
		}
	}
}
