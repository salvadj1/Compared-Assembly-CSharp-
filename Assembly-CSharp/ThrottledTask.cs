using System;
using System.Collections.Generic;
using Facepunch.Clocks.Counters;
using Facepunch.Progress;
using UnityEngine;

// Token: 0x020001F8 RID: 504
[AddComponentMenu("")]
public abstract class ThrottledTask : MonoBehaviour
{
	// Token: 0x1700037C RID: 892
	// (get) Token: 0x06000E26 RID: 3622 RVA: 0x00036578 File Offset: 0x00034778
	public static bool Operational
	{
		get
		{
			return global::ThrottledTask.numWorking > 0;
		}
	}

	// Token: 0x1700037D RID: 893
	// (get) Token: 0x06000E27 RID: 3623 RVA: 0x00036584 File Offset: 0x00034784
	public static global::ThrottledTask[] AllWorkingTasks
	{
		get
		{
			global::ThrottledTask[] array = new global::ThrottledTask[global::ThrottledTask.numWorking];
			int num = 0;
			foreach (global::ThrottledTask throttledTask in global::ThrottledTask.AllTasks)
			{
				if (throttledTask.working)
				{
					array[num++] = throttledTask;
					if (num == global::ThrottledTask.numWorking)
					{
						break;
					}
				}
			}
			return array;
		}
	}

	// Token: 0x1700037E RID: 894
	// (get) Token: 0x06000E28 RID: 3624 RVA: 0x00036614 File Offset: 0x00034814
	public static IEnumerable<Facepunch.Progress.IProgress> AllWorkingTasksProgress
	{
		get
		{
			foreach (global::ThrottledTask task in global::ThrottledTask.AllTasks)
			{
				if (task.working && task is Facepunch.Progress.IProgress)
				{
					yield return task as Facepunch.Progress.IProgress;
				}
			}
			yield break;
		}
	}

	// Token: 0x1700037F RID: 895
	// (get) Token: 0x06000E29 RID: 3625 RVA: 0x00036630 File Offset: 0x00034830
	protected global::ThrottledTask.Timer Begin
	{
		get
		{
			return global::ThrottledTask.Timer.Start;
		}
	}

	// Token: 0x17000380 RID: 896
	// (get) Token: 0x06000E2A RID: 3626 RVA: 0x00036638 File Offset: 0x00034838
	// (set) Token: 0x06000E2B RID: 3627 RVA: 0x00036640 File Offset: 0x00034840
	public bool Working
	{
		get
		{
			return this.working;
		}
		protected set
		{
			this.SetWorking(value);
		}
	}

	// Token: 0x06000E2C RID: 3628 RVA: 0x0003664C File Offset: 0x0003484C
	private void SetWorking(bool on)
	{
		if (on != this.working)
		{
			this.working = on;
			if (on)
			{
				global::ThrottledTask.numWorking++;
			}
			else
			{
				global::ThrottledTask.numWorking--;
			}
		}
	}

	// Token: 0x06000E2D RID: 3629 RVA: 0x00036690 File Offset: 0x00034890
	protected void Awake()
	{
		if (!this.added)
		{
			this.added = true;
			global::ThrottledTask.AllTasks.Add(this);
		}
	}

	// Token: 0x06000E2E RID: 3630 RVA: 0x000366B0 File Offset: 0x000348B0
	protected void OnDestroy()
	{
		if (this.added)
		{
			global::ThrottledTask.AllTasks.Remove(this);
		}
		else
		{
			this.added = true;
		}
		this.SetWorking(false);
	}

	// Token: 0x040008B5 RID: 2229
	private const int kTargetMSPerFrame = 400;

	// Token: 0x040008B6 RID: 2230
	[NonSerialized]
	private bool working;

	// Token: 0x040008B7 RID: 2231
	[NonSerialized]
	private bool added;

	// Token: 0x040008B8 RID: 2232
	private static int numWorking;

	// Token: 0x040008B9 RID: 2233
	private static List<global::ThrottledTask> AllTasks = new List<global::ThrottledTask>();

	// Token: 0x020001F9 RID: 505
	protected struct Timer
	{
		// Token: 0x06000E2F RID: 3631 RVA: 0x000366E8 File Offset: 0x000348E8
		private Timer(Facepunch.Clocks.Counters.SystemTimestamp clock)
		{
			this.clock = clock;
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000E30 RID: 3632 RVA: 0x000366F4 File Offset: 0x000348F4
		internal static global::ThrottledTask.Timer Start
		{
			get
			{
				return new global::ThrottledTask.Timer(Facepunch.Clocks.Counters.SystemTimestamp.Restart);
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000E31 RID: 3633 RVA: 0x00036700 File Offset: 0x00034900
		public bool Continue
		{
			get
			{
				return global::ThrottledTask.numWorking == 0 || this.clock.Elapsed.TotalMilliseconds < 400.0 / (double)global::ThrottledTask.numWorking;
			}
		}

		// Token: 0x040008BA RID: 2234
		private readonly Facepunch.Clocks.Counters.SystemTimestamp clock;
	}
}
