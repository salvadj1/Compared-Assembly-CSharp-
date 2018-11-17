using System;

// Token: 0x02000809 RID: 2057
[global::dfMarkupTagInfo("br")]
public class dfMarkupTagBr : global::dfMarkupTag
{
	// Token: 0x06004746 RID: 18246 RVA: 0x0010DB44 File Offset: 0x0010BD44
	public dfMarkupTagBr() : base("br")
	{
		this.IsClosedTag = true;
	}

	// Token: 0x06004747 RID: 18247 RVA: 0x0010DB58 File Offset: 0x0010BD58
	public dfMarkupTagBr(global::dfMarkupTag original) : base(original)
	{
		this.IsClosedTag = true;
	}

	// Token: 0x06004748 RID: 18248 RVA: 0x0010DB68 File Offset: 0x0010BD68
	protected override void _PerformLayoutImpl(global::dfMarkupBox container, global::dfMarkupStyle style)
	{
		container.AddLineBreak();
	}
}
