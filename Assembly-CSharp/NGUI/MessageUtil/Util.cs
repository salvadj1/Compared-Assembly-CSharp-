using System;
using UnityEngine;

namespace NGUI.MessageUtil
{
	// Token: 0x020007DF RID: 2015
	public static class Util
	{
		// Token: 0x0600482F RID: 18479 RVA: 0x001240D0 File Offset: 0x001222D0
		public static void Select(this GameObject recv, bool selected)
		{
			Util.MSG(recv, "OnSelect", Boxed.Box(selected), true);
		}

		// Token: 0x06004830 RID: 18480 RVA: 0x001240E4 File Offset: 0x001222E4
		public static void Press(this GameObject recv, bool press)
		{
			Util.MSG(recv, "OnPress", Boxed.Box(press), true);
		}

		// Token: 0x06004831 RID: 18481 RVA: 0x001240F8 File Offset: 0x001222F8
		public static void Hover(this GameObject recv, bool highlight)
		{
			Util.MSG(recv, "OnHover", Boxed.Box(highlight), true);
		}

		// Token: 0x06004832 RID: 18482 RVA: 0x0012410C File Offset: 0x0012230C
		public static void Tooltip(this GameObject recv, bool show)
		{
			Util.MSG(recv, "OnTooltip", Boxed.Box(show), true);
		}

		// Token: 0x06004833 RID: 18483 RVA: 0x00124120 File Offset: 0x00122320
		public static void Key(this GameObject recv, KeyCode key)
		{
			Util.MSG(recv, "OnKey", Boxed.Box(key), true);
		}

		// Token: 0x06004834 RID: 18484 RVA: 0x00124134 File Offset: 0x00122334
		public static void Drop(this GameObject recv, GameObject obj)
		{
			Util.MSG(recv, "OnDrop", Boxed.Box<GameObject>(obj), true);
		}

		// Token: 0x06004835 RID: 18485 RVA: 0x00124148 File Offset: 0x00122348
		public static void Drag(this GameObject recv, Vector2 delta)
		{
			Util.MSG(recv, "OnDrag", Boxed.Box<Vector2>(delta), true);
		}

		// Token: 0x06004836 RID: 18486 RVA: 0x0012415C File Offset: 0x0012235C
		public static void Scroll(this GameObject recv, float y)
		{
			Util.MSG(recv, "OnScroll", Boxed.Box<float>(y), true);
		}

		// Token: 0x06004837 RID: 18487 RVA: 0x00124170 File Offset: 0x00122370
		public static void ScrollX(this GameObject recv, float x)
		{
			Util.MSG(recv, "OnScrollX", Boxed.Box<float>(x), true);
		}

		// Token: 0x06004838 RID: 18488 RVA: 0x00124184 File Offset: 0x00122384
		public static void Input(this GameObject recv, string input)
		{
			Util.MSG(recv, "OnInput", Boxed.Box<string>(input), true);
		}

		// Token: 0x06004839 RID: 18489 RVA: 0x00124198 File Offset: 0x00122398
		public static void Click(this GameObject recv)
		{
			Util.MSG(recv, "OnClick", null, false);
		}

		// Token: 0x0600483A RID: 18490 RVA: 0x001241A8 File Offset: 0x001223A8
		public static void DoubleClick(this GameObject recv)
		{
			Util.MSG(recv, "OnDoubleClick", null, false);
		}

		// Token: 0x0600483B RID: 18491 RVA: 0x001241B8 File Offset: 0x001223B8
		public static void DragState(this GameObject recv, bool dragging)
		{
			Util.MSG(recv, "OnDragState", Boxed.Box(dragging), true);
		}

		// Token: 0x0600483C RID: 18492 RVA: 0x001241CC File Offset: 0x001223CC
		public static void AltPress(this GameObject recv, bool press)
		{
			Util.MSG(recv, "OnAltPress", Boxed.Box(press), true);
		}

		// Token: 0x0600483D RID: 18493 RVA: 0x001241E0 File Offset: 0x001223E0
		public static void AltClick(this GameObject recv)
		{
			Util.MSG(recv, "OnAltClick", null, false);
		}

		// Token: 0x0600483E RID: 18494 RVA: 0x001241F0 File Offset: 0x001223F0
		public static void AltDoubleClick(this GameObject recv)
		{
			Util.MSG(recv, "OnAltDoubleClick", null, false);
		}

		// Token: 0x0600483F RID: 18495 RVA: 0x00124200 File Offset: 0x00122400
		public static void MidPress(this GameObject recv, bool press)
		{
			Util.MSG(recv, "OnMidPress", Boxed.Box(press), true);
		}

		// Token: 0x06004840 RID: 18496 RVA: 0x00124214 File Offset: 0x00122414
		public static void MidClick(this GameObject recv)
		{
			Util.MSG(recv, "OnMidClick", null, false);
		}

		// Token: 0x06004841 RID: 18497 RVA: 0x00124224 File Offset: 0x00122424
		public static void MidDoubleClick(this GameObject recv)
		{
			Util.MSG(recv, "OnMidDoubleClick", null, false);
		}

		// Token: 0x06004842 RID: 18498 RVA: 0x00124234 File Offset: 0x00122434
		public static void NGUIMessage(this GameObject recv, string message)
		{
			Util.MSG(recv, message, null, false);
		}

		// Token: 0x06004843 RID: 18499 RVA: 0x00124240 File Offset: 0x00122440
		public static void NGUIMessage(this GameObject recv, string message, bool value)
		{
			Util.MSG(recv, message, Boxed.Box(value), true);
		}

		// Token: 0x06004844 RID: 18500 RVA: 0x00124250 File Offset: 0x00122450
		public static void NGUIMessage(this GameObject recv, string message, int value)
		{
			Util.MSG(recv, message, Boxed.Box(value), true);
		}

		// Token: 0x06004845 RID: 18501 RVA: 0x00124260 File Offset: 0x00122460
		public static void NGUIMessage(this GameObject recv, string message, KeyCode value)
		{
			Util.MSG(recv, message, Boxed.Box(value), true);
		}

		// Token: 0x06004846 RID: 18502 RVA: 0x00124270 File Offset: 0x00122470
		public static void NGUIMessage(this GameObject recv, string message, GameObject value)
		{
			Util.MSG(recv, message, Boxed.Box<GameObject>(value), true);
		}

		// Token: 0x06004847 RID: 18503 RVA: 0x00124280 File Offset: 0x00122480
		public static void NGUIMessage(this GameObject recv, string message, object value)
		{
			Util.MSG(recv, message, value, true);
		}

		// Token: 0x06004848 RID: 18504 RVA: 0x0012428C File Offset: 0x0012248C
		public static void NGUIMessage<T>(this GameObject recv, string message, T value)
		{
			Util.MSG(recv, message, Boxed.Box<T>(value), true);
		}

		// Token: 0x06004849 RID: 18505 RVA: 0x0012429C File Offset: 0x0012249C
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
