using System;
using UnityEngine;

// Token: 0x02000829 RID: 2089
[ExecuteInEditMode]
public class TOD_Components : MonoBehaviour
{
	// Token: 0x06004A5F RID: 19039 RVA: 0x00142638 File Offset: 0x00140838
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
		this.Sky = base.GetComponent<TOD_Sky>();
		this.Animation = base.GetComponent<TOD_Animation>();
		this.Time = base.GetComponent<TOD_Time>();
		this.Weather = base.GetComponent<TOD_Weather>();
		this.Resources = base.GetComponent<TOD_Resources>();
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

	// Token: 0x04002AF3 RID: 10995
	public GameObject Sun;

	// Token: 0x04002AF4 RID: 10996
	public GameObject Moon;

	// Token: 0x04002AF5 RID: 10997
	public GameObject Atmosphere;

	// Token: 0x04002AF6 RID: 10998
	public GameObject Clear;

	// Token: 0x04002AF7 RID: 10999
	public GameObject Clouds;

	// Token: 0x04002AF8 RID: 11000
	public GameObject Space;

	// Token: 0x04002AF9 RID: 11001
	public GameObject Light;

	// Token: 0x04002AFA RID: 11002
	public GameObject Projector;

	// Token: 0x04002AFB RID: 11003
	internal Transform DomeTransform;

	// Token: 0x04002AFC RID: 11004
	internal Transform SunTransform;

	// Token: 0x04002AFD RID: 11005
	internal Transform MoonTransform;

	// Token: 0x04002AFE RID: 11006
	internal Transform CameraTransform;

	// Token: 0x04002AFF RID: 11007
	internal Transform LightTransform;

	// Token: 0x04002B00 RID: 11008
	internal Renderer SpaceRenderer;

	// Token: 0x04002B01 RID: 11009
	internal Renderer AtmosphereRenderer;

	// Token: 0x04002B02 RID: 11010
	internal Renderer ClearRenderer;

	// Token: 0x04002B03 RID: 11011
	internal Renderer CloudRenderer;

	// Token: 0x04002B04 RID: 11012
	internal Renderer SunRenderer;

	// Token: 0x04002B05 RID: 11013
	internal Renderer MoonRenderer;

	// Token: 0x04002B06 RID: 11014
	internal MeshFilter SpaceMeshFilter;

	// Token: 0x04002B07 RID: 11015
	internal MeshFilter AtmosphereMeshFilter;

	// Token: 0x04002B08 RID: 11016
	internal MeshFilter ClearMeshFilter;

	// Token: 0x04002B09 RID: 11017
	internal MeshFilter CloudMeshFilter;

	// Token: 0x04002B0A RID: 11018
	internal MeshFilter SunMeshFilter;

	// Token: 0x04002B0B RID: 11019
	internal MeshFilter MoonMeshFilter;

	// Token: 0x04002B0C RID: 11020
	internal Material SpaceShader;

	// Token: 0x04002B0D RID: 11021
	internal Material AtmosphereShader;

	// Token: 0x04002B0E RID: 11022
	internal Material ClearShader;

	// Token: 0x04002B0F RID: 11023
	internal Material CloudShader;

	// Token: 0x04002B10 RID: 11024
	internal Material SunShader;

	// Token: 0x04002B11 RID: 11025
	internal Material MoonShader;

	// Token: 0x04002B12 RID: 11026
	internal Material ShadowShader;

	// Token: 0x04002B13 RID: 11027
	internal Light LightSource;

	// Token: 0x04002B14 RID: 11028
	internal Projector ShadowProjector;

	// Token: 0x04002B15 RID: 11029
	internal TOD_Sky Sky;

	// Token: 0x04002B16 RID: 11030
	internal TOD_Animation Animation;

	// Token: 0x04002B17 RID: 11031
	internal TOD_Time Time;

	// Token: 0x04002B18 RID: 11032
	internal TOD_Weather Weather;

	// Token: 0x04002B19 RID: 11033
	internal TOD_Resources Resources;

	// Token: 0x04002B1A RID: 11034
	internal TOD_SunShafts SunShafts;
}
