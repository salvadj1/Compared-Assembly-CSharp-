using System;
using UnityEngine;

// Token: 0x02000920 RID: 2336
[Serializable]
public class TOD_AtmosphereParameters
{
	// Token: 0x06004F22 RID: 20258 RVA: 0x0014CB58 File Offset: 0x0014AD58
	public void CheckRange()
	{
		this.MieMultiplier = Mathf.Max(0f, this.MieMultiplier);
		this.RayleighMultiplier = Mathf.Max(0f, this.RayleighMultiplier);
		this.Brightness = Mathf.Max(0f, this.Brightness);
		this.Contrast = Mathf.Max(0f, this.Contrast);
		this.Directionality = Mathf.Clamp01(this.Directionality);
		this.Haziness = Mathf.Clamp01(this.Haziness);
		this.Fogginess = Mathf.Clamp01(this.Fogginess);
	}

	// Token: 0x04002D71 RID: 11633
	public Color ScatteringColor = Color.white;

	// Token: 0x04002D72 RID: 11634
	public float RayleighMultiplier = 1f;

	// Token: 0x04002D73 RID: 11635
	public float MieMultiplier = 1f;

	// Token: 0x04002D74 RID: 11636
	public float Brightness = 1f;

	// Token: 0x04002D75 RID: 11637
	public float Contrast = 1f;

	// Token: 0x04002D76 RID: 11638
	public float Directionality = 0.5f;

	// Token: 0x04002D77 RID: 11639
	public float Haziness = 0.5f;

	// Token: 0x04002D78 RID: 11640
	public float Fogginess;
}
