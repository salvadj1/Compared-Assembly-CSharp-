using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x020004B4 RID: 1204
public static class FavouriteList
{
	// Token: 0x06002923 RID: 10531 RVA: 0x00096A58 File Offset: 0x00094C58
	public static void Add(string strName)
	{
		if (global::FavouriteList.Contains(strName))
		{
			return;
		}
		if (strName.Length < 8)
		{
			return;
		}
		global::FavouriteList.faveList.Add(strName);
	}

	// Token: 0x06002924 RID: 10532 RVA: 0x00096A8C File Offset: 0x00094C8C
	public static bool Remove(string strName)
	{
		return global::FavouriteList.Contains(strName) && global::FavouriteList.faveList.Remove(strName);
	}

	// Token: 0x06002925 RID: 10533 RVA: 0x00096AA8 File Offset: 0x00094CA8
	public static bool Contains(string strName)
	{
		return global::FavouriteList.faveList.Contains(strName);
	}

	// Token: 0x06002926 RID: 10534 RVA: 0x00096AB8 File Offset: 0x00094CB8
	public static void Save()
	{
		string text = string.Empty;
		if (!Directory.Exists("cfg"))
		{
			Directory.CreateDirectory("cfg");
		}
		foreach (string text2 in global::FavouriteList.faveList)
		{
			text = text + "serverfavourite.add \"" + text2.ToString() + "\"\r\n";
			Debug.Log("serverfavourite.add \"" + text2.ToString() + "\"\r\n");
		}
		File.WriteAllText("cfg/favourites.cfg", text);
		Debug.Log(text);
	}

	// Token: 0x06002927 RID: 10535 RVA: 0x00096B78 File Offset: 0x00094D78
	public static void Load()
	{
		global::FavouriteList.Clear();
		if (!File.Exists("cfg/favourites.cfg"))
		{
			return;
		}
		string text = File.ReadAllText("cfg/favourites.cfg");
		if (string.IsNullOrEmpty(text))
		{
			return;
		}
		Debug.Log("Running cfg/favourites.cfg");
		global::ConsoleSystem.RunFile(text);
	}

	// Token: 0x06002928 RID: 10536 RVA: 0x00096BC4 File Offset: 0x00094DC4
	public static void Clear()
	{
		global::FavouriteList.faveList.Clear();
	}

	// Token: 0x040013BD RID: 5053
	private static List<string> faveList = new List<string>();
}
