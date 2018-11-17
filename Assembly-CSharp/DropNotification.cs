using System;
using NGUI.MessageUtil;
using UnityEngine;

// Token: 0x020007C6 RID: 1990
public static class DropNotification
{
	// Token: 0x06004785 RID: 18309 RVA: 0x001200AC File Offset: 0x0011E2AC
	public static void StopDragging(GameObject item)
	{
		if (DropNotification.inDrag == DragEventKind.None)
		{
			Debug.LogError("StopDragging can only be called from within Drop or Land messages");
		}
		else if (item != DropNotification.scanItem)
		{
			Debug.LogWarning("StopDragging was called with a invalid value, should have been the thing being dragged");
		}
		else
		{
			DropNotification.stopDrag = true;
		}
	}

	// Token: 0x06004786 RID: 18310 RVA: 0x001200F8 File Offset: 0x0011E2F8
	private static bool Message(GameObject target, GameObject parameter, string messageName, GameObject scan, DragEventKind kind, ref bool drop)
	{
		if (target)
		{
			GameObject gameObject = DropNotification.scanItem;
			bool flag = DropNotification.stopDrag;
			DragEventKind dragEventKind = DropNotification.inDrag;
			try
			{
				DropNotification.scanItem = scan;
				DropNotification.stopDrag = drop;
				DropNotification.inDrag = kind;
				target.NGUIMessage(messageName, parameter);
				drop = DropNotification.stopDrag;
				return true;
			}
			finally
			{
				DropNotification.scanItem = gameObject;
				DropNotification.stopDrag = flag;
				DropNotification.inDrag = dragEventKind;
			}
			return false;
		}
		return false;
	}

	// Token: 0x06004787 RID: 18311 RVA: 0x00120188 File Offset: 0x0011E388
	private static bool Message(GameObject target, string messageName, GameObject scan, DragEventKind kind, ref bool drop)
	{
		if (target)
		{
			GameObject gameObject = DropNotification.scanItem;
			bool flag = DropNotification.stopDrag;
			DragEventKind dragEventKind = DropNotification.inDrag;
			try
			{
				DropNotification.scanItem = scan;
				DropNotification.stopDrag = drop;
				DropNotification.inDrag = kind;
				target.NGUIMessage(messageName);
				drop = DropNotification.stopDrag;
				return true;
			}
			finally
			{
				DropNotification.scanItem = gameObject;
				DropNotification.stopDrag = flag;
				DropNotification.inDrag = dragEventKind;
			}
			return false;
		}
		return false;
	}

	// Token: 0x06004788 RID: 18312 RVA: 0x00120214 File Offset: 0x0011E414
	internal static bool DropMessage(ref DropNotificationFlags flags, DragEventKind kind, GameObject Pressed, GameObject Released)
	{
		bool result;
		bool flag;
		DropNotificationFlags dropNotificationFlags;
		DropNotificationFlags dropNotificationFlags2;
		DropNotificationFlags dropNotificationFlags3;
		string messageName;
		string messageName2;
		switch (kind)
		{
		case DragEventKind.Drag:
			result = true;
			if (Released)
			{
				flag = true;
				dropNotificationFlags = DropNotificationFlags.DragDrop;
				dropNotificationFlags2 = DropNotificationFlags.DragLand;
				dropNotificationFlags3 = DropNotificationFlags.DragReverse;
				messageName = "OnDrop";
				messageName2 = "OnLand";
			}
			else
			{
				flag = false;
				dropNotificationFlags = (DropNotificationFlags)(-2147483648);
				dropNotificationFlags3 = DropNotificationFlags.DragLandOutside;
				dropNotificationFlags2 = DropNotificationFlags.DragLandOutside;
				messageName = "----";
				messageName2 = "OnLandOutside";
			}
			break;
		case DragEventKind.Alt:
			flag = true;
			result = false;
			dropNotificationFlags = DropNotificationFlags.AltDrop;
			dropNotificationFlags2 = DropNotificationFlags.AltLand;
			dropNotificationFlags3 = DropNotificationFlags.AltReverse;
			messageName = "OnAltDrop";
			messageName2 = "OnAltLand";
			break;
		case DragEventKind.Mid:
			flag = true;
			result = false;
			dropNotificationFlags = DropNotificationFlags.MidDrop;
			dropNotificationFlags2 = DropNotificationFlags.MidLand;
			dropNotificationFlags3 = DropNotificationFlags.MidReverse;
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
					DropNotification.Message(Pressed, Released, messageName2, Pressed, kind, ref result);
				}
				else
				{
					DropNotification.Message(Pressed, messageName2, Pressed, kind, ref result);
				}
			}
			if ((flags & dropNotificationFlags) == dropNotificationFlags)
			{
				if (flag)
				{
					DropNotification.Message(Released, Pressed, messageName, Pressed, kind, ref result);
				}
				else
				{
					DropNotification.Message(Released, messageName, Pressed, kind, ref result);
				}
			}
		}
		else
		{
			if ((flags & dropNotificationFlags) == dropNotificationFlags)
			{
				if (flag)
				{
					DropNotification.Message(Released, Pressed, messageName, Pressed, kind, ref result);
				}
				else
				{
					DropNotification.Message(Released, messageName, Pressed, kind, ref result);
				}
			}
			if ((flags & dropNotificationFlags2) == dropNotificationFlags2)
			{
				if (flag)
				{
					DropNotification.Message(Pressed, Released, messageName2, Pressed, kind, ref result);
				}
				else
				{
					DropNotification.Message(Pressed, messageName2, Pressed, kind, ref result);
				}
			}
		}
		return result;
	}

	// Token: 0x040027A1 RID: 10145
	public const DropNotificationFlags DragDrop = DropNotificationFlags.DragDrop;

	// Token: 0x040027A2 RID: 10146
	public const DropNotificationFlags DragLand = DropNotificationFlags.DragLand;

	// Token: 0x040027A3 RID: 10147
	public const DropNotificationFlags kDragReverseBit = DropNotificationFlags.DragReverse;

	// Token: 0x040027A4 RID: 10148
	public const DropNotificationFlags AltDrop = DropNotificationFlags.AltDrop;

	// Token: 0x040027A5 RID: 10149
	public const DropNotificationFlags AltLand = DropNotificationFlags.AltLand;

	// Token: 0x040027A6 RID: 10150
	public const DropNotificationFlags kAltReverseBit = DropNotificationFlags.AltReverse;

	// Token: 0x040027A7 RID: 10151
	public const DropNotificationFlags MidDrop = DropNotificationFlags.MidDrop;

	// Token: 0x040027A8 RID: 10152
	public const DropNotificationFlags MidLand = DropNotificationFlags.MidLand;

	// Token: 0x040027A9 RID: 10153
	public const DropNotificationFlags kMidReverseBit = DropNotificationFlags.MidReverse;

	// Token: 0x040027AA RID: 10154
	public const DropNotificationFlags DragLandOutside = DropNotificationFlags.DragLandOutside;

	// Token: 0x040027AB RID: 10155
	private const DropNotificationFlags kInvalidNeverSet = (DropNotificationFlags)(-2147483648);

	// Token: 0x040027AC RID: 10156
	public const DropNotificationFlags DragDropThenLand = DropNotificationFlags.DragDrop | DropNotificationFlags.DragLand;

	// Token: 0x040027AD RID: 10157
	public const DropNotificationFlags DragLandThenDrop = DropNotificationFlags.DragDrop | DropNotificationFlags.DragLand | DropNotificationFlags.DragReverse;

	// Token: 0x040027AE RID: 10158
	public const DropNotificationFlags AltDropThenLand = DropNotificationFlags.AltDrop | DropNotificationFlags.AltLand;

	// Token: 0x040027AF RID: 10159
	public const DropNotificationFlags AltLandThenDrop = DropNotificationFlags.AltDrop | DropNotificationFlags.AltLand | DropNotificationFlags.AltReverse;

	// Token: 0x040027B0 RID: 10160
	public const DropNotificationFlags MidDropThenLand = DropNotificationFlags.MidDrop | DropNotificationFlags.MidLand;

	// Token: 0x040027B1 RID: 10161
	public const DropNotificationFlags MidLandThenDrop = DropNotificationFlags.MidDrop | DropNotificationFlags.MidLand | DropNotificationFlags.MidReverse;

	// Token: 0x040027B2 RID: 10162
	public const DropNotificationFlags kDefault = DropNotificationFlags.DragDrop;

	// Token: 0x040027B3 RID: 10163
	public const DropNotificationFlags kMask_Drag = DropNotificationFlags.DragDrop | DropNotificationFlags.DragLand | DropNotificationFlags.DragReverse;

	// Token: 0x040027B4 RID: 10164
	public const DropNotificationFlags kMask_Alt = DropNotificationFlags.AltDrop | DropNotificationFlags.AltLand | DropNotificationFlags.AltReverse;

	// Token: 0x040027B5 RID: 10165
	public const DropNotificationFlags kMask_Mid = DropNotificationFlags.MidDrop | DropNotificationFlags.MidLand | DropNotificationFlags.MidReverse;

	// Token: 0x040027B6 RID: 10166
	public const DropNotificationFlags kMask_Active = DropNotificationFlags.DragDrop | DropNotificationFlags.DragLand | DropNotificationFlags.AltDrop | DropNotificationFlags.AltLand | DropNotificationFlags.MidDrop | DropNotificationFlags.MidLand;

	// Token: 0x040027B7 RID: 10167
	public const DropNotificationFlags Disable = (DropNotificationFlags)0;

	// Token: 0x040027B8 RID: 10168
	private static GameObject scanItem;

	// Token: 0x040027B9 RID: 10169
	private static bool stopDrag;

	// Token: 0x040027BA RID: 10170
	private static DragEventKind inDrag;
}
