using System;

// Token: 0x020004B5 RID: 1205
public class serverfavourite : global::ConsoleSystem
{
	// Token: 0x0600292A RID: 10538 RVA: 0x00096BD8 File Offset: 0x00094DD8
	[global::ConsoleSystem.Help("Adds a server to favourites", "")]
	[global::ConsoleSystem.Client]
	public static void add(ref global::ConsoleSystem.Arg arg)
	{
		string @string = arg.GetString(0, string.Empty);
		global::FavouriteList.Add(@string);
	}

	// Token: 0x0600292B RID: 10539 RVA: 0x00096BFC File Offset: 0x00094DFC
	[global::ConsoleSystem.Client]
	[global::ConsoleSystem.Help("Removes a server to favourites", "")]
	public static void remove(ref global::ConsoleSystem.Arg arg)
	{
		string @string = arg.GetString(0, string.Empty);
		global::FavouriteList.Remove(@string);
	}

	// Token: 0x0600292C RID: 10540 RVA: 0x00096C20 File Offset: 0x00094E20
	[global::ConsoleSystem.Help("Save fave list", "")]
	[global::ConsoleSystem.Client]
	public static void save(ref global::ConsoleSystem.Arg arg)
	{
		global::FavouriteList.Save();
	}

	// Token: 0x0600292D RID: 10541 RVA: 0x00096C28 File Offset: 0x00094E28
	[global::ConsoleSystem.Help("Load fave list", "")]
	[global::ConsoleSystem.Client]
	public static void load(ref global::ConsoleSystem.Arg arg)
	{
		global::FavouriteList.Load();
	}
}
