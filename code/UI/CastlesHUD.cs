using Sandbox;
using Sandbox.UI;

namespace Castles.UI
{
	[Library]
	public class CastlesHUD : HudEntity<RootPanel>
	{
		public CastlesHUD()
		{
			if ( !IsClient )
				return;

			RootPanel.AddChild<NameTags>();
			RootPanel.AddChild<CrosshairCanvas>();
			RootPanel.AddChild<ChatBox>();
			RootPanel.AddChild<VoiceList>();
			RootPanel.AddChild<KillFeed>();

			RootPanel.AddChild<AmmoCounter>();
		}
	}
}
