using System;
using Facepunch.Hash;

// Token: 0x0200054D RID: 1357
public sealed class IngredientList<DB> : IngredientList, IEquatable<IngredientList<DB>> where DB : Datablock, IComparable<DB>
{
	// Token: 0x06002DAF RID: 11695 RVA: 0x000B6328 File Offset: 0x000B4528
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

	// Token: 0x17000A15 RID: 2581
	// (get) Token: 0x06002DB0 RID: 11696 RVA: 0x000B6378 File Offset: 0x000B4578
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

	// Token: 0x17000A16 RID: 2582
	// (get) Token: 0x06002DB1 RID: 11697 RVA: 0x000B6394 File Offset: 0x000B4594
	private bool needReSort
	{
		get
		{
			return this.sorted == null || this.sorted.Length != this.unsorted.Length;
		}
	}

	// Token: 0x06002DB2 RID: 11698 RVA: 0x000B63BC File Offset: 0x000B45BC
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

	// Token: 0x06002DB3 RID: 11699 RVA: 0x000B6414 File Offset: 0x000B4614
	public IngredientList<DB> EnsureContents(DB[] original)
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

	// Token: 0x17000A17 RID: 2583
	// (get) Token: 0x06002DB4 RID: 11700 RVA: 0x000B6440 File Offset: 0x000B4640
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
			if (num > IngredientList.tempHash.Length)
			{
				Array.Resize<int>(ref IngredientList.tempHash, num);
			}
			for (int i = 0; i < num; i++)
			{
				IngredientList.tempHash[i] = ordered[i].uniqueID;
			}
			this.hash = MurmurHash2.UINT(IngredientList.tempHash, num, 4027449069u);
			this.madeHashCode = true;
			return this.hash;
		}
	}

	// Token: 0x06002DB5 RID: 11701 RVA: 0x000B64F4 File Offset: 0x000B46F4
	public override int GetHashCode()
	{
		return (int)this.hashCode;
	}

	// Token: 0x06002DB6 RID: 11702 RVA: 0x000B64FC File Offset: 0x000B46FC
	public override bool Equals(object obj)
	{
		return obj is IngredientList<DB> && this.Equals((IngredientList<DB>)obj);
	}

	// Token: 0x06002DB7 RID: 11703 RVA: 0x000B6518 File Offset: 0x000B4718
	private static string Combine(DB[] dbs)
	{
		string[] array = new string[dbs.Length];
		for (int i = 0; i < dbs.Length; i++)
		{
			array[i] = Convert.ToString(dbs[i]);
		}
		return string.Join(",", array);
	}

	// Token: 0x17000A18 RID: 2584
	// (get) Token: 0x06002DB8 RID: 11704 RVA: 0x000B6568 File Offset: 0x000B4768
	public string text
	{
		get
		{
			string result;
			if ((result = this.lastToString) == null)
			{
				result = (this.lastToString = IngredientList<DB>.Combine(this.ordered));
			}
			return result;
		}
	}

	// Token: 0x06002DB9 RID: 11705 RVA: 0x000B6598 File Offset: 0x000B4798
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

	// Token: 0x06002DBA RID: 11706 RVA: 0x000B65F0 File Offset: 0x000B47F0
	public bool Equals(IngredientList<DB> other)
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

	// Token: 0x04001904 RID: 6404
	public DB[] unsorted;

	// Token: 0x04001905 RID: 6405
	private DB[] sorted;

	// Token: 0x04001906 RID: 6406
	private bool madeHashCode;

	// Token: 0x04001907 RID: 6407
	private uint hash;

	// Token: 0x04001908 RID: 6408
	private string lastToString;
}
