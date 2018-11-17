using System;
using uLink;

namespace InventoryExtensions
{
	// Token: 0x020005D6 RID: 1494
	public static class inventory_helper
	{
		// Token: 0x060035F2 RID: 13810 RVA: 0x000C34A4 File Offset: 0x000C16A4
		public static int ReadInvInt(this BitStream stream)
		{
			return (int)stream.ReadByte();
		}

		// Token: 0x060035F3 RID: 13811 RVA: 0x000C34AC File Offset: 0x000C16AC
		public static void WriteInvInt(this BitStream stream, int i)
		{
			stream.WriteByte((byte)i);
		}

		// Token: 0x060035F4 RID: 13812 RVA: 0x000C34B8 File Offset: 0x000C16B8
		public static void WriteInvInt(this BitStream stream, byte i)
		{
			stream.WriteByte(i);
		}
	}
}
