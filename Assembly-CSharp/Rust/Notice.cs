using System;
using Facepunch.Utility;

namespace Rust
{
	// Token: 0x020003F8 RID: 1016
	public static class Notice
	{
		// Token: 0x0600255E RID: 9566 RVA: 0x0008F8D8 File Offset: 0x0008DAD8
		public static void Popup(string strIcon, string strText, float fDuration = 4f)
		{
			strIcon = Facepunch.Utility.String.QuoteSafe(strIcon);
			strText = Facepunch.Utility.String.QuoteSafe(strText);
			ConsoleSystem.Run(string.Concat(new string[]
			{
				"notice.popup ",
				fDuration.ToString(),
				" ",
				strIcon,
				" ",
				strText
			}), false);
		}

		// Token: 0x0600255F RID: 9567 RVA: 0x0008F934 File Offset: 0x0008DB34
		public static void Inventory(string strIcon, string strText)
		{
			strText = Facepunch.Utility.String.QuoteSafe(strText);
			ConsoleSystem.Run("notice.inventory " + strText, false);
		}
	}
}
