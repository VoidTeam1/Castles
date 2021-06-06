using System.Collections.Generic;
using System.Linq;
using Sandbox;

namespace Castles
{
	public class Team
	{
		public string Name { get; set; }
		public Color Color { get; set; }

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
			client.Pawn.Delete();

			var player = new GamePlayer(this);
			client.Pawn = player;

			player.Respawn();
		}
	}
}
