using System;
using UnityEngine;

// Token: 0x020004F8 RID: 1272
public class ZombieSpawner : MonoBehaviour
{
	// Token: 0x06002B29 RID: 11049 RVA: 0x000ACD14 File Offset: 0x000AAF14
	private ZombieSpawner()
	{
		this.zombiePrefabs = new string[]
		{
			"npc_zombie"
		};
	}

	// Token: 0x06002B2A RID: 11050 RVA: 0x000ACD5C File Offset: 0x000AAF5C
	private void Awake()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06002B2B RID: 11051 RVA: 0x000ACD6C File Offset: 0x000AAF6C
	public void OnDrawGizmos()
	{
		Gizmos.color = new Color(0.3f, 0.3f, 0.3f, 0.5f);
		Gizmos.DrawWireSphere(base.transform.position, this.radius);
		Gizmos.color = Color.yellow;
		Gizmos.DrawCube(base.transform.position, Vector3.one * 0.5f);
	}

	// Token: 0x040017A7 RID: 6055
	public string[] zombiePrefabs;

	// Token: 0x040017A8 RID: 6056
	public int targetPopulation = 10;

	// Token: 0x040017A9 RID: 6057
	public float radius = 40f;

	// Token: 0x040017AA RID: 6058
	public float thinkDelay = 60f;

	// Token: 0x040017AB RID: 6059
	[NonSerialized]
	private int exaustCount;
}
