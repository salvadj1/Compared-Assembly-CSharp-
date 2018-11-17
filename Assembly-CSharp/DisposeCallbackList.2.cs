using System;
using UnityEngine;

// Token: 0x0200015C RID: 348
public struct DisposeCallbackList<TCallback> : IDisposable where TCallback : class
{
	// Token: 0x06000A88 RID: 2696 RVA: 0x0002A0A8 File Offset: 0x000282A8
	public DisposeCallbackList(DisposeCallbackList<Object, TCallback>.Function invoke)
	{
		this.def = new DisposeCallbackList<Object, TCallback>(null, invoke);
	}

	// Token: 0x06000A89 RID: 2697 RVA: 0x0002A0B8 File Offset: 0x000282B8
	public bool Add(TCallback callback)
	{
		return this.def.Add(callback);
	}

	// Token: 0x06000A8A RID: 2698 RVA: 0x0002A0C8 File Offset: 0x000282C8
	public bool Remove(TCallback callback)
	{
		return this.def.Remove(callback);
	}

	// Token: 0x06000A8B RID: 2699 RVA: 0x0002A0D8 File Offset: 0x000282D8
	public void Dispose()
	{
		this.def.Dispose();
	}

	// Token: 0x170002F5 RID: 757
	// (get) Token: 0x06000A8C RID: 2700 RVA: 0x0002A0E8 File Offset: 0x000282E8
	public static DisposeCallbackList<TCallback> invalid
	{
		get
		{
			return default(DisposeCallbackList<TCallback>);
		}
	}

	// Token: 0x040006DA RID: 1754
	private DisposeCallbackList<Object, TCallback> def;
}
