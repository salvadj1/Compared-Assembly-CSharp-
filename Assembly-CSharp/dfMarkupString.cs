using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

// Token: 0x0200071A RID: 1818
public class dfMarkupString : dfMarkupElement
{
	// Token: 0x0600429C RID: 17052 RVA: 0x00102794 File Offset: 0x00100994
	public dfMarkupString(string text)
	{
		this.Text = this.processWhitespace(dfMarkupEntity.Replace(text));
		this.isWhitespace = dfMarkupString.whitespacePattern.IsMatch(this.Text);
	}

	// Token: 0x17000D16 RID: 3350
	// (get) Token: 0x0600429E RID: 17054 RVA: 0x001027F8 File Offset: 0x001009F8
	// (set) Token: 0x0600429F RID: 17055 RVA: 0x00102800 File Offset: 0x00100A00
	public string Text { get; private set; }

	// Token: 0x17000D17 RID: 3351
	// (get) Token: 0x060042A0 RID: 17056 RVA: 0x0010280C File Offset: 0x00100A0C
	public bool IsWhitespace
	{
		get
		{
			return this.isWhitespace;
		}
	}

	// Token: 0x060042A1 RID: 17057 RVA: 0x00102814 File Offset: 0x00100A14
	public override string ToString()
	{
		return this.Text;
	}

	// Token: 0x060042A2 RID: 17058 RVA: 0x0010281C File Offset: 0x00100A1C
	internal dfMarkupElement SplitWords()
	{
		dfMarkupTagSpan dfMarkupTagSpan = dfMarkupTagSpan.Obtain();
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
				dfMarkupTagSpan.AddChildNode(dfMarkupString.Obtain(this.Text.Substring(num, i - num)));
				num = i;
			}
			while (i < length && this.Text[i] != '\n' && char.IsWhiteSpace(this.Text[i]))
			{
				i++;
			}
			if (i > num)
			{
				dfMarkupTagSpan.AddChildNode(dfMarkupString.Obtain(this.Text.Substring(num, i - num)));
				num = i;
			}
			if (i < length && this.Text[i] == '\n')
			{
				dfMarkupTagSpan.AddChildNode(dfMarkupString.Obtain("\n"));
				i = (num = i + 1);
			}
		}
		return dfMarkupTagSpan;
	}

	// Token: 0x060042A3 RID: 17059 RVA: 0x00102924 File Offset: 0x00100B24
	protected override void _PerformLayoutImpl(dfMarkupBox container, dfMarkupStyle style)
	{
		if (style.Font == null)
		{
			return;
		}
		string text = (!style.PreserveWhitespace && this.isWhitespace) ? " " : this.Text;
		dfMarkupBoxText dfMarkupBoxText = dfMarkupBoxText.Obtain(this, dfMarkupDisplayType.inline, style);
		dfMarkupBoxText.SetText(text);
		container.AddChild(dfMarkupBoxText);
	}

	// Token: 0x060042A4 RID: 17060 RVA: 0x00102984 File Offset: 0x00100B84
	internal static dfMarkupString Obtain(string text)
	{
		if (dfMarkupString.objectPool.Count > 0)
		{
			dfMarkupString dfMarkupString = dfMarkupString.objectPool.Dequeue();
			dfMarkupString.Text = dfMarkupEntity.Replace(text);
			dfMarkupString.isWhitespace = dfMarkupString.whitespacePattern.IsMatch(dfMarkupString.Text);
			return dfMarkupString;
		}
		return new dfMarkupString(text);
	}

	// Token: 0x060042A5 RID: 17061 RVA: 0x001029D8 File Offset: 0x00100BD8
	internal override void Release()
	{
		base.Release();
		dfMarkupString.objectPool.Enqueue(this);
	}

	// Token: 0x060042A6 RID: 17062 RVA: 0x001029EC File Offset: 0x00100BEC
	private string processWhitespace(string text)
	{
		dfMarkupString.buffer.Length = 0;
		dfMarkupString.buffer.Append(text);
		dfMarkupString.buffer.Replace("\r\n", "\n");
		dfMarkupString.buffer.Replace("\r", "\n");
		dfMarkupString.buffer.Replace("\t", "    ");
		return dfMarkupString.buffer.ToString();
	}

	// Token: 0x04002318 RID: 8984
	private static StringBuilder buffer = new StringBuilder();

	// Token: 0x04002319 RID: 8985
	private static Regex whitespacePattern = new Regex("\\s+");

	// Token: 0x0400231A RID: 8986
	private static Queue<dfMarkupString> objectPool = new Queue<dfMarkupString>();

	// Token: 0x0400231B RID: 8987
	private bool isWhitespace;
}
