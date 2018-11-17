using System;
using UnityEngine;

// Token: 0x02000925 RID: 2341
[Serializable]
public class TOD_CloudParameters
{
	// Token: 0x06004F2C RID: 20268 RVA: 0x0014CFF0 File Offset: 0x0014B1F0
	public void CheckRange()
	{
		this.Scale1 = new Vector2(Mathf.Max(1f, this.Scale1.x), Mathf.Max(1f, this.Scale1.y));
		this.Scale2 = new Vector2(Mathf.Max(1f, this.Scale2.x), Mathf.Max(1f, this.Scale2.y));
		this.Density = Mathf.Max(0f, this.Density);
		this.Sharpness = Mathf.Max(0f, this.Sharpness);
		this.Brightness = Mathf.Max(0f, this.Brightness);
		this.ShadowStrength = Mathf.Clamp01(this.ShadowStrength);
	}

	// Token: 0x04002D95 RID: 11669
	public float Density = 3f;

	// Token: 0x04002D96 RID: 11670
	public float Sharpness = 3f;

	// Token: 0x04002D97 RID: 11671
	public float Brightness = 1f;

	// Token: 0x04002D98 RID: 11672
	public float ShadowStrength;

	// Token: 0x04002D99 RID: 11673
	public Vector2 Scale1 = new Vector2(3f, 3f);

	// Token: 0x04002D9A RID: 11674
	public Vector2 Scale2 = new Vector2(7f, 7f);
}
