using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x020001B2 RID: 434
public struct EventLister<T>
{
	// Token: 0x06000C50 RID: 3152 RVA: 0x00030DF8 File Offset: 0x0002EFF8
	static EventLister()
	{
		if (!typeof(T).IsSubclassOf(typeof(Delegate)))
		{
			throw new InvalidOperationException("T is not a delegate");
		}
		EventListerInvokeAttribute eventListerInvokeAttribute = (EventListerInvokeAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(EventListerInvokeAttribute), false);
		EventLister<T>.InvokeCallType = eventListerInvokeAttribute.InvokeCall;
		EventLister<T>.CalleeType = eventListerInvokeAttribute.InvokeClass;
		MethodInfo method = EventLister<T>.InvokeCallType.GetMethod("Invoke");
		ParameterInfo[] parameters = method.GetParameters();
		Type[] array = new Type[parameters.Length];
		for (int i = 0; i < parameters.Length; i++)
		{
			array[i] = parameters[i].ParameterType;
		}
		EventLister<T>.CalleeMethod = EventLister<T>.CalleeType.GetMethod(eventListerInvokeAttribute.InvokeMember, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, method.CallingConvention, array, null);
		ParameterInfo[] parameters2 = EventLister<T>.CalleeMethod.GetParameters();
		for (int j = 0; j < parameters2.Length; j++)
		{
			if ((parameters2[j].Attributes & parameters[j].Attributes) != parameters[j].Attributes)
			{
				throw new InvalidOperationException("Parameter does not match the InvokeCall " + parameters2[j]);
			}
		}
	}

	// Token: 0x1700031A RID: 794
	// (get) Token: 0x06000C51 RID: 3153 RVA: 0x00030F28 File Offset: 0x0002F128
	public bool empty
	{
		get
		{
			return object.ReferenceEquals(this.node, null);
		}
	}

	// Token: 0x06000C52 RID: 3154 RVA: 0x00030F38 File Offset: 0x0002F138
	public bool Add(T callback)
	{
		if (object.ReferenceEquals(this.node, null))
		{
			this.node = new EventLister<T>.Node(callback);
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

	// Token: 0x06000C53 RID: 3155 RVA: 0x00030FA4 File Offset: 0x0002F1A4
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

	// Token: 0x06000C54 RID: 3156 RVA: 0x0003105C File Offset: 0x0002F25C
	public void Clear()
	{
		this.node = null;
	}

	// Token: 0x06000C55 RID: 3157 RVA: 0x00031068 File Offset: 0x0002F268
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
		EventLister<T>.ExecCall<C> invoke = EventLister<T>.Invocation<C>.Invoke;
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

	// Token: 0x06000C56 RID: 3158 RVA: 0x00031184 File Offset: 0x0002F384
	public void InvokeManual<C>(T callback, C caller)
	{
		try
		{
			EventLister<T>.Invocation<C>.Invoke(caller, callback);
		}
		catch (Exception ex)
		{
			Debug.LogError(ex);
		}
	}

	// Token: 0x06000C57 RID: 3159 RVA: 0x000311CC File Offset: 0x0002F3CC
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
		EventLister<T>.ExecCall<C, D> invoke = EventLister<T>.Invocation<C, D>.Invoke;
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

	// Token: 0x06000C58 RID: 3160 RVA: 0x000312EC File Offset: 0x0002F4EC
	public void InvokeManual<C, D>(T callback, C caller, ref D data)
	{
		try
		{
			EventLister<T>.Invocation<C, D>.Invoke(caller, ref data, callback);
		}
		catch (Exception ex)
		{
			Debug.LogError(ex);
		}
	}

	// Token: 0x0400077B RID: 1915
	public static readonly Type InvokeCallType;

	// Token: 0x0400077C RID: 1916
	public static readonly Type CalleeType;

	// Token: 0x0400077D RID: 1917
	public static readonly MethodInfo CalleeMethod;

	// Token: 0x0400077E RID: 1918
	private EventLister<T>.Node node;

	// Token: 0x020001B3 RID: 435
	internal static class Invocation<C>
	{
		// Token: 0x06000C59 RID: 3161 RVA: 0x00031334 File Offset: 0x0002F534
		static Invocation()
		{
			if (EventLister<T>.InvokeCallType != typeof(EventLister<T>.ExecCall<C>))
			{
				throw new InvalidOperationException(EventLister<T>.InvokeCallType.Name + " should have been used.");
			}
			EventLister<T>.Invocation<C>.Invoke = (EventLister<T>.ExecCall<C>)Delegate.CreateDelegate(typeof(EventLister<T>.ExecCall<C>), EventLister<T>.CalleeMethod);
		}

		// Token: 0x0400077F RID: 1919
		public static readonly EventLister<T>.ExecCall<C> Invoke;
	}

	// Token: 0x020001B4 RID: 436
	internal static class Invocation<C, D>
	{
		// Token: 0x06000C5A RID: 3162 RVA: 0x00031390 File Offset: 0x0002F590
		static Invocation()
		{
			if (EventLister<T>.InvokeCallType != typeof(EventLister<T>.ExecCall<C, D>))
			{
				throw new InvalidOperationException(EventLister<T>.InvokeCallType.Name + " should have been used.");
			}
			EventLister<T>.Invocation<C, D>.Invoke = (EventLister<T>.ExecCall<C, D>)Delegate.CreateDelegate(typeof(EventLister<T>.ExecCall<C, D>), EventLister<T>.CalleeMethod);
		}

		// Token: 0x04000780 RID: 1920
		public static readonly EventLister<T>.ExecCall<C, D> Invoke;
	}

	// Token: 0x020001B5 RID: 437
	private sealed class Node
	{
		// Token: 0x06000C5B RID: 3163 RVA: 0x000313EC File Offset: 0x0002F5EC
		internal Node(T callback)
		{
			this.hashSet.Add(callback);
			this.list.Add(callback);
			this.count = 1;
		}

		// Token: 0x04000781 RID: 1921
		internal readonly HashSet<T> hashSet = new HashSet<T>();

		// Token: 0x04000782 RID: 1922
		internal readonly List<T> list = new List<T>();

		// Token: 0x04000783 RID: 1923
		internal int count;

		// Token: 0x04000784 RID: 1924
		internal int iter;

		// Token: 0x04000785 RID: 1925
		internal bool invoking;
	}

	// Token: 0x02000866 RID: 2150
	// (Invoke) Token: 0x06004B70 RID: 19312
	public delegate void ExecCall<C>(C caller, T callback);

	// Token: 0x02000867 RID: 2151
	// (Invoke) Token: 0x06004B74 RID: 19316
	public delegate void ExecCall<C, D>(C caller, ref D data, T callback);
}
