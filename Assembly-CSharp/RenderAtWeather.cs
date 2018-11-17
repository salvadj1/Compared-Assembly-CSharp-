using System;
using UnityEngine;

// Token: 0x0200093D RID: 2365
[RequireComponent(typeof(Renderer))]
public class RenderAtWeather : MonoBehaviour
{
	// Token: 0x06004F91 RID: 20369 RVA: 0x00150B5C File Offset: 0x0014ED5C
	protected void OnEnable()
	{
		if (!this.sky)
		{
			Debug.LogError("Sky instance reference not set. Disabling script.");
			base.enabled = false;
		}
		this.rendererComponent = base.renderer;
	}

	// Token: 0x06004F92 RID: 20370 RVA: 0x00150B8C File Offset: 0x0014ED8C
	protected void Update()
	{
		this.rendererComponent.enabled = (this.sky.Components.Weather.Weather == this.type);
	}

	// Token: 0x04002E3D RID: 11837
	public global::TOD_Sky sky;

	// Token: 0x04002E3E RID: 11838
	public global::TOD_Weather.WeatherType type;

	// Token: 0x04002E3F RID: 11839
	private Renderer rendererComponent;
}
