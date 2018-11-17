using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x020007EA RID: 2026
public static class UITextMarkupUtility
{
	// Token: 0x0600486D RID: 18541 RVA: 0x00126038 File Offset: 0x00124238
	public static void SortMarkup(this List<UITextMarkup> list)
	{
		list.Sort(delegate(UITextMarkup x, UITextMarkup y)
		{
			int num = x.index.CompareTo(y.index);
			int result;
			if (num == 0)
			{
				byte mod = (byte)x.mod;
				result = mod.CompareTo((byte)y.mod);
			}
			else
			{
				result = num;
			}
			return result;
		});
	}

	// Token: 0x0600486E RID: 18542 RVA: 0x00126060 File Offset: 0x00124260
	public static string MarkUp(this List<UITextMarkup> list, string input)
	{
		int count;
		if (list == null || (count = list.Count) == 0)
		{
			return input;
		}
		int index = list[0].index;
		StringBuilder stringBuilder = new StringBuilder(input, 0, index, input.Length + count);
		int num = 0;
		UITextMarkup uitextMarkup = list[num];
		for (int i = uitextMarkup.index; i < input.Length; i++)
		{
			char c = input[i];
			if (i == uitextMarkup.index)
			{
				do
				{
					switch (uitextMarkup.mod)
					{
					case UITextMod.End:
						i = input.Length + 1;
						c = '\0';
						break;
					case UITextMod.Removed:
						c = '\0';
						break;
					case UITextMod.Replaced:
						stringBuilder.Append(uitextMarkup.value);
						c = '\0';
						break;
					case UITextMod.Added:
						stringBuilder.Append(uitextMarkup.value);
						break;
					}
					if (++num == count)
					{
						if (i < input.Length)
						{
							goto Block_4;
						}
					}
					else
					{
						uitextMarkup = list[num];
					}
				}
				while (uitextMarkup.index == i);
				IL_161:
				if (c != '\0')
				{
					stringBuilder.Append(c);
				}
				goto IL_186;
				Block_4:
				if (c != '\0')
				{
					stringBuilder.Append(input, i, input.Length - i);
				}
				else
				{
					if (++i >= input.Length)
					{
						goto IL_161;
					}
					stringBuilder.Append(input, i, input.Length - i);
				}
				i = input.Length + 1;
				goto IL_161;
			}
			if (c != '\0')
			{
				stringBuilder.Append(c);
			}
			IL_186:;
		}
		while (++num < count)
		{
			switch (uitextMarkup.mod)
			{
			case UITextMod.End:
				continue;
			case UITextMod.Added:
				stringBuilder.Append(uitextMarkup.value);
				continue;
			}
			Debug.Log("Unsupported end markup " + uitextMarkup);
		}
		return stringBuilder.ToString();
	}
}
