using System;
using UnityEngine;

// Token: 0x02000926 RID: 2342
[Serializable]
public class TOD_WorldParameters
{
	// Token: 0x06004F2E RID: 20270 RVA: 0x0014D0C4 File Offset: 0x0014B2C4
	public void CheckRange()
	{
		this.FogColorBias = Mathf.Clamp01(this.FogColorBias);
		this.ViewerHeight = Mathf.Clamp01(this.ViewerHeight);
		this.HorizonOffset = Mathf.Clamp01(this.HorizonOffset);
	}

	// Token: 0x04002D9B RID: 11675
	public bool SetAmbientLight;

	// Token: 0x04002D9C RID: 11676
	public bool SetFogColor;

	// Token: 0x04002D9D RID: 11677
	public float FogColorBias;

	// Token: 0x04002D9E RID: 11678
	public float ViewerHeight;

	// Token: 0x04002D9F RID: 11679
	public float HorizonOffset;
}
