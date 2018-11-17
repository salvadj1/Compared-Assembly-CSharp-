using System;

// Token: 0x020006BC RID: 1724
[AttributeUsage(AttributeTargets.Delegate, Inherited = true, AllowMultiple = false)]
public class dfEventCategoryAttribute : Attribute
{
	// Token: 0x06003C66 RID: 15462 RVA: 0x000E3DF8 File Offset: 0x000E1FF8
	public dfEventCategoryAttribute(string category)
	{
		this.Category = category;
	}

	// Token: 0x17000BAD RID: 2989
	// (get) Token: 0x06003C67 RID: 15463 RVA: 0x000E3E08 File Offset: 0x000E2008
	// (set) Token: 0x06003C68 RID: 15464 RVA: 0x000E3E10 File Offset: 0x000E2010
	public string Category { get; private set; }
}
