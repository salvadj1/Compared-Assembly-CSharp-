using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace NGUI.Meshing
{
	// Token: 0x020007A0 RID: 1952
	[StructLayout(LayoutKind.Explicit, Size = 36)]
	public struct Vertex
	{
		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x06004695 RID: 18069 RVA: 0x001190B4 File Offset: 0x001172B4
		// (set) Token: 0x06004696 RID: 18070 RVA: 0x001190EC File Offset: 0x001172EC
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

		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x06004697 RID: 18071 RVA: 0x00119124 File Offset: 0x00117324
		// (set) Token: 0x06004698 RID: 18072 RVA: 0x00119150 File Offset: 0x00117350
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

		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x06004699 RID: 18073 RVA: 0x00119170 File Offset: 0x00117370
		// (set) Token: 0x0600469A RID: 18074 RVA: 0x001191B8 File Offset: 0x001173B8
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

		// Token: 0x040026BA RID: 9914
		[FieldOffset(0)]
		public float x;

		// Token: 0x040026BB RID: 9915
		[FieldOffset(4)]
		public float y;

		// Token: 0x040026BC RID: 9916
		[FieldOffset(8)]
		public float z;

		// Token: 0x040026BD RID: 9917
		[FieldOffset(12)]
		public float u;

		// Token: 0x040026BE RID: 9918
		[FieldOffset(16)]
		public float v;

		// Token: 0x040026BF RID: 9919
		[FieldOffset(20)]
		public float r;

		// Token: 0x040026C0 RID: 9920
		[FieldOffset(24)]
		public float g;

		// Token: 0x040026C1 RID: 9921
		[FieldOffset(28)]
		public float b;

		// Token: 0x040026C2 RID: 9922
		[FieldOffset(32)]
		public float a;
	}
}
