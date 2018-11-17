using System;

// Token: 0x02000801 RID: 2049
[global::dfMarkupTagInfo("a")]
public class dfMarkupTagAnchor : global::dfMarkupTag
{
	// Token: 0x06004729 RID: 18217 RVA: 0x0010D174 File Offset: 0x0010B374
	public dfMarkupTagAnchor() : base("a")
	{
	}

	// Token: 0x0600472A RID: 18218 RVA: 0x0010D184 File Offset: 0x0010B384
	public dfMarkupTagAnchor(global::dfMarkupTag original) : base(original)
	{
	}

	// Token: 0x17000DAD RID: 3501
	// (get) Token: 0x0600472B RID: 18219 RVA: 0x0010D190 File Offset: 0x0010B390
	public string HRef
	{
		get
		{
			global::dfMarkupAttribute dfMarkupAttribute = base.findAttribute(new string[]
			{
				"href"
			});
			return (dfMarkupAttribute == null) ? string.Empty : dfMarkupAttribute.Value;
		}
	}

	// Token: 0x0600472C RID: 18220 RVA: 0x0010D1C8 File Offset: 0x0010B3C8
	protected override void _PerformLayoutImpl(global::dfMarkupBox container, global::dfMarkupStyle style)
	{
		style.TextDecoration = global::dfMarkupTextDecoration.Underline;
		style = base.applyTextStyleAttributes(style);
		int i = 0;
		while (i < base.ChildNodes.Count)
		{
			global::dfMarkupElement dfMarkupElement = base.ChildNodes[i];
			if (!(dfMarkupElement is global::dfMarkupString))
			{
				goto IL_63;
			}
			global::dfMarkupString dfMarkupString = dfMarkupElement as global::dfMarkupString;
			if (!(dfMarkupString.Text == "\n"))
			{
				goto IL_63;
			}
			if (style.PreserveWhitespace)
			{
				container.AddLineBreak();
			}
			IL_6B:
			i++;
			continue;
			IL_63:
			dfMarkupElement.PerformLayout(container, style);
			goto IL_6B;
		}
	}
}
