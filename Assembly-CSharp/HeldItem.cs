using System;
using InventoryExtensions;
using uLink;
using UnityEngine;

// Token: 0x020005CB RID: 1483
public abstract class HeldItem<T> : InventoryItem<T> where T : HeldItemDataBlock
{
	// Token: 0x0600356C RID: 13676 RVA: 0x000C2634 File Offset: 0x000C0834
	public HeldItem(T datablock) : base(datablock)
	{
	}

	// Token: 0x17000A88 RID: 2696
	// (get) Token: 0x0600356D RID: 13677 RVA: 0x000C264C File Offset: 0x000C084C
	// (set) Token: 0x0600356E RID: 13678 RVA: 0x000C2654 File Offset: 0x000C0854
	public int totalModSlots { get; private set; }

	// Token: 0x17000A89 RID: 2697
	// (get) Token: 0x0600356F RID: 13679 RVA: 0x000C2660 File Offset: 0x000C0860
	// (set) Token: 0x06003570 RID: 13680 RVA: 0x000C2668 File Offset: 0x000C0868
	public int usedModSlots { get; private set; }

	// Token: 0x17000A8A RID: 2698
	// (get) Token: 0x06003571 RID: 13681 RVA: 0x000C2674 File Offset: 0x000C0874
	public int freeModSlots
	{
		get
		{
			return this.totalModSlots - this.usedModSlots;
		}
	}

	// Token: 0x17000A8B RID: 2699
	// (get) Token: 0x06003572 RID: 13682 RVA: 0x000C2684 File Offset: 0x000C0884
	public ItemModDataBlock[] itemMods
	{
		get
		{
			return this._itemMods;
		}
	}

	// Token: 0x17000A8C RID: 2700
	// (get) Token: 0x06003573 RID: 13683 RVA: 0x000C268C File Offset: 0x000C088C
	// (set) Token: 0x06003574 RID: 13684 RVA: 0x000C2694 File Offset: 0x000C0894
	public ViewModel viewModelInstance
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

	// Token: 0x17000A8D RID: 2701
	// (get) Token: 0x06003575 RID: 13685 RVA: 0x000C26A0 File Offset: 0x000C08A0
	// (set) Token: 0x06003576 RID: 13686 RVA: 0x000C26A8 File Offset: 0x000C08A8
	public ItemRepresentation itemRepresentation
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

	// Token: 0x17000A8E RID: 2702
	// (get) Token: 0x06003577 RID: 13687 RVA: 0x000C26B4 File Offset: 0x000C08B4
	public bool canActivate
	{
		get
		{
			return this.CanSetActivate(true);
		}
	}

	// Token: 0x17000A8F RID: 2703
	// (get) Token: 0x06003578 RID: 13688 RVA: 0x000C26C0 File Offset: 0x000C08C0
	public bool canDeactivate
	{
		get
		{
			return this.CanSetActivate(false);
		}
	}

	// Token: 0x06003579 RID: 13689 RVA: 0x000C26CC File Offset: 0x000C08CC
	protected virtual bool CanSetActivate(bool value)
	{
		return !value || !base.IsBroken();
	}

	// Token: 0x0600357A RID: 13690 RVA: 0x000C26E4 File Offset: 0x000C08E4
	protected virtual void SetItemRepresentation(ItemRepresentation itemRep)
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

	// Token: 0x0600357B RID: 13691 RVA: 0x000C2754 File Offset: 0x000C0954
	protected virtual void CreateViewModel()
	{
		this.DestroyViewModel();
		if (this.datablock._viewModelPrefab == null || actor.forceThirdPerson)
		{
			return;
		}
		this._vm = (ViewModel)Object.Instantiate(this.datablock._viewModelPrefab);
		this._vm.PlayDeployAnimation();
		if (this.datablock.deploySound)
		{
			this.datablock.deploySound.Play(1f);
		}
		CameraFX.ReplaceViewModel(this._vm, this._itemRep, this.iface as IHeldItem, false);
	}

	// Token: 0x0600357C RID: 13692 RVA: 0x000C280C File Offset: 0x000C0A0C
	protected virtual void DestroyViewModel()
	{
		if (this._vm)
		{
			CameraFX.RemoveViewModel(ref this._vm, true, false);
		}
	}

	// Token: 0x0600357D RID: 13693 RVA: 0x000C282C File Offset: 0x000C0A2C
	public void OnActivate()
	{
		this.OnSetActive(true);
	}

	// Token: 0x0600357E RID: 13694 RVA: 0x000C2838 File Offset: 0x000C0A38
	public void OnDeactivate()
	{
		this.OnSetActive(false);
	}

	// Token: 0x0600357F RID: 13695 RVA: 0x000C2844 File Offset: 0x000C0A44
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

	// Token: 0x06003580 RID: 13696 RVA: 0x000C2860 File Offset: 0x000C0A60
	public override void OnMovedTo(Inventory toInv, int toSlot)
	{
		if (base.active)
		{
			base.inventory.DeactivateItem();
		}
	}

	// Token: 0x17000A90 RID: 2704
	// (get) Token: 0x06003581 RID: 13697 RVA: 0x000C2878 File Offset: 0x000C0A78
	public bool canAim
	{
		get
		{
			return this.CanAim();
		}
	}

	// Token: 0x06003582 RID: 13698 RVA: 0x000C2880 File Offset: 0x000C0A80
	protected virtual bool CanAim()
	{
		return true;
	}

	// Token: 0x06003583 RID: 13699 RVA: 0x000C2884 File Offset: 0x000C0A84
	public virtual void ItemPreFrame(ref HumanController.InputSample sample)
	{
		if (sample.attack2 && this.datablock.secondaryFireAims && this.CanAim())
		{
			sample.attack2 = false;
			sample.aim = true;
			sample.yaw *= this.datablock.aimSensitivtyPercent;
			sample.pitch *= this.datablock.aimSensitivtyPercent;
		}
	}

	// Token: 0x06003584 RID: 13700 RVA: 0x000C2904 File Offset: 0x000C0B04
	public virtual void ItemPostFrame(ref HumanController.InputSample sample)
	{
	}

	// Token: 0x06003585 RID: 13701 RVA: 0x000C2908 File Offset: 0x000C0B08
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

	// Token: 0x06003586 RID: 13702 RVA: 0x000C2948 File Offset: 0x000C0B48
	public void AddMod(ItemModDataBlock mod)
	{
		this.RecalculateMods();
		int usedModSlots = this.usedModSlots;
		this._itemMods[usedModSlots] = mod;
		this.RecalculateMods();
		this.OnModAdded(mod);
		base.MarkDirty();
	}

	// Token: 0x06003587 RID: 13703 RVA: 0x000C2980 File Offset: 0x000C0B80
	public int FindMod(ItemModDataBlock mod)
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

	// Token: 0x06003588 RID: 13704 RVA: 0x000C29C0 File Offset: 0x000C0BC0
	protected virtual void OnModAdded(ItemModDataBlock mod)
	{
	}

	// Token: 0x06003589 RID: 13705 RVA: 0x000C29C4 File Offset: 0x000C0BC4
	public virtual void PreCameraRender()
	{
	}

	// Token: 0x17000A91 RID: 2705
	// (get) Token: 0x0600358A RID: 13706 RVA: 0x000C29C8 File Offset: 0x000C0BC8
	public ItemModFlags modFlags
	{
		get
		{
			ItemModFlags itemModFlags = ItemModFlags.Other;
			if (this._itemMods != null)
			{
				foreach (ItemModDataBlock itemModDataBlock in this._itemMods)
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

	// Token: 0x0600358B RID: 13707 RVA: 0x000C2A18 File Offset: 0x000C0C18
	public void SetTotalModSlotCount(int count)
	{
		this.totalModSlots = count;
	}

	// Token: 0x0600358C RID: 13708 RVA: 0x000C2A24 File Offset: 0x000C0C24
	public void SetUsedModSlotCount(int count)
	{
		this.usedModSlots = count;
	}

	// Token: 0x0600358D RID: 13709 RVA: 0x000C2A30 File Offset: 0x000C0C30
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

	// Token: 0x0600358E RID: 13710 RVA: 0x000C2A84 File Offset: 0x000C0C84
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
				this._itemMods[i] = (DatablockDictionary.GetByUniqueID(stream.ReadInt32()) as ItemModDataBlock);
			}
			else
			{
				this._itemMods[i] = null;
			}
		}
	}

	// Token: 0x0600358F RID: 13711 RVA: 0x000C2AF8 File Offset: 0x000C0CF8
	public override void ConditionChanged(float oldCondition)
	{
	}

	// Token: 0x04001A71 RID: 6769
	protected ItemModDataBlock[] _itemMods = new ItemModDataBlock[5];

	// Token: 0x04001A72 RID: 6770
	private ViewModel _vm;

	// Token: 0x04001A73 RID: 6771
	private ItemRepresentation _itemRep;
}
