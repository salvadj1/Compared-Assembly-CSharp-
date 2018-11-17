using System;

// Token: 0x02000788 RID: 1928
public class dfControlEventArgs
{
	// Token: 0x06004073 RID: 16499 RVA: 0x000EC960 File Offset: 0x000EAB60
	internal dfControlEventArgs(global::dfControl Target)
	{
		this.Source = Target;
	}

	// Token: 0x17000C32 RID: 3122
	// (get) Token: 0x06004074 RID: 16500 RVA: 0x000EC970 File Offset: 0x000EAB70
	// (set) Token: 0x06004075 RID: 16501 RVA: 0x000EC978 File Offset: 0x000EAB78
	public global::dfControl Source { get; private set; }

	// Token: 0x17000C33 RID: 3123
	// (get) Token: 0x06004076 RID: 16502 RVA: 0x000EC984 File Offset: 0x000EAB84
	// (set) Token: 0x06004077 RID: 16503 RVA: 0x000EC98C File Offset: 0x000EAB8C
	public bool Used { get; private set; }

	// Token: 0x06004078 RID: 16504 RVA: 0x000EC998 File Offset: 0x000EAB98
	public void Use()
	{
		this.Used = true;
	}
}
