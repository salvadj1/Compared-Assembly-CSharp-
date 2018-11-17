using System;

// Token: 0x02000395 RID: 917
public class VisActionMessageExit : VisActionMessageEnter
{
	// Token: 0x060022C9 RID: 8905 RVA: 0x00085BB8 File Offset: 0x00083DB8
	public override void Accomplish(IDMain self, IDMain instigator)
	{
	}

	// Token: 0x060022CA RID: 8906 RVA: 0x00085BBC File Offset: 0x00083DBC
	public override void UnAcomplish(IDMain self, IDMain instigator)
	{
		base.Accomplish(self, instigator);
	}
}
