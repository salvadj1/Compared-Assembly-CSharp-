using System;
using uLink;
using UnityEngine;

// Token: 0x02000594 RID: 1428
public class ThrowableItemDataBlock : WeaponDataBlock
{
	// Token: 0x060032E7 RID: 13031 RVA: 0x000BD824 File Offset: 0x000BBA24
	protected override IInventoryItem ConstructItem()
	{
		return new ThrowableItemDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x060032E8 RID: 13032 RVA: 0x000BD82C File Offset: 0x000BBA2C
	public override void InstallData(IInventoryItem item)
	{
		base.InstallData(item);
	}

	// Token: 0x060032E9 RID: 13033 RVA: 0x000BD838 File Offset: 0x000BBA38
	public IThrowableItem GetThrowableInstance(IInventoryItem itemInstance)
	{
		return itemInstance as IThrowableItem;
	}

	// Token: 0x060032EA RID: 13034 RVA: 0x000BD840 File Offset: 0x000BBA40
	public virtual void PrimaryAttack(ViewModel vm, ItemRepresentation itemRep, IThrowableItem itemInstance, ref HumanController.InputSample sample)
	{
		this.GetThrowableInstance(itemInstance).BeginHoldingBack();
	}

	// Token: 0x060032EB RID: 13035 RVA: 0x000BD850 File Offset: 0x000BBA50
	public virtual void SecondaryAttack(ViewModel vm, ItemRepresentation itemRep, IThrowableItem itemInstance, ref HumanController.InputSample sample)
	{
	}

	// Token: 0x060032EC RID: 13036 RVA: 0x000BD854 File Offset: 0x000BBA54
	public virtual void AttackReleased(ViewModel vm, ItemRepresentation itemRep, IThrowableItem itemInstance, ref HumanController.InputSample sample)
	{
		Debug.Log("Throwable attack released");
	}

	// Token: 0x060032ED RID: 13037 RVA: 0x000BD860 File Offset: 0x000BBA60
	protected virtual GameObject SpawnThrowItem(NetworkViewID owningViewID, GameObject prefab, Vector3 position, Quaternion rotation, Vector3 velocity)
	{
		return null;
	}

	// Token: 0x060032EE RID: 13038 RVA: 0x000BD864 File Offset: 0x000BBA64
	protected virtual GameObject ThrowItem(ItemRepresentation rep, IThrowableItem item, Vector3 origin, Vector3 forward, NetworkViewID owner)
	{
		Vector3 velocity = forward * item.heldThrowStrength;
		Vector3 position = origin + forward * 1f;
		Quaternion rotation = Quaternion.LookRotation(Vector3.up);
		return this.SpawnThrowItem(owner, this.throwObjectPrefab, position, rotation, velocity);
	}

	// Token: 0x060032EF RID: 13039 RVA: 0x000BD8B0 File Offset: 0x000BBAB0
	public virtual void DoActualThrow(ItemRepresentation itemRep, IThrowableItem itemInstance, ViewModel vm)
	{
		Character component = PlayerClient.GetLocalPlayer().controllable.GetComponent<Character>();
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

	// Token: 0x060032F0 RID: 13040 RVA: 0x000BD940 File Offset: 0x000BBB40
	protected override void SecureWriteMemberValues(BitStream stream)
	{
		base.SecureWriteMemberValues(stream);
		stream.Write<float>(this.throwStrengthMin, new object[0]);
		stream.Write<float>(this.throwStrengthPerSec, new object[0]);
		stream.Write<float>(this.throwStrengthMax, new object[0]);
	}

	// Token: 0x040019FE RID: 6654
	public GameObject throwObjectPrefab;

	// Token: 0x040019FF RID: 6655
	public float throwStrengthMin = 10f;

	// Token: 0x04001A00 RID: 6656
	public float throwStrengthPerSec = 10f;

	// Token: 0x04001A01 RID: 6657
	public float throwStrengthMax = 10f;

	// Token: 0x02000595 RID: 1429
	private sealed class ITEM_TYPE : ThrowableItem<ThrowableItemDataBlock>, IHeldItem, IInventoryItem, IThrowableItem, IWeaponItem
	{
		// Token: 0x060032F1 RID: 13041 RVA: 0x000BD98C File Offset: 0x000BBB8C
		public ITEM_TYPE(ThrowableItemDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x060032F2 RID: 13042 RVA: 0x000BD998 File Offset: 0x000BBB98
		ItemDataBlock IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x060032F3 RID: 13043 RVA: 0x000BD9A0 File Offset: 0x000BBBA0
		float get_holdingStartTime()
		{
			return base.holdingStartTime;
		}

		// Token: 0x060032F4 RID: 13044 RVA: 0x000BD9A8 File Offset: 0x000BBBA8
		void set_holdingStartTime(float value)
		{
			base.holdingStartTime = value;
		}

		// Token: 0x060032F5 RID: 13045 RVA: 0x000BD9B4 File Offset: 0x000BBBB4
		bool get_holdingBack()
		{
			return base.holdingBack;
		}

		// Token: 0x060032F6 RID: 13046 RVA: 0x000BD9BC File Offset: 0x000BBBBC
		void set_holdingBack(bool value)
		{
			base.holdingBack = value;
		}

		// Token: 0x060032F7 RID: 13047 RVA: 0x000BD9C8 File Offset: 0x000BBBC8
		float get_minReleaseTime()
		{
			return base.minReleaseTime;
		}

		// Token: 0x060032F8 RID: 13048 RVA: 0x000BD9D0 File Offset: 0x000BBBD0
		void set_minReleaseTime(float value)
		{
			base.minReleaseTime = value;
		}

		// Token: 0x060032F9 RID: 13049 RVA: 0x000BD9DC File Offset: 0x000BBBDC
		bool ValidatePrimaryMessageTime(double timestamp)
		{
			return base.ValidatePrimaryMessageTime(timestamp);
		}

		// Token: 0x060032FA RID: 13050 RVA: 0x000BD9E8 File Offset: 0x000BBBE8
		bool get_canAim()
		{
			return base.canAim;
		}

		// Token: 0x060032FB RID: 13051 RVA: 0x000BD9F0 File Offset: 0x000BBBF0
		float get_nextPrimaryAttackTime()
		{
			return base.nextPrimaryAttackTime;
		}

		// Token: 0x060032FC RID: 13052 RVA: 0x000BD9F8 File Offset: 0x000BBBF8
		void set_nextPrimaryAttackTime(float value)
		{
			base.nextPrimaryAttackTime = value;
		}

		// Token: 0x060032FD RID: 13053 RVA: 0x000BDA04 File Offset: 0x000BBC04
		float get_nextSecondaryAttackTime()
		{
			return base.nextSecondaryAttackTime;
		}

		// Token: 0x060032FE RID: 13054 RVA: 0x000BDA0C File Offset: 0x000BBC0C
		void set_nextSecondaryAttackTime(float value)
		{
			base.nextSecondaryAttackTime = value;
		}

		// Token: 0x060032FF RID: 13055 RVA: 0x000BDA18 File Offset: 0x000BBC18
		float get_deployFinishedTime()
		{
			return base.deployFinishedTime;
		}

		// Token: 0x06003300 RID: 13056 RVA: 0x000BDA20 File Offset: 0x000BBC20
		void set_deployFinishedTime(float value)
		{
			base.deployFinishedTime = value;
		}

		// Token: 0x06003301 RID: 13057 RVA: 0x000BDA2C File Offset: 0x000BBC2C
		void SetTotalModSlotCount(int count)
		{
			base.SetTotalModSlotCount(count);
		}

		// Token: 0x06003302 RID: 13058 RVA: 0x000BDA38 File Offset: 0x000BBC38
		void SetUsedModSlotCount(int count)
		{
			base.SetUsedModSlotCount(count);
		}

		// Token: 0x06003303 RID: 13059 RVA: 0x000BDA44 File Offset: 0x000BBC44
		void AddMod(ItemModDataBlock mod)
		{
			base.AddMod(mod);
		}

		// Token: 0x06003304 RID: 13060 RVA: 0x000BDA50 File Offset: 0x000BBC50
		int FindMod(ItemModDataBlock mod)
		{
			return base.FindMod(mod);
		}

		// Token: 0x06003305 RID: 13061 RVA: 0x000BDA5C File Offset: 0x000BBC5C
		void OnActivate()
		{
			base.OnActivate();
		}

		// Token: 0x06003306 RID: 13062 RVA: 0x000BDA64 File Offset: 0x000BBC64
		void OnDeactivate()
		{
			base.OnDeactivate();
		}

		// Token: 0x06003307 RID: 13063 RVA: 0x000BDA6C File Offset: 0x000BBC6C
		ViewModel get_viewModelInstance()
		{
			return base.viewModelInstance;
		}

		// Token: 0x06003308 RID: 13064 RVA: 0x000BDA74 File Offset: 0x000BBC74
		ItemRepresentation get_itemRepresentation()
		{
			return base.itemRepresentation;
		}

		// Token: 0x06003309 RID: 13065 RVA: 0x000BDA7C File Offset: 0x000BBC7C
		void set_itemRepresentation(ItemRepresentation value)
		{
			base.itemRepresentation = value;
		}

		// Token: 0x0600330A RID: 13066 RVA: 0x000BDA88 File Offset: 0x000BBC88
		bool get_canActivate()
		{
			return base.canActivate;
		}

		// Token: 0x0600330B RID: 13067 RVA: 0x000BDA90 File Offset: 0x000BBC90
		bool get_canDeactivate()
		{
			return base.canDeactivate;
		}

		// Token: 0x0600330C RID: 13068 RVA: 0x000BDA98 File Offset: 0x000BBC98
		ItemModFlags get_modFlags()
		{
			return base.modFlags;
		}

		// Token: 0x0600330D RID: 13069 RVA: 0x000BDAA0 File Offset: 0x000BBCA0
		ItemModDataBlock[] get_itemMods()
		{
			return base.itemMods;
		}

		// Token: 0x0600330E RID: 13070 RVA: 0x000BDAA8 File Offset: 0x000BBCA8
		int get_totalModSlots()
		{
			return base.totalModSlots;
		}

		// Token: 0x0600330F RID: 13071 RVA: 0x000BDAB0 File Offset: 0x000BBCB0
		int get_usedModSlots()
		{
			return base.usedModSlots;
		}

		// Token: 0x06003310 RID: 13072 RVA: 0x000BDAB8 File Offset: 0x000BBCB8
		int get_freeModSlots()
		{
			return base.freeModSlots;
		}

		// Token: 0x06003311 RID: 13073 RVA: 0x000BDAC0 File Offset: 0x000BBCC0
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x06003312 RID: 13074 RVA: 0x000BDAC8 File Offset: 0x000BBCC8
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x06003313 RID: 13075 RVA: 0x000BDAD0 File Offset: 0x000BBCD0
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x06003314 RID: 13076 RVA: 0x000BDAD8 File Offset: 0x000BBCD8
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x06003315 RID: 13077 RVA: 0x000BDAE4 File Offset: 0x000BBCE4
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x06003316 RID: 13078 RVA: 0x000BDAF0 File Offset: 0x000BBCF0
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x06003317 RID: 13079 RVA: 0x000BDAFC File Offset: 0x000BBCFC
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x06003318 RID: 13080 RVA: 0x000BDB08 File Offset: 0x000BBD08
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x06003319 RID: 13081 RVA: 0x000BDB14 File Offset: 0x000BBD14
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x0600331A RID: 13082 RVA: 0x000BDB20 File Offset: 0x000BBD20
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x0600331B RID: 13083 RVA: 0x000BDB2C File Offset: 0x000BBD2C
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x0600331C RID: 13084 RVA: 0x000BDB38 File Offset: 0x000BBD38
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x0600331D RID: 13085 RVA: 0x000BDB40 File Offset: 0x000BBD40
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x0600331E RID: 13086 RVA: 0x000BDB48 File Offset: 0x000BBD48
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x0600331F RID: 13087 RVA: 0x000BDB50 File Offset: 0x000BBD50
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x06003320 RID: 13088 RVA: 0x000BDB58 File Offset: 0x000BBD58
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x06003321 RID: 13089 RVA: 0x000BDB60 File Offset: 0x000BBD60
		Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x06003322 RID: 13090 RVA: 0x000BDB68 File Offset: 0x000BBD68
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x06003323 RID: 13091 RVA: 0x000BDB70 File Offset: 0x000BBD70
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x06003324 RID: 13092 RVA: 0x000BDB78 File Offset: 0x000BBD78
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x06003325 RID: 13093 RVA: 0x000BDB84 File Offset: 0x000BBD84
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x06003326 RID: 13094 RVA: 0x000BDB8C File Offset: 0x000BBD8C
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x06003327 RID: 13095 RVA: 0x000BDB94 File Offset: 0x000BBD94
		Character get_character()
		{
			return base.character;
		}

		// Token: 0x06003328 RID: 13096 RVA: 0x000BDB9C File Offset: 0x000BBD9C
		Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x06003329 RID: 13097 RVA: 0x000BDBA4 File Offset: 0x000BBDA4
		Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x0600332A RID: 13098 RVA: 0x000BDBAC File Offset: 0x000BBDAC
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x0600332B RID: 13099 RVA: 0x000BDBB4 File Offset: 0x000BBDB4
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
