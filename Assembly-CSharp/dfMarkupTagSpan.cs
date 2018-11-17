using System;
using System.Collections.Generic;

// Token: 0x02000724 RID: 1828
[dfMarkupTagInfo("span")]
public class dfMarkupTagSpan : dfMarkupTag
{
	// Token: 0x060042DF RID: 17119 RVA: 0x00103D70 File Offset: 0x00101F70
	public dfMarkupTagSpan() : base("span")
	{
	}

	// Token: 0x060042E0 RID: 17120 RVA: 0x00103D80 File Offset: 0x00101F80
	public dfMarkupTagSpan(dfMarkupTag original) : base(original)
	{
	}

	// Token: 0x060042E2 RID: 17122 RVA: 0x00103D98 File Offset: 0x00101F98
	protected override void _PerformLayoutImpl(dfMarkupBox container, dfMarkupStyle style)
	{
		style = base.applyTextStyleAttributes(style);
		int i = 0;
		while (i < base.ChildNodes.Count)
		{
			dfMarkupElement dfMarkupElement = base.ChildNodes[i];
			if (!(dfMarkupElement is dfMarkupString))
			{
				goto IL_5B;
			}
			dfMarkupString dfMarkupString = dfMarkupElement as dfMarkupString;
			if (!(dfMarkupString.Text == "\n"))
			{
				goto IL_5B;
			}
			if (style.PreserveWhitespace)
			{
				container.AddLineBreak();
			}
			IL_63:
			i++;
			continue;
			IL_5B:
			dfMarkupElement.PerformLayout(container, style);
			goto IL_63;
		}
	}

	// Token: 0x060042E3 RID: 17123 RVA: 0x00103E20 File Offset: 0x00102020
	internal static dfMarkupTagSpan Obtain()
	{
		if (dfMarkupTagSpan.objectPool.Count > 0)
		{
			return dfMarkupTagSpan.objectPool.Dequeue();
		}
		return new dfMarkupTagSpan();
	}

	// Token: 0x060042E4 RID: 17124 RVA: 0x00103E50 File Offset: 0x00102050
	internal override void Release()
	{
		base.Release();
		dfMarkupTagSpan.objectPool.Enqueue(this);
	}

	// Token: 0x04002353 RID: 9043
	private static Queue<dfMarkupTagSpan> objectPool = new Queue<dfMarkupTagSpan>();
}
