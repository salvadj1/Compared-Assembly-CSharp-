using System;
using InventoryExtensions;
using uLink;
using UnityEngine;

// Token: 0x02000689 RID: 1673
public abstract class HeldItem<T> : global::InventoryItem<T> where T : global::HeldItemDataBlock
{
	// Token: 0x06003934 RID: 14644 RVA: 0x000CA890 File Offset: 0x000C8A90
	public HeldItem(T datablock) : base(datablock)
	{
	}

	// Token: 0x17000AFE RID: 2814
	// (get) Token: 0x06003935 RID: 14645 RVA: 0x000CA8A8 File Offset: 0x000C8AA8
	// (set) Token: 0x06003936 RID: 14646 RVA: 0x000CA8B0 File Offset: 0x000C8AB0
	public int totalModSlots { get; private set; }

	// Token: 0x17000AFF RID: 2815
	// (get) Token: 0x06003937 RID: 14647 RVA: 0x000CA8BC File Offset: 0x000C8ABC
	// (set) Token: 0x06003938 RID: 14648 RVA: 0x000CA8C4 File Offset: 0x000C8AC4
	public int usedModSlots { get; private set; }

	// Token: 0x17000B00 RID: 2816
	// (get) Token: 0x06003939 RID: 14649 RVA: 0x000CA8D0 File Offset: 0x000C8AD0
	public int freeModSlots
	{
		get
		{
			return this.totalModSlots - this.usedModSlots;
		}
	}

	// Token: 0x17000B01 RID: 2817
	// (get) Token: 0x0600393A RID: 14650 RVA: 0x000CA8E0 File Offset: 0x000C8AE0
	public global::ItemModDataBlock[] itemMods
	{
		get
		{
			return this._itemMods;
		}
	}

	// Token: 0x17000B02 RID: 2818
	// (get) Token: 0x0600393B RID: 14651 RVA: 0x000CA8E8 File Offset: 0x000C8AE8
	// (set) Token: 0x0600393C RID: 14652 RVA: 0x000CA8F0 File Offset: 0x000C8AF0
	public global::ViewModel viewModelInstance
	{
		get
		{
			return this._vm;
		}
		protected set
		{
			this._vm = value;
		}
	}

	// Token: 0x17000B03 RID: 2819
	// (get) Token: 0x0600393D RID: 14653 RVA: 0x000CA8FC File Offset: 0x000C8AFC
	// (set) Token: 0x0600393E RID: 14654 RVA: 0x000CA904 File Offset: 0x000C8B04
	public global::ItemRepresentation itemRepresentation
	{
		get
		{
			return this._itemRep;
		}
		set
		{
			this.SetItemRepresentation(value);
		}
	}

	// Token: 0x17000B04 RID: 2820
	// (get) Token: 0x0600393F RID: 14655 RVA: 0x000CA910 File Offset: 0x000C8B10
	public bool canActivate
	{
		get
		{
			return this.CanSetActivate(true);
		}
	}

	// Token: 0x17000B05 RID: 2821
	// (get) Token: 0x06003940 RID: 14656 RVA: 0x000CA91C File Offset: 0x000C8B1C
	public bool canDeactivate
	{
		get
		{
			return this.CanSetActivate(false);
		}
	}

	// Token: 0x06003941 RID: 14657 RVA: 0x000CA928 File Offset: 0x000C8B28
	protected virtual bool CanSetActivate(bool value)
	{
		return !value || !base.IsBroken();
	}

	// Token: 0x06003942 RID: 14658 RVA: 0x000CA940 File Offset: 0x000C8B40
	protected virtual void SetItemRepresentation(global::ItemRepresentation itemRep)
	{
		this._itemRep = itemRep;
		if (this._itemRep)
		{
			if (this._itemRep.datablock != this.datablock)
			{
				Debug.Log("yea the code below wasn't pointless..");
				this._itemRep.SetDataBlockFromHeldItem<T>(this);
			}
			this._itemRep.SetParent(base.inventory.gameObject);
		}
	}

	// Token: 0x06003943 RID: 14659 RVA: 0x000CA9B0 File Offset: 0x000C8BB0
	protected virtual void CreateViewModel()
	{
		this.DestroyViewModel();
		if (this.datablock._viewModelPrefab == null || global::actor.forceThirdPerson)
		{
			return;
		}
		this._vm = (global::ViewModel)Object.Instantiate(this.datablock._viewModelPrefab);
		this._vm.PlayDeployAnimation();
		if (this.datablock.deploySound)
		{
			this.datablock.deploySound.Play(1f);
		}
		global::CameraFX.ReplaceViewModel(this._vm, this._itemRep, this.iface as global::IHeldItem, false);
	}

	// Token: 0x06003944 RID: 14660 RVA: 0x000CAA68 File Offset: 0x000C8C68
	protected virtual void DestroyViewModel()
	{
		if (this._vm)
		{
			global::CameraFX.RemoveViewModel(ref this._vm, true, false);
		}
	}

	// Token: 0x06003945 RID: 14661 RVA: 0x000CAA88 File Offset: 0x000C8C88
	public void OnActivate()
	{
		this.OnSetActive(true);
	}

	// Token: 0x06003946 RID: 14662 RVA: 0x000CAA94 File Offset: 0x000C8C94
	public void OnDeactivate()
	{
		this.OnSetActive(false);
	}

	// Token: 0x06003947 RID: 14663 RVA: 0x000CAAA0 File Offset: 0x000C8CA0
	protected virtual void OnSetActive(bool isActive)
	{
		if (isActive)
		{
			this.CreateViewModel();
		}
		else
		{
			this.DestroyViewModel();
		}
	}

	// Token: 0x06003948 RID: 14664 RVA: 0x000CAABC File Offset: 0x000C8CBC
	public override void OnMovedTo(global::Inventory toInv, int toSlot)
	{
		if (base.active)
		{
			base.inventory.DeactivateItem();
		}
	}

	// Token: 0x17000B06 RID: 2822
	// (get) Token: 0x06003949 RID: 14665 RVA: 0x000CAAD4 File Offset: 0x000C8CD4
	public bool canAim
	{
		get
		{
			return this.CanAim();
		}
	}

	// Token: 0x0600394A RID: 14666 RVA: 0x000CAADC File Offset: 0x000C8CDC
	protected virtual bool CanAim()
	{
		return true;
	}

	// Token: 0x0600394B RID: 14667 RVA: 0x000CAAE0 File Offset: 0x000C8CE0
	public virtual void ItemPreFrame(ref global::HumanController.InputSample sample)
	{
		if (sample.attack2 && this.datablock.secondaryFireAims && this.CanAim())
		{
			sample.attack2 = false;
			sample.aim = true;
			sample.yaw *= this.datablock.aimSensitivtyPercent;
			sample.pitch *= this.datablock.aimSensitivtyPercent;
		}
	}

	// Token: 0x0600394C RID: 14668 RVA: 0x000CAB60 File Offset: 0x000C8D60
	public virtual void ItemPostFrame(ref global::HumanController.InputSample sample)
	{
	}

	// Token: 0x0600394D RID: 14669 RVA: 0x000CAB64 File Offset: 0x000C8D64
	private void RecalculateMods()
	{
		int num = 0;
		for (int i = 0; i < 5; i++)
		{
			if (this._itemMods[i] != null)
			{
				num++;
			}
		}
		this.usedModSlots = num;
	}

	// Token: 0x0600394E RID: 14670 RVA: 0x000CABA4 File Offset: 0x000C8DA4
	public void AddMod(global::ItemModDataBlock mod)
	{
		this.RecalculateMods();
		int usedModSlots = this.usedModSlots;
		this._itemMods[usedModSlots] = mod;
		this.RecalculateMods();
		this.OnModAdded(mod);
		base.MarkDirty();
	}

	// Token: 0x0600394F RID: 14671 RVA: 0x000CABDC File Offset: 0x000C8DDC
	public int FindMod(global::ItemModDataBlock mod)
	{
		if (mod)
		{
			for (int i = 0; i < 5; i++)
			{
				if (this._itemMods[i] == mod)
				{
					return i;
				}
			}
		}
		return -1;
	}

	// Token: 0x06003950 RID: 14672 RVA: 0x000CAC1C File Offset: 0x000C8E1C
	protected virtual void OnModAdded(global::ItemModDataBlock mod)
	{
	}

	// Token: 0x06003951 RID: 14673 RVA: 0x000CAC20 File Offset: 0x000C8E20
	public virtual void PreCameraRender()
	{
	}

	// Token: 0x17000B07 RID: 2823
	// (get) Token: 0x06003952 RID: 14674 RVA: 0x000CAC24 File Offset: 0x000C8E24
	public global::ItemModFlags modFlags
	{
		get
		{
			global::ItemModFlags itemModFlags = global::ItemModFlags.Other;
			if (this._itemMods != null)
			{
				foreach (global::ItemModDataBlock itemModDataBlock in this._itemMods)
				{
					if (itemModDataBlock != null)
					{
						itemModFlags |= itemModDataBlock.modFlag;
					}
				}
			}
			return itemModFlags;
		}
	}

	// Token: 0x06003953 RID: 14675 RVA: 0x000CAC74 File Offset: 0x000C8E74
	public void SetTotalModSlotCount(int count)
	{
		this.totalModSlots = count;
	}

	// Token: 0x06003954 RID: 14676 RVA: 0x000CAC80 File Offset: 0x000C8E80
	public void SetUsedModSlotCount(int count)
	{
		this.usedModSlots = count;
	}

	// Token: 0x06003955 RID: 14677 RVA: 0x000CAC8C File Offset: 0x000C8E8C
	protected override void OnBitStreamWrite(BitStream stream)
	{
		base.OnBitStreamWrite(stream);
		stream.WriteInvInt(this.totalModSlots);
		int usedModSlots = this.usedModSlots;
		stream.WriteInvInt(usedModSlots);
		for (int i = 0; i < usedModSlots; i++)
		{
			stream.WriteInt32(this._itemMods[i].uniqueID);
		}
	}

	// Token: 0x06003956 RID: 14678 RVA: 0x000CACE0 File Offset: 0x000C8EE0
	protected override void OnBitStreamRead(BitStream stream)
	{
		base.OnBitStreamRead(stream);
		this.SetTotalModSlotCount(stream.ReadInvInt());
		this.SetUsedModSlotCount(stream.ReadInvInt());
		int usedModSlots = this.usedModSlots;
		for (int i = 0; i < 5; i++)
		{
			if (i < usedModSlots)
			{
				this._itemMods[i] = (global::DatablockDictionary.GetByUniqueID(stream.ReadInt32()) as global::ItemModDataBlock);
			}
			else
			{
				this._itemMods[i] = null;
			}
		}
	}

	// Token: 0x06003957 RID: 14679 RVA: 0x000CAD54 File Offset: 0x000C8F54
	public override void ConditionChanged(float oldCondition)
	{
	}

	// Token: 0x04001C42 RID: 7234
	protected global::ItemModDataBlock[] _itemMods = new global::ItemModDataBlock[5];

	// Token: 0x04001C43 RID: 7235
	private global::ViewModel _vm;

	// Token: 0x04001C44 RID: 7236
	private global::ItemRepresentation _itemRep;
}
