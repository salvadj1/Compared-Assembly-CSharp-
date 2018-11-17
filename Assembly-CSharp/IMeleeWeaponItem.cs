using System;

// Token: 0x020005DE RID: 1502
public interface IMeleeWeaponItem : IHeldItem, IInventoryItem, IWeaponItem
{
	// Token: 0x17000AB9 RID: 2745
	// (get) Token: 0x060035FE RID: 13822
	// (set) Token: 0x060035FF RID: 13823
	float queuedSwingAttackTime { get; set; }

	// Token: 0x06003600 RID: 13824
	void QueueMidSwing(float time);

	// Token: 0x17000ABA RID: 2746
	// (get) Token: 0x06003601 RID: 13825
	// (set) Token: 0x06003602 RID: 13826
	float queuedSwingSoundTime { get; set; }

	// Token: 0x06003603 RID: 13827
	void QueueSwingSound(float time);
}
