using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000188 RID: 392
public class MultiValue<TValue> : IEnumerable, IEnumerable<TValue>, ICollection<TValue>, IList<TValue>, ICloneable
{
	// Token: 0x06000BB7 RID: 2999 RVA: 0x0002DE7C File Offset: 0x0002C07C
	private MultiValue(bool ignore)
	{
	}

	// Token: 0x06000BB8 RID: 3000 RVA: 0x0002DE84 File Offset: 0x0002C084
	public MultiValue()
	{
		this.list = new List<TValue>();
		this.hashSet = new HashSet<TValue>();
	}

	// Token: 0x06000BB9 RID: 3001 RVA: 0x0002DEA4 File Offset: 0x0002C0A4
	private MultiValue(IEqualityComparer<TValue> comparer, global::MultiValue<TValue> mv)
	{
		this.list = new List<TValue>(mv.list);
		this.hashSet = new HashSet<TValue>(mv.hashSet, comparer);
		this.count = mv.count;
	}

	// Token: 0x06000BBA RID: 3002 RVA: 0x0002DEDC File Offset: 0x0002C0DC
	public MultiValue(IEnumerable<TValue> v)
	{
		this.hashSet = new HashSet<TValue>();
		global::MultiValue<TValue>.InitData initData;
		initData.mv = this;
		using (initData.enumerator = v.GetEnumerator())
		{
			initData.RecurseInit();
		}
	}

	// Token: 0x06000BBB RID: 3003 RVA: 0x0002DF48 File Offset: 0x0002C148
	public MultiValue(IEnumerable<TValue> v, IEqualityComparer<TValue> equalityComparer)
	{
		this.hashSet = new HashSet<TValue>(equalityComparer);
		global::MultiValue<TValue>.InitData initData;
		initData.mv = this;
		using (initData.enumerator = v.GetEnumerator())
		{
			initData.RecurseInit();
		}
	}

	// Token: 0x06000BBC RID: 3004 RVA: 0x0002DFB8 File Offset: 0x0002C1B8
	public MultiValue(int capacity, IEqualityComparer<TValue> equalityComparer)
	{
		this.hashSet = new HashSet<TValue>(equalityComparer);
		this.list = new List<TValue>(capacity);
	}

	// Token: 0x06000BBD RID: 3005 RVA: 0x0002DFD8 File Offset: 0x0002C1D8
	public MultiValue(IEqualityComparer<TValue> equalityComparer)
	{
		this.hashSet = new HashSet<TValue>(equalityComparer);
		this.list = new List<TValue>();
	}

	// Token: 0x06000BBE RID: 3006 RVA: 0x0002DFF8 File Offset: 0x0002C1F8
	IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
	{
		return ((IEnumerable<TValue>)this.list).GetEnumerator();
	}

	// Token: 0x06000BBF RID: 3007 RVA: 0x0002E008 File Offset: 0x0002C208
	IEnumerator IEnumerable.GetEnumerator()
	{
		return ((IEnumerable)this.list).GetEnumerator();
	}

	// Token: 0x06000BC0 RID: 3008 RVA: 0x0002E018 File Offset: 0x0002C218
	void IList<TValue>.Insert(int index, TValue value)
	{
		this.InsertOrMove(index, value);
	}

	// Token: 0x06000BC1 RID: 3009 RVA: 0x0002E024 File Offset: 0x0002C224
	void IList<TValue>.RemoveAt(int index)
	{
		TValue item = this.list[index];
		this.list.RemoveAt(index);
		this.hashSet.Remove(item);
		this.count--;
	}

	// Token: 0x06000BC2 RID: 3010 RVA: 0x0002E068 File Offset: 0x0002C268
	void ICollection<TValue>.Add(TValue item)
	{
		if (this.hashSet.Add(item))
		{
			this.list.Add(item);
			this.count++;
		}
	}

	// Token: 0x06000BC3 RID: 3011 RVA: 0x0002E098 File Offset: 0x0002C298
	void ICollection<TValue>.Clear()
	{
		if (this.count > 0)
		{
			this.list.Clear();
			this.hashSet.Clear();
			this.count = 0;
		}
	}

	// Token: 0x06000BC4 RID: 3012 RVA: 0x0002E0C4 File Offset: 0x0002C2C4
	bool ICollection<TValue>.Remove(TValue value)
	{
		return this.Remove(value) != 0;
	}

	// Token: 0x17000338 RID: 824
	// (get) Token: 0x06000BC5 RID: 3013 RVA: 0x0002E0D4 File Offset: 0x0002C2D4
	bool ICollection<TValue>.IsReadOnly
	{
		get
		{
			return false;
		}
	}

	// Token: 0x06000BC6 RID: 3014 RVA: 0x0002E0D8 File Offset: 0x0002C2D8
	object ICloneable.Clone()
	{
		return this.Clone();
	}

	// Token: 0x06000BC7 RID: 3015 RVA: 0x0002E0E0 File Offset: 0x0002C2E0
	public List<TValue>.Enumerator GetEnumerator()
	{
		return this.list.GetEnumerator();
	}

	// Token: 0x06000BC8 RID: 3016 RVA: 0x0002E0F0 File Offset: 0x0002C2F0
	public int IndexOf(TValue item)
	{
		if (this.count >= 16 && !this.hashSet.Contains(item))
		{
			return -1;
		}
		return this.list.IndexOf(item);
	}

	// Token: 0x06000BC9 RID: 3017 RVA: 0x0002E12C File Offset: 0x0002C32C
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

	// Token: 0x06000BCA RID: 3018 RVA: 0x0002E314 File Offset: 0x0002C514
	public bool RemoveAt(int index)
	{
		TValue item = this.list[index];
		this.list.RemoveAt(index);
		this.hashSet.Remove(item);
		return --this.count != 0;
	}

	// Token: 0x17000339 RID: 825
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

	// Token: 0x06000BCD RID: 3021 RVA: 0x0002E380 File Offset: 0x0002C580
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

	// Token: 0x06000BCE RID: 3022 RVA: 0x0002E3BC File Offset: 0x0002C5BC
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

	// Token: 0x06000BCF RID: 3023 RVA: 0x0002E3F8 File Offset: 0x0002C5F8
	public bool Contains(TValue item)
	{
		return this.hashSet.Contains(item);
	}

	// Token: 0x06000BD0 RID: 3024 RVA: 0x0002E408 File Offset: 0x0002C608
	public void CopyTo(TValue[] array, int arrayIndex)
	{
		this.list.CopyTo(array, arrayIndex);
	}

	// Token: 0x06000BD1 RID: 3025 RVA: 0x0002E418 File Offset: 0x0002C618
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

	// Token: 0x1700033A RID: 826
	// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x0002E478 File Offset: 0x0002C678
	public int Count
	{
		get
		{
			return this.count;
		}
	}

	// Token: 0x06000BD3 RID: 3027 RVA: 0x0002E480 File Offset: 0x0002C680
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

	// Token: 0x06000BD4 RID: 3028 RVA: 0x0002E4EC File Offset: 0x0002C6EC
	public void Set(global::MultiValue<TValue> other)
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

	// Token: 0x06000BD5 RID: 3029 RVA: 0x0002E560 File Offset: 0x0002C760
	public global::MultiValue<TValue> Clone()
	{
		return new global::MultiValue<TValue>(false)
		{
			hashSet = new HashSet<TValue>(this.hashSet),
			list = new List<TValue>(this.list),
			count = this.count
		};
	}

	// Token: 0x06000BD6 RID: 3030 RVA: 0x0002E5A4 File Offset: 0x0002C7A4
	public bool Clone(IEqualityComparer<TValue> valueComparer, out global::MultiValue<TValue> val)
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
		val = new global::MultiValue<TValue>(this.list, valueComparer);
		if (val.count == 0)
		{
			val = null;
			return false;
		}
		return true;
	}

	// Token: 0x040007EA RID: 2026
	private const int kCheckHashCountMin = 16;

	// Token: 0x040007EB RID: 2027
	private const bool kIsReadOnly = false;

	// Token: 0x040007EC RID: 2028
	private HashSet<TValue> hashSet;

	// Token: 0x040007ED RID: 2029
	private List<TValue> list;

	// Token: 0x040007EE RID: 2030
	private int count;

	// Token: 0x02000189 RID: 393
	private struct InitData
	{
		// Token: 0x06000BD7 RID: 3031 RVA: 0x0002E5FC File Offset: 0x0002C7FC
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

		// Token: 0x040007EF RID: 2031
		public global::MultiValue<TValue> mv;

		// Token: 0x040007F0 RID: 2032
		public IEnumerator<TValue> enumerator;
	}

	// Token: 0x0200018A RID: 394
	public struct KeyPair<TKey> : IEnumerable, IEnumerable<TValue>, ICollection<TValue>, IList<TValue>
	{
		// Token: 0x06000BD8 RID: 3032 RVA: 0x0002E688 File Offset: 0x0002C888
		public KeyPair(global::DictionaryMultiValue<TKey, TValue> dict, TKey key)
		{
			this.dict = dict;
			this.key = key;
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x0002E698 File Offset: 0x0002C898
		IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
		{
			global::MultiValue<TValue> multiValue;
			if (this.GetMultiValue(out multiValue))
			{
				return ((IEnumerable<TValue>)multiValue).GetEnumerator();
			}
			return ((IEnumerable<TValue>)global::MultiValue<TValue>.KeyPair<TKey>.g.emptyList).GetEnumerator();
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x0002E6C4 File Offset: 0x0002C8C4
		IEnumerator IEnumerable.GetEnumerator()
		{
			global::MultiValue<TValue> multiValue;
			if (this.GetMultiValue(out multiValue))
			{
				return ((IEnumerable)multiValue).GetEnumerator();
			}
			return ((IEnumerable)global::MultiValue<TValue>.KeyPair<TKey>.g.emptyList).GetEnumerator();
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x0002E6F0 File Offset: 0x0002C8F0
		bool ICollection<TValue>.Remove(TValue value)
		{
			global::MultiValue<TValue> multiValue;
			return this.GetMultiValue(out multiValue) && ((ICollection<TValue>)multiValue).Remove(value);
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x0002E714 File Offset: 0x0002C914
		void IList<TValue>.RemoveAt(int index)
		{
			global::MultiValue<TValue> multiValue;
			if (!this.GetMultiValue(out multiValue))
			{
				global::MultiValue<TValue>.KeyPair<TKey>.g.emptyList.RemoveAt(index);
			}
			else
			{
				((IList<TValue>)multiValue).RemoveAt(index);
			}
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x0002E748 File Offset: 0x0002C948
		void IList<TValue>.Insert(int index, TValue value)
		{
			global::MultiValue<TValue> multiValue;
			bool orCreateMultiValue = this.GetOrCreateMultiValue(out multiValue);
			((IList<TValue>)multiValue).Insert(index, value);
			if (!orCreateMultiValue && multiValue.count > 0)
			{
				this.Bind(multiValue);
			}
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x0002E780 File Offset: 0x0002C980
		void ICollection<TValue>.Add(TValue item)
		{
			global::MultiValue<TValue> multiValue;
			bool orCreateMultiValue = this.GetOrCreateMultiValue(out multiValue);
			((ICollection<TValue>)multiValue).Add(item);
			if (!orCreateMultiValue && multiValue.count != 0)
			{
				this.Bind(multiValue);
			}
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x0002E7B8 File Offset: 0x0002C9B8
		void ICollection<TValue>.Clear()
		{
			global::MultiValue<TValue> multiValue;
			if (this.GetMultiValue(out multiValue))
			{
				((ICollection<TValue>)multiValue).Clear();
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000BE0 RID: 3040 RVA: 0x0002E7D8 File Offset: 0x0002C9D8
		bool ICollection<TValue>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x0002E7DC File Offset: 0x0002C9DC
		public TKey Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000BE2 RID: 3042 RVA: 0x0002E7E4 File Offset: 0x0002C9E4
		public global::DictionaryMultiValue<TKey, TValue> Dictionary
		{
			get
			{
				return this.dict;
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x0002E7EC File Offset: 0x0002C9EC
		public bool Valid
		{
			get
			{
				return this.dict != null;
			}
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x0002E7FC File Offset: 0x0002C9FC
		private bool GetMultiValue(out global::MultiValue<TValue> v)
		{
			if (this.dict == null)
			{
				v = null;
			}
			return this.dict.GetMultiValue(this.key, out v);
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x0002E82C File Offset: 0x0002CA2C
		private bool GetOrCreateMultiValue(out global::MultiValue<TValue> v)
		{
			if (this.dict == null)
			{
				throw new InvalidOperationException("The KeyPair is invalid");
			}
			return this.dict.GetOrCreateMultiValue(this.key, out v);
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x0002E864 File Offset: 0x0002CA64
		private void Bind(global::MultiValue<TValue> v)
		{
			this.dict.SetMultiValue(this.key, v);
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x0002E878 File Offset: 0x0002CA78
		public List<TValue>.Enumerator GetEnumerator()
		{
			global::MultiValue<TValue> multiValue;
			if (this.GetMultiValue(out multiValue))
			{
				return multiValue.GetEnumerator();
			}
			return global::MultiValue<TValue>.KeyPair<TKey>.g.emptyList.GetEnumerator();
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x0002E8A4 File Offset: 0x0002CAA4
		public int Count
		{
			get
			{
				global::MultiValue<TValue> multiValue;
				return (!this.GetMultiValue(out multiValue)) ? multiValue.Count : 0;
			}
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x0002E8CC File Offset: 0x0002CACC
		public bool Add(TValue value)
		{
			global::MultiValue<TValue> multiValue;
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

		// Token: 0x06000BEA RID: 3050 RVA: 0x0002E900 File Offset: 0x0002CB00
		public int InsertOrMove(int index, TValue item)
		{
			global::MultiValue<TValue> multiValue;
			bool orCreateMultiValue = this.GetOrCreateMultiValue(out multiValue);
			int num = multiValue.InsertOrMove(index, item);
			if (num == 1 && !orCreateMultiValue)
			{
				this.Bind(multiValue);
			}
			return num;
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x0002E934 File Offset: 0x0002CB34
		public int Remove(TValue value)
		{
			global::MultiValue<TValue> multiValue;
			return (!this.GetMultiValue(out multiValue)) ? 0 : multiValue.Remove(value);
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x0002E95C File Offset: 0x0002CB5C
		public bool Contains(TValue value)
		{
			global::MultiValue<TValue> multiValue;
			return this.GetMultiValue(out multiValue) && multiValue.Contains(value);
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x0002E980 File Offset: 0x0002CB80
		public bool Clear(TValue value)
		{
			global::MultiValue<TValue> multiValue;
			return this.GetMultiValue(out multiValue) && multiValue.Clear();
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x0002E9A4 File Offset: 0x0002CBA4
		public bool RemoveAt(int index)
		{
			global::MultiValue<TValue> multiValue;
			return this.GetMultiValue(out multiValue) && multiValue.RemoveAt(index);
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x0002E9C8 File Offset: 0x0002CBC8
		public void CopyTo(TValue[] array, int arrayIndex)
		{
			global::MultiValue<TValue> multiValue;
			if (this.GetMultiValue(out multiValue))
			{
				multiValue.CopyTo(array, arrayIndex);
			}
			else
			{
				global::MultiValue<TValue>.KeyPair<TKey>.g.emptyList.CopyTo(array, arrayIndex);
			}
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x0002E9FC File Offset: 0x0002CBFC
		public int IndexOf(TValue item)
		{
			global::MultiValue<TValue> multiValue;
			return (!this.GetMultiValue(out multiValue)) ? -1 : multiValue.IndexOf(item);
		}

		// Token: 0x17000340 RID: 832
		public TValue this[int i]
		{
			get
			{
				global::MultiValue<TValue> multiValue;
				if (!this.GetMultiValue(out multiValue))
				{
					return global::MultiValue<TValue>.KeyPair<TKey>.g.emptyList[i];
				}
				return multiValue[i];
			}
			set
			{
				global::MultiValue<TValue> multiValue;
				if (!this.GetMultiValue(out multiValue))
				{
					global::MultiValue<TValue>.KeyPair<TKey>.g.emptyList[i] = value;
				}
				else
				{
					multiValue[i] = value;
				}
			}
		}

		// Token: 0x040007F1 RID: 2033
		private readonly TKey key;

		// Token: 0x040007F2 RID: 2034
		private readonly global::DictionaryMultiValue<TKey, TValue> dict;

		// Token: 0x0200018B RID: 395
		private static class g
		{
			// Token: 0x040007F3 RID: 2035
			public static readonly List<TValue> emptyList = new List<TValue>(0);
		}
	}
}
