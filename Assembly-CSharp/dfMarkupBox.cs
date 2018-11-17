using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007EF RID: 2031
public class dfMarkupBox
{
	// Token: 0x06004699 RID: 18073 RVA: 0x00109D00 File Offset: 0x00107F00
	private dfMarkupBox()
	{
		throw new NotImplementedException();
	}

	// Token: 0x0600469A RID: 18074 RVA: 0x00109D58 File Offset: 0x00107F58
	public dfMarkupBox(global::dfMarkupElement element, global::dfMarkupDisplayType display, global::dfMarkupStyle style)
	{
		this.Element = element;
		this.Display = display;
		this.Style = style;
		this.Baseline = style.FontSize;
	}

	// Token: 0x17000D93 RID: 3475
	// (get) Token: 0x0600469B RID: 18075 RVA: 0x00109DCC File Offset: 0x00107FCC
	// (set) Token: 0x0600469C RID: 18076 RVA: 0x00109DD4 File Offset: 0x00107FD4
	public global::dfMarkupBox Parent { get; protected set; }

	// Token: 0x17000D94 RID: 3476
	// (get) Token: 0x0600469D RID: 18077 RVA: 0x00109DE0 File Offset: 0x00107FE0
	// (set) Token: 0x0600469E RID: 18078 RVA: 0x00109DE8 File Offset: 0x00107FE8
	public global::dfMarkupElement Element { get; protected set; }

	// Token: 0x17000D95 RID: 3477
	// (get) Token: 0x0600469F RID: 18079 RVA: 0x00109DF4 File Offset: 0x00107FF4
	public List<global::dfMarkupBox> Children
	{
		get
		{
			return this.children;
		}
	}

	// Token: 0x17000D96 RID: 3478
	// (get) Token: 0x060046A0 RID: 18080 RVA: 0x00109DFC File Offset: 0x00107FFC
	// (set) Token: 0x060046A1 RID: 18081 RVA: 0x00109E0C File Offset: 0x0010800C
	public int Width
	{
		get
		{
			return (int)this.Size.x;
		}
		set
		{
			this.Size = new Vector2((float)value, this.Size.y);
		}
	}

	// Token: 0x17000D97 RID: 3479
	// (get) Token: 0x060046A2 RID: 18082 RVA: 0x00109E28 File Offset: 0x00108028
	// (set) Token: 0x060046A3 RID: 18083 RVA: 0x00109E38 File Offset: 0x00108038
	public int Height
	{
		get
		{
			return (int)this.Size.y;
		}
		set
		{
			this.Size = new Vector2(this.Size.x, (float)value);
		}
	}

	// Token: 0x060046A4 RID: 18084 RVA: 0x00109E54 File Offset: 0x00108054
	internal global::dfMarkupBox HitTest(Vector2 point)
	{
		Vector2 offset = this.GetOffset();
		Vector2 vector = offset + this.Size;
		if (point.x < offset.x || point.x > vector.x || point.y < offset.y || point.y > vector.y)
		{
			return null;
		}
		for (int i = 0; i < this.children.Count; i++)
		{
			global::dfMarkupBox dfMarkupBox = this.children[i].HitTest(point);
			if (dfMarkupBox != null)
			{
				return dfMarkupBox;
			}
		}
		return this;
	}

	// Token: 0x060046A5 RID: 18085 RVA: 0x00109EFC File Offset: 0x001080FC
	internal global::dfRenderData Render()
	{
		global::dfRenderData result;
		try
		{
			this.endCurrentLine(false);
			result = this.OnRebuildRenderData();
		}
		finally
		{
		}
		return result;
	}

	// Token: 0x060046A6 RID: 18086 RVA: 0x00109F40 File Offset: 0x00108140
	public virtual Vector2 GetOffset()
	{
		Vector2 vector = Vector2.zero;
		for (global::dfMarkupBox dfMarkupBox = this; dfMarkupBox != null; dfMarkupBox = dfMarkupBox.Parent)
		{
			vector += dfMarkupBox.Position;
		}
		return vector;
	}

	// Token: 0x060046A7 RID: 18087 RVA: 0x00109F78 File Offset: 0x00108178
	internal void AddLineBreak()
	{
		if (this.currentLine != null)
		{
			this.currentLine.IsNewline = true;
		}
		int verticalPosition = this.getVerticalPosition(0);
		this.endCurrentLine(false);
		global::dfMarkupBox containingBlock = this.GetContainingBlock();
		this.currentLine = new global::dfMarkupBox(this.Element, global::dfMarkupDisplayType.block, this.Style)
		{
			Size = new Vector2(containingBlock.Size.x, (float)this.Style.FontSize),
			Position = new Vector2(0f, (float)verticalPosition),
			Parent = this
		};
		this.children.Add(this.currentLine);
	}

	// Token: 0x060046A8 RID: 18088 RVA: 0x0010A018 File Offset: 0x00108218
	public virtual void AddChild(global::dfMarkupBox box)
	{
		global::dfMarkupDisplayType display = box.Display;
		bool flag = display == global::dfMarkupDisplayType.block || display == global::dfMarkupDisplayType.table || display == global::dfMarkupDisplayType.listItem || display == global::dfMarkupDisplayType.tableRow;
		if (flag)
		{
			this.addBlock(box);
		}
		else
		{
			this.addInline(box);
		}
	}

	// Token: 0x060046A9 RID: 18089 RVA: 0x0010A064 File Offset: 0x00108264
	public virtual void Release()
	{
		for (int i = 0; i < this.children.Count; i++)
		{
			this.children[i].Release();
		}
		this.children.Clear();
		this.Element = null;
		this.Parent = null;
		this.Margins = default(global::dfMarkupBorders);
	}

	// Token: 0x060046AA RID: 18090 RVA: 0x0010A0C8 File Offset: 0x001082C8
	protected virtual global::dfRenderData OnRebuildRenderData()
	{
		return null;
	}

	// Token: 0x060046AB RID: 18091 RVA: 0x0010A0CC File Offset: 0x001082CC
	protected void renderDebugBox(global::dfRenderData renderData)
	{
		Vector3 zero = Vector3.zero;
		Vector3 vector = zero + Vector3.right * this.Size.x;
		Vector3 item = vector + Vector3.down * this.Size.y;
		Vector3 item2 = zero + Vector3.down * this.Size.y;
		renderData.Vertices.Add(zero);
		renderData.Vertices.Add(vector);
		renderData.Vertices.Add(item);
		renderData.Vertices.Add(item2);
		renderData.Triangles.AddRange(new int[]
		{
			0,
			1,
			3,
			3,
			1,
			2
		});
		renderData.UV.Add(Vector2.zero);
		renderData.UV.Add(Vector2.zero);
		renderData.UV.Add(Vector2.zero);
		renderData.UV.Add(Vector2.zero);
		Color backgroundColor = this.Style.BackgroundColor;
		renderData.Colors.Add(backgroundColor);
		renderData.Colors.Add(backgroundColor);
		renderData.Colors.Add(backgroundColor);
		renderData.Colors.Add(backgroundColor);
	}

	// Token: 0x060046AC RID: 18092 RVA: 0x0010A214 File Offset: 0x00108414
	public void FitToContents(bool recursive = false)
	{
		if (this.children.Count == 0)
		{
			this.Size = new Vector2(this.Size.x, 0f);
			return;
		}
		this.endCurrentLine(false);
		Vector2 vector = Vector2.zero;
		for (int i = 0; i < this.children.Count; i++)
		{
			global::dfMarkupBox dfMarkupBox = this.children[i];
			vector = Vector2.Max(vector, dfMarkupBox.Position + dfMarkupBox.Size);
		}
		this.Size = vector;
	}

	// Token: 0x060046AD RID: 18093 RVA: 0x0010A2A4 File Offset: 0x001084A4
	private global::dfMarkupBox GetContainingBlock()
	{
		for (global::dfMarkupBox dfMarkupBox = this; dfMarkupBox != null; dfMarkupBox = dfMarkupBox.Parent)
		{
			global::dfMarkupDisplayType display = dfMarkupBox.Display;
			bool flag = display == global::dfMarkupDisplayType.block || display == global::dfMarkupDisplayType.inlineBlock || display == global::dfMarkupDisplayType.listItem || display == global::dfMarkupDisplayType.table || display == global::dfMarkupDisplayType.tableRow || display == global::dfMarkupDisplayType.tableCell;
			if (flag)
			{
				return dfMarkupBox;
			}
		}
		return null;
	}

	// Token: 0x060046AE RID: 18094 RVA: 0x0010A304 File Offset: 0x00108504
	private void addInline(global::dfMarkupBox box)
	{
		global::dfMarkupBorders margins = box.Margins;
		bool flag = !this.Style.Preformatted && this.currentLine != null && (float)this.currentLinePos + box.Size.x > this.currentLine.Size.x;
		if (this.currentLine == null || flag)
		{
			this.endCurrentLine(false);
			int verticalPosition = this.getVerticalPosition(margins.top);
			global::dfMarkupBox containingBlock = this.GetContainingBlock();
			if (containingBlock == null)
			{
				Debug.LogError("Containing block not found");
				return;
			}
			global::dfDynamicFont dfDynamicFont = this.Style.Font ?? this.Style.Host.Font;
			float num = (float)dfDynamicFont.FontSize / (float)dfDynamicFont.FontSize;
			float num2 = (float)dfDynamicFont.Baseline * num;
			this.currentLine = new global::dfMarkupBox(this.Element, global::dfMarkupDisplayType.block, this.Style)
			{
				Size = new Vector2(containingBlock.Size.x, (float)this.Style.LineHeight),
				Position = new Vector2(0f, (float)verticalPosition),
				Parent = this,
				Baseline = (int)num2
			};
			this.children.Add(this.currentLine);
		}
		if (this.currentLinePos == 0 && !box.Style.PreserveWhitespace && box is global::dfMarkupBoxText)
		{
			global::dfMarkupBoxText dfMarkupBoxText = box as global::dfMarkupBoxText;
			if (dfMarkupBoxText.IsWhitespace)
			{
				return;
			}
		}
		Vector2 position;
		position..ctor((float)(this.currentLinePos + margins.left), (float)margins.top);
		box.Position = position;
		box.Parent = this.currentLine;
		this.currentLine.children.Add(box);
		this.currentLinePos = (int)(position.x + box.Size.x + (float)box.Margins.right);
		float num3 = Mathf.Max(this.currentLine.Size.x, position.x + box.Size.x);
		float num4 = Mathf.Max(this.currentLine.Size.y, position.y + box.Size.y);
		this.currentLine.Size = new Vector2(num3, num4);
	}

	// Token: 0x060046AF RID: 18095 RVA: 0x0010A564 File Offset: 0x00108764
	private int getVerticalPosition(int topMargin)
	{
		if (this.children.Count == 0)
		{
			return topMargin;
		}
		int num = 0;
		int index = 0;
		for (int i = 0; i < this.children.Count; i++)
		{
			global::dfMarkupBox dfMarkupBox = this.children[i];
			float num2 = dfMarkupBox.Position.y + dfMarkupBox.Size.y + (float)dfMarkupBox.Margins.bottom;
			if (num2 > (float)num)
			{
				num = (int)num2;
				index = i;
			}
		}
		global::dfMarkupBox dfMarkupBox2 = this.children[index];
		int num3 = Mathf.Max(dfMarkupBox2.Margins.bottom, topMargin);
		return (int)(dfMarkupBox2.Position.y + dfMarkupBox2.Size.y + (float)num3);
	}

	// Token: 0x060046B0 RID: 18096 RVA: 0x0010A628 File Offset: 0x00108828
	private void addBlock(global::dfMarkupBox box)
	{
		if (this.currentLine != null)
		{
			this.currentLine.IsNewline = true;
			this.endCurrentLine(true);
		}
		global::dfMarkupBox containingBlock = this.GetContainingBlock();
		if (box.Size.sqrMagnitude <= 1.401298E-45f)
		{
			box.Size = new Vector2(containingBlock.Size.x - (float)box.Margins.horizontal, (float)this.Style.FontSize);
		}
		int verticalPosition = this.getVerticalPosition(box.Margins.top);
		box.Position = new Vector2((float)box.Margins.left, (float)verticalPosition);
		this.Size = new Vector2(this.Size.x, Mathf.Max(this.Size.y, box.Position.y + box.Size.y));
		box.Parent = this;
		this.children.Add(box);
	}

	// Token: 0x060046B1 RID: 18097 RVA: 0x0010A71C File Offset: 0x0010891C
	private void endCurrentLine(bool removeEmpty = false)
	{
		if (this.currentLine == null)
		{
			return;
		}
		if (this.currentLinePos == 0)
		{
			if (removeEmpty)
			{
				this.children.Remove(this.currentLine);
			}
		}
		else
		{
			this.currentLine.doHorizontalAlignment();
			this.currentLine.doVerticalAlignment();
		}
		this.currentLine = null;
		this.currentLinePos = 0;
	}

	// Token: 0x060046B2 RID: 18098 RVA: 0x0010A784 File Offset: 0x00108984
	private void doVerticalAlignment()
	{
		if (this.children.Count == 0)
		{
			return;
		}
		float num = float.MinValue;
		float num2 = float.MaxValue;
		float num3 = float.MinValue;
		this.Baseline = (int)(this.Size.y * 0.95f);
		for (int i = 0; i < this.children.Count; i++)
		{
			global::dfMarkupBox dfMarkupBox = this.children[i];
			num3 = Mathf.Max(num3, dfMarkupBox.Position.y + (float)dfMarkupBox.Baseline);
		}
		for (int j = 0; j < this.children.Count; j++)
		{
			global::dfMarkupBox dfMarkupBox2 = this.children[j];
			global::dfMarkupVerticalAlign verticalAlign = dfMarkupBox2.Style.VerticalAlign;
			Vector2 position = dfMarkupBox2.Position;
			if (verticalAlign == global::dfMarkupVerticalAlign.Baseline)
			{
				position.y = num3 - (float)dfMarkupBox2.Baseline;
			}
			dfMarkupBox2.Position = position;
		}
		for (int k = 0; k < this.children.Count; k++)
		{
			global::dfMarkupBox dfMarkupBox3 = this.children[k];
			Vector2 position2 = dfMarkupBox3.Position;
			Vector2 size = dfMarkupBox3.Size;
			num2 = Mathf.Min(num2, position2.y);
			num = Mathf.Max(num, position2.y + size.y);
		}
		for (int l = 0; l < this.children.Count; l++)
		{
			global::dfMarkupBox dfMarkupBox4 = this.children[l];
			global::dfMarkupVerticalAlign verticalAlign2 = dfMarkupBox4.Style.VerticalAlign;
			Vector2 position3 = dfMarkupBox4.Position;
			Vector2 size2 = dfMarkupBox4.Size;
			if (verticalAlign2 == global::dfMarkupVerticalAlign.Top)
			{
				position3.y = num2;
			}
			else if (verticalAlign2 == global::dfMarkupVerticalAlign.Bottom)
			{
				position3.y = num - size2.y;
			}
			else if (verticalAlign2 == global::dfMarkupVerticalAlign.Middle)
			{
				position3.y = (this.Size.y - size2.y) * 0.5f;
			}
			dfMarkupBox4.Position = position3;
		}
		int num4 = int.MaxValue;
		for (int m = 0; m < this.children.Count; m++)
		{
			num4 = Mathf.Min(num4, (int)this.children[m].Position.y);
		}
		for (int n = 0; n < this.children.Count; n++)
		{
			Vector2 position4 = this.children[n].Position;
			position4.y -= (float)num4;
			this.children[n].Position = position4;
		}
	}

	// Token: 0x060046B3 RID: 18099 RVA: 0x0010AA34 File Offset: 0x00108C34
	private void doHorizontalAlignment()
	{
		if (this.Style.Align == global::dfMarkupTextAlign.Left || this.children.Count == 0)
		{
			return;
		}
		int i;
		for (i = this.children.Count - 1; i > 0; i--)
		{
			global::dfMarkupBoxText dfMarkupBoxText = this.children[i] as global::dfMarkupBoxText;
			if (dfMarkupBoxText == null || !dfMarkupBoxText.IsWhitespace)
			{
				break;
			}
		}
		if (this.Style.Align == global::dfMarkupTextAlign.Center)
		{
			float num = 0f;
			for (int j = 0; j <= i; j++)
			{
				num += this.children[j].Size.x;
			}
			float num2 = (this.Size.x - (float)this.Padding.horizontal - num) * 0.5f;
			for (int k = 0; k <= i; k++)
			{
				Vector2 position = this.children[k].Position;
				position.x += num2;
				this.children[k].Position = position;
			}
		}
		else if (this.Style.Align == global::dfMarkupTextAlign.Right)
		{
			float num3 = this.Size.x - (float)this.Padding.horizontal;
			for (int l = i; l >= 0; l--)
			{
				Vector2 position2 = this.children[l].Position;
				position2.x = num3 - this.children[l].Size.x;
				this.children[l].Position = position2;
				num3 -= this.children[l].Size.x;
			}
		}
		else
		{
			if (this.Style.Align != global::dfMarkupTextAlign.Justify)
			{
				throw new NotImplementedException("text-align: " + this.Style.Align + " is not implemented");
			}
			if (this.children.Count <= 1)
			{
				return;
			}
			if (this.IsNewline || this.children[this.children.Count - 1].IsNewline)
			{
				return;
			}
			float num4 = 0f;
			for (int m = 0; m <= i; m++)
			{
				global::dfMarkupBox dfMarkupBox = this.children[m];
				num4 = Mathf.Max(num4, dfMarkupBox.Position.x + dfMarkupBox.Size.x);
			}
			float num5 = (this.Size.x - (float)this.Padding.horizontal - num4) / (float)this.children.Count;
			for (int n = 1; n <= i; n++)
			{
				this.children[n].Position += new Vector2((float)n * num5, 0f);
			}
			global::dfMarkupBox dfMarkupBox2 = this.children[i];
			Vector2 position3 = dfMarkupBox2.Position;
			position3.x = this.Size.x - (float)this.Padding.horizontal - dfMarkupBox2.Size.x;
			dfMarkupBox2.Position = position3;
		}
	}

	// Token: 0x04002519 RID: 9497
	public Vector2 Position = Vector2.zero;

	// Token: 0x0400251A RID: 9498
	public Vector2 Size = Vector2.zero;

	// Token: 0x0400251B RID: 9499
	public global::dfMarkupDisplayType Display;

	// Token: 0x0400251C RID: 9500
	public global::dfMarkupBorders Margins = new global::dfMarkupBorders(0, 0, 0, 0);

	// Token: 0x0400251D RID: 9501
	public global::dfMarkupBorders Padding = new global::dfMarkupBorders(0, 0, 0, 0);

	// Token: 0x0400251E RID: 9502
	public global::dfMarkupStyle Style;

	// Token: 0x0400251F RID: 9503
	public bool IsNewline;

	// Token: 0x04002520 RID: 9504
	public int Baseline;

	// Token: 0x04002521 RID: 9505
	private List<global::dfMarkupBox> children = new List<global::dfMarkupBox>();

	// Token: 0x04002522 RID: 9506
	private global::dfMarkupBox currentLine;

	// Token: 0x04002523 RID: 9507
	private int currentLinePos;
}
