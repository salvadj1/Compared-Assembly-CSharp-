using System;
using UnityEngine;

// Token: 0x020004F4 RID: 1268
public class WeightSelection
{
	// Token: 0x06002B1B RID: 11035 RVA: 0x000ACA28 File Offset: 0x000AAC28
	public static object RandomPick(WeightSelection.WeightedEntry[] array)
	{
		return WeightSelection.RandomPickEntry(array).obj;
	}

	// Token: 0x06002B1C RID: 11036 RVA: 0x000ACA38 File Offset: 0x000AAC38
	public static WeightSelection.WeightedEntry RandomPickEntry(WeightSelection.WeightedEntry[] array)
	{
		float num = 0f;
		foreach (WeightSelection.WeightedEntry weightedEntry in array)
		{
			num += weightedEntry.weight;
		}
		if (num == 0f)
		{
			return null;
		}
		float num2 = Random.Range(0f, num);
		foreach (WeightSelection.WeightedEntry weightedEntry2 in array)
		{
			if ((num2 -= weightedEntry2.weight) <= 0f)
			{
				return weightedEntry2;
			}
		}
		return array[array.Length - 1];
	}

	// Token: 0x06002B1D RID: 11037 RVA: 0x000ACACC File Offset: 0x000AACCC
	public static T RandomPick<T>(WeightSelection.WeightedEntry<T>[] array)
	{
		return WeightSelection.RandomPickEntry<T>(array).obj;
	}

	// Token: 0x06002B1E RID: 11038 RVA: 0x000ACADC File Offset: 0x000AACDC
	public static WeightSelection.WeightedEntry<T> RandomPickEntry<T>(WeightSelection.WeightedEntry<T>[] array)
	{
		float num = 0f;
		foreach (WeightSelection.WeightedEntry<T> weightedEntry in array)
		{
			num += weightedEntry.weight;
		}
		if (num == 0f)
		{
			return null;
		}
		float num2 = Random.Range(0f, num);
		foreach (WeightSelection.WeightedEntry<T> weightedEntry2 in array)
		{
			if ((num2 -= weightedEntry2.weight) <= 0f)
			{
				return weightedEntry2;
			}
		}
		return array[array.Length - 1];
	}

	// Token: 0x020004F5 RID: 1269
	[Serializable]
	public class WeightedEntry
	{
		// Token: 0x040017A1 RID: 6049
		public float weight;

		// Token: 0x040017A2 RID: 6050
		public Object obj;
	}

	// Token: 0x020004F6 RID: 1270
	[Serializable]
	public class WeightedEntry<T>
	{
		// Token: 0x040017A3 RID: 6051
		public float weight;

		// Token: 0x040017A4 RID: 6052
		public T obj;
	}
}
