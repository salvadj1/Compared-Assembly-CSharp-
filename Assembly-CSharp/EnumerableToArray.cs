using System;
using System.Collections.Generic;

// Token: 0x0200016C RID: 364
public static class EnumerableToArray
{
	// Token: 0x06000B1A RID: 2842 RVA: 0x0002B9EC File Offset: 0x00029BEC
	public static T[] ToArray<T>(this T[] array)
	{
		int num = array.Length;
		if (num == 0)
		{
			return EmptyArray<T>.array;
		}
		T[] array2 = new T[num];
		Array.Copy(array, array2, num);
		return array2;
	}

	// Token: 0x06000B1B RID: 2843 RVA: 0x0002BA1C File Offset: 0x00029C1C
	public static T[] ToArray<T>(this IEnumerable<T> enumerable)
	{
		if (enumerable is ICollection<T>)
		{
			ICollection<T> collection = (ICollection<T>)enumerable;
			T[] array = new T[collection.Count];
			collection.CopyTo(array, 0);
			return array;
		}
		T[] array2;
		using (IEnumerator<T> enumerator = enumerable.GetEnumerator())
		{
			EnumerableToArray.EnumeratorToArray<T> enumeratorToArray = new EnumerableToArray.EnumeratorToArray<T>(enumerator);
			array2 = enumeratorToArray.array;
		}
		return array2;
	}

	// Token: 0x06000B1C RID: 2844 RVA: 0x0002BAA0 File Offset: 0x00029CA0
	public static T[] ToArray<T>(this ICollection<T> collection)
	{
		T[] array = new T[collection.Count];
		collection.CopyTo(array, 0);
		return array;
	}

	// Token: 0x0200016D RID: 365
	private struct EnumeratorToArray<T>
	{
		// Token: 0x06000B1D RID: 2845 RVA: 0x0002BAC4 File Offset: 0x00029CC4
		public EnumeratorToArray(IEnumerator<T> enumerator)
		{
			this.array = null;
			this.enumerator = enumerator;
			this.len = 0;
			if (enumerator.MoveNext())
			{
				this.Fill();
			}
			else
			{
				this.array = EmptyArray<T>.array;
			}
			this.enumerator = null;
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x0002BB04 File Offset: 0x00029D04
		private void Fill()
		{
			int num = this.len++;
			T t = this.enumerator.Current;
			if (this.enumerator.MoveNext())
			{
				this.Fill();
			}
			else
			{
				this.array = new T[this.len];
			}
			this.array[num] = t;
		}

		// Token: 0x040006FE RID: 1790
		public T[] array;

		// Token: 0x040006FF RID: 1791
		private IEnumerator<T> enumerator;

		// Token: 0x04000700 RID: 1792
		private int len;
	}
}
