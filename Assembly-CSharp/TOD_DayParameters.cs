using System;
using UnityEngine;

// Token: 0x02000922 RID: 2338
[Serializable]
public class TOD_DayParameters
{
	// Token: 0x06004F26 RID: 20262 RVA: 0x0014CD08 File Offset: 0x0014AF08
	public void CheckRange()
	{
		this.SunLightIntensity = Mathf.Max(0f, this.SunLightIntensity);
		this.SunMeshSize = Mathf.Max(0f, this.SunMeshSize);
		this.AmbientIntensity = Mathf.Clamp01(this.AmbientIntensity);
		this.ShadowStrength = Mathf.Clamp01(this.ShadowStrength);
		this.SkyMultiplier = Mathf.Clamp01(this.SkyMultiplier);
		this.CloudMultiplier = Mathf.Clamp01(this.CloudMultiplier);
	}

	// Token: 0x04002D7B RID: 11643
	public Color AdditiveColor = Color.black;

	// Token: 0x04002D7C RID: 11644
	public Color SunMeshColor = new Color32(byte.MaxValue, 233, 180, byte.MaxValue);

	// Token: 0x04002D7D RID: 11645
	public Color SunLightColor = new Color32(byte.MaxValue, 243, 234, byte.MaxValue);

	// Token: 0x04002D7E RID: 11646
	public Color SunShaftColor = new Color32(byte.MaxValue, 243, 234, byte.MaxValue);

	// Token: 0x04002D7F RID: 11647
	public float SunMeshSize = 1f;

	// Token: 0x04002D80 RID: 11648
	public float SunLightIntensity = 0.75f;

	// Token: 0x04002D81 RID: 11649
	public float AmbientIntensity = 0.75f;

	// Token: 0x04002D82 RID: 11650
	public float ShadowStrength = 1f;

	// Token: 0x04002D83 RID: 11651
	public float SkyMultiplier = 1f;

	// Token: 0x04002D84 RID: 11652
	public float CloudMultiplier = 1f;
}
