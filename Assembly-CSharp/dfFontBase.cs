using System;
using UnityEngine;

// Token: 0x020006C9 RID: 1737
[Serializable]
public abstract class dfFontBase : MonoBehaviour
{
	// Token: 0x17000BD7 RID: 3031
	// (get) Token: 0x06003CE4 RID: 15588
	// (set) Token: 0x06003CE5 RID: 15589
	public abstract Material Material { get; set; }

	// Token: 0x17000BD8 RID: 3032
	// (get) Token: 0x06003CE6 RID: 15590
	public abstract Texture Texture { get; }

	// Token: 0x17000BD9 RID: 3033
	// (get) Token: 0x06003CE7 RID: 15591
	public abstract bool IsValid { get; }

	// Token: 0x17000BDA RID: 3034
	// (get) Token: 0x06003CE8 RID: 15592
	// (set) Token: 0x06003CE9 RID: 15593
	public abstract int FontSize { get; set; }

	// Token: 0x17000BDB RID: 3035
	// (get) Token: 0x06003CEA RID: 15594
	// (set) Token: 0x06003CEB RID: 15595
	public abstract int LineHeight { get; set; }

	// Token: 0x06003CEC RID: 15596
	public abstract dfFontRendererBase ObtainRenderer();
}
