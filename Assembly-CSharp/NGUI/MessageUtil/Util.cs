using System;
using UnityEngine;

namespace NGUI.MessageUtil
{
	// Token: 0x020008D0 RID: 2256
	public static class Util
	{
		// Token: 0x06004CDA RID: 19674 RVA: 0x0012E034 File Offset: 0x0012C234
		public static void Select(this GameObject recv, bool selected)
		{
			Util.MSG(recv, "OnSelect", Boxed.Box(selected), true);
		}

		// Token: 0x06004CDB RID: 19675 RVA: 0x0012E048 File Offset: 0x0012C248
		public static void Press(this GameObject recv, bool press)
		{
			Util.MSG(recv, "OnPress", Boxed.Box(press), true);
		}

		// Token: 0x06004CDC RID: 19676 RVA: 0x0012E05C File Offset: 0x0012C25C
		public static void Hover(this GameObject recv, bool highlight)
		{
			Util.MSG(recv, "OnHover", Boxed.Box(highlight), true);
		}

		// Token: 0x06004CDD RID: 19677 RVA: 0x0012E070 File Offset: 0x0012C270
		public static void Tooltip(this GameObject recv, bool show)
		{
			Util.MSG(recv, "OnTooltip", Boxed.Box(show), true);
		}

		// Token: 0x06004CDE RID: 19678 RVA: 0x0012E084 File Offset: 0x0012C284
		public static void Key(this GameObject recv, KeyCode key)
		{
			Util.MSG(recv, "OnKey", Boxed.Box(key), true);
		}

		// Token: 0x06004CDF RID: 19679 RVA: 0x0012E098 File Offset: 0x0012C298
		public static void Drop(this GameObject recv, GameObject obj)
		{
			Util.MSG(recv, "OnDrop", Boxed.Box<GameObject>(obj), true);
		}

		// Token: 0x06004CE0 RID: 19680 RVA: 0x0012E0AC File Offset: 0x0012C2AC
		public static void Drag(this GameObject recv, Vector2 delta)
		{
			Util.MSG(recv, "OnDrag", Boxed.Box<Vector2>(delta), true);
		}

		// Token: 0x06004CE1 RID: 19681 RVA: 0x0012E0C0 File Offset: 0x0012C2C0
		public static void Scroll(this GameObject recv, float y)
		{
			Util.MSG(recv, "OnScroll", Boxed.Box<float>(y), true);
		}

		// Token: 0x06004CE2 RID: 19682 RVA: 0x0012E0D4 File Offset: 0x0012C2D4
		public static void ScrollX(this GameObject recv, float x)
		{
			Util.MSG(recv, "OnScrollX", Boxed.Box<float>(x), true);
		}

		// Token: 0x06004CE3 RID: 19683 RVA: 0x0012E0E8 File Offset: 0x0012C2E8
		public static void Input(this GameObject recv, string input)
		{
			Util.MSG(recv, "OnInput", Boxed.Box<string>(input), true);
		}

		// Token: 0x06004CE4 RID: 19684 RVA: 0x0012E0FC File Offset: 0x0012C2FC
		public static void Click(this GameObject recv)
		{
			Util.MSG(recv, "OnClick", null, false);
		}

		// Token: 0x06004CE5 RID: 19685 RVA: 0x0012E10C File Offset: 0x0012C30C
		public static void DoubleClick(this GameObject recv)
		{
			Util.MSG(recv, "OnDoubleClick", null, false);
		}

		// Token: 0x06004CE6 RID: 19686 RVA: 0x0012E11C File Offset: 0x0012C31C
		public static void DragState(this GameObject recv, bool dragging)
		{
			Util.MSG(recv, "OnDragState", Boxed.Box(dragging), true);
		}

		// Token: 0x06004CE7 RID: 19687 RVA: 0x0012E130 File Offset: 0x0012C330
		public static void AltPress(this GameObject recv, bool press)
		{
			Util.MSG(recv, "OnAltPress", Boxed.Box(press), true);
		}

		// Token: 0x06004CE8 RID: 19688 RVA: 0x0012E144 File Offset: 0x0012C344
		public static void AltClick(this GameObject recv)
		{
			Util.MSG(recv, "OnAltClick", null, false);
		}

		// Token: 0x06004CE9 RID: 19689 RVA: 0x0012E154 File Offset: 0x0012C354
		public static void AltDoubleClick(this GameObject recv)
		{
			Util.MSG(recv, "OnAltDoubleClick", null, false);
		}

		// Token: 0x06004CEA RID: 19690 RVA: 0x0012E164 File Offset: 0x0012C364
		public static void MidPress(this GameObject recv, bool press)
		{
			Util.MSG(recv, "OnMidPress", Boxed.Box(press), true);
		}

		// Token: 0x06004CEB RID: 19691 RVA: 0x0012E178 File Offset: 0x0012C378
		public static void MidClick(this GameObject recv)
		{
			Util.MSG(recv, "OnMidClick", null, false);
		}

		// Token: 0x06004CEC RID: 19692 RVA: 0x0012E188 File Offset: 0x0012C388
		public static void MidDoubleClick(this GameObject recv)
		{
			Util.MSG(recv, "OnMidDoubleClick", null, false);
		}

		// Token: 0x06004CED RID: 19693 RVA: 0x0012E198 File Offset: 0x0012C398
		public static void NGUIMessage(this GameObject recv, string message)
		{
			Util.MSG(recv, message, null, false);
		}

		// Token: 0x06004CEE RID: 19694 RVA: 0x0012E1A4 File Offset: 0x0012C3A4
		public static void NGUIMessage(this GameObject recv, string message, bool value)
		{
			Util.MSG(recv, message, Boxed.Box(value), true);
		}

		// Token: 0x06004CEF RID: 19695 RVA: 0x0012E1B4 File Offset: 0x0012C3B4
		public static void NGUIMessage(this GameObject recv, string message, int value)
		{
			Util.MSG(recv, message, Boxed.Box(value), true);
		}

		// Token: 0x06004CF0 RID: 19696 RVA: 0x0012E1C4 File Offset: 0x0012C3C4
		public static void NGUIMessage(this GameObject recv, string message, KeyCode value)
		{
			Util.MSG(recv, message, Boxed.Box(value), true);
		}

		// Token: 0x06004CF1 RID: 19697 RVA: 0x0012E1D4 File Offset: 0x0012C3D4
		public static void NGUIMessage(this GameObject recv, string message, GameObject value)
		{
			Util.MSG(recv, message, Boxed.Box<GameObject>(value), true);
		}

		// Token: 0x06004CF2 RID: 19698 RVA: 0x0012E1E4 File Offset: 0x0012C3E4
		public static void NGUIMessage(this GameObject recv, string message, object value)
		{
			Util.MSG(recv, message, value, true);
		}

		// Token: 0x06004CF3 RID: 19699 RVA: 0x0012E1F0 File Offset: 0x0012C3F0
		public static void NGUIMessage<T>(this GameObject recv, string message, T value)
		{
			Util.MSG(recv, message, Boxed.Box<T>(value), true);
		}

		// Token: 0x06004CF4 RID: 19700 RVA: 0x0012E200 File Offset: 0x0012C400
		private static void MSG(GameObject recv, string message, object value, bool withValue)
		{
			if (recv)
			{
				if (withValue)
				{
					if (object.ReferenceEquals(value, null))
					{
						Debug.LogWarning(string.Format("((GameObject){2}).SendMessage(\"{0}\", SendMessageOptions.{1}, null ) was not called because of the null argument.", message, 1, recv), recv);
					}
					else
					{
						try
						{
							recv.SendMessage(message, value, 1);
						}
						catch (Exception ex)
						{
							Debug.LogError(string.Format("((GameObject){2}).SendMessage(\"{0}\", {4}({5}), SendMessageOptions.{1}) threw the exception below\r\n{3}", new object[]
							{
								message,
								1,
								recv,
								ex,
								value,
								value.GetType()
							}), recv);
						}
					}
				}
				else
				{
					try
					{
						recv.SendMessage(message, 1);
					}
					catch (Exception ex2)
					{
						Debug.LogError(string.Format("((GameObject){2}).SendMessage(\"{0}\", SendMessageOptions.{1}) threw the exception below\r\n{3}", new object[]
						{
							message,
							1,
							recv,
							ex2
						}), recv);
					}
				}
			}
			else if (!withValue)
			{
				Debug.LogWarning(string.Format("((GameObject)null).SendMessage(\"{0}\", SendMessageOptions.{1})", message, 1));
			}
			else
			{
				Debug.LogWarning(string.Format("((GameObject)null).SendMessage(\"{0}\", {1}, SendMessageOptions.{2})", message, value, 1));
			}
		}
	}
}
