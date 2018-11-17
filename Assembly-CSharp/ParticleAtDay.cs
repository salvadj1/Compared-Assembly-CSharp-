using System;
using UnityEngine;

// Token: 0x02000843 RID: 2115
[RequireComponent(typeof(ParticleSystem))]
public class ParticleAtDay : MonoBehaviour
{
	// Token: 0x06004AC7 RID: 19143 RVA: 0x001468FC File Offset: 0x00144AFC
	protected void OnEnable()
	{
		if (!this.sky)
		{
			Debug.LogError("Sky instance reference not set. Disabling script.");
			base.enabled = false;
		}
		this.particleComponent = base.particleSystem;
		this.particleEmission = this.particleComponent.emissionRate;
	}

	// Token: 0x06004AC8 RID: 19144 RVA: 0x00146948 File Offset: 0x00144B48
	protected void Update()
	{
		int num = (!this.sky.IsDay) ? -1 : 1;
		this.lerpTime = Mathf.Clamp01(this.lerpTime + (float)num * Time.deltaTime / this.fadeTime);
		this.particleComponent.emissionRate = Mathf.Lerp(0f, this.particleEmission, this.lerpTime);
	}

	// Token: 0x04002BDB RID: 11227
	public TOD_Sky sky;

	// Token: 0x04002BDC RID: 11228
	public float fadeTime = 1f;

	// Token: 0x04002BDD RID: 11229
	private float lerpTime;

	// Token: 0x04002BDE RID: 11230
	private ParticleSystem particleComponent;

	// Token: 0x04002BDF RID: 11231
	private float particleEmission;
}
