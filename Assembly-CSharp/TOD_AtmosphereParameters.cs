using System;
using UnityEngine;

// Token: 0x0200082B RID: 2091
[Serializable]
public class TOD_AtmosphereParameters
{
	// Token: 0x06004A67 RID: 19047 RVA: 0x00142BF4 File Offset: 0x00140DF4
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

	// Token: 0x04002B23 RID: 11043
	public Color ScatteringColor = Color.white;

	// Token: 0x04002B24 RID: 11044
	public float RayleighMultiplier = 1f;

	// Token: 0x04002B25 RID: 11045
	public float MieMultiplier = 1f;

	// Token: 0x04002B26 RID: 11046
	public float Brightness = 1f;

	// Token: 0x04002B27 RID: 11047
	public float Contrast = 1f;

	// Token: 0x04002B28 RID: 11048
	public float Directionality = 0.5f;

	// Token: 0x04002B29 RID: 11049
	public float Haziness = 0.5f;

	// Token: 0x04002B2A RID: 11050
	public float Fogginess;
}
