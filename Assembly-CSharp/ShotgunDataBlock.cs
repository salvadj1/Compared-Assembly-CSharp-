using System;
using uLink;
using UnityEngine;

// Token: 0x0200058E RID: 1422
public class ShotgunDataBlock : BulletWeaponDataBlock
{
	// Token: 0x0600322E RID: 12846 RVA: 0x000BCB5C File Offset: 0x000BAD5C
	protected override IInventoryItem ConstructItem()
	{
		return new ShotgunDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x0600322F RID: 12847 RVA: 0x000BCB64 File Offset: 0x000BAD64
	public override float GetGUIDamage()
	{
		return base.GetGUIDamage() * (float)this.numPellets;
	}

	// Token: 0x06003230 RID: 12848 RVA: 0x000BCB74 File Offset: 0x000BAD74
	public virtual void FireSingleBullet(BitStream sendStream, Ray ray, ItemRepresentation itemRep, out Component hitComponent, out bool allowBlood)
	{
		NetEntityID hitView = NetEntityID.unassigned;
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
				hitView = NetEntityID.Get(idmain);
				if (!hitView.isUnassigned)
				{
					TakeDamage component = idmain.GetComponent<TakeDamage>();
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

	// Token: 0x06003231 RID: 12849 RVA: 0x000BCC80 File Offset: 0x000BAE80
	public override void Local_FireWeapon(ViewModel vm, ItemRepresentation itemRep, IBulletWeaponItem itemInstance, ref HumanController.InputSample sample)
	{
		Character character = itemInstance.character;
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
		Socket muzzle;
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
		RecoilSimulation recoilSimulation = character.recoilSimulation;
		if (recoilSimulation)
		{
			recoilSimulation.AddRecoil(this.recoilDuration, pitch, yaw);
		}
		HeadBob component2 = CameraMount.current.GetComponent<HeadBob>();
		if (component2 && this.shotBob)
		{
			component2.AddEffect(this.shotBob);
		}
	}

	// Token: 0x06003232 RID: 12850 RVA: 0x000BCE88 File Offset: 0x000BB088
	public virtual void MakeTracer(Vector3 startPos, Vector3 endPos, float range, Component component, bool allowBlood)
	{
		Vector3 vector = endPos - startPos;
		vector.Normalize();
		GameObject gameObject = (GameObject)Object.Instantiate(this.tracerPrefab, startPos, Quaternion.LookRotation(vector));
		Tracer component2 = gameObject.GetComponent<Tracer>();
		if (component2)
		{
			component2.Init(component, 406721553, range, allowBlood);
		}
	}

	// Token: 0x06003233 RID: 12851 RVA: 0x000BCEE0 File Offset: 0x000BB0E0
	public virtual void DoWeaponEffects(Transform soundTransform, Socket muzzleSocket, bool firstPerson, ItemRepresentation itemRep)
	{
		this.PlayFireSound(soundTransform, firstPerson, itemRep);
		GameObject gameObject = muzzleSocket.InstantiateAsChild((!firstPerson) ? this.muzzleFlashWorld : this.muzzleflashVM, false);
		Object.Destroy(gameObject, 1f);
	}

	// Token: 0x06003234 RID: 12852 RVA: 0x000BCF24 File Offset: 0x000BB124
	public override void DoAction1(BitStream stream, ItemRepresentation rep, ref NetworkMessageInfo info)
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
			NetEntityID netEntityID;
			Transform transform;
			Vector3 endPos;
			Vector3 vector;
			bool flag2;
			this.ReadHitInfo(stream, out gameObject, out allowBlood, out flag, out bodyPart, out idremoteBodyPart, out netEntityID, out transform, out endPos, out vector, out flag2);
			Component component = (!idremoteBodyPart) ? ((!gameObject) ? null : gameObject.GetComponentInChildren<CapsuleCollider>()) : idremoteBodyPart;
			this.MakeTracer(rep.muzzle.position, endPos, bulletRange, component, allowBlood);
		}
	}

	// Token: 0x06003235 RID: 12853 RVA: 0x000BCFCC File Offset: 0x000BB1CC
	protected override void SecureWriteMemberValues(BitStream stream)
	{
		base.SecureWriteMemberValues(stream);
		stream.Write<float>(this.xSpread, new object[0]);
		stream.Write<int>(this.numPellets, new object[0]);
		stream.Write<float>(this.ySpread, new object[0]);
	}

	// Token: 0x040019F5 RID: 6645
	public int numPellets = 6;

	// Token: 0x040019F6 RID: 6646
	public float xSpread = 8f;

	// Token: 0x040019F7 RID: 6647
	public float ySpread = 8f;

	// Token: 0x0200058F RID: 1423
	private sealed class ITEM_TYPE : BulletWeaponItem<ShotgunDataBlock>, IBulletWeaponItem, IHeldItem, IInventoryItem, IWeaponItem
	{
		// Token: 0x06003236 RID: 12854 RVA: 0x000BD018 File Offset: 0x000BB218
		public ITEM_TYPE(ShotgunDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06003237 RID: 12855 RVA: 0x000BD024 File Offset: 0x000BB224
		ItemDataBlock IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x06003238 RID: 12856 RVA: 0x000BD02C File Offset: 0x000BB22C
		MagazineDataBlock get_clipType()
		{
			return base.clipType;
		}

		// Token: 0x06003239 RID: 12857 RVA: 0x000BD034 File Offset: 0x000BB234
		int get_clipAmmo()
		{
			return base.clipAmmo;
		}

		// Token: 0x0600323A RID: 12858 RVA: 0x000BD03C File Offset: 0x000BB23C
		void set_clipAmmo(int value)
		{
			base.clipAmmo = value;
		}

		// Token: 0x0600323B RID: 12859 RVA: 0x000BD048 File Offset: 0x000BB248
		int get_cachedCasings()
		{
			return base.cachedCasings;
		}

		// Token: 0x0600323C RID: 12860 RVA: 0x000BD050 File Offset: 0x000BB250
		void set_cachedCasings(int value)
		{
			base.cachedCasings = value;
		}

		// Token: 0x0600323D RID: 12861 RVA: 0x000BD05C File Offset: 0x000BB25C
		float get_nextCasingsTime()
		{
			return base.nextCasingsTime;
		}

		// Token: 0x0600323E RID: 12862 RVA: 0x000BD064 File Offset: 0x000BB264
		void set_nextCasingsTime(float value)
		{
			base.nextCasingsTime = value;
		}

		// Token: 0x0600323F RID: 12863 RVA: 0x000BD070 File Offset: 0x000BB270
		bool ValidatePrimaryMessageTime(double timestamp)
		{
			return base.ValidatePrimaryMessageTime(timestamp);
		}

		// Token: 0x06003240 RID: 12864 RVA: 0x000BD07C File Offset: 0x000BB27C
		bool get_canAim()
		{
			return base.canAim;
		}

		// Token: 0x06003241 RID: 12865 RVA: 0x000BD084 File Offset: 0x000BB284
		float get_nextPrimaryAttackTime()
		{
			return base.nextPrimaryAttackTime;
		}

		// Token: 0x06003242 RID: 12866 RVA: 0x000BD08C File Offset: 0x000BB28C
		void set_nextPrimaryAttackTime(float value)
		{
			base.nextPrimaryAttackTime = value;
		}

		// Token: 0x06003243 RID: 12867 RVA: 0x000BD098 File Offset: 0x000BB298
		float get_nextSecondaryAttackTime()
		{
			return base.nextSecondaryAttackTime;
		}

		// Token: 0x06003244 RID: 12868 RVA: 0x000BD0A0 File Offset: 0x000BB2A0
		void set_nextSecondaryAttackTime(float value)
		{
			base.nextSecondaryAttackTime = value;
		}

		// Token: 0x06003245 RID: 12869 RVA: 0x000BD0AC File Offset: 0x000BB2AC
		float get_deployFinishedTime()
		{
			return base.deployFinishedTime;
		}

		// Token: 0x06003246 RID: 12870 RVA: 0x000BD0B4 File Offset: 0x000BB2B4
		void set_deployFinishedTime(float value)
		{
			base.deployFinishedTime = value;
		}

		// Token: 0x06003247 RID: 12871 RVA: 0x000BD0C0 File Offset: 0x000BB2C0
		void SetTotalModSlotCount(int count)
		{
			base.SetTotalModSlotCount(count);
		}

		// Token: 0x06003248 RID: 12872 RVA: 0x000BD0CC File Offset: 0x000BB2CC
		void SetUsedModSlotCount(int count)
		{
			base.SetUsedModSlotCount(count);
		}

		// Token: 0x06003249 RID: 12873 RVA: 0x000BD0D8 File Offset: 0x000BB2D8
		void AddMod(ItemModDataBlock mod)
		{
			base.AddMod(mod);
		}

		// Token: 0x0600324A RID: 12874 RVA: 0x000BD0E4 File Offset: 0x000BB2E4
		int FindMod(ItemModDataBlock mod)
		{
			return base.FindMod(mod);
		}

		// Token: 0x0600324B RID: 12875 RVA: 0x000BD0F0 File Offset: 0x000BB2F0
		void OnActivate()
		{
			base.OnActivate();
		}

		// Token: 0x0600324C RID: 12876 RVA: 0x000BD0F8 File Offset: 0x000BB2F8
		void OnDeactivate()
		{
			base.OnDeactivate();
		}

		// Token: 0x0600324D RID: 12877 RVA: 0x000BD100 File Offset: 0x000BB300
		ViewModel get_viewModelInstance()
		{
			return base.viewModelInstance;
		}

		// Token: 0x0600324E RID: 12878 RVA: 0x000BD108 File Offset: 0x000BB308
		ItemRepresentation get_itemRepresentation()
		{
			return base.itemRepresentation;
		}

		// Token: 0x0600324F RID: 12879 RVA: 0x000BD110 File Offset: 0x000BB310
		void set_itemRepresentation(ItemRepresentation value)
		{
			base.itemRepresentation = value;
		}

		// Token: 0x06003250 RID: 12880 RVA: 0x000BD11C File Offset: 0x000BB31C
		bool get_canActivate()
		{
			return base.canActivate;
		}

		// Token: 0x06003251 RID: 12881 RVA: 0x000BD124 File Offset: 0x000BB324
		bool get_canDeactivate()
		{
			return base.canDeactivate;
		}

		// Token: 0x06003252 RID: 12882 RVA: 0x000BD12C File Offset: 0x000BB32C
		ItemModFlags get_modFlags()
		{
			return base.modFlags;
		}

		// Token: 0x06003253 RID: 12883 RVA: 0x000BD134 File Offset: 0x000BB334
		ItemModDataBlock[] get_itemMods()
		{
			return base.itemMods;
		}

		// Token: 0x06003254 RID: 12884 RVA: 0x000BD13C File Offset: 0x000BB33C
		int get_totalModSlots()
		{
			return base.totalModSlots;
		}

		// Token: 0x06003255 RID: 12885 RVA: 0x000BD144 File Offset: 0x000BB344
		int get_usedModSlots()
		{
			return base.usedModSlots;
		}

		// Token: 0x06003256 RID: 12886 RVA: 0x000BD14C File Offset: 0x000BB34C
		int get_freeModSlots()
		{
			return base.freeModSlots;
		}

		// Token: 0x06003257 RID: 12887 RVA: 0x000BD154 File Offset: 0x000BB354
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x06003258 RID: 12888 RVA: 0x000BD15C File Offset: 0x000BB35C
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x06003259 RID: 12889 RVA: 0x000BD164 File Offset: 0x000BB364
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x0600325A RID: 12890 RVA: 0x000BD16C File Offset: 0x000BB36C
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x0600325B RID: 12891 RVA: 0x000BD178 File Offset: 0x000BB378
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x0600325C RID: 12892 RVA: 0x000BD184 File Offset: 0x000BB384
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x0600325D RID: 12893 RVA: 0x000BD190 File Offset: 0x000BB390
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x0600325E RID: 12894 RVA: 0x000BD19C File Offset: 0x000BB39C
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x0600325F RID: 12895 RVA: 0x000BD1A8 File Offset: 0x000BB3A8
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x06003260 RID: 12896 RVA: 0x000BD1B4 File Offset: 0x000BB3B4
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x06003261 RID: 12897 RVA: 0x000BD1C0 File Offset: 0x000BB3C0
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x06003262 RID: 12898 RVA: 0x000BD1CC File Offset: 0x000BB3CC
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x06003263 RID: 12899 RVA: 0x000BD1D4 File Offset: 0x000BB3D4
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x06003264 RID: 12900 RVA: 0x000BD1DC File Offset: 0x000BB3DC
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x06003265 RID: 12901 RVA: 0x000BD1E4 File Offset: 0x000BB3E4
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x06003266 RID: 12902 RVA: 0x000BD1EC File Offset: 0x000BB3EC
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x06003267 RID: 12903 RVA: 0x000BD1F4 File Offset: 0x000BB3F4
		Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x06003268 RID: 12904 RVA: 0x000BD1FC File Offset: 0x000BB3FC
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x06003269 RID: 12905 RVA: 0x000BD204 File Offset: 0x000BB404
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x0600326A RID: 12906 RVA: 0x000BD20C File Offset: 0x000BB40C
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x0600326B RID: 12907 RVA: 0x000BD218 File Offset: 0x000BB418
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x0600326C RID: 12908 RVA: 0x000BD220 File Offset: 0x000BB420
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x0600326D RID: 12909 RVA: 0x000BD228 File Offset: 0x000BB428
		Character get_character()
		{
			return base.character;
		}

		// Token: 0x0600326E RID: 12910 RVA: 0x000BD230 File Offset: 0x000BB430
		Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x0600326F RID: 12911 RVA: 0x000BD238 File Offset: 0x000BB438
		Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x06003270 RID: 12912 RVA: 0x000BD240 File Offset: 0x000BB440
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x06003271 RID: 12913 RVA: 0x000BD248 File Offset: 0x000BB448
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
