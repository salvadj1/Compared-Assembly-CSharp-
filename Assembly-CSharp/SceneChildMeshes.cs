using System;
using UnityEngine;

// Token: 0x02000735 RID: 1845
public class SceneChildMeshes : MonoBehaviour
{
	// Token: 0x06003D3F RID: 15679 RVA: 0x000DB238 File Offset: 0x000D9438
	private static global::SceneChildMeshes GetMapSingleton(bool canCreate)
	{
		if (!global::SceneChildMeshes.lastFound)
		{
			Object[] array = Object.FindObjectsOfType(typeof(global::SceneChildMeshes));
			if (array.Length == 0)
			{
				if (canCreate)
				{
					GameObject gameObject = new GameObject("__Scene Child Meshes", new Type[]
					{
						typeof(global::SceneChildMeshes)
					})
					{
						hideFlags = 1
					};
					global::SceneChildMeshes.lastFound = gameObject.GetComponent<global::SceneChildMeshes>();
				}
			}
			else
			{
				global::SceneChildMeshes.lastFound = (global::SceneChildMeshes)array[0];
			}
		}
		return global::SceneChildMeshes.lastFound;
	}

	// Token: 0x04001F49 RID: 8009
	[SerializeField]
	private Mesh[] sceneMeshes;

	// Token: 0x04001F4A RID: 8010
	[SerializeField]
	private Mesh[] treeMeshes;

	// Token: 0x04001F4B RID: 8011
	private static global::SceneChildMeshes lastFound;
}
