using System;
using UnityEngine;

// Token: 0x02000497 RID: 1175
public class DisplayRenderBounds : MonoBehaviour
{
	// Token: 0x060029BE RID: 10686 RVA: 0x000A37A0 File Offset: 0x000A19A0
	private void OnDrawGizmos()
	{
		Renderer renderer = base.renderer;
		if (renderer)
		{
			Bounds bounds = renderer.bounds;
			Gizmos.color = Color.green;
			Gizmos.DrawWireCube(bounds.center, bounds.size);
			if (renderer is SkinnedMeshRenderer)
			{
				SkinnedMeshRenderer skinnedMeshRenderer = renderer as SkinnedMeshRenderer;
				Gizmos.color = Color.yellow;
				Gizmos.matrix = skinnedMeshRenderer.localToWorldMatrix;
				bounds = skinnedMeshRenderer.localBounds;
				Gizmos.DrawWireCube(bounds.center, bounds.size);
			}
			else
			{
				MeshFilter component = base.GetComponent<MeshFilter>();
				if (component)
				{
					Mesh sharedMesh = component.sharedMesh;
					if (sharedMesh)
					{
						Gizmos.color = Color.magenta;
						Gizmos.matrix = base.transform.localToWorldMatrix;
						bounds = sharedMesh.bounds;
						Gizmos.DrawWireCube(bounds.center, bounds.size);
					}
				}
			}
		}
	}
}
