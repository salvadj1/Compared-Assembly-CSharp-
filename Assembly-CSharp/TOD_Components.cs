using System;
using UnityEngine;

// Token: 0x0200091E RID: 2334
[ExecuteInEditMode]
public class TOD_Components : MonoBehaviour
{
	// Token: 0x06004F1A RID: 20250 RVA: 0x0014C59C File Offset: 0x0014A79C
	protected void OnEnable()
	{
		this.DomeTransform = base.transform;
		if (Camera.main != null)
		{
			this.CameraTransform = Camera.main.transform;
		}
		else
		{
			Debug.LogWarning("Main camera does not exist or is not tagged 'MainCamera'.");
		}
		this.Sky = base.GetComponent<global::TOD_Sky>();
		this.Animation = base.GetComponent<global::TOD_Animation>();
		this.Time = base.GetComponent<global::TOD_Time>();
		this.Weather = base.GetComponent<global::TOD_Weather>();
		this.Resources = base.GetComponent<global::TOD_Resources>();
		if (!this.Space)
		{
			Debug.LogError("Space reference not set. Disabling TOD_Sky script.");
			this.Sky.enabled = false;
			return;
		}
		this.SpaceRenderer = this.Space.renderer;
		this.SpaceShader = this.SpaceRenderer.sharedMaterial;
		this.SpaceMeshFilter = this.Space.GetComponent<MeshFilter>();
		if (!this.Atmosphere)
		{
			Debug.LogError("Atmosphere reference not set. Disabling TOD_Sky script.");
			this.Sky.enabled = false;
			return;
		}
		this.AtmosphereRenderer = this.Atmosphere.renderer;
		this.AtmosphereShader = this.AtmosphereRenderer.sharedMaterial;
		this.AtmosphereMeshFilter = this.Atmosphere.GetComponent<MeshFilter>();
		if (!this.Clear)
		{
			Debug.LogError("Clear reference not set. Disabling TOD_Sky script.");
			this.Sky.enabled = false;
			return;
		}
		this.ClearRenderer = this.Clear.renderer;
		this.ClearShader = this.ClearRenderer.sharedMaterial;
		this.ClearMeshFilter = this.Clear.GetComponent<MeshFilter>();
		if (!this.Clouds)
		{
			Debug.LogError("Clouds reference not set. Disabling TOD_Sky script.");
			this.Sky.enabled = false;
			return;
		}
		this.CloudRenderer = this.Clouds.renderer;
		this.CloudShader = this.CloudRenderer.sharedMaterial;
		this.CloudMeshFilter = this.Clouds.GetComponent<MeshFilter>();
		if (!this.Projector)
		{
			Debug.LogError("Projector reference not set. Disabling TOD_Sky script.");
			this.Sky.enabled = false;
			return;
		}
		this.ShadowProjector = this.Projector.GetComponent<Projector>();
		this.ShadowShader = this.ShadowProjector.material;
		if (!this.Light)
		{
			Debug.LogError("Light reference not set. Disabling TOD_Sky script.");
			this.Sky.enabled = false;
			return;
		}
		this.LightTransform = this.Light.transform;
		this.LightSource = this.Light.light;
		if (!this.Sun)
		{
			Debug.LogError("Sun reference not set. Disabling TOD_Sky script.");
			this.Sky.enabled = false;
			return;
		}
		this.SunTransform = this.Sun.transform;
		this.SunRenderer = this.Sun.renderer;
		this.SunShader = this.SunRenderer.sharedMaterial;
		this.SunMeshFilter = this.Sun.GetComponent<MeshFilter>();
		if (this.Moon)
		{
			this.MoonTransform = this.Moon.transform;
			this.MoonRenderer = this.Moon.renderer;
			this.MoonShader = this.MoonRenderer.sharedMaterial;
			this.MoonMeshFilter = this.Moon.GetComponent<MeshFilter>();
			return;
		}
		Debug.LogError("Moon reference not set. Disabling TOD_Sky script.");
		this.Sky.enabled = false;
	}

	// Token: 0x04002D41 RID: 11585
	public GameObject Sun;

	// Token: 0x04002D42 RID: 11586
	public GameObject Moon;

	// Token: 0x04002D43 RID: 11587
	public GameObject Atmosphere;

	// Token: 0x04002D44 RID: 11588
	public GameObject Clear;

	// Token: 0x04002D45 RID: 11589
	public GameObject Clouds;

	// Token: 0x04002D46 RID: 11590
	public GameObject Space;

	// Token: 0x04002D47 RID: 11591
	public GameObject Light;

	// Token: 0x04002D48 RID: 11592
	public GameObject Projector;

	// Token: 0x04002D49 RID: 11593
	internal Transform DomeTransform;

	// Token: 0x04002D4A RID: 11594
	internal Transform SunTransform;

	// Token: 0x04002D4B RID: 11595
	internal Transform MoonTransform;

	// Token: 0x04002D4C RID: 11596
	internal Transform CameraTransform;

	// Token: 0x04002D4D RID: 11597
	internal Transform LightTransform;

	// Token: 0x04002D4E RID: 11598
	internal Renderer SpaceRenderer;

	// Token: 0x04002D4F RID: 11599
	internal Renderer AtmosphereRenderer;

	// Token: 0x04002D50 RID: 11600
	internal Renderer ClearRenderer;

	// Token: 0x04002D51 RID: 11601
	internal Renderer CloudRenderer;

	// Token: 0x04002D52 RID: 11602
	internal Renderer SunRenderer;

	// Token: 0x04002D53 RID: 11603
	internal Renderer MoonRenderer;

	// Token: 0x04002D54 RID: 11604
	internal MeshFilter SpaceMeshFilter;

	// Token: 0x04002D55 RID: 11605
	internal MeshFilter AtmosphereMeshFilter;

	// Token: 0x04002D56 RID: 11606
	internal MeshFilter ClearMeshFilter;

	// Token: 0x04002D57 RID: 11607
	internal MeshFilter CloudMeshFilter;

	// Token: 0x04002D58 RID: 11608
	internal MeshFilter SunMeshFilter;

	// Token: 0x04002D59 RID: 11609
	internal MeshFilter MoonMeshFilter;

	// Token: 0x04002D5A RID: 11610
	internal Material SpaceShader;

	// Token: 0x04002D5B RID: 11611
	internal Material AtmosphereShader;

	// Token: 0x04002D5C RID: 11612
	internal Material ClearShader;

	// Token: 0x04002D5D RID: 11613
	internal Material CloudShader;

	// Token: 0x04002D5E RID: 11614
	internal Material SunShader;

	// Token: 0x04002D5F RID: 11615
	internal Material MoonShader;

	// Token: 0x04002D60 RID: 11616
	internal Material ShadowShader;

	// Token: 0x04002D61 RID: 11617
	internal Light LightSource;

	// Token: 0x04002D62 RID: 11618
	internal Projector ShadowProjector;

	// Token: 0x04002D63 RID: 11619
	internal global::TOD_Sky Sky;

	// Token: 0x04002D64 RID: 11620
	internal global::TOD_Animation Animation;

	// Token: 0x04002D65 RID: 11621
	internal global::TOD_Time Time;

	// Token: 0x04002D66 RID: 11622
	internal global::TOD_Weather Weather;

	// Token: 0x04002D67 RID: 11623
	internal global::TOD_Resources Resources;

	// Token: 0x04002D68 RID: 11624
	internal TOD_SunShafts SunShafts;
}
