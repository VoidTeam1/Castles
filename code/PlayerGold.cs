using Sandbox;

namespace Castles
{
	public partial class GamePlayer
	{
		[Net]
		public int Gold { get; set; }

		public bool CanAfford( int gold ) => Gold >= gold;

		public void AddGold( int gold )
		{
			Gold += gold;
		}

		public bool TakeGold( int gold )
		{
			if ( !CanAfford( gold ) ) return false;
			Gold -= gold;

			return true;
		}
	}
}
