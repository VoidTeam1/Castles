using System.Collections.Generic;
using System.Linq;
using Sandbox;

namespace Castles
{
	public class Team
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
			client.Pawn.Delete();

			var player = new GamePlayer(this);
			client.Pawn = player;

			player.Respawn();
		}

		public override string ToString() => Name;
	}
}
