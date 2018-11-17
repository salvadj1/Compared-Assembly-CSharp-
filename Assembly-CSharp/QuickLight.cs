using System;
using UnityEngine;

// Token: 0x020005A7 RID: 1447
public class QuickLight : MonoBehaviour
{
	// Token: 0x06002EA4 RID: 11940 RVA: 0x000B399C File Offset: 0x000B1B9C
	public void Update()
	{
		base.light.range -= Time.deltaTime / this.duration;
		if (base.light.range <= 0f)
		{
			base.light.range = 0f;
			base.light.intensity = 0f;
		}
	}

	// Token: 0x0400193C RID: 6460
	public float range = 1f;

	// Token: 0x0400193D RID: 6461
	public float duration = 0.25f;
}
