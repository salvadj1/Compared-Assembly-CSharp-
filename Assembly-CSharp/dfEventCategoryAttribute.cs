using System;

// Token: 0x02000787 RID: 1927
[AttributeUsage(AttributeTargets.Delegate, Inherited = true, AllowMultiple = false)]
public class dfEventCategoryAttribute : Attribute
{
	// Token: 0x06004070 RID: 16496 RVA: 0x000EC93C File Offset: 0x000EAB3C
	public dfEventCategoryAttribute(string category)
	{
		this.Category = category;
	}

	// Token: 0x17000C31 RID: 3121
	// (get) Token: 0x06004071 RID: 16497 RVA: 0x000EC94C File Offset: 0x000EAB4C
	// (set) Token: 0x06004072 RID: 16498 RVA: 0x000EC954 File Offset: 0x000EAB54
	public string Category { get; private set; }
}
