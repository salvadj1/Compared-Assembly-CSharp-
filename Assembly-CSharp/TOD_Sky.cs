using System;
using UnityEngine;

// Token: 0x02000929 RID: 2345
[ExecuteInEditMode]
public class TOD_Sky : MonoBehaviour
{
	// Token: 0x06004F3C RID: 20284 RVA: 0x0014D610 File Offset: 0x0014B810
	internal Vector3 OrbitalToUnity(float radius, float theta, float phi)
	{
		float num = Mathf.Sin(theta);
		float num2 = Mathf.Cos(theta);
		float num3 = Mathf.Sin(phi);
		float num4 = Mathf.Cos(phi);
		Vector3 result;
		result.z = radius * num * num4;
		result.y = radius * num2;
		result.x = radius * num * num3;
		return result;
	}

	// Token: 0x06004F3D RID: 20285 RVA: 0x0014D660 File Offset: 0x0014B860
	internal Vector3 OrbitalToLocal(float theta, float phi)
	{
		float num = Mathf.Sin(theta);
		float y = Mathf.Cos(theta);
		float num2 = Mathf.Sin(phi);
		float num3 = Mathf.Cos(phi);
		Vector3 result;
		result.z = num * num3;
		result.y = y;
		result.x = num * num2;
		return result;
	}

	// Token: 0x06004F3E RID: 20286 RVA: 0x0014D6A8 File Offset: 0x0014B8A8
	internal Color SampleAtmosphere(Vector3 direction, bool clampAlpha = true)
	{
		direction = this.Components.DomeTransform.InverseTransformDirection(direction);
		float horizonOffset = this.World.HorizonOffset;
		float p = this.Atmosphere.Contrast * 0.454545438f;
		float haziness = this.Atmosphere.Haziness;
		float fogginess = this.Atmosphere.Fogginess;
		Color sunColor = this.SunColor;
		Color moonColor = this.MoonColor;
		Color moonHaloColor = this.MoonHaloColor;
		Color cloudColor = this.CloudColor;
		Color additiveColor = this.AdditiveColor;
		Vector3 vector = this.Components.DomeTransform.InverseTransformDirection(this.SunDirection);
		Vector3 vector2 = this.Components.DomeTransform.InverseTransformDirection(this.MoonDirection);
		Vector3 vector3 = this.opticalDepth;
		Vector3 vector4 = this.oneOverBeta;
		Vector3 vector5 = this.betaRayleigh;
		Vector3 vector6 = this.betaRayleighTheta;
		Vector3 vector7 = this.betaMie;
		Vector3 vector8 = this.betaMieTheta;
		Vector3 vector9 = this.betaMiePhase;
		Vector3 vector10 = this.betaNight;
		Color color = Color.black;
		float num = Mathf.Max(0f, Vector3.Dot(-direction, vector));
		float num2 = Mathf.Clamp(direction.y + horizonOffset, 0.001f, 1f);
		float num3 = Mathf.Pow(num2, haziness);
		float num4 = (1f - num3) * 190000f;
		float num5 = num4 + num3 * (vector3.x - num4);
		float num6 = num4 + num3 * (vector3.y - num4);
		float num7 = 1f + num * num;
		Vector3 vector11 = vector5 * num5 + vector7 * num6;
		Vector3 vector12 = vector6 + vector8 / Mathf.Pow(vector9.x - vector9.y * num, 1.5f);
		float r = sunColor.r;
		float g = sunColor.g;
		float b = sunColor.b;
		float r2 = moonColor.r;
		float g2 = moonColor.g;
		float b2 = moonColor.b;
		float num8 = Mathf.Exp(-vector11.x);
		float num9 = Mathf.Exp(-vector11.y);
		float num10 = Mathf.Exp(-vector11.z);
		float num11 = num7 * vector12.x * vector4.x;
		float num12 = num7 * vector12.y * vector4.y;
		float num13 = num7 * vector12.z * vector4.z;
		float x = vector10.x;
		float y = vector10.y;
		float z = vector10.z;
		color.r = (1f - num8) * (r * num11 + r2 * x);
		color.g = (1f - num9) * (g * num12 + g2 * y);
		color.b = (1f - num10) * (b * num13 + b2 * z);
		color.a = 10f * this.Max3(color.r, color.g, color.b);
		color += moonHaloColor * Mathf.Pow(Mathf.Max(0f, Vector3.Dot(vector2, -direction)), 10f);
		color += additiveColor;
		color.r = Mathf.Lerp(color.r, cloudColor.r, fogginess);
		color.g = Mathf.Lerp(color.g, cloudColor.g, fogginess);
		color.b = Mathf.Lerp(color.b, cloudColor.b, fogginess);
		color.a += fogginess;
		if (clampAlpha)
		{
			color.a = Mathf.Clamp01(color.a);
		}
		color = this.PowRGBA(color, p);
		return color;
	}

	// Token: 0x06004F3F RID: 20287 RVA: 0x0014DA68 File Offset: 0x0014BC68
	private void SetupScattering()
	{
		float num = 0.001f + this.Atmosphere.RayleighMultiplier * this.Atmosphere.ScatteringColor.r;
		float num2 = 0.001f + this.Atmosphere.RayleighMultiplier * this.Atmosphere.ScatteringColor.g;
		float num3 = 0.001f + this.Atmosphere.RayleighMultiplier * this.Atmosphere.ScatteringColor.b;
		this.betaRayleigh.x = 5.8E-06f * num;
		this.betaRayleigh.y = 1.35E-05f * num2;
		this.betaRayleigh.z = 3.31E-05f * num3;
		this.betaRayleighTheta.x = 0.000116f * num * 0.0596831031f;
		this.betaRayleighTheta.y = 0.00027f * num2 * 0.0596831031f;
		this.betaRayleighTheta.z = 0.000662000035f * num3 * 0.0596831031f;
		this.opticalDepth.x = 8000f * Mathf.Exp(-this.World.ViewerHeight * 50000f / 8000f);
		float num4 = 0.001f + this.Atmosphere.MieMultiplier * this.Atmosphere.ScatteringColor.r;
		float num5 = 0.001f + this.Atmosphere.MieMultiplier * this.Atmosphere.ScatteringColor.g;
		float num6 = 0.001f + this.Atmosphere.MieMultiplier * this.Atmosphere.ScatteringColor.b;
		float directionality = this.Atmosphere.Directionality;
		float num7 = 0.238732412f * (1f - directionality * directionality) / (2f + directionality * directionality);
		this.betaMie.x = 2E-06f * num4;
		this.betaMie.y = 2E-06f * num5;
		this.betaMie.z = 2E-06f * num6;
		this.betaMieTheta.x = 4E-05f * num4 * num7;
		this.betaMieTheta.y = 4E-05f * num5 * num7;
		this.betaMieTheta.z = 4E-05f * num6 * num7;
		this.betaMiePhase.x = 1f + directionality * directionality;
		this.betaMiePhase.y = 2f * directionality;
		this.opticalDepth.y = 1200f * Mathf.Exp(-this.World.ViewerHeight * 50000f / 1200f);
		this.oneOverBeta = this.Inverse(this.betaMie + this.betaRayleigh);
		this.betaNight = Vector3.Scale(this.betaRayleighTheta + this.betaMieTheta / Mathf.Pow(this.betaMiePhase.x, 1.5f), this.oneOverBeta);
	}

	// Token: 0x06004F40 RID: 20288 RVA: 0x0014DD58 File Offset: 0x0014BF58
	private void SetupSunAndMoon()
	{
		float num = 0.0174532924f * this.Cycle.Latitude;
		float num2 = Mathf.Sin(num);
		float num3 = Mathf.Cos(num);
		float longitude = this.Cycle.Longitude;
		float num4 = (float)(367 * this.Cycle.Year - 7 * (this.Cycle.Year + (this.Cycle.Month + 9) / 12) / 4 + 275 * this.Cycle.Month / 9 + this.Cycle.Day - 730530);
		float num5 = this.Cycle.Hour - this.Cycle.UTC;
		float num6 = 23.4393f - 3.563E-07f * num4;
		float num7 = 0.0174532924f * num6;
		float num8 = Mathf.Sin(num7);
		float num9 = Mathf.Cos(num7);
		float num10 = 282.9404f + 4.70935E-05f * num4;
		float num11 = 0.016709f - 1.151E-09f * num4;
		float num12 = 356.047f + 0.985600233f * num4;
		float num13 = 0.0174532924f * num12;
		float num14 = Mathf.Sin(num13);
		float num15 = Mathf.Cos(num13);
		float num16 = num12 + num11 * 57.29578f * num14 * (1f + num11 * num15);
		float num17 = 0.0174532924f * num16;
		float num18 = Mathf.Sin(num17);
		float num19 = Mathf.Cos(num17);
		float num20 = num19 - num11;
		float num21 = num18 * Mathf.Sqrt(1f - num11 * num11);
		float num22 = 57.29578f * Mathf.Atan2(num21, num20);
		float num23 = Mathf.Sqrt(num20 * num20 + num21 * num21);
		float num24 = num22 + num10;
		float num25 = 0.0174532924f * num24;
		float num26 = Mathf.Sin(num25);
		float num27 = Mathf.Cos(num25);
		float num28 = num23 * num27;
		float num29 = num23 * num26;
		float num30 = num28;
		float num31 = num29 * num9;
		float num32 = num29 * num8;
		float num33 = Mathf.Atan2(num31, num30);
		float num34 = 57.29578f * num33;
		float num35 = Mathf.Atan2(num32, Mathf.Sqrt(num30 * num30 + num31 * num31));
		float num36 = Mathf.Sin(num35);
		float num37 = Mathf.Cos(num35);
		float num38 = num22 + num10 + 180f;
		float num39 = num38 + num5 * 15f;
		float num40 = num39 + longitude;
		float num41 = num40 - num34;
		float num42 = 0.0174532924f * num41;
		float num43 = Mathf.Sin(num42);
		float num44 = Mathf.Cos(num42);
		float num45 = num44 * num37;
		float num46 = num43 * num37;
		float num47 = num36;
		float num48 = num45 * num2 - num47 * num3;
		float num49 = num46;
		float num50 = num45 * num3 + num47 * num2;
		float num51 = Mathf.Atan2(num49, num48) + 3.14159274f;
		float num52 = Mathf.Atan2(num50, Mathf.Sqrt(num48 * num48 + num49 * num49));
		float num53 = 1.57079637f - num52;
		float phi = num51;
		Vector3 localPosition = this.OrbitalToLocal(num53, phi);
		this.Components.SunTransform.localPosition = localPosition;
		this.Components.SunTransform.LookAt(this.Components.DomeTransform.position, this.Components.SunTransform.up);
		if (this.Components.CameraTransform != null)
		{
			Vector3 eulerAngles = this.Components.CameraTransform.rotation.eulerAngles;
			Vector3 localEulerAngles = this.Components.SunTransform.localEulerAngles;
			localEulerAngles.z = 2f * Time.time + Mathf.Abs(eulerAngles.x) + Mathf.Abs(eulerAngles.y) + Mathf.Abs(eulerAngles.z);
			this.Components.SunTransform.localEulerAngles = localEulerAngles;
		}
		Vector3 localPosition2 = this.OrbitalToLocal(num53 + 3.14159274f, phi);
		this.Components.MoonTransform.localPosition = localPosition2;
		this.Components.MoonTransform.LookAt(this.Components.DomeTransform.position, this.Components.MoonTransform.up);
		float num54 = 4f * Mathf.Tan(0.008726646f * this.Day.SunMeshSize);
		float num55 = 2f * num54;
		Vector3 localScale;
		localScale..ctor(num55, num55, num55);
		this.Components.SunTransform.localScale = localScale;
		float num56 = 2f * Mathf.Tan(0.008726646f * this.Night.MoonMeshSize);
		float num57 = 2f * num56;
		Vector3 localScale2;
		localScale2..ctor(num57, num57, num57);
		this.Components.MoonTransform.localScale = localScale2;
		this.SunZenith = 57.29578f * num53;
		this.MoonZenith = Mathf.PingPong(this.SunZenith + 180f, 180f);
		bool enabled = this.Components.SunTransform.localPosition.y > -0.5f;
		bool enabled2 = this.Components.MoonTransform.localPosition.y > -0.1f;
		bool enabled3 = this.SampleAtmosphere(Vector3.up, false).a < 1.1f;
		bool enabled4 = this.Clouds.Density > 0f;
		this.Components.SunRenderer.enabled = enabled;
		this.Components.MoonRenderer.enabled = enabled2;
		this.Components.SpaceRenderer.enabled = enabled3;
		this.Components.CloudRenderer.enabled = enabled4;
		this.SetupLightSource(num53, phi);
	}

	// Token: 0x06004F41 RID: 20289 RVA: 0x0014E2D0 File Offset: 0x0014C4D0
	private void SetupLightSource(float theta, float phi)
	{
		float num = Mathf.Cos(Mathf.Pow(theta / 6.28318548f, 2f - this.Light.Falloff) * 2f * 3.14159274f);
		float num2 = Mathf.Sqrt(501264f * num * num + 1416f + 1f) - 708f * num;
		float num3 = this.Day.SunLightColor.r;
		float num4 = this.Day.SunLightColor.g;
		float num5 = this.Day.SunLightColor.b;
		float num6 = this.Components.LightSource.intensity / Mathf.Max(this.Day.SunLightIntensity, this.Night.MoonLightIntensity);
		num3 *= Mathf.Exp(-0.008735f * Mathf.Pow(0.68f, -4.08f * num2));
		num4 *= Mathf.Exp(-0.008735f * Mathf.Pow(0.55f, -4.08f * num2));
		num5 *= Mathf.Exp(-0.008735f * Mathf.Pow(0.44f, -4.08f * num2));
		this.LerpValue = Mathf.Clamp01(1.1f * this.Max3(num3, num4, num5));
		float r = this.Night.MoonLightColor.r;
		float g = this.Night.MoonLightColor.g;
		float b = this.Night.MoonLightColor.b;
		float num7 = this.Day.SunLightColor.r * Mathf.Lerp(1f, num3, this.Light.Coloring);
		float num8 = this.Day.SunLightColor.g * Mathf.Lerp(1f, num4, this.Light.Coloring);
		float num9 = this.Day.SunLightColor.b * Mathf.Lerp(1f, num5, this.Light.Coloring);
		float num10 = this.Day.SunShaftColor.r * Mathf.Lerp(1f, num3, this.Light.ShaftColoring);
		float num11 = this.Day.SunShaftColor.g * Mathf.Lerp(1f, num4, this.Light.ShaftColoring);
		float num12 = this.Day.SunShaftColor.b * Mathf.Lerp(1f, num5, this.Light.ShaftColoring);
		Color color;
		color..ctor(r, g, b, num6);
		Color color2;
		color2..ctor(num7, num8, num9, num6);
		Color color3 = Color.Lerp(color, color2, this.Max3(color2.r, color2.g, color2.b));
		this.SunShaftColor = new Color(num10, num11, num12, num6);
		float num13 = Mathf.Lerp(this.Night.AmbientIntensity, this.Day.AmbientIntensity, this.LerpValue);
		this.AmbientColor = new Color(color3.r * num13, color3.g * num13, color3.b * num13, 1f);
		this.SunColor = this.Atmosphere.Brightness * this.Day.SkyMultiplier * Mathf.Lerp(1f, 0.1f, Mathf.Sqrt(this.SunZenith / 90f) - 0.25f) * Color.Lerp(this.Day.SunLightColor * this.LerpValue, new Color(num3, num4, num5, num6), this.Light.SkyColoring);
		this.SunColor = new Color(this.SunColor.r, this.SunColor.g, this.SunColor.b, this.LerpValue);
		this.MoonColor = (1f - this.LerpValue) * 0.5f * this.Atmosphere.Brightness * this.Night.SkyMultiplier * this.Night.MoonLightColor;
		this.MoonColor = new Color(this.MoonColor.r, this.MoonColor.g, this.MoonColor.b, 1f - this.LerpValue);
		Color moonHaloColor = (1f - this.LerpValue) * (1f - Mathf.Abs(this.Cycle.MoonPhase)) * this.Atmosphere.Brightness * this.Night.MoonHaloColor;
		moonHaloColor.r *= moonHaloColor.a;
		moonHaloColor.g *= moonHaloColor.a;
		moonHaloColor.b *= moonHaloColor.a;
		moonHaloColor.a = this.Max3(moonHaloColor.r, moonHaloColor.g, moonHaloColor.b);
		this.MoonHaloColor = moonHaloColor;
		Color color4 = Color.Lerp(this.MoonColor, this.SunColor, this.LerpValue);
		float num14 = Mathf.Lerp(this.Night.CloudMultiplier, this.Day.CloudMultiplier, this.LerpValue);
		float num15 = (color4.r + color4.g + color4.b) / 3f;
		this.CloudColor = num14 * 1.25f * this.Clouds.Brightness * Color.Lerp(new Color(num15, num15, num15), color4, this.Light.CloudColoring);
		this.CloudColor = new Color(this.CloudColor.r, this.CloudColor.g, this.CloudColor.b, num14);
		Color additiveColor = Color.Lerp(this.Night.AdditiveColor, this.Day.AdditiveColor, this.LerpValue);
		additiveColor.r *= additiveColor.a;
		additiveColor.g *= additiveColor.a;
		additiveColor.b *= additiveColor.a;
		additiveColor.a = this.Max3(additiveColor.r, additiveColor.g, additiveColor.b);
		this.AdditiveColor = additiveColor;
		float intensity;
		float shadowStrength;
		Vector3 localPosition;
		if (this.LerpValue > 0.2f)
		{
			float num16 = (this.LerpValue - 0.2f) / 0.8f;
			intensity = Mathf.Lerp(0f, this.Day.SunLightIntensity, num16);
			shadowStrength = this.Day.ShadowStrength;
			localPosition = this.OrbitalToLocal(Mathf.Min(theta, (1f - this.Light.MinimumHeight) * 3.14159274f / 2f), phi);
			this.Components.LightSource.color = color2;
		}
		else
		{
			float num17 = (0.2f - this.LerpValue) / 0.2f;
			float num18 = 1f - Mathf.Abs(this.Cycle.MoonPhase);
			intensity = Mathf.Lerp(0f, this.Night.MoonLightIntensity * num18, num17);
			shadowStrength = this.Night.ShadowStrength;
			localPosition = this.OrbitalToLocal(Mathf.Max(theta + 3.14159274f, (1f + this.Light.MinimumHeight) * 3.14159274f / 2f + 3.14159274f), phi);
			this.Components.LightSource.color = color;
		}
		LightShadows shadows = (this.Components.LightSource.shadowStrength != 0f) ? 2 : 0;
		this.Components.LightSource.intensity = intensity;
		this.Components.LightSource.shadowStrength = shadowStrength;
		this.Components.LightTransform.localPosition = localPosition;
		this.Components.LightTransform.LookAt(this.Components.DomeTransform.position);
		this.Components.LightSource.shadows = shadows;
	}

	// Token: 0x06004F42 RID: 20290 RVA: 0x0014EAEC File Offset: 0x0014CCEC
	private Color SampleFogColor()
	{
		Vector3 vector = (!(this.Components.CameraTransform != null)) ? Vector3.forward : this.Components.CameraTransform.forward;
		Vector3 direction = Vector3.Lerp(new Vector3(vector.x, 0f, vector.z), Vector3.up, this.World.FogColorBias);
		Color color = this.SampleAtmosphere(direction, true);
		return new Color(color.a * color.r, color.a * color.g, color.a * color.b, 1f);
	}

	// Token: 0x06004F43 RID: 20291 RVA: 0x0014EB98 File Offset: 0x0014CD98
	private Color PowRGB(Color c, float p)
	{
		return new Color(Mathf.Pow(c.r, p), Mathf.Pow(c.g, p), Mathf.Pow(c.b, p), c.a);
	}

	// Token: 0x06004F44 RID: 20292 RVA: 0x0014EBD0 File Offset: 0x0014CDD0
	private Color PowRGBA(Color c, float p)
	{
		return new Color(Mathf.Pow(c.r, p), Mathf.Pow(c.g, p), Mathf.Pow(c.b, p), Mathf.Pow(c.a, p));
	}

	// Token: 0x06004F45 RID: 20293 RVA: 0x0014EC18 File Offset: 0x0014CE18
	private float Max3(float a, float b, float c)
	{
		return (a < b || a < c) ? ((b < c) ? c : b) : a;
	}

	// Token: 0x06004F46 RID: 20294 RVA: 0x0014EC48 File Offset: 0x0014CE48
	private Vector3 Inverse(Vector3 v)
	{
		return new Vector3(1f / v.x, 1f / v.y, 1f / v.z);
	}

	// Token: 0x06004F47 RID: 20295 RVA: 0x0014EC84 File Offset: 0x0014CE84
	private void SetupQualitySettings()
	{
		global::TOD_Resources resources = this.Components.Resources;
		Material material = null;
		Material material2 = null;
		switch (this.CloudQuality)
		{
		case global::TOD_Sky.CloudQualityType.Fastest:
			material = resources.CloudMaterialFastest;
			material2 = resources.ShadowMaterialFastest;
			break;
		case global::TOD_Sky.CloudQualityType.Density:
			material = resources.CloudMaterialDensity;
			material2 = resources.ShadowMaterialDensity;
			break;
		case global::TOD_Sky.CloudQualityType.Bumped:
			material = resources.CloudMaterialBumped;
			material2 = resources.ShadowMaterialBumped;
			break;
		default:
			Debug.LogError("Unknown cloud quality.");
			break;
		}
		Mesh mesh = null;
		Mesh mesh2 = null;
		Mesh mesh3 = null;
		Mesh mesh4 = null;
		Mesh mesh5 = null;
		Mesh mesh6 = null;
		switch (this.MeshQuality)
		{
		case global::TOD_Sky.MeshQualityType.Low:
			mesh = resources.IcosphereLow;
			mesh2 = resources.IcosphereLow;
			mesh3 = resources.IcosphereLow;
			mesh4 = resources.HalfIcosphereLow;
			mesh5 = resources.Quad;
			mesh6 = resources.SphereLow;
			break;
		case global::TOD_Sky.MeshQualityType.Medium:
			mesh = resources.IcosphereMedium;
			mesh2 = resources.IcosphereMedium;
			mesh3 = resources.IcosphereLow;
			mesh4 = resources.HalfIcosphereMedium;
			mesh5 = resources.Quad;
			mesh6 = resources.SphereMedium;
			break;
		case global::TOD_Sky.MeshQualityType.High:
			mesh = resources.IcosphereHigh;
			mesh2 = resources.IcosphereHigh;
			mesh3 = resources.IcosphereLow;
			mesh4 = resources.HalfIcosphereHigh;
			mesh5 = resources.Quad;
			mesh6 = resources.SphereHigh;
			break;
		default:
			Debug.LogError("Unknown mesh quality.");
			break;
		}
		if (!this.Components.SpaceShader || this.Components.SpaceShader.name != resources.SpaceMaterial.name)
		{
			global::TOD_Components components = this.Components;
			Material material3 = resources.SpaceMaterial;
			this.Components.SpaceRenderer.sharedMaterial = material3;
			components.SpaceShader = material3;
		}
		if (!this.Components.AtmosphereShader || this.Components.AtmosphereShader.name != resources.AtmosphereMaterial.name)
		{
			global::TOD_Components components2 = this.Components;
			Material material3 = resources.AtmosphereMaterial;
			this.Components.AtmosphereRenderer.sharedMaterial = material3;
			components2.AtmosphereShader = material3;
		}
		if (!this.Components.ClearShader || this.Components.ClearShader.name != resources.ClearMaterial.name)
		{
			global::TOD_Components components3 = this.Components;
			Material material3 = resources.ClearMaterial;
			this.Components.ClearRenderer.sharedMaterial = material3;
			components3.ClearShader = material3;
		}
		if (!this.Components.CloudShader || this.Components.CloudShader.name != material.name)
		{
			global::TOD_Components components4 = this.Components;
			Material material3 = material;
			this.Components.CloudRenderer.sharedMaterial = material3;
			components4.CloudShader = material3;
		}
		if (!this.Components.ShadowShader || this.Components.ShadowShader.name != material2.name)
		{
			global::TOD_Components components5 = this.Components;
			Material material3 = material2;
			this.Components.ShadowProjector.material = material3;
			components5.ShadowShader = material3;
		}
		if (!this.Components.SunShader || this.Components.SunShader.name != resources.SunMaterial.name)
		{
			global::TOD_Components components6 = this.Components;
			Material material3 = resources.SunMaterial;
			this.Components.SunRenderer.sharedMaterial = material3;
			components6.SunShader = material3;
		}
		if (!this.Components.MoonShader || this.Components.MoonShader.name != resources.MoonMaterial.name)
		{
			global::TOD_Components components7 = this.Components;
			Material material3 = resources.MoonMaterial;
			this.Components.MoonRenderer.sharedMaterial = material3;
			components7.MoonShader = material3;
		}
		if (this.Components.SpaceMeshFilter.sharedMesh != mesh)
		{
			this.Components.SpaceMeshFilter.mesh = mesh;
		}
		if (this.Components.AtmosphereMeshFilter.sharedMesh != mesh2)
		{
			this.Components.AtmosphereMeshFilter.mesh = mesh2;
		}
		if (this.Components.ClearMeshFilter.sharedMesh != mesh3)
		{
			this.Components.ClearMeshFilter.mesh = mesh3;
		}
		if (this.Components.CloudMeshFilter.sharedMesh != mesh4)
		{
			this.Components.CloudMeshFilter.mesh = mesh4;
		}
		if (this.Components.SunMeshFilter.sharedMesh != mesh5)
		{
			this.Components.SunMeshFilter.mesh = mesh5;
		}
		if (this.Components.MoonMeshFilter.sharedMesh != mesh6)
		{
			this.Components.MoonMeshFilter.mesh = mesh6;
		}
	}

	// Token: 0x06004F48 RID: 20296 RVA: 0x0014F18C File Offset: 0x0014D38C
	protected void OnEnable()
	{
		this.Components = base.GetComponent<global::TOD_Components>();
		if (!this.Components)
		{
			Debug.LogError("TOD_Components not found. Disabling script.");
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06004F49 RID: 20297 RVA: 0x0014F1C8 File Offset: 0x0014D3C8
	protected void Update()
	{
		if (this.Components.SunShafts != null && this.Components.SunShafts.enabled)
		{
			if (!this.Components.ClearRenderer.enabled)
			{
				this.Components.ClearRenderer.enabled = true;
			}
		}
		else if (this.Components.ClearRenderer.enabled)
		{
			this.Components.ClearRenderer.enabled = false;
		}
		this.Cycle.CheckRange();
		this.SetupQualitySettings();
		this.SetupSunAndMoon();
		this.SetupScattering();
		if (this.World.SetFogColor)
		{
			Color fogColor = this.SampleFogColor();
			RenderSettings.fogColor = fogColor;
		}
		if (this.World.SetAmbientLight)
		{
			RenderSettings.ambientLight = this.AmbientColor;
		}
		Vector4 vector = this.Components.Animation.CloudUV + this.Components.Animation.OffsetUV;
		Shader.SetGlobalFloat("TOD_Gamma", this.Gamma);
		Shader.SetGlobalFloat("TOD_OneOverGamma", this.OneOverGamma);
		Shader.SetGlobalColor("TOD_LightColor", this.LightColor);
		Shader.SetGlobalColor("TOD_CloudColor", this.CloudColor);
		Shader.SetGlobalColor("TOD_SunColor", this.SunColor);
		Shader.SetGlobalColor("TOD_MoonColor", this.MoonColor);
		Shader.SetGlobalColor("TOD_AdditiveColor", this.AdditiveColor);
		Shader.SetGlobalColor("TOD_MoonHaloColor", this.MoonHaloColor);
		Shader.SetGlobalVector("TOD_SunDirection", this.SunDirection);
		Shader.SetGlobalVector("TOD_MoonDirection", this.MoonDirection);
		Shader.SetGlobalVector("TOD_LightDirection", this.LightDirection);
		Shader.SetGlobalVector("TOD_LocalSunDirection", this.Components.DomeTransform.InverseTransformDirection(this.SunDirection));
		Shader.SetGlobalVector("TOD_LocalMoonDirection", this.Components.DomeTransform.InverseTransformDirection(this.MoonDirection));
		Shader.SetGlobalVector("TOD_LocalLightDirection", this.Components.DomeTransform.InverseTransformDirection(this.LightDirection));
		if (this.Components.AtmosphereShader != null)
		{
			this.Components.AtmosphereShader.SetFloat("_Contrast", this.Atmosphere.Contrast * this.OneOverGamma);
			this.Components.AtmosphereShader.SetFloat("_Haziness", this.Atmosphere.Haziness);
			this.Components.AtmosphereShader.SetFloat("_Fogginess", this.Atmosphere.Fogginess);
			this.Components.AtmosphereShader.SetFloat("_Horizon", this.World.HorizonOffset);
			this.Components.AtmosphereShader.SetVector("_OpticalDepth", this.opticalDepth);
			this.Components.AtmosphereShader.SetVector("_OneOverBeta", this.oneOverBeta);
			this.Components.AtmosphereShader.SetVector("_BetaRayleigh", this.betaRayleigh);
			this.Components.AtmosphereShader.SetVector("_BetaRayleighTheta", this.betaRayleighTheta);
			this.Components.AtmosphereShader.SetVector("_BetaMie", this.betaMie);
			this.Components.AtmosphereShader.SetVector("_BetaMieTheta", this.betaMieTheta);
			this.Components.AtmosphereShader.SetVector("_BetaMiePhase", this.betaMiePhase);
			this.Components.AtmosphereShader.SetVector("_BetaNight", this.betaNight);
		}
		if (this.Components.CloudShader != null)
		{
			float num = (1f - this.Atmosphere.Fogginess) * this.LerpValue;
			float num2 = (1f - this.Atmosphere.Fogginess) * 0.6f * (1f - Mathf.Abs(this.Cycle.MoonPhase));
			this.Components.CloudShader.SetFloat("_SunGlow", num);
			this.Components.CloudShader.SetFloat("_MoonGlow", num2);
			this.Components.CloudShader.SetFloat("_CloudDensity", this.Clouds.Density);
			this.Components.CloudShader.SetFloat("_CloudSharpness", this.Clouds.Sharpness);
			this.Components.CloudShader.SetVector("_CloudScale1", this.Clouds.Scale1);
			this.Components.CloudShader.SetVector("_CloudScale2", this.Clouds.Scale2);
			this.Components.CloudShader.SetVector("_CloudUV", vector);
		}
		if (this.Components.SpaceShader != null)
		{
			Vector2 mainTextureScale;
			mainTextureScale..ctor(this.Stars.Tiling, this.Stars.Tiling);
			this.Components.SpaceShader.mainTextureScale = mainTextureScale;
			this.Components.SpaceShader.SetFloat("_Subtract", 1f - Mathf.Pow(this.Stars.Density, 0.1f));
		}
		if (this.Components.SunShader != null)
		{
			this.Components.SunShader.SetColor("_Color", this.Day.SunMeshColor * this.LerpValue * (1f - this.Atmosphere.Fogginess));
		}
		if (this.Components.MoonShader != null)
		{
			this.Components.MoonShader.SetColor("_Color", this.Night.MoonMeshColor);
			this.Components.MoonShader.SetFloat("_Phase", this.Cycle.MoonPhase);
		}
		if (this.Components.ShadowShader != null)
		{
			float num3 = this.Clouds.ShadowStrength * Mathf.Clamp01(1f - this.LightZenith / 90f);
			this.Components.ShadowShader.SetFloat("_Alpha", num3);
			this.Components.ShadowShader.SetFloat("_CloudDensity", this.Clouds.Density);
			this.Components.ShadowShader.SetFloat("_CloudSharpness", this.Clouds.Sharpness);
			this.Components.ShadowShader.SetVector("_CloudScale1", this.Clouds.Scale1);
			this.Components.ShadowShader.SetVector("_CloudScale2", this.Clouds.Scale2);
			this.Components.ShadowShader.SetVector("_CloudUV", vector);
		}
		if (this.Components.ShadowProjector != null)
		{
			bool enabled = this.Clouds.ShadowStrength != 0f && this.Components.ShadowShader != null;
			float farClipPlane = this.Radius * 2f;
			float radius = this.Radius;
			this.Components.ShadowProjector.enabled = enabled;
			this.Components.ShadowProjector.farClipPlane = farClipPlane;
			this.Components.ShadowProjector.orthographicSize = radius;
		}
	}

	// Token: 0x17000F28 RID: 3880
	// (get) Token: 0x06004F4A RID: 20298 RVA: 0x0014F964 File Offset: 0x0014DB64
	// (set) Token: 0x06004F4B RID: 20299 RVA: 0x0014F96C File Offset: 0x0014DB6C
	internal global::TOD_Components Components { get; private set; }

	// Token: 0x17000F29 RID: 3881
	// (get) Token: 0x06004F4C RID: 20300 RVA: 0x0014F978 File Offset: 0x0014DB78
	internal bool IsDay
	{
		get
		{
			return this.LerpValue > 0f;
		}
	}

	// Token: 0x17000F2A RID: 3882
	// (get) Token: 0x06004F4D RID: 20301 RVA: 0x0014F988 File Offset: 0x0014DB88
	internal bool IsNight
	{
		get
		{
			return this.LerpValue == 0f;
		}
	}

	// Token: 0x17000F2B RID: 3883
	// (get) Token: 0x06004F4E RID: 20302 RVA: 0x0014F998 File Offset: 0x0014DB98
	internal float Radius
	{
		get
		{
			return this.Components.DomeTransform.localScale.x;
		}
	}

	// Token: 0x17000F2C RID: 3884
	// (get) Token: 0x06004F4F RID: 20303 RVA: 0x0014F9C0 File Offset: 0x0014DBC0
	internal float Gamma
	{
		get
		{
			return ((this.UnityColorSpace != global::TOD_Sky.ColorSpaceDetection.Auto || QualitySettings.activeColorSpace != 1) && this.UnityColorSpace != global::TOD_Sky.ColorSpaceDetection.Linear) ? 2.2f : 1f;
		}
	}

	// Token: 0x17000F2D RID: 3885
	// (get) Token: 0x06004F50 RID: 20304 RVA: 0x0014F9F4 File Offset: 0x0014DBF4
	internal float OneOverGamma
	{
		get
		{
			return ((this.UnityColorSpace != global::TOD_Sky.ColorSpaceDetection.Auto || QualitySettings.activeColorSpace != 1) && this.UnityColorSpace != global::TOD_Sky.ColorSpaceDetection.Linear) ? 0.454545438f : 1f;
		}
	}

	// Token: 0x17000F2E RID: 3886
	// (get) Token: 0x06004F51 RID: 20305 RVA: 0x0014FA28 File Offset: 0x0014DC28
	// (set) Token: 0x06004F52 RID: 20306 RVA: 0x0014FA30 File Offset: 0x0014DC30
	internal float LerpValue { get; private set; }

	// Token: 0x17000F2F RID: 3887
	// (get) Token: 0x06004F53 RID: 20307 RVA: 0x0014FA3C File Offset: 0x0014DC3C
	// (set) Token: 0x06004F54 RID: 20308 RVA: 0x0014FA44 File Offset: 0x0014DC44
	internal float SunZenith { get; private set; }

	// Token: 0x17000F30 RID: 3888
	// (get) Token: 0x06004F55 RID: 20309 RVA: 0x0014FA50 File Offset: 0x0014DC50
	// (set) Token: 0x06004F56 RID: 20310 RVA: 0x0014FA58 File Offset: 0x0014DC58
	internal float MoonZenith { get; private set; }

	// Token: 0x17000F31 RID: 3889
	// (get) Token: 0x06004F57 RID: 20311 RVA: 0x0014FA64 File Offset: 0x0014DC64
	internal float LightZenith
	{
		get
		{
			return Mathf.Min(this.SunZenith, this.MoonZenith);
		}
	}

	// Token: 0x17000F32 RID: 3890
	// (get) Token: 0x06004F58 RID: 20312 RVA: 0x0014FA78 File Offset: 0x0014DC78
	internal float LightIntensity
	{
		get
		{
			return this.Components.LightSource.intensity;
		}
	}

	// Token: 0x17000F33 RID: 3891
	// (get) Token: 0x06004F59 RID: 20313 RVA: 0x0014FA8C File Offset: 0x0014DC8C
	internal Vector3 MoonDirection
	{
		get
		{
			return this.Components.MoonTransform.forward;
		}
	}

	// Token: 0x17000F34 RID: 3892
	// (get) Token: 0x06004F5A RID: 20314 RVA: 0x0014FAA0 File Offset: 0x0014DCA0
	internal Vector3 SunDirection
	{
		get
		{
			return this.Components.SunTransform.forward;
		}
	}

	// Token: 0x17000F35 RID: 3893
	// (get) Token: 0x06004F5B RID: 20315 RVA: 0x0014FAB4 File Offset: 0x0014DCB4
	internal Vector3 LightDirection
	{
		get
		{
			return Vector3.Lerp(this.MoonDirection, this.SunDirection, this.LerpValue * this.LerpValue);
		}
	}

	// Token: 0x17000F36 RID: 3894
	// (get) Token: 0x06004F5C RID: 20316 RVA: 0x0014FAE0 File Offset: 0x0014DCE0
	internal Color LightColor
	{
		get
		{
			return this.Components.LightSource.color;
		}
	}

	// Token: 0x17000F37 RID: 3895
	// (get) Token: 0x06004F5D RID: 20317 RVA: 0x0014FAF4 File Offset: 0x0014DCF4
	// (set) Token: 0x06004F5E RID: 20318 RVA: 0x0014FAFC File Offset: 0x0014DCFC
	internal Color SunShaftColor { get; private set; }

	// Token: 0x17000F38 RID: 3896
	// (get) Token: 0x06004F5F RID: 20319 RVA: 0x0014FB08 File Offset: 0x0014DD08
	// (set) Token: 0x06004F60 RID: 20320 RVA: 0x0014FB10 File Offset: 0x0014DD10
	internal Color SunColor { get; private set; }

	// Token: 0x17000F39 RID: 3897
	// (get) Token: 0x06004F61 RID: 20321 RVA: 0x0014FB1C File Offset: 0x0014DD1C
	// (set) Token: 0x06004F62 RID: 20322 RVA: 0x0014FB24 File Offset: 0x0014DD24
	internal Color MoonColor { get; private set; }

	// Token: 0x17000F3A RID: 3898
	// (get) Token: 0x06004F63 RID: 20323 RVA: 0x0014FB30 File Offset: 0x0014DD30
	// (set) Token: 0x06004F64 RID: 20324 RVA: 0x0014FB38 File Offset: 0x0014DD38
	internal Color MoonHaloColor { get; private set; }

	// Token: 0x17000F3B RID: 3899
	// (get) Token: 0x06004F65 RID: 20325 RVA: 0x0014FB44 File Offset: 0x0014DD44
	// (set) Token: 0x06004F66 RID: 20326 RVA: 0x0014FB4C File Offset: 0x0014DD4C
	internal Color CloudColor { get; private set; }

	// Token: 0x17000F3C RID: 3900
	// (get) Token: 0x06004F67 RID: 20327 RVA: 0x0014FB58 File Offset: 0x0014DD58
	// (set) Token: 0x06004F68 RID: 20328 RVA: 0x0014FB60 File Offset: 0x0014DD60
	internal Color AdditiveColor { get; private set; }

	// Token: 0x17000F3D RID: 3901
	// (get) Token: 0x06004F69 RID: 20329 RVA: 0x0014FB6C File Offset: 0x0014DD6C
	// (set) Token: 0x06004F6A RID: 20330 RVA: 0x0014FB74 File Offset: 0x0014DD74
	internal Color AmbientColor { get; private set; }

	// Token: 0x17000F3E RID: 3902
	// (get) Token: 0x06004F6B RID: 20331 RVA: 0x0014FB80 File Offset: 0x0014DD80
	internal Color FogColor
	{
		get
		{
			return (!this.World.SetFogColor) ? this.SampleFogColor() : RenderSettings.fogColor;
		}
	}

	// Token: 0x04002DB6 RID: 11702
	private const float pi = 3.14159274f;

	// Token: 0x04002DB7 RID: 11703
	private const float pi2 = 9.869605f;

	// Token: 0x04002DB8 RID: 11704
	private const float pi3 = 31.006279f;

	// Token: 0x04002DB9 RID: 11705
	private const float pi4 = 97.4091f;

	// Token: 0x04002DBA RID: 11706
	private Vector2 opticalDepth;

	// Token: 0x04002DBB RID: 11707
	private Vector3 oneOverBeta;

	// Token: 0x04002DBC RID: 11708
	private Vector3 betaRayleigh;

	// Token: 0x04002DBD RID: 11709
	private Vector3 betaRayleighTheta;

	// Token: 0x04002DBE RID: 11710
	private Vector3 betaMie;

	// Token: 0x04002DBF RID: 11711
	private Vector3 betaMieTheta;

	// Token: 0x04002DC0 RID: 11712
	private Vector2 betaMiePhase;

	// Token: 0x04002DC1 RID: 11713
	private Vector3 betaNight;

	// Token: 0x04002DC2 RID: 11714
	public global::TOD_Sky.ColorSpaceDetection UnityColorSpace;

	// Token: 0x04002DC3 RID: 11715
	public global::TOD_Sky.CloudQualityType CloudQuality = global::TOD_Sky.CloudQualityType.Bumped;

	// Token: 0x04002DC4 RID: 11716
	public global::TOD_Sky.MeshQualityType MeshQuality = global::TOD_Sky.MeshQualityType.High;

	// Token: 0x04002DC5 RID: 11717
	public global::TOD_CycleParameters Cycle;

	// Token: 0x04002DC6 RID: 11718
	public global::TOD_AtmosphereParameters Atmosphere;

	// Token: 0x04002DC7 RID: 11719
	public global::TOD_DayParameters Day;

	// Token: 0x04002DC8 RID: 11720
	public global::TOD_NightParameters Night;

	// Token: 0x04002DC9 RID: 11721
	public global::TOD_LightParameters Light;

	// Token: 0x04002DCA RID: 11722
	public global::TOD_StarParameters Stars;

	// Token: 0x04002DCB RID: 11723
	public global::TOD_CloudParameters Clouds;

	// Token: 0x04002DCC RID: 11724
	public global::TOD_WorldParameters World;

	// Token: 0x0200092A RID: 2346
	public enum ColorSpaceDetection
	{
		// Token: 0x04002DD9 RID: 11737
		Auto,
		// Token: 0x04002DDA RID: 11738
		Linear,
		// Token: 0x04002DDB RID: 11739
		Gamma
	}

	// Token: 0x0200092B RID: 2347
	public enum CloudQualityType
	{
		// Token: 0x04002DDD RID: 11741
		Fastest,
		// Token: 0x04002DDE RID: 11742
		Density,
		// Token: 0x04002DDF RID: 11743
		Bumped
	}

	// Token: 0x0200092C RID: 2348
	public enum MeshQualityType
	{
		// Token: 0x04002DE1 RID: 11745
		Low,
		// Token: 0x04002DE2 RID: 11746
		Medium,
		// Token: 0x04002DE3 RID: 11747
		High
	}
}
