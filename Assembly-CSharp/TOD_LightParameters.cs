using System;
using UnityEngine;

// Token: 0x0200082F RID: 2095
[Serializable]
public class TOD_LightParameters
{
	// Token: 0x06004A6F RID: 19055 RVA: 0x00142FAC File Offset: 0x001411AC
	public void CheckRange()
	{
		this.MinimumHeight = Mathf.Clamp(this.MinimumHeight, -1f, 1f);
		this.Falloff = Mathf.Clamp01(this.Falloff);
		this.Coloring = Mathf.Clamp01(this.Coloring);
		this.SkyColoring = Mathf.Clamp01(this.SkyColoring);
		this.CloudColoring = Mathf.Clamp01(this.CloudColoring);
		this.ShaftColoring = Mathf.Clamp01(this.ShaftColoring);
	}

	// Token: 0x04002B41 RID: 11073
	public float MinimumHeight;

	// Token: 0x04002B42 RID: 11074
	public float Falloff = 0.7f;

	// Token: 0x04002B43 RID: 11075
	public float Coloring = 0.7f;

	// Token: 0x04002B44 RID: 11076
	public float SkyColoring = 0.5f;

	// Token: 0x04002B45 RID: 11077
	public float CloudColoring = 0.9f;

	// Token: 0x04002B46 RID: 11078
	public float ShaftColoring = 0.9f;
}
