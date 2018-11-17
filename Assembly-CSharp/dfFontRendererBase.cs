using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006CA RID: 1738
public abstract class dfFontRendererBase : IDisposable
{
	// Token: 0x17000BDC RID: 3036
	// (get) Token: 0x06003CEE RID: 15598 RVA: 0x000E5E2C File Offset: 0x000E402C
	// (set) Token: 0x06003CEF RID: 15599 RVA: 0x000E5E34 File Offset: 0x000E4034
	public dfFontBase Font { get; protected set; }

	// Token: 0x17000BDD RID: 3037
	// (get) Token: 0x06003CF0 RID: 15600 RVA: 0x000E5E40 File Offset: 0x000E4040
	// (set) Token: 0x06003CF1 RID: 15601 RVA: 0x000E5E48 File Offset: 0x000E4048
	public Vector2 MaxSize { get; set; }

	// Token: 0x17000BDE RID: 3038
	// (get) Token: 0x06003CF2 RID: 15602 RVA: 0x000E5E54 File Offset: 0x000E4054
	// (set) Token: 0x06003CF3 RID: 15603 RVA: 0x000E5E5C File Offset: 0x000E405C
	public float PixelRatio { get; set; }

	// Token: 0x17000BDF RID: 3039
	// (get) Token: 0x06003CF4 RID: 15604 RVA: 0x000E5E68 File Offset: 0x000E4068
	// (set) Token: 0x06003CF5 RID: 15605 RVA: 0x000E5E70 File Offset: 0x000E4070
	public float TextScale { get; set; }

	// Token: 0x17000BE0 RID: 3040
	// (get) Token: 0x06003CF6 RID: 15606 RVA: 0x000E5E7C File Offset: 0x000E407C
	// (set) Token: 0x06003CF7 RID: 15607 RVA: 0x000E5E84 File Offset: 0x000E4084
	public int CharacterSpacing { get; set; }

	// Token: 0x17000BE1 RID: 3041
	// (get) Token: 0x06003CF8 RID: 15608 RVA: 0x000E5E90 File Offset: 0x000E4090
	// (set) Token: 0x06003CF9 RID: 15609 RVA: 0x000E5E98 File Offset: 0x000E4098
	public Vector3 VectorOffset { get; set; }

	// Token: 0x17000BE2 RID: 3042
	// (get) Token: 0x06003CFA RID: 15610 RVA: 0x000E5EA4 File Offset: 0x000E40A4
	// (set) Token: 0x06003CFB RID: 15611 RVA: 0x000E5EAC File Offset: 0x000E40AC
	public bool ProcessMarkup { get; set; }

	// Token: 0x17000BE3 RID: 3043
	// (get) Token: 0x06003CFC RID: 15612 RVA: 0x000E5EB8 File Offset: 0x000E40B8
	// (set) Token: 0x06003CFD RID: 15613 RVA: 0x000E5EC0 File Offset: 0x000E40C0
	public bool WordWrap { get; set; }

	// Token: 0x17000BE4 RID: 3044
	// (get) Token: 0x06003CFE RID: 15614 RVA: 0x000E5ECC File Offset: 0x000E40CC
	// (set) Token: 0x06003CFF RID: 15615 RVA: 0x000E5ED4 File Offset: 0x000E40D4
	public bool MultiLine { get; set; }

	// Token: 0x17000BE5 RID: 3045
	// (get) Token: 0x06003D00 RID: 15616 RVA: 0x000E5EE0 File Offset: 0x000E40E0
	// (set) Token: 0x06003D01 RID: 15617 RVA: 0x000E5EE8 File Offset: 0x000E40E8
	public bool OverrideMarkupColors { get; set; }

	// Token: 0x17000BE6 RID: 3046
	// (get) Token: 0x06003D02 RID: 15618 RVA: 0x000E5EF4 File Offset: 0x000E40F4
	// (set) Token: 0x06003D03 RID: 15619 RVA: 0x000E5EFC File Offset: 0x000E40FC
	public bool ColorizeSymbols { get; set; }

	// Token: 0x17000BE7 RID: 3047
	// (get) Token: 0x06003D04 RID: 15620 RVA: 0x000E5F08 File Offset: 0x000E4108
	// (set) Token: 0x06003D05 RID: 15621 RVA: 0x000E5F10 File Offset: 0x000E4110
	public TextAlignment TextAlign { get; set; }

	// Token: 0x17000BE8 RID: 3048
	// (get) Token: 0x06003D06 RID: 15622 RVA: 0x000E5F1C File Offset: 0x000E411C
	// (set) Token: 0x06003D07 RID: 15623 RVA: 0x000E5F24 File Offset: 0x000E4124
	public Color32 DefaultColor { get; set; }

	// Token: 0x17000BE9 RID: 3049
	// (get) Token: 0x06003D08 RID: 15624 RVA: 0x000E5F30 File Offset: 0x000E4130
	// (set) Token: 0x06003D09 RID: 15625 RVA: 0x000E5F38 File Offset: 0x000E4138
	public Color32? BottomColor { get; set; }

	// Token: 0x17000BEA RID: 3050
	// (get) Token: 0x06003D0A RID: 15626 RVA: 0x000E5F44 File Offset: 0x000E4144
	// (set) Token: 0x06003D0B RID: 15627 RVA: 0x000E5F4C File Offset: 0x000E414C
	public float Opacity { get; set; }

	// Token: 0x17000BEB RID: 3051
	// (get) Token: 0x06003D0C RID: 15628 RVA: 0x000E5F58 File Offset: 0x000E4158
	// (set) Token: 0x06003D0D RID: 15629 RVA: 0x000E5F60 File Offset: 0x000E4160
	public bool Outline { get; set; }

	// Token: 0x17000BEC RID: 3052
	// (get) Token: 0x06003D0E RID: 15630 RVA: 0x000E5F6C File Offset: 0x000E416C
	// (set) Token: 0x06003D0F RID: 15631 RVA: 0x000E5F74 File Offset: 0x000E4174
	public int OutlineSize { get; set; }

	// Token: 0x17000BED RID: 3053
	// (get) Token: 0x06003D10 RID: 15632 RVA: 0x000E5F80 File Offset: 0x000E4180
	// (set) Token: 0x06003D11 RID: 15633 RVA: 0x000E5F88 File Offset: 0x000E4188
	public Color32 OutlineColor { get; set; }

	// Token: 0x17000BEE RID: 3054
	// (get) Token: 0x06003D12 RID: 15634 RVA: 0x000E5F94 File Offset: 0x000E4194
	// (set) Token: 0x06003D13 RID: 15635 RVA: 0x000E5F9C File Offset: 0x000E419C
	public bool Shadow { get; set; }

	// Token: 0x17000BEF RID: 3055
	// (get) Token: 0x06003D14 RID: 15636 RVA: 0x000E5FA8 File Offset: 0x000E41A8
	// (set) Token: 0x06003D15 RID: 15637 RVA: 0x000E5FB0 File Offset: 0x000E41B0
	public Color32 ShadowColor { get; set; }

	// Token: 0x17000BF0 RID: 3056
	// (get) Token: 0x06003D16 RID: 15638 RVA: 0x000E5FBC File Offset: 0x000E41BC
	// (set) Token: 0x06003D17 RID: 15639 RVA: 0x000E5FC4 File Offset: 0x000E41C4
	public Vector2 ShadowOffset { get; set; }

	// Token: 0x17000BF1 RID: 3057
	// (get) Token: 0x06003D18 RID: 15640 RVA: 0x000E5FD0 File Offset: 0x000E41D0
	// (set) Token: 0x06003D19 RID: 15641 RVA: 0x000E5FD8 File Offset: 0x000E41D8
	public int TabSize { get; set; }

	// Token: 0x17000BF2 RID: 3058
	// (get) Token: 0x06003D1A RID: 15642 RVA: 0x000E5FE4 File Offset: 0x000E41E4
	// (set) Token: 0x06003D1B RID: 15643 RVA: 0x000E5FEC File Offset: 0x000E41EC
	public List<int> TabStops { get; set; }

	// Token: 0x17000BF3 RID: 3059
	// (get) Token: 0x06003D1C RID: 15644 RVA: 0x000E5FF8 File Offset: 0x000E41F8
	// (set) Token: 0x06003D1D RID: 15645 RVA: 0x000E6000 File Offset: 0x000E4200
	public Vector2 RenderedSize { get; internal set; }

	// Token: 0x17000BF4 RID: 3060
	// (get) Token: 0x06003D1E RID: 15646 RVA: 0x000E600C File Offset: 0x000E420C
	// (set) Token: 0x06003D1F RID: 15647 RVA: 0x000E6014 File Offset: 0x000E4214
	public int LinesRendered { get; internal set; }

	// Token: 0x06003D20 RID: 15648
	public abstract void Release();

	// Token: 0x06003D21 RID: 15649
	public abstract float[] GetCharacterWidths(string text);

	// Token: 0x06003D22 RID: 15650
	public abstract Vector2 MeasureString(string text);

	// Token: 0x06003D23 RID: 15651
	public abstract void Render(string text, dfRenderData destination);

	// Token: 0x06003D24 RID: 15652 RVA: 0x000E6020 File Offset: 0x000E4220
	protected virtual void Reset()
	{
		this.Font = null;
		this.PixelRatio = 0f;
		this.TextScale = 1f;
		this.CharacterSpacing = 0;
		this.VectorOffset = Vector3.zero;
		this.ProcessMarkup = false;
		this.WordWrap = false;
		this.MultiLine = false;
		this.OverrideMarkupColors = false;
		this.ColorizeSymbols = false;
		this.TextAlign = 0;
		this.DefaultColor = Color.white;
		this.BottomColor = null;
		this.Opacity = 1f;
		this.Outline = false;
		this.Shadow = false;
	}

	// Token: 0x06003D25 RID: 15653 RVA: 0x000E60C0 File Offset: 0x000E42C0
	public void Dispose()
	{
		this.Release();
	}
}
