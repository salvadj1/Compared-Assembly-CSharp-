using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000875 RID: 2165
public static class NGUIMath
{
	// Token: 0x06004A4C RID: 19020 RVA: 0x0011E848 File Offset: 0x0011CA48
	public static float WrapAngle(float angle)
	{
		while (angle > 180f)
		{
			angle -= 360f;
		}
		while (angle < -180f)
		{
			angle += 360f;
		}
		return angle;
	}

	// Token: 0x06004A4D RID: 19021 RVA: 0x0011E880 File Offset: 0x0011CA80
	public static int HexToDecimal(char ch)
	{
		switch (ch)
		{
		case '0':
			return 0;
		case '1':
			return 1;
		case '2':
			return 2;
		case '3':
			return 3;
		case '4':
			return 4;
		case '5':
			return 5;
		case '6':
			return 6;
		case '7':
			return 7;
		case '8':
			return 8;
		case '9':
			return 9;
		default:
			switch (ch)
			{
			case 'a':
				break;
			case 'b':
				return 11;
			case 'c':
				return 12;
			case 'd':
				return 13;
			case 'e':
				return 14;
			case 'f':
				return 15;
			default:
				return 15;
			}
			break;
		case 'A':
			break;
		case 'B':
			return 11;
		case 'C':
			return 12;
		case 'D':
			return 13;
		case 'E':
			return 14;
		case 'F':
			return 15;
		}
		return 10;
	}

	// Token: 0x06004A4E RID: 19022 RVA: 0x0011E944 File Offset: 0x0011CB44
	public static int ColorToInt(Color c)
	{
		int num = 0;
		num |= Mathf.RoundToInt(c.r * 255f) << 24;
		num |= Mathf.RoundToInt(c.g * 255f) << 16;
		num |= Mathf.RoundToInt(c.b * 255f) << 8;
		return num | Mathf.RoundToInt(c.a * 255f);
	}

	// Token: 0x06004A4F RID: 19023 RVA: 0x0011E9B0 File Offset: 0x0011CBB0
	public static Color IntToColor(int val)
	{
		float num = 0.003921569f;
		Color black = Color.black;
		black.r = num * (float)(val >> 24 & 255);
		black.g = num * (float)(val >> 16 & 255);
		black.b = num * (float)(val >> 8 & 255);
		black.a = num * (float)(val & 255);
		return black;
	}

	// Token: 0x06004A50 RID: 19024 RVA: 0x0011EA18 File Offset: 0x0011CC18
	public static string IntToBinary(int val, int bits)
	{
		string text = string.Empty;
		int i = bits;
		while (i > 0)
		{
			if (i == 8 || i == 16 || i == 24)
			{
				text += " ";
			}
			text += (((val & 1 << --i) == 0) ? '0' : '1');
		}
		return text;
	}

	// Token: 0x06004A51 RID: 19025 RVA: 0x0011EA88 File Offset: 0x0011CC88
	public static Color HexToColor(uint val)
	{
		return global::NGUIMath.IntToColor((int)val);
	}

	// Token: 0x06004A52 RID: 19026 RVA: 0x0011EA90 File Offset: 0x0011CC90
	public static Rect ConvertToTexCoords(Rect rect, int width, int height)
	{
		Rect result = rect;
		if ((float)width != 0f && (float)height != 0f)
		{
			result.xMin = rect.xMin / (float)width;
			result.xMax = rect.xMax / (float)width;
			result.yMin = 1f - rect.yMax / (float)height;
			result.yMax = 1f - rect.yMin / (float)height;
		}
		return result;
	}

	// Token: 0x06004A53 RID: 19027 RVA: 0x0011EB08 File Offset: 0x0011CD08
	public static Rect ConvertToPixels(Rect rect, int width, int height, bool round)
	{
		Rect result = rect;
		if (round)
		{
			result.xMin = (float)Mathf.RoundToInt(rect.xMin * (float)width);
			result.xMax = (float)Mathf.RoundToInt(rect.xMax * (float)width);
			result.yMin = (float)Mathf.RoundToInt((1f - rect.yMax) * (float)height);
			result.yMax = (float)Mathf.RoundToInt((1f - rect.yMin) * (float)height);
		}
		else
		{
			result.xMin = rect.xMin * (float)width;
			result.xMax = rect.xMax * (float)width;
			result.yMin = (1f - rect.yMax) * (float)height;
			result.yMax = (1f - rect.yMin) * (float)height;
		}
		return result;
	}

	// Token: 0x06004A54 RID: 19028 RVA: 0x0011EBDC File Offset: 0x0011CDDC
	public static Rect MakePixelPerfect(Rect rect)
	{
		rect.xMin = (float)Mathf.RoundToInt(rect.xMin);
		rect.yMin = (float)Mathf.RoundToInt(rect.yMin);
		rect.xMax = (float)Mathf.RoundToInt(rect.xMax);
		rect.yMax = (float)Mathf.RoundToInt(rect.yMax);
		return rect;
	}

	// Token: 0x06004A55 RID: 19029 RVA: 0x0011EC3C File Offset: 0x0011CE3C
	public static Rect MakePixelPerfect(Rect rect, int width, int height)
	{
		rect = global::NGUIMath.ConvertToPixels(rect, width, height, true);
		rect.xMin = (float)Mathf.RoundToInt(rect.xMin);
		rect.yMin = (float)Mathf.RoundToInt(rect.yMin);
		rect.xMax = (float)Mathf.RoundToInt(rect.xMax);
		rect.yMax = (float)Mathf.RoundToInt(rect.yMax);
		return global::NGUIMath.ConvertToTexCoords(rect, width, height);
	}

	// Token: 0x06004A56 RID: 19030 RVA: 0x0011ECAC File Offset: 0x0011CEAC
	public static Vector3 ApplyHalfPixelOffset(Vector3 pos)
	{
		RuntimePlatform platform = Application.platform;
		if (platform == 2 || platform == 5 || platform == 7)
		{
			pos.x -= 0.5f;
			pos.y += 0.5f;
		}
		return pos;
	}

	// Token: 0x06004A57 RID: 19031 RVA: 0x0011ED00 File Offset: 0x0011CF00
	public static Vector3 ApplyHalfPixelOffset(Vector3 pos, Vector3 scale)
	{
		RuntimePlatform platform = Application.platform;
		if (platform == 2 || platform == 5 || platform == 7)
		{
			if (Mathf.RoundToInt(scale.x) == Mathf.RoundToInt(scale.x * 0.5f) * 2)
			{
				pos.x -= 0.5f;
			}
			if (Mathf.RoundToInt(scale.y) == Mathf.RoundToInt(scale.y * 0.5f) * 2)
			{
				pos.y += 0.5f;
			}
		}
		return pos;
	}

	// Token: 0x06004A58 RID: 19032 RVA: 0x0011ED9C File Offset: 0x0011CF9C
	public static Vector2 ConstrainRect(Vector2 minRect, Vector2 maxRect, Vector2 minArea, Vector2 maxArea)
	{
		Vector2 zero = Vector2.zero;
		float num = maxRect.x - minRect.x;
		float num2 = maxRect.y - minRect.y;
		float num3 = maxArea.x - minArea.x;
		float num4 = maxArea.y - minArea.y;
		if (num > num3)
		{
			float num5 = num - num3;
			minArea.x -= num5;
			maxArea.x += num5;
		}
		if (num2 > num4)
		{
			float num6 = num2 - num4;
			minArea.y -= num6;
			maxArea.y += num6;
		}
		if (minRect.x < minArea.x)
		{
			zero.x += minArea.x - minRect.x;
		}
		if (maxRect.x > maxArea.x)
		{
			zero.x -= maxRect.x - maxArea.x;
		}
		if (minRect.y < minArea.y)
		{
			zero.y += minArea.y - minRect.y;
		}
		if (maxRect.y > maxArea.y)
		{
			zero.y -= maxRect.y - maxArea.y;
		}
		return zero;
	}

	// Token: 0x06004A59 RID: 19033 RVA: 0x0011EF0C File Offset: 0x0011D10C
	public static global::AABBox CalculateAbsoluteWidgetBounds(Transform trans)
	{
		global::AABBox result;
		using (global::NGUIMath.WidgetList widgetsInChildren = global::NGUIMath.GetWidgetsInChildren(trans))
		{
			if (widgetsInChildren.empty)
			{
				result = default(global::AABBox);
			}
			else
			{
				global::AABBox aabbox = default(global::AABBox);
				bool flag = true;
				foreach (global::UIWidget uiwidget in widgetsInChildren)
				{
					Vector2 vector;
					Vector2 vector2;
					uiwidget.GetPivotOffsetAndRelativeSize(out vector, out vector2);
					Vector3 vector3;
					vector3.x = (vector.x + 0.5f) * vector2.x;
					vector3.y = (vector.y - 0.5f) * vector2.y;
					Vector3 vector4;
					vector4.x = vector3.x + vector2.x * 0.5f;
					vector4.y = vector3.y + vector2.y * 0.5f;
					vector3.x -= vector2.x * 0.5f;
					vector3.y -= vector2.y * 0.5f;
					vector3.z = 0f;
					vector4.z = 0f;
					global::AABBox aabbox2 = new global::AABBox(ref vector3, ref vector4);
					Matrix4x4 localToWorldMatrix = uiwidget.cachedTransform.localToWorldMatrix;
					global::AABBox aabbox3;
					aabbox2.TransformedAABB3x4(ref localToWorldMatrix, out aabbox3);
					if (flag)
					{
						aabbox = aabbox3;
						flag = false;
					}
					else
					{
						aabbox.Encapsulate(ref aabbox3);
					}
				}
				if (flag)
				{
					result = new global::AABBox(trans.position);
				}
				else
				{
					result = aabbox;
				}
			}
		}
		return result;
	}

	// Token: 0x06004A5A RID: 19034 RVA: 0x0011F0EC File Offset: 0x0011D2EC
	private static void FillWidgetListWithChildren(Transform trans, ref global::NGUIMath.WidgetList list, ref bool madeList)
	{
		global::UIWidget component = trans.GetComponent<global::UIWidget>();
		if (component)
		{
			if (!madeList)
			{
				list = global::NGUIMath.WidgetList.Generate();
				madeList = true;
			}
			list.Add(component);
		}
		int childCount = trans.childCount;
		while (childCount-- > 0)
		{
			global::NGUIMath.FillWidgetListWithChildren(trans.GetChild(childCount), ref list, ref madeList);
		}
	}

	// Token: 0x06004A5B RID: 19035 RVA: 0x0011F14C File Offset: 0x0011D34C
	private static global::NGUIMath.WidgetList GetWidgetsInChildren(Transform trans)
	{
		if (trans)
		{
			bool flag = false;
			global::NGUIMath.WidgetList result = null;
			global::NGUIMath.FillWidgetListWithChildren(trans, ref result, ref flag);
			if (flag)
			{
				return result;
			}
		}
		return global::NGUIMath.WidgetList.Empty;
	}

	// Token: 0x06004A5C RID: 19036 RVA: 0x0011F180 File Offset: 0x0011D380
	public static global::AABBox CalculateRelativeWidgetBounds(Transform root, Transform child)
	{
		global::AABBox result;
		using (global::NGUIMath.WidgetList widgetsInChildren = global::NGUIMath.GetWidgetsInChildren(child))
		{
			if (widgetsInChildren.empty)
			{
				result = default(global::AABBox);
			}
			else
			{
				bool flag = true;
				global::AABBox aabbox = default(global::AABBox);
				Matrix4x4 worldToLocalMatrix = root.worldToLocalMatrix;
				foreach (global::UIWidget uiwidget in widgetsInChildren)
				{
					Vector2 vector;
					Vector2 vector2;
					uiwidget.GetPivotOffsetAndRelativeSize(out vector, out vector2);
					Vector3 vector3;
					vector3.x = (vector.x + 0.5f) * vector2.x;
					vector3.y = (vector.x - 0.5f) * vector2.y;
					vector3.z = 0f;
					Vector3 vector4;
					vector4.x = vector3.x + vector2.x * 0.5f;
					vector4.y = vector3.y + vector2.y * 0.5f;
					vector4.z = 0f;
					vector3.x -= vector2.x * 0.5f;
					vector3.y -= vector2.y * 0.5f;
					Matrix4x4 matrix4x = worldToLocalMatrix * uiwidget.cachedTransform.localToWorldMatrix;
					global::AABBox aabbox2 = new global::AABBox(ref vector3, ref vector4);
					global::AABBox aabbox3;
					aabbox2.TransformedAABB3x4(ref matrix4x, out aabbox3);
					if (flag)
					{
						aabbox = aabbox3;
						flag = false;
					}
					else
					{
						aabbox.Encapsulate(ref aabbox3);
					}
				}
				result = aabbox;
			}
		}
		return result;
	}

	// Token: 0x06004A5D RID: 19037 RVA: 0x0011F354 File Offset: 0x0011D554
	public static global::AABBox CalculateRelativeInnerBounds(Transform root, global::UISlicedSprite sprite)
	{
		Transform cachedTransform = sprite.cachedTransform;
		Matrix4x4 matrix4x = root.worldToLocalMatrix * cachedTransform.localToWorldMatrix;
		Vector2 vector;
		Vector2 vector2;
		sprite.GetPivotOffsetAndRelativeSize(out vector, out vector2);
		float num = (vector.x + 0.5f) * vector2.x;
		float num2 = (vector.y - 0.5f) * vector2.y;
		vector2 *= 0.5f;
		Vector3 localScale = cachedTransform.localScale;
		float x = localScale.x;
		float y = localScale.y;
		Vector4 border = sprite.border;
		if (x != 0f)
		{
			border.x /= x;
			border.z /= x;
		}
		if (y != 0f)
		{
			border.y /= y;
			border.w /= y;
		}
		Vector3 vector3;
		vector3.x = num - vector2.x + border.x;
		Vector3 vector4;
		vector4.x = num + vector2.x - border.z;
		vector3.y = num2 - vector2.y + border.y;
		vector4.y = num2 + vector2.y - border.w;
		vector3.z = (vector4.z = 0f);
		global::AABBox aabbox = new global::AABBox(ref vector3, ref vector4);
		global::AABBox result;
		aabbox.TransformedAABB3x4(ref matrix4x, out result);
		return result;
	}

	// Token: 0x06004A5E RID: 19038 RVA: 0x0011F4CC File Offset: 0x0011D6CC
	public static global::AABBox CalculateRelativeInnerBounds(Transform root, global::UISprite sprite)
	{
		if (sprite is global::UISlicedSprite)
		{
			return global::NGUIMath.CalculateRelativeInnerBounds(root, sprite as global::UISlicedSprite);
		}
		return global::NGUIMath.CalculateRelativeWidgetBounds(root, sprite.cachedTransform);
	}

	// Token: 0x06004A5F RID: 19039 RVA: 0x0011F500 File Offset: 0x0011D700
	public static global::AABBox CalculateRelativeWidgetBounds(Transform trans)
	{
		return global::NGUIMath.CalculateRelativeWidgetBounds(trans, trans);
	}

	// Token: 0x06004A60 RID: 19040 RVA: 0x0011F50C File Offset: 0x0011D70C
	public static Vector3 SpringDampen(ref Vector3 velocity, float strength, float deltaTime)
	{
		if (Mathf.Approximately(velocity.x, 0f) && Mathf.Approximately(velocity.y, 0f) && Mathf.Approximately(velocity.z, 0f))
		{
			velocity = Vector3.zero;
			return Vector3.zero;
		}
		float num = 1f - strength * 0.001f;
		int num2 = Mathf.RoundToInt(deltaTime * 1000f);
		Vector3 vector = Vector3.zero;
		for (int i = 0; i < num2; i++)
		{
			vector += velocity * 0.06f;
			velocity *= num;
		}
		return vector;
	}

	// Token: 0x06004A61 RID: 19041 RVA: 0x0011F5C8 File Offset: 0x0011D7C8
	public static Vector2 SpringDampen(ref Vector2 velocity, float strength, float deltaTime)
	{
		float num = 1f - strength * 0.001f;
		int num2 = Mathf.RoundToInt(deltaTime * 1000f);
		Vector2 vector = Vector2.zero;
		for (int i = 0; i < num2; i++)
		{
			vector += velocity * 0.06f;
			velocity *= num;
		}
		return vector;
	}

	// Token: 0x06004A62 RID: 19042 RVA: 0x0011F634 File Offset: 0x0011D834
	public static float SpringLerp(float strength, float deltaTime)
	{
		int num = Mathf.RoundToInt(deltaTime * 1000f);
		deltaTime = 0.001f * strength;
		float num2 = 0f;
		for (int i = 0; i < num; i++)
		{
			num2 = Mathf.Lerp(num2, 1f, deltaTime);
		}
		return num2;
	}

	// Token: 0x06004A63 RID: 19043 RVA: 0x0011F680 File Offset: 0x0011D880
	public static float SpringLerp(float from, float to, float strength, float deltaTime)
	{
		int num = Mathf.RoundToInt(deltaTime * 1000f);
		deltaTime = 0.001f * strength;
		for (int i = 0; i < num; i++)
		{
			from = Mathf.Lerp(from, to, deltaTime);
		}
		return from;
	}

	// Token: 0x06004A64 RID: 19044 RVA: 0x0011F6C0 File Offset: 0x0011D8C0
	public static Vector2 SpringLerp(Vector2 from, Vector2 to, float strength, float deltaTime)
	{
		return Vector2.Lerp(from, to, global::NGUIMath.SpringLerp(strength, deltaTime));
	}

	// Token: 0x06004A65 RID: 19045 RVA: 0x0011F6D0 File Offset: 0x0011D8D0
	public static Vector3 SpringLerp(Vector3 from, Vector3 to, float strength, float deltaTime)
	{
		return Vector3.Lerp(from, to, global::NGUIMath.SpringLerp(strength, deltaTime));
	}

	// Token: 0x06004A66 RID: 19046 RVA: 0x0011F6E0 File Offset: 0x0011D8E0
	public static Quaternion SpringLerp(Quaternion from, Quaternion to, float strength, float deltaTime)
	{
		return Quaternion.Slerp(from, to, global::NGUIMath.SpringLerp(strength, deltaTime));
	}

	// Token: 0x06004A67 RID: 19047 RVA: 0x0011F6F0 File Offset: 0x0011D8F0
	public static float RotateTowards(float from, float to, float maxAngle)
	{
		float num = global::NGUIMath.WrapAngle(to - from);
		if (Mathf.Abs(num) > maxAngle)
		{
			num = maxAngle * Mathf.Sign(num);
		}
		return from + num;
	}

	// Token: 0x02000876 RID: 2166
	private class WidgetList : List<global::UIWidget>, IDisposable
	{
		// Token: 0x06004A68 RID: 19048 RVA: 0x0011F720 File Offset: 0x0011D920
		private WidgetList(bool staticEmpty)
		{
			this.staticEmpty = staticEmpty;
		}

		// Token: 0x17000E24 RID: 3620
		// (get) Token: 0x06004A6A RID: 19050 RVA: 0x0011F748 File Offset: 0x0011D948
		public bool empty
		{
			get
			{
				return this.staticEmpty;
			}
		}

		// Token: 0x06004A6B RID: 19051 RVA: 0x0011F750 File Offset: 0x0011D950
		public static global::NGUIMath.WidgetList Generate()
		{
			if (global::NGUIMath.WidgetList.tempWidgetListsSize == 0)
			{
				return new global::NGUIMath.WidgetList(false);
			}
			global::NGUIMath.WidgetList widgetList = global::NGUIMath.WidgetList.tempWidgetLists.Dequeue();
			widgetList.disposed = false;
			global::NGUIMath.WidgetList.tempWidgetListsSize--;
			return widgetList;
		}

		// Token: 0x06004A6C RID: 19052 RVA: 0x0011F790 File Offset: 0x0011D990
		public new void Add(global::UIWidget widget)
		{
			if (this.staticEmpty)
			{
				throw new InvalidOperationException();
			}
			base.Add(widget);
		}

		// Token: 0x06004A6D RID: 19053 RVA: 0x0011F7AC File Offset: 0x0011D9AC
		public void Dispose()
		{
			if (!this.disposed && !this.staticEmpty)
			{
				this.Clear();
				global::NGUIMath.WidgetList.tempWidgetLists.Enqueue(this);
				global::NGUIMath.WidgetList.tempWidgetListsSize++;
				this.disposed = true;
			}
		}

		// Token: 0x0400288E RID: 10382
		private readonly bool staticEmpty;

		// Token: 0x0400288F RID: 10383
		private bool disposed;

		// Token: 0x04002890 RID: 10384
		private static int tempWidgetListsSize;

		// Token: 0x04002891 RID: 10385
		private static Queue<global::NGUIMath.WidgetList> tempWidgetLists = new Queue<global::NGUIMath.WidgetList>();

		// Token: 0x04002892 RID: 10386
		public static readonly global::NGUIMath.WidgetList Empty = new global::NGUIMath.WidgetList(true);
	}
}
