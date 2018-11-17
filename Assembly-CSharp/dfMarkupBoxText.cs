using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

// Token: 0x02000716 RID: 1814
public class dfMarkupBoxText : dfMarkupBox
{
	// Token: 0x06004281 RID: 17025 RVA: 0x00101F94 File Offset: 0x00100194
	public dfMarkupBoxText(dfMarkupElement element, dfMarkupDisplayType display, dfMarkupStyle style) : base(element, display, style)
	{
	}

	// Token: 0x17000D11 RID: 3345
	// (get) Token: 0x06004283 RID: 17027 RVA: 0x00101FE0 File Offset: 0x001001E0
	// (set) Token: 0x06004284 RID: 17028 RVA: 0x00101FE8 File Offset: 0x001001E8
	public string Text { get; private set; }

	// Token: 0x17000D12 RID: 3346
	// (get) Token: 0x06004285 RID: 17029 RVA: 0x00101FF4 File Offset: 0x001001F4
	public bool IsWhitespace
	{
		get
		{
			return this.isWhitespace;
		}
	}

	// Token: 0x06004286 RID: 17030 RVA: 0x00101FFC File Offset: 0x001001FC
	public static dfMarkupBoxText Obtain(dfMarkupElement element, dfMarkupDisplayType display, dfMarkupStyle style)
	{
		if (dfMarkupBoxText.objectPool.Count > 0)
		{
			dfMarkupBoxText dfMarkupBoxText = dfMarkupBoxText.objectPool.Dequeue();
			dfMarkupBoxText.Element = element;
			dfMarkupBoxText.Display = display;
			dfMarkupBoxText.Style = style;
			dfMarkupBoxText.Position = Vector2.zero;
			dfMarkupBoxText.Size = Vector2.zero;
			dfMarkupBoxText.Baseline = (int)((float)style.FontSize * 1.1f);
			dfMarkupBoxText.Margins = default(dfMarkupBorders);
			dfMarkupBoxText.Padding = default(dfMarkupBorders);
			return dfMarkupBoxText;
		}
		return new dfMarkupBoxText(element, display, style);
	}

	// Token: 0x06004287 RID: 17031 RVA: 0x0010208C File Offset: 0x0010028C
	public override void Release()
	{
		base.Release();
		this.Text = string.Empty;
		this.renderData.Clear();
		dfMarkupBoxText.objectPool.Enqueue(this);
	}

	// Token: 0x06004288 RID: 17032 RVA: 0x001020C0 File Offset: 0x001002C0
	internal void SetText(string text)
	{
		this.Text = text;
		if (this.Style.Font == null)
		{
			return;
		}
		this.isWhitespace = dfMarkupBoxText.whitespacePattern.IsMatch(this.Text);
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
		dfDynamicFont font = this.Style.Font;
		float num2 = (float)fontSize / (float)font.FontSize;
		this.Baseline = Mathf.CeilToInt((float)font.Baseline * num2);
	}

	// Token: 0x06004289 RID: 17033 RVA: 0x00102244 File Offset: 0x00100444
	protected override dfRenderData OnRebuildRenderData()
	{
		this.renderData.Clear();
		if (this.Style.Font == null)
		{
			return null;
		}
		if (this.Style.TextDecoration == dfMarkupTextDecoration.Underline)
		{
			this.renderUnderline();
		}
		this.renderText(this.Text);
		return this.renderData;
	}

	// Token: 0x0600428A RID: 17034 RVA: 0x001022A0 File Offset: 0x001004A0
	private void renderUnderline()
	{
	}

	// Token: 0x0600428B RID: 17035 RVA: 0x001022A4 File Offset: 0x001004A4
	private void renderText(string text)
	{
		dfDynamicFont font = this.Style.Font;
		int fontSize = this.Style.FontSize;
		FontStyle fontStyle = this.Style.FontStyle;
		dfList<Vector3> vertices = this.renderData.Vertices;
		dfList<int> triangles = this.renderData.Triangles;
		dfList<Vector2> uv = this.renderData.UV;
		dfList<Color32> colors = this.renderData.Colors;
		float num = (float)fontSize / (float)font.FontSize;
		float num2 = (float)font.Descent * num;
		float num3 = 0f;
		CharacterInfo[] array = font.RequestCharacters(text, fontSize, fontStyle);
		this.renderData.Material = font.Material;
		for (int i = 0; i < text.Length; i++)
		{
			CharacterInfo characterInfo = array[i];
			dfMarkupBoxText.addTriangleIndices(vertices, triangles);
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

	// Token: 0x0600428C RID: 17036 RVA: 0x00102548 File Offset: 0x00100748
	private static void addTriangleIndices(dfList<Vector3> verts, dfList<int> triangles)
	{
		int count = verts.Count;
		int[] triangle_INDICES = dfMarkupBoxText.TRIANGLE_INDICES;
		for (int i = 0; i < triangle_INDICES.Length; i++)
		{
			triangles.Add(count + triangle_INDICES[i]);
		}
	}

	// Token: 0x0400230B RID: 8971
	private static int[] TRIANGLE_INDICES = new int[]
	{
		0,
		1,
		2,
		0,
		2,
		3
	};

	// Token: 0x0400230C RID: 8972
	private static Queue<dfMarkupBoxText> objectPool = new Queue<dfMarkupBoxText>();

	// Token: 0x0400230D RID: 8973
	private static Regex whitespacePattern = new Regex("\\s+");

	// Token: 0x0400230E RID: 8974
	private dfRenderData renderData = new dfRenderData(32);

	// Token: 0x0400230F RID: 8975
	private bool isWhitespace;
}
