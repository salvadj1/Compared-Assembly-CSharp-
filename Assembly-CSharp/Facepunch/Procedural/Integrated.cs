using System;

namespace Facepunch.Procedural
{
	// Token: 0x020004E9 RID: 1257
	public struct Integrated<T> where T : struct
	{
		// Token: 0x06002AEC RID: 10988 RVA: 0x000AB6DC File Offset: 0x000A98DC
		public void SetImmediate(ref T value)
		{
			this.begin = (this.end = (this.current = value));
			this.clock.SetImmediate();
		}

		// Token: 0x04001778 RID: 6008
		[NonSerialized]
		public MillisClock clock;

		// Token: 0x04001779 RID: 6009
		[NonSerialized]
		public T begin;

		// Token: 0x0400177A RID: 6010
		[NonSerialized]
		public T end;

		// Token: 0x0400177B RID: 6011
		[NonSerialized]
		public T current;
	}
}
