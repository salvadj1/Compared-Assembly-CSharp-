using System;

// Token: 0x0200063B RID: 1595
public class RustServerManagement : ServerManagement
{
	// Token: 0x060037E5 RID: 14309 RVA: 0x000CD178 File Offset: 0x000CB378
	public new static RustServerManagement Get()
	{
		return (RustServerManagement)ServerManagement.Get();
	}
}
