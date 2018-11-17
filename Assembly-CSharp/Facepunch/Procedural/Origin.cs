using System;
using UnityEngine;

namespace Facepunch.Procedural
{
	// Token: 0x020005A6 RID: 1446
	public struct Origin
	{
		// Token: 0x06002EA1 RID: 11937 RVA: 0x000B3644 File Offset: 0x000B1844
		public void Target(ref Vector3 target, float moveSpeed)
		{
			if (!this.value.clock.once)
			{
				this.delta.x = (this.delta.y = (this.delta.z = 0f));
				this.value.SetImmediate(ref target);
			}
			else
			{
				this.delta.x = target.x - this.value.current.x;
				this.delta.y = target.y - this.value.current.y;
				this.delta.z = target.z - this.value.current.z;
				float num = this.delta.x * this.delta.x + this.delta.y * this.delta.y + this.delta.z * this.delta.z;
				float num2 = moveSpeed * float.Epsilon;
				float num3 = num2 * num2;
				if (num <= num3 || moveSpeed == 0f)
				{
					this.delta.x = (this.delta.y = (this.delta.z = 0f));
					this.value.SetImmediate(ref target);
				}
				else
				{
					float num4 = Mathf.Sqrt(num);
					this.value.begin = this.value.current;
					this.value.end = target;
					if (moveSpeed < 0f)
					{
						moveSpeed = -moveSpeed;
					}
					this.value.clock.remain = (this.value.clock.duration = (ulong)Math.Ceiling((double)num4 * 1000.0 / (double)moveSpeed));
					if (this.value.clock.remain <= 1UL)
					{
						this.delta.x = (this.delta.y = (this.delta.z = 0f));
						this.value.SetImmediate(ref target);
					}
				}
			}
		}

		// Token: 0x06002EA2 RID: 11938 RVA: 0x000B3880 File Offset: 0x000B1A80
		public Integration Advance(ulong millis)
		{
			Integration integration = this.value.clock.IntegrateTime(millis);
			Integration integration2 = integration;
			if (integration2 != Integration.Moved)
			{
				if (integration2 == Integration.MovedDestination)
				{
					this.value.current = this.value.end;
				}
			}
			else
			{
				double percent = this.value.clock.percent;
				this.value.current.x = (float)((double)this.value.begin.x + (double)this.delta.x * percent);
				this.value.current.y = (float)((double)this.value.begin.y + (double)this.delta.y * percent);
				this.value.current.z = (float)((double)this.value.begin.z + (double)this.delta.z * percent);
			}
			return integration;
		}

		// Token: 0x0400193A RID: 6458
		[NonSerialized]
		public Integrated<Vector3> value;

		// Token: 0x0400193B RID: 6459
		[NonSerialized]
		public Vector3 delta;
	}
}
