using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004DC RID: 1244
public class GenericSpawnerSpawnList : ScriptableObject
{
	// Token: 0x06002AC8 RID: 10952 RVA: 0x000AAD30 File Offset: 0x000A8F30
	public List<GenericSpawnerSpawnList.GenericSpawnInstance> GetCopy()
	{
		List<GenericSpawnerSpawnList.GenericSpawnInstance> list = new List<GenericSpawnerSpawnList.GenericSpawnInstance>(this._spawnList.Count);
		foreach (GenericSpawnerSpawnList.GenericSpawnInstance genericSpawnInstance in this._spawnList)
		{
			list.Add(genericSpawnInstance.Clone());
		}
		return list;
	}

	// Token: 0x0400174E RID: 5966
	[SerializeField]
	public List<GenericSpawnerSpawnList.GenericSpawnInstance> _spawnList;

	// Token: 0x020004DD RID: 1245
	[Serializable]
	public class GenericSpawnInstance
	{
		// Token: 0x06002ACA RID: 10954 RVA: 0x000AADD4 File Offset: 0x000A8FD4
		public int GetNumActive()
		{
			return this.spawned.Count;
		}

		// Token: 0x06002ACB RID: 10955 RVA: 0x000AADE4 File Offset: 0x000A8FE4
		public GenericSpawnerSpawnList.GenericSpawnInstance Clone()
		{
			return new GenericSpawnerSpawnList.GenericSpawnInstance
			{
				prefabName = this.prefabName,
				targetPopulation = this.targetPopulation,
				numToSpawnPerTick = this.numToSpawnPerTick,
				forceStaticInstantiate = this.forceStaticInstantiate,
				spawned = new List<GameObject>()
			};
		}

		// Token: 0x0400174F RID: 5967
		public string prefabName = string.Empty;

		// Token: 0x04001750 RID: 5968
		public int targetPopulation;

		// Token: 0x04001751 RID: 5969
		public int numToSpawnPerTick = 1;

		// Token: 0x04001752 RID: 5970
		public bool forceStaticInstantiate;

		// Token: 0x04001753 RID: 5971
		public bool useNavmeshSample = true;

		// Token: 0x04001754 RID: 5972
		public List<GameObject> spawned;
	}
}
