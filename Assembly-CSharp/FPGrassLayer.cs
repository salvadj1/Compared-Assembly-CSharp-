using System;
using UnityEngine;

// Token: 0x0200003F RID: 63
[ExecuteInEditMode]
public class FPGrassLayer : MonoBehaviour
{
	// Token: 0x06000238 RID: 568 RVA: 0x0000C7E0 File Offset: 0x0000A9E0
	private void OnEnable()
	{
		this._enabled = true;
	}

	// Token: 0x06000239 RID: 569 RVA: 0x0000C7EC File Offset: 0x0000A9EC
	private void OnDisable()
	{
		this._enabled = false;
	}

	// Token: 0x0600023A RID: 570 RVA: 0x0000C7F8 File Offset: 0x0000A9F8
	private void UpdateDisplacement(bool on)
	{
		if (!on)
		{
			Shader.SetGlobalVector("_DisplacementWorldMin", Vector2.zero);
			Shader.SetGlobalVector("_DisplacementWorldMax", Vector2.zero);
			return;
		}
		FPGrassDisplacementCamera fpgrassDisplacementCamera = FPGrassDisplacementCamera.Get();
		Camera camera = (!(fpgrassDisplacementCamera != null)) ? null : fpgrassDisplacementCamera.camera;
		if (camera == null)
		{
			return;
		}
		float orthographicSize = camera.orthographicSize;
		float num = orthographicSize / (float)camera.targetTexture.width;
		Vector3 position = base.camera.transform.position;
		bool flag = TransformHelpers.Dist2D(position, camera.transform.position) > 5f;
		if (flag)
		{
			Vector3 vector;
			vector.x = Mathf.Round(position.x / num) * num;
			vector.y = Mathf.Round(position.y / num) * num;
			vector.z = Mathf.Round(position.z / num) * num;
			camera.transform.position = vector + new Vector3(0f, 50f, 0f);
		}
		Vector3 position2 = camera.transform.position;
		Vector2 vector2;
		vector2.x = position2.x - orthographicSize;
		vector2.y = position2.z - orthographicSize;
		Vector2 vector3;
		vector3.x = position2.x + orthographicSize;
		vector3.y = position2.z + orthographicSize;
		Shader.SetGlobalVector("_DisplacementWorldMin", vector2);
		Shader.SetGlobalVector("_DisplacementWorldMax", vector3);
		camera.Render();
	}

	// Token: 0x0600023B RID: 571 RVA: 0x0000C98C File Offset: 0x0000AB8C
	private void OnPreCull()
	{
		if (!Terrain.activeTerrain || !Terrain.activeTerrain.terrainData)
		{
			return;
		}
		if (this._enabled && grass.on && FPGrass.anyEnabled)
		{
			Terrain activeTerrain = Terrain.activeTerrain;
			this.UpdateDisplacement(grass.displacement);
			if (activeTerrain)
			{
				Camera camera = base.camera;
				this._frustum = GeometryUtility.CalculateFrustumPlanes(camera);
				FPGrass.RenderArguments renderArguments;
				renderArguments.frustum = this._frustum;
				renderArguments.camera = camera;
				renderArguments.immediate = false;
				renderArguments.terrain = activeTerrain;
				renderArguments.center = camera.transform.position;
				FPGrass.DrawAllGrass(ref renderArguments);
			}
		}
	}

	// Token: 0x0400016B RID: 363
	[NonSerialized]
	private bool _enabled;

	// Token: 0x0400016C RID: 364
	[NonSerialized]
	private Plane[] _frustum;
}
