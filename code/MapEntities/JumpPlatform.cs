using Sandbox;

namespace Castles.MapEntities
{
	[Library("jump_platform", Description = "A jump platform that extends itself vertically.")]
	public class JumpPlatform : FuncBrush, IUpgradeable
	{
		[Property("start_height")]
		public int StartHeight { get; set; } 
		
		[Property("activated_height")]
		public int ActivatedHeight { get; set; }
		
		[Property("activated_by")]
		public string ActivatedBy { get; set; }

		[Property( "activate_speed" )] 
		public float ActivateSpeed { get; set; } = 2f;
		
		public bool Activated { get; set; }

		public void Upgrade(int upgradeLevel)
		{
			Activated = true;
		}

		[Event.Tick]
		public void Tick()
		{
			if ( !IsServer || !Activated ) return;
			
			var desiredPos = Position.WithZ( ActivatedHeight );
			Position = Position.LerpTo( desiredPos,  ActivateSpeed * Time.Delta);
		}
	}
}
