using System;

// Token: 0x02000442 RID: 1090
public class VisActionMessageExit : global::VisActionMessageEnter
{
	// Token: 0x0600262B RID: 9771 RVA: 0x0008AFB4 File Offset: 0x000891B4
	public override void Accomplish(IDMain self, IDMain instigator)
	{
	}

	// Token: 0x0600262C RID: 9772 RVA: 0x0008AFB8 File Offset: 0x000891B8
	public override void UnAcomplish(IDMain self, IDMain instigator)
	{
		base.Accomplish(self, instigator);
	}
}
