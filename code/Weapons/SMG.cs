using Castles.Weapons.Base;
using Sandbox;

namespace Castles.Weapons
{
	[Library( "castles_smg", Title = "SMG" )]
	public class SMG : ProjectileWeapon
	{
		public override string PrintName => "SMG";
		public override AmmoType AmmoType => AmmoType.SemiAuto;
		public override float PrimaryRate => 11f;

		public override int MagSize => 24;
		public override float ReloadTime => 3.0f;
		public override float Spread => 1.3f;
		public override float SpreadResetTime => 1.5f;
		public override float SpreadMoveMultiplier => 2.3f;
		public override float ProjectileDrop => 5f;
		public override float BaseDamage => 22f;

		public override string MuzzleFlashParticle => "particles/pistol_muzzleflash.vpcf";
		public override string MuzzleFlashAttachment => "muzzle";

		public override string ViewModelPath => "weapons/rust_smg/v_rust_smg.vmdl";
		public override string WorldModel => "weapons/rust_smg/rust_smg.vmdl";

		public override int? HoldType => 2;

		public override string ProjectileModel => "weapons/shells/pistol_shell.vmdl";
		public override float ProjectileVelocity => 700f;
	}
}
