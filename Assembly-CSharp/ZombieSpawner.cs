using System;
using UnityEngine;

// Token: 0x020005B5 RID: 1461
public class ZombieSpawner : MonoBehaviour
{
	// Token: 0x06002EE9 RID: 12009 RVA: 0x000B4DB0 File Offset: 0x000B2FB0
	private ZombieSpawner()
	{
		this.zombiePrefabs = new string[]
		{
			"npc_zombie"
		};
	}

	// Token: 0x06002EEA RID: 12010 RVA: 0x000B4DF8 File Offset: 0x000B2FF8
	private void Awake()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06002EEB RID: 12011 RVA: 0x000B4E08 File Offset: 0x000B3008
	public void OnDrawGizmos()
	{
		Gizmos.color = new Color(0.3f, 0.3f, 0.3f, 0.5f);
		Gizmos.DrawWireSphere(base.transform.position, this.radius);
		Gizmos.color = Color.yellow;
		Gizmos.DrawCube(base.transform.position, Vector3.one * 0.5f);
	}

	// Token: 0x04001973 RID: 6515
	public string[] zombiePrefabs;

	// Token: 0x04001974 RID: 6516
	public int targetPopulation = 10;

	// Token: 0x04001975 RID: 6517
	public float radius = 40f;

	// Token: 0x04001976 RID: 6518
	public float thinkDelay = 60f;

	// Token: 0x04001977 RID: 6519
	[NonSerialized]
	private int exaustCount;
}
