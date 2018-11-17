using System;
using uLink;

// Token: 0x0200068C RID: 1676
public interface IInventoryItem
{
	// Token: 0x17000B09 RID: 2825
	// (get) Token: 0x0600395A RID: 14682
	global::ItemDataBlock datablock { get; }

	// Token: 0x17000B0A RID: 2826
	// (get) Token: 0x0600395B RID: 14683
	int slot { get; }

	// Token: 0x17000B0B RID: 2827
	// (get) Token: 0x0600395C RID: 14684
	float condition { get; }

	// Token: 0x17000B0C RID: 2828
	// (get) Token: 0x0600395D RID: 14685
	float maxcondition { get; }

	// Token: 0x0600395E RID: 14686
	bool IsDamaged();

	// Token: 0x0600395F RID: 14687
	bool IsBroken();

	// Token: 0x06003960 RID: 14688
	float GetConditionPercent();

	// Token: 0x17000B0D RID: 2829
	// (get) Token: 0x06003961 RID: 14689
	int uses { get; }

	// Token: 0x17000B0E RID: 2830
	// (get) Token: 0x06003962 RID: 14690
	global::Inventory inventory { get; }

	// Token: 0x17000B0F RID: 2831
	// (get) Token: 0x06003963 RID: 14691
	bool dirty { get; }

	// Token: 0x17000B10 RID: 2832
	// (get) Token: 0x06003964 RID: 14692
	// (set) Token: 0x06003965 RID: 14693
	float lastUseTime { get; set; }

	// Token: 0x17000B11 RID: 2833
	// (get) Token: 0x06003966 RID: 14694
	bool isInLocalInventory { get; }

	// Token: 0x17000B12 RID: 2834
	// (get) Token: 0x06003967 RID: 14695
	IDMain idMain { get; }

	// Token: 0x17000B13 RID: 2835
	// (get) Token: 0x06003968 RID: 14696
	global::Character character { get; }

	// Token: 0x17000B14 RID: 2836
	// (get) Token: 0x06003969 RID: 14697
	global::Controller controller { get; }

	// Token: 0x17000B15 RID: 2837
	// (get) Token: 0x0600396A RID: 14698
	global::Controllable controllable { get; }

	// Token: 0x17000B16 RID: 2838
	// (get) Token: 0x0600396B RID: 14699
	bool active { get; }

	// Token: 0x17000B17 RID: 2839
	// (get) Token: 0x0600396C RID: 14700
	string toolTip { get; }

	// Token: 0x0600396D RID: 14701
	int AddUses(int count);

	// Token: 0x0600396E RID: 14702
	void SetUses(int count);

	// Token: 0x0600396F RID: 14703
	void SetCondition(float condition);

	// Token: 0x06003970 RID: 14704
	void SetMaxCondition(float condition);

	// Token: 0x06003971 RID: 14705
	bool Consume(ref int count);

	// Token: 0x06003972 RID: 14706
	bool TryConditionLoss(float probability, float percentLoss);

	// Token: 0x06003973 RID: 14707
	void Serialize(BitStream stream);

	// Token: 0x06003974 RID: 14708
	void Deserialize(BitStream stream);

	// Token: 0x06003975 RID: 14709
	void OnMovedTo(global::Inventory inv, int slot);

	// Token: 0x06003976 RID: 14710
	void OnAddedTo(global::Inventory inv, int slot);

	// Token: 0x06003977 RID: 14711
	global::InventoryItem.MenuItemResult OnMenuOption(global::InventoryItem.MenuItem option);

	// Token: 0x17000B18 RID: 2840
	// (get) Token: 0x06003978 RID: 14712
	bool doNotSave { get; }

	// Token: 0x06003979 RID: 14713
	bool MarkDirty();

	// Token: 0x0600397A RID: 14714
	global::InventoryItem.MergeResult TryStack(global::IInventoryItem other);

	// Token: 0x0600397B RID: 14715
	global::InventoryItem.MergeResult TryCombine(global::IInventoryItem other);
}
