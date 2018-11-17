using System;
using UnityEngine;

// Token: 0x0200093A RID: 2362
[RequireComponent(typeof(ParticleSystem))]
public class ParticleAtWeather : MonoBehaviour
{
	// Token: 0x06004F88 RID: 20360 RVA: 0x001509F0 File Offset: 0x0014EBF0
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

	// Token: 0x06004F89 RID: 20361 RVA: 0x00150A3C File Offset: 0x0014EC3C
	protected void Update()
	{
		int num = (this.sky.Components.Weather.Weather != this.type) ? -1 : 1;
		this.lerpTime = Mathf.Clamp01(this.lerpTime + (float)num * Time.deltaTime / this.fadeTime);
		this.particleComponent.emissionRate = Mathf.Lerp(0f, this.particleEmission, this.lerpTime);
	}

	// Token: 0x04002E33 RID: 11827
	public global::TOD_Sky sky;

	// Token: 0x04002E34 RID: 11828
	public global::TOD_Weather.WeatherType type;

	// Token: 0x04002E35 RID: 11829
	public float fadeTime = 1f;

	// Token: 0x04002E36 RID: 11830
	private float lerpTime;

	// Token: 0x04002E37 RID: 11831
	private ParticleSystem particleComponent;

	// Token: 0x04002E38 RID: 11832
	private float particleEmission;
}
