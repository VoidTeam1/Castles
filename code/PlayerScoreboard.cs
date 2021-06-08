using System;
using System.Linq;
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
		public static void PlayerJoinedTeamRpc( ulong steamId, string team )
		{
			var client = Client.All.FirstOrDefault( x => x.SteamId == steamId );
			if ( client == null ) return;
			
			PlayerJoinedTeam.Invoke( client, team );
			Log.Info( client.Name );
		}
		
		[ClientRpc]
		public static void PlayerLeftTeamRpc( ulong steamId, string team )
		{
			var client = Client.All.FirstOrDefault( x => x.SteamId == steamId );
			if ( client == null ) return;
			
			PlayerLeftTeam.Invoke( client, team );
		}

		[ClientRpc]
		public static void PlayerJoinedSpectatorRpc( ulong steamId )
		{
			var client = Client.All.FirstOrDefault( x => x.SteamId == steamId );
			if ( client == null ) return;
			
			PlayerJoinedSpectator.Invoke( client );
		}

		[ClientRpc]
		public static void PlayerLeftSpectatorRpc( ulong steamId )
		{
			var client = Client.All.FirstOrDefault( x => x.SteamId == steamId );
			if ( client == null ) return;
			
			PlayerLeftSpectator.Invoke( client );
		}

	}
}
