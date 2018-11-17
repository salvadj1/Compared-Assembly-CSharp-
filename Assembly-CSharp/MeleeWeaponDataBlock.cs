using System;
using Rust;
using uLink;
using UnityEngine;

// Token: 0x0200063F RID: 1599
public class MeleeWeaponDataBlock : global::WeaponDataBlock
{
	// Token: 0x06003531 RID: 13617 RVA: 0x000C3A48 File Offset: 0x000C1C48
	protected override global::IInventoryItem ConstructItem()
	{
		return new global::MeleeWeaponDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x06003532 RID: 13618 RVA: 0x000C3A50 File Offset: 0x000C1C50
	public override float GetDamage()
	{
		return Random.Range(this.damageMin, this.damageMax);
	}

	// Token: 0x06003533 RID: 13619 RVA: 0x000C3A64 File Offset: 0x000C1C64
	public virtual float GetRange()
	{
		return this.range;
	}

	// Token: 0x06003534 RID: 13620 RVA: 0x000C3A6C File Offset: 0x000C1C6C
	private void StartSwingWorldAnimations(global::ItemRepresentation itemRep)
	{
		if (!string.IsNullOrEmpty(this._swingingMovementAnimationGroupName) && this._swingingMovementAnimationGroupName != this.animationGroupName)
		{
			itemRep.OverrideAnimationGroupName(this._swingingMovementAnimationGroupName);
		}
		itemRep.PlayWorldAnimation(0, this.worldSwingAnimationSpeed);
	}

	// Token: 0x06003535 RID: 13621 RVA: 0x000C3ABC File Offset: 0x000C1CBC
	private void EndSwingWorldAnimations(global::ItemRepresentation itemRep)
	{
		if (!string.IsNullOrEmpty(this._swingingMovementAnimationGroupName) && this._swingingMovementAnimationGroupName != this.animationGroupName)
		{
			itemRep.OverrideAnimationGroupName(null);
		}
	}

	// Token: 0x06003536 RID: 13622 RVA: 0x000C3AF8 File Offset: 0x000C1CF8
	public virtual void Local_SecondaryFire(global::ViewModel vm, global::ItemRepresentation itemRep, global::IMeleeWeaponItem itemInstance, ref global::HumanController.InputSample sample)
	{
		global::Character character = itemInstance.character;
		if (character == null)
		{
			return;
		}
		Ray eyesRay = character.eyesRay;
		RaycastHit raycastHit;
		bool flag = Physics.SphereCast(eyesRay, 0.3f, ref raycastHit, this.GetRange(), 406721553);
		if (flag)
		{
			IDBase component = raycastHit.collider.gameObject.GetComponent<IDBase>();
			if (component)
			{
				global::NetEntityID netEntityID = global::NetEntityID.Get(component);
				global::RepairReceiver local = component.GetLocal<global::RepairReceiver>();
				if (local != null && netEntityID != global::NetEntityID.unassigned)
				{
					if (vm)
					{
						vm.PlayFireAnimation();
					}
					itemInstance.QueueSwingSound(Time.time + this.swingSoundDelay);
					itemRep.Action<global::NetEntityID>(2, 0, netEntityID);
				}
			}
		}
	}

	// Token: 0x06003537 RID: 13623 RVA: 0x000C3BBC File Offset: 0x000C1DBC
	public virtual void Local_FireWeapon(global::ViewModel vm, global::ItemRepresentation itemRep, global::IMeleeWeaponItem itemInstance, ref global::HumanController.InputSample sample)
	{
		this.StartSwingWorldAnimations(itemRep);
		if (vm)
		{
			vm.PlayFireAnimation();
		}
		itemInstance.QueueSwingSound(Time.time + this.swingSoundDelay);
		itemInstance.QueueMidSwing(Time.time + this.midSwingDelay);
		if (itemRep.networkViewIsMine)
		{
			itemRep.Action(3, 0);
		}
	}

	// Token: 0x06003538 RID: 13624 RVA: 0x000C3C18 File Offset: 0x000C1E18
	public virtual void SwingSound()
	{
		this.swingSound.PlayLocal(Camera.main.transform, Vector3.zero, 1f, Random.Range(0.85f, 1f), 3f, 20f, 0);
	}

	// Token: 0x06003539 RID: 13625 RVA: 0x000C3C60 File Offset: 0x000C1E60
	public bool Physics2SphereCast(Ray ray, float radius, float range, int hitMask, out Vector3 point, out Vector3 normal, out Collider hitCollider, out BodyPart part)
	{
		RaycastHit raycastHit = default(RaycastHit);
		bool flag = false;
		bool flag2 = false;
		RaycastHit raycastHit2;
		if (Physics.SphereCast(ray, radius, ref raycastHit2, range - radius, hitMask & -131073))
		{
			flag2 = true;
			raycastHit = raycastHit2;
			RaycastHit raycastHit3;
			if (Physics.Raycast(ray, ref raycastHit3, range, hitMask & -131073))
			{
				flag = true;
				if (raycastHit3.distance < raycastHit2.distance)
				{
					raycastHit = raycastHit3;
				}
			}
		}
		bool flag3 = flag2 || flag;
		RaycastHit2 raycastHit4;
		if (Physics2.Raycast2(ray, ref raycastHit4, range, hitMask) && (!flag3 || raycastHit4.distance < raycastHit.distance))
		{
			point = raycastHit4.point;
			normal = raycastHit4.normal;
			hitCollider = raycastHit4.collider;
			part = raycastHit4.bodyPart;
			return true;
		}
		if (!flag3)
		{
			Collider[] array = Physics.OverlapSphere(ray.origin, 0.3f, 131072);
			if (array.Length > 0)
			{
				point = ray.origin + ray.direction * 0.5f;
				normal = ray.direction * -1f;
				hitCollider = array[0];
				part = 0;
				return true;
			}
		}
		if (!flag3)
		{
			point = ray.origin + ray.direction * range;
			normal = ray.direction * -1f;
			hitCollider = null;
			part = 0;
			return false;
		}
		point = raycastHit.point;
		normal = raycastHit.normal;
		hitCollider = raycastHit.collider;
		part = 0;
		return true;
	}

	// Token: 0x0600353A RID: 13626 RVA: 0x000C3E1C File Offset: 0x000C201C
	public virtual void Local_MidSwing(global::ViewModel vm, global::ItemRepresentation itemRep, global::IMeleeWeaponItem itemInstance, ref global::HumanController.InputSample sample)
	{
		global::Character character = itemInstance.character;
		if (character == null)
		{
			return;
		}
		Ray eyesRay = character.eyesRay;
		Collider collider = null;
		Vector3 zero = Vector3.zero;
		Vector3 up = Vector3.up;
		global::NetEntityID netEntityID = global::NetEntityID.unassigned;
		bool flag = false;
		BodyPart bodyPart;
		bool flag2 = this.Physics2SphereCast(eyesRay, 0.3f, this.GetRange(), 406721553, out zero, out up, out collider, out bodyPart);
		bool flag3 = false;
		global::TakeDamage takeDamage = null;
		if (flag2)
		{
			IDBase idbase;
			global::TransformHelpers.GetIDBaseFromCollider(collider, out idbase);
			IDMain idmain = (!idbase) ? null : idbase.idMain;
			if (idmain)
			{
				netEntityID = global::NetEntityID.Get(idmain);
				flag = !netEntityID.isUnassigned;
				takeDamage = idmain.GetComponent<global::TakeDamage>();
				if (takeDamage && takeDamage.ShouldPlayHitNotification())
				{
					this.PlayHitNotification(zero, character);
				}
			}
			flag3 = collider.gameObject.CompareTag("Tree Collider");
			if (flag3)
			{
				global::WoodBlockerTemp blockerForPoint = global::WoodBlockerTemp.GetBlockerForPoint(zero);
				if (!blockerForPoint.HasWood())
				{
					flag3 = false;
					Rust.Notice.Popup("", "There's no wood left here", 2f);
				}
				else
				{
					blockerForPoint.ConsumeWood(this.efficiencies[2]);
				}
			}
			this.DoMeleeEffects(eyesRay.origin, zero, Quaternion.LookRotation(up), collider.gameObject);
			if (vm && (takeDamage || flag3))
			{
				vm.CrossFade("pull_out", 0.05f, 0, 1.1f);
			}
		}
		BitStream bitStream = new BitStream(false);
		if (flag)
		{
			bitStream.WriteBoolean(flag);
			bitStream.Write<global::NetEntityID>(netEntityID, new object[0]);
			bitStream.WriteVector3(zero);
		}
		else
		{
			bitStream.WriteBoolean(false);
			bitStream.WriteVector3(zero);
		}
		bitStream.WriteBoolean(flag3);
		itemRep.ActionStream(1, 0, bitStream);
		this.EndSwingWorldAnimations(itemRep);
	}

	// Token: 0x0600353B RID: 13627 RVA: 0x000C4004 File Offset: 0x000C2204
	public virtual void DoMeleeEffects(Vector3 fromPos, Vector3 pos, Quaternion rot, GameObject hitObj)
	{
		if (hitObj.CompareTag("Tree Collider"))
		{
			GameObject gameObject = Object.Instantiate(this.impactEffectWood, pos, rot) as GameObject;
			Object.Destroy(gameObject, 1.5f);
			this.impactSoundWood.Play(pos, 1f, 2f, 10f);
			return;
		}
		global::SurfaceInfo.DoImpact(hitObj, global::SurfaceInfoObject.ImpactType.Melee, pos, rot);
	}

	// Token: 0x0600353C RID: 13628 RVA: 0x000C4068 File Offset: 0x000C2268
	public override void DoAction2(BitStream stream, global::ItemRepresentation rep, ref uLink.NetworkMessageInfo info)
	{
		global::NetEntityID netEntityID = stream.Read<global::NetEntityID>(new object[0]);
		if (!netEntityID.isUnassigned)
		{
			IDBase idBase = netEntityID.idBase;
			if (!idBase)
			{
				return;
			}
		}
	}

	// Token: 0x0600353D RID: 13629 RVA: 0x000C40A4 File Offset: 0x000C22A4
	public override void DoAction3(BitStream stream, global::ItemRepresentation itemRep, ref uLink.NetworkMessageInfo info)
	{
		this.StartSwingWorldAnimations(itemRep);
	}

	// Token: 0x0600353E RID: 13630 RVA: 0x000C40B0 File Offset: 0x000C22B0
	public override void DoAction1(BitStream stream, global::ItemRepresentation rep, ref uLink.NetworkMessageInfo info)
	{
		bool flag = stream.ReadBoolean();
		GameObject gameObject;
		if (flag)
		{
			global::NetEntityID netEntityID = stream.Read<global::NetEntityID>(new object[0]);
			if (!netEntityID.isUnassigned)
			{
				gameObject = netEntityID.gameObject;
				if (!gameObject)
				{
					netEntityID = global::NetEntityID.unassigned;
				}
			}
			else
			{
				gameObject = null;
			}
		}
		else
		{
			global::NetEntityID netEntityID = global::NetEntityID.unassigned;
			gameObject = null;
		}
		Vector3 vector = stream.ReadVector3();
		bool flag2 = stream.ReadBoolean();
		this.EndSwingWorldAnimations(rep);
		if (gameObject)
		{
			Quaternion rot = Quaternion.LookRotation((rep.transform.position - vector).normalized);
			this.DoMeleeEffects(rep.transform.position, vector, rot, gameObject);
		}
	}

	// Token: 0x04001B92 RID: 7058
	public const int hitMask = 406721553;

	// Token: 0x04001B93 RID: 7059
	public float range = 2f;

	// Token: 0x04001B94 RID: 7060
	public GameObject impactEffect;

	// Token: 0x04001B95 RID: 7061
	public GameObject impactEffectFlesh;

	// Token: 0x04001B96 RID: 7062
	public GameObject impactEffectWood;

	// Token: 0x04001B97 RID: 7063
	public AudioClip impactSoundWood;

	// Token: 0x04001B98 RID: 7064
	public AudioClip swingSound;

	// Token: 0x04001B99 RID: 7065
	public float swingSoundDelay = 0.2f;

	// Token: 0x04001B9A RID: 7066
	public AudioClip impactSoundGeneric;

	// Token: 0x04001B9B RID: 7067
	public AudioClip[] impactSoundFlesh;

	// Token: 0x04001B9C RID: 7068
	public float midSwingDelay = 0.35f;

	// Token: 0x04001B9D RID: 7069
	public float gatherPerHitAmount = 0.25f;

	// Token: 0x04001B9E RID: 7070
	public bool gathersResources;

	// Token: 0x04001B9F RID: 7071
	public float caloriesPerSwing = 5f;

	// Token: 0x04001BA0 RID: 7072
	public float worldSwingAnimationSpeed = 1f;

	// Token: 0x04001BA1 RID: 7073
	[SerializeField]
	protected string _swingingMovementAnimationGroupName;

	// Token: 0x04001BA2 RID: 7074
	public float[] efficiencies;

	// Token: 0x02000640 RID: 1600
	private sealed class ITEM_TYPE : global::MeleeWeaponItem<global::MeleeWeaponDataBlock>, global::IHeldItem, global::IInventoryItem, global::IMeleeWeaponItem, global::IWeaponItem
	{
		// Token: 0x0600353F RID: 13631 RVA: 0x000C4168 File Offset: 0x000C2368
		public ITEM_TYPE(global::MeleeWeaponDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x06003540 RID: 13632 RVA: 0x000C4174 File Offset: 0x000C2374
		global::ItemDataBlock global::IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x06003541 RID: 13633 RVA: 0x000C417C File Offset: 0x000C237C
		float get_queuedSwingAttackTime()
		{
			return base.queuedSwingAttackTime;
		}

		// Token: 0x06003542 RID: 13634 RVA: 0x000C4184 File Offset: 0x000C2384
		void set_queuedSwingAttackTime(float value)
		{
			base.queuedSwingAttackTime = value;
		}

		// Token: 0x06003543 RID: 13635 RVA: 0x000C4190 File Offset: 0x000C2390
		float get_queuedSwingSoundTime()
		{
			return base.queuedSwingSoundTime;
		}

		// Token: 0x06003544 RID: 13636 RVA: 0x000C4198 File Offset: 0x000C2398
		void set_queuedSwingSoundTime(float value)
		{
			base.queuedSwingSoundTime = value;
		}

		// Token: 0x06003545 RID: 13637 RVA: 0x000C41A4 File Offset: 0x000C23A4
		bool ValidatePrimaryMessageTime(double timestamp)
		{
			return base.ValidatePrimaryMessageTime(timestamp);
		}

		// Token: 0x06003546 RID: 13638 RVA: 0x000C41B0 File Offset: 0x000C23B0
		bool get_canAim()
		{
			return base.canAim;
		}

		// Token: 0x06003547 RID: 13639 RVA: 0x000C41B8 File Offset: 0x000C23B8
		float get_nextPrimaryAttackTime()
		{
			return base.nextPrimaryAttackTime;
		}

		// Token: 0x06003548 RID: 13640 RVA: 0x000C41C0 File Offset: 0x000C23C0
		void set_nextPrimaryAttackTime(float value)
		{
			base.nextPrimaryAttackTime = value;
		}

		// Token: 0x06003549 RID: 13641 RVA: 0x000C41CC File Offset: 0x000C23CC
		float get_nextSecondaryAttackTime()
		{
			return base.nextSecondaryAttackTime;
		}

		// Token: 0x0600354A RID: 13642 RVA: 0x000C41D4 File Offset: 0x000C23D4
		void set_nextSecondaryAttackTime(float value)
		{
			base.nextSecondaryAttackTime = value;
		}

		// Token: 0x0600354B RID: 13643 RVA: 0x000C41E0 File Offset: 0x000C23E0
		float get_deployFinishedTime()
		{
			return base.deployFinishedTime;
		}

		// Token: 0x0600354C RID: 13644 RVA: 0x000C41E8 File Offset: 0x000C23E8
		void set_deployFinishedTime(float value)
		{
			base.deployFinishedTime = value;
		}

		// Token: 0x0600354D RID: 13645 RVA: 0x000C41F4 File Offset: 0x000C23F4
		void SetTotalModSlotCount(int count)
		{
			base.SetTotalModSlotCount(count);
		}

		// Token: 0x0600354E RID: 13646 RVA: 0x000C4200 File Offset: 0x000C2400
		void SetUsedModSlotCount(int count)
		{
			base.SetUsedModSlotCount(count);
		}

		// Token: 0x0600354F RID: 13647 RVA: 0x000C420C File Offset: 0x000C240C
		void AddMod(global::ItemModDataBlock mod)
		{
			base.AddMod(mod);
		}

		// Token: 0x06003550 RID: 13648 RVA: 0x000C4218 File Offset: 0x000C2418
		int FindMod(global::ItemModDataBlock mod)
		{
			return base.FindMod(mod);
		}

		// Token: 0x06003551 RID: 13649 RVA: 0x000C4224 File Offset: 0x000C2424
		void OnActivate()
		{
			base.OnActivate();
		}

		// Token: 0x06003552 RID: 13650 RVA: 0x000C422C File Offset: 0x000C242C
		void OnDeactivate()
		{
			base.OnDeactivate();
		}

		// Token: 0x06003553 RID: 13651 RVA: 0x000C4234 File Offset: 0x000C2434
		global::ViewModel get_viewModelInstance()
		{
			return base.viewModelInstance;
		}

		// Token: 0x06003554 RID: 13652 RVA: 0x000C423C File Offset: 0x000C243C
		global::ItemRepresentation get_itemRepresentation()
		{
			return base.itemRepresentation;
		}

		// Token: 0x06003555 RID: 13653 RVA: 0x000C4244 File Offset: 0x000C2444
		void set_itemRepresentation(global::ItemRepresentation value)
		{
			base.itemRepresentation = value;
		}

		// Token: 0x06003556 RID: 13654 RVA: 0x000C4250 File Offset: 0x000C2450
		bool get_canActivate()
		{
			return base.canActivate;
		}

		// Token: 0x06003557 RID: 13655 RVA: 0x000C4258 File Offset: 0x000C2458
		bool get_canDeactivate()
		{
			return base.canDeactivate;
		}

		// Token: 0x06003558 RID: 13656 RVA: 0x000C4260 File Offset: 0x000C2460
		global::ItemModFlags get_modFlags()
		{
			return base.modFlags;
		}

		// Token: 0x06003559 RID: 13657 RVA: 0x000C4268 File Offset: 0x000C2468
		global::ItemModDataBlock[] get_itemMods()
		{
			return base.itemMods;
		}

		// Token: 0x0600355A RID: 13658 RVA: 0x000C4270 File Offset: 0x000C2470
		int get_totalModSlots()
		{
			return base.totalModSlots;
		}

		// Token: 0x0600355B RID: 13659 RVA: 0x000C4278 File Offset: 0x000C2478
		int get_usedModSlots()
		{
			return base.usedModSlots;
		}

		// Token: 0x0600355C RID: 13660 RVA: 0x000C4280 File Offset: 0x000C2480
		int get_freeModSlots()
		{
			return base.freeModSlots;
		}

		// Token: 0x0600355D RID: 13661 RVA: 0x000C4288 File Offset: 0x000C2488
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x0600355E RID: 13662 RVA: 0x000C4290 File Offset: 0x000C2490
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x0600355F RID: 13663 RVA: 0x000C4298 File Offset: 0x000C2498
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x06003560 RID: 13664 RVA: 0x000C42A0 File Offset: 0x000C24A0
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x06003561 RID: 13665 RVA: 0x000C42AC File Offset: 0x000C24AC
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x06003562 RID: 13666 RVA: 0x000C42B8 File Offset: 0x000C24B8
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x06003563 RID: 13667 RVA: 0x000C42C4 File Offset: 0x000C24C4
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x06003564 RID: 13668 RVA: 0x000C42D0 File Offset: 0x000C24D0
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x06003565 RID: 13669 RVA: 0x000C42DC File Offset: 0x000C24DC
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x06003566 RID: 13670 RVA: 0x000C42E8 File Offset: 0x000C24E8
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x06003567 RID: 13671 RVA: 0x000C42F4 File Offset: 0x000C24F4
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x06003568 RID: 13672 RVA: 0x000C4300 File Offset: 0x000C2500
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x06003569 RID: 13673 RVA: 0x000C4308 File Offset: 0x000C2508
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x0600356A RID: 13674 RVA: 0x000C4310 File Offset: 0x000C2510
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x0600356B RID: 13675 RVA: 0x000C4318 File Offset: 0x000C2518
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x0600356C RID: 13676 RVA: 0x000C4320 File Offset: 0x000C2520
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x0600356D RID: 13677 RVA: 0x000C4328 File Offset: 0x000C2528
		global::Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x0600356E RID: 13678 RVA: 0x000C4330 File Offset: 0x000C2530
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x0600356F RID: 13679 RVA: 0x000C4338 File Offset: 0x000C2538
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x06003570 RID: 13680 RVA: 0x000C4340 File Offset: 0x000C2540
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x06003571 RID: 13681 RVA: 0x000C434C File Offset: 0x000C254C
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x06003572 RID: 13682 RVA: 0x000C4354 File Offset: 0x000C2554
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x06003573 RID: 13683 RVA: 0x000C435C File Offset: 0x000C255C
		global::Character get_character()
		{
			return base.character;
		}

		// Token: 0x06003574 RID: 13684 RVA: 0x000C4364 File Offset: 0x000C2564
		global::Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x06003575 RID: 13685 RVA: 0x000C436C File Offset: 0x000C256C
		global::Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x06003576 RID: 13686 RVA: 0x000C4374 File Offset: 0x000C2574
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x06003577 RID: 13687 RVA: 0x000C437C File Offset: 0x000C257C
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
