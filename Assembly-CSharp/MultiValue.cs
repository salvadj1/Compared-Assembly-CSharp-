using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200015D RID: 349
public class MultiValue<TValue> : IEnumerable, IEnumerable<TValue>, ICollection<TValue>, IList<TValue>, ICloneable
{
	// Token: 0x06000A8D RID: 2701 RVA: 0x0002A100 File Offset: 0x00028300
	private MultiValue(bool ignore)
	{
	}

	// Token: 0x06000A8E RID: 2702 RVA: 0x0002A108 File Offset: 0x00028308
	public MultiValue()
	{
		this.list = new List<TValue>();
		this.hashSet = new HashSet<TValue>();
	}

	// Token: 0x06000A8F RID: 2703 RVA: 0x0002A128 File Offset: 0x00028328
	private MultiValue(IEqualityComparer<TValue> comparer, MultiValue<TValue> mv)
	{
		this.list = new List<TValue>(mv.list);
		this.hashSet = new HashSet<TValue>(mv.hashSet, comparer);
		this.count = mv.count;
	}

	// Token: 0x06000A90 RID: 2704 RVA: 0x0002A160 File Offset: 0x00028360
	public MultiValue(IEnumerable<TValue> v)
	{
		this.hashSet = new HashSet<TValue>();
		MultiValue<TValue>.InitData initData;
		initData.mv = this;
		using (initData.enumerator = v.GetEnumerator())
		{
			initData.RecurseInit();
		}
	}

	// Token: 0x06000A91 RID: 2705 RVA: 0x0002A1CC File Offset: 0x000283CC
	public MultiValue(IEnumerable<TValue> v, IEqualityComparer<TValue> equalityComparer)
	{
		this.hashSet = new HashSet<TValue>(equalityComparer);
		MultiValue<TValue>.InitData initData;
		initData.mv = this;
		using (initData.enumerator = v.GetEnumerator())
		{
			initData.RecurseInit();
		}
	}

	// Token: 0x06000A92 RID: 2706 RVA: 0x0002A23C File Offset: 0x0002843C
	public MultiValue(int capacity, IEqualityComparer<TValue> equalityComparer)
	{
		this.hashSet = new HashSet<TValue>(equalityComparer);
		this.list = new List<TValue>(capacity);
	}

	// Token: 0x06000A93 RID: 2707 RVA: 0x0002A25C File Offset: 0x0002845C
	public MultiValue(IEqualityComparer<TValue> equalityComparer)
	{
		this.hashSet = new HashSet<TValue>(equalityComparer);
		this.list = new List<TValue>();
	}

	// Token: 0x06000A94 RID: 2708 RVA: 0x0002A27C File Offset: 0x0002847C
	IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
	{
		return ((IEnumerable<TValue>)this.list).GetEnumerator();
	}

	// Token: 0x06000A95 RID: 2709 RVA: 0x0002A28C File Offset: 0x0002848C
	IEnumerator IEnumerable.GetEnumerator()
	{
		return ((IEnumerable)this.list).GetEnumerator();
	}

	// Token: 0x06000A96 RID: 2710 RVA: 0x0002A29C File Offset: 0x0002849C
	void IList<TValue>.Insert(int index, TValue value)
	{
		this.InsertOrMove(index, value);
	}

	// Token: 0x06000A97 RID: 2711 RVA: 0x0002A2A8 File Offset: 0x000284A8
	void IList<TValue>.RemoveAt(int index)
	{
		TValue item = this.list[index];
		this.list.RemoveAt(index);
		this.hashSet.Remove(item);
		this.count--;
	}

	// Token: 0x06000A98 RID: 2712 RVA: 0x0002A2EC File Offset: 0x000284EC
	void ICollection<TValue>.Add(TValue item)
	{
		if (this.hashSet.Add(item))
		{
			this.list.Add(item);
			this.count++;
		}
	}

	// Token: 0x06000A99 RID: 2713 RVA: 0x0002A31C File Offset: 0x0002851C
	void ICollection<TValue>.Clear()
	{
		if (this.count > 0)
		{
			this.list.Clear();
			this.hashSet.Clear();
			this.count = 0;
		}
	}

	// Token: 0x06000A9A RID: 2714 RVA: 0x0002A348 File Offset: 0x00028548
	bool ICollection<TValue>.Remove(TValue value)
	{
		return this.Remove(value) != 0;
	}

	// Token: 0x170002F6 RID: 758
	// (get) Token: 0x06000A9B RID: 2715 RVA: 0x0002A358 File Offset: 0x00028558
	bool ICollection<TValue>.IsReadOnly
	{
		get
		{
			return false;
		}
	}

	// Token: 0x06000A9C RID: 2716 RVA: 0x0002A35C File Offset: 0x0002855C
	object ICloneable.Clone()
	{
		return this.Clone();
	}

	// Token: 0x06000A9D RID: 2717 RVA: 0x0002A364 File Offset: 0x00028564
	public List<TValue>.Enumerator GetEnumerator()
	{
		return this.list.GetEnumerator();
	}

	// Token: 0x06000A9E RID: 2718 RVA: 0x0002A374 File Offset: 0x00028574
	public int IndexOf(TValue item)
	{
		if (this.count >= 16 && !this.hashSet.Contains(item))
		{
			return -1;
		}
		return this.list.IndexOf(item);
	}

	// Token: 0x06000A9F RID: 2719 RVA: 0x0002A3B0 File Offset: 0x000285B0
	public int InsertOrMove(int index, TValue item)
	{
		if (index == this.count)
		{
			if (this.hashSet.Add(item))
			{
				this.list.Add(item);
				this.count++;
				return 1;
			}
			int num = this.list.IndexOf(item);
			int num2 = this.count - num;
			if (num2 != 1)
			{
				if (num2 != 2)
				{
					this.list.RemoveAt(num);
					this.list.Add(item);
				}
				else
				{
					this.list.Reverse(this.count - 2, 2);
				}
				return 2;
			}
			return 0;
		}
		else
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "index < 0");
			}
			if (index > this.count)
			{
				throw new ArgumentOutOfRangeException("index", "index > count");
			}
			if (this.hashSet.Add(item))
			{
				this.list.Insert(index, item);
				this.count++;
				return 1;
			}
			int num3 = this.list.IndexOf(item);
			int num4 = index - num3;
			int num2 = num4;
			switch (num2 + 1)
			{
			case 0:
				this.list.Reverse(num3, 2);
				break;
			case 1:
				return 0;
			case 2:
				this.list.Reverse(index, 2);
				break;
			default:
				if (num4 <= -2)
				{
					for (int i = num3; i > index; i--)
					{
						this.list[i] = this.list[i - 1];
					}
				}
				else if (num4 >= 2)
				{
					for (int j = num3; j < index; j++)
					{
						this.list[j] = this.list[j + 1];
					}
				}
				this.list[index] = item;
				break;
			}
			return 2;
		}
	}

	// Token: 0x06000AA0 RID: 2720 RVA: 0x0002A598 File Offset: 0x00028798
	public bool RemoveAt(int index)
	{
		TValue item = this.list[index];
		this.list.RemoveAt(index);
		this.hashSet.Remove(item);
		return --this.count != 0;
	}

	// Token: 0x170002F7 RID: 759
	public TValue this[int index]
	{
		get
		{
			return this.list[index];
		}
		set
		{
			this.list[index] = value;
		}
	}

	// Token: 0x06000AA3 RID: 2723 RVA: 0x0002A604 File Offset: 0x00028804
	public bool Add(TValue item)
	{
		if (this.hashSet.Add(item))
		{
			this.list.Add(item);
			this.count++;
			return true;
		}
		return false;
	}

	// Token: 0x06000AA4 RID: 2724 RVA: 0x0002A640 File Offset: 0x00028840
	public bool Clear()
	{
		if (this.count > 0)
		{
			this.list.Clear();
			this.hashSet.Clear();
			this.count = 0;
			return true;
		}
		return false;
	}

	// Token: 0x06000AA5 RID: 2725 RVA: 0x0002A67C File Offset: 0x0002887C
	public bool Contains(TValue item)
	{
		return this.hashSet.Contains(item);
	}

	// Token: 0x06000AA6 RID: 2726 RVA: 0x0002A68C File Offset: 0x0002888C
	public void CopyTo(TValue[] array, int arrayIndex)
	{
		this.list.CopyTo(array, arrayIndex);
	}

	// Token: 0x06000AA7 RID: 2727 RVA: 0x0002A69C File Offset: 0x0002889C
	public int Remove(TValue item)
	{
		if (!this.hashSet.Remove(item))
		{
			return 0;
		}
		if (!this.list.Remove(item))
		{
			this.hashSet.Add(item);
			return 0;
		}
		return (--this.count != 0) ? 1 : 2;
	}

	// Token: 0x170002F8 RID: 760
	// (get) Token: 0x06000AA8 RID: 2728 RVA: 0x0002A6FC File Offset: 0x000288FC
	public int Count
	{
		get
		{
			return this.count;
		}
	}

	// Token: 0x06000AA9 RID: 2729 RVA: 0x0002A704 File Offset: 0x00028904
	public int AddRange(IEnumerable<TValue> value)
	{
		int num = 0;
		foreach (TValue item in value)
		{
			if (this.Add(item))
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x06000AAA RID: 2730 RVA: 0x0002A770 File Offset: 0x00028970
	public void Set(MultiValue<TValue> other)
	{
		if (other == this)
		{
			return;
		}
		this.Clear();
		foreach (TValue item in other)
		{
			this.Add(item);
		}
	}

	// Token: 0x06000AAB RID: 2731 RVA: 0x0002A7E4 File Offset: 0x000289E4
	public MultiValue<TValue> Clone()
	{
		return new MultiValue<TValue>(false)
		{
			hashSet = new HashSet<TValue>(this.hashSet),
			list = new List<TValue>(this.list),
			count = this.count
		};
	}

	// Token: 0x06000AAC RID: 2732 RVA: 0x0002A828 File Offset: 0x00028A28
	public bool Clone(IEqualityComparer<TValue> valueComparer, out MultiValue<TValue> val)
	{
		if (this.count == 0)
		{
			val = null;
			return false;
		}
		if (valueComparer == this.hashSet.Comparer)
		{
			val = this.Clone();
			return true;
		}
		val = new MultiValue<TValue>(this.list, valueComparer);
		if (val.count == 0)
		{
			val = null;
			return false;
		}
		return true;
	}

	// Token: 0x040006DB RID: 1755
	private const int kCheckHashCountMin = 16;

	// Token: 0x040006DC RID: 1756
	private const bool kIsReadOnly = false;

	// Token: 0x040006DD RID: 1757
	private HashSet<TValue> hashSet;

	// Token: 0x040006DE RID: 1758
	private List<TValue> list;

	// Token: 0x040006DF RID: 1759
	private int count;

	// Token: 0x0200015E RID: 350
	private struct InitData
	{
		// Token: 0x06000AAD RID: 2733 RVA: 0x0002A880 File Offset: 0x00028A80
		public void RecurseInit()
		{
			while (this.enumerator.MoveNext())
			{
				TValue item = this.enumerator.Current;
				if (this.mv.hashSet.Add(item))
				{
					this.mv.count++;
					this.RecurseInit();
					this.mv.list.Add(item);
					return;
				}
			}
			this.mv.list = new List<TValue>(this.mv.count);
		}

		// Token: 0x040006E0 RID: 1760
		public MultiValue<TValue> mv;

		// Token: 0x040006E1 RID: 1761
		public IEnumerator<TValue> enumerator;
	}

	// Token: 0x0200015F RID: 351
	public struct KeyPair<TKey> : IEnumerable, IEnumerable<TValue>, ICollection<TValue>, IList<TValue>
	{
		// Token: 0x06000AAE RID: 2734 RVA: 0x0002A90C File Offset: 0x00028B0C
		public KeyPair(DictionaryMultiValue<TKey, TValue> dict, TKey key)
		{
			this.dict = dict;
			this.key = key;
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x0002A91C File Offset: 0x00028B1C
		IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
		{
			MultiValue<TValue> multiValue;
			if (this.GetMultiValue(out multiValue))
			{
				return ((IEnumerable<TValue>)multiValue).GetEnumerator();
			}
			return ((IEnumerable<TValue>)MultiValue<TValue>.KeyPair<TKey>.g.emptyList).GetEnumerator();
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x0002A948 File Offset: 0x00028B48
		IEnumerator IEnumerable.GetEnumerator()
		{
			MultiValue<TValue> multiValue;
			if (this.GetMultiValue(out multiValue))
			{
				return ((IEnumerable)multiValue).GetEnumerator();
			}
			return ((IEnumerable)MultiValue<TValue>.KeyPair<TKey>.g.emptyList).GetEnumerator();
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x0002A974 File Offset: 0x00028B74
		bool ICollection<TValue>.Remove(TValue value)
		{
			MultiValue<TValue> multiValue;
			return this.GetMultiValue(out multiValue) && ((ICollection<TValue>)multiValue).Remove(value);
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x0002A998 File Offset: 0x00028B98
		void IList<TValue>.RemoveAt(int index)
		{
			MultiValue<TValue> multiValue;
			if (!this.GetMultiValue(out multiValue))
			{
				MultiValue<TValue>.KeyPair<TKey>.g.emptyList.RemoveAt(index);
			}
			else
			{
				((IList<TValue>)multiValue).RemoveAt(index);
			}
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x0002A9CC File Offset: 0x00028BCC
		void IList<TValue>.Insert(int index, TValue value)
		{
			MultiValue<TValue> multiValue;
			bool orCreateMultiValue = this.GetOrCreateMultiValue(out multiValue);
			((IList<TValue>)multiValue).Insert(index, value);
			if (!orCreateMultiValue && multiValue.count > 0)
			{
				this.Bind(multiValue);
			}
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x0002AA04 File Offset: 0x00028C04
		void ICollection<TValue>.Add(TValue item)
		{
			MultiValue<TValue> multiValue;
			bool orCreateMultiValue = this.GetOrCreateMultiValue(out multiValue);
			((ICollection<TValue>)multiValue).Add(item);
			if (!orCreateMultiValue && multiValue.count != 0)
			{
				this.Bind(multiValue);
			}
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x0002AA3C File Offset: 0x00028C3C
		void ICollection<TValue>.Clear()
		{
			MultiValue<TValue> multiValue;
			if (this.GetMultiValue(out multiValue))
			{
				((ICollection<TValue>)multiValue).Clear();
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000AB6 RID: 2742 RVA: 0x0002AA5C File Offset: 0x00028C5C
		bool ICollection<TValue>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000AB7 RID: 2743 RVA: 0x0002AA60 File Offset: 0x00028C60
		public TKey Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x0002AA68 File Offset: 0x00028C68
		public DictionaryMultiValue<TKey, TValue> Dictionary
		{
			get
			{
				return this.dict;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000AB9 RID: 2745 RVA: 0x0002AA70 File Offset: 0x00028C70
		public bool Valid
		{
			get
			{
				return this.dict != null;
			}
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x0002AA80 File Offset: 0x00028C80
		private bool GetMultiValue(out MultiValue<TValue> v)
		{
			if (this.dict == null)
			{
				v = null;
			}
			return this.dict.GetMultiValue(this.key, out v);
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x0002AAB0 File Offset: 0x00028CB0
		private bool GetOrCreateMultiValue(out MultiValue<TValue> v)
		{
			if (this.dict == null)
			{
				throw new InvalidOperationException("The KeyPair is invalid");
			}
			return this.dict.GetOrCreateMultiValue(this.key, out v);
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x0002AAE8 File Offset: 0x00028CE8
		private void Bind(MultiValue<TValue> v)
		{
			this.dict.SetMultiValue(this.key, v);
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x0002AAFC File Offset: 0x00028CFC
		public List<TValue>.Enumerator GetEnumerator()
		{
			MultiValue<TValue> multiValue;
			if (this.GetMultiValue(out multiValue))
			{
				return multiValue.GetEnumerator();
			}
			return MultiValue<TValue>.KeyPair<TKey>.g.emptyList.GetEnumerator();
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x0002AB28 File Offset: 0x00028D28
		public int Count
		{
			get
			{
				MultiValue<TValue> multiValue;
				return (!this.GetMultiValue(out multiValue)) ? multiValue.Count : 0;
			}
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x0002AB50 File Offset: 0x00028D50
		public bool Add(TValue value)
		{
			MultiValue<TValue> multiValue;
			bool orCreateMultiValue = this.GetOrCreateMultiValue(out multiValue);
			if (multiValue.Add(value))
			{
				if (!orCreateMultiValue)
				{
					this.Bind(multiValue);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x0002AB84 File Offset: 0x00028D84
		public int InsertOrMove(int index, TValue item)
		{
			MultiValue<TValue> multiValue;
			bool orCreateMultiValue = this.GetOrCreateMultiValue(out multiValue);
			int num = multiValue.InsertOrMove(index, item);
			if (num == 1 && !orCreateMultiValue)
			{
				this.Bind(multiValue);
			}
			return num;
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x0002ABB8 File Offset: 0x00028DB8
		public int Remove(TValue value)
		{
			MultiValue<TValue> multiValue;
			return (!this.GetMultiValue(out multiValue)) ? 0 : multiValue.Remove(value);
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x0002ABE0 File Offset: 0x00028DE0
		public bool Contains(TValue value)
		{
			MultiValue<TValue> multiValue;
			return this.GetMultiValue(out multiValue) && multiValue.Contains(value);
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x0002AC04 File Offset: 0x00028E04
		public bool Clear(TValue value)
		{
			MultiValue<TValue> multiValue;
			return this.GetMultiValue(out multiValue) && multiValue.Clear();
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x0002AC28 File Offset: 0x00028E28
		public bool RemoveAt(int index)
		{
			MultiValue<TValue> multiValue;
			return this.GetMultiValue(out multiValue) && multiValue.RemoveAt(index);
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x0002AC4C File Offset: 0x00028E4C
		public void CopyTo(TValue[] array, int arrayIndex)
		{
			MultiValue<TValue> multiValue;
			if (this.GetMultiValue(out multiValue))
			{
				multiValue.CopyTo(array, arrayIndex);
			}
			else
			{
				MultiValue<TValue>.KeyPair<TKey>.g.emptyList.CopyTo(array, arrayIndex);
			}
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x0002AC80 File Offset: 0x00028E80
		public int IndexOf(TValue item)
		{
			MultiValue<TValue> multiValue;
			return (!this.GetMultiValue(out multiValue)) ? -1 : multiValue.IndexOf(item);
		}

		// Token: 0x170002FE RID: 766
		public TValue this[int i]
		{
			get
			{
				MultiValue<TValue> multiValue;
				if (!this.GetMultiValue(out multiValue))
				{
					return MultiValue<TValue>.KeyPair<TKey>.g.emptyList[i];
				}
				return multiValue[i];
			}
			set
			{
				MultiValue<TValue> multiValue;
				if (!this.GetMultiValue(out multiValue))
				{
					MultiValue<TValue>.KeyPair<TKey>.g.emptyList[i] = value;
				}
				else
				{
					multiValue[i] = value;
				}
			}
		}

		// Token: 0x040006E2 RID: 1762
		private readonly TKey key;

		// Token: 0x040006E3 RID: 1763
		private readonly DictionaryMultiValue<TKey, TValue> dict;

		// Token: 0x02000160 RID: 352
		private static class g
		{
			// Token: 0x040006E4 RID: 1764
			public static readonly List<TValue> emptyList = new List<TValue>(0);
		}
	}
}
