using System;

// Token: 0x0200005B RID: 91
public class gfx : global::ConsoleSystem
{
	// Token: 0x1700007E RID: 126
	// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000F520 File Offset: 0x0000D720
	// (set) Token: 0x060002F2 RID: 754 RVA: 0x0000F528 File Offset: 0x0000D728
	[global::ConsoleSystem.Client]
	[global::ConsoleSystem.Saved]
	public static bool ssaa
	{
		get
		{
			return global::ImageEffectManager.GetEnabled<AntialiasingAsPostEffect>();
		}
		set
		{
			global::ImageEffectManager.SetEnabled<AntialiasingAsPostEffect>(value);
		}
	}

	// Token: 0x1700007F RID: 127
	// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000F530 File Offset: 0x0000D730
	// (set) Token: 0x060002F4 RID: 756 RVA: 0x0000F538 File Offset: 0x0000D738
	[global::ConsoleSystem.Saved]
	[global::ConsoleSystem.Client]
	public static bool bloom
	{
		get
		{
			return global::ImageEffectManager.GetEnabled<Bloom>();
		}
		set
		{
			global::ImageEffectManager.SetEnabled<Bloom>(value);
		}
	}

	// Token: 0x17000080 RID: 128
	// (get) Token: 0x060002F5 RID: 757 RVA: 0x0000F540 File Offset: 0x0000D740
	// (set) Token: 0x060002F6 RID: 758 RVA: 0x0000F548 File Offset: 0x0000D748
	[global::ConsoleSystem.Client]
	[global::ConsoleSystem.Saved]
	public static bool grain
	{
		get
		{
			return global::ImageEffectManager.GetEnabled<NoiseAndGrain>();
		}
		set
		{
			global::ImageEffectManager.SetEnabled<NoiseAndGrain>(value);
		}
	}

	// Token: 0x17000081 RID: 129
	// (get) Token: 0x060002F7 RID: 759 RVA: 0x0000F550 File Offset: 0x0000D750
	// (set) Token: 0x060002F8 RID: 760 RVA: 0x0000F558 File Offset: 0x0000D758
	[global::ConsoleSystem.Client]
	[global::ConsoleSystem.Saved]
	public static bool ssao
	{
		get
		{
			return global::ImageEffectManager.GetEnabled<SSAOEffect>();
		}
		set
		{
			global::ImageEffectManager.SetEnabled<SSAOEffect>(value);
		}
	}

	// Token: 0x17000082 RID: 130
	// (get) Token: 0x060002F9 RID: 761 RVA: 0x0000F560 File Offset: 0x0000D760
	// (set) Token: 0x060002FA RID: 762 RVA: 0x0000F568 File Offset: 0x0000D768
	[global::ConsoleSystem.Saved]
	[global::ConsoleSystem.Client]
	public static bool tonemap
	{
		get
		{
			return global::ImageEffectManager.GetEnabled<Tonemapping>();
		}
		set
		{
			global::ImageEffectManager.SetEnabled<Tonemapping>(value);
		}
	}

	// Token: 0x17000083 RID: 131
	// (get) Token: 0x060002FB RID: 763 RVA: 0x0000F570 File Offset: 0x0000D770
	// (set) Token: 0x060002FC RID: 764 RVA: 0x0000F578 File Offset: 0x0000D778
	[global::ConsoleSystem.Client]
	[global::ConsoleSystem.Saved]
	public static bool shafts
	{
		get
		{
			return global::ImageEffectManager.GetEnabled<TOD_SunShafts>();
		}
		set
		{
			global::ImageEffectManager.SetEnabled<TOD_SunShafts>(value);
		}
	}

	// Token: 0x17000084 RID: 132
	// (get) Token: 0x060002FD RID: 765 RVA: 0x0000F580 File Offset: 0x0000D780
	// (set) Token: 0x060002FE RID: 766 RVA: 0x0000F588 File Offset: 0x0000D788
	[global::ConsoleSystem.Saved]
	[global::ConsoleSystem.Client]
	public static bool damage
	{
		get
		{
			return global::ImageEffectManager.GetEnabled<global::GameFullscreen>();
		}
		set
		{
			global::ImageEffectManager.SetEnabled<global::GameFullscreen>(value);
		}
	}

	// Token: 0x17000085 RID: 133
	// (get) Token: 0x060002FF RID: 767 RVA: 0x0000F590 File Offset: 0x0000D790
	// (set) Token: 0x06000300 RID: 768 RVA: 0x0000F5E4 File Offset: 0x0000D7E4
	[global::ConsoleSystem.Client]
	public static bool all
	{
		get
		{
			return global::gfx.ssaa && global::gfx.bloom && global::gfx.grain && global::gfx.ssao && global::gfx.tonemap && global::gfx.shafts && global::gfx.damage;
		}
		set
		{
			global::gfx.damage = value;
			global::gfx.shafts = value;
			global::gfx.tonemap = value;
			global::gfx.ssao = value;
			global::gfx.grain = value;
			global::gfx.bloom = value;
			global::gfx.ssaa = value;
		}
	}
}
