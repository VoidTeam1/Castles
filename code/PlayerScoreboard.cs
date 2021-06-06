using System;
using Sandbox;

namespace Castles
{
	public partial class PlayerScoreboard
	{
		public static Action<Player, string> PlayerJoinedTeam { get; set; } = ( _, _ ) => { };
		public static Action<Player, string> PlayerLeftTeam { get; set; } = ( _, _ ) => { };
		public static Action<Player> PlayerJoinedSpectator { get; set; } = _ => {};
		public static Action<Player> PlayerLeftSpectator { get; set; } = _ => {};

		[ClientRpc]
		public static void PlayerJoinedTeamRpc( Entity entity, string team ) => PlayerJoinedTeam( entity as Player, team );
		
		[ClientRpc]
		public static void PlayerLeftTeamRpc( Entity entity, string team ) => PlayerLeftTeam( entity as Player, team );
		
		[ClientRpc]
		public static void PlayerJoinedSpectatorRpc( Entity entity ) => PlayerJoinedSpectator( entity as Player );
		
		[ClientRpc]
		public static void PlayerLeftSpectatorRpc( Entity entity ) => PlayerLeftSpectator( entity as Player );

	}
}
