using System;
using UnityEngine;

// Token: 0x020004EC RID: 1260
public class QuickLight : MonoBehaviour
{
	// Token: 0x06002AF2 RID: 10994 RVA: 0x000ABC04 File Offset: 0x000A9E04
	public void Update()
	{
		base.light.range -= Time.deltaTime / this.duration;
		if (base.light.range <= 0f)
		{
			base.light.range = 0f;
			base.light.intensity = 0f;
		}
	}

	// Token: 0x0400177F RID: 6015
	public float range = 1f;

	// Token: 0x04001780 RID: 6016
	public float duration = 0.25f;
}
