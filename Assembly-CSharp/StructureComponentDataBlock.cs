using System;
using uLink;
using UnityEngine;

// Token: 0x02000592 RID: 1426
public class StructureComponentDataBlock : HeldItemDataBlock
{
	// Token: 0x060032B4 RID: 12980 RVA: 0x000BD524 File Offset: 0x000BB724
	protected override IInventoryItem ConstructItem()
	{
		return new StructureComponentDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x17000A3E RID: 2622
	// (get) Token: 0x060032B5 RID: 12981 RVA: 0x000BD52C File Offset: 0x000BB72C
	public StructureComponent structureToPlacePrefab
	{
		get
		{
			if (!this._loadedStructureToPlace && Application.isPlaying)
			{
				NetCull.LoadPrefabScript<StructureComponent>(this.structureToPlaceName, out this._structureToPlace);
				this._loadedStructureToPlace = true;
			}
			return this._structureToPlace;
		}
	}

	// Token: 0x060032B6 RID: 12982 RVA: 0x000BD570 File Offset: 0x000BB770
	public bool MasterFromRay(Ray ray)
	{
		foreach (StructureMaster structureMaster in StructureMaster.RayTestStructures(ray))
		{
			if (structureMaster)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060032B7 RID: 12983 RVA: 0x000BD5AC File Offset: 0x000BB7AC
	public bool CheckBlockers(Vector3 pos)
	{
		if (this._structureToPlace.type == StructureComponent.StructureComponentType.Foundation)
		{
			Collider[] array = Physics.OverlapSphere(pos, 12f, 271975425);
			foreach (Collider collider in array)
			{
				IDMain main = IDBase.GetMain(collider.gameObject);
				if (main)
				{
					float num = TransformHelpers.Dist2D(main.transform.position, pos);
					if (main.GetComponent<SpikeWall>() && num < 5f)
					{
						return false;
					}
				}
			}
		}
		return NoPlacementZone.ValidPos(pos);
	}

	// Token: 0x060032B8 RID: 12984 RVA: 0x000BD650 File Offset: 0x000BB850
	public override void DoAction1(BitStream stream, ItemRepresentation rep, ref NetworkMessageInfo info)
	{
	}

	// Token: 0x040019FA RID: 6650
	public string structureToPlaceName;

	// Token: 0x040019FB RID: 6651
	[NonSerialized]
	private StructureComponent _structureToPlace;

	// Token: 0x040019FC RID: 6652
	[NonSerialized]
	private bool _loadedStructureToPlace;

	// Token: 0x040019FD RID: 6653
	public Material overrideMat;

	// Token: 0x02000593 RID: 1427
	private sealed class ITEM_TYPE : StructureComponentItem<StructureComponentDataBlock>, IHeldItem, IInventoryItem, IStructureComponentItem
	{
		// Token: 0x060032B9 RID: 12985 RVA: 0x000BD654 File Offset: 0x000BB854
		public ITEM_TYPE(StructureComponentDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x060032BA RID: 12986 RVA: 0x000BD660 File Offset: 0x000BB860
		ItemDataBlock IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x060032BB RID: 12987 RVA: 0x000BD668 File Offset: 0x000BB868
		void SetTotalModSlotCount(int count)
		{
			base.SetTotalModSlotCount(count);
		}

		// Token: 0x060032BC RID: 12988 RVA: 0x000BD674 File Offset: 0x000BB874
		void SetUsedModSlotCount(int count)
		{
			base.SetUsedModSlotCount(count);
		}

		// Token: 0x060032BD RID: 12989 RVA: 0x000BD680 File Offset: 0x000BB880
		void AddMod(ItemModDataBlock mod)
		{
			base.AddMod(mod);
		}

		// Token: 0x060032BE RID: 12990 RVA: 0x000BD68C File Offset: 0x000BB88C
		int FindMod(ItemModDataBlock mod)
		{
			return base.FindMod(mod);
		}

		// Token: 0x060032BF RID: 12991 RVA: 0x000BD698 File Offset: 0x000BB898
		void OnActivate()
		{
			base.OnActivate();
		}

		// Token: 0x060032C0 RID: 12992 RVA: 0x000BD6A0 File Offset: 0x000BB8A0
		void OnDeactivate()
		{
			base.OnDeactivate();
		}

		// Token: 0x060032C1 RID: 12993 RVA: 0x000BD6A8 File Offset: 0x000BB8A8
		ViewModel get_viewModelInstance()
		{
			return base.viewModelInstance;
		}

		// Token: 0x060032C2 RID: 12994 RVA: 0x000BD6B0 File Offset: 0x000BB8B0
		ItemRepresentation get_itemRepresentation()
		{
			return base.itemRepresentation;
		}

		// Token: 0x060032C3 RID: 12995 RVA: 0x000BD6B8 File Offset: 0x000BB8B8
		void set_itemRepresentation(ItemRepresentation value)
		{
			base.itemRepresentation = value;
		}

		// Token: 0x060032C4 RID: 12996 RVA: 0x000BD6C4 File Offset: 0x000BB8C4
		bool get_canActivate()
		{
			return base.canActivate;
		}

		// Token: 0x060032C5 RID: 12997 RVA: 0x000BD6CC File Offset: 0x000BB8CC
		bool get_canDeactivate()
		{
			return base.canDeactivate;
		}

		// Token: 0x060032C6 RID: 12998 RVA: 0x000BD6D4 File Offset: 0x000BB8D4
		ItemModFlags get_modFlags()
		{
			return base.modFlags;
		}

		// Token: 0x060032C7 RID: 12999 RVA: 0x000BD6DC File Offset: 0x000BB8DC
		ItemModDataBlock[] get_itemMods()
		{
			return base.itemMods;
		}

		// Token: 0x060032C8 RID: 13000 RVA: 0x000BD6E4 File Offset: 0x000BB8E4
		int get_totalModSlots()
		{
			return base.totalModSlots;
		}

		// Token: 0x060032C9 RID: 13001 RVA: 0x000BD6EC File Offset: 0x000BB8EC
		int get_usedModSlots()
		{
			return base.usedModSlots;
		}

		// Token: 0x060032CA RID: 13002 RVA: 0x000BD6F4 File Offset: 0x000BB8F4
		int get_freeModSlots()
		{
			return base.freeModSlots;
		}

		// Token: 0x060032CB RID: 13003 RVA: 0x000BD6FC File Offset: 0x000BB8FC
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x060032CC RID: 13004 RVA: 0x000BD704 File Offset: 0x000BB904
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x060032CD RID: 13005 RVA: 0x000BD70C File Offset: 0x000BB90C
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x060032CE RID: 13006 RVA: 0x000BD714 File Offset: 0x000BB914
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x060032CF RID: 13007 RVA: 0x000BD720 File Offset: 0x000BB920
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x060032D0 RID: 13008 RVA: 0x000BD72C File Offset: 0x000BB92C
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x060032D1 RID: 13009 RVA: 0x000BD738 File Offset: 0x000BB938
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x060032D2 RID: 13010 RVA: 0x000BD744 File Offset: 0x000BB944
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x060032D3 RID: 13011 RVA: 0x000BD750 File Offset: 0x000BB950
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x060032D4 RID: 13012 RVA: 0x000BD75C File Offset: 0x000BB95C
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x060032D5 RID: 13013 RVA: 0x000BD768 File Offset: 0x000BB968
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x060032D6 RID: 13014 RVA: 0x000BD774 File Offset: 0x000BB974
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x060032D7 RID: 13015 RVA: 0x000BD77C File Offset: 0x000BB97C
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x060032D8 RID: 13016 RVA: 0x000BD784 File Offset: 0x000BB984
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x060032D9 RID: 13017 RVA: 0x000BD78C File Offset: 0x000BB98C
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x060032DA RID: 13018 RVA: 0x000BD794 File Offset: 0x000BB994
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x060032DB RID: 13019 RVA: 0x000BD79C File Offset: 0x000BB99C
		Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x060032DC RID: 13020 RVA: 0x000BD7A4 File Offset: 0x000BB9A4
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x060032DD RID: 13021 RVA: 0x000BD7AC File Offset: 0x000BB9AC
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x060032DE RID: 13022 RVA: 0x000BD7B4 File Offset: 0x000BB9B4
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x060032DF RID: 13023 RVA: 0x000BD7C0 File Offset: 0x000BB9C0
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x060032E0 RID: 13024 RVA: 0x000BD7C8 File Offset: 0x000BB9C8
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x060032E1 RID: 13025 RVA: 0x000BD7D0 File Offset: 0x000BB9D0
		Character get_character()
		{
			return base.character;
		}

		// Token: 0x060032E2 RID: 13026 RVA: 0x000BD7D8 File Offset: 0x000BB9D8
		Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x060032E3 RID: 13027 RVA: 0x000BD7E0 File Offset: 0x000BB9E0
		Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x060032E4 RID: 13028 RVA: 0x000BD7E8 File Offset: 0x000BB9E8
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x060032E5 RID: 13029 RVA: 0x000BD7F0 File Offset: 0x000BB9F0
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
