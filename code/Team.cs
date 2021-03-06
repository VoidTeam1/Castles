using System.Collections.Generic;
using System.Linq;
using Sandbox;

namespace Castles
{
	[Library("team")]
	public partial class Team
	{
		public static List<Team> All { get; set; } = new List<Team>
		{
			new Team("Red", Color.Red),
			new Team("Blue", Color.Blue),
			new Team("Green", Color.Green),
			new Team("Yellow", Color.Yellow)
		};
		
		public string Name { get; set; }
		public Color Color { get; set; }
		

		public Team( string name, Color color )
		{
			Name = name;
			Color = color;
		}
		
		public List<GamePlayer> Members
		{
			get
			{
				return Entity.All
					.Where(x => (x as GamePlayer)?.Team == this)
					.Cast<GamePlayer>()
					.ToList();
			}
		}
		
		public void Join( Client client )
		{
			if ( client.Pawn is SpectatorPlayer )
			{
				PlayerScoreboard.PlayerLeftSpectatorRpc( client.SteamId );	
			}
			
			client.Pawn.Delete();

			var player = new GamePlayer();
			player.Team = this;
			client.Pawn = player;

			player.Respawn();
			player.ReceiveTeamData( Name );

			PlayerScoreboard.PlayerJoinedTeamRpc(To.Everyone, client.SteamId, Name);
		}

		public void Leave( Client client )
		{
			PlayerScoreboard.PlayerLeftTeamRpc(client.SteamId, Name);
		}

		[ServerCmd("join_team")]
		public static void JoinTeam(string teamName)
		{
			var team = All.FirstOrDefault( x => x.Name == teamName );
			if ( team == null ) return;
			if ( ConsoleSystem.Caller == null ) return;

			var caller = ConsoleSystem.Caller;
			if ( caller.Pawn is not SpectatorPlayer ) return;

			team.Join( caller );
		}

		public override string ToString() => Name;
	}
}
