using System;
using UnityEngine;

// Token: 0x02000664 RID: 1636
public class LootableObjectSpawner : MonoBehaviour
{
	// Token: 0x060038F2 RID: 14578 RVA: 0x000D1564 File Offset: 0x000CF764
	private void Awake()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x060038F3 RID: 14579 RVA: 0x000D1574 File Offset: 0x000CF774
	public void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(base.transform.position, 0.5f);
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(base.transform.position, base.transform.position + base.transform.forward * 1f);
	}

	// Token: 0x060038F4 RID: 14580 RVA: 0x000D15E0 File Offset: 0x000CF7E0
	public void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(base.transform.position, 0.5f);
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(base.transform.position, base.transform.position + base.transform.forward * 1f);
	}

	// Token: 0x04001D1B RID: 7451
	public LootableObjectSpawner.ChancePick[] _lootableChances;

	// Token: 0x04001D1C RID: 7452
	public bool spawnOnStart = true;

	// Token: 0x04001D1D RID: 7453
	public float spawnTimeMin = 5f;

	// Token: 0x04001D1E RID: 7454
	public float spawnTimeMax = 10f;

	// Token: 0x02000665 RID: 1637
	[Serializable]
	public class ChancePick
	{
		// Token: 0x04001D1F RID: 7455
		public LootableObject obj;

		// Token: 0x04001D20 RID: 7456
		public float weight;
	}
}
