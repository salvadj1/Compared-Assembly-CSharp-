using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Rust.Steam
{
	// Token: 0x020003DE RID: 990
	public static class SteamGroups
	{
		// Token: 0x060024D8 RID: 9432 RVA: 0x0008D6C4 File Offset: 0x0008B8C4
		public static void Init()
		{
			SteamGroups.groupList.Clear();
			int num = SteamGroups.SteamGroup_GetCount();
			for (int i = 0; i < num; i++)
			{
				SteamGroups.Group group = new SteamGroups.Group();
				group.steamid = SteamGroups.SteamGroup_GetSteamID(i);
				SteamGroups.groupList.Add(group);
			}
		}

		// Token: 0x060024D9 RID: 9433 RVA: 0x0008D710 File Offset: 0x0008B910
		public static bool MemberOf(ulong iGroupID)
		{
			return SteamGroups.groupList.Any((SteamGroups.Group item) => item.steamid == iGroupID);
		}

		// Token: 0x060024DA RID: 9434
		[DllImport("librust")]
		private static extern int SteamGroup_GetCount();

		// Token: 0x060024DB RID: 9435
		[DllImport("librust")]
		private static extern ulong SteamGroup_GetSteamID(int iCount);

		// Token: 0x040011D3 RID: 4563
		public static List<SteamGroups.Group> groupList = new List<SteamGroups.Group>();

		// Token: 0x020003DF RID: 991
		public class Group
		{
			// Token: 0x040011D4 RID: 4564
			public ulong steamid;
		}
	}
}
