using System;
using System.Collections.Generic;
using Facepunch;
using UnityEngine;

// Token: 0x02000095 RID: 149
public class SupplyDropZone : MonoBehaviour
{
	// Token: 0x06000326 RID: 806 RVA: 0x0000FD60 File Offset: 0x0000DF60
	public void Awake()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06000327 RID: 807 RVA: 0x0000FD70 File Offset: 0x0000DF70
	public void OnDrawGizmos()
	{
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere(base.transform.position, this.radius);
	}

	// Token: 0x06000328 RID: 808 RVA: 0x0000FDA0 File Offset: 0x0000DFA0
	public void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(base.transform.position, this.radius);
	}

	// Token: 0x040002AF RID: 687
	public static List<SupplyDropZone> _dropZones;

	// Token: 0x040002B0 RID: 688
	public float radius = 100f;
}
