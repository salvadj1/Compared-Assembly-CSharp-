using System;
using NGUI.Meshing;
using NGUI.Structures;
using UnityEngine;

// Token: 0x020004DE RID: 1246
public class UISpriteWheel : global::UISlicedSprite
{
	// Token: 0x17000975 RID: 2421
	// (get) Token: 0x06002B0A RID: 11018 RVA: 0x0009FD20 File Offset: 0x0009DF20
	// (set) Token: 0x06002B0B RID: 11019 RVA: 0x0009FD28 File Offset: 0x0009DF28
	public float innerRadius
	{
		get
		{
			return this._innerRadius;
		}
		set
		{
			if (value < 0f)
			{
				if (this._innerRadius != 0f)
				{
					this._innerRadius = 0f;
					this.MarkAsChanged();
				}
			}
			else if (value > 1f)
			{
				if (this._innerRadius != 1f)
				{
					this._innerRadius = 1f;
					this.MarkAsChanged();
				}
			}
			else if (this._innerRadius != value)
			{
				this._innerRadius = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x17000976 RID: 2422
	// (get) Token: 0x06002B0C RID: 11020 RVA: 0x0009FDB0 File Offset: 0x0009DFB0
	// (set) Token: 0x06002B0D RID: 11021 RVA: 0x0009FDC0 File Offset: 0x0009DFC0
	public float outerRadius
	{
		get
		{
			return 1f - this._innerRadius;
		}
		set
		{
			this.innerRadius = 1f - value;
		}
	}

	// Token: 0x17000977 RID: 2423
	// (get) Token: 0x06002B0E RID: 11022 RVA: 0x0009FDD0 File Offset: 0x0009DFD0
	// (set) Token: 0x06002B0F RID: 11023 RVA: 0x0009FDD8 File Offset: 0x0009DFD8
	public float sliceDegrees
	{
		get
		{
			return this._sliceDegrees;
		}
		set
		{
			if (value < 0f)
			{
				value = 0f;
			}
			if (this._sliceDegrees != value)
			{
				this._sliceDegrees = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x17000978 RID: 2424
	// (get) Token: 0x06002B10 RID: 11024 RVA: 0x0009FE08 File Offset: 0x0009E008
	// (set) Token: 0x06002B11 RID: 11025 RVA: 0x0009FE10 File Offset: 0x0009E010
	public float circumferenceFillRatio
	{
		get
		{
			return this._sliceFill;
		}
		set
		{
			if (value < 0.05f)
			{
				value = 0.05f;
			}
			else if (value > 1f)
			{
				value = 1f;
			}
			if (this._sliceFill != value)
			{
				this._sliceFill = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x17000979 RID: 2425
	// (get) Token: 0x06002B12 RID: 11026 RVA: 0x0009FE60 File Offset: 0x0009E060
	// (set) Token: 0x06002B13 RID: 11027 RVA: 0x0009FE68 File Offset: 0x0009E068
	public float degreesOfRotation
	{
		get
		{
			return this._degreesOfRotation;
		}
		set
		{
			if (value < 0.01f)
			{
				value = 0.01f;
			}
			else if (value > 360f)
			{
				value = 360f;
			}
			if (value != this._degreesOfRotation)
			{
				this._degreesOfRotation = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x1700097A RID: 2426
	// (get) Token: 0x06002B14 RID: 11028 RVA: 0x0009FEB8 File Offset: 0x0009E0B8
	// (set) Token: 0x06002B15 RID: 11029 RVA: 0x0009FEC0 File Offset: 0x0009E0C0
	public float facialCrank
	{
		get
		{
			return this._facialRotationOffset;
		}
		set
		{
			if (value < -1f)
			{
				value = -1f;
			}
			else if (value > 1f)
			{
				value = 1f;
			}
			if (value != this._facialRotationOffset)
			{
				this._facialRotationOffset = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x1700097B RID: 2427
	// (get) Token: 0x06002B16 RID: 11030 RVA: 0x0009FF10 File Offset: 0x0009E110
	// (set) Token: 0x06002B17 RID: 11031 RVA: 0x0009FF18 File Offset: 0x0009E118
	public float additionalRotation
	{
		get
		{
			return this._addDegrees;
		}
		set
		{
			if (float.IsInfinity(value) || float.IsNaN(value))
			{
				return;
			}
			while (value > 180f)
			{
				value -= 360f;
			}
			while (value <= -180f)
			{
				value += 360f;
			}
			if (value != this._addDegrees)
			{
				this._addDegrees = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x1700097C RID: 2428
	// (get) Token: 0x06002B18 RID: 11032 RVA: 0x0009FF88 File Offset: 0x0009E188
	// (set) Token: 0x06002B19 RID: 11033 RVA: 0x0009FF90 File Offset: 0x0009E190
	public float targetDegreeResolution
	{
		get
		{
			return this._targetDegreeResolution;
		}
		set
		{
			if (0.5f > value)
			{
				value = 0.5f;
			}
			if (this._targetDegreeResolution != value)
			{
				this._targetDegreeResolution = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x1700097D RID: 2429
	// (get) Token: 0x06002B1A RID: 11034 RVA: 0x0009FFC0 File Offset: 0x0009E1C0
	// (set) Token: 0x06002B1B RID: 11035 RVA: 0x0009FFC8 File Offset: 0x0009E1C8
	public Vector2 center
	{
		get
		{
			return this._center;
		}
		set
		{
			if (this._center != value)
			{
				this._center = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x1700097E RID: 2430
	// (get) Token: 0x06002B1C RID: 11036 RVA: 0x0009FFE8 File Offset: 0x0009E1E8
	// (set) Token: 0x06002B1D RID: 11037 RVA: 0x0009FFF0 File Offset: 0x0009E1F0
	public int slices
	{
		get
		{
			return this._slices;
		}
		set
		{
			if (value < 0)
			{
				value = 0;
			}
			else if (value > 360)
			{
				value = 360;
			}
			if (this._slices != value)
			{
				this._slices = value;
				this.MarkAsChanged();
			}
		}
	}

	// Token: 0x06002B1E RID: 11038 RVA: 0x000A0038 File Offset: 0x0009E238
	public override void OnFill(NGUI.Meshing.MeshBuffer m)
	{
		float num = this._degreesOfRotation * 0.0174532924f;
		float num2 = this._sliceDegrees * 0.0174532924f;
		float sliceFill = this._sliceFill;
		int num3 = this.slices + 1;
		float num4 = (num - num2 * (float)this.slices) * sliceFill;
		float num5 = num4 / (float)num3;
		float num6 = num4 / 6.28318548f;
		float num7 = (num - num4) / (float)num3;
		NGUI.Structures.float3 @float = default(NGUI.Structures.float3);
		@float.xyz = base.cachedTransform.localScale;
		float num8 = (@float.x >= @float.y) ? @float.x : @float.y;
		@float.xy.x = 3.14159274f * num8 / (float)num3 * num6;
		@float.xy.y = num8 * (this.outerRadius * 0.5f);
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
		NGUI.Structures.NineRectangle.Calculate(global::UIWidget.Pivot.Center, base.atlas.pixelSize, base.mainTexture, ref vector, ref vector2, ref @float.xy, out nineRectangle, out nineRectangle2);
		if (this.innerRadius > 0f && !Mathf.Approximately(nineRectangle.zz.x - nineRectangle.yy.x, 0f))
		{
			@float.xy.x = 3.14159274f * num8 * this.innerRadius / (float)num3 * num6;
			NGUI.Structures.NineRectangle nineRectangle3;
			NGUI.Structures.NineRectangle nineRectangle4;
			NGUI.Structures.NineRectangle.Calculate(global::UIWidget.Pivot.Center, base.atlas.pixelSize, base.mainTexture, ref vector, ref vector2, ref @float.xy, out nineRectangle3, out nineRectangle4);
			float num9 = (nineRectangle.yy.x + nineRectangle.zz.x) * 0.5f;
			if (nineRectangle3.yy.x > num9)
			{
				float num10 = (nineRectangle3.yy.x - num9) / (nineRectangle.ww.x - num9);
				if (num10 >= 1f)
				{
					nineRectangle3.xx.x = nineRectangle.xx.x;
					nineRectangle3.xx.y = nineRectangle.xx.y;
					nineRectangle3.yy.x = nineRectangle.yy.x;
					nineRectangle3.yy.y = nineRectangle.yy.y;
					nineRectangle3.zz.x = nineRectangle.zz.x;
					nineRectangle3.zz.y = nineRectangle.zz.y;
					nineRectangle3.ww.x = nineRectangle.ww.x;
					nineRectangle3.ww.y = nineRectangle.ww.y;
					nineRectangle4.xx.x = nineRectangle2.xx.x;
					nineRectangle4.xx.y = nineRectangle2.xx.y;
					nineRectangle4.yy.x = nineRectangle2.yy.x;
					nineRectangle4.yy.y = nineRectangle2.yy.y;
					nineRectangle4.zz.x = nineRectangle2.zz.x;
					nineRectangle4.zz.y = nineRectangle2.zz.y;
					nineRectangle4.ww.x = nineRectangle2.ww.x;
					nineRectangle4.ww.y = nineRectangle2.ww.y;
				}
				else
				{
					float num11 = 1f - num10;
					nineRectangle3.xx.y = nineRectangle.xx.y * num10 + nineRectangle3.xx.y * num11;
					nineRectangle3.yy.x = nineRectangle.yy.x * num10 + 0.5f * num11;
					nineRectangle3.yy.y = nineRectangle.yy.y * num10 + nineRectangle3.yy.y * num11;
					nineRectangle3.zz.x = nineRectangle.zz.x * num10 + 0.5f * num11;
					nineRectangle3.zz.y = nineRectangle.zz.y * num10 + nineRectangle3.zz.y * num11;
					nineRectangle3.ww.y = nineRectangle.ww.y * num10 + nineRectangle3.ww.y * num11;
					nineRectangle3.ww.x = nineRectangle.ww.x;
					nineRectangle3.xx.x = nineRectangle.xx.x;
				}
			}
		}
		else
		{
			NGUI.Structures.NineRectangle nineRectangle3;
			nineRectangle3.xx.x = nineRectangle.xx.x;
			nineRectangle3.xx.y = nineRectangle.xx.y;
			nineRectangle3.yy.x = nineRectangle.yy.x;
			nineRectangle3.yy.y = nineRectangle.yy.y;
			nineRectangle3.zz.x = nineRectangle.zz.x;
			nineRectangle3.zz.y = nineRectangle.zz.y;
			nineRectangle3.ww.x = nineRectangle.ww.x;
			nineRectangle3.ww.y = nineRectangle.ww.y;
			NGUI.Structures.NineRectangle nineRectangle4;
			nineRectangle4.xx.x = nineRectangle2.xx.x;
			nineRectangle4.xx.y = nineRectangle2.xx.y;
			nineRectangle4.yy.x = nineRectangle2.yy.x;
			nineRectangle4.yy.y = nineRectangle2.yy.y;
			nineRectangle4.zz.x = nineRectangle2.zz.x;
			nineRectangle4.zz.y = nineRectangle2.zz.y;
			nineRectangle4.ww.x = nineRectangle2.ww.x;
			nineRectangle4.ww.y = nineRectangle2.ww.y;
		}
		float num12 = Mathf.Abs(nineRectangle.ww.x - nineRectangle.xx.x);
		float num13 = num5 / num12;
		if (num2 > 0f)
		{
			num12 += num2 / num13;
			num13 = num5 / num12;
		}
		float num14 = this.innerRadius * 0.5f;
		float num15 = this.outerRadius * 0.5f;
		float num16 = Mathf.Min(nineRectangle.xx.y, nineRectangle.ww.y);
		float num17 = Mathf.Max(nineRectangle.ww.y, nineRectangle.xx.y) - num16;
		Color color = base.color;
		int num18 = m.vSize;
		float num19 = num7 + num5;
		float num20 = num7 * -0.5f + (this._facialRotationOffset * 0.5f + 0.5f) * num5 + this._addDegrees * 0.0174532924f;
		for (;;)
		{
			NGUI.Meshing.Vertex[] v = m.v;
			int vSize = m.vSize;
			for (int i = num18; i < vSize; i++)
			{
				float num21 = num14 + (v[i].y - num16) / num17 * num15;
				float num22 = v[i].x * num13 + num20;
				v[i].x = 0.5f + Mathf.Sin(num22) * num21;
				v[i].y = -0.5f + Mathf.Cos(num22) * num21;
			}
			if (--num3 <= 0)
			{
				break;
			}
			num20 += num19;
			num18 = vSize;
		}
	}

	// Token: 0x040014F3 RID: 5363
	[HideInInspector]
	[SerializeField]
	private float _innerRadius = 0.5f;

	// Token: 0x040014F4 RID: 5364
	[SerializeField]
	[HideInInspector]
	private Vector2 _center;

	// Token: 0x040014F5 RID: 5365
	[SerializeField]
	[HideInInspector]
	private int _slices;

	// Token: 0x040014F6 RID: 5366
	[HideInInspector]
	[SerializeField]
	private float _sliceDegrees;

	// Token: 0x040014F7 RID: 5367
	[SerializeField]
	[HideInInspector]
	private float _targetDegreeResolution = 10f;

	// Token: 0x040014F8 RID: 5368
	[SerializeField]
	[HideInInspector]
	private float _sliceFill = 1f;

	// Token: 0x040014F9 RID: 5369
	[HideInInspector]
	[SerializeField]
	private float _degreesOfRotation = 360f;

	// Token: 0x040014FA RID: 5370
	[HideInInspector]
	[SerializeField]
	private float _facialRotationOffset;

	// Token: 0x040014FB RID: 5371
	[SerializeField]
	[HideInInspector]
	private float _addDegrees;
}
