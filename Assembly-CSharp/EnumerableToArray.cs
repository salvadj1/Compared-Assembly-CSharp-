using System;
using System.Collections.Generic;

// Token: 0x02000198 RID: 408
public static class EnumerableToArray
{
	// Token: 0x06000C4A RID: 3146 RVA: 0x0002F8D8 File Offset: 0x0002DAD8
	public static T[] ToArray<T>(this T[] array)
	{
		int num = array.Length;
		if (num == 0)
		{
			return global::EmptyArray<T>.array;
		}
		T[] array2 = new T[num];
		Array.Copy(array, array2, num);
		return array2;
	}

	// Token: 0x06000C4B RID: 3147 RVA: 0x0002F908 File Offset: 0x0002DB08
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
			global::EnumerableToArray.EnumeratorToArray<T> enumeratorToArray = new global::EnumerableToArray.EnumeratorToArray<T>(enumerator);
			array2 = enumeratorToArray.array;
		}
		return array2;
	}

	// Token: 0x06000C4C RID: 3148 RVA: 0x0002F98C File Offset: 0x0002DB8C
	public static T[] ToArray<T>(this ICollection<T> collection)
	{
		T[] array = new T[collection.Count];
		collection.CopyTo(array, 0);
		return array;
	}

	// Token: 0x02000199 RID: 409
	private struct EnumeratorToArray<T>
	{
		// Token: 0x06000C4D RID: 3149 RVA: 0x0002F9B0 File Offset: 0x0002DBB0
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
				this.array = global::EmptyArray<T>.array;
			}
			this.enumerator = null;
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x0002F9F0 File Offset: 0x0002DBF0
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

		// Token: 0x04000812 RID: 2066
		public T[] array;

		// Token: 0x04000813 RID: 2067
		private IEnumerator<T> enumerator;

		// Token: 0x04000814 RID: 2068
		private int len;
	}
}
