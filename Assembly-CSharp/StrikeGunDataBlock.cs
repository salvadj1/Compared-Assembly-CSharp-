using System;
using uLink;
using UnityEngine;

// Token: 0x0200064E RID: 1614
public class StrikeGunDataBlock : global::ShotgunDataBlock
{
	// Token: 0x0600363B RID: 13883 RVA: 0x000C54B4 File Offset: 0x000C36B4
	protected override global::IInventoryItem ConstructItem()
	{
		return new global::StrikeGunDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x0600363C RID: 13884 RVA: 0x000C54BC File Offset: 0x000C36BC
	public virtual void Local_CancelStrikes(global::ViewModel vm, global::ItemRepresentation itemRep, global::IStrikeGunItem itemInstance, ref global::HumanController.InputSample sample)
	{
		vm.CrossFade("idle", 0.15f);
	}

	// Token: 0x0600363D RID: 13885 RVA: 0x000C54D0 File Offset: 0x000C36D0
	public virtual void Local_BeginStrikes(int numStrikes, global::ViewModel vm, global::ItemRepresentation itemRep, global::IStrikeGunItem itemInstance, ref global::HumanController.InputSample sample)
	{
		string name = "strike" + numStrikes;
		vm.Play(name, 4);
		AudioClip clip = this.strikeSounds[numStrikes - 1];
		clip.PlayLocal(Camera.main.transform, Vector3.zero, 1f, Random.Range(0.96f, 1.03f), 2f, 2f, 0);
	}

	// Token: 0x0600363E RID: 13886 RVA: 0x000C5538 File Offset: 0x000C3738
	public override string GetItemDescription()
	{
		return "Unreliable shotgun type weapon, uses homemade shells";
	}

	// Token: 0x04001BC9 RID: 7113
	public float[] strikeDurations;

	// Token: 0x04001BCA RID: 7114
	public AudioClip[] strikeSounds;

	// Token: 0x0200064F RID: 1615
	private sealed class ITEM_TYPE : global::StrikeGunItem<global::StrikeGunDataBlock>, global::IBulletWeaponItem, global::IHeldItem, global::IInventoryItem, global::IStrikeGunItem, global::IWeaponItem
	{
		// Token: 0x0600363F RID: 13887 RVA: 0x000C5540 File Offset: 0x000C3740
		public ITEM_TYPE(global::StrikeGunDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x06003640 RID: 13888 RVA: 0x000C554C File Offset: 0x000C374C
		global::ItemDataBlock global::IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x06003641 RID: 13889 RVA: 0x000C5554 File Offset: 0x000C3754
		global::MagazineDataBlock get_clipType()
		{
			return base.clipType;
		}

		// Token: 0x06003642 RID: 13890 RVA: 0x000C555C File Offset: 0x000C375C
		int get_clipAmmo()
		{
			return base.clipAmmo;
		}

		// Token: 0x06003643 RID: 13891 RVA: 0x000C5564 File Offset: 0x000C3764
		void set_clipAmmo(int value)
		{
			base.clipAmmo = value;
		}

		// Token: 0x06003644 RID: 13892 RVA: 0x000C5570 File Offset: 0x000C3770
		int get_cachedCasings()
		{
			return base.cachedCasings;
		}

		// Token: 0x06003645 RID: 13893 RVA: 0x000C5578 File Offset: 0x000C3778
		void set_cachedCasings(int value)
		{
			base.cachedCasings = value;
		}

		// Token: 0x06003646 RID: 13894 RVA: 0x000C5584 File Offset: 0x000C3784
		float get_nextCasingsTime()
		{
			return base.nextCasingsTime;
		}

		// Token: 0x06003647 RID: 13895 RVA: 0x000C558C File Offset: 0x000C378C
		void set_nextCasingsTime(float value)
		{
			base.nextCasingsTime = value;
		}

		// Token: 0x06003648 RID: 13896 RVA: 0x000C5598 File Offset: 0x000C3798
		bool ValidatePrimaryMessageTime(double timestamp)
		{
			return base.ValidatePrimaryMessageTime(timestamp);
		}

		// Token: 0x06003649 RID: 13897 RVA: 0x000C55A4 File Offset: 0x000C37A4
		bool get_canAim()
		{
			return base.canAim;
		}

		// Token: 0x0600364A RID: 13898 RVA: 0x000C55AC File Offset: 0x000C37AC
		float get_nextPrimaryAttackTime()
		{
			return base.nextPrimaryAttackTime;
		}

		// Token: 0x0600364B RID: 13899 RVA: 0x000C55B4 File Offset: 0x000C37B4
		void set_nextPrimaryAttackTime(float value)
		{
			base.nextPrimaryAttackTime = value;
		}

		// Token: 0x0600364C RID: 13900 RVA: 0x000C55C0 File Offset: 0x000C37C0
		float get_nextSecondaryAttackTime()
		{
			return base.nextSecondaryAttackTime;
		}

		// Token: 0x0600364D RID: 13901 RVA: 0x000C55C8 File Offset: 0x000C37C8
		void set_nextSecondaryAttackTime(float value)
		{
			base.nextSecondaryAttackTime = value;
		}

		// Token: 0x0600364E RID: 13902 RVA: 0x000C55D4 File Offset: 0x000C37D4
		float get_deployFinishedTime()
		{
			return base.deployFinishedTime;
		}

		// Token: 0x0600364F RID: 13903 RVA: 0x000C55DC File Offset: 0x000C37DC
		void set_deployFinishedTime(float value)
		{
			base.deployFinishedTime = value;
		}

		// Token: 0x06003650 RID: 13904 RVA: 0x000C55E8 File Offset: 0x000C37E8
		void SetTotalModSlotCount(int count)
		{
			base.SetTotalModSlotCount(count);
		}

		// Token: 0x06003651 RID: 13905 RVA: 0x000C55F4 File Offset: 0x000C37F4
		void SetUsedModSlotCount(int count)
		{
			base.SetUsedModSlotCount(count);
		}

		// Token: 0x06003652 RID: 13906 RVA: 0x000C5600 File Offset: 0x000C3800
		void AddMod(global::ItemModDataBlock mod)
		{
			base.AddMod(mod);
		}

		// Token: 0x06003653 RID: 13907 RVA: 0x000C560C File Offset: 0x000C380C
		int FindMod(global::ItemModDataBlock mod)
		{
			return base.FindMod(mod);
		}

		// Token: 0x06003654 RID: 13908 RVA: 0x000C5618 File Offset: 0x000C3818
		void OnActivate()
		{
			base.OnActivate();
		}

		// Token: 0x06003655 RID: 13909 RVA: 0x000C5620 File Offset: 0x000C3820
		void OnDeactivate()
		{
			base.OnDeactivate();
		}

		// Token: 0x06003656 RID: 13910 RVA: 0x000C5628 File Offset: 0x000C3828
		global::ViewModel get_viewModelInstance()
		{
			return base.viewModelInstance;
		}

		// Token: 0x06003657 RID: 13911 RVA: 0x000C5630 File Offset: 0x000C3830
		global::ItemRepresentation get_itemRepresentation()
		{
			return base.itemRepresentation;
		}

		// Token: 0x06003658 RID: 13912 RVA: 0x000C5638 File Offset: 0x000C3838
		void set_itemRepresentation(global::ItemRepresentation value)
		{
			base.itemRepresentation = value;
		}

		// Token: 0x06003659 RID: 13913 RVA: 0x000C5644 File Offset: 0x000C3844
		bool get_canActivate()
		{
			return base.canActivate;
		}

		// Token: 0x0600365A RID: 13914 RVA: 0x000C564C File Offset: 0x000C384C
		bool get_canDeactivate()
		{
			return base.canDeactivate;
		}

		// Token: 0x0600365B RID: 13915 RVA: 0x000C5654 File Offset: 0x000C3854
		global::ItemModFlags get_modFlags()
		{
			return base.modFlags;
		}

		// Token: 0x0600365C RID: 13916 RVA: 0x000C565C File Offset: 0x000C385C
		global::ItemModDataBlock[] get_itemMods()
		{
			return base.itemMods;
		}

		// Token: 0x0600365D RID: 13917 RVA: 0x000C5664 File Offset: 0x000C3864
		int get_totalModSlots()
		{
			return base.totalModSlots;
		}

		// Token: 0x0600365E RID: 13918 RVA: 0x000C566C File Offset: 0x000C386C
		int get_usedModSlots()
		{
			return base.usedModSlots;
		}

		// Token: 0x0600365F RID: 13919 RVA: 0x000C5674 File Offset: 0x000C3874
		int get_freeModSlots()
		{
			return base.freeModSlots;
		}

		// Token: 0x06003660 RID: 13920 RVA: 0x000C567C File Offset: 0x000C387C
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x06003661 RID: 13921 RVA: 0x000C5684 File Offset: 0x000C3884
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x06003662 RID: 13922 RVA: 0x000C568C File Offset: 0x000C388C
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x06003663 RID: 13923 RVA: 0x000C5694 File Offset: 0x000C3894
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x06003664 RID: 13924 RVA: 0x000C56A0 File Offset: 0x000C38A0
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x06003665 RID: 13925 RVA: 0x000C56AC File Offset: 0x000C38AC
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x06003666 RID: 13926 RVA: 0x000C56B8 File Offset: 0x000C38B8
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x06003667 RID: 13927 RVA: 0x000C56C4 File Offset: 0x000C38C4
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x06003668 RID: 13928 RVA: 0x000C56D0 File Offset: 0x000C38D0
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x06003669 RID: 13929 RVA: 0x000C56DC File Offset: 0x000C38DC
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x0600366A RID: 13930 RVA: 0x000C56E8 File Offset: 0x000C38E8
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x0600366B RID: 13931 RVA: 0x000C56F4 File Offset: 0x000C38F4
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x0600366C RID: 13932 RVA: 0x000C56FC File Offset: 0x000C38FC
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x0600366D RID: 13933 RVA: 0x000C5704 File Offset: 0x000C3904
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x0600366E RID: 13934 RVA: 0x000C570C File Offset: 0x000C390C
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x0600366F RID: 13935 RVA: 0x000C5714 File Offset: 0x000C3914
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x06003670 RID: 13936 RVA: 0x000C571C File Offset: 0x000C391C
		global::Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x06003671 RID: 13937 RVA: 0x000C5724 File Offset: 0x000C3924
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x06003672 RID: 13938 RVA: 0x000C572C File Offset: 0x000C392C
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x06003673 RID: 13939 RVA: 0x000C5734 File Offset: 0x000C3934
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x06003674 RID: 13940 RVA: 0x000C5740 File Offset: 0x000C3940
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x06003675 RID: 13941 RVA: 0x000C5748 File Offset: 0x000C3948
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x06003676 RID: 13942 RVA: 0x000C5750 File Offset: 0x000C3950
		global::Character get_character()
		{
			return base.character;
		}

		// Token: 0x06003677 RID: 13943 RVA: 0x000C5758 File Offset: 0x000C3958
		global::Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x06003678 RID: 13944 RVA: 0x000C5760 File Offset: 0x000C3960
		global::Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x06003679 RID: 13945 RVA: 0x000C5768 File Offset: 0x000C3968
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x0600367A RID: 13946 RVA: 0x000C5770 File Offset: 0x000C3970
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
