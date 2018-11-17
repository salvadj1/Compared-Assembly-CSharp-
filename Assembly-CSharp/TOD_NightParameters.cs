using System;
using UnityEngine;

// Token: 0x0200082E RID: 2094
[Serializable]
public class TOD_NightParameters
{
	// Token: 0x06004A6D RID: 19053 RVA: 0x00142EEC File Offset: 0x001410EC
	public void CheckRange()
	{
		this.MoonLightIntensity = Mathf.Max(0f, this.MoonLightIntensity);
		this.MoonMeshSize = Mathf.Max(0f, this.MoonMeshSize);
		this.AmbientIntensity = Mathf.Clamp01(this.AmbientIntensity);
		this.ShadowStrength = Mathf.Clamp01(this.ShadowStrength);
		this.SkyMultiplier = Mathf.Clamp01(this.SkyMultiplier);
		this.CloudMultiplier = Mathf.Clamp01(this.CloudMultiplier);
	}

	// Token: 0x04002B37 RID: 11063
	public Color AdditiveColor = Color.black;

	// Token: 0x04002B38 RID: 11064
	public Color MoonMeshColor = new Color32(byte.MaxValue, 233, 200, byte.MaxValue);

	// Token: 0x04002B39 RID: 11065
	public Color MoonLightColor = new Color32(181, 204, byte.MaxValue, byte.MaxValue);

	// Token: 0x04002B3A RID: 11066
	public Color MoonHaloColor = new Color32(81, 104, 155, byte.MaxValue);

	// Token: 0x04002B3B RID: 11067
	public float MoonMeshSize = 1f;

	// Token: 0x04002B3C RID: 11068
	public float MoonLightIntensity = 0.1f;

	// Token: 0x04002B3D RID: 11069
	public float AmbientIntensity = 0.2f;

	// Token: 0x04002B3E RID: 11070
	public float ShadowStrength = 1f;

	// Token: 0x04002B3F RID: 11071
	public float SkyMultiplier = 0.1f;

	// Token: 0x04002B40 RID: 11072
	public float CloudMultiplier = 0.2f;
}
