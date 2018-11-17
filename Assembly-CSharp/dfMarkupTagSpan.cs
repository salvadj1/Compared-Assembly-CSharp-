using System;
using System.Collections.Generic;

// Token: 0x02000800 RID: 2048
[global::dfMarkupTagInfo("span")]
public class dfMarkupTagSpan : global::dfMarkupTag
{
	// Token: 0x06004723 RID: 18211 RVA: 0x0010D080 File Offset: 0x0010B280
	public dfMarkupTagSpan() : base("span")
	{
	}

	// Token: 0x06004724 RID: 18212 RVA: 0x0010D090 File Offset: 0x0010B290
	public dfMarkupTagSpan(global::dfMarkupTag original) : base(original)
	{
	}

	// Token: 0x06004726 RID: 18214 RVA: 0x0010D0A8 File Offset: 0x0010B2A8
	protected override void _PerformLayoutImpl(global::dfMarkupBox container, global::dfMarkupStyle style)
	{
		style = base.applyTextStyleAttributes(style);
		int i = 0;
		while (i < base.ChildNodes.Count)
		{
			global::dfMarkupElement dfMarkupElement = base.ChildNodes[i];
			if (!(dfMarkupElement is global::dfMarkupString))
			{
				goto IL_5B;
			}
			global::dfMarkupString dfMarkupString = dfMarkupElement as global::dfMarkupString;
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

	// Token: 0x06004727 RID: 18215 RVA: 0x0010D130 File Offset: 0x0010B330
	internal static global::dfMarkupTagSpan Obtain()
	{
		if (global::dfMarkupTagSpan.objectPool.Count > 0)
		{
			return global::dfMarkupTagSpan.objectPool.Dequeue();
		}
		return new global::dfMarkupTagSpan();
	}

	// Token: 0x06004728 RID: 18216 RVA: 0x0010D160 File Offset: 0x0010B360
	internal override void Release()
	{
		base.Release();
		global::dfMarkupTagSpan.objectPool.Enqueue(this);
	}

	// Token: 0x04002576 RID: 9590
	private static Queue<global::dfMarkupTagSpan> objectPool = new Queue<global::dfMarkupTagSpan>();
}
