using System;

// Token: 0x02000805 RID: 2053
[global::dfMarkupTagInfo("strong")]
[global::dfMarkupTagInfo("b")]
public class dfMarkupTagBold : global::dfMarkupTag
{
	// Token: 0x06004739 RID: 18233 RVA: 0x0010D75C File Offset: 0x0010B95C
	public dfMarkupTagBold() : base("b")
	{
	}

	// Token: 0x0600473A RID: 18234 RVA: 0x0010D76C File Offset: 0x0010B96C
	public dfMarkupTagBold(global::dfMarkupTag original) : base(original)
	{
	}

	// Token: 0x0600473B RID: 18235 RVA: 0x0010D778 File Offset: 0x0010B978
	protected override void _PerformLayoutImpl(global::dfMarkupBox container, global::dfMarkupStyle style)
	{
		style = base.applyTextStyleAttributes(style);
		if (style.FontStyle == null)
		{
			style.FontStyle = 1;
		}
		else if (style.FontStyle == 2)
		{
			style.FontStyle = 3;
		}
		base._PerformLayoutImpl(container, style);
	}
}
