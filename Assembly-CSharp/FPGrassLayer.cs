using System;
using UnityEngine;

// Token: 0x02000051 RID: 81
[ExecuteInEditMode]
public class FPGrassLayer : MonoBehaviour
{
	// Token: 0x060002AA RID: 682 RVA: 0x0000DD88 File Offset: 0x0000BF88
	private void OnEnable()
	{
		this._enabled = true;
	}

	// Token: 0x060002AB RID: 683 RVA: 0x0000DD94 File Offset: 0x0000BF94
	private void OnDisable()
	{
		this._enabled = false;
	}

	// Token: 0x060002AC RID: 684 RVA: 0x0000DDA0 File Offset: 0x0000BFA0
	private void UpdateDisplacement(bool on)
	{
		if (!on)
		{
			Shader.SetGlobalVector("_DisplacementWorldMin", Vector2.zero);
			Shader.SetGlobalVector("_DisplacementWorldMax", Vector2.zero);
			return;
		}
		global::FPGrassDisplacementCamera fpgrassDisplacementCamera = global::FPGrassDisplacementCamera.Get();
		Camera camera = (!(fpgrassDisplacementCamera != null)) ? null : fpgrassDisplacementCamera.camera;
		if (camera == null)
		{
			return;
		}
		float orthographicSize = camera.orthographicSize;
		float num = orthographicSize / (float)camera.targetTexture.width;
		Vector3 position = base.camera.transform.position;
		bool flag = global::TransformHelpers.Dist2D(position, camera.transform.position) > 5f;
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

	// Token: 0x060002AD RID: 685 RVA: 0x0000DF34 File Offset: 0x0000C134
	private void OnPreCull()
	{
		if (!Terrain.activeTerrain || !Terrain.activeTerrain.terrainData)
		{
			return;
		}
		if (this._enabled && global::grass.on && global::FPGrass.anyEnabled)
		{
			Terrain activeTerrain = Terrain.activeTerrain;
			this.UpdateDisplacement(global::grass.displacement);
			if (activeTerrain)
			{
				Camera camera = base.camera;
				this._frustum = GeometryUtility.CalculateFrustumPlanes(camera);
				global::FPGrass.RenderArguments renderArguments;
				renderArguments.frustum = this._frustum;
				renderArguments.camera = camera;
				renderArguments.immediate = false;
				renderArguments.terrain = activeTerrain;
				renderArguments.center = camera.transform.position;
				global::FPGrass.DrawAllGrass(ref renderArguments);
			}
		}
	}

	// Token: 0x040001CD RID: 461
	[NonSerialized]
	private bool _enabled;

	// Token: 0x040001CE RID: 462
	[NonSerialized]
	private Plane[] _frustum;
}
