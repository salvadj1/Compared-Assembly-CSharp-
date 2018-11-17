using System;

// Token: 0x02000807 RID: 2055
[global::dfMarkupTagInfo("i")]
[global::dfMarkupTagInfo("em")]
public class dfMarkupTagItalic : global::dfMarkupTag
{
	// Token: 0x06004740 RID: 18240 RVA: 0x0010D9B8 File Offset: 0x0010BBB8
	public dfMarkupTagItalic() : base("i")
	{
	}

	// Token: 0x06004741 RID: 18241 RVA: 0x0010D9C8 File Offset: 0x0010BBC8
	public dfMarkupTagItalic(global::dfMarkupTag original) : base(original)
	{
	}

	// Token: 0x06004742 RID: 18242 RVA: 0x0010D9D4 File Offset: 0x0010BBD4
	protected override void _PerformLayoutImpl(global::dfMarkupBox container, global::dfMarkupStyle style)
	{
		style = base.applyTextStyleAttributes(style);
		if (style.FontStyle == null)
		{
			style.FontStyle = 2;
		}
		else if (style.FontStyle == 1)
		{
			style.FontStyle = 3;
		}
		base._PerformLayoutImpl(container, style);
	}
}
