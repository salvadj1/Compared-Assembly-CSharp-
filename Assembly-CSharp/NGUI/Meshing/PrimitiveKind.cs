using System;

namespace NGUI.Meshing
{
	// Token: 0x0200088C RID: 2188
	public enum PrimitiveKind : byte
	{
		// Token: 0x040028FB RID: 10491
		Triangle,
		// Token: 0x040028FC RID: 10492
		Quad,
		// Token: 0x040028FD RID: 10493
		Grid1x1 = 1,
		// Token: 0x040028FE RID: 10494
		Grid2x1,
		// Token: 0x040028FF RID: 10495
		Grid1x2,
		// Token: 0x04002900 RID: 10496
		Grid2x2,
		// Token: 0x04002901 RID: 10497
		Grid1x3,
		// Token: 0x04002902 RID: 10498
		Grid3x1,
		// Token: 0x04002903 RID: 10499
		Grid3x2,
		// Token: 0x04002904 RID: 10500
		Grid2x3,
		// Token: 0x04002905 RID: 10501
		Grid3x3,
		// Token: 0x04002906 RID: 10502
		Hole3x3,
		// Token: 0x04002907 RID: 10503
		Invalid = 255
	}
}
