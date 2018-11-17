using System;

// Token: 0x02000725 RID: 1829
[dfMarkupTagInfo("a")]
public class dfMarkupTagAnchor : dfMarkupTag
{
	// Token: 0x060042E5 RID: 17125 RVA: 0x00103E64 File Offset: 0x00102064
	public dfMarkupTagAnchor() : base("a")
	{
	}

	// Token: 0x060042E6 RID: 17126 RVA: 0x00103E74 File Offset: 0x00102074
	public dfMarkupTagAnchor(dfMarkupTag original) : base(original)
	{
	}

	// Token: 0x17000D23 RID: 3363
	// (get) Token: 0x060042E7 RID: 17127 RVA: 0x00103E80 File Offset: 0x00102080
	public string HRef
	{
		get
		{
			dfMarkupAttribute dfMarkupAttribute = base.findAttribute(new string[]
			{
				"href"
			});
			return (dfMarkupAttribute == null) ? string.Empty : dfMarkupAttribute.Value;
		}
	}

	// Token: 0x060042E8 RID: 17128 RVA: 0x00103EB8 File Offset: 0x001020B8
	protected override void _PerformLayoutImpl(dfMarkupBox container, dfMarkupStyle style)
	{
		style.TextDecoration = dfMarkupTextDecoration.Underline;
		style = base.applyTextStyleAttributes(style);
		int i = 0;
		while (i < base.ChildNodes.Count)
		{
			dfMarkupElement dfMarkupElement = base.ChildNodes[i];
			if (!(dfMarkupElement is dfMarkupString))
			{
				goto IL_63;
			}
			dfMarkupString dfMarkupString = dfMarkupElement as dfMarkupString;
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
