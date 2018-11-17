using System;
using Rust;
using uLink;
using UnityEngine;

// Token: 0x02000581 RID: 1409
public class MeleeWeaponDataBlock : WeaponDataBlock
{
	// Token: 0x06003169 RID: 12649 RVA: 0x000BB7EC File Offset: 0x000B99EC
	protected override IInventoryItem ConstructItem()
	{
		return new MeleeWeaponDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x0600316A RID: 12650 RVA: 0x000BB7F4 File Offset: 0x000B99F4
	public override float GetDamage()
	{
		return Random.Range(this.damageMin, this.damageMax);
	}

	// Token: 0x0600316B RID: 12651 RVA: 0x000BB808 File Offset: 0x000B9A08
	public virtual float GetRange()
	{
		return this.range;
	}

	// Token: 0x0600316C RID: 12652 RVA: 0x000BB810 File Offset: 0x000B9A10
	private void StartSwingWorldAnimations(ItemRepresentation itemRep)
	{
		if (!string.IsNullOrEmpty(this._swingingMovementAnimationGroupName) && this._swingingMovementAnimationGroupName != this.animationGroupName)
		{
			itemRep.OverrideAnimationGroupName(this._swingingMovementAnimationGroupName);
		}
		itemRep.PlayWorldAnimation(0, this.worldSwingAnimationSpeed);
	}

	// Token: 0x0600316D RID: 12653 RVA: 0x000BB860 File Offset: 0x000B9A60
	private void EndSwingWorldAnimations(ItemRepresentation itemRep)
	{
		if (!string.IsNullOrEmpty(this._swingingMovementAnimationGroupName) && this._swingingMovementAnimationGroupName != this.animationGroupName)
		{
			itemRep.OverrideAnimationGroupName(null);
		}
	}

	// Token: 0x0600316E RID: 12654 RVA: 0x000BB89C File Offset: 0x000B9A9C
	public virtual void Local_SecondaryFire(ViewModel vm, ItemRepresentation itemRep, IMeleeWeaponItem itemInstance, ref HumanController.InputSample sample)
	{
		Character character = itemInstance.character;
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
				NetEntityID netEntityID = NetEntityID.Get(component);
				RepairReceiver local = component.GetLocal<RepairReceiver>();
				if (local != null && netEntityID != NetEntityID.unassigned)
				{
					if (vm)
					{
						vm.PlayFireAnimation();
					}
					itemInstance.QueueSwingSound(Time.time + this.swingSoundDelay);
					itemRep.Action<NetEntityID>(2, 0, netEntityID);
				}
			}
		}
	}

	// Token: 0x0600316F RID: 12655 RVA: 0x000BB960 File Offset: 0x000B9B60
	public virtual void Local_FireWeapon(ViewModel vm, ItemRepresentation itemRep, IMeleeWeaponItem itemInstance, ref HumanController.InputSample sample)
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

	// Token: 0x06003170 RID: 12656 RVA: 0x000BB9BC File Offset: 0x000B9BBC
	public virtual void SwingSound()
	{
		this.swingSound.PlayLocal(Camera.main.transform, Vector3.zero, 1f, Random.Range(0.85f, 1f), 3f, 20f, 0);
	}

	// Token: 0x06003171 RID: 12657 RVA: 0x000BBA04 File Offset: 0x000B9C04
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

	// Token: 0x06003172 RID: 12658 RVA: 0x000BBBC0 File Offset: 0x000B9DC0
	public virtual void Local_MidSwing(ViewModel vm, ItemRepresentation itemRep, IMeleeWeaponItem itemInstance, ref HumanController.InputSample sample)
	{
		Character character = itemInstance.character;
		if (character == null)
		{
			return;
		}
		Ray eyesRay = character.eyesRay;
		Collider collider = null;
		Vector3 zero = Vector3.zero;
		Vector3 up = Vector3.up;
		NetEntityID netEntityID = NetEntityID.unassigned;
		bool flag = false;
		BodyPart bodyPart;
		bool flag2 = this.Physics2SphereCast(eyesRay, 0.3f, this.GetRange(), 406721553, out zero, out up, out collider, out bodyPart);
		bool flag3 = false;
		TakeDamage takeDamage = null;
		if (flag2)
		{
			IDBase idbase;
			TransformHelpers.GetIDBaseFromCollider(collider, out idbase);
			IDMain idmain = (!idbase) ? null : idbase.idMain;
			if (idmain)
			{
				netEntityID = NetEntityID.Get(idmain);
				flag = !netEntityID.isUnassigned;
				takeDamage = idmain.GetComponent<TakeDamage>();
				if (takeDamage && takeDamage.ShouldPlayHitNotification())
				{
					this.PlayHitNotification(zero, character);
				}
			}
			flag3 = collider.gameObject.CompareTag("Tree Collider");
			if (flag3)
			{
				WoodBlockerTemp blockerForPoint = WoodBlockerTemp.GetBlockerForPoint(zero);
				if (!blockerForPoint.HasWood())
				{
					flag3 = false;
					Notice.Popup("", "There's no wood left here", 2f);
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
			bitStream.Write<NetEntityID>(netEntityID, new object[0]);
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

	// Token: 0x06003173 RID: 12659 RVA: 0x000BBDA8 File Offset: 0x000B9FA8
	public virtual void DoMeleeEffects(Vector3 fromPos, Vector3 pos, Quaternion rot, GameObject hitObj)
	{
		if (hitObj.CompareTag("Tree Collider"))
		{
			GameObject gameObject = Object.Instantiate(this.impactEffectWood, pos, rot) as GameObject;
			Object.Destroy(gameObject, 1.5f);
			this.impactSoundWood.Play(pos, 1f, 2f, 10f);
			return;
		}
		SurfaceInfo.DoImpact(hitObj, SurfaceInfoObject.ImpactType.Melee, pos, rot);
	}

	// Token: 0x06003174 RID: 12660 RVA: 0x000BBE0C File Offset: 0x000BA00C
	public override void DoAction2(BitStream stream, ItemRepresentation rep, ref NetworkMessageInfo info)
	{
		NetEntityID netEntityID = stream.Read<NetEntityID>(new object[0]);
		if (!netEntityID.isUnassigned)
		{
			IDBase idBase = netEntityID.idBase;
			if (!idBase)
			{
				return;
			}
		}
	}

	// Token: 0x06003175 RID: 12661 RVA: 0x000BBE48 File Offset: 0x000BA048
	public override void DoAction3(BitStream stream, ItemRepresentation itemRep, ref NetworkMessageInfo info)
	{
		this.StartSwingWorldAnimations(itemRep);
	}

	// Token: 0x06003176 RID: 12662 RVA: 0x000BBE54 File Offset: 0x000BA054
	public override void DoAction1(BitStream stream, ItemRepresentation rep, ref NetworkMessageInfo info)
	{
		bool flag = stream.ReadBoolean();
		GameObject gameObject;
		if (flag)
		{
			NetEntityID netEntityID = stream.Read<NetEntityID>(new object[0]);
			if (!netEntityID.isUnassigned)
			{
				gameObject = netEntityID.gameObject;
				if (!gameObject)
				{
					netEntityID = NetEntityID.unassigned;
				}
			}
			else
			{
				gameObject = null;
			}
		}
		else
		{
			NetEntityID netEntityID = NetEntityID.unassigned;
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

	// Token: 0x040019C1 RID: 6593
	public const int hitMask = 406721553;

	// Token: 0x040019C2 RID: 6594
	public float range = 2f;

	// Token: 0x040019C3 RID: 6595
	public GameObject impactEffect;

	// Token: 0x040019C4 RID: 6596
	public GameObject impactEffectFlesh;

	// Token: 0x040019C5 RID: 6597
	public GameObject impactEffectWood;

	// Token: 0x040019C6 RID: 6598
	public AudioClip impactSoundWood;

	// Token: 0x040019C7 RID: 6599
	public AudioClip swingSound;

	// Token: 0x040019C8 RID: 6600
	public float swingSoundDelay = 0.2f;

	// Token: 0x040019C9 RID: 6601
	public AudioClip impactSoundGeneric;

	// Token: 0x040019CA RID: 6602
	public AudioClip[] impactSoundFlesh;

	// Token: 0x040019CB RID: 6603
	public float midSwingDelay = 0.35f;

	// Token: 0x040019CC RID: 6604
	public float gatherPerHitAmount = 0.25f;

	// Token: 0x040019CD RID: 6605
	public bool gathersResources;

	// Token: 0x040019CE RID: 6606
	public float caloriesPerSwing = 5f;

	// Token: 0x040019CF RID: 6607
	public float worldSwingAnimationSpeed = 1f;

	// Token: 0x040019D0 RID: 6608
	[SerializeField]
	protected string _swingingMovementAnimationGroupName;

	// Token: 0x040019D1 RID: 6609
	public float[] efficiencies;

	// Token: 0x02000582 RID: 1410
	private sealed class ITEM_TYPE : MeleeWeaponItem<MeleeWeaponDataBlock>, IHeldItem, IInventoryItem, IMeleeWeaponItem, IWeaponItem
	{
		// Token: 0x06003177 RID: 12663 RVA: 0x000BBF0C File Offset: 0x000BA10C
		public ITEM_TYPE(MeleeWeaponDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x06003178 RID: 12664 RVA: 0x000BBF18 File Offset: 0x000BA118
		ItemDataBlock IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x06003179 RID: 12665 RVA: 0x000BBF20 File Offset: 0x000BA120
		float get_queuedSwingAttackTime()
		{
			return base.queuedSwingAttackTime;
		}

		// Token: 0x0600317A RID: 12666 RVA: 0x000BBF28 File Offset: 0x000BA128
		void set_queuedSwingAttackTime(float value)
		{
			base.queuedSwingAttackTime = value;
		}

		// Token: 0x0600317B RID: 12667 RVA: 0x000BBF34 File Offset: 0x000BA134
		float get_queuedSwingSoundTime()
		{
			return base.queuedSwingSoundTime;
		}

		// Token: 0x0600317C RID: 12668 RVA: 0x000BBF3C File Offset: 0x000BA13C
		void set_queuedSwingSoundTime(float value)
		{
			base.queuedSwingSoundTime = value;
		}

		// Token: 0x0600317D RID: 12669 RVA: 0x000BBF48 File Offset: 0x000BA148
		bool ValidatePrimaryMessageTime(double timestamp)
		{
			return base.ValidatePrimaryMessageTime(timestamp);
		}

		// Token: 0x0600317E RID: 12670 RVA: 0x000BBF54 File Offset: 0x000BA154
		bool get_canAim()
		{
			return base.canAim;
		}

		// Token: 0x0600317F RID: 12671 RVA: 0x000BBF5C File Offset: 0x000BA15C
		float get_nextPrimaryAttackTime()
		{
			return base.nextPrimaryAttackTime;
		}

		// Token: 0x06003180 RID: 12672 RVA: 0x000BBF64 File Offset: 0x000BA164
		void set_nextPrimaryAttackTime(float value)
		{
			base.nextPrimaryAttackTime = value;
		}

		// Token: 0x06003181 RID: 12673 RVA: 0x000BBF70 File Offset: 0x000BA170
		float get_nextSecondaryAttackTime()
		{
			return base.nextSecondaryAttackTime;
		}

		// Token: 0x06003182 RID: 12674 RVA: 0x000BBF78 File Offset: 0x000BA178
		void set_nextSecondaryAttackTime(float value)
		{
			base.nextSecondaryAttackTime = value;
		}

		// Token: 0x06003183 RID: 12675 RVA: 0x000BBF84 File Offset: 0x000BA184
		float get_deployFinishedTime()
		{
			return base.deployFinishedTime;
		}

		// Token: 0x06003184 RID: 12676 RVA: 0x000BBF8C File Offset: 0x000BA18C
		void set_deployFinishedTime(float value)
		{
			base.deployFinishedTime = value;
		}

		// Token: 0x06003185 RID: 12677 RVA: 0x000BBF98 File Offset: 0x000BA198
		void SetTotalModSlotCount(int count)
		{
			base.SetTotalModSlotCount(count);
		}

		// Token: 0x06003186 RID: 12678 RVA: 0x000BBFA4 File Offset: 0x000BA1A4
		void SetUsedModSlotCount(int count)
		{
			base.SetUsedModSlotCount(count);
		}

		// Token: 0x06003187 RID: 12679 RVA: 0x000BBFB0 File Offset: 0x000BA1B0
		void AddMod(ItemModDataBlock mod)
		{
			base.AddMod(mod);
		}

		// Token: 0x06003188 RID: 12680 RVA: 0x000BBFBC File Offset: 0x000BA1BC
		int FindMod(ItemModDataBlock mod)
		{
			return base.FindMod(mod);
		}

		// Token: 0x06003189 RID: 12681 RVA: 0x000BBFC8 File Offset: 0x000BA1C8
		void OnActivate()
		{
			base.OnActivate();
		}

		// Token: 0x0600318A RID: 12682 RVA: 0x000BBFD0 File Offset: 0x000BA1D0
		void OnDeactivate()
		{
			base.OnDeactivate();
		}

		// Token: 0x0600318B RID: 12683 RVA: 0x000BBFD8 File Offset: 0x000BA1D8
		ViewModel get_viewModelInstance()
		{
			return base.viewModelInstance;
		}

		// Token: 0x0600318C RID: 12684 RVA: 0x000BBFE0 File Offset: 0x000BA1E0
		ItemRepresentation get_itemRepresentation()
		{
			return base.itemRepresentation;
		}

		// Token: 0x0600318D RID: 12685 RVA: 0x000BBFE8 File Offset: 0x000BA1E8
		void set_itemRepresentation(ItemRepresentation value)
		{
			base.itemRepresentation = value;
		}

		// Token: 0x0600318E RID: 12686 RVA: 0x000BBFF4 File Offset: 0x000BA1F4
		bool get_canActivate()
		{
			return base.canActivate;
		}

		// Token: 0x0600318F RID: 12687 RVA: 0x000BBFFC File Offset: 0x000BA1FC
		bool get_canDeactivate()
		{
			return base.canDeactivate;
		}

		// Token: 0x06003190 RID: 12688 RVA: 0x000BC004 File Offset: 0x000BA204
		ItemModFlags get_modFlags()
		{
			return base.modFlags;
		}

		// Token: 0x06003191 RID: 12689 RVA: 0x000BC00C File Offset: 0x000BA20C
		ItemModDataBlock[] get_itemMods()
		{
			return base.itemMods;
		}

		// Token: 0x06003192 RID: 12690 RVA: 0x000BC014 File Offset: 0x000BA214
		int get_totalModSlots()
		{
			return base.totalModSlots;
		}

		// Token: 0x06003193 RID: 12691 RVA: 0x000BC01C File Offset: 0x000BA21C
		int get_usedModSlots()
		{
			return base.usedModSlots;
		}

		// Token: 0x06003194 RID: 12692 RVA: 0x000BC024 File Offset: 0x000BA224
		int get_freeModSlots()
		{
			return base.freeModSlots;
		}

		// Token: 0x06003195 RID: 12693 RVA: 0x000BC02C File Offset: 0x000BA22C
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x06003196 RID: 12694 RVA: 0x000BC034 File Offset: 0x000BA234
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x06003197 RID: 12695 RVA: 0x000BC03C File Offset: 0x000BA23C
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x06003198 RID: 12696 RVA: 0x000BC044 File Offset: 0x000BA244
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x06003199 RID: 12697 RVA: 0x000BC050 File Offset: 0x000BA250
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x0600319A RID: 12698 RVA: 0x000BC05C File Offset: 0x000BA25C
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x0600319B RID: 12699 RVA: 0x000BC068 File Offset: 0x000BA268
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x0600319C RID: 12700 RVA: 0x000BC074 File Offset: 0x000BA274
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x0600319D RID: 12701 RVA: 0x000BC080 File Offset: 0x000BA280
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x0600319E RID: 12702 RVA: 0x000BC08C File Offset: 0x000BA28C
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x0600319F RID: 12703 RVA: 0x000BC098 File Offset: 0x000BA298
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x060031A0 RID: 12704 RVA: 0x000BC0A4 File Offset: 0x000BA2A4
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x060031A1 RID: 12705 RVA: 0x000BC0AC File Offset: 0x000BA2AC
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x060031A2 RID: 12706 RVA: 0x000BC0B4 File Offset: 0x000BA2B4
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x060031A3 RID: 12707 RVA: 0x000BC0BC File Offset: 0x000BA2BC
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x060031A4 RID: 12708 RVA: 0x000BC0C4 File Offset: 0x000BA2C4
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x060031A5 RID: 12709 RVA: 0x000BC0CC File Offset: 0x000BA2CC
		Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x060031A6 RID: 12710 RVA: 0x000BC0D4 File Offset: 0x000BA2D4
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x060031A7 RID: 12711 RVA: 0x000BC0DC File Offset: 0x000BA2DC
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x060031A8 RID: 12712 RVA: 0x000BC0E4 File Offset: 0x000BA2E4
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x060031A9 RID: 12713 RVA: 0x000BC0F0 File Offset: 0x000BA2F0
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x060031AA RID: 12714 RVA: 0x000BC0F8 File Offset: 0x000BA2F8
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x060031AB RID: 12715 RVA: 0x000BC100 File Offset: 0x000BA300
		Character get_character()
		{
			return base.character;
		}

		// Token: 0x060031AC RID: 12716 RVA: 0x000BC108 File Offset: 0x000BA308
		Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x060031AD RID: 12717 RVA: 0x000BC110 File Offset: 0x000BA310
		Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x060031AE RID: 12718 RVA: 0x000BC118 File Offset: 0x000BA318
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x060031AF RID: 12719 RVA: 0x000BC120 File Offset: 0x000BA320
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
