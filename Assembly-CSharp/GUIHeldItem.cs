using System;
using UnityEngine;

// Token: 0x02000495 RID: 1173
public class GUIHeldItem : MonoBehaviour
{
	// Token: 0x0600286F RID: 10351 RVA: 0x0009384C File Offset: 0x00091A4C
	public static global::GUIHeldItem Get()
	{
		return global::GUIHeldItem._guiHeldItem;
	}

	// Token: 0x06002870 RID: 10352 RVA: 0x00093854 File Offset: 0x00091A54
	public static global::IInventoryItem CurrentItem()
	{
		return global::GUIHeldItem.Get()._itemHolding;
	}

	// Token: 0x06002871 RID: 10353 RVA: 0x00093860 File Offset: 0x00091A60
	private void Start()
	{
		this.startingIconColor = this._icon.color;
		this._icon.enabled = false;
		global::GUIHeldItem._guiHeldItem = this;
		this._myMaterial = this._icon.material.Clone();
		this._icon.material = this._myMaterial;
		this.mTrans = base.transform;
		if (this.uiCamera == null)
		{
			this.uiCamera = global::NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		this.planeTest = new Plane(this.uiCamera.transform.forward * 1f, new Vector3(0f, 0f, 2f));
		this.started = true;
	}

	// Token: 0x06002872 RID: 10354 RVA: 0x0009392C File Offset: 0x00091B2C
	private void OnDestroy()
	{
		Object.Destroy(this._myMaterial);
	}

	// Token: 0x06002873 RID: 10355 RVA: 0x0009393C File Offset: 0x00091B3C
	private void SetPosition(Vector3 world)
	{
		Vector3 localPosition = this.mTrans.localPosition + this.mTrans.InverseTransformPoint(world);
		localPosition.z = -190f;
		this.mTrans.localPosition = localPosition;
	}

	// Token: 0x06002874 RID: 10356 RVA: 0x00093980 File Offset: 0x00091B80
	private void Update()
	{
		if (this.hasItem)
		{
			Vector3 vector = global::UICamera.lastMousePosition + this.offsetPoint;
			Ray ray = this.uiCamera.ScreenPointToRay(vector);
			float num = 0f;
			if (this.planeTest.Raycast(ray, ref num))
			{
				this.SetPosition(ray.GetPoint(num));
			}
			this.offsetPoint = Vector3.SmoothDamp(this.offsetPoint, Vector3.zero, ref this.offsetVelocity, 0.06f, 600f);
		}
		else if (this.fadingOut)
		{
			this.fadeOutPoint = Vector3.SmoothDamp(this.fadeOutPoint, this.fadeOutPointEnd, ref this.fadeOutVelocity, 0.1f, 50f);
			this.fadeOutAlpha = this.startingIconColor.a * (1f - Mathf.Clamp01(Mathf.Abs(Vector3.Dot(this.fadeOutPointNormal, this.fadeOutPoint) - this.fadeOutPointDistance) / this.fadeOutPointMagnitude));
			if ((double)this.fadeOutAlpha <= 0.00390625)
			{
				this.fadingOut = false;
				this.MakeEmpty();
			}
			else
			{
				Color color = this._icon.color;
				this.SetPosition(this.fadeOutPoint);
				color.a = this.fadeOutAlpha;
				this._icon.color = color;
			}
		}
	}

	// Token: 0x06002875 RID: 10357 RVA: 0x00093AE8 File Offset: 0x00091CE8
	private void Opaque()
	{
		this.fadeOutAlpha = 1f;
		this.fadeOutPointStart = Vector3.zero;
		this.fadeOutPointEnd = Vector3.right;
		this.fadeOutPointDistance = 1f;
		this.fadeOutPointMagnitude = 1f;
		this.fadeOutPointNormal = Vector3.right;
		this.fadeOutVelocity = Vector3.zero;
		this.fadingOut = false;
		if (this.started)
		{
			this._icon.color = this.startingIconColor;
		}
	}

	// Token: 0x06002876 RID: 10358 RVA: 0x00093B68 File Offset: 0x00091D68
	public bool SetHeldItem(global::IInventoryItem item)
	{
		if (item == null)
		{
			this.MakeEmpty();
			if (!this.fadingOut)
			{
				this.Opaque();
			}
			return false;
		}
		this.hasItem = true;
		Texture iconTex = item.datablock.iconTex;
		global::ItemDataBlock.LoadIconOrUnknown<Texture>(item.datablock.icon, ref iconTex);
		this._icon.enabled = true;
		this._myMaterial.Set("_MainTex", iconTex);
		this._itemHolding = item;
		this.offsetVelocity = (this.offsetPoint = default(Vector2));
		this.Opaque();
		return true;
	}

	// Token: 0x06002877 RID: 10359 RVA: 0x00093C04 File Offset: 0x00091E04
	public bool SetHeldItem(global::RPOSInventoryCell cell)
	{
		global::IInventoryItem heldItem;
		if (cell)
		{
			global::IInventoryItem slotItem = cell.slotItem;
			heldItem = slotItem;
		}
		else
		{
			heldItem = null;
		}
		if (this.SetHeldItem(heldItem))
		{
			try
			{
				Vector3 vector;
				if (global::NGUITools.GetCentroid(cell, out vector))
				{
					Vector2 vector2 = global::UICamera.FindCameraForLayer(cell.gameObject.layer).cachedCamera.WorldToScreenPoint(vector);
					this.offsetPoint = vector2 - global::UICamera.lastMousePosition;
				}
			}
			catch
			{
				this.offsetPoint = Vector3.zero;
			}
			return true;
		}
		return false;
	}

	// Token: 0x06002878 RID: 10360 RVA: 0x00093CB0 File Offset: 0x00091EB0
	public void FadeOutToPoint(Vector3 worldPoint)
	{
		this.Opaque();
		this.fadeOutPointStart = this.mTrans.position;
		this.fadeOutPointEnd = new Vector3(worldPoint.x, worldPoint.y, worldPoint.z);
		if (this.fadeOutPointStart == this.fadeOutPointEnd)
		{
			this.fadeOutPointEnd.z = this.fadeOutPointEnd.z + 1f;
		}
		this.fadeOutPointNormal = this.fadeOutPointEnd - this.fadeOutPointStart;
		this.fadeOutPointMagnitude = this.fadeOutPointNormal.magnitude;
		this.fadeOutPointNormal /= this.fadeOutPointMagnitude;
		this.fadeOutPointDistance = Vector3.Dot(this.fadeOutPointNormal, this.fadeOutPointStart);
		this.fadeOutAlpha = 1f;
		this.fadingOut = true;
		this._icon.enabled = true;
		this.fadeOutPoint = this.fadeOutPointStart;
	}

	// Token: 0x06002879 RID: 10361 RVA: 0x00093DA4 File Offset: 0x00091FA4
	public void ClearHeldItem()
	{
		if (this.hasItem)
		{
			this.SetHeldItem(null);
			if (!this.fadingOut)
			{
				this.Opaque();
			}
		}
	}

	// Token: 0x0600287A RID: 10362 RVA: 0x00093DD8 File Offset: 0x00091FD8
	public void ClearHeldItem(global::RPOSInventoryCell fadeToCell)
	{
		if (this.hasItem)
		{
			this.fadingOut = true;
			this.ClearHeldItem();
			try
			{
				Vector3 worldPoint;
				if (global::NGUITools.GetCentroid(fadeToCell, out worldPoint))
				{
					this.FadeOutToPoint(worldPoint);
				}
				return;
			}
			catch
			{
			}
			this.Opaque();
		}
	}

	// Token: 0x0600287B RID: 10363 RVA: 0x00093E44 File Offset: 0x00092044
	private void MakeEmpty()
	{
		if (this._icon)
		{
			this._icon.enabled = false;
		}
		this._itemHolding = null;
		this.hasItem = false;
	}

	// Token: 0x04001356 RID: 4950
	private const float kOffsetSpeed = 600f;

	// Token: 0x04001357 RID: 4951
	private const float kFadeSpeed = 50f;

	// Token: 0x04001358 RID: 4952
	private const float kOffsetSmoothTime = 0.06f;

	// Token: 0x04001359 RID: 4953
	private const float kFadeSmoothTime = 0.1f;

	// Token: 0x0400135A RID: 4954
	private static global::GUIHeldItem _guiHeldItem;

	// Token: 0x0400135B RID: 4955
	public global::UITexture _icon;

	// Token: 0x0400135C RID: 4956
	private global::UIMaterial _myMaterial;

	// Token: 0x0400135D RID: 4957
	public Camera uiCamera;

	// Token: 0x0400135E RID: 4958
	private Transform mTrans;

	// Token: 0x0400135F RID: 4959
	private Plane planeTest;

	// Token: 0x04001360 RID: 4960
	private global::IInventoryItem _itemHolding;

	// Token: 0x04001361 RID: 4961
	private Vector3 offsetPoint;

	// Token: 0x04001362 RID: 4962
	private Vector3 offsetVelocity;

	// Token: 0x04001363 RID: 4963
	private float lastTime;

	// Token: 0x04001364 RID: 4964
	private bool hasItem;

	// Token: 0x04001365 RID: 4965
	private bool fadingOut;

	// Token: 0x04001366 RID: 4966
	private Vector3 fadeOutPointStart;

	// Token: 0x04001367 RID: 4967
	private Vector3 fadeOutPointEnd;

	// Token: 0x04001368 RID: 4968
	private Vector3 fadeOutPoint;

	// Token: 0x04001369 RID: 4969
	private Vector3 fadeOutVelocity;

	// Token: 0x0400136A RID: 4970
	private Vector3 fadeOutPointNormal;

	// Token: 0x0400136B RID: 4971
	private float fadeOutPointDistance;

	// Token: 0x0400136C RID: 4972
	private float fadeOutPointMagnitude;

	// Token: 0x0400136D RID: 4973
	private float fadeOutAlpha;

	// Token: 0x0400136E RID: 4974
	private Color startingIconColor = Color.white;

	// Token: 0x0400136F RID: 4975
	private bool started;
}
