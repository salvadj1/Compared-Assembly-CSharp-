using System;
using UnityEngine;

namespace Facepunch.Procedural
{
	// Token: 0x020004E8 RID: 1256
	public struct MillisClock
	{
		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06002AE3 RID: 10979 RVA: 0x000AB4C4 File Offset: 0x000A96C4
		public ClockStatus clockStatus
		{
			get
			{
				return (!this.once) ? ClockStatus.Unset : ((this.remain != 0UL) ? ((this.remain >= this.duration) ? ClockStatus.Negative : ClockStatus.WillElapse) : ((this.duration != 0UL) ? ClockStatus.Elapsed : ClockStatus.Unset));
			}
		}

		// Token: 0x06002AE4 RID: 10980 RVA: 0x000AB51C File Offset: 0x000A971C
		public ClockStatus ResetRandomDurationSeconds(double secondsMin, double secondsMax)
		{
			return this.ResetDurationSeconds(secondsMin + (double)Random.value * (secondsMax - secondsMin));
		}

		// Token: 0x06002AE5 RID: 10981 RVA: 0x000AB530 File Offset: 0x000A9730
		public ClockStatus ResetDurationSeconds(double seconds)
		{
			return this.ResetDurationMillis((ulong)Math.Ceiling(seconds * 1000.0));
		}

		// Token: 0x06002AE6 RID: 10982 RVA: 0x000AB54C File Offset: 0x000A974C
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

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x06002AE7 RID: 10983 RVA: 0x000AB584 File Offset: 0x000A9784
		public float percentf
		{
			get
			{
				return (this.remain != 0UL) ? ((this.remain < this.duration) ? ((float)(1.0 - this.remain / this.duration)) : 0f) : 1f;
			}
		}

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x06002AE8 RID: 10984 RVA: 0x000AB5E0 File Offset: 0x000A97E0
		public double percent
		{
			get
			{
				return (this.remain != 0UL) ? ((this.remain < this.duration) ? (1.0 - this.remain / this.duration) : 0.0) : 1.0;
			}
		}

		// Token: 0x06002AE9 RID: 10985 RVA: 0x000AB640 File Offset: 0x000A9840
		public void SetImmediate()
		{
			this.once = true;
			this.remain = 1UL;
			this.duration = 2UL;
		}

		// Token: 0x06002AEA RID: 10986 RVA: 0x000AB65C File Offset: 0x000A985C
		public bool IntegrateTime_Reached(ulong millis)
		{
			return (byte)(this.IntegrateTime(millis) & Integration.Stationary) == 1;
		}

		// Token: 0x06002AEB RID: 10987 RVA: 0x000AB66C File Offset: 0x000A986C
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

		// Token: 0x04001775 RID: 6005
		[NonSerialized]
		public ulong remain;

		// Token: 0x04001776 RID: 6006
		[NonSerialized]
		public ulong duration;

		// Token: 0x04001777 RID: 6007
		[NonSerialized]
		public bool once;
	}
}
