using System;

namespace NGUI.Meshing
{
	// Token: 0x020007A1 RID: 1953
	public enum PrimitiveKind : byte
	{
		// Token: 0x040026C4 RID: 9924
		Triangle,
		// Token: 0x040026C5 RID: 9925
		Quad,
		// Token: 0x040026C6 RID: 9926
		Grid1x1 = 1,
		// Token: 0x040026C7 RID: 9927
		Grid2x1,
		// Token: 0x040026C8 RID: 9928
		Grid1x2,
		// Token: 0x040026C9 RID: 9929
		Grid2x2,
		// Token: 0x040026CA RID: 9930
		Grid1x3,
		// Token: 0x040026CB RID: 9931
		Grid3x1,
		// Token: 0x040026CC RID: 9932
		Grid3x2,
		// Token: 0x040026CD RID: 9933
		Grid2x3,
		// Token: 0x040026CE RID: 9934
		Grid3x3,
		// Token: 0x040026CF RID: 9935
		Hole3x3,
		// Token: 0x040026D0 RID: 9936
		Invalid = 255
	}
}
