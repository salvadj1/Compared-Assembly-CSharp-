using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000445 RID: 1093
[RequireComponent(typeof(SkinnedMeshRenderer))]
public class BoundHack : MonoBehaviour
{
	// Token: 0x06002807 RID: 10247 RVA: 0x0009C26C File Offset: 0x0009A46C
	private void Awake()
	{
		this.renderer = (base.renderer as SkinnedMeshRenderer);
		if (BoundHack.renders == null)
		{
			BoundHack.renders = new HashSet<BoundHack>();
		}
		BoundHack.renders.Add(this);
	}

	// Token: 0x06002808 RID: 10248 RVA: 0x0009C2A0 File Offset: 0x0009A4A0
	private void OnDestroy()
	{
		if (BoundHack.renders != null)
		{
			BoundHack.renders.Remove(this);
		}
	}

	// Token: 0x06002809 RID: 10249 RVA: 0x0009C2B8 File Offset: 0x0009A4B8
	public static void Achieve(Vector3 centroid)
	{
		if (BoundHack.renders != null)
		{
			foreach (BoundHack boundHack in BoundHack.renders)
			{
				boundHack.renderer.localBounds = new Bounds(((!boundHack.rootbone) ? boundHack.transform : boundHack.rootbone).InverseTransformPoint(centroid), new Vector3(100f, 100f, 100f));
			}
		}
	}

	// Token: 0x0600280A RID: 10250 RVA: 0x0009C36C File Offset: 0x0009A56C
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

	// Token: 0x040013EC RID: 5100
	private static HashSet<BoundHack> renders;

	// Token: 0x040013ED RID: 5101
	private SkinnedMeshRenderer renderer;

	// Token: 0x040013EE RID: 5102
	public Transform rootbone;
}
