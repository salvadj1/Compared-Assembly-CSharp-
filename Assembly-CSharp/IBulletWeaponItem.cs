using System;

// Token: 0x0200067C RID: 1660
public interface IBulletWeaponItem : global::IHeldItem, global::IInventoryItem, global::IWeaponItem
{
	// Token: 0x17000AEA RID: 2794
	// (get) Token: 0x060038F4 RID: 14580
	global::MagazineDataBlock clipType { get; }

	// Token: 0x17000AEB RID: 2795
	// (get) Token: 0x060038F5 RID: 14581
	// (set) Token: 0x060038F6 RID: 14582
	int clipAmmo { get; set; }

	// Token: 0x17000AEC RID: 2796
	// (get) Token: 0x060038F7 RID: 14583
	// (set) Token: 0x060038F8 RID: 14584
	int cachedCasings { get; set; }

	// Token: 0x17000AED RID: 2797
	// (get) Token: 0x060038F9 RID: 14585
	// (set) Token: 0x060038FA RID: 14586
	float nextCasingsTime { get; set; }

	// Token: 0x060038FB RID: 14587
	void ActualReload();
}
