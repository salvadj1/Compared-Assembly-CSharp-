using System;
using UnityEngine;

// Token: 0x020005B1 RID: 1457
public class WeightSelection
{
	// Token: 0x06002EDB RID: 11995 RVA: 0x000B4AC4 File Offset: 0x000B2CC4
	public static object RandomPick(global::WeightSelection.WeightedEntry[] array)
	{
		return global::WeightSelection.RandomPickEntry(array).obj;
	}

	// Token: 0x06002EDC RID: 11996 RVA: 0x000B4AD4 File Offset: 0x000B2CD4
	public static global::WeightSelection.WeightedEntry RandomPickEntry(global::WeightSelection.WeightedEntry[] array)
	{
		float num = 0f;
		foreach (global::WeightSelection.WeightedEntry weightedEntry in array)
		{
			num += weightedEntry.weight;
		}
		if (num == 0f)
		{
			return null;
		}
		float num2 = Random.Range(0f, num);
		foreach (global::WeightSelection.WeightedEntry weightedEntry2 in array)
		{
			if ((num2 -= weightedEntry2.weight) <= 0f)
			{
				return weightedEntry2;
			}
		}
		return array[array.Length - 1];
	}

	// Token: 0x06002EDD RID: 11997 RVA: 0x000B4B68 File Offset: 0x000B2D68
	public static T RandomPick<T>(global::WeightSelection.WeightedEntry<T>[] array)
	{
		return global::WeightSelection.RandomPickEntry<T>(array).obj;
	}

	// Token: 0x06002EDE RID: 11998 RVA: 0x000B4B78 File Offset: 0x000B2D78
	public static global::WeightSelection.WeightedEntry<T> RandomPickEntry<T>(global::WeightSelection.WeightedEntry<T>[] array)
	{
		float num = 0f;
		foreach (global::WeightSelection.WeightedEntry<T> weightedEntry in array)
		{
			num += weightedEntry.weight;
		}
		if (num == 0f)
		{
			return null;
		}
		float num2 = Random.Range(0f, num);
		foreach (global::WeightSelection.WeightedEntry<T> weightedEntry2 in array)
		{
			if ((num2 -= weightedEntry2.weight) <= 0f)
			{
				return weightedEntry2;
			}
		}
		return array[array.Length - 1];
	}

	// Token: 0x020005B2 RID: 1458
	[Serializable]
	public class WeightedEntry
	{
		// Token: 0x0400196D RID: 6509
		public float weight;

		// Token: 0x0400196E RID: 6510
		public Object obj;
	}

	// Token: 0x020005B3 RID: 1459
	[Serializable]
	public class WeightedEntry<T>
	{
		// Token: 0x0400196F RID: 6511
		public float weight;

		// Token: 0x04001970 RID: 6512
		public T obj;
	}
}
