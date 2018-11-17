using System;
using NGUI.Meshing;
using NGUI.Structures;
using UnityEngine;

// Token: 0x020007F0 RID: 2032
public class UIGeometricSprite : UISprite
{
	// Token: 0x060048B7 RID: 18615 RVA: 0x00129C1C File Offset: 0x00127E1C
	protected UIGeometricSprite(UIWidget.WidgetFlags additionalFlags) : base(additionalFlags)
	{
	}

	// Token: 0x17000E22 RID: 3618
	// (get) Token: 0x060048B8 RID: 18616 RVA: 0x00129C38 File Offset: 0x00127E38
	public Rect innerUV
	{
		get
		{
			this.UpdateUVs(false);
			return this.mInnerUV;
		}
	}

	// Token: 0x17000E23 RID: 3619
	// (get) Token: 0x060048B9 RID: 18617 RVA: 0x00129C48 File Offset: 0x00127E48
	// (set) Token: 0x060048BA RID: 18618 RVA: 0x00129C50 File Offset: 0x00127E50
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

	// Token: 0x060048BB RID: 18619 RVA: 0x00129C6C File Offset: 0x00127E6C
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
				if (base.atlas.coordinates == UIAtlas.Coordinates.Pixels)
				{
					this.mOuterUV = NGUIMath.ConvertToTexCoords(this.mOuterUV, mainTexture.width, mainTexture.height);
					this.mInnerUV = NGUIMath.ConvertToTexCoords(this.mInnerUV, mainTexture.width, mainTexture.height);
				}
			}
		}
	}

	// Token: 0x060048BC RID: 18620 RVA: 0x00129D8C File Offset: 0x00127F8C
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

	// Token: 0x060048BD RID: 18621 RVA: 0x00129E4C File Offset: 0x0012804C
	public override void OnFill(MeshBuffer m)
	{
		if (this.mOuterUV == this.mInnerUV)
		{
			base.OnFill(m);
			return;
		}
		float3 @float = default(float3);
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
		NineRectangle nineRectangle;
		NineRectangle nineRectangle2;
		NineRectangle.Calculate(base.pivot, base.atlas.pixelSize, base.mainTexture, ref vector, ref vector2, ref @float.xy, out nineRectangle, out nineRectangle2);
		Color color = base.color;
		if (this.mFillCenter)
		{
			NineRectangle.Fill9(ref nineRectangle, ref nineRectangle2, ref color, m);
		}
		else
		{
			NineRectangle.Fill8(ref nineRectangle, ref nineRectangle2, ref color, m);
		}
	}

	// Token: 0x04002906 RID: 10502
	[HideInInspector]
	[SerializeField]
	private bool mFillCenter = true;

	// Token: 0x04002907 RID: 10503
	protected Rect mInner;

	// Token: 0x04002908 RID: 10504
	protected Rect mInnerUV;

	// Token: 0x04002909 RID: 10505
	protected Vector3 mScale = Vector3.one;
}
