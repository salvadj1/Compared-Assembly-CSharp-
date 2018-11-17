using System;

// Token: 0x020007F7 RID: 2039
public class dfMarkupAttribute
{
	// Token: 0x060046EB RID: 18155 RVA: 0x0010BD6C File Offset: 0x00109F6C
	public dfMarkupAttribute(string name, string value)
	{
		this.Name = name;
		this.Value = value;
	}

	// Token: 0x17000DA2 RID: 3490
	// (get) Token: 0x060046EC RID: 18156 RVA: 0x0010BD84 File Offset: 0x00109F84
	// (set) Token: 0x060046ED RID: 18157 RVA: 0x0010BD8C File Offset: 0x00109F8C
	public string Name { get; set; }

	// Token: 0x17000DA3 RID: 3491
	// (get) Token: 0x060046EE RID: 18158 RVA: 0x0010BD98 File Offset: 0x00109F98
	// (set) Token: 0x060046EF RID: 18159 RVA: 0x0010BDA0 File Offset: 0x00109FA0
	public string Value { get; set; }

	// Token: 0x060046F0 RID: 18160 RVA: 0x0010BDAC File Offset: 0x00109FAC
	public override string ToString()
	{
		return string.Format("{0}='{1}'", this.Name, this.Value);
	}
}
