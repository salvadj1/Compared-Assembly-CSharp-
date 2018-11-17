using System;
using System.Collections.Generic;
using Facepunch;
using UnityEngine;

// Token: 0x020000A8 RID: 168
public class SupplyDropZone : MonoBehaviour
{
	// Token: 0x0600039E RID: 926 RVA: 0x00011550 File Offset: 0x0000F750
	public void Awake()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x0600039F RID: 927 RVA: 0x00011560 File Offset: 0x0000F760
	public void OnDrawGizmos()
	{
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere(base.transform.position, this.radius);
	}

	// Token: 0x060003A0 RID: 928 RVA: 0x00011590 File Offset: 0x0000F790
	public void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(base.transform.position, this.radius);
	}

	// Token: 0x0400031A RID: 794
	public static List<global::SupplyDropZone> _dropZones;

	// Token: 0x0400031B RID: 795
	public float radius = 100f;
}
