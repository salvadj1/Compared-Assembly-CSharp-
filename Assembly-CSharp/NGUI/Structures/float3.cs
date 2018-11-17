using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace NGUI.Structures
{
	// Token: 0x020007FF RID: 2047
	[StructLayout(LayoutKind.Explicit)]
	public struct float3
	{
		// Token: 0x04002985 RID: 10629
		[FieldOffset(0)]
		public Vector2 xy;

		// Token: 0x04002986 RID: 10630
		[FieldOffset(4)]
		public Vector2 yz;

		// Token: 0x04002987 RID: 10631
		[FieldOffset(0)]
		public Vector3 xyz;

		// Token: 0x04002988 RID: 10632
		[FieldOffset(0)]
		public float x;

		// Token: 0x04002989 RID: 10633
		[FieldOffset(4)]
		public float y;

		// Token: 0x0400298A RID: 10634
		[FieldOffset(8)]
		public float z;
	}
}
