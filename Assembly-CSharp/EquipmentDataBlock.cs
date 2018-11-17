using System;
using uLink;

// Token: 0x02000570 RID: 1392
public class EquipmentDataBlock : ItemDataBlock
{
	// Token: 0x06003037 RID: 12343 RVA: 0x000BA728 File Offset: 0x000B8928
	protected override IInventoryItem ConstructItem()
	{
		return new EquipmentDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x06003038 RID: 12344 RVA: 0x000BA730 File Offset: 0x000B8930
	public virtual void OnEquipped(IEquipmentItem item)
	{
	}

	// Token: 0x06003039 RID: 12345 RVA: 0x000BA734 File Offset: 0x000B8934
	public virtual void OnUnEquipped(IEquipmentItem item)
	{
	}

	// Token: 0x02000571 RID: 1393
	private sealed class ITEM_TYPE : EquipmentItem<EquipmentDataBlock>, IEquipmentItem, IInventoryItem
	{
		// Token: 0x0600303A RID: 12346 RVA: 0x000BA738 File Offset: 0x000B8938
		public ITEM_TYPE(EquipmentDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x0600303B RID: 12347 RVA: 0x000BA744 File Offset: 0x000B8944
		ItemDataBlock IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x0600303C RID: 12348 RVA: 0x000BA74C File Offset: 0x000B894C
		void OnUnEquipped()
		{
			base.OnUnEquipped();
		}

		// Token: 0x0600303D RID: 12349 RVA: 0x000BA754 File Offset: 0x000B8954
		void OnEquipped()
		{
			base.OnEquipped();
		}

		// Token: 0x0600303E RID: 12350 RVA: 0x000BA75C File Offset: 0x000B895C
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x0600303F RID: 12351 RVA: 0x000BA764 File Offset: 0x000B8964
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x06003040 RID: 12352 RVA: 0x000BA76C File Offset: 0x000B896C
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x06003041 RID: 12353 RVA: 0x000BA774 File Offset: 0x000B8974
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x06003042 RID: 12354 RVA: 0x000BA780 File Offset: 0x000B8980
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x06003043 RID: 12355 RVA: 0x000BA78C File Offset: 0x000B898C
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x06003044 RID: 12356 RVA: 0x000BA798 File Offset: 0x000B8998
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x06003045 RID: 12357 RVA: 0x000BA7A4 File Offset: 0x000B89A4
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x06003046 RID: 12358 RVA: 0x000BA7B0 File Offset: 0x000B89B0
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x06003047 RID: 12359 RVA: 0x000BA7BC File Offset: 0x000B89BC
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x06003048 RID: 12360 RVA: 0x000BA7C8 File Offset: 0x000B89C8
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x06003049 RID: 12361 RVA: 0x000BA7D4 File Offset: 0x000B89D4
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x0600304A RID: 12362 RVA: 0x000BA7DC File Offset: 0x000B89DC
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x0600304B RID: 12363 RVA: 0x000BA7E4 File Offset: 0x000B89E4
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x0600304C RID: 12364 RVA: 0x000BA7EC File Offset: 0x000B89EC
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x0600304D RID: 12365 RVA: 0x000BA7F4 File Offset: 0x000B89F4
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x0600304E RID: 12366 RVA: 0x000BA7FC File Offset: 0x000B89FC
		Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x0600304F RID: 12367 RVA: 0x000BA804 File Offset: 0x000B8A04
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x06003050 RID: 12368 RVA: 0x000BA80C File Offset: 0x000B8A0C
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x06003051 RID: 12369 RVA: 0x000BA814 File Offset: 0x000B8A14
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x06003052 RID: 12370 RVA: 0x000BA820 File Offset: 0x000B8A20
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x06003053 RID: 12371 RVA: 0x000BA828 File Offset: 0x000B8A28
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x06003054 RID: 12372 RVA: 0x000BA830 File Offset: 0x000B8A30
		Character get_character()
		{
			return base.character;
		}

		// Token: 0x06003055 RID: 12373 RVA: 0x000BA838 File Offset: 0x000B8A38
		Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x06003056 RID: 12374 RVA: 0x000BA840 File Offset: 0x000B8A40
		Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x06003057 RID: 12375 RVA: 0x000BA848 File Offset: 0x000B8A48
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x06003058 RID: 12376 RVA: 0x000BA850 File Offset: 0x000B8A50
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
