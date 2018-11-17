using System;
using UnityEngine;

// Token: 0x02000794 RID: 1940
[Serializable]
public abstract class dfFontBase : MonoBehaviour
{
	// Token: 0x17000C5B RID: 3163
	// (get) Token: 0x060040EE RID: 16622
	// (set) Token: 0x060040EF RID: 16623
	public abstract Material Material { get; set; }

	// Token: 0x17000C5C RID: 3164
	// (get) Token: 0x060040F0 RID: 16624
	public abstract Texture Texture { get; }

	// Token: 0x17000C5D RID: 3165
	// (get) Token: 0x060040F1 RID: 16625
	public abstract bool IsValid { get; }

	// Token: 0x17000C5E RID: 3166
	// (get) Token: 0x060040F2 RID: 16626
	// (set) Token: 0x060040F3 RID: 16627
	public abstract int FontSize { get; set; }

	// Token: 0x17000C5F RID: 3167
	// (get) Token: 0x060040F4 RID: 16628
	// (set) Token: 0x060040F5 RID: 16629
	public abstract int LineHeight { get; set; }

	// Token: 0x060040F6 RID: 16630
	public abstract global::dfFontRendererBase ObtainRenderer();
}
