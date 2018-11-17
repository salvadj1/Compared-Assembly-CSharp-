using System;
using UnityEngine;

// Token: 0x02000845 RID: 2117
[RequireComponent(typeof(ParticleSystem))]
public class ParticleAtWeather : MonoBehaviour
{
	// Token: 0x06004ACD RID: 19149 RVA: 0x00146A8C File Offset: 0x00144C8C
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

	// Token: 0x06004ACE RID: 19150 RVA: 0x00146AD8 File Offset: 0x00144CD8
	protected void Update()
	{
		int num = (this.sky.Components.Weather.Weather != this.type) ? -1 : 1;
		this.lerpTime = Mathf.Clamp01(this.lerpTime + (float)num * Time.deltaTime / this.fadeTime);
		this.particleComponent.emissionRate = Mathf.Lerp(0f, this.particleEmission, this.lerpTime);
	}

	// Token: 0x04002BE5 RID: 11237
	public TOD_Sky sky;

	// Token: 0x04002BE6 RID: 11238
	public TOD_Weather.WeatherType type;

	// Token: 0x04002BE7 RID: 11239
	public float fadeTime = 1f;

	// Token: 0x04002BE8 RID: 11240
	private float lerpTime;

	// Token: 0x04002BE9 RID: 11241
	private ParticleSystem particleComponent;

	// Token: 0x04002BEA RID: 11242
	private float particleEmission;
}
