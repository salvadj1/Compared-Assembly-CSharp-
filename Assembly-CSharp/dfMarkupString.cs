using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

// Token: 0x020007F6 RID: 2038
public class dfMarkupString : global::dfMarkupElement
{
	// Token: 0x060046E0 RID: 18144 RVA: 0x0010BAA4 File Offset: 0x00109CA4
	public dfMarkupString(string text)
	{
		this.Text = this.processWhitespace(global::dfMarkupEntity.Replace(text));
		this.isWhitespace = global::dfMarkupString.whitespacePattern.IsMatch(this.Text);
	}

	// Token: 0x17000DA0 RID: 3488
	// (get) Token: 0x060046E2 RID: 18146 RVA: 0x0010BB08 File Offset: 0x00109D08
	// (set) Token: 0x060046E3 RID: 18147 RVA: 0x0010BB10 File Offset: 0x00109D10
	public string Text { get; private set; }

	// Token: 0x17000DA1 RID: 3489
	// (get) Token: 0x060046E4 RID: 18148 RVA: 0x0010BB1C File Offset: 0x00109D1C
	public bool IsWhitespace
	{
		get
		{
			return this.isWhitespace;
		}
	}

	// Token: 0x060046E5 RID: 18149 RVA: 0x0010BB24 File Offset: 0x00109D24
	public override string ToString()
	{
		return this.Text;
	}

	// Token: 0x060046E6 RID: 18150 RVA: 0x0010BB2C File Offset: 0x00109D2C
	internal global::dfMarkupElement SplitWords()
	{
		global::dfMarkupTagSpan dfMarkupTagSpan = global::dfMarkupTagSpan.Obtain();
		int i = 0;
		int num = 0;
		int length = this.Text.Length;
		while (i < length)
		{
			while (i < length && !char.IsWhiteSpace(this.Text[i]))
			{
				i++;
			}
			if (i > num)
			{
				dfMarkupTagSpan.AddChildNode(global::dfMarkupString.Obtain(this.Text.Substring(num, i - num)));
				num = i;
			}
			while (i < length && this.Text[i] != '\n' && char.IsWhiteSpace(this.Text[i]))
			{
				i++;
			}
			if (i > num)
			{
				dfMarkupTagSpan.AddChildNode(global::dfMarkupString.Obtain(this.Text.Substring(num, i - num)));
				num = i;
			}
			if (i < length && this.Text[i] == '\n')
			{
				dfMarkupTagSpan.AddChildNode(global::dfMarkupString.Obtain("\n"));
				i = (num = i + 1);
			}
		}
		return dfMarkupTagSpan;
	}

	// Token: 0x060046E7 RID: 18151 RVA: 0x0010BC34 File Offset: 0x00109E34
	protected override void _PerformLayoutImpl(global::dfMarkupBox container, global::dfMarkupStyle style)
	{
		if (style.Font == null)
		{
			return;
		}
		string text = (!style.PreserveWhitespace && this.isWhitespace) ? " " : this.Text;
		global::dfMarkupBoxText dfMarkupBoxText = global::dfMarkupBoxText.Obtain(this, global::dfMarkupDisplayType.inline, style);
		dfMarkupBoxText.SetText(text);
		container.AddChild(dfMarkupBoxText);
	}

	// Token: 0x060046E8 RID: 18152 RVA: 0x0010BC94 File Offset: 0x00109E94
	internal static global::dfMarkupString Obtain(string text)
	{
		if (global::dfMarkupString.objectPool.Count > 0)
		{
			global::dfMarkupString dfMarkupString = global::dfMarkupString.objectPool.Dequeue();
			dfMarkupString.Text = global::dfMarkupEntity.Replace(text);
			dfMarkupString.isWhitespace = global::dfMarkupString.whitespacePattern.IsMatch(dfMarkupString.Text);
			return dfMarkupString;
		}
		return new global::dfMarkupString(text);
	}

	// Token: 0x060046E9 RID: 18153 RVA: 0x0010BCE8 File Offset: 0x00109EE8
	internal override void Release()
	{
		base.Release();
		global::dfMarkupString.objectPool.Enqueue(this);
	}

	// Token: 0x060046EA RID: 18154 RVA: 0x0010BCFC File Offset: 0x00109EFC
	private string processWhitespace(string text)
	{
		global::dfMarkupString.buffer.Length = 0;
		global::dfMarkupString.buffer.Append(text);
		global::dfMarkupString.buffer.Replace("\r\n", "\n");
		global::dfMarkupString.buffer.Replace("\r", "\n");
		global::dfMarkupString.buffer.Replace("\t", "    ");
		return global::dfMarkupString.buffer.ToString();
	}

	// Token: 0x0400253B RID: 9531
	private static StringBuilder buffer = new StringBuilder();

	// Token: 0x0400253C RID: 9532
	private static Regex whitespacePattern = new Regex("\\s+");

	// Token: 0x0400253D RID: 9533
	private static Queue<global::dfMarkupString> objectPool = new Queue<global::dfMarkupString>();

	// Token: 0x0400253E RID: 9534
	private bool isWhitespace;
}
