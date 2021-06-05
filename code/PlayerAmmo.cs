using System;
using System.Collections.Generic;
using Castles.Weapons.Base;
using Sandbox;

namespace Castles
{
	public partial class GamePlayer
	{
		[Net]
		private List<int> AmmoList { get; set; } = new();

		public int CurrentWeaponAmmo
		{
			get
			{
				var weapon = ActiveChild as ProjectileWeapon;
				if ( weapon == null ) return 0;

				return AmmoCount( weapon.AmmoType );
			}
		}

		public void ClearAmmo()
		{
			AmmoList.Clear();
		}

		public int AmmoCount( AmmoType type )
		{
			var iType = (int)type;
			if ( AmmoList == null ) return 0;
			if ( AmmoList.Count <= iType ) return 0;

			return AmmoList[(int)type];
		}

		public bool SetAmmo( AmmoType type, int amount )
		{
			var iType = (int)type;
			if ( !Host.IsServer ) return false;
			if ( AmmoList == null ) return false;

			while ( AmmoList.Count <= iType )
			{
				AmmoList.Add( 0 );
			}

			AmmoList[(int)type] = amount;
			return true;
		}

		public bool GiveAmmo( AmmoType type, int amount )
		{
			if ( !Host.IsServer ) return false;
			if ( AmmoList == null ) return false;

			SetAmmo( type, AmmoCount( type ) + amount );
			return true;
		}

		public int TakeAmmo( AmmoType type, int amount )
		{
			if ( AmmoList == null ) return 0;

			var available = AmmoCount( type );
			amount = Math.Min( available, amount );

			SetAmmo( type, available - amount );
			return amount;
		}
	}
}
