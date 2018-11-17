using System;
using UnityEngine;

// Token: 0x020005C3 RID: 1475
public abstract class DeployableItem<T> : HeldItem<T> where T : DeployableItemDataBlock
{
	// Token: 0x0600354B RID: 13643 RVA: 0x000C20B0 File Offset: 0x000C02B0
	protected DeployableItem(T db) : base(db)
	{
	}

	// Token: 0x0600354C RID: 13644 RVA: 0x000C20C4 File Offset: 0x000C02C4
	protected override void OnSetActive(bool isActive)
	{
		base.OnSetActive(isActive);
		if (isActive)
		{
			if (!this._aimHelper && this.datablock.aimGizmo)
			{
				this._aimHelper = this.datablock.aimGizmo.Create<GameGizmo.Instance>(out this._aimGizmo);
			}
		}
		else if (this._aimHelper)
		{
			this._aimHelper = !this._aimGizmo.gameGizmo.Destroy<GameGizmo.Instance>(ref this._aimGizmo);
		}
	}

	// Token: 0x0600354D RID: 13645 RVA: 0x000C2154 File Offset: 0x000C0354
	public override void PreCameraRender()
	{
		if (!this._aimHelper)
		{
			return;
		}
		T datablock = this.datablock;
		Vector3 vector;
		Quaternion rot;
		TransCarrier carrier;
		bool flag = datablock.CheckPlacement(base.character.eyesRay, out vector, out rot, out carrier);
		Color color = (!flag) ? this._aimGizmo.gameGizmo.badColor : this._aimGizmo.gameGizmo.goodColor;
		this._aimGizmo.propertyBlock.Clear();
		this._aimGizmo.propertyBlock.AddColor("_EmissionColor", color);
		Vector4 vector2;
		vector2..ctor(1f, 1f, 0f, Mathf.Repeat(Time.time, 30f));
		this._aimGizmo.propertyBlock.AddVector("_MainTex_ST", vector2);
		this._aimGizmo.propertyBlock.AddVector("_GizmoWorldPos", vector);
		if (this._aimGizmo is GameGizmoWaveAnimation.Instance)
		{
			GameGizmoWaveAnimation.Instance instance = (GameGizmoWaveAnimation.Instance)this._aimGizmo;
			if (flag)
			{
				instance.propertyBlock.AddFloat("_PushOut", (float)instance.value);
			}
			else
			{
				instance.propertyBlock.AddFloat("_PushIn", (float)instance.value);
				instance.propertyBlock.AddFloat("_PushOut", (float)(-(float)instance.value));
			}
		}
		this.RenderDeployPreview(vector, rot, carrier);
		this._aimGizmo.Render();
	}

	// Token: 0x0600354E RID: 13646 RVA: 0x000C22C8 File Offset: 0x000C04C8
	public virtual void RenderDeployPreview(Vector3 point, Quaternion rot, TransCarrier carrier)
	{
		if (this._aimGizmo != null)
		{
			this._aimGizmo.rotation = rot;
			this._aimGizmo.position = point;
		}
		if (this._prefabRenderer == null)
		{
			T datablock = this.datablock;
			DeployableObject objectToPlace = datablock.ObjectToPlace;
			if (!objectToPlace)
			{
				return;
			}
			this._prefabRenderer = PrefabRenderer.GetOrCreateRender(objectToPlace.gameObject);
		}
		Material overrideMat = this.datablock.overrideMat;
		if (overrideMat)
		{
			PrefabRenderer prefabRenderer = this._prefabRenderer;
			Camera camera = MountedCamera.main.camera;
			T datablock2 = this.datablock;
			prefabRenderer.RenderOneMaterial(camera, Matrix4x4.TRS(point, rot, datablock2.ObjectToPlace.transform.localScale), this._aimGizmo.propertyBlock, overrideMat);
		}
		else
		{
			PrefabRenderer prefabRenderer2 = this._prefabRenderer;
			Camera camera2 = MountedCamera.main.camera;
			T datablock3 = this.datablock;
			prefabRenderer2.Render(camera2, Matrix4x4.TRS(point, rot, datablock3.ObjectToPlace.transform.localScale), this._aimGizmo.propertyBlock, null);
		}
		if (this._aimGizmo != null)
		{
			bool flag = false;
			if (carrier)
			{
				Renderer renderer = carrier.renderer;
				if (renderer is MeshRenderer && renderer && renderer.enabled)
				{
					flag = true;
					this._aimGizmo.carrierRenderer = (MeshRenderer)renderer;
				}
			}
			if (!flag)
			{
				this._aimGizmo.carrierRenderer = null;
			}
		}
	}

	// Token: 0x0600354F RID: 13647 RVA: 0x000C244C File Offset: 0x000C064C
	public override void ItemPreFrame(ref HumanController.InputSample sample)
	{
		base.ItemPreFrame(ref sample);
		if (!this._aimHelper)
		{
			return;
		}
		if (sample.attack && this.CanPlace())
		{
			Character character = base.inventory.idMain as Character;
			if (character && !character.stateFlags.grounded)
			{
				return;
			}
			this.DoPlace();
		}
	}

	// Token: 0x06003550 RID: 13648 RVA: 0x000C24B8 File Offset: 0x000C06B8
	public virtual bool CanPlace()
	{
		T datablock = this.datablock;
		if (datablock.ObjectToPlace == null)
		{
			return false;
		}
		T datablock2 = this.datablock;
		Vector3 position;
		Quaternion rotation;
		TransCarrier transCarrier;
		bool flag = datablock2.CheckPlacement(base.character.eyesRay, out position, out rotation, out transCarrier);
		this._aimGizmo.rotation = rotation;
		this._aimGizmo.position = position;
		return flag && this._nextPlaceTime <= Time.time;
	}

	// Token: 0x06003551 RID: 13649 RVA: 0x000C2540 File Offset: 0x000C0740
	public virtual void DoPlace()
	{
		Ray eyesRay = base.character.eyesRay;
		T datablock = this.datablock;
		Vector3 vector;
		Quaternion quaternion;
		TransCarrier transCarrier;
		datablock.CheckPlacement(eyesRay, out vector, out quaternion, out transCarrier);
		this._nextPlaceTime = Time.time + 0.5f;
		base.itemRepresentation.Action(1, 0, new object[]
		{
			eyesRay.origin,
			eyesRay.direction
		});
	}

	// Token: 0x04001A6D RID: 6765
	protected float _nextPlaceTime = Time.time;

	// Token: 0x04001A6E RID: 6766
	protected PrefabRenderer _prefabRenderer;

	// Token: 0x04001A6F RID: 6767
	[NonSerialized]
	private bool _aimHelper;

	// Token: 0x04001A70 RID: 6768
	[NonSerialized]
	private GameGizmo.Instance _aimGizmo;
}
