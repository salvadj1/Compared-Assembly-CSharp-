using System;
using UnityEngine;

// Token: 0x02000834 RID: 2100
[ExecuteInEditMode]
public class TOD_Sky : MonoBehaviour
{
	// Token: 0x06004A81 RID: 19073 RVA: 0x001436AC File Offset: 0x001418AC
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

	// Token: 0x06004A82 RID: 19074 RVA: 0x001436FC File Offset: 0x001418FC
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

	// Token: 0x06004A83 RID: 19075 RVA: 0x00143744 File Offset: 0x00141944
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

	// Token: 0x06004A84 RID: 19076 RVA: 0x00143B04 File Offset: 0x00141D04
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

	// Token: 0x06004A85 RID: 19077 RVA: 0x00143DF4 File Offset: 0x00141FF4
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

	// Token: 0x06004A86 RID: 19078 RVA: 0x0014436C File Offset: 0x0014256C
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

	// Token: 0x06004A87 RID: 19079 RVA: 0x00144B88 File Offset: 0x00142D88
	private Color SampleFogColor()
	{
		Vector3 vector = (!(this.Components.CameraTransform != null)) ? Vector3.forward : this.Components.CameraTransform.forward;
		Vector3 direction = Vector3.Lerp(new Vector3(vector.x, 0f, vector.z), Vector3.up, this.World.FogColorBias);
		Color color = this.SampleAtmosphere(direction, true);
		return new Color(color.a * color.r, color.a * color.g, color.a * color.b, 1f);
	}

	// Token: 0x06004A88 RID: 19080 RVA: 0x00144C34 File Offset: 0x00142E34
	private Color PowRGB(Color c, float p)
	{
		return new Color(Mathf.Pow(c.r, p), Mathf.Pow(c.g, p), Mathf.Pow(c.b, p), c.a);
	}

	// Token: 0x06004A89 RID: 19081 RVA: 0x00144C6C File Offset: 0x00142E6C
	private Color PowRGBA(Color c, float p)
	{
		return new Color(Mathf.Pow(c.r, p), Mathf.Pow(c.g, p), Mathf.Pow(c.b, p), Mathf.Pow(c.a, p));
	}

	// Token: 0x06004A8A RID: 19082 RVA: 0x00144CB4 File Offset: 0x00142EB4
	private float Max3(float a, float b, float c)
	{
		return (a < b || a < c) ? ((b < c) ? c : b) : a;
	}

	// Token: 0x06004A8B RID: 19083 RVA: 0x00144CE4 File Offset: 0x00142EE4
	private Vector3 Inverse(Vector3 v)
	{
		return new Vector3(1f / v.x, 1f / v.y, 1f / v.z);
	}

	// Token: 0x06004A8C RID: 19084 RVA: 0x00144D20 File Offset: 0x00142F20
	private void SetupQualitySettings()
	{
		TOD_Resources resources = this.Components.Resources;
		Material material = null;
		Material material2 = null;
		switch (this.CloudQuality)
		{
		case TOD_Sky.CloudQualityType.Fastest:
			material = resources.CloudMaterialFastest;
			material2 = resources.ShadowMaterialFastest;
			break;
		case TOD_Sky.CloudQualityType.Density:
			material = resources.CloudMaterialDensity;
			material2 = resources.ShadowMaterialDensity;
			break;
		case TOD_Sky.CloudQualityType.Bumped:
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
		case TOD_Sky.MeshQualityType.Low:
			mesh = resources.IcosphereLow;
			mesh2 = resources.IcosphereLow;
			mesh3 = resources.IcosphereLow;
			mesh4 = resources.HalfIcosphereLow;
			mesh5 = resources.Quad;
			mesh6 = resources.SphereLow;
			break;
		case TOD_Sky.MeshQualityType.Medium:
			mesh = resources.IcosphereMedium;
			mesh2 = resources.IcosphereMedium;
			mesh3 = resources.IcosphereLow;
			mesh4 = resources.HalfIcosphereMedium;
			mesh5 = resources.Quad;
			mesh6 = resources.SphereMedium;
			break;
		case TOD_Sky.MeshQualityType.High:
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
			TOD_Components components = this.Components;
			Material material3 = resources.SpaceMaterial;
			this.Components.SpaceRenderer.sharedMaterial = material3;
			components.SpaceShader = material3;
		}
		if (!this.Components.AtmosphereShader || this.Components.AtmosphereShader.name != resources.AtmosphereMaterial.name)
		{
			TOD_Components components2 = this.Components;
			Material material3 = resources.AtmosphereMaterial;
			this.Components.AtmosphereRenderer.sharedMaterial = material3;
			components2.AtmosphereShader = material3;
		}
		if (!this.Components.ClearShader || this.Components.ClearShader.name != resources.ClearMaterial.name)
		{
			TOD_Components components3 = this.Components;
			Material material3 = resources.ClearMaterial;
			this.Components.ClearRenderer.sharedMaterial = material3;
			components3.ClearShader = material3;
		}
		if (!this.Components.CloudShader || this.Components.CloudShader.name != material.name)
		{
			TOD_Components components4 = this.Components;
			Material material3 = material;
			this.Components.CloudRenderer.sharedMaterial = material3;
			components4.CloudShader = material3;
		}
		if (!this.Components.ShadowShader || this.Components.ShadowShader.name != material2.name)
		{
			TOD_Components components5 = this.Components;
			Material material3 = material2;
			this.Components.ShadowProjector.material = material3;
			components5.ShadowShader = material3;
		}
		if (!this.Components.SunShader || this.Components.SunShader.name != resources.SunMaterial.name)
		{
			TOD_Components components6 = this.Components;
			Material material3 = resources.SunMaterial;
			this.Components.SunRenderer.sharedMaterial = material3;
			components6.SunShader = material3;
		}
		if (!this.Components.MoonShader || this.Components.MoonShader.name != resources.MoonMaterial.name)
		{
			TOD_Components components7 = this.Components;
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

	// Token: 0x06004A8D RID: 19085 RVA: 0x00145228 File Offset: 0x00143428
	protected void OnEnable()
	{
		this.Components = base.GetComponent<TOD_Components>();
		if (!this.Components)
		{
			Debug.LogError("TOD_Components not found. Disabling script.");
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06004A8E RID: 19086 RVA: 0x00145264 File Offset: 0x00143464
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

	// Token: 0x17000E8E RID: 3726
	// (get) Token: 0x06004A8F RID: 19087 RVA: 0x00145A00 File Offset: 0x00143C00
	// (set) Token: 0x06004A90 RID: 19088 RVA: 0x00145A08 File Offset: 0x00143C08
	internal TOD_Components Components { get; private set; }

	// Token: 0x17000E8F RID: 3727
	// (get) Token: 0x06004A91 RID: 19089 RVA: 0x00145A14 File Offset: 0x00143C14
	internal bool IsDay
	{
		get
		{
			return this.LerpValue > 0f;
		}
	}

	// Token: 0x17000E90 RID: 3728
	// (get) Token: 0x06004A92 RID: 19090 RVA: 0x00145A24 File Offset: 0x00143C24
	internal bool IsNight
	{
		get
		{
			return this.LerpValue == 0f;
		}
	}

	// Token: 0x17000E91 RID: 3729
	// (get) Token: 0x06004A93 RID: 19091 RVA: 0x00145A34 File Offset: 0x00143C34
	internal float Radius
	{
		get
		{
			return this.Components.DomeTransform.localScale.x;
		}
	}

	// Token: 0x17000E92 RID: 3730
	// (get) Token: 0x06004A94 RID: 19092 RVA: 0x00145A5C File Offset: 0x00143C5C
	internal float Gamma
	{
		get
		{
			return ((this.UnityColorSpace != TOD_Sky.ColorSpaceDetection.Auto || QualitySettings.activeColorSpace != 1) && this.UnityColorSpace != TOD_Sky.ColorSpaceDetection.Linear) ? 2.2f : 1f;
		}
	}

	// Token: 0x17000E93 RID: 3731
	// (get) Token: 0x06004A95 RID: 19093 RVA: 0x00145A90 File Offset: 0x00143C90
	internal float OneOverGamma
	{
		get
		{
			return ((this.UnityColorSpace != TOD_Sky.ColorSpaceDetection.Auto || QualitySettings.activeColorSpace != 1) && this.UnityColorSpace != TOD_Sky.ColorSpaceDetection.Linear) ? 0.454545438f : 1f;
		}
	}

	// Token: 0x17000E94 RID: 3732
	// (get) Token: 0x06004A96 RID: 19094 RVA: 0x00145AC4 File Offset: 0x00143CC4
	// (set) Token: 0x06004A97 RID: 19095 RVA: 0x00145ACC File Offset: 0x00143CCC
	internal float LerpValue { get; private set; }

	// Token: 0x17000E95 RID: 3733
	// (get) Token: 0x06004A98 RID: 19096 RVA: 0x00145AD8 File Offset: 0x00143CD8
	// (set) Token: 0x06004A99 RID: 19097 RVA: 0x00145AE0 File Offset: 0x00143CE0
	internal float SunZenith { get; private set; }

	// Token: 0x17000E96 RID: 3734
	// (get) Token: 0x06004A9A RID: 19098 RVA: 0x00145AEC File Offset: 0x00143CEC
	// (set) Token: 0x06004A9B RID: 19099 RVA: 0x00145AF4 File Offset: 0x00143CF4
	internal float MoonZenith { get; private set; }

	// Token: 0x17000E97 RID: 3735
	// (get) Token: 0x06004A9C RID: 19100 RVA: 0x00145B00 File Offset: 0x00143D00
	internal float LightZenith
	{
		get
		{
			return Mathf.Min(this.SunZenith, this.MoonZenith);
		}
	}

	// Token: 0x17000E98 RID: 3736
	// (get) Token: 0x06004A9D RID: 19101 RVA: 0x00145B14 File Offset: 0x00143D14
	internal float LightIntensity
	{
		get
		{
			return this.Components.LightSource.intensity;
		}
	}

	// Token: 0x17000E99 RID: 3737
	// (get) Token: 0x06004A9E RID: 19102 RVA: 0x00145B28 File Offset: 0x00143D28
	internal Vector3 MoonDirection
	{
		get
		{
			return this.Components.MoonTransform.forward;
		}
	}

	// Token: 0x17000E9A RID: 3738
	// (get) Token: 0x06004A9F RID: 19103 RVA: 0x00145B3C File Offset: 0x00143D3C
	internal Vector3 SunDirection
	{
		get
		{
			return this.Components.SunTransform.forward;
		}
	}

	// Token: 0x17000E9B RID: 3739
	// (get) Token: 0x06004AA0 RID: 19104 RVA: 0x00145B50 File Offset: 0x00143D50
	internal Vector3 LightDirection
	{
		get
		{
			return Vector3.Lerp(this.MoonDirection, this.SunDirection, this.LerpValue * this.LerpValue);
		}
	}

	// Token: 0x17000E9C RID: 3740
	// (get) Token: 0x06004AA1 RID: 19105 RVA: 0x00145B7C File Offset: 0x00143D7C
	internal Color LightColor
	{
		get
		{
			return this.Components.LightSource.color;
		}
	}

	// Token: 0x17000E9D RID: 3741
	// (get) Token: 0x06004AA2 RID: 19106 RVA: 0x00145B90 File Offset: 0x00143D90
	// (set) Token: 0x06004AA3 RID: 19107 RVA: 0x00145B98 File Offset: 0x00143D98
	internal Color SunShaftColor { get; private set; }

	// Token: 0x17000E9E RID: 3742
	// (get) Token: 0x06004AA4 RID: 19108 RVA: 0x00145BA4 File Offset: 0x00143DA4
	// (set) Token: 0x06004AA5 RID: 19109 RVA: 0x00145BAC File Offset: 0x00143DAC
	internal Color SunColor { get; private set; }

	// Token: 0x17000E9F RID: 3743
	// (get) Token: 0x06004AA6 RID: 19110 RVA: 0x00145BB8 File Offset: 0x00143DB8
	// (set) Token: 0x06004AA7 RID: 19111 RVA: 0x00145BC0 File Offset: 0x00143DC0
	internal Color MoonColor { get; private set; }

	// Token: 0x17000EA0 RID: 3744
	// (get) Token: 0x06004AA8 RID: 19112 RVA: 0x00145BCC File Offset: 0x00143DCC
	// (set) Token: 0x06004AA9 RID: 19113 RVA: 0x00145BD4 File Offset: 0x00143DD4
	internal Color MoonHaloColor { get; private set; }

	// Token: 0x17000EA1 RID: 3745
	// (get) Token: 0x06004AAA RID: 19114 RVA: 0x00145BE0 File Offset: 0x00143DE0
	// (set) Token: 0x06004AAB RID: 19115 RVA: 0x00145BE8 File Offset: 0x00143DE8
	internal Color CloudColor { get; private set; }

	// Token: 0x17000EA2 RID: 3746
	// (get) Token: 0x06004AAC RID: 19116 RVA: 0x00145BF4 File Offset: 0x00143DF4
	// (set) Token: 0x06004AAD RID: 19117 RVA: 0x00145BFC File Offset: 0x00143DFC
	internal Color AdditiveColor { get; private set; }

	// Token: 0x17000EA3 RID: 3747
	// (get) Token: 0x06004AAE RID: 19118 RVA: 0x00145C08 File Offset: 0x00143E08
	// (set) Token: 0x06004AAF RID: 19119 RVA: 0x00145C10 File Offset: 0x00143E10
	internal Color AmbientColor { get; private set; }

	// Token: 0x17000EA4 RID: 3748
	// (get) Token: 0x06004AB0 RID: 19120 RVA: 0x00145C1C File Offset: 0x00143E1C
	internal Color FogColor
	{
		get
		{
			return (!this.World.SetFogColor) ? this.SampleFogColor() : RenderSettings.fogColor;
		}
	}

	// Token: 0x04002B68 RID: 11112
	private const float pi = 3.14159274f;

	// Token: 0x04002B69 RID: 11113
	private const float pi2 = 9.869605f;

	// Token: 0x04002B6A RID: 11114
	private const float pi3 = 31.006279f;

	// Token: 0x04002B6B RID: 11115
	private const float pi4 = 97.4091f;

	// Token: 0x04002B6C RID: 11116
	private Vector2 opticalDepth;

	// Token: 0x04002B6D RID: 11117
	private Vector3 oneOverBeta;

	// Token: 0x04002B6E RID: 11118
	private Vector3 betaRayleigh;

	// Token: 0x04002B6F RID: 11119
	private Vector3 betaRayleighTheta;

	// Token: 0x04002B70 RID: 11120
	private Vector3 betaMie;

	// Token: 0x04002B71 RID: 11121
	private Vector3 betaMieTheta;

	// Token: 0x04002B72 RID: 11122
	private Vector2 betaMiePhase;

	// Token: 0x04002B73 RID: 11123
	private Vector3 betaNight;

	// Token: 0x04002B74 RID: 11124
	public TOD_Sky.ColorSpaceDetection UnityColorSpace;

	// Token: 0x04002B75 RID: 11125
	public TOD_Sky.CloudQualityType CloudQuality = TOD_Sky.CloudQualityType.Bumped;

	// Token: 0x04002B76 RID: 11126
	public TOD_Sky.MeshQualityType MeshQuality = TOD_Sky.MeshQualityType.High;

	// Token: 0x04002B77 RID: 11127
	public TOD_CycleParameters Cycle;

	// Token: 0x04002B78 RID: 11128
	public TOD_AtmosphereParameters Atmosphere;

	// Token: 0x04002B79 RID: 11129
	public TOD_DayParameters Day;

	// Token: 0x04002B7A RID: 11130
	public TOD_NightParameters Night;

	// Token: 0x04002B7B RID: 11131
	public TOD_LightParameters Light;

	// Token: 0x04002B7C RID: 11132
	public TOD_StarParameters Stars;

	// Token: 0x04002B7D RID: 11133
	public TOD_CloudParameters Clouds;

	// Token: 0x04002B7E RID: 11134
	public TOD_WorldParameters World;

	// Token: 0x02000835 RID: 2101
	public enum ColorSpaceDetection
	{
		// Token: 0x04002B8B RID: 11147
		Auto,
		// Token: 0x04002B8C RID: 11148
		Linear,
		// Token: 0x04002B8D RID: 11149
		Gamma
	}

	// Token: 0x02000836 RID: 2102
	public enum CloudQualityType
	{
		// Token: 0x04002B8F RID: 11151
		Fastest,
		// Token: 0x04002B90 RID: 11152
		Density,
		// Token: 0x04002B91 RID: 11153
		Bumped
	}

	// Token: 0x02000837 RID: 2103
	public enum MeshQualityType
	{
		// Token: 0x04002B93 RID: 11155
		Low,
		// Token: 0x04002B94 RID: 11156
		Medium,
		// Token: 0x04002B95 RID: 11157
		High
	}
}
