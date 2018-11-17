using System;
using UnityEngine;

// Token: 0x020008A9 RID: 2217
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Anchor")]
public class UIAnchor : MonoBehaviour
{
	// Token: 0x17000E59 RID: 3673
	// (get) Token: 0x06004BEE RID: 19438 RVA: 0x00128AA8 File Offset: 0x00126CA8
	protected Transform mTrans
	{
		get
		{
			if (!this.mTransGot)
			{
				this.__mTrans = base.transform;
				this.mTransGot = true;
			}
			return this.__mTrans;
		}
	}

	// Token: 0x06004BEF RID: 19439 RVA: 0x00128ADC File Offset: 0x00126CDC
	private void OnEnable()
	{
		if (!this.uiCamera)
		{
			this.uiCamera = global::NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
	}

	// Token: 0x06004BF0 RID: 19440 RVA: 0x00128B10 File Offset: 0x00126D10
	public static void ScreenOrigin(global::UIAnchor.Side side, float xMin, float xMax, float yMin, float yMax, out float x, out float y)
	{
		switch (side)
		{
		case global::UIAnchor.Side.BottomLeft:
			x = xMin;
			y = yMin;
			break;
		case global::UIAnchor.Side.Left:
			x = xMin;
			y = (yMin + yMax) / 2f;
			break;
		case global::UIAnchor.Side.TopLeft:
			x = xMin;
			y = yMax;
			break;
		case global::UIAnchor.Side.Top:
			x = (xMin + xMax) / 2f;
			y = yMax;
			break;
		case global::UIAnchor.Side.TopRight:
			x = xMax;
			y = yMax;
			break;
		case global::UIAnchor.Side.Right:
			x = xMax;
			y = (yMin + yMax) / 2f;
			break;
		case global::UIAnchor.Side.BottomRight:
			x = xMax;
			y = yMin;
			break;
		case global::UIAnchor.Side.Bottom:
			x = (xMin + xMax) / 2f;
			y = yMin;
			break;
		case global::UIAnchor.Side.Center:
			x = (xMin + xMax) / 2f;
			y = (yMin + yMax) / 2f;
			break;
		default:
			throw new ArgumentOutOfRangeException();
		}
	}

	// Token: 0x06004BF1 RID: 19441 RVA: 0x00128C00 File Offset: 0x00126E00
	public static void ScreenOrigin(global::UIAnchor.Side side, float xMin, float xMax, float yMin, float yMax, float relativeOffsetX, float relativeOffsetY, out float x, out float y)
	{
		float num;
		float num2;
		global::UIAnchor.ScreenOrigin(side, xMin, xMax, yMin, yMax, out num, out num2);
		x = num + relativeOffsetX * (xMax - xMin);
		y = num2 + relativeOffsetY * (yMax - yMin);
	}

	// Token: 0x06004BF2 RID: 19442 RVA: 0x00128C34 File Offset: 0x00126E34
	public static void ScreenOrigin(global::UIAnchor.Side side, float xMin, float xMax, float yMin, float yMax, float relativeOffsetX, float relativeOffsetY, global::UIAnchor.Flags flags, out float x, out float y)
	{
		switch ((byte)(flags & (global::UIAnchor.Flags.CameraIsOrthographic | global::UIAnchor.Flags.HalfPixelOffset)))
		{
		case 1:
		{
			float num;
			float num2;
			global::UIAnchor.ScreenOrigin(side, xMin, xMax, yMin, yMax, relativeOffsetX, relativeOffsetY, out num, out num2);
			x = Mathf.Round(num);
			y = Mathf.Round(num2);
			return;
		}
		case 3:
		{
			float num3;
			float num4;
			global::UIAnchor.ScreenOrigin(side, xMin, xMax, yMin, yMax, relativeOffsetX, relativeOffsetY, out num3, out num4);
			x = Mathf.Round(num3) - 0.5f;
			y = Mathf.Round(num4) + 0.5f;
			return;
		}
		}
		global::UIAnchor.ScreenOrigin(side, xMin, xMax, yMin, yMax, relativeOffsetX, relativeOffsetY, out x, out y);
	}

	// Token: 0x06004BF3 RID: 19443 RVA: 0x00128CDC File Offset: 0x00126EDC
	public static void ScreenOrigin(global::UIAnchor.Side side, float xMin, float xMax, float yMin, float yMax, global::UIAnchor.Flags flags, out float x, out float y)
	{
		switch ((byte)(flags & ((!global::UIAnchor.Info.isWindows) ? global::UIAnchor.Flags.CameraIsOrthographic : (global::UIAnchor.Flags.CameraIsOrthographic | global::UIAnchor.Flags.HalfPixelOffset))))
		{
		case 1:
		{
			float num;
			float num2;
			global::UIAnchor.ScreenOrigin(side, xMin, xMax, yMin, yMax, out num, out num2);
			x = Mathf.Round(num);
			y = Mathf.Round(num2);
			return;
		}
		case 3:
		{
			float num3;
			float num4;
			global::UIAnchor.ScreenOrigin(side, xMin, xMax, yMin, yMax, out num3, out num4);
			x = Mathf.Round(num3) - 0.5f;
			y = Mathf.Round(num4) + 0.5f;
			return;
		}
		}
		global::UIAnchor.ScreenOrigin(side, xMin, xMax, yMin, yMax, out x, out y);
	}

	// Token: 0x06004BF4 RID: 19444 RVA: 0x00128D88 File Offset: 0x00126F88
	public static Vector3 WorldOrigin(Camera camera, global::UIAnchor.Side side, float depthOffset, float relativeOffsetX, float relativeOffsetY, bool halfPixel)
	{
		Vector3 vector;
		vector.z = depthOffset;
		Rect pixelRect = camera.pixelRect;
		float xMin = pixelRect.xMin;
		float xMax = pixelRect.xMax;
		float yMin = pixelRect.yMin;
		float yMax = pixelRect.yMax;
		global::UIAnchor.ScreenOrigin(side, xMin, xMax, yMin, yMax, relativeOffsetX, relativeOffsetY, (!camera.isOrthoGraphic) ? ((!halfPixel) ? ((global::UIAnchor.Flags)0) : global::UIAnchor.Flags.HalfPixelOffset) : ((!halfPixel) ? global::UIAnchor.Flags.CameraIsOrthographic : (global::UIAnchor.Flags.CameraIsOrthographic | global::UIAnchor.Flags.HalfPixelOffset)), out vector.x, out vector.y);
		return camera.ScreenToWorldPoint(vector);
	}

	// Token: 0x06004BF5 RID: 19445 RVA: 0x00128E18 File Offset: 0x00127018
	public static Vector3 WorldOrigin(Camera camera, global::UIAnchor.Side side, float relativeOffsetX, float relativeOffsetY, bool halfPixel)
	{
		Vector3 vector;
		vector.z = 0f;
		Rect pixelRect = camera.pixelRect;
		float xMin = pixelRect.xMin;
		float xMax = pixelRect.xMax;
		float yMin = pixelRect.yMin;
		float yMax = pixelRect.yMax;
		global::UIAnchor.ScreenOrigin(side, xMin, xMax, yMin, yMax, relativeOffsetX, relativeOffsetY, (!camera.isOrthoGraphic) ? ((!halfPixel) ? ((global::UIAnchor.Flags)0) : global::UIAnchor.Flags.HalfPixelOffset) : ((!halfPixel) ? global::UIAnchor.Flags.CameraIsOrthographic : (global::UIAnchor.Flags.CameraIsOrthographic | global::UIAnchor.Flags.HalfPixelOffset)), out vector.x, out vector.y);
		return camera.ScreenToWorldPoint(vector);
	}

	// Token: 0x06004BF6 RID: 19446 RVA: 0x00128EA8 File Offset: 0x001270A8
	public static Vector3 WorldOrigin(Camera camera, global::UIAnchor.Side side, float depthOffset, bool halfPixel)
	{
		Vector3 vector;
		vector.z = depthOffset;
		Rect pixelRect = camera.pixelRect;
		float xMin = pixelRect.xMin;
		float xMax = pixelRect.xMax;
		float yMin = pixelRect.yMin;
		float yMax = pixelRect.yMax;
		global::UIAnchor.ScreenOrigin(side, xMin, xMax, yMin, yMax, (!camera.isOrthoGraphic) ? ((!halfPixel) ? ((global::UIAnchor.Flags)0) : global::UIAnchor.Flags.HalfPixelOffset) : ((!halfPixel) ? global::UIAnchor.Flags.CameraIsOrthographic : (global::UIAnchor.Flags.CameraIsOrthographic | global::UIAnchor.Flags.HalfPixelOffset)), out vector.x, out vector.y);
		return camera.ScreenToWorldPoint(vector);
	}

	// Token: 0x06004BF7 RID: 19447 RVA: 0x00128F30 File Offset: 0x00127130
	public static Vector3 WorldOrigin(Camera camera, global::UIAnchor.Side side, bool halfPixel)
	{
		Vector3 vector;
		vector.z = 0f;
		Rect pixelRect = camera.pixelRect;
		float xMin = pixelRect.xMin;
		float xMax = pixelRect.xMax;
		float yMin = pixelRect.yMin;
		float yMax = pixelRect.yMax;
		global::UIAnchor.ScreenOrigin(side, xMin, xMax, yMin, yMax, (!camera.isOrthoGraphic) ? ((!halfPixel) ? ((global::UIAnchor.Flags)0) : global::UIAnchor.Flags.HalfPixelOffset) : ((!halfPixel) ? global::UIAnchor.Flags.CameraIsOrthographic : (global::UIAnchor.Flags.CameraIsOrthographic | global::UIAnchor.Flags.HalfPixelOffset)), out vector.x, out vector.y);
		return camera.ScreenToWorldPoint(vector);
	}

	// Token: 0x06004BF8 RID: 19448 RVA: 0x00128FBC File Offset: 0x001271BC
	public static Vector3 WorldOrigin(Camera camera, global::UIAnchor.Side side, RectOffset offset, float depthOffset, float relativeOffsetX, float relativeOffsetY, bool halfPixel)
	{
		Vector3 vector;
		vector.z = depthOffset;
		Rect rect = offset.Add(camera.pixelRect);
		float xMin = rect.xMin;
		float xMax = rect.xMax;
		float yMin = rect.yMin;
		float yMax = rect.yMax;
		global::UIAnchor.ScreenOrigin(side, xMin, xMax, yMin, yMax, relativeOffsetX, relativeOffsetY, (!camera.isOrthoGraphic) ? ((!halfPixel) ? ((global::UIAnchor.Flags)0) : global::UIAnchor.Flags.HalfPixelOffset) : ((!halfPixel) ? global::UIAnchor.Flags.CameraIsOrthographic : (global::UIAnchor.Flags.CameraIsOrthographic | global::UIAnchor.Flags.HalfPixelOffset)), out vector.x, out vector.y);
		return camera.ScreenToWorldPoint(vector);
	}

	// Token: 0x06004BF9 RID: 19449 RVA: 0x00129050 File Offset: 0x00127250
	public static Vector3 WorldOrigin(Camera camera, global::UIAnchor.Side side, RectOffset offset, float relativeOffsetX, float relativeOffsetY, bool halfPixel)
	{
		Vector3 vector;
		vector.z = 0f;
		Rect rect = offset.Add(camera.pixelRect);
		float xMin = rect.xMin;
		float xMax = rect.xMax;
		float yMin = rect.yMin;
		float yMax = rect.yMax;
		global::UIAnchor.ScreenOrigin(side, xMin, xMax, yMin, yMax, relativeOffsetX, relativeOffsetY, (!camera.isOrthoGraphic) ? ((!halfPixel) ? ((global::UIAnchor.Flags)0) : global::UIAnchor.Flags.HalfPixelOffset) : ((!halfPixel) ? global::UIAnchor.Flags.CameraIsOrthographic : (global::UIAnchor.Flags.CameraIsOrthographic | global::UIAnchor.Flags.HalfPixelOffset)), out vector.x, out vector.y);
		return camera.ScreenToWorldPoint(vector);
	}

	// Token: 0x06004BFA RID: 19450 RVA: 0x001290E8 File Offset: 0x001272E8
	public static Vector3 WorldOrigin(Camera camera, global::UIAnchor.Side side, RectOffset offset, float depthOffset, bool halfPixel)
	{
		Vector3 vector;
		vector.z = depthOffset;
		Rect rect = offset.Add(camera.pixelRect);
		float xMin = rect.xMin;
		float xMax = rect.xMax;
		float yMin = rect.yMin;
		float yMax = rect.yMax;
		global::UIAnchor.ScreenOrigin(side, xMin, xMax, yMin, yMax, (!camera.isOrthoGraphic) ? ((!halfPixel) ? ((global::UIAnchor.Flags)0) : global::UIAnchor.Flags.HalfPixelOffset) : ((!halfPixel) ? global::UIAnchor.Flags.CameraIsOrthographic : (global::UIAnchor.Flags.CameraIsOrthographic | global::UIAnchor.Flags.HalfPixelOffset)), out vector.x, out vector.y);
		return camera.ScreenToWorldPoint(vector);
	}

	// Token: 0x06004BFB RID: 19451 RVA: 0x00129178 File Offset: 0x00127378
	public static Vector3 WorldOrigin(Camera camera, global::UIAnchor.Side side, RectOffset offset, bool halfPixel)
	{
		Vector3 vector;
		vector.z = 0f;
		Rect rect = offset.Add(camera.pixelRect);
		float xMin = rect.xMin;
		float xMax = rect.xMax;
		float yMin = rect.yMin;
		float yMax = rect.yMax;
		global::UIAnchor.ScreenOrigin(side, xMin, xMax, yMin, yMax, (!camera.isOrthoGraphic) ? ((!halfPixel) ? ((global::UIAnchor.Flags)0) : global::UIAnchor.Flags.HalfPixelOffset) : ((!halfPixel) ? global::UIAnchor.Flags.CameraIsOrthographic : (global::UIAnchor.Flags.CameraIsOrthographic | global::UIAnchor.Flags.HalfPixelOffset)), out vector.x, out vector.y);
		return camera.ScreenToWorldPoint(vector);
	}

	// Token: 0x06004BFC RID: 19452 RVA: 0x0012920C File Offset: 0x0012740C
	protected void SetPosition(ref Vector3 newPosition)
	{
		Transform mTrans = this.mTrans;
		if (this.otherThingsMightMoveThis || !this.mOnce)
		{
			this.mLastPosition = mTrans.position;
			this.mOnce = true;
		}
		if (newPosition.x != this.mLastPosition.x || newPosition.y != this.mLastPosition.y || newPosition.z != this.mLastPosition.z)
		{
			mTrans.position = newPosition;
		}
	}

	// Token: 0x06004BFD RID: 19453 RVA: 0x00129298 File Offset: 0x00127498
	protected void Update()
	{
		if (this.uiCamera)
		{
			Vector3 vector = global::UIAnchor.WorldOrigin(this.uiCamera, this.side, this.depthOffset, this.relativeOffset.x, this.relativeOffset.y, this.halfPixelOffset);
			this.SetPosition(ref vector);
		}
	}

	// Token: 0x0400299F RID: 10655
	public Camera uiCamera;

	// Token: 0x040029A0 RID: 10656
	public global::UIAnchor.Side side = global::UIAnchor.Side.Center;

	// Token: 0x040029A1 RID: 10657
	public bool halfPixelOffset = true;

	// Token: 0x040029A2 RID: 10658
	public bool otherThingsMightMoveThis;

	// Token: 0x040029A3 RID: 10659
	public float depthOffset;

	// Token: 0x040029A4 RID: 10660
	public Vector2 relativeOffset = Vector2.zero;

	// Token: 0x040029A5 RID: 10661
	[NonSerialized]
	private Transform __mTrans;

	// Token: 0x040029A6 RID: 10662
	[NonSerialized]
	private bool mTransGot;

	// Token: 0x040029A7 RID: 10663
	[NonSerialized]
	private bool mOnce;

	// Token: 0x040029A8 RID: 10664
	[NonSerialized]
	private Vector3 mLastPosition;

	// Token: 0x020008AA RID: 2218
	public enum Side
	{
		// Token: 0x040029AA RID: 10666
		BottomLeft,
		// Token: 0x040029AB RID: 10667
		Left,
		// Token: 0x040029AC RID: 10668
		TopLeft,
		// Token: 0x040029AD RID: 10669
		Top,
		// Token: 0x040029AE RID: 10670
		TopRight,
		// Token: 0x040029AF RID: 10671
		Right,
		// Token: 0x040029B0 RID: 10672
		BottomRight,
		// Token: 0x040029B1 RID: 10673
		Bottom,
		// Token: 0x040029B2 RID: 10674
		Center
	}

	// Token: 0x020008AB RID: 2219
	protected static class Info
	{
		// Token: 0x06004BFE RID: 19454 RVA: 0x001292F4 File Offset: 0x001274F4
		static Info()
		{
			RuntimePlatform platform = Application.platform;
			global::UIAnchor.Info.isWindows = (platform == 2 || platform == 5 || platform == 7);
		}

		// Token: 0x040029B3 RID: 10675
		public static readonly bool isWindows;
	}

	// Token: 0x020008AC RID: 2220
	[Flags]
	public enum Flags : byte
	{
		// Token: 0x040029B5 RID: 10677
		CameraIsOrthographic = 1,
		// Token: 0x040029B6 RID: 10678
		HalfPixelOffset = 2
	}
}
