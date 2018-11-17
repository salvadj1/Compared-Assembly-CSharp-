using System;
using UnityEngine;

// Token: 0x0200082D RID: 2093
[Serializable]
public class TOD_DayParameters
{
	// Token: 0x06004A6B RID: 19051 RVA: 0x00142DA4 File Offset: 0x00140FA4
	public void CheckRange()
	{
		this.SunLightIntensity = Mathf.Max(0f, this.SunLightIntensity);
		this.SunMeshSize = Mathf.Max(0f, this.SunMeshSize);
		this.AmbientIntensity = Mathf.Clamp01(this.AmbientIntensity);
		this.ShadowStrength = Mathf.Clamp01(this.ShadowStrength);
		this.SkyMultiplier = Mathf.Clamp01(this.SkyMultiplier);
		this.CloudMultiplier = Mathf.Clamp01(this.CloudMultiplier);
	}

	// Token: 0x04002B2D RID: 11053
	public Color AdditiveColor = Color.black;

	// Token: 0x04002B2E RID: 11054
	public Color SunMeshColor = new Color32(byte.MaxValue, 233, 180, byte.MaxValue);

	// Token: 0x04002B2F RID: 11055
	public Color SunLightColor = new Color32(byte.MaxValue, 243, 234, byte.MaxValue);

	// Token: 0x04002B30 RID: 11056
	public Color SunShaftColor = new Color32(byte.MaxValue, 243, 234, byte.MaxValue);

	// Token: 0x04002B31 RID: 11057
	public float SunMeshSize = 1f;

	// Token: 0x04002B32 RID: 11058
	public float SunLightIntensity = 0.75f;

	// Token: 0x04002B33 RID: 11059
	public float AmbientIntensity = 0.75f;

	// Token: 0x04002B34 RID: 11060
	public float ShadowStrength = 1f;

	// Token: 0x04002B35 RID: 11061
	public float SkyMultiplier = 1f;

	// Token: 0x04002B36 RID: 11062
	public float CloudMultiplier = 1f;
}
