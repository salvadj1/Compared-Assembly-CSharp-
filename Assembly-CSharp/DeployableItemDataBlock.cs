using System;
using Facepunch.MeshBatch;
using uLink;
using UnityEngine;

// Token: 0x0200062A RID: 1578
public class DeployableItemDataBlock : global::HeldItemDataBlock
{
	// Token: 0x060033C9 RID: 13257 RVA: 0x000C1EA0 File Offset: 0x000C00A0
	protected override global::IInventoryItem ConstructItem()
	{
		return new global::DeployableItemDataBlock.ITEM_TYPE(this);
	}

	// Token: 0x17000AA0 RID: 2720
	// (get) Token: 0x060033CA RID: 13258 RVA: 0x000C1EA8 File Offset: 0x000C00A8
	public global::DeployableObject ObjectToPlace
	{
		get
		{
			if (!this._loadedDeployableObject && Application.isPlaying)
			{
				global::NetCull.LoadPrefabScript<global::DeployableObject>(this.DeployableObjectPrefabName, out this._deployableObject);
				this._loadedDeployableObject = true;
			}
			return this._deployableObject;
		}
	}

	// Token: 0x060033CB RID: 13259 RVA: 0x000C1EEC File Offset: 0x000C00EC
	public bool CheckPlacement(Ray ray, out Vector3 pos, out Quaternion rot, out global::TransCarrier carrier)
	{
		global::DeployableItemDataBlock.DeployPlaceResults deployPlaceResults;
		this.CheckPlacementResults(ray, out pos, out rot, out carrier, out deployPlaceResults);
		return deployPlaceResults.Valid();
	}

	// Token: 0x060033CC RID: 13260 RVA: 0x000C1F10 File Offset: 0x000C0110
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

	// Token: 0x060033CD RID: 13261 RVA: 0x000C2024 File Offset: 0x000C0224
	public void CheckPlacementResults(Ray ray, out Vector3 pos, out Quaternion rot, out global::TransCarrier carrier, out global::DeployableItemDataBlock.DeployPlaceResults results)
	{
		float num = this.placeRange;
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		global::DeployableObject deployableObject = null;
		bool flag4 = false;
		bool flag5 = false;
		bool flag6 = this.minCastRadius >= float.Epsilon;
		RaycastHit raycastHit;
		bool flag8;
		MeshBatchInstance meshBatchInstance;
		bool flag7 = (!flag6) ? Facepunch.MeshBatch.MeshBatchPhysics.Raycast(ray, ref raycastHit, num, -472317957, ref flag8, ref meshBatchInstance) : Facepunch.MeshBatch.MeshBatchPhysics.SphereCast(ray, this.minCastRadius, ref raycastHit, num, -472317957, ref flag8, ref meshBatchInstance);
		Vector3 point = ray.GetPoint(num);
		if (!flag7)
		{
			Vector3 vector = point;
			vector.y += 0.5f;
			flag4 = Facepunch.MeshBatch.MeshBatchPhysics.Raycast(vector, Vector3.down, ref raycastHit, 5f, -472317957, ref flag8, ref meshBatchInstance);
		}
		Vector3 vector2;
		Vector3 vector3;
		if (flag7 || flag4)
		{
			IDMain idmain = (!flag8) ? IDBase.GetMain(raycastHit.collider) : meshBatchInstance.idMain;
			flag3 = (idmain is global::StructureComponent || idmain is global::StructureMaster);
			vector2 = raycastHit.point;
			vector3 = raycastHit.normal;
			flag = (!flag3 && (deployableObject = (idmain as global::DeployableObject)));
			if (this.carrierSphereCastMode != global::DeployableItemDataBlock.CarrierSphereCastMode.Allowed && flag7 && flag6 && !global::DeployableItemDataBlock.NonVariantSphereCast(ray, vector2))
			{
				float num2;
				Ray ray2;
				if (this.carrierSphereCastMode == global::DeployableItemDataBlock.CarrierSphereCastMode.AdjustedRay)
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
				if (!(flag9 = Facepunch.MeshBatch.MeshBatchPhysics.Raycast(ray2, ref raycastHit2, num2, -472317957, ref flag10, ref meshBatchInstance2)))
				{
					Vector3 vector5 = vector2;
					vector5.y += 0.5f;
					flag9 = Facepunch.MeshBatch.MeshBatchPhysics.Raycast(vector5, Vector3.down, ref raycastHit2, 5f, -472317957, ref flag10, ref meshBatchInstance2);
				}
				if (flag9)
				{
					IDMain idmain2 = (!flag10) ? IDBase.GetMain(raycastHit2.collider) : meshBatchInstance2.idMain;
					carrier = ((!idmain2) ? raycastHit2.collider.GetComponent<global::TransCarrier>() : idmain2.GetLocal<global::TransCarrier>());
				}
				else
				{
					carrier = null;
				}
			}
			else
			{
				carrier = ((!idmain) ? raycastHit.collider.gameObject : idmain.gameObject).GetComponent<global::TransCarrier>();
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
		global::Hardpoint hardpoint = null;
		if (this.hardpointType != global::Hardpoint.hardpoint_type.None)
		{
			hardpoint = global::Hardpoint.GetHardpointFromRay(ray, this.hardpointType);
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
		if (this.orientationMode == global::DeployableOrientationMode.Default)
		{
			if (this.uprightOnly)
			{
				this.orientationMode = global::DeployableOrientationMode.Upright;
			}
			else
			{
				this.orientationMode = global::DeployableOrientationMode.NormalUp;
			}
		}
		Quaternion quaternion;
		switch (this.orientationMode)
		{
		case global::DeployableOrientationMode.NormalUp:
			quaternion = global::TransformHelpers.LookRotationForcedUp(ray.direction, vector3);
			break;
		case global::DeployableOrientationMode.Upright:
			quaternion = global::TransformHelpers.LookRotationForcedUp(ray.direction, Vector3.up);
			break;
		case global::DeployableOrientationMode.NormalForward:
		{
			Vector3 forward = Vector3.Cross(ray.direction, Vector3.up);
			quaternion = global::TransformHelpers.LookRotationForcedUp(forward, vector3);
			break;
		}
		case global::DeployableOrientationMode.HardpointPosRot:
			if (flag11)
			{
				quaternion = hardpoint.transform.rotation;
			}
			else
			{
				quaternion = global::TransformHelpers.LookRotationForcedUp(ray.direction, Vector3.up);
			}
			break;
		default:
			throw new NotImplementedException();
		}
		rot = quaternion * this.ObjectToPlace.transform.localRotation;
		bool flag14 = false;
		if (this.checkPlacementZones)
		{
			flag14 = global::NoPlacementZone.ValidPos(pos);
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
		results.falseFromFitRequirements = (this.fitRequirements != null && !this.fitRequirements.Test(pos, (!this.fitTestForcedUp) ? rot : global::TransformHelpers.LookRotationForcedUp(rot, Vector3.up), this.ObjectToPlace.transform.localScale));
	}

	// Token: 0x060033CE RID: 13262 RVA: 0x000C2698 File Offset: 0x000C0898
	public override void DoAction1(BitStream stream, global::ItemRepresentation rep, ref uLink.NetworkMessageInfo info)
	{
	}

	// Token: 0x04001B3A RID: 6970
	public global::GameGizmo aimGizmo;

	// Token: 0x04001B3B RID: 6971
	[NonSerialized]
	private global::DeployableObject _deployableObject;

	// Token: 0x04001B3C RID: 6972
	[NonSerialized]
	private bool _loadedDeployableObject;

	// Token: 0x04001B3D RID: 6973
	public string DeployableObjectPrefabName;

	// Token: 0x04001B3E RID: 6974
	public Material overrideMat;

	// Token: 0x04001B3F RID: 6975
	public bool uprightOnly;

	// Token: 0x04001B40 RID: 6976
	public global::DeployableOrientationMode orientationMode;

	// Token: 0x04001B41 RID: 6977
	public bool CanStackOnDeployables = true;

	// Token: 0x04001B42 RID: 6978
	public float minCastRadius = 0.25f;

	// Token: 0x04001B43 RID: 6979
	public bool StructureOnly;

	// Token: 0x04001B44 RID: 6980
	public bool TerrainOnly;

	// Token: 0x04001B45 RID: 6981
	public float spacingRadius;

	// Token: 0x04001B46 RID: 6982
	public float placeRange = 8f;

	// Token: 0x04001B47 RID: 6983
	public bool requireHardpoint;

	// Token: 0x04001B48 RID: 6984
	public global::Hardpoint.hardpoint_type hardpointType;

	// Token: 0x04001B49 RID: 6985
	public bool checkPlacementZones;

	// Token: 0x04001B4A RID: 6986
	public bool forcePlaceable;

	// Token: 0x04001B4B RID: 6987
	public bool neverGrabCarrier;

	// Token: 0x04001B4C RID: 6988
	public global::DeployableItemDataBlock.CarrierSphereCastMode carrierSphereCastMode;

	// Token: 0x04001B4D RID: 6989
	public global::FitRequirements fitRequirements;

	// Token: 0x04001B4E RID: 6990
	public bool fitTestForcedUp;

	// Token: 0x0200062B RID: 1579
	private sealed class ITEM_TYPE : global::DeployableItem<global::DeployableItemDataBlock>, global::IDeployableItem, global::IHeldItem, global::IInventoryItem
	{
		// Token: 0x060033CF RID: 13263 RVA: 0x000C269C File Offset: 0x000C089C
		public ITEM_TYPE(global::DeployableItemDataBlock BLOCK) : base(BLOCK)
		{
		}

		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x060033D0 RID: 13264 RVA: 0x000C26A8 File Offset: 0x000C08A8
		global::ItemDataBlock global::IInventoryItem.datablock
		{
			get
			{
				return this.datablock;
			}
		}

		// Token: 0x060033D1 RID: 13265 RVA: 0x000C26B0 File Offset: 0x000C08B0
		void SetTotalModSlotCount(int count)
		{
			base.SetTotalModSlotCount(count);
		}

		// Token: 0x060033D2 RID: 13266 RVA: 0x000C26BC File Offset: 0x000C08BC
		void SetUsedModSlotCount(int count)
		{
			base.SetUsedModSlotCount(count);
		}

		// Token: 0x060033D3 RID: 13267 RVA: 0x000C26C8 File Offset: 0x000C08C8
		void AddMod(global::ItemModDataBlock mod)
		{
			base.AddMod(mod);
		}

		// Token: 0x060033D4 RID: 13268 RVA: 0x000C26D4 File Offset: 0x000C08D4
		int FindMod(global::ItemModDataBlock mod)
		{
			return base.FindMod(mod);
		}

		// Token: 0x060033D5 RID: 13269 RVA: 0x000C26E0 File Offset: 0x000C08E0
		void OnActivate()
		{
			base.OnActivate();
		}

		// Token: 0x060033D6 RID: 13270 RVA: 0x000C26E8 File Offset: 0x000C08E8
		void OnDeactivate()
		{
			base.OnDeactivate();
		}

		// Token: 0x060033D7 RID: 13271 RVA: 0x000C26F0 File Offset: 0x000C08F0
		global::ViewModel get_viewModelInstance()
		{
			return base.viewModelInstance;
		}

		// Token: 0x060033D8 RID: 13272 RVA: 0x000C26F8 File Offset: 0x000C08F8
		global::ItemRepresentation get_itemRepresentation()
		{
			return base.itemRepresentation;
		}

		// Token: 0x060033D9 RID: 13273 RVA: 0x000C2700 File Offset: 0x000C0900
		void set_itemRepresentation(global::ItemRepresentation value)
		{
			base.itemRepresentation = value;
		}

		// Token: 0x060033DA RID: 13274 RVA: 0x000C270C File Offset: 0x000C090C
		bool get_canActivate()
		{
			return base.canActivate;
		}

		// Token: 0x060033DB RID: 13275 RVA: 0x000C2714 File Offset: 0x000C0914
		bool get_canDeactivate()
		{
			return base.canDeactivate;
		}

		// Token: 0x060033DC RID: 13276 RVA: 0x000C271C File Offset: 0x000C091C
		global::ItemModFlags get_modFlags()
		{
			return base.modFlags;
		}

		// Token: 0x060033DD RID: 13277 RVA: 0x000C2724 File Offset: 0x000C0924
		global::ItemModDataBlock[] get_itemMods()
		{
			return base.itemMods;
		}

		// Token: 0x060033DE RID: 13278 RVA: 0x000C272C File Offset: 0x000C092C
		int get_totalModSlots()
		{
			return base.totalModSlots;
		}

		// Token: 0x060033DF RID: 13279 RVA: 0x000C2734 File Offset: 0x000C0934
		int get_usedModSlots()
		{
			return base.usedModSlots;
		}

		// Token: 0x060033E0 RID: 13280 RVA: 0x000C273C File Offset: 0x000C093C
		int get_freeModSlots()
		{
			return base.freeModSlots;
		}

		// Token: 0x060033E1 RID: 13281 RVA: 0x000C2744 File Offset: 0x000C0944
		bool IsDamaged()
		{
			return base.IsDamaged();
		}

		// Token: 0x060033E2 RID: 13282 RVA: 0x000C274C File Offset: 0x000C094C
		bool IsBroken()
		{
			return base.IsBroken();
		}

		// Token: 0x060033E3 RID: 13283 RVA: 0x000C2754 File Offset: 0x000C0954
		float GetConditionPercent()
		{
			return base.GetConditionPercent();
		}

		// Token: 0x060033E4 RID: 13284 RVA: 0x000C275C File Offset: 0x000C095C
		int AddUses(int count)
		{
			return base.AddUses(count);
		}

		// Token: 0x060033E5 RID: 13285 RVA: 0x000C2768 File Offset: 0x000C0968
		void SetUses(int count)
		{
			base.SetUses(count);
		}

		// Token: 0x060033E6 RID: 13286 RVA: 0x000C2774 File Offset: 0x000C0974
		void SetCondition(float condition)
		{
			base.SetCondition(condition);
		}

		// Token: 0x060033E7 RID: 13287 RVA: 0x000C2780 File Offset: 0x000C0980
		void SetMaxCondition(float condition)
		{
			base.SetMaxCondition(condition);
		}

		// Token: 0x060033E8 RID: 13288 RVA: 0x000C278C File Offset: 0x000C098C
		bool Consume(ref int count)
		{
			return base.Consume(ref count);
		}

		// Token: 0x060033E9 RID: 13289 RVA: 0x000C2798 File Offset: 0x000C0998
		bool TryConditionLoss(float probability, float percentLoss)
		{
			return base.TryConditionLoss(probability, percentLoss);
		}

		// Token: 0x060033EA RID: 13290 RVA: 0x000C27A4 File Offset: 0x000C09A4
		void Serialize(BitStream stream)
		{
			base.Serialize(stream);
		}

		// Token: 0x060033EB RID: 13291 RVA: 0x000C27B0 File Offset: 0x000C09B0
		void Deserialize(BitStream stream)
		{
			base.Deserialize(stream);
		}

		// Token: 0x060033EC RID: 13292 RVA: 0x000C27BC File Offset: 0x000C09BC
		bool MarkDirty()
		{
			return base.MarkDirty();
		}

		// Token: 0x060033ED RID: 13293 RVA: 0x000C27C4 File Offset: 0x000C09C4
		int get_slot()
		{
			return base.slot;
		}

		// Token: 0x060033EE RID: 13294 RVA: 0x000C27CC File Offset: 0x000C09CC
		float get_condition()
		{
			return base.condition;
		}

		// Token: 0x060033EF RID: 13295 RVA: 0x000C27D4 File Offset: 0x000C09D4
		float get_maxcondition()
		{
			return base.maxcondition;
		}

		// Token: 0x060033F0 RID: 13296 RVA: 0x000C27DC File Offset: 0x000C09DC
		int get_uses()
		{
			return base.uses;
		}

		// Token: 0x060033F1 RID: 13297 RVA: 0x000C27E4 File Offset: 0x000C09E4
		global::Inventory get_inventory()
		{
			return base.inventory;
		}

		// Token: 0x060033F2 RID: 13298 RVA: 0x000C27EC File Offset: 0x000C09EC
		bool get_dirty()
		{
			return base.dirty;
		}

		// Token: 0x060033F3 RID: 13299 RVA: 0x000C27F4 File Offset: 0x000C09F4
		float get_lastUseTime()
		{
			return base.lastUseTime;
		}

		// Token: 0x060033F4 RID: 13300 RVA: 0x000C27FC File Offset: 0x000C09FC
		void set_lastUseTime(float value)
		{
			base.lastUseTime = value;
		}

		// Token: 0x060033F5 RID: 13301 RVA: 0x000C2808 File Offset: 0x000C0A08
		bool get_isInLocalInventory()
		{
			return base.isInLocalInventory;
		}

		// Token: 0x060033F6 RID: 13302 RVA: 0x000C2810 File Offset: 0x000C0A10
		IDMain get_idMain()
		{
			return base.idMain;
		}

		// Token: 0x060033F7 RID: 13303 RVA: 0x000C2818 File Offset: 0x000C0A18
		global::Character get_character()
		{
			return base.character;
		}

		// Token: 0x060033F8 RID: 13304 RVA: 0x000C2820 File Offset: 0x000C0A20
		global::Controller get_controller()
		{
			return base.controller;
		}

		// Token: 0x060033F9 RID: 13305 RVA: 0x000C2828 File Offset: 0x000C0A28
		global::Controllable get_controllable()
		{
			return base.controllable;
		}

		// Token: 0x060033FA RID: 13306 RVA: 0x000C2830 File Offset: 0x000C0A30
		bool get_active()
		{
			return base.active;
		}

		// Token: 0x060033FB RID: 13307 RVA: 0x000C2838 File Offset: 0x000C0A38
		bool get_doNotSave()
		{
			return base.doNotSave;
		}
	}

	// Token: 0x0200062C RID: 1580
	public enum CarrierSphereCastMode
	{
		// Token: 0x04001B50 RID: 6992
		Allowed,
		// Token: 0x04001B51 RID: 6993
		AdjustedRay,
		// Token: 0x04001B52 RID: 6994
		InputRay
	}

	// Token: 0x0200062D RID: 1581
	public struct DeployPlaceResults
	{
		// Token: 0x060033FC RID: 13308 RVA: 0x000C2840 File Offset: 0x000C0A40
		public bool Valid()
		{
			return !this.falseFromAngle && !this.falseFromDeployable && !this.falseFromTerrian && !this.falseFromClose && !this.falseFromHardpoint && !this.falseFromPlacementZone && !this.falseFromFitRequirements && !this.falseFromHittingNothing && !this.falseFromStructure;
		}

		// Token: 0x060033FD RID: 13309 RVA: 0x000C28B4 File Offset: 0x000C0AB4
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

		// Token: 0x04001B53 RID: 6995
		public bool falseFromDeployable;

		// Token: 0x04001B54 RID: 6996
		public bool falseFromTerrian;

		// Token: 0x04001B55 RID: 6997
		public bool falseFromClose;

		// Token: 0x04001B56 RID: 6998
		public bool falseFromHardpoint;

		// Token: 0x04001B57 RID: 6999
		public bool falseFromAngle;

		// Token: 0x04001B58 RID: 7000
		public bool falseFromPlacementZone;

		// Token: 0x04001B59 RID: 7001
		public bool falseFromFitRequirements;

		// Token: 0x04001B5A RID: 7002
		public bool falseFromHittingNothing;

		// Token: 0x04001B5B RID: 7003
		public bool falseFromStructure;
	}
}
