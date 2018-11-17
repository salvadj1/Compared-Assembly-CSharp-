using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000052 RID: 82
public class FPGrassLevel : MonoBehaviour, global::IFPGrassAsset
{
	// Token: 0x060002AF RID: 687 RVA: 0x0000E008 File Offset: 0x0000C208
	public void UpdateLevel(Vector3 position, Terrain terrain)
	{
		int num = Mathf.FloorToInt(position.x / this.gridSpacingAtLevel);
		int num2 = Mathf.FloorToInt(position.z / this.gridSpacingAtLevel);
		Vector3 zero = Vector3.zero;
		zero.x = (float)num * this.gridSpacingAtLevel;
		zero.z = (float)num2 * this.gridSpacingAtLevel;
		if (zero != this.lastPosition && !this.probabilityUpdateQueued)
		{
			if (Application.isPlaying)
			{
				this.probabilityUpdateQueued = true;
			}
			else
			{
				this.UpdateMapsNow(zero);
			}
		}
		this.lastPosition = zero;
	}

	// Token: 0x060002B0 RID: 688 RVA: 0x0000E0A4 File Offset: 0x0000C2A4
	private void UpdateMapsNow(Vector3 gridPosition)
	{
		Terrain activeTerrain = Terrain.activeTerrain;
		if (activeTerrain)
		{
			this.probabilityGenerator.UpdateMap(activeTerrain.transform.InverseTransformPoint(gridPosition));
			this.levelMaterial.SetTexture("_TextureIndexTex", this.probabilityGenerator.probabilityTexture);
			this.levelMaterial.SetVector("_TerrainPosition", activeTerrain.transform.position);
		}
	}

	// Token: 0x060002B1 RID: 689 RVA: 0x0000E114 File Offset: 0x0000C314
	private void OnDestroy()
	{
		this.probabilityGenerator.DestroyObjects();
	}

	// Token: 0x060002B2 RID: 690 RVA: 0x0000E124 File Offset: 0x0000C324
	internal void Draw(global::FPGrassPatch patch, Mesh mesh, ref Vector3 renderPosition, ref global::FPGrass.RenderArguments renderArgs)
	{
		if (this.probabilityUpdateQueued || global::grass.forceredraw)
		{
			this.UpdateMapsNow(this.lastPosition);
			this.probabilityUpdateQueued = false;
		}
		if (global::grass.displacement)
		{
			Graphics.Blit(global::FPGrassDisplacementCamera.GetRT(), this.probabilityGenerator.probabilityTexture, global::FPGrassDisplacementCamera.GetBlitMat());
		}
		if (renderArgs.immediate)
		{
			GL.PushMatrix();
			this.levelMaterial.SetPass(0);
			Graphics.DrawMeshNow(mesh, renderPosition, global::FPGrassLevel.Constant.rotation, 0);
			GL.PopMatrix();
		}
		else
		{
			Graphics.DrawMesh(mesh, renderPosition, global::FPGrassLevel.Constant.rotation, this.levelMaterial, base.gameObject.layer, base.camera, 0, null, global::FPGrass.castShadows, global::FPGrass.receiveShadows);
		}
	}

	// Token: 0x060002B3 RID: 691 RVA: 0x0000E1EC File Offset: 0x0000C3EC
	internal void Render(ref global::FPGrass.RenderArguments renderArgs)
	{
		global::FPGrass.RenderArguments renderArguments = renderArgs;
		renderArguments.center = this.lastPosition;
		foreach (global::FPGrassPatch fpgrassPatch in this.children)
		{
			if (fpgrassPatch.enabled)
			{
				fpgrassPatch.Render(ref renderArguments);
			}
		}
	}

	// Token: 0x040001CF RID: 463
	public int levelNumber;

	// Token: 0x040001D0 RID: 464
	public Material levelMaterial;

	// Token: 0x040001D1 RID: 465
	public global::FPGrass parent;

	// Token: 0x040001D2 RID: 466
	[SerializeField]
	private List<global::FPGrassPatch> children = new List<global::FPGrassPatch>();

	// Token: 0x040001D3 RID: 467
	[SerializeField]
	private float gridSpacingAtLevel;

	// Token: 0x040001D4 RID: 468
	[SerializeField]
	private float levelSize;

	// Token: 0x040001D5 RID: 469
	[SerializeField]
	private int gridSize;

	// Token: 0x040001D6 RID: 470
	[SerializeField]
	private int gridSizeAtLevel;

	// Token: 0x040001D7 RID: 471
	private Vector3 lastPosition;

	// Token: 0x040001D8 RID: 472
	public global::FPGrassProbabilityGenerator probabilityGenerator;

	// Token: 0x040001D9 RID: 473
	[NonSerialized]
	private bool probabilityUpdateQueued;

	// Token: 0x02000053 RID: 83
	private static class Constant
	{
		// Token: 0x040001DA RID: 474
		public static readonly Quaternion rotation = Quaternion.identity;
	}
}
