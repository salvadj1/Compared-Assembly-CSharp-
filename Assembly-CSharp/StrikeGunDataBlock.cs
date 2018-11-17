using System;
using uLink;
using UnityEngine;

// Token: 0x02000590 RID: 1424
public class StrikeGunDataBlock : ShotgunDataBlock
{
	// Token: 0x06003273 RID: 12915 RVA: 0x000BD258 File Offset: 0x000BB458
	protected override IInventoryItem ConstructItem()
	{
		return new StrikeGunDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x06003274 RID: 12916 RVA: 0x000BD260 File Offset: 0x000BB460
	public virtual void Local_CancelStrikes(ViewModel vm, ItemRepresentation itemRep, IStrikeGunItem itemInstance, ref HumanController.InputSample sample)
	{
		vm.CrossFade("idle", 0.15f);
	}

	// Token: 0x06003275 RID: 12917 RVA: 0x000BD274 File Offset: 0x000BB474
	public virtual void Local_BeginStrikes(int numStrikes, ViewModel vm, ItemRepresentation itemRep, IStrikeGunItem itemInstance, ref HumanController.InputSample sample)
	{
		string name = "strike" + numStrikes;
		vm.Play(name, 4);
		AudioClip clip = this.strikeSounds[numStrikes - 1];
		clip.PlayLocal(Camera.main.transform, Vector3.zero, 1f, Random.Range(0.96f, 1.03f), 2f, 2f, 0);
	}

	// Token: 0x06003276 RID: 12918 RVA: 0x000BD2DC File Offset: 0x000BB4DC
	public override string GetItemDescription()
	{
		return "Unreliable shotgun type weapon, uses homemade shells";
	}

	// Token: 0x040019F8 RID: 6648
	public float[] strikeDurations;

	// Token: 0x040019F9 RID: 6649
	public AudioClip[] strikeSounds;

	// Token: 0x02000591 RID: 1425
	private sealed class ITEM_TYPE : StrikeGunItem<StrikeGunDataBlock>, IBulletWeaponItem, IHeldItem, IInventoryItem, IStrikeGunItem, IWeaponItem
	{
		// Token: 0x06003277 RID: 12919 RVA: 0x000BD2E4 File Offset: 0x000BB4E4
		public ITEM_TYPE(StrikeGunDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06003278 RID: 12920 RVA: 0x000BD2F0 File Offset: 0x000BB4F0
		ItemDataBlock IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x06003279 RID: 12921 RVA: 0x000BD2F8 File Offset: 0x000BB4F8
		MagazineDataBlock get_clipType()
		{
			return base.clipType;
		}

		// Token: 0x0600327A RID: 12922 RVA: 0x000BD300 File Offset: 0x000BB500
		int get_clipAmmo()
		{
			return base.clipAmmo;
		}

		// Token: 0x0600327B RID: 12923 RVA: 0x000BD308 File Offset: 0x000BB508
		void set_clipAmmo(int value)
		{
			base.clipAmmo = value;
		}

		// Token: 0x0600327C RID: 12924 RVA: 0x000BD314 File Offset: 0x000BB514
		int get_cachedCasings()
		{
			return base.cachedCasings;
		}

		// Token: 0x0600327D RID: 12925 RVA: 0x000BD31C File Offset: 0x000BB51C
		void set_cachedCasings(int value)
		{
			base.cachedCasings = value;
		}

		// Token: 0x0600327E RID: 12926 RVA: 0x000BD328 File Offset: 0x000BB528
		float get_nextCasingsTime()
		{
			return base.nextCasingsTime;
		}

		// Token: 0x0600327F RID: 12927 RVA: 0x000BD330 File Offset: 0x000BB530
		void set_nextCasingsTime(float value)
		{
			base.nextCasingsTime = value;
		}

		// Token: 0x06003280 RID: 12928 RVA: 0x000BD33C File Offset: 0x000BB53C
		bool ValidatePrimaryMessageTime(double timestamp)
		{
			return base.ValidatePrimaryMessageTime(timestamp);
		}

		// Token: 0x06003281 RID: 12929 RVA: 0x000BD348 File Offset: 0x000BB548
		bool get_canAim()
		{
			return base.canAim;
		}

		// Token: 0x06003282 RID: 12930 RVA: 0x000BD350 File Offset: 0x000BB550
		float get_nextPrimaryAttackTime()
		{
			return base.nextPrimaryAttackTime;
		}

		// Token: 0x06003283 RID: 12931 RVA: 0x000BD358 File Offset: 0x000BB558
		void set_nextPrimaryAttackTime(float value)
		{
			base.nextPrimaryAttackTime = value;
		}

		// Token: 0x06003284 RID: 12932 RVA: 0x000BD364 File Offset: 0x000BB564
		float get_nextSecondaryAttackTime()
		{
			return base.nextSecondaryAttackTime;
		}

		// Token: 0x06003285 RID: 12933 RVA: 0x000BD36C File Offset: 0x000BB56C
		void set_nextSecondaryAttackTime(float value)
		{
			base.nextSecondaryAttackTime = value;
		}

		// Token: 0x06003286 RID: 12934 RVA: 0x000BD378 File Offset: 0x000BB578
		float get_deployFinishedTime()
		{
			return base.deployFinishedTime;
		}

		// Token: 0x06003287 RID: 12935 RVA: 0x000BD380 File Offset: 0x000BB580
		void set_deployFinishedTime(float value)
		{
			base.deployFinishedTime = value;
		}

		// Token: 0x06003288 RID: 12936 RVA: 0x000BD38C File Offset: 0x000BB58C
		void SetTotalModSlotCount(int count)
		{
			base.SetTotalModSlotCount(count);
		}

		// Token: 0x06003289 RID: 12937 RVA: 0x000BD398 File Offset: 0x000BB598
		void SetUsedModSlotCount(int count)
		{
			base.SetUsedModSlotCount(count);
		}

		// Token: 0x0600328A RID: 12938 RVA: 0x000BD3A4 File Offset: 0x000BB5A4
		void AddMod(ItemModDataBlock mod)
		{
			base.AddMod(mod);
		}

		// Token: 0x0600328B RID: 12939 RVA: 0x000BD3B0 File Offset: 0x000BB5B0
		int FindMod(ItemModDataBlock mod)
		{
			return base.FindMod(mod);
		}

		// Token: 0x0600328C RID: 12940 RVA: 0x000BD3BC File Offset: 0x000BB5BC
		void OnActivate()
		{
			base.OnActivate();
		}

		// Token: 0x0600328D RID: 12941 RVA: 0x000BD3C4 File Offset: 0x000BB5C4
		void OnDeactivate()
		{
			base.OnDeactivate();
		}

		// Token: 0x0600328E RID: 12942 RVA: 0x000BD3CC File Offset: 0x000BB5CC
		ViewModel get_viewModelInstance()
		{
			return base.viewModelInstance;
		}

		// Token: 0x0600328F RID: 12943 RVA: 0x000BD3D4 File Offset: 0x000BB5D4
		ItemRepresentation get_itemRepresentation()
		{
			return base.itemRepresentation;
		}

		// Token: 0x06003290 RID: 12944 RVA: 0x000BD3DC File Offset: 0x000BB5DC
		void set_itemRepresentation(ItemRepresentation value)
		{
			base.itemRepresentation = value;
		}

		// Token: 0x06003291 RID: 12945 RVA: 0x000BD3E8 File Offset: 0x000BB5E8
		bool get_canActivate()
		{
			return base.canActivate;
		}

		// Token: 0x06003292 RID: 12946 RVA: 0x000BD3F0 File Offset: 0x000BB5F0
		bool get_canDeactivate()
		{
			return base.canDeactivate;
		}

		// Token: 0x06003293 RID: 12947 RVA: 0x000BD3F8 File Offset: 0x000BB5F8
		ItemModFlags get_modFlags()
		{
			return base.modFlags;
		}

		// Token: 0x06003294 RID: 12948 RVA: 0x000BD400 File Offset: 0x000BB600
		ItemModDataBlock[] get_itemMods()
		{
			return base.itemMods;
		}

		// Token: 0x06003295 RID: 12949 RVA: 0x000BD408 File Offset: 0x000BB608
		int get_totalModSlots()
		{
			return base.totalModSlots;
		}

		// Token: 0x06003296 RID: 12950 RVA: 0x000BD410 File Offset: 0x000BB610
		int get_usedModSlots()
		{
			return base.usedModSlots;
		}

		// Token: 0x06003297 RID: 12951 RVA: 0x000BD418 File Offset: 0x000BB618
		int get_freeModSlots()
		{
			return base.freeModSlots;
		}

		// Token: 0x06003298 RID: 12952 RVA: 0x000BD420 File Offset: 0x000BB620
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x06003299 RID: 12953 RVA: 0x000BD428 File Offset: 0x000BB628
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x0600329A RID: 12954 RVA: 0x000BD430 File Offset: 0x000BB630
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x0600329B RID: 12955 RVA: 0x000BD438 File Offset: 0x000BB638
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x0600329C RID: 12956 RVA: 0x000BD444 File Offset: 0x000BB644
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x0600329D RID: 12957 RVA: 0x000BD450 File Offset: 0x000BB650
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x0600329E RID: 12958 RVA: 0x000BD45C File Offset: 0x000BB65C
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x0600329F RID: 12959 RVA: 0x000BD468 File Offset: 0x000BB668
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x060032A0 RID: 12960 RVA: 0x000BD474 File Offset: 0x000BB674
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x060032A1 RID: 12961 RVA: 0x000BD480 File Offset: 0x000BB680
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x060032A2 RID: 12962 RVA: 0x000BD48C File Offset: 0x000BB68C
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x060032A3 RID: 12963 RVA: 0x000BD498 File Offset: 0x000BB698
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x060032A4 RID: 12964 RVA: 0x000BD4A0 File Offset: 0x000BB6A0
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x060032A5 RID: 12965 RVA: 0x000BD4A8 File Offset: 0x000BB6A8
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x060032A6 RID: 12966 RVA: 0x000BD4B0 File Offset: 0x000BB6B0
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x060032A7 RID: 12967 RVA: 0x000BD4B8 File Offset: 0x000BB6B8
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x060032A8 RID: 12968 RVA: 0x000BD4C0 File Offset: 0x000BB6C0
		Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x060032A9 RID: 12969 RVA: 0x000BD4C8 File Offset: 0x000BB6C8
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x060032AA RID: 12970 RVA: 0x000BD4D0 File Offset: 0x000BB6D0
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x060032AB RID: 12971 RVA: 0x000BD4D8 File Offset: 0x000BB6D8
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x060032AC RID: 12972 RVA: 0x000BD4E4 File Offset: 0x000BB6E4
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x060032AD RID: 12973 RVA: 0x000BD4EC File Offset: 0x000BB6EC
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x060032AE RID: 12974 RVA: 0x000BD4F4 File Offset: 0x000BB6F4
		Character get_character()
		{
			return base.character;
		}

		// Token: 0x060032AF RID: 12975 RVA: 0x000BD4FC File Offset: 0x000BB6FC
		Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x060032B0 RID: 12976 RVA: 0x000BD504 File Offset: 0x000BB704
		Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x060032B1 RID: 12977 RVA: 0x000BD50C File Offset: 0x000BB70C
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x060032B2 RID: 12978 RVA: 0x000BD514 File Offset: 0x000BB714
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
