using System;
using UnityEngine;

// Token: 0x02000728 RID: 1832
public class LootableObjectSpawner : MonoBehaviour
{
	// Token: 0x06003CE6 RID: 15590 RVA: 0x000D9F44 File Offset: 0x000D8144
	private void Awake()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06003CE7 RID: 15591 RVA: 0x000D9F54 File Offset: 0x000D8154
	public void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(base.transform.position, 0.5f);
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(base.transform.position, base.transform.position + base.transform.forward * 1f);
	}

	// Token: 0x06003CE8 RID: 15592 RVA: 0x000D9FC0 File Offset: 0x000D81C0
	public void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(base.transform.position, 0.5f);
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(base.transform.position, base.transform.position + base.transform.forward * 1f);
	}

	// Token: 0x04001F13 RID: 7955
	public global::LootableObjectSpawner.ChancePick[] _lootableChances;

	// Token: 0x04001F14 RID: 7956
	public bool spawnOnStart = true;

	// Token: 0x04001F15 RID: 7957
	public float spawnTimeMin = 5f;

	// Token: 0x04001F16 RID: 7958
	public float spawnTimeMax = 10f;

	// Token: 0x02000729 RID: 1833
	[Serializable]
	public class ChancePick
	{
		// Token: 0x04001F17 RID: 7959
		public global::LootableObject obj;

		// Token: 0x04001F18 RID: 7960
		public float weight;
	}
}
