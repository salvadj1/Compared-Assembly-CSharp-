using System;
using uLink;
using UnityEngine;

// Token: 0x02000624 RID: 1572
public class ConsumableDataBlock : global::ItemDataBlock
{
	// Token: 0x06003373 RID: 13171 RVA: 0x000C17D8 File Offset: 0x000BF9D8
	protected override global::IInventoryItem ConstructItem()
	{
		return new global::ConsumableDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x06003374 RID: 13172 RVA: 0x000C17E0 File Offset: 0x000BF9E0
	public override int RetreiveMenuOptions(global::IInventoryItem item, global::InventoryItem.MenuItem[] results, int offset)
	{
		offset = base.RetreiveMenuOptions(item, results, offset);
		if (item.isInLocalInventory)
		{
			results[offset++] = this.GetConsumeMenuItem();
		}
		return offset;
	}

	// Token: 0x06003375 RID: 13173 RVA: 0x000C1814 File Offset: 0x000BFA14
	public global::InventoryItem.MenuItem GetConsumeMenuItem()
	{
		if (this.calories > 0f && this.litresOfWater <= 0f)
		{
			return global::InventoryItem.MenuItem.Eat;
		}
		if (this.litresOfWater > 0f && this.calories <= 0f)
		{
			return global::InventoryItem.MenuItem.Drink;
		}
		return global::InventoryItem.MenuItem.Consume;
	}

	// Token: 0x06003376 RID: 13174 RVA: 0x000C1868 File Offset: 0x000BFA68
	public override global::InventoryItem.MenuItemResult ExecuteMenuOption(global::InventoryItem.MenuItem option, global::IInventoryItem item)
	{
		if (option == this.GetConsumeMenuItem())
		{
			return global::InventoryItem.MenuItemResult.DoneOnServer;
		}
		return base.ExecuteMenuOption(option, item);
	}

	// Token: 0x06003377 RID: 13175 RVA: 0x000C1880 File Offset: 0x000BFA80
	public virtual void UseItem(global::IConsumableItem item)
	{
		global::Inventory inventory = item.inventory;
		global::Metabolism local = inventory.GetLocal<global::Metabolism>();
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
			global::HumanBodyTakeDamage local2 = inventory.GetLocal<global::HumanBodyTakeDamage>();
			if (local2 != null)
			{
				if (this.healthToHeal > 0f)
				{
					local2.HealOverTime(this.healthToHeal);
				}
				else
				{
					global::TakeDamage.HurtSelf(inventory.idMain, Mathf.Abs(this.healthToHeal), null);
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

	// Token: 0x06003378 RID: 13176 RVA: 0x000C19B8 File Offset: 0x000BFBB8
	public override void PopulateInfoWindow(global::ItemToolTip infoWindow, global::IInventoryItem tipItem)
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

	// Token: 0x06003379 RID: 13177 RVA: 0x000C1AE0 File Offset: 0x000BFCE0
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

	// Token: 0x04001B26 RID: 6950
	public float litresOfWater;

	// Token: 0x04001B27 RID: 6951
	public float calories;

	// Token: 0x04001B28 RID: 6952
	public float antiRads;

	// Token: 0x04001B29 RID: 6953
	public float healthToHeal;

	// Token: 0x04001B2A RID: 6954
	public float poisonAmount;

	// Token: 0x04001B2B RID: 6955
	public bool cookable;

	// Token: 0x04001B2C RID: 6956
	public int numToCookPerTick;

	// Token: 0x04001B2D RID: 6957
	public global::ItemDataBlock cookedVersion;

	// Token: 0x04001B2E RID: 6958
	public int burnTemp = 10;

	// Token: 0x04001B2F RID: 6959
	public int cookHeatRequirement = 1;

	// Token: 0x02000625 RID: 1573
	private sealed class ITEM_TYPE : global::ConsumableItem<global::ConsumableDataBlock>, global::IConsumableItem, global::ICookableItem, global::IInventoryItem
	{
		// Token: 0x0600337A RID: 13178 RVA: 0x000C1B9C File Offset: 0x000BFD9C
		public ITEM_TYPE(global::ConsumableDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x0600337B RID: 13179 RVA: 0x000C1BA8 File Offset: 0x000BFDA8
		global::ItemDataBlock global::IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x0600337C RID: 13180 RVA: 0x000C1BB0 File Offset: 0x000BFDB0
		bool GetCookableInfo(out int consumeCount, out global::ItemDataBlock cookedVersion, out int cookedCount, out int cookTempMin, out int burnTemp)
		{
			return base.GetCookableInfo(out consumeCount, out cookedVersion, out cookedCount, out cookTempMin, out burnTemp);
		}

		// Token: 0x0600337D RID: 13181 RVA: 0x000C1BC0 File Offset: 0x000BFDC0
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x0600337E RID: 13182 RVA: 0x000C1BC8 File Offset: 0x000BFDC8
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x0600337F RID: 13183 RVA: 0x000C1BD0 File Offset: 0x000BFDD0
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x06003380 RID: 13184 RVA: 0x000C1BD8 File Offset: 0x000BFDD8
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x06003381 RID: 13185 RVA: 0x000C1BE4 File Offset: 0x000BFDE4
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x06003382 RID: 13186 RVA: 0x000C1BF0 File Offset: 0x000BFDF0
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x06003383 RID: 13187 RVA: 0x000C1BFC File Offset: 0x000BFDFC
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x06003384 RID: 13188 RVA: 0x000C1C08 File Offset: 0x000BFE08
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x06003385 RID: 13189 RVA: 0x000C1C14 File Offset: 0x000BFE14
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x06003386 RID: 13190 RVA: 0x000C1C20 File Offset: 0x000BFE20
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x06003387 RID: 13191 RVA: 0x000C1C2C File Offset: 0x000BFE2C
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x06003388 RID: 13192 RVA: 0x000C1C38 File Offset: 0x000BFE38
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x06003389 RID: 13193 RVA: 0x000C1C40 File Offset: 0x000BFE40
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x0600338A RID: 13194 RVA: 0x000C1C48 File Offset: 0x000BFE48
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x0600338B RID: 13195 RVA: 0x000C1C50 File Offset: 0x000BFE50
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x0600338C RID: 13196 RVA: 0x000C1C58 File Offset: 0x000BFE58
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x0600338D RID: 13197 RVA: 0x000C1C60 File Offset: 0x000BFE60
		global::Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x0600338E RID: 13198 RVA: 0x000C1C68 File Offset: 0x000BFE68
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x0600338F RID: 13199 RVA: 0x000C1C70 File Offset: 0x000BFE70
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x06003390 RID: 13200 RVA: 0x000C1C78 File Offset: 0x000BFE78
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x06003391 RID: 13201 RVA: 0x000C1C84 File Offset: 0x000BFE84
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x06003392 RID: 13202 RVA: 0x000C1C8C File Offset: 0x000BFE8C
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x06003393 RID: 13203 RVA: 0x000C1C94 File Offset: 0x000BFE94
		global::Character get_character()
		{
			return base.character;
		}

		// Token: 0x06003394 RID: 13204 RVA: 0x000C1C9C File Offset: 0x000BFE9C
		global::Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x06003395 RID: 13205 RVA: 0x000C1CA4 File Offset: 0x000BFEA4
		global::Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x06003396 RID: 13206 RVA: 0x000C1CAC File Offset: 0x000BFEAC
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x06003397 RID: 13207 RVA: 0x000C1CB4 File Offset: 0x000BFEB4
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
