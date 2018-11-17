using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200015B RID: 347
public struct DisposeCallbackList<TOwner, TCallback> : IDisposable where TOwner : Object where TCallback : class
{
	// Token: 0x06000A82 RID: 2690 RVA: 0x00029E74 File Offset: 0x00028074
	public DisposeCallbackList(TOwner owner, DisposeCallbackList<TOwner, TCallback>.Function invoke)
	{
		if (invoke == null)
		{
			throw new ArgumentNullException("invoke");
		}
		this.function = invoke;
		this.list = null;
		this.destroyIndex = -1;
		this.count = 0;
		this.owner = owner;
	}

	// Token: 0x06000A83 RID: 2691 RVA: 0x00029EB8 File Offset: 0x000280B8
	private void Invoke(TCallback value)
	{
		try
		{
			this.function(this.owner, this.list[this.destroyIndex]);
		}
		catch (Exception ex)
		{
			Debug.LogError(string.Format("There was a exception thrown while attempting to invoke '{0}' thru '{1}' via owner '{2}'. exception is below\r\n{3}", new object[]
			{
				value,
				this.function,
				this.owner,
				ex
			}), this.owner);
		}
	}

	// Token: 0x06000A84 RID: 2692 RVA: 0x00029F54 File Offset: 0x00028154
	public bool Add(TCallback value)
	{
		if (this.list == null)
		{
			this.list = new List<TCallback>();
		}
		else
		{
			int num = this.list.IndexOf(value);
			if (num != -1)
			{
				if (this.destroyIndex < num && this.count - 1 != num)
				{
					this.list.RemoveAt(num);
					this.list.Add(value);
				}
				return false;
			}
		}
		this.list.Add(value);
		if (this.destroyIndex == this.count++)
		{
			this.Invoke(value);
			this.destroyIndex++;
		}
		return true;
	}

	// Token: 0x06000A85 RID: 2693 RVA: 0x0002A004 File Offset: 0x00028204
	public bool Remove(TCallback value)
	{
		return this.destroyIndex == -1 && this.list != null && this.list.Remove(value);
	}

	// Token: 0x06000A86 RID: 2694 RVA: 0x0002A03C File Offset: 0x0002823C
	public void Dispose()
	{
		if (this.destroyIndex == -1)
		{
			while (++this.destroyIndex < this.count)
			{
				this.Invoke(this.list[this.destroyIndex]);
			}
		}
	}

	// Token: 0x170002F4 RID: 756
	// (get) Token: 0x06000A87 RID: 2695 RVA: 0x0002A090 File Offset: 0x00028290
	public static DisposeCallbackList<TOwner, TCallback> invalid
	{
		get
		{
			return default(DisposeCallbackList<TOwner, TCallback>);
		}
	}

	// Token: 0x040006D5 RID: 1749
	private readonly DisposeCallbackList<TOwner, TCallback>.Function function;

	// Token: 0x040006D6 RID: 1750
	private TOwner owner;

	// Token: 0x040006D7 RID: 1751
	private List<TCallback> list;

	// Token: 0x040006D8 RID: 1752
	private int destroyIndex;

	// Token: 0x040006D9 RID: 1753
	private int count;

	// Token: 0x02000863 RID: 2147
	// (Invoke) Token: 0x06004B64 RID: 19300
	public delegate void Function(TOwner owner, TCallback callback);
}
