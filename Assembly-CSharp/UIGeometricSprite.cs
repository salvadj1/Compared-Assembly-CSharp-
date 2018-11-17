using System;
using NGUI.Meshing;
using NGUI.Structures;
using UnityEngine;

// Token: 0x020008E1 RID: 2273
public class UIGeometricSprite : global::UISprite
{
	// Token: 0x06004D62 RID: 19810 RVA: 0x00133B80 File Offset: 0x00131D80
	protected UIGeometricSprite(global::UIWidget.WidgetFlags additionalFlags) : base(additionalFlags)
	{
	}

	// Token: 0x17000EBC RID: 3772
	// (get) Token: 0x06004D63 RID: 19811 RVA: 0x00133B9C File Offset: 0x00131D9C
	public Rect innerUV
	{
		get
		{
			this.UpdateUVs(false);
			return this.mInnerUV;
		}
	}

	// Token: 0x17000EBD RID: 3773
	// (get) Token: 0x06004D64 RID: 19812 RVA: 0x00133BAC File Offset: 0x00131DAC
	// (set) Token: 0x06004D65 RID: 19813 RVA: 0x00133BB4 File Offset: 0x00131DB4
	public bool fillCenter
	{
		get
		{
			return this.mFillCenter;
		}
		set
		{
			if (this.mFillCenter != value)
			{
				this.mFillCenter = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x06004D66 RID: 19814 RVA: 0x00133BD0 File Offset: 0x00131DD0
	public override void UpdateUVs(bool force)
	{
		if (base.cachedTransform.localScale != this.mScale)
		{
			this.mScale = base.cachedTransform.localScale;
			base.ChangedAuto();
		}
		if (base.sprite != null && (force || this.mInner != this.mSprite.inner || this.mOuter != this.mSprite.outer))
		{
			Texture mainTexture = base.mainTexture;
			if (mainTexture != null)
			{
				this.mInner = this.mSprite.inner;
				this.mOuter = this.mSprite.outer;
				this.mInnerUV = this.mInner;
				this.mOuterUV = this.mOuter;
				if (base.atlas.coordinates == global::UIAtlas.Coordinates.Pixels)
				{
					this.mOuterUV = global::NGUIMath.ConvertToTexCoords(this.mOuterUV, mainTexture.width, mainTexture.height);
					this.mInnerUV = global::NGUIMath.ConvertToTexCoords(this.mInnerUV, mainTexture.width, mainTexture.height);
				}
			}
		}
	}

	// Token: 0x06004D67 RID: 19815 RVA: 0x00133CF0 File Offset: 0x00131EF0
	public override void MakePixelPerfect()
	{
		Vector3 localPosition = base.cachedTransform.localPosition;
		localPosition.x = (float)Mathf.RoundToInt(localPosition.x);
		localPosition.y = (float)Mathf.RoundToInt(localPosition.y);
		localPosition.z = (float)Mathf.RoundToInt(localPosition.z);
		base.cachedTransform.localPosition = localPosition;
		Vector3 localScale = base.cachedTransform.localScale;
		localScale.x = (float)(Mathf.RoundToInt(localScale.x * 0.5f) << 1);
		localScale.y = (float)(Mathf.RoundToInt(localScale.y * 0.5f) << 1);
		localScale.z = 1f;
		base.cachedTransform.localScale = localScale;
	}

	// Token: 0x06004D68 RID: 19816 RVA: 0x00133DB0 File Offset: 0x00131FB0
	public override void OnFill(NGUI.Meshing.MeshBuffer m)
	{
		if (this.mOuterUV == this.mInnerUV)
		{
			base.OnFill(m);
			return;
		}
		NGUI.Structures.float3 @float = default(NGUI.Structures.float3);
		@float.xyz = base.cachedTransform.localScale;
		Vector4 vector;
		vector.x = this.mOuterUV.xMin;
		vector.y = this.mInnerUV.xMin;
		vector.z = this.mInnerUV.xMax;
		vector.w = this.mOuterUV.xMax;
		Vector4 vector2;
		vector2.x = this.mOuterUV.yMin;
		vector2.y = this.mInnerUV.yMin;
		vector2.z = this.mInnerUV.yMax;
		vector2.w = this.mOuterUV.yMax;
		NGUI.Structures.NineRectangle nineRectangle;
		NGUI.Structures.NineRectangle nineRectangle2;
		NGUI.Structures.NineRectangle.Calculate(base.pivot, base.atlas.pixelSize, base.mainTexture, ref vector, ref vector2, ref @float.xy, out nineRectangle, out nineRectangle2);
		Color color = base.color;
		if (this.mFillCenter)
		{
			NGUI.Structures.NineRectangle.Fill9(ref nineRectangle, ref nineRectangle2, ref color, m);
		}
		else
		{
			NGUI.Structures.NineRectangle.Fill8(ref nineRectangle, ref nineRectangle2, ref color, m);
		}
	}

	// Token: 0x04002B54 RID: 11092
	[SerializeField]
	[HideInInspector]
	private bool mFillCenter = true;

	// Token: 0x04002B55 RID: 11093
	protected Rect mInner;

	// Token: 0x04002B56 RID: 11094
	protected Rect mInnerUV;

	// Token: 0x04002B57 RID: 11095
	protected Vector3 mScale = Vector3.one;
}
