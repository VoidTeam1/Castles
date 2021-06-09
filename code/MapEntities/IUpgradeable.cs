namespace Castles.MapEntities
{
	public interface IUpgradeable
	{
		string ActivatedBy { get; set; }
		void Upgrade(int upgradeLevel);
	}
}
