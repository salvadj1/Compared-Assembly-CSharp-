using System;
using UnityEngine;

namespace Facepunch.Progress
{
	// Token: 0x020001EE RID: 494
	public static class IProgressUtility
	{
		// Token: 0x06000DAB RID: 3499 RVA: 0x000354FC File Offset: 0x000336FC
		public static bool Poll(this IProgress IProgress, out float progress)
		{
			bool flag;
			if (IProgress is Object)
			{
				flag = !(Object)IProgress;
			}
			else
			{
				flag = object.ReferenceEquals(IProgress, null);
			}
			if (flag)
			{
				progress = 0f;
				return false;
			}
			float progress2 = IProgress.progress;
			if (progress2 >= 1f)
			{
				progress = 1f;
			}
			else if (progress2 <= 0f)
			{
				progress = 0f;
			}
			else
			{
				progress = progress2;
			}
			return true;
		}
	}
}
