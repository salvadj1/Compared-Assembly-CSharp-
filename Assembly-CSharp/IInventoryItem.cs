using System;
using uLink;

// Token: 0x020005CE RID: 1486
public interface IInventoryItem
{
	// Token: 0x17000A93 RID: 2707
	// (get) Token: 0x06003592 RID: 13714
	ItemDataBlock datablock { get; }

	// Token: 0x17000A94 RID: 2708
	// (get) Token: 0x06003593 RID: 13715
	int slot { get; }

	// Token: 0x17000A95 RID: 2709
	// (get) Token: 0x06003594 RID: 13716
	float condition { get; }

	// Token: 0x17000A96 RID: 2710
	// (get) Token: 0x06003595 RID: 13717
	float maxcondition { get; }

	// Token: 0x06003596 RID: 13718
	bool IsDamaged();

	// Token: 0x06003597 RID: 13719
	bool IsBroken();

	// Token: 0x06003598 RID: 13720
	float GetConditionPercent();

	// Token: 0x17000A97 RID: 2711
	// (get) Token: 0x06003599 RID: 13721
	int uses { get; }

	// Token: 0x17000A98 RID: 2712
	// (get) Token: 0x0600359A RID: 13722
	Inventory inventory { get; }

	// Token: 0x17000A99 RID: 2713
	// (get) Token: 0x0600359B RID: 13723
	bool dirty { get; }

	// Token: 0x17000A9A RID: 2714
	// (get) Token: 0x0600359C RID: 13724
	// (set) Token: 0x0600359D RID: 13725
	float lastUseTime { get; set; }

	// Token: 0x17000A9B RID: 2715
	// (get) Token: 0x0600359E RID: 13726
	bool isInLocalInventory { get; }

	// Token: 0x17000A9C RID: 2716
	// (get) Token: 0x0600359F RID: 13727
	IDMain idMain { get; }

	// Token: 0x17000A9D RID: 2717
	// (get) Token: 0x060035A0 RID: 13728
	Character character { get; }

	// Token: 0x17000A9E RID: 2718
	// (get) Token: 0x060035A1 RID: 13729
	Controller controller { get; }

	// Token: 0x17000A9F RID: 2719
	// (get) Token: 0x060035A2 RID: 13730
	Controllable controllable { get; }

	// Token: 0x17000AA0 RID: 2720
	// (get) Token: 0x060035A3 RID: 13731
	bool active { get; }

	// Token: 0x17000AA1 RID: 2721
	// (get) Token: 0x060035A4 RID: 13732
	string toolTip { get; }

	// Token: 0x060035A5 RID: 13733
	int AddUses(int count);

	// Token: 0x060035A6 RID: 13734
	void SetUses(int count);

	// Token: 0x060035A7 RID: 13735
	void SetCondition(float condition);

	// Token: 0x060035A8 RID: 13736
	void SetMaxCondition(float condition);

	// Token: 0x060035A9 RID: 13737
	bool Consume(ref int count);

	// Token: 0x060035AA RID: 13738
	bool TryConditionLoss(float probability, float percentLoss);

	// Token: 0x060035AB RID: 13739
	void Serialize(BitStream stream);

	// Token: 0x060035AC RID: 13740
	void Deserialize(BitStream stream);

	// Token: 0x060035AD RID: 13741
	void OnMovedTo(Inventory inv, int slot);

	// Token: 0x060035AE RID: 13742
	void OnAddedTo(Inventory inv, int slot);

	// Token: 0x060035AF RID: 13743
	InventoryItem.MenuItemResult OnMenuOption(InventoryItem.MenuItem option);

	// Token: 0x17000AA2 RID: 2722
	// (get) Token: 0x060035B0 RID: 13744
	bool doNotSave { get; }

	// Token: 0x060035B1 RID: 13745
	bool MarkDirty();

	// Token: 0x060035B2 RID: 13746
	InventoryItem.MergeResult TryStack(IInventoryItem other);

	// Token: 0x060035B3 RID: 13747
	InventoryItem.MergeResult TryCombine(IInventoryItem other);
}
