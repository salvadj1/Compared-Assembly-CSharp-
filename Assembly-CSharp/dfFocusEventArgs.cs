using System;

// Token: 0x02000789 RID: 1929
public class dfFocusEventArgs : global::dfControlEventArgs
{
	// Token: 0x06004079 RID: 16505 RVA: 0x000EC9A4 File Offset: 0x000EABA4
	internal dfFocusEventArgs(global::dfControl GotFocus, global::dfControl LostFocus) : base(GotFocus)
	{
		this.LostFocus = LostFocus;
	}

	// Token: 0x17000C34 RID: 3124
	// (get) Token: 0x0600407A RID: 16506 RVA: 0x000EC9B4 File Offset: 0x000EABB4
	public global::dfControl GotFocus
	{
		get
		{
			return base.Source;
		}
	}

	// Token: 0x17000C35 RID: 3125
	// (get) Token: 0x0600407B RID: 16507 RVA: 0x000EC9BC File Offset: 0x000EABBC
	// (set) Token: 0x0600407C RID: 16508 RVA: 0x000EC9C4 File Offset: 0x000EABC4
	public global::dfControl LostFocus { get; private set; }
}
