using System;
using NGUI.Meshing;
using NGUI.Structures;
using UnityEngine;

// Token: 0x02000428 RID: 1064
public class UISpriteWheel : UISlicedSprite
{
	// Token: 0x1700090D RID: 2317
	// (get) Token: 0x0600277A RID: 10106 RVA: 0x00099DA0 File Offset: 0x00097FA0
	// (set) Token: 0x0600277B RID: 10107 RVA: 0x00099DA8 File Offset: 0x00097FA8
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

	// Token: 0x1700090E RID: 2318
	// (get) Token: 0x0600277C RID: 10108 RVA: 0x00099E30 File Offset: 0x00098030
	// (set) Token: 0x0600277D RID: 10109 RVA: 0x00099E40 File Offset: 0x00098040
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

	// Token: 0x1700090F RID: 2319
	// (get) Token: 0x0600277E RID: 10110 RVA: 0x00099E50 File Offset: 0x00098050
	// (set) Token: 0x0600277F RID: 10111 RVA: 0x00099E58 File Offset: 0x00098058
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

	// Token: 0x17000910 RID: 2320
	// (get) Token: 0x06002780 RID: 10112 RVA: 0x00099E88 File Offset: 0x00098088
	// (set) Token: 0x06002781 RID: 10113 RVA: 0x00099E90 File Offset: 0x00098090
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

	// Token: 0x17000911 RID: 2321
	// (get) Token: 0x06002782 RID: 10114 RVA: 0x00099EE0 File Offset: 0x000980E0
	// (set) Token: 0x06002783 RID: 10115 RVA: 0x00099EE8 File Offset: 0x000980E8
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

	// Token: 0x17000912 RID: 2322
	// (get) Token: 0x06002784 RID: 10116 RVA: 0x00099F38 File Offset: 0x00098138
	// (set) Token: 0x06002785 RID: 10117 RVA: 0x00099F40 File Offset: 0x00098140
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

	// Token: 0x17000913 RID: 2323
	// (get) Token: 0x06002786 RID: 10118 RVA: 0x00099F90 File Offset: 0x00098190
	// (set) Token: 0x06002787 RID: 10119 RVA: 0x00099F98 File Offset: 0x00098198
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

	// Token: 0x17000914 RID: 2324
	// (get) Token: 0x06002788 RID: 10120 RVA: 0x0009A008 File Offset: 0x00098208
	// (set) Token: 0x06002789 RID: 10121 RVA: 0x0009A010 File Offset: 0x00098210
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

	// Token: 0x17000915 RID: 2325
	// (get) Token: 0x0600278A RID: 10122 RVA: 0x0009A040 File Offset: 0x00098240
	// (set) Token: 0x0600278B RID: 10123 RVA: 0x0009A048 File Offset: 0x00098248
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

	// Token: 0x17000916 RID: 2326
	// (get) Token: 0x0600278C RID: 10124 RVA: 0x0009A068 File Offset: 0x00098268
	// (set) Token: 0x0600278D RID: 10125 RVA: 0x0009A070 File Offset: 0x00098270
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

	// Token: 0x0600278E RID: 10126 RVA: 0x0009A0B8 File Offset: 0x000982B8
	public override void OnFill(MeshBuffer m)
	{
		float num = this._degreesOfRotation * 0.0174532924f;
		float num2 = this._sliceDegrees * 0.0174532924f;
		float sliceFill = this._sliceFill;
		int num3 = this.slices + 1;
		float num4 = (num - num2 * (float)this.slices) * sliceFill;
		float num5 = num4 / (float)num3;
		float num6 = num4 / 6.28318548f;
		float num7 = (num - num4) / (float)num3;
		float3 @float = default(float3);
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
		NineRectangle nineRectangle;
		NineRectangle nineRectangle2;
		NineRectangle.Calculate(UIWidget.Pivot.Center, base.atlas.pixelSize, base.mainTexture, ref vector, ref vector2, ref @float.xy, out nineRectangle, out nineRectangle2);
		if (this.innerRadius > 0f && !Mathf.Approximately(nineRectangle.zz.x - nineRectangle.yy.x, 0f))
		{
			@float.xy.x = 3.14159274f * num8 * this.innerRadius / (float)num3 * num6;
			NineRectangle nineRectangle3;
			NineRectangle nineRectangle4;
			NineRectangle.Calculate(UIWidget.Pivot.Center, base.atlas.pixelSize, base.mainTexture, ref vector, ref vector2, ref @float.xy, out nineRectangle3, out nineRectangle4);
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
			NineRectangle nineRectangle3;
			nineRectangle3.xx.x = nineRectangle.xx.x;
			nineRectangle3.xx.y = nineRectangle.xx.y;
			nineRectangle3.yy.x = nineRectangle.yy.x;
			nineRectangle3.yy.y = nineRectangle.yy.y;
			nineRectangle3.zz.x = nineRectangle.zz.x;
			nineRectangle3.zz.y = nineRectangle.zz.y;
			nineRectangle3.ww.x = nineRectangle.ww.x;
			nineRectangle3.ww.y = nineRectangle.ww.y;
			NineRectangle nineRectangle4;
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
			Vertex[] v = m.v;
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

	// Token: 0x04001370 RID: 4976
	[SerializeField]
	[HideInInspector]
	private float _innerRadius = 0.5f;

	// Token: 0x04001371 RID: 4977
	[HideInInspector]
	[SerializeField]
	private Vector2 _center;

	// Token: 0x04001372 RID: 4978
	[HideInInspector]
	[SerializeField]
	private int _slices;

	// Token: 0x04001373 RID: 4979
	[HideInInspector]
	[SerializeField]
	private float _sliceDegrees;

	// Token: 0x04001374 RID: 4980
	[HideInInspector]
	[SerializeField]
	private float _targetDegreeResolution = 10f;

	// Token: 0x04001375 RID: 4981
	[SerializeField]
	[HideInInspector]
	private float _sliceFill = 1f;

	// Token: 0x04001376 RID: 4982
	[HideInInspector]
	[SerializeField]
	private float _degreesOfRotation = 360f;

	// Token: 0x04001377 RID: 4983
	[HideInInspector]
	[SerializeField]
	private float _facialRotationOffset;

	// Token: 0x04001378 RID: 4984
	[HideInInspector]
	[SerializeField]
	private float _addDegrees;
}
