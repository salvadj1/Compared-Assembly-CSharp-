using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace NGUI.Meshing
{
	// Token: 0x0200088B RID: 2187
	[StructLayout(LayoutKind.Explicit, Size = 36)]
	public struct Vertex
	{
		// Token: 0x17000E34 RID: 3636
		// (get) Token: 0x06004B1A RID: 19226 RVA: 0x00122A34 File Offset: 0x00120C34
		// (set) Token: 0x06004B1B RID: 19227 RVA: 0x00122A6C File Offset: 0x00120C6C
		public Vector3 position
		{
			get
			{
				Vector3 result;
				result.x = this.x;
				result.y = this.y;
				result.z = this.z;
				return result;
			}
			set
			{
				this.x = value.x;
				this.y = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x17000E35 RID: 3637
		// (get) Token: 0x06004B1C RID: 19228 RVA: 0x00122AA4 File Offset: 0x00120CA4
		// (set) Token: 0x06004B1D RID: 19229 RVA: 0x00122AD0 File Offset: 0x00120CD0
		public Vector2 texcoord
		{
			get
			{
				Vector2 result;
				result.x = this.u;
				result.y = this.v;
				return result;
			}
			set
			{
				this.u = value.x;
				this.v = value.y;
			}
		}

		// Token: 0x17000E36 RID: 3638
		// (get) Token: 0x06004B1E RID: 19230 RVA: 0x00122AF0 File Offset: 0x00120CF0
		// (set) Token: 0x06004B1F RID: 19231 RVA: 0x00122B38 File Offset: 0x00120D38
		public Color color
		{
			get
			{
				Color result;
				result.r = this.r;
				result.g = this.g;
				result.b = this.b;
				result.a = this.a;
				return result;
			}
			set
			{
				this.r = value.r;
				this.g = value.g;
				this.b = value.b;
				this.a = value.a;
			}
		}

		// Token: 0x040028F1 RID: 10481
		[FieldOffset(0)]
		public float x;

		// Token: 0x040028F2 RID: 10482
		[FieldOffset(4)]
		public float y;

		// Token: 0x040028F3 RID: 10483
		[FieldOffset(8)]
		public float z;

		// Token: 0x040028F4 RID: 10484
		[FieldOffset(12)]
		public float u;

		// Token: 0x040028F5 RID: 10485
		[FieldOffset(16)]
		public float v;

		// Token: 0x040028F6 RID: 10486
		[FieldOffset(20)]
		public float r;

		// Token: 0x040028F7 RID: 10487
		[FieldOffset(24)]
		public float g;

		// Token: 0x040028F8 RID: 10488
		[FieldOffset(28)]
		public float b;

		// Token: 0x040028F9 RID: 10489
		[FieldOffset(32)]
		public float a;
	}
}
