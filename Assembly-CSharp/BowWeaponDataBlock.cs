using System;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x0200061D RID: 1565
public class BowWeaponDataBlock : global::WeaponDataBlock
{
	// Token: 0x060032B9 RID: 12985 RVA: 0x000C0070 File Offset: 0x000BE270
	protected override global::IInventoryItem ConstructItem()
	{
		return new global::BowWeaponDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x060032BA RID: 12986 RVA: 0x000C0078 File Offset: 0x000BE278
	public override byte GetMaxEligableSlots()
	{
		return 0;
	}

	// Token: 0x060032BB RID: 12987 RVA: 0x000C007C File Offset: 0x000BE27C
	public override void InstallData(global::IInventoryItem item)
	{
		base.InstallData(item);
	}

	// Token: 0x060032BC RID: 12988 RVA: 0x000C0088 File Offset: 0x000BE288
	public virtual void Local_ReadyArrow(global::ViewModel vm, global::ItemRepresentation itemRep, global::IBowWeaponItem itemInstance, ref global::HumanController.InputSample sample)
	{
		if (vm)
		{
			vm.Play("drawarrow", 0);
		}
		itemInstance.completeDrawTime = Time.time + this.drawLength;
		this.drawArrowSound.PlayLocal(Camera.main.transform, Vector3.zero, 1f, 1f, 3f, 20f, 0);
	}

	// Token: 0x060032BD RID: 12989 RVA: 0x000C00F0 File Offset: 0x000BE2F0
	public virtual void Local_FireArrow(global::ViewModel vm, global::ItemRepresentation itemRep, global::IBowWeaponItem itemInstance, ref global::HumanController.InputSample sample)
	{
		if (vm)
		{
			vm.Play("fire_1", 0);
		}
		this.MakeReadyIn(itemInstance, this.fireRate);
		global::Character character = itemInstance.character;
		if (character == null)
		{
			return;
		}
		Ray eyesRay = character.eyesRay;
		Vector3 eyesOrigin = character.eyesOrigin;
		this.FireArrow(eyesOrigin, character.eyesRotation, itemRep, itemInstance);
		BitStream bitStream = new BitStream(false);
		bitStream.WriteVector3(eyesOrigin);
		bitStream.WriteQuaternion(character.eyesRotation);
		itemRep.ActionStream(1, 0, bitStream);
	}

	// Token: 0x060032BE RID: 12990 RVA: 0x000C0178 File Offset: 0x000BE378
	public void FireArrow(Vector3 pos, Quaternion ang, global::ItemRepresentation itemRep, global::IBowWeaponItem itemInstance)
	{
		GameObject gameObject = (GameObject)Object.Instantiate(this.arrowPrefab, pos, ang);
		global::ArrowMovement component = gameObject.GetComponent<global::ArrowMovement>();
		component.Init(this.arrowSpeed, itemRep, itemInstance, false);
		this.fireArrowSound.Play(pos, 1f, 2f, 10f);
	}

	// Token: 0x060032BF RID: 12991 RVA: 0x000C01CC File Offset: 0x000BE3CC
	public void ArrowReportMiss(global::ArrowMovement arrow, global::ItemRepresentation itemRepresentation)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<Vector3>(arrow.transform.position, new object[0]);
		itemRepresentation.ActionStream(3, 0, bitStream);
	}

	// Token: 0x060032C0 RID: 12992 RVA: 0x000C0200 File Offset: 0x000BE400
	public void ArrowReportHit(IDMain hitMain, global::ArrowMovement arrow, global::ItemRepresentation itemRepresentation, global::IBowWeaponItem itemInstance)
	{
		if (!hitMain)
		{
			return;
		}
		global::TakeDamage component = hitMain.GetComponent<global::TakeDamage>();
		if (!component)
		{
			return;
		}
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NetEntityID>(global::NetEntityID.Get(hitMain), new object[0]);
		bitStream.Write<Vector3>(hitMain.transform.position, new object[0]);
		itemRepresentation.ActionStream(2, 0, bitStream);
		global::Character character = itemInstance.character;
		if (component && component.ShouldPlayHitNotification())
		{
			this.PlayHitNotification(arrow.transform.position, character);
		}
	}

	// Token: 0x060032C1 RID: 12993 RVA: 0x000C0298 File Offset: 0x000BE498
	public override void DoAction1(BitStream stream, global::ItemRepresentation rep, ref uLink.NetworkMessageInfo info)
	{
		Vector3 pos = stream.ReadVector3();
		Quaternion ang = stream.ReadQuaternion();
		this.FireArrow(pos, ang, rep, null);
	}

	// Token: 0x060032C2 RID: 12994 RVA: 0x000C02C0 File Offset: 0x000BE4C0
	public virtual void Local_CancelArrow(global::ViewModel vm, global::ItemRepresentation itemRep, global::IBowWeaponItem itemInstance, ref global::HumanController.InputSample sample)
	{
		if (vm)
		{
			vm.CrossFade("cancelarrow", 0.15f);
		}
		this.MakeReadyIn(itemInstance, this.fireRate * 3f);
		this.cancelArrowSound.PlayLocal(Camera.main.transform, Vector3.zero, 1f, 1f, 3f, 20f, 0);
	}

	// Token: 0x060032C3 RID: 12995 RVA: 0x000C032C File Offset: 0x000BE52C
	public virtual void MakeReadyIn(global::IBowWeaponItem itemInstance, float delay)
	{
		itemInstance.MakeReadyIn(delay);
	}

	// Token: 0x060032C4 RID: 12996 RVA: 0x000C0338 File Offset: 0x000BE538
	public virtual void DoWeaponEffects(Transform soundTransform, Vector3 startPos, Vector3 endPos, global::Socket muzzleSocket, bool firstPerson, Component hitComponent, bool allowBlood, global::ItemRepresentation itemRep)
	{
	}

	// Token: 0x060032C5 RID: 12997 RVA: 0x000C033C File Offset: 0x000BE53C
	public virtual void Local_GetTired(global::ViewModel vm, global::ItemRepresentation itemRep, global::IBowWeaponItem itemInstance, ref global::HumanController.InputSample sample)
	{
		if (itemInstance.tired)
		{
			return;
		}
		if (vm)
		{
			vm.CrossFade("tiredloop", 5f);
		}
	}

	// Token: 0x060032C6 RID: 12998 RVA: 0x000C0368 File Offset: 0x000BE568
	public virtual float GetGUIDamage()
	{
		return 999f;
	}

	// Token: 0x060032C7 RID: 12999 RVA: 0x000C0370 File Offset: 0x000BE570
	public override void PopulateInfoWindow(global::ItemToolTip infoWindow, global::IInventoryItem tipItem)
	{
	}

	// Token: 0x060032C8 RID: 13000 RVA: 0x000C0374 File Offset: 0x000BE574
	public override string GetItemDescription()
	{
		return "This is a weapon. Drag it to your belt (right side of screen) and press the corresponding number key to use it.";
	}

	// Token: 0x060032C9 RID: 13001 RVA: 0x000C037C File Offset: 0x000BE57C
	protected new virtual void PlayHitNotification(Vector3 point, global::Character shooterOrNull)
	{
		if (global::WeaponDataBlock._hitNotify || Facepunch.Bundling.Load<AudioClip>("content/shared/sfx/hitnotification", out global::WeaponDataBlock._hitNotify))
		{
			global::WeaponDataBlock._hitNotify.PlayLocal(Camera.main.transform, Vector3.zero, 1f, 1);
		}
		if (global::BowWeaponDataBlock._hitIndicator || Facepunch.Bundling.Load<global::HUDHitIndicator>("content/hud/HUDHitIndicator", out global::BowWeaponDataBlock._hitIndicator))
		{
			bool followPoint = true;
			global::HUDHitIndicator.CreateIndicator(point, followPoint, global::BowWeaponDataBlock._hitIndicator);
		}
	}

	// Token: 0x060032CA RID: 13002 RVA: 0x000C03FC File Offset: 0x000BE5FC
	protected override void SecureWriteMemberValues(BitStream stream)
	{
		base.SecureWriteMemberValues(stream);
	}

	// Token: 0x04001AF5 RID: 6901
	public AudioClip drawArrowSound;

	// Token: 0x04001AF6 RID: 6902
	public AudioClip fireArrowSound;

	// Token: 0x04001AF7 RID: 6903
	public AudioClip cancelArrowSound;

	// Token: 0x04001AF8 RID: 6904
	public float arrowSpeed;

	// Token: 0x04001AF9 RID: 6905
	public float tooTiredLength = 8f;

	// Token: 0x04001AFA RID: 6906
	public float drawLength = 2f;

	// Token: 0x04001AFB RID: 6907
	public global::ItemDataBlock defaultAmmo;

	// Token: 0x04001AFC RID: 6908
	public GameObject arrowPrefab;

	// Token: 0x04001AFD RID: 6909
	public string arrowPickupString;

	// Token: 0x04001AFE RID: 6910
	private static global::HUDHitIndicator _hitIndicator;

	// Token: 0x0200061E RID: 1566
	private sealed class ITEM_TYPE : global::BowWeaponItem<global::BowWeaponDataBlock>, global::IBowWeaponItem, global::IHeldItem, global::IInventoryItem, global::IWeaponItem
	{
		// Token: 0x060032CB RID: 13003 RVA: 0x000C0408 File Offset: 0x000BE608
		public ITEM_TYPE(global::BowWeaponDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x060032CC RID: 13004 RVA: 0x000C0414 File Offset: 0x000BE614
		global::ItemDataBlock global::IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x060032CD RID: 13005 RVA: 0x000C041C File Offset: 0x000BE61C
		global::IInventoryItem FindAmmo()
		{
			return base.FindAmmo();
		}

		// Token: 0x060032CE RID: 13006 RVA: 0x000C0424 File Offset: 0x000BE624
		void ArrowReportMiss(global::ArrowMovement arrow)
		{
			base.ArrowReportMiss(arrow);
		}

		// Token: 0x060032CF RID: 13007 RVA: 0x000C0430 File Offset: 0x000BE630
		void ArrowReportHit(IDMain hitMain, global::ArrowMovement arrow)
		{
			base.ArrowReportHit(hitMain, arrow);
		}

		// Token: 0x060032D0 RID: 13008 RVA: 0x000C043C File Offset: 0x000BE63C
		void MakeReadyIn(float delay)
		{
			base.MakeReadyIn(delay);
		}

		// Token: 0x060032D1 RID: 13009 RVA: 0x000C0448 File Offset: 0x000BE648
		bool get_arrowDrawn()
		{
			return base.arrowDrawn;
		}

		// Token: 0x060032D2 RID: 13010 RVA: 0x000C0450 File Offset: 0x000BE650
		void set_arrowDrawn(bool value)
		{
			base.arrowDrawn = value;
		}

		// Token: 0x060032D3 RID: 13011 RVA: 0x000C045C File Offset: 0x000BE65C
		bool get_tired()
		{
			return base.tired;
		}

		// Token: 0x060032D4 RID: 13012 RVA: 0x000C0464 File Offset: 0x000BE664
		void set_tired(bool value)
		{
			base.tired = value;
		}

		// Token: 0x060032D5 RID: 13013 RVA: 0x000C0470 File Offset: 0x000BE670
		float get_completeDrawTime()
		{
			return base.completeDrawTime;
		}

		// Token: 0x060032D6 RID: 13014 RVA: 0x000C0478 File Offset: 0x000BE678
		void set_completeDrawTime(float value)
		{
			base.completeDrawTime = value;
		}

		// Token: 0x060032D7 RID: 13015 RVA: 0x000C0484 File Offset: 0x000BE684
		int get_currentArrowID()
		{
			return base.currentArrowID;
		}

		// Token: 0x060032D8 RID: 13016 RVA: 0x000C048C File Offset: 0x000BE68C
		void set_currentArrowID(int value)
		{
			base.currentArrowID = value;
		}

		// Token: 0x060032D9 RID: 13017 RVA: 0x000C0498 File Offset: 0x000BE698
		bool ValidatePrimaryMessageTime(double timestamp)
		{
			return base.ValidatePrimaryMessageTime(timestamp);
		}

		// Token: 0x060032DA RID: 13018 RVA: 0x000C04A4 File Offset: 0x000BE6A4
		bool get_canAim()
		{
			return base.canAim;
		}

		// Token: 0x060032DB RID: 13019 RVA: 0x000C04AC File Offset: 0x000BE6AC
		float get_nextPrimaryAttackTime()
		{
			return base.nextPrimaryAttackTime;
		}

		// Token: 0x060032DC RID: 13020 RVA: 0x000C04B4 File Offset: 0x000BE6B4
		void set_nextPrimaryAttackTime(float value)
		{
			base.nextPrimaryAttackTime = value;
		}

		// Token: 0x060032DD RID: 13021 RVA: 0x000C04C0 File Offset: 0x000BE6C0
		float get_nextSecondaryAttackTime()
		{
			return base.nextSecondaryAttackTime;
		}

		// Token: 0x060032DE RID: 13022 RVA: 0x000C04C8 File Offset: 0x000BE6C8
		void set_nextSecondaryAttackTime(float value)
		{
			base.nextSecondaryAttackTime = value;
		}

		// Token: 0x060032DF RID: 13023 RVA: 0x000C04D4 File Offset: 0x000BE6D4
		float get_deployFinishedTime()
		{
			return base.deployFinishedTime;
		}

		// Token: 0x060032E0 RID: 13024 RVA: 0x000C04DC File Offset: 0x000BE6DC
		void set_deployFinishedTime(float value)
		{
			base.deployFinishedTime = value;
		}

		// Token: 0x060032E1 RID: 13025 RVA: 0x000C04E8 File Offset: 0x000BE6E8
		void SetTotalModSlotCount(int count)
		{
			base.SetTotalModSlotCount(count);
		}

		// Token: 0x060032E2 RID: 13026 RVA: 0x000C04F4 File Offset: 0x000BE6F4
		void SetUsedModSlotCount(int count)
		{
			base.SetUsedModSlotCount(count);
		}

		// Token: 0x060032E3 RID: 13027 RVA: 0x000C0500 File Offset: 0x000BE700
		void AddMod(global::ItemModDataBlock mod)
		{
			base.AddMod(mod);
		}

		// Token: 0x060032E4 RID: 13028 RVA: 0x000C050C File Offset: 0x000BE70C
		int FindMod(global::ItemModDataBlock mod)
		{
			return base.FindMod(mod);
		}

		// Token: 0x060032E5 RID: 13029 RVA: 0x000C0518 File Offset: 0x000BE718
		void OnActivate()
		{
			base.OnActivate();
		}

		// Token: 0x060032E6 RID: 13030 RVA: 0x000C0520 File Offset: 0x000BE720
		void OnDeactivate()
		{
			base.OnDeactivate();
		}

		// Token: 0x060032E7 RID: 13031 RVA: 0x000C0528 File Offset: 0x000BE728
		global::ViewModel get_viewModelInstance()
		{
			return base.viewModelInstance;
		}

		// Token: 0x060032E8 RID: 13032 RVA: 0x000C0530 File Offset: 0x000BE730
		global::ItemRepresentation get_itemRepresentation()
		{
			return base.itemRepresentation;
		}

		// Token: 0x060032E9 RID: 13033 RVA: 0x000C0538 File Offset: 0x000BE738
		void set_itemRepresentation(global::ItemRepresentation value)
		{
			base.itemRepresentation = value;
		}

		// Token: 0x060032EA RID: 13034 RVA: 0x000C0544 File Offset: 0x000BE744
		bool get_canActivate()
		{
			return base.canActivate;
		}

		// Token: 0x060032EB RID: 13035 RVA: 0x000C054C File Offset: 0x000BE74C
		bool get_canDeactivate()
		{
			return base.canDeactivate;
		}

		// Token: 0x060032EC RID: 13036 RVA: 0x000C0554 File Offset: 0x000BE754
		global::ItemModFlags get_modFlags()
		{
			return base.modFlags;
		}

		// Token: 0x060032ED RID: 13037 RVA: 0x000C055C File Offset: 0x000BE75C
		global::ItemModDataBlock[] get_itemMods()
		{
			return base.itemMods;
		}

		// Token: 0x060032EE RID: 13038 RVA: 0x000C0564 File Offset: 0x000BE764
		int get_totalModSlots()
		{
			return base.totalModSlots;
		}

		// Token: 0x060032EF RID: 13039 RVA: 0x000C056C File Offset: 0x000BE76C
		int get_usedModSlots()
		{
			return base.usedModSlots;
		}

		// Token: 0x060032F0 RID: 13040 RVA: 0x000C0574 File Offset: 0x000BE774
		int get_freeModSlots()
		{
			return base.freeModSlots;
		}

		// Token: 0x060032F1 RID: 13041 RVA: 0x000C057C File Offset: 0x000BE77C
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x060032F2 RID: 13042 RVA: 0x000C0584 File Offset: 0x000BE784
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x060032F3 RID: 13043 RVA: 0x000C058C File Offset: 0x000BE78C
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x060032F4 RID: 13044 RVA: 0x000C0594 File Offset: 0x000BE794
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x060032F5 RID: 13045 RVA: 0x000C05A0 File Offset: 0x000BE7A0
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x060032F6 RID: 13046 RVA: 0x000C05AC File Offset: 0x000BE7AC
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x060032F7 RID: 13047 RVA: 0x000C05B8 File Offset: 0x000BE7B8
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x060032F8 RID: 13048 RVA: 0x000C05C4 File Offset: 0x000BE7C4
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x060032F9 RID: 13049 RVA: 0x000C05D0 File Offset: 0x000BE7D0
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x060032FA RID: 13050 RVA: 0x000C05DC File Offset: 0x000BE7DC
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x060032FB RID: 13051 RVA: 0x000C05E8 File Offset: 0x000BE7E8
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x060032FC RID: 13052 RVA: 0x000C05F4 File Offset: 0x000BE7F4
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x060032FD RID: 13053 RVA: 0x000C05FC File Offset: 0x000BE7FC
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x060032FE RID: 13054 RVA: 0x000C0604 File Offset: 0x000BE804
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x060032FF RID: 13055 RVA: 0x000C060C File Offset: 0x000BE80C
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x06003300 RID: 13056 RVA: 0x000C0614 File Offset: 0x000BE814
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x06003301 RID: 13057 RVA: 0x000C061C File Offset: 0x000BE81C
		global::Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x06003302 RID: 13058 RVA: 0x000C0624 File Offset: 0x000BE824
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x06003303 RID: 13059 RVA: 0x000C062C File Offset: 0x000BE82C
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x06003304 RID: 13060 RVA: 0x000C0634 File Offset: 0x000BE834
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x06003305 RID: 13061 RVA: 0x000C0640 File Offset: 0x000BE840
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x06003306 RID: 13062 RVA: 0x000C0648 File Offset: 0x000BE848
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x06003307 RID: 13063 RVA: 0x000C0650 File Offset: 0x000BE850
		global::Character get_character()
		{
			return base.character;
		}

		// Token: 0x06003308 RID: 13064 RVA: 0x000C0658 File Offset: 0x000BE858
		global::Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x06003309 RID: 13065 RVA: 0x000C0660 File Offset: 0x000BE860
		global::Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x0600330A RID: 13066 RVA: 0x000C0668 File Offset: 0x000BE868
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x0600330B RID: 13067 RVA: 0x000C0670 File Offset: 0x000BE870
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
