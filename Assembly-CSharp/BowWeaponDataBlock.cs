using System;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x0200055F RID: 1375
public class BowWeaponDataBlock : WeaponDataBlock
{
	// Token: 0x06002EF1 RID: 12017 RVA: 0x000B7E14 File Offset: 0x000B6014
	protected override IInventoryItem ConstructItem()
	{
		return new BowWeaponDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x06002EF2 RID: 12018 RVA: 0x000B7E1C File Offset: 0x000B601C
	public override byte GetMaxEligableSlots()
	{
		return 0;
	}

	// Token: 0x06002EF3 RID: 12019 RVA: 0x000B7E20 File Offset: 0x000B6020
	public override void InstallData(IInventoryItem item)
	{
		base.InstallData(item);
	}

	// Token: 0x06002EF4 RID: 12020 RVA: 0x000B7E2C File Offset: 0x000B602C
	public virtual void Local_ReadyArrow(ViewModel vm, ItemRepresentation itemRep, IBowWeaponItem itemInstance, ref HumanController.InputSample sample)
	{
		if (vm)
		{
			vm.Play("drawarrow", 0);
		}
		itemInstance.completeDrawTime = Time.time + this.drawLength;
		this.drawArrowSound.PlayLocal(Camera.main.transform, Vector3.zero, 1f, 1f, 3f, 20f, 0);
	}

	// Token: 0x06002EF5 RID: 12021 RVA: 0x000B7E94 File Offset: 0x000B6094
	public virtual void Local_FireArrow(ViewModel vm, ItemRepresentation itemRep, IBowWeaponItem itemInstance, ref HumanController.InputSample sample)
	{
		if (vm)
		{
			vm.Play("fire_1", 0);
		}
		this.MakeReadyIn(itemInstance, this.fireRate);
		Character character = itemInstance.character;
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

	// Token: 0x06002EF6 RID: 12022 RVA: 0x000B7F1C File Offset: 0x000B611C
	public void FireArrow(Vector3 pos, Quaternion ang, ItemRepresentation itemRep, IBowWeaponItem itemInstance)
	{
		GameObject gameObject = (GameObject)Object.Instantiate(this.arrowPrefab, pos, ang);
		ArrowMovement component = gameObject.GetComponent<ArrowMovement>();
		component.Init(this.arrowSpeed, itemRep, itemInstance, false);
		this.fireArrowSound.Play(pos, 1f, 2f, 10f);
	}

	// Token: 0x06002EF7 RID: 12023 RVA: 0x000B7F70 File Offset: 0x000B6170
	public void ArrowReportMiss(ArrowMovement arrow, ItemRepresentation itemRepresentation)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<Vector3>(arrow.transform.position, new object[0]);
		itemRepresentation.ActionStream(3, 0, bitStream);
	}

	// Token: 0x06002EF8 RID: 12024 RVA: 0x000B7FA4 File Offset: 0x000B61A4
	public void ArrowReportHit(IDMain hitMain, ArrowMovement arrow, ItemRepresentation itemRepresentation, IBowWeaponItem itemInstance)
	{
		if (!hitMain)
		{
			return;
		}
		TakeDamage component = hitMain.GetComponent<TakeDamage>();
		if (!component)
		{
			return;
		}
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NetEntityID>(NetEntityID.Get(hitMain), new object[0]);
		bitStream.Write<Vector3>(hitMain.transform.position, new object[0]);
		itemRepresentation.ActionStream(2, 0, bitStream);
		Character character = itemInstance.character;
		if (component && component.ShouldPlayHitNotification())
		{
			this.PlayHitNotification(arrow.transform.position, character);
		}
	}

	// Token: 0x06002EF9 RID: 12025 RVA: 0x000B803C File Offset: 0x000B623C
	public override void DoAction1(BitStream stream, ItemRepresentation rep, ref NetworkMessageInfo info)
	{
		Vector3 pos = stream.ReadVector3();
		Quaternion ang = stream.ReadQuaternion();
		this.FireArrow(pos, ang, rep, null);
	}

	// Token: 0x06002EFA RID: 12026 RVA: 0x000B8064 File Offset: 0x000B6264
	public virtual void Local_CancelArrow(ViewModel vm, ItemRepresentation itemRep, IBowWeaponItem itemInstance, ref HumanController.InputSample sample)
	{
		if (vm)
		{
			vm.CrossFade("cancelarrow", 0.15f);
		}
		this.MakeReadyIn(itemInstance, this.fireRate * 3f);
		this.cancelArrowSound.PlayLocal(Camera.main.transform, Vector3.zero, 1f, 1f, 3f, 20f, 0);
	}

	// Token: 0x06002EFB RID: 12027 RVA: 0x000B80D0 File Offset: 0x000B62D0
	public virtual void MakeReadyIn(IBowWeaponItem itemInstance, float delay)
	{
		itemInstance.MakeReadyIn(delay);
	}

	// Token: 0x06002EFC RID: 12028 RVA: 0x000B80DC File Offset: 0x000B62DC
	public virtual void DoWeaponEffects(Transform soundTransform, Vector3 startPos, Vector3 endPos, Socket muzzleSocket, bool firstPerson, Component hitComponent, bool allowBlood, ItemRepresentation itemRep)
	{
	}

	// Token: 0x06002EFD RID: 12029 RVA: 0x000B80E0 File Offset: 0x000B62E0
	public virtual void Local_GetTired(ViewModel vm, ItemRepresentation itemRep, IBowWeaponItem itemInstance, ref HumanController.InputSample sample)
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

	// Token: 0x06002EFE RID: 12030 RVA: 0x000B810C File Offset: 0x000B630C
	public virtual float GetGUIDamage()
	{
		return 999f;
	}

	// Token: 0x06002EFF RID: 12031 RVA: 0x000B8114 File Offset: 0x000B6314
	public override void PopulateInfoWindow(ItemToolTip infoWindow, IInventoryItem tipItem)
	{
	}

	// Token: 0x06002F00 RID: 12032 RVA: 0x000B8118 File Offset: 0x000B6318
	public override string GetItemDescription()
	{
		return "This is a weapon. Drag it to your belt (right side of screen) and press the corresponding number key to use it.";
	}

	// Token: 0x06002F01 RID: 12033 RVA: 0x000B8120 File Offset: 0x000B6320
	protected new virtual void PlayHitNotification(Vector3 point, Character shooterOrNull)
	{
		if (WeaponDataBlock._hitNotify || Bundling.Load<AudioClip>("content/shared/sfx/hitnotification", out WeaponDataBlock._hitNotify))
		{
			WeaponDataBlock._hitNotify.PlayLocal(Camera.main.transform, Vector3.zero, 1f, 1);
		}
		if (BowWeaponDataBlock._hitIndicator || Bundling.Load<HUDHitIndicator>("content/hud/HUDHitIndicator", out BowWeaponDataBlock._hitIndicator))
		{
			bool followPoint = true;
			HUDHitIndicator.CreateIndicator(point, followPoint, BowWeaponDataBlock._hitIndicator);
		}
	}

	// Token: 0x06002F02 RID: 12034 RVA: 0x000B81A0 File Offset: 0x000B63A0
	protected override void SecureWriteMemberValues(BitStream stream)
	{
		base.SecureWriteMemberValues(stream);
	}

	// Token: 0x04001924 RID: 6436
	public AudioClip drawArrowSound;

	// Token: 0x04001925 RID: 6437
	public AudioClip fireArrowSound;

	// Token: 0x04001926 RID: 6438
	public AudioClip cancelArrowSound;

	// Token: 0x04001927 RID: 6439
	public float arrowSpeed;

	// Token: 0x04001928 RID: 6440
	public float tooTiredLength = 8f;

	// Token: 0x04001929 RID: 6441
	public float drawLength = 2f;

	// Token: 0x0400192A RID: 6442
	public ItemDataBlock defaultAmmo;

	// Token: 0x0400192B RID: 6443
	public GameObject arrowPrefab;

	// Token: 0x0400192C RID: 6444
	public string arrowPickupString;

	// Token: 0x0400192D RID: 6445
	private static HUDHitIndicator _hitIndicator;

	// Token: 0x02000560 RID: 1376
	private sealed class ITEM_TYPE : BowWeaponItem<BowWeaponDataBlock>, IBowWeaponItem, IHeldItem, IInventoryItem, IWeaponItem
	{
		// Token: 0x06002F03 RID: 12035 RVA: 0x000B81AC File Offset: 0x000B63AC
		public ITEM_TYPE(BowWeaponDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x06002F04 RID: 12036 RVA: 0x000B81B8 File Offset: 0x000B63B8
		ItemDataBlock IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x06002F05 RID: 12037 RVA: 0x000B81C0 File Offset: 0x000B63C0
		IInventoryItem FindAmmo()
		{
			return base.FindAmmo();
		}

		// Token: 0x06002F06 RID: 12038 RVA: 0x000B81C8 File Offset: 0x000B63C8
		void ArrowReportMiss(ArrowMovement arrow)
		{
			base.ArrowReportMiss(arrow);
		}

		// Token: 0x06002F07 RID: 12039 RVA: 0x000B81D4 File Offset: 0x000B63D4
		void ArrowReportHit(IDMain hitMain, ArrowMovement arrow)
		{
			base.ArrowReportHit(hitMain, arrow);
		}

		// Token: 0x06002F08 RID: 12040 RVA: 0x000B81E0 File Offset: 0x000B63E0
		void MakeReadyIn(float delay)
		{
			base.MakeReadyIn(delay);
		}

		// Token: 0x06002F09 RID: 12041 RVA: 0x000B81EC File Offset: 0x000B63EC
		bool get_arrowDrawn()
		{
			return base.arrowDrawn;
		}

		// Token: 0x06002F0A RID: 12042 RVA: 0x000B81F4 File Offset: 0x000B63F4
		void set_arrowDrawn(bool value)
		{
			base.arrowDrawn = value;
		}

		// Token: 0x06002F0B RID: 12043 RVA: 0x000B8200 File Offset: 0x000B6400
		bool get_tired()
		{
			return base.tired;
		}

		// Token: 0x06002F0C RID: 12044 RVA: 0x000B8208 File Offset: 0x000B6408
		void set_tired(bool value)
		{
			base.tired = value;
		}

		// Token: 0x06002F0D RID: 12045 RVA: 0x000B8214 File Offset: 0x000B6414
		float get_completeDrawTime()
		{
			return base.completeDrawTime;
		}

		// Token: 0x06002F0E RID: 12046 RVA: 0x000B821C File Offset: 0x000B641C
		void set_completeDrawTime(float value)
		{
			base.completeDrawTime = value;
		}

		// Token: 0x06002F0F RID: 12047 RVA: 0x000B8228 File Offset: 0x000B6428
		int get_currentArrowID()
		{
			return base.currentArrowID;
		}

		// Token: 0x06002F10 RID: 12048 RVA: 0x000B8230 File Offset: 0x000B6430
		void set_currentArrowID(int value)
		{
			base.currentArrowID = value;
		}

		// Token: 0x06002F11 RID: 12049 RVA: 0x000B823C File Offset: 0x000B643C
		bool ValidatePrimaryMessageTime(double timestamp)
		{
			return base.ValidatePrimaryMessageTime(timestamp);
		}

		// Token: 0x06002F12 RID: 12050 RVA: 0x000B8248 File Offset: 0x000B6448
		bool get_canAim()
		{
			return base.canAim;
		}

		// Token: 0x06002F13 RID: 12051 RVA: 0x000B8250 File Offset: 0x000B6450
		float get_nextPrimaryAttackTime()
		{
			return base.nextPrimaryAttackTime;
		}

		// Token: 0x06002F14 RID: 12052 RVA: 0x000B8258 File Offset: 0x000B6458
		void set_nextPrimaryAttackTime(float value)
		{
			base.nextPrimaryAttackTime = value;
		}

		// Token: 0x06002F15 RID: 12053 RVA: 0x000B8264 File Offset: 0x000B6464
		float get_nextSecondaryAttackTime()
		{
			return base.nextSecondaryAttackTime;
		}

		// Token: 0x06002F16 RID: 12054 RVA: 0x000B826C File Offset: 0x000B646C
		void set_nextSecondaryAttackTime(float value)
		{
			base.nextSecondaryAttackTime = value;
		}

		// Token: 0x06002F17 RID: 12055 RVA: 0x000B8278 File Offset: 0x000B6478
		float get_deployFinishedTime()
		{
			return base.deployFinishedTime;
		}

		// Token: 0x06002F18 RID: 12056 RVA: 0x000B8280 File Offset: 0x000B6480
		void set_deployFinishedTime(float value)
		{
			base.deployFinishedTime = value;
		}

		// Token: 0x06002F19 RID: 12057 RVA: 0x000B828C File Offset: 0x000B648C
		void SetTotalModSlotCount(int count)
		{
			base.SetTotalModSlotCount(count);
		}

		// Token: 0x06002F1A RID: 12058 RVA: 0x000B8298 File Offset: 0x000B6498
		void SetUsedModSlotCount(int count)
		{
			base.SetUsedModSlotCount(count);
		}

		// Token: 0x06002F1B RID: 12059 RVA: 0x000B82A4 File Offset: 0x000B64A4
		void AddMod(ItemModDataBlock mod)
		{
			base.AddMod(mod);
		}

		// Token: 0x06002F1C RID: 12060 RVA: 0x000B82B0 File Offset: 0x000B64B0
		int FindMod(ItemModDataBlock mod)
		{
			return base.FindMod(mod);
		}

		// Token: 0x06002F1D RID: 12061 RVA: 0x000B82BC File Offset: 0x000B64BC
		void OnActivate()
		{
			base.OnActivate();
		}

		// Token: 0x06002F1E RID: 12062 RVA: 0x000B82C4 File Offset: 0x000B64C4
		void OnDeactivate()
		{
			base.OnDeactivate();
		}

		// Token: 0x06002F1F RID: 12063 RVA: 0x000B82CC File Offset: 0x000B64CC
		ViewModel get_viewModelInstance()
		{
			return base.viewModelInstance;
		}

		// Token: 0x06002F20 RID: 12064 RVA: 0x000B82D4 File Offset: 0x000B64D4
		ItemRepresentation get_itemRepresentation()
		{
			return base.itemRepresentation;
		}

		// Token: 0x06002F21 RID: 12065 RVA: 0x000B82DC File Offset: 0x000B64DC
		void set_itemRepresentation(ItemRepresentation value)
		{
			base.itemRepresentation = value;
		}

		// Token: 0x06002F22 RID: 12066 RVA: 0x000B82E8 File Offset: 0x000B64E8
		bool get_canActivate()
		{
			return base.canActivate;
		}

		// Token: 0x06002F23 RID: 12067 RVA: 0x000B82F0 File Offset: 0x000B64F0
		bool get_canDeactivate()
		{
			return base.canDeactivate;
		}

		// Token: 0x06002F24 RID: 12068 RVA: 0x000B82F8 File Offset: 0x000B64F8
		ItemModFlags get_modFlags()
		{
			return base.modFlags;
		}

		// Token: 0x06002F25 RID: 12069 RVA: 0x000B8300 File Offset: 0x000B6500
		ItemModDataBlock[] get_itemMods()
		{
			return base.itemMods;
		}

		// Token: 0x06002F26 RID: 12070 RVA: 0x000B8308 File Offset: 0x000B6508
		int get_totalModSlots()
		{
			return base.totalModSlots;
		}

		// Token: 0x06002F27 RID: 12071 RVA: 0x000B8310 File Offset: 0x000B6510
		int get_usedModSlots()
		{
			return base.usedModSlots;
		}

		// Token: 0x06002F28 RID: 12072 RVA: 0x000B8318 File Offset: 0x000B6518
		int get_freeModSlots()
		{
			return base.freeModSlots;
		}

		// Token: 0x06002F29 RID: 12073 RVA: 0x000B8320 File Offset: 0x000B6520
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x06002F2A RID: 12074 RVA: 0x000B8328 File Offset: 0x000B6528
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x06002F2B RID: 12075 RVA: 0x000B8330 File Offset: 0x000B6530
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x06002F2C RID: 12076 RVA: 0x000B8338 File Offset: 0x000B6538
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x06002F2D RID: 12077 RVA: 0x000B8344 File Offset: 0x000B6544
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x06002F2E RID: 12078 RVA: 0x000B8350 File Offset: 0x000B6550
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x06002F2F RID: 12079 RVA: 0x000B835C File Offset: 0x000B655C
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x06002F30 RID: 12080 RVA: 0x000B8368 File Offset: 0x000B6568
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x06002F31 RID: 12081 RVA: 0x000B8374 File Offset: 0x000B6574
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x06002F32 RID: 12082 RVA: 0x000B8380 File Offset: 0x000B6580
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x06002F33 RID: 12083 RVA: 0x000B838C File Offset: 0x000B658C
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x06002F34 RID: 12084 RVA: 0x000B8398 File Offset: 0x000B6598
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x06002F35 RID: 12085 RVA: 0x000B83A0 File Offset: 0x000B65A0
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x06002F36 RID: 12086 RVA: 0x000B83A8 File Offset: 0x000B65A8
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x06002F37 RID: 12087 RVA: 0x000B83B0 File Offset: 0x000B65B0
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x06002F38 RID: 12088 RVA: 0x000B83B8 File Offset: 0x000B65B8
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x06002F39 RID: 12089 RVA: 0x000B83C0 File Offset: 0x000B65C0
		Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x06002F3A RID: 12090 RVA: 0x000B83C8 File Offset: 0x000B65C8
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x06002F3B RID: 12091 RVA: 0x000B83D0 File Offset: 0x000B65D0
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x06002F3C RID: 12092 RVA: 0x000B83D8 File Offset: 0x000B65D8
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x06002F3D RID: 12093 RVA: 0x000B83E4 File Offset: 0x000B65E4
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x06002F3E RID: 12094 RVA: 0x000B83EC File Offset: 0x000B65EC
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x06002F3F RID: 12095 RVA: 0x000B83F4 File Offset: 0x000B65F4
		Character get_character()
		{
			return base.character;
		}

		// Token: 0x06002F40 RID: 12096 RVA: 0x000B83FC File Offset: 0x000B65FC
		Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x06002F41 RID: 12097 RVA: 0x000B8404 File Offset: 0x000B6604
		Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x06002F42 RID: 12098 RVA: 0x000B840C File Offset: 0x000B660C
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x06002F43 RID: 12099 RVA: 0x000B8414 File Offset: 0x000B6614
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
