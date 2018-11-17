using System;
using UnityEngine;

// Token: 0x02000671 RID: 1649
public class SceneChildMeshes : MonoBehaviour
{
	// Token: 0x0600394B RID: 14667 RVA: 0x000D2858 File Offset: 0x000D0A58
	private static SceneChildMeshes GetMapSingleton(bool canCreate)
	{
		if (!SceneChildMeshes.lastFound)
		{
			Object[] array = Object.FindObjectsOfType(typeof(SceneChildMeshes));
			if (array.Length == 0)
			{
				if (canCreate)
				{
					GameObject gameObject = new GameObject("__Scene Child Meshes", new Type[]
					{
						typeof(SceneChildMeshes)
					})
					{
						hideFlags = 1
					};
					SceneChildMeshes.lastFound = gameObject.GetComponent<SceneChildMeshes>();
				}
			}
			else
			{
				SceneChildMeshes.lastFound = (SceneChildMeshes)array[0];
			}
		}
		return SceneChildMeshes.lastFound;
	}

	// Token: 0x04001D51 RID: 7505
	[SerializeField]
	private Mesh[] sceneMeshes;

	// Token: 0x04001D52 RID: 7506
	[SerializeField]
	private Mesh[] treeMeshes;

	// Token: 0x04001D53 RID: 7507
	private static SceneChildMeshes lastFound;
}
