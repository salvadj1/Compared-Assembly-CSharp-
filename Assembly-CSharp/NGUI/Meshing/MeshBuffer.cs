using System;
using UnityEngine;

namespace NGUI.Meshing
{
	// Token: 0x0200088E RID: 2190
	public class MeshBuffer
	{
		// Token: 0x06004B28 RID: 19240 RVA: 0x0012373C File Offset: 0x0012193C
		private static int Gen_Alloc<T>(int count, ref int size, ref int cap, ref T[] array, int initAllocSize, int maxAllocSize, int maxAllocSizeIncrement)
		{
			if (count <= 0)
			{
				return -1;
			}
			int result = size;
			size += count;
			if (size > cap)
			{
				if (cap == 0)
				{
					cap = initAllocSize;
				}
				while (cap < size)
				{
					if (cap < maxAllocSize)
					{
						cap *= 2;
					}
					else
					{
						cap += maxAllocSizeIncrement;
					}
				}
				Array.Resize<T>(ref array, cap);
			}
			return result;
		}

		// Token: 0x06004B29 RID: 19241 RVA: 0x001237A4 File Offset: 0x001219A4
		public int Alloc(PrimitiveKind kind, out int end)
		{
			int num = Primitive.VertexCount(kind);
			if (this.lastPrimitiveKind != kind)
			{
				int num2 = MeshBuffer.Gen_Alloc<Primitive>(1, ref this.primSize, ref this.primCapacity, ref this.primitives, 4, 32, 32);
				if (Primitive.JoinsInList(kind))
				{
					this.lastPrimitiveKind = kind;
				}
				else
				{
					this.lastPrimitiveKind = PrimitiveKind.Invalid;
				}
				this.primitives[num2] = new Primitive(kind, (ushort)this.vSize);
			}
			this.iCount += Primitive.IndexCount(kind);
			int num3 = MeshBuffer.Gen_Alloc<Vertex>(num, ref this.vSize, ref this.vertCapacity, ref this.v, 32, 512, 512);
			end = num3 + num;
			return num3;
		}

		// Token: 0x06004B2A RID: 19242 RVA: 0x00123860 File Offset: 0x00121A60
		public int Alloc(PrimitiveKind primitive, Color color, out int end)
		{
			int num = this.Alloc(primitive, out end);
			float r = color.r;
			float g = color.g;
			float b = color.b;
			float a = color.a;
			for (int i = num; i < end; i++)
			{
				this.v[i].r = r;
				this.v[i].g = g;
				this.v[i].b = b;
				this.v[i].a = a;
			}
			return num;
		}

		// Token: 0x06004B2B RID: 19243 RVA: 0x00123900 File Offset: 0x00121B00
		public int Alloc(PrimitiveKind primitive, float z, Color color, out int end)
		{
			int num = this.Alloc(primitive, out end);
			float r = color.r;
			float g = color.g;
			float b = color.b;
			float a = color.a;
			for (int i = num; i < end; i++)
			{
				this.v[i].r = r;
				this.v[i].g = g;
				this.v[i].b = b;
				this.v[i].a = a;
				this.v[i].z = z;
			}
			return num;
		}

		// Token: 0x06004B2C RID: 19244 RVA: 0x001239B8 File Offset: 0x00121BB8
		public int Alloc(PrimitiveKind primitive, float z, ref Color color, out int end)
		{
			int num = this.Alloc(primitive, out end);
			float r = color.r;
			float g = color.g;
			float b = color.b;
			float a = color.a;
			for (int i = num; i < end; i++)
			{
				this.v[i].r = r;
				this.v[i].g = g;
				this.v[i].b = b;
				this.v[i].a = a;
				this.v[i].z = z;
			}
			return num;
		}

		// Token: 0x06004B2D RID: 19245 RVA: 0x00123A6C File Offset: 0x00121C6C
		public int Alloc(PrimitiveKind primitive, float z, out int end)
		{
			int num = this.Alloc(primitive, out end);
			for (int i = num; i < end; i++)
			{
				this.v[i].z = z;
				this.v[i].r = (this.v[i].g = (this.v[i].b = (this.v[i].a = 1f)));
			}
			return num;
		}

		// Token: 0x06004B2E RID: 19246 RVA: 0x00123AFC File Offset: 0x00121CFC
		public int Alloc(PrimitiveKind primitive, Vertex V, out int end)
		{
			int num = this.Alloc(primitive, out end);
			for (int i = num; i < end; i++)
			{
				this.v[i].x = V.x;
				this.v[i].y = V.y;
				this.v[i].r = V.r;
				this.v[i].u = V.u;
				this.v[i].v = V.v;
				this.v[i].g = V.g;
				this.v[i].b = V.b;
				this.v[i].a = V.a;
			}
			return num;
		}

		// Token: 0x06004B2F RID: 19247 RVA: 0x00123BE8 File Offset: 0x00121DE8
		public int Alloc(PrimitiveKind kind)
		{
			int num;
			return this.Alloc(kind, out num);
		}

		// Token: 0x06004B30 RID: 19248 RVA: 0x00123C00 File Offset: 0x00121E00
		public int Alloc(PrimitiveKind kind, Color color)
		{
			int num;
			return this.Alloc(kind, color, out num);
		}

		// Token: 0x06004B31 RID: 19249 RVA: 0x00123C18 File Offset: 0x00121E18
		public int Alloc(PrimitiveKind kind, float z)
		{
			int num;
			return this.Alloc(kind, z, out num);
		}

		// Token: 0x06004B32 RID: 19250 RVA: 0x00123C30 File Offset: 0x00121E30
		public int Alloc(PrimitiveKind kind, float z, Color color)
		{
			int num;
			return this.Alloc(kind, z, color, out num);
		}

		// Token: 0x06004B33 RID: 19251 RVA: 0x00123C48 File Offset: 0x00121E48
		public int Alloc(PrimitiveKind kind, float z, ref Color color)
		{
			int num;
			return this.Alloc(kind, z, ref color, out num);
		}

		// Token: 0x06004B34 RID: 19252 RVA: 0x00123C60 File Offset: 0x00121E60
		public int Alloc(PrimitiveKind kind, Vertex v)
		{
			int num;
			return this.Alloc(kind, v, out num);
		}

		// Token: 0x06004B35 RID: 19253 RVA: 0x00123C78 File Offset: 0x00121E78
		public int Triangle(Vertex A, Vertex B, Vertex C)
		{
			int num2;
			int num = this.Alloc(PrimitiveKind.Triangle, out num2);
			int num3 = num;
			this.v[num3++] = A;
			this.v[num3++] = B;
			this.v[num3++] = C;
			return num;
		}

		// Token: 0x06004B36 RID: 19254 RVA: 0x00123CD4 File Offset: 0x00121ED4
		public int Quad(Vertex A, Vertex B, Vertex C, Vertex D)
		{
			int num2;
			int num = this.Alloc(PrimitiveKind.Quad, out num2);
			int num3 = num;
			this.v[num3++] = B;
			this.v[num3++] = A;
			this.v[num3++] = C;
			this.v[num3++] = D;
			return num;
		}

		// Token: 0x06004B37 RID: 19255 RVA: 0x00123D48 File Offset: 0x00121F48
		public int QuadAlt(Vertex A, Vertex B, Vertex C, Vertex D)
		{
			return this.Quad(D, A, B, C);
		}

		// Token: 0x06004B38 RID: 19256 RVA: 0x00123D58 File Offset: 0x00121F58
		public int TextureQuad(Vertex A, Vertex B, Vertex C, Vertex D)
		{
			int num2;
			int num = this.Alloc(PrimitiveKind.Quad, out num2);
			int num3 = num;
			this.v[num3++] = D;
			this.v[num3++] = A;
			this.v[num3++] = C;
			this.v[num3++] = B;
			return num;
		}

		// Token: 0x06004B39 RID: 19257 RVA: 0x00123DCC File Offset: 0x00121FCC
		public int FastQuad(Vector2 uv0, Vector2 uv1, Color color)
		{
			Vertex a;
			Vertex b;
			a.x = (b.x = 1f);
			Vertex c;
			b.y = (c.y = -1f);
			Vertex d;
			a.y = (a.z = (b.z = (c.x = (c.z = (d.x = (d.y = (d.z = 0f)))))));
			a.u = (b.u = uv1.x);
			a.v = (d.v = uv1.y);
			c.u = (d.u = uv0.x);
			b.v = (c.v = uv0.y);
			a.r = (b.r = (c.r = (d.r = color.r)));
			a.g = (b.g = (c.g = (d.g = color.g)));
			a.b = (b.b = (c.b = (d.b = color.b)));
			a.a = (b.a = (c.a = (d.a = color.a)));
			return this.Quad(a, b, c, d);
		}

		// Token: 0x06004B3A RID: 19258 RVA: 0x00123FA4 File Offset: 0x001221A4
		public int FastQuad(Rect uv, Color color)
		{
			return this.FastQuad(new Vector2(uv.xMin, uv.yMin), new Vector2(uv.xMax, uv.yMax), color);
		}

		// Token: 0x06004B3B RID: 19259 RVA: 0x00123FE0 File Offset: 0x001221E0
		public int FastQuad(Vector2 xy0, Vector2 xy1, Vector2 uv0, Vector2 uv1, Color color)
		{
			Vertex a;
			Vertex b;
			a.x = (b.x = xy1.x);
			Vertex d;
			a.y = (d.y = xy1.y);
			Vertex c;
			c.x = (d.x = xy0.x);
			b.y = (c.y = xy0.y);
			a.z = (b.z = (c.z = (d.z = 0f)));
			a.u = (b.u = uv1.x);
			a.v = (d.v = uv1.y);
			c.u = (d.u = uv0.x);
			b.v = (c.v = uv0.y);
			a.r = (b.r = (c.r = (d.r = color.r)));
			a.g = (b.g = (c.g = (d.g = color.g)));
			a.b = (b.b = (c.b = (d.b = color.b)));
			a.a = (b.a = (c.a = (d.a = color.a)));
			return this.Quad(a, b, c, d);
		}

		// Token: 0x06004B3C RID: 19260 RVA: 0x001241C0 File Offset: 0x001223C0
		public void FastCell(Vector2 xy0, Vector2 xy1, Vector2 uv0, Vector2 uv1, ref Color color)
		{
			Vertex a;
			Vertex b;
			a.x = (b.x = xy1.x);
			Vertex d;
			a.y = (d.y = xy1.y);
			Vertex c;
			c.x = (d.x = xy0.x);
			b.y = (c.y = xy0.y);
			a.z = (b.z = (c.z = (d.z = 0f)));
			a.u = (b.u = uv1.x);
			a.v = (d.v = uv1.y);
			c.u = (d.u = uv0.x);
			b.v = (c.v = uv0.y);
			a.r = (b.r = (c.r = (d.r = color.r)));
			a.g = (b.g = (c.g = (d.g = color.g)));
			a.b = (b.b = (c.b = (d.b = color.b)));
			a.a = (b.a = (c.a = (d.a = color.a)));
			this.Quad(a, b, c, d);
		}

		// Token: 0x06004B3D RID: 19261 RVA: 0x001243A0 File Offset: 0x001225A0
		public int FastQuad(Vector2 xy0, Vector2 xy1, float z, Vector2 uv0, Vector2 uv1, Color color)
		{
			Vertex a;
			Vertex b;
			a.x = (b.x = xy1.x);
			Vertex d;
			a.y = (d.y = xy1.y);
			Vertex c;
			c.x = (d.x = xy0.x);
			b.y = (c.y = xy0.y);
			d.z = z;
			c.z = z;
			b.z = z;
			a.z = z;
			a.u = (b.u = uv1.x);
			a.v = (d.v = uv1.y);
			c.u = (d.u = uv0.x);
			b.v = (c.v = uv0.y);
			a.r = (b.r = (c.r = (d.r = color.r)));
			a.g = (b.g = (c.g = (d.g = color.g)));
			a.b = (b.b = (c.b = (d.b = color.b)));
			a.a = (b.a = (c.a = (d.a = color.a)));
			return this.Quad(a, b, c, d);
		}

		// Token: 0x06004B3E RID: 19262 RVA: 0x0012457C File Offset: 0x0012277C
		private void Extract(MeshBuffer.FillBuffer<Vector3> vertices, MeshBuffer.FillBuffer<Vector2> uvs, MeshBuffer.FillBuffer<Color> colors, MeshBuffer.FillBuffer<int> triangles)
		{
			Vector3[] buf = vertices.buf;
			Vector2[] buf2 = uvs.buf;
			Color[] buf3 = colors.buf;
			int[] buf4 = triangles.buf;
			int num = vertices.offset;
			int num2 = uvs.offset;
			int num3 = colors.offset;
			for (int i = 0; i < this.vSize; i++)
			{
				buf[num].x = this.v[i].x;
				buf[num].y = this.v[i].y;
				buf[num].z = this.v[i].z;
				buf2[num2].x = this.v[i].u;
				buf2[num2].y = this.v[i].v;
				buf3[num3].r = this.v[i].r;
				buf3[num3].g = this.v[i].g;
				buf3[num3].b = this.v[i].b;
				buf3[num3].a = this.v[i].a;
				num++;
				num2++;
				num3++;
			}
			int offset = triangles.offset;
			int offset2 = vertices.offset;
			if (this.primSize > 0)
			{
				for (int j = 0; j < this.primSize - 1; j++)
				{
					this.primitives[j].Put(buf4, ref offset2, ref offset, (int)this.primitives[j + 1].start);
				}
				this.primitives[this.primSize - 1].Put(buf4, ref offset2, ref offset, this.vSize);
			}
		}

		// Token: 0x06004B3F RID: 19263 RVA: 0x0012479C File Offset: 0x0012299C
		private static bool ResizeChecked<T>(ref T[] array, int size)
		{
			if (size == 0)
			{
				if (array != null && array.Length != 0)
				{
					array = null;
					return true;
				}
				return false;
			}
			else
			{
				if (array == null || array.Length != size)
				{
					Array.Resize<T>(ref array, size);
					return true;
				}
				return false;
			}
		}

		// Token: 0x06004B40 RID: 19264 RVA: 0x001247E4 File Offset: 0x001229E4
		public bool ExtractMeshBuffers(ref Vector3[] vertices, ref Vector2[] uvs, ref Color[] colors, ref int[] triangles)
		{
			bool result = MeshBuffer.ResizeChecked<Vector3>(ref vertices, this.vSize) | MeshBuffer.ResizeChecked<Vector2>(ref uvs, this.vSize) | MeshBuffer.ResizeChecked<Color>(ref colors, this.vSize) | MeshBuffer.ResizeChecked<int>(ref triangles, this.iCount);
			this.Extract(new MeshBuffer.FillBuffer<Vector3>
			{
				buf = vertices
			}, new MeshBuffer.FillBuffer<Vector2>
			{
				buf = uvs
			}, new MeshBuffer.FillBuffer<Color>
			{
				buf = colors
			}, new MeshBuffer.FillBuffer<int>
			{
				buf = triangles
			});
			return result;
		}

		// Token: 0x06004B41 RID: 19265 RVA: 0x00124878 File Offset: 0x00122A78
		public void Offset(float x, float y, float z)
		{
			if (x == 0f)
			{
				if (y == 0f)
				{
					if (z == 0f)
					{
						return;
					}
					for (int i = 0; i < this.vSize; i++)
					{
						Vertex[] array = this.v;
						int num = i;
						array[num].z = array[num].z + z;
					}
				}
				else if (z == 0f)
				{
					for (int j = 0; j < this.vSize; j++)
					{
						Vertex[] array2 = this.v;
						int num2 = j;
						array2[num2].y = array2[num2].y + y;
					}
				}
				else
				{
					for (int k = 0; k < this.vSize; k++)
					{
						Vertex[] array3 = this.v;
						int num3 = k;
						array3[num3].y = array3[num3].y + y;
						Vertex[] array4 = this.v;
						int num4 = k;
						array4[num4].z = array4[num4].z + z;
					}
				}
			}
			else if (y == 0f)
			{
				if (z == 0f)
				{
					for (int l = 0; l < this.vSize; l++)
					{
						Vertex[] array5 = this.v;
						int num5 = l;
						array5[num5].x = array5[num5].x + x;
					}
				}
				else
				{
					for (int m = 0; m < this.vSize; m++)
					{
						Vertex[] array6 = this.v;
						int num6 = m;
						array6[num6].x = array6[num6].x + x;
						Vertex[] array7 = this.v;
						int num7 = m;
						array7[num7].z = array7[num7].z + z;
					}
				}
			}
			else if (z == 0f)
			{
				for (int n = 0; n < this.vSize; n++)
				{
					Vertex[] array8 = this.v;
					int num8 = n;
					array8[num8].x = array8[num8].x + x;
					Vertex[] array9 = this.v;
					int num9 = n;
					array9[num9].y = array9[num9].y + y;
				}
			}
			else
			{
				for (int num10 = 0; num10 < this.vSize; num10++)
				{
					Vertex[] array10 = this.v;
					int num11 = num10;
					array10[num11].x = array10[num11].x + x;
					Vertex[] array11 = this.v;
					int num12 = num10;
					array11[num12].y = array11[num12].y + y;
					Vertex[] array12 = this.v;
					int num13 = num10;
					array12[num13].z = array12[num13].z + z;
				}
			}
		}

		// Token: 0x06004B42 RID: 19266 RVA: 0x00124AD4 File Offset: 0x00122CD4
		public void BuildTransformedVertices4x4(ref Vector3[] tV, float m00, float m10, float m20, float m30, float m01, float m11, float m21, float m31, float m02, float m12, float m22, float m32, float m03, float m13, float m23, float m33)
		{
			Array.Resize<Vector3>(ref tV, this.vSize);
			for (int i = 0; i < this.vSize; i++)
			{
				float num = 1f / (m30 * this.v[i].x + m31 * this.v[i].y + m32 * this.v[i].z + m33);
				tV[i].x = (m00 * this.v[i].x + m01 * this.v[i].y + m02 * this.v[i].z + m03) * num;
				tV[i].y = (m10 * this.v[i].x + m11 * this.v[i].y + m12 * this.v[i].z + m13) * num;
				tV[i].z = (m20 * this.v[i].x + m21 * this.v[i].y + m22 * this.v[i].z + m23) * num;
			}
		}

		// Token: 0x06004B43 RID: 19267 RVA: 0x00124C3C File Offset: 0x00122E3C
		public void TransformVertices(float m00, float m10, float m20, float m01, float m11, float m21, float m02, float m12, float m22, float m03, float m13, float m23)
		{
			for (int i = 0; i < this.vSize; i++)
			{
				float x = this.v[i].x;
				float y = this.v[i].y;
				float z = this.v[i].z;
				this.v[i].x = m00 * x + m01 * y + m02 * z + m03;
				this.v[i].y = m10 * x + m11 * y + m12 * z + m13;
				this.v[i].z = m20 * x + m21 * y + m22 * z + m23;
			}
		}

		// Token: 0x06004B44 RID: 19268 RVA: 0x00124CFC File Offset: 0x00122EFC
		public void OffsetThenTransformVertices(float x, float y, float z, float m00, float m10, float m20, float m01, float m11, float m21, float m02, float m12, float m22, float m03, float m13, float m23)
		{
			if (x == 0f)
			{
				if (y == 0f)
				{
					if (z == 0f)
					{
						for (int i = 0; i < this.vSize; i++)
						{
							float num = this.v[i].x;
							float num2 = this.v[i].y;
							float num3 = this.v[i].z;
							this.v[i].x = m00 * num + m01 * num2 + m02 * num3 + m03;
							this.v[i].y = m10 * num + m11 * num2 + m12 * num3 + m13;
							this.v[i].z = m20 * num + m21 * num2 + m22 * num3 + m23;
						}
					}
					else
					{
						for (int j = 0; j < this.vSize; j++)
						{
							float num = this.v[j].x;
							float num2 = this.v[j].y;
							float num3 = this.v[j].z + z;
							this.v[j].x = m00 * num + m01 * num2 + m02 * num3 + m03;
							this.v[j].y = m10 * num + m11 * num2 + m12 * num3 + m13;
							this.v[j].z = m20 * num + m21 * num2 + m22 * num3 + m23;
						}
					}
				}
				else if (z == 0f)
				{
					for (int k = 0; k < this.vSize; k++)
					{
						float num = this.v[k].x;
						float num2 = this.v[k].y + y;
						float num3 = this.v[k].z;
						this.v[k].x = m00 * num + m01 * num2 + m02 * num3 + m03;
						this.v[k].y = m10 * num + m11 * num2 + m12 * num3 + m13;
						this.v[k].z = m20 * num + m21 * num2 + m22 * num3 + m23;
					}
				}
				else
				{
					for (int l = 0; l < this.vSize; l++)
					{
						float num = this.v[l].x;
						float num2 = this.v[l].y + y;
						float num3 = this.v[l].z + z;
						this.v[l].x = m00 * num + m01 * num2 + m02 * num3 + m03;
						this.v[l].y = m10 * num + m11 * num2 + m12 * num3 + m13;
						this.v[l].z = m20 * num + m21 * num2 + m22 * num3 + m23;
					}
				}
			}
			else if (y == 0f)
			{
				if (z == 0f)
				{
					for (int n = 0; n < this.vSize; n++)
					{
						float num = this.v[n].x + x;
						float num2 = this.v[n].y;
						float num3 = this.v[n].z;
						this.v[n].x = m00 * num + m01 * num2 + m02 * num3 + m03;
						this.v[n].y = m10 * num + m11 * num2 + m12 * num3 + m13;
						this.v[n].z = m20 * num + m21 * num2 + m22 * num3 + m23;
					}
				}
				else
				{
					for (int num4 = 0; num4 < this.vSize; num4++)
					{
						float num = this.v[num4].x + x;
						float num2 = this.v[num4].y;
						float num3 = this.v[num4].z + z;
						this.v[num4].x = m00 * num + m01 * num2 + m02 * num3 + m03;
						this.v[num4].y = m10 * num + m11 * num2 + m12 * num3 + m13;
						this.v[num4].z = m20 * num + m21 * num2 + m22 * num3 + m23;
					}
				}
			}
			else if (z == 0f)
			{
				for (int num5 = 0; num5 < this.vSize; num5++)
				{
					float num = this.v[num5].x + x;
					float num2 = this.v[num5].y + y;
					float num3 = this.v[num5].z;
					this.v[num5].x = m00 * num + m01 * num2 + m02 * num3 + m03;
					this.v[num5].y = m10 * num + m11 * num2 + m12 * num3 + m13;
					this.v[num5].z = m20 * num + m21 * num2 + m22 * num3 + m23;
				}
			}
			else
			{
				for (int num6 = 0; num6 < this.vSize; num6++)
				{
					float num = this.v[num6].x + x;
					float num2 = this.v[num6].y + y;
					float num3 = this.v[num6].z + z;
					this.v[num6].x = m00 * num + m01 * num2 + m02 * num3 + m03;
					this.v[num6].y = m10 * num + m11 * num2 + m12 * num3 + m13;
					this.v[num6].z = m20 * num + m21 * num2 + m22 * num3 + m23;
				}
			}
		}

		// Token: 0x06004B45 RID: 19269 RVA: 0x00125370 File Offset: 0x00123570
		public void TransformThenOffsetVertices(float m00, float m10, float m20, float m01, float m11, float m21, float m02, float m12, float m22, float m03, float m13, float m23, float x, float y, float z)
		{
			if (x == 0f)
			{
				if (y == 0f)
				{
					if (z == 0f)
					{
						for (int i = 0; i < this.vSize; i++)
						{
							float x2 = this.v[i].x;
							float y2 = this.v[i].y;
							float z2 = this.v[i].z;
							this.v[i].x = m00 * x2 + m01 * y2 + m02 * z2 + m03;
							this.v[i].y = m10 * x2 + m11 * y2 + m12 * z2 + m13;
							this.v[i].z = m20 * x2 + m21 * y2 + m22 * z2 + m23;
						}
					}
					else
					{
						for (int j = 0; j < this.vSize; j++)
						{
							float x2 = this.v[j].x;
							float y2 = this.v[j].y;
							float z2 = this.v[j].z;
							this.v[j].x = m00 * x2 + m01 * y2 + m02 * z2 + m03;
							this.v[j].y = m10 * x2 + m11 * y2 + m12 * z2 + m13;
							this.v[j].z = m20 * x2 + m21 * y2 + m22 * z2 + m23 + z;
						}
					}
				}
				else if (z == 0f)
				{
					for (int k = 0; k < this.vSize; k++)
					{
						float x2 = this.v[k].x;
						float y2 = this.v[k].y;
						float z2 = this.v[k].z;
						this.v[k].x = m00 * x2 + m01 * y2 + m02 * z2 + m03;
						this.v[k].y = m10 * x2 + m11 * y2 + m12 * z2 + m13 + y;
						this.v[k].z = m20 * x2 + m21 * y2 + m22 * z2 + m23;
					}
				}
				else
				{
					for (int l = 0; l < this.vSize; l++)
					{
						float x2 = this.v[l].x;
						float y2 = this.v[l].y;
						float z2 = this.v[l].z;
						this.v[l].x = m00 * x2 + m01 * y2 + m02 * z2 + m03;
						this.v[l].y = m10 * x2 + m11 * y2 + m12 * z2 + m13 + y;
						this.v[l].z = m20 * x2 + m21 * y2 + m22 * z2 + m23 + z;
					}
				}
			}
			else if (y == 0f)
			{
				if (z == 0f)
				{
					for (int n = 0; n < this.vSize; n++)
					{
						float x2 = this.v[n].x;
						float y2 = this.v[n].y;
						float z2 = this.v[n].z;
						this.v[n].x = m00 * x2 + m01 * y2 + m02 * z2 + m03 + x;
						this.v[n].y = m10 * x2 + m11 * y2 + m12 * z2 + m13;
						this.v[n].z = m20 * x2 + m21 * y2 + m22 * z2 + m23;
					}
				}
				else
				{
					for (int num = 0; num < this.vSize; num++)
					{
						float x2 = this.v[num].x;
						float y2 = this.v[num].y;
						float z2 = this.v[num].z;
						this.v[num].x = m00 * x2 + m01 * y2 + m02 * z2 + m03 + x;
						this.v[num].y = m10 * x2 + m11 * y2 + m12 * z2 + m13;
						this.v[num].z = m20 * x2 + m21 * y2 + m22 * z2 + m23 + z;
					}
				}
			}
			else if (z == 0f)
			{
				for (int num2 = 0; num2 < this.vSize; num2++)
				{
					float x2 = this.v[num2].x;
					float y2 = this.v[num2].y;
					float z2 = this.v[num2].z;
					this.v[num2].x = m00 * x2 + m01 * y2 + m02 * z2 + m03 + x;
					this.v[num2].y = m10 * x2 + m11 * y2 + m12 * z2 + m13 + y;
					this.v[num2].z = m20 * x2 + m21 * y2 + m22 * z2 + m23;
				}
			}
			else
			{
				for (int num3 = 0; num3 < this.vSize; num3++)
				{
					float x2 = this.v[num3].x;
					float y2 = this.v[num3].y;
					float z2 = this.v[num3].z;
					this.v[num3].x = m00 * x2 + m01 * y2 + m02 * z2 + m03 + x;
					this.v[num3].y = m10 * x2 + m11 * y2 + m12 * z2 + m13 + y;
					this.v[num3].z = m20 * x2 + m21 * y2 + m22 * z2 + m23 + z;
				}
			}
		}

		// Token: 0x06004B46 RID: 19270 RVA: 0x001259E0 File Offset: 0x00123BE0
		public void Clear()
		{
			this.vSize = 0;
			this.iCount = 0;
			this.primSize = 0;
			this.lastPrimitiveKind = PrimitiveKind.Invalid;
		}

		// Token: 0x06004B47 RID: 19271 RVA: 0x00125A10 File Offset: 0x00123C10
		private static bool ZeroedXYScale(Transform transform)
		{
			if (!transform)
			{
				return false;
			}
			Vector3 localScale = transform.localScale;
			return localScale.x == 0f || localScale.y == 0f;
		}

		// Token: 0x06004B48 RID: 19272 RVA: 0x00125A54 File Offset: 0x00123C54
		private bool SeekPrimitiveIndex(int start, out int i)
		{
			for (i = this.primSize - 1; i >= 0; i--)
			{
				if ((int)this.primitives[i].start <= start)
				{
					return true;
				}
			}
			i = -1;
			return false;
		}

		// Token: 0x06004B49 RID: 19273 RVA: 0x00125A94 File Offset: 0x00123C94
		private void ApplyShadow(int start, int end, int primitiveIndex, float pixel, float r, float g, float b, float a)
		{
			while (start < end)
			{
				if (primitiveIndex != this.primSize - 1 && (int)this.primitives[primitiveIndex + 1].start <= start)
				{
					primitiveIndex++;
				}
				int num;
				int i = this.Alloc(this.primitives[primitiveIndex].kind, out num);
				if (i == num)
				{
					throw new InvalidOperationException();
				}
				while (i < num)
				{
					this.v[i++] = this.v[start];
					this.v[start].r = r;
					this.v[start].g = g;
					this.v[start].b = b;
					Vertex[] array = this.v;
					int num2 = start;
					array[num2].a = array[num2].a * a;
					Vertex[] array2 = this.v;
					int num3 = start;
					array2[num3].x = array2[num3].x + pixel;
					Vertex[] array3 = this.v;
					int num4 = start;
					array3[num4].y = array3[num4].y - pixel;
					start++;
				}
			}
		}

		// Token: 0x06004B4A RID: 19274 RVA: 0x00125BC0 File Offset: 0x00123DC0
		private void ApplyOutline(int start, int end, int primitiveIndex, float pixel, float r, float g, float b, float a)
		{
			while (start < end)
			{
				if (primitiveIndex != this.primSize - 1 && (int)this.primitives[primitiveIndex + 1].start <= start)
				{
					primitiveIndex++;
				}
				int num2;
				int num = this.Alloc(this.primitives[primitiveIndex].kind, out num2);
				int num4;
				int num3 = this.Alloc(this.primitives[primitiveIndex].kind, out num4);
				int num6;
				int num5 = this.Alloc(this.primitives[primitiveIndex].kind, out num6);
				int num7;
				int i = this.Alloc(this.primitives[primitiveIndex].kind, out num7);
				if (i == num7)
				{
					throw new InvalidOperationException();
				}
				while (i < num7)
				{
					this.v[i] = this.v[start];
					this.v[start].r = r;
					this.v[start].g = g;
					this.v[start].b = b;
					Vertex[] array = this.v;
					int num8 = start;
					array[num8].a = array[num8].a * a;
					this.v[num] = (this.v[num3] = (this.v[num5] = this.v[start]));
					Vertex[] array2 = this.v;
					int num9 = start;
					array2[num9].x = array2[num9].x + pixel;
					Vertex[] array3 = this.v;
					int num10 = start;
					array3[num10].y = array3[num10].y - pixel;
					Vertex[] array4 = this.v;
					int num11 = num;
					array4[num11].x = array4[num11].x - pixel;
					Vertex[] array5 = this.v;
					int num12 = num;
					array5[num12].y = array5[num12].y + pixel;
					Vertex[] array6 = this.v;
					int num13 = num3;
					array6[num13].x = array6[num13].x + pixel;
					Vertex[] array7 = this.v;
					int num14 = num3;
					array7[num14].y = array7[num14].y + pixel;
					Vertex[] array8 = this.v;
					int num15 = num5;
					array8[num15].x = array8[num15].x - pixel;
					Vertex[] array9 = this.v;
					int num16 = num5;
					array9[num16].y = array9[num16].y - pixel;
					num++;
					num3++;
					num5++;
					i++;
					start++;
				}
			}
		}

		// Token: 0x06004B4B RID: 19275 RVA: 0x00125E48 File Offset: 0x00124048
		public void ApplyEffect(Transform transform, int vertexStart, global::UILabel.Effect effect, Color effectColor, float size)
		{
			this.ApplyEffect(transform, vertexStart, this.vSize, effect, effectColor, size);
		}

		// Token: 0x06004B4C RID: 19276 RVA: 0x00125E60 File Offset: 0x00124060
		public void ApplyEffect(Transform transform, int vertexStart, int vertexEnd, global::UILabel.Effect effect, Color effectColor, float size)
		{
			int primitiveIndex;
			if (effect != global::UILabel.Effect.None && vertexStart != vertexEnd && !global::NGUITools.ZeroAlpha(effectColor.a) && size != 0f && !MeshBuffer.ZeroedXYScale(transform) && this.SeekPrimitiveIndex(vertexStart, out primitiveIndex))
			{
				float pixel = 1f / size;
				if (effect != global::UILabel.Effect.Shadow)
				{
					if (effect == global::UILabel.Effect.Outline)
					{
						this.ApplyOutline(vertexStart, vertexEnd, primitiveIndex, pixel, effectColor.r, effectColor.g, effectColor.b, effectColor.a);
					}
				}
				else
				{
					this.ApplyShadow(vertexStart, vertexEnd, primitiveIndex, pixel, effectColor.r, effectColor.g, effectColor.b, effectColor.a);
				}
			}
		}

		// Token: 0x06004B4D RID: 19277 RVA: 0x00125F28 File Offset: 0x00124128
		public void WriteBuffers(Vector3[] transformedVertexes, MeshBuffer target)
		{
			if (transformedVertexes == null)
			{
				this.WriteBuffers(target);
			}
			else if (this.primSize > 0)
			{
				int num = 0;
				int i;
				for (i = 0; i < this.primSize - 1; i++)
				{
					this.primitives[i].Copy(ref num, this.v, transformedVertexes, (int)this.primitives[i + 1].start, target);
				}
				this.primitives[i].Copy(ref num, this.v, transformedVertexes, this.vSize, target);
			}
		}

		// Token: 0x06004B4E RID: 19278 RVA: 0x00125FC0 File Offset: 0x001241C0
		public void WriteBuffers(MeshBuffer target)
		{
			if (this.primSize > 0)
			{
				int num = 0;
				int i;
				for (i = 0; i < this.primSize - 1; i++)
				{
					this.primitives[i].Copy(ref num, this.v, (int)this.primitives[i + 1].start, target);
				}
				this.primitives[i].Copy(ref num, this.v, this.vSize, target);
			}
		}

		// Token: 0x0400290A RID: 10506
		public Vertex[] v;

		// Token: 0x0400290B RID: 10507
		public int vSize;

		// Token: 0x0400290C RID: 10508
		public int iCount;

		// Token: 0x0400290D RID: 10509
		private Primitive[] primitives;

		// Token: 0x0400290E RID: 10510
		private int vertCapacity;

		// Token: 0x0400290F RID: 10511
		private int primSize;

		// Token: 0x04002910 RID: 10512
		private int primCapacity;

		// Token: 0x04002911 RID: 10513
		private PrimitiveKind lastPrimitiveKind = PrimitiveKind.Invalid;

		// Token: 0x0200088F RID: 2191
		private struct FillBuffer<T>
		{
			// Token: 0x04002912 RID: 10514
			public T[] buf;

			// Token: 0x04002913 RID: 10515
			public int offset;
		}
	}
}
