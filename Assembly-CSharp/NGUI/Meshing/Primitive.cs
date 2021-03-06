﻿using System;
using UnityEngine;

namespace NGUI.Meshing
{
	// Token: 0x0200088D RID: 2189
	public struct Primitive
	{
		// Token: 0x06004B20 RID: 19232 RVA: 0x00122B80 File Offset: 0x00120D80
		public Primitive(PrimitiveKind kind, ushort start)
		{
			this.kind = kind;
			this.start = start;
		}

		// Token: 0x06004B21 RID: 19233 RVA: 0x00122B90 File Offset: 0x00120D90
		public static int VertexCount(PrimitiveKind kind)
		{
			switch (kind)
			{
			case PrimitiveKind.Triangle:
				return 3;
			case PrimitiveKind.Quad:
				return 4;
			case PrimitiveKind.Grid2x1:
			case PrimitiveKind.Grid1x2:
				return 6;
			case PrimitiveKind.Grid2x2:
				return 9;
			case PrimitiveKind.Grid1x3:
			case PrimitiveKind.Grid3x1:
				return 8;
			case PrimitiveKind.Grid3x2:
			case PrimitiveKind.Grid2x3:
				return 12;
			case PrimitiveKind.Grid3x3:
			case PrimitiveKind.Hole3x3:
				return 16;
			default:
				throw new NotImplementedException();
			}
		}

		// Token: 0x06004B22 RID: 19234 RVA: 0x00122BEC File Offset: 0x00120DEC
		public static int IndexCount(PrimitiveKind kind)
		{
			switch (kind)
			{
			case PrimitiveKind.Triangle:
				return 3;
			case PrimitiveKind.Quad:
				return 6;
			case PrimitiveKind.Grid2x1:
			case PrimitiveKind.Grid1x2:
				return 12;
			case PrimitiveKind.Grid2x2:
				return 24;
			case PrimitiveKind.Grid1x3:
			case PrimitiveKind.Grid3x1:
				return 18;
			case PrimitiveKind.Grid3x2:
			case PrimitiveKind.Grid2x3:
				return 36;
			case PrimitiveKind.Grid3x3:
				return 54;
			case PrimitiveKind.Hole3x3:
				return 48;
			default:
				throw new NotImplementedException();
			}
		}

		// Token: 0x06004B23 RID: 19235 RVA: 0x00122C50 File Offset: 0x00120E50
		public static bool JoinsInList(PrimitiveKind kind)
		{
			return true;
		}

		// Token: 0x06004B24 RID: 19236 RVA: 0x00122C54 File Offset: 0x00120E54
		public void Copy(ref int start, Vertex[] v, int end, MeshBuffer p)
		{
			int num = (end - start) / Primitive.VertexCount(this.kind);
			while (num-- > 0)
			{
				int num2;
				int i = p.Alloc(this.kind, out num2);
				while (i < num2)
				{
					p.v[i++] = v[start++];
				}
			}
		}

		// Token: 0x06004B25 RID: 19237 RVA: 0x00122CC8 File Offset: 0x00120EC8
		public void Copy(ref int start, Vertex[] v, Vector3[] transformed, int end, MeshBuffer p)
		{
			int num = (end - start) / Primitive.VertexCount(this.kind);
			while (num-- > 0)
			{
				int num2;
				int i = p.Alloc(this.kind, out num2);
				while (i < num2)
				{
					p.v[i].x = transformed[start].x;
					p.v[i].y = transformed[start].y;
					p.v[i].z = transformed[start].z;
					p.v[i].u = v[start].u;
					p.v[i].v = v[start].v;
					p.v[i].r = v[start].r;
					p.v[i].g = v[start].g;
					p.v[i].b = v[start].b;
					p.v[i].a = v[start].a;
					i++;
					start++;
				}
			}
		}

		// Token: 0x06004B26 RID: 19238 RVA: 0x00122E34 File Offset: 0x00121034
		public void Put(int[] t, ref int v, ref int i, int end)
		{
			int num = (end - (int)this.start) / Primitive.VertexCount(this.kind);
			switch (this.kind)
			{
			case PrimitiveKind.Triangle:
				while (num-- > 0)
				{
					t[i++] = v;
					t[i++] = v + 1;
					t[i++] = v + 2;
					v += 3;
				}
				break;
			case PrimitiveKind.Quad:
				while (num-- > 0)
				{
					t[i++] = v;
					t[i++] = v + 1;
					t[i++] = v + 3;
					t[i++] = v + 2;
					t[i++] = v;
					t[i++] = v + 3;
					v += 4;
				}
				break;
			case PrimitiveKind.Grid2x1:
				while (num-- > 0)
				{
					for (int j = 0; j < 2; j++)
					{
						for (int k = 0; k < 1; k++)
						{
							t[i++] = v + (j + k * 3);
							t[i++] = v + (j + 1 + k * 3);
							t[i++] = v + (j + (k + 1) * 3);
							t[i++] = v + (j + 1 + k * 3);
							t[i++] = v + (j + 1 + (k + 1) * 3);
							t[i++] = v + (j + (k + 1) * 3);
						}
					}
					v += 6;
				}
				break;
			case PrimitiveKind.Grid1x2:
				while (num-- > 0)
				{
					for (int l = 0; l < 1; l++)
					{
						for (int m = 0; m < 2; m++)
						{
							t[i++] = v + (l + m * 2);
							t[i++] = v + (l + 1 + m * 2);
							t[i++] = v + (l + (m + 1) * 2);
							t[i++] = v + (l + 1 + m * 2);
							t[i++] = v + (l + 1 + (m + 1) * 2);
							t[i++] = v + (l + (m + 1) * 2);
						}
					}
					v += 6;
				}
				break;
			case PrimitiveKind.Grid2x2:
				while (num-- > 0)
				{
					for (int n = 0; n < 2; n++)
					{
						for (int num2 = 0; num2 < 2; num2++)
						{
							t[i++] = v + (n + num2 * 3);
							t[i++] = v + (n + 1 + num2 * 3);
							t[i++] = v + (n + (num2 + 1) * 3);
							t[i++] = v + (n + 1 + num2 * 3);
							t[i++] = v + (n + 1 + (num2 + 1) * 3);
							t[i++] = v + (n + (num2 + 1) * 3);
						}
					}
					v += 9;
				}
				break;
			case PrimitiveKind.Grid1x3:
				while (num-- > 0)
				{
					for (int num3 = 0; num3 < 1; num3++)
					{
						for (int num4 = 0; num4 < 3; num4++)
						{
							t[i++] = v + (num3 + num4 * 2);
							t[i++] = v + (num3 + 1 + num4 * 2);
							t[i++] = v + (num3 + (num4 + 1) * 2);
							t[i++] = v + (num3 + 1 + num4 * 2);
							t[i++] = v + (num3 + 1 + (num4 + 1) * 2);
							t[i++] = v + (num3 + (num4 + 1) * 2);
						}
					}
					v += 8;
				}
				break;
			case PrimitiveKind.Grid3x1:
				while (num-- > 0)
				{
					for (int num5 = 0; num5 < 3; num5++)
					{
						for (int num6 = 0; num6 < 1; num6++)
						{
							t[i++] = v + (num5 + num6 * 4);
							t[i++] = v + (num5 + 1 + num6 * 4);
							t[i++] = v + (num5 + (num6 + 1) * 4);
							t[i++] = v + (num5 + 1 + num6 * 4);
							t[i++] = v + (num5 + 1 + (num6 + 1) * 4);
							t[i++] = v + (num5 + (num6 + 1) * 4);
						}
					}
					v += 8;
				}
				break;
			case PrimitiveKind.Grid3x2:
				while (num-- > 0)
				{
					for (int num7 = 0; num7 < 3; num7++)
					{
						for (int num8 = 0; num8 < 2; num8++)
						{
							t[i++] = v + (num7 + num8 * 4);
							t[i++] = v + (num7 + 1 + num8 * 4);
							t[i++] = v + (num7 + (num8 + 1) * 4);
							t[i++] = v + (num7 + 1 + num8 * 4);
							t[i++] = v + (num7 + 1 + (num8 + 1) * 4);
							t[i++] = v + (num7 + (num8 + 1) * 4);
						}
					}
					v += 12;
				}
				break;
			case PrimitiveKind.Grid2x3:
				while (num-- > 0)
				{
					for (int num9 = 0; num9 < 2; num9++)
					{
						for (int num10 = 0; num10 < 3; num10++)
						{
							t[i++] = v + (num9 + num10 * 3);
							t[i++] = v + (num9 + 1 + num10 * 3);
							t[i++] = v + (num9 + (num10 + 1) * 3);
							t[i++] = v + (num9 + 1 + num10 * 3);
							t[i++] = v + (num9 + 1 + (num10 + 1) * 3);
							t[i++] = v + (num9 + (num10 + 1) * 3);
						}
					}
					v += 12;
				}
				break;
			case PrimitiveKind.Grid3x3:
				while (num-- > 0)
				{
					for (int num11 = 0; num11 < 3; num11++)
					{
						for (int num12 = 0; num12 < 3; num12++)
						{
							t[i++] = v + (num11 + num12 * 4);
							t[i++] = v + (num11 + 1 + num12 * 4);
							t[i++] = v + (num11 + (num12 + 1) * 4);
							t[i++] = v + (num11 + 1 + num12 * 4);
							t[i++] = v + (num11 + 1 + (num12 + 1) * 4);
							t[i++] = v + (num11 + (num12 + 1) * 4);
						}
					}
					v += 16;
				}
				break;
			case PrimitiveKind.Hole3x3:
				while (num-- > 0)
				{
					for (int num13 = 0; num13 < 3; num13++)
					{
						for (int num14 = 0; num14 < 3; num14++)
						{
							if (num13 != 1 || num14 != 1)
							{
								t[i++] = v + (num13 + num14 * 4);
								t[i++] = v + (num13 + 1 + num14 * 4);
								t[i++] = v + (num13 + (num14 + 1) * 4);
								t[i++] = v + (num13 + 1 + num14 * 4);
								t[i++] = v + (num13 + 1 + (num14 + 1) * 4);
								t[i++] = v + (num13 + (num14 + 1) * 4);
							}
						}
					}
					v += 16;
				}
				break;
			default:
				throw new NotImplementedException();
			}
		}

		// Token: 0x04002908 RID: 10504
		public readonly PrimitiveKind kind;

		// Token: 0x04002909 RID: 10505
		public readonly ushort start;
	}
}
