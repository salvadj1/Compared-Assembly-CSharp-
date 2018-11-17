using System;

// Token: 0x0200071B RID: 1819
public class dfMarkupAttribute
{
	// Token: 0x060042A7 RID: 17063 RVA: 0x00102A5C File Offset: 0x00100C5C
	public dfMarkupAttribute(string name, string value)
	{
		this.Name = name;
		this.Value = value;
	}

	// Token: 0x17000D18 RID: 3352
	// (get) Token: 0x060042A8 RID: 17064 RVA: 0x00102A74 File Offset: 0x00100C74
	// (set) Token: 0x060042A9 RID: 17065 RVA: 0x00102A7C File Offset: 0x00100C7C
	public string Name { get; set; }

	// Token: 0x17000D19 RID: 3353
	// (get) Token: 0x060042AA RID: 17066 RVA: 0x00102A88 File Offset: 0x00100C88
	// (set) Token: 0x060042AB RID: 17067 RVA: 0x00102A90 File Offset: 0x00100C90
	public string Value { get; set; }

	// Token: 0x060042AC RID: 17068 RVA: 0x00102A9C File Offset: 0x00100C9C
	public override string ToString()
	{
		return string.Format("{0}='{1}'", this.Name, this.Value);
	}
}
