using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000185 RID: 389
public struct DisposeCallbackList<TOwner, TCallback> : IDisposable where TOwner : Object where TCallback : class
{
	// Token: 0x06000BA8 RID: 2984 RVA: 0x0002DBF0 File Offset: 0x0002BDF0
	public DisposeCallbackList(TOwner owner, global::DisposeCallbackList<TOwner, TCallback>.Function invoke)
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

	// Token: 0x06000BA9 RID: 2985 RVA: 0x0002DC34 File Offset: 0x0002BE34
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

	// Token: 0x06000BAA RID: 2986 RVA: 0x0002DCD0 File Offset: 0x0002BED0
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

	// Token: 0x06000BAB RID: 2987 RVA: 0x0002DD80 File Offset: 0x0002BF80
	public bool Remove(TCallback value)
	{
		return this.destroyIndex == -1 && this.list != null && this.list.Remove(value);
	}

	// Token: 0x06000BAC RID: 2988 RVA: 0x0002DDB8 File Offset: 0x0002BFB8
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

	// Token: 0x17000336 RID: 822
	// (get) Token: 0x06000BAD RID: 2989 RVA: 0x0002DE0C File Offset: 0x0002C00C
	public static global::DisposeCallbackList<TOwner, TCallback> invalid
	{
		get
		{
			return default(global::DisposeCallbackList<TOwner, TCallback>);
		}
	}

	// Token: 0x040007E4 RID: 2020
	private readonly global::DisposeCallbackList<TOwner, TCallback>.Function function;

	// Token: 0x040007E5 RID: 2021
	private TOwner owner;

	// Token: 0x040007E6 RID: 2022
	private List<TCallback> list;

	// Token: 0x040007E7 RID: 2023
	private int destroyIndex;

	// Token: 0x040007E8 RID: 2024
	private int count;

	// Token: 0x02000186 RID: 390
	// (Invoke) Token: 0x06000BAF RID: 2991
	public delegate void Function(TOwner owner, TCallback callback);
}
