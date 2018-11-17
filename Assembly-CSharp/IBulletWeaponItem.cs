using System;

// Token: 0x020005BE RID: 1470
public interface IBulletWeaponItem : IHeldItem, IInventoryItem, IWeaponItem
{
	// Token: 0x17000A74 RID: 2676
	// (get) Token: 0x0600352C RID: 13612
	MagazineDataBlock clipType { get; }

	// Token: 0x17000A75 RID: 2677
	// (get) Token: 0x0600352D RID: 13613
	// (set) Token: 0x0600352E RID: 13614
	int clipAmmo { get; set; }

	// Token: 0x17000A76 RID: 2678
	// (get) Token: 0x0600352F RID: 13615
	// (set) Token: 0x06003530 RID: 13616
	int cachedCasings { get; set; }

	// Token: 0x17000A77 RID: 2679
	// (get) Token: 0x06003531 RID: 13617
	// (set) Token: 0x06003532 RID: 13618
	float nextCasingsTime { get; set; }

	// Token: 0x06003533 RID: 13619
	void ActualReload();
}
