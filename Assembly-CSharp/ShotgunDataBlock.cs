using System;
using uLink;
using UnityEngine;

// Token: 0x0200064C RID: 1612
public class ShotgunDataBlock : global::BulletWeaponDataBlock
{
	// Token: 0x060035F6 RID: 13814 RVA: 0x000C4DB8 File Offset: 0x000C2FB8
	protected override global::IInventoryItem ConstructItem()
	{
		return new global::ShotgunDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x060035F7 RID: 13815 RVA: 0x000C4DC0 File Offset: 0x000C2FC0
	public override float GetGUIDamage()
	{
		return base.GetGUIDamage() * (float)this.numPellets;
	}

	// Token: 0x060035F8 RID: 13816 RVA: 0x000C4DD0 File Offset: 0x000C2FD0
	public virtual void FireSingleBullet(BitStream sendStream, Ray ray, global::ItemRepresentation itemRep, out Component hitComponent, out bool allowBlood)
	{
		global::NetEntityID hitView = global::NetEntityID.unassigned;
		bool flag = false;
		RaycastHit2 raycastHit;
		bool flag2 = Physics2.Raycast2(ray, ref raycastHit, this.GetBulletRange(itemRep), 406721553);
		if (flag2)
		{
			Vector3 point = raycastHit.point;
			IDBase id = raycastHit.id;
			hitComponent = ((!raycastHit.remoteBodyPart) ? raycastHit.collider : raycastHit.remoteBodyPart);
			IDMain idmain = (!id) ? null : id.idMain;
			if (idmain)
			{
				hitView = global::NetEntityID.Get(idmain);
				if (!hitView.isUnassigned)
				{
					global::TakeDamage component = idmain.GetComponent<global::TakeDamage>();
					if (component)
					{
						flag = true;
						if (component.ShouldPlayHitNotification())
						{
							this.PlayHitNotification(point, null);
						}
					}
				}
			}
		}
		else
		{
			Vector3 point = ray.GetPoint(this.GetBulletRange(itemRep));
			hitComponent = null;
		}
		this.WriteHitInfo(sendStream, ref ray, flag2, ref raycastHit, flag, hitView);
		allowBlood = (flag2 && flag);
	}

	// Token: 0x060035F9 RID: 13817 RVA: 0x000C4EDC File Offset: 0x000C30DC
	public override void Local_FireWeapon(global::ViewModel vm, global::ItemRepresentation itemRep, global::IBulletWeaponItem itemInstance, ref global::HumanController.InputSample sample)
	{
		global::Character character = itemInstance.character;
		if (character == null)
		{
			return;
		}
		if (itemInstance.clipAmmo <= 0)
		{
			return;
		}
		Ray eyesRay = character.eyesRay;
		int num = 1;
		itemInstance.Consume(ref num);
		BitStream bitStream = new BitStream(false);
		float bulletRange = this.GetBulletRange(itemRep);
		for (int i = 0; i < this.numPellets; i++)
		{
			Ray ray = eyesRay;
			ray.direction = Quaternion.LookRotation(eyesRay.direction) * Quaternion.Euler(Random.Range(-this.xSpread, this.xSpread), Random.Range(-this.ySpread, this.ySpread), 0f) * Vector3.forward;
			Component component = null;
			bool allowBlood;
			this.FireSingleBullet(bitStream, ray, itemRep, out component, out allowBlood);
			this.MakeTracer(ray.origin, ray.origin + ray.direction * bulletRange, bulletRange, component, allowBlood);
		}
		itemRep.ActionStream(1, 0, bitStream);
		bool flag = vm;
		global::Socket muzzle;
		if (flag)
		{
			muzzle = vm.muzzle;
		}
		else
		{
			muzzle = itemRep.muzzle;
		}
		this.DoWeaponEffects(character.transform, muzzle, flag, itemRep);
		if (flag)
		{
			vm.PlayFireAnimation();
		}
		float num2 = 1f;
		if (sample.aim)
		{
			num2 -= this.aimingRecoilSubtract;
		}
		else if (sample.crouch)
		{
			num2 -= this.crouchRecoilSubtract;
		}
		float pitch = Random.Range(this.recoilPitchMin, this.recoilPitchMax) * num2;
		float yaw = Random.Range(this.recoilYawMin, this.recoilYawMax) * num2;
		global::RecoilSimulation recoilSimulation = character.recoilSimulation;
		if (recoilSimulation)
		{
			recoilSimulation.AddRecoil(this.recoilDuration, pitch, yaw);
		}
		global::HeadBob component2 = global::CameraMount.current.GetComponent<global::HeadBob>();
		if (component2 && this.shotBob)
		{
			component2.AddEffect(this.shotBob);
		}
	}

	// Token: 0x060035FA RID: 13818 RVA: 0x000C50E4 File Offset: 0x000C32E4
	public virtual void MakeTracer(Vector3 startPos, Vector3 endPos, float range, Component component, bool allowBlood)
	{
		Vector3 vector = endPos - startPos;
		vector.Normalize();
		GameObject gameObject = (GameObject)Object.Instantiate(this.tracerPrefab, startPos, Quaternion.LookRotation(vector));
		global::Tracer component2 = gameObject.GetComponent<global::Tracer>();
		if (component2)
		{
			component2.Init(component, 406721553, range, allowBlood);
		}
	}

	// Token: 0x060035FB RID: 13819 RVA: 0x000C513C File Offset: 0x000C333C
	public virtual void DoWeaponEffects(Transform soundTransform, global::Socket muzzleSocket, bool firstPerson, global::ItemRepresentation itemRep)
	{
		this.PlayFireSound(soundTransform, firstPerson, itemRep);
		GameObject gameObject = muzzleSocket.InstantiateAsChild((!firstPerson) ? this.muzzleFlashWorld : this.muzzleflashVM, false);
		Object.Destroy(gameObject, 1f);
	}

	// Token: 0x060035FC RID: 13820 RVA: 0x000C5180 File Offset: 0x000C3380
	public override void DoAction1(BitStream stream, global::ItemRepresentation rep, ref uLink.NetworkMessageInfo info)
	{
		this.DoWeaponEffects(rep.transform.parent, rep.muzzle, false, rep);
		float bulletRange = this.GetBulletRange(rep);
		for (int i = 0; i < this.numPellets; i++)
		{
			GameObject gameObject;
			bool allowBlood;
			bool flag;
			BodyPart bodyPart;
			IDRemoteBodyPart idremoteBodyPart;
			global::NetEntityID netEntityID;
			Transform transform;
			Vector3 endPos;
			Vector3 vector;
			bool flag2;
			this.ReadHitInfo(stream, out gameObject, out allowBlood, out flag, out bodyPart, out idremoteBodyPart, out netEntityID, out transform, out endPos, out vector, out flag2);
			Component component = (!idremoteBodyPart) ? ((!gameObject) ? null : gameObject.GetComponentInChildren<CapsuleCollider>()) : idremoteBodyPart;
			this.MakeTracer(rep.muzzle.position, endPos, bulletRange, component, allowBlood);
		}
	}

	// Token: 0x060035FD RID: 13821 RVA: 0x000C5228 File Offset: 0x000C3428
	protected override void SecureWriteMemberValues(BitStream stream)
	{
		base.SecureWriteMemberValues(stream);
		stream.Write<float>(this.xSpread, new object[0]);
		stream.Write<int>(this.numPellets, new object[0]);
		stream.Write<float>(this.ySpread, new object[0]);
	}

	// Token: 0x04001BC6 RID: 7110
	public int numPellets = 6;

	// Token: 0x04001BC7 RID: 7111
	public float xSpread = 8f;

	// Token: 0x04001BC8 RID: 7112
	public float ySpread = 8f;

	// Token: 0x0200064D RID: 1613
	private sealed class ITEM_TYPE : global::BulletWeaponItem<global::ShotgunDataBlock>, global::IBulletWeaponItem, global::IHeldItem, global::IInventoryItem, global::IWeaponItem
	{
		// Token: 0x060035FE RID: 13822 RVA: 0x000C5274 File Offset: 0x000C3474
		public ITEM_TYPE(global::ShotgunDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x060035FF RID: 13823 RVA: 0x000C5280 File Offset: 0x000C3480
		global::ItemDataBlock global::IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x06003600 RID: 13824 RVA: 0x000C5288 File Offset: 0x000C3488
		global::MagazineDataBlock get_clipType()
		{
			return base.clipType;
		}

		// Token: 0x06003601 RID: 13825 RVA: 0x000C5290 File Offset: 0x000C3490
		int get_clipAmmo()
		{
			return base.clipAmmo;
		}

		// Token: 0x06003602 RID: 13826 RVA: 0x000C5298 File Offset: 0x000C3498
		void set_clipAmmo(int value)
		{
			base.clipAmmo = value;
		}

		// Token: 0x06003603 RID: 13827 RVA: 0x000C52A4 File Offset: 0x000C34A4
		int get_cachedCasings()
		{
			return base.cachedCasings;
		}

		// Token: 0x06003604 RID: 13828 RVA: 0x000C52AC File Offset: 0x000C34AC
		void set_cachedCasings(int value)
		{
			base.cachedCasings = value;
		}

		// Token: 0x06003605 RID: 13829 RVA: 0x000C52B8 File Offset: 0x000C34B8
		float get_nextCasingsTime()
		{
			return base.nextCasingsTime;
		}

		// Token: 0x06003606 RID: 13830 RVA: 0x000C52C0 File Offset: 0x000C34C0
		void set_nextCasingsTime(float value)
		{
			base.nextCasingsTime = value;
		}

		// Token: 0x06003607 RID: 13831 RVA: 0x000C52CC File Offset: 0x000C34CC
		bool ValidatePrimaryMessageTime(double timestamp)
		{
			return base.ValidatePrimaryMessageTime(timestamp);
		}

		// Token: 0x06003608 RID: 13832 RVA: 0x000C52D8 File Offset: 0x000C34D8
		bool get_canAim()
		{
			return base.canAim;
		}

		// Token: 0x06003609 RID: 13833 RVA: 0x000C52E0 File Offset: 0x000C34E0
		float get_nextPrimaryAttackTime()
		{
			return base.nextPrimaryAttackTime;
		}

		// Token: 0x0600360A RID: 13834 RVA: 0x000C52E8 File Offset: 0x000C34E8
		void set_nextPrimaryAttackTime(float value)
		{
			base.nextPrimaryAttackTime = value;
		}

		// Token: 0x0600360B RID: 13835 RVA: 0x000C52F4 File Offset: 0x000C34F4
		float get_nextSecondaryAttackTime()
		{
			return base.nextSecondaryAttackTime;
		}

		// Token: 0x0600360C RID: 13836 RVA: 0x000C52FC File Offset: 0x000C34FC
		void set_nextSecondaryAttackTime(float value)
		{
			base.nextSecondaryAttackTime = value;
		}

		// Token: 0x0600360D RID: 13837 RVA: 0x000C5308 File Offset: 0x000C3508
		float get_deployFinishedTime()
		{
			return base.deployFinishedTime;
		}

		// Token: 0x0600360E RID: 13838 RVA: 0x000C5310 File Offset: 0x000C3510
		void set_deployFinishedTime(float value)
		{
			base.deployFinishedTime = value;
		}

		// Token: 0x0600360F RID: 13839 RVA: 0x000C531C File Offset: 0x000C351C
		void SetTotalModSlotCount(int count)
		{
			base.SetTotalModSlotCount(count);
		}

		// Token: 0x06003610 RID: 13840 RVA: 0x000C5328 File Offset: 0x000C3528
		void SetUsedModSlotCount(int count)
		{
			base.SetUsedModSlotCount(count);
		}

		// Token: 0x06003611 RID: 13841 RVA: 0x000C5334 File Offset: 0x000C3534
		void AddMod(global::ItemModDataBlock mod)
		{
			base.AddMod(mod);
		}

		// Token: 0x06003612 RID: 13842 RVA: 0x000C5340 File Offset: 0x000C3540
		int FindMod(global::ItemModDataBlock mod)
		{
			return base.FindMod(mod);
		}

		// Token: 0x06003613 RID: 13843 RVA: 0x000C534C File Offset: 0x000C354C
		void OnActivate()
		{
			base.OnActivate();
		}

		// Token: 0x06003614 RID: 13844 RVA: 0x000C5354 File Offset: 0x000C3554
		void OnDeactivate()
		{
			base.OnDeactivate();
		}

		// Token: 0x06003615 RID: 13845 RVA: 0x000C535C File Offset: 0x000C355C
		global::ViewModel get_viewModelInstance()
		{
			return base.viewModelInstance;
		}

		// Token: 0x06003616 RID: 13846 RVA: 0x000C5364 File Offset: 0x000C3564
		global::ItemRepresentation get_itemRepresentation()
		{
			return base.itemRepresentation;
		}

		// Token: 0x06003617 RID: 13847 RVA: 0x000C536C File Offset: 0x000C356C
		void set_itemRepresentation(global::ItemRepresentation value)
		{
			base.itemRepresentation = value;
		}

		// Token: 0x06003618 RID: 13848 RVA: 0x000C5378 File Offset: 0x000C3578
		bool get_canActivate()
		{
			return base.canActivate;
		}

		// Token: 0x06003619 RID: 13849 RVA: 0x000C5380 File Offset: 0x000C3580
		bool get_canDeactivate()
		{
			return base.canDeactivate;
		}

		// Token: 0x0600361A RID: 13850 RVA: 0x000C5388 File Offset: 0x000C3588
		global::ItemModFlags get_modFlags()
		{
			return base.modFlags;
		}

		// Token: 0x0600361B RID: 13851 RVA: 0x000C5390 File Offset: 0x000C3590
		global::ItemModDataBlock[] get_itemMods()
		{
			return base.itemMods;
		}

		// Token: 0x0600361C RID: 13852 RVA: 0x000C5398 File Offset: 0x000C3598
		int get_totalModSlots()
		{
			return base.totalModSlots;
		}

		// Token: 0x0600361D RID: 13853 RVA: 0x000C53A0 File Offset: 0x000C35A0
		int get_usedModSlots()
		{
			return base.usedModSlots;
		}

		// Token: 0x0600361E RID: 13854 RVA: 0x000C53A8 File Offset: 0x000C35A8
		int get_freeModSlots()
		{
			return base.freeModSlots;
		}

		// Token: 0x0600361F RID: 13855 RVA: 0x000C53B0 File Offset: 0x000C35B0
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x06003620 RID: 13856 RVA: 0x000C53B8 File Offset: 0x000C35B8
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x06003621 RID: 13857 RVA: 0x000C53C0 File Offset: 0x000C35C0
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x06003622 RID: 13858 RVA: 0x000C53C8 File Offset: 0x000C35C8
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x06003623 RID: 13859 RVA: 0x000C53D4 File Offset: 0x000C35D4
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x06003624 RID: 13860 RVA: 0x000C53E0 File Offset: 0x000C35E0
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x06003625 RID: 13861 RVA: 0x000C53EC File Offset: 0x000C35EC
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x06003626 RID: 13862 RVA: 0x000C53F8 File Offset: 0x000C35F8
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x06003627 RID: 13863 RVA: 0x000C5404 File Offset: 0x000C3604
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x06003628 RID: 13864 RVA: 0x000C5410 File Offset: 0x000C3610
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x06003629 RID: 13865 RVA: 0x000C541C File Offset: 0x000C361C
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x0600362A RID: 13866 RVA: 0x000C5428 File Offset: 0x000C3628
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x0600362B RID: 13867 RVA: 0x000C5430 File Offset: 0x000C3630
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x0600362C RID: 13868 RVA: 0x000C5438 File Offset: 0x000C3638
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x0600362D RID: 13869 RVA: 0x000C5440 File Offset: 0x000C3640
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x0600362E RID: 13870 RVA: 0x000C5448 File Offset: 0x000C3648
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x0600362F RID: 13871 RVA: 0x000C5450 File Offset: 0x000C3650
		global::Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x06003630 RID: 13872 RVA: 0x000C5458 File Offset: 0x000C3658
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x06003631 RID: 13873 RVA: 0x000C5460 File Offset: 0x000C3660
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x06003632 RID: 13874 RVA: 0x000C5468 File Offset: 0x000C3668
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x06003633 RID: 13875 RVA: 0x000C5474 File Offset: 0x000C3674
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x06003634 RID: 13876 RVA: 0x000C547C File Offset: 0x000C367C
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x06003635 RID: 13877 RVA: 0x000C5484 File Offset: 0x000C3684
		global::Character get_character()
		{
			return base.character;
		}

		// Token: 0x06003636 RID: 13878 RVA: 0x000C548C File Offset: 0x000C368C
		global::Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x06003637 RID: 13879 RVA: 0x000C5494 File Offset: 0x000C3694
		global::Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x06003638 RID: 13880 RVA: 0x000C549C File Offset: 0x000C369C
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x06003639 RID: 13881 RVA: 0x000C54A4 File Offset: 0x000C36A4
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
