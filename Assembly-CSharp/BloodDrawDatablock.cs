using System;
using uLink;
using UnityEngine;

// Token: 0x02000558 RID: 1368
public class BloodDrawDatablock : ItemDataBlock
{
	// Token: 0x06002E9D RID: 11933 RVA: 0x000B7470 File Offset: 0x000B5670
	protected override IInventoryItem ConstructItem()
	{
		return new BloodDrawDatablock.ITEM_TYPE(this);
	}

	// Token: 0x06002E9E RID: 11934 RVA: 0x000B7478 File Offset: 0x000B5678
	public override int RetreiveMenuOptions(IInventoryItem item, InventoryItem.MenuItem[] results, int offset)
	{
		offset = base.RetreiveMenuOptions(item, results, offset);
		if (item.isInLocalInventory)
		{
			results[offset++] = InventoryItem.MenuItem.Use;
		}
		return offset;
	}

	// Token: 0x06002E9F RID: 11935 RVA: 0x000B74A8 File Offset: 0x000B56A8
	public override InventoryItem.MenuItemResult ExecuteMenuOption(InventoryItem.MenuItem option, IInventoryItem item)
	{
		if (option != InventoryItem.MenuItem.Use)
		{
			return base.ExecuteMenuOption(option, item);
		}
		return InventoryItem.MenuItemResult.DoneOnServer;
	}

	// Token: 0x06002EA0 RID: 11936 RVA: 0x000B74D0 File Offset: 0x000B56D0
	public virtual void UseItem(IBloodDrawItem draw)
	{
		if (Time.time < draw.lastUseTime + 2f)
		{
			return;
		}
		Inventory inventory = draw.inventory;
		HumanBodyTakeDamage local = inventory.GetLocal<HumanBodyTakeDamage>();
		if (local.health <= this.bloodToTake)
		{
			return;
		}
		IDMain idMain = inventory.idMain;
		TakeDamage.Hurt(idMain, idMain, this.bloodToTake, null);
		inventory.AddItem(ref BloodDrawDatablock.LateLoaded.blood, Inventory.Slot.Preference.Define(Inventory.Slot.Kind.Default, true, Inventory.Slot.KindFlags.Belt), 1);
		draw.lastUseTime = Time.time;
	}

	// Token: 0x06002EA1 RID: 11937 RVA: 0x000B7554 File Offset: 0x000B5754
	public override string GetItemDescription()
	{
		return "Used to extract your own blood, perhaps to make a medkit";
	}

	// Token: 0x04001916 RID: 6422
	public float bloodToTake = 25f;

	// Token: 0x02000559 RID: 1369
	private sealed class ITEM_TYPE : BloodDrawItem<BloodDrawDatablock>, IBloodDrawItem, IInventoryItem
	{
		// Token: 0x06002EA2 RID: 11938 RVA: 0x000B755C File Offset: 0x000B575C
		public ITEM_TYPE(BloodDrawDatablock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x06002EA3 RID: 11939 RVA: 0x000B7568 File Offset: 0x000B5768
		ItemDataBlock IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x06002EA4 RID: 11940 RVA: 0x000B7570 File Offset: 0x000B5770
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x06002EA5 RID: 11941 RVA: 0x000B7578 File Offset: 0x000B5778
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x06002EA6 RID: 11942 RVA: 0x000B7580 File Offset: 0x000B5780
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x06002EA7 RID: 11943 RVA: 0x000B7588 File Offset: 0x000B5788
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x06002EA8 RID: 11944 RVA: 0x000B7594 File Offset: 0x000B5794
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x06002EA9 RID: 11945 RVA: 0x000B75A0 File Offset: 0x000B57A0
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x06002EAA RID: 11946 RVA: 0x000B75AC File Offset: 0x000B57AC
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x06002EAB RID: 11947 RVA: 0x000B75B8 File Offset: 0x000B57B8
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x06002EAC RID: 11948 RVA: 0x000B75C4 File Offset: 0x000B57C4
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x06002EAD RID: 11949 RVA: 0x000B75D0 File Offset: 0x000B57D0
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x06002EAE RID: 11950 RVA: 0x000B75DC File Offset: 0x000B57DC
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x06002EAF RID: 11951 RVA: 0x000B75E8 File Offset: 0x000B57E8
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x06002EB0 RID: 11952 RVA: 0x000B75F0 File Offset: 0x000B57F0
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x06002EB1 RID: 11953 RVA: 0x000B75F8 File Offset: 0x000B57F8
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x06002EB2 RID: 11954 RVA: 0x000B7600 File Offset: 0x000B5800
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x06002EB3 RID: 11955 RVA: 0x000B7608 File Offset: 0x000B5808
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x06002EB4 RID: 11956 RVA: 0x000B7610 File Offset: 0x000B5810
		Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x06002EB5 RID: 11957 RVA: 0x000B7618 File Offset: 0x000B5818
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x06002EB6 RID: 11958 RVA: 0x000B7620 File Offset: 0x000B5820
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x06002EB7 RID: 11959 RVA: 0x000B7628 File Offset: 0x000B5828
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x06002EB8 RID: 11960 RVA: 0x000B7634 File Offset: 0x000B5834
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x06002EB9 RID: 11961 RVA: 0x000B763C File Offset: 0x000B583C
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x06002EBA RID: 11962 RVA: 0x000B7644 File Offset: 0x000B5844
		Character get_character()
		{
			return base.character;
		}

		// Token: 0x06002EBB RID: 11963 RVA: 0x000B764C File Offset: 0x000B584C
		Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x06002EBC RID: 11964 RVA: 0x000B7654 File Offset: 0x000B5854
		Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x06002EBD RID: 11965 RVA: 0x000B765C File Offset: 0x000B585C
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x06002EBE RID: 11966 RVA: 0x000B7664 File Offset: 0x000B5864
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}

	// Token: 0x0200055A RID: 1370
	private static class LateLoaded
	{
		// Token: 0x04001917 RID: 6423
		public static Datablock.Ident blood = "Blood";
	}
}
