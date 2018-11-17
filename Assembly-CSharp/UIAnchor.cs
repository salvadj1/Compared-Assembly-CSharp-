using System;
using UnityEngine;

// Token: 0x020007BC RID: 1980
[AddComponentMenu("NGUI/UI/Anchor")]
[ExecuteInEditMode]
public class UIAnchor : MonoBehaviour
{
	// Token: 0x17000DC7 RID: 3527
	// (get) Token: 0x0600475F RID: 18271 RVA: 0x0011F084 File Offset: 0x0011D284
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

	// Token: 0x06004760 RID: 18272 RVA: 0x0011F0B8 File Offset: 0x0011D2B8
	private void OnEnable()
	{
		if (!this.uiCamera)
		{
			this.uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
	}

	// Token: 0x06004761 RID: 18273 RVA: 0x0011F0EC File Offset: 0x0011D2EC
	public static void ScreenOrigin(UIAnchor.Side side, float xMin, float xMax, float yMin, float yMax, out float x, out float y)
	{
		switch (side)
		{
		case UIAnchor.Side.BottomLeft:
			x = xMin;
			y = yMin;
			break;
		case UIAnchor.Side.Left:
			x = xMin;
			y = (yMin + yMax) / 2f;
			break;
		case UIAnchor.Side.TopLeft:
			x = xMin;
			y = yMax;
			break;
		case UIAnchor.Side.Top:
			x = (xMin + xMax) / 2f;
			y = yMax;
			break;
		case UIAnchor.Side.TopRight:
			x = xMax;
			y = yMax;
			break;
		case UIAnchor.Side.Right:
			x = xMax;
			y = (yMin + yMax) / 2f;
			break;
		case UIAnchor.Side.BottomRight:
			x = xMax;
			y = yMin;
			break;
		case UIAnchor.Side.Bottom:
			x = (xMin + xMax) / 2f;
			y = yMin;
			break;
		case UIAnchor.Side.Center:
			x = (xMin + xMax) / 2f;
			y = (yMin + yMax) / 2f;
			break;
		default:
			throw new ArgumentOutOfRangeException();
		}
	}

	// Token: 0x06004762 RID: 18274 RVA: 0x0011F1DC File Offset: 0x0011D3DC
	public static void ScreenOrigin(UIAnchor.Side side, float xMin, float xMax, float yMin, float yMax, float relativeOffsetX, float relativeOffsetY, out float x, out float y)
	{
		float num;
		float num2;
		UIAnchor.ScreenOrigin(side, xMin, xMax, yMin, yMax, out num, out num2);
		x = num + relativeOffsetX * (xMax - xMin);
		y = num2 + relativeOffsetY * (yMax - yMin);
	}

	// Token: 0x06004763 RID: 18275 RVA: 0x0011F210 File Offset: 0x0011D410
	public static void ScreenOrigin(UIAnchor.Side side, float xMin, float xMax, float yMin, float yMax, float relativeOffsetX, float relativeOffsetY, UIAnchor.Flags flags, out float x, out float y)
	{
		switch ((byte)(flags & (UIAnchor.Flags.CameraIsOrthographic | UIAnchor.Flags.HalfPixelOffset)))
		{
		case 1:
		{
			float num;
			float num2;
			UIAnchor.ScreenOrigin(side, xMin, xMax, yMin, yMax, relativeOffsetX, relativeOffsetY, out num, out num2);
			x = Mathf.Round(num);
			y = Mathf.Round(num2);
			return;
		}
		case 3:
		{
			float num3;
			float num4;
			UIAnchor.ScreenOrigin(side, xMin, xMax, yMin, yMax, relativeOffsetX, relativeOffsetY, out num3, out num4);
			x = Mathf.Round(num3) - 0.5f;
			y = Mathf.Round(num4) + 0.5f;
			return;
		}
		}
		UIAnchor.ScreenOrigin(side, xMin, xMax, yMin, yMax, relativeOffsetX, relativeOffsetY, out x, out y);
	}

	// Token: 0x06004764 RID: 18276 RVA: 0x0011F2B8 File Offset: 0x0011D4B8
	public static void ScreenOrigin(UIAnchor.Side side, float xMin, float xMax, float yMin, float yMax, UIAnchor.Flags flags, out float x, out float y)
	{
		switch ((byte)(flags & ((!UIAnchor.Info.isWindows) ? UIAnchor.Flags.CameraIsOrthographic : (UIAnchor.Flags.CameraIsOrthographic | UIAnchor.Flags.HalfPixelOffset))))
		{
		case 1:
		{
			float num;
			float num2;
			UIAnchor.ScreenOrigin(side, xMin, xMax, yMin, yMax, out num, out num2);
			x = Mathf.Round(num);
			y = Mathf.Round(num2);
			return;
		}
		case 3:
		{
			float num3;
			float num4;
			UIAnchor.ScreenOrigin(side, xMin, xMax, yMin, yMax, out num3, out num4);
			x = Mathf.Round(num3) - 0.5f;
			y = Mathf.Round(num4) + 0.5f;
			return;
		}
		}
		UIAnchor.ScreenOrigin(side, xMin, xMax, yMin, yMax, out x, out y);
	}

	// Token: 0x06004765 RID: 18277 RVA: 0x0011F364 File Offset: 0x0011D564
	public static Vector3 WorldOrigin(Camera camera, UIAnchor.Side side, float depthOffset, float relativeOffsetX, float relativeOffsetY, bool halfPixel)
	{
		Vector3 vector;
		vector.z = depthOffset;
		Rect pixelRect = camera.pixelRect;
		float xMin = pixelRect.xMin;
		float xMax = pixelRect.xMax;
		float yMin = pixelRect.yMin;
		float yMax = pixelRect.yMax;
		UIAnchor.ScreenOrigin(side, xMin, xMax, yMin, yMax, relativeOffsetX, relativeOffsetY, (!camera.isOrthoGraphic) ? ((!halfPixel) ? ((UIAnchor.Flags)0) : UIAnchor.Flags.HalfPixelOffset) : ((!halfPixel) ? UIAnchor.Flags.CameraIsOrthographic : (UIAnchor.Flags.CameraIsOrthographic | UIAnchor.Flags.HalfPixelOffset)), out vector.x, out vector.y);
		return camera.ScreenToWorldPoint(vector);
	}

	// Token: 0x06004766 RID: 18278 RVA: 0x0011F3F4 File Offset: 0x0011D5F4
	public static Vector3 WorldOrigin(Camera camera, UIAnchor.Side side, float relativeOffsetX, float relativeOffsetY, bool halfPixel)
	{
		Vector3 vector;
		vector.z = 0f;
		Rect pixelRect = camera.pixelRect;
		float xMin = pixelRect.xMin;
		float xMax = pixelRect.xMax;
		float yMin = pixelRect.yMin;
		float yMax = pixelRect.yMax;
		UIAnchor.ScreenOrigin(side, xMin, xMax, yMin, yMax, relativeOffsetX, relativeOffsetY, (!camera.isOrthoGraphic) ? ((!halfPixel) ? ((UIAnchor.Flags)0) : UIAnchor.Flags.HalfPixelOffset) : ((!halfPixel) ? UIAnchor.Flags.CameraIsOrthographic : (UIAnchor.Flags.CameraIsOrthographic | UIAnchor.Flags.HalfPixelOffset)), out vector.x, out vector.y);
		return camera.ScreenToWorldPoint(vector);
	}

	// Token: 0x06004767 RID: 18279 RVA: 0x0011F484 File Offset: 0x0011D684
	public static Vector3 WorldOrigin(Camera camera, UIAnchor.Side side, float depthOffset, bool halfPixel)
	{
		Vector3 vector;
		vector.z = depthOffset;
		Rect pixelRect = camera.pixelRect;
		float xMin = pixelRect.xMin;
		float xMax = pixelRect.xMax;
		float yMin = pixelRect.yMin;
		float yMax = pixelRect.yMax;
		UIAnchor.ScreenOrigin(side, xMin, xMax, yMin, yMax, (!camera.isOrthoGraphic) ? ((!halfPixel) ? ((UIAnchor.Flags)0) : UIAnchor.Flags.HalfPixelOffset) : ((!halfPixel) ? UIAnchor.Flags.CameraIsOrthographic : (UIAnchor.Flags.CameraIsOrthographic | UIAnchor.Flags.HalfPixelOffset)), out vector.x, out vector.y);
		return camera.ScreenToWorldPoint(vector);
	}

	// Token: 0x06004768 RID: 18280 RVA: 0x0011F50C File Offset: 0x0011D70C
	public static Vector3 WorldOrigin(Camera camera, UIAnchor.Side side, bool halfPixel)
	{
		Vector3 vector;
		vector.z = 0f;
		Rect pixelRect = camera.pixelRect;
		float xMin = pixelRect.xMin;
		float xMax = pixelRect.xMax;
		float yMin = pixelRect.yMin;
		float yMax = pixelRect.yMax;
		UIAnchor.ScreenOrigin(side, xMin, xMax, yMin, yMax, (!camera.isOrthoGraphic) ? ((!halfPixel) ? ((UIAnchor.Flags)0) : UIAnchor.Flags.HalfPixelOffset) : ((!halfPixel) ? UIAnchor.Flags.CameraIsOrthographic : (UIAnchor.Flags.CameraIsOrthographic | UIAnchor.Flags.HalfPixelOffset)), out vector.x, out vector.y);
		return camera.ScreenToWorldPoint(vector);
	}

	// Token: 0x06004769 RID: 18281 RVA: 0x0011F598 File Offset: 0x0011D798
	public static Vector3 WorldOrigin(Camera camera, UIAnchor.Side side, RectOffset offset, float depthOffset, float relativeOffsetX, float relativeOffsetY, bool halfPixel)
	{
		Vector3 vector;
		vector.z = depthOffset;
		Rect rect = offset.Add(camera.pixelRect);
		float xMin = rect.xMin;
		float xMax = rect.xMax;
		float yMin = rect.yMin;
		float yMax = rect.yMax;
		UIAnchor.ScreenOrigin(side, xMin, xMax, yMin, yMax, relativeOffsetX, relativeOffsetY, (!camera.isOrthoGraphic) ? ((!halfPixel) ? ((UIAnchor.Flags)0) : UIAnchor.Flags.HalfPixelOffset) : ((!halfPixel) ? UIAnchor.Flags.CameraIsOrthographic : (UIAnchor.Flags.CameraIsOrthographic | UIAnchor.Flags.HalfPixelOffset)), out vector.x, out vector.y);
		return camera.ScreenToWorldPoint(vector);
	}

	// Token: 0x0600476A RID: 18282 RVA: 0x0011F62C File Offset: 0x0011D82C
	public static Vector3 WorldOrigin(Camera camera, UIAnchor.Side side, RectOffset offset, float relativeOffsetX, float relativeOffsetY, bool halfPixel)
	{
		Vector3 vector;
		vector.z = 0f;
		Rect rect = offset.Add(camera.pixelRect);
		float xMin = rect.xMin;
		float xMax = rect.xMax;
		float yMin = rect.yMin;
		float yMax = rect.yMax;
		UIAnchor.ScreenOrigin(side, xMin, xMax, yMin, yMax, relativeOffsetX, relativeOffsetY, (!camera.isOrthoGraphic) ? ((!halfPixel) ? ((UIAnchor.Flags)0) : UIAnchor.Flags.HalfPixelOffset) : ((!halfPixel) ? UIAnchor.Flags.CameraIsOrthographic : (UIAnchor.Flags.CameraIsOrthographic | UIAnchor.Flags.HalfPixelOffset)), out vector.x, out vector.y);
		return camera.ScreenToWorldPoint(vector);
	}

	// Token: 0x0600476B RID: 18283 RVA: 0x0011F6C4 File Offset: 0x0011D8C4
	public static Vector3 WorldOrigin(Camera camera, UIAnchor.Side side, RectOffset offset, float depthOffset, bool halfPixel)
	{
		Vector3 vector;
		vector.z = depthOffset;
		Rect rect = offset.Add(camera.pixelRect);
		float xMin = rect.xMin;
		float xMax = rect.xMax;
		float yMin = rect.yMin;
		float yMax = rect.yMax;
		UIAnchor.ScreenOrigin(side, xMin, xMax, yMin, yMax, (!camera.isOrthoGraphic) ? ((!halfPixel) ? ((UIAnchor.Flags)0) : UIAnchor.Flags.HalfPixelOffset) : ((!halfPixel) ? UIAnchor.Flags.CameraIsOrthographic : (UIAnchor.Flags.CameraIsOrthographic | UIAnchor.Flags.HalfPixelOffset)), out vector.x, out vector.y);
		return camera.ScreenToWorldPoint(vector);
	}

	// Token: 0x0600476C RID: 18284 RVA: 0x0011F754 File Offset: 0x0011D954
	public static Vector3 WorldOrigin(Camera camera, UIAnchor.Side side, RectOffset offset, bool halfPixel)
	{
		Vector3 vector;
		vector.z = 0f;
		Rect rect = offset.Add(camera.pixelRect);
		float xMin = rect.xMin;
		float xMax = rect.xMax;
		float yMin = rect.yMin;
		float yMax = rect.yMax;
		UIAnchor.ScreenOrigin(side, xMin, xMax, yMin, yMax, (!camera.isOrthoGraphic) ? ((!halfPixel) ? ((UIAnchor.Flags)0) : UIAnchor.Flags.HalfPixelOffset) : ((!halfPixel) ? UIAnchor.Flags.CameraIsOrthographic : (UIAnchor.Flags.CameraIsOrthographic | UIAnchor.Flags.HalfPixelOffset)), out vector.x, out vector.y);
		return camera.ScreenToWorldPoint(vector);
	}

	// Token: 0x0600476D RID: 18285 RVA: 0x0011F7E8 File Offset: 0x0011D9E8
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

	// Token: 0x0600476E RID: 18286 RVA: 0x0011F874 File Offset: 0x0011DA74
	protected void Update()
	{
		if (this.uiCamera)
		{
			Vector3 vector = UIAnchor.WorldOrigin(this.uiCamera, this.side, this.depthOffset, this.relativeOffset.x, this.relativeOffset.y, this.halfPixelOffset);
			this.SetPosition(ref vector);
		}
	}

	// Token: 0x04002765 RID: 10085
	public Camera uiCamera;

	// Token: 0x04002766 RID: 10086
	public UIAnchor.Side side = UIAnchor.Side.Center;

	// Token: 0x04002767 RID: 10087
	public bool halfPixelOffset = true;

	// Token: 0x04002768 RID: 10088
	public bool otherThingsMightMoveThis;

	// Token: 0x04002769 RID: 10089
	public float depthOffset;

	// Token: 0x0400276A RID: 10090
	public Vector2 relativeOffset = Vector2.zero;

	// Token: 0x0400276B RID: 10091
	[NonSerialized]
	private Transform __mTrans;

	// Token: 0x0400276C RID: 10092
	[NonSerialized]
	private bool mTransGot;

	// Token: 0x0400276D RID: 10093
	[NonSerialized]
	private bool mOnce;

	// Token: 0x0400276E RID: 10094
	[NonSerialized]
	private Vector3 mLastPosition;

	// Token: 0x020007BD RID: 1981
	public enum Side
	{
		// Token: 0x04002770 RID: 10096
		BottomLeft,
		// Token: 0x04002771 RID: 10097
		Left,
		// Token: 0x04002772 RID: 10098
		TopLeft,
		// Token: 0x04002773 RID: 10099
		Top,
		// Token: 0x04002774 RID: 10100
		TopRight,
		// Token: 0x04002775 RID: 10101
		Right,
		// Token: 0x04002776 RID: 10102
		BottomRight,
		// Token: 0x04002777 RID: 10103
		Bottom,
		// Token: 0x04002778 RID: 10104
		Center
	}

	// Token: 0x020007BE RID: 1982
	protected static class Info
	{
		// Token: 0x0600476F RID: 18287 RVA: 0x0011F8D0 File Offset: 0x0011DAD0
		static Info()
		{
			RuntimePlatform platform = Application.platform;
			UIAnchor.Info.isWindows = (platform == 2 || platform == 5 || platform == 7);
		}

		// Token: 0x04002779 RID: 10105
		public static readonly bool isWindows;
	}

	// Token: 0x020007BF RID: 1983
	[Flags]
	public enum Flags : byte
	{
		// Token: 0x0400277B RID: 10107
		CameraIsOrthographic = 1,
		// Token: 0x0400277C RID: 10108
		HalfPixelOffset = 2
	}
}
