using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Rust.Steam
{
	// Token: 0x0200048B RID: 1163
	public static class SteamGroups
	{
		// Token: 0x0600283A RID: 10298 RVA: 0x00092A98 File Offset: 0x00090C98
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

		// Token: 0x0600283B RID: 10299 RVA: 0x00092AE4 File Offset: 0x00090CE4
		public static bool MemberOf(ulong iGroupID)
		{
			return SteamGroups.groupList.Any((SteamGroups.Group item) => item.steamid == iGroupID);
		}

		// Token: 0x0600283C RID: 10300
		[DllImport("librust")]
		private static extern int SteamGroup_GetCount();

		// Token: 0x0600283D RID: 10301
		[DllImport("librust")]
		private static extern ulong SteamGroup_GetSteamID(int iCount);

		// Token: 0x04001339 RID: 4921
		public static List<SteamGroups.Group> groupList = new List<SteamGroups.Group>();

		// Token: 0x0200048C RID: 1164
		public class Group
		{
			// Token: 0x0400133A RID: 4922
			public ulong steamid;
		}
	}
}
