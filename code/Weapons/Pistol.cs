using Castles.Weapons.Base;
using Sandbox;

namespace Castles.Weapons
{
	[Library( "castles_pistol", Title = "Pistol" )]
	public class Pistol : ProjectileWeapon
	{
		public override string PrintName => "Pistol";
		public override AmmoType AmmoType => AmmoType.Pistol;
		
		public override int MagSize => 8;
		public override float ReloadTime => 2.0f;
		public override float Spread => 0.8f;
		public override float SpreadResetTime => 1f;
		public override float SpreadMoveMultiplier => 2f;
		public override float ProjectileDrop => 5f;
		public override float BaseDamage => 25f;
		
		public override string AttackSound => "rust_pistol.shoot";

		public override string MuzzleFlashParticle => "particles/pistol_muzzleflash.vpcf";
		public override string MuzzleFlashAttachment => "muzzle";

		public override string ViewModelPath => "weapons/rust_pistol/v_rust_pistol.vmdl";
		public override string WorldModel => "weapons/rust_pistol/rust_pistol.vmdl";

		public override string ProjectileModel => "weapons/shells/pistol_shell.vmdl";
		public override float ProjectileVelocity => 600f;
	}
}
