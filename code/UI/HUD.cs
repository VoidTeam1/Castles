using Sandbox;
using Sandbox.UI;

namespace Castles.UI
{
	[Library]
	public class HUD : HudEntity<RootPanel>
	{
		public HUD()
		{
			if ( !IsClient )
				return;

			RootPanel.AddChild<NameTags>();
			RootPanel.AddChild<CrosshairCanvas>();
			RootPanel.AddChild<ChatBox>();
			RootPanel.AddChild<VoiceList>();
			RootPanel.AddChild<KillFeed>();
			RootPanel.AddChild<Scoreboard>();

			RootPanel.AddChild<AmmoCounter>();
		}
	}
}
