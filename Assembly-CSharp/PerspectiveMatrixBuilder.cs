using System;
using Facepunch.Precision;

// Token: 0x02000501 RID: 1281
public struct PerspectiveMatrixBuilder
{
	// Token: 0x06002BD9 RID: 11225 RVA: 0x000A3EC4 File Offset: 0x000A20C4
	public void ToProjectionMatrix(out Matrix4x4G proj)
	{
		Matrix4x4G.Perspective(ref this.fieldOfView, ref this.aspectRatio, ref this.nearPlane, ref this.farPlane, ref proj);
	}

	// Token: 0x0400159F RID: 5535
	public double fieldOfView;

	// Token: 0x040015A0 RID: 5536
	public double aspectRatio;

	// Token: 0x040015A1 RID: 5537
	public double nearPlane;

	// Token: 0x040015A2 RID: 5538
	public double farPlane;
}
