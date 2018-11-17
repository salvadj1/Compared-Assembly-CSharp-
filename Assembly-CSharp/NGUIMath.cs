using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000790 RID: 1936
public static class NGUIMath
{
	// Token: 0x060045DF RID: 17887 RVA: 0x00114EC8 File Offset: 0x001130C8
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

	// Token: 0x060045E0 RID: 17888 RVA: 0x00114F00 File Offset: 0x00113100
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

	// Token: 0x060045E1 RID: 17889 RVA: 0x00114FC4 File Offset: 0x001131C4
	public static int ColorToInt(Color c)
	{
		int num = 0;
		num |= Mathf.RoundToInt(c.r * 255f) << 24;
		num |= Mathf.RoundToInt(c.g * 255f) << 16;
		num |= Mathf.RoundToInt(c.b * 255f) << 8;
		return num | Mathf.RoundToInt(c.a * 255f);
	}

	// Token: 0x060045E2 RID: 17890 RVA: 0x00115030 File Offset: 0x00113230
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

	// Token: 0x060045E3 RID: 17891 RVA: 0x00115098 File Offset: 0x00113298
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

	// Token: 0x060045E4 RID: 17892 RVA: 0x00115108 File Offset: 0x00113308
	public static Color HexToColor(uint val)
	{
		return NGUIMath.IntToColor((int)val);
	}

	// Token: 0x060045E5 RID: 17893 RVA: 0x00115110 File Offset: 0x00113310
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

	// Token: 0x060045E6 RID: 17894 RVA: 0x00115188 File Offset: 0x00113388
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

	// Token: 0x060045E7 RID: 17895 RVA: 0x0011525C File Offset: 0x0011345C
	public static Rect MakePixelPerfect(Rect rect)
	{
		rect.xMin = (float)Mathf.RoundToInt(rect.xMin);
		rect.yMin = (float)Mathf.RoundToInt(rect.yMin);
		rect.xMax = (float)Mathf.RoundToInt(rect.xMax);
		rect.yMax = (float)Mathf.RoundToInt(rect.yMax);
		return rect;
	}

	// Token: 0x060045E8 RID: 17896 RVA: 0x001152BC File Offset: 0x001134BC
	public static Rect MakePixelPerfect(Rect rect, int width, int height)
	{
		rect = NGUIMath.ConvertToPixels(rect, width, height, true);
		rect.xMin = (float)Mathf.RoundToInt(rect.xMin);
		rect.yMin = (float)Mathf.RoundToInt(rect.yMin);
		rect.xMax = (float)Mathf.RoundToInt(rect.xMax);
		rect.yMax = (float)Mathf.RoundToInt(rect.yMax);
		return NGUIMath.ConvertToTexCoords(rect, width, height);
	}

	// Token: 0x060045E9 RID: 17897 RVA: 0x0011532C File Offset: 0x0011352C
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

	// Token: 0x060045EA RID: 17898 RVA: 0x00115380 File Offset: 0x00113580
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

	// Token: 0x060045EB RID: 17899 RVA: 0x0011541C File Offset: 0x0011361C
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

	// Token: 0x060045EC RID: 17900 RVA: 0x0011558C File Offset: 0x0011378C
	public static AABBox CalculateAbsoluteWidgetBounds(Transform trans)
	{
		AABBox result;
		using (NGUIMath.WidgetList widgetsInChildren = NGUIMath.GetWidgetsInChildren(trans))
		{
			if (widgetsInChildren.empty)
			{
				result = default(AABBox);
			}
			else
			{
				AABBox aabbox = default(AABBox);
				bool flag = true;
				foreach (UIWidget uiwidget in widgetsInChildren)
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
					AABBox aabbox2 = new AABBox(ref vector3, ref vector4);
					Matrix4x4 localToWorldMatrix = uiwidget.cachedTransform.localToWorldMatrix;
					AABBox aabbox3;
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
					result = new AABBox(trans.position);
				}
				else
				{
					result = aabbox;
				}
			}
		}
		return result;
	}

	// Token: 0x060045ED RID: 17901 RVA: 0x0011576C File Offset: 0x0011396C
	private static void FillWidgetListWithChildren(Transform trans, ref NGUIMath.WidgetList list, ref bool madeList)
	{
		UIWidget component = trans.GetComponent<UIWidget>();
		if (component)
		{
			if (!madeList)
			{
				list = NGUIMath.WidgetList.Generate();
				madeList = true;
			}
			list.Add(component);
		}
		int childCount = trans.childCount;
		while (childCount-- > 0)
		{
			NGUIMath.FillWidgetListWithChildren(trans.GetChild(childCount), ref list, ref madeList);
		}
	}

	// Token: 0x060045EE RID: 17902 RVA: 0x001157CC File Offset: 0x001139CC
	private static NGUIMath.WidgetList GetWidgetsInChildren(Transform trans)
	{
		if (trans)
		{
			bool flag = false;
			NGUIMath.WidgetList result = null;
			NGUIMath.FillWidgetListWithChildren(trans, ref result, ref flag);
			if (flag)
			{
				return result;
			}
		}
		return NGUIMath.WidgetList.Empty;
	}

	// Token: 0x060045EF RID: 17903 RVA: 0x00115800 File Offset: 0x00113A00
	public static AABBox CalculateRelativeWidgetBounds(Transform root, Transform child)
	{
		AABBox result;
		using (NGUIMath.WidgetList widgetsInChildren = NGUIMath.GetWidgetsInChildren(child))
		{
			if (widgetsInChildren.empty)
			{
				result = default(AABBox);
			}
			else
			{
				bool flag = true;
				AABBox aabbox = default(AABBox);
				Matrix4x4 worldToLocalMatrix = root.worldToLocalMatrix;
				foreach (UIWidget uiwidget in widgetsInChildren)
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
					AABBox aabbox2 = new AABBox(ref vector3, ref vector4);
					AABBox aabbox3;
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

	// Token: 0x060045F0 RID: 17904 RVA: 0x001159D4 File Offset: 0x00113BD4
	public static AABBox CalculateRelativeInnerBounds(Transform root, UISlicedSprite sprite)
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
		AABBox aabbox = new AABBox(ref vector3, ref vector4);
		AABBox result;
		aabbox.TransformedAABB3x4(ref matrix4x, out result);
		return result;
	}

	// Token: 0x060045F1 RID: 17905 RVA: 0x00115B4C File Offset: 0x00113D4C
	public static AABBox CalculateRelativeInnerBounds(Transform root, UISprite sprite)
	{
		if (sprite is UISlicedSprite)
		{
			return NGUIMath.CalculateRelativeInnerBounds(root, sprite as UISlicedSprite);
		}
		return NGUIMath.CalculateRelativeWidgetBounds(root, sprite.cachedTransform);
	}

	// Token: 0x060045F2 RID: 17906 RVA: 0x00115B80 File Offset: 0x00113D80
	public static AABBox CalculateRelativeWidgetBounds(Transform trans)
	{
		return NGUIMath.CalculateRelativeWidgetBounds(trans, trans);
	}

	// Token: 0x060045F3 RID: 17907 RVA: 0x00115B8C File Offset: 0x00113D8C
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

	// Token: 0x060045F4 RID: 17908 RVA: 0x00115C48 File Offset: 0x00113E48
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

	// Token: 0x060045F5 RID: 17909 RVA: 0x00115CB4 File Offset: 0x00113EB4
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

	// Token: 0x060045F6 RID: 17910 RVA: 0x00115D00 File Offset: 0x00113F00
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

	// Token: 0x060045F7 RID: 17911 RVA: 0x00115D40 File Offset: 0x00113F40
	public static Vector2 SpringLerp(Vector2 from, Vector2 to, float strength, float deltaTime)
	{
		return Vector2.Lerp(from, to, NGUIMath.SpringLerp(strength, deltaTime));
	}

	// Token: 0x060045F8 RID: 17912 RVA: 0x00115D50 File Offset: 0x00113F50
	public static Vector3 SpringLerp(Vector3 from, Vector3 to, float strength, float deltaTime)
	{
		return Vector3.Lerp(from, to, NGUIMath.SpringLerp(strength, deltaTime));
	}

	// Token: 0x060045F9 RID: 17913 RVA: 0x00115D60 File Offset: 0x00113F60
	public static Quaternion SpringLerp(Quaternion from, Quaternion to, float strength, float deltaTime)
	{
		return Quaternion.Slerp(from, to, NGUIMath.SpringLerp(strength, deltaTime));
	}

	// Token: 0x060045FA RID: 17914 RVA: 0x00115D70 File Offset: 0x00113F70
	public static float RotateTowards(float from, float to, float maxAngle)
	{
		float num = NGUIMath.WrapAngle(to - from);
		if (Mathf.Abs(num) > maxAngle)
		{
			num = maxAngle * Mathf.Sign(num);
		}
		return from + num;
	}

	// Token: 0x02000791 RID: 1937
	private class WidgetList : List<UIWidget>, IDisposable
	{
		// Token: 0x060045FB RID: 17915 RVA: 0x00115DA0 File Offset: 0x00113FA0
		private WidgetList(bool staticEmpty)
		{
			this.staticEmpty = staticEmpty;
		}

		// Token: 0x17000D94 RID: 3476
		// (get) Token: 0x060045FD RID: 17917 RVA: 0x00115DC8 File Offset: 0x00113FC8
		public bool empty
		{
			get
			{
				return this.staticEmpty;
			}
		}

		// Token: 0x060045FE RID: 17918 RVA: 0x00115DD0 File Offset: 0x00113FD0
		public static NGUIMath.WidgetList Generate()
		{
			if (NGUIMath.WidgetList.tempWidgetListsSize == 0)
			{
				return new NGUIMath.WidgetList(false);
			}
			NGUIMath.WidgetList widgetList = NGUIMath.WidgetList.tempWidgetLists.Dequeue();
			widgetList.disposed = false;
			NGUIMath.WidgetList.tempWidgetListsSize--;
			return widgetList;
		}

		// Token: 0x060045FF RID: 17919 RVA: 0x00115E10 File Offset: 0x00114010
		public new void Add(UIWidget widget)
		{
			if (this.staticEmpty)
			{
				throw new InvalidOperationException();
			}
			base.Add(widget);
		}

		// Token: 0x06004600 RID: 17920 RVA: 0x00115E2C File Offset: 0x0011402C
		public void Dispose()
		{
			if (!this.disposed && !this.staticEmpty)
			{
				this.Clear();
				NGUIMath.WidgetList.tempWidgetLists.Enqueue(this);
				NGUIMath.WidgetList.tempWidgetListsSize++;
				this.disposed = true;
			}
		}

		// Token: 0x04002657 RID: 9815
		private readonly bool staticEmpty;

		// Token: 0x04002658 RID: 9816
		private bool disposed;

		// Token: 0x04002659 RID: 9817
		private static int tempWidgetListsSize;

		// Token: 0x0400265A RID: 9818
		private static Queue<NGUIMath.WidgetList> tempWidgetLists = new Queue<NGUIMath.WidgetList>();

		// Token: 0x0400265B RID: 9819
		public static readonly NGUIMath.WidgetList Empty = new NGUIMath.WidgetList(true);
	}
}
