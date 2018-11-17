using System;
using uLink;

// Token: 0x0200060C RID: 1548
public class AmmoItemDataBlock : global::ItemDataBlock
{
	// Token: 0x06003184 RID: 12676 RVA: 0x000BE920 File Offset: 0x000BCB20
	protected override global::IInventoryItem ConstructItem()
	{
		return new global::AmmoItemDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x06003185 RID: 12677 RVA: 0x000BE928 File Offset: 0x000BCB28
	public override string GetItemDescription()
	{
		return "Ammunition for a weapon";
	}

	// Token: 0x04001ADA RID: 6874
	public global::ItemDataBlock spentCasingType;

	// Token: 0x0200060D RID: 1549
	private sealed class ITEM_TYPE : global::AmmoItem<global::AmmoItemDataBlock>, global::IAmmoItem, global::IInventoryItem
	{
		// Token: 0x06003186 RID: 12678 RVA: 0x000BE930 File Offset: 0x000BCB30
		public ITEM_TYPE(global::AmmoItemDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x06003187 RID: 12679 RVA: 0x000BE93C File Offset: 0x000BCB3C
		global::ItemDataBlock global::IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x06003188 RID: 12680 RVA: 0x000BE944 File Offset: 0x000BCB44
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x06003189 RID: 12681 RVA: 0x000BE94C File Offset: 0x000BCB4C
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x0600318A RID: 12682 RVA: 0x000BE954 File Offset: 0x000BCB54
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x0600318B RID: 12683 RVA: 0x000BE95C File Offset: 0x000BCB5C
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x0600318C RID: 12684 RVA: 0x000BE968 File Offset: 0x000BCB68
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x0600318D RID: 12685 RVA: 0x000BE974 File Offset: 0x000BCB74
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x0600318E RID: 12686 RVA: 0x000BE980 File Offset: 0x000BCB80
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x0600318F RID: 12687 RVA: 0x000BE98C File Offset: 0x000BCB8C
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x06003190 RID: 12688 RVA: 0x000BE998 File Offset: 0x000BCB98
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x06003191 RID: 12689 RVA: 0x000BE9A4 File Offset: 0x000BCBA4
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x06003192 RID: 12690 RVA: 0x000BE9B0 File Offset: 0x000BCBB0
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x06003193 RID: 12691 RVA: 0x000BE9BC File Offset: 0x000BCBBC
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x06003194 RID: 12692 RVA: 0x000BE9C4 File Offset: 0x000BCBC4
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x06003195 RID: 12693 RVA: 0x000BE9CC File Offset: 0x000BCBCC
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x06003196 RID: 12694 RVA: 0x000BE9D4 File Offset: 0x000BCBD4
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x06003197 RID: 12695 RVA: 0x000BE9DC File Offset: 0x000BCBDC
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x06003198 RID: 12696 RVA: 0x000BE9E4 File Offset: 0x000BCBE4
		global::Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x06003199 RID: 12697 RVA: 0x000BE9EC File Offset: 0x000BCBEC
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x0600319A RID: 12698 RVA: 0x000BE9F4 File Offset: 0x000BCBF4
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x0600319B RID: 12699 RVA: 0x000BE9FC File Offset: 0x000BCBFC
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x0600319C RID: 12700 RVA: 0x000BEA08 File Offset: 0x000BCC08
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x0600319D RID: 12701 RVA: 0x000BEA10 File Offset: 0x000BCC10
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x0600319E RID: 12702 RVA: 0x000BEA18 File Offset: 0x000BCC18
		global::Character get_character()
		{
			return base.character;
		}

		// Token: 0x0600319F RID: 12703 RVA: 0x000BEA20 File Offset: 0x000BCC20
		global::Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x060031A0 RID: 12704 RVA: 0x000BEA28 File Offset: 0x000BCC28
		global::Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x060031A1 RID: 12705 RVA: 0x000BEA30 File Offset: 0x000BCC30
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x060031A2 RID: 12706 RVA: 0x000BEA38 File Offset: 0x000BCC38
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
