using System;
using UnityEngine;

// Token: 0x02000562 RID: 1378
public class SurfaceInfoTerrain : global::SurfaceInfo
{
	// Token: 0x06002DA7 RID: 11687 RVA: 0x000ABF80 File Offset: 0x000AA180
	public override global::SurfaceInfoObject SurfaceObj()
	{
		return this.surfaces[0];
	}

	// Token: 0x06002DA8 RID: 11688 RVA: 0x000ABF8C File Offset: 0x000AA18C
	public override global::SurfaceInfoObject SurfaceObj(Vector3 worldPos)
	{
		int textureIndex = global::TerrainTextureHelper.GetTextureIndex(worldPos);
		if (textureIndex >= this.surfaces.Length)
		{
			Debug.Log("Missing surface info for splat index " + textureIndex);
			return this.surfaces[0];
		}
		return this.surfaces[textureIndex];
	}

	// Token: 0x04001797 RID: 6039
	public global::SurfaceInfoObject[] surfaces;
}
