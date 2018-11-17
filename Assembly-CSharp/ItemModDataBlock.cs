using System;
using System.Collections.Generic;
using uLink;
using UnityEngine;

// Token: 0x02000586 RID: 1414
public class ItemModDataBlock : ItemDataBlock
{
	// Token: 0x060031D7 RID: 12759 RVA: 0x000BC648 File Offset: 0x000BA848
	protected ItemModDataBlock(Type minimumModRepresentationType)
	{
		if (!typeof(ItemModRepresentation).IsAssignableFrom(minimumModRepresentationType))
		{
			throw new ArgumentOutOfRangeException("minimumModRepresentationType", minimumModRepresentationType, "!typeof(ItemModRepresentation).IsAssignableFrom(minimumModRepresentationType)");
		}
		this.minimumModRepresentationType = minimumModRepresentationType;
	}

	// Token: 0x060031D8 RID: 12760 RVA: 0x000BC694 File Offset: 0x000BA894
	public ItemModDataBlock() : this(typeof(ItemModRepresentation))
	{
	}

	// Token: 0x060031D9 RID: 12761 RVA: 0x000BC6A8 File Offset: 0x000BA8A8
	protected override IInventoryItem ConstructItem()
	{
		return new ItemModDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x17000A39 RID: 2617
	// (get) Token: 0x060031DA RID: 12762 RVA: 0x000BC6B0 File Offset: 0x000BA8B0
	public bool hasModRepresentation
	{
		get
		{
			return !string.IsNullOrEmpty(this.modRepresentationTypeName);
		}
	}

	// Token: 0x060031DB RID: 12763 RVA: 0x000BC6C0 File Offset: 0x000BA8C0
	internal bool AddModRepresentationComponent(GameObject gameObject, out ItemModRepresentation rep)
	{
		if (this.hasModRepresentation)
		{
			ItemModDataBlock.g.TypePair typePair;
			if (!ItemModDataBlock.g.cachedTypeLookup.TryGetValue(base.name, out typePair) || typePair.typeString != this.modRepresentationTypeName)
			{
				typePair = new ItemModDataBlock.g.TypePair
				{
					typeString = this.modRepresentationTypeName
				};
				typePair.type = Types.GetType(typePair.typeString, "Assembly-CSharp");
				if (typePair.type == null)
				{
					Debug.LogError(string.Format("modRepresentationTypeName:{0} resolves to no type", typePair.typeString), this);
				}
				else if (!this.minimumModRepresentationType.IsAssignableFrom(typePair.type))
				{
					Debug.LogError(string.Format("modRepresentationTypeName:{0} resolved to {1} but {1} is not a {2}", typePair.typeString, typePair.type, this.minimumModRepresentationType), this);
					typePair.type = null;
				}
				ItemModDataBlock.g.cachedTypeLookup[base.name] = typePair;
			}
			if (typePair.type != null)
			{
				rep = (ItemModRepresentation)gameObject.AddComponent(typePair.type);
				if (rep)
				{
					this.CustomizeItemModRepresentation(rep);
					if (rep)
					{
						return true;
					}
				}
			}
		}
		rep = null;
		return false;
	}

	// Token: 0x060031DC RID: 12764 RVA: 0x000BC7EC File Offset: 0x000BA9EC
	internal void BindAsProxy(ItemModRepresentation rep)
	{
		this.InstallToItemModRepresentation(rep);
	}

	// Token: 0x060031DD RID: 12765 RVA: 0x000BC7F8 File Offset: 0x000BA9F8
	internal void UnBindAsProxy(ItemModRepresentation rep)
	{
		this.UninstallFromItemModRepresentation(rep);
	}

	// Token: 0x060031DE RID: 12766 RVA: 0x000BC804 File Offset: 0x000BAA04
	internal void BindAsLocal(ref ModViewModelAddArgs a)
	{
		this.InstallToViewModel(ref a);
	}

	// Token: 0x060031DF RID: 12767 RVA: 0x000BC810 File Offset: 0x000BAA10
	internal void UnBindAsLocal(ref ModViewModelRemoveArgs a)
	{
		this.UninstallFromViewModel(ref a);
	}

	// Token: 0x060031E0 RID: 12768 RVA: 0x000BC81C File Offset: 0x000BAA1C
	protected virtual void CustomizeItemModRepresentation(ItemModRepresentation rep)
	{
	}

	// Token: 0x060031E1 RID: 12769 RVA: 0x000BC820 File Offset: 0x000BAA20
	protected virtual void InstallToItemModRepresentation(ItemModRepresentation rep)
	{
	}

	// Token: 0x060031E2 RID: 12770 RVA: 0x000BC824 File Offset: 0x000BAA24
	protected virtual void UninstallFromItemModRepresentation(ItemModRepresentation rep)
	{
	}

	// Token: 0x060031E3 RID: 12771 RVA: 0x000BC828 File Offset: 0x000BAA28
	protected virtual bool InstallToViewModel(ref ModViewModelAddArgs a)
	{
		return false;
	}

	// Token: 0x060031E4 RID: 12772 RVA: 0x000BC82C File Offset: 0x000BAA2C
	protected virtual void UninstallFromViewModel(ref ModViewModelRemoveArgs a)
	{
	}

	// Token: 0x060031E5 RID: 12773 RVA: 0x000BC830 File Offset: 0x000BAA30
	protected void OnDestroy()
	{
	}

	// Token: 0x060031E6 RID: 12774 RVA: 0x000BC834 File Offset: 0x000BAA34
	protected override void SecureWriteMemberValues(BitStream stream)
	{
		base.SecureWriteMemberValues(stream);
		stream.Write<ItemModFlags>(this.modFlag, new object[0]);
		stream.Write<string>(this.modRepresentationTypeName, new object[0]);
	}

	// Token: 0x040019E0 RID: 6624
	[SerializeField]
	private string modRepresentationTypeName = "ItemModRepresentation";

	// Token: 0x040019E1 RID: 6625
	public ItemModFlags modFlag;

	// Token: 0x040019E2 RID: 6626
	public AudioClip onSound;

	// Token: 0x040019E3 RID: 6627
	public AudioClip offSound;

	// Token: 0x040019E4 RID: 6628
	private readonly Type minimumModRepresentationType;

	// Token: 0x02000587 RID: 1415
	private sealed class ITEM_TYPE : ItemModItem<ItemModDataBlock>, IInventoryItem, IItemModItem
	{
		// Token: 0x060031E7 RID: 12775 RVA: 0x000BC864 File Offset: 0x000BAA64
		public ITEM_TYPE(ItemModDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x060031E8 RID: 12776 RVA: 0x000BC870 File Offset: 0x000BAA70
		ItemDataBlock IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x060031E9 RID: 12777 RVA: 0x000BC878 File Offset: 0x000BAA78
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x060031EA RID: 12778 RVA: 0x000BC880 File Offset: 0x000BAA80
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x060031EB RID: 12779 RVA: 0x000BC888 File Offset: 0x000BAA88
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x060031EC RID: 12780 RVA: 0x000BC890 File Offset: 0x000BAA90
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x060031ED RID: 12781 RVA: 0x000BC89C File Offset: 0x000BAA9C
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x060031EE RID: 12782 RVA: 0x000BC8A8 File Offset: 0x000BAAA8
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x060031EF RID: 12783 RVA: 0x000BC8B4 File Offset: 0x000BAAB4
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x060031F0 RID: 12784 RVA: 0x000BC8C0 File Offset: 0x000BAAC0
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x060031F1 RID: 12785 RVA: 0x000BC8CC File Offset: 0x000BAACC
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x060031F2 RID: 12786 RVA: 0x000BC8D8 File Offset: 0x000BAAD8
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x060031F3 RID: 12787 RVA: 0x000BC8E4 File Offset: 0x000BAAE4
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x060031F4 RID: 12788 RVA: 0x000BC8F0 File Offset: 0x000BAAF0
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x060031F5 RID: 12789 RVA: 0x000BC8F8 File Offset: 0x000BAAF8
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x060031F6 RID: 12790 RVA: 0x000BC900 File Offset: 0x000BAB00
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x060031F7 RID: 12791 RVA: 0x000BC908 File Offset: 0x000BAB08
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x060031F8 RID: 12792 RVA: 0x000BC910 File Offset: 0x000BAB10
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x060031F9 RID: 12793 RVA: 0x000BC918 File Offset: 0x000BAB18
		Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x060031FA RID: 12794 RVA: 0x000BC920 File Offset: 0x000BAB20
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x060031FB RID: 12795 RVA: 0x000BC928 File Offset: 0x000BAB28
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x060031FC RID: 12796 RVA: 0x000BC930 File Offset: 0x000BAB30
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x060031FD RID: 12797 RVA: 0x000BC93C File Offset: 0x000BAB3C
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x060031FE RID: 12798 RVA: 0x000BC944 File Offset: 0x000BAB44
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x060031FF RID: 12799 RVA: 0x000BC94C File Offset: 0x000BAB4C
		Character get_character()
		{
			return base.character;
		}

		// Token: 0x06003200 RID: 12800 RVA: 0x000BC954 File Offset: 0x000BAB54
		Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x06003201 RID: 12801 RVA: 0x000BC95C File Offset: 0x000BAB5C
		Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x06003202 RID: 12802 RVA: 0x000BC964 File Offset: 0x000BAB64
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x06003203 RID: 12803 RVA: 0x000BC96C File Offset: 0x000BAB6C
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}

	// Token: 0x02000588 RID: 1416
	private static class g
	{
		// Token: 0x040019E5 RID: 6629
		public static Dictionary<string, ItemModDataBlock.g.TypePair> cachedTypeLookup = new Dictionary<string, ItemModDataBlock.g.TypePair>();

		// Token: 0x02000589 RID: 1417
		public class TypePair
		{
			// Token: 0x040019E6 RID: 6630
			public string typeString;

			// Token: 0x040019E7 RID: 6631
			public Type type;
		}
	}
}
