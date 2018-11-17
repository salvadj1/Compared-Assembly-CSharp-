using System;

// Token: 0x02000049 RID: 73
public class gfx : ConsoleSystem
{
	// Token: 0x17000068 RID: 104
	// (get) Token: 0x0600027F RID: 639 RVA: 0x0000DF78 File Offset: 0x0000C178
	// (set) Token: 0x06000280 RID: 640 RVA: 0x0000DF80 File Offset: 0x0000C180
	[ConsoleSystem.Saved]
	[ConsoleSystem.Client]
	public static bool ssaa
	{
		get
		{
			return ImageEffectManager.GetEnabled<AntialiasingAsPostEffect>();
		}
		set
		{
			ImageEffectManager.SetEnabled<AntialiasingAsPostEffect>(value);
		}
	}

	// Token: 0x17000069 RID: 105
	// (get) Token: 0x06000281 RID: 641 RVA: 0x0000DF88 File Offset: 0x0000C188
	// (set) Token: 0x06000282 RID: 642 RVA: 0x0000DF90 File Offset: 0x0000C190
	[ConsoleSystem.Client]
	[ConsoleSystem.Saved]
	public static bool bloom
	{
		get
		{
			return ImageEffectManager.GetEnabled<Bloom>();
		}
		set
		{
			ImageEffectManager.SetEnabled<Bloom>(value);
		}
	}

	// Token: 0x1700006A RID: 106
	// (get) Token: 0x06000283 RID: 643 RVA: 0x0000DF98 File Offset: 0x0000C198
	// (set) Token: 0x06000284 RID: 644 RVA: 0x0000DFA0 File Offset: 0x0000C1A0
	[ConsoleSystem.Client]
	[ConsoleSystem.Saved]
	public static bool grain
	{
		get
		{
			return ImageEffectManager.GetEnabled<NoiseAndGrain>();
		}
		set
		{
			ImageEffectManager.SetEnabled<NoiseAndGrain>(value);
		}
	}

	// Token: 0x1700006B RID: 107
	// (get) Token: 0x06000285 RID: 645 RVA: 0x0000DFA8 File Offset: 0x0000C1A8
	// (set) Token: 0x06000286 RID: 646 RVA: 0x0000DFB0 File Offset: 0x0000C1B0
	[ConsoleSystem.Saved]
	[ConsoleSystem.Client]
	public static bool ssao
	{
		get
		{
			return ImageEffectManager.GetEnabled<SSAOEffect>();
		}
		set
		{
			ImageEffectManager.SetEnabled<SSAOEffect>(value);
		}
	}

	// Token: 0x1700006C RID: 108
	// (get) Token: 0x06000287 RID: 647 RVA: 0x0000DFB8 File Offset: 0x0000C1B8
	// (set) Token: 0x06000288 RID: 648 RVA: 0x0000DFC0 File Offset: 0x0000C1C0
	[ConsoleSystem.Client]
	[ConsoleSystem.Saved]
	public static bool tonemap
	{
		get
		{
			return ImageEffectManager.GetEnabled<Tonemapping>();
		}
		set
		{
			ImageEffectManager.SetEnabled<Tonemapping>(value);
		}
	}

	// Token: 0x1700006D RID: 109
	// (get) Token: 0x06000289 RID: 649 RVA: 0x0000DFC8 File Offset: 0x0000C1C8
	// (set) Token: 0x0600028A RID: 650 RVA: 0x0000DFD0 File Offset: 0x0000C1D0
	[ConsoleSystem.Client]
	[ConsoleSystem.Saved]
	public static bool shafts
	{
		get
		{
			return ImageEffectManager.GetEnabled<TOD_SunShafts>();
		}
		set
		{
			ImageEffectManager.SetEnabled<TOD_SunShafts>(value);
		}
	}

	// Token: 0x1700006E RID: 110
	// (get) Token: 0x0600028B RID: 651 RVA: 0x0000DFD8 File Offset: 0x0000C1D8
	// (set) Token: 0x0600028C RID: 652 RVA: 0x0000DFE0 File Offset: 0x0000C1E0
	[ConsoleSystem.Saved]
	[ConsoleSystem.Client]
	public static bool damage
	{
		get
		{
			return ImageEffectManager.GetEnabled<GameFullscreen>();
		}
		set
		{
			ImageEffectManager.SetEnabled<GameFullscreen>(value);
		}
	}

	// Token: 0x1700006F RID: 111
	// (get) Token: 0x0600028D RID: 653 RVA: 0x0000DFE8 File Offset: 0x0000C1E8
	// (set) Token: 0x0600028E RID: 654 RVA: 0x0000E03C File Offset: 0x0000C23C
	[ConsoleSystem.Client]
	public static bool all
	{
		get
		{
			return gfx.ssaa && gfx.bloom && gfx.grain && gfx.ssao && gfx.tonemap && gfx.shafts && gfx.damage;
		}
		set
		{
			gfx.damage = value;
			gfx.shafts = value;
			gfx.tonemap = value;
			gfx.ssao = value;
			gfx.grain = value;
			gfx.bloom = value;
			gfx.ssaa = value;
		}
	}
}
