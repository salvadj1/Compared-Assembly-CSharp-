using System;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x0200065C RID: 1628
public class WeaponDataBlock : global::HeldItemDataBlock
{
	// Token: 0x060037B1 RID: 14257 RVA: 0x000C69F0 File Offset: 0x000C4BF0
	protected override global::IInventoryItem ConstructItem()
	{
		return new global::WeaponDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x060037B2 RID: 14258 RVA: 0x000C69F8 File Offset: 0x000C4BF8
	public virtual float GetDamage()
	{
		return Random.Range(this.damageMin, this.damageMax);
	}

	// Token: 0x060037B3 RID: 14259 RVA: 0x000C6A0C File Offset: 0x000C4C0C
	public override void InstallData(global::IInventoryItem item)
	{
		base.InstallData(item);
	}

	// Token: 0x060037B4 RID: 14260 RVA: 0x000C6A18 File Offset: 0x000C4C18
	protected virtual void PlayHitNotification(Vector3 point, global::Character shooterOrNull)
	{
		if (global::WeaponDataBlock._hitNotify || Facepunch.Bundling.Load<AudioClip>("content/shared/sfx/hitnotification", out global::WeaponDataBlock._hitNotify))
		{
			global::WeaponDataBlock._hitNotify.PlayLocal(Camera.main.transform, Vector3.zero, 1f, 1);
		}
		if (global::WeaponDataBlock._hitIndicator || Facepunch.Bundling.Load<global::HUDHitIndicator>("content/hud/HUDHitIndicator", out global::WeaponDataBlock._hitIndicator))
		{
			bool followPoint = !shooterOrNull || !shooterOrNull.stateFlags.aim;
			global::HUDHitIndicator.CreateIndicator(point, followPoint, global::WeaponDataBlock._hitIndicator);
		}
	}

	// Token: 0x060037B5 RID: 14261 RVA: 0x000C6AB4 File Offset: 0x000C4CB4
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

	// Token: 0x04001BD6 RID: 7126
	public bool isSemiAuto;

	// Token: 0x04001BD7 RID: 7127
	public float fireRate = 1f;

	// Token: 0x04001BD8 RID: 7128
	public float fireRateSecondary = 1f;

	// Token: 0x04001BD9 RID: 7129
	public float deployLength = 0.75f;

	// Token: 0x04001BDA RID: 7130
	public float damageMin = 5f;

	// Token: 0x04001BDB RID: 7131
	public float damageMax = 5f;

	// Token: 0x04001BDC RID: 7132
	public static AudioClip _hitNotify;

	// Token: 0x04001BDD RID: 7133
	private static global::HUDHitIndicator _hitIndicator;

	// Token: 0x0200065D RID: 1629
	private sealed class ITEM_TYPE : global::WeaponItem<global::WeaponDataBlock>, global::IHeldItem, global::IInventoryItem, global::IWeaponItem
	{
		// Token: 0x060037B6 RID: 14262 RVA: 0x000C6B34 File Offset: 0x000C4D34
		public ITEM_TYPE(global::WeaponDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x060037B7 RID: 14263 RVA: 0x000C6B40 File Offset: 0x000C4D40
		global::ItemDataBlock global::IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x060037B8 RID: 14264 RVA: 0x000C6B48 File Offset: 0x000C4D48
		bool ValidatePrimaryMessageTime(double timestamp)
		{
			return base.ValidatePrimaryMessageTime(timestamp);
		}

		// Token: 0x060037B9 RID: 14265 RVA: 0x000C6B54 File Offset: 0x000C4D54
		bool get_canAim()
		{
			return base.canAim;
		}

		// Token: 0x060037BA RID: 14266 RVA: 0x000C6B5C File Offset: 0x000C4D5C
		float get_nextPrimaryAttackTime()
		{
			return base.nextPrimaryAttackTime;
		}

		// Token: 0x060037BB RID: 14267 RVA: 0x000C6B64 File Offset: 0x000C4D64
		void set_nextPrimaryAttackTime(float value)
		{
			base.nextPrimaryAttackTime = value;
		}

		// Token: 0x060037BC RID: 14268 RVA: 0x000C6B70 File Offset: 0x000C4D70
		float get_nextSecondaryAttackTime()
		{
			return base.nextSecondaryAttackTime;
		}

		// Token: 0x060037BD RID: 14269 RVA: 0x000C6B78 File Offset: 0x000C4D78
		void set_nextSecondaryAttackTime(float value)
		{
			base.nextSecondaryAttackTime = value;
		}

		// Token: 0x060037BE RID: 14270 RVA: 0x000C6B84 File Offset: 0x000C4D84
		float get_deployFinishedTime()
		{
			return base.deployFinishedTime;
		}

		// Token: 0x060037BF RID: 14271 RVA: 0x000C6B8C File Offset: 0x000C4D8C
		void set_deployFinishedTime(float value)
		{
			base.deployFinishedTime = value;
		}

		// Token: 0x060037C0 RID: 14272 RVA: 0x000C6B98 File Offset: 0x000C4D98
		void SetTotalModSlotCount(int count)
		{
			base.SetTotalModSlotCount(count);
		}

		// Token: 0x060037C1 RID: 14273 RVA: 0x000C6BA4 File Offset: 0x000C4DA4
		void SetUsedModSlotCount(int count)
		{
			base.SetUsedModSlotCount(count);
		}

		// Token: 0x060037C2 RID: 14274 RVA: 0x000C6BB0 File Offset: 0x000C4DB0
		void AddMod(global::ItemModDataBlock mod)
		{
			base.AddMod(mod);
		}

		// Token: 0x060037C3 RID: 14275 RVA: 0x000C6BBC File Offset: 0x000C4DBC
		int FindMod(global::ItemModDataBlock mod)
		{
			return base.FindMod(mod);
		}

		// Token: 0x060037C4 RID: 14276 RVA: 0x000C6BC8 File Offset: 0x000C4DC8
		void OnActivate()
		{
			base.OnActivate();
		}

		// Token: 0x060037C5 RID: 14277 RVA: 0x000C6BD0 File Offset: 0x000C4DD0
		void OnDeactivate()
		{
			base.OnDeactivate();
		}

		// Token: 0x060037C6 RID: 14278 RVA: 0x000C6BD8 File Offset: 0x000C4DD8
		global::ViewModel get_viewModelInstance()
		{
			return base.viewModelInstance;
		}

		// Token: 0x060037C7 RID: 14279 RVA: 0x000C6BE0 File Offset: 0x000C4DE0
		global::ItemRepresentation get_itemRepresentation()
		{
			return base.itemRepresentation;
		}

		// Token: 0x060037C8 RID: 14280 RVA: 0x000C6BE8 File Offset: 0x000C4DE8
		void set_itemRepresentation(global::ItemRepresentation value)
		{
			base.itemRepresentation = value;
		}

		// Token: 0x060037C9 RID: 14281 RVA: 0x000C6BF4 File Offset: 0x000C4DF4
		bool get_canActivate()
		{
			return base.canActivate;
		}

		// Token: 0x060037CA RID: 14282 RVA: 0x000C6BFC File Offset: 0x000C4DFC
		bool get_canDeactivate()
		{
			return base.canDeactivate;
		}

		// Token: 0x060037CB RID: 14283 RVA: 0x000C6C04 File Offset: 0x000C4E04
		global::ItemModFlags get_modFlags()
		{
			return base.modFlags;
		}

		// Token: 0x060037CC RID: 14284 RVA: 0x000C6C0C File Offset: 0x000C4E0C
		global::ItemModDataBlock[] get_itemMods()
		{
			return base.itemMods;
		}

		// Token: 0x060037CD RID: 14285 RVA: 0x000C6C14 File Offset: 0x000C4E14
		int get_totalModSlots()
		{
			return base.totalModSlots;
		}

		// Token: 0x060037CE RID: 14286 RVA: 0x000C6C1C File Offset: 0x000C4E1C
		int get_usedModSlots()
		{
			return base.usedModSlots;
		}

		// Token: 0x060037CF RID: 14287 RVA: 0x000C6C24 File Offset: 0x000C4E24
		int get_freeModSlots()
		{
			return base.freeModSlots;
		}

		// Token: 0x060037D0 RID: 14288 RVA: 0x000C6C2C File Offset: 0x000C4E2C
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x060037D1 RID: 14289 RVA: 0x000C6C34 File Offset: 0x000C4E34
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x060037D2 RID: 14290 RVA: 0x000C6C3C File Offset: 0x000C4E3C
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x060037D3 RID: 14291 RVA: 0x000C6C44 File Offset: 0x000C4E44
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x060037D4 RID: 14292 RVA: 0x000C6C50 File Offset: 0x000C4E50
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x060037D5 RID: 14293 RVA: 0x000C6C5C File Offset: 0x000C4E5C
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x060037D6 RID: 14294 RVA: 0x000C6C68 File Offset: 0x000C4E68
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x060037D7 RID: 14295 RVA: 0x000C6C74 File Offset: 0x000C4E74
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x060037D8 RID: 14296 RVA: 0x000C6C80 File Offset: 0x000C4E80
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x060037D9 RID: 14297 RVA: 0x000C6C8C File Offset: 0x000C4E8C
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x060037DA RID: 14298 RVA: 0x000C6C98 File Offset: 0x000C4E98
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x060037DB RID: 14299 RVA: 0x000C6CA4 File Offset: 0x000C4EA4
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x060037DC RID: 14300 RVA: 0x000C6CAC File Offset: 0x000C4EAC
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x060037DD RID: 14301 RVA: 0x000C6CB4 File Offset: 0x000C4EB4
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x060037DE RID: 14302 RVA: 0x000C6CBC File Offset: 0x000C4EBC
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x060037DF RID: 14303 RVA: 0x000C6CC4 File Offset: 0x000C4EC4
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x060037E0 RID: 14304 RVA: 0x000C6CCC File Offset: 0x000C4ECC
		global::Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x060037E1 RID: 14305 RVA: 0x000C6CD4 File Offset: 0x000C4ED4
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x060037E2 RID: 14306 RVA: 0x000C6CDC File Offset: 0x000C4EDC
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x060037E3 RID: 14307 RVA: 0x000C6CE4 File Offset: 0x000C4EE4
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x060037E4 RID: 14308 RVA: 0x000C6CF0 File Offset: 0x000C4EF0
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x060037E5 RID: 14309 RVA: 0x000C6CF8 File Offset: 0x000C4EF8
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x060037E6 RID: 14310 RVA: 0x000C6D00 File Offset: 0x000C4F00
		global::Character get_character()
		{
			return base.character;
		}

		// Token: 0x060037E7 RID: 14311 RVA: 0x000C6D08 File Offset: 0x000C4F08
		global::Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x060037E8 RID: 14312 RVA: 0x000C6D10 File Offset: 0x000C4F10
		global::Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x060037E9 RID: 14313 RVA: 0x000C6D18 File Offset: 0x000C4F18
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x060037EA RID: 14314 RVA: 0x000C6D20 File Offset: 0x000C4F20
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
