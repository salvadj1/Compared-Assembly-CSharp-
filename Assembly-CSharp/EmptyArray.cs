using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000194 RID: 404
public static class EmptyArray<T>
{
	// Token: 0x1700034B RID: 843
	// (get) Token: 0x06000C3B RID: 3131 RVA: 0x0002F82C File Offset: 0x0002DA2C
	public static object defaultBoxedValue
	{
		get
		{
			return (!global::EmptyArray<T>.isByRef) ? global::EmptyArray<T>.DefaultBoxedValue.value : null;
		}
	}

	// Token: 0x1700034C RID: 844
	// (get) Token: 0x06000C3C RID: 3132 RVA: 0x0002F844 File Offset: 0x0002DA44
	public static IEnumerator<T> emptyEnumerator
	{
		get
		{
			return global::EmptyArray<T>.EmptyEnumerator.singleton;
		}
	}

	// Token: 0x1700034D RID: 845
	// (get) Token: 0x06000C3D RID: 3133 RVA: 0x0002F84C File Offset: 0x0002DA4C
	public static IEnumerable<T> emptyEnumerable
	{
		get
		{
			return global::EmptyArray<T>.EmptyEnumerable.singleton;
		}
	}

	// Token: 0x0400080D RID: 2061
	public static readonly T[] array = new T[0];

	// Token: 0x0400080E RID: 2062
	public static readonly bool isByRef = typeof(T).IsByRef;

	// Token: 0x02000195 RID: 405
	private static class DefaultBoxedValue
	{
		// Token: 0x0400080F RID: 2063
		public static object value = default(T);
	}

	// Token: 0x02000196 RID: 406
	private class EmptyEnumerator : IDisposable, IEnumerator, IEnumerator<T>
	{
		// Token: 0x06000C3F RID: 3135 RVA: 0x0002F874 File Offset: 0x0002DA74
		private EmptyEnumerator()
		{
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000C41 RID: 3137 RVA: 0x0002F888 File Offset: 0x0002DA88
		object IEnumerator.Current
		{
			get
			{
				return global::EmptyArray<T>.defaultBoxedValue;
			}
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x0002F890 File Offset: 0x0002DA90
		public bool MoveNext()
		{
			return false;
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000C43 RID: 3139 RVA: 0x0002F894 File Offset: 0x0002DA94
		public T Current
		{
			get
			{
				return default(T);
			}
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x0002F8AC File Offset: 0x0002DAAC
		public void Reset()
		{
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x0002F8B0 File Offset: 0x0002DAB0
		public void Dispose()
		{
		}

		// Token: 0x04000810 RID: 2064
		public static IEnumerator<T> singleton = new global::EmptyArray<T>.EmptyEnumerator();
	}

	// Token: 0x02000197 RID: 407
	private class EmptyEnumerable : IEnumerable, IEnumerable<T>
	{
		// Token: 0x06000C48 RID: 3144 RVA: 0x0002F8C8 File Offset: 0x0002DAC8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return global::EmptyArray<T>.EmptyEnumerator.singleton;
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x0002F8D0 File Offset: 0x0002DAD0
		public IEnumerator<T> GetEnumerator()
		{
			return global::EmptyArray<T>.EmptyEnumerator.singleton;
		}

		// Token: 0x04000811 RID: 2065
		public static IEnumerable<T> singleton = new global::EmptyArray<T>.EmptyEnumerable();
	}
}
