using System;
using UnityEngine;

// Token: 0x020004A7 RID: 1191
public class SurfaceInfoTerrain : SurfaceInfo
{
	// Token: 0x060029F5 RID: 10741 RVA: 0x000A41E8 File Offset: 0x000A23E8
	public override SurfaceInfoObject SurfaceObj()
	{
		return this.surfaces[0];
	}

	// Token: 0x060029F6 RID: 10742 RVA: 0x000A41F4 File Offset: 0x000A23F4
	public override SurfaceInfoObject SurfaceObj(Vector3 worldPos)
	{
		int textureIndex = TerrainTextureHelper.GetTextureIndex(worldPos);
		if (textureIndex >= this.surfaces.Length)
		{
			Debug.Log("Missing surface info for splat index " + textureIndex);
			return this.surfaces[0];
		}
		return this.surfaces[textureIndex];
	}

	// Token: 0x040015DA RID: 5594
	public SurfaceInfoObject[] surfaces;
}
