using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000713 RID: 1811
public class dfMarkupBox
{
	// Token: 0x06004255 RID: 16981 RVA: 0x001009F0 File Offset: 0x000FEBF0
	private dfMarkupBox()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06004256 RID: 16982 RVA: 0x00100A48 File Offset: 0x000FEC48
	public dfMarkupBox(dfMarkupElement element, dfMarkupDisplayType display, dfMarkupStyle style)
	{
		this.Element = element;
		this.Display = display;
		this.Style = style;
		this.Baseline = style.FontSize;
	}

	// Token: 0x17000D09 RID: 3337
	// (get) Token: 0x06004257 RID: 16983 RVA: 0x00100ABC File Offset: 0x000FECBC
	// (set) Token: 0x06004258 RID: 16984 RVA: 0x00100AC4 File Offset: 0x000FECC4
	public dfMarkupBox Parent { get; protected set; }

	// Token: 0x17000D0A RID: 3338
	// (get) Token: 0x06004259 RID: 16985 RVA: 0x00100AD0 File Offset: 0x000FECD0
	// (set) Token: 0x0600425A RID: 16986 RVA: 0x00100AD8 File Offset: 0x000FECD8
	public dfMarkupElement Element { get; protected set; }

	// Token: 0x17000D0B RID: 3339
	// (get) Token: 0x0600425B RID: 16987 RVA: 0x00100AE4 File Offset: 0x000FECE4
	public List<dfMarkupBox> Children
	{
		get
		{
			return this.children;
		}
	}

	// Token: 0x17000D0C RID: 3340
	// (get) Token: 0x0600425C RID: 16988 RVA: 0x00100AEC File Offset: 0x000FECEC
	// (set) Token: 0x0600425D RID: 16989 RVA: 0x00100AFC File Offset: 0x000FECFC
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

	// Token: 0x17000D0D RID: 3341
	// (get) Token: 0x0600425E RID: 16990 RVA: 0x00100B18 File Offset: 0x000FED18
	// (set) Token: 0x0600425F RID: 16991 RVA: 0x00100B28 File Offset: 0x000FED28
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

	// Token: 0x06004260 RID: 16992 RVA: 0x00100B44 File Offset: 0x000FED44
	internal dfMarkupBox HitTest(Vector2 point)
	{
		Vector2 offset = this.GetOffset();
		Vector2 vector = offset + this.Size;
		if (point.x < offset.x || point.x > vector.x || point.y < offset.y || point.y > vector.y)
		{
			return null;
		}
		for (int i = 0; i < this.children.Count; i++)
		{
			dfMarkupBox dfMarkupBox = this.children[i].HitTest(point);
			if (dfMarkupBox != null)
			{
				return dfMarkupBox;
			}
		}
		return this;
	}

	// Token: 0x06004261 RID: 16993 RVA: 0x00100BEC File Offset: 0x000FEDEC
	internal dfRenderData Render()
	{
		dfRenderData result;
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

	// Token: 0x06004262 RID: 16994 RVA: 0x00100C30 File Offset: 0x000FEE30
	public virtual Vector2 GetOffset()
	{
		Vector2 vector = Vector2.zero;
		for (dfMarkupBox dfMarkupBox = this; dfMarkupBox != null; dfMarkupBox = dfMarkupBox.Parent)
		{
			vector += dfMarkupBox.Position;
		}
		return vector;
	}

	// Token: 0x06004263 RID: 16995 RVA: 0x00100C68 File Offset: 0x000FEE68
	internal void AddLineBreak()
	{
		if (this.currentLine != null)
		{
			this.currentLine.IsNewline = true;
		}
		int verticalPosition = this.getVerticalPosition(0);
		this.endCurrentLine(false);
		dfMarkupBox containingBlock = this.GetContainingBlock();
		this.currentLine = new dfMarkupBox(this.Element, dfMarkupDisplayType.block, this.Style)
		{
			Size = new Vector2(containingBlock.Size.x, (float)this.Style.FontSize),
			Position = new Vector2(0f, (float)verticalPosition),
			Parent = this
		};
		this.children.Add(this.currentLine);
	}

	// Token: 0x06004264 RID: 16996 RVA: 0x00100D08 File Offset: 0x000FEF08
	public virtual void AddChild(dfMarkupBox box)
	{
		dfMarkupDisplayType display = box.Display;
		bool flag = display == dfMarkupDisplayType.block || display == dfMarkupDisplayType.table || display == dfMarkupDisplayType.listItem || display == dfMarkupDisplayType.tableRow;
		if (flag)
		{
			this.addBlock(box);
		}
		else
		{
			this.addInline(box);
		}
	}

	// Token: 0x06004265 RID: 16997 RVA: 0x00100D54 File Offset: 0x000FEF54
	public virtual void Release()
	{
		for (int i = 0; i < this.children.Count; i++)
		{
			this.children[i].Release();
		}
		this.children.Clear();
		this.Element = null;
		this.Parent = null;
		this.Margins = default(dfMarkupBorders);
	}

	// Token: 0x06004266 RID: 16998 RVA: 0x00100DB8 File Offset: 0x000FEFB8
	protected virtual dfRenderData OnRebuildRenderData()
	{
		return null;
	}

	// Token: 0x06004267 RID: 16999 RVA: 0x00100DBC File Offset: 0x000FEFBC
	protected void renderDebugBox(dfRenderData renderData)
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

	// Token: 0x06004268 RID: 17000 RVA: 0x00100F04 File Offset: 0x000FF104
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
			dfMarkupBox dfMarkupBox = this.children[i];
			vector = Vector2.Max(vector, dfMarkupBox.Position + dfMarkupBox.Size);
		}
		this.Size = vector;
	}

	// Token: 0x06004269 RID: 17001 RVA: 0x00100F94 File Offset: 0x000FF194
	private dfMarkupBox GetContainingBlock()
	{
		for (dfMarkupBox dfMarkupBox = this; dfMarkupBox != null; dfMarkupBox = dfMarkupBox.Parent)
		{
			dfMarkupDisplayType display = dfMarkupBox.Display;
			bool flag = display == dfMarkupDisplayType.block || display == dfMarkupDisplayType.inlineBlock || display == dfMarkupDisplayType.listItem || display == dfMarkupDisplayType.table || display == dfMarkupDisplayType.tableRow || display == dfMarkupDisplayType.tableCell;
			if (flag)
			{
				return dfMarkupBox;
			}
		}
		return null;
	}

	// Token: 0x0600426A RID: 17002 RVA: 0x00100FF4 File Offset: 0x000FF1F4
	private void addInline(dfMarkupBox box)
	{
		dfMarkupBorders margins = box.Margins;
		bool flag = !this.Style.Preformatted && this.currentLine != null && (float)this.currentLinePos + box.Size.x > this.currentLine.Size.x;
		if (this.currentLine == null || flag)
		{
			this.endCurrentLine(false);
			int verticalPosition = this.getVerticalPosition(margins.top);
			dfMarkupBox containingBlock = this.GetContainingBlock();
			if (containingBlock == null)
			{
				Debug.LogError("Containing block not found");
				return;
			}
			dfDynamicFont dfDynamicFont = this.Style.Font ?? this.Style.Host.Font;
			float num = (float)dfDynamicFont.FontSize / (float)dfDynamicFont.FontSize;
			float num2 = (float)dfDynamicFont.Baseline * num;
			this.currentLine = new dfMarkupBox(this.Element, dfMarkupDisplayType.block, this.Style)
			{
				Size = new Vector2(containingBlock.Size.x, (float)this.Style.LineHeight),
				Position = new Vector2(0f, (float)verticalPosition),
				Parent = this,
				Baseline = (int)num2
			};
			this.children.Add(this.currentLine);
		}
		if (this.currentLinePos == 0 && !box.Style.PreserveWhitespace && box is dfMarkupBoxText)
		{
			dfMarkupBoxText dfMarkupBoxText = box as dfMarkupBoxText;
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

	// Token: 0x0600426B RID: 17003 RVA: 0x00101254 File Offset: 0x000FF454
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
			dfMarkupBox dfMarkupBox = this.children[i];
			float num2 = dfMarkupBox.Position.y + dfMarkupBox.Size.y + (float)dfMarkupBox.Margins.bottom;
			if (num2 > (float)num)
			{
				num = (int)num2;
				index = i;
			}
		}
		dfMarkupBox dfMarkupBox2 = this.children[index];
		int num3 = Mathf.Max(dfMarkupBox2.Margins.bottom, topMargin);
		return (int)(dfMarkupBox2.Position.y + dfMarkupBox2.Size.y + (float)num3);
	}

	// Token: 0x0600426C RID: 17004 RVA: 0x00101318 File Offset: 0x000FF518
	private void addBlock(dfMarkupBox box)
	{
		if (this.currentLine != null)
		{
			this.currentLine.IsNewline = true;
			this.endCurrentLine(true);
		}
		dfMarkupBox containingBlock = this.GetContainingBlock();
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

	// Token: 0x0600426D RID: 17005 RVA: 0x0010140C File Offset: 0x000FF60C
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

	// Token: 0x0600426E RID: 17006 RVA: 0x00101474 File Offset: 0x000FF674
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
			dfMarkupBox dfMarkupBox = this.children[i];
			num3 = Mathf.Max(num3, dfMarkupBox.Position.y + (float)dfMarkupBox.Baseline);
		}
		for (int j = 0; j < this.children.Count; j++)
		{
			dfMarkupBox dfMarkupBox2 = this.children[j];
			dfMarkupVerticalAlign verticalAlign = dfMarkupBox2.Style.VerticalAlign;
			Vector2 position = dfMarkupBox2.Position;
			if (verticalAlign == dfMarkupVerticalAlign.Baseline)
			{
				position.y = num3 - (float)dfMarkupBox2.Baseline;
			}
			dfMarkupBox2.Position = position;
		}
		for (int k = 0; k < this.children.Count; k++)
		{
			dfMarkupBox dfMarkupBox3 = this.children[k];
			Vector2 position2 = dfMarkupBox3.Position;
			Vector2 size = dfMarkupBox3.Size;
			num2 = Mathf.Min(num2, position2.y);
			num = Mathf.Max(num, position2.y + size.y);
		}
		for (int l = 0; l < this.children.Count; l++)
		{
			dfMarkupBox dfMarkupBox4 = this.children[l];
			dfMarkupVerticalAlign verticalAlign2 = dfMarkupBox4.Style.VerticalAlign;
			Vector2 position3 = dfMarkupBox4.Position;
			Vector2 size2 = dfMarkupBox4.Size;
			if (verticalAlign2 == dfMarkupVerticalAlign.Top)
			{
				position3.y = num2;
			}
			else if (verticalAlign2 == dfMarkupVerticalAlign.Bottom)
			{
				position3.y = num - size2.y;
			}
			else if (verticalAlign2 == dfMarkupVerticalAlign.Middle)
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

	// Token: 0x0600426F RID: 17007 RVA: 0x00101724 File Offset: 0x000FF924
	private void doHorizontalAlignment()
	{
		if (this.Style.Align == dfMarkupTextAlign.Left || this.children.Count == 0)
		{
			return;
		}
		int i;
		for (i = this.children.Count - 1; i > 0; i--)
		{
			dfMarkupBoxText dfMarkupBoxText = this.children[i] as dfMarkupBoxText;
			if (dfMarkupBoxText == null || !dfMarkupBoxText.IsWhitespace)
			{
				break;
			}
		}
		if (this.Style.Align == dfMarkupTextAlign.Center)
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
		else if (this.Style.Align == dfMarkupTextAlign.Right)
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
			if (this.Style.Align != dfMarkupTextAlign.Justify)
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
				dfMarkupBox dfMarkupBox = this.children[m];
				num4 = Mathf.Max(num4, dfMarkupBox.Position.x + dfMarkupBox.Size.x);
			}
			float num5 = (this.Size.x - (float)this.Padding.horizontal - num4) / (float)this.children.Count;
			for (int n = 1; n <= i; n++)
			{
				this.children[n].Position += new Vector2((float)n * num5, 0f);
			}
			dfMarkupBox dfMarkupBox2 = this.children[i];
			Vector2 position3 = dfMarkupBox2.Position;
			position3.x = this.Size.x - (float)this.Padding.horizontal - dfMarkupBox2.Size.x;
			dfMarkupBox2.Position = position3;
		}
	}

	// Token: 0x040022F6 RID: 8950
	public Vector2 Position = Vector2.zero;

	// Token: 0x040022F7 RID: 8951
	public Vector2 Size = Vector2.zero;

	// Token: 0x040022F8 RID: 8952
	public dfMarkupDisplayType Display;

	// Token: 0x040022F9 RID: 8953
	public dfMarkupBorders Margins = new dfMarkupBorders(0, 0, 0, 0);

	// Token: 0x040022FA RID: 8954
	public dfMarkupBorders Padding = new dfMarkupBorders(0, 0, 0, 0);

	// Token: 0x040022FB RID: 8955
	public dfMarkupStyle Style;

	// Token: 0x040022FC RID: 8956
	public bool IsNewline;

	// Token: 0x040022FD RID: 8957
	public int Baseline;

	// Token: 0x040022FE RID: 8958
	private List<dfMarkupBox> children = new List<dfMarkupBox>();

	// Token: 0x040022FF RID: 8959
	private dfMarkupBox currentLine;

	// Token: 0x04002300 RID: 8960
	private int currentLinePos;
}
