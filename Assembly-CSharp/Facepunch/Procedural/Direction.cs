using System;
using UnityEngine;

namespace Facepunch.Procedural
{
	// Token: 0x020004EA RID: 1258
	public struct Direction
	{
		// Token: 0x06002AED RID: 10989 RVA: 0x000AB714 File Offset: 0x000A9914
		public void Target(ref Vector3 target, float degreeSpeed)
		{
			if (!this.value.clock.once)
			{
				this.value.SetImmediate(ref target);
			}
			else
			{
				float num = Mathf.Abs(Vector3.Angle(this.value.current, target));
				if (num < degreeSpeed * 1.401298E-45f || degreeSpeed == 0f)
				{
					this.value.SetImmediate(ref target);
				}
				else
				{
					this.value.begin = this.value.current;
					this.value.end = target;
					if (degreeSpeed < 0f)
					{
						degreeSpeed = -degreeSpeed;
					}
					this.value.clock.duration = (this.value.clock.remain = (ulong)Math.Ceiling((double)num * 1000.0 / (double)degreeSpeed));
					if (this.value.clock.remain <= 1UL)
					{
						this.value.SetImmediate(ref target);
					}
				}
			}
		}

		// Token: 0x06002AEE RID: 10990 RVA: 0x000AB820 File Offset: 0x000A9A20
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
				this.value.current = Vector3.Slerp(this.value.begin, this.value.end, this.value.clock.percentf);
			}
			return integration;
		}

		// Token: 0x0400177C RID: 6012
		[NonSerialized]
		public Integrated<Vector3> value;
	}
}
