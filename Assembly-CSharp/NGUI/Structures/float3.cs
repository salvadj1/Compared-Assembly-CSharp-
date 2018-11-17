using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace NGUI.Structures
{
	// Token: 0x020008F1 RID: 2289
	[StructLayout(LayoutKind.Explicit)]
	public struct float3
	{
		// Token: 0x04002BD3 RID: 11219
		[FieldOffset(0)]
		public Vector2 xy;

		// Token: 0x04002BD4 RID: 11220
		[FieldOffset(4)]
		public Vector2 yz;

		// Token: 0x04002BD5 RID: 11221
		[FieldOffset(0)]
		public Vector3 xyz;

		// Token: 0x04002BD6 RID: 11222
		[FieldOffset(0)]
		public float x;

		// Token: 0x04002BD7 RID: 11223
		[FieldOffset(4)]
		public float y;

		// Token: 0x04002BD8 RID: 11224
		[FieldOffset(8)]
		public float z;
	}
}
