using System;
using System.Collections.Generic;
using Facepunch.Clocks.Counters;
using Facepunch.Progress;
using UnityEngine;

// Token: 0x020001C8 RID: 456
[AddComponentMenu("")]
public abstract class ThrottledTask : MonoBehaviour
{
	// Token: 0x17000338 RID: 824
	// (get) Token: 0x06000CE6 RID: 3302 RVA: 0x0003268C File Offset: 0x0003088C
	public static bool Operational
	{
		get
		{
			return ThrottledTask.numWorking > 0;
		}
	}

	// Token: 0x17000339 RID: 825
	// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x00032698 File Offset: 0x00030898
	public static ThrottledTask[] AllWorkingTasks
	{
		get
		{
			ThrottledTask[] array = new ThrottledTask[ThrottledTask.numWorking];
			int num = 0;
			foreach (ThrottledTask throttledTask in ThrottledTask.AllTasks)
			{
				if (throttledTask.working)
				{
					array[num++] = throttledTask;
					if (num == ThrottledTask.numWorking)
					{
						break;
					}
				}
			}
			return array;
		}
	}

	// Token: 0x1700033A RID: 826
	// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x00032728 File Offset: 0x00030928
	public static IEnumerable<IProgress> AllWorkingTasksProgress
	{
		get
		{
			foreach (ThrottledTask task in ThrottledTask.AllTasks)
			{
				if (task.working && task is IProgress)
				{
					yield return task as IProgress;
				}
			}
			yield break;
		}
	}

	// Token: 0x1700033B RID: 827
	// (get) Token: 0x06000CE9 RID: 3305 RVA: 0x00032744 File Offset: 0x00030944
	protected ThrottledTask.Timer Begin
	{
		get
		{
			return ThrottledTask.Timer.Start;
		}
	}

	// Token: 0x1700033C RID: 828
	// (get) Token: 0x06000CEA RID: 3306 RVA: 0x0003274C File Offset: 0x0003094C
	// (set) Token: 0x06000CEB RID: 3307 RVA: 0x00032754 File Offset: 0x00030954
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

	// Token: 0x06000CEC RID: 3308 RVA: 0x00032760 File Offset: 0x00030960
	private void SetWorking(bool on)
	{
		if (on != this.working)
		{
			this.working = on;
			if (on)
			{
				ThrottledTask.numWorking++;
			}
			else
			{
				ThrottledTask.numWorking--;
			}
		}
	}

	// Token: 0x06000CED RID: 3309 RVA: 0x000327A4 File Offset: 0x000309A4
	protected void Awake()
	{
		if (!this.added)
		{
			this.added = true;
			ThrottledTask.AllTasks.Add(this);
		}
	}

	// Token: 0x06000CEE RID: 3310 RVA: 0x000327C4 File Offset: 0x000309C4
	protected void OnDestroy()
	{
		if (this.added)
		{
			ThrottledTask.AllTasks.Remove(this);
		}
		else
		{
			this.added = true;
		}
		this.SetWorking(false);
	}

	// Token: 0x040007A1 RID: 1953
	private const int kTargetMSPerFrame = 400;

	// Token: 0x040007A2 RID: 1954
	[NonSerialized]
	private bool working;

	// Token: 0x040007A3 RID: 1955
	[NonSerialized]
	private bool added;

	// Token: 0x040007A4 RID: 1956
	private static int numWorking;

	// Token: 0x040007A5 RID: 1957
	private static List<ThrottledTask> AllTasks = new List<ThrottledTask>();

	// Token: 0x020001C9 RID: 457
	protected struct Timer
	{
		// Token: 0x06000CEF RID: 3311 RVA: 0x000327FC File Offset: 0x000309FC
		private Timer(SystemTimestamp clock)
		{
			this.clock = clock;
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x00032808 File Offset: 0x00030A08
		internal static ThrottledTask.Timer Start
		{
			get
			{
				return new ThrottledTask.Timer(SystemTimestamp.Restart);
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000CF1 RID: 3313 RVA: 0x00032814 File Offset: 0x00030A14
		public bool Continue
		{
			get
			{
				return ThrottledTask.numWorking == 0 || this.clock.Elapsed.TotalMilliseconds < 400.0 / (double)ThrottledTask.numWorking;
			}
		}

		// Token: 0x040007A6 RID: 1958
		private readonly SystemTimestamp clock;
	}
}
