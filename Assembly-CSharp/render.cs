using System;
using UnityEngine;

// Token: 0x0200005F RID: 95
public class render : global::ConsoleSystem
{
	// Token: 0x17000088 RID: 136
	// (get) Token: 0x06000313 RID: 787 RVA: 0x0000F7A8 File Offset: 0x0000D9A8
	// (set) Token: 0x06000314 RID: 788 RVA: 0x0000F7BC File Offset: 0x0000D9BC
	[global::ConsoleSystem.Help("The render quality level. (0-1)", "")]
	[global::ConsoleSystem.Client]
	public static float level
	{
		get
		{
			return (float)QualitySettings.GetQualityLevel() / (float)(QualitySettings.names.Length - 1);
		}
		set
		{
			int num = Mathf.RoundToInt(value * (float)(QualitySettings.names.Length - 1));
			QualitySettings.SetQualityLevel(num, true);
			global::render.update();
		}
	}

	// Token: 0x17000089 RID: 137
	// (get) Token: 0x06000315 RID: 789 RVA: 0x0000F7E8 File Offset: 0x0000D9E8
	// (set) Token: 0x06000316 RID: 790 RVA: 0x0000F7F0 File Offset: 0x0000D9F0
	[global::ConsoleSystem.Client]
	[global::ConsoleSystem.Saved]
	[global::ConsoleSystem.Help("The relative render distance. (0-1)", "")]
	public static float distance
	{
		get
		{
			return global::render.distance_real;
		}
		set
		{
			global::render.distance_real = Mathf.Clamp01(value);
			global::render.update();
		}
	}

	// Token: 0x1700008A RID: 138
	// (get) Token: 0x06000317 RID: 791 RVA: 0x0000F804 File Offset: 0x0000DA04
	// (set) Token: 0x06000318 RID: 792 RVA: 0x0000F80C File Offset: 0x0000DA0C
	[global::ConsoleSystem.Client]
	[global::ConsoleSystem.Help("The limit for how many frames may be rendered per second. (default -1 for no fps limit)", "")]
	[global::ConsoleSystem.Saved]
	public static int frames
	{
		get
		{
			return global::render.frames_real;
		}
		set
		{
			global::render.frames_real = value;
			global::render.update();
		}
	}

	// Token: 0x1700008B RID: 139
	// (get) Token: 0x06000319 RID: 793 RVA: 0x0000F81C File Offset: 0x0000DA1C
	// (set) Token: 0x0600031A RID: 794 RVA: 0x0000F824 File Offset: 0x0000DA24
	[global::ConsoleSystem.Saved]
	[global::ConsoleSystem.Help("The field of view. (60-120, default 60)", "")]
	[global::ConsoleSystem.Client]
	public static int fov
	{
		get
		{
			return global::render.fov_real;
		}
		set
		{
			global::render.fov_real = Mathf.Clamp(value, 60, 120);
			global::render.update();
		}
	}

	// Token: 0x1700008C RID: 140
	// (get) Token: 0x0600031B RID: 795 RVA: 0x0000F83C File Offset: 0x0000DA3C
	// (set) Token: 0x0600031C RID: 796 RVA: 0x0000F844 File Offset: 0x0000DA44
	[global::ConsoleSystem.Client]
	[global::ConsoleSystem.Saved]
	[global::ConsoleSystem.Help("Whether VSync should be enabled or disabled", "")]
	public static bool vsync
	{
		get
		{
			return global::render.vsync_real;
		}
		set
		{
			global::render.vsync_real = value;
			global::render.update();
		}
	}

	// Token: 0x0600031D RID: 797 RVA: 0x0000F854 File Offset: 0x0000DA54
	[global::ConsoleSystem.Help("Makes sure settings match their convar values. You shouldn't need to call this manually.", "")]
	[global::ConsoleSystem.Client]
	public static void update(ref global::ConsoleSystem.Arg args)
	{
		global::render.update();
	}

	// Token: 0x0600031E RID: 798 RVA: 0x0000F85C File Offset: 0x0000DA5C
	private static void update()
	{
		QualitySettings.vSyncCount = ((!global::render.vsync_real) ? 0 : 1);
		Application.targetFrameRate = global::render.frames_real;
		int qualityLevel = QualitySettings.GetQualityLevel();
		if (PlayerPrefs.GetInt("UnityGraphicsQualityBackup", -1) != qualityLevel)
		{
			PlayerPrefs.SetInt("UnityGraphicsQualityBackup", qualityLevel);
			switch (qualityLevel)
			{
			case 0:
			case 1:
			case 2:
				global::gfx.ssaa = false;
				global::gfx.bloom = false;
				global::gfx.ssao = false;
				global::gfx.tonemap = false;
				global::gfx.shafts = false;
				break;
			case 3:
				global::gfx.ssaa = false;
				global::gfx.bloom = false;
				global::gfx.ssao = false;
				global::gfx.tonemap = false;
				global::gfx.shafts = true;
				break;
			case 4:
				break;
			default:
				global::gfx.ssaa = true;
				global::gfx.bloom = true;
				global::gfx.ssao = true;
				global::gfx.tonemap = true;
				global::gfx.shafts = true;
				break;
			}
		}
		global::GameEvent.DoQualitySettingsRefresh();
	}

	// Token: 0x0400020B RID: 523
	private static float distance_real = 0.2f;

	// Token: 0x0400020C RID: 524
	private static int frames_real = -1;

	// Token: 0x0400020D RID: 525
	private static int fov_real = 60;

	// Token: 0x0400020E RID: 526
	private static bool vsync_real;
}
