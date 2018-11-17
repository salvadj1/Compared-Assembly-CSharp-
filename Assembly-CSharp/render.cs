using System;
using UnityEngine;

// Token: 0x0200004D RID: 77
public class render : ConsoleSystem
{
	// Token: 0x17000072 RID: 114
	// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000E200 File Offset: 0x0000C400
	// (set) Token: 0x060002A2 RID: 674 RVA: 0x0000E214 File Offset: 0x0000C414
	[ConsoleSystem.Help("The render quality level. (0-1)", "")]
	[ConsoleSystem.Client]
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
			render.update();
		}
	}

	// Token: 0x17000073 RID: 115
	// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000E240 File Offset: 0x0000C440
	// (set) Token: 0x060002A4 RID: 676 RVA: 0x0000E248 File Offset: 0x0000C448
	[ConsoleSystem.Client]
	[ConsoleSystem.Help("The relative render distance. (0-1)", "")]
	[ConsoleSystem.Saved]
	public static float distance
	{
		get
		{
			return render.distance_real;
		}
		set
		{
			render.distance_real = Mathf.Clamp01(value);
			render.update();
		}
	}

	// Token: 0x17000074 RID: 116
	// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000E25C File Offset: 0x0000C45C
	// (set) Token: 0x060002A6 RID: 678 RVA: 0x0000E264 File Offset: 0x0000C464
	[ConsoleSystem.Client]
	[ConsoleSystem.Saved]
	[ConsoleSystem.Help("The limit for how many frames may be rendered per second. (default -1 for no fps limit)", "")]
	public static int frames
	{
		get
		{
			return render.frames_real;
		}
		set
		{
			render.frames_real = value;
			render.update();
		}
	}

	// Token: 0x17000075 RID: 117
	// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000E274 File Offset: 0x0000C474
	// (set) Token: 0x060002A8 RID: 680 RVA: 0x0000E27C File Offset: 0x0000C47C
	[ConsoleSystem.Help("The field of view. (60-120, default 60)", "")]
	[ConsoleSystem.Saved]
	[ConsoleSystem.Client]
	public static int fov
	{
		get
		{
			return render.fov_real;
		}
		set
		{
			render.fov_real = Mathf.Clamp(value, 60, 120);
			render.update();
		}
	}

	// Token: 0x17000076 RID: 118
	// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000E294 File Offset: 0x0000C494
	// (set) Token: 0x060002AA RID: 682 RVA: 0x0000E29C File Offset: 0x0000C49C
	[ConsoleSystem.Saved]
	[ConsoleSystem.Help("Whether VSync should be enabled or disabled", "")]
	[ConsoleSystem.Client]
	public static bool vsync
	{
		get
		{
			return render.vsync_real;
		}
		set
		{
			render.vsync_real = value;
			render.update();
		}
	}

	// Token: 0x060002AB RID: 683 RVA: 0x0000E2AC File Offset: 0x0000C4AC
	[ConsoleSystem.Client]
	[ConsoleSystem.Help("Makes sure settings match their convar values. You shouldn't need to call this manually.", "")]
	public static void update(ref ConsoleSystem.Arg args)
	{
		render.update();
	}

	// Token: 0x060002AC RID: 684 RVA: 0x0000E2B4 File Offset: 0x0000C4B4
	private static void update()
	{
		QualitySettings.vSyncCount = ((!render.vsync_real) ? 0 : 1);
		Application.targetFrameRate = render.frames_real;
		int qualityLevel = QualitySettings.GetQualityLevel();
		if (PlayerPrefs.GetInt("UnityGraphicsQualityBackup", -1) != qualityLevel)
		{
			PlayerPrefs.SetInt("UnityGraphicsQualityBackup", qualityLevel);
			switch (qualityLevel)
			{
			case 0:
			case 1:
			case 2:
				gfx.ssaa = false;
				gfx.bloom = false;
				gfx.ssao = false;
				gfx.tonemap = false;
				gfx.shafts = false;
				break;
			case 3:
				gfx.ssaa = false;
				gfx.bloom = false;
				gfx.ssao = false;
				gfx.tonemap = false;
				gfx.shafts = true;
				break;
			case 4:
				break;
			default:
				gfx.ssaa = true;
				gfx.bloom = true;
				gfx.ssao = true;
				gfx.tonemap = true;
				gfx.shafts = true;
				break;
			}
		}
		GameEvent.DoQualitySettingsRefresh();
	}

	// Token: 0x040001A9 RID: 425
	private static float distance_real = 0.2f;

	// Token: 0x040001AA RID: 426
	private static int frames_real = -1;

	// Token: 0x040001AB RID: 427
	private static int fov_real = 60;

	// Token: 0x040001AC RID: 428
	private static bool vsync_real;
}
