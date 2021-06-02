using Castles.Weapons.Base;
using Sandbox;

namespace Castles.Weapons
{
	[Library( "castles_shotgun", Title = "Shotgun" )]
	public class Shotgun : ProjectileWeapon
	{
		public override string PrintName => "Shotgun";
		public override AmmoType AmmoType => AmmoType.Buckshot;
		public override float PrimaryRate => 1f;

		public override int MagSize => 6;
		public override float ReloadTime => 0.6f;
		public override float Spread => 4f;
		public override float SpreadResetTime => 0.6f;
		public override float SpreadMoveMultiplier => 0.3f;
		public override float ProjectileDrop => 15f;
		public override float BaseDamage => 8f;
		public override float SpraySpreadMultiplier => 1.4f;

		public override string AttackSound => "rust_pumpshotgun.shoot";

		public override string MuzzleFlashParticle => "particles/pistol_muzzleflash.vpcf";
		public override string MuzzleFlashAttachment => "muzzle";

		public override string ViewModelPath => "weapons/rust_pumpshotgun/v_rust_pumpshotgun.vmdl";
		public override string WorldModel => "weapons/rust_pumpshotgun/rust_pumpshotgun.vmdl";

		public override int? HoldType => 2;

		public override string ProjectileModel => "weapons/shells/pistol_shell.vmdl";
		public override float ProjectileVelocity => 700f;
		public override int FireNumBullets => 10;
		public override bool GradualReloading => true;
	}
}
