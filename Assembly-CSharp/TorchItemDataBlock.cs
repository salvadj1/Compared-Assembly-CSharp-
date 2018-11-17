using System;
using uLink;
using UnityEngine;

// Token: 0x0200059C RID: 1436
public class TorchItemDataBlock : ThrowableItemDataBlock
{
	// Token: 0x06003396 RID: 13206 RVA: 0x000BE1E0 File Offset: 0x000BC3E0
	protected override IInventoryItem ConstructItem()
	{
		return new TorchItemDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x06003397 RID: 13207 RVA: 0x000BE1E8 File Offset: 0x000BC3E8
	public ITorchItem GetTorchInstance(IThrowableItem itemInstance)
	{
		return itemInstance as ITorchItem;
	}

	// Token: 0x06003398 RID: 13208 RVA: 0x000BE1F0 File Offset: 0x000BC3F0
	public TorchItemRep GetTorchRep(ItemRepresentation rep)
	{
		return rep as TorchItemRep;
	}

	// Token: 0x06003399 RID: 13209 RVA: 0x000BE1F8 File Offset: 0x000BC3F8
	public override void PrimaryAttack(ViewModel vm, ItemRepresentation itemRep, IThrowableItem itemInstance, ref HumanController.InputSample sample)
	{
		ITorchItem torchInstance = this.GetTorchInstance(itemInstance);
		if (torchInstance.isLit)
		{
			return;
		}
		if (vm)
		{
			vm.Play("ignite");
		}
		torchInstance.realIgniteTime = Time.time + 0.8f;
		torchInstance.nextPrimaryAttackTime = Time.time + 1.5f;
		torchInstance.nextSecondaryAttackTime = Time.time + 1.5f;
	}

	// Token: 0x0600339A RID: 13210 RVA: 0x000BE264 File Offset: 0x000BC464
	public override void DoActualThrow(ItemRepresentation itemRep, IThrowableItem itemInstance, ViewModel vm)
	{
		Character component = PlayerClient.GetLocalPlayer().controllable.GetComponent<Character>();
		Vector3 eyesOrigin = component.eyesOrigin;
		Vector3 forward = component.eyesAngles.forward;
		if (vm)
		{
			vm.PlayQueued("deploy");
		}
		this.GetTorchInstance(itemInstance).Extinguish();
		int num = 1;
		if (itemInstance.Consume(ref num))
		{
			itemInstance.inventory.RemoveItem(itemInstance.slot);
		}
		BitStream bitStream = new BitStream(false);
		bitStream.WriteVector3(eyesOrigin);
		bitStream.WriteVector3(forward);
		itemRep.ActionStream(1, 0, bitStream);
	}

	// Token: 0x0600339B RID: 13211 RVA: 0x000BE300 File Offset: 0x000BC500
	public void DoActualIgnite(ItemRepresentation itemRep, IThrowableItem itemInstance, ViewModel vm)
	{
		this.Ignite(vm, itemRep, this.GetTorchInstance(itemInstance));
		itemRep.Action(2, 0);
	}

	// Token: 0x0600339C RID: 13212 RVA: 0x000BE31C File Offset: 0x000BC51C
	public override void SecondaryAttack(ViewModel vm, ItemRepresentation itemRep, IThrowableItem itemInstance, ref HumanController.InputSample sample)
	{
		ITorchItem torchInstance = this.GetTorchInstance(itemInstance);
		if (!torchInstance.isLit)
		{
			this.PrimaryAttack(vm, itemRep, itemInstance, ref sample);
			torchInstance.forceSecondaryTime = Time.time + 1.51f;
			return;
		}
		if (vm)
		{
			vm.Play("throw");
		}
		torchInstance.realThrowTime = Time.time + 0.5f;
		torchInstance.nextPrimaryAttackTime = Time.time + 1.5f;
		torchInstance.nextSecondaryAttackTime = Time.time + 1.5f;
	}

	// Token: 0x0600339D RID: 13213 RVA: 0x000BE3A4 File Offset: 0x000BC5A4
	public override void DoAction2(BitStream stream, ItemRepresentation itemRep, ref NetworkMessageInfo info)
	{
		this.Ignite(null, itemRep, null);
	}

	// Token: 0x0600339E RID: 13214 RVA: 0x000BE3B0 File Offset: 0x000BC5B0
	public override void DoAction1(BitStream stream, ItemRepresentation rep, ref NetworkMessageInfo info)
	{
		(rep as TorchItemRep).RepExtinguish();
	}

	// Token: 0x0600339F RID: 13215 RVA: 0x000BE3C0 File Offset: 0x000BC5C0
	public override void DoAction3(BitStream stream, ItemRepresentation itemRep, ref NetworkMessageInfo info)
	{
	}

	// Token: 0x060033A0 RID: 13216 RVA: 0x000BE3C4 File Offset: 0x000BC5C4
	public void OnExtinguish(ViewModel vm, ItemRepresentation itemRep, ITorchItem torchItem)
	{
	}

	// Token: 0x060033A1 RID: 13217 RVA: 0x000BE3C8 File Offset: 0x000BC5C8
	public void Ignite(ViewModel vm, ItemRepresentation itemRep, ITorchItem torchItem)
	{
		if (torchItem != null)
		{
			torchItem.Ignite();
		}
		bool flag = vm != null;
		if (flag)
		{
			this.StrikeSound.Play();
			GameObject light = vm.socketMap["muzzle"].socket.InstantiateAsChild(this.FirstPersonLightPrefab, false);
			if (torchItem != null)
			{
				torchItem.light = light;
			}
		}
		else if ((torchItem == null || !torchItem.light) && (!itemRep.networkView.isMine || actor.forceThirdPerson))
		{
			if (this.ThirdPersonLightPrefab)
			{
				((TorchItemRep)itemRep)._myLightPrefab = this.ThirdPersonLightPrefab;
			}
			((TorchItemRep)itemRep).RepIgnite();
			if (((TorchItemRep)itemRep)._myLight && torchItem != null)
			{
				torchItem.light = ((TorchItemRep)itemRep)._myLight;
			}
		}
	}

	// Token: 0x04001A02 RID: 6658
	public GameObject FirstPersonLightPrefab;

	// Token: 0x04001A03 RID: 6659
	public GameObject ThirdPersonLightPrefab;

	// Token: 0x04001A04 RID: 6660
	public AudioClip StrikeSound;

	// Token: 0x0200059D RID: 1437
	private sealed class ITEM_TYPE : TorchItem<TorchItemDataBlock>, IHeldItem, IInventoryItem, IThrowableItem, ITorchItem, IWeaponItem
	{
		// Token: 0x060033A2 RID: 13218 RVA: 0x000BE4BC File Offset: 0x000BC6BC
		public ITEM_TYPE(TorchItemDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x060033A3 RID: 13219 RVA: 0x000BE4C8 File Offset: 0x000BC6C8
		ItemDataBlock IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x060033A4 RID: 13220 RVA: 0x000BE4D0 File Offset: 0x000BC6D0
		void Ignite()
		{
			base.Ignite();
		}

		// Token: 0x060033A5 RID: 13221 RVA: 0x000BE4D8 File Offset: 0x000BC6D8
		void Extinguish()
		{
			base.Extinguish();
		}

		// Token: 0x060033A6 RID: 13222 RVA: 0x000BE4E0 File Offset: 0x000BC6E0
		bool get_isLit()
		{
			return base.isLit;
		}

		// Token: 0x060033A7 RID: 13223 RVA: 0x000BE4E8 File Offset: 0x000BC6E8
		float get_realThrowTime()
		{
			return base.realThrowTime;
		}

		// Token: 0x060033A8 RID: 13224 RVA: 0x000BE4F0 File Offset: 0x000BC6F0
		void set_realThrowTime(float value)
		{
			base.realThrowTime = value;
		}

		// Token: 0x060033A9 RID: 13225 RVA: 0x000BE4FC File Offset: 0x000BC6FC
		float get_realIgniteTime()
		{
			return base.realIgniteTime;
		}

		// Token: 0x060033AA RID: 13226 RVA: 0x000BE504 File Offset: 0x000BC704
		void set_realIgniteTime(float value)
		{
			base.realIgniteTime = value;
		}

		// Token: 0x060033AB RID: 13227 RVA: 0x000BE510 File Offset: 0x000BC710
		float get_forceSecondaryTime()
		{
			return base.forceSecondaryTime;
		}

		// Token: 0x060033AC RID: 13228 RVA: 0x000BE518 File Offset: 0x000BC718
		void set_forceSecondaryTime(float value)
		{
			base.forceSecondaryTime = value;
		}

		// Token: 0x060033AD RID: 13229 RVA: 0x000BE524 File Offset: 0x000BC724
		GameObject get_light()
		{
			return base.light;
		}

		// Token: 0x060033AE RID: 13230 RVA: 0x000BE52C File Offset: 0x000BC72C
		void set_light(GameObject value)
		{
			base.light = value;
		}

		// Token: 0x060033AF RID: 13231 RVA: 0x000BE538 File Offset: 0x000BC738
		float get_holdingStartTime()
		{
			return base.holdingStartTime;
		}

		// Token: 0x060033B0 RID: 13232 RVA: 0x000BE540 File Offset: 0x000BC740
		void set_holdingStartTime(float value)
		{
			base.holdingStartTime = value;
		}

		// Token: 0x060033B1 RID: 13233 RVA: 0x000BE54C File Offset: 0x000BC74C
		bool get_holdingBack()
		{
			return base.holdingBack;
		}

		// Token: 0x060033B2 RID: 13234 RVA: 0x000BE554 File Offset: 0x000BC754
		void set_holdingBack(bool value)
		{
			base.holdingBack = value;
		}

		// Token: 0x060033B3 RID: 13235 RVA: 0x000BE560 File Offset: 0x000BC760
		float get_minReleaseTime()
		{
			return base.minReleaseTime;
		}

		// Token: 0x060033B4 RID: 13236 RVA: 0x000BE568 File Offset: 0x000BC768
		void set_minReleaseTime(float value)
		{
			base.minReleaseTime = value;
		}

		// Token: 0x060033B5 RID: 13237 RVA: 0x000BE574 File Offset: 0x000BC774
		bool ValidatePrimaryMessageTime(double timestamp)
		{
			return base.ValidatePrimaryMessageTime(timestamp);
		}

		// Token: 0x060033B6 RID: 13238 RVA: 0x000BE580 File Offset: 0x000BC780
		bool get_canAim()
		{
			return base.canAim;
		}

		// Token: 0x060033B7 RID: 13239 RVA: 0x000BE588 File Offset: 0x000BC788
		float get_nextPrimaryAttackTime()
		{
			return base.nextPrimaryAttackTime;
		}

		// Token: 0x060033B8 RID: 13240 RVA: 0x000BE590 File Offset: 0x000BC790
		void set_nextPrimaryAttackTime(float value)
		{
			base.nextPrimaryAttackTime = value;
		}

		// Token: 0x060033B9 RID: 13241 RVA: 0x000BE59C File Offset: 0x000BC79C
		float get_nextSecondaryAttackTime()
		{
			return base.nextSecondaryAttackTime;
		}

		// Token: 0x060033BA RID: 13242 RVA: 0x000BE5A4 File Offset: 0x000BC7A4
		void set_nextSecondaryAttackTime(float value)
		{
			base.nextSecondaryAttackTime = value;
		}

		// Token: 0x060033BB RID: 13243 RVA: 0x000BE5B0 File Offset: 0x000BC7B0
		float get_deployFinishedTime()
		{
			return base.deployFinishedTime;
		}

		// Token: 0x060033BC RID: 13244 RVA: 0x000BE5B8 File Offset: 0x000BC7B8
		void set_deployFinishedTime(float value)
		{
			base.deployFinishedTime = value;
		}

		// Token: 0x060033BD RID: 13245 RVA: 0x000BE5C4 File Offset: 0x000BC7C4
		void SetTotalModSlotCount(int count)
		{
			base.SetTotalModSlotCount(count);
		}

		// Token: 0x060033BE RID: 13246 RVA: 0x000BE5D0 File Offset: 0x000BC7D0
		void SetUsedModSlotCount(int count)
		{
			base.SetUsedModSlotCount(count);
		}

		// Token: 0x060033BF RID: 13247 RVA: 0x000BE5DC File Offset: 0x000BC7DC
		void AddMod(ItemModDataBlock mod)
		{
			base.AddMod(mod);
		}

		// Token: 0x060033C0 RID: 13248 RVA: 0x000BE5E8 File Offset: 0x000BC7E8
		int FindMod(ItemModDataBlock mod)
		{
			return base.FindMod(mod);
		}

		// Token: 0x060033C1 RID: 13249 RVA: 0x000BE5F4 File Offset: 0x000BC7F4
		void OnActivate()
		{
			base.OnActivate();
		}

		// Token: 0x060033C2 RID: 13250 RVA: 0x000BE5FC File Offset: 0x000BC7FC
		void OnDeactivate()
		{
			base.OnDeactivate();
		}

		// Token: 0x060033C3 RID: 13251 RVA: 0x000BE604 File Offset: 0x000BC804
		ViewModel get_viewModelInstance()
		{
			return base.viewModelInstance;
		}

		// Token: 0x060033C4 RID: 13252 RVA: 0x000BE60C File Offset: 0x000BC80C
		ItemRepresentation get_itemRepresentation()
		{
			return base.itemRepresentation;
		}

		// Token: 0x060033C5 RID: 13253 RVA: 0x000BE614 File Offset: 0x000BC814
		void set_itemRepresentation(ItemRepresentation value)
		{
			base.itemRepresentation = value;
		}

		// Token: 0x060033C6 RID: 13254 RVA: 0x000BE620 File Offset: 0x000BC820
		bool get_canActivate()
		{
			return base.canActivate;
		}

		// Token: 0x060033C7 RID: 13255 RVA: 0x000BE628 File Offset: 0x000BC828
		bool get_canDeactivate()
		{
			return base.canDeactivate;
		}

		// Token: 0x060033C8 RID: 13256 RVA: 0x000BE630 File Offset: 0x000BC830
		ItemModFlags get_modFlags()
		{
			return base.modFlags;
		}

		// Token: 0x060033C9 RID: 13257 RVA: 0x000BE638 File Offset: 0x000BC838
		ItemModDataBlock[] get_itemMods()
		{
			return base.itemMods;
		}

		// Token: 0x060033CA RID: 13258 RVA: 0x000BE640 File Offset: 0x000BC840
		int get_totalModSlots()
		{
			return base.totalModSlots;
		}

		// Token: 0x060033CB RID: 13259 RVA: 0x000BE648 File Offset: 0x000BC848
		int get_usedModSlots()
		{
			return base.usedModSlots;
		}

		// Token: 0x060033CC RID: 13260 RVA: 0x000BE650 File Offset: 0x000BC850
		int get_freeModSlots()
		{
			return base.freeModSlots;
		}

		// Token: 0x060033CD RID: 13261 RVA: 0x000BE658 File Offset: 0x000BC858
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x060033CE RID: 13262 RVA: 0x000BE660 File Offset: 0x000BC860
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x060033CF RID: 13263 RVA: 0x000BE668 File Offset: 0x000BC868
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x060033D0 RID: 13264 RVA: 0x000BE670 File Offset: 0x000BC870
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x060033D1 RID: 13265 RVA: 0x000BE67C File Offset: 0x000BC87C
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x060033D2 RID: 13266 RVA: 0x000BE688 File Offset: 0x000BC888
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x060033D3 RID: 13267 RVA: 0x000BE694 File Offset: 0x000BC894
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x060033D4 RID: 13268 RVA: 0x000BE6A0 File Offset: 0x000BC8A0
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x060033D5 RID: 13269 RVA: 0x000BE6AC File Offset: 0x000BC8AC
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x060033D6 RID: 13270 RVA: 0x000BE6B8 File Offset: 0x000BC8B8
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x060033D7 RID: 13271 RVA: 0x000BE6C4 File Offset: 0x000BC8C4
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x060033D8 RID: 13272 RVA: 0x000BE6D0 File Offset: 0x000BC8D0
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x060033D9 RID: 13273 RVA: 0x000BE6D8 File Offset: 0x000BC8D8
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x060033DA RID: 13274 RVA: 0x000BE6E0 File Offset: 0x000BC8E0
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x060033DB RID: 13275 RVA: 0x000BE6E8 File Offset: 0x000BC8E8
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x060033DC RID: 13276 RVA: 0x000BE6F0 File Offset: 0x000BC8F0
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x060033DD RID: 13277 RVA: 0x000BE6F8 File Offset: 0x000BC8F8
		Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x060033DE RID: 13278 RVA: 0x000BE700 File Offset: 0x000BC900
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x060033DF RID: 13279 RVA: 0x000BE708 File Offset: 0x000BC908
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x060033E0 RID: 13280 RVA: 0x000BE710 File Offset: 0x000BC910
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x060033E1 RID: 13281 RVA: 0x000BE71C File Offset: 0x000BC91C
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x060033E2 RID: 13282 RVA: 0x000BE724 File Offset: 0x000BC924
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x060033E3 RID: 13283 RVA: 0x000BE72C File Offset: 0x000BC92C
		Character get_character()
		{
			return base.character;
		}

		// Token: 0x060033E4 RID: 13284 RVA: 0x000BE734 File Offset: 0x000BC934
		Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x060033E5 RID: 13285 RVA: 0x000BE73C File Offset: 0x000BC93C
		Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x060033E6 RID: 13286 RVA: 0x000BE744 File Offset: 0x000BC944
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x060033E7 RID: 13287 RVA: 0x000BE74C File Offset: 0x000BC94C
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
