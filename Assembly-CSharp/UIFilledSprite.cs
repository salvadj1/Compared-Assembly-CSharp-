﻿using System;
using NGUI.Meshing;
using UnityEngine;

// Token: 0x020007E0 RID: 2016
[AddComponentMenu("NGUI/UI/Sprite (Filled)")]
[ExecuteInEditMode]
public class UIFilledSprite : UISprite
{
	// Token: 0x17000E01 RID: 3585
	// (get) Token: 0x0600484B RID: 18507 RVA: 0x00124400 File Offset: 0x00122600
	// (set) Token: 0x0600484C RID: 18508 RVA: 0x00124408 File Offset: 0x00122608
	public UIFilledSprite.FillDirection fillDirection
	{
		get
		{
			return this.mFillDirection;
		}
		set
		{
			if (this.mFillDirection != value)
			{
				this.mFillDirection = value;
				base.ChangedAuto();
			}
		}
	}

	// Token: 0x17000E02 RID: 3586
	// (get) Token: 0x0600484D RID: 18509 RVA: 0x00124424 File Offset: 0x00122624
	// (set) Token: 0x0600484E RID: 18510 RVA: 0x0012442C File Offset: 0x0012262C
	public float fillAmount
	{
		get
		{
			return this.mFillAmount;
		}
		set
		{
			float num = Mathf.Clamp01(value);
			if (this.mFillAmount != num)
			{
				this.mFillAmount = num;
				base.ChangedAuto();
			}
		}
	}

	// Token: 0x17000E03 RID: 3587
	// (get) Token: 0x0600484F RID: 18511 RVA: 0x0012445C File Offset: 0x0012265C
	// (set) Token: 0x06004850 RID: 18512 RVA: 0x00124464 File Offset: 0x00122664
	public bool invert
	{
		get
		{
			return this.mInvert;
		}
		set
		{
			if (this.mInvert != value)
			{
				this.mInvert = value;
				base.ChangedAuto();
			}
		}
	}

	// Token: 0x06004851 RID: 18513 RVA: 0x00124480 File Offset: 0x00122680
	private bool AdjustRadial(Vector2[] xy, Vector2[] uv, float fill, bool invert)
	{
		if (fill < 0.001f)
		{
			return false;
		}
		if (!invert && fill > 0.999f)
		{
			return true;
		}
		float num = Mathf.Clamp01(fill);
		if (!invert)
		{
			num = 1f - num;
		}
		num *= 1.57079637f;
		float num2 = Mathf.Sin(num);
		float num3 = Mathf.Cos(num);
		if (num2 > num3)
		{
			num3 *= 1f / num2;
			num2 = 1f;
			if (!invert)
			{
				xy[0].y = Mathf.Lerp(xy[2].y, xy[0].y, num3);
				xy[3].y = xy[0].y;
				uv[0].y = Mathf.Lerp(uv[2].y, uv[0].y, num3);
				uv[3].y = uv[0].y;
			}
		}
		else if (num3 > num2)
		{
			num2 *= 1f / num3;
			num3 = 1f;
			if (invert)
			{
				xy[0].x = Mathf.Lerp(xy[2].x, xy[0].x, num2);
				xy[1].x = xy[0].x;
				uv[0].x = Mathf.Lerp(uv[2].x, uv[0].x, num2);
				uv[1].x = uv[0].x;
			}
		}
		else
		{
			num2 = 1f;
			num3 = 1f;
		}
		if (invert)
		{
			xy[1].y = Mathf.Lerp(xy[2].y, xy[0].y, num3);
			uv[1].y = Mathf.Lerp(uv[2].y, uv[0].y, num3);
		}
		else
		{
			xy[3].x = Mathf.Lerp(xy[2].x, xy[0].x, num2);
			uv[3].x = Mathf.Lerp(uv[2].x, uv[0].x, num2);
		}
		return true;
	}

	// Token: 0x06004852 RID: 18514 RVA: 0x001246EC File Offset: 0x001228EC
	private void Rotate(Vector2[] v, int offset)
	{
		for (int i = 0; i < offset; i++)
		{
			Vector2 vector;
			vector..ctor(v[3].x, v[3].y);
			v[3].x = v[2].y;
			v[3].y = v[2].x;
			v[2].x = v[1].y;
			v[2].y = v[1].x;
			v[1].x = v[0].y;
			v[1].y = v[0].x;
			v[0].x = vector.y;
			v[0].y = vector.x;
		}
	}

	// Token: 0x06004853 RID: 18515 RVA: 0x001247E0 File Offset: 0x001229E0
	public override void OnFill(MeshBuffer m)
	{
		float num = 0f;
		float num2 = 0f;
		float num3 = 1f;
		float num4 = -1f;
		float num5 = this.mOuterUV.xMin;
		float num6 = this.mOuterUV.yMin;
		float num7 = this.mOuterUV.xMax;
		float num8 = this.mOuterUV.yMax;
		if (this.mFillDirection == UIFilledSprite.FillDirection.Horizontal || this.mFillDirection == UIFilledSprite.FillDirection.Vertical)
		{
			float num9 = (num7 - num5) * this.mFillAmount;
			float num10 = (num8 - num6) * this.mFillAmount;
			if (this.fillDirection == UIFilledSprite.FillDirection.Horizontal)
			{
				if (this.mInvert)
				{
					num = 1f - this.mFillAmount;
					num5 = num7 - num9;
				}
				else
				{
					num3 *= this.mFillAmount;
					num7 = num5 + num9;
				}
			}
			else if (this.fillDirection == UIFilledSprite.FillDirection.Vertical)
			{
				if (this.mInvert)
				{
					num4 *= this.mFillAmount;
					num6 = num8 - num10;
				}
				else
				{
					num2 = -(1f - this.mFillAmount);
					num8 = num6 + num10;
				}
			}
		}
		Vector2[] array = new Vector2[4];
		Vector2[] array2 = new Vector2[4];
		array[0] = new Vector2(num3, num2);
		array[1] = new Vector2(num3, num4);
		array[2] = new Vector2(num, num4);
		array[3] = new Vector2(num, num2);
		array2[0] = new Vector2(num7, num8);
		array2[1] = new Vector2(num7, num6);
		array2[2] = new Vector2(num5, num6);
		array2[3] = new Vector2(num5, num8);
		Color color = base.color;
		if (this.fillDirection == UIFilledSprite.FillDirection.Radial90)
		{
			if (!this.AdjustRadial(array, array2, this.mFillAmount, this.mInvert))
			{
				return;
			}
		}
		else
		{
			if (this.fillDirection == UIFilledSprite.FillDirection.Radial180)
			{
				Vector2[] array3 = new Vector2[4];
				Vector2[] array4 = new Vector2[4];
				for (int i = 0; i < 2; i++)
				{
					array3[0] = new Vector2(0f, 0f);
					array3[1] = new Vector2(0f, 1f);
					array3[2] = new Vector2(1f, 1f);
					array3[3] = new Vector2(1f, 0f);
					array4[0] = new Vector2(0f, 0f);
					array4[1] = new Vector2(0f, 1f);
					array4[2] = new Vector2(1f, 1f);
					array4[3] = new Vector2(1f, 0f);
					if (this.mInvert)
					{
						if (i > 0)
						{
							this.Rotate(array3, i);
							this.Rotate(array4, i);
						}
					}
					else if (i < 1)
					{
						this.Rotate(array3, 1 - i);
						this.Rotate(array4, 1 - i);
					}
					float num11;
					float num12;
					if (i == 1)
					{
						num11 = ((!this.mInvert) ? 1f : 0.5f);
						num12 = ((!this.mInvert) ? 0.5f : 1f);
					}
					else
					{
						num11 = ((!this.mInvert) ? 0.5f : 1f);
						num12 = ((!this.mInvert) ? 1f : 0.5f);
					}
					array3[1].y = Mathf.Lerp(num11, num12, array3[1].y);
					array3[2].y = Mathf.Lerp(num11, num12, array3[2].y);
					array4[1].y = Mathf.Lerp(num11, num12, array4[1].y);
					array4[2].y = Mathf.Lerp(num11, num12, array4[2].y);
					float fill = this.mFillAmount * 2f - (float)i;
					bool flag = i % 2 == 1;
					if (this.AdjustRadial(array3, array4, fill, !flag))
					{
						if (this.mInvert)
						{
							flag = !flag;
						}
						if (flag)
						{
							int num13 = m.Alloc(PrimitiveKind.Quad);
							for (int j = 0; j < 4; j++)
							{
								m.v[num13].x = Mathf.Lerp(array[0].x, array[2].x, array3[j].x);
								m.v[num13].y = Mathf.Lerp(array[0].y, array[2].y, array3[j].y);
								m.v[num13].z = 0f;
								m.v[num13].u = Mathf.Lerp(array2[0].x, array2[2].x, array4[j].x);
								m.v[num13].v = Mathf.Lerp(array2[0].y, array2[2].y, array4[j].y);
								m.v[num13].r = color.r;
								m.v[num13].g = color.g;
								m.v[num13].b = color.b;
								m.v[num13].a = color.a;
								num13++;
							}
						}
						else
						{
							int num14 = m.Alloc(PrimitiveKind.Quad);
							for (int k = 3; k > -1; k--)
							{
								m.v[num14].x = Mathf.Lerp(array[0].x, array[2].x, array3[k].x);
								m.v[num14].y = Mathf.Lerp(array[0].y, array[2].y, array3[k].y);
								m.v[num14].z = 0f;
								m.v[num14].u = Mathf.Lerp(array2[0].x, array2[2].x, array4[k].x);
								m.v[num14].v = Mathf.Lerp(array2[0].y, array2[2].y, array4[k].y);
								m.v[num14].r = color.r;
								m.v[num14].g = color.g;
								m.v[num14].b = color.b;
								m.v[num14].a = color.a;
								num14++;
							}
						}
					}
				}
				return;
			}
			if (this.fillDirection == UIFilledSprite.FillDirection.Radial360)
			{
				float[] array5 = new float[]
				{
					0.5f,
					1f,
					0f,
					0.5f,
					0.5f,
					1f,
					0.5f,
					1f,
					0f,
					0.5f,
					0.5f,
					1f,
					0f,
					0.5f,
					0f,
					0.5f
				};
				Vector2[] array6 = new Vector2[4];
				Vector2[] array7 = new Vector2[4];
				for (int l = 0; l < 4; l++)
				{
					array6[0] = new Vector2(0f, 0f);
					array6[1] = new Vector2(0f, 1f);
					array6[2] = new Vector2(1f, 1f);
					array6[3] = new Vector2(1f, 0f);
					array7[0] = new Vector2(0f, 0f);
					array7[1] = new Vector2(0f, 1f);
					array7[2] = new Vector2(1f, 1f);
					array7[3] = new Vector2(1f, 0f);
					if (this.mInvert)
					{
						if (l > 0)
						{
							this.Rotate(array6, l);
							this.Rotate(array7, l);
						}
					}
					else if (l < 3)
					{
						this.Rotate(array6, 3 - l);
						this.Rotate(array7, 3 - l);
					}
					for (int n = 0; n < 4; n++)
					{
						int num15 = (!this.mInvert) ? (l * 4) : ((3 - l) * 4);
						float num16 = array5[num15];
						float num17 = array5[num15 + 1];
						float num18 = array5[num15 + 2];
						float num19 = array5[num15 + 3];
						array6[n].x = Mathf.Lerp(num16, num17, array6[n].x);
						array6[n].y = Mathf.Lerp(num18, num19, array6[n].y);
						array7[n].x = Mathf.Lerp(num16, num17, array7[n].x);
						array7[n].y = Mathf.Lerp(num18, num19, array7[n].y);
					}
					float fill2 = this.mFillAmount * 4f - (float)l;
					bool flag2 = l % 2 == 1;
					if (this.AdjustRadial(array6, array7, fill2, !flag2))
					{
						if (this.mInvert)
						{
							flag2 = !flag2;
						}
						if (flag2)
						{
							int num20 = m.Alloc(PrimitiveKind.Quad);
							for (int num21 = 0; num21 < 4; num21++)
							{
								m.v[num20].x = Mathf.Lerp(array[0].x, array[2].x, array6[num21].x);
								m.v[num20].y = Mathf.Lerp(array[0].y, array[2].y, array6[num21].y);
								m.v[num20].z = 0f;
								m.v[num20].u = Mathf.Lerp(array2[0].x, array2[2].x, array7[num21].x);
								m.v[num20].v = Mathf.Lerp(array2[0].y, array2[2].y, array7[num21].y);
								m.v[num20].r = color.r;
								m.v[num20].g = color.g;
								m.v[num20].b = color.b;
								m.v[num20].a = color.a;
								num20++;
							}
						}
						else
						{
							int num22 = m.Alloc(PrimitiveKind.Quad);
							for (int num23 = 3; num23 > -1; num23--)
							{
								m.v[num22].x = Mathf.Lerp(array[0].x, array[2].x, array6[num23].x);
								m.v[num22].y = Mathf.Lerp(array[0].y, array[2].y, array6[num23].y);
								m.v[num22].z = 0f;
								m.v[num22].u = Mathf.Lerp(array2[0].x, array2[2].x, array7[num23].x);
								m.v[num22].v = Mathf.Lerp(array2[0].y, array2[2].y, array7[num23].y);
								m.v[num22].r = color.r;
								m.v[num22].g = color.g;
								m.v[num22].b = color.b;
								m.v[num22].a = color.a;
								num22++;
							}
						}
					}
				}
				return;
			}
		}
		Vertex a;
		a.x = array[0].x;
		a.y = array[0].y;
		a.u = array2[0].x;
		a.v = array2[0].y;
		Vertex b;
		b.x = array[1].x;
		b.y = array[1].y;
		b.u = array2[1].x;
		b.v = array2[1].y;
		Vertex c;
		c.x = array[2].x;
		c.y = array[2].y;
		c.u = array2[2].x;
		c.v = array2[2].y;
		Vertex d;
		d.x = array[3].x;
		d.y = array[3].y;
		d.u = array2[3].x;
		d.v = array2[3].y;
		a.z = (b.z = (c.z = (d.z = 0f)));
		a.r = (b.r = (c.r = (d.r = color.r)));
		a.g = (b.g = (c.g = (d.g = color.g)));
		a.b = (b.b = (c.b = (d.b = color.b)));
		a.a = (b.a = (c.a = (d.a = color.a)));
		m.Quad(a, b, c, d);
	}

	// Token: 0x04002886 RID: 10374
	[HideInInspector]
	[SerializeField]
	private UIFilledSprite.FillDirection mFillDirection = UIFilledSprite.FillDirection.Radial360;

	// Token: 0x04002887 RID: 10375
	[HideInInspector]
	[SerializeField]
	private float mFillAmount = 1f;

	// Token: 0x04002888 RID: 10376
	[SerializeField]
	[HideInInspector]
	private bool mInvert;

	// Token: 0x020007E1 RID: 2017
	public enum FillDirection
	{
		// Token: 0x0400288A RID: 10378
		Horizontal,
		// Token: 0x0400288B RID: 10379
		Vertical,
		// Token: 0x0400288C RID: 10380
		Radial90,
		// Token: 0x0400288D RID: 10381
		Radial180,
		// Token: 0x0400288E RID: 10382
		Radial360
	}
}
