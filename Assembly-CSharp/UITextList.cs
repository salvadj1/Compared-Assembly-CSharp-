using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020008F8 RID: 2296
[AddComponentMenu("NGUI/UI/Text List")]
public class UITextList : MonoBehaviour
{
	// Token: 0x06004E6C RID: 20076 RVA: 0x00143924 File Offset: 0x00141B24
	public void Clear()
	{
		this.mParagraphs.Clear();
		this.UpdateVisibleText();
	}

	// Token: 0x06004E6D RID: 20077 RVA: 0x00143938 File Offset: 0x00141B38
	public void Add(string text)
	{
		this.Add(text, true);
	}

	// Token: 0x06004E6E RID: 20078 RVA: 0x00143944 File Offset: 0x00141B44
	protected void Add(string text, bool updateVisible)
	{
		global::UITextList.Paragraph paragraph;
		if (this.mParagraphs.Count < this.maxEntries)
		{
			paragraph = new global::UITextList.Paragraph();
		}
		else
		{
			paragraph = this.mParagraphs[0];
			this.mParagraphs.RemoveAt(0);
		}
		paragraph.text = text;
		this.mParagraphs.Add(paragraph);
		if (this.textLabel != null && this.textLabel.font != null)
		{
			paragraph.lines = this.textLabel.font.WrapText(global::UIFont.tempMarkup, paragraph.text, this.maxWidth / this.textLabel.transform.localScale.y, this.textLabel.maxLineCount, this.textLabel.supportEncoding, this.textLabel.symbolStyle).Split(this.mSeparator);
			this.mTotalLines = 0;
			int i = 0;
			int count = this.mParagraphs.Count;
			while (i < count)
			{
				this.mTotalLines += this.mParagraphs[i].lines.Length;
				i++;
			}
		}
		if (updateVisible)
		{
			this.UpdateVisibleText();
		}
	}

	// Token: 0x06004E6F RID: 20079 RVA: 0x00143A84 File Offset: 0x00141C84
	private void Awake()
	{
		if (this.textLabel == null)
		{
			this.textLabel = base.GetComponentInChildren<global::UILabel>();
		}
		if (this.textLabel != null)
		{
			this.textLabel.lineWidth = 0;
		}
		Collider collider = base.collider;
		if (collider != null)
		{
			if (this.maxHeight <= 0f)
			{
				this.maxHeight = collider.bounds.size.y / base.transform.lossyScale.y;
			}
			if (this.maxWidth <= 0f)
			{
				this.maxWidth = collider.bounds.size.x / base.transform.lossyScale.x;
			}
		}
	}

	// Token: 0x06004E70 RID: 20080 RVA: 0x00143B64 File Offset: 0x00141D64
	private void OnSelect(bool selected)
	{
		this.mSelected = selected;
	}

	// Token: 0x06004E71 RID: 20081 RVA: 0x00143B70 File Offset: 0x00141D70
	protected void UpdateVisibleText()
	{
		if (this.textLabel != null)
		{
			global::UIFont font = this.textLabel.font;
			if (font != null)
			{
				int num = 0;
				int num2 = (this.maxHeight <= 0f) ? 100000 : Mathf.FloorToInt(this.maxHeight / this.textLabel.cachedTransform.localScale.y);
				int num3 = Mathf.RoundToInt(this.mScroll);
				if (num2 + num3 > this.mTotalLines)
				{
					num3 = Mathf.Max(0, this.mTotalLines - num2);
					this.mScroll = (float)num3;
				}
				if (this.style == global::UITextList.Style.Chat)
				{
					num3 = Mathf.Max(0, this.mTotalLines - num2 - num3);
				}
				string text = string.Empty;
				int i = 0;
				int count = this.mParagraphs.Count;
				while (i < count)
				{
					global::UITextList.Paragraph paragraph = this.mParagraphs[i];
					int j = 0;
					int num4 = paragraph.lines.Length;
					while (j < num4)
					{
						string str = paragraph.lines[j];
						if (num3 > 0)
						{
							num3--;
						}
						else
						{
							if (text.Length > 0)
							{
								text += "\n";
							}
							text += str;
							num++;
							if (num >= num2)
							{
								break;
							}
						}
						j++;
					}
					if (num >= num2)
					{
						break;
					}
					i++;
				}
				this.textLabel.text = text;
			}
		}
	}

	// Token: 0x06004E72 RID: 20082 RVA: 0x00143D00 File Offset: 0x00141F00
	private void OnScroll(float val)
	{
		if (this.mSelected && this.supportScrollWheel)
		{
			val *= ((this.style != global::UITextList.Style.Chat) ? -10f : 10f);
			this.mScroll = Mathf.Max(0f, this.mScroll + val);
			this.UpdateVisibleText();
		}
	}

	// Token: 0x04002BFF RID: 11263
	public global::UITextList.Style style;

	// Token: 0x04002C00 RID: 11264
	public global::UILabel textLabel;

	// Token: 0x04002C01 RID: 11265
	public float maxWidth;

	// Token: 0x04002C02 RID: 11266
	public float maxHeight;

	// Token: 0x04002C03 RID: 11267
	public int maxEntries = 50;

	// Token: 0x04002C04 RID: 11268
	public bool supportScrollWheel = true;

	// Token: 0x04002C05 RID: 11269
	protected char[] mSeparator = new char[]
	{
		'\n'
	};

	// Token: 0x04002C06 RID: 11270
	protected List<global::UITextList.Paragraph> mParagraphs = new List<global::UITextList.Paragraph>();

	// Token: 0x04002C07 RID: 11271
	protected float mScroll;

	// Token: 0x04002C08 RID: 11272
	protected bool mSelected;

	// Token: 0x04002C09 RID: 11273
	protected int mTotalLines;

	// Token: 0x020008F9 RID: 2297
	public enum Style
	{
		// Token: 0x04002C0B RID: 11275
		Text,
		// Token: 0x04002C0C RID: 11276
		Chat
	}

	// Token: 0x020008FA RID: 2298
	protected class Paragraph
	{
		// Token: 0x04002C0D RID: 11277
		public string text;

		// Token: 0x04002C0E RID: 11278
		public string[] lines;
	}
}
