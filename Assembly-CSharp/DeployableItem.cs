using System;
using UnityEngine;

// Token: 0x02000681 RID: 1665
public abstract class DeployableItem<T> : global::HeldItem<T> where T : global::DeployableItemDataBlock
{
	// Token: 0x06003913 RID: 14611 RVA: 0x000CA30C File Offset: 0x000C850C
	protected DeployableItem(T db) : base(db)
	{
	}

	// Token: 0x06003914 RID: 14612 RVA: 0x000CA320 File Offset: 0x000C8520
	protected override void OnSetActive(bool isActive)
	{
		base.OnSetActive(isActive);
		if (isActive)
		{
			if (!this._aimHelper && this.datablock.aimGizmo)
			{
				this._aimHelper = this.datablock.aimGizmo.Create<global::GameGizmo.Instance>(out this._aimGizmo);
			}
		}
		else if (this._aimHelper)
		{
			this._aimHelper = !this._aimGizmo.gameGizmo.Destroy<global::GameGizmo.Instance>(ref this._aimGizmo);
		}
	}

	// Token: 0x06003915 RID: 14613 RVA: 0x000CA3B0 File Offset: 0x000C85B0
	public override void PreCameraRender()
	{
		if (!this._aimHelper)
		{
			return;
		}
		T datablock = this.datablock;
		Vector3 vector;
		Quaternion rot;
		global::TransCarrier carrier;
		bool flag = datablock.CheckPlacement(base.character.eyesRay, out vector, out rot, out carrier);
		Color color = (!flag) ? this._aimGizmo.gameGizmo.badColor : this._aimGizmo.gameGizmo.goodColor;
		this._aimGizmo.propertyBlock.Clear();
		this._aimGizmo.propertyBlock.AddColor("_EmissionColor", color);
		Vector4 vector2;
		vector2..ctor(1f, 1f, 0f, Mathf.Repeat(Time.time, 30f));
		this._aimGizmo.propertyBlock.AddVector("_MainTex_ST", vector2);
		this._aimGizmo.propertyBlock.AddVector("_GizmoWorldPos", vector);
		if (this._aimGizmo is global::GameGizmoWaveAnimation.Instance)
		{
			global::GameGizmoWaveAnimation.Instance instance = (global::GameGizmoWaveAnimation.Instance)this._aimGizmo;
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

	// Token: 0x06003916 RID: 14614 RVA: 0x000CA524 File Offset: 0x000C8724
	public virtual void RenderDeployPreview(Vector3 point, Quaternion rot, global::TransCarrier carrier)
	{
		if (this._aimGizmo != null)
		{
			this._aimGizmo.rotation = rot;
			this._aimGizmo.position = point;
		}
		if (this._prefabRenderer == null)
		{
			T datablock = this.datablock;
			global::DeployableObject objectToPlace = datablock.ObjectToPlace;
			if (!objectToPlace)
			{
				return;
			}
			this._prefabRenderer = global::PrefabRenderer.GetOrCreateRender(objectToPlace.gameObject);
		}
		Material overrideMat = this.datablock.overrideMat;
		if (overrideMat)
		{
			global::PrefabRenderer prefabRenderer = this._prefabRenderer;
			Camera camera = global::MountedCamera.main.camera;
			T datablock2 = this.datablock;
			prefabRenderer.RenderOneMaterial(camera, Matrix4x4.TRS(point, rot, datablock2.ObjectToPlace.transform.localScale), this._aimGizmo.propertyBlock, overrideMat);
		}
		else
		{
			global::PrefabRenderer prefabRenderer2 = this._prefabRenderer;
			Camera camera2 = global::MountedCamera.main.camera;
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

	// Token: 0x06003917 RID: 14615 RVA: 0x000CA6A8 File Offset: 0x000C88A8
	public override void ItemPreFrame(ref global::HumanController.InputSample sample)
	{
		base.ItemPreFrame(ref sample);
		if (!this._aimHelper)
		{
			return;
		}
		if (sample.attack && this.CanPlace())
		{
			global::Character character = base.inventory.idMain as global::Character;
			if (character && !character.stateFlags.grounded)
			{
				return;
			}
			this.DoPlace();
		}
	}

	// Token: 0x06003918 RID: 14616 RVA: 0x000CA714 File Offset: 0x000C8914
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
		global::TransCarrier transCarrier;
		bool flag = datablock2.CheckPlacement(base.character.eyesRay, out position, out rotation, out transCarrier);
		this._aimGizmo.rotation = rotation;
		this._aimGizmo.position = position;
		return flag && this._nextPlaceTime <= Time.time;
	}

	// Token: 0x06003919 RID: 14617 RVA: 0x000CA79C File Offset: 0x000C899C
	public virtual void DoPlace()
	{
		Ray eyesRay = base.character.eyesRay;
		T datablock = this.datablock;
		Vector3 vector;
		Quaternion quaternion;
		global::TransCarrier transCarrier;
		datablock.CheckPlacement(eyesRay, out vector, out quaternion, out transCarrier);
		this._nextPlaceTime = Time.time + 0.5f;
		base.itemRepresentation.Action(1, 0, new object[]
		{
			eyesRay.origin,
			eyesRay.direction
		});
	}

	// Token: 0x04001C3E RID: 7230
	protected float _nextPlaceTime = Time.time;

	// Token: 0x04001C3F RID: 7231
	protected global::PrefabRenderer _prefabRenderer;

	// Token: 0x04001C40 RID: 7232
	[NonSerialized]
	private bool _aimHelper;

	// Token: 0x04001C41 RID: 7233
	[NonSerialized]
	private global::GameGizmo.Instance _aimGizmo;
}
