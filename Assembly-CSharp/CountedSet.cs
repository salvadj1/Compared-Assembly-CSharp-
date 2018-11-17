using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200018C RID: 396
public class CountedSet<TValue> : IEnumerable, IEnumerable<TValue>, ICollection<TValue>
{
	// Token: 0x06000BF4 RID: 3060 RVA: 0x0002EA98 File Offset: 0x0002CC98
	public CountedSet(IEnumerable<TValue> values, IEqualityComparer<TValue> comparer)
	{
		this.index = new Dictionary<TValue, global::CountedSet<TValue>.Node>(comparer);
		foreach (TValue value in values)
		{
			this.Retain(value);
		}
	}

	// Token: 0x06000BF6 RID: 3062 RVA: 0x0002EB1C File Offset: 0x0002CD1C
	void ICollection<TValue>.Add(TValue item)
	{
		((ICollection<TValue>)this.index.Keys).Add(item);
	}

	// Token: 0x06000BF7 RID: 3063 RVA: 0x0002EB30 File Offset: 0x0002CD30
	void ICollection<TValue>.Clear()
	{
		((ICollection<TValue>)this.index.Keys).Clear();
	}

	// Token: 0x06000BF8 RID: 3064 RVA: 0x0002EB44 File Offset: 0x0002CD44
	void ICollection<TValue>.CopyTo(TValue[] array, int arrayIndex)
	{
		((ICollection<TValue>)this.index.Keys).CopyTo(array, arrayIndex);
	}

	// Token: 0x06000BF9 RID: 3065 RVA: 0x0002EB58 File Offset: 0x0002CD58
	bool ICollection<TValue>.Remove(TValue item)
	{
		throw new NotSupportedException();
	}

	// Token: 0x06000BFA RID: 3066 RVA: 0x0002EB60 File Offset: 0x0002CD60
	IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
	{
		return ((IEnumerable<TValue>)this.index.Keys).GetEnumerator();
	}

	// Token: 0x06000BFB RID: 3067 RVA: 0x0002EB74 File Offset: 0x0002CD74
	IEnumerator IEnumerable.GetEnumerator()
	{
		return ((IEnumerable)this.index.Keys).GetEnumerator();
	}

	// Token: 0x06000BFC RID: 3068 RVA: 0x0002EB88 File Offset: 0x0002CD88
	private static EqualityComparer<global::CountedSet<TValue>.Node> ConvertEqualityComparer(IEqualityComparer<TValue> comparer)
	{
		if (comparer == null || comparer == global::CountedSet<TValue>.DefaultComparer.Singleton.Value.Comparer)
		{
			return global::CountedSet<TValue>.DefaultComparer.Singleton.Value;
		}
		return new global::CountedSet<TValue>.CustomComparer(comparer);
	}

	// Token: 0x17000341 RID: 833
	// (get) Token: 0x06000BFD RID: 3069 RVA: 0x0002EBB8 File Offset: 0x0002CDB8
	public int Count
	{
		get
		{
			return (int)this.nodeCount;
		}
	}

	// Token: 0x06000BFE RID: 3070 RVA: 0x0002EBC0 File Offset: 0x0002CDC0
	public int Retain(TValue value)
	{
		global::CountedSet<TValue>.Node node;
		if (!this.index.TryGetValue(value, out node))
		{
			node = (this.index[value] = new global::CountedSet<TValue>.Node
			{
				v = value
			});
			this.nodeCount += 1u;
		}
		uint count = node.count;
		node.Retain();
		this.totalRetains += 1u;
		return (int)count;
	}

	// Token: 0x06000BFF RID: 3071 RVA: 0x0002EC28 File Offset: 0x0002CE28
	public bool Contains(TValue value)
	{
		return this.index.ContainsKey(value);
	}

	// Token: 0x06000C00 RID: 3072 RVA: 0x0002EC38 File Offset: 0x0002CE38
	public int Release(TValue value)
	{
		global::CountedSet<TValue>.Node node;
		if (!this.index.TryGetValue(value, out node))
		{
			return -1;
		}
		bool flag = node.Release();
		this.totalRetains -= 1u;
		if (flag)
		{
			this.index.Remove(value);
			this.nodeCount -= 1u;
		}
		return (int)node.count;
	}

	// Token: 0x06000C01 RID: 3073 RVA: 0x0002EC98 File Offset: 0x0002CE98
	public TValue[] ReleaseAll()
	{
		TValue[] array;
		using (global::CountedSet<TValue>.ReleaseRecursor releaseRecursor = new global::CountedSet<TValue>.ReleaseRecursor(this))
		{
			releaseRecursor.Run();
			array = releaseRecursor.array;
		}
		return array;
	}

	// Token: 0x06000C02 RID: 3074 RVA: 0x0002ECF0 File Offset: 0x0002CEF0
	public void RetainAll()
	{
		foreach (global::CountedSet<TValue>.Node node in this.index.Values)
		{
			node.Retain();
			this.totalRetains += 1u;
		}
	}

	// Token: 0x17000342 RID: 834
	public int this[TValue value]
	{
		get
		{
			global::CountedSet<TValue>.Node node;
			return (int)((!this.index.TryGetValue(value, out node)) ? uint.MaxValue : (node.count - 1u));
		}
	}

	// Token: 0x17000343 RID: 835
	// (get) Token: 0x06000C04 RID: 3076 RVA: 0x0002ED9C File Offset: 0x0002CF9C
	public bool IsReadOnly
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06000C05 RID: 3077 RVA: 0x0002EDA0 File Offset: 0x0002CFA0
	public Dictionary<TValue, global::CountedSet<TValue>.Node>.KeyCollection.Enumerator GetEnumerator()
	{
		return this.index.Keys.GetEnumerator();
	}

	// Token: 0x040007F4 RID: 2036
	private Dictionary<TValue, global::CountedSet<TValue>.Node> index;

	// Token: 0x040007F5 RID: 2037
	private uint totalRetains;

	// Token: 0x040007F6 RID: 2038
	private uint nodeCount;

	// Token: 0x040007F7 RID: 2039
	private static TValue[] empty = new TValue[0];

	// Token: 0x0200018D RID: 397
	public class Node
	{
		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000C07 RID: 3079 RVA: 0x0002EDBC File Offset: 0x0002CFBC
		public bool Released
		{
			get
			{
				return this.done;
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x0002EDC4 File Offset: 0x0002CFC4
		public bool Retained
		{
			get
			{
				return !this.done;
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000C09 RID: 3081 RVA: 0x0002EDD0 File Offset: 0x0002CFD0
		public uint ReferenceCount
		{
			get
			{
				return this.count + 1u;
			}
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x0002EDDC File Offset: 0x0002CFDC
		public bool Release()
		{
			return !this.done && (this.count -= 1u) == 0u;
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x0002EE0C File Offset: 0x0002D00C
		public bool Retain()
		{
			return !this.done && this.count++ == 0u;
		}

		// Token: 0x040007F8 RID: 2040
		public TValue v;

		// Token: 0x040007F9 RID: 2041
		public bool done;

		// Token: 0x040007FA RID: 2042
		public uint count;
	}

	// Token: 0x0200018E RID: 398
	private class CustomComparer : EqualityComparer<global::CountedSet<TValue>.Node>, IDisposable
	{
		// Token: 0x06000C0C RID: 3084 RVA: 0x0002EE3C File Offset: 0x0002D03C
		public CustomComparer(IEqualityComparer<TValue> comparer)
		{
			this.comparer = comparer;
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x0002EE4C File Offset: 0x0002D04C
		public override bool Equals(global::CountedSet<TValue>.Node x, global::CountedSet<TValue>.Node y)
		{
			return this.comparer.Equals(x.v, y.v);
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x0002EE68 File Offset: 0x0002D068
		public override int GetHashCode(global::CountedSet<TValue>.Node obj)
		{
			return this.comparer.GetHashCode(obj.v);
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x0002EE7C File Offset: 0x0002D07C
		public void Dispose()
		{
			if (this.comparer is IDisposable)
			{
				((IDisposable)this.comparer).Dispose();
			}
			this.comparer = null;
		}

		// Token: 0x040007FB RID: 2043
		private IEqualityComparer<TValue> comparer;
	}

	// Token: 0x0200018F RID: 399
	private class DefaultComparer : EqualityComparer<global::CountedSet<TValue>.Node>
	{
		// Token: 0x06000C10 RID: 3088 RVA: 0x0002EEA8 File Offset: 0x0002D0A8
		private DefaultComparer()
		{
			this.Comparer = EqualityComparer<TValue>.Default;
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x0002EEBC File Offset: 0x0002D0BC
		public override bool Equals(global::CountedSet<TValue>.Node x, global::CountedSet<TValue>.Node y)
		{
			return this.Comparer.Equals(x.v, y.v);
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x0002EED8 File Offset: 0x0002D0D8
		public override int GetHashCode(global::CountedSet<TValue>.Node obj)
		{
			return this.Comparer.GetHashCode(obj.v);
		}

		// Token: 0x040007FC RID: 2044
		public readonly EqualityComparer<TValue> Comparer;

		// Token: 0x02000190 RID: 400
		public static class Singleton
		{
			// Token: 0x040007FD RID: 2045
			public static readonly global::CountedSet<TValue>.DefaultComparer Value = new global::CountedSet<TValue>.DefaultComparer();
		}
	}

	// Token: 0x02000191 RID: 401
	private struct ReleaseRecursor : IDisposable
	{
		// Token: 0x06000C14 RID: 3092 RVA: 0x0002EEF8 File Offset: 0x0002D0F8
		public ReleaseRecursor(global::CountedSet<TValue> v)
		{
			this.s = v;
			this.dict = this.s.index;
			this.enumerator = this.dict.Values.GetEnumerator();
			this.array = global::CountedSet<TValue>.empty;
			this.count = 0;
			this.disposed = false;
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x0002EF4C File Offset: 0x0002D14C
		public void Run()
		{
			if (this.enumerator.MoveNext())
			{
				global::CountedSet<TValue>.Node node = this.enumerator.Current;
				if (node.Release())
				{
					this.s.totalRetains -= 1u;
					this.count++;
					this.Run();
					this.dict.Remove(node.v);
					this.s.nodeCount -= 1u;
					this.array[this.count--] = node.v;
				}
				else
				{
					this.s.totalRetains -= 1u;
				}
			}
			else
			{
				this.Dispose();
				if (this.count > 0)
				{
					this.array = new TValue[this.count];
				}
				this.count--;
			}
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x0002F03C File Offset: 0x0002D23C
		public void Dispose()
		{
			if (!this.disposed)
			{
				this.disposed = true;
				this.enumerator.Dispose();
			}
		}

		// Token: 0x040007FE RID: 2046
		private global::CountedSet<TValue> s;

		// Token: 0x040007FF RID: 2047
		private Dictionary<TValue, global::CountedSet<TValue>.Node> dict;

		// Token: 0x04000800 RID: 2048
		private Dictionary<TValue, global::CountedSet<TValue>.Node>.ValueCollection.Enumerator enumerator;

		// Token: 0x04000801 RID: 2049
		public TValue[] array;

		// Token: 0x04000802 RID: 2050
		private int count;

		// Token: 0x04000803 RID: 2051
		private bool disposed;
	}
}
