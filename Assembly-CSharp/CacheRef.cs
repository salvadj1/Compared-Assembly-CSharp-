using System;
using UnityEngine;

// Token: 0x020001DE RID: 478
public struct CacheRef<T> where T : Object
{
	// Token: 0x06000D83 RID: 3459 RVA: 0x00034C18 File Offset: 0x00032E18
	private CacheRef(T value)
	{
		this.value = value;
		this.existed = value;
		this.cached = true;
	}

	// Token: 0x1700035D RID: 861
	// (get) Token: 0x06000D84 RID: 3460 RVA: 0x00034C3C File Offset: 0x00032E3C
	public bool alive
	{
		get
		{
			return this.existed && (this.existed = this.value);
		}
	}

	// Token: 0x06000D85 RID: 3461 RVA: 0x00034C70 File Offset: 0x00032E70
	public bool Get(out T value)
	{
		value = this.value;
		return this.cached && this.existed && (this.existed = value);
	}

	// Token: 0x06000D86 RID: 3462 RVA: 0x00034CBC File Offset: 0x00032EBC
	public static implicit operator global::CacheRef<T>(T value)
	{
		return new global::CacheRef<T>(value);
	}

	// Token: 0x04000889 RID: 2185
	[NonSerialized]
	public T value;

	// Token: 0x0400088A RID: 2186
	[NonSerialized]
	public readonly bool cached;

	// Token: 0x0400088B RID: 2187
	[NonSerialized]
	private bool existed;
}
