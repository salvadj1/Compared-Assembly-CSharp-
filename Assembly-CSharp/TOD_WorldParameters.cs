using System;
using UnityEngine;

// Token: 0x02000831 RID: 2097
[Serializable]
public class TOD_WorldParameters
{
	// Token: 0x06004A73 RID: 19059 RVA: 0x00143160 File Offset: 0x00141360
	public void CheckRange()
	{
		this.FogColorBias = Mathf.Clamp01(this.FogColorBias);
		this.ViewerHeight = Mathf.Clamp01(this.ViewerHeight);
		this.HorizonOffset = Mathf.Clamp01(this.HorizonOffset);
	}

	// Token: 0x04002B4D RID: 11085
	public bool SetAmbientLight;

	// Token: 0x04002B4E RID: 11086
	public bool SetFogColor;

	// Token: 0x04002B4F RID: 11087
	public float FogColorBias;

	// Token: 0x04002B50 RID: 11088
	public float ViewerHeight;

	// Token: 0x04002B51 RID: 11089
	public float HorizonOffset;
}
