using System;
using UnityEngine;

namespace NGUI.MessageUtil
{
	// Token: 0x020008CF RID: 2255
	public static class Boxed
	{
		// Token: 0x06004CD6 RID: 19670 RVA: 0x0012DF5C File Offset: 0x0012C15C
		public static object Box(bool b)
		{
			return (!b) ? Boxed.@false : Boxed.@true;
		}

		// Token: 0x06004CD7 RID: 19671 RVA: 0x0012DF74 File Offset: 0x0012C174
		public static object Box(int i)
		{
			switch (i)
			{
			case 0:
				return Boxed.int_0;
			case 1:
				return Boxed.int_1;
			case 2:
				return Boxed.int_2;
			default:
				return i;
			}
		}

		// Token: 0x06004CD8 RID: 19672 RVA: 0x0012DFB4 File Offset: 0x0012C1B4
		public static object Box<T>(T o)
		{
			return o;
		}

		// Token: 0x06004CD9 RID: 19673 RVA: 0x0012DFBC File Offset: 0x0012C1BC
		public static object Box(KeyCode k)
		{
			switch (k)
			{
			case 273:
				return Boxed.key_up;
			case 274:
				return Boxed.key_down;
			case 275:
				return Boxed.key_right;
			case 276:
				return Boxed.key_left;
			default:
				if (k == null)
				{
					return Boxed.key_none;
				}
				if (k == 9)
				{
					return Boxed.key_tab;
				}
				if (k != 27)
				{
					return k;
				}
				return Boxed.key_escape;
			}
		}

		// Token: 0x04002AC8 RID: 10952
		public static readonly object @true = true;

		// Token: 0x04002AC9 RID: 10953
		public static readonly object @false = false;

		// Token: 0x04002ACA RID: 10954
		public static readonly object int_0 = 0;

		// Token: 0x04002ACB RID: 10955
		public static readonly object int_1 = 1;

		// Token: 0x04002ACC RID: 10956
		public static readonly object int_2 = 2;

		// Token: 0x04002ACD RID: 10957
		public static readonly object key_escape = 27;

		// Token: 0x04002ACE RID: 10958
		public static readonly object key_left = 276;

		// Token: 0x04002ACF RID: 10959
		public static readonly object key_right = 275;

		// Token: 0x04002AD0 RID: 10960
		public static readonly object key_up = 273;

		// Token: 0x04002AD1 RID: 10961
		public static readonly object key_down = 274;

		// Token: 0x04002AD2 RID: 10962
		public static readonly object key_tab = 9;

		// Token: 0x04002AD3 RID: 10963
		public static readonly object key_none = 0;
	}
}
