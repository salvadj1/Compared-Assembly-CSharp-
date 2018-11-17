using System;
using UnityEngine;

namespace NGUI.MessageUtil
{
	// Token: 0x020007DE RID: 2014
	public static class Boxed
	{
		// Token: 0x0600482B RID: 18475 RVA: 0x00123FF8 File Offset: 0x001221F8
		public static object Box(bool b)
		{
			return (!b) ? Boxed.@false : Boxed.@true;
		}

		// Token: 0x0600482C RID: 18476 RVA: 0x00124010 File Offset: 0x00122210
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

		// Token: 0x0600482D RID: 18477 RVA: 0x00124050 File Offset: 0x00122250
		public static object Box<T>(T o)
		{
			return o;
		}

		// Token: 0x0600482E RID: 18478 RVA: 0x00124058 File Offset: 0x00122258
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

		// Token: 0x0400287A RID: 10362
		public static readonly object @true = true;

		// Token: 0x0400287B RID: 10363
		public static readonly object @false = false;

		// Token: 0x0400287C RID: 10364
		public static readonly object int_0 = 0;

		// Token: 0x0400287D RID: 10365
		public static readonly object int_1 = 1;

		// Token: 0x0400287E RID: 10366
		public static readonly object int_2 = 2;

		// Token: 0x0400287F RID: 10367
		public static readonly object key_escape = 27;

		// Token: 0x04002880 RID: 10368
		public static readonly object key_left = 276;

		// Token: 0x04002881 RID: 10369
		public static readonly object key_right = 275;

		// Token: 0x04002882 RID: 10370
		public static readonly object key_up = 273;

		// Token: 0x04002883 RID: 10371
		public static readonly object key_down = 274;

		// Token: 0x04002884 RID: 10372
		public static readonly object key_tab = 9;

		// Token: 0x04002885 RID: 10373
		public static readonly object key_none = 0;
	}
}
