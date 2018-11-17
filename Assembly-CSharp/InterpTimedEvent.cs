using System;
using System.Collections.Generic;
using uLink;
using UnityEngine;

// Token: 0x020002AC RID: 684
public sealed class InterpTimedEvent : IDisposable
{
	// Token: 0x06001877 RID: 6263 RVA: 0x00060950 File Offset: 0x0005EB50
	private InterpTimedEvent()
	{
	}

	// Token: 0x17000716 RID: 1814
	// (get) Token: 0x06001879 RID: 6265 RVA: 0x00060968 File Offset: 0x0005EB68
	public static int ArgumentCount
	{
		get
		{
			return InterpTimedEvent.current.args.length;
		}
	}

	// Token: 0x17000717 RID: 1815
	// (get) Token: 0x0600187A RID: 6266 RVA: 0x0006097C File Offset: 0x0005EB7C
	public static MonoBehaviour Target
	{
		get
		{
			return InterpTimedEvent.current.component;
		}
	}

	// Token: 0x17000718 RID: 1816
	// (get) Token: 0x0600187B RID: 6267 RVA: 0x00060988 File Offset: 0x0005EB88
	public static string Tag
	{
		get
		{
			return InterpTimedEvent.current.tag;
		}
	}

	// Token: 0x0600187C RID: 6268 RVA: 0x00060994 File Offset: 0x0005EB94
	public static object Argument(int index)
	{
		return InterpTimedEvent.current.args.parameters[index];
	}

	// Token: 0x17000719 RID: 1817
	// (get) Token: 0x0600187D RID: 6269 RVA: 0x000609A8 File Offset: 0x0005EBA8
	public static NetworkMessageInfo Info
	{
		get
		{
			return InterpTimedEvent.current.info;
		}
	}

	// Token: 0x1700071A RID: 1818
	// (get) Token: 0x0600187E RID: 6270 RVA: 0x000609B4 File Offset: 0x0005EBB4
	public static double Timestamp
	{
		get
		{
			return InterpTimedEvent.current.info.timestamp;
		}
	}

	// Token: 0x1700071B RID: 1819
	// (get) Token: 0x0600187F RID: 6271 RVA: 0x000609C8 File Offset: 0x0005EBC8
	public static ulong TimestampInMilliseconds
	{
		get
		{
			return InterpTimedEvent.current.info.timestampInMillis;
		}
	}

	// Token: 0x1700071C RID: 1820
	// (get) Token: 0x06001880 RID: 6272 RVA: 0x000609DC File Offset: 0x0005EBDC
	public static NetworkPlayer Sender
	{
		get
		{
			return InterpTimedEvent.current.info.sender;
		}
	}

	// Token: 0x1700071D RID: 1821
	// (get) Token: 0x06001881 RID: 6273 RVA: 0x000609F0 File Offset: 0x0005EBF0
	public static NetworkView NetworkView
	{
		get
		{
			return InterpTimedEvent.current.info.networkView;
		}
	}

	// Token: 0x1700071E RID: 1822
	// (get) Token: 0x06001882 RID: 6274 RVA: 0x00060A04 File Offset: 0x0005EC04
	public static NetworkFlags Flags
	{
		get
		{
			return InterpTimedEvent.current.info.flags;
		}
	}

	// Token: 0x06001883 RID: 6275 RVA: 0x00060A18 File Offset: 0x0005EC18
	public static Type ArgumentType(int index)
	{
		return InterpTimedEvent.current.args.types[index];
	}

	// Token: 0x1700071F RID: 1823
	// (get) Token: 0x06001884 RID: 6276 RVA: 0x00060A2C File Offset: 0x0005EC2C
	public static object[] ArgumentArray
	{
		get
		{
			return InterpTimedEvent.current.args.parameters;
		}
	}

	// Token: 0x17000720 RID: 1824
	// (get) Token: 0x06001885 RID: 6277 RVA: 0x00060A40 File Offset: 0x0005EC40
	public static Type[] ArgumentTypeArray
	{
		get
		{
			return InterpTimedEvent.current.args.types;
		}
	}

	// Token: 0x06001886 RID: 6278 RVA: 0x00060A54 File Offset: 0x0005EC54
	public static T Argument<T>(int index)
	{
		Type type = InterpTimedEvent.current.args.types[index];
		if (!typeof(T).IsAssignableFrom(type) && (typeof(void) != type || typeof(T).IsValueType))
		{
			throw new InvalidCastException(string.Format("Argument #{0} was a {1} and {2} is not assignable by {1}", index, InterpTimedEvent.current.args.types[index], typeof(T)));
		}
		return (T)((object)InterpTimedEvent.current.args.parameters[index]);
	}

	// Token: 0x06001887 RID: 6279 RVA: 0x00060AF4 File Offset: 0x0005ECF4
	public static bool ArgumentIs<T>(int index)
	{
		Type type = InterpTimedEvent.current.args.types[index];
		return typeof(T).IsAssignableFrom(type) || (type == typeof(void) && !typeof(T).IsValueType);
	}

	// Token: 0x06001888 RID: 6280 RVA: 0x00060B50 File Offset: 0x0005ED50
	public static bool ArgumentIs(int index, Type comptype)
	{
		Type type = InterpTimedEvent.current.args.types[index];
		return comptype.IsAssignableFrom(InterpTimedEvent.current.args.types[index]) || (type == typeof(void) && !comptype.IsValueType);
	}

	// Token: 0x06001889 RID: 6281 RVA: 0x00060BAC File Offset: 0x0005EDAC
	public static void MarkUnhandled()
	{
		Debug.LogWarning("Unhandled Timed Event :" + ((!string.IsNullOrEmpty(InterpTimedEvent.current.tag)) ? InterpTimedEvent.current.tag : " without a tag"), InterpTimedEvent.current.component);
	}

	// Token: 0x17000721 RID: 1825
	// (get) Token: 0x0600188A RID: 6282 RVA: 0x00060BFC File Offset: 0x0005EDFC
	// (set) Token: 0x0600188B RID: 6283 RVA: 0x00060C04 File Offset: 0x0005EE04
	public static bool syncronizationPaused
	{
		get
		{
			return InterpTimedEventSyncronizer.paused;
		}
		set
		{
			InterpTimedEventSyncronizer.paused = value;
		}
	}

	// Token: 0x17000722 RID: 1826
	// (get) Token: 0x0600188C RID: 6284 RVA: 0x00060C0C File Offset: 0x0005EE0C
	public IInterpTimedEventReceiver receiver
	{
		get
		{
			return this.component as IInterpTimedEventReceiver;
		}
	}

	// Token: 0x0600188D RID: 6285 RVA: 0x00060C1C File Offset: 0x0005EE1C
	public static void EmergencyDump()
	{
	}

	// Token: 0x0600188E RID: 6286 RVA: 0x00060C20 File Offset: 0x0005EE20
	public void Dispose()
	{
		if (this.inlist)
		{
			InterpTimedEvent.queue.Remove(this);
		}
		if (!this.disposed)
		{
			this.prev = default(InterpTimedEvent.Dir);
			this.next = InterpTimedEvent.dump;
			InterpTimedEvent.dump.has = true;
			InterpTimedEvent.dump.node = this;
			this.component = null;
			this.args.Dispose();
			this.args = null;
			this.info = null;
			this.tag = null;
			InterpTimedEvent.dumpCount++;
			this.disposed = true;
		}
	}

	// Token: 0x0600188F RID: 6287 RVA: 0x00060CBC File Offset: 0x0005EEBC
	private static void InvokeDirect(InterpTimedEvent evnt)
	{
		InterpTimedEvent interpTimedEvent = InterpTimedEvent.current;
		InterpTimedEvent.current = evnt;
		InterpTimedEvent.Invoke();
		InterpTimedEvent.current = interpTimedEvent;
	}

	// Token: 0x06001890 RID: 6288 RVA: 0x00060CE0 File Offset: 0x0005EEE0
	private static void Invoke()
	{
		MonoBehaviour monoBehaviour = InterpTimedEvent.current.component;
		if (monoBehaviour)
		{
			IInterpTimedEventReceiver interpTimedEventReceiver = monoBehaviour as IInterpTimedEventReceiver;
			try
			{
				interpTimedEventReceiver.OnInterpTimedEvent();
			}
			catch (Exception arg)
			{
				Debug.LogError("Exception thrown during catchup \r\n" + arg, monoBehaviour);
			}
		}
		else
		{
			Debug.LogWarning("A component implementing IInterpTimeEventReceiver was destroyed without properly calling InterpEvent.Remove() in OnDestroy!\r\n" + ((!string.IsNullOrEmpty(InterpTimedEvent.current.tag)) ? ("The tag was \"" + InterpTimedEvent.current.tag + "\"") : "There was no tag set"));
		}
		InterpTimedEvent.current.Dispose();
	}

	// Token: 0x06001891 RID: 6289 RVA: 0x00060DA0 File Offset: 0x0005EFA0
	public static void Catchup()
	{
		InterpTimedEvent.Catchup(Interpolation.timeInMillis);
	}

	// Token: 0x06001892 RID: 6290 RVA: 0x00060DAC File Offset: 0x0005EFAC
	public static void ForceCatchupToDate()
	{
		InterpTimedEvent._forceCatchupToDate = true;
	}

	// Token: 0x06001893 RID: 6291 RVA: 0x00060DB4 File Offset: 0x0005EFB4
	public static void Catchup(ulong playhead)
	{
		InterpTimedEvent._forceCatchupToDate = false;
		while (InterpTimedEvent.queue.Dequeue(playhead, out InterpTimedEvent.current))
		{
			InterpTimedEvent.Invoke();
		}
	}

	// Token: 0x06001894 RID: 6292 RVA: 0x00060DDC File Offset: 0x0005EFDC
	public static void Clear()
	{
		InterpTimedEvent.Clear(false);
	}

	// Token: 0x06001895 RID: 6293 RVA: 0x00060DE4 File Offset: 0x0005EFE4
	public static void Clear(bool invokePending)
	{
		InterpTimedEvent.LList.Iterator iterator = default(InterpTimedEvent.LList.Iterator);
		if (invokePending)
		{
			InterpTimedEvent interpTimedEvent;
			while (InterpTimedEvent.queue.Dequeue(18446744073709551615UL, out interpTimedEvent, ref iterator))
			{
				InterpTimedEvent.InvokeDirect(interpTimedEvent);
			}
		}
		else
		{
			InterpTimedEvent interpTimedEvent;
			while (InterpTimedEvent.queue.Dequeue(18446744073709551615UL, out interpTimedEvent, ref iterator))
			{
				interpTimedEvent.Dispose();
			}
		}
	}

	// Token: 0x06001896 RID: 6294 RVA: 0x00060E48 File Offset: 0x0005F048
	public static void Remove(MonoBehaviour receiver)
	{
		InterpTimedEvent.Remove(receiver, false);
	}

	// Token: 0x06001897 RID: 6295 RVA: 0x00060E54 File Offset: 0x0005F054
	public static void Remove(MonoBehaviour receiver, bool invokePending)
	{
		InterpTimedEvent.LList.Iterator iterator = default(InterpTimedEvent.LList.Iterator);
		if (invokePending)
		{
			InterpTimedEvent interpTimedEvent;
			while (InterpTimedEvent.queue.Dequeue(receiver, 18446744073709551615UL, out interpTimedEvent, ref iterator))
			{
				InterpTimedEvent.InvokeDirect(interpTimedEvent);
			}
		}
		else
		{
			InterpTimedEvent interpTimedEvent;
			while (InterpTimedEvent.queue.Dequeue(receiver, 18446744073709551615UL, out interpTimedEvent, ref iterator))
			{
				interpTimedEvent.Dispose();
			}
		}
	}

	// Token: 0x06001898 RID: 6296 RVA: 0x00060EB8 File Offset: 0x0005F0B8
	internal static InterpTimedEvent New(MonoBehaviour receiver, string tag, ref NetworkMessageInfo info, object[] args, bool immediate)
	{
		if (!receiver)
		{
			Debug.LogError("receiver is null or has been destroyed", receiver);
			return null;
		}
		if (!(receiver is IInterpTimedEventReceiver))
		{
			Debug.LogError("receiver of type " + receiver.GetType() + " does not implement IInterpTimedEventReceiver", receiver);
			return null;
		}
		InterpTimedEvent interpTimedEvent;
		if (InterpTimedEvent.dump.has)
		{
			InterpTimedEvent.dumpCount--;
			interpTimedEvent = InterpTimedEvent.dump.node;
			InterpTimedEvent.dump = interpTimedEvent.next;
			interpTimedEvent.next = default(InterpTimedEvent.Dir);
			interpTimedEvent.prev = default(InterpTimedEvent.Dir);
			interpTimedEvent.disposed = false;
		}
		else
		{
			interpTimedEvent = new InterpTimedEvent();
		}
		interpTimedEvent.args = InterpTimedEvent.ArgList.New(args);
		interpTimedEvent.tag = tag;
		interpTimedEvent.component = receiver;
		interpTimedEvent.info = info;
		if (!immediate)
		{
			InterpTimedEvent.queue.Insert(interpTimedEvent);
		}
		return interpTimedEvent;
	}

	// Token: 0x06001899 RID: 6297 RVA: 0x00060F9C File Offset: 0x0005F19C
	public static bool Queue(IInterpTimedEventReceiver receiver, string tag, ref NetworkMessageInfo info)
	{
		return InterpTimedEvent.QueueOrExecute(receiver, false, tag, ref info, InterpTimedEvent.emptyArgs);
	}

	// Token: 0x0600189A RID: 6298 RVA: 0x00060FAC File Offset: 0x0005F1AC
	public static bool Queue(IInterpTimedEventReceiver receiver, string tag, ref NetworkMessageInfo info, params object[] args)
	{
		return InterpTimedEvent.QueueOrExecute(receiver, false, tag, ref info, args);
	}

	// Token: 0x0600189B RID: 6299 RVA: 0x00060FB8 File Offset: 0x0005F1B8
	public static bool Execute(IInterpTimedEventReceiver receiver, string tag, ref NetworkMessageInfo info)
	{
		return InterpTimedEvent.QueueOrExecute(receiver, true, tag, ref info, InterpTimedEvent.emptyArgs);
	}

	// Token: 0x0600189C RID: 6300 RVA: 0x00060FC8 File Offset: 0x0005F1C8
	public static bool Execute(IInterpTimedEventReceiver receiver, string tag, ref NetworkMessageInfo info, params object[] args)
	{
		return InterpTimedEvent.QueueOrExecute(receiver, true, tag, ref info, args);
	}

	// Token: 0x0600189D RID: 6301 RVA: 0x00060FD4 File Offset: 0x0005F1D4
	public static bool QueueOrExecute(IInterpTimedEventReceiver receiver, bool immediate, string tag, ref NetworkMessageInfo info)
	{
		return InterpTimedEvent.QueueOrExecute(receiver, immediate, tag, ref info, InterpTimedEvent.emptyArgs);
	}

	// Token: 0x0600189E RID: 6302 RVA: 0x00060FE4 File Offset: 0x0005F1E4
	public static bool QueueOrExecute(IInterpTimedEventReceiver receiver, bool immediate, string tag, ref NetworkMessageInfo info, params object[] args)
	{
		MonoBehaviour receiver2 = receiver as MonoBehaviour;
		InterpTimedEvent interpTimedEvent = InterpTimedEvent.New(receiver2, tag, ref info, args, immediate);
		if (interpTimedEvent == null)
		{
			return false;
		}
		if (immediate)
		{
			InterpTimedEvent.InvokeDirect(interpTimedEvent);
		}
		else if (!InterpTimedEventSyncronizer.available)
		{
			Debug.LogWarning("Not running event because theres no syncronizer available. " + tag, receiver as Object);
			return false;
		}
		return true;
	}

	// Token: 0x0600189F RID: 6303 RVA: 0x00061040 File Offset: 0x0005F240
	public static void EMERGENCY_DUMP(bool TRY_TO_EXECUTE)
	{
		Debug.LogWarning("RUNNING EMERGENCY DUMP: TRY TO EXECUTE=" + TRY_TO_EXECUTE);
		try
		{
			if (TRY_TO_EXECUTE)
			{
				try
				{
					List<InterpTimedEvent> list = InterpTimedEvent.queue.EmergencyDump(true);
					foreach (InterpTimedEvent interpTimedEvent in list)
					{
						try
						{
							InterpTimedEvent.InvokeDirect(interpTimedEvent);
						}
						catch (Exception ex)
						{
							Debug.LogException(ex);
						}
						finally
						{
							try
							{
								interpTimedEvent.Dispose();
							}
							catch (Exception ex2)
							{
								Debug.LogException(ex2);
							}
						}
					}
				}
				catch (Exception ex3)
				{
					Debug.LogException(ex3);
				}
			}
			else
			{
				InterpTimedEvent.queue.EmergencyDump(false);
			}
		}
		catch (Exception ex4)
		{
			Debug.LogException(ex4);
		}
		finally
		{
			InterpTimedEvent.queue = default(InterpTimedEvent.LList);
			InterpTimedEvent.dump = default(InterpTimedEvent.Dir);
			InterpTimedEvent.dumpCount = 0;
		}
		Debug.LogWarning("END OF EMERGENCY DUMP: TRY TO EXECUTE=" + TRY_TO_EXECUTE);
	}

	// Token: 0x04000D07 RID: 3335
	internal static InterpTimedEvent current;

	// Token: 0x04000D08 RID: 3336
	public MonoBehaviour component;

	// Token: 0x04000D09 RID: 3337
	public NetworkMessageInfo info;

	// Token: 0x04000D0A RID: 3338
	public InterpTimedEvent.ArgList args;

	// Token: 0x04000D0B RID: 3339
	public string tag;

	// Token: 0x04000D0C RID: 3340
	private bool disposed;

	// Token: 0x04000D0D RID: 3341
	private bool inlist;

	// Token: 0x04000D0E RID: 3342
	private static int dumpCount;

	// Token: 0x04000D0F RID: 3343
	private static InterpTimedEvent.Dir dump;

	// Token: 0x04000D10 RID: 3344
	private static InterpTimedEvent.LList queue;

	// Token: 0x04000D11 RID: 3345
	internal InterpTimedEvent.Dir next;

	// Token: 0x04000D12 RID: 3346
	internal InterpTimedEvent.Dir prev;

	// Token: 0x04000D13 RID: 3347
	private static bool _forceCatchupToDate;

	// Token: 0x04000D14 RID: 3348
	private static readonly object[] emptyArgs = new object[0];

	// Token: 0x020002AD RID: 685
	public sealed class ArgList : IDisposable
	{
		// Token: 0x060018A0 RID: 6304 RVA: 0x000611F4 File Offset: 0x0005F3F4
		private ArgList(int length)
		{
			this.length = length;
			this.parameters = new object[length];
			this.types = new Type[length];
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x00061234 File Offset: 0x0005F434
		private void AddToDump(ref InterpTimedEvent.ArgList.Dump dump)
		{
			this.dumpNext = dump.last;
			dump.count++;
			dump.last = this;
		}

		// Token: 0x060018A3 RID: 6307 RVA: 0x00061258 File Offset: 0x0005F458
		public void Dispose()
		{
			if (!this.disposed && this.length != 0)
			{
				for (int i = 0; i < this.length; i++)
				{
					this.types[i] = null;
					this.parameters[i] = null;
				}
				if (InterpTimedEvent.ArgList.dumps.Length <= this.length)
				{
					Array.Resize<InterpTimedEvent.ArgList.Dump>(ref InterpTimedEvent.ArgList.dumps, this.length + 1);
				}
				this.AddToDump(ref InterpTimedEvent.ArgList.dumps[this.length]);
				this.disposed = true;
			}
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x000612E8 File Offset: 0x0005F4E8
		private static InterpTimedEvent.ArgList Recycle(ref InterpTimedEvent.ArgList.Dump dump, int length)
		{
			if (dump.count > 0)
			{
				InterpTimedEvent.ArgList last = dump.last;
				dump.last = last.dumpNext;
				dump.count--;
				last.dumpNext = null;
				last.disposed = false;
				return last;
			}
			return new InterpTimedEvent.ArgList(length);
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x00061338 File Offset: 0x0005F538
		public static InterpTimedEvent.ArgList New(object[] args)
		{
			int num = (args != null) ? args.Length : 0;
			if (num == 0)
			{
				return InterpTimedEvent.ArgList.voidParameters;
			}
			InterpTimedEvent.ArgList argList;
			if (InterpTimedEvent.ArgList.dumps.Length > num)
			{
				argList = InterpTimedEvent.ArgList.Recycle(ref InterpTimedEvent.ArgList.dumps[num], num);
			}
			else
			{
				argList = new InterpTimedEvent.ArgList(num);
			}
			for (int i = 0; i < num; i++)
			{
				object obj = args[i];
				argList.parameters[i] = obj;
				argList.types[i] = ((obj != null) ? obj.GetType() : typeof(void));
			}
			return argList;
		}

		// Token: 0x04000D15 RID: 3349
		public readonly object[] parameters;

		// Token: 0x04000D16 RID: 3350
		public readonly Type[] types;

		// Token: 0x04000D17 RID: 3351
		public readonly int length;

		// Token: 0x04000D18 RID: 3352
		private bool disposed;

		// Token: 0x04000D19 RID: 3353
		private static InterpTimedEvent.ArgList voidParameters = new InterpTimedEvent.ArgList(0);

		// Token: 0x04000D1A RID: 3354
		private static InterpTimedEvent.ArgList.Dump[] dumps = new InterpTimedEvent.ArgList.Dump[4];

		// Token: 0x04000D1B RID: 3355
		private InterpTimedEvent.ArgList dumpNext;

		// Token: 0x020002AE RID: 686
		private struct Dump
		{
			// Token: 0x04000D1C RID: 3356
			public InterpTimedEvent.ArgList last;

			// Token: 0x04000D1D RID: 3357
			public int count;
		}
	}

	// Token: 0x020002AF RID: 687
	internal struct Dir
	{
		// Token: 0x04000D1E RID: 3358
		public bool has;

		// Token: 0x04000D1F RID: 3359
		public InterpTimedEvent node;
	}

	// Token: 0x020002B0 RID: 688
	internal struct LList
	{
		// Token: 0x060018A6 RID: 6310 RVA: 0x000613D0 File Offset: 0x0005F5D0
		public bool Dequeue(ulong playhead, out InterpTimedEvent node)
		{
			InterpTimedEvent.LList.Iterator iterator = default(InterpTimedEvent.LList.Iterator);
			return this.Dequeue(playhead, out node, ref iterator);
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x000613F4 File Offset: 0x0005F5F4
		public bool Dequeue(ulong playhead, out InterpTimedEvent node, ref InterpTimedEvent.LList.Iterator iter_)
		{
			if (this.count <= 0)
			{
				node = null;
				return false;
			}
			InterpTimedEvent.Dir dir = (!iter_.started) ? this.first : iter_.d;
			if (dir.has)
			{
				if (playhead >= dir.node.info.timestamp)
				{
					node = dir.node;
					iter_.d = node.next;
					iter_.started = true;
					this.Remove(node);
					return true;
				}
			}
			iter_.d = default(InterpTimedEvent.Dir);
			iter_.started = true;
			node = null;
			return false;
		}

		// Token: 0x060018A8 RID: 6312 RVA: 0x000614A0 File Offset: 0x0005F6A0
		public bool Dequeue(MonoBehaviour script, ulong playhead, out InterpTimedEvent node)
		{
			InterpTimedEvent.LList.Iterator iterator = default(InterpTimedEvent.LList.Iterator);
			return this.Dequeue(script, playhead, out node, ref iterator);
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x000614C4 File Offset: 0x0005F6C4
		public bool Dequeue(MonoBehaviour script, ulong playhead, out InterpTimedEvent node, ref InterpTimedEvent.LList.Iterator iter_)
		{
			if (this.count <= 0)
			{
				node = null;
				return false;
			}
			InterpTimedEvent.Dir dir = (!iter_.started) ? this.first : iter_.d;
			while (dir.has)
			{
				if (playhead < dir.node.info.timestamp)
				{
					break;
				}
				if (dir.node.component == script)
				{
					node = dir.node;
					iter_.d = node.next;
					iter_.started = true;
					this.Remove(node);
					return true;
				}
				dir = dir.node.next;
			}
			iter_.d = default(InterpTimedEvent.Dir);
			iter_.started = true;
			node = null;
			return false;
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x0006159C File Offset: 0x0005F79C
		internal bool Remove(InterpTimedEvent node)
		{
			if (this.RemoveUnsafe(node))
			{
				if (this.FAIL_SAFE_SET != null)
				{
					this.FAIL_SAFE_SET.Remove(node);
				}
				return true;
			}
			return false;
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x000615C8 File Offset: 0x0005F7C8
		private bool RemoveUnsafe(InterpTimedEvent node)
		{
			if (this.count > 0 && node != null && node.inlist)
			{
				if (node.prev.has)
				{
					if (node.next.has)
					{
						node.next.node.prev = node.prev;
						node.prev.node.next = node.next;
						this.count--;
						node.prev = (node.next = default(InterpTimedEvent.Dir));
						node.inlist = false;
						return true;
					}
					this.last = node.prev;
					this.last.node.next = default(InterpTimedEvent.Dir);
					this.count--;
					node.prev = (node.next = default(InterpTimedEvent.Dir));
					node.inlist = false;
					return true;
				}
				else
				{
					if (node.next.has)
					{
						this.first = node.next;
						this.first.node.prev = default(InterpTimedEvent.Dir);
						this.count--;
						node.prev = (node.next = default(InterpTimedEvent.Dir));
						node.inlist = false;
						return true;
					}
					if (this.first.node == node)
					{
						this.first = (this.last = default(InterpTimedEvent.Dir));
						this.count = 0;
						node.prev = (node.next = default(InterpTimedEvent.Dir));
						node.inlist = false;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060018AC RID: 6316 RVA: 0x00061780 File Offset: 0x0005F980
		private bool Insert(ref InterpTimedEvent.Dir ent)
		{
			if (ent.node == null)
			{
				return false;
			}
			if (ent.node.inlist)
			{
				return false;
			}
			if (this.count == 0)
			{
				this.first = (this.last = ent);
			}
			else if (this.last.node.info.timestampInMillis <= ent.node.info.timestampInMillis)
			{
				if (this.count == 1)
				{
					this.first = this.last;
					this.last = ent;
					ent.node.prev = this.first;
					this.first.node.next = this.last;
				}
				else
				{
					ent.node.prev = this.last;
					this.last.node.next = ent;
					this.last = ent;
				}
			}
			else if (this.count == 1)
			{
				this.first = ent;
				this.first.node.next = this.last;
				this.last.node.prev = this.first;
			}
			else if (this.first.node.info.timestampInMillis > ent.node.info.timestampInMillis)
			{
				ent.node.next = this.first;
				this.first.node.prev = ent;
				this.first = ent;
			}
			else
			{
				InterpTimedEvent.Dir prev;
				if (this.first.node.info.timestampInMillis == ent.node.info.timestampInMillis)
				{
					prev = this.first;
					while (prev.node.next.has)
					{
						if (prev.node.next.node.info.timestampInMillis > ent.node.info.timestampInMillis)
						{
							break;
						}
						prev = prev.node.next;
					}
				}
				else
				{
					prev = this.last;
					while (prev.node.prev.has)
					{
						prev = prev.node.prev;
						if (prev.node.info.timestampInMillis <= ent.node.info.timestampInMillis)
						{
							break;
						}
					}
				}
				ent.node.next = prev.node.next;
				ent.node.prev = prev;
			}
			this.count++;
			ent.node.inlist = true;
			if (this.FAIL_SAFE_SET == null)
			{
				this.FAIL_SAFE_SET = new HashSet<InterpTimedEvent>();
			}
			this.FAIL_SAFE_SET.Add(ent.node);
			return true;
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x00061A84 File Offset: 0x0005FC84
		public bool Insert(InterpTimedEvent node)
		{
			InterpTimedEvent.Dir dir;
			dir.node = node;
			dir.has = true;
			return this.Insert(ref dir);
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x00061AAC File Offset: 0x0005FCAC
		public List<InterpTimedEvent> EmergencyDump(bool botherSorting)
		{
			HashSet<InterpTimedEvent> hashSet = new HashSet<InterpTimedEvent>();
			InterpTimedEvent.LList.Iterator iterator = default(InterpTimedEvent.LList.Iterator);
			bool flag;
			do
			{
				InterpTimedEvent item;
				try
				{
					flag = this.Dequeue(ulong.MaxValue, out item, ref iterator);
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
					break;
				}
				if (flag)
				{
					hashSet.Add(item);
				}
			}
			while (flag);
			this.first = (this.last = default(InterpTimedEvent.Dir));
			this.count = 0;
			HashSet<InterpTimedEvent> fail_SAFE_SET = this.FAIL_SAFE_SET;
			this.FAIL_SAFE_SET = null;
			if (fail_SAFE_SET != null)
			{
				hashSet.UnionWith(fail_SAFE_SET);
			}
			List<InterpTimedEvent> list = new List<InterpTimedEvent>(hashSet);
			if (botherSorting)
			{
				try
				{
					list.Sort(delegate(InterpTimedEvent x, InterpTimedEvent y)
					{
						if (x == null)
						{
							if (y == null)
							{
								return 0;
							}
							return 0.CompareTo(1);
						}
						else
						{
							if (y == null)
							{
								return 1.CompareTo(0);
							}
							ulong timestampInMillis = x.info.timestampInMillis;
							return timestampInMillis.CompareTo(y.info.timestampInMillis);
						}
					});
				}
				catch (Exception ex2)
				{
					Debug.LogException(ex2);
				}
			}
			return list;
		}

		// Token: 0x04000D20 RID: 3360
		public InterpTimedEvent.Dir first;

		// Token: 0x04000D21 RID: 3361
		public InterpTimedEvent.Dir last;

		// Token: 0x04000D22 RID: 3362
		public int count;

		// Token: 0x04000D23 RID: 3363
		private HashSet<InterpTimedEvent> FAIL_SAFE_SET;

		// Token: 0x020002B1 RID: 689
		public struct Iterator
		{
			// Token: 0x04000D25 RID: 3365
			internal InterpTimedEvent.Dir d;

			// Token: 0x04000D26 RID: 3366
			internal bool started;
		}
	}
}
