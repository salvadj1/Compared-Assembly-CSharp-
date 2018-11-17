using System;
using UnityEngine;

namespace Facepunch.Procedural
{
	// Token: 0x020005A3 RID: 1443
	public struct MillisClock
	{
		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06002E95 RID: 11925 RVA: 0x000B325C File Offset: 0x000B145C
		public ClockStatus clockStatus
		{
			get
			{
				return (!this.once) ? ClockStatus.Unset : ((this.remain != 0UL) ? ((this.remain >= this.duration) ? ClockStatus.Negative : ClockStatus.WillElapse) : ((this.duration != 0UL) ? ClockStatus.Elapsed : ClockStatus.Unset));
			}
		}

		// Token: 0x06002E96 RID: 11926 RVA: 0x000B32B4 File Offset: 0x000B14B4
		public ClockStatus ResetRandomDurationSeconds(double secondsMin, double secondsMax)
		{
			return this.ResetDurationSeconds(secondsMin + (double)Random.value * (secondsMax - secondsMin));
		}

		// Token: 0x06002E97 RID: 11927 RVA: 0x000B32C8 File Offset: 0x000B14C8
		public ClockStatus ResetDurationSeconds(double seconds)
		{
			return this.ResetDurationMillis((ulong)Math.Ceiling(seconds * 1000.0));
		}

		// Token: 0x06002E98 RID: 11928 RVA: 0x000B32E4 File Offset: 0x000B14E4
		public ClockStatus ResetDurationMillis(ulong millis)
		{
			if (millis <= 1UL)
			{
				this.SetImmediate();
				return ClockStatus.DidElapse;
			}
			this.once = true;
			this.duration = millis;
			this.remain = millis;
			return ClockStatus.WillElapse;
		}

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06002E99 RID: 11929 RVA: 0x000B331C File Offset: 0x000B151C
		public float percentf
		{
			get
			{
				return (this.remain != 0UL) ? ((this.remain < this.duration) ? ((float)(1.0 - this.remain / this.duration)) : 0f) : 1f;
			}
		}

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06002E9A RID: 11930 RVA: 0x000B3378 File Offset: 0x000B1578
		public double percent
		{
			get
			{
				return (this.remain != 0UL) ? ((this.remain < this.duration) ? (1.0 - this.remain / this.duration) : 0.0) : 1.0;
			}
		}

		// Token: 0x06002E9B RID: 11931 RVA: 0x000B33D8 File Offset: 0x000B15D8
		public void SetImmediate()
		{
			this.once = true;
			this.remain = 1UL;
			this.duration = 2UL;
		}

		// Token: 0x06002E9C RID: 11932 RVA: 0x000B33F4 File Offset: 0x000B15F4
		public bool IntegrateTime_Reached(ulong millis)
		{
			return (byte)(this.IntegrateTime(millis) & Integration.Stationary) == 1;
		}

		// Token: 0x06002E9D RID: 11933 RVA: 0x000B3404 File Offset: 0x000B1604
		public Integration IntegrateTime(ulong millis)
		{
			if (!this.once || this.remain == 0UL || this.duration == 0UL || millis == 0UL)
			{
				return Integration.Stationary;
			}
			if (this.remain <= millis)
			{
				this.remain = 0UL;
				return Integration.Stationary;
			}
			this.remain -= millis;
			if (this.remain < this.duration)
			{
				return Integration.Moved;
			}
			return Integration.MovedDestination;
		}

		// Token: 0x04001932 RID: 6450
		[NonSerialized]
		public ulong remain;

		// Token: 0x04001933 RID: 6451
		[NonSerialized]
		public ulong duration;

		// Token: 0x04001934 RID: 6452
		[NonSerialized]
		public bool once;
	}
}
