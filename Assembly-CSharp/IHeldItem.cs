using System;

// Token: 0x02000688 RID: 1672
public interface IHeldItem : global::IInventoryItem
{
	// Token: 0x17000AF5 RID: 2805
	// (get) Token: 0x06003921 RID: 14625
	global::ViewModel viewModelInstance { get; }

	// Token: 0x17000AF6 RID: 2806
	// (get) Token: 0x06003922 RID: 14626
	// (set) Token: 0x06003923 RID: 14627
	global::ItemRepresentation itemRepresentation { get; set; }

	// Token: 0x17000AF7 RID: 2807
	// (get) Token: 0x06003924 RID: 14628
	bool canActivate { get; }

	// Token: 0x17000AF8 RID: 2808
	// (get) Token: 0x06003925 RID: 14629
	bool canDeactivate { get; }

	// Token: 0x06003926 RID: 14630
	void ItemPreFrame(ref global::HumanController.InputSample input);

	// Token: 0x06003927 RID: 14631
	void ItemPostFrame(ref global::HumanController.InputSample input);

	// Token: 0x06003928 RID: 14632
	void PreCameraRender();

	// Token: 0x17000AF9 RID: 2809
	// (get) Token: 0x06003929 RID: 14633
	global::ItemModFlags modFlags { get; }

	// Token: 0x17000AFA RID: 2810
	// (get) Token: 0x0600392A RID: 14634
	global::ItemModDataBlock[] itemMods { get; }

	// Token: 0x17000AFB RID: 2811
	// (get) Token: 0x0600392B RID: 14635
	int totalModSlots { get; }

	// Token: 0x17000AFC RID: 2812
	// (get) Token: 0x0600392C RID: 14636
	int usedModSlots { get; }

	// Token: 0x17000AFD RID: 2813
	// (get) Token: 0x0600392D RID: 14637
	int freeModSlots { get; }

	// Token: 0x0600392E RID: 14638
	void SetTotalModSlotCount(int count);

	// Token: 0x0600392F RID: 14639
	void SetUsedModSlotCount(int count);

	// Token: 0x06003930 RID: 14640
	void AddMod(global::ItemModDataBlock mod);

	// Token: 0x06003931 RID: 14641
	int FindMod(global::ItemModDataBlock mod);

	// Token: 0x06003932 RID: 14642
	void OnActivate();

	// Token: 0x06003933 RID: 14643
	void OnDeactivate();
}
