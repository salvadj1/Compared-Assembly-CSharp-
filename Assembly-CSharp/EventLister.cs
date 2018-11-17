using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x020001E0 RID: 480
public struct EventLister<T>
{
	// Token: 0x06000D88 RID: 3464 RVA: 0x00034CE4 File Offset: 0x00032EE4
	static EventLister()
	{
		if (!typeof(T).IsSubclassOf(typeof(Delegate)))
		{
			throw new InvalidOperationException("T is not a delegate");
		}
		global::EventListerInvokeAttribute eventListerInvokeAttribute = (global::EventListerInvokeAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(global::EventListerInvokeAttribute), false);
		global::EventLister<T>.InvokeCallType = eventListerInvokeAttribute.InvokeCall;
		global::EventLister<T>.CalleeType = eventListerInvokeAttribute.InvokeClass;
		MethodInfo method = global::EventLister<T>.InvokeCallType.GetMethod("Invoke");
		ParameterInfo[] parameters = method.GetParameters();
		Type[] array = new Type[parameters.Length];
		for (int i = 0; i < parameters.Length; i++)
		{
			array[i] = parameters[i].ParameterType;
		}
		global::EventLister<T>.CalleeMethod = global::EventLister<T>.CalleeType.GetMethod(eventListerInvokeAttribute.InvokeMember, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, method.CallingConvention, array, null);
		ParameterInfo[] parameters2 = global::EventLister<T>.CalleeMethod.GetParameters();
		for (int j = 0; j < parameters2.Length; j++)
		{
			if ((parameters2[j].Attributes & parameters[j].Attributes) != parameters[j].Attributes)
			{
				throw new InvalidOperationException("Parameter does not match the InvokeCall " + parameters2[j]);
			}
		}
	}

	// Token: 0x1700035E RID: 862
	// (get) Token: 0x06000D89 RID: 3465 RVA: 0x00034E14 File Offset: 0x00033014
	public bool empty
	{
		get
		{
			return object.ReferenceEquals(this.node, null);
		}
	}

	// Token: 0x06000D8A RID: 3466 RVA: 0x00034E24 File Offset: 0x00033024
	public bool Add(T callback)
	{
		if (object.ReferenceEquals(this.node, null))
		{
			this.node = new global::EventLister<T>.Node(callback);
			return true;
		}
		if (this.node.hashSet.Add(callback))
		{
			this.node.list.Add(callback);
			this.node.count++;
			return true;
		}
		return false;
	}

	// Token: 0x06000D8B RID: 3467 RVA: 0x00034E90 File Offset: 0x00033090
	public bool Remove(T callback)
	{
		if (object.ReferenceEquals(this.node, null) || !this.node.hashSet.Remove(callback))
		{
			return false;
		}
		if (--this.node.count == 0 && !this.node.invoking)
		{
			this.node = null;
		}
		else
		{
			int num = this.node.list.IndexOf(callback);
			this.node.list.RemoveAt(num);
			if (this.node.iter > num)
			{
				this.node.iter--;
			}
		}
		return true;
	}

	// Token: 0x06000D8C RID: 3468 RVA: 0x00034F48 File Offset: 0x00033148
	public void Clear()
	{
		this.node = null;
	}

	// Token: 0x06000D8D RID: 3469 RVA: 0x00034F54 File Offset: 0x00033154
	public bool Invoke<C>(C caller)
	{
		if (object.ReferenceEquals(this.node, null))
		{
			return false;
		}
		if (this.node.invoking)
		{
			throw new InvalidOperationException("This lister is invoking already");
		}
		global::EventLister<T>.ExecCall<C> invoke = global::EventLister<T>.Invocation<C>.Invoke;
		try
		{
			this.node.invoking = true;
			this.node.iter = 0;
			while (this.node.iter < this.node.count)
			{
				T callback = this.node.list[this.node.iter++];
				try
				{
					invoke(caller, callback);
				}
				catch (Exception ex)
				{
					Debug.LogError(ex);
				}
			}
		}
		finally
		{
			if (this.node.count == 0)
			{
				this.node = null;
			}
			else
			{
				this.node.invoking = false;
			}
		}
		return true;
	}

	// Token: 0x06000D8E RID: 3470 RVA: 0x00035070 File Offset: 0x00033270
	public void InvokeManual<C>(T callback, C caller)
	{
		try
		{
			global::EventLister<T>.Invocation<C>.Invoke(caller, callback);
		}
		catch (Exception ex)
		{
			Debug.LogError(ex);
		}
	}

	// Token: 0x06000D8F RID: 3471 RVA: 0x000350B8 File Offset: 0x000332B8
	public bool Invoke<C, D>(C caller, ref D data)
	{
		if (object.ReferenceEquals(this.node, null))
		{
			return false;
		}
		if (this.node.invoking)
		{
			throw new InvalidOperationException("This lister is invoking already");
		}
		global::EventLister<T>.ExecCall<C, D> invoke = global::EventLister<T>.Invocation<C, D>.Invoke;
		try
		{
			this.node.invoking = true;
			this.node.iter = 0;
			while (this.node.iter < this.node.count)
			{
				T callback = this.node.list[this.node.iter++];
				try
				{
					invoke(caller, ref data, callback);
				}
				catch (Exception ex)
				{
					Debug.LogError(ex);
				}
			}
		}
		finally
		{
			if (this.node.count == 0)
			{
				this.node = null;
			}
			else
			{
				this.node.invoking = false;
			}
		}
		return true;
	}

	// Token: 0x06000D90 RID: 3472 RVA: 0x000351D8 File Offset: 0x000333D8
	public void InvokeManual<C, D>(T callback, C caller, ref D data)
	{
		try
		{
			global::EventLister<T>.Invocation<C, D>.Invoke(caller, ref data, callback);
		}
		catch (Exception ex)
		{
			Debug.LogError(ex);
		}
	}

	// Token: 0x0400088F RID: 2191
	public static readonly Type InvokeCallType;

	// Token: 0x04000890 RID: 2192
	public static readonly Type CalleeType;

	// Token: 0x04000891 RID: 2193
	public static readonly MethodInfo CalleeMethod;

	// Token: 0x04000892 RID: 2194
	private global::EventLister<T>.Node node;

	// Token: 0x020001E1 RID: 481
	internal static class Invocation<C>
	{
		// Token: 0x06000D91 RID: 3473 RVA: 0x00035220 File Offset: 0x00033420
		static Invocation()
		{
			if (global::EventLister<T>.InvokeCallType != typeof(global::EventLister<T>.ExecCall<C>))
			{
				throw new InvalidOperationException(global::EventLister<T>.InvokeCallType.Name + " should have been used.");
			}
			global::EventLister<T>.Invocation<C>.Invoke = (global::EventLister<T>.ExecCall<C>)Delegate.CreateDelegate(typeof(global::EventLister<T>.ExecCall<C>), global::EventLister<T>.CalleeMethod);
		}

		// Token: 0x04000893 RID: 2195
		public static readonly global::EventLister<T>.ExecCall<C> Invoke;
	}

	// Token: 0x020001E2 RID: 482
	internal static class Invocation<C, D>
	{
		// Token: 0x06000D92 RID: 3474 RVA: 0x0003527C File Offset: 0x0003347C
		static Invocation()
		{
			if (global::EventLister<T>.InvokeCallType != typeof(global::EventLister<T>.ExecCall<C, D>))
			{
				throw new InvalidOperationException(global::EventLister<T>.InvokeCallType.Name + " should have been used.");
			}
			global::EventLister<T>.Invocation<C, D>.Invoke = (global::EventLister<T>.ExecCall<C, D>)Delegate.CreateDelegate(typeof(global::EventLister<T>.ExecCall<C, D>), global::EventLister<T>.CalleeMethod);
		}

		// Token: 0x04000894 RID: 2196
		public static readonly global::EventLister<T>.ExecCall<C, D> Invoke;
	}

	// Token: 0x020001E3 RID: 483
	private sealed class Node
	{
		// Token: 0x06000D93 RID: 3475 RVA: 0x000352D8 File Offset: 0x000334D8
		internal Node(T callback)
		{
			this.hashSet.Add(callback);
			this.list.Add(callback);
			this.count = 1;
		}

		// Token: 0x04000895 RID: 2197
		internal readonly HashSet<T> hashSet = new HashSet<T>();

		// Token: 0x04000896 RID: 2198
		internal readonly List<T> list = new List<T>();

		// Token: 0x04000897 RID: 2199
		internal int count;

		// Token: 0x04000898 RID: 2200
		internal int iter;

		// Token: 0x04000899 RID: 2201
		internal bool invoking;
	}

	// Token: 0x020001E4 RID: 484
	// (Invoke) Token: 0x06000D95 RID: 3477
	public delegate void ExecCall<C>(C caller, T callback);

	// Token: 0x020001E5 RID: 485
	// (Invoke) Token: 0x06000D99 RID: 3481
	public delegate void ExecCall<C, D>(C caller, ref D data, T callback);
}
