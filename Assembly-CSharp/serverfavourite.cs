using System;

// Token: 0x02000404 RID: 1028
public class serverfavourite : ConsoleSystem
{
	// Token: 0x060025B2 RID: 9650 RVA: 0x00090DA0 File Offset: 0x0008EFA0
	[ConsoleSystem.Client]
	[ConsoleSystem.Help("Adds a server to favourites", "")]
	public static void add(ref ConsoleSystem.Arg arg)
	{
		string @string = arg.GetString(0, string.Empty);
		FavouriteList.Add(@string);
	}

	// Token: 0x060025B3 RID: 9651 RVA: 0x00090DC4 File Offset: 0x0008EFC4
	[ConsoleSystem.Help("Removes a server to favourites", "")]
	[ConsoleSystem.Client]
	public static void remove(ref ConsoleSystem.Arg arg)
	{
		string @string = arg.GetString(0, string.Empty);
		FavouriteList.Remove(@string);
	}

	// Token: 0x060025B4 RID: 9652 RVA: 0x00090DE8 File Offset: 0x0008EFE8
	[ConsoleSystem.Help("Save fave list", "")]
	[ConsoleSystem.Client]
	public static void save(ref ConsoleSystem.Arg arg)
	{
		FavouriteList.Save();
	}

	// Token: 0x060025B5 RID: 9653 RVA: 0x00090DF0 File Offset: 0x0008EFF0
	[ConsoleSystem.Help("Load fave list", "")]
	[ConsoleSystem.Client]
	public static void load(ref ConsoleSystem.Arg arg)
	{
		FavouriteList.Load();
	}
}
