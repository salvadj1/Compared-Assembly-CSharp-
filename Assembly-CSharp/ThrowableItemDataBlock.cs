using System;
using uLink;
using UnityEngine;

// Token: 0x02000652 RID: 1618
public class ThrowableItemDataBlock : global::WeaponDataBlock
{
	// Token: 0x060036AF RID: 13999 RVA: 0x000C5A80 File Offset: 0x000C3C80
	protected override global::IInventoryItem ConstructItem()
	{
		return new global::ThrowableItemDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x060036B0 RID: 14000 RVA: 0x000C5A88 File Offset: 0x000C3C88
	public override void InstallData(global::IInventoryItem item)
	{
		base.InstallData(item);
	}

	// Token: 0x060036B1 RID: 14001 RVA: 0x000C5A94 File Offset: 0x000C3C94
	public global::IThrowableItem GetThrowableInstance(global::IInventoryItem itemInstance)
	{
		return itemInstance as global::IThrowableItem;
	}

	// Token: 0x060036B2 RID: 14002 RVA: 0x000C5A9C File Offset: 0x000C3C9C
	public virtual void PrimaryAttack(global::ViewModel vm, global::ItemRepresentation itemRep, global::IThrowableItem itemInstance, ref global::HumanController.InputSample sample)
	{
		this.GetThrowableInstance(itemInstance).BeginHoldingBack();
	}

	// Token: 0x060036B3 RID: 14003 RVA: 0x000C5AAC File Offset: 0x000C3CAC
	public virtual void SecondaryAttack(global::ViewModel vm, global::ItemRepresentation itemRep, global::IThrowableItem itemInstance, ref global::HumanController.InputSample sample)
	{
	}

	// Token: 0x060036B4 RID: 14004 RVA: 0x000C5AB0 File Offset: 0x000C3CB0
	public virtual void AttackReleased(global::ViewModel vm, global::ItemRepresentation itemRep, global::IThrowableItem itemInstance, ref global::HumanController.InputSample sample)
	{
		Debug.Log("Throwable attack released");
	}

	// Token: 0x060036B5 RID: 14005 RVA: 0x000C5ABC File Offset: 0x000C3CBC
	protected virtual GameObject SpawnThrowItem(uLink.NetworkViewID owningViewID, GameObject prefab, Vector3 position, Quaternion rotation, Vector3 velocity)
	{
		return null;
	}

	// Token: 0x060036B6 RID: 14006 RVA: 0x000C5AC0 File Offset: 0x000C3CC0
	protected virtual GameObject ThrowItem(global::ItemRepresentation rep, global::IThrowableItem item, Vector3 origin, Vector3 forward, uLink.NetworkViewID owner)
	{
		Vector3 velocity = forward * item.heldThrowStrength;
		Vector3 position = origin + forward * 1f;
		Quaternion rotation = Quaternion.LookRotation(Vector3.up);
		return this.SpawnThrowItem(owner, this.throwObjectPrefab, position, rotation, velocity);
	}

	// Token: 0x060036B7 RID: 14007 RVA: 0x000C5B0C File Offset: 0x000C3D0C
	public virtual void DoActualThrow(global::ItemRepresentation itemRep, global::IThrowableItem itemInstance, global::ViewModel vm)
	{
		global::Character component = global::PlayerClient.GetLocalPlayer().controllable.GetComponent<global::Character>();
		Vector3 eyesOrigin = component.eyesOrigin;
		Vector3 forward = component.eyesAngles.forward;
		if (vm)
		{
			vm.PlayQueued("deploy");
		}
		int num = 1;
		if (itemInstance.Consume(ref num))
		{
			itemInstance.inventory.RemoveItem(itemInstance.slot);
		}
		BitStream bitStream = new BitStream(false);
		bitStream.WriteVector3(eyesOrigin);
		bitStream.WriteVector3(forward);
		itemRep.Action<BitStream>(1, 0, bitStream);
	}

	// Token: 0x060036B8 RID: 14008 RVA: 0x000C5B9C File Offset: 0x000C3D9C
	protected override void SecureWriteMemberValues(BitStream stream)
	{
		base.SecureWriteMemberValues(stream);
		stream.Write<float>(this.throwStrengthMin, new object[0]);
		stream.Write<float>(this.throwStrengthPerSec, new object[0]);
		stream.Write<float>(this.throwStrengthMax, new object[0]);
	}

	// Token: 0x04001BCF RID: 7119
	public GameObject throwObjectPrefab;

	// Token: 0x04001BD0 RID: 7120
	public float throwStrengthMin = 10f;

	// Token: 0x04001BD1 RID: 7121
	public float throwStrengthPerSec = 10f;

	// Token: 0x04001BD2 RID: 7122
	public float throwStrengthMax = 10f;

	// Token: 0x02000653 RID: 1619
	private sealed class ITEM_TYPE : global::ThrowableItem<global::ThrowableItemDataBlock>, global::IHeldItem, global::IInventoryItem, global::IThrowableItem, global::IWeaponItem
	{
		// Token: 0x060036B9 RID: 14009 RVA: 0x000C5BE8 File Offset: 0x000C3DE8
		public ITEM_TYPE(global::ThrowableItemDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x060036BA RID: 14010 RVA: 0x000C5BF4 File Offset: 0x000C3DF4
		global::ItemDataBlock global::IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x060036BB RID: 14011 RVA: 0x000C5BFC File Offset: 0x000C3DFC
		float get_holdingStartTime()
		{
			return base.holdingStartTime;
		}

		// Token: 0x060036BC RID: 14012 RVA: 0x000C5C04 File Offset: 0x000C3E04
		void set_holdingStartTime(float value)
		{
			base.holdingStartTime = value;
		}

		// Token: 0x060036BD RID: 14013 RVA: 0x000C5C10 File Offset: 0x000C3E10
		bool get_holdingBack()
		{
			return base.holdingBack;
		}

		// Token: 0x060036BE RID: 14014 RVA: 0x000C5C18 File Offset: 0x000C3E18
		void set_holdingBack(bool value)
		{
			base.holdingBack = value;
		}

		// Token: 0x060036BF RID: 14015 RVA: 0x000C5C24 File Offset: 0x000C3E24
		float get_minReleaseTime()
		{
			return base.minReleaseTime;
		}

		// Token: 0x060036C0 RID: 14016 RVA: 0x000C5C2C File Offset: 0x000C3E2C
		void set_minReleaseTime(float value)
		{
			base.minReleaseTime = value;
		}

		// Token: 0x060036C1 RID: 14017 RVA: 0x000C5C38 File Offset: 0x000C3E38
		bool ValidatePrimaryMessageTime(double timestamp)
		{
			return base.ValidatePrimaryMessageTime(timestamp);
		}

		// Token: 0x060036C2 RID: 14018 RVA: 0x000C5C44 File Offset: 0x000C3E44
		bool get_canAim()
		{
			return base.canAim;
		}

		// Token: 0x060036C3 RID: 14019 RVA: 0x000C5C4C File Offset: 0x000C3E4C
		float get_nextPrimaryAttackTime()
		{
			return base.nextPrimaryAttackTime;
		}

		// Token: 0x060036C4 RID: 14020 RVA: 0x000C5C54 File Offset: 0x000C3E54
		void set_nextPrimaryAttackTime(float value)
		{
			base.nextPrimaryAttackTime = value;
		}

		// Token: 0x060036C5 RID: 14021 RVA: 0x000C5C60 File Offset: 0x000C3E60
		float get_nextSecondaryAttackTime()
		{
			return base.nextSecondaryAttackTime;
		}

		// Token: 0x060036C6 RID: 14022 RVA: 0x000C5C68 File Offset: 0x000C3E68
		void set_nextSecondaryAttackTime(float value)
		{
			base.nextSecondaryAttackTime = value;
		}

		// Token: 0x060036C7 RID: 14023 RVA: 0x000C5C74 File Offset: 0x000C3E74
		float get_deployFinishedTime()
		{
			return base.deployFinishedTime;
		}

		// Token: 0x060036C8 RID: 14024 RVA: 0x000C5C7C File Offset: 0x000C3E7C
		void set_deployFinishedTime(float value)
		{
			base.deployFinishedTime = value;
		}

		// Token: 0x060036C9 RID: 14025 RVA: 0x000C5C88 File Offset: 0x000C3E88
		void SetTotalModSlotCount(int count)
		{
			base.SetTotalModSlotCount(count);
		}

		// Token: 0x060036CA RID: 14026 RVA: 0x000C5C94 File Offset: 0x000C3E94
		void SetUsedModSlotCount(int count)
		{
			base.SetUsedModSlotCount(count);
		}

		// Token: 0x060036CB RID: 14027 RVA: 0x000C5CA0 File Offset: 0x000C3EA0
		void AddMod(global::ItemModDataBlock mod)
		{
			base.AddMod(mod);
		}

		// Token: 0x060036CC RID: 14028 RVA: 0x000C5CAC File Offset: 0x000C3EAC
		int FindMod(global::ItemModDataBlock mod)
		{
			return base.FindMod(mod);
		}

		// Token: 0x060036CD RID: 14029 RVA: 0x000C5CB8 File Offset: 0x000C3EB8
		void OnActivate()
		{
			base.OnActivate();
		}

		// Token: 0x060036CE RID: 14030 RVA: 0x000C5CC0 File Offset: 0x000C3EC0
		void OnDeactivate()
		{
			base.OnDeactivate();
		}

		// Token: 0x060036CF RID: 14031 RVA: 0x000C5CC8 File Offset: 0x000C3EC8
		global::ViewModel get_viewModelInstance()
		{
			return base.viewModelInstance;
		}

		// Token: 0x060036D0 RID: 14032 RVA: 0x000C5CD0 File Offset: 0x000C3ED0
		global::ItemRepresentation get_itemRepresentation()
		{
			return base.itemRepresentation;
		}

		// Token: 0x060036D1 RID: 14033 RVA: 0x000C5CD8 File Offset: 0x000C3ED8
		void set_itemRepresentation(global::ItemRepresentation value)
		{
			base.itemRepresentation = value;
		}

		// Token: 0x060036D2 RID: 14034 RVA: 0x000C5CE4 File Offset: 0x000C3EE4
		bool get_canActivate()
		{
			return base.canActivate;
		}

		// Token: 0x060036D3 RID: 14035 RVA: 0x000C5CEC File Offset: 0x000C3EEC
		bool get_canDeactivate()
		{
			return base.canDeactivate;
		}

		// Token: 0x060036D4 RID: 14036 RVA: 0x000C5CF4 File Offset: 0x000C3EF4
		global::ItemModFlags get_modFlags()
		{
			return base.modFlags;
		}

		// Token: 0x060036D5 RID: 14037 RVA: 0x000C5CFC File Offset: 0x000C3EFC
		global::ItemModDataBlock[] get_itemMods()
		{
			return base.itemMods;
		}

		// Token: 0x060036D6 RID: 14038 RVA: 0x000C5D04 File Offset: 0x000C3F04
		int get_totalModSlots()
		{
			return base.totalModSlots;
		}

		// Token: 0x060036D7 RID: 14039 RVA: 0x000C5D0C File Offset: 0x000C3F0C
		int get_usedModSlots()
		{
			return base.usedModSlots;
		}

		// Token: 0x060036D8 RID: 14040 RVA: 0x000C5D14 File Offset: 0x000C3F14
		int get_freeModSlots()
		{
			return base.freeModSlots;
		}

		// Token: 0x060036D9 RID: 14041 RVA: 0x000C5D1C File Offset: 0x000C3F1C
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x060036DA RID: 14042 RVA: 0x000C5D24 File Offset: 0x000C3F24
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x060036DB RID: 14043 RVA: 0x000C5D2C File Offset: 0x000C3F2C
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x060036DC RID: 14044 RVA: 0x000C5D34 File Offset: 0x000C3F34
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x060036DD RID: 14045 RVA: 0x000C5D40 File Offset: 0x000C3F40
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x060036DE RID: 14046 RVA: 0x000C5D4C File Offset: 0x000C3F4C
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x060036DF RID: 14047 RVA: 0x000C5D58 File Offset: 0x000C3F58
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x060036E0 RID: 14048 RVA: 0x000C5D64 File Offset: 0x000C3F64
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x060036E1 RID: 14049 RVA: 0x000C5D70 File Offset: 0x000C3F70
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x060036E2 RID: 14050 RVA: 0x000C5D7C File Offset: 0x000C3F7C
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x060036E3 RID: 14051 RVA: 0x000C5D88 File Offset: 0x000C3F88
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x060036E4 RID: 14052 RVA: 0x000C5D94 File Offset: 0x000C3F94
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x060036E5 RID: 14053 RVA: 0x000C5D9C File Offset: 0x000C3F9C
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x060036E6 RID: 14054 RVA: 0x000C5DA4 File Offset: 0x000C3FA4
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x060036E7 RID: 14055 RVA: 0x000C5DAC File Offset: 0x000C3FAC
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x060036E8 RID: 14056 RVA: 0x000C5DB4 File Offset: 0x000C3FB4
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x060036E9 RID: 14057 RVA: 0x000C5DBC File Offset: 0x000C3FBC
		global::Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x060036EA RID: 14058 RVA: 0x000C5DC4 File Offset: 0x000C3FC4
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x060036EB RID: 14059 RVA: 0x000C5DCC File Offset: 0x000C3FCC
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x060036EC RID: 14060 RVA: 0x000C5DD4 File Offset: 0x000C3FD4
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x060036ED RID: 14061 RVA: 0x000C5DE0 File Offset: 0x000C3FE0
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x060036EE RID: 14062 RVA: 0x000C5DE8 File Offset: 0x000C3FE8
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x060036EF RID: 14063 RVA: 0x000C5DF0 File Offset: 0x000C3FF0
		global::Character get_character()
		{
			return base.character;
		}

		// Token: 0x060036F0 RID: 14064 RVA: 0x000C5DF8 File Offset: 0x000C3FF8
		global::Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x060036F1 RID: 14065 RVA: 0x000C5E00 File Offset: 0x000C4000
		global::Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x060036F2 RID: 14066 RVA: 0x000C5E08 File Offset: 0x000C4008
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x060036F3 RID: 14067 RVA: 0x000C5E10 File Offset: 0x000C4010
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
