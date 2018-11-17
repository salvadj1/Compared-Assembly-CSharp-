using System;

// Token: 0x020007F4 RID: 2036
[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
public class dfMarkupTagInfoAttribute : Attribute
{
	// Token: 0x060046D4 RID: 18132 RVA: 0x0010BA0C File Offset: 0x00109C0C
	public dfMarkupTagInfoAttribute(string tagName)
	{
		this.TagName = tagName;
	}

	// Token: 0x17000D9D RID: 3485
	// (get) Token: 0x060046D5 RID: 18133 RVA: 0x0010BA1C File Offset: 0x00109C1C
	// (set) Token: 0x060046D6 RID: 18134 RVA: 0x0010BA24 File Offset: 0x00109C24
	public string TagName { get; set; }
}
