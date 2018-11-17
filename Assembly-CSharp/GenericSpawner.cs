using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004DB RID: 1243
public class GenericSpawner : MonoBehaviour
{
	// Token: 0x06002AC4 RID: 10948 RVA: 0x000AAC7C File Offset: 0x000A8E7C
	public void Awake()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06002AC5 RID: 10949 RVA: 0x000AAC8C File Offset: 0x000A8E8C
	public void OnDrawGizmos()
	{
		Gizmos.color = new Color(0.3f, 0.3f, 1f, 0.5f);
		Gizmos.DrawWireSphere(base.transform.position, this.radius);
		Gizmos.color = Color.yellow;
		Gizmos.DrawCube(base.transform.position, Vector3.one * 0.5f);
	}

	// Token: 0x06002AC6 RID: 10950 RVA: 0x000AACF8 File Offset: 0x000A8EF8
	public void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(base.transform.position, this.radius);
	}

	// Token: 0x04001748 RID: 5960
	private static float spawnStagger;

	// Token: 0x04001749 RID: 5961
	public float radius = 40f;

	// Token: 0x0400174A RID: 5962
	public float thinkDelay = 60f;

	// Token: 0x0400174B RID: 5963
	public bool initialSpawn;

	// Token: 0x0400174C RID: 5964
	[SerializeField]
	public List<GenericSpawnerSpawnList.GenericSpawnInstance> _spawnList;

	// Token: 0x0400174D RID: 5965
	public GenericSpawnerSpawnList spawnListObjectOverride;
}
