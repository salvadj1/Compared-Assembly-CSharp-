using System;
using UnityEngine;

// Token: 0x02000924 RID: 2340
[Serializable]
public class TOD_LightParameters
{
	// Token: 0x06004F2A RID: 20266 RVA: 0x0014CF10 File Offset: 0x0014B110
	public void CheckRange()
	{
		this.MinimumHeight = Mathf.Clamp(this.MinimumHeight, -1f, 1f);
		this.Falloff = Mathf.Clamp01(this.Falloff);
		this.Coloring = Mathf.Clamp01(this.Coloring);
		this.SkyColoring = Mathf.Clamp01(this.SkyColoring);
		this.CloudColoring = Mathf.Clamp01(this.CloudColoring);
		this.ShaftColoring = Mathf.Clamp01(this.ShaftColoring);
	}

	// Token: 0x04002D8F RID: 11663
	public float MinimumHeight;

	// Token: 0x04002D90 RID: 11664
	public float Falloff = 0.7f;

	// Token: 0x04002D91 RID: 11665
	public float Coloring = 0.7f;

	// Token: 0x04002D92 RID: 11666
	public float SkyColoring = 0.5f;

	// Token: 0x04002D93 RID: 11667
	public float CloudColoring = 0.9f;

	// Token: 0x04002D94 RID: 11668
	public float ShaftColoring = 0.9f;
}
