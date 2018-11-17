using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x02000403 RID: 1027
public static class FavouriteList
{
	// Token: 0x060025AB RID: 9643 RVA: 0x00090C20 File Offset: 0x0008EE20
	public static void Add(string strName)
	{
		if (FavouriteList.Contains(strName))
		{
			return;
		}
		if (strName.Length < 8)
		{
			return;
		}
		FavouriteList.faveList.Add(strName);
	}

	// Token: 0x060025AC RID: 9644 RVA: 0x00090C54 File Offset: 0x0008EE54
	public static bool Remove(string strName)
	{
		return FavouriteList.Contains(strName) && FavouriteList.faveList.Remove(strName);
	}

	// Token: 0x060025AD RID: 9645 RVA: 0x00090C70 File Offset: 0x0008EE70
	public static bool Contains(string strName)
	{
		return FavouriteList.faveList.Contains(strName);
	}

	// Token: 0x060025AE RID: 9646 RVA: 0x00090C80 File Offset: 0x0008EE80
	public static void Save()
	{
		string text = string.Empty;
		if (!Directory.Exists("cfg"))
		{
			Directory.CreateDirectory("cfg");
		}
		foreach (string text2 in FavouriteList.faveList)
		{
			text = text + "serverfavourite.add \"" + text2.ToString() + "\"\r\n";
			Debug.Log("serverfavourite.add \"" + text2.ToString() + "\"\r\n");
		}
		File.WriteAllText("cfg/favourites.cfg", text);
		Debug.Log(text);
	}

	// Token: 0x060025AF RID: 9647 RVA: 0x00090D40 File Offset: 0x0008EF40
	public static void Load()
	{
		FavouriteList.Clear();
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
		ConsoleSystem.RunFile(text);
	}

	// Token: 0x060025B0 RID: 9648 RVA: 0x00090D8C File Offset: 0x0008EF8C
	public static void Clear()
	{
		FavouriteList.faveList.Clear();
	}

	// Token: 0x04001240 RID: 4672
	private static List<string> faveList = new List<string>();
}
