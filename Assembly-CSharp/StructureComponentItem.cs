using System;
using uLink;
using UnityEngine;

// Token: 0x020005E7 RID: 1511
public abstract class StructureComponentItem<T> : HeldItem<T> where T : StructureComponentDataBlock
{
	// Token: 0x0600361B RID: 13851 RVA: 0x000C3BE4 File Offset: 0x000C1DE4
	protected StructureComponentItem(T db) : base(db)
	{
	}

	// Token: 0x0600361C RID: 13852 RVA: 0x000C3BF8 File Offset: 0x000C1DF8
	protected void RenderPlacementHelpers()
	{
		T datablock = this.datablock;
		StructureComponent structureToPlacePrefab = datablock.structureToPlacePrefab;
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
		Character character = base.character;
		if (character == null)
		{
			return;
		}
		Ray eyesRay = character.eyesRay;
		float num = (structureToPlacePrefab.type != StructureComponent.StructureComponentType.Ceiling) ? 8f : 4f;
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
		case StructureComponent.StructureComponentType.Ceiling:
		case StructureComponent.StructureComponentType.Foundation:
		case StructureComponent.StructureComponentType.Ramp:
			vector.y -= 3.5f;
			break;
		}
		bool flag2 = false;
		bool flag3 = false;
		Vector3 placePos = vector;
		Quaternion placeRot = TransformHelpers.LookRotationForcedUp(character.eyesAngles.forward, Vector3.up) * this.desiredRotation;
		foreach (StructureMaster structureMaster in StructureMaster.RayTestStructures(eyesRay))
		{
			if (structureMaster)
			{
				int num2;
				int num3;
				int num4;
				structureMaster.GetStructureSize(out num2, out num3, out num4);
				this._placePos = StructureMaster.SnapToGrid(structureMaster.transform, vector, true);
				this._placeRot = TransformHelpers.LookRotationForcedUp(structureMaster.transform.forward, structureMaster.transform.transform.up) * this.desiredRotation;
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
			if (structureToPlacePrefab.type == StructureComponent.StructureComponentType.Foundation)
			{
				if (flag && raycastHit.collider is TerrainCollider)
				{
					bool flag4 = false;
					foreach (StructureMaster structureMaster2 in StructureMaster.AllStructuresWithBounds)
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
						this._placeRot = TransformHelpers.LookRotationForcedUp(character.eyesAngles.forward, Vector3.up) * this.desiredRotation;
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

	// Token: 0x0600361D RID: 13853 RVA: 0x000C40A8 File Offset: 0x000C22A8
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

	// Token: 0x0600361E RID: 13854 RVA: 0x000C40E0 File Offset: 0x000C22E0
	public override void PreCameraRender()
	{
		try
		{
			this.RenderPlacementHelpers();
		}
		catch (Exception e)
		{
			this.InformException(e, "in PreCameraRender()", ref StructureComponentItem<T>.informedPreRender, null);
		}
	}

	// Token: 0x0600361F RID: 13855 RVA: 0x000C412C File Offset: 0x000C232C
	public virtual void RenderDeployPreview(Vector3 point, Quaternion rot)
	{
		if (this._prefabRenderer == null)
		{
			T datablock = this.datablock;
			StructureComponent structureToPlacePrefab = datablock.structureToPlacePrefab;
			if (!structureToPlacePrefab)
			{
				return;
			}
			this._prefabRenderer = PrefabRenderer.GetOrCreateRender(structureToPlacePrefab.gameObject);
			this._materialProps = new MaterialPropertyBlock();
		}
		Material overrideMat = this.datablock.overrideMat;
		if (overrideMat)
		{
			this._prefabRenderer.RenderOneMaterial(MountedCamera.main.camera, Matrix4x4.TRS(point, rot, Vector3.one), this._materialProps, overrideMat);
		}
		else
		{
			this._prefabRenderer.Render(MountedCamera.main.camera, Matrix4x4.TRS(point, rot, Vector3.one), this._materialProps, null);
		}
	}

	// Token: 0x06003620 RID: 13856 RVA: 0x000C41F4 File Offset: 0x000C23F4
	public override void ItemPreFrame(ref HumanController.InputSample sample)
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
			this.InformException(e, "in ItemPreFrame", ref StructureComponentItem<T>.informedPreFrame, null);
		}
	}

	// Token: 0x06003621 RID: 13857 RVA: 0x000C42A0 File Offset: 0x000C24A0
	public virtual void DoPlace()
	{
		this._nextPlaceTime = Time.time + 0.5f;
		Character character = base.character;
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
			(!(this._master != null)) ? NetworkViewID.unassigned : this._master.networkView.viewID
		});
	}

	// Token: 0x06003622 RID: 13858 RVA: 0x000C4364 File Offset: 0x000C2564
	public bool IsValidLocation()
	{
		return false;
	}

	// Token: 0x06003623 RID: 13859 RVA: 0x000C4368 File Offset: 0x000C2568
	public virtual bool CanPlace()
	{
		return this.validLocation && this._nextPlaceTime <= Time.time;
	}

	// Token: 0x04001AA3 RID: 6819
	protected bool validLocation;

	// Token: 0x04001AA4 RID: 6820
	protected float _nextPlaceTime;

	// Token: 0x04001AA5 RID: 6821
	protected StructureMaster _master;

	// Token: 0x04001AA6 RID: 6822
	protected Vector3 _placePos;

	// Token: 0x04001AA7 RID: 6823
	protected Quaternion _placeRot;

	// Token: 0x04001AA8 RID: 6824
	protected PrefabRenderer _prefabRenderer;

	// Token: 0x04001AA9 RID: 6825
	protected MaterialPropertyBlock _materialProps;

	// Token: 0x04001AAA RID: 6826
	protected bool lastFrameAttack2;

	// Token: 0x04001AAB RID: 6827
	public Quaternion desiredRotation = Quaternion.identity;

	// Token: 0x04001AAC RID: 6828
	private static bool informedPreRender;

	// Token: 0x04001AAD RID: 6829
	private static bool informedPreFrame;
}
