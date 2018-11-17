using System;
using NGUI.MessageUtil;
using UnityEngine;

// Token: 0x020008B3 RID: 2227
public static class DropNotification
{
	// Token: 0x06004C14 RID: 19476 RVA: 0x00129AD0 File Offset: 0x00127CD0
	public static void StopDragging(GameObject item)
	{
		if (global::DropNotification.inDrag == global::DragEventKind.None)
		{
			Debug.LogError("StopDragging can only be called from within Drop or Land messages");
		}
		else if (item != global::DropNotification.scanItem)
		{
			Debug.LogWarning("StopDragging was called with a invalid value, should have been the thing being dragged");
		}
		else
		{
			global::DropNotification.stopDrag = true;
		}
	}

	// Token: 0x06004C15 RID: 19477 RVA: 0x00129B1C File Offset: 0x00127D1C
	private static bool Message(GameObject target, GameObject parameter, string messageName, GameObject scan, global::DragEventKind kind, ref bool drop)
	{
		if (target)
		{
			GameObject gameObject = global::DropNotification.scanItem;
			bool flag = global::DropNotification.stopDrag;
			global::DragEventKind dragEventKind = global::DropNotification.inDrag;
			try
			{
				global::DropNotification.scanItem = scan;
				global::DropNotification.stopDrag = drop;
				global::DropNotification.inDrag = kind;
				target.NGUIMessage(messageName, parameter);
				drop = global::DropNotification.stopDrag;
				return true;
			}
			finally
			{
				global::DropNotification.scanItem = gameObject;
				global::DropNotification.stopDrag = flag;
				global::DropNotification.inDrag = dragEventKind;
			}
			return false;
		}
		return false;
	}

	// Token: 0x06004C16 RID: 19478 RVA: 0x00129BAC File Offset: 0x00127DAC
	private static bool Message(GameObject target, string messageName, GameObject scan, global::DragEventKind kind, ref bool drop)
	{
		if (target)
		{
			GameObject gameObject = global::DropNotification.scanItem;
			bool flag = global::DropNotification.stopDrag;
			global::DragEventKind dragEventKind = global::DropNotification.inDrag;
			try
			{
				global::DropNotification.scanItem = scan;
				global::DropNotification.stopDrag = drop;
				global::DropNotification.inDrag = kind;
				target.NGUIMessage(messageName);
				drop = global::DropNotification.stopDrag;
				return true;
			}
			finally
			{
				global::DropNotification.scanItem = gameObject;
				global::DropNotification.stopDrag = flag;
				global::DropNotification.inDrag = dragEventKind;
			}
			return false;
		}
		return false;
	}

	// Token: 0x06004C17 RID: 19479 RVA: 0x00129C38 File Offset: 0x00127E38
	internal static bool DropMessage(ref global::DropNotificationFlags flags, global::DragEventKind kind, GameObject Pressed, GameObject Released)
	{
		bool result;
		bool flag;
		global::DropNotificationFlags dropNotificationFlags;
		global::DropNotificationFlags dropNotificationFlags2;
		global::DropNotificationFlags dropNotificationFlags3;
		string messageName;
		string messageName2;
		switch (kind)
		{
		case global::DragEventKind.Drag:
			result = true;
			if (Released)
			{
				flag = true;
				dropNotificationFlags = global::DropNotificationFlags.DragDrop;
				dropNotificationFlags2 = global::DropNotificationFlags.DragLand;
				dropNotificationFlags3 = global::DropNotificationFlags.DragReverse;
				messageName = "OnDrop";
				messageName2 = "OnLand";
			}
			else
			{
				flag = false;
				dropNotificationFlags = (global::DropNotificationFlags)-2147483648;
				dropNotificationFlags3 = global::DropNotificationFlags.DragLandOutside;
				dropNotificationFlags2 = global::DropNotificationFlags.DragLandOutside;
				messageName = "----";
				messageName2 = "OnLandOutside";
			}
			break;
		case global::DragEventKind.Alt:
			flag = true;
			result = false;
			dropNotificationFlags = global::DropNotificationFlags.AltDrop;
			dropNotificationFlags2 = global::DropNotificationFlags.AltLand;
			dropNotificationFlags3 = global::DropNotificationFlags.AltReverse;
			messageName = "OnAltDrop";
			messageName2 = "OnAltLand";
			break;
		case global::DragEventKind.Mid:
			flag = true;
			result = false;
			dropNotificationFlags = global::DropNotificationFlags.MidDrop;
			dropNotificationFlags2 = global::DropNotificationFlags.MidLand;
			dropNotificationFlags3 = global::DropNotificationFlags.MidReverse;
			messageName = "OnMidDrop";
			messageName2 = "OnMidLand";
			break;
		default:
			throw new ArgumentOutOfRangeException();
		}
		if ((flags & dropNotificationFlags3) == dropNotificationFlags3)
		{
			if ((flags & dropNotificationFlags2) == dropNotificationFlags2)
			{
				if (flag)
				{
					global::DropNotification.Message(Pressed, Released, messageName2, Pressed, kind, ref result);
				}
				else
				{
					global::DropNotification.Message(Pressed, messageName2, Pressed, kind, ref result);
				}
			}
			if ((flags & dropNotificationFlags) == dropNotificationFlags)
			{
				if (flag)
				{
					global::DropNotification.Message(Released, Pressed, messageName, Pressed, kind, ref result);
				}
				else
				{
					global::DropNotification.Message(Released, messageName, Pressed, kind, ref result);
				}
			}
		}
		else
		{
			if ((flags & dropNotificationFlags) == dropNotificationFlags)
			{
				if (flag)
				{
					global::DropNotification.Message(Released, Pressed, messageName, Pressed, kind, ref result);
				}
				else
				{
					global::DropNotification.Message(Released, messageName, Pressed, kind, ref result);
				}
			}
			if ((flags & dropNotificationFlags2) == dropNotificationFlags2)
			{
				if (flag)
				{
					global::DropNotification.Message(Pressed, Released, messageName2, Pressed, kind, ref result);
				}
				else
				{
					global::DropNotification.Message(Pressed, messageName2, Pressed, kind, ref result);
				}
			}
		}
		return result;
	}

	// Token: 0x040029DB RID: 10715
	public const global::DropNotificationFlags DragDrop = global::DropNotificationFlags.DragDrop;

	// Token: 0x040029DC RID: 10716
	public const global::DropNotificationFlags DragLand = global::DropNotificationFlags.DragLand;

	// Token: 0x040029DD RID: 10717
	public const global::DropNotificationFlags kDragReverseBit = global::DropNotificationFlags.DragReverse;

	// Token: 0x040029DE RID: 10718
	public const global::DropNotificationFlags AltDrop = global::DropNotificationFlags.AltDrop;

	// Token: 0x040029DF RID: 10719
	public const global::DropNotificationFlags AltLand = global::DropNotificationFlags.AltLand;

	// Token: 0x040029E0 RID: 10720
	public const global::DropNotificationFlags kAltReverseBit = global::DropNotificationFlags.AltReverse;

	// Token: 0x040029E1 RID: 10721
	public const global::DropNotificationFlags MidDrop = global::DropNotificationFlags.MidDrop;

	// Token: 0x040029E2 RID: 10722
	public const global::DropNotificationFlags MidLand = global::DropNotificationFlags.MidLand;

	// Token: 0x040029E3 RID: 10723
	public const global::DropNotificationFlags kMidReverseBit = global::DropNotificationFlags.MidReverse;

	// Token: 0x040029E4 RID: 10724
	public const global::DropNotificationFlags DragLandOutside = global::DropNotificationFlags.DragLandOutside;

	// Token: 0x040029E5 RID: 10725
	private const global::DropNotificationFlags kInvalidNeverSet = (global::DropNotificationFlags)-2147483648;

	// Token: 0x040029E6 RID: 10726
	public const global::DropNotificationFlags DragDropThenLand = global::DropNotificationFlags.DragDrop | global::DropNotificationFlags.DragLand;

	// Token: 0x040029E7 RID: 10727
	public const global::DropNotificationFlags DragLandThenDrop = global::DropNotificationFlags.DragDrop | global::DropNotificationFlags.DragLand | global::DropNotificationFlags.DragReverse;

	// Token: 0x040029E8 RID: 10728
	public const global::DropNotificationFlags AltDropThenLand = global::DropNotificationFlags.AltDrop | global::DropNotificationFlags.AltLand;

	// Token: 0x040029E9 RID: 10729
	public const global::DropNotificationFlags AltLandThenDrop = global::DropNotificationFlags.AltDrop | global::DropNotificationFlags.AltLand | global::DropNotificationFlags.AltReverse;

	// Token: 0x040029EA RID: 10730
	public const global::DropNotificationFlags MidDropThenLand = global::DropNotificationFlags.MidDrop | global::DropNotificationFlags.MidLand;

	// Token: 0x040029EB RID: 10731
	public const global::DropNotificationFlags MidLandThenDrop = global::DropNotificationFlags.MidDrop | global::DropNotificationFlags.MidLand | global::DropNotificationFlags.MidReverse;

	// Token: 0x040029EC RID: 10732
	public const global::DropNotificationFlags kDefault = global::DropNotificationFlags.DragDrop;

	// Token: 0x040029ED RID: 10733
	public const global::DropNotificationFlags kMask_Drag = global::DropNotificationFlags.DragDrop | global::DropNotificationFlags.DragLand | global::DropNotificationFlags.DragReverse;

	// Token: 0x040029EE RID: 10734
	public const global::DropNotificationFlags kMask_Alt = global::DropNotificationFlags.AltDrop | global::DropNotificationFlags.AltLand | global::DropNotificationFlags.AltReverse;

	// Token: 0x040029EF RID: 10735
	public const global::DropNotificationFlags kMask_Mid = global::DropNotificationFlags.MidDrop | global::DropNotificationFlags.MidLand | global::DropNotificationFlags.MidReverse;

	// Token: 0x040029F0 RID: 10736
	public const global::DropNotificationFlags kMask_Active = global::DropNotificationFlags.DragDrop | global::DropNotificationFlags.DragLand | global::DropNotificationFlags.AltDrop | global::DropNotificationFlags.AltLand | global::DropNotificationFlags.MidDrop | global::DropNotificationFlags.MidLand;

	// Token: 0x040029F1 RID: 10737
	public const global::DropNotificationFlags Disable = (global::DropNotificationFlags)0;

	// Token: 0x040029F2 RID: 10738
	private static GameObject scanItem;

	// Token: 0x040029F3 RID: 10739
	private static bool stopDrag;

	// Token: 0x040029F4 RID: 10740
	private static global::DragEventKind inDrag;
}
