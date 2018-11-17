using System;
using uLink;
using UnityEngine;

// Token: 0x02000561 RID: 1377
public class BulletWeaponDataBlock : WeaponDataBlock
{
	// Token: 0x06002F46 RID: 12102 RVA: 0x000B8488 File Offset: 0x000B6688
	protected override IInventoryItem ConstructItem()
	{
		return new BulletWeaponDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x06002F47 RID: 12103 RVA: 0x000B8490 File Offset: 0x000B6690
	public void Awake()
	{
	}

	// Token: 0x06002F48 RID: 12104 RVA: 0x000B8494 File Offset: 0x000B6694
	public override byte GetMaxEligableSlots()
	{
		return (byte)this.maxEligableSlots;
	}

	// Token: 0x06002F49 RID: 12105 RVA: 0x000B84A0 File Offset: 0x000B66A0
	public override int RetreiveMenuOptions(IInventoryItem item, InventoryItem.MenuItem[] results, int offset)
	{
		offset = base.RetreiveMenuOptions(item, results, offset);
		if (item.isInLocalInventory)
		{
			results[offset++] = InventoryItem.MenuItem.Unload;
		}
		return offset;
	}

	// Token: 0x06002F4A RID: 12106 RVA: 0x000B84D0 File Offset: 0x000B66D0
	public override InventoryItem.MenuItemResult ExecuteMenuOption(InventoryItem.MenuItem option, IInventoryItem item)
	{
		if (option != InventoryItem.MenuItem.Unload)
		{
			return base.ExecuteMenuOption(option, item);
		}
		return InventoryItem.MenuItemResult.DoneOnServer;
	}

	// Token: 0x06002F4B RID: 12107 RVA: 0x000B84F8 File Offset: 0x000B66F8
	public override void InstallData(IInventoryItem item)
	{
		base.InstallData(item);
		IBulletWeaponItem bulletWeaponItem = item as IBulletWeaponItem;
		this._maxUses = this.maxClipAmmo;
		bulletWeaponItem.clipAmmo = this.maxClipAmmo;
	}

	// Token: 0x06002F4C RID: 12108 RVA: 0x000B852C File Offset: 0x000B672C
	public virtual void Local_DryFire(ViewModel vm, ItemRepresentation itemRep)
	{
		this.dryFireSound.PlayLocal(itemRep.transform, Vector3.zero, 1f, 0);
	}

	// Token: 0x06002F4D RID: 12109 RVA: 0x000B8558 File Offset: 0x000B6758
	public virtual void Local_Reload(ViewModel vm, ItemRepresentation itemRep, IBulletWeaponItem itemInstance, ref HumanController.InputSample sample)
	{
		if (vm)
		{
			vm.PlayReloadAnimation();
		}
		this.reloadSound.PlayLocal(itemRep.transform, Vector3.zero, 1f, 0);
		itemRep.Action(3, 0);
	}

	// Token: 0x06002F4E RID: 12110 RVA: 0x000B859C File Offset: 0x000B679C
	public virtual void Local_FireWeapon(ViewModel vm, ItemRepresentation itemRep, IBulletWeaponItem itemInstance, ref HumanController.InputSample sample)
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
		NetEntityID hitView = NetEntityID.unassigned;
		bool hitNetworkView = false;
		int num = 1;
		itemInstance.Consume(ref num);
		RaycastHit2 raycastHit;
		bool flag = Physics2.Raycast2(eyesRay, ref raycastHit, this.GetBulletRange(itemRep), 406721553);
		TakeDamage takeDamage = null;
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
				hitView = NetEntityID.Get(idmain);
				if (!hitView.isUnassigned)
				{
					hitNetworkView = true;
					takeDamage = idmain.GetComponent<TakeDamage>();
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
		Socket socket;
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
		if (BulletWeaponDataBlock.weaponRecoil && character.recoilSimulation)
		{
			character.recoilSimulation.AddRecoil(this.recoilDuration, pitch, yaw);
		}
		HeadBob component = CameraMount.current.GetComponent<HeadBob>();
		if (component && this.shotBob && BulletWeaponDataBlock.headRecoil)
		{
			component.AddEffect(this.shotBob);
		}
		BitStream bitStream = new BitStream(false);
		this.WriteHitInfo(bitStream, ref eyesRay, flag, ref raycastHit, hitNetworkView, hitView);
		itemRep.ActionStream(1, 0, bitStream);
	}

	// Token: 0x06002F4F RID: 12111 RVA: 0x000B8920 File Offset: 0x000B6B20
	protected void WriteHitInfo(BitStream sendStream, ref Ray ray, bool didHit, ref RaycastHit2 hit)
	{
		NetEntityID hitView;
		bool hitNetworkView;
		if (didHit)
		{
			IDBase id = hit.id;
			if (id && id.idMain)
			{
				hitView = NetEntityID.Get(id.idMain);
				hitNetworkView = !hitView.isUnassigned;
			}
			else
			{
				hitNetworkView = false;
				hitView = NetEntityID.unassigned;
			}
		}
		else
		{
			hitView = NetEntityID.unassigned;
			hitNetworkView = false;
		}
		this.WriteHitInfo(sendStream, ref ray, didHit, ref hit, hitNetworkView, hitView);
	}

	// Token: 0x06002F50 RID: 12112 RVA: 0x000B8994 File Offset: 0x000B6B94
	protected virtual void WriteHitInfo(BitStream sendStream, ref Ray ray, bool didHit, ref RaycastHit2 hit, bool hitNetworkView, NetEntityID hitView)
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
				sendStream.Write<NetEntityID>(hitView, new object[0]);
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

	// Token: 0x06002F51 RID: 12113 RVA: 0x000B8A44 File Offset: 0x000B6C44
	protected virtual void ReadHitInfo(BitStream stream, out GameObject hitObj, out bool hitNetworkObj, out bool hitBodyPart, out BodyPart bodyPart, out IDRemoteBodyPart remoteBodyPart, out NetEntityID hitViewID, out Transform fromTransform, out Vector3 endPos, out Vector3 offset, out bool isHeadshot)
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
			hitViewID = stream.Read<NetEntityID>(new object[0]);
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
							HitBoxSystem hitBoxSystem;
							if (idMain is Character)
							{
								hitBoxSystem = ((Character)idMain).hitBoxSystem;
							}
							else
							{
								hitBoxSystem = idMain.GetRemote<HitBoxSystem>();
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
			hitViewID = NetEntityID.unassigned;
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

	// Token: 0x06002F52 RID: 12114 RVA: 0x000B8C6C File Offset: 0x000B6E6C
	public override void DoAction1(BitStream stream, ItemRepresentation rep, ref NetworkMessageInfo info)
	{
		GameObject gameObject;
		bool flag;
		bool flag2;
		BodyPart bodyPart;
		IDRemoteBodyPart idremoteBodyPart;
		NetEntityID netEntityID;
		Transform transform;
		Vector3 endPos;
		Vector3 vector;
		bool flag3;
		this.ReadHitInfo(stream, out gameObject, out flag, out flag2, out bodyPart, out idremoteBodyPart, out netEntityID, out transform, out endPos, out vector, out flag3);
		if (flag3)
		{
			this.headshotSound.Play(gameObject.transform.position, 1f, 4f, 30f);
		}
		this.DoWeaponEffects(rep.transform.parent, rep.muzzle.position, endPos, rep.muzzle, false, (!idremoteBodyPart) ? ((!gameObject) ? null : gameObject.GetComponentInChildren<CapsuleCollider>()) : idremoteBodyPart, flag && (!idremoteBodyPart || BodyParts.IsDefined(bodyPart) || gameObject.GetComponent<TakeDamage>() != null), rep);
	}

	// Token: 0x06002F53 RID: 12115 RVA: 0x000B8D40 File Offset: 0x000B6F40
	public virtual float GetBulletRange(ItemRepresentation itemRep)
	{
		if (!itemRep)
		{
			return this.bulletRange;
		}
		return this.bulletRange * ((!this.IsSilenced(itemRep)) ? 1f : 0.75f);
	}

	// Token: 0x06002F54 RID: 12116 RVA: 0x000B8D84 File Offset: 0x000B6F84
	public virtual float GetDamage(ItemRepresentation itemRep)
	{
		float num = Random.Range(this.damageMin, this.damageMax);
		return num * ((!this.IsSilenced(itemRep)) ? 1f : 0.8f);
	}

	// Token: 0x06002F55 RID: 12117 RVA: 0x000B8DC0 File Offset: 0x000B6FC0
	public virtual bool IsSilenced(ItemRepresentation itemRep)
	{
		return (itemRep.modFlags & ItemModFlags.Audio) == ItemModFlags.Audio;
	}

	// Token: 0x06002F56 RID: 12118 RVA: 0x000B8DD0 File Offset: 0x000B6FD0
	public virtual AudioClip GetFireSound(ItemRepresentation itemRep)
	{
		if (this.IsSilenced(itemRep))
		{
			return this.fireSound_Silenced;
		}
		return this.fireSound;
	}

	// Token: 0x06002F57 RID: 12119 RVA: 0x000B8DEC File Offset: 0x000B6FEC
	public virtual AudioClip GetFarFireSound(ItemRepresentation itemRep)
	{
		if (this.IsSilenced(itemRep))
		{
			return this.fireSound_SilencedFar;
		}
		return this.fireSound_Far;
	}

	// Token: 0x06002F58 RID: 12120 RVA: 0x000B8E08 File Offset: 0x000B7008
	public virtual float GetFireSoundRangeMin()
	{
		return 2f;
	}

	// Token: 0x06002F59 RID: 12121 RVA: 0x000B8E10 File Offset: 0x000B7010
	public virtual float GetFireSoundRangeMax()
	{
		return 60f;
	}

	// Token: 0x06002F5A RID: 12122 RVA: 0x000B8E18 File Offset: 0x000B7018
	public virtual float GetFarFireSoundRangeMin()
	{
		return this.GetFireSoundRangeMax() * 0.5f;
	}

	// Token: 0x06002F5B RID: 12123 RVA: 0x000B8E28 File Offset: 0x000B7028
	public virtual float GetFarFireSoundRangeMax()
	{
		return this.fireSoundRange;
	}

	// Token: 0x06002F5C RID: 12124 RVA: 0x000B8E30 File Offset: 0x000B7030
	public virtual void PlayFireSound(Transform soundTransform, bool firstPerson, ItemRepresentation itemRep)
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

	// Token: 0x06002F5D RID: 12125 RVA: 0x000B8F18 File Offset: 0x000B7118
	public virtual void DoWeaponEffects(Transform soundTransform, Vector3 startPos, Vector3 endPos, Socket muzzleSocket, bool firstPerson, Component hitComponent, bool allowBlood, ItemRepresentation itemRep)
	{
		Vector3 vector = endPos - startPos;
		vector.Normalize();
		bool flag = this.IsSilenced(itemRep);
		GameObject gameObject = (GameObject)Object.Instantiate(this.tracerPrefab, startPos, Quaternion.LookRotation(vector));
		Tracer component = gameObject.GetComponent<Tracer>();
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

	// Token: 0x06002F5E RID: 12126 RVA: 0x000B8FD4 File Offset: 0x000B71D4
	public override void DoAction2(BitStream stream, ItemRepresentation itemRep, ref NetworkMessageInfo info)
	{
	}

	// Token: 0x06002F5F RID: 12127 RVA: 0x000B8FD8 File Offset: 0x000B71D8
	public virtual float GetGUIDamage()
	{
		return this.damageMin + (this.damageMax - this.damageMin) * 0.5f;
	}

	// Token: 0x06002F60 RID: 12128 RVA: 0x000B8FF4 File Offset: 0x000B71F4
	public override void PopulateInfoWindow(ItemToolTip infoWindow, IInventoryItem tipItem)
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

	// Token: 0x06002F61 RID: 12129 RVA: 0x000B90E0 File Offset: 0x000B72E0
	public override string GetItemDescription()
	{
		return "This is a weapon. Drag it to your belt (right side of screen) and press the corresponding number key to use it.";
	}

	// Token: 0x06002F62 RID: 12130 RVA: 0x000B90E8 File Offset: 0x000B72E8
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

	// Token: 0x0400192E RID: 6446
	public const int hitMask = 406721553;

	// Token: 0x0400192F RID: 6447
	private const byte kDidNotHitNetworkView = 255;

	// Token: 0x04001930 RID: 6448
	private const byte kDidHitNetworkViewWithoutBodyPart = 254;

	// Token: 0x04001931 RID: 6449
	public AmmoItemDataBlock ammoType;

	// Token: 0x04001932 RID: 6450
	public int maxClipAmmo;

	// Token: 0x04001933 RID: 6451
	public GameObject tracerPrefab;

	// Token: 0x04001934 RID: 6452
	public GameObject muzzleflashVM;

	// Token: 0x04001935 RID: 6453
	public GameObject muzzleFlashWorld;

	// Token: 0x04001936 RID: 6454
	public AudioClip fireSound;

	// Token: 0x04001937 RID: 6455
	public AudioClip fireSound_Far;

	// Token: 0x04001938 RID: 6456
	public AudioClip reloadSound;

	// Token: 0x04001939 RID: 6457
	public AudioClip headshotSound;

	// Token: 0x0400193A RID: 6458
	public AudioClip fireSound_SilencedFar;

	// Token: 0x0400193B RID: 6459
	public AudioClip fireSound_Silenced;

	// Token: 0x0400193C RID: 6460
	public AudioClip dryFireSound;

	// Token: 0x0400193D RID: 6461
	public float fireSoundRange = 300f;

	// Token: 0x0400193E RID: 6462
	public float bulletRange = 200f;

	// Token: 0x0400193F RID: 6463
	public float recoilPitchMin;

	// Token: 0x04001940 RID: 6464
	public float recoilPitchMax;

	// Token: 0x04001941 RID: 6465
	public float recoilYawMin;

	// Token: 0x04001942 RID: 6466
	public float recoilYawMax;

	// Token: 0x04001943 RID: 6467
	public float recoilDuration;

	// Token: 0x04001944 RID: 6468
	public float aimingRecoilSubtract = 0.5f;

	// Token: 0x04001945 RID: 6469
	public float crouchRecoilSubtract = 0.2f;

	// Token: 0x04001946 RID: 6470
	public float reloadDuration = 1.5f;

	// Token: 0x04001947 RID: 6471
	public int maxEligableSlots = 5;

	// Token: 0x04001948 RID: 6472
	public bool NoAimingAfterShot;

	// Token: 0x04001949 RID: 6473
	public float aimSway;

	// Token: 0x0400194A RID: 6474
	public float aimSwaySpeed = 1f;

	// Token: 0x0400194B RID: 6475
	public BobEffect shotBob;

	// Token: 0x0400194C RID: 6476
	private static bool weaponRecoil = true;

	// Token: 0x0400194D RID: 6477
	private static bool headRecoil = true;

	// Token: 0x02000562 RID: 1378
	private sealed class ITEM_TYPE : BulletWeaponItem<BulletWeaponDataBlock>, IBulletWeaponItem, IHeldItem, IInventoryItem, IWeaponItem
	{
		// Token: 0x06002F63 RID: 12131 RVA: 0x000B91CC File Offset: 0x000B73CC
		public ITEM_TYPE(BulletWeaponDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x06002F64 RID: 12132 RVA: 0x000B91D8 File Offset: 0x000B73D8
		ItemDataBlock IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x06002F65 RID: 12133 RVA: 0x000B91E0 File Offset: 0x000B73E0
		MagazineDataBlock get_clipType()
		{
			return base.clipType;
		}

		// Token: 0x06002F66 RID: 12134 RVA: 0x000B91E8 File Offset: 0x000B73E8
		int get_clipAmmo()
		{
			return base.clipAmmo;
		}

		// Token: 0x06002F67 RID: 12135 RVA: 0x000B91F0 File Offset: 0x000B73F0
		void set_clipAmmo(int value)
		{
			base.clipAmmo = value;
		}

		// Token: 0x06002F68 RID: 12136 RVA: 0x000B91FC File Offset: 0x000B73FC
		int get_cachedCasings()
		{
			return base.cachedCasings;
		}

		// Token: 0x06002F69 RID: 12137 RVA: 0x000B9204 File Offset: 0x000B7404
		void set_cachedCasings(int value)
		{
			base.cachedCasings = value;
		}

		// Token: 0x06002F6A RID: 12138 RVA: 0x000B9210 File Offset: 0x000B7410
		float get_nextCasingsTime()
		{
			return base.nextCasingsTime;
		}

		// Token: 0x06002F6B RID: 12139 RVA: 0x000B9218 File Offset: 0x000B7418
		void set_nextCasingsTime(float value)
		{
			base.nextCasingsTime = value;
		}

		// Token: 0x06002F6C RID: 12140 RVA: 0x000B9224 File Offset: 0x000B7424
		bool ValidatePrimaryMessageTime(double timestamp)
		{
			return base.ValidatePrimaryMessageTime(timestamp);
		}

		// Token: 0x06002F6D RID: 12141 RVA: 0x000B9230 File Offset: 0x000B7430
		bool get_canAim()
		{
			return base.canAim;
		}

		// Token: 0x06002F6E RID: 12142 RVA: 0x000B9238 File Offset: 0x000B7438
		float get_nextPrimaryAttackTime()
		{
			return base.nextPrimaryAttackTime;
		}

		// Token: 0x06002F6F RID: 12143 RVA: 0x000B9240 File Offset: 0x000B7440
		void set_nextPrimaryAttackTime(float value)
		{
			base.nextPrimaryAttackTime = value;
		}

		// Token: 0x06002F70 RID: 12144 RVA: 0x000B924C File Offset: 0x000B744C
		float get_nextSecondaryAttackTime()
		{
			return base.nextSecondaryAttackTime;
		}

		// Token: 0x06002F71 RID: 12145 RVA: 0x000B9254 File Offset: 0x000B7454
		void set_nextSecondaryAttackTime(float value)
		{
			base.nextSecondaryAttackTime = value;
		}

		// Token: 0x06002F72 RID: 12146 RVA: 0x000B9260 File Offset: 0x000B7460
		float get_deployFinishedTime()
		{
			return base.deployFinishedTime;
		}

		// Token: 0x06002F73 RID: 12147 RVA: 0x000B9268 File Offset: 0x000B7468
		void set_deployFinishedTime(float value)
		{
			base.deployFinishedTime = value;
		}

		// Token: 0x06002F74 RID: 12148 RVA: 0x000B9274 File Offset: 0x000B7474
		void SetTotalModSlotCount(int count)
		{
			base.SetTotalModSlotCount(count);
		}

		// Token: 0x06002F75 RID: 12149 RVA: 0x000B9280 File Offset: 0x000B7480
		void SetUsedModSlotCount(int count)
		{
			base.SetUsedModSlotCount(count);
		}

		// Token: 0x06002F76 RID: 12150 RVA: 0x000B928C File Offset: 0x000B748C
		void AddMod(ItemModDataBlock mod)
		{
			base.AddMod(mod);
		}

		// Token: 0x06002F77 RID: 12151 RVA: 0x000B9298 File Offset: 0x000B7498
		int FindMod(ItemModDataBlock mod)
		{
			return base.FindMod(mod);
		}

		// Token: 0x06002F78 RID: 12152 RVA: 0x000B92A4 File Offset: 0x000B74A4
		void OnActivate()
		{
			base.OnActivate();
		}

		// Token: 0x06002F79 RID: 12153 RVA: 0x000B92AC File Offset: 0x000B74AC
		void OnDeactivate()
		{
			base.OnDeactivate();
		}

		// Token: 0x06002F7A RID: 12154 RVA: 0x000B92B4 File Offset: 0x000B74B4
		ViewModel get_viewModelInstance()
		{
			return base.viewModelInstance;
		}

		// Token: 0x06002F7B RID: 12155 RVA: 0x000B92BC File Offset: 0x000B74BC
		ItemRepresentation get_itemRepresentation()
		{
			return base.itemRepresentation;
		}

		// Token: 0x06002F7C RID: 12156 RVA: 0x000B92C4 File Offset: 0x000B74C4
		void set_itemRepresentation(ItemRepresentation value)
		{
			base.itemRepresentation = value;
		}

		// Token: 0x06002F7D RID: 12157 RVA: 0x000B92D0 File Offset: 0x000B74D0
		bool get_canActivate()
		{
			return base.canActivate;
		}

		// Token: 0x06002F7E RID: 12158 RVA: 0x000B92D8 File Offset: 0x000B74D8
		bool get_canDeactivate()
		{
			return base.canDeactivate;
		}

		// Token: 0x06002F7F RID: 12159 RVA: 0x000B92E0 File Offset: 0x000B74E0
		ItemModFlags get_modFlags()
		{
			return base.modFlags;
		}

		// Token: 0x06002F80 RID: 12160 RVA: 0x000B92E8 File Offset: 0x000B74E8
		ItemModDataBlock[] get_itemMods()
		{
			return base.itemMods;
		}

		// Token: 0x06002F81 RID: 12161 RVA: 0x000B92F0 File Offset: 0x000B74F0
		int get_totalModSlots()
		{
			return base.totalModSlots;
		}

		// Token: 0x06002F82 RID: 12162 RVA: 0x000B92F8 File Offset: 0x000B74F8
		int get_usedModSlots()
		{
			return base.usedModSlots;
		}

		// Token: 0x06002F83 RID: 12163 RVA: 0x000B9300 File Offset: 0x000B7500
		int get_freeModSlots()
		{
			return base.freeModSlots;
		}

		// Token: 0x06002F84 RID: 12164 RVA: 0x000B9308 File Offset: 0x000B7508
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x06002F85 RID: 12165 RVA: 0x000B9310 File Offset: 0x000B7510
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x06002F86 RID: 12166 RVA: 0x000B9318 File Offset: 0x000B7518
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x06002F87 RID: 12167 RVA: 0x000B9320 File Offset: 0x000B7520
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x06002F88 RID: 12168 RVA: 0x000B932C File Offset: 0x000B752C
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x06002F89 RID: 12169 RVA: 0x000B9338 File Offset: 0x000B7538
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x06002F8A RID: 12170 RVA: 0x000B9344 File Offset: 0x000B7544
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x06002F8B RID: 12171 RVA: 0x000B9350 File Offset: 0x000B7550
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x06002F8C RID: 12172 RVA: 0x000B935C File Offset: 0x000B755C
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x06002F8D RID: 12173 RVA: 0x000B9368 File Offset: 0x000B7568
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x06002F8E RID: 12174 RVA: 0x000B9374 File Offset: 0x000B7574
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x06002F8F RID: 12175 RVA: 0x000B9380 File Offset: 0x000B7580
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x06002F90 RID: 12176 RVA: 0x000B9388 File Offset: 0x000B7588
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x06002F91 RID: 12177 RVA: 0x000B9390 File Offset: 0x000B7590
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x06002F92 RID: 12178 RVA: 0x000B9398 File Offset: 0x000B7598
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x06002F93 RID: 12179 RVA: 0x000B93A0 File Offset: 0x000B75A0
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x06002F94 RID: 12180 RVA: 0x000B93A8 File Offset: 0x000B75A8
		Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x06002F95 RID: 12181 RVA: 0x000B93B0 File Offset: 0x000B75B0
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x06002F96 RID: 12182 RVA: 0x000B93B8 File Offset: 0x000B75B8
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x06002F97 RID: 12183 RVA: 0x000B93C0 File Offset: 0x000B75C0
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x06002F98 RID: 12184 RVA: 0x000B93CC File Offset: 0x000B75CC
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x06002F99 RID: 12185 RVA: 0x000B93D4 File Offset: 0x000B75D4
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x06002F9A RID: 12186 RVA: 0x000B93DC File Offset: 0x000B75DC
		Character get_character()
		{
			return base.character;
		}

		// Token: 0x06002F9B RID: 12187 RVA: 0x000B93E4 File Offset: 0x000B75E4
		Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x06002F9C RID: 12188 RVA: 0x000B93EC File Offset: 0x000B75EC
		Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x06002F9D RID: 12189 RVA: 0x000B93F4 File Offset: 0x000B75F4
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x06002F9E RID: 12190 RVA: 0x000B93FC File Offset: 0x000B75FC
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}
}
