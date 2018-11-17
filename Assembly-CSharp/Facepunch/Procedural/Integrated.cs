using System;

namespace Facepunch.Procedural
{
	// Token: 0x020005A4 RID: 1444
	public struct Integrated<T> where T : struct
	{
		// Token: 0x06002E9E RID: 11934 RVA: 0x000B3474 File Offset: 0x000B1674
		public void SetImmediate(ref T value)
		{
			this.begin = (this.end = (this.current = value));
			this.clock.SetImmediate();
		}

		// Token: 0x04001935 RID: 6453
		[NonSerialized]
		public MillisClock clock;

		// Token: 0x04001936 RID: 6454
		[NonSerialized]
		public T begin;

		// Token: 0x04001937 RID: 6455
		[NonSerialized]
		public T end;

		// Token: 0x04001938 RID: 6456
		[NonSerialized]
		public T current;
	}
}
