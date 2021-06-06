using System;
using Sandbox;

namespace Castles
{
	public partial class PlayerScoreboard
	{
		public static Action<Client, string> PlayerJoinedTeam { get; set; } = ( _, _ ) => { };
		public static Action<Client, string> PlayerLeftTeam { get; set; } = ( _, _ ) => { };
		public static Action<Client> PlayerJoinedSpectator { get; set; } = _ => {};
		public static Action<Client> PlayerLeftSpectator { get; set; } = _ => {};

		[ClientRpc]
		public static void PlayerJoinedTeamRpc( Client entity, string team )
		{
			PlayerJoinedTeam.Invoke( entity, team );
		}
		
		[ClientRpc]
		public static void PlayerLeftTeamRpc( Client entity, string team )
		{
			PlayerLeftTeam.Invoke( entity, team );
		}

		[ClientRpc]
		public static void PlayerJoinedSpectatorRpc( Client entity )
		{
			PlayerJoinedSpectator.Invoke( entity );
		}

		[ClientRpc]
		public static void PlayerLeftSpectatorRpc( Client entity )
		{
			PlayerLeftSpectator.Invoke( entity );
		}

	}
}
