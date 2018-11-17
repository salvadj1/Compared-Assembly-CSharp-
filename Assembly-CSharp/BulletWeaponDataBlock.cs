using System;
using uLink;
using UnityEngine;

// Token: 0x0200061F RID: 1567
public class BulletWeaponDataBlock : global::WeaponDataBlock
{
	// Token: 0x0600330E RID: 13070 RVA: 0x000C06E4 File Offset: 0x000BE8E4
	protected override global::IInventoryItem ConstructItem()
	{
		return new global::BulletWeaponDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x0600330F RID: 13071 RVA: 0x000C06EC File Offset: 0x000BE8EC
	public void Awake()
	{
	}

	// Token: 0x06003310 RID: 13072 RVA: 0x000C06F0 File Offset: 0x000BE8F0
	public override byte GetMaxEligableSlots()
	{
		return (byte)this.maxEligableSlots;
	}

	// Token: 0x06003311 RID: 13073 RVA: 0x000C06FC File Offset: 0x000BE8FC
	public override int RetreiveMenuOptions(global::IInventoryItem item, global::InventoryItem.MenuItem[] results, int offset)
	{
		offset = base.RetreiveMenuOptions(item, results, offset);
		if (item.isInLocalInventory)
		{
			results[offset++] = global::InventoryItem.MenuItem.Unload;
		}
		return offset;
	}

	// Token: 0x06003312 RID: 13074 RVA: 0x000C072C File Offset: 0x000BE92C
	public override global::InventoryItem.MenuItemResult ExecuteMenuOption(global::InventoryItem.MenuItem option, global::IInventoryItem item)
	{
		if (option != global::InventoryItem.MenuItem.Unload)
		{
			return base.ExecuteMenuOption(option, item);
		}
		return global::InventoryItem.MenuItemResult.DoneOnServer;
	}

	// Token: 0x06003313 RID: 13075 RVA: 0x000C0754 File Offset: 0x000BE954
	public override void InstallData(global::IInventoryItem item)
	{
		base.InstallData(item);
		global::IBulletWeaponItem bulletWeaponItem = item as global::IBulletWeaponItem;
		this._maxUses = this.maxClipAmmo;
		bulletWeaponItem.clipAmmo = this.maxClipAmmo;
	}

	// Token: 0x06003314 RID: 13076 RVA: 0x000C0788 File Offset: 0x000BE988
	public virtual void Local_DryFire(global::ViewModel vm, global::ItemRepresentation itemRep)
	{
		this.dryFireSound.PlayLocal(itemRep.transform, Vector3.zero, 1f, 0);
	}

	// Token: 0x06003315 RID: 13077 RVA: 0x000C07B4 File Offset: 0x000BE9B4
	public virtual void Local_Reload(global::ViewModel vm, global::ItemRepresentation itemRep, global::IBulletWeaponItem itemInstance, ref global::HumanController.InputSample sample)
	{
		if (vm)
		{
			vm.PlayReloadAnimation();
		}
		this.reloadSound.PlayLocal(itemRep.transform, Vector3.zero, 1f, 0);
		itemRep.Action(3, 0);
	}

	// Token: 0x06003316 RID: 13078 RVA: 0x000C07F8 File Offset: 0x000BE9F8
	public virtual void Local_FireWeapon(global::ViewModel vm, global::ItemRepresentation itemRep, global::IBulletWeaponItem itemInstance, ref global::HumanController.InputSample sample)
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
		global::NetEntityID hitView = global::NetEntityID.unassigned;
		bool hitNetworkView = false;
		int num = 1;
		itemInstance.Consume(ref num);
		RaycastHit2 raycastHit;
		bool flag = Physics2.Raycast2(eyesRay, ref raycastHit, this.GetBulletRange(itemRep), 406721553);
		global::TakeDamage takeDamage = null;
		Vector3 point;
		Component hitComponent;
		if (flag)
		{
			point = raycastHit.point;
			IDBase id = raycastHit.id;
			hitComponent = ((!raycastHit.remoteBodyPart) ? raycastHit.collider : raycastHit.remoteBodyPart);
			IDMain idmain = (!id) ? null : id.idMain;
			if (idmain)
			{
				hitView = global::NetEntityID.Get(idmain);
				if (!hitView.isUnassigned)
				{
					hitNetworkView = true;
					takeDamage = idmain.GetComponent<global::TakeDamage>();
					if (takeDamage && takeDamage.ShouldPlayHitNotification())
					{
						this.PlayHitNotification(point, character);
					}
					bool flag2 = false;
					if (raycastHit.remoteBodyPart)
					{
						BodyPart bodyPart = raycastHit.remoteBodyPart.bodyPart;
						switch (bodyPart)
						{
						case 16:
						case 20:
						case 21:
							break;
						default:
							switch (bodyPart)
							{
							case 9:
							case 12:
								goto IL_164;
							}
							flag2 = false;
							goto IL_174;
						}
						IL_164:
						flag2 = true;
					}
					IL_174:
					if (flag2)
					{
						this.headshotSound.Play();
					}
				}
			}
		}
		else
		{
			point = eyesRay.GetPoint(1000f);
			hitComponent = null;
		}
		bool allowBlood = flag && (!raycastHit.isHitboxHit || BodyParts.IsDefined(raycastHit.bodyPart) || takeDamage != null);
		global::Socket socket;
		bool flag3;
		if (vm)
		{
			socket = vm.socketMap["muzzle"].socket;
			flag3 = true;
		}
		else
		{
			socket = itemRep.muzzle;
			flag3 = false;
		}
		Vector3 position = socket.position;
		this.DoWeaponEffects(character.transform, position, point, socket, flag3, hitComponent, allowBlood, itemRep);
		if (flag3)
		{
			vm.PlayFireAnimation();
		}
		float num2 = 1f;
		bool flag4 = sample.aim && sample.crouch;
		if (flag4)
		{
			num2 -= this.aimingRecoilSubtract + this.crouchRecoilSubtract * 0.5f;
		}
		else if (sample.aim)
		{
			num2 -= this.aimingRecoilSubtract;
		}
		else if (sample.crouch)
		{
			num2 -= this.crouchRecoilSubtract;
		}
		num2 = Mathf.Clamp01(num2);
		float pitch = Random.Range(this.recoilPitchMin, this.recoilPitchMax) * num2;
		float yaw = Random.Range(this.recoilYawMin, this.recoilYawMax) * num2;
		if (!global::BulletWeaponDataBlock.weaponRecoil && character.recoilSimulation)
		{
			character.recoilSimulation.AddRecoil(this.recoilDuration, pitch, yaw);
		}
		global::HeadBob component = global::CameraMount.current.GetComponent<global::HeadBob>();
		if (component && this.shotBob && global::BulletWeaponDataBlock.headRecoil)
		{
			component.AddEffect(this.shotBob);
		}
		BitStream bitStream = new BitStream(false);
		this.WriteHitInfo(bitStream, ref eyesRay, flag, ref raycastHit, hitNetworkView, hitView);
		itemRep.ActionStream(1, 0, bitStream);
	}

	// Token: 0x06003317 RID: 13079 RVA: 0x000C0B7C File Offset: 0x000BED7C
	protected void WriteHitInfo(BitStream sendStream, ref Ray ray, bool didHit, ref RaycastHit2 hit)
	{
		global::NetEntityID hitView;
		bool hitNetworkView;
		if (didHit)
		{
			IDBase id = hit.id;
			if (id && id.idMain)
			{
				hitView = global::NetEntityID.Get(id.idMain);
				hitNetworkView = !hitView.isUnassigned;
			}
			else
			{
				hitNetworkView = false;
				hitView = global::NetEntityID.unassigned;
			}
		}
		else
		{
			hitView = global::NetEntityID.unassigned;
			hitNetworkView = false;
		}
		this.WriteHitInfo(sendStream, ref ray, didHit, ref hit, hitNetworkView, hitView);
	}

	// Token: 0x06003318 RID: 13080 RVA: 0x000C0BF0 File Offset: 0x000BEDF0
	protected virtual void WriteHitInfo(BitStream sendStream, ref Ray ray, bool didHit, ref RaycastHit2 hit, bool hitNetworkView, global::NetEntityID hitView)
	{
		Vector3 vector;
		if (didHit)
		{
			if (hitNetworkView)
			{
				IDRemoteBodyPart remoteBodyPart = hit.remoteBodyPart;
				Transform transform;
				if (remoteBodyPart)
				{
					sendStream.WriteByte(remoteBodyPart.bodyPart);
					transform = remoteBodyPart.transform;
				}
				else
				{
					sendStream.WriteByte(254);
					transform = hitView.transform;
				}
				sendStream.Write<global::NetEntityID>(hitView, new object[0]);
				vector = transform.InverseTransformPoint(hit.point);
			}
			else
			{
				sendStream.WriteByte(byte.MaxValue);
				vector = hit.point;
			}
		}
		else
		{
			sendStream.WriteByte(byte.MaxValue);
			vector = ray.GetPoint(1000f);
		}
		sendStream.WriteVector3(vector);
	}

	// Token: 0x06003319 RID: 13081 RVA: 0x000C0CA0 File Offset: 0x000BEEA0
	protected virtual void ReadHitInfo(BitStream stream, out GameObject hitObj, out bool hitNetworkObj, out bool hitBodyPart, out BodyPart bodyPart, out IDRemoteBodyPart remoteBodyPart, out global::NetEntityID hitViewID, out Transform fromTransform, out Vector3 endPos, out Vector3 offset, out bool isHeadshot)
	{
		byte b = stream.ReadByte();
		if (b < 255)
		{
			hitNetworkObj = true;
			if (b < 120)
			{
				hitBodyPart = true;
				bodyPart = (int)b;
			}
			else
			{
				hitBodyPart = false;
				bodyPart = 0;
			}
		}
		else
		{
			hitNetworkObj = false;
			hitBodyPart = false;
			bodyPart = 0;
		}
		if (hitNetworkObj)
		{
			hitViewID = stream.Read<global::NetEntityID>(new object[0]);
			if (!hitViewID.isUnassigned)
			{
				hitObj = hitViewID.gameObject;
				if (hitObj)
				{
					IDBase idbase = IDBase.Get(hitObj);
					if (idbase)
					{
						IDMain idMain = idbase.idMain;
						if (idMain)
						{
							global::HitBoxSystem hitBoxSystem;
							if (idMain is global::Character)
							{
								hitBoxSystem = ((global::Character)idMain).hitBoxSystem;
							}
							else
							{
								hitBoxSystem = idMain.GetRemote<global::HitBoxSystem>();
							}
							if (hitBoxSystem)
							{
								hitBoxSystem.bodyParts.TryGetValue(bodyPart, ref remoteBodyPart);
							}
							else
							{
								remoteBodyPart = null;
							}
						}
						else
						{
							remoteBodyPart = null;
						}
					}
					else
					{
						remoteBodyPart = null;
					}
				}
				else
				{
					remoteBodyPart = null;
				}
			}
			else
			{
				hitObj = null;
				remoteBodyPart = null;
			}
		}
		else
		{
			hitViewID = global::NetEntityID.unassigned;
			hitObj = null;
			remoteBodyPart = null;
		}
		endPos = stream.ReadVector3();
		offset = Vector3.zero;
		if (remoteBodyPart)
		{
			fromTransform = remoteBodyPart.transform;
			BodyPart bodyPart2 = bodyPart;
			switch (bodyPart2)
			{
			case 16:
			case 20:
			case 21:
				break;
			default:
				switch (bodyPart2)
				{
				case 9:
				case 12:
					goto IL_1A3;
				}
				isHeadshot = false;
				goto IL_1B5;
			}
			IL_1A3:
			isHeadshot = true;
			IL_1B5:;
		}
		else if (hitObj)
		{
			fromTransform = hitObj.transform;
			isHeadshot = false;
		}
		else
		{
			fromTransform = null;
			isHeadshot = false;
		}
		if (fromTransform)
		{
			offset = endPos;
			endPos = fromTransform.TransformPoint(endPos);
		}
	}

	// Token: 0x0600331A RID: 13082 RVA: 0x000C0EC8 File Offset: 0x000BF0C8
	public override void DoAction1(BitStream stream, global::ItemRepresentation rep, ref uLink.NetworkMessageInfo info)
	{
		GameObject gameObject;
		bool flag;
		bool flag2;
		BodyPart bodyPart;
		IDRemoteBodyPart idremoteBodyPart;
		global::NetEntityID netEntityID;
		Transform transform;
		Vector3 endPos;
		Vector3 vector;
		bool flag3;
		this.ReadHitInfo(stream, out gameObject, out flag, out flag2, out bodyPart, out idremoteBodyPart, out netEntityID, out transform, out endPos, out vector, out flag3);
		if (flag3)
		{
			this.headshotSound.Play(gameObject.transform.position, 1f, 4f, 30f);
		}
		this.DoWeaponEffects(rep.transform.parent, rep.muzzle.position, endPos, rep.muzzle, false, (!idremoteBodyPart) ? ((!gameObject) ? null : gameObject.GetComponentInChildren<CapsuleCollider>()) : idremoteBodyPart, flag && (!idremoteBodyPart || BodyParts.IsDefined(bodyPart) || gameObject.GetComponent<global::TakeDamage>() != null), rep);
	}

	// Token: 0x0600331B RID: 13083 RVA: 0x000C0F9C File Offset: 0x000BF19C
	public virtual float GetBulletRange(global::ItemRepresentation itemRep)
	{
		if (!itemRep)
		{
			return this.bulletRange;
		}
		return this.bulletRange * ((!this.IsSilenced(itemRep)) ? 1f : 0.75f);
	}

	// Token: 0x0600331C RID: 13084 RVA: 0x000C0FE0 File Offset: 0x000BF1E0
	public virtual float GetDamage(global::ItemRepresentation itemRep)
	{
		float num = Random.Range(this.damageMin, this.damageMax);
		return num * ((!this.IsSilenced(itemRep)) ? 1f : 0.8f);
	}

	// Token: 0x0600331D RID: 13085 RVA: 0x000C101C File Offset: 0x000BF21C
	public virtual bool IsSilenced(global::ItemRepresentation itemRep)
	{
		return (itemRep.modFlags & global::ItemModFlags.Audio) == global::ItemModFlags.Audio;
	}

	// Token: 0x0600331E RID: 13086 RVA: 0x000C102C File Offset: 0x000BF22C
	public virtual AudioClip GetFireSound(global::ItemRepresentation itemRep)
	{
		if (this.IsSilenced(itemRep))
		{
			return this.fireSound_Silenced;
		}
		return this.fireSound;
	}

	// Token: 0x0600331F RID: 13087 RVA: 0x000C1048 File Offset: 0x000BF248
	public virtual AudioClip GetFarFireSound(global::ItemRepresentation itemRep)
	{
		if (this.IsSilenced(itemRep))
		{
			return this.fireSound_SilencedFar;
		}
		return this.fireSound_Far;
	}

	// Token: 0x06003320 RID: 13088 RVA: 0x000C1064 File Offset: 0x000BF264
	public virtual float GetFireSoundRangeMin()
	{
		return 2f;
	}

	// Token: 0x06003321 RID: 13089 RVA: 0x000C106C File Offset: 0x000BF26C
	public virtual float GetFireSoundRangeMax()
	{
		return 60f;
	}

	// Token: 0x06003322 RID: 13090 RVA: 0x000C1074 File Offset: 0x000BF274
	public virtual float GetFarFireSoundRangeMin()
	{
		return this.GetFireSoundRangeMax() * 0.5f;
	}

	// Token: 0x06003323 RID: 13091 RVA: 0x000C1084 File Offset: 0x000BF284
	public virtual float GetFarFireSoundRangeMax()
	{
		return this.fireSoundRange;
	}

	// Token: 0x06003324 RID: 13092 RVA: 0x000C108C File Offset: 0x000BF28C
	public virtual void PlayFireSound(Transform soundTransform, bool firstPerson, global::ItemRepresentation itemRep)
	{
		bool flag = this.IsSilenced(itemRep);
		AudioClip clip = this.GetFireSound(itemRep);
		float num = Vector3.Distance(soundTransform.position, Camera.main.transform.position);
		float farFireSoundRangeMin = this.GetFarFireSoundRangeMin();
		clip.PlayLocal(soundTransform, Vector3.zero, 1f, Random.Range(0.92f, 1.08f), this.GetFireSoundRangeMin(), this.GetFireSoundRangeMax() * ((!flag) ? 1f : 1.5f), (!firstPerson) ? 20 : 0);
		if (!firstPerson && num > farFireSoundRangeMin && !flag)
		{
			AudioClip farFireSound = this.GetFarFireSound(itemRep);
			if (farFireSound)
			{
				farFireSound.PlayLocal(soundTransform, Vector3.zero, 1f, Random.Range(0.9f, 1.1f), 0f, this.GetFarFireSoundRangeMax(), 50);
			}
		}
	}

	// Token: 0x06003325 RID: 13093 RVA: 0x000C1174 File Offset: 0x000BF374
	public virtual void DoWeaponEffects(Transform soundTransform, Vector3 startPos, Vector3 endPos, global::Socket muzzleSocket, bool firstPerson, Component hitComponent, bool allowBlood, global::ItemRepresentation itemRep)
	{
		Vector3 vector = endPos - startPos;
		vector.Normalize();
		bool flag = this.IsSilenced(itemRep);
		GameObject gameObject = (GameObject)Object.Instantiate(this.tracerPrefab, startPos, Quaternion.LookRotation(vector));
		global::Tracer component = gameObject.GetComponent<global::Tracer>();
		if (component)
		{
			component.Init(hitComponent, 406721553, this.GetBulletRange(itemRep), allowBlood);
		}
		if (flag)
		{
			component.startScale = Vector3.zero;
		}
		this.PlayFireSound(soundTransform, firstPerson, itemRep);
		if (!flag)
		{
			GameObject gameObject2 = muzzleSocket.InstantiateAsChild((!firstPerson) ? this.muzzleFlashWorld : this.muzzleflashVM, false);
			Object.Destroy(gameObject2, 1f);
		}
	}

	// Token: 0x06003326 RID: 13094 RVA: 0x000C1230 File Offset: 0x000BF430
	public override void DoAction2(BitStream stream, global::ItemRepresentation itemRep, ref uLink.NetworkMessageInfo info)
	{
	}

	// Token: 0x06003327 RID: 13095 RVA: 0x000C1234 File Offset: 0x000BF434
	public virtual float GetGUIDamage()
	{
		return this.damageMin + (this.damageMax - this.damageMin) * 0.5f;
	}

	// Token: 0x06003328 RID: 13096 RVA: 0x000C1250 File Offset: 0x000BF450
	public override void PopulateInfoWindow(global::ItemToolTip infoWindow, global::IInventoryItem tipItem)
	{
		infoWindow.AddItemTitle(this, tipItem, 0f);
		infoWindow.AddConditionInfo(tipItem);
		infoWindow.AddSectionTitle("Weapon Stats", 20f);
		float currentAmount = this.recoilPitchMax + this.recoilYawMax;
		float maxAmount = 60f;
		float currentAmount2 = 1f / this.fireRate;
		if (this.isSemiAuto)
		{
			infoWindow.AddBasicLabel("Semi Automatic Weapon", 15f);
		}
		else
		{
			infoWindow.AddProgressStat("Fire Rate", currentAmount2, 12f, 15f);
		}
		infoWindow.AddProgressStat("Damage", this.GetGUIDamage(), 100f, 15f);
		infoWindow.AddProgressStat("Recoil", currentAmount, maxAmount, 15f);
		infoWindow.AddProgressStat("Range", this.GetBulletRange(null), 200f, 15f);
		infoWindow.AddItemDescription(this, 15f);
		infoWindow.FinishPopulating();
	}

	// Token: 0x06003329 RID: 13097 RVA: 0x000C133C File Offset: 0x000BF53C
	public override string GetItemDescription()
	{
		return "This is a weapon. Drag it to your belt (right side of screen) and press the corresponding number key to use it.";
	}

	// Token: 0x0600332A RID: 13098 RVA: 0x000C1344 File Offset: 0x000BF544
	protected override void SecureWriteMemberValues(BitStream stream)
	{
		base.SecureWriteMemberValues(stream);
		stream.Write<int>(406721553, new object[0]);
		stream.Write<float>(this.crouchRecoilSubtract, new object[0]);
		stream.Write<int>(this.maxClipAmmo, new object[0]);
		stream.Write<float>(this.recoilPitchMin, new object[0]);
		stream.Write<float>(this.recoilPitchMax, new object[0]);
		stream.Write<float>(this.recoilYawMin, new object[0]);
		stream.Write<float>(this.recoilYawMax, new object[0]);
		stream.Write<float>(this.recoilDuration, new object[0]);
		stream.Write<float>(this.aimingRecoilSubtract, new object[0]);
		stream.Write<int>((!this.ammoType) ? 0 : this.ammoType.uniqueID, new object[0]);
	}

	// Token: 0x04001AFF RID: 6911
	public const int hitMask = 406721553;

	// Token: 0x04001B00 RID: 6912
	private const byte kDidNotHitNetworkView = 255;

	// Token: 0x04001B01 RID: 6913
	private const byte kDidHitNetworkViewWithoutBodyPart = 254;

	// Token: 0x04001B02 RID: 6914
	public global::AmmoItemDataBlock ammoType;

	// Token: 0x04001B03 RID: 6915
	public int maxClipAmmo;

	// Token: 0x04001B04 RID: 6916
	public GameObject tracerPrefab;

	// Token: 0x04001B05 RID: 6917
	public GameObject muzzleflashVM;

	// Token: 0x04001B06 RID: 6918
	public GameObject muzzleFlashWorld;

	// Token: 0x04001B07 RID: 6919
	public AudioClip fireSound;

	// Token: 0x04001B08 RID: 6920
	public AudioClip fireSound_Far;

	// Token: 0x04001B09 RID: 6921
	public AudioClip reloadSound;

	// Token: 0x04001B0A RID: 6922
	public AudioClip headshotSound;

	// Token: 0x04001B0B RID: 6923
	public AudioClip fireSound_SilencedFar;

	// Token: 0x04001B0C RID: 6924
	public AudioClip fireSound_Silenced;

	// Token: 0x04001B0D RID: 6925
	public AudioClip dryFireSound;

	// Token: 0x04001B0E RID: 6926
	public float fireSoundRange = 300f;

	// Token: 0x04001B0F RID: 6927
	public float bulletRange = 200f;

	// Token: 0x04001B10 RID: 6928
	public float recoilPitchMin;

	// Token: 0x04001B11 RID: 6929
	public float recoilPitchMax;

	// Token: 0x04001B12 RID: 6930
	public float recoilYawMin;

	// Token: 0x04001B13 RID: 6931
	public float recoilYawMax;

	// Token: 0x04001B14 RID: 6932
	public float recoilDuration;

	// Token: 0x04001B15 RID: 6933
	public float aimingRecoilSubtract = 0.5f;

	// Token: 0x04001B16 RID: 6934
	public float crouchRecoilSubtract = 0.2f;

	// Token: 0x04001B17 RID: 6935
	public float reloadDuration = 1.5f;

	// Token: 0x04001B18 RID: 6936
	public int maxEligableSlots = 5;

	// Token: 0x04001B19 RID: 6937
	public bool NoAimingAfterShot;

	// Token: 0x04001B1A RID: 6938
	public float aimSway;

	// Token: 0x04001B1B RID: 6939
	public float aimSwaySpeed = 1f;

	// Token: 0x04001B1C RID: 6940
	public global::BobEffect shotBob;

	// Token: 0x04001B1D RID: 6941
	private static bool weaponRecoil = false;

	// Token: 0x04001B1E RID: 6942
	private static bool headRecoil = true;

	// Token: 0x02000620 RID: 1568
	private sealed class ITEM_TYPE : global::BulletWeaponItem<global::BulletWeaponDataBlock>, global::IBulletWeaponItem, global::IHeldItem, global::IInventoryItem, global::IWeaponItem
	{
		// Token: 0x0600332B RID: 13099 RVA: 0x000C1428 File Offset: 0x000BF628
		public ITEM_TYPE(global::BulletWeaponDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x0600332C RID: 13100 RVA: 0x000C1434 File Offset: 0x000BF634
		global::ItemDataBlock global::IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x0600332D RID: 13101 RVA: 0x000C143C File Offset: 0x000BF63C
		global::MagazineDataBlock get_clipType()
		{
			return base.clipType;
		}

		// Token: 0x0600332E RID: 13102 RVA: 0x000C1444 File Offset: 0x000BF644
		int get_clipAmmo()
		{
			return base.clipAmmo;
		}

		// Token: 0x0600332F RID: 13103 RVA: 0x000C144C File Offset: 0x000BF64C
		void set_clipAmmo(int value)
		{
			base.clipAmmo = value;
		}

		// Token: 0x06003330 RID: 13104 RVA: 0x000C1458 File Offset: 0x000BF658
		int get_cachedCasings()
		{
			return base.cachedCasings;
		}

		// Token: 0x06003331 RID: 13105 RVA: 0x000C1460 File Offset: 0x000BF660
		void set_cachedCasings(int value)
		{
			base.cachedCasings = value;
		}

		// Token: 0x06003332 RID: 13106 RVA: 0x000C146C File Offset: 0x000BF66C
		float get_nextCasingsTime()
		{
			return base.nextCasingsTime;
		}

		// Token: 0x06003333 RID: 13107 RVA: 0x000C1474 File Offset: 0x000BF674
		void set_nextCasingsTime(float value)
		{
			base.nextCasingsTime = value;
		}

		// Token: 0x06003334 RID: 13108 RVA: 0x000C1480 File Offset: 0x000BF680
		bool ValidatePrimaryMessageTime(double timestamp)
		{
			return base.ValidatePrimaryMessageTime(timestamp);
		}

		// Token: 0x06003335 RID: 13109 RVA: 0x000C148C File Offset: 0x000BF68C
		bool get_canAim()
		{
			return base.canAim;
		}

		// Token: 0x06003336 RID: 13110 RVA: 0x000C1494 File Offset: 0x000BF694
		float get_nextPrimaryAttackTime()
		{
			return base.nextPrimaryAttackTime;
		}

		// Token: 0x06003337 RID: 13111 RVA: 0x000C149C File Offset: 0x000BF69C
		void set_nextPrimaryAttackTime(float value)
		{
			base.nextPrimaryAttackTime = value;
		}

		// Token: 0x06003338 RID: 13112 RVA: 0x000C14A8 File Offset: 0x000BF6A8
		float get_nextSecondaryAttackTime()
		{
			return base.nextSecondaryAttackTime;
		}

		// Token: 0x06003339 RID: 13113 RVA: 0x000C14B0 File Offset: 0x000BF6B0
		void set_nextSecondaryAttackTime(float value)
		{
			base.nextSecondaryAttackTime = value;
		}

		// Token: 0x0600333A RID: 13114 RVA: 0x000C14BC File Offset: 0x000BF6BC
		float get_deployFinishedTime()
		{
			return base.deployFinishedTime;
		}

		// Token: 0x0600333B RID: 13115 RVA: 0x000C14C4 File Offset: 0x000BF6C4
		void set_deployFinishedTime(float value)
		{
			base.deployFinishedTime = value;
		}

		// Token: 0x0600333C RID: 13116 RVA: 0x000C14D0 File Offset: 0x000BF6D0
		void SetTotalModSlotCount(int count)
		{
			base.SetTotalModSlotCount(count);
		}

		// Token: 0x0600333D RID: 13117 RVA: 0x000C14DC File Offset: 0x000BF6DC
		void SetUsedModSlotCount(int count)
		{
			base.SetUsedModSlotCount(count);
		}

		// Token: 0x0600333E RID: 13118 RVA: 0x000C14E8 File Offset: 0x000BF6E8
		void AddMod(global::ItemModDataBlock mod)
		{
			base.AddMod(mod);
		}

		// Token: 0x0600333F RID: 13119 RVA: 0x000C14F4 File Offset: 0x000BF6F4
		int FindMod(global::ItemModDataBlock mod)
		{
			return base.FindMod(mod);
		}

		// Token: 0x06003340 RID: 13120 RVA: 0x000C1500 File Offset: 0x000BF700
		void OnActivate()
		{
			base.OnActivate();
		}

		// Token: 0x06003341 RID: 13121 RVA: 0x000C1508 File Offset: 0x000BF708
		void OnDeactivate()
		{
			base.OnDeactivate();
		}

		// Token: 0x06003342 RID: 13122 RVA: 0x000C1510 File Offset: 0x000BF710
		global::ViewModel get_viewModelInstance()
		{
			return base.viewModelInstance;
		}

		// Token: 0x06003343 RID: 13123 RVA: 0x000C1518 File Offset: 0x000BF718
		global::ItemRepresentation get_itemRepresentation()
		{
			return base.itemRepresentation;
		}

		// Token: 0x06003344 RID: 13124 RVA: 0x000C1520 File Offset: 0x000BF720
		void set_itemRepresentation(global::ItemRepresentation value)
		{
			base.itemRepresentation = value;
		}

		// Token: 0x06003345 RID: 13125 RVA: 0x000C152C File Offset: 0x000BF72C
		bool get_canActivate()
		{
			return base.canActivate;
		}

		// Token: 0x06003346 RID: 13126 RVA: 0x000C1534 File Offset: 0x000BF734
		bool get_canDeactivate()
		{
			return base.canDeactivate;
		}

		// Token: 0x06003347 RID: 13127 RVA: 0x000C153C File Offset: 0x000BF73C
		global::ItemModFlags get_modFlags()
		{
			return base.modFlags;
		}

		// Token: 0x06003348 RID: 13128 RVA: 0x000C1544 File Offset: 0x000BF744
		global::ItemModDataBlock[] get_itemMods()
		{
			return base.itemMods;
		}

		// Token: 0x06003349 RID: 13129 RVA: 0x000C154C File Offset: 0x000BF74C
		int get_totalModSlots()
		{
			return base.totalModSlots;
		}

		// Token: 0x0600334A RID: 13130 RVA: 0x000C1554 File Offset: 0x000BF754
		int get_usedModSlots()
		{
			return base.usedModSlots;
		}

		// Token: 0x0600334B RID: 13131 RVA: 0x000C155C File Offset: 0x000BF75C
		int get_freeModSlots()
		{
			return base.freeModSlots;
		}

		// Token: 0x0600334C RID: 13132 RVA: 0x000C1564 File Offset: 0x000BF764
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x0600334D RID: 13133 RVA: 0x000C156C File Offset: 0x000BF76C
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x0600334E RID: 13134 RVA: 0x000C1574 File Offset: 0x000BF774
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x0600334F RID: 13135 RVA: 0x000C157C File Offset: 0x000BF77C
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x06003350 RID: 13136 RVA: 0x000C1588 File Offset: 0x000BF788
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x06003351 RID: 13137 RVA: 0x000C1594 File Offset: 0x000BF794
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x06003352 RID: 13138 RVA: 0x000C15A0 File Offset: 0x000BF7A0
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x06003353 RID: 13139 RVA: 0x000C15AC File Offset: 0x000BF7AC
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x06003354 RID: 13140 RVA: 0x000C15B8 File Offset: 0x000BF7B8
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x06003355 RID: 13141 RVA: 0x000C15C4 File Offset: 0x000BF7C4
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x06003356 RID: 13142 RVA: 0x000C15D0 File Offset: 0x000BF7D0
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x06003357 RID: 13143 RVA: 0x000C15DC File Offset: 0x000BF7DC
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x06003358 RID: 13144 RVA: 0x000C15E4 File Offset: 0x000BF7E4
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x06003359 RID: 13145 RVA: 0x000C15EC File Offset: 0x000BF7EC
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x0600335A RID: 13146 RVA: 0x000C15F4 File Offset: 0x000BF7F4
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x0600335B RID: 13147 RVA: 0x000C15FC File Offset: 0x000BF7FC
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x0600335C RID: 13148 RVA: 0x000C1604 File Offset: 0x000BF804
		global::Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x0600335D RID: 13149 RVA: 0x000C160C File Offset: 0x000BF80C
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x0600335E RID: 13150 RVA: 0x000C1614 File Offset: 0x000BF814
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x0600335F RID: 13151 RVA: 0x000C161C File Offset: 0x000BF81C
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x06003360 RID: 13152 RVA: 0x000C1628 File Offset: 0x000BF828
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x06003361 RID: 13153 RVA: 0x000C1630 File Offset: 0x000BF830
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x06003362 RID: 13154 RVA: 0x000C1638 File Offset: 0x000BF838
		global::Character get_character()
		{
			return base.character;
		}

		// Token: 0x06003363 RID: 13155 RVA: 0x000C1640 File Offset: 0x000BF840
		global::Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x06003364 RID: 13156 RVA: 0x000C1648 File Offset: 0x000BF848
		global::Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x06003365 RID: 13157 RVA: 0x000C1650 File Offset: 0x000BF850
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x06003366 RID: 13158 RVA: 0x000C1658 File Offset: 0x000BF858
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
