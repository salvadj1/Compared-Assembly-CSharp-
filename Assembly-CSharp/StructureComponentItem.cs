using System;
using uLink;
using UnityEngine;

// Token: 0x020006A5 RID: 1701
public abstract class StructureComponentItem<T> : global::HeldItem<T> where T : global::StructureComponentDataBlock
{
	// Token: 0x060039E3 RID: 14819 RVA: 0x000CBE40 File Offset: 0x000CA040
	protected StructureComponentItem(T db) : base(db)
	{
	}

	// Token: 0x060039E4 RID: 14820 RVA: 0x000CBE54 File Offset: 0x000CA054
	protected void RenderPlacementHelpers()
	{
		T datablock = this.datablock;
		global::StructureComponent structureToPlacePrefab = datablock.structureToPlacePrefab;
		this._master = null;
		this._placePos = Vector3.zero;
		this._placeRot = Quaternion.identity;
		this.validLocation = false;
		float axis = Input.GetAxis("Mouse ScrollWheel");
		if (axis > 0f)
		{
			this.desiredRotation *= Quaternion.AngleAxis(90f, Vector3.up);
		}
		else if (axis < 0f)
		{
			this.desiredRotation *= Quaternion.AngleAxis(-90f, Vector3.up);
		}
		global::Character character = base.character;
		if (character == null)
		{
			return;
		}
		Ray eyesRay = character.eyesRay;
		float num = (structureToPlacePrefab.type != global::StructureComponent.StructureComponentType.Ceiling) ? 8f : 4f;
		Vector3 vector = Vector3.zero;
		Vector3 vector2 = Vector3.up;
		Vector3 zero = Vector3.zero;
		RaycastHit raycastHit;
		bool flag;
		if (Physics.Raycast(eyesRay, ref raycastHit, num))
		{
			vector = raycastHit.point;
			vector2 = raycastHit.normal;
			flag = true;
		}
		else
		{
			flag = false;
			vector = eyesRay.origin + eyesRay.direction * num;
		}
		switch (structureToPlacePrefab.type)
		{
		case global::StructureComponent.StructureComponentType.Ceiling:
		case global::StructureComponent.StructureComponentType.Foundation:
		case global::StructureComponent.StructureComponentType.Ramp:
			vector.y -= 3.5f;
			break;
		}
		bool flag2 = false;
		bool flag3 = false;
		Vector3 placePos = vector;
		Quaternion placeRot = global::TransformHelpers.LookRotationForcedUp(character.eyesAngles.forward, Vector3.up) * this.desiredRotation;
		foreach (global::StructureMaster structureMaster in global::StructureMaster.RayTestStructures(eyesRay))
		{
			if (structureMaster)
			{
				int num2;
				int num3;
				int num4;
				structureMaster.GetStructureSize(out num2, out num3, out num4);
				this._placePos = global::StructureMaster.SnapToGrid(structureMaster.transform, vector, true);
				this._placeRot = global::TransformHelpers.LookRotationForcedUp(structureMaster.transform.forward, structureMaster.transform.transform.up) * this.desiredRotation;
				if (!flag3)
				{
					placePos = this._placePos;
					placeRot = this._placeRot;
					flag3 = true;
				}
				if (structureToPlacePrefab.CheckLocation(structureMaster, this._placePos, this._placeRot))
				{
					this._master = structureMaster;
					flag2 = true;
					break;
				}
			}
		}
		if (!flag2)
		{
			if (structureToPlacePrefab.type == global::StructureComponent.StructureComponentType.Foundation)
			{
				if (flag && raycastHit.collider is TerrainCollider)
				{
					bool flag4 = false;
					foreach (global::StructureMaster structureMaster2 in global::StructureMaster.AllStructuresWithBounds)
					{
						if (structureMaster2.containedBounds.Intersects(new Bounds(vector, new Vector3(5f, 5f, 4f))))
						{
							flag4 = true;
							break;
						}
					}
					if (!flag4)
					{
						this._placePos = vector;
						this._placeRot = global::TransformHelpers.LookRotationForcedUp(character.eyesAngles.forward, Vector3.up) * this.desiredRotation;
						this.validLocation = true;
					}
				}
				else
				{
					this._placePos = placePos;
					this._placeRot = placeRot;
					this.validLocation = false;
				}
			}
			else
			{
				this._placePos = placePos;
				this._placeRot = placeRot;
				this.validLocation = false;
			}
		}
		else
		{
			this.validLocation = true;
		}
		T datablock2 = this.datablock;
		if (!datablock2.CheckBlockers(this._placePos))
		{
			this.validLocation = false;
		}
		Color color = Color.red;
		if (this.validLocation)
		{
			color = Color.green;
		}
		color.a = 0.5f + Mathf.Abs(Mathf.Sin(Time.time * 8f)) * 0.25f;
		if (this._materialProps != null)
		{
			this._materialProps.Clear();
			this._materialProps.AddColor("_EmissionColor", color);
			this._materialProps.AddVector("_MainTex_ST", new Vector4(1f, 1f, 0f, Mathf.Repeat(Time.time, 30f)));
		}
		if (!this.validLocation)
		{
			this._placePos = vector;
		}
		this.RenderDeployPreview(this._placePos, this._placeRot);
	}

	// Token: 0x060039E5 RID: 14821 RVA: 0x000CC304 File Offset: 0x000CA504
	private void InformException(Exception e, string title, ref bool informedOnce, Object obj = null)
	{
		if (!informedOnce)
		{
			Debug.LogError(title + "\n" + e, obj);
			informedOnce = true;
		}
		else
		{
			Debug.LogException(e);
		}
	}

	// Token: 0x060039E6 RID: 14822 RVA: 0x000CC33C File Offset: 0x000CA53C
	public override void PreCameraRender()
	{
		try
		{
			this.RenderPlacementHelpers();
		}
		catch (Exception e)
		{
			this.InformException(e, "in PreCameraRender()", ref global::StructureComponentItem<T>.informedPreRender, null);
		}
	}

	// Token: 0x060039E7 RID: 14823 RVA: 0x000CC388 File Offset: 0x000CA588
	public virtual void RenderDeployPreview(Vector3 point, Quaternion rot)
	{
		if (this._prefabRenderer == null)
		{
			T datablock = this.datablock;
			global::StructureComponent structureToPlacePrefab = datablock.structureToPlacePrefab;
			if (!structureToPlacePrefab)
			{
				return;
			}
			this._prefabRenderer = global::PrefabRenderer.GetOrCreateRender(structureToPlacePrefab.gameObject);
			this._materialProps = new MaterialPropertyBlock();
		}
		Material overrideMat = this.datablock.overrideMat;
		if (overrideMat)
		{
			this._prefabRenderer.RenderOneMaterial(global::MountedCamera.main.camera, Matrix4x4.TRS(point, rot, Vector3.one), this._materialProps, overrideMat);
		}
		else
		{
			this._prefabRenderer.Render(global::MountedCamera.main.camera, Matrix4x4.TRS(point, rot, Vector3.one), this._materialProps, null);
		}
	}

	// Token: 0x060039E8 RID: 14824 RVA: 0x000CC450 File Offset: 0x000CA650
	public override void ItemPreFrame(ref global::HumanController.InputSample sample)
	{
		base.ItemPreFrame(ref sample);
		try
		{
			if (sample.attack2 && !this.lastFrameAttack2)
			{
				this.desiredRotation *= Quaternion.AngleAxis(90f, Vector3.up);
			}
			if (sample.attack && this.CanPlace())
			{
				this.DoPlace();
			}
			this.lastFrameAttack2 = sample.attack2;
		}
		catch (Exception e)
		{
			this.InformException(e, "in ItemPreFrame", ref global::StructureComponentItem<T>.informedPreFrame, null);
		}
	}

	// Token: 0x060039E9 RID: 14825 RVA: 0x000CC4FC File Offset: 0x000CA6FC
	public virtual void DoPlace()
	{
		this._nextPlaceTime = Time.time + 0.5f;
		global::Character character = base.character;
		if (character == null)
		{
			Debug.Log("NO char for placement");
			return;
		}
		Ray eyesRay = character.eyesRay;
		base.itemRepresentation.Action(1, 0, new object[]
		{
			eyesRay.origin,
			eyesRay.direction,
			this._placePos,
			this._placeRot,
			(!(this._master != null)) ? uLink.NetworkViewID.unassigned : this._master.networkView.viewID
		});
	}

	// Token: 0x060039EA RID: 14826 RVA: 0x000CC5C0 File Offset: 0x000CA7C0
	public bool IsValidLocation()
	{
		return false;
	}

	// Token: 0x060039EB RID: 14827 RVA: 0x000CC5C4 File Offset: 0x000CA7C4
	public virtual bool CanPlace()
	{
		return this.validLocation && this._nextPlaceTime <= Time.time;
	}

	// Token: 0x04001C74 RID: 7284
	protected bool validLocation;

	// Token: 0x04001C75 RID: 7285
	protected float _nextPlaceTime;

	// Token: 0x04001C76 RID: 7286
	protected global::StructureMaster _master;

	// Token: 0x04001C77 RID: 7287
	protected Vector3 _placePos;

	// Token: 0x04001C78 RID: 7288
	protected Quaternion _placeRot;

	// Token: 0x04001C79 RID: 7289
	protected global::PrefabRenderer _prefabRenderer;

	// Token: 0x04001C7A RID: 7290
	protected MaterialPropertyBlock _materialProps;

	// Token: 0x04001C7B RID: 7291
	protected bool lastFrameAttack2;

	// Token: 0x04001C7C RID: 7292
	public Quaternion desiredRotation = Quaternion.identity;

	// Token: 0x04001C7D RID: 7293
	private static bool informedPreRender;

	// Token: 0x04001C7E RID: 7294
	private static bool informedPreFrame;
}
