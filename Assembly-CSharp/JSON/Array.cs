using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace JSON
{
	// Token: 0x02000855 RID: 2133
	public class Array : IEnumerable, IEnumerable<Value>
	{
		// Token: 0x06004B10 RID: 19216 RVA: 0x001482D8 File Offset: 0x001464D8
		public Array()
		{
		}

		// Token: 0x06004B11 RID: 19217 RVA: 0x001482EC File Offset: 0x001464EC
		public Array(Array array)
		{
			this.values = new List<Value>();
			foreach (Value value in array.values)
			{
				this.values.Add(new Value(value));
			}
		}

		// Token: 0x06004B12 RID: 19218 RVA: 0x00148378 File Offset: 0x00146578
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.values.GetEnumerator();
		}

		// Token: 0x06004B13 RID: 19219 RVA: 0x0014838C File Offset: 0x0014658C
		public void Add(Value value)
		{
			this.values.Add(value);
		}

		// Token: 0x17000EAC RID: 3756
		public Value this[int index]
		{
			get
			{
				return this.values[index];
			}
			set
			{
				this.values[index] = value;
			}
		}

		// Token: 0x17000EAD RID: 3757
		// (get) Token: 0x06004B16 RID: 19222 RVA: 0x001483BC File Offset: 0x001465BC
		public int Length
		{
			get
			{
				return this.values.Count;
			}
		}

		// Token: 0x06004B17 RID: 19223 RVA: 0x001483CC File Offset: 0x001465CC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('[');
			foreach (Value value in this.values)
			{
				stringBuilder.Append(value.ToString());
				stringBuilder.Append(',');
			}
			if (this.values.Count > 0)
			{
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
			}
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}

		// Token: 0x06004B18 RID: 19224 RVA: 0x00148480 File Offset: 0x00146680
		public IEnumerator<Value> GetEnumerator()
		{
			return this.values.GetEnumerator();
		}

		// Token: 0x06004B19 RID: 19225 RVA: 0x00148494 File Offset: 0x00146694
		public static Array Parse(string jsonString)
		{
			Object @object = Object.Parse("{ \"array\" :" + jsonString + '}');
			return (@object != null) ? @object.GetValue("array").Array : null;
		}

		// Token: 0x06004B1A RID: 19226 RVA: 0x001484D8 File Offset: 0x001466D8
		public void Clear()
		{
			this.values.Clear();
		}

		// Token: 0x06004B1B RID: 19227 RVA: 0x001484E8 File Offset: 0x001466E8
		public void Remove(int index)
		{
			if (index >= 0 && index < this.values.Count)
			{
				this.values.RemoveAt(index);
			}
			else
			{
				Debug.LogError(string.Concat(new object[]
				{
					"index out of range: ",
					index,
					" (Expected 0 <= index < ",
					this.values.Count,
					")"
				}));
			}
		}

		// Token: 0x06004B1C RID: 19228 RVA: 0x00148564 File Offset: 0x00146764
		public static Array operator +(Array lhs, Array rhs)
		{
			Array array = new Array(lhs);
			foreach (Value value in rhs.values)
			{
				array.Add(value);
			}
			return array;
		}

		// Token: 0x04002C19 RID: 11289
		private readonly List<Value> values = new List<Value>();
	}
}
