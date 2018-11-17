using System;

// Token: 0x0200029B RID: 667
public abstract class CCTotem<TTotemObject> : CCTotem where TTotemObject : CCTotem.TotemicObject
{
	// Token: 0x060017D1 RID: 6097 RVA: 0x0005CE90 File Offset: 0x0005B090
	internal CCTotem()
	{
	}

	// Token: 0x170006EA RID: 1770
	// (get) Token: 0x060017D2 RID: 6098 RVA: 0x0005CE98 File Offset: 0x0005B098
	internal sealed override CCTotem.TotemicObject _Object
	{
		get
		{
			return this.totemicObject;
		}
	}

	// Token: 0x04000CAC RID: 3244
	protected internal new TTotemObject totemicObject;
}
