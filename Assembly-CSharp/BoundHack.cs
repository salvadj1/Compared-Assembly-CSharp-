using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004FB RID: 1275
[RequireComponent(typeof(SkinnedMeshRenderer))]
public class BoundHack : MonoBehaviour
{
	// Token: 0x06002B97 RID: 11159 RVA: 0x000A21EC File Offset: 0x000A03EC
	private void Awake()
	{
		this.renderer = (base.renderer as SkinnedMeshRenderer);
		if (global::BoundHack.renders == null)
		{
			global::BoundHack.renders = new HashSet<global::BoundHack>();
		}
		global::BoundHack.renders.Add(this);
	}

	// Token: 0x06002B98 RID: 11160 RVA: 0x000A2220 File Offset: 0x000A0420
	private void OnDestroy()
	{
		if (global::BoundHack.renders != null)
		{
			global::BoundHack.renders.Remove(this);
		}
	}

	// Token: 0x06002B99 RID: 11161 RVA: 0x000A2238 File Offset: 0x000A0438
	public static void Achieve(Vector3 centroid)
	{
		if (global::BoundHack.renders != null)
		{
			foreach (global::BoundHack boundHack in global::BoundHack.renders)
			{
				boundHack.renderer.localBounds = new Bounds(((!boundHack.rootbone) ? boundHack.transform : boundHack.rootbone).InverseTransformPoint(centroid), new Vector3(100f, 100f, 100f));
			}
		}
	}

	// Token: 0x06002B9A RID: 11162 RVA: 0x000A22EC File Offset: 0x000A04EC
	private void OnDrawGizmosSelected()
	{
		if (base.renderer)
		{
			Gizmos.DrawWireCube(base.renderer.bounds.center, base.renderer.bounds.size);
		}
		if (this.rootbone && this.renderer)
		{
			Gizmos.color = new Color(0.8f, 0.8f, 1f, 0.1f);
			Gizmos.matrix = this.rootbone.localToWorldMatrix;
			Gizmos.DrawCube(this.renderer.localBounds.center, this.renderer.localBounds.size);
		}
	}

	// Token: 0x0400156F RID: 5487
	private static HashSet<global::BoundHack> renders;

	// Token: 0x04001570 RID: 5488
	private SkinnedMeshRenderer renderer;

	// Token: 0x04001571 RID: 5489
	public Transform rootbone;
}
