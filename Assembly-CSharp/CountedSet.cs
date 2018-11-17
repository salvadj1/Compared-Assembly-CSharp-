using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000161 RID: 353
public class CountedSet<TValue> : IEnumerable, IEnumerable<TValue>, ICollection<TValue>
{
	// Token: 0x06000ACA RID: 2762 RVA: 0x0002AD1C File Offset: 0x00028F1C
	public CountedSet(IEnumerable<TValue> values, IEqualityComparer<TValue> comparer)
	{
		this.index = new Dictionary<TValue, CountedSet<TValue>.Node>(comparer);
		foreach (TValue value in values)
		{
			this.Retain(value);
		}
	}

	// Token: 0x06000ACC RID: 2764 RVA: 0x0002ADA0 File Offset: 0x00028FA0
	void ICollection<TValue>.Add(TValue item)
	{
		((ICollection<TValue>)this.index.Keys).Add(item);
	}

	// Token: 0x06000ACD RID: 2765 RVA: 0x0002ADB4 File Offset: 0x00028FB4
	void ICollection<TValue>.Clear()
	{
		((ICollection<TValue>)this.index.Keys).Clear();
	}

	// Token: 0x06000ACE RID: 2766 RVA: 0x0002ADC8 File Offset: 0x00028FC8
	void ICollection<TValue>.CopyTo(TValue[] array, int arrayIndex)
	{
		((ICollection<TValue>)this.index.Keys).CopyTo(array, arrayIndex);
	}

	// Token: 0x06000ACF RID: 2767 RVA: 0x0002ADDC File Offset: 0x00028FDC
	bool ICollection<TValue>.Remove(TValue item)
	{
		throw new NotSupportedException();
	}

	// Token: 0x06000AD0 RID: 2768 RVA: 0x0002ADE4 File Offset: 0x00028FE4
	IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
	{
		return ((IEnumerable<TValue>)this.index.Keys).GetEnumerator();
	}

	// Token: 0x06000AD1 RID: 2769 RVA: 0x0002ADF8 File Offset: 0x00028FF8
	IEnumerator IEnumerable.GetEnumerator()
	{
		return ((IEnumerable)this.index.Keys).GetEnumerator();
	}

	// Token: 0x06000AD2 RID: 2770 RVA: 0x0002AE0C File Offset: 0x0002900C
	private static EqualityComparer<CountedSet<TValue>.Node> ConvertEqualityComparer(IEqualityComparer<TValue> comparer)
	{
		if (comparer == null || comparer == CountedSet<TValue>.DefaultComparer.Singleton.Value.Comparer)
		{
			return CountedSet<TValue>.DefaultComparer.Singleton.Value;
		}
		return new CountedSet<TValue>.CustomComparer(comparer);
	}

	// Token: 0x170002FF RID: 767
	// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x0002AE3C File Offset: 0x0002903C
	public int Count
	{
		get
		{
			return (int)this.nodeCount;
		}
	}

	// Token: 0x06000AD4 RID: 2772 RVA: 0x0002AE44 File Offset: 0x00029044
	public int Retain(TValue value)
	{
		CountedSet<TValue>.Node node;
		if (!this.index.TryGetValue(value, out node))
		{
			node = (this.index[value] = new CountedSet<TValue>.Node
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

	// Token: 0x06000AD5 RID: 2773 RVA: 0x0002AEAC File Offset: 0x000290AC
	public bool Contains(TValue value)
	{
		return this.index.ContainsKey(value);
	}

	// Token: 0x06000AD6 RID: 2774 RVA: 0x0002AEBC File Offset: 0x000290BC
	public int Release(TValue value)
	{
		CountedSet<TValue>.Node node;
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

	// Token: 0x06000AD7 RID: 2775 RVA: 0x0002AF1C File Offset: 0x0002911C
	public TValue[] ReleaseAll()
	{
		TValue[] array;
		using (CountedSet<TValue>.ReleaseRecursor releaseRecursor = new CountedSet<TValue>.ReleaseRecursor(this))
		{
			releaseRecursor.Run();
			array = releaseRecursor.array;
		}
		return array;
	}

	// Token: 0x06000AD8 RID: 2776 RVA: 0x0002AF74 File Offset: 0x00029174
	public void RetainAll()
	{
		foreach (CountedSet<TValue>.Node node in this.index.Values)
		{
			node.Retain();
			this.totalRetains += 1u;
		}
	}

	// Token: 0x17000300 RID: 768
	public int this[TValue value]
	{
		get
		{
			CountedSet<TValue>.Node node;
			return (int)((!this.index.TryGetValue(value, out node)) ? uint.MaxValue : (node.count - 1u));
		}
	}

	// Token: 0x17000301 RID: 769
	// (get) Token: 0x06000ADA RID: 2778 RVA: 0x0002B020 File Offset: 0x00029220
	public bool IsReadOnly
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06000ADB RID: 2779 RVA: 0x0002B024 File Offset: 0x00029224
	public Dictionary<TValue, CountedSet<TValue>.Node>.KeyCollection.Enumerator GetEnumerator()
	{
		return this.index.Keys.GetEnumerator();
	}

	// Token: 0x040006E5 RID: 1765
	private Dictionary<TValue, CountedSet<TValue>.Node> index;

	// Token: 0x040006E6 RID: 1766
	private uint totalRetains;

	// Token: 0x040006E7 RID: 1767
	private uint nodeCount;

	// Token: 0x040006E8 RID: 1768
	private static TValue[] empty = new TValue[0];

	// Token: 0x02000162 RID: 354
	public class Node
	{
		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000ADD RID: 2781 RVA: 0x0002B040 File Offset: 0x00029240
		public bool Released
		{
			get
			{
				return this.done;
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x0002B048 File Offset: 0x00029248
		public bool Retained
		{
			get
			{
				return !this.done;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000ADF RID: 2783 RVA: 0x0002B054 File Offset: 0x00029254
		public uint ReferenceCount
		{
			get
			{
				return this.count + 1u;
			}
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0002B060 File Offset: 0x00029260
		public bool Release()
		{
			return !this.done && (this.count -= 1u) == 0u;
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x0002B090 File Offset: 0x00029290
		public bool Retain()
		{
			return !this.done && this.count++ == 0u;
		}

		// Token: 0x040006E9 RID: 1769
		public TValue v;

		// Token: 0x040006EA RID: 1770
		public bool done;

		// Token: 0x040006EB RID: 1771
		public uint count;
	}

	// Token: 0x02000163 RID: 355
	private class CustomComparer : EqualityComparer<CountedSet<TValue>.Node>, IDisposable
	{
		// Token: 0x06000AE2 RID: 2786 RVA: 0x0002B0C0 File Offset: 0x000292C0
		public CustomComparer(IEqualityComparer<TValue> comparer)
		{
			this.comparer = comparer;
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0002B0D0 File Offset: 0x000292D0
		public override bool Equals(CountedSet<TValue>.Node x, CountedSet<TValue>.Node y)
		{
			return this.comparer.Equals(x.v, y.v);
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x0002B0EC File Offset: 0x000292EC
		public override int GetHashCode(CountedSet<TValue>.Node obj)
		{
			return this.comparer.GetHashCode(obj.v);
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x0002B100 File Offset: 0x00029300
		public void Dispose()
		{
			if (this.comparer is IDisposable)
			{
				((IDisposable)this.comparer).Dispose();
			}
			this.comparer = null;
		}

		// Token: 0x040006EC RID: 1772
		private IEqualityComparer<TValue> comparer;
	}

	// Token: 0x02000164 RID: 356
	private class DefaultComparer : EqualityComparer<CountedSet<TValue>.Node>
	{
		// Token: 0x06000AE6 RID: 2790 RVA: 0x0002B12C File Offset: 0x0002932C
		private DefaultComparer()
		{
			this.Comparer = EqualityComparer<TValue>.Default;
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x0002B140 File Offset: 0x00029340
		public override bool Equals(CountedSet<TValue>.Node x, CountedSet<TValue>.Node y)
		{
			return this.Comparer.Equals(x.v, y.v);
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0002B15C File Offset: 0x0002935C
		public override int GetHashCode(CountedSet<TValue>.Node obj)
		{
			return this.Comparer.GetHashCode(obj.v);
		}

		// Token: 0x040006ED RID: 1773
		public readonly EqualityComparer<TValue> Comparer;

		// Token: 0x02000165 RID: 357
		public static class Singleton
		{
			// Token: 0x040006EE RID: 1774
			public static readonly CountedSet<TValue>.DefaultComparer Value = new CountedSet<TValue>.DefaultComparer();
		}
	}

	// Token: 0x02000166 RID: 358
	private struct ReleaseRecursor : IDisposable
	{
		// Token: 0x06000AEA RID: 2794 RVA: 0x0002B17C File Offset: 0x0002937C
		public ReleaseRecursor(CountedSet<TValue> v)
		{
			this.s = v;
			this.dict = this.s.index;
			this.enumerator = this.dict.Values.GetEnumerator();
			this.array = CountedSet<TValue>.empty;
			this.count = 0;
			this.disposed = false;
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x0002B1D0 File Offset: 0x000293D0
		public void Run()
		{
			if (this.enumerator.MoveNext())
			{
				CountedSet<TValue>.Node node = this.enumerator.Current;
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

		// Token: 0x06000AEC RID: 2796 RVA: 0x0002B2C0 File Offset: 0x000294C0
		public void Dispose()
		{
			if (!this.disposed)
			{
				this.disposed = true;
				this.enumerator.Dispose();
			}
		}

		// Token: 0x040006EF RID: 1775
		private CountedSet<TValue> s;

		// Token: 0x040006F0 RID: 1776
		private Dictionary<TValue, CountedSet<TValue>.Node> dict;

		// Token: 0x040006F1 RID: 1777
		private Dictionary<TValue, CountedSet<TValue>.Node>.ValueCollection.Enumerator enumerator;

		// Token: 0x040006F2 RID: 1778
		public TValue[] array;

		// Token: 0x040006F3 RID: 1779
		private int count;

		// Token: 0x040006F4 RID: 1780
		private bool disposed;
	}
}
