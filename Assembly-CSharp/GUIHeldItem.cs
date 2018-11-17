using System;
using UnityEngine;

// Token: 0x020003E5 RID: 997
public class GUIHeldItem : MonoBehaviour
{
	// Token: 0x060024FD RID: 9469 RVA: 0x0008DE60 File Offset: 0x0008C060
	public static GUIHeldItem Get()
	{
		return GUIHeldItem._guiHeldItem;
	}

	// Token: 0x060024FE RID: 9470 RVA: 0x0008DE68 File Offset: 0x0008C068
	public static IInventoryItem CurrentItem()
	{
		return GUIHeldItem.Get()._itemHolding;
	}

	// Token: 0x060024FF RID: 9471 RVA: 0x0008DE74 File Offset: 0x0008C074
	private void Start()
	{
		this.startingIconColor = this._icon.color;
		this._icon.enabled = false;
		GUIHeldItem._guiHeldItem = this;
		this._myMaterial = this._icon.material.Clone();
		this._icon.material = this._myMaterial;
		this.mTrans = base.transform;
		if (this.uiCamera == null)
		{
			this.uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		this.planeTest = new Plane(this.uiCamera.transform.forward * 1f, new Vector3(0f, 0f, 2f));
		this.started = true;
	}

	// Token: 0x06002500 RID: 9472 RVA: 0x0008DF40 File Offset: 0x0008C140
	private void OnDestroy()
	{
		Object.Destroy(this._myMaterial);
	}

	// Token: 0x06002501 RID: 9473 RVA: 0x0008DF50 File Offset: 0x0008C150
	private void SetPosition(Vector3 world)
	{
		Vector3 localPosition = this.mTrans.localPosition + this.mTrans.InverseTransformPoint(world);
		localPosition.z = -190f;
		this.mTrans.localPosition = localPosition;
	}

	// Token: 0x06002502 RID: 9474 RVA: 0x0008DF94 File Offset: 0x0008C194
	private void Update()
	{
		if (this.hasItem)
		{
			Vector3 vector = UICamera.lastMousePosition + this.offsetPoint;
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

	// Token: 0x06002503 RID: 9475 RVA: 0x0008E0FC File Offset: 0x0008C2FC
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

	// Token: 0x06002504 RID: 9476 RVA: 0x0008E17C File Offset: 0x0008C37C
	public bool SetHeldItem(IInventoryItem item)
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
		ItemDataBlock.LoadIconOrUnknown<Texture>(item.datablock.icon, ref iconTex);
		this._icon.enabled = true;
		this._myMaterial.Set("_MainTex", iconTex);
		this._itemHolding = item;
		this.offsetVelocity = (this.offsetPoint = default(Vector2));
		this.Opaque();
		return true;
	}

	// Token: 0x06002505 RID: 9477 RVA: 0x0008E218 File Offset: 0x0008C418
	public bool SetHeldItem(RPOSInventoryCell cell)
	{
		IInventoryItem heldItem;
		if (cell)
		{
			IInventoryItem slotItem = cell.slotItem;
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
				if (NGUITools.GetCentroid(cell, out vector))
				{
					Vector2 vector2 = UICamera.FindCameraForLayer(cell.gameObject.layer).cachedCamera.WorldToScreenPoint(vector);
					this.offsetPoint = vector2 - UICamera.lastMousePosition;
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

	// Token: 0x06002506 RID: 9478 RVA: 0x0008E2C4 File Offset: 0x0008C4C4
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

	// Token: 0x06002507 RID: 9479 RVA: 0x0008E3B8 File Offset: 0x0008C5B8
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

	// Token: 0x06002508 RID: 9480 RVA: 0x0008E3EC File Offset: 0x0008C5EC
	public void ClearHeldItem(RPOSInventoryCell fadeToCell)
	{
		if (this.hasItem)
		{
			this.fadingOut = true;
			this.ClearHeldItem();
			try
			{
				Vector3 worldPoint;
				if (NGUITools.GetCentroid(fadeToCell, out worldPoint))
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

	// Token: 0x06002509 RID: 9481 RVA: 0x0008E458 File Offset: 0x0008C658
	private void MakeEmpty()
	{
		if (this._icon)
		{
			this._icon.enabled = false;
		}
		this._itemHolding = null;
		this.hasItem = false;
	}

	// Token: 0x040011DC RID: 4572
	private const float kOffsetSpeed = 600f;

	// Token: 0x040011DD RID: 4573
	private const float kFadeSpeed = 50f;

	// Token: 0x040011DE RID: 4574
	private const float kOffsetSmoothTime = 0.06f;

	// Token: 0x040011DF RID: 4575
	private const float kFadeSmoothTime = 0.1f;

	// Token: 0x040011E0 RID: 4576
	private static GUIHeldItem _guiHeldItem;

	// Token: 0x040011E1 RID: 4577
	public UITexture _icon;

	// Token: 0x040011E2 RID: 4578
	private UIMaterial _myMaterial;

	// Token: 0x040011E3 RID: 4579
	public Camera uiCamera;

	// Token: 0x040011E4 RID: 4580
	private Transform mTrans;

	// Token: 0x040011E5 RID: 4581
	private Plane planeTest;

	// Token: 0x040011E6 RID: 4582
	private IInventoryItem _itemHolding;

	// Token: 0x040011E7 RID: 4583
	private Vector3 offsetPoint;

	// Token: 0x040011E8 RID: 4584
	private Vector3 offsetVelocity;

	// Token: 0x040011E9 RID: 4585
	private float lastTime;

	// Token: 0x040011EA RID: 4586
	private bool hasItem;

	// Token: 0x040011EB RID: 4587
	private bool fadingOut;

	// Token: 0x040011EC RID: 4588
	private Vector3 fadeOutPointStart;

	// Token: 0x040011ED RID: 4589
	private Vector3 fadeOutPointEnd;

	// Token: 0x040011EE RID: 4590
	private Vector3 fadeOutPoint;

	// Token: 0x040011EF RID: 4591
	private Vector3 fadeOutVelocity;

	// Token: 0x040011F0 RID: 4592
	private Vector3 fadeOutPointNormal;

	// Token: 0x040011F1 RID: 4593
	private float fadeOutPointDistance;

	// Token: 0x040011F2 RID: 4594
	private float fadeOutPointMagnitude;

	// Token: 0x040011F3 RID: 4595
	private float fadeOutAlpha;

	// Token: 0x040011F4 RID: 4596
	private Color startingIconColor = Color.white;

	// Token: 0x040011F5 RID: 4597
	private bool started;
}
