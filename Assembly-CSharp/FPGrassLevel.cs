using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000040 RID: 64
public class FPGrassLevel : MonoBehaviour, IFPGrassAsset
{
	// Token: 0x0600023D RID: 573 RVA: 0x0000CA60 File Offset: 0x0000AC60
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

	// Token: 0x0600023E RID: 574 RVA: 0x0000CAFC File Offset: 0x0000ACFC
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

	// Token: 0x0600023F RID: 575 RVA: 0x0000CB6C File Offset: 0x0000AD6C
	private void OnDestroy()
	{
		this.probabilityGenerator.DestroyObjects();
	}

	// Token: 0x06000240 RID: 576 RVA: 0x0000CB7C File Offset: 0x0000AD7C
	internal void Draw(FPGrassPatch patch, Mesh mesh, ref Vector3 renderPosition, ref FPGrass.RenderArguments renderArgs)
	{
		if (this.probabilityUpdateQueued || grass.forceredraw)
		{
			this.UpdateMapsNow(this.lastPosition);
			this.probabilityUpdateQueued = false;
		}
		if (grass.displacement)
		{
			Graphics.Blit(FPGrassDisplacementCamera.GetRT(), this.probabilityGenerator.probabilityTexture, FPGrassDisplacementCamera.GetBlitMat());
		}
		if (renderArgs.immediate)
		{
			GL.PushMatrix();
			this.levelMaterial.SetPass(0);
			Graphics.DrawMeshNow(mesh, renderPosition, FPGrassLevel.Constant.rotation, 0);
			GL.PopMatrix();
		}
		else
		{
			Graphics.DrawMesh(mesh, renderPosition, FPGrassLevel.Constant.rotation, this.levelMaterial, base.gameObject.layer, base.camera, 0, null, FPGrass.castShadows, FPGrass.receiveShadows);
		}
	}

	// Token: 0x06000241 RID: 577 RVA: 0x0000CC44 File Offset: 0x0000AE44
	internal void Render(ref FPGrass.RenderArguments renderArgs)
	{
		FPGrass.RenderArguments renderArguments = renderArgs;
		renderArguments.center = this.lastPosition;
		foreach (FPGrassPatch fpgrassPatch in this.children)
		{
			if (fpgrassPatch.enabled)
			{
				fpgrassPatch.Render(ref renderArguments);
			}
		}
	}

	// Token: 0x0400016D RID: 365
	public int levelNumber;

	// Token: 0x0400016E RID: 366
	public Material levelMaterial;

	// Token: 0x0400016F RID: 367
	public FPGrass parent;

	// Token: 0x04000170 RID: 368
	[SerializeField]
	private List<FPGrassPatch> children = new List<FPGrassPatch>();

	// Token: 0x04000171 RID: 369
	[SerializeField]
	private float gridSpacingAtLevel;

	// Token: 0x04000172 RID: 370
	[SerializeField]
	private float levelSize;

	// Token: 0x04000173 RID: 371
	[SerializeField]
	private int gridSize;

	// Token: 0x04000174 RID: 372
	[SerializeField]
	private int gridSizeAtLevel;

	// Token: 0x04000175 RID: 373
	private Vector3 lastPosition;

	// Token: 0x04000176 RID: 374
	public FPGrassProbabilityGenerator probabilityGenerator;

	// Token: 0x04000177 RID: 375
	[NonSerialized]
	private bool probabilityUpdateQueued;

	// Token: 0x02000041 RID: 65
	private static class Constant
	{
		// Token: 0x04000178 RID: 376
		public static readonly Quaternion rotation = Quaternion.identity;
	}
}
