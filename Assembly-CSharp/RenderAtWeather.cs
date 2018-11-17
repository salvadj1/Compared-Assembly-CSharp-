using System;
using UnityEngine;

// Token: 0x02000848 RID: 2120
[RequireComponent(typeof(Renderer))]
public class RenderAtWeather : MonoBehaviour
{
	// Token: 0x06004AD6 RID: 19158 RVA: 0x00146BF8 File Offset: 0x00144DF8
	protected void OnEnable()
	{
		if (!this.sky)
		{
			Debug.LogError("Sky instance reference not set. Disabling script.");
			base.enabled = false;
		}
		this.rendererComponent = base.renderer;
	}

	// Token: 0x06004AD7 RID: 19159 RVA: 0x00146C28 File Offset: 0x00144E28
	protected void Update()
	{
		this.rendererComponent.enabled = (this.sky.Components.Weather.Weather == this.type);
	}

	// Token: 0x04002BEF RID: 11247
	public TOD_Sky sky;

	// Token: 0x04002BF0 RID: 11248
	public TOD_Weather.WeatherType type;

	// Token: 0x04002BF1 RID: 11249
	private Renderer rendererComponent;
}
