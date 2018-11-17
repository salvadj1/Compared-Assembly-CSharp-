using System;

// Token: 0x02000718 RID: 1816
[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
public class dfMarkupTagInfoAttribute : Attribute
{
	// Token: 0x06004290 RID: 17040 RVA: 0x001026FC File Offset: 0x001008FC
	public dfMarkupTagInfoAttribute(string tagName)
	{
		this.TagName = tagName;
	}

	// Token: 0x17000D13 RID: 3347
	// (get) Token: 0x06004291 RID: 17041 RVA: 0x0010270C File Offset: 0x0010090C
	// (set) Token: 0x06004292 RID: 17042 RVA: 0x00102714 File Offset: 0x00100914
	public string TagName { get; set; }
}
