using System;
using Facepunch.MeshBatch;
using uLink;
using UnityEngine;

// Token: 0x0200056C RID: 1388
public class DeployableItemDataBlock : HeldItemDataBlock
{
	// Token: 0x06003001 RID: 12289 RVA: 0x000B9C44 File Offset: 0x000B7E44
	protected override IInventoryItem ConstructItem()
	{
		return new DeployableItemDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x17000A2A RID: 2602
	// (get) Token: 0x06003002 RID: 12290 RVA: 0x000B9C4C File Offset: 0x000B7E4C
	public DeployableObject ObjectToPlace
	{
		get
		{
			if (!this._loadedDeployableObject && Application.isPlaying)
			{
				NetCull.LoadPrefabScript<DeployableObject>(this.DeployableObjectPrefabName, out this._deployableObject);
				this._loadedDeployableObject = true;
			}
			return this._deployableObject;
		}
	}

	// Token: 0x06003003 RID: 12291 RVA: 0x000B9C90 File Offset: 0x000B7E90
	public bool CheckPlacement(Ray ray, out Vector3 pos, out Quaternion rot, out TransCarrier carrier)
	{
		DeployableItemDataBlock.DeployPlaceResults deployPlaceResults;
		this.CheckPlacementResults(ray, out pos, out rot, out carrier, out deployPlaceResults);
		return deployPlaceResults.Valid();
	}

	// Token: 0x06003004 RID: 12292 RVA: 0x000B9CB4 File Offset: 0x000B7EB4
	private static bool NonVariantSphereCast(Ray r, Vector3 p)
	{
		Vector3 origin = r.origin;
		Vector3 direction = r.direction;
		float num = direction.x * p.x + direction.y * p.y + direction.z * p.z - (direction.x * origin.x + direction.y * origin.y + direction.z * origin.z);
		Vector3 vector;
		vector.x = p.x - (direction.x * num + origin.x);
		vector.y = p.y - (direction.y * num + origin.y);
		vector.z = p.z - (direction.z * num + origin.z);
		return vector.x * vector.x + vector.y * vector.y + vector.z * vector.z < 0.001f;
	}

	// Token: 0x06003005 RID: 12293 RVA: 0x000B9DC8 File Offset: 0x000B7FC8
	public void CheckPlacementResults(Ray ray, out Vector3 pos, out Quaternion rot, out TransCarrier carrier, out DeployableItemDataBlock.DeployPlaceResults results)
	{
		float num = this.placeRange;
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		DeployableObject deployableObject = null;
		bool flag4 = false;
		bool flag5 = false;
		bool flag6 = this.minCastRadius >= float.Epsilon;
		RaycastHit raycastHit;
		bool flag8;
		MeshBatchInstance meshBatchInstance;
		bool flag7 = (!flag6) ? MeshBatchPhysics.Raycast(ray, ref raycastHit, num, -472317957, ref flag8, ref meshBatchInstance) : MeshBatchPhysics.SphereCast(ray, this.minCastRadius, ref raycastHit, num, -472317957, ref flag8, ref meshBatchInstance);
		Vector3 point = ray.GetPoint(num);
		if (!flag7)
		{
			Vector3 vector = point;
			vector.y += 0.5f;
			flag4 = MeshBatchPhysics.Raycast(vector, Vector3.down, ref raycastHit, 5f, -472317957, ref flag8, ref meshBatchInstance);
		}
		Vector3 vector2;
		Vector3 vector3;
		if (flag7 || flag4)
		{
			IDMain idmain = (!flag8) ? IDBase.GetMain(raycastHit.collider) : meshBatchInstance.idMain;
			flag3 = (idmain is StructureComponent || idmain is StructureMaster);
			vector2 = raycastHit.point;
			vector3 = raycastHit.normal;
			flag = (!flag3 && (deployableObject = (idmain as DeployableObject)));
			if (this.carrierSphereCastMode != DeployableItemDataBlock.CarrierSphereCastMode.Allowed && flag7 && flag6 && !DeployableItemDataBlock.NonVariantSphereCast(ray, vector2))
			{
				float num2;
				Ray ray2;
				if (this.carrierSphereCastMode == DeployableItemDataBlock.CarrierSphereCastMode.AdjustedRay)
				{
					Vector3 origin = ray.origin;
					Vector3 point2 = raycastHit.point;
					Vector3 vector4 = point2 - origin;
					num2 = vector4.magnitude + this.minCastRadius * 2f;
					ray2..ctor(origin, vector4);
					Debug.DrawLine(ray.origin, ray.GetPoint(num2), Color.cyan);
				}
				else
				{
					num2 = num + this.minCastRadius;
					ray2 = ray;
				}
				RaycastHit raycastHit2;
				bool flag10;
				MeshBatchInstance meshBatchInstance2;
				bool flag9;
				if (!(flag9 = MeshBatchPhysics.Raycast(ray2, ref raycastHit2, num2, -472317957, ref flag10, ref meshBatchInstance2)))
				{
					Vector3 vector5 = vector2;
					vector5.y += 0.5f;
					flag9 = MeshBatchPhysics.Raycast(vector5, Vector3.down, ref raycastHit2, 5f, -472317957, ref flag10, ref meshBatchInstance2);
				}
				if (flag9)
				{
					IDMain idmain2 = (!flag10) ? IDBase.GetMain(raycastHit2.collider) : meshBatchInstance2.idMain;
					carrier = ((!idmain2) ? raycastHit2.collider.GetComponent<TransCarrier>() : idmain2.GetLocal<TransCarrier>());
				}
				else
				{
					carrier = null;
				}
			}
			else
			{
				carrier = ((!idmain) ? raycastHit.collider.gameObject : idmain.gameObject).GetComponent<TransCarrier>();
			}
			flag2 = (raycastHit.collider is TerrainCollider || raycastHit.collider.gameObject.layer == 10);
			flag5 = true;
		}
		else
		{
			vector2 = point;
			vector3 = Vector3.up;
			carrier = null;
		}
		bool flag11 = false;
		Hardpoint hardpoint = null;
		if (this.hardpointType != Hardpoint.hardpoint_type.None)
		{
			hardpoint = Hardpoint.GetHardpointFromRay(ray, this.hardpointType);
			if (hardpoint)
			{
				flag11 = true;
				vector2 = hardpoint.transform.position;
				vector3 = hardpoint.transform.up;
				carrier = hardpoint.GetMaster().GetTransCarrier();
				flag5 = true;
			}
		}
		bool flag12 = false;
		if (this.spacingRadius > 0f)
		{
			Collider[] array = Physics.OverlapSphere(vector2, this.spacingRadius);
			foreach (Collider collider in array)
			{
				GameObject gameObject = collider.gameObject;
				IDBase component = collider.gameObject.GetComponent<IDBase>();
				if (component != null)
				{
					gameObject = component.idMain.gameObject;
				}
				if (gameObject.CompareTag(this.ObjectToPlace.gameObject.tag) && Vector3.Distance(vector2, gameObject.transform.position) < this.spacingRadius)
				{
					flag12 = true;
					break;
				}
			}
		}
		bool flag13 = false;
		if (flag && !this.forcePlaceable && deployableObject.cantPlaceOn)
		{
			flag13 = true;
		}
		pos = vector2;
		if (this.orientationMode == DeployableOrientationMode.Default)
		{
			if (this.uprightOnly)
			{
				this.orientationMode = DeployableOrientationMode.Upright;
			}
			else
			{
				this.orientationMode = DeployableOrientationMode.NormalUp;
			}
		}
		Quaternion quaternion;
		switch (this.orientationMode)
		{
		case DeployableOrientationMode.NormalUp:
			quaternion = TransformHelpers.LookRotationForcedUp(ray.direction, vector3);
			break;
		case DeployableOrientationMode.Upright:
			quaternion = TransformHelpers.LookRotationForcedUp(ray.direction, Vector3.up);
			break;
		case DeployableOrientationMode.NormalForward:
		{
			Vector3 forward = Vector3.Cross(ray.direction, Vector3.up);
			quaternion = TransformHelpers.LookRotationForcedUp(forward, vector3);
			break;
		}
		case DeployableOrientationMode.HardpointPosRot:
			if (flag11)
			{
				quaternion = hardpoint.transform.rotation;
			}
			else
			{
				quaternion = TransformHelpers.LookRotationForcedUp(ray.direction, Vector3.up);
			}
			break;
		default:
			throw new NotImplementedException();
		}
		rot = quaternion * this.ObjectToPlace.transform.localRotation;
		bool flag14 = false;
		if (this.checkPlacementZones)
		{
			flag14 = NoPlacementZone.ValidPos(pos);
		}
		float num3 = Vector3.Angle(vector3, Vector3.up);
		results.falseFromDeployable = ((!this.CanStackOnDeployables && flag) || flag13);
		results.falseFromTerrian = (this.TerrainOnly && !flag2);
		results.falseFromClose = (this.spacingRadius > 0f && flag12);
		results.falseFromHardpoint = (this.requireHardpoint && !flag11);
		results.falseFromAngle = (!this.requireHardpoint && num3 >= this.ObjectToPlace.maxSlope);
		results.falseFromPlacementZone = (this.checkPlacementZones && !flag14);
		results.falseFromHittingNothing = !flag5;
		results.falseFromStructure = (this.StructureOnly && !flag3);
		results.falseFromFitRequirements = (this.fitRequirements != null && !this.fitRequirements.Test(pos, (!this.fitTestForcedUp) ? rot : TransformHelpers.LookRotationForcedUp(rot, Vector3.up), this.ObjectToPlace.transform.localScale));
	}

	// Token: 0x06003006 RID: 12294 RVA: 0x000BA43C File Offset: 0x000B863C
	public override void DoAction1(BitStream stream, ItemRepresentation rep, ref NetworkMessageInfo info)
	{
	}

	// Token: 0x04001969 RID: 6505
	public GameGizmo aimGizmo;

	// Token: 0x0400196A RID: 6506
	[NonSerialized]
	private DeployableObject _deployableObject;

	// Token: 0x0400196B RID: 6507
	[NonSerialized]
	private bool _loadedDeployableObject;

	// Token: 0x0400196C RID: 6508
	public string DeployableObjectPrefabName;

	// Token: 0x0400196D RID: 6509
	public Material overrideMat;

	// Token: 0x0400196E RID: 6510
	public bool uprightOnly;

	// Token: 0x0400196F RID: 6511
	public DeployableOrientationMode orientationMode;

	// Token: 0x04001970 RID: 6512
	public bool CanStackOnDeployables = true;

	// Token: 0x04001971 RID: 6513
	public float minCastRadius = 0.25f;

	// Token: 0x04001972 RID: 6514
	public bool StructureOnly;

	// Token: 0x04001973 RID: 6515
	public bool TerrainOnly;

	// Token: 0x04001974 RID: 6516
	public float spacingRadius;

	// Token: 0x04001975 RID: 6517
	public float placeRange = 8f;

	// Token: 0x04001976 RID: 6518
	public bool requireHardpoint;

	// Token: 0x04001977 RID: 6519
	public Hardpoint.hardpoint_type hardpointType;

	// Token: 0x04001978 RID: 6520
	public bool checkPlacementZones;

	// Token: 0x04001979 RID: 6521
	public bool forcePlaceable;

	// Token: 0x0400197A RID: 6522
	public bool neverGrabCarrier;

	// Token: 0x0400197B RID: 6523
	public DeployableItemDataBlock.CarrierSphereCastMode carrierSphereCastMode;

	// Token: 0x0400197C RID: 6524
	public FitRequirements fitRequirements;

	// Token: 0x0400197D RID: 6525
	public bool fitTestForcedUp;

	// Token: 0x0200056D RID: 1389
	private sealed class ITEM_TYPE : DeployableItem<DeployableItemDataBlock>, IDeployableItem, IHeldItem, IInventoryItem
	{
		// Token: 0x06003007 RID: 12295 RVA: 0x000BA440 File Offset: 0x000B8640
		public ITEM_TYPE(DeployableItemDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x06003008 RID: 12296 RVA: 0x000BA44C File Offset: 0x000B864C
		ItemDataBlock IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x06003009 RID: 12297 RVA: 0x000BA454 File Offset: 0x000B8654
		void SetTotalModSlotCount(int count)
		{
			base.SetTotalModSlotCount(count);
		}

		// Token: 0x0600300A RID: 12298 RVA: 0x000BA460 File Offset: 0x000B8660
		void SetUsedModSlotCount(int count)
		{
			base.SetUsedModSlotCount(count);
		}

		// Token: 0x0600300B RID: 12299 RVA: 0x000BA46C File Offset: 0x000B866C
		void AddMod(ItemModDataBlock mod)
		{
			base.AddMod(mod);
		}

		// Token: 0x0600300C RID: 12300 RVA: 0x000BA478 File Offset: 0x000B8678
		int FindMod(ItemModDataBlock mod)
		{
			return base.FindMod(mod);
		}

		// Token: 0x0600300D RID: 12301 RVA: 0x000BA484 File Offset: 0x000B8684
		void OnActivate()
		{
			base.OnActivate();
		}

		// Token: 0x0600300E RID: 12302 RVA: 0x000BA48C File Offset: 0x000B868C
		void OnDeactivate()
		{
			base.OnDeactivate();
		}

		// Token: 0x0600300F RID: 12303 RVA: 0x000BA494 File Offset: 0x000B8694
		ViewModel get_viewModelInstance()
		{
			return base.viewModelInstance;
		}

		// Token: 0x06003010 RID: 12304 RVA: 0x000BA49C File Offset: 0x000B869C
		ItemRepresentation get_itemRepresentation()
		{
			return base.itemRepresentation;
		}

		// Token: 0x06003011 RID: 12305 RVA: 0x000BA4A4 File Offset: 0x000B86A4
		void set_itemRepresentation(ItemRepresentation value)
		{
			base.itemRepresentation = value;
		}

		// Token: 0x06003012 RID: 12306 RVA: 0x000BA4B0 File Offset: 0x000B86B0
		bool get_canActivate()
		{
			return base.canActivate;
		}

		// Token: 0x06003013 RID: 12307 RVA: 0x000BA4B8 File Offset: 0x000B86B8
		bool get_canDeactivate()
		{
			return base.canDeactivate;
		}

		// Token: 0x06003014 RID: 12308 RVA: 0x000BA4C0 File Offset: 0x000B86C0
		ItemModFlags get_modFlags()
		{
			return base.modFlags;
		}

		// Token: 0x06003015 RID: 12309 RVA: 0x000BA4C8 File Offset: 0x000B86C8
		ItemModDataBlock[] get_itemMods()
		{
			return base.itemMods;
		}

		// Token: 0x06003016 RID: 12310 RVA: 0x000BA4D0 File Offset: 0x000B86D0
		int get_totalModSlots()
		{
			return base.totalModSlots;
		}

		// Token: 0x06003017 RID: 12311 RVA: 0x000BA4D8 File Offset: 0x000B86D8
		int get_usedModSlots()
		{
			return base.usedModSlots;
		}

		// Token: 0x06003018 RID: 12312 RVA: 0x000BA4E0 File Offset: 0x000B86E0
		int get_freeModSlots()
		{
			return base.freeModSlots;
		}

		// Token: 0x06003019 RID: 12313 RVA: 0x000BA4E8 File Offset: 0x000B86E8
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x0600301A RID: 12314 RVA: 0x000BA4F0 File Offset: 0x000B86F0
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x0600301B RID: 12315 RVA: 0x000BA4F8 File Offset: 0x000B86F8
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x0600301C RID: 12316 RVA: 0x000BA500 File Offset: 0x000B8700
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x0600301D RID: 12317 RVA: 0x000BA50C File Offset: 0x000B870C
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x0600301E RID: 12318 RVA: 0x000BA518 File Offset: 0x000B8718
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x0600301F RID: 12319 RVA: 0x000BA524 File Offset: 0x000B8724
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x06003020 RID: 12320 RVA: 0x000BA530 File Offset: 0x000B8730
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x06003021 RID: 12321 RVA: 0x000BA53C File Offset: 0x000B873C
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x06003022 RID: 12322 RVA: 0x000BA548 File Offset: 0x000B8748
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x06003023 RID: 12323 RVA: 0x000BA554 File Offset: 0x000B8754
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x06003024 RID: 12324 RVA: 0x000BA560 File Offset: 0x000B8760
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x06003025 RID: 12325 RVA: 0x000BA568 File Offset: 0x000B8768
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x06003026 RID: 12326 RVA: 0x000BA570 File Offset: 0x000B8770
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x06003027 RID: 12327 RVA: 0x000BA578 File Offset: 0x000B8778
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x06003028 RID: 12328 RVA: 0x000BA580 File Offset: 0x000B8780
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x06003029 RID: 12329 RVA: 0x000BA588 File Offset: 0x000B8788
		Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x0600302A RID: 12330 RVA: 0x000BA590 File Offset: 0x000B8790
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x0600302B RID: 12331 RVA: 0x000BA598 File Offset: 0x000B8798
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x0600302C RID: 12332 RVA: 0x000BA5A0 File Offset: 0x000B87A0
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x0600302D RID: 12333 RVA: 0x000BA5AC File Offset: 0x000B87AC
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x0600302E RID: 12334 RVA: 0x000BA5B4 File Offset: 0x000B87B4
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x0600302F RID: 12335 RVA: 0x000BA5BC File Offset: 0x000B87BC
		Character get_character()
		{
			return base.character;
		}

		// Token: 0x06003030 RID: 12336 RVA: 0x000BA5C4 File Offset: 0x000B87C4
		Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x06003031 RID: 12337 RVA: 0x000BA5CC File Offset: 0x000B87CC
		Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x06003032 RID: 12338 RVA: 0x000BA5D4 File Offset: 0x000B87D4
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x06003033 RID: 12339 RVA: 0x000BA5DC File Offset: 0x000B87DC
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}

	// Token: 0x0200056E RID: 1390
	public enum CarrierSphereCastMode
	{
		// Token: 0x0400197F RID: 6527
		Allowed,
		// Token: 0x04001980 RID: 6528
		AdjustedRay,
		// Token: 0x04001981 RID: 6529
		InputRay
	}

	// Token: 0x0200056F RID: 1391
	public struct DeployPlaceResults
	{
		// Token: 0x06003034 RID: 12340 RVA: 0x000BA5E4 File Offset: 0x000B87E4
		public bool Valid()
		{
			return !this.falseFromAngle && !this.falseFromDeployable && !this.falseFromTerrian && !this.falseFromClose && !this.falseFromHardpoint && !this.falseFromPlacementZone && !this.falseFromFitRequirements && !this.falseFromHittingNothing && !this.falseFromStructure;
		}

		// Token: 0x06003035 RID: 12341 RVA: 0x000BA658 File Offset: 0x000B8858
		public void PrintResults()
		{
			if (this.Valid())
			{
				Debug.Log("VALID!");
			}
			else
			{
				string str = string.Format("Results ang:{0} dep:{1} ter:{2} close:{3} hardpoint:{4} npz:{5} fit:{6} nothin:{7} struct:{8}", new object[]
				{
					this.falseFromAngle,
					this.falseFromDeployable,
					this.falseFromTerrian,
					this.falseFromClose,
					this.falseFromHardpoint,
					this.falseFromPlacementZone,
					this.falseFromFitRequirements,
					this.falseFromHittingNothing,
					this.falseFromStructure
				});
				Debug.Log("FAIL! - " + str);
			}
		}

		// Token: 0x04001982 RID: 6530
		public bool falseFromDeployable;

		// Token: 0x04001983 RID: 6531
		public bool falseFromTerrian;

		// Token: 0x04001984 RID: 6532
		public bool falseFromClose;

		// Token: 0x04001985 RID: 6533
		public bool falseFromHardpoint;

		// Token: 0x04001986 RID: 6534
		public bool falseFromAngle;

		// Token: 0x04001987 RID: 6535
		public bool falseFromPlacementZone;

		// Token: 0x04001988 RID: 6536
		public bool falseFromFitRequirements;

		// Token: 0x04001989 RID: 6537
		public bool falseFromHittingNothing;

		// Token: 0x0400198A RID: 6538
		public bool falseFromStructure;
	}
}
