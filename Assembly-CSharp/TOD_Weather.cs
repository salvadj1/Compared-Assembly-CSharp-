using System;
using UnityEngine;

// Token: 0x02000931 RID: 2353
public class TOD_Weather : MonoBehaviour
{
	// Token: 0x06004F74 RID: 20340 RVA: 0x00150218 File Offset: 0x0014E418
	protected void Start()
	{
		this.sky = base.GetComponent<global::TOD_Sky>();
		this.cloudBrightness = (this.cloudBrightnessDefault = this.sky.Clouds.Brightness);
		this.cloudDensity = (this.cloudDensityDefault = this.sky.Clouds.Density);
		this.atmosphereFog = (this.atmosphereFogDefault = this.sky.Atmosphere.Fogginess);
		this.cloudSharpness = this.sky.Clouds.Sharpness;
	}

	// Token: 0x06004F75 RID: 20341 RVA: 0x001502A4 File Offset: 0x0014E4A4
	protected void Update()
	{
		if (this.Clouds == global::TOD_Weather.CloudType.Custom && this.Weather == global::TOD_Weather.WeatherType.Custom)
		{
			return;
		}
		switch (this.Clouds)
		{
		case global::TOD_Weather.CloudType.Custom:
			this.cloudDensity = this.sky.Clouds.Density;
			this.cloudSharpness = this.sky.Clouds.Sharpness;
			break;
		case global::TOD_Weather.CloudType.None:
			this.cloudDensity = 0f;
			this.cloudSharpness = 1f;
			break;
		case global::TOD_Weather.CloudType.Few:
			this.cloudDensity = this.cloudDensityDefault;
			this.cloudSharpness = 6f;
			break;
		case global::TOD_Weather.CloudType.Scattered:
			this.cloudDensity = this.cloudDensityDefault;
			this.cloudSharpness = 3f;
			break;
		case global::TOD_Weather.CloudType.Broken:
			this.cloudDensity = this.cloudDensityDefault;
			this.cloudSharpness = 1f;
			break;
		case global::TOD_Weather.CloudType.Overcast:
			this.cloudDensity = this.cloudDensityDefault;
			this.cloudSharpness = 0.1f;
			break;
		}
		switch (this.Weather)
		{
		case global::TOD_Weather.WeatherType.Custom:
			this.cloudBrightness = this.sky.Clouds.Brightness;
			this.atmosphereFog = this.sky.Atmosphere.Fogginess;
			break;
		case global::TOD_Weather.WeatherType.Clear:
			this.cloudBrightness = this.cloudBrightnessDefault;
			this.atmosphereFog = this.atmosphereFogDefault;
			break;
		case global::TOD_Weather.WeatherType.Storm:
			this.cloudBrightness = 0.3f;
			this.atmosphereFog = 1f;
			break;
		case global::TOD_Weather.WeatherType.Dust:
			this.cloudBrightness = this.cloudBrightnessDefault;
			this.atmosphereFog = 0.5f;
			break;
		case global::TOD_Weather.WeatherType.Fog:
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

	// Token: 0x04002E00 RID: 11776
	public float FadeTime = 10f;

	// Token: 0x04002E01 RID: 11777
	public global::TOD_Weather.CloudType Clouds;

	// Token: 0x04002E02 RID: 11778
	public global::TOD_Weather.WeatherType Weather;

	// Token: 0x04002E03 RID: 11779
	private float cloudBrightnessDefault;

	// Token: 0x04002E04 RID: 11780
	private float cloudDensityDefault;

	// Token: 0x04002E05 RID: 11781
	private float atmosphereFogDefault;

	// Token: 0x04002E06 RID: 11782
	private float cloudBrightness;

	// Token: 0x04002E07 RID: 11783
	private float cloudDensity;

	// Token: 0x04002E08 RID: 11784
	private float atmosphereFog;

	// Token: 0x04002E09 RID: 11785
	private float cloudSharpness;

	// Token: 0x04002E0A RID: 11786
	private global::TOD_Sky sky;

	// Token: 0x02000932 RID: 2354
	public enum CloudType
	{
		// Token: 0x04002E0C RID: 11788
		Custom,
		// Token: 0x04002E0D RID: 11789
		None,
		// Token: 0x04002E0E RID: 11790
		Few,
		// Token: 0x04002E0F RID: 11791
		Scattered,
		// Token: 0x04002E10 RID: 11792
		Broken,
		// Token: 0x04002E11 RID: 11793
		Overcast
	}

	// Token: 0x02000933 RID: 2355
	public enum WeatherType
	{
		// Token: 0x04002E13 RID: 11795
		Custom,
		// Token: 0x04002E14 RID: 11796
		Clear,
		// Token: 0x04002E15 RID: 11797
		Storm,
		// Token: 0x04002E16 RID: 11798
		Dust,
		// Token: 0x04002E17 RID: 11799
		Fog
	}
}
