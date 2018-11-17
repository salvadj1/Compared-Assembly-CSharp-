using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000597 RID: 1431
public class GenericSpawnerSpawnList : ScriptableObject
{
	// Token: 0x06002E7A RID: 11898 RVA: 0x000B2AC8 File Offset: 0x000B0CC8
	public List<global::GenericSpawnerSpawnList.GenericSpawnInstance> GetCopy()
	{
		List<global::GenericSpawnerSpawnList.GenericSpawnInstance> list = new List<global::GenericSpawnerSpawnList.GenericSpawnInstance>(this._spawnList.Count);
		foreach (global::GenericSpawnerSpawnList.GenericSpawnInstance genericSpawnInstance in this._spawnList)
		{
			list.Add(genericSpawnInstance.Clone());
		}
		return list;
	}

	// Token: 0x0400190B RID: 6411
	[SerializeField]
	public List<global::GenericSpawnerSpawnList.GenericSpawnInstance> _spawnList;

	// Token: 0x02000598 RID: 1432
	[Serializable]
	public class GenericSpawnInstance
	{
		// Token: 0x06002E7C RID: 11900 RVA: 0x000B2B6C File Offset: 0x000B0D6C
		public int GetNumActive()
		{
			return this.spawned.Count;
		}

		// Token: 0x06002E7D RID: 11901 RVA: 0x000B2B7C File Offset: 0x000B0D7C
		public global::GenericSpawnerSpawnList.GenericSpawnInstance Clone()
		{
			return new global::GenericSpawnerSpawnList.GenericSpawnInstance
			{
				prefabName = this.prefabName,
				targetPopulation = this.targetPopulation,
				numToSpawnPerTick = this.numToSpawnPerTick,
				forceStaticInstantiate = this.forceStaticInstantiate,
				spawned = new List<GameObject>()
			};
		}

		// Token: 0x0400190C RID: 6412
		public string prefabName = string.Empty;

		// Token: 0x0400190D RID: 6413
		public int targetPopulation;

		// Token: 0x0400190E RID: 6414
		public int numToSpawnPerTick = 1;

		// Token: 0x0400190F RID: 6415
		public bool forceStaticInstantiate;

		// Token: 0x04001910 RID: 6416
		public bool useNavmeshSample = true;

		// Token: 0x04001911 RID: 6417
		public List<GameObject> spawned;
	}
}
