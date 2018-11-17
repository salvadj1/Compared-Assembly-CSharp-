using System;
using UnityEngine;

// Token: 0x02000923 RID: 2339
[Serializable]
public class TOD_NightParameters
{
	// Token: 0x06004F28 RID: 20264 RVA: 0x0014CE50 File Offset: 0x0014B050
	public void CheckRange()
	{
		this.MoonLightIntensity = Mathf.Max(0f, this.MoonLightIntensity);
		this.MoonMeshSize = Mathf.Max(0f, this.MoonMeshSize);
		this.AmbientIntensity = Mathf.Clamp01(this.AmbientIntensity);
		this.ShadowStrength = Mathf.Clamp01(this.ShadowStrength);
		this.SkyMultiplier = Mathf.Clamp01(this.SkyMultiplier);
		this.CloudMultiplier = Mathf.Clamp01(this.CloudMultiplier);
	}

	// Token: 0x04002D85 RID: 11653
	public Color AdditiveColor = Color.black;

	// Token: 0x04002D86 RID: 11654
	public Color MoonMeshColor = new Color32(byte.MaxValue, 233, 200, byte.MaxValue);

	// Token: 0x04002D87 RID: 11655
	public Color MoonLightColor = new Color32(181, 204, byte.MaxValue, byte.MaxValue);

	// Token: 0x04002D88 RID: 11656
	public Color MoonHaloColor = new Color32(81, 104, 155, byte.MaxValue);

	// Token: 0x04002D89 RID: 11657
	public float MoonMeshSize = 1f;

	// Token: 0x04002D8A RID: 11658
	public float MoonLightIntensity = 0.1f;

	// Token: 0x04002D8B RID: 11659
	public float AmbientIntensity = 0.2f;

	// Token: 0x04002D8C RID: 11660
	public float ShadowStrength = 1f;

	// Token: 0x04002D8D RID: 11661
	public float SkyMultiplier = 0.1f;

	// Token: 0x04002D8E RID: 11662
	public float CloudMultiplier = 0.2f;
}
