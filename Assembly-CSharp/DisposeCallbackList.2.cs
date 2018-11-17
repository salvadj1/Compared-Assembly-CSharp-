using System;
using UnityEngine;

// Token: 0x02000187 RID: 391
public struct DisposeCallbackList<TCallback> : IDisposable where TCallback : class
{
	// Token: 0x06000BB2 RID: 2994 RVA: 0x0002DE24 File Offset: 0x0002C024
	public DisposeCallbackList(global::DisposeCallbackList<Object, TCallback>.Function invoke)
	{
		this.def = new global::DisposeCallbackList<Object, TCallback>(null, invoke);
	}

	// Token: 0x06000BB3 RID: 2995 RVA: 0x0002DE34 File Offset: 0x0002C034
	public bool Add(TCallback callback)
	{
		return this.def.Add(callback);
	}

	// Token: 0x06000BB4 RID: 2996 RVA: 0x0002DE44 File Offset: 0x0002C044
	public bool Remove(TCallback callback)
	{
		return this.def.Remove(callback);
	}

	// Token: 0x06000BB5 RID: 2997 RVA: 0x0002DE54 File Offset: 0x0002C054
	public void Dispose()
	{
		this.def.Dispose();
	}

	// Token: 0x17000337 RID: 823
	// (get) Token: 0x06000BB6 RID: 2998 RVA: 0x0002DE64 File Offset: 0x0002C064
	public static global::DisposeCallbackList<TCallback> invalid
	{
		get
		{
			return default(global::DisposeCallbackList<TCallback>);
		}
	}

	// Token: 0x040007E9 RID: 2025
	private global::DisposeCallbackList<Object, TCallback> def;
}
