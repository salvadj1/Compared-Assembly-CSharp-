using System;
using UnityEngine;

// Token: 0x02000830 RID: 2096
[Serializable]
public class TOD_CloudParameters
{
	// Token: 0x06004A71 RID: 19057 RVA: 0x0014308C File Offset: 0x0014128C
	public void CheckRange()
	{
		this.Scale1 = new Vector2(Mathf.Max(1f, this.Scale1.x), Mathf.Max(1f, this.Scale1.y));
		this.Scale2 = new Vector2(Mathf.Max(1f, this.Scale2.x), Mathf.Max(1f, this.Scale2.y));
		this.Density = Mathf.Max(0f, this.Density);
		this.Sharpness = Mathf.Max(0f, this.Sharpness);
		this.Brightness = Mathf.Max(0f, this.Brightness);
		this.ShadowStrength = Mathf.Clamp01(this.ShadowStrength);
	}

	// Token: 0x04002B47 RID: 11079
	public float Density = 3f;

	// Token: 0x04002B48 RID: 11080
	public float Sharpness = 3f;

	// Token: 0x04002B49 RID: 11081
	public float Brightness = 1f;

	// Token: 0x04002B4A RID: 11082
	public float ShadowStrength;

	// Token: 0x04002B4B RID: 11083
	public Vector2 Scale1 = new Vector2(3f, 3f);

	// Token: 0x04002B4C RID: 11084
	public Vector2 Scale2 = new Vector2(7f, 7f);
}
