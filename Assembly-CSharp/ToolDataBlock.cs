using System;
using uLink;
using UnityEngine;

// Token: 0x0200059A RID: 1434
public class ToolDataBlock : ItemDataBlock
{
	// Token: 0x06003373 RID: 13171 RVA: 0x000BE03C File Offset: 0x000BC23C
	protected override IInventoryItem ConstructItem()
	{
		return new ToolDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x06003374 RID: 13172 RVA: 0x000BE044 File Offset: 0x000BC244
	public virtual bool CanWork(IToolItem tool, Inventory workbenchInv)
	{
		return false;
	}

	// Token: 0x06003375 RID: 13173 RVA: 0x000BE048 File Offset: 0x000BC248
	public virtual bool CompleteWork(IToolItem tool, Inventory workbenchInv)
	{
		return false;
	}

	// Token: 0x06003376 RID: 13174 RVA: 0x000BE04C File Offset: 0x000BC24C
	public virtual float GetWorkDuration(IToolItem tool)
	{
		return 1f;
	}

	// Token: 0x06003377 RID: 13175 RVA: 0x000BE054 File Offset: 0x000BC254
	public IInventoryItem GetFirstItemNotTool(IToolItem tool, Inventory workbenchInv)
	{
		using (Inventory.OccupiedIterator occupiedIterator = workbenchInv.occupiedIterator)
		{
			IInventoryItem inventoryItem;
			while (occupiedIterator.Next(out inventoryItem))
			{
				if (!object.ReferenceEquals(inventoryItem, tool))
				{
					return inventoryItem;
				}
			}
		}
		Debug.LogWarning("Could not find target item");
		return null;
	}

	// Token: 0x0200059B RID: 1435
	private sealed class ITEM_TYPE : ToolItem<ToolDataBlock>, IInventoryItem, IToolItem
	{
		// Token: 0x06003378 RID: 13176 RVA: 0x000BE0C8 File Offset: 0x000BC2C8
		public ITEM_TYPE(ToolDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x06003379 RID: 13177 RVA: 0x000BE0D4 File Offset: 0x000BC2D4
		ItemDataBlock IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x0600337A RID: 13178 RVA: 0x000BE0DC File Offset: 0x000BC2DC
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x0600337B RID: 13179 RVA: 0x000BE0E4 File Offset: 0x000BC2E4
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x0600337C RID: 13180 RVA: 0x000BE0EC File Offset: 0x000BC2EC
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x0600337D RID: 13181 RVA: 0x000BE0F4 File Offset: 0x000BC2F4
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x0600337E RID: 13182 RVA: 0x000BE100 File Offset: 0x000BC300
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x0600337F RID: 13183 RVA: 0x000BE10C File Offset: 0x000BC30C
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x06003380 RID: 13184 RVA: 0x000BE118 File Offset: 0x000BC318
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x06003381 RID: 13185 RVA: 0x000BE124 File Offset: 0x000BC324
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x06003382 RID: 13186 RVA: 0x000BE130 File Offset: 0x000BC330
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x06003383 RID: 13187 RVA: 0x000BE13C File Offset: 0x000BC33C
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x06003384 RID: 13188 RVA: 0x000BE148 File Offset: 0x000BC348
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x06003385 RID: 13189 RVA: 0x000BE154 File Offset: 0x000BC354
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x06003386 RID: 13190 RVA: 0x000BE15C File Offset: 0x000BC35C
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x06003387 RID: 13191 RVA: 0x000BE164 File Offset: 0x000BC364
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x06003388 RID: 13192 RVA: 0x000BE16C File Offset: 0x000BC36C
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x06003389 RID: 13193 RVA: 0x000BE174 File Offset: 0x000BC374
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x0600338A RID: 13194 RVA: 0x000BE17C File Offset: 0x000BC37C
		Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x0600338B RID: 13195 RVA: 0x000BE184 File Offset: 0x000BC384
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x0600338C RID: 13196 RVA: 0x000BE18C File Offset: 0x000BC38C
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x0600338D RID: 13197 RVA: 0x000BE194 File Offset: 0x000BC394
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x0600338E RID: 13198 RVA: 0x000BE1A0 File Offset: 0x000BC3A0
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x0600338F RID: 13199 RVA: 0x000BE1A8 File Offset: 0x000BC3A8
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x06003390 RID: 13200 RVA: 0x000BE1B0 File Offset: 0x000BC3B0
		Character get_character()
		{
			return base.character;
		}

		// Token: 0x06003391 RID: 13201 RVA: 0x000BE1B8 File Offset: 0x000BC3B8
		Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x06003392 RID: 13202 RVA: 0x000BE1C0 File Offset: 0x000BC3C0
		Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x06003393 RID: 13203 RVA: 0x000BE1C8 File Offset: 0x000BC3C8
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x06003394 RID: 13204 RVA: 0x000BE1D0 File Offset: 0x000BC3D0
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
