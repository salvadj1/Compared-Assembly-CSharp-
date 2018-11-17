using System;
using System.Collections.Generic;
using uLink;
using UnityEngine;

// Token: 0x02000644 RID: 1604
public class ItemModDataBlock : global::ItemDataBlock
{
	// Token: 0x0600359F RID: 13727 RVA: 0x000C48A4 File Offset: 0x000C2AA4
	protected ItemModDataBlock(Type minimumModRepresentationType)
	{
		if (!typeof(global::ItemModRepresentation).IsAssignableFrom(minimumModRepresentationType))
		{
			throw new ArgumentOutOfRangeException("minimumModRepresentationType", minimumModRepresentationType, "!typeof(ItemModRepresentation).IsAssignableFrom(minimumModRepresentationType)");
		}
		this.minimumModRepresentationType = minimumModRepresentationType;
	}

	// Token: 0x060035A0 RID: 13728 RVA: 0x000C48F0 File Offset: 0x000C2AF0
	public ItemModDataBlock() : this(typeof(global::ItemModRepresentation))
	{
	}

	// Token: 0x060035A1 RID: 13729 RVA: 0x000C4904 File Offset: 0x000C2B04
	protected override global::IInventoryItem ConstructItem()
	{
		return new global::ItemModDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x17000AAF RID: 2735
	// (get) Token: 0x060035A2 RID: 13730 RVA: 0x000C490C File Offset: 0x000C2B0C
	public bool hasModRepresentation
	{
		get
		{
			return !string.IsNullOrEmpty(this.modRepresentationTypeName);
		}
	}

	// Token: 0x060035A3 RID: 13731 RVA: 0x000C491C File Offset: 0x000C2B1C
	internal bool AddModRepresentationComponent(GameObject gameObject, out global::ItemModRepresentation rep)
	{
		if (this.hasModRepresentation)
		{
			global::ItemModDataBlock.g.TypePair typePair;
			if (!global::ItemModDataBlock.g.cachedTypeLookup.TryGetValue(base.name, out typePair) || typePair.typeString != this.modRepresentationTypeName)
			{
				typePair = new global::ItemModDataBlock.g.TypePair
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
				global::ItemModDataBlock.g.cachedTypeLookup[base.name] = typePair;
			}
			if (typePair.type != null)
			{
				rep = (global::ItemModRepresentation)gameObject.AddComponent(typePair.type);
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

	// Token: 0x060035A4 RID: 13732 RVA: 0x000C4A48 File Offset: 0x000C2C48
	internal void BindAsProxy(global::ItemModRepresentation rep)
	{
		this.InstallToItemModRepresentation(rep);
	}

	// Token: 0x060035A5 RID: 13733 RVA: 0x000C4A54 File Offset: 0x000C2C54
	internal void UnBindAsProxy(global::ItemModRepresentation rep)
	{
		this.UninstallFromItemModRepresentation(rep);
	}

	// Token: 0x060035A6 RID: 13734 RVA: 0x000C4A60 File Offset: 0x000C2C60
	internal void BindAsLocal(ref global::ModViewModelAddArgs a)
	{
		this.InstallToViewModel(ref a);
	}

	// Token: 0x060035A7 RID: 13735 RVA: 0x000C4A6C File Offset: 0x000C2C6C
	internal void UnBindAsLocal(ref global::ModViewModelRemoveArgs a)
	{
		this.UninstallFromViewModel(ref a);
	}

	// Token: 0x060035A8 RID: 13736 RVA: 0x000C4A78 File Offset: 0x000C2C78
	protected virtual void CustomizeItemModRepresentation(global::ItemModRepresentation rep)
	{
	}

	// Token: 0x060035A9 RID: 13737 RVA: 0x000C4A7C File Offset: 0x000C2C7C
	protected virtual void InstallToItemModRepresentation(global::ItemModRepresentation rep)
	{
	}

	// Token: 0x060035AA RID: 13738 RVA: 0x000C4A80 File Offset: 0x000C2C80
	protected virtual void UninstallFromItemModRepresentation(global::ItemModRepresentation rep)
	{
	}

	// Token: 0x060035AB RID: 13739 RVA: 0x000C4A84 File Offset: 0x000C2C84
	protected virtual bool InstallToViewModel(ref global::ModViewModelAddArgs a)
	{
		return false;
	}

	// Token: 0x060035AC RID: 13740 RVA: 0x000C4A88 File Offset: 0x000C2C88
	protected virtual void UninstallFromViewModel(ref global::ModViewModelRemoveArgs a)
	{
	}

	// Token: 0x060035AD RID: 13741 RVA: 0x000C4A8C File Offset: 0x000C2C8C
	protected void OnDestroy()
	{
	}

	// Token: 0x060035AE RID: 13742 RVA: 0x000C4A90 File Offset: 0x000C2C90
	protected override void SecureWriteMemberValues(BitStream stream)
	{
		base.SecureWriteMemberValues(stream);
		stream.Write<global::ItemModFlags>(this.modFlag, new object[0]);
		stream.Write<string>(this.modRepresentationTypeName, new object[0]);
	}

	// Token: 0x04001BB1 RID: 7089
	[SerializeField]
	private string modRepresentationTypeName = "ItemModRepresentation";

	// Token: 0x04001BB2 RID: 7090
	public global::ItemModFlags modFlag;

	// Token: 0x04001BB3 RID: 7091
	public AudioClip onSound;

	// Token: 0x04001BB4 RID: 7092
	public AudioClip offSound;

	// Token: 0x04001BB5 RID: 7093
	private readonly Type minimumModRepresentationType;

	// Token: 0x02000645 RID: 1605
	private sealed class ITEM_TYPE : global::ItemModItem<global::ItemModDataBlock>, global::IInventoryItem, global::IItemModItem
	{
		// Token: 0x060035AF RID: 13743 RVA: 0x000C4AC0 File Offset: 0x000C2CC0
		public ITEM_TYPE(global::ItemModDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x060035B0 RID: 13744 RVA: 0x000C4ACC File Offset: 0x000C2CCC
		global::ItemDataBlock global::IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x060035B1 RID: 13745 RVA: 0x000C4AD4 File Offset: 0x000C2CD4
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x060035B2 RID: 13746 RVA: 0x000C4ADC File Offset: 0x000C2CDC
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x060035B3 RID: 13747 RVA: 0x000C4AE4 File Offset: 0x000C2CE4
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x060035B4 RID: 13748 RVA: 0x000C4AEC File Offset: 0x000C2CEC
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x060035B5 RID: 13749 RVA: 0x000C4AF8 File Offset: 0x000C2CF8
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x060035B6 RID: 13750 RVA: 0x000C4B04 File Offset: 0x000C2D04
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x060035B7 RID: 13751 RVA: 0x000C4B10 File Offset: 0x000C2D10
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x060035B8 RID: 13752 RVA: 0x000C4B1C File Offset: 0x000C2D1C
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x060035B9 RID: 13753 RVA: 0x000C4B28 File Offset: 0x000C2D28
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x060035BA RID: 13754 RVA: 0x000C4B34 File Offset: 0x000C2D34
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x060035BB RID: 13755 RVA: 0x000C4B40 File Offset: 0x000C2D40
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x060035BC RID: 13756 RVA: 0x000C4B4C File Offset: 0x000C2D4C
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x060035BD RID: 13757 RVA: 0x000C4B54 File Offset: 0x000C2D54
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x060035BE RID: 13758 RVA: 0x000C4B5C File Offset: 0x000C2D5C
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x060035BF RID: 13759 RVA: 0x000C4B64 File Offset: 0x000C2D64
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x060035C0 RID: 13760 RVA: 0x000C4B6C File Offset: 0x000C2D6C
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x060035C1 RID: 13761 RVA: 0x000C4B74 File Offset: 0x000C2D74
		global::Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x060035C2 RID: 13762 RVA: 0x000C4B7C File Offset: 0x000C2D7C
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x060035C3 RID: 13763 RVA: 0x000C4B84 File Offset: 0x000C2D84
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x060035C4 RID: 13764 RVA: 0x000C4B8C File Offset: 0x000C2D8C
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x060035C5 RID: 13765 RVA: 0x000C4B98 File Offset: 0x000C2D98
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x060035C6 RID: 13766 RVA: 0x000C4BA0 File Offset: 0x000C2DA0
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x060035C7 RID: 13767 RVA: 0x000C4BA8 File Offset: 0x000C2DA8
		global::Character get_character()
		{
			return base.character;
		}

		// Token: 0x060035C8 RID: 13768 RVA: 0x000C4BB0 File Offset: 0x000C2DB0
		global::Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x060035C9 RID: 13769 RVA: 0x000C4BB8 File Offset: 0x000C2DB8
		global::Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x060035CA RID: 13770 RVA: 0x000C4BC0 File Offset: 0x000C2DC0
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x060035CB RID: 13771 RVA: 0x000C4BC8 File Offset: 0x000C2DC8
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}

	// Token: 0x02000646 RID: 1606
	private static class g
	{
		// Token: 0x04001BB6 RID: 7094
		public static Dictionary<string, global::ItemModDataBlock.g.TypePair> cachedTypeLookup = new Dictionary<string, global::ItemModDataBlock.g.TypePair>();

		// Token: 0x02000647 RID: 1607
		public class TypePair
		{
			// Token: 0x04001BB7 RID: 7095
			public string typeString;

			// Token: 0x04001BB8 RID: 7096
			public Type type;
		}
	}
}
