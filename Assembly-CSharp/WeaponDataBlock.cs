using System;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x0200059E RID: 1438
public class WeaponDataBlock : HeldItemDataBlock
{
	// Token: 0x060033E9 RID: 13289 RVA: 0x000BE794 File Offset: 0x000BC994
	protected override IInventoryItem ConstructItem()
	{
		return new WeaponDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x060033EA RID: 13290 RVA: 0x000BE79C File Offset: 0x000BC99C
	public virtual float GetDamage()
	{
		return Random.Range(this.damageMin, this.damageMax);
	}

	// Token: 0x060033EB RID: 13291 RVA: 0x000BE7B0 File Offset: 0x000BC9B0
	public override void InstallData(IInventoryItem item)
	{
		base.InstallData(item);
	}

	// Token: 0x060033EC RID: 13292 RVA: 0x000BE7BC File Offset: 0x000BC9BC
	protected virtual void PlayHitNotification(Vector3 point, Character shooterOrNull)
	{
		if (WeaponDataBlock._hitNotify || Bundling.Load<AudioClip>("content/shared/sfx/hitnotification", out WeaponDataBlock._hitNotify))
		{
			WeaponDataBlock._hitNotify.PlayLocal(Camera.main.transform, Vector3.zero, 1f, 1);
		}
		if (WeaponDataBlock._hitIndicator || Bundling.Load<HUDHitIndicator>("content/hud/HUDHitIndicator", out WeaponDataBlock._hitIndicator))
		{
			bool followPoint = !shooterOrNull || !shooterOrNull.stateFlags.aim;
			HUDHitIndicator.CreateIndicator(point, followPoint, WeaponDataBlock._hitIndicator);
		}
	}

	// Token: 0x060033ED RID: 13293 RVA: 0x000BE858 File Offset: 0x000BCA58
	protected override void SecureWriteMemberValues(BitStream stream)
	{
		base.SecureWriteMemberValues(stream);
		stream.Write<float>(this.deployLength, new object[0]);
		stream.Write<float>(this.damageMin, new object[0]);
		stream.Write<float>(this.damageMax, new object[0]);
		stream.Write<float>(this.fireRate, new object[0]);
		stream.Write<float>(this.fireRateSecondary, new object[0]);
		stream.Write<bool>(this.isSemiAuto, new object[0]);
	}

	// Token: 0x04001A05 RID: 6661
	public bool isSemiAuto;

	// Token: 0x04001A06 RID: 6662
	public float fireRate = 1f;

	// Token: 0x04001A07 RID: 6663
	public float fireRateSecondary = 1f;

	// Token: 0x04001A08 RID: 6664
	public float deployLength = 0.75f;

	// Token: 0x04001A09 RID: 6665
	public float damageMin = 5f;

	// Token: 0x04001A0A RID: 6666
	public float damageMax = 5f;

	// Token: 0x04001A0B RID: 6667
	public static AudioClip _hitNotify;

	// Token: 0x04001A0C RID: 6668
	private static HUDHitIndicator _hitIndicator;

	// Token: 0x0200059F RID: 1439
	private sealed class ITEM_TYPE : WeaponItem<WeaponDataBlock>, IHeldItem, IInventoryItem, IWeaponItem
	{
		// Token: 0x060033EE RID: 13294 RVA: 0x000BE8D8 File Offset: 0x000BCAD8
		public ITEM_TYPE(WeaponDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x060033EF RID: 13295 RVA: 0x000BE8E4 File Offset: 0x000BCAE4
		ItemDataBlock IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x060033F0 RID: 13296 RVA: 0x000BE8EC File Offset: 0x000BCAEC
		bool ValidatePrimaryMessageTime(double timestamp)
		{
			return base.ValidatePrimaryMessageTime(timestamp);
		}

		// Token: 0x060033F1 RID: 13297 RVA: 0x000BE8F8 File Offset: 0x000BCAF8
		bool get_canAim()
		{
			return base.canAim;
		}

		// Token: 0x060033F2 RID: 13298 RVA: 0x000BE900 File Offset: 0x000BCB00
		float get_nextPrimaryAttackTime()
		{
			return base.nextPrimaryAttackTime;
		}

		// Token: 0x060033F3 RID: 13299 RVA: 0x000BE908 File Offset: 0x000BCB08
		void set_nextPrimaryAttackTime(float value)
		{
			base.nextPrimaryAttackTime = value;
		}

		// Token: 0x060033F4 RID: 13300 RVA: 0x000BE914 File Offset: 0x000BCB14
		float get_nextSecondaryAttackTime()
		{
			return base.nextSecondaryAttackTime;
		}

		// Token: 0x060033F5 RID: 13301 RVA: 0x000BE91C File Offset: 0x000BCB1C
		void set_nextSecondaryAttackTime(float value)
		{
			base.nextSecondaryAttackTime = value;
		}

		// Token: 0x060033F6 RID: 13302 RVA: 0x000BE928 File Offset: 0x000BCB28
		float get_deployFinishedTime()
		{
			return base.deployFinishedTime;
		}

		// Token: 0x060033F7 RID: 13303 RVA: 0x000BE930 File Offset: 0x000BCB30
		void set_deployFinishedTime(float value)
		{
			base.deployFinishedTime = value;
		}

		// Token: 0x060033F8 RID: 13304 RVA: 0x000BE93C File Offset: 0x000BCB3C
		void SetTotalModSlotCount(int count)
		{
			base.SetTotalModSlotCount(count);
		}

		// Token: 0x060033F9 RID: 13305 RVA: 0x000BE948 File Offset: 0x000BCB48
		void SetUsedModSlotCount(int count)
		{
			base.SetUsedModSlotCount(count);
		}

		// Token: 0x060033FA RID: 13306 RVA: 0x000BE954 File Offset: 0x000BCB54
		void AddMod(ItemModDataBlock mod)
		{
			base.AddMod(mod);
		}

		// Token: 0x060033FB RID: 13307 RVA: 0x000BE960 File Offset: 0x000BCB60
		int FindMod(ItemModDataBlock mod)
		{
			return base.FindMod(mod);
		}

		// Token: 0x060033FC RID: 13308 RVA: 0x000BE96C File Offset: 0x000BCB6C
		void OnActivate()
		{
			base.OnActivate();
		}

		// Token: 0x060033FD RID: 13309 RVA: 0x000BE974 File Offset: 0x000BCB74
		void OnDeactivate()
		{
			base.OnDeactivate();
		}

		// Token: 0x060033FE RID: 13310 RVA: 0x000BE97C File Offset: 0x000BCB7C
		ViewModel get_viewModelInstance()
		{
			return base.viewModelInstance;
		}

		// Token: 0x060033FF RID: 13311 RVA: 0x000BE984 File Offset: 0x000BCB84
		ItemRepresentation get_itemRepresentation()
		{
			return base.itemRepresentation;
		}

		// Token: 0x06003400 RID: 13312 RVA: 0x000BE98C File Offset: 0x000BCB8C
		void set_itemRepresentation(ItemRepresentation value)
		{
			base.itemRepresentation = value;
		}

		// Token: 0x06003401 RID: 13313 RVA: 0x000BE998 File Offset: 0x000BCB98
		bool get_canActivate()
		{
			return base.canActivate;
		}

		// Token: 0x06003402 RID: 13314 RVA: 0x000BE9A0 File Offset: 0x000BCBA0
		bool get_canDeactivate()
		{
			return base.canDeactivate;
		}

		// Token: 0x06003403 RID: 13315 RVA: 0x000BE9A8 File Offset: 0x000BCBA8
		ItemModFlags get_modFlags()
		{
			return base.modFlags;
		}

		// Token: 0x06003404 RID: 13316 RVA: 0x000BE9B0 File Offset: 0x000BCBB0
		ItemModDataBlock[] get_itemMods()
		{
			return base.itemMods;
		}

		// Token: 0x06003405 RID: 13317 RVA: 0x000BE9B8 File Offset: 0x000BCBB8
		int get_totalModSlots()
		{
			return base.totalModSlots;
		}

		// Token: 0x06003406 RID: 13318 RVA: 0x000BE9C0 File Offset: 0x000BCBC0
		int get_usedModSlots()
		{
			return base.usedModSlots;
		}

		// Token: 0x06003407 RID: 13319 RVA: 0x000BE9C8 File Offset: 0x000BCBC8
		int get_freeModSlots()
		{
			return base.freeModSlots;
		}

		// Token: 0x06003408 RID: 13320 RVA: 0x000BE9D0 File Offset: 0x000BCBD0
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x06003409 RID: 13321 RVA: 0x000BE9D8 File Offset: 0x000BCBD8
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x0600340A RID: 13322 RVA: 0x000BE9E0 File Offset: 0x000BCBE0
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x0600340B RID: 13323 RVA: 0x000BE9E8 File Offset: 0x000BCBE8
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x0600340C RID: 13324 RVA: 0x000BE9F4 File Offset: 0x000BCBF4
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x0600340D RID: 13325 RVA: 0x000BEA00 File Offset: 0x000BCC00
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x0600340E RID: 13326 RVA: 0x000BEA0C File Offset: 0x000BCC0C
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x0600340F RID: 13327 RVA: 0x000BEA18 File Offset: 0x000BCC18
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x06003410 RID: 13328 RVA: 0x000BEA24 File Offset: 0x000BCC24
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x06003411 RID: 13329 RVA: 0x000BEA30 File Offset: 0x000BCC30
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x06003412 RID: 13330 RVA: 0x000BEA3C File Offset: 0x000BCC3C
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x06003413 RID: 13331 RVA: 0x000BEA48 File Offset: 0x000BCC48
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x06003414 RID: 13332 RVA: 0x000BEA50 File Offset: 0x000BCC50
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x06003415 RID: 13333 RVA: 0x000BEA58 File Offset: 0x000BCC58
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x06003416 RID: 13334 RVA: 0x000BEA60 File Offset: 0x000BCC60
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x06003417 RID: 13335 RVA: 0x000BEA68 File Offset: 0x000BCC68
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x06003418 RID: 13336 RVA: 0x000BEA70 File Offset: 0x000BCC70
		Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x06003419 RID: 13337 RVA: 0x000BEA78 File Offset: 0x000BCC78
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x0600341A RID: 13338 RVA: 0x000BEA80 File Offset: 0x000BCC80
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x0600341B RID: 13339 RVA: 0x000BEA88 File Offset: 0x000BCC88
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x0600341C RID: 13340 RVA: 0x000BEA94 File Offset: 0x000BCC94
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x0600341D RID: 13341 RVA: 0x000BEA9C File Offset: 0x000BCC9C
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x0600341E RID: 13342 RVA: 0x000BEAA4 File Offset: 0x000BCCA4
		Character get_character()
		{
			return base.character;
		}

		// Token: 0x0600341F RID: 13343 RVA: 0x000BEAAC File Offset: 0x000BCCAC
		Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x06003420 RID: 13344 RVA: 0x000BEAB4 File Offset: 0x000BCCB4
		Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x06003421 RID: 13345 RVA: 0x000BEABC File Offset: 0x000BCCBC
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x06003422 RID: 13346 RVA: 0x000BEAC4 File Offset: 0x000BCCC4
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
