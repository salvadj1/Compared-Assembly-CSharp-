using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

// Token: 0x020007F2 RID: 2034
public class dfMarkupBoxText : global::dfMarkupBox
{
	// Token: 0x060046C5 RID: 18117 RVA: 0x0010B2A4 File Offset: 0x001094A4
	public dfMarkupBoxText(global::dfMarkupElement element, global::dfMarkupDisplayType display, global::dfMarkupStyle style) : base(element, display, style)
	{
	}

	// Token: 0x17000D9B RID: 3483
	// (get) Token: 0x060046C7 RID: 18119 RVA: 0x0010B2F0 File Offset: 0x001094F0
	// (set) Token: 0x060046C8 RID: 18120 RVA: 0x0010B2F8 File Offset: 0x001094F8
	public string Text { get; private set; }

	// Token: 0x17000D9C RID: 3484
	// (get) Token: 0x060046C9 RID: 18121 RVA: 0x0010B304 File Offset: 0x00109504
	public bool IsWhitespace
	{
		get
		{
			return this.isWhitespace;
		}
	}

	// Token: 0x060046CA RID: 18122 RVA: 0x0010B30C File Offset: 0x0010950C
	public static global::dfMarkupBoxText Obtain(global::dfMarkupElement element, global::dfMarkupDisplayType display, global::dfMarkupStyle style)
	{
		if (global::dfMarkupBoxText.objectPool.Count > 0)
		{
			global::dfMarkupBoxText dfMarkupBoxText = global::dfMarkupBoxText.objectPool.Dequeue();
			dfMarkupBoxText.Element = element;
			dfMarkupBoxText.Display = display;
			dfMarkupBoxText.Style = style;
			dfMarkupBoxText.Position = Vector2.zero;
			dfMarkupBoxText.Size = Vector2.zero;
			dfMarkupBoxText.Baseline = (int)((float)style.FontSize * 1.1f);
			dfMarkupBoxText.Margins = default(global::dfMarkupBorders);
			dfMarkupBoxText.Padding = default(global::dfMarkupBorders);
			return dfMarkupBoxText;
		}
		return new global::dfMarkupBoxText(element, display, style);
	}

	// Token: 0x060046CB RID: 18123 RVA: 0x0010B39C File Offset: 0x0010959C
	public override void Release()
	{
		base.Release();
		this.Text = string.Empty;
		this.renderData.Clear();
		global::dfMarkupBoxText.objectPool.Enqueue(this);
	}

	// Token: 0x060046CC RID: 18124 RVA: 0x0010B3D0 File Offset: 0x001095D0
	internal void SetText(string text)
	{
		this.Text = text;
		if (this.Style.Font == null)
		{
			return;
		}
		this.isWhitespace = global::dfMarkupBoxText.whitespacePattern.IsMatch(this.Text);
		string text2 = (!this.Style.PreserveWhitespace && this.isWhitespace) ? " " : this.Text;
		CharacterInfo[] array = this.Style.Font.RequestCharacters(text2, this.Style.FontSize, this.Style.FontStyle);
		int fontSize = this.Style.FontSize;
		Vector2 size;
		size..ctor(0f, (float)this.Style.LineHeight);
		for (int i = 0; i < text2.Length; i++)
		{
			CharacterInfo characterInfo = array[i];
			float num = characterInfo.vert.x + characterInfo.vert.width;
			if (text2[i] == ' ')
			{
				num = Mathf.Max(num, (float)fontSize * 0.33f);
			}
			else if (text2[i] == '\t')
			{
				num += (float)(fontSize * 3);
			}
			size.x += num;
		}
		this.Size = size;
		global::dfDynamicFont font = this.Style.Font;
		float num2 = (float)fontSize / (float)font.FontSize;
		this.Baseline = Mathf.CeilToInt((float)font.Baseline * num2);
	}

	// Token: 0x060046CD RID: 18125 RVA: 0x0010B554 File Offset: 0x00109754
	protected override global::dfRenderData OnRebuildRenderData()
	{
		this.renderData.Clear();
		if (this.Style.Font == null)
		{
			return null;
		}
		if (this.Style.TextDecoration == global::dfMarkupTextDecoration.Underline)
		{
			this.renderUnderline();
		}
		this.renderText(this.Text);
		return this.renderData;
	}

	// Token: 0x060046CE RID: 18126 RVA: 0x0010B5B0 File Offset: 0x001097B0
	private void renderUnderline()
	{
	}

	// Token: 0x060046CF RID: 18127 RVA: 0x0010B5B4 File Offset: 0x001097B4
	private void renderText(string text)
	{
		global::dfDynamicFont font = this.Style.Font;
		int fontSize = this.Style.FontSize;
		FontStyle fontStyle = this.Style.FontStyle;
		global::dfList<Vector3> vertices = this.renderData.Vertices;
		global::dfList<int> triangles = this.renderData.Triangles;
		global::dfList<Vector2> uv = this.renderData.UV;
		global::dfList<Color32> colors = this.renderData.Colors;
		float num = (float)fontSize / (float)font.FontSize;
		float num2 = (float)font.Descent * num;
		float num3 = 0f;
		CharacterInfo[] array = font.RequestCharacters(text, fontSize, fontStyle);
		this.renderData.Material = font.Material;
		for (int i = 0; i < text.Length; i++)
		{
			CharacterInfo characterInfo = array[i];
			global::dfMarkupBoxText.addTriangleIndices(vertices, triangles);
			float num4 = (float)font.FontSize + characterInfo.vert.y - (float)fontSize + num2;
			float num5 = num3 + characterInfo.vert.x;
			float num6 = num4;
			float num7 = num5 + characterInfo.vert.width;
			float num8 = num6 + characterInfo.vert.height;
			Vector3 item;
			item..ctor(num5, num6);
			Vector3 item2;
			item2..ctor(num7, num6);
			Vector3 item3;
			item3..ctor(num7, num8);
			Vector3 item4;
			item4..ctor(num5, num8);
			vertices.Add(item);
			vertices.Add(item2);
			vertices.Add(item3);
			vertices.Add(item4);
			Color color = this.Style.Color;
			colors.Add(color);
			colors.Add(color);
			colors.Add(color);
			colors.Add(color);
			Rect uv2 = characterInfo.uv;
			float x = uv2.x;
			float num9 = uv2.y + uv2.height;
			float num10 = x + uv2.width;
			float y = uv2.y;
			if (characterInfo.flipped)
			{
				uv.Add(new Vector2(num10, y));
				uv.Add(new Vector2(num10, num9));
				uv.Add(new Vector2(x, num9));
				uv.Add(new Vector2(x, y));
			}
			else
			{
				uv.Add(new Vector2(x, num9));
				uv.Add(new Vector2(num10, num9));
				uv.Add(new Vector2(num10, y));
				uv.Add(new Vector2(x, y));
			}
			num3 += (float)Mathf.CeilToInt(characterInfo.vert.x + characterInfo.vert.width);
		}
	}

	// Token: 0x060046D0 RID: 18128 RVA: 0x0010B858 File Offset: 0x00109A58
	private static void addTriangleIndices(global::dfList<Vector3> verts, global::dfList<int> triangles)
	{
		int count = verts.Count;
		int[] triangle_INDICES = global::dfMarkupBoxText.TRIANGLE_INDICES;
		for (int i = 0; i < triangle_INDICES.Length; i++)
		{
			triangles.Add(count + triangle_INDICES[i]);
		}
	}

	// Token: 0x0400252E RID: 9518
	private static int[] TRIANGLE_INDICES = new int[]
	{
		0,
		1,
		2,
		0,
		2,
		3
	};

	// Token: 0x0400252F RID: 9519
	private static Queue<global::dfMarkupBoxText> objectPool = new Queue<global::dfMarkupBoxText>();

	// Token: 0x04002530 RID: 9520
	private static Regex whitespacePattern = new Regex("\\s+");

	// Token: 0x04002531 RID: 9521
	private global::dfRenderData renderData = new global::dfRenderData(32);

	// Token: 0x04002532 RID: 9522
	private bool isWhitespace;
}
