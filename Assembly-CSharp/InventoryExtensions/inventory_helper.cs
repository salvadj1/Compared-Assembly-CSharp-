using System;
using uLink;

namespace InventoryExtensions
{
	// Token: 0x02000694 RID: 1684
	public static class inventory_helper
	{
		// Token: 0x060039BA RID: 14778 RVA: 0x000CB700 File Offset: 0x000C9900
		public static int ReadInvInt(this BitStream stream)
		{
			return (int)stream.ReadByte();
		}

		// Token: 0x060039BB RID: 14779 RVA: 0x000CB708 File Offset: 0x000C9908
		public static void WriteInvInt(this BitStream stream, int i)
		{
			stream.WriteByte((byte)i);
		}

		// Token: 0x060039BC RID: 14780 RVA: 0x000CB714 File Offset: 0x000C9914
		public static void WriteInvInt(this BitStream stream, byte i)
		{
			stream.WriteByte(i);
		}
	}
}
