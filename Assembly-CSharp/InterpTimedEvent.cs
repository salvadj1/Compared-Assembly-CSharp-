using System;
using System.Collections.Generic;
using uLink;
using UnityEngine;

// Token: 0x020002E9 RID: 745
public sealed class InterpTimedEvent : IDisposable
{
	// Token: 0x06001A07 RID: 6663 RVA: 0x000652C4 File Offset: 0x000634C4
	private InterpTimedEvent()
	{
	}

	// Token: 0x1700076A RID: 1898
	// (get) Token: 0x06001A09 RID: 6665 RVA: 0x000652DC File Offset: 0x000634DC
	public static int ArgumentCount
	{
		get
		{
			return global::InterpTimedEvent.current.args.length;
		}
	}

	// Token: 0x1700076B RID: 1899
	// (get) Token: 0x06001A0A RID: 6666 RVA: 0x000652F0 File Offset: 0x000634F0
	public static MonoBehaviour Target
	{
		get
		{
			return global::InterpTimedEvent.current.component;
		}
	}

	// Token: 0x1700076C RID: 1900
	// (get) Token: 0x06001A0B RID: 6667 RVA: 0x000652FC File Offset: 0x000634FC
	public static string Tag
	{
		get
		{
			return global::InterpTimedEvent.current.tag;
		}
	}

	// Token: 0x06001A0C RID: 6668 RVA: 0x00065308 File Offset: 0x00063508
	public static object Argument(int index)
	{
		return global::InterpTimedEvent.current.args.parameters[index];
	}

	// Token: 0x1700076D RID: 1901
	// (get) Token: 0x06001A0D RID: 6669 RVA: 0x0006531C File Offset: 0x0006351C
	public static uLink.NetworkMessageInfo Info
	{
		get
		{
			return global::InterpTimedEvent.current.info;
		}
	}

	// Token: 0x1700076E RID: 1902
	// (get) Token: 0x06001A0E RID: 6670 RVA: 0x00065328 File Offset: 0x00063528
	public static double Timestamp
	{
		get
		{
			return global::InterpTimedEvent.current.info.timestamp;
		}
	}

	// Token: 0x1700076F RID: 1903
	// (get) Token: 0x06001A0F RID: 6671 RVA: 0x0006533C File Offset: 0x0006353C
	public static ulong TimestampInMilliseconds
	{
		get
		{
			return global::InterpTimedEvent.current.info.timestampInMillis;
		}
	}

	// Token: 0x17000770 RID: 1904
	// (get) Token: 0x06001A10 RID: 6672 RVA: 0x00065350 File Offset: 0x00063550
	public static uLink.NetworkPlayer Sender
	{
		get
		{
			return global::InterpTimedEvent.current.info.sender;
		}
	}

	// Token: 0x17000771 RID: 1905
	// (get) Token: 0x06001A11 RID: 6673 RVA: 0x00065364 File Offset: 0x00063564
	public static uLink.NetworkView NetworkView
	{
		get
		{
			return global::InterpTimedEvent.current.info.networkView;
		}
	}

	// Token: 0x17000772 RID: 1906
	// (get) Token: 0x06001A12 RID: 6674 RVA: 0x00065378 File Offset: 0x00063578
	public static NetworkFlags Flags
	{
		get
		{
			return global::InterpTimedEvent.current.info.flags;
		}
	}

	// Token: 0x06001A13 RID: 6675 RVA: 0x0006538C File Offset: 0x0006358C
	public static Type ArgumentType(int index)
	{
		return global::InterpTimedEvent.current.args.types[index];
	}

	// Token: 0x17000773 RID: 1907
	// (get) Token: 0x06001A14 RID: 6676 RVA: 0x000653A0 File Offset: 0x000635A0
	public static object[] ArgumentArray
	{
		get
		{
			return global::InterpTimedEvent.current.args.parameters;
		}
	}

	// Token: 0x17000774 RID: 1908
	// (get) Token: 0x06001A15 RID: 6677 RVA: 0x000653B4 File Offset: 0x000635B4
	public static Type[] ArgumentTypeArray
	{
		get
		{
			return global::InterpTimedEvent.current.args.types;
		}
	}

	// Token: 0x06001A16 RID: 6678 RVA: 0x000653C8 File Offset: 0x000635C8
	public static T Argument<T>(int index)
	{
		Type type = global::InterpTimedEvent.current.args.types[index];
		if (!typeof(T).IsAssignableFrom(type) && (typeof(void) != type || typeof(T).IsValueType))
		{
			throw new InvalidCastException(string.Format("Argument #{0} was a {1} and {2} is not assignable by {1}", index, global::InterpTimedEvent.current.args.types[index], typeof(T)));
		}
		return (T)((object)global::InterpTimedEvent.current.args.parameters[index]);
	}

	// Token: 0x06001A17 RID: 6679 RVA: 0x00065468 File Offset: 0x00063668
	public static bool ArgumentIs<T>(int index)
	{
		Type type = global::InterpTimedEvent.current.args.types[index];
		return typeof(T).IsAssignableFrom(type) || (type == typeof(void) && !typeof(T).IsValueType);
	}

	// Token: 0x06001A18 RID: 6680 RVA: 0x000654C4 File Offset: 0x000636C4
	public static bool ArgumentIs(int index, Type comptype)
	{
		Type type = global::InterpTimedEvent.current.args.types[index];
		return comptype.IsAssignableFrom(global::InterpTimedEvent.current.args.types[index]) || (type == typeof(void) && !comptype.IsValueType);
	}

	// Token: 0x06001A19 RID: 6681 RVA: 0x00065520 File Offset: 0x00063720
	public static void MarkUnhandled()
	{
		Debug.LogWarning("Unhandled Timed Event :" + ((!string.IsNullOrEmpty(global::InterpTimedEvent.current.tag)) ? global::InterpTimedEvent.current.tag : " without a tag"), global::InterpTimedEvent.current.component);
	}

	// Token: 0x17000775 RID: 1909
	// (get) Token: 0x06001A1A RID: 6682 RVA: 0x00065570 File Offset: 0x00063770
	// (set) Token: 0x06001A1B RID: 6683 RVA: 0x00065578 File Offset: 0x00063778
	public static bool syncronizationPaused
	{
		get
		{
			return global::InterpTimedEventSyncronizer.paused;
		}
		set
		{
			global::InterpTimedEventSyncronizer.paused = value;
		}
	}

	// Token: 0x17000776 RID: 1910
	// (get) Token: 0x06001A1C RID: 6684 RVA: 0x00065580 File Offset: 0x00063780
	public global::IInterpTimedEventReceiver receiver
	{
		get
		{
			return this.component as global::IInterpTimedEventReceiver;
		}
	}

	// Token: 0x06001A1D RID: 6685 RVA: 0x00065590 File Offset: 0x00063790
	public static void EmergencyDump()
	{
	}

	// Token: 0x06001A1E RID: 6686 RVA: 0x00065594 File Offset: 0x00063794
	public void Dispose()
	{
		if (this.inlist)
		{
			global::InterpTimedEvent.queue.Remove(this);
		}
		if (!this.disposed)
		{
			this.prev = default(global::InterpTimedEvent.Dir);
			this.next = global::InterpTimedEvent.dump;
			global::InterpTimedEvent.dump.has = true;
			global::InterpTimedEvent.dump.node = this;
			this.component = null;
			this.args.Dispose();
			this.args = null;
			this.info = null;
			this.tag = null;
			global::InterpTimedEvent.dumpCount++;
			this.disposed = true;
		}
	}

	// Token: 0x06001A1F RID: 6687 RVA: 0x00065630 File Offset: 0x00063830
	private static void InvokeDirect(global::InterpTimedEvent evnt)
	{
		global::InterpTimedEvent interpTimedEvent = global::InterpTimedEvent.current;
		global::InterpTimedEvent.current = evnt;
		global::InterpTimedEvent.Invoke();
		global::InterpTimedEvent.current = interpTimedEvent;
	}

	// Token: 0x06001A20 RID: 6688 RVA: 0x00065654 File Offset: 0x00063854
	private static void Invoke()
	{
		MonoBehaviour monoBehaviour = global::InterpTimedEvent.current.component;
		if (monoBehaviour)
		{
			global::IInterpTimedEventReceiver interpTimedEventReceiver = monoBehaviour as global::IInterpTimedEventReceiver;
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
			Debug.LogWarning("A component implementing IInterpTimeEventReceiver was destroyed without properly calling InterpEvent.Remove() in OnDestroy!\r\n" + ((!string.IsNullOrEmpty(global::InterpTimedEvent.current.tag)) ? ("The tag was \"" + global::InterpTimedEvent.current.tag + "\"") : "There was no tag set"));
		}
		global::InterpTimedEvent.current.Dispose();
	}

	// Token: 0x06001A21 RID: 6689 RVA: 0x00065714 File Offset: 0x00063914
	public static void Catchup()
	{
		global::InterpTimedEvent.Catchup(global::Interpolation.timeInMillis);
	}

	// Token: 0x06001A22 RID: 6690 RVA: 0x00065720 File Offset: 0x00063920
	public static void ForceCatchupToDate()
	{
		global::InterpTimedEvent._forceCatchupToDate = true;
	}

	// Token: 0x06001A23 RID: 6691 RVA: 0x00065728 File Offset: 0x00063928
	public static void Catchup(ulong playhead)
	{
		global::InterpTimedEvent._forceCatchupToDate = false;
		while (global::InterpTimedEvent.queue.Dequeue(playhead, out global::InterpTimedEvent.current))
		{
			global::InterpTimedEvent.Invoke();
		}
	}

	// Token: 0x06001A24 RID: 6692 RVA: 0x00065750 File Offset: 0x00063950
	public static void Clear()
	{
		global::InterpTimedEvent.Clear(false);
	}

	// Token: 0x06001A25 RID: 6693 RVA: 0x00065758 File Offset: 0x00063958
	public static void Clear(bool invokePending)
	{
		global::InterpTimedEvent.LList.Iterator iterator = default(global::InterpTimedEvent.LList.Iterator);
		if (invokePending)
		{
			global::InterpTimedEvent interpTimedEvent;
			while (global::InterpTimedEvent.queue.Dequeue(18446744073709551615UL, out interpTimedEvent, ref iterator))
			{
				global::InterpTimedEvent.InvokeDirect(interpTimedEvent);
			}
		}
		else
		{
			global::InterpTimedEvent interpTimedEvent;
			while (global::InterpTimedEvent.queue.Dequeue(18446744073709551615UL, out interpTimedEvent, ref iterator))
			{
				interpTimedEvent.Dispose();
			}
		}
	}

	// Token: 0x06001A26 RID: 6694 RVA: 0x000657BC File Offset: 0x000639BC
	public static void Remove(MonoBehaviour receiver)
	{
		global::InterpTimedEvent.Remove(receiver, false);
	}

	// Token: 0x06001A27 RID: 6695 RVA: 0x000657C8 File Offset: 0x000639C8
	public static void Remove(MonoBehaviour receiver, bool invokePending)
	{
		global::InterpTimedEvent.LList.Iterator iterator = default(global::InterpTimedEvent.LList.Iterator);
		if (invokePending)
		{
			global::InterpTimedEvent interpTimedEvent;
			while (global::InterpTimedEvent.queue.Dequeue(receiver, 18446744073709551615UL, out interpTimedEvent, ref iterator))
			{
				global::InterpTimedEvent.InvokeDirect(interpTimedEvent);
			}
		}
		else
		{
			global::InterpTimedEvent interpTimedEvent;
			while (global::InterpTimedEvent.queue.Dequeue(receiver, 18446744073709551615UL, out interpTimedEvent, ref iterator))
			{
				interpTimedEvent.Dispose();
			}
		}
	}

	// Token: 0x06001A28 RID: 6696 RVA: 0x0006582C File Offset: 0x00063A2C
	internal static global::InterpTimedEvent New(MonoBehaviour receiver, string tag, ref uLink.NetworkMessageInfo info, object[] args, bool immediate)
	{
		if (!receiver)
		{
			Debug.LogError("receiver is null or has been destroyed", receiver);
			return null;
		}
		if (!(receiver is global::IInterpTimedEventReceiver))
		{
			Debug.LogError("receiver of type " + receiver.GetType() + " does not implement IInterpTimedEventReceiver", receiver);
			return null;
		}
		global::InterpTimedEvent interpTimedEvent;
		if (global::InterpTimedEvent.dump.has)
		{
			global::InterpTimedEvent.dumpCount--;
			interpTimedEvent = global::InterpTimedEvent.dump.node;
			global::InterpTimedEvent.dump = interpTimedEvent.next;
			interpTimedEvent.next = default(global::InterpTimedEvent.Dir);
			interpTimedEvent.prev = default(global::InterpTimedEvent.Dir);
			interpTimedEvent.disposed = false;
		}
		else
		{
			interpTimedEvent = new global::InterpTimedEvent();
		}
		interpTimedEvent.args = global::InterpTimedEvent.ArgList.New(args);
		interpTimedEvent.tag = tag;
		interpTimedEvent.component = receiver;
		interpTimedEvent.info = info;
		if (!immediate)
		{
			global::InterpTimedEvent.queue.Insert(interpTimedEvent);
		}
		return interpTimedEvent;
	}

	// Token: 0x06001A29 RID: 6697 RVA: 0x00065910 File Offset: 0x00063B10
	public static bool Queue(global::IInterpTimedEventReceiver receiver, string tag, ref uLink.NetworkMessageInfo info)
	{
		return global::InterpTimedEvent.QueueOrExecute(receiver, false, tag, ref info, global::InterpTimedEvent.emptyArgs);
	}

	// Token: 0x06001A2A RID: 6698 RVA: 0x00065920 File Offset: 0x00063B20
	public static bool Queue(global::IInterpTimedEventReceiver receiver, string tag, ref uLink.NetworkMessageInfo info, params object[] args)
	{
		return global::InterpTimedEvent.QueueOrExecute(receiver, false, tag, ref info, args);
	}

	// Token: 0x06001A2B RID: 6699 RVA: 0x0006592C File Offset: 0x00063B2C
	public static bool Execute(global::IInterpTimedEventReceiver receiver, string tag, ref uLink.NetworkMessageInfo info)
	{
		return global::InterpTimedEvent.QueueOrExecute(receiver, true, tag, ref info, global::InterpTimedEvent.emptyArgs);
	}

	// Token: 0x06001A2C RID: 6700 RVA: 0x0006593C File Offset: 0x00063B3C
	public static bool Execute(global::IInterpTimedEventReceiver receiver, string tag, ref uLink.NetworkMessageInfo info, params object[] args)
	{
		return global::InterpTimedEvent.QueueOrExecute(receiver, true, tag, ref info, args);
	}

	// Token: 0x06001A2D RID: 6701 RVA: 0x00065948 File Offset: 0x00063B48
	public static bool QueueOrExecute(global::IInterpTimedEventReceiver receiver, bool immediate, string tag, ref uLink.NetworkMessageInfo info)
	{
		return global::InterpTimedEvent.QueueOrExecute(receiver, immediate, tag, ref info, global::InterpTimedEvent.emptyArgs);
	}

	// Token: 0x06001A2E RID: 6702 RVA: 0x00065958 File Offset: 0x00063B58
	public static bool QueueOrExecute(global::IInterpTimedEventReceiver receiver, bool immediate, string tag, ref uLink.NetworkMessageInfo info, params object[] args)
	{
		MonoBehaviour receiver2 = receiver as MonoBehaviour;
		global::InterpTimedEvent interpTimedEvent = global::InterpTimedEvent.New(receiver2, tag, ref info, args, immediate);
		if (interpTimedEvent == null)
		{
			return false;
		}
		if (immediate)
		{
			global::InterpTimedEvent.InvokeDirect(interpTimedEvent);
		}
		else if (!global::InterpTimedEventSyncronizer.available)
		{
			Debug.LogWarning("Not running event because theres no syncronizer available. " + tag, receiver as Object);
			return false;
		}
		return true;
	}

	// Token: 0x06001A2F RID: 6703 RVA: 0x000659B4 File Offset: 0x00063BB4
	public static void EMERGENCY_DUMP(bool TRY_TO_EXECUTE)
	{
		Debug.LogWarning("RUNNING EMERGENCY DUMP: TRY TO EXECUTE=" + TRY_TO_EXECUTE);
		try
		{
			if (TRY_TO_EXECUTE)
			{
				try
				{
					List<global::InterpTimedEvent> list = global::InterpTimedEvent.queue.EmergencyDump(true);
					foreach (global::InterpTimedEvent interpTimedEvent in list)
					{
						try
						{
							global::InterpTimedEvent.InvokeDirect(interpTimedEvent);
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
				global::InterpTimedEvent.queue.EmergencyDump(false);
			}
		}
		catch (Exception ex4)
		{
			Debug.LogException(ex4);
		}
		finally
		{
			global::InterpTimedEvent.queue = default(global::InterpTimedEvent.LList);
			global::InterpTimedEvent.dump = default(global::InterpTimedEvent.Dir);
			global::InterpTimedEvent.dumpCount = 0;
		}
		Debug.LogWarning("END OF EMERGENCY DUMP: TRY TO EXECUTE=" + TRY_TO_EXECUTE);
	}

	// Token: 0x04000E42 RID: 3650
	internal static global::InterpTimedEvent current;

	// Token: 0x04000E43 RID: 3651
	public MonoBehaviour component;

	// Token: 0x04000E44 RID: 3652
	public uLink.NetworkMessageInfo info;

	// Token: 0x04000E45 RID: 3653
	public global::InterpTimedEvent.ArgList args;

	// Token: 0x04000E46 RID: 3654
	public string tag;

	// Token: 0x04000E47 RID: 3655
	private bool disposed;

	// Token: 0x04000E48 RID: 3656
	private bool inlist;

	// Token: 0x04000E49 RID: 3657
	private static int dumpCount;

	// Token: 0x04000E4A RID: 3658
	private static global::InterpTimedEvent.Dir dump;

	// Token: 0x04000E4B RID: 3659
	private static global::InterpTimedEvent.LList queue;

	// Token: 0x04000E4C RID: 3660
	internal global::InterpTimedEvent.Dir next;

	// Token: 0x04000E4D RID: 3661
	internal global::InterpTimedEvent.Dir prev;

	// Token: 0x04000E4E RID: 3662
	private static bool _forceCatchupToDate;

	// Token: 0x04000E4F RID: 3663
	private static readonly object[] emptyArgs = new object[0];

	// Token: 0x020002EA RID: 746
	public sealed class ArgList : IDisposable
	{
		// Token: 0x06001A30 RID: 6704 RVA: 0x00065B68 File Offset: 0x00063D68
		private ArgList(int length)
		{
			this.length = length;
			this.parameters = new object[length];
			this.types = new Type[length];
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x00065BA8 File Offset: 0x00063DA8
		private void AddToDump(ref global::InterpTimedEvent.ArgList.Dump dump)
		{
			this.dumpNext = dump.last;
			dump.count++;
			dump.last = this;
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x00065BCC File Offset: 0x00063DCC
		public void Dispose()
		{
			if (!this.disposed && this.length != 0)
			{
				for (int i = 0; i < this.length; i++)
				{
					this.types[i] = null;
					this.parameters[i] = null;
				}
				if (global::InterpTimedEvent.ArgList.dumps.Length <= this.length)
				{
					Array.Resize<global::InterpTimedEvent.ArgList.Dump>(ref global::InterpTimedEvent.ArgList.dumps, this.length + 1);
				}
				this.AddToDump(ref global::InterpTimedEvent.ArgList.dumps[this.length]);
				this.disposed = true;
			}
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x00065C5C File Offset: 0x00063E5C
		private static global::InterpTimedEvent.ArgList Recycle(ref global::InterpTimedEvent.ArgList.Dump dump, int length)
		{
			if (dump.count > 0)
			{
				global::InterpTimedEvent.ArgList last = dump.last;
				dump.last = last.dumpNext;
				dump.count--;
				last.dumpNext = null;
				last.disposed = false;
				return last;
			}
			return new global::InterpTimedEvent.ArgList(length);
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x00065CAC File Offset: 0x00063EAC
		public static global::InterpTimedEvent.ArgList New(object[] args)
		{
			int num = (args != null) ? args.Length : 0;
			if (num == 0)
			{
				return global::InterpTimedEvent.ArgList.voidParameters;
			}
			global::InterpTimedEvent.ArgList argList;
			if (global::InterpTimedEvent.ArgList.dumps.Length > num)
			{
				argList = global::InterpTimedEvent.ArgList.Recycle(ref global::InterpTimedEvent.ArgList.dumps[num], num);
			}
			else
			{
				argList = new global::InterpTimedEvent.ArgList(num);
			}
			for (int i = 0; i < num; i++)
			{
				object obj = args[i];
				argList.parameters[i] = obj;
				argList.types[i] = ((obj != null) ? obj.GetType() : typeof(void));
			}
			return argList;
		}

		// Token: 0x04000E50 RID: 3664
		public readonly object[] parameters;

		// Token: 0x04000E51 RID: 3665
		public readonly Type[] types;

		// Token: 0x04000E52 RID: 3666
		public readonly int length;

		// Token: 0x04000E53 RID: 3667
		private bool disposed;

		// Token: 0x04000E54 RID: 3668
		private static global::InterpTimedEvent.ArgList voidParameters = new global::InterpTimedEvent.ArgList(0);

		// Token: 0x04000E55 RID: 3669
		private static global::InterpTimedEvent.ArgList.Dump[] dumps = new global::InterpTimedEvent.ArgList.Dump[4];

		// Token: 0x04000E56 RID: 3670
		private global::InterpTimedEvent.ArgList dumpNext;

		// Token: 0x020002EB RID: 747
		private struct Dump
		{
			// Token: 0x04000E57 RID: 3671
			public global::InterpTimedEvent.ArgList last;

			// Token: 0x04000E58 RID: 3672
			public int count;
		}
	}

	// Token: 0x020002EC RID: 748
	internal struct Dir
	{
		// Token: 0x04000E59 RID: 3673
		public bool has;

		// Token: 0x04000E5A RID: 3674
		public global::InterpTimedEvent node;
	}

	// Token: 0x020002ED RID: 749
	internal struct LList
	{
		// Token: 0x06001A36 RID: 6710 RVA: 0x00065D44 File Offset: 0x00063F44
		public bool Dequeue(ulong playhead, out global::InterpTimedEvent node)
		{
			global::InterpTimedEvent.LList.Iterator iterator = default(global::InterpTimedEvent.LList.Iterator);
			return this.Dequeue(playhead, out node, ref iterator);
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x00065D68 File Offset: 0x00063F68
		public bool Dequeue(ulong playhead, out global::InterpTimedEvent node, ref global::InterpTimedEvent.LList.Iterator iter_)
		{
			if (this.count <= 0)
			{
				node = null;
				return false;
			}
			global::InterpTimedEvent.Dir dir = (!iter_.started) ? this.first : iter_.d;
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
			iter_.d = default(global::InterpTimedEvent.Dir);
			iter_.started = true;
			node = null;
			return false;
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x00065E14 File Offset: 0x00064014
		public bool Dequeue(MonoBehaviour script, ulong playhead, out global::InterpTimedEvent node)
		{
			global::InterpTimedEvent.LList.Iterator iterator = default(global::InterpTimedEvent.LList.Iterator);
			return this.Dequeue(script, playhead, out node, ref iterator);
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x00065E38 File Offset: 0x00064038
		public bool Dequeue(MonoBehaviour script, ulong playhead, out global::InterpTimedEvent node, ref global::InterpTimedEvent.LList.Iterator iter_)
		{
			if (this.count <= 0)
			{
				node = null;
				return false;
			}
			global::InterpTimedEvent.Dir dir = (!iter_.started) ? this.first : iter_.d;
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
			iter_.d = default(global::InterpTimedEvent.Dir);
			iter_.started = true;
			node = null;
			return false;
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x00065F10 File Offset: 0x00064110
		internal bool Remove(global::InterpTimedEvent node)
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

		// Token: 0x06001A3B RID: 6715 RVA: 0x00065F3C File Offset: 0x0006413C
		private bool RemoveUnsafe(global::InterpTimedEvent node)
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
						node.prev = (node.next = default(global::InterpTimedEvent.Dir));
						node.inlist = false;
						return true;
					}
					this.last = node.prev;
					this.last.node.next = default(global::InterpTimedEvent.Dir);
					this.count--;
					node.prev = (node.next = default(global::InterpTimedEvent.Dir));
					node.inlist = false;
					return true;
				}
				else
				{
					if (node.next.has)
					{
						this.first = node.next;
						this.first.node.prev = default(global::InterpTimedEvent.Dir);
						this.count--;
						node.prev = (node.next = default(global::InterpTimedEvent.Dir));
						node.inlist = false;
						return true;
					}
					if (this.first.node == node)
					{
						this.first = (this.last = default(global::InterpTimedEvent.Dir));
						this.count = 0;
						node.prev = (node.next = default(global::InterpTimedEvent.Dir));
						node.inlist = false;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001A3C RID: 6716 RVA: 0x000660F4 File Offset: 0x000642F4
		private bool Insert(ref global::InterpTimedEvent.Dir ent)
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
				global::InterpTimedEvent.Dir prev;
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
				this.FAIL_SAFE_SET = new HashSet<global::InterpTimedEvent>();
			}
			this.FAIL_SAFE_SET.Add(ent.node);
			return true;
		}

		// Token: 0x06001A3D RID: 6717 RVA: 0x000663F8 File Offset: 0x000645F8
		public bool Insert(global::InterpTimedEvent node)
		{
			global::InterpTimedEvent.Dir dir;
			dir.node = node;
			dir.has = true;
			return this.Insert(ref dir);
		}

		// Token: 0x06001A3E RID: 6718 RVA: 0x00066420 File Offset: 0x00064620
		public List<global::InterpTimedEvent> EmergencyDump(bool botherSorting)
		{
			HashSet<global::InterpTimedEvent> hashSet = new HashSet<global::InterpTimedEvent>();
			global::InterpTimedEvent.LList.Iterator iterator = default(global::InterpTimedEvent.LList.Iterator);
			bool flag;
			do
			{
				global::InterpTimedEvent item;
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
			this.first = (this.last = default(global::InterpTimedEvent.Dir));
			this.count = 0;
			HashSet<global::InterpTimedEvent> fail_SAFE_SET = this.FAIL_SAFE_SET;
			this.FAIL_SAFE_SET = null;
			if (fail_SAFE_SET != null)
			{
				hashSet.UnionWith(fail_SAFE_SET);
			}
			List<global::InterpTimedEvent> list = new List<global::InterpTimedEvent>(hashSet);
			if (botherSorting)
			{
				try
				{
					list.Sort(delegate(global::InterpTimedEvent x, global::InterpTimedEvent y)
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

		// Token: 0x04000E5B RID: 3675
		public global::InterpTimedEvent.Dir first;

		// Token: 0x04000E5C RID: 3676
		public global::InterpTimedEvent.Dir last;

		// Token: 0x04000E5D RID: 3677
		public int count;

		// Token: 0x04000E5E RID: 3678
		private HashSet<global::InterpTimedEvent> FAIL_SAFE_SET;

		// Token: 0x020002EE RID: 750
		public struct Iterator
		{
			// Token: 0x04000E60 RID: 3680
			internal global::InterpTimedEvent.Dir d;

			// Token: 0x04000E61 RID: 3681
			internal bool started;
		}
	}
}
