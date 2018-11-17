using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000795 RID: 1941
public abstract class dfFontRendererBase : IDisposable
{
	// Token: 0x17000C60 RID: 3168
	// (get) Token: 0x060040F8 RID: 16632 RVA: 0x000EE970 File Offset: 0x000ECB70
	// (set) Token: 0x060040F9 RID: 16633 RVA: 0x000EE978 File Offset: 0x000ECB78
	public global::dfFontBase Font { get; protected set; }

	// Token: 0x17000C61 RID: 3169
	// (get) Token: 0x060040FA RID: 16634 RVA: 0x000EE984 File Offset: 0x000ECB84
	// (set) Token: 0x060040FB RID: 16635 RVA: 0x000EE98C File Offset: 0x000ECB8C
	public Vector2 MaxSize { get; set; }

	// Token: 0x17000C62 RID: 3170
	// (get) Token: 0x060040FC RID: 16636 RVA: 0x000EE998 File Offset: 0x000ECB98
	// (set) Token: 0x060040FD RID: 16637 RVA: 0x000EE9A0 File Offset: 0x000ECBA0
	public float PixelRatio { get; set; }

	// Token: 0x17000C63 RID: 3171
	// (get) Token: 0x060040FE RID: 16638 RVA: 0x000EE9AC File Offset: 0x000ECBAC
	// (set) Token: 0x060040FF RID: 16639 RVA: 0x000EE9B4 File Offset: 0x000ECBB4
	public float TextScale { get; set; }

	// Token: 0x17000C64 RID: 3172
	// (get) Token: 0x06004100 RID: 16640 RVA: 0x000EE9C0 File Offset: 0x000ECBC0
	// (set) Token: 0x06004101 RID: 16641 RVA: 0x000EE9C8 File Offset: 0x000ECBC8
	public int CharacterSpacing { get; set; }

	// Token: 0x17000C65 RID: 3173
	// (get) Token: 0x06004102 RID: 16642 RVA: 0x000EE9D4 File Offset: 0x000ECBD4
	// (set) Token: 0x06004103 RID: 16643 RVA: 0x000EE9DC File Offset: 0x000ECBDC
	public Vector3 VectorOffset { get; set; }

	// Token: 0x17000C66 RID: 3174
	// (get) Token: 0x06004104 RID: 16644 RVA: 0x000EE9E8 File Offset: 0x000ECBE8
	// (set) Token: 0x06004105 RID: 16645 RVA: 0x000EE9F0 File Offset: 0x000ECBF0
	public bool ProcessMarkup { get; set; }

	// Token: 0x17000C67 RID: 3175
	// (get) Token: 0x06004106 RID: 16646 RVA: 0x000EE9FC File Offset: 0x000ECBFC
	// (set) Token: 0x06004107 RID: 16647 RVA: 0x000EEA04 File Offset: 0x000ECC04
	public bool WordWrap { get; set; }

	// Token: 0x17000C68 RID: 3176
	// (get) Token: 0x06004108 RID: 16648 RVA: 0x000EEA10 File Offset: 0x000ECC10
	// (set) Token: 0x06004109 RID: 16649 RVA: 0x000EEA18 File Offset: 0x000ECC18
	public bool MultiLine { get; set; }

	// Token: 0x17000C69 RID: 3177
	// (get) Token: 0x0600410A RID: 16650 RVA: 0x000EEA24 File Offset: 0x000ECC24
	// (set) Token: 0x0600410B RID: 16651 RVA: 0x000EEA2C File Offset: 0x000ECC2C
	public bool OverrideMarkupColors { get; set; }

	// Token: 0x17000C6A RID: 3178
	// (get) Token: 0x0600410C RID: 16652 RVA: 0x000EEA38 File Offset: 0x000ECC38
	// (set) Token: 0x0600410D RID: 16653 RVA: 0x000EEA40 File Offset: 0x000ECC40
	public bool ColorizeSymbols { get; set; }

	// Token: 0x17000C6B RID: 3179
	// (get) Token: 0x0600410E RID: 16654 RVA: 0x000EEA4C File Offset: 0x000ECC4C
	// (set) Token: 0x0600410F RID: 16655 RVA: 0x000EEA54 File Offset: 0x000ECC54
	public TextAlignment TextAlign { get; set; }

	// Token: 0x17000C6C RID: 3180
	// (get) Token: 0x06004110 RID: 16656 RVA: 0x000EEA60 File Offset: 0x000ECC60
	// (set) Token: 0x06004111 RID: 16657 RVA: 0x000EEA68 File Offset: 0x000ECC68
	public Color32 DefaultColor { get; set; }

	// Token: 0x17000C6D RID: 3181
	// (get) Token: 0x06004112 RID: 16658 RVA: 0x000EEA74 File Offset: 0x000ECC74
	// (set) Token: 0x06004113 RID: 16659 RVA: 0x000EEA7C File Offset: 0x000ECC7C
	public Color32? BottomColor { get; set; }

	// Token: 0x17000C6E RID: 3182
	// (get) Token: 0x06004114 RID: 16660 RVA: 0x000EEA88 File Offset: 0x000ECC88
	// (set) Token: 0x06004115 RID: 16661 RVA: 0x000EEA90 File Offset: 0x000ECC90
	public float Opacity { get; set; }

	// Token: 0x17000C6F RID: 3183
	// (get) Token: 0x06004116 RID: 16662 RVA: 0x000EEA9C File Offset: 0x000ECC9C
	// (set) Token: 0x06004117 RID: 16663 RVA: 0x000EEAA4 File Offset: 0x000ECCA4
	public bool Outline { get; set; }

	// Token: 0x17000C70 RID: 3184
	// (get) Token: 0x06004118 RID: 16664 RVA: 0x000EEAB0 File Offset: 0x000ECCB0
	// (set) Token: 0x06004119 RID: 16665 RVA: 0x000EEAB8 File Offset: 0x000ECCB8
	public int OutlineSize { get; set; }

	// Token: 0x17000C71 RID: 3185
	// (get) Token: 0x0600411A RID: 16666 RVA: 0x000EEAC4 File Offset: 0x000ECCC4
	// (set) Token: 0x0600411B RID: 16667 RVA: 0x000EEACC File Offset: 0x000ECCCC
	public Color32 OutlineColor { get; set; }

	// Token: 0x17000C72 RID: 3186
	// (get) Token: 0x0600411C RID: 16668 RVA: 0x000EEAD8 File Offset: 0x000ECCD8
	// (set) Token: 0x0600411D RID: 16669 RVA: 0x000EEAE0 File Offset: 0x000ECCE0
	public bool Shadow { get; set; }

	// Token: 0x17000C73 RID: 3187
	// (get) Token: 0x0600411E RID: 16670 RVA: 0x000EEAEC File Offset: 0x000ECCEC
	// (set) Token: 0x0600411F RID: 16671 RVA: 0x000EEAF4 File Offset: 0x000ECCF4
	public Color32 ShadowColor { get; set; }

	// Token: 0x17000C74 RID: 3188
	// (get) Token: 0x06004120 RID: 16672 RVA: 0x000EEB00 File Offset: 0x000ECD00
	// (set) Token: 0x06004121 RID: 16673 RVA: 0x000EEB08 File Offset: 0x000ECD08
	public Vector2 ShadowOffset { get; set; }

	// Token: 0x17000C75 RID: 3189
	// (get) Token: 0x06004122 RID: 16674 RVA: 0x000EEB14 File Offset: 0x000ECD14
	// (set) Token: 0x06004123 RID: 16675 RVA: 0x000EEB1C File Offset: 0x000ECD1C
	public int TabSize { get; set; }

	// Token: 0x17000C76 RID: 3190
	// (get) Token: 0x06004124 RID: 16676 RVA: 0x000EEB28 File Offset: 0x000ECD28
	// (set) Token: 0x06004125 RID: 16677 RVA: 0x000EEB30 File Offset: 0x000ECD30
	public List<int> TabStops { get; set; }

	// Token: 0x17000C77 RID: 3191
	// (get) Token: 0x06004126 RID: 16678 RVA: 0x000EEB3C File Offset: 0x000ECD3C
	// (set) Token: 0x06004127 RID: 16679 RVA: 0x000EEB44 File Offset: 0x000ECD44
	public Vector2 RenderedSize { get; internal set; }

	// Token: 0x17000C78 RID: 3192
	// (get) Token: 0x06004128 RID: 16680 RVA: 0x000EEB50 File Offset: 0x000ECD50
	// (set) Token: 0x06004129 RID: 16681 RVA: 0x000EEB58 File Offset: 0x000ECD58
	public int LinesRendered { get; internal set; }

	// Token: 0x0600412A RID: 16682
	public abstract void Release();

	// Token: 0x0600412B RID: 16683
	public abstract float[] GetCharacterWidths(string text);

	// Token: 0x0600412C RID: 16684
	public abstract Vector2 MeasureString(string text);

	// Token: 0x0600412D RID: 16685
	public abstract void Render(string text, global::dfRenderData destination);

	// Token: 0x0600412E RID: 16686 RVA: 0x000EEB64 File Offset: 0x000ECD64
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

	// Token: 0x0600412F RID: 16687 RVA: 0x000EEC04 File Offset: 0x000ECE04
	public void Dispose()
	{
		this.Release();
	}
}
