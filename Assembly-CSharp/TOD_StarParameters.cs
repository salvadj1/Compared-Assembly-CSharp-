using System;
using UnityEngine;

// Token: 0x02000921 RID: 2337
[Serializable]
public class TOD_StarParameters
{
	// Token: 0x06004F24 RID: 20260 RVA: 0x0014CC10 File Offset: 0x0014AE10
	public void CheckRange()
	{
		this.Tiling = Mathf.Max(0f, this.Tiling);
		this.Density = Mathf.Clamp01(this.Density);
	}

	// Token: 0x04002D79 RID: 11641
	public float Tiling = 2f;

	// Token: 0x04002D7A RID: 11642
	public float Density = 0.5f;
}
