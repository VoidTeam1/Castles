using Sandbox.UI;

namespace Castles.UI
{
	public partial class DebugHUD : Panel
	{
		public Label Text { get; set; }

		public DebugHUD()
		{
			StyleSheet.Load( "/ui/DebugHUD.scss" );
		}
	}
}
