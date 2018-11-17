using System;
using uLink;
using UnityEngine;

// Token: 0x02000650 RID: 1616
public class StructureComponentDataBlock : global::HeldItemDataBlock
{
	// Token: 0x0600367C RID: 13948 RVA: 0x000C5780 File Offset: 0x000C3980
	protected override global::IInventoryItem ConstructItem()
	{
		return new global::StructureComponentDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x17000AB4 RID: 2740
	// (get) Token: 0x0600367D RID: 13949 RVA: 0x000C5788 File Offset: 0x000C3988
	public global::StructureComponent structureToPlacePrefab
	{
		get
		{
			if (!this._loadedStructureToPlace && Application.isPlaying)
			{
				global::NetCull.LoadPrefabScript<global::StructureComponent>(this.structureToPlaceName, out this._structureToPlace);
				this._loadedStructureToPlace = true;
			}
			return this._structureToPlace;
		}
	}

	// Token: 0x0600367E RID: 13950 RVA: 0x000C57CC File Offset: 0x000C39CC
	public bool MasterFromRay(Ray ray)
	{
		foreach (global::StructureMaster structureMaster in global::StructureMaster.RayTestStructures(ray))
		{
			if (structureMaster)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600367F RID: 13951 RVA: 0x000C5808 File Offset: 0x000C3A08
	public bool CheckBlockers(Vector3 pos)
	{
		if (this._structureToPlace.type == global::StructureComponent.StructureComponentType.Foundation)
		{
			Collider[] array = Physics.OverlapSphere(pos, 12f, 271975425);
			foreach (Collider collider in array)
			{
				IDMain main = IDBase.GetMain(collider.gameObject);
				if (main)
				{
					float num = global::TransformHelpers.Dist2D(main.transform.position, pos);
					if (main.GetComponent<global::SpikeWall>() && num < 5f)
					{
						return false;
					}
				}
			}
		}
		return global::NoPlacementZone.ValidPos(pos);
	}

	// Token: 0x06003680 RID: 13952 RVA: 0x000C58AC File Offset: 0x000C3AAC
	public override void DoAction1(BitStream stream, global::ItemRepresentation rep, ref uLink.NetworkMessageInfo info)
	{
	}

	// Token: 0x04001BCB RID: 7115
	public string structureToPlaceName;

	// Token: 0x04001BCC RID: 7116
	[NonSerialized]
	private global::StructureComponent _structureToPlace;

	// Token: 0x04001BCD RID: 7117
	[NonSerialized]
	private bool _loadedStructureToPlace;

	// Token: 0x04001BCE RID: 7118
	public Material overrideMat;

	// Token: 0x02000651 RID: 1617
	private sealed class ITEM_TYPE : global::StructureComponentItem<global::StructureComponentDataBlock>, global::IHeldItem, global::IInventoryItem, global::IStructureComponentItem
	{
		// Token: 0x06003681 RID: 13953 RVA: 0x000C58B0 File Offset: 0x000C3AB0
		public ITEM_TYPE(global::StructureComponentDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x06003682 RID: 13954 RVA: 0x000C58BC File Offset: 0x000C3ABC
		global::ItemDataBlock global::IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x06003683 RID: 13955 RVA: 0x000C58C4 File Offset: 0x000C3AC4
		void SetTotalModSlotCount(int count)
		{
			base.SetTotalModSlotCount(count);
		}

		// Token: 0x06003684 RID: 13956 RVA: 0x000C58D0 File Offset: 0x000C3AD0
		void SetUsedModSlotCount(int count)
		{
			base.SetUsedModSlotCount(count);
		}

		// Token: 0x06003685 RID: 13957 RVA: 0x000C58DC File Offset: 0x000C3ADC
		void AddMod(global::ItemModDataBlock mod)
		{
			base.AddMod(mod);
		}

		// Token: 0x06003686 RID: 13958 RVA: 0x000C58E8 File Offset: 0x000C3AE8
		int FindMod(global::ItemModDataBlock mod)
		{
			return base.FindMod(mod);
		}

		// Token: 0x06003687 RID: 13959 RVA: 0x000C58F4 File Offset: 0x000C3AF4
		void OnActivate()
		{
			base.OnActivate();
		}

		// Token: 0x06003688 RID: 13960 RVA: 0x000C58FC File Offset: 0x000C3AFC
		void OnDeactivate()
		{
			base.OnDeactivate();
		}

		// Token: 0x06003689 RID: 13961 RVA: 0x000C5904 File Offset: 0x000C3B04
		global::ViewModel get_viewModelInstance()
		{
			return base.viewModelInstance;
		}

		// Token: 0x0600368A RID: 13962 RVA: 0x000C590C File Offset: 0x000C3B0C
		global::ItemRepresentation get_itemRepresentation()
		{
			return base.itemRepresentation;
		}

		// Token: 0x0600368B RID: 13963 RVA: 0x000C5914 File Offset: 0x000C3B14
		void set_itemRepresentation(global::ItemRepresentation value)
		{
			base.itemRepresentation = value;
		}

		// Token: 0x0600368C RID: 13964 RVA: 0x000C5920 File Offset: 0x000C3B20
		bool get_canActivate()
		{
			return base.canActivate;
		}

		// Token: 0x0600368D RID: 13965 RVA: 0x000C5928 File Offset: 0x000C3B28
		bool get_canDeactivate()
		{
			return base.canDeactivate;
		}

		// Token: 0x0600368E RID: 13966 RVA: 0x000C5930 File Offset: 0x000C3B30
		global::ItemModFlags get_modFlags()
		{
			return base.modFlags;
		}

		// Token: 0x0600368F RID: 13967 RVA: 0x000C5938 File Offset: 0x000C3B38
		global::ItemModDataBlock[] get_itemMods()
		{
			return base.itemMods;
		}

		// Token: 0x06003690 RID: 13968 RVA: 0x000C5940 File Offset: 0x000C3B40
		int get_totalModSlots()
		{
			return base.totalModSlots;
		}

		// Token: 0x06003691 RID: 13969 RVA: 0x000C5948 File Offset: 0x000C3B48
		int get_usedModSlots()
		{
			return base.usedModSlots;
		}

		// Token: 0x06003692 RID: 13970 RVA: 0x000C5950 File Offset: 0x000C3B50
		int get_freeModSlots()
		{
			return base.freeModSlots;
		}

		// Token: 0x06003693 RID: 13971 RVA: 0x000C5958 File Offset: 0x000C3B58
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x06003694 RID: 13972 RVA: 0x000C5960 File Offset: 0x000C3B60
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x06003695 RID: 13973 RVA: 0x000C5968 File Offset: 0x000C3B68
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x06003696 RID: 13974 RVA: 0x000C5970 File Offset: 0x000C3B70
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x06003697 RID: 13975 RVA: 0x000C597C File Offset: 0x000C3B7C
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x06003698 RID: 13976 RVA: 0x000C5988 File Offset: 0x000C3B88
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x06003699 RID: 13977 RVA: 0x000C5994 File Offset: 0x000C3B94
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x0600369A RID: 13978 RVA: 0x000C59A0 File Offset: 0x000C3BA0
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x0600369B RID: 13979 RVA: 0x000C59AC File Offset: 0x000C3BAC
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x0600369C RID: 13980 RVA: 0x000C59B8 File Offset: 0x000C3BB8
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x0600369D RID: 13981 RVA: 0x000C59C4 File Offset: 0x000C3BC4
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x0600369E RID: 13982 RVA: 0x000C59D0 File Offset: 0x000C3BD0
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x0600369F RID: 13983 RVA: 0x000C59D8 File Offset: 0x000C3BD8
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x060036A0 RID: 13984 RVA: 0x000C59E0 File Offset: 0x000C3BE0
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x060036A1 RID: 13985 RVA: 0x000C59E8 File Offset: 0x000C3BE8
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x060036A2 RID: 13986 RVA: 0x000C59F0 File Offset: 0x000C3BF0
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x060036A3 RID: 13987 RVA: 0x000C59F8 File Offset: 0x000C3BF8
		global::Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x060036A4 RID: 13988 RVA: 0x000C5A00 File Offset: 0x000C3C00
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x060036A5 RID: 13989 RVA: 0x000C5A08 File Offset: 0x000C3C08
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x060036A6 RID: 13990 RVA: 0x000C5A10 File Offset: 0x000C3C10
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x060036A7 RID: 13991 RVA: 0x000C5A1C File Offset: 0x000C3C1C
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x060036A8 RID: 13992 RVA: 0x000C5A24 File Offset: 0x000C3C24
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x060036A9 RID: 13993 RVA: 0x000C5A2C File Offset: 0x000C3C2C
		global::Character get_character()
		{
			return base.character;
		}

		// Token: 0x060036AA RID: 13994 RVA: 0x000C5A34 File Offset: 0x000C3C34
		global::Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x060036AB RID: 13995 RVA: 0x000C5A3C File Offset: 0x000C3C3C
		global::Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x060036AC RID: 13996 RVA: 0x000C5A44 File Offset: 0x000C3C44
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x060036AD RID: 13997 RVA: 0x000C5A4C File Offset: 0x000C3C4C
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
