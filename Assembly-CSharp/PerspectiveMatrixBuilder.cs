using System;
using Facepunch.Precision;

// Token: 0x0200044B RID: 1099
public struct PerspectiveMatrixBuilder
{
	// Token: 0x06002849 RID: 10313 RVA: 0x0009DF44 File Offset: 0x0009C144
	public void ToProjectionMatrix(out Matrix4x4G proj)
	{
		Matrix4x4G.Perspective(ref this.fieldOfView, ref this.aspectRatio, ref this.nearPlane, ref this.farPlane, ref proj);
	}

	// Token: 0x0400141C RID: 5148
	public double fieldOfView;

	// Token: 0x0400141D RID: 5149
	public double aspectRatio;

	// Token: 0x0400141E RID: 5150
	public double nearPlane;

	// Token: 0x0400141F RID: 5151
	public double farPlane;
}
