using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000168 RID: 360
public static class EmptyArray<T>
{
	// Token: 0x17000307 RID: 775
	// (get) Token: 0x06000B0B RID: 2827 RVA: 0x0002B940 File Offset: 0x00029B40
	public static object defaultBoxedValue
	{
		get
		{
			return (!EmptyArray<T>.isByRef) ? EmptyArray<T>.DefaultBoxedValue.value : null;
		}
	}

	// Token: 0x17000308 RID: 776
	// (get) Token: 0x06000B0C RID: 2828 RVA: 0x0002B958 File Offset: 0x00029B58
	public static IEnumerator<T> emptyEnumerator
	{
		get
		{
			return EmptyArray<T>.EmptyEnumerator.singleton;
		}
	}

	// Token: 0x17000309 RID: 777
	// (get) Token: 0x06000B0D RID: 2829 RVA: 0x0002B960 File Offset: 0x00029B60
	public static IEnumerable<T> emptyEnumerable
	{
		get
		{
			return EmptyArray<T>.EmptyEnumerable.singleton;
		}
	}

	// Token: 0x040006F9 RID: 1785
	public static readonly T[] array = new T[0];

	// Token: 0x040006FA RID: 1786
	public static readonly bool isByRef = typeof(T).IsByRef;

	// Token: 0x02000169 RID: 361
	private static class DefaultBoxedValue
	{
		// Token: 0x040006FB RID: 1787
		public static object value = default(T);
	}

	// Token: 0x0200016A RID: 362
	private class EmptyEnumerator : IDisposable, IEnumerator, IEnumerator<T>
	{
		// Token: 0x06000B0F RID: 2831 RVA: 0x0002B988 File Offset: 0x00029B88
		private EmptyEnumerator()
		{
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000B11 RID: 2833 RVA: 0x0002B99C File Offset: 0x00029B9C
		object IEnumerator.Current
		{
			get
			{
				return EmptyArray<T>.defaultBoxedValue;
			}
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0002B9A4 File Offset: 0x00029BA4
		public bool MoveNext()
		{
			return false;
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x0002B9A8 File Offset: 0x00029BA8
		public T Current
		{
			get
			{
				return default(T);
			}
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x0002B9C0 File Offset: 0x00029BC0
		public void Reset()
		{
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x0002B9C4 File Offset: 0x00029BC4
		public void Dispose()
		{
		}

		// Token: 0x040006FC RID: 1788
		public static IEnumerator<T> singleton = new EmptyArray<T>.EmptyEnumerator();
	}

	// Token: 0x0200016B RID: 363
	private class EmptyEnumerable : IEnumerable, IEnumerable<T>
	{
		// Token: 0x06000B18 RID: 2840 RVA: 0x0002B9DC File Offset: 0x00029BDC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return EmptyArray<T>.EmptyEnumerator.singleton;
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x0002B9E4 File Offset: 0x00029BE4
		public IEnumerator<T> GetEnumerator()
		{
			return EmptyArray<T>.EmptyEnumerator.singleton;
		}

		// Token: 0x040006FD RID: 1789
		public static IEnumerable<T> singleton = new EmptyArray<T>.EmptyEnumerable();
	}
}
