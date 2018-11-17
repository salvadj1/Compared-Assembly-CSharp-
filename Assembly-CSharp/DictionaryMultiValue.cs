using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000167 RID: 359
public class DictionaryMultiValue<TKey, TValue> : IEnumerable, IEnumerable<MultiValue<TValue>.KeyPair<TKey>>
{
	// Token: 0x06000AED RID: 2797 RVA: 0x0002B2E0 File Offset: 0x000294E0
	public DictionaryMultiValue(IEnumerable<KeyValuePair<TKey, TValue>> dict, IEqualityComparer<TKey> keyComp, IEqualityComparer<TValue> valComp)
	{
		this.HasKeyComparer = (keyComp != null);
		this.HasValueComparer = (valComp != null);
		this.ValueComparer = valComp;
		this.dict = ((!this.HasKeyComparer) ? new Dictionary<TKey, MultiValue<TValue>>() : new Dictionary<TKey, MultiValue<TValue>>(keyComp));
		this.AddRange(dict);
	}

	// Token: 0x06000AEE RID: 2798 RVA: 0x0002B340 File Offset: 0x00029540
	IEnumerator IEnumerable.GetEnumerator()
	{
		return this.GetEnumerator();
	}

	// Token: 0x17000305 RID: 773
	// (get) Token: 0x06000AEF RID: 2799 RVA: 0x0002B348 File Offset: 0x00029548
	public IEqualityComparer<TKey> KeyComparer
	{
		get
		{
			return this.dict.Comparer;
		}
	}

	// Token: 0x06000AF0 RID: 2800 RVA: 0x0002B358 File Offset: 0x00029558
	internal bool GetMultiValue(TKey key, out MultiValue<TValue> v)
	{
		return this.dict.TryGetValue(key, out v);
	}

	// Token: 0x06000AF1 RID: 2801 RVA: 0x0002B368 File Offset: 0x00029568
	private MultiValue<TValue> CreateMultiValue()
	{
		if (this.HasValueComparer)
		{
			return new MultiValue<TValue>(this.ValueComparer);
		}
		return new MultiValue<TValue>();
	}

	// Token: 0x06000AF2 RID: 2802 RVA: 0x0002B388 File Offset: 0x00029588
	private MultiValue<TValue> CreateMultiValue(IEnumerable<TValue> enumerable)
	{
		if (this.HasValueComparer)
		{
			return new MultiValue<TValue>(enumerable, this.ValueComparer);
		}
		return new MultiValue<TValue>(enumerable);
	}

	// Token: 0x06000AF3 RID: 2803 RVA: 0x0002B3A8 File Offset: 0x000295A8
	internal bool GetOrCreateMultiValue(TKey key, out MultiValue<TValue> v)
	{
		if (this.dict.TryGetValue(key, out v))
		{
			return true;
		}
		v = this.CreateMultiValue();
		return false;
	}

	// Token: 0x06000AF4 RID: 2804 RVA: 0x0002B3C8 File Offset: 0x000295C8
	internal bool GetOrCreateMultiValue(TKey key, out MultiValue<TValue> v, IEnumerable<TValue> enumerable)
	{
		if (this.dict.TryGetValue(key, out v))
		{
			return true;
		}
		v = this.CreateMultiValue(enumerable);
		return false;
	}

	// Token: 0x06000AF5 RID: 2805 RVA: 0x0002B3E8 File Offset: 0x000295E8
	public bool Add(KeyValuePair<TKey, TValue> kv)
	{
		MultiValue<TValue> multiValue;
		if (this.GetOrCreateMultiValue(kv.Key, out multiValue))
		{
			return multiValue.Add(kv.Value);
		}
		if (multiValue.Add(kv.Value))
		{
			this.dict.Add(kv.Key, multiValue);
			return true;
		}
		return false;
	}

	// Token: 0x06000AF6 RID: 2806 RVA: 0x0002B440 File Offset: 0x00029640
	public bool Add(TKey key, TValue value)
	{
		MultiValue<TValue> multiValue;
		if (this.GetOrCreateMultiValue(key, out multiValue))
		{
			return multiValue.Add(value);
		}
		if (multiValue.Add(value))
		{
			this.dict.Add(key, multiValue);
			return true;
		}
		return false;
	}

	// Token: 0x06000AF7 RID: 2807 RVA: 0x0002B480 File Offset: 0x00029680
	public int AddRange(TKey key, IEnumerable<TValue> value)
	{
		MultiValue<TValue> multiValue;
		if (this.GetOrCreateMultiValue(key, out multiValue, value))
		{
			return multiValue.AddRange(value);
		}
		int count = multiValue.Count;
		if (count > 0)
		{
			this.dict.Add(key, multiValue);
		}
		return count;
	}

	// Token: 0x06000AF8 RID: 2808 RVA: 0x0002B4C0 File Offset: 0x000296C0
	public int AddRange<TValueEnumerable>(KeyValuePair<TKey, TValueEnumerable> kv) where TValueEnumerable : IEnumerable<TValue>
	{
		MultiValue<TValue> multiValue;
		if (this.GetOrCreateMultiValue(kv.Key, out multiValue))
		{
			return multiValue.AddRange(kv.Value);
		}
		int count = multiValue.Count;
		if (count > 0)
		{
			this.dict.Add(kv.Key, multiValue);
		}
		return count;
	}

	// Token: 0x06000AF9 RID: 2809 RVA: 0x0002B518 File Offset: 0x00029718
	public int AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs)
	{
		int num = 0;
		foreach (KeyValuePair<TKey, TValue> kv in pairs)
		{
			if (this.Add(kv))
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x06000AFA RID: 2810 RVA: 0x0002B584 File Offset: 0x00029784
	public int AddRange<TValueEnumerable>(IEnumerable<KeyValuePair<TKey, TValueEnumerable>> pairs) where TValueEnumerable : IEnumerable<TValue>
	{
		int num = 0;
		foreach (KeyValuePair<TKey, TValueEnumerable> kv in pairs)
		{
			num += this.AddRange<TValueEnumerable>(kv);
		}
		return num;
	}

	// Token: 0x06000AFB RID: 2811 RVA: 0x0002B5E8 File Offset: 0x000297E8
	public bool Remove(TKey key)
	{
		MultiValue<TValue> multiValue;
		return this.GetMultiValue(key, out multiValue) && this.dict.Remove(key) && multiValue.Clear();
	}

	// Token: 0x06000AFC RID: 2812 RVA: 0x0002B620 File Offset: 0x00029820
	public bool Clear(TKey key)
	{
		MultiValue<TValue> multiValue;
		return this.GetMultiValue(key, out multiValue) && multiValue.Clear();
	}

	// Token: 0x06000AFD RID: 2813 RVA: 0x0002B644 File Offset: 0x00029844
	public bool Clear(TKey key, bool erase)
	{
		return this.Clear(key) && this.dict.Remove(key);
	}

	// Token: 0x06000AFE RID: 2814 RVA: 0x0002B664 File Offset: 0x00029864
	public bool RemoveAt(TKey key, int index)
	{
		MultiValue<TValue> multiValue;
		return this.GetMultiValue(key, out multiValue) && multiValue.RemoveAt(index);
	}

	// Token: 0x06000AFF RID: 2815 RVA: 0x0002B68C File Offset: 0x0002988C
	public int ValueCount(TKey key)
	{
		MultiValue<TValue> multiValue;
		return (!this.GetMultiValue(key, out multiValue)) ? 0 : multiValue.Count;
	}

	// Token: 0x06000B00 RID: 2816 RVA: 0x0002B6B4 File Offset: 0x000298B4
	public bool ContainsKey(TKey key)
	{
		return this.dict.ContainsKey(key);
	}

	// Token: 0x06000B01 RID: 2817 RVA: 0x0002B6C4 File Offset: 0x000298C4
	public bool Contains(TKey key, TValue value)
	{
		MultiValue<TValue> multiValue;
		return this.GetMultiValue(key, out multiValue) && multiValue.Contains(value);
	}

	// Token: 0x06000B02 RID: 2818 RVA: 0x0002B6EC File Offset: 0x000298EC
	public bool ContainsValue(TKey key, TValue value)
	{
		return this.Contains(key, value);
	}

	// Token: 0x06000B03 RID: 2819 RVA: 0x0002B6F8 File Offset: 0x000298F8
	public bool Contains(KeyValuePair<TKey, TValue> kv)
	{
		return this.Contains(kv.Key, kv.Value);
	}

	// Token: 0x06000B04 RID: 2820 RVA: 0x0002B710 File Offset: 0x00029910
	public bool ContainsValue(TValue value)
	{
		foreach (MultiValue<TValue> multiValue in this.dict.Values)
		{
			if (multiValue.Contains(value))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000B05 RID: 2821 RVA: 0x0002B78C File Offset: 0x0002998C
	internal void SetMultiValue(TKey key, MultiValue<TValue> mv)
	{
		this.dict.Add(key, mv);
	}

	// Token: 0x06000B06 RID: 2822 RVA: 0x0002B79C File Offset: 0x0002999C
	public IEnumerator<MultiValue<TValue>.KeyPair<TKey>> GetEnumerator()
	{
		foreach (TKey key in this.dict.Keys)
		{
			yield return new MultiValue<TValue>.KeyPair<TKey>(this, key);
		}
		yield break;
	}

	// Token: 0x06000B07 RID: 2823 RVA: 0x0002B7B8 File Offset: 0x000299B8
	private bool AreEqual(TKey l, TKey r)
	{
		IEqualityComparer<TKey> comparer = this.dict.Comparer;
		return comparer.GetHashCode(l) == comparer.GetHashCode(r) && comparer.Equals(l, r);
	}

	// Token: 0x17000306 RID: 774
	public MultiValue<TValue>.KeyPair<TKey> this[TKey key]
	{
		get
		{
			return new MultiValue<TValue>.KeyPair<TKey>(this, key);
		}
		set
		{
			if (value.Dictionary == this)
			{
				MultiValue<TValue> multiValue5;
				if (this.AreEqual(value.Key, key))
				{
					MultiValue<TValue> multiValue;
					if (this.GetMultiValue(value.Key, out multiValue))
					{
						MultiValue<TValue> multiValue2;
						if (this.GetMultiValue(key, out multiValue2))
						{
							multiValue2.Set(multiValue);
						}
						else if (multiValue.Count > 0)
						{
							this.dict.Add(value.Key, multiValue.Clone());
						}
					}
				}
				else if (value.Valid)
				{
					MultiValue<TValue> multiValue3;
					if (value.Dictionary.GetMultiValue(value.Key, out multiValue3))
					{
						MultiValue<TValue> multiValue4;
						if (this.GetMultiValue(key, out multiValue4))
						{
							multiValue4.Set(multiValue3);
						}
						else if (multiValue3.Count > 0 && multiValue3.Clone(this.ValueComparer, out multiValue4))
						{
							this.dict.Add(value.Key, multiValue4);
						}
					}
				}
				else if (value.Dictionary.GetMultiValue(value.Key, out multiValue5))
				{
					multiValue5.Clear();
				}
			}
		}
	}

	// Token: 0x040006F5 RID: 1781
	private Dictionary<TKey, MultiValue<TValue>> dict;

	// Token: 0x040006F6 RID: 1782
	public readonly bool HasKeyComparer;

	// Token: 0x040006F7 RID: 1783
	public readonly IEqualityComparer<TValue> ValueComparer;

	// Token: 0x040006F8 RID: 1784
	public readonly bool HasValueComparer;
}
