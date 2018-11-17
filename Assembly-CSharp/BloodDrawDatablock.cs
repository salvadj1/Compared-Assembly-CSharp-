using System;
using uLink;
using UnityEngine;

// Token: 0x02000616 RID: 1558
public class BloodDrawDatablock : global::ItemDataBlock
{
	// Token: 0x06003265 RID: 12901 RVA: 0x000BF6CC File Offset: 0x000BD8CC
	protected override global::IInventoryItem ConstructItem()
	{
		return new global::BloodDrawDatablock.ITEM_TYPE(this);
	}

	// Token: 0x06003266 RID: 12902 RVA: 0x000BF6D4 File Offset: 0x000BD8D4
	public override int RetreiveMenuOptions(global::IInventoryItem item, global::InventoryItem.MenuItem[] results, int offset)
	{
		offset = base.RetreiveMenuOptions(item, results, offset);
		if (item.isInLocalInventory)
		{
			results[offset++] = global::InventoryItem.MenuItem.Use;
		}
		return offset;
	}

	// Token: 0x06003267 RID: 12903 RVA: 0x000BF704 File Offset: 0x000BD904
	public override global::InventoryItem.MenuItemResult ExecuteMenuOption(global::InventoryItem.MenuItem option, global::IInventoryItem item)
	{
		if (option != global::InventoryItem.MenuItem.Use)
		{
			return base.ExecuteMenuOption(option, item);
		}
		return global::InventoryItem.MenuItemResult.DoneOnServer;
	}

	// Token: 0x06003268 RID: 12904 RVA: 0x000BF72C File Offset: 0x000BD92C
	public virtual void UseItem(global::IBloodDrawItem draw)
	{
		if (Time.time < draw.lastUseTime + 2f)
		{
			return;
		}
		global::Inventory inventory = draw.inventory;
		global::HumanBodyTakeDamage local = inventory.GetLocal<global::HumanBodyTakeDamage>();
		if (local.health <= this.bloodToTake)
		{
			return;
		}
		IDMain idMain = inventory.idMain;
		global::TakeDamage.Hurt(idMain, idMain, this.bloodToTake, null);
		inventory.AddItem(ref global::BloodDrawDatablock.LateLoaded.blood, global::Inventory.Slot.Preference.Define(global::Inventory.Slot.Kind.Default, true, global::Inventory.Slot.KindFlags.Belt), 1);
		draw.lastUseTime = Time.time;
	}

	// Token: 0x06003269 RID: 12905 RVA: 0x000BF7B0 File Offset: 0x000BD9B0
	public override string GetItemDescription()
	{
		return "Used to extract your own blood, perhaps to make a medkit";
	}

	// Token: 0x04001AE7 RID: 6887
	public float bloodToTake = 25f;

	// Token: 0x02000617 RID: 1559
	private sealed class ITEM_TYPE : global::BloodDrawItem<global::BloodDrawDatablock>, global::IBloodDrawItem, global::IInventoryItem
	{
		// Token: 0x0600326A RID: 12906 RVA: 0x000BF7B8 File Offset: 0x000BD9B8
		public ITEM_TYPE(global::BloodDrawDatablock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x0600326B RID: 12907 RVA: 0x000BF7C4 File Offset: 0x000BD9C4
		global::ItemDataBlock global::IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x0600326C RID: 12908 RVA: 0x000BF7CC File Offset: 0x000BD9CC
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x0600326D RID: 12909 RVA: 0x000BF7D4 File Offset: 0x000BD9D4
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x0600326E RID: 12910 RVA: 0x000BF7DC File Offset: 0x000BD9DC
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x0600326F RID: 12911 RVA: 0x000BF7E4 File Offset: 0x000BD9E4
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x06003270 RID: 12912 RVA: 0x000BF7F0 File Offset: 0x000BD9F0
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x06003271 RID: 12913 RVA: 0x000BF7FC File Offset: 0x000BD9FC
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x06003272 RID: 12914 RVA: 0x000BF808 File Offset: 0x000BDA08
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x06003273 RID: 12915 RVA: 0x000BF814 File Offset: 0x000BDA14
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x06003274 RID: 12916 RVA: 0x000BF820 File Offset: 0x000BDA20
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x06003275 RID: 12917 RVA: 0x000BF82C File Offset: 0x000BDA2C
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x06003276 RID: 12918 RVA: 0x000BF838 File Offset: 0x000BDA38
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x06003277 RID: 12919 RVA: 0x000BF844 File Offset: 0x000BDA44
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x06003278 RID: 12920 RVA: 0x000BF84C File Offset: 0x000BDA4C
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x06003279 RID: 12921 RVA: 0x000BF854 File Offset: 0x000BDA54
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x0600327A RID: 12922 RVA: 0x000BF85C File Offset: 0x000BDA5C
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x0600327B RID: 12923 RVA: 0x000BF864 File Offset: 0x000BDA64
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x0600327C RID: 12924 RVA: 0x000BF86C File Offset: 0x000BDA6C
		global::Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x0600327D RID: 12925 RVA: 0x000BF874 File Offset: 0x000BDA74
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x0600327E RID: 12926 RVA: 0x000BF87C File Offset: 0x000BDA7C
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x0600327F RID: 12927 RVA: 0x000BF884 File Offset: 0x000BDA84
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x06003280 RID: 12928 RVA: 0x000BF890 File Offset: 0x000BDA90
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x06003281 RID: 12929 RVA: 0x000BF898 File Offset: 0x000BDA98
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x06003282 RID: 12930 RVA: 0x000BF8A0 File Offset: 0x000BDAA0
		global::Character get_character()
		{
			return base.character;
		}

		// Token: 0x06003283 RID: 12931 RVA: 0x000BF8A8 File Offset: 0x000BDAA8
		global::Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x06003284 RID: 12932 RVA: 0x000BF8B0 File Offset: 0x000BDAB0
		global::Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x06003285 RID: 12933 RVA: 0x000BF8B8 File Offset: 0x000BDAB8
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x06003286 RID: 12934 RVA: 0x000BF8C0 File Offset: 0x000BDAC0
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}

	// Token: 0x02000618 RID: 1560
	private static class LateLoaded
	{
		// Token: 0x04001AE8 RID: 6888
		public static global::Datablock.Ident blood = "Blood";
	}
}
