using System;
using Facepunch.Utility;

namespace Rust
{
	// Token: 0x020004A9 RID: 1193
	public static class Notice
	{
		// Token: 0x060028D6 RID: 10454 RVA: 0x00095710 File Offset: 0x00093910
		public static void Popup(string strIcon, string strText, float fDuration = 4f)
		{
			strIcon = Facepunch.Utility.String.QuoteSafe(strIcon);
			strText = Facepunch.Utility.String.QuoteSafe(strText);
			global::ConsoleSystem.Run(string.Concat(new string[]
			{
				"notice.popup ",
				fDuration.ToString(),
				" ",
				strIcon,
				" ",
				strText
			}), false);
		}

		// Token: 0x060028D7 RID: 10455 RVA: 0x0009576C File Offset: 0x0009396C
		public static void Inventory(string strIcon, string strText)
		{
			strText = Facepunch.Utility.String.QuoteSafe(strText);
			global::ConsoleSystem.Run("notice.inventory " + strText, false);
		}
	}
}
