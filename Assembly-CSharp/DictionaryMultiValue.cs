using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000192 RID: 402
public class DictionaryMultiValue<TKey, TValue> : IEnumerable, IEnumerable<global::MultiValue<TValue>.KeyPair<TKey>>
{
	// Token: 0x06000C17 RID: 3095 RVA: 0x0002F05C File Offset: 0x0002D25C
	public DictionaryMultiValue(IEnumerable<KeyValuePair<TKey, TValue>> dict, IEqualityComparer<TKey> keyComp, IEqualityComparer<TValue> valComp)
	{
		this.HasKeyComparer = (keyComp != null);
		this.HasValueComparer = (valComp != null);
		this.ValueComparer = valComp;
		this.dict = ((!this.HasKeyComparer) ? new Dictionary<TKey, global::MultiValue<TValue>>() : new Dictionary<TKey, global::MultiValue<TValue>>(keyComp));
		this.AddRange(dict);
	}

	// Token: 0x06000C18 RID: 3096 RVA: 0x0002F0BC File Offset: 0x0002D2BC
	IEnumerator IEnumerable.GetEnumerator()
	{
		return this.GetEnumerator();
	}

	// Token: 0x17000347 RID: 839
	// (get) Token: 0x06000C19 RID: 3097 RVA: 0x0002F0C4 File Offset: 0x0002D2C4
	public IEqualityComparer<TKey> KeyComparer
	{
		get
		{
			return this.dict.Comparer;
		}
	}

	// Token: 0x06000C1A RID: 3098 RVA: 0x0002F0D4 File Offset: 0x0002D2D4
	internal bool GetMultiValue(TKey key, out global::MultiValue<TValue> v)
	{
		return this.dict.TryGetValue(key, out v);
	}

	// Token: 0x06000C1B RID: 3099 RVA: 0x0002F0E4 File Offset: 0x0002D2E4
	private global::MultiValue<TValue> CreateMultiValue()
	{
		if (this.HasValueComparer)
		{
			return new global::MultiValue<TValue>(this.ValueComparer);
		}
		return new global::MultiValue<TValue>();
	}

	// Token: 0x06000C1C RID: 3100 RVA: 0x0002F104 File Offset: 0x0002D304
	private global::MultiValue<TValue> CreateMultiValue(IEnumerable<TValue> enumerable)
	{
		if (this.HasValueComparer)
		{
			return new global::MultiValue<TValue>(enumerable, this.ValueComparer);
		}
		return new global::MultiValue<TValue>(enumerable);
	}

	// Token: 0x06000C1D RID: 3101 RVA: 0x0002F124 File Offset: 0x0002D324
	internal bool GetOrCreateMultiValue(TKey key, out global::MultiValue<TValue> v)
	{
		if (this.dict.TryGetValue(key, out v))
		{
			return true;
		}
		v = this.CreateMultiValue();
		return false;
	}

	// Token: 0x06000C1E RID: 3102 RVA: 0x0002F144 File Offset: 0x0002D344
	internal bool GetOrCreateMultiValue(TKey key, out global::MultiValue<TValue> v, IEnumerable<TValue> enumerable)
	{
		if (this.dict.TryGetValue(key, out v))
		{
			return true;
		}
		v = this.CreateMultiValue(enumerable);
		return false;
	}

	// Token: 0x06000C1F RID: 3103 RVA: 0x0002F164 File Offset: 0x0002D364
	public bool Add(KeyValuePair<TKey, TValue> kv)
	{
		global::MultiValue<TValue> multiValue;
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

	// Token: 0x06000C20 RID: 3104 RVA: 0x0002F1BC File Offset: 0x0002D3BC
	public bool Add(TKey key, TValue value)
	{
		global::MultiValue<TValue> multiValue;
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

	// Token: 0x06000C21 RID: 3105 RVA: 0x0002F1FC File Offset: 0x0002D3FC
	public int AddRange(TKey key, IEnumerable<TValue> value)
	{
		global::MultiValue<TValue> multiValue;
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

	// Token: 0x06000C22 RID: 3106 RVA: 0x0002F23C File Offset: 0x0002D43C
	public int AddRange<TValueEnumerable>(KeyValuePair<TKey, TValueEnumerable> kv) where TValueEnumerable : IEnumerable<TValue>
	{
		global::MultiValue<TValue> multiValue;
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

	// Token: 0x06000C23 RID: 3107 RVA: 0x0002F294 File Offset: 0x0002D494
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

	// Token: 0x06000C24 RID: 3108 RVA: 0x0002F300 File Offset: 0x0002D500
	public int AddRange<TValueEnumerable>(IEnumerable<KeyValuePair<TKey, TValueEnumerable>> pairs) where TValueEnumerable : IEnumerable<TValue>
	{
		int num = 0;
		foreach (KeyValuePair<TKey, TValueEnumerable> kv in pairs)
		{
			num += this.AddRange<TValueEnumerable>(kv);
		}
		return num;
	}

	// Token: 0x06000C25 RID: 3109 RVA: 0x0002F364 File Offset: 0x0002D564
	public bool Remove(TKey key)
	{
		global::MultiValue<TValue> multiValue;
		return this.GetMultiValue(key, out multiValue) && this.dict.Remove(key) && multiValue.Clear();
	}

	// Token: 0x06000C26 RID: 3110 RVA: 0x0002F39C File Offset: 0x0002D59C
	public bool Clear(TKey key)
	{
		global::MultiValue<TValue> multiValue;
		return this.GetMultiValue(key, out multiValue) && multiValue.Clear();
	}

	// Token: 0x06000C27 RID: 3111 RVA: 0x0002F3C0 File Offset: 0x0002D5C0
	public bool Clear(TKey key, bool erase)
	{
		return this.Clear(key) && this.dict.Remove(key);
	}

	// Token: 0x06000C28 RID: 3112 RVA: 0x0002F3E0 File Offset: 0x0002D5E0
	public bool RemoveAt(TKey key, int index)
	{
		global::MultiValue<TValue> multiValue;
		return this.GetMultiValue(key, out multiValue) && multiValue.RemoveAt(index);
	}

	// Token: 0x06000C29 RID: 3113 RVA: 0x0002F408 File Offset: 0x0002D608
	public int ValueCount(TKey key)
	{
		global::MultiValue<TValue> multiValue;
		return (!this.GetMultiValue(key, out multiValue)) ? 0 : multiValue.Count;
	}

	// Token: 0x06000C2A RID: 3114 RVA: 0x0002F430 File Offset: 0x0002D630
	public bool ContainsKey(TKey key)
	{
		return this.dict.ContainsKey(key);
	}

	// Token: 0x06000C2B RID: 3115 RVA: 0x0002F440 File Offset: 0x0002D640
	public bool Contains(TKey key, TValue value)
	{
		global::MultiValue<TValue> multiValue;
		return this.GetMultiValue(key, out multiValue) && multiValue.Contains(value);
	}

	// Token: 0x06000C2C RID: 3116 RVA: 0x0002F468 File Offset: 0x0002D668
	public bool ContainsValue(TKey key, TValue value)
	{
		return this.Contains(key, value);
	}

	// Token: 0x06000C2D RID: 3117 RVA: 0x0002F474 File Offset: 0x0002D674
	public bool Contains(KeyValuePair<TKey, TValue> kv)
	{
		return this.Contains(kv.Key, kv.Value);
	}

	// Token: 0x06000C2E RID: 3118 RVA: 0x0002F48C File Offset: 0x0002D68C
	public bool ContainsValue(TValue value)
	{
		foreach (global::MultiValue<TValue> multiValue in this.dict.Values)
		{
			if (multiValue.Contains(value))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000C2F RID: 3119 RVA: 0x0002F508 File Offset: 0x0002D708
	internal void SetMultiValue(TKey key, global::MultiValue<TValue> mv)
	{
		this.dict.Add(key, mv);
	}

	// Token: 0x06000C30 RID: 3120 RVA: 0x0002F518 File Offset: 0x0002D718
	public IEnumerator<global::MultiValue<TValue>.KeyPair<TKey>> GetEnumerator()
	{
		foreach (TKey key in this.dict.Keys)
		{
			yield return new global::MultiValue<TValue>.KeyPair<TKey>(this, key);
		}
		yield break;
	}

	// Token: 0x06000C31 RID: 3121 RVA: 0x0002F534 File Offset: 0x0002D734
	private bool AreEqual(TKey l, TKey r)
	{
		IEqualityComparer<TKey> comparer = this.dict.Comparer;
		return comparer.GetHashCode(l) == comparer.GetHashCode(r) && comparer.Equals(l, r);
	}

	// Token: 0x17000348 RID: 840
	public global::MultiValue<TValue>.KeyPair<TKey> this[TKey key]
	{
		get
		{
			return new global::MultiValue<TValue>.KeyPair<TKey>(this, key);
		}
		set
		{
			if (value.Dictionary == this)
			{
				global::MultiValue<TValue> multiValue5;
				if (this.AreEqual(value.Key, key))
				{
					global::MultiValue<TValue> multiValue;
					if (this.GetMultiValue(value.Key, out multiValue))
					{
						global::MultiValue<TValue> multiValue2;
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
					global::MultiValue<TValue> multiValue3;
					if (value.Dictionary.GetMultiValue(value.Key, out multiValue3))
					{
						global::MultiValue<TValue> multiValue4;
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

	// Token: 0x04000804 RID: 2052
	private Dictionary<TKey, global::MultiValue<TValue>> dict;

	// Token: 0x04000805 RID: 2053
	public readonly bool HasKeyComparer;

	// Token: 0x04000806 RID: 2054
	public readonly IEqualityComparer<TValue> ValueComparer;

	// Token: 0x04000807 RID: 2055
	public readonly bool HasValueComparer;
}
