using System;

// Token: 0x020005CA RID: 1482
public interface IHeldItem : IInventoryItem
{
	// Token: 0x17000A7F RID: 2687
	// (get) Token: 0x06003559 RID: 13657
	ViewModel viewModelInstance { get; }

	// Token: 0x17000A80 RID: 2688
	// (get) Token: 0x0600355A RID: 13658
	// (set) Token: 0x0600355B RID: 13659
	ItemRepresentation itemRepresentation { get; set; }

	// Token: 0x17000A81 RID: 2689
	// (get) Token: 0x0600355C RID: 13660
	bool canActivate { get; }

	// Token: 0x17000A82 RID: 2690
	// (get) Token: 0x0600355D RID: 13661
	bool canDeactivate { get; }

	// Token: 0x0600355E RID: 13662
	void ItemPreFrame(ref HumanController.InputSample input);

	// Token: 0x0600355F RID: 13663
	void ItemPostFrame(ref HumanController.InputSample input);

	// Token: 0x06003560 RID: 13664
	void PreCameraRender();

	// Token: 0x17000A83 RID: 2691
	// (get) Token: 0x06003561 RID: 13665
	ItemModFlags modFlags { get; }

	// Token: 0x17000A84 RID: 2692
	// (get) Token: 0x06003562 RID: 13666
	ItemModDataBlock[] itemMods { get; }

	// Token: 0x17000A85 RID: 2693
	// (get) Token: 0x06003563 RID: 13667
	int totalModSlots { get; }

	// Token: 0x17000A86 RID: 2694
	// (get) Token: 0x06003564 RID: 13668
	int usedModSlots { get; }

	// Token: 0x17000A87 RID: 2695
	// (get) Token: 0x06003565 RID: 13669
	int freeModSlots { get; }

	// Token: 0x06003566 RID: 13670
	void SetTotalModSlotCount(int count);

	// Token: 0x06003567 RID: 13671
	void SetUsedModSlotCount(int count);

	// Token: 0x06003568 RID: 13672
	void AddMod(ItemModDataBlock mod);

	// Token: 0x06003569 RID: 13673
	int FindMod(ItemModDataBlock mod);

	// Token: 0x0600356A RID: 13674
	void OnActivate();

	// Token: 0x0600356B RID: 13675
	void OnDeactivate();
}
