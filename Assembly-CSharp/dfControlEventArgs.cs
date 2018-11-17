using System;

// Token: 0x020006BD RID: 1725
public class dfControlEventArgs
{
	// Token: 0x06003C69 RID: 15465 RVA: 0x000E3E1C File Offset: 0x000E201C
	internal dfControlEventArgs(dfControl Target)
	{
		this.Source = Target;
	}

	// Token: 0x17000BAE RID: 2990
	// (get) Token: 0x06003C6A RID: 15466 RVA: 0x000E3E2C File Offset: 0x000E202C
	// (set) Token: 0x06003C6B RID: 15467 RVA: 0x000E3E34 File Offset: 0x000E2034
	public dfControl Source { get; private set; }

	// Token: 0x17000BAF RID: 2991
	// (get) Token: 0x06003C6C RID: 15468 RVA: 0x000E3E40 File Offset: 0x000E2040
	// (set) Token: 0x06003C6D RID: 15469 RVA: 0x000E3E48 File Offset: 0x000E2048
	public bool Used { get; private set; }

	// Token: 0x06003C6E RID: 15470 RVA: 0x000E3E54 File Offset: 0x000E2054
	public void Use()
	{
		this.Used = true;
	}
}
