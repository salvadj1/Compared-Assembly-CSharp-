using System;

// Token: 0x020005EE RID: 1518
public interface IWeaponItem : IHeldItem, IInventoryItem
{
	// Token: 0x17000AD4 RID: 2772
	// (get) Token: 0x06003663 RID: 13923
	bool canPrimaryAttack { get; }

	// Token: 0x06003664 RID: 13924
	void PrimaryAttack(ref HumanController.InputSample sample);

	// Token: 0x17000AD5 RID: 2773
	// (get) Token: 0x06003665 RID: 13925
	bool canSecondaryAttack { get; }

	// Token: 0x06003666 RID: 13926
	void SecondaryAttack(ref HumanController.InputSample sample);

	// Token: 0x06003667 RID: 13927
	void Reload(ref HumanController.InputSample sample);

	// Token: 0x17000AD6 RID: 2774
	// (get) Token: 0x06003668 RID: 13928
	bool canAim { get; }

	// Token: 0x17000AD7 RID: 2775
	// (get) Token: 0x06003669 RID: 13929
	bool deployed { get; }

	// Token: 0x17000AD8 RID: 2776
	// (get) Token: 0x0600366A RID: 13930
	int possibleReloadCount { get; }

	// Token: 0x17000AD9 RID: 2777
	// (get) Token: 0x0600366B RID: 13931
	// (set) Token: 0x0600366C RID: 13932
	float nextPrimaryAttackTime { get; set; }

	// Token: 0x17000ADA RID: 2778
	// (get) Token: 0x0600366D RID: 13933
	// (set) Token: 0x0600366E RID: 13934
	float nextSecondaryAttackTime { get; set; }

	// Token: 0x17000ADB RID: 2779
	// (get) Token: 0x0600366F RID: 13935
	// (set) Token: 0x06003670 RID: 13936
	float deployFinishedTime { get; set; }

	// Token: 0x06003671 RID: 13937
	bool ValidatePrimaryMessageTime(double timestamp);
}
