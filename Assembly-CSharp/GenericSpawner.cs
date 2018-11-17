using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000596 RID: 1430
public class GenericSpawner : MonoBehaviour
{
	// Token: 0x06002E76 RID: 11894 RVA: 0x000B2A14 File Offset: 0x000B0C14
	public void Awake()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06002E77 RID: 11895 RVA: 0x000B2A24 File Offset: 0x000B0C24
	public void OnDrawGizmos()
	{
		Gizmos.color = new Color(0.3f, 0.3f, 1f, 0.5f);
		Gizmos.DrawWireSphere(base.transform.position, this.radius);
		Gizmos.color = Color.yellow;
		Gizmos.DrawCube(base.transform.position, Vector3.one * 0.5f);
	}

	// Token: 0x06002E78 RID: 11896 RVA: 0x000B2A90 File Offset: 0x000B0C90
	public void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(base.transform.position, this.radius);
	}

	// Token: 0x04001905 RID: 6405
	private static float spawnStagger;

	// Token: 0x04001906 RID: 6406
	public float radius = 40f;

	// Token: 0x04001907 RID: 6407
	public float thinkDelay = 60f;

	// Token: 0x04001908 RID: 6408
	public bool initialSpawn;

	// Token: 0x04001909 RID: 6409
	[SerializeField]
	public List<global::GenericSpawnerSpawnList.GenericSpawnInstance> _spawnList;

	// Token: 0x0400190A RID: 6410
	public global::GenericSpawnerSpawnList spawnListObjectOverride;
}
