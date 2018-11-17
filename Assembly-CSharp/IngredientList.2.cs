using System;
using Facepunch.Hash;

// Token: 0x0200060B RID: 1547
public sealed class IngredientList<DB> : global::IngredientList, IEquatable<global::IngredientList<DB>> where DB : global::Datablock, IComparable<DB>
{
	// Token: 0x06003177 RID: 12663 RVA: 0x000BE584 File Offset: 0x000BC784
	public IngredientList(DB[] unsorted)
	{
		this.unsorted = (unsorted ?? new DB[0]);
		if (unsorted.Length > 255)
		{
			throw new ArgumentException("items in list cannot exceed 255");
		}
		this.sorted = null;
		this.lastToString = null;
	}

	// Token: 0x17000A8B RID: 2699
	// (get) Token: 0x06003178 RID: 12664 RVA: 0x000BE5D4 File Offset: 0x000BC7D4
	public DB[] ordered
	{
		get
		{
			if (this.needReSort)
			{
				this.ReSort();
			}
			return this.sorted;
		}
	}

	// Token: 0x17000A8C RID: 2700
	// (get) Token: 0x06003179 RID: 12665 RVA: 0x000BE5F0 File Offset: 0x000BC7F0
	private bool needReSort
	{
		get
		{
			return this.sorted == null || this.sorted.Length != this.unsorted.Length;
		}
	}

	// Token: 0x0600317A RID: 12666 RVA: 0x000BE618 File Offset: 0x000BC818
	private void ReSort()
	{
		int num = this.unsorted.Length;
		Array.Resize<DB>(ref this.sorted, num);
		Array.Copy(this.unsorted, this.sorted, num);
		if (num > 255)
		{
			throw new InvalidOperationException("There can't be more than 255 ingredients per blueprint");
		}
		Array.Sort<DB>(this.sorted);
	}

	// Token: 0x0600317B RID: 12667 RVA: 0x000BE670 File Offset: 0x000BC870
	public global::IngredientList<DB> EnsureContents(DB[] original)
	{
		if (this.unsorted != original)
		{
			this.sorted = null;
			this.lastToString = null;
			this.madeHashCode = false;
			this.unsorted = original;
		}
		return this;
	}

	// Token: 0x17000A8D RID: 2701
	// (get) Token: 0x0600317C RID: 12668 RVA: 0x000BE69C File Offset: 0x000BC89C
	public uint hashCode
	{
		get
		{
			DB[] ordered;
			if (!this.madeHashCode)
			{
				ordered = this.ordered;
			}
			else
			{
				if (!this.needReSort)
				{
					return this.hash;
				}
				this.ReSort();
				ordered = this.sorted;
			}
			int num = ordered.Length;
			if (num > global::IngredientList.tempHash.Length)
			{
				Array.Resize<int>(ref global::IngredientList.tempHash, num);
			}
			for (int i = 0; i < num; i++)
			{
				global::IngredientList.tempHash[i] = ordered[i].uniqueID;
			}
			this.hash = Facepunch.Hash.MurmurHash2.UINT(global::IngredientList.tempHash, num, 4027449069u);
			this.madeHashCode = true;
			return this.hash;
		}
	}

	// Token: 0x0600317D RID: 12669 RVA: 0x000BE750 File Offset: 0x000BC950
	public override int GetHashCode()
	{
		return (int)this.hashCode;
	}

	// Token: 0x0600317E RID: 12670 RVA: 0x000BE758 File Offset: 0x000BC958
	public override bool Equals(object obj)
	{
		return obj is global::IngredientList<DB> && this.Equals((global::IngredientList<DB>)obj);
	}

	// Token: 0x0600317F RID: 12671 RVA: 0x000BE774 File Offset: 0x000BC974
	private static string Combine(DB[] dbs)
	{
		string[] array = new string[dbs.Length];
		for (int i = 0; i < dbs.Length; i++)
		{
			array[i] = Convert.ToString(dbs[i]);
		}
		return string.Join(",", array);
	}

	// Token: 0x17000A8E RID: 2702
	// (get) Token: 0x06003180 RID: 12672 RVA: 0x000BE7C4 File Offset: 0x000BC9C4
	public string text
	{
		get
		{
			string result;
			if ((result = this.lastToString) == null)
			{
				result = (this.lastToString = global::IngredientList<DB>.Combine(this.ordered));
			}
			return result;
		}
	}

	// Token: 0x06003181 RID: 12673 RVA: 0x000BE7F4 File Offset: 0x000BC9F4
	public override string ToString()
	{
		return string.Format("[IngredientList<{0}>[{1}]{2:X}:{3}]", new object[]
		{
			typeof(DB).Name,
			this.unsorted.Length,
			this.hashCode,
			this.text
		});
	}

	// Token: 0x06003182 RID: 12674 RVA: 0x000BE84C File Offset: 0x000BCA4C
	public bool Equals(global::IngredientList<DB> other)
	{
		if (object.ReferenceEquals(other, this))
		{
			return true;
		}
		if (other != null && this.unsorted.Length == other.unsorted.Length && this.hashCode == other.hashCode)
		{
			DB[] array = this.sorted;
			DB[] array2 = other.sorted;
			int i = 0;
			int num = array.Length;
			while (i < num)
			{
				if (array[i] != array2[i])
				{
					return false;
				}
				if (--num <= i)
				{
					break;
				}
				if (array[num] != array2[num])
				{
					return false;
				}
				i++;
			}
			return true;
		}
		return false;
	}

	// Token: 0x04001AD5 RID: 6869
	public DB[] unsorted;

	// Token: 0x04001AD6 RID: 6870
	private DB[] sorted;

	// Token: 0x04001AD7 RID: 6871
	private bool madeHashCode;

	// Token: 0x04001AD8 RID: 6872
	private uint hash;

	// Token: 0x04001AD9 RID: 6873
	private string lastToString;
}
