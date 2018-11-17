using System;
using UnityEngine;

// Token: 0x0200083C RID: 2108
public class TOD_Weather : MonoBehaviour
{
	// Token: 0x06004AB9 RID: 19129 RVA: 0x001462B4 File Offset: 0x001444B4
	protected void Start()
	{
		this.sky = base.GetComponent<TOD_Sky>();
		this.cloudBrightness = (this.cloudBrightnessDefault = this.sky.Clouds.Brightness);
		this.cloudDensity = (this.cloudDensityDefault = this.sky.Clouds.Density);
		this.atmosphereFog = (this.atmosphereFogDefault = this.sky.Atmosphere.Fogginess);
		this.cloudSharpness = this.sky.Clouds.Sharpness;
	}

	// Token: 0x06004ABA RID: 19130 RVA: 0x00146340 File Offset: 0x00144540
	protected void Update()
	{
		if (this.Clouds == TOD_Weather.CloudType.Custom && this.Weather == TOD_Weather.WeatherType.Custom)
		{
			return;
		}
		switch (this.Clouds)
		{
		case TOD_Weather.CloudType.Custom:
			this.cloudDensity = this.sky.Clouds.Density;
			this.cloudSharpness = this.sky.Clouds.Sharpness;
			break;
		case TOD_Weather.CloudType.None:
			this.cloudDensity = 0f;
			this.cloudSharpness = 1f;
			break;
		case TOD_Weather.CloudType.Few:
			this.cloudDensity = this.cloudDensityDefault;
			this.cloudSharpness = 6f;
			break;
		case TOD_Weather.CloudType.Scattered:
			this.cloudDensity = this.cloudDensityDefault;
			this.cloudSharpness = 3f;
			break;
		case TOD_Weather.CloudType.Broken:
			this.cloudDensity = this.cloudDensityDefault;
			this.cloudSharpness = 1f;
			break;
		case TOD_Weather.CloudType.Overcast:
			this.cloudDensity = this.cloudDensityDefault;
			this.cloudSharpness = 0.1f;
			break;
		}
		switch (this.Weather)
		{
		case TOD_Weather.WeatherType.Custom:
			this.cloudBrightness = this.sky.Clouds.Brightness;
			this.atmosphereFog = this.sky.Atmosphere.Fogginess;
			break;
		case TOD_Weather.WeatherType.Clear:
			this.cloudBrightness = this.cloudBrightnessDefault;
			this.atmosphereFog = this.atmosphereFogDefault;
			break;
		case TOD_Weather.WeatherType.Storm:
			this.cloudBrightness = 0.3f;
			this.atmosphereFog = 1f;
			break;
		case TOD_Weather.WeatherType.Dust:
			this.cloudBrightness = this.cloudBrightnessDefault;
			this.atmosphereFog = 0.5f;
			break;
		case TOD_Weather.WeatherType.Fog:
			this.cloudBrightness = this.cloudBrightnessDefault;
			this.atmosphereFog = 1f;
			break;
		}
		float num = Time.deltaTime / this.FadeTime;
		this.sky.Clouds.Brightness = Mathf.Lerp(this.sky.Clouds.Brightness, this.cloudBrightness, num);
		this.sky.Clouds.Density = Mathf.Lerp(this.sky.Clouds.Density, this.cloudDensity, num);
		this.sky.Clouds.Sharpness = Mathf.Lerp(this.sky.Clouds.Sharpness, this.cloudSharpness, num);
		this.sky.Atmosphere.Fogginess = Mathf.Lerp(this.sky.Atmosphere.Fogginess, this.atmosphereFog, num);
	}

	// Token: 0x04002BB2 RID: 11186
	public float FadeTime = 10f;

	// Token: 0x04002BB3 RID: 11187
	public TOD_Weather.CloudType Clouds;

	// Token: 0x04002BB4 RID: 11188
	public TOD_Weather.WeatherType Weather;

	// Token: 0x04002BB5 RID: 11189
	private float cloudBrightnessDefault;

	// Token: 0x04002BB6 RID: 11190
	private float cloudDensityDefault;

	// Token: 0x04002BB7 RID: 11191
	private float atmosphereFogDefault;

	// Token: 0x04002BB8 RID: 11192
	private float cloudBrightness;

	// Token: 0x04002BB9 RID: 11193
	private float cloudDensity;

	// Token: 0x04002BBA RID: 11194
	private float atmosphereFog;

	// Token: 0x04002BBB RID: 11195
	private float cloudSharpness;

	// Token: 0x04002BBC RID: 11196
	private TOD_Sky sky;

	// Token: 0x0200083D RID: 2109
	public enum CloudType
	{
		// Token: 0x04002BBE RID: 11198
		Custom,
		// Token: 0x04002BBF RID: 11199
		None,
		// Token: 0x04002BC0 RID: 11200
		Few,
		// Token: 0x04002BC1 RID: 11201
		Scattered,
		// Token: 0x04002BC2 RID: 11202
		Broken,
		// Token: 0x04002BC3 RID: 11203
		Overcast
	}

	// Token: 0x0200083E RID: 2110
	public enum WeatherType
	{
		// Token: 0x04002BC5 RID: 11205
		Custom,
		// Token: 0x04002BC6 RID: 11206
		Clear,
		// Token: 0x04002BC7 RID: 11207
		Storm,
		// Token: 0x04002BC8 RID: 11208
		Dust,
		// Token: 0x04002BC9 RID: 11209
		Fog
	}
}
