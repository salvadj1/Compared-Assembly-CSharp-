using System;

// Token: 0x020002D8 RID: 728
public abstract class CCTotem<TTotemObject> : global::CCTotem where TTotemObject : global::CCTotem.TotemicObject
{
	// Token: 0x06001961 RID: 6497 RVA: 0x00061804 File Offset: 0x0005FA04
	internal CCTotem()
	{
	}

	// Token: 0x1700073E RID: 1854
	// (get) Token: 0x06001962 RID: 6498 RVA: 0x0006180C File Offset: 0x0005FA0C
	internal sealed override global::CCTotem.TotemicObject _Object
	{
		get
		{
			return this.totemicObject;
		}
	}

	// Token: 0x04000DE7 RID: 3559
	protected internal new TTotemObject totemicObject;
}
