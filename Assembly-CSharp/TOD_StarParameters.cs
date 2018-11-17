using System;
using UnityEngine;

// Token: 0x0200082C RID: 2092
[Serializable]
public class TOD_StarParameters
{
	// Token: 0x06004A69 RID: 19049 RVA: 0x00142CAC File Offset: 0x00140EAC
	public void CheckRange()
	{
		this.Tiling = Mathf.Max(0f, this.Tiling);
		this.Density = Mathf.Clamp01(this.Density);
	}

	// Token: 0x04002B2B RID: 11051
	public float Tiling = 2f;

	// Token: 0x04002B2C RID: 11052
	public float Density = 0.5f;
}
