using System;
using System.Runtime.InteropServices;
using NGUI.Meshing;
using UnityEngine;

namespace NGUI.Structures
{
	// Token: 0x02000800 RID: 2048
	[StructLayout(LayoutKind.Explicit)]
	public struct NineRectangle
	{
		// Token: 0x17000E5C RID: 3676
		// (get) Token: 0x0600497A RID: 18810 RVA: 0x0012E27C File Offset: 0x0012C47C
		// (set) Token: 0x0600497B RID: 18811 RVA: 0x0012E2B0 File Offset: 0x0012C4B0
		public Vector2 xy
		{
			get
			{
				Vector2 result;
				result.x = this.xx.x;
				result.y = this.yy.y;
				return result;
			}
			set
			{
				this.xx.x = value.x;
				this.yy.y = value.y;
			}
		}

		// Token: 0x17000E5D RID: 3677
		// (get) Token: 0x0600497C RID: 18812 RVA: 0x0012E2E4 File Offset: 0x0012C4E4
		// (set) Token: 0x0600497D RID: 18813 RVA: 0x0012E318 File Offset: 0x0012C518
		public Vector2 xz
		{
			get
			{
				Vector2 result;
				result.x = this.xx.x;
				result.y = this.zz.y;
				return result;
			}
			set
			{
				this.xx.x = value.x;
				this.zz.y = value.y;
			}
		}

		// Token: 0x17000E5E RID: 3678
		// (get) Token: 0x0600497E RID: 18814 RVA: 0x0012E34C File Offset: 0x0012C54C
		// (set) Token: 0x0600497F RID: 18815 RVA: 0x0012E380 File Offset: 0x0012C580
		public Vector2 xw
		{
			get
			{
				Vector2 result;
				result.x = this.xx.x;
				result.y = this.ww.y;
				return result;
			}
			set
			{
				this.xx.x = value.x;
				this.ww.y = value.y;
			}
		}

		// Token: 0x17000E5F RID: 3679
		// (get) Token: 0x06004980 RID: 18816 RVA: 0x0012E3B4 File Offset: 0x0012C5B4
		// (set) Token: 0x06004981 RID: 18817 RVA: 0x0012E3E8 File Offset: 0x0012C5E8
		public Vector2 yx
		{
			get
			{
				Vector2 result;
				result.x = this.yy.x;
				result.y = this.xx.y;
				return result;
			}
			set
			{
				this.yy.x = value.x;
				this.xx.y = value.y;
			}
		}

		// Token: 0x17000E60 RID: 3680
		// (get) Token: 0x06004982 RID: 18818 RVA: 0x0012E41C File Offset: 0x0012C61C
		// (set) Token: 0x06004983 RID: 18819 RVA: 0x0012E450 File Offset: 0x0012C650
		public Vector2 yz
		{
			get
			{
				Vector2 result;
				result.x = this.yy.x;
				result.y = this.zz.y;
				return result;
			}
			set
			{
				this.yy.x = value.x;
				this.zz.y = value.y;
			}
		}

		// Token: 0x17000E61 RID: 3681
		// (get) Token: 0x06004984 RID: 18820 RVA: 0x0012E484 File Offset: 0x0012C684
		// (set) Token: 0x06004985 RID: 18821 RVA: 0x0012E4B8 File Offset: 0x0012C6B8
		public Vector2 yw
		{
			get
			{
				Vector2 result;
				result.x = this.yy.x;
				result.y = this.ww.y;
				return result;
			}
			set
			{
				this.yy.x = value.x;
				this.ww.y = value.y;
			}
		}

		// Token: 0x17000E62 RID: 3682
		// (get) Token: 0x06004986 RID: 18822 RVA: 0x0012E4EC File Offset: 0x0012C6EC
		// (set) Token: 0x06004987 RID: 18823 RVA: 0x0012E520 File Offset: 0x0012C720
		public Vector2 zx
		{
			get
			{
				Vector2 result;
				result.x = this.zz.x;
				result.y = this.xx.y;
				return result;
			}
			set
			{
				this.zz.x = value.x;
				this.xx.y = value.y;
			}
		}

		// Token: 0x17000E63 RID: 3683
		// (get) Token: 0x06004988 RID: 18824 RVA: 0x0012E554 File Offset: 0x0012C754
		// (set) Token: 0x06004989 RID: 18825 RVA: 0x0012E588 File Offset: 0x0012C788
		public Vector2 zy
		{
			get
			{
				Vector2 result;
				result.x = this.zz.x;
				result.y = this.yy.y;
				return result;
			}
			set
			{
				this.zz.x = value.x;
				this.zz.y = value.y;
			}
		}

		// Token: 0x17000E64 RID: 3684
		// (get) Token: 0x0600498A RID: 18826 RVA: 0x0012E5BC File Offset: 0x0012C7BC
		// (set) Token: 0x0600498B RID: 18827 RVA: 0x0012E5F0 File Offset: 0x0012C7F0
		public Vector2 zw
		{
			get
			{
				Vector2 result;
				result.x = this.zz.x;
				result.y = this.ww.y;
				return result;
			}
			set
			{
				this.zz.x = value.x;
				this.ww.y = value.y;
			}
		}

		// Token: 0x17000E65 RID: 3685
		// (get) Token: 0x0600498C RID: 18828 RVA: 0x0012E624 File Offset: 0x0012C824
		// (set) Token: 0x0600498D RID: 18829 RVA: 0x0012E658 File Offset: 0x0012C858
		public Vector2 wx
		{
			get
			{
				Vector2 result;
				result.x = this.ww.x;
				result.y = this.xx.y;
				return result;
			}
			set
			{
				this.ww.x = value.x;
				this.xx.y = value.y;
			}
		}

		// Token: 0x17000E66 RID: 3686
		// (get) Token: 0x0600498E RID: 18830 RVA: 0x0012E68C File Offset: 0x0012C88C
		// (set) Token: 0x0600498F RID: 18831 RVA: 0x0012E6C0 File Offset: 0x0012C8C0
		public Vector2 wy
		{
			get
			{
				Vector2 result;
				result.x = this.ww.x;
				result.y = this.yy.y;
				return result;
			}
			set
			{
				this.ww.x = value.x;
				this.zz.y = value.y;
			}
		}

		// Token: 0x17000E67 RID: 3687
		// (get) Token: 0x06004990 RID: 18832 RVA: 0x0012E6F4 File Offset: 0x0012C8F4
		// (set) Token: 0x06004991 RID: 18833 RVA: 0x0012E728 File Offset: 0x0012C928
		public Vector2 wz
		{
			get
			{
				Vector2 result;
				result.x = this.ww.x;
				result.y = this.zz.y;
				return result;
			}
			set
			{
				this.ww.x = value.x;
				this.zz.y = value.y;
			}
		}

		// Token: 0x17000E68 RID: 3688
		public Vector2 this[int i]
		{
			get
			{
				switch (i)
				{
				case 0:
					return this.xx;
				case 1:
					return this.yy;
				case 2:
					return this.zz;
				case 3:
					return this.ww;
				default:
					throw new IndexOutOfRangeException();
				}
			}
		}

		// Token: 0x17000E69 RID: 3689
		public float this[int i, int a]
		{
			get
			{
				switch (i)
				{
				case 0:
					return this.xx[a];
				case 1:
					return this.yy[a];
				case 2:
					return this.zz[a];
				case 3:
					return this.ww[a];
				default:
					throw new IndexOutOfRangeException();
				}
			}
		}

		// Token: 0x06004994 RID: 18836 RVA: 0x0012E80C File Offset: 0x0012CA0C
		public static void Calculate(UIWidget.Pivot pivot, float pixelSize, Texture tex, ref Vector4 minMaxX, ref Vector4 minMaxY, ref Vector2 scale, out NineRectangle nqV, out NineRectangle nqT)
		{
			if (tex && pixelSize != 0f)
			{
				float num = (minMaxX.y - minMaxX.x) * pixelSize;
				float num2 = (minMaxX.w - minMaxX.z) * pixelSize;
				float num3 = (minMaxY.z - minMaxY.w) * pixelSize;
				float num4 = (minMaxY.x - minMaxY.y) * pixelSize;
				Vector2 vector;
				Vector2 vector2;
				if (scale.x < 0f)
				{
					scale.x = 0f;
					vector.x = num / 0f;
					vector2.x = num2 / 0f;
				}
				else
				{
					float num5 = (float)(1.0 / ((double)scale.x / (double)tex.width));
					vector.x = num * num5;
					vector2.x = num2 * num5;
				}
				if (scale.y < 0f)
				{
					scale.y = 0f;
					vector.y = num3 / 0f;
					vector2.y = num4 / 0f;
				}
				else
				{
					float num6 = (float)(1.0 / ((double)scale.y / (double)tex.height));
					vector.y = num3 * num6;
					vector2.y = num4 * num6;
				}
				float num7;
				switch (pivot)
				{
				case UIWidget.Pivot.TopRight:
				case UIWidget.Pivot.Right:
					break;
				default:
					if (pivot != UIWidget.Pivot.BottomRight)
					{
						nqV.xx.x = 0f;
						nqV.yy.x = vector.x;
						num7 = 1f - vector2.x;
						nqV.zz.x = ((num7 <= vector.x) ? vector.x : num7);
						num7 = vector.x + vector2.x;
						nqV.ww.x = ((num7 <= 1f) ? 1f : num7);
						goto IL_320;
					}
					break;
				}
				num7 = vector2.x + vector.x;
				if (num7 <= 1f)
				{
					nqV.xx.x = 0f;
					nqV.ww.x = 1f;
					nqV.yy.x = vector.x;
					num7 = 1f - vector2.x;
					nqV.zz.x = ((num7 <= vector.x) ? vector.x : num7);
				}
				else
				{
					nqV.xx.x = 1f - num7;
					nqV.yy.x = nqV.xx.x + vector.x;
					nqV.ww.x = nqV.xx.x + num7;
					num7 = 1f - vector2.x;
					nqV.zz.x = nqV.xx.x + ((num7 <= vector.x) ? vector.x : num7);
				}
				IL_320:
				switch (pivot)
				{
				case UIWidget.Pivot.BottomLeft:
				case UIWidget.Pivot.Bottom:
				case UIWidget.Pivot.BottomRight:
					num7 = -1f - vector2.y + vector.y;
					if (num7 <= 0f)
					{
						nqV.xx.y = 0f;
						nqV.yy.y = vector.y;
						num7 = -1f - vector2.y;
						nqV.zz.y = ((num7 >= vector.y) ? vector.y : num7);
						num7 = vector.y + vector2.y;
						nqV.ww.y = ((num7 >= -1f) ? -1f : num7);
					}
					else
					{
						nqV.xx.y = num7;
						nqV.yy.y = nqV.xx.y + vector.x;
						num7 = -1f - vector2.y;
						nqV.zz.y = nqV.xx.y + ((num7 >= vector.y) ? vector.y : num7);
						num7 = vector.y + vector2.y;
						nqV.ww.y = nqV.xx.y + ((num7 >= -1f) ? -1f : num7);
					}
					break;
				default:
					nqV.xx.y = 0f;
					nqV.yy.y = vector.y;
					num7 = -1f - vector2.y;
					nqV.zz.y = ((num7 >= vector.y) ? vector.y : num7);
					num7 = vector2.y + vector.y;
					nqV.ww.y = ((num7 >= -1f) ? -1f : num7);
					break;
				}
				nqT.xx.x = minMaxX.x;
				nqT.yy.x = minMaxX.y;
				nqT.zz.x = minMaxX.z;
				nqT.ww.x = minMaxX.w;
				nqT.xx.y = minMaxY.w;
				nqT.yy.y = minMaxY.z;
				nqT.zz.y = minMaxY.y;
				nqT.ww.y = minMaxY.x;
			}
			else
			{
				nqV.xx.x = (nqV.yy.x = (nqV.xx.y = (nqV.yy.y = 0f)));
				nqV.zz.x = (nqV.ww.x = 1f);
				nqV.zz.y = (nqV.ww.y = -1f);
				nqT = default(NineRectangle);
			}
		}

		// Token: 0x06004995 RID: 18837 RVA: 0x0012EE90 File Offset: 0x0012D090
		public static void Fill9(ref NineRectangle nqV, ref NineRectangle nqT, ref Color color, MeshBuffer m)
		{
			if (nqV.xx.x == nqV.yy.x)
			{
				if (nqV.yy.x == nqV.zz.x)
				{
					if (nqV.zz.x != nqT.ww.x)
					{
						NineRectangle.FillColumn1(ref nqV, ref nqT, 2, ref color, m);
					}
				}
				else if (nqV.zz.x == nqT.ww.x)
				{
					NineRectangle.FillColumn1(ref nqV, ref nqT, 1, ref color, m);
				}
				else
				{
					NineRectangle.FillColumn2(ref nqV, ref nqT, 1, ref color, m);
				}
			}
			else if (nqV.yy.x == nqV.zz.x)
			{
				if (nqV.zz.x == nqV.ww.x)
				{
					NineRectangle.FillColumn1(ref nqV, ref nqT, 1, ref color, m);
				}
				else
				{
					NineRectangle.FillColumn2(ref nqV, ref nqT, 2, ref color, m);
				}
			}
			else if (nqV.zz.x == nqV.ww.x)
			{
				NineRectangle.FillColumn2(ref nqV, ref nqT, 0, ref color, m);
			}
			else
			{
				NineRectangle.FillColumn3(ref nqV, ref nqT, ref color, m);
			}
		}

		// Token: 0x06004996 RID: 18838 RVA: 0x0012EFC0 File Offset: 0x0012D1C0
		private static void FillColumn1(ref NineRectangle nqV, ref NineRectangle nqT, int columnStart, ref Color color, MeshBuffer m)
		{
			if (nqV.xx.y == nqV.yy.y)
			{
				if (nqV.yy.y == nqV.zz.y)
				{
					if (nqV.zz.y != nqV.ww.y)
					{
						switch (columnStart)
						{
						case 0:
							m.FastCell(nqV.xz, nqV.yw, nqT.xz, nqT.yw, ref color);
							break;
						case 1:
							m.FastCell(nqV.yz, nqV.zw, nqT.yz, nqT.zw, ref color);
							break;
						case 2:
							m.FastCell(nqV.zz, nqV.ww, nqT.zz, nqT.ww, ref color);
							break;
						}
					}
				}
				else if (nqV.zz.y == nqV.ww.y)
				{
					switch (columnStart)
					{
					case 0:
						m.FastCell(nqV.xy, nqV.yz, nqT.xy, nqT.yz, ref color);
						break;
					case 1:
						m.FastCell(nqV.yy, nqV.zz, nqT.yy, nqT.zz, ref color);
						break;
					case 2:
						m.FastCell(nqV.zy, nqV.wz, nqT.zy, nqT.wz, ref color);
						break;
					}
				}
				else
				{
					int num = m.Alloc(PrimitiveKind.Grid1x2, 0f, color);
					switch (columnStart)
					{
					case 0:
						m.v[num].x = nqV.xx.x;
						m.v[num].y = nqV.yy.y;
						m.v[num].u = nqT.xx.x;
						m.v[num].v = nqT.yy.y;
						m.v[num + 1].x = nqV.yy.x;
						m.v[num + 1].y = nqV.yy.y;
						m.v[num + 1].u = nqT.yy.x;
						m.v[num + 1].v = nqT.yy.y;
						m.v[num + 2].x = nqV.xx.x;
						m.v[num + 2].y = nqV.zz.y;
						m.v[num + 2].u = nqT.xx.x;
						m.v[num + 2].v = nqT.zz.y;
						m.v[num + 3].x = nqV.yy.x;
						m.v[num + 3].y = nqV.zz.y;
						m.v[num + 3].u = nqT.yy.x;
						m.v[num + 3].v = nqT.zz.y;
						m.v[num + 4].x = nqV.xx.x;
						m.v[num + 4].y = nqV.ww.y;
						m.v[num + 4].u = nqT.xx.x;
						m.v[num + 4].v = nqT.ww.y;
						m.v[num + 5].x = nqV.yy.x;
						m.v[num + 5].y = nqV.ww.y;
						m.v[num + 5].u = nqT.yy.x;
						m.v[num + 5].v = nqT.ww.y;
						break;
					case 1:
						m.v[num].x = nqV.yy.x;
						m.v[num].y = nqV.yy.y;
						m.v[num].u = nqT.yy.x;
						m.v[num].v = nqT.yy.y;
						m.v[num + 1].x = nqV.zz.x;
						m.v[num + 1].y = nqV.yy.y;
						m.v[num + 1].u = nqT.zz.x;
						m.v[num + 1].v = nqT.yy.y;
						m.v[num + 2].x = nqV.yy.x;
						m.v[num + 2].y = nqV.zz.y;
						m.v[num + 2].u = nqT.yy.x;
						m.v[num + 2].v = nqT.zz.y;
						m.v[num + 3].x = nqV.zz.x;
						m.v[num + 3].y = nqV.zz.y;
						m.v[num + 3].u = nqT.zz.x;
						m.v[num + 3].v = nqT.zz.y;
						m.v[num + 4].x = nqV.yy.x;
						m.v[num + 4].y = nqV.ww.y;
						m.v[num + 4].u = nqT.yy.x;
						m.v[num + 4].v = nqT.ww.y;
						m.v[num + 5].x = nqV.zz.x;
						m.v[num + 5].y = nqV.ww.y;
						m.v[num + 5].u = nqT.zz.x;
						m.v[num + 5].v = nqT.ww.y;
						break;
					case 2:
						m.v[num].x = nqV.zz.x;
						m.v[num].y = nqV.yy.y;
						m.v[num].u = nqT.zz.x;
						m.v[num].v = nqT.yy.y;
						m.v[num + 1].x = nqV.ww.x;
						m.v[num + 1].y = nqV.yy.y;
						m.v[num + 1].u = nqT.ww.x;
						m.v[num + 1].v = nqT.yy.y;
						m.v[num + 2].x = nqV.zz.x;
						m.v[num + 2].y = nqV.zz.y;
						m.v[num + 2].u = nqT.zz.x;
						m.v[num + 2].v = nqT.zz.y;
						m.v[num + 3].x = nqV.ww.x;
						m.v[num + 3].y = nqV.zz.y;
						m.v[num + 3].u = nqT.ww.x;
						m.v[num + 3].v = nqT.zz.y;
						m.v[num + 4].x = nqV.zz.x;
						m.v[num + 4].y = nqV.ww.y;
						m.v[num + 4].u = nqT.zz.x;
						m.v[num + 4].v = nqT.ww.y;
						m.v[num + 5].x = nqV.ww.x;
						m.v[num + 5].y = nqV.ww.y;
						m.v[num + 5].u = nqT.ww.x;
						m.v[num + 5].v = nqT.ww.y;
						break;
					}
				}
			}
			else if (nqV.yy.y == nqV.zz.y)
			{
				if (nqV.zz.y == nqV.ww.y)
				{
					switch (columnStart)
					{
					case 0:
						m.FastCell(nqV.xx, nqV.yy, nqT.xx, nqT.yy, ref color);
						break;
					case 1:
						m.FastCell(nqV.yx, nqV.zy, nqT.yx, nqT.zy, ref color);
						break;
					case 2:
						m.FastCell(nqV.zx, nqV.wy, nqT.zx, nqT.wy, ref color);
						break;
					}
				}
				else
				{
					switch (columnStart)
					{
					case 0:
						m.FastCell(nqV.xx, nqV.yy, nqT.xx, nqT.yy, ref color);
						m.FastCell(nqV.xz, nqV.yw, nqT.xz, nqT.yw, ref color);
						break;
					case 1:
						m.FastCell(nqV.yx, nqV.zy, nqT.yx, nqT.zy, ref color);
						m.FastCell(nqV.yz, nqV.zw, nqT.yz, nqT.zw, ref color);
						break;
					case 2:
						m.FastCell(nqV.zx, nqV.wy, nqT.zx, nqT.wy, ref color);
						m.FastCell(nqV.zz, nqV.ww, nqT.zz, nqT.ww, ref color);
						break;
					}
				}
			}
			else if (nqV.zz.y == nqV.ww.y)
			{
				int num2 = m.Alloc(PrimitiveKind.Grid1x2, 0f, color);
				switch (columnStart)
				{
				case 0:
					m.v[num2].x = nqV.xx.x;
					m.v[num2].y = nqV.xx.y;
					m.v[num2].u = nqT.xx.x;
					m.v[num2].v = nqT.xx.y;
					m.v[num2 + 1].x = nqV.yy.x;
					m.v[num2 + 1].y = nqV.xx.y;
					m.v[num2 + 1].u = nqT.yy.x;
					m.v[num2 + 1].v = nqT.xx.y;
					m.v[num2 + 2].x = nqV.xx.x;
					m.v[num2 + 2].y = nqV.yy.y;
					m.v[num2 + 2].u = nqT.xx.x;
					m.v[num2 + 2].v = nqT.yy.y;
					m.v[num2 + 3].x = nqV.yy.x;
					m.v[num2 + 3].y = nqV.yy.y;
					m.v[num2 + 3].u = nqT.yy.x;
					m.v[num2 + 3].v = nqT.yy.y;
					m.v[num2 + 4].x = nqV.xx.x;
					m.v[num2 + 4].y = nqV.zz.y;
					m.v[num2 + 4].u = nqT.xx.x;
					m.v[num2 + 4].v = nqT.zz.y;
					m.v[num2 + 5].x = nqV.yy.x;
					m.v[num2 + 5].y = nqV.zz.y;
					m.v[num2 + 5].u = nqT.yy.x;
					m.v[num2 + 5].v = nqT.zz.y;
					break;
				case 1:
					m.v[num2].x = nqV.yy.x;
					m.v[num2].y = nqV.xx.y;
					m.v[num2].u = nqT.yy.x;
					m.v[num2].v = nqT.xx.y;
					m.v[num2 + 1].x = nqV.zz.x;
					m.v[num2 + 1].y = nqV.xx.y;
					m.v[num2 + 1].u = nqT.zz.x;
					m.v[num2 + 1].v = nqT.xx.y;
					m.v[num2 + 2].x = nqV.yy.x;
					m.v[num2 + 2].y = nqV.yy.y;
					m.v[num2 + 2].u = nqT.yy.x;
					m.v[num2 + 2].v = nqT.yy.y;
					m.v[num2 + 3].x = nqV.zz.x;
					m.v[num2 + 3].y = nqV.yy.y;
					m.v[num2 + 3].u = nqT.zz.x;
					m.v[num2 + 3].v = nqT.yy.y;
					m.v[num2 + 4].x = nqV.yy.x;
					m.v[num2 + 4].y = nqV.zz.y;
					m.v[num2 + 4].u = nqT.yy.x;
					m.v[num2 + 4].v = nqT.zz.y;
					m.v[num2 + 5].x = nqV.zz.x;
					m.v[num2 + 5].y = nqV.zz.y;
					m.v[num2 + 5].u = nqT.zz.x;
					m.v[num2 + 5].v = nqT.zz.y;
					break;
				case 2:
					m.v[num2].x = nqV.zz.x;
					m.v[num2].y = nqV.xx.y;
					m.v[num2].u = nqT.zz.x;
					m.v[num2].v = nqT.xx.y;
					m.v[num2 + 1].x = nqV.ww.x;
					m.v[num2 + 1].y = nqV.xx.y;
					m.v[num2 + 1].u = nqT.ww.x;
					m.v[num2 + 1].v = nqT.xx.y;
					m.v[num2 + 2].x = nqV.zz.x;
					m.v[num2 + 2].y = nqV.yy.y;
					m.v[num2 + 2].u = nqT.zz.x;
					m.v[num2 + 2].v = nqT.yy.y;
					m.v[num2 + 3].x = nqV.ww.x;
					m.v[num2 + 3].y = nqV.yy.y;
					m.v[num2 + 3].u = nqT.ww.x;
					m.v[num2 + 3].v = nqT.yy.y;
					m.v[num2 + 4].x = nqV.zz.x;
					m.v[num2 + 4].y = nqV.zz.y;
					m.v[num2 + 4].u = nqT.zz.x;
					m.v[num2 + 4].v = nqT.zz.y;
					m.v[num2 + 5].x = nqV.ww.x;
					m.v[num2 + 5].y = nqV.zz.y;
					m.v[num2 + 5].u = nqT.ww.x;
					m.v[num2 + 5].v = nqT.zz.y;
					break;
				}
			}
			else
			{
				int num3 = m.Alloc(PrimitiveKind.Grid1x2, 0f, color);
				switch (columnStart)
				{
				case 0:
					m.v[num3].x = nqV.xx.x;
					m.v[num3].y = nqV.xx.y;
					m.v[num3].u = nqT.xx.x;
					m.v[num3].v = nqT.xx.y;
					m.v[num3 + 1].x = nqV.yy.x;
					m.v[num3 + 1].y = nqV.xx.y;
					m.v[num3 + 1].u = nqT.yy.x;
					m.v[num3 + 1].v = nqT.xx.y;
					m.v[num3 + 2].x = nqV.xx.x;
					m.v[num3 + 2].y = nqV.yy.y;
					m.v[num3 + 2].u = nqT.xx.x;
					m.v[num3 + 2].v = nqT.yy.y;
					m.v[num3 + 3].x = nqV.yy.x;
					m.v[num3 + 3].y = nqV.yy.y;
					m.v[num3 + 3].u = nqT.yy.x;
					m.v[num3 + 3].v = nqT.yy.y;
					m.v[num3 + 4].x = nqV.xx.x;
					m.v[num3 + 4].y = nqV.zz.y;
					m.v[num3 + 4].u = nqT.xx.x;
					m.v[num3 + 4].v = nqT.zz.y;
					m.v[num3 + 5].x = nqV.yy.x;
					m.v[num3 + 5].y = nqV.zz.y;
					m.v[num3 + 5].u = nqT.yy.x;
					m.v[num3 + 5].v = nqT.zz.y;
					m.v[num3 + 6].x = nqV.xx.x;
					m.v[num3 + 6].y = nqV.ww.y;
					m.v[num3 + 6].u = nqT.xx.x;
					m.v[num3 + 6].v = nqT.ww.y;
					m.v[num3 + 7].x = nqV.yy.x;
					m.v[num3 + 7].y = nqV.ww.y;
					m.v[num3 + 7].u = nqT.yy.x;
					m.v[num3 + 7].v = nqT.ww.y;
					break;
				case 1:
					m.v[num3].x = nqV.yy.x;
					m.v[num3].y = nqV.xx.y;
					m.v[num3].u = nqT.yy.x;
					m.v[num3].v = nqT.xx.y;
					m.v[num3 + 1].x = nqV.zz.x;
					m.v[num3 + 1].y = nqV.xx.y;
					m.v[num3 + 1].u = nqT.zz.x;
					m.v[num3 + 1].v = nqT.xx.y;
					m.v[num3 + 2].x = nqV.yy.x;
					m.v[num3 + 2].y = nqV.yy.y;
					m.v[num3 + 2].u = nqT.yy.x;
					m.v[num3 + 2].v = nqT.yy.y;
					m.v[num3 + 3].x = nqV.zz.x;
					m.v[num3 + 3].y = nqV.yy.y;
					m.v[num3 + 3].u = nqT.zz.x;
					m.v[num3 + 3].v = nqT.yy.y;
					m.v[num3 + 4].x = nqV.yy.x;
					m.v[num3 + 4].y = nqV.zz.y;
					m.v[num3 + 4].u = nqT.yy.x;
					m.v[num3 + 4].v = nqT.zz.y;
					m.v[num3 + 5].x = nqV.zz.x;
					m.v[num3 + 5].y = nqV.zz.y;
					m.v[num3 + 5].u = nqT.zz.x;
					m.v[num3 + 5].v = nqT.zz.y;
					m.v[num3 + 6].x = nqV.yy.x;
					m.v[num3 + 6].y = nqV.ww.y;
					m.v[num3 + 6].u = nqT.yy.x;
					m.v[num3 + 6].v = nqT.ww.y;
					m.v[num3 + 7].x = nqV.zz.x;
					m.v[num3 + 7].y = nqV.ww.y;
					m.v[num3 + 7].u = nqT.zz.x;
					m.v[num3 + 7].v = nqT.ww.y;
					break;
				case 2:
					m.v[num3].x = nqV.zz.x;
					m.v[num3].y = nqV.xx.y;
					m.v[num3].u = nqT.zz.x;
					m.v[num3].v = nqT.xx.y;
					m.v[num3 + 1].x = nqV.ww.x;
					m.v[num3 + 1].y = nqV.xx.y;
					m.v[num3 + 1].u = nqT.ww.x;
					m.v[num3 + 1].v = nqT.xx.y;
					m.v[num3 + 2].x = nqV.zz.x;
					m.v[num3 + 2].y = nqV.yy.y;
					m.v[num3 + 2].u = nqT.zz.x;
					m.v[num3 + 2].v = nqT.yy.y;
					m.v[num3 + 3].x = nqV.ww.x;
					m.v[num3 + 3].y = nqV.yy.y;
					m.v[num3 + 3].u = nqT.ww.x;
					m.v[num3 + 3].v = nqT.yy.y;
					m.v[num3 + 4].x = nqV.zz.x;
					m.v[num3 + 4].y = nqV.zz.y;
					m.v[num3 + 4].u = nqT.zz.x;
					m.v[num3 + 4].v = nqT.zz.y;
					m.v[num3 + 5].x = nqV.ww.x;
					m.v[num3 + 5].y = nqV.zz.y;
					m.v[num3 + 5].u = nqT.ww.x;
					m.v[num3 + 5].v = nqT.zz.y;
					m.v[num3 + 6].x = nqV.zz.x;
					m.v[num3 + 6].y = nqV.ww.y;
					m.v[num3 + 6].u = nqT.zz.x;
					m.v[num3 + 6].v = nqT.ww.y;
					m.v[num3 + 7].x = nqV.ww.x;
					m.v[num3 + 7].y = nqV.ww.y;
					m.v[num3 + 7].u = nqT.ww.x;
					m.v[num3 + 7].v = nqT.ww.y;
					break;
				}
			}
		}

		// Token: 0x06004997 RID: 18839 RVA: 0x001310A4 File Offset: 0x0012F2A4
		private static void FillColumn2(ref NineRectangle nqV, ref NineRectangle nqT, int columnStart, ref Color color, MeshBuffer m)
		{
			if (nqV.xx.y == nqV.yy.y)
			{
				if (nqV.yy.y == nqV.zz.y)
				{
					if (nqV.zz.y != nqV.ww.y)
					{
						switch (columnStart)
						{
						case 0:
						{
							int num = m.Alloc(PrimitiveKind.Grid2x1, 0f, color);
							m.v[num].x = nqV.xx.x;
							m.v[num].y = nqV.zz.y;
							m.v[num].u = nqT.xx.x;
							m.v[num].v = nqT.zz.y;
							m.v[num + 1].x = nqV.yy.x;
							m.v[num + 1].y = nqV.zz.y;
							m.v[num + 1].u = nqT.yy.x;
							m.v[num + 1].v = nqT.zz.y;
							m.v[num + 2].x = nqV.zz.x;
							m.v[num + 2].y = nqV.zz.y;
							m.v[num + 2].u = nqT.zz.x;
							m.v[num + 2].v = nqT.zz.y;
							m.v[num + 3].x = nqV.xx.x;
							m.v[num + 3].y = nqV.ww.y;
							m.v[num + 3].u = nqT.xx.x;
							m.v[num + 3].v = nqT.ww.y;
							m.v[num + 4].x = nqV.yy.x;
							m.v[num + 4].y = nqV.ww.y;
							m.v[num + 4].u = nqT.yy.x;
							m.v[num + 4].v = nqT.ww.y;
							m.v[num + 5].x = nqV.zz.x;
							m.v[num + 5].y = nqV.ww.y;
							m.v[num + 5].u = nqT.zz.x;
							m.v[num + 5].v = nqT.ww.y;
							break;
						}
						case 1:
						{
							int num = m.Alloc(PrimitiveKind.Grid2x1, 0f, color);
							m.v[num].x = nqV.yy.x;
							m.v[num].y = nqV.zz.y;
							m.v[num].u = nqT.yy.x;
							m.v[num].v = nqT.zz.y;
							m.v[num + 1].x = nqV.zz.x;
							m.v[num + 1].y = nqV.zz.y;
							m.v[num + 1].u = nqT.zz.x;
							m.v[num + 1].v = nqT.zz.y;
							m.v[num + 2].x = nqV.ww.x;
							m.v[num + 2].y = nqV.zz.y;
							m.v[num + 2].u = nqT.ww.x;
							m.v[num + 2].v = nqT.zz.y;
							m.v[num + 3].x = nqV.yy.x;
							m.v[num + 3].y = nqV.ww.y;
							m.v[num + 3].u = nqT.yy.x;
							m.v[num + 3].v = nqT.ww.y;
							m.v[num + 4].x = nqV.zz.x;
							m.v[num + 4].y = nqV.ww.y;
							m.v[num + 4].u = nqT.zz.x;
							m.v[num + 4].v = nqT.ww.y;
							m.v[num + 5].x = nqV.ww.x;
							m.v[num + 5].y = nqV.ww.y;
							m.v[num + 5].u = nqT.ww.x;
							m.v[num + 5].v = nqT.ww.y;
							break;
						}
						case 2:
							m.FastCell(nqV.xz, nqV.yw, nqT.xz, nqT.yw, ref color);
							m.FastCell(nqV.zz, nqV.ww, nqT.zz, nqT.ww, ref color);
							break;
						}
					}
				}
				else if (nqV.zz.y == nqV.ww.y)
				{
					switch (columnStart)
					{
					case 0:
					{
						int num2 = m.Alloc(PrimitiveKind.Grid2x1, 0f, color);
						m.v[num2].x = nqV.xx.x;
						m.v[num2].y = nqV.yy.y;
						m.v[num2].u = nqT.xx.x;
						m.v[num2].v = nqT.yy.y;
						m.v[num2 + 1].x = nqV.yy.x;
						m.v[num2 + 1].y = nqV.yy.y;
						m.v[num2 + 1].u = nqT.yy.x;
						m.v[num2 + 1].v = nqT.yy.y;
						m.v[num2 + 2].x = nqV.zz.x;
						m.v[num2 + 2].y = nqV.yy.y;
						m.v[num2 + 2].u = nqT.zz.x;
						m.v[num2 + 2].v = nqT.yy.y;
						m.v[num2 + 3].x = nqV.xx.x;
						m.v[num2 + 3].y = nqV.zz.y;
						m.v[num2 + 3].u = nqT.xx.x;
						m.v[num2 + 3].v = nqT.zz.y;
						m.v[num2 + 4].x = nqV.yy.x;
						m.v[num2 + 4].y = nqV.zz.y;
						m.v[num2 + 4].u = nqT.yy.x;
						m.v[num2 + 4].v = nqT.zz.y;
						m.v[num2 + 5].x = nqV.zz.x;
						m.v[num2 + 5].y = nqV.zz.y;
						m.v[num2 + 5].u = nqT.zz.x;
						m.v[num2 + 5].v = nqT.zz.y;
						break;
					}
					case 1:
					{
						int num2 = m.Alloc(PrimitiveKind.Grid2x1, 0f, color);
						m.v[num2].x = nqV.yy.x;
						m.v[num2].y = nqV.yy.y;
						m.v[num2].u = nqT.yy.x;
						m.v[num2].v = nqT.yy.y;
						m.v[num2 + 1].x = nqV.zz.x;
						m.v[num2 + 1].y = nqV.yy.y;
						m.v[num2 + 1].u = nqT.zz.x;
						m.v[num2 + 1].v = nqT.yy.y;
						m.v[num2 + 2].x = nqV.ww.x;
						m.v[num2 + 2].y = nqV.yy.y;
						m.v[num2 + 2].u = nqT.ww.x;
						m.v[num2 + 2].v = nqT.yy.y;
						m.v[num2 + 3].x = nqV.yy.x;
						m.v[num2 + 3].y = nqV.zz.y;
						m.v[num2 + 3].u = nqT.yy.x;
						m.v[num2 + 3].v = nqT.zz.y;
						m.v[num2 + 4].x = nqV.zz.x;
						m.v[num2 + 4].y = nqV.zz.y;
						m.v[num2 + 4].u = nqT.zz.x;
						m.v[num2 + 4].v = nqT.zz.y;
						m.v[num2 + 5].x = nqV.ww.x;
						m.v[num2 + 5].y = nqV.zz.y;
						m.v[num2 + 5].u = nqT.ww.x;
						m.v[num2 + 5].v = nqT.zz.y;
						break;
					}
					case 2:
						m.FastCell(nqV.xy, nqV.yz, nqT.xy, nqT.yz, ref color);
						m.FastCell(nqV.zy, nqV.wz, nqT.zy, nqT.wz, ref color);
						break;
					}
				}
				else
				{
					switch (columnStart)
					{
					case 0:
					{
						int num3 = m.Alloc(PrimitiveKind.Grid2x2, 0f, color);
						m.v[num3].x = nqV.xx.x;
						m.v[num3].y = nqV.yy.y;
						m.v[num3].u = nqT.xx.x;
						m.v[num3].v = nqT.yy.y;
						m.v[num3 + 1].x = nqV.yy.x;
						m.v[num3 + 1].y = nqV.yy.y;
						m.v[num3 + 1].u = nqT.yy.x;
						m.v[num3 + 1].v = nqT.yy.y;
						m.v[num3 + 2].x = nqV.zz.x;
						m.v[num3 + 2].y = nqV.yy.y;
						m.v[num3 + 2].u = nqT.zz.x;
						m.v[num3 + 2].v = nqT.yy.y;
						m.v[num3 + 3].x = nqV.xx.x;
						m.v[num3 + 3].y = nqV.zz.y;
						m.v[num3 + 3].u = nqT.xx.x;
						m.v[num3 + 3].v = nqT.zz.y;
						m.v[num3 + 4].x = nqV.yy.x;
						m.v[num3 + 4].y = nqV.zz.y;
						m.v[num3 + 4].u = nqT.yy.x;
						m.v[num3 + 4].v = nqT.zz.y;
						m.v[num3 + 5].x = nqV.zz.x;
						m.v[num3 + 5].y = nqV.zz.y;
						m.v[num3 + 5].u = nqT.zz.x;
						m.v[num3 + 5].v = nqT.zz.y;
						m.v[num3 + 6].x = nqV.xx.x;
						m.v[num3 + 6].y = nqV.ww.y;
						m.v[num3 + 6].u = nqT.xx.x;
						m.v[num3 + 6].v = nqT.ww.y;
						m.v[num3 + 7].x = nqV.yy.x;
						m.v[num3 + 7].y = nqV.ww.y;
						m.v[num3 + 7].u = nqT.yy.x;
						m.v[num3 + 7].v = nqT.ww.y;
						m.v[num3 + 8].x = nqV.zz.x;
						m.v[num3 + 8].y = nqV.ww.y;
						m.v[num3 + 8].u = nqT.zz.x;
						m.v[num3 + 8].v = nqT.ww.y;
						break;
					}
					case 1:
					{
						int num3 = m.Alloc(PrimitiveKind.Grid2x2, 0f, color);
						m.v[num3].x = nqV.yy.x;
						m.v[num3].y = nqV.yy.y;
						m.v[num3].u = nqT.yy.x;
						m.v[num3].v = nqT.yy.y;
						m.v[num3 + 1].x = nqV.zz.x;
						m.v[num3 + 1].y = nqV.yy.y;
						m.v[num3 + 1].u = nqT.zz.x;
						m.v[num3 + 1].v = nqT.yy.y;
						m.v[num3 + 2].x = nqV.ww.x;
						m.v[num3 + 2].y = nqV.yy.y;
						m.v[num3 + 2].u = nqT.ww.x;
						m.v[num3 + 2].v = nqT.yy.y;
						m.v[num3 + 3].x = nqV.yy.x;
						m.v[num3 + 3].y = nqV.zz.y;
						m.v[num3 + 3].u = nqT.yy.x;
						m.v[num3 + 3].v = nqT.zz.y;
						m.v[num3 + 4].x = nqV.zz.x;
						m.v[num3 + 4].y = nqV.zz.y;
						m.v[num3 + 4].u = nqT.zz.x;
						m.v[num3 + 4].v = nqT.zz.y;
						m.v[num3 + 5].x = nqV.ww.x;
						m.v[num3 + 5].y = nqV.zz.y;
						m.v[num3 + 5].u = nqT.ww.x;
						m.v[num3 + 5].v = nqT.zz.y;
						m.v[num3 + 6].x = nqV.yy.x;
						m.v[num3 + 6].y = nqV.ww.y;
						m.v[num3 + 6].u = nqT.yy.x;
						m.v[num3 + 6].v = nqT.ww.y;
						m.v[num3 + 7].x = nqV.zz.x;
						m.v[num3 + 7].y = nqV.ww.y;
						m.v[num3 + 7].u = nqT.zz.x;
						m.v[num3 + 7].v = nqT.ww.y;
						m.v[num3 + 8].x = nqV.ww.x;
						m.v[num3 + 8].y = nqV.ww.y;
						m.v[num3 + 8].u = nqT.ww.x;
						m.v[num3 + 8].v = nqT.ww.y;
						break;
					}
					case 2:
					{
						int num3 = m.Alloc(PrimitiveKind.Grid2x1, 0f, color);
						m.v[num3].x = nqV.xx.x;
						m.v[num3].y = nqV.yy.y;
						m.v[num3].u = nqT.xx.x;
						m.v[num3].v = nqT.yy.y;
						m.v[num3 + 1].x = nqV.yy.x;
						m.v[num3 + 1].y = nqV.yy.y;
						m.v[num3 + 1].u = nqT.yy.x;
						m.v[num3 + 1].v = nqT.yy.y;
						m.v[num3 + 2].x = nqV.xx.x;
						m.v[num3 + 2].y = nqV.zz.y;
						m.v[num3 + 2].u = nqT.xx.x;
						m.v[num3 + 2].v = nqT.zz.y;
						m.v[num3 + 3].x = nqV.yy.x;
						m.v[num3 + 3].y = nqV.zz.y;
						m.v[num3 + 3].u = nqT.yy.x;
						m.v[num3 + 3].v = nqT.zz.y;
						m.v[num3 + 4].x = nqV.yy.x;
						m.v[num3 + 4].y = nqV.ww.y;
						m.v[num3 + 4].u = nqT.yy.x;
						m.v[num3 + 4].v = nqT.ww.y;
						m.v[num3 + 5].x = nqV.zz.x;
						m.v[num3 + 5].y = nqV.ww.y;
						m.v[num3 + 5].u = nqT.zz.x;
						m.v[num3 + 5].v = nqT.ww.y;
						num3 = m.Alloc(PrimitiveKind.Grid2x1, 0f, color);
						m.v[num3].x = nqV.zz.x;
						m.v[num3].y = nqV.yy.y;
						m.v[num3].u = nqT.zz.x;
						m.v[num3].v = nqT.yy.y;
						m.v[num3 + 1].x = nqV.ww.x;
						m.v[num3 + 1].y = nqV.yy.y;
						m.v[num3 + 1].u = nqT.ww.x;
						m.v[num3 + 1].v = nqT.yy.y;
						m.v[num3 + 2].x = nqV.zz.x;
						m.v[num3 + 2].y = nqV.zz.y;
						m.v[num3 + 2].u = nqT.zz.x;
						m.v[num3 + 2].v = nqT.zz.y;
						m.v[num3 + 3].x = nqV.ww.x;
						m.v[num3 + 3].y = nqV.zz.y;
						m.v[num3 + 3].u = nqT.ww.x;
						m.v[num3 + 3].v = nqT.zz.y;
						m.v[num3 + 4].x = nqV.zz.x;
						m.v[num3 + 4].y = nqV.ww.y;
						m.v[num3 + 4].u = nqT.zz.x;
						m.v[num3 + 4].v = nqT.ww.y;
						m.v[num3 + 5].x = nqV.ww.x;
						m.v[num3 + 5].y = nqV.ww.y;
						m.v[num3 + 5].u = nqT.ww.x;
						m.v[num3 + 5].v = nqT.ww.y;
						break;
					}
					}
				}
			}
			else if (nqV.yy.y == nqV.zz.y)
			{
				if (nqV.zz.y == nqV.ww.y)
				{
					switch (columnStart)
					{
					case 0:
					{
						int num4 = m.Alloc(PrimitiveKind.Grid2x1, 0f, color);
						m.v[num4].x = nqV.xx.x;
						m.v[num4].y = nqV.xx.y;
						m.v[num4].u = nqT.xx.x;
						m.v[num4].v = nqT.xx.y;
						m.v[num4 + 1].x = nqV.yy.x;
						m.v[num4 + 1].y = nqV.xx.y;
						m.v[num4 + 1].u = nqT.yy.x;
						m.v[num4 + 1].v = nqT.xx.y;
						m.v[num4 + 2].x = nqV.zz.x;
						m.v[num4 + 2].y = nqV.xx.y;
						m.v[num4 + 2].u = nqT.zz.x;
						m.v[num4 + 2].v = nqT.xx.y;
						m.v[num4 + 3].x = nqV.xx.x;
						m.v[num4 + 3].y = nqV.yy.y;
						m.v[num4 + 3].u = nqT.xx.x;
						m.v[num4 + 3].v = nqT.yy.y;
						m.v[num4 + 4].x = nqV.yy.x;
						m.v[num4 + 4].y = nqV.yy.y;
						m.v[num4 + 4].u = nqT.yy.x;
						m.v[num4 + 4].v = nqT.yy.y;
						m.v[num4 + 5].x = nqV.zz.x;
						m.v[num4 + 5].y = nqV.yy.y;
						m.v[num4 + 5].u = nqT.zz.x;
						m.v[num4 + 5].v = nqT.yy.y;
						break;
					}
					case 1:
					{
						int num4 = m.Alloc(PrimitiveKind.Grid2x1, 0f, color);
						m.v[num4].x = nqV.yy.x;
						m.v[num4].y = nqV.xx.y;
						m.v[num4].u = nqT.yy.x;
						m.v[num4].v = nqT.xx.y;
						m.v[num4 + 1].x = nqV.zz.x;
						m.v[num4 + 1].y = nqV.xx.y;
						m.v[num4 + 1].u = nqT.zz.x;
						m.v[num4 + 1].v = nqT.xx.y;
						m.v[num4 + 2].x = nqV.ww.x;
						m.v[num4 + 2].y = nqV.xx.y;
						m.v[num4 + 2].u = nqT.ww.x;
						m.v[num4 + 2].v = nqT.xx.y;
						m.v[num4 + 3].x = nqV.yy.x;
						m.v[num4 + 3].y = nqV.yy.y;
						m.v[num4 + 3].u = nqT.yy.x;
						m.v[num4 + 3].v = nqT.yy.y;
						m.v[num4 + 4].x = nqV.zz.x;
						m.v[num4 + 4].y = nqV.yy.y;
						m.v[num4 + 4].u = nqT.zz.x;
						m.v[num4 + 4].v = nqT.yy.y;
						m.v[num4 + 5].x = nqV.ww.x;
						m.v[num4 + 5].y = nqV.yy.y;
						m.v[num4 + 5].u = nqT.ww.x;
						m.v[num4 + 5].v = nqT.yy.y;
						break;
					}
					case 2:
						m.FastCell(nqV.xx, nqV.yy, nqT.xx, nqT.yy, ref color);
						m.FastCell(nqV.zx, nqV.wy, nqT.zx, nqT.wy, ref color);
						break;
					}
				}
				else
				{
					switch (columnStart)
					{
					case 0:
					{
						int num5 = m.Alloc(PrimitiveKind.Grid2x1, 0f, color);
						m.v[num5].x = nqV.xx.x;
						m.v[num5].y = nqV.xx.y;
						m.v[num5].u = nqT.xx.x;
						m.v[num5].v = nqT.xx.y;
						m.v[num5 + 1].x = nqV.yy.x;
						m.v[num5 + 1].y = nqV.xx.y;
						m.v[num5 + 1].u = nqT.yy.x;
						m.v[num5 + 1].v = nqT.xx.y;
						m.v[num5 + 2].x = nqV.zz.x;
						m.v[num5 + 2].y = nqV.xx.y;
						m.v[num5 + 2].u = nqT.zz.x;
						m.v[num5 + 2].v = nqT.xx.y;
						m.v[num5 + 3].x = nqV.xx.x;
						m.v[num5 + 3].y = nqV.yy.y;
						m.v[num5 + 3].u = nqT.xx.x;
						m.v[num5 + 3].v = nqT.yy.y;
						m.v[num5 + 4].x = nqV.yy.x;
						m.v[num5 + 4].y = nqV.yy.y;
						m.v[num5 + 4].u = nqT.yy.x;
						m.v[num5 + 4].v = nqT.yy.y;
						m.v[num5 + 5].x = nqV.zz.x;
						m.v[num5 + 5].y = nqV.yy.y;
						m.v[num5 + 5].u = nqT.zz.x;
						m.v[num5 + 5].v = nqT.yy.y;
						num5 = m.Alloc(PrimitiveKind.Grid2x1, 0f, color);
						m.v[num5].x = nqV.xx.x;
						m.v[num5].y = nqV.zz.y;
						m.v[num5].u = nqT.xx.x;
						m.v[num5].v = nqT.zz.y;
						m.v[num5 + 1].x = nqV.yy.x;
						m.v[num5 + 1].y = nqV.zz.y;
						m.v[num5 + 1].u = nqT.yy.x;
						m.v[num5 + 1].v = nqT.zz.y;
						m.v[num5 + 2].x = nqV.zz.x;
						m.v[num5 + 2].y = nqV.zz.y;
						m.v[num5 + 2].u = nqT.zz.x;
						m.v[num5 + 2].v = nqT.zz.y;
						m.v[num5 + 3].x = nqV.xx.x;
						m.v[num5 + 3].y = nqV.ww.y;
						m.v[num5 + 3].u = nqT.xx.x;
						m.v[num5 + 3].v = nqT.ww.y;
						m.v[num5 + 4].x = nqV.yy.x;
						m.v[num5 + 4].y = nqV.ww.y;
						m.v[num5 + 4].u = nqT.yy.x;
						m.v[num5 + 4].v = nqT.ww.y;
						m.v[num5 + 5].x = nqV.zz.x;
						m.v[num5 + 5].y = nqV.ww.y;
						m.v[num5 + 5].u = nqT.zz.x;
						m.v[num5 + 5].v = nqT.ww.y;
						break;
					}
					case 1:
					{
						int num5 = m.Alloc(PrimitiveKind.Grid2x1, 0f, color);
						m.v[num5].x = nqV.yy.x;
						m.v[num5].y = nqV.xx.y;
						m.v[num5].u = nqT.yy.x;
						m.v[num5].v = nqT.xx.y;
						m.v[num5 + 1].x = nqV.zz.x;
						m.v[num5 + 1].y = nqV.xx.y;
						m.v[num5 + 1].u = nqT.zz.x;
						m.v[num5 + 1].v = nqT.xx.y;
						m.v[num5 + 2].x = nqV.ww.x;
						m.v[num5 + 2].y = nqV.xx.y;
						m.v[num5 + 2].u = nqT.ww.x;
						m.v[num5 + 2].v = nqT.xx.y;
						m.v[num5 + 3].x = nqV.yy.x;
						m.v[num5 + 3].y = nqV.yy.y;
						m.v[num5 + 3].u = nqT.yy.x;
						m.v[num5 + 3].v = nqT.yy.y;
						m.v[num5 + 4].x = nqV.zz.x;
						m.v[num5 + 4].y = nqV.yy.y;
						m.v[num5 + 4].u = nqT.zz.x;
						m.v[num5 + 4].v = nqT.yy.y;
						m.v[num5 + 5].x = nqV.ww.x;
						m.v[num5 + 5].y = nqV.yy.y;
						m.v[num5 + 5].u = nqT.ww.x;
						m.v[num5 + 5].v = nqT.yy.y;
						num5 = m.Alloc(PrimitiveKind.Grid2x1, 0f, color);
						m.v[num5].x = nqV.yy.x;
						m.v[num5].y = nqV.zz.y;
						m.v[num5].u = nqT.yy.x;
						m.v[num5].v = nqT.zz.y;
						m.v[num5 + 1].x = nqV.zz.x;
						m.v[num5 + 1].y = nqV.zz.y;
						m.v[num5 + 1].u = nqT.zz.x;
						m.v[num5 + 1].v = nqT.zz.y;
						m.v[num5 + 2].x = nqV.ww.x;
						m.v[num5 + 2].y = nqV.zz.y;
						m.v[num5 + 2].u = nqT.ww.x;
						m.v[num5 + 2].v = nqT.zz.y;
						m.v[num5 + 3].x = nqV.yy.x;
						m.v[num5 + 3].y = nqV.ww.y;
						m.v[num5 + 3].u = nqT.yy.x;
						m.v[num5 + 3].v = nqT.ww.y;
						m.v[num5 + 4].x = nqV.zz.x;
						m.v[num5 + 4].y = nqV.ww.y;
						m.v[num5 + 4].u = nqT.zz.x;
						m.v[num5 + 4].v = nqT.ww.y;
						m.v[num5 + 5].x = nqV.ww.x;
						m.v[num5 + 5].y = nqV.ww.y;
						m.v[num5 + 5].u = nqT.ww.x;
						m.v[num5 + 5].v = nqT.ww.y;
						break;
					}
					case 2:
						m.FastCell(nqV.xx, nqV.yy, nqT.xx, nqT.yy, ref color);
						m.FastCell(nqV.zx, nqV.wy, nqT.zx, nqT.wy, ref color);
						m.FastCell(nqV.xz, nqV.yw, nqT.xz, nqT.yw, ref color);
						m.FastCell(nqV.zz, nqV.ww, nqT.zz, nqT.ww, ref color);
						break;
					}
				}
			}
			else if (nqV.zz.y == nqV.ww.y)
			{
				switch (columnStart)
				{
				case 0:
				{
					int num6 = m.Alloc(PrimitiveKind.Grid2x2, 0f, color);
					m.v[num6].x = nqV.xx.x;
					m.v[num6].y = nqV.xx.y;
					m.v[num6].u = nqT.xx.x;
					m.v[num6].v = nqT.xx.y;
					m.v[num6 + 1].x = nqV.yy.x;
					m.v[num6 + 1].y = nqV.xx.y;
					m.v[num6 + 1].u = nqT.yy.x;
					m.v[num6 + 1].v = nqT.xx.y;
					m.v[num6 + 2].x = nqV.zz.x;
					m.v[num6 + 2].y = nqV.xx.y;
					m.v[num6 + 2].u = nqT.zz.x;
					m.v[num6 + 2].v = nqT.xx.y;
					m.v[num6 + 3].x = nqV.xx.x;
					m.v[num6 + 3].y = nqV.yy.y;
					m.v[num6 + 3].u = nqT.xx.x;
					m.v[num6 + 3].v = nqT.zz.y;
					m.v[num6 + 4].x = nqV.yy.x;
					m.v[num6 + 4].y = nqV.yy.y;
					m.v[num6 + 4].u = nqT.yy.x;
					m.v[num6 + 4].v = nqT.yy.y;
					m.v[num6 + 5].x = nqV.zz.x;
					m.v[num6 + 5].y = nqV.yy.y;
					m.v[num6 + 5].u = nqT.zz.x;
					m.v[num6 + 5].v = nqT.yy.y;
					m.v[num6 + 6].x = nqV.xx.x;
					m.v[num6 + 6].y = nqV.zz.y;
					m.v[num6 + 6].u = nqT.xx.x;
					m.v[num6 + 6].v = nqT.zz.y;
					m.v[num6 + 7].x = nqV.yy.x;
					m.v[num6 + 7].y = nqV.zz.y;
					m.v[num6 + 7].u = nqT.yy.x;
					m.v[num6 + 7].v = nqT.zz.y;
					m.v[num6 + 8].x = nqV.zz.x;
					m.v[num6 + 8].y = nqV.zz.y;
					m.v[num6 + 8].u = nqT.zz.x;
					m.v[num6 + 8].v = nqT.zz.y;
					break;
				}
				case 1:
				{
					int num6 = m.Alloc(PrimitiveKind.Grid2x2, 0f, color);
					m.v[num6].x = nqV.yy.x;
					m.v[num6].y = nqV.xx.y;
					m.v[num6].u = nqT.yy.x;
					m.v[num6].v = nqT.xx.y;
					m.v[num6 + 1].x = nqV.zz.x;
					m.v[num6 + 1].y = nqV.xx.y;
					m.v[num6 + 1].u = nqT.zz.x;
					m.v[num6 + 1].v = nqT.xx.y;
					m.v[num6 + 2].x = nqV.ww.x;
					m.v[num6 + 2].y = nqV.xx.y;
					m.v[num6 + 2].u = nqT.ww.x;
					m.v[num6 + 2].v = nqT.xx.y;
					m.v[num6 + 3].x = nqV.yy.x;
					m.v[num6 + 3].y = nqV.yy.y;
					m.v[num6 + 3].u = nqT.yy.x;
					m.v[num6 + 3].v = nqT.yy.y;
					m.v[num6 + 4].x = nqV.zz.x;
					m.v[num6 + 4].y = nqV.yy.y;
					m.v[num6 + 4].u = nqT.zz.x;
					m.v[num6 + 4].v = nqT.yy.y;
					m.v[num6 + 5].x = nqV.ww.x;
					m.v[num6 + 5].y = nqV.yy.y;
					m.v[num6 + 5].u = nqT.ww.x;
					m.v[num6 + 5].v = nqT.yy.y;
					m.v[num6 + 6].x = nqV.yy.x;
					m.v[num6 + 6].y = nqV.zz.y;
					m.v[num6 + 6].u = nqT.yy.x;
					m.v[num6 + 6].v = nqT.zz.y;
					m.v[num6 + 7].x = nqV.zz.x;
					m.v[num6 + 7].y = nqV.zz.y;
					m.v[num6 + 7].u = nqT.zz.x;
					m.v[num6 + 7].v = nqT.zz.y;
					m.v[num6 + 8].x = nqV.ww.x;
					m.v[num6 + 8].y = nqV.zz.y;
					m.v[num6 + 8].u = nqT.ww.x;
					m.v[num6 + 8].v = nqT.zz.y;
					break;
				}
				case 2:
				{
					int num6 = m.Alloc(PrimitiveKind.Grid2x1, 0f, color);
					m.v[num6].x = nqV.xx.x;
					m.v[num6].y = nqV.xx.y;
					m.v[num6].u = nqT.xx.x;
					m.v[num6].v = nqT.xx.y;
					m.v[num6 + 1].x = nqV.yy.x;
					m.v[num6 + 1].y = nqV.xx.y;
					m.v[num6 + 1].u = nqT.yy.x;
					m.v[num6 + 1].v = nqT.xx.y;
					m.v[num6 + 2].x = nqV.xx.x;
					m.v[num6 + 2].y = nqV.yy.y;
					m.v[num6 + 2].u = nqT.xx.x;
					m.v[num6 + 2].v = nqT.yy.y;
					m.v[num6 + 3].x = nqV.yy.x;
					m.v[num6 + 3].y = nqV.yy.y;
					m.v[num6 + 3].u = nqT.yy.x;
					m.v[num6 + 3].v = nqT.yy.y;
					m.v[num6 + 4].x = nqV.yy.x;
					m.v[num6 + 4].y = nqV.zz.y;
					m.v[num6 + 4].u = nqT.yy.x;
					m.v[num6 + 4].v = nqT.zz.y;
					m.v[num6 + 5].x = nqV.zz.x;
					m.v[num6 + 5].y = nqV.zz.y;
					m.v[num6 + 5].u = nqT.zz.x;
					m.v[num6 + 5].v = nqT.zz.y;
					num6 = m.Alloc(PrimitiveKind.Grid2x1, 0f, color);
					m.v[num6].x = nqV.zz.x;
					m.v[num6].y = nqV.xx.y;
					m.v[num6].u = nqT.zz.x;
					m.v[num6].v = nqT.xx.y;
					m.v[num6 + 1].x = nqV.ww.x;
					m.v[num6 + 1].y = nqV.xx.y;
					m.v[num6 + 1].u = nqT.ww.x;
					m.v[num6 + 1].v = nqT.xx.y;
					m.v[num6 + 2].x = nqV.zz.x;
					m.v[num6 + 2].y = nqV.yy.y;
					m.v[num6 + 2].u = nqT.zz.x;
					m.v[num6 + 2].v = nqT.yy.y;
					m.v[num6 + 3].x = nqV.ww.x;
					m.v[num6 + 3].y = nqV.yy.y;
					m.v[num6 + 3].u = nqT.ww.x;
					m.v[num6 + 3].v = nqT.yy.y;
					m.v[num6 + 4].x = nqV.zz.x;
					m.v[num6 + 4].y = nqV.zz.y;
					m.v[num6 + 4].u = nqT.zz.x;
					m.v[num6 + 4].v = nqT.zz.y;
					m.v[num6 + 5].x = nqV.ww.x;
					m.v[num6 + 5].y = nqV.zz.y;
					m.v[num6 + 5].u = nqT.ww.x;
					m.v[num6 + 5].v = nqT.zz.y;
					break;
				}
				}
			}
			else
			{
				switch (columnStart)
				{
				case 0:
				{
					int num7 = m.Alloc(PrimitiveKind.Grid2x3, 0f, color);
					m.v[num7].x = nqV.xx.x;
					m.v[num7].y = nqV.xx.y;
					m.v[num7].u = nqT.xx.x;
					m.v[num7].v = nqT.xx.y;
					m.v[num7 + 1].x = nqV.yy.x;
					m.v[num7 + 1].y = nqV.xx.y;
					m.v[num7 + 1].u = nqT.yy.x;
					m.v[num7 + 1].v = nqT.xx.y;
					m.v[num7 + 2].x = nqV.zz.x;
					m.v[num7 + 2].y = nqV.xx.y;
					m.v[num7 + 2].u = nqT.zz.x;
					m.v[num7 + 2].v = nqT.xx.y;
					m.v[num7 + 3].x = nqV.xx.x;
					m.v[num7 + 3].y = nqV.yy.y;
					m.v[num7 + 3].u = nqT.xx.x;
					m.v[num7 + 3].v = nqT.yy.y;
					m.v[num7 + 4].x = nqV.yy.x;
					m.v[num7 + 4].y = nqV.yy.y;
					m.v[num7 + 4].u = nqT.yy.x;
					m.v[num7 + 4].v = nqT.yy.y;
					m.v[num7 + 5].x = nqV.zz.x;
					m.v[num7 + 5].y = nqV.yy.y;
					m.v[num7 + 5].u = nqT.zz.x;
					m.v[num7 + 5].v = nqT.yy.y;
					m.v[num7 + 6].x = nqV.xx.x;
					m.v[num7 + 6].y = nqV.zz.y;
					m.v[num7 + 6].u = nqT.xx.x;
					m.v[num7 + 6].v = nqT.zz.y;
					m.v[num7 + 7].x = nqV.yy.x;
					m.v[num7 + 7].y = nqV.zz.y;
					m.v[num7 + 7].u = nqT.yy.x;
					m.v[num7 + 7].v = nqT.zz.y;
					m.v[num7 + 8].x = nqV.zz.x;
					m.v[num7 + 8].y = nqV.zz.y;
					m.v[num7 + 8].u = nqT.zz.x;
					m.v[num7 + 8].v = nqT.zz.y;
					m.v[num7 + 9].x = nqV.xx.x;
					m.v[num7 + 9].y = nqV.ww.y;
					m.v[num7 + 9].u = nqT.xx.x;
					m.v[num7 + 9].v = nqT.ww.y;
					m.v[num7 + 10].x = nqV.yy.x;
					m.v[num7 + 10].y = nqV.ww.y;
					m.v[num7 + 10].u = nqT.yy.x;
					m.v[num7 + 10].v = nqT.ww.y;
					m.v[num7 + 11].x = nqV.zz.x;
					m.v[num7 + 11].y = nqV.ww.y;
					m.v[num7 + 11].u = nqT.zz.x;
					m.v[num7 + 11].v = nqT.ww.y;
					break;
				}
				case 1:
				{
					int num7 = m.Alloc(PrimitiveKind.Grid2x3, 0f, color);
					m.v[num7].x = nqV.yy.x;
					m.v[num7].y = nqV.xx.y;
					m.v[num7].u = nqT.yy.x;
					m.v[num7].v = nqT.xx.y;
					m.v[num7 + 1].x = nqV.zz.x;
					m.v[num7 + 1].y = nqV.xx.y;
					m.v[num7 + 1].u = nqT.zz.x;
					m.v[num7 + 1].v = nqT.xx.y;
					m.v[num7 + 2].x = nqV.ww.x;
					m.v[num7 + 2].y = nqV.xx.y;
					m.v[num7 + 2].u = nqT.ww.x;
					m.v[num7 + 2].v = nqT.xx.y;
					m.v[num7 + 3].x = nqV.yy.x;
					m.v[num7 + 3].y = nqV.yy.y;
					m.v[num7 + 3].u = nqT.yy.x;
					m.v[num7 + 3].v = nqT.yy.y;
					m.v[num7 + 4].x = nqV.zz.x;
					m.v[num7 + 4].y = nqV.yy.y;
					m.v[num7 + 4].u = nqT.zz.x;
					m.v[num7 + 4].v = nqT.yy.y;
					m.v[num7 + 5].x = nqV.ww.x;
					m.v[num7 + 5].y = nqV.yy.y;
					m.v[num7 + 5].u = nqT.ww.x;
					m.v[num7 + 5].v = nqT.yy.y;
					m.v[num7 + 6].x = nqV.yy.x;
					m.v[num7 + 6].y = nqV.zz.y;
					m.v[num7 + 6].u = nqT.yy.x;
					m.v[num7 + 6].v = nqT.zz.y;
					m.v[num7 + 7].x = nqV.zz.x;
					m.v[num7 + 7].y = nqV.zz.y;
					m.v[num7 + 7].u = nqT.zz.x;
					m.v[num7 + 7].v = nqT.zz.y;
					m.v[num7 + 8].x = nqV.ww.x;
					m.v[num7 + 8].y = nqV.zz.y;
					m.v[num7 + 8].u = nqT.ww.x;
					m.v[num7 + 8].v = nqT.zz.y;
					m.v[num7 + 9].x = nqV.yy.x;
					m.v[num7 + 9].y = nqV.ww.y;
					m.v[num7 + 9].u = nqT.yy.x;
					m.v[num7 + 9].v = nqT.ww.y;
					m.v[num7 + 10].x = nqV.zz.x;
					m.v[num7 + 10].y = nqV.ww.y;
					m.v[num7 + 10].u = nqT.zz.x;
					m.v[num7 + 10].v = nqT.ww.y;
					m.v[num7 + 11].x = nqV.ww.x;
					m.v[num7 + 11].y = nqV.ww.y;
					m.v[num7 + 11].u = nqT.ww.x;
					m.v[num7 + 11].v = nqT.ww.y;
					break;
				}
				case 2:
				{
					int num7 = m.Alloc(PrimitiveKind.Grid1x3, 0f, color);
					m.v[num7].x = nqV.xx.x;
					m.v[num7].y = nqV.xx.y;
					m.v[num7].u = nqT.xx.x;
					m.v[num7].v = nqT.xx.y;
					m.v[num7 + 1].x = nqV.yy.x;
					m.v[num7 + 1].y = nqV.xx.y;
					m.v[num7 + 1].u = nqT.yy.x;
					m.v[num7 + 1].v = nqT.xx.y;
					m.v[num7 + 2].x = nqV.xx.x;
					m.v[num7 + 2].y = nqV.yy.y;
					m.v[num7 + 2].u = nqT.xx.x;
					m.v[num7 + 2].v = nqT.yy.y;
					m.v[num7 + 3].x = nqV.yy.x;
					m.v[num7 + 3].y = nqV.yy.y;
					m.v[num7 + 3].u = nqT.yy.x;
					m.v[num7 + 3].v = nqT.yy.y;
					m.v[num7 + 4].x = nqV.xx.x;
					m.v[num7 + 4].y = nqV.zz.y;
					m.v[num7 + 4].u = nqT.xx.x;
					m.v[num7 + 4].v = nqT.zz.y;
					m.v[num7 + 5].x = nqV.yy.x;
					m.v[num7 + 5].y = nqV.zz.y;
					m.v[num7 + 5].u = nqT.yy.x;
					m.v[num7 + 5].v = nqT.zz.y;
					m.v[num7 + 6].x = nqV.xx.x;
					m.v[num7 + 6].y = nqV.ww.y;
					m.v[num7 + 6].u = nqT.xx.x;
					m.v[num7 + 6].v = nqT.ww.y;
					m.v[num7 + 7].x = nqV.yy.x;
					m.v[num7 + 7].y = nqV.ww.y;
					m.v[num7 + 7].u = nqT.yy.x;
					m.v[num7 + 7].v = nqT.ww.y;
					num7 = m.Alloc(PrimitiveKind.Grid1x3, 0f, color);
					m.v[num7].x = nqV.zz.x;
					m.v[num7].y = nqV.xx.y;
					m.v[num7].u = nqT.zz.x;
					m.v[num7].v = nqT.xx.y;
					m.v[num7 + 1].x = nqV.ww.x;
					m.v[num7 + 1].y = nqV.xx.y;
					m.v[num7 + 1].u = nqT.ww.x;
					m.v[num7 + 1].v = nqT.xx.y;
					m.v[num7 + 2].x = nqV.zz.x;
					m.v[num7 + 2].y = nqV.yy.y;
					m.v[num7 + 2].u = nqT.zz.x;
					m.v[num7 + 2].v = nqT.yy.y;
					m.v[num7 + 3].x = nqV.ww.x;
					m.v[num7 + 3].y = nqV.yy.y;
					m.v[num7 + 3].u = nqT.ww.x;
					m.v[num7 + 3].v = nqT.yy.y;
					m.v[num7 + 4].x = nqV.zz.x;
					m.v[num7 + 4].y = nqV.zz.y;
					m.v[num7 + 4].u = nqT.zz.x;
					m.v[num7 + 4].v = nqT.zz.y;
					m.v[num7 + 5].x = nqV.ww.x;
					m.v[num7 + 5].y = nqV.zz.y;
					m.v[num7 + 5].u = nqT.ww.x;
					m.v[num7 + 5].v = nqT.zz.y;
					m.v[num7 + 6].x = nqV.zz.x;
					m.v[num7 + 6].y = nqV.ww.y;
					m.v[num7 + 6].u = nqT.zz.x;
					m.v[num7 + 6].v = nqT.ww.y;
					m.v[num7 + 7].x = nqV.ww.x;
					m.v[num7 + 7].y = nqV.ww.y;
					m.v[num7 + 7].u = nqT.ww.x;
					m.v[num7 + 7].v = nqT.ww.y;
					break;
				}
				}
			}
		}

		// Token: 0x06004998 RID: 18840 RVA: 0x00136418 File Offset: 0x00134618
		private static void FillColumn3(ref NineRectangle nqV, ref NineRectangle nqT, ref Color color, MeshBuffer m)
		{
			if (nqV.xx.y == nqV.yy.y)
			{
				if (nqV.yy.y == nqV.zz.y)
				{
					if (nqV.zz.y != nqV.ww.y)
					{
						int num = m.Alloc(PrimitiveKind.Grid3x1, 0f, color);
						m.v[num].x = nqV.xx.x;
						m.v[num].y = nqV.zz.y;
						m.v[num].u = nqT.xx.x;
						m.v[num].v = nqT.zz.y;
						m.v[num + 1].x = nqV.yy.x;
						m.v[num + 1].y = nqV.zz.y;
						m.v[num + 1].u = nqT.yy.x;
						m.v[num + 1].v = nqT.zz.y;
						m.v[num + 2].x = nqV.zz.x;
						m.v[num + 2].y = nqV.zz.y;
						m.v[num + 2].u = nqT.zz.x;
						m.v[num + 2].v = nqT.zz.y;
						m.v[num + 3].x = nqV.ww.x;
						m.v[num + 3].y = nqV.zz.y;
						m.v[num + 3].u = nqT.ww.x;
						m.v[num + 3].v = nqT.zz.y;
						m.v[num + 4].x = nqV.xx.x;
						m.v[num + 4].y = nqV.ww.y;
						m.v[num + 4].u = nqT.xx.x;
						m.v[num + 4].v = nqT.ww.y;
						m.v[num + 5].x = nqV.yy.x;
						m.v[num + 5].y = nqV.ww.y;
						m.v[num + 5].u = nqT.yy.x;
						m.v[num + 5].v = nqT.ww.y;
						m.v[num + 6].x = nqV.zz.x;
						m.v[num + 6].y = nqV.ww.y;
						m.v[num + 6].u = nqT.zz.x;
						m.v[num + 6].v = nqT.ww.y;
						m.v[num + 7].x = nqV.ww.x;
						m.v[num + 7].y = nqV.ww.y;
						m.v[num + 7].u = nqT.ww.x;
						m.v[num + 7].v = nqT.ww.y;
					}
				}
				else if (nqV.zz.y == nqV.ww.y)
				{
					int num2 = m.Alloc(PrimitiveKind.Grid3x1, 0f, color);
					m.v[num2].x = nqV.xx.x;
					m.v[num2].y = nqV.yy.y;
					m.v[num2].u = nqT.xx.x;
					m.v[num2].v = nqT.yy.y;
					m.v[num2 + 1].x = nqV.yy.x;
					m.v[num2 + 1].y = nqV.yy.y;
					m.v[num2 + 1].u = nqT.yy.x;
					m.v[num2 + 1].v = nqT.yy.y;
					m.v[num2 + 2].x = nqV.zz.x;
					m.v[num2 + 2].y = nqV.yy.y;
					m.v[num2 + 2].u = nqT.zz.x;
					m.v[num2 + 2].v = nqT.yy.y;
					m.v[num2 + 3].x = nqV.ww.x;
					m.v[num2 + 3].y = nqV.yy.y;
					m.v[num2 + 3].u = nqT.ww.x;
					m.v[num2 + 3].v = nqT.yy.y;
					m.v[num2 + 4].x = nqV.xx.x;
					m.v[num2 + 4].y = nqV.zz.y;
					m.v[num2 + 4].u = nqT.xx.x;
					m.v[num2 + 4].v = nqT.zz.y;
					m.v[num2 + 5].x = nqV.yy.x;
					m.v[num2 + 5].y = nqV.zz.y;
					m.v[num2 + 5].u = nqT.yy.x;
					m.v[num2 + 5].v = nqT.zz.y;
					m.v[num2 + 6].x = nqV.zz.x;
					m.v[num2 + 6].y = nqV.zz.y;
					m.v[num2 + 6].u = nqT.zz.x;
					m.v[num2 + 6].v = nqT.zz.y;
					m.v[num2 + 7].x = nqV.ww.x;
					m.v[num2 + 7].y = nqV.zz.y;
					m.v[num2 + 7].u = nqT.ww.x;
					m.v[num2 + 7].v = nqT.zz.y;
				}
				else
				{
					int num3 = m.Alloc(PrimitiveKind.Grid3x2, 0f, color);
					m.v[num3].x = nqV.xx.x;
					m.v[num3].y = nqV.yy.y;
					m.v[num3].u = nqT.xx.x;
					m.v[num3].v = nqT.yy.y;
					m.v[num3 + 1].x = nqV.yy.x;
					m.v[num3 + 1].y = nqV.yy.y;
					m.v[num3 + 1].u = nqT.yy.x;
					m.v[num3 + 1].v = nqT.yy.y;
					m.v[num3 + 2].x = nqV.zz.x;
					m.v[num3 + 2].y = nqV.yy.y;
					m.v[num3 + 2].u = nqT.zz.x;
					m.v[num3 + 2].v = nqT.yy.y;
					m.v[num3 + 3].x = nqV.ww.x;
					m.v[num3 + 3].y = nqV.yy.y;
					m.v[num3 + 3].u = nqT.ww.x;
					m.v[num3 + 3].v = nqT.yy.y;
					m.v[num3 + 4].x = nqV.xx.x;
					m.v[num3 + 4].y = nqV.zz.y;
					m.v[num3 + 4].u = nqT.xx.x;
					m.v[num3 + 4].v = nqT.zz.y;
					m.v[num3 + 5].x = nqV.yy.x;
					m.v[num3 + 5].y = nqV.zz.y;
					m.v[num3 + 5].u = nqT.yy.x;
					m.v[num3 + 5].v = nqT.zz.y;
					m.v[num3 + 6].x = nqV.zz.x;
					m.v[num3 + 6].y = nqV.zz.y;
					m.v[num3 + 6].u = nqT.zz.x;
					m.v[num3 + 6].v = nqT.zz.y;
					m.v[num3 + 7].x = nqV.ww.x;
					m.v[num3 + 7].y = nqV.zz.y;
					m.v[num3 + 7].u = nqT.ww.x;
					m.v[num3 + 7].v = nqT.zz.y;
					m.v[num3 + 8].x = nqV.xx.x;
					m.v[num3 + 8].y = nqV.ww.y;
					m.v[num3 + 8].u = nqT.xx.x;
					m.v[num3 + 8].v = nqT.ww.y;
					m.v[num3 + 9].x = nqV.yy.x;
					m.v[num3 + 9].y = nqV.ww.y;
					m.v[num3 + 9].u = nqT.yy.x;
					m.v[num3 + 9].v = nqT.ww.y;
					m.v[num3 + 10].x = nqV.zz.x;
					m.v[num3 + 10].y = nqV.ww.y;
					m.v[num3 + 10].u = nqT.zz.x;
					m.v[num3 + 10].v = nqT.ww.y;
					m.v[num3 + 11].x = nqV.ww.x;
					m.v[num3 + 11].y = nqV.ww.y;
					m.v[num3 + 11].u = nqT.ww.x;
					m.v[num3 + 11].v = nqT.ww.y;
				}
			}
			else if (nqV.yy.y == nqV.zz.y)
			{
				if (nqV.zz.y == nqV.ww.y)
				{
					int num4 = m.Alloc(PrimitiveKind.Grid3x1, 0f, color);
					m.v[num4].x = nqV.xx.x;
					m.v[num4].y = nqV.xx.y;
					m.v[num4].u = nqT.xx.x;
					m.v[num4].v = nqT.xx.y;
					m.v[num4 + 1].x = nqV.yy.x;
					m.v[num4 + 1].y = nqV.xx.y;
					m.v[num4 + 1].u = nqT.yy.x;
					m.v[num4 + 1].v = nqT.xx.y;
					m.v[num4 + 2].x = nqV.zz.x;
					m.v[num4 + 2].y = nqV.xx.y;
					m.v[num4 + 2].u = nqT.zz.x;
					m.v[num4 + 2].v = nqT.xx.y;
					m.v[num4 + 3].x = nqV.ww.x;
					m.v[num4 + 3].y = nqV.xx.y;
					m.v[num4 + 3].u = nqT.ww.x;
					m.v[num4 + 3].v = nqT.xx.y;
					m.v[num4 + 4].x = nqV.xx.x;
					m.v[num4 + 4].y = nqV.yy.y;
					m.v[num4 + 4].u = nqT.xx.x;
					m.v[num4 + 4].v = nqT.yy.y;
					m.v[num4 + 5].x = nqV.yy.x;
					m.v[num4 + 5].y = nqV.yy.y;
					m.v[num4 + 5].u = nqT.yy.x;
					m.v[num4 + 5].v = nqT.yy.y;
					m.v[num4 + 6].x = nqV.zz.x;
					m.v[num4 + 6].y = nqV.yy.y;
					m.v[num4 + 6].u = nqT.zz.x;
					m.v[num4 + 6].v = nqT.yy.y;
					m.v[num4 + 7].x = nqV.ww.x;
					m.v[num4 + 7].y = nqV.yy.y;
					m.v[num4 + 7].u = nqT.ww.x;
					m.v[num4 + 7].v = nqT.yy.y;
				}
				else
				{
					int num5 = m.Alloc(PrimitiveKind.Grid3x1, 0f, color);
					m.v[num5].x = nqV.xx.x;
					m.v[num5].y = nqV.xx.y;
					m.v[num5].u = nqT.xx.x;
					m.v[num5].v = nqT.xx.y;
					m.v[num5 + 1].x = nqV.yy.x;
					m.v[num5 + 1].y = nqV.xx.y;
					m.v[num5 + 1].u = nqT.yy.x;
					m.v[num5 + 1].v = nqT.xx.y;
					m.v[num5 + 2].x = nqV.zz.x;
					m.v[num5 + 2].y = nqV.xx.y;
					m.v[num5 + 2].u = nqT.zz.x;
					m.v[num5 + 2].v = nqT.xx.y;
					m.v[num5 + 3].x = nqV.ww.x;
					m.v[num5 + 3].y = nqV.xx.y;
					m.v[num5 + 3].u = nqT.ww.x;
					m.v[num5 + 3].v = nqT.xx.y;
					m.v[num5 + 4].x = nqV.xx.x;
					m.v[num5 + 4].y = nqV.yy.y;
					m.v[num5 + 4].u = nqT.xx.x;
					m.v[num5 + 4].v = nqT.yy.y;
					m.v[num5 + 5].x = nqV.yy.x;
					m.v[num5 + 5].y = nqV.yy.y;
					m.v[num5 + 5].u = nqT.yy.x;
					m.v[num5 + 5].v = nqT.yy.y;
					m.v[num5 + 6].x = nqV.zz.x;
					m.v[num5 + 6].y = nqV.yy.y;
					m.v[num5 + 6].u = nqT.zz.x;
					m.v[num5 + 6].v = nqT.yy.y;
					m.v[num5 + 7].x = nqV.ww.x;
					m.v[num5 + 7].y = nqV.yy.y;
					m.v[num5 + 7].u = nqT.ww.x;
					m.v[num5 + 7].v = nqT.yy.y;
					num5 = m.Alloc(PrimitiveKind.Grid3x1, 0f, color);
					m.v[num5].x = nqV.xx.x;
					m.v[num5].y = nqV.zz.y;
					m.v[num5].u = nqT.xx.x;
					m.v[num5].v = nqT.zz.y;
					m.v[num5 + 1].x = nqV.yy.x;
					m.v[num5 + 1].y = nqV.zz.y;
					m.v[num5 + 1].u = nqT.yy.x;
					m.v[num5 + 1].v = nqT.zz.y;
					m.v[num5 + 2].x = nqV.zz.x;
					m.v[num5 + 2].y = nqV.zz.y;
					m.v[num5 + 2].u = nqT.zz.x;
					m.v[num5 + 2].v = nqT.zz.y;
					m.v[num5 + 3].x = nqV.ww.x;
					m.v[num5 + 3].y = nqV.zz.y;
					m.v[num5 + 3].u = nqT.ww.x;
					m.v[num5 + 3].v = nqT.zz.y;
					m.v[num5 + 4].x = nqV.xx.x;
					m.v[num5 + 4].y = nqV.ww.y;
					m.v[num5 + 4].u = nqT.xx.x;
					m.v[num5 + 4].v = nqT.ww.y;
					m.v[num5 + 5].x = nqV.yy.x;
					m.v[num5 + 5].y = nqV.ww.y;
					m.v[num5 + 5].u = nqT.yy.x;
					m.v[num5 + 5].v = nqT.ww.y;
					m.v[num5 + 6].x = nqV.zz.x;
					m.v[num5 + 6].y = nqV.ww.y;
					m.v[num5 + 6].u = nqT.zz.x;
					m.v[num5 + 6].v = nqT.ww.y;
					m.v[num5 + 7].x = nqV.ww.x;
					m.v[num5 + 7].y = nqV.ww.y;
					m.v[num5 + 7].u = nqT.ww.x;
					m.v[num5 + 7].v = nqT.ww.y;
				}
			}
			else if (nqV.zz.y == nqV.ww.y)
			{
				int num6 = m.Alloc(PrimitiveKind.Grid3x2, 0f, color);
				m.v[num6].x = nqV.xx.x;
				m.v[num6].y = nqV.xx.y;
				m.v[num6].u = nqT.xx.x;
				m.v[num6].v = nqT.xx.y;
				m.v[num6 + 1].x = nqV.yy.x;
				m.v[num6 + 1].y = nqV.xx.y;
				m.v[num6 + 1].u = nqT.yy.x;
				m.v[num6 + 1].v = nqT.xx.y;
				m.v[num6 + 2].x = nqV.zz.x;
				m.v[num6 + 2].y = nqV.xx.y;
				m.v[num6 + 2].u = nqT.zz.x;
				m.v[num6 + 2].v = nqT.xx.y;
				m.v[num6 + 3].x = nqV.ww.x;
				m.v[num6 + 3].y = nqV.xx.y;
				m.v[num6 + 3].u = nqT.ww.x;
				m.v[num6 + 3].v = nqT.xx.y;
				m.v[num6 + 4].x = nqV.xx.x;
				m.v[num6 + 4].y = nqV.yy.y;
				m.v[num6 + 4].u = nqT.xx.x;
				m.v[num6 + 4].v = nqT.yy.y;
				m.v[num6 + 5].x = nqV.yy.x;
				m.v[num6 + 5].y = nqV.yy.y;
				m.v[num6 + 5].u = nqT.yy.x;
				m.v[num6 + 5].v = nqT.yy.y;
				m.v[num6 + 6].x = nqV.zz.x;
				m.v[num6 + 6].y = nqV.yy.y;
				m.v[num6 + 6].u = nqT.zz.x;
				m.v[num6 + 6].v = nqT.yy.y;
				m.v[num6 + 7].x = nqV.ww.x;
				m.v[num6 + 7].y = nqV.yy.y;
				m.v[num6 + 7].u = nqT.ww.x;
				m.v[num6 + 7].v = nqT.yy.y;
				m.v[num6 + 8].x = nqV.xx.x;
				m.v[num6 + 8].y = nqV.zz.y;
				m.v[num6 + 8].u = nqT.xx.x;
				m.v[num6 + 8].v = nqT.zz.y;
				m.v[num6 + 9].x = nqV.yy.x;
				m.v[num6 + 9].y = nqV.zz.y;
				m.v[num6 + 9].u = nqT.yy.x;
				m.v[num6 + 9].v = nqT.zz.y;
				m.v[num6 + 10].x = nqV.zz.x;
				m.v[num6 + 10].y = nqV.zz.y;
				m.v[num6 + 10].u = nqT.zz.x;
				m.v[num6 + 10].v = nqT.zz.y;
				m.v[num6 + 11].x = nqV.ww.x;
				m.v[num6 + 11].y = nqV.zz.y;
				m.v[num6 + 11].u = nqT.ww.x;
				m.v[num6 + 11].v = nqT.zz.y;
			}
			else
			{
				NineRectangle.Commit3x3(m.Alloc(PrimitiveKind.Grid3x3), ref nqV, ref nqT, ref color, m);
			}
		}

		// Token: 0x06004999 RID: 18841 RVA: 0x001383EC File Offset: 0x001365EC
		public static void Fill8(ref NineRectangle nqV, ref NineRectangle nqT, ref Color color, MeshBuffer m)
		{
			NineRectangle.Commit3x3(m.Alloc(PrimitiveKind.Hole3x3), ref nqV, ref nqT, ref color, m);
		}

		// Token: 0x0600499A RID: 18842 RVA: 0x00138400 File Offset: 0x00136600
		private static void Commit3x3(int start, ref NineRectangle nqV, ref NineRectangle nqT, ref Color color, MeshBuffer m)
		{
			m.v[start].x = nqV.xx.x;
			m.v[start].y = nqV.xx.y;
			m.v[start].u = nqT.xx.x;
			m.v[start].v = nqT.xx.y;
			m.v[start + 1].x = nqV.yx.x;
			m.v[start + 1].y = nqV.yx.y;
			m.v[start + 1].u = nqT.yx.x;
			m.v[start + 1].v = nqT.yx.y;
			m.v[start + 2].x = nqV.zx.x;
			m.v[start + 2].y = nqV.zx.y;
			m.v[start + 2].u = nqT.zx.x;
			m.v[start + 2].v = nqT.zx.y;
			m.v[start + 3].x = nqV.wx.x;
			m.v[start + 3].y = nqV.wx.y;
			m.v[start + 3].u = nqT.wx.x;
			m.v[start + 3].v = nqT.wx.y;
			m.v[start + 4].x = nqV.xy.x;
			m.v[start + 4].y = nqV.xy.y;
			m.v[start + 4].u = nqT.xy.x;
			m.v[start + 4].v = nqT.xy.y;
			m.v[start + 1 + 4].x = nqV.yy.x;
			m.v[start + 1 + 4].y = nqV.yy.y;
			m.v[start + 1 + 4].u = nqT.yy.x;
			m.v[start + 1 + 4].v = nqT.yy.y;
			m.v[start + 2 + 4].x = nqV.zy.x;
			m.v[start + 2 + 4].y = nqV.zy.y;
			m.v[start + 2 + 4].u = nqT.zy.x;
			m.v[start + 2 + 4].v = nqT.zy.y;
			m.v[start + 3 + 4].x = nqV.wy.x;
			m.v[start + 3 + 4].y = nqV.wy.y;
			m.v[start + 3 + 4].u = nqT.wy.x;
			m.v[start + 3 + 4].v = nqT.wy.y;
			m.v[start + 8].x = nqV.xz.x;
			m.v[start + 8].y = nqV.xz.y;
			m.v[start + 8].u = nqT.xz.x;
			m.v[start + 8].v = nqT.xz.y;
			m.v[start + 1 + 8].x = nqV.yz.x;
			m.v[start + 1 + 8].y = nqV.yz.y;
			m.v[start + 1 + 8].u = nqT.yz.x;
			m.v[start + 1 + 8].v = nqT.yz.y;
			m.v[start + 2 + 8].x = nqV.zz.x;
			m.v[start + 2 + 8].y = nqV.zz.y;
			m.v[start + 2 + 8].u = nqT.zz.x;
			m.v[start + 2 + 8].v = nqT.zz.y;
			m.v[start + 3 + 8].x = nqV.wz.x;
			m.v[start + 3 + 8].y = nqV.wz.y;
			m.v[start + 3 + 8].u = nqT.wz.x;
			m.v[start + 3 + 8].v = nqT.wz.y;
			m.v[start + 12].x = nqV.xw.x;
			m.v[start + 12].y = nqV.xw.y;
			m.v[start + 12].u = nqT.xw.x;
			m.v[start + 12].v = nqT.xw.y;
			m.v[start + 1 + 12].x = nqV.yw.x;
			m.v[start + 1 + 12].y = nqV.yw.y;
			m.v[start + 1 + 12].u = nqT.yw.x;
			m.v[start + 1 + 12].v = nqT.yw.y;
			m.v[start + 2 + 12].x = nqV.zw.x;
			m.v[start + 2 + 12].y = nqV.zw.y;
			m.v[start + 2 + 12].u = nqT.zw.x;
			m.v[start + 2 + 12].v = nqT.zw.y;
			m.v[start + 3 + 12].x = nqV.ww.x;
			m.v[start + 3 + 12].y = nqV.ww.y;
			m.v[start + 3 + 12].u = nqT.ww.x;
			m.v[start + 3 + 12].v = nqT.ww.y;
			for (int i = 0; i < 16; i++)
			{
				m.v[start + i].z = 0f;
				m.v[start + i].r = color.r;
				m.v[start + i].g = color.g;
				m.v[start + i].b = color.b;
				m.v[start + i].a = color.a;
			}
		}

		// Token: 0x0400298B RID: 10635
		private const PrimitiveKind GRID_3ROWS_3COLUMNS = PrimitiveKind.Grid3x3;

		// Token: 0x0400298C RID: 10636
		private const PrimitiveKind GRID_3ROWS_2COLUMNS = PrimitiveKind.Grid2x3;

		// Token: 0x0400298D RID: 10637
		private const PrimitiveKind GRID_3ROWS_1COLUMNS = PrimitiveKind.Grid1x3;

		// Token: 0x0400298E RID: 10638
		private const PrimitiveKind GRID_2ROWS_3COLUMNS = PrimitiveKind.Grid3x2;

		// Token: 0x0400298F RID: 10639
		private const PrimitiveKind GRID_2ROWS_2COLUMNS = PrimitiveKind.Grid2x2;

		// Token: 0x04002990 RID: 10640
		private const PrimitiveKind GRID_2ROWS_1COLUMNS = PrimitiveKind.Grid1x2;

		// Token: 0x04002991 RID: 10641
		private const PrimitiveKind GRID_1ROWS_3COLUMNS = PrimitiveKind.Grid3x1;

		// Token: 0x04002992 RID: 10642
		private const PrimitiveKind GRID_1ROWS_2COLUMNS = PrimitiveKind.Grid2x1;

		// Token: 0x04002993 RID: 10643
		private const PrimitiveKind GRID_1ROWS_1COLUMNS = PrimitiveKind.Quad;

		// Token: 0x04002994 RID: 10644
		[FieldOffset(0)]
		public Vector2 xx;

		// Token: 0x04002995 RID: 10645
		[FieldOffset(8)]
		public Vector2 yy;

		// Token: 0x04002996 RID: 10646
		[FieldOffset(16)]
		public Vector2 zz;

		// Token: 0x04002997 RID: 10647
		[FieldOffset(24)]
		public Vector2 ww;
	}
}
