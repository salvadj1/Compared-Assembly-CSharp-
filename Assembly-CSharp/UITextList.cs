using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000806 RID: 2054
[AddComponentMenu("NGUI/UI/Text List")]
public class UITextList : MonoBehaviour
{
	// Token: 0x060049BD RID: 18877 RVA: 0x001399C0 File Offset: 0x00137BC0
	public void Clear()
	{
		this.mParagraphs.Clear();
		this.UpdateVisibleText();
	}

	// Token: 0x060049BE RID: 18878 RVA: 0x001399D4 File Offset: 0x00137BD4
	public void Add(string text)
	{
		this.Add(text, true);
	}

	// Token: 0x060049BF RID: 18879 RVA: 0x001399E0 File Offset: 0x00137BE0
	protected void Add(string text, bool updateVisible)
	{
		UITextList.Paragraph paragraph;
		if (this.mParagraphs.Count < this.maxEntries)
		{
			paragraph = new UITextList.Paragraph();
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
			paragraph.lines = this.textLabel.font.WrapText(UIFont.tempMarkup, paragraph.text, this.maxWidth / this.textLabel.transform.localScale.y, this.textLabel.maxLineCount, this.textLabel.supportEncoding, this.textLabel.symbolStyle).Split(this.mSeparator);
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

	// Token: 0x060049C0 RID: 18880 RVA: 0x00139B20 File Offset: 0x00137D20
	private void Awake()
	{
		if (this.textLabel == null)
		{
			this.textLabel = base.GetComponentInChildren<UILabel>();
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

	// Token: 0x060049C1 RID: 18881 RVA: 0x00139C00 File Offset: 0x00137E00
	private void OnSelect(bool selected)
	{
		this.mSelected = selected;
	}

	// Token: 0x060049C2 RID: 18882 RVA: 0x00139C0C File Offset: 0x00137E0C
	protected void UpdateVisibleText()
	{
		if (this.textLabel != null)
		{
			UIFont font = this.textLabel.font;
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
				if (this.style == UITextList.Style.Chat)
				{
					num3 = Mathf.Max(0, this.mTotalLines - num2 - num3);
				}
				string text = string.Empty;
				int i = 0;
				int count = this.mParagraphs.Count;
				while (i < count)
				{
					UITextList.Paragraph paragraph = this.mParagraphs[i];
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

	// Token: 0x060049C3 RID: 18883 RVA: 0x00139D9C File Offset: 0x00137F9C
	private void OnScroll(float val)
	{
		if (this.mSelected && this.supportScrollWheel)
		{
			val *= ((this.style != UITextList.Style.Chat) ? -10f : 10f);
			this.mScroll = Mathf.Max(0f, this.mScroll + val);
			this.UpdateVisibleText();
		}
	}

	// Token: 0x040029B1 RID: 10673
	public UITextList.Style style;

	// Token: 0x040029B2 RID: 10674
	public UILabel textLabel;

	// Token: 0x040029B3 RID: 10675
	public float maxWidth;

	// Token: 0x040029B4 RID: 10676
	public float maxHeight;

	// Token: 0x040029B5 RID: 10677
	public int maxEntries = 50;

	// Token: 0x040029B6 RID: 10678
	public bool supportScrollWheel = true;

	// Token: 0x040029B7 RID: 10679
	protected char[] mSeparator = new char[]
	{
		'\n'
	};

	// Token: 0x040029B8 RID: 10680
	protected List<UITextList.Paragraph> mParagraphs = new List<UITextList.Paragraph>();

	// Token: 0x040029B9 RID: 10681
	protected float mScroll;

	// Token: 0x040029BA RID: 10682
	protected bool mSelected;

	// Token: 0x040029BB RID: 10683
	protected int mTotalLines;

	// Token: 0x02000807 RID: 2055
	public enum Style
	{
		// Token: 0x040029BD RID: 10685
		Text,
		// Token: 0x040029BE RID: 10686
		Chat
	}

	// Token: 0x02000808 RID: 2056
	protected class Paragraph
	{
		// Token: 0x040029BF RID: 10687
		public string text;

		// Token: 0x040029C0 RID: 10688
		public string[] lines;
	}
}
