using System;
using uLink;
using UnityEngine;

// Token: 0x02000566 RID: 1382
public class ConsumableDataBlock : ItemDataBlock
{
	// Token: 0x06002FAB RID: 12203 RVA: 0x000B957C File Offset: 0x000B777C
	protected override IInventoryItem ConstructItem()
	{
		return new ConsumableDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x06002FAC RID: 12204 RVA: 0x000B9584 File Offset: 0x000B7784
	public override int RetreiveMenuOptions(IInventoryItem item, InventoryItem.MenuItem[] results, int offset)
	{
		offset = base.RetreiveMenuOptions(item, results, offset);
		if (item.isInLocalInventory)
		{
			results[offset++] = this.GetConsumeMenuItem();
		}
		return offset;
	}

	// Token: 0x06002FAD RID: 12205 RVA: 0x000B95B8 File Offset: 0x000B77B8
	public InventoryItem.MenuItem GetConsumeMenuItem()
	{
		if (this.calories > 0f && this.litresOfWater <= 0f)
		{
			return InventoryItem.MenuItem.Eat;
		}
		if (this.litresOfWater > 0f && this.calories <= 0f)
		{
			return InventoryItem.MenuItem.Drink;
		}
		return InventoryItem.MenuItem.Consume;
	}

	// Token: 0x06002FAE RID: 12206 RVA: 0x000B960C File Offset: 0x000B780C
	public override InventoryItem.MenuItemResult ExecuteMenuOption(InventoryItem.MenuItem option, IInventoryItem item)
	{
		if (option == this.GetConsumeMenuItem())
		{
			return InventoryItem.MenuItemResult.DoneOnServer;
		}
		return base.ExecuteMenuOption(option, item);
	}

	// Token: 0x06002FAF RID: 12207 RVA: 0x000B9624 File Offset: 0x000B7824
	public virtual void UseItem(IConsumableItem item)
	{
		Inventory inventory = item.inventory;
		Metabolism local = inventory.GetLocal<Metabolism>();
		if (local == null)
		{
			return;
		}
		if (!local.CanConsumeYet())
		{
			return;
		}
		local.MarkConsumptionTime();
		float numCalories = Mathf.Min(local.GetRemainingCaloricSpace(), this.calories);
		if (this.calories > 0f)
		{
			local.AddCalories(numCalories);
		}
		if (this.litresOfWater > 0f)
		{
			local.AddWater(this.litresOfWater);
		}
		if (this.antiRads > 0f)
		{
			local.AddAntiRad(this.antiRads);
		}
		if (this.healthToHeal != 0f)
		{
			HumanBodyTakeDamage local2 = inventory.GetLocal<HumanBodyTakeDamage>();
			if (local2 != null)
			{
				if (this.healthToHeal > 0f)
				{
					local2.HealOverTime(this.healthToHeal);
				}
				else
				{
					TakeDamage.HurtSelf(inventory.idMain, Mathf.Abs(this.healthToHeal), null);
				}
			}
		}
		if (this.poisonAmount > 0f)
		{
			local.AddPoison(this.poisonAmount);
		}
		int num = 1;
		if (item.Consume(ref num))
		{
			inventory.RemoveItem(item.slot);
		}
	}

	// Token: 0x06002FB0 RID: 12208 RVA: 0x000B975C File Offset: 0x000B795C
	public override void PopulateInfoWindow(ItemToolTip infoWindow, IInventoryItem tipItem)
	{
		infoWindow.AddItemTitle(this, tipItem, 0f);
		infoWindow.AddSectionTitle("Consumable", 15f);
		if (this.calories > 0f)
		{
			infoWindow.AddBasicLabel(this.calories + " Calories", 15f);
		}
		if (this.litresOfWater > 0f)
		{
			infoWindow.AddBasicLabel(this.litresOfWater + "L Water", 15f);
		}
		if (this.antiRads > 0f)
		{
			infoWindow.AddBasicLabel("-" + this.antiRads + " Rads", 15f);
		}
		if (this.healthToHeal != 0f)
		{
			infoWindow.AddBasicLabel(((this.healthToHeal <= 0f) ? string.Empty : "+") + this.healthToHeal + " Health", 15f);
		}
		infoWindow.AddItemDescription(this, 15f);
		infoWindow.FinishPopulating();
	}

	// Token: 0x06002FB1 RID: 12209 RVA: 0x000B9884 File Offset: 0x000B7A84
	public override string GetItemDescription()
	{
		string text = string.Empty;
		if (this.calories > 0f && this.litresOfWater > 0f)
		{
			text += "This is a food item, consuming it (via right click) will replenish your food and water. ";
		}
		else if (this.calories > 0f)
		{
			text += "This is a food item, eating it will satisfy some of your hunger. ";
		}
		else if (this.litresOfWater > 0f)
		{
			text += "This is a beverage, drinking it will quench some of your thirst. ";
		}
		if (this.antiRads > 0f)
		{
			text += "This item has some anti-radioactive properties, consuming it will lower your radiation level. ";
		}
		if (this.healthToHeal > 0f)
		{
			text += "It will also provide minor healing";
		}
		return text;
	}

	// Token: 0x04001955 RID: 6485
	public float litresOfWater;

	// Token: 0x04001956 RID: 6486
	public float calories;

	// Token: 0x04001957 RID: 6487
	public float antiRads;

	// Token: 0x04001958 RID: 6488
	public float healthToHeal;

	// Token: 0x04001959 RID: 6489
	public float poisonAmount;

	// Token: 0x0400195A RID: 6490
	public bool cookable;

	// Token: 0x0400195B RID: 6491
	public int numToCookPerTick;

	// Token: 0x0400195C RID: 6492
	public ItemDataBlock cookedVersion;

	// Token: 0x0400195D RID: 6493
	public int burnTemp = 10;

	// Token: 0x0400195E RID: 6494
	public int cookHeatRequirement = 1;

	// Token: 0x02000567 RID: 1383
	private sealed class ITEM_TYPE : ConsumableItem<ConsumableDataBlock>, IConsumableItem, ICookableItem, IInventoryItem
	{
		// Token: 0x06002FB2 RID: 12210 RVA: 0x000B9940 File Offset: 0x000B7B40
		public ITEM_TYPE(ConsumableDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x06002FB3 RID: 12211 RVA: 0x000B994C File Offset: 0x000B7B4C
		ItemDataBlock IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x06002FB4 RID: 12212 RVA: 0x000B9954 File Offset: 0x000B7B54
		bool GetCookableInfo(out int consumeCount, out ItemDataBlock cookedVersion, out int cookedCount, out int cookTempMin, out int burnTemp)
		{
			return base.GetCookableInfo(out consumeCount, out cookedVersion, out cookedCount, out cookTempMin, out burnTemp);
		}

		// Token: 0x06002FB5 RID: 12213 RVA: 0x000B9964 File Offset: 0x000B7B64
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x06002FB6 RID: 12214 RVA: 0x000B996C File Offset: 0x000B7B6C
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x06002FB7 RID: 12215 RVA: 0x000B9974 File Offset: 0x000B7B74
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x06002FB8 RID: 12216 RVA: 0x000B997C File Offset: 0x000B7B7C
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x06002FB9 RID: 12217 RVA: 0x000B9988 File Offset: 0x000B7B88
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x06002FBA RID: 12218 RVA: 0x000B9994 File Offset: 0x000B7B94
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x06002FBB RID: 12219 RVA: 0x000B99A0 File Offset: 0x000B7BA0
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x06002FBC RID: 12220 RVA: 0x000B99AC File Offset: 0x000B7BAC
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x06002FBD RID: 12221 RVA: 0x000B99B8 File Offset: 0x000B7BB8
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x06002FBE RID: 12222 RVA: 0x000B99C4 File Offset: 0x000B7BC4
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x06002FBF RID: 12223 RVA: 0x000B99D0 File Offset: 0x000B7BD0
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x06002FC0 RID: 12224 RVA: 0x000B99DC File Offset: 0x000B7BDC
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x06002FC1 RID: 12225 RVA: 0x000B99E4 File Offset: 0x000B7BE4
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x06002FC2 RID: 12226 RVA: 0x000B99EC File Offset: 0x000B7BEC
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x06002FC3 RID: 12227 RVA: 0x000B99F4 File Offset: 0x000B7BF4
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x06002FC4 RID: 12228 RVA: 0x000B99FC File Offset: 0x000B7BFC
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x06002FC5 RID: 12229 RVA: 0x000B9A04 File Offset: 0x000B7C04
		Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x06002FC6 RID: 12230 RVA: 0x000B9A0C File Offset: 0x000B7C0C
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x06002FC7 RID: 12231 RVA: 0x000B9A14 File Offset: 0x000B7C14
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x06002FC8 RID: 12232 RVA: 0x000B9A1C File Offset: 0x000B7C1C
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x06002FC9 RID: 12233 RVA: 0x000B9A28 File Offset: 0x000B7C28
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x06002FCA RID: 12234 RVA: 0x000B9A30 File Offset: 0x000B7C30
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x06002FCB RID: 12235 RVA: 0x000B9A38 File Offset: 0x000B7C38
		Character get_character()
		{
			return base.character;
		}

		// Token: 0x06002FCC RID: 12236 RVA: 0x000B9A40 File Offset: 0x000B7C40
		Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x06002FCD RID: 12237 RVA: 0x000B9A48 File Offset: 0x000B7C48
		Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x06002FCE RID: 12238 RVA: 0x000B9A50 File Offset: 0x000B7C50
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x06002FCF RID: 12239 RVA: 0x000B9A58 File Offset: 0x000B7C58
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
