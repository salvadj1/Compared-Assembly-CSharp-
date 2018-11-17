using System;
using UnityEngine;

// Token: 0x020001B0 RID: 432
public struct CacheRef<T> where T : Object
{
	// Token: 0x06000C4B RID: 3147 RVA: 0x00030D2C File Offset: 0x0002EF2C
	private CacheRef(T value)
	{
		this.value = value;
		this.existed = value;
		this.cached = true;
	}

	// Token: 0x17000319 RID: 793
	// (get) Token: 0x06000C4C RID: 3148 RVA: 0x00030D50 File Offset: 0x0002EF50
	public bool alive
	{
		get
		{
			return this.existed && (this.existed = this.value);
		}
	}

	// Token: 0x06000C4D RID: 3149 RVA: 0x00030D84 File Offset: 0x0002EF84
	public bool Get(out T value)
	{
		value = this.value;
		return this.cached && this.existed && (this.existed = value);
	}

	// Token: 0x06000C4E RID: 3150 RVA: 0x00030DD0 File Offset: 0x0002EFD0
	public static implicit operator CacheRef<T>(T value)
	{
		return new CacheRef<T>(value);
	}

	// Token: 0x04000775 RID: 1909
	[NonSerialized]
	public T value;

	// Token: 0x04000776 RID: 1910
	[NonSerialized]
	public readonly bool cached;

	// Token: 0x04000777 RID: 1911
	[NonSerialized]
	private bool existed;
}
