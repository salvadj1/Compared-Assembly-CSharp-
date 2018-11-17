using System;

// Token: 0x0200069C RID: 1692
public interface IMeleeWeaponItem : global::IHeldItem, global::IInventoryItem, global::IWeaponItem
{
	// Token: 0x17000B2F RID: 2863
	// (get) Token: 0x060039C6 RID: 14790
	// (set) Token: 0x060039C7 RID: 14791
	float queuedSwingAttackTime { get; set; }

	// Token: 0x060039C8 RID: 14792
	void QueueMidSwing(float time);

	// Token: 0x17000B30 RID: 2864
	// (get) Token: 0x060039C9 RID: 14793
	// (set) Token: 0x060039CA RID: 14794
	float queuedSwingSoundTime { get; set; }

	// Token: 0x060039CB RID: 14795
	void QueueSwingSound(float time);
}
