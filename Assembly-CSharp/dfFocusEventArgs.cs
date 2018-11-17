using System;

// Token: 0x020006BE RID: 1726
public class dfFocusEventArgs : dfControlEventArgs
{
	// Token: 0x06003C6F RID: 15471 RVA: 0x000E3E60 File Offset: 0x000E2060
	internal dfFocusEventArgs(dfControl GotFocus, dfControl LostFocus) : base(GotFocus)
	{
		this.LostFocus = LostFocus;
	}

	// Token: 0x17000BB0 RID: 2992
	// (get) Token: 0x06003C70 RID: 15472 RVA: 0x000E3E70 File Offset: 0x000E2070
	public dfControl GotFocus
	{
		get
		{
			return base.Source;
		}
	}

	// Token: 0x17000BB1 RID: 2993
	// (get) Token: 0x06003C71 RID: 15473 RVA: 0x000E3E78 File Offset: 0x000E2078
	// (set) Token: 0x06003C72 RID: 15474 RVA: 0x000E3E80 File Offset: 0x000E2080
	public dfControl LostFocus { get; private set; }
}
