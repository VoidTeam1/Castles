using Sandbox;

namespace Castles.Extensions
{
	public static class BBoxExtensions
	{
		public static bool Contains( this BBox bBox, Vector3 vec )
		{
			Vector3 mins = bBox.Mins;
			Vector3 maxs = bBox.Maxs;

			return (vec.x >= mins.x && vec.y >= mins.y && vec.z >= mins.z) && (vec.x <= maxs.x && vec.y <= maxs.y && vec.z <= maxs.z);
		}
	}
}
