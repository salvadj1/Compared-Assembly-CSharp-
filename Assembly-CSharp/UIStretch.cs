using System;
using UnityEngine;

// Token: 0x020008F5 RID: 2293
[AddComponentMenu("NGUI/UI/Stretch")]
[ExecuteInEditMode]
public class UIStretch : MonoBehaviour
{
	// Token: 0x06004E68 RID: 20072 RVA: 0x00143738 File Offset: 0x00141938
	private void OnEnable()
	{
		if (this.uiCamera == null)
		{
			this.uiCamera = global::NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		this.mRoot = global::NGUITools.FindInParents<global::UIRoot>(base.gameObject);
	}

	// Token: 0x06004E69 RID: 20073 RVA: 0x00143780 File Offset: 0x00141980
	private void Update()
	{
		if (this.uiCamera != null && this.style != global::UIStretch.Style.None)
		{
			if (this.mTrans == null)
			{
				this.mTrans = base.transform;
			}
			Rect pixelRect = this.uiCamera.pixelRect;
			float num = pixelRect.width;
			float num2 = pixelRect.height;
			if (this.mRoot != null && !this.mRoot.automatic && num2 > 1f)
			{
				float num3 = (float)this.mRoot.manualHeight / num2;
				num *= num3;
				num2 *= num3;
			}
			Vector3 localScale = this.mTrans.localScale;
			if (this.style == global::UIStretch.Style.BasedOnHeight)
			{
				localScale.x = this.relativeSize.x * num2;
				localScale.y = this.relativeSize.y * num2;
			}
			else
			{
				if (this.style == global::UIStretch.Style.Both || this.style == global::UIStretch.Style.Horizontal)
				{
					localScale.x = this.relativeSize.x * num;
				}
				if (this.style == global::UIStretch.Style.Both || this.style == global::UIStretch.Style.Vertical)
				{
					localScale.y = this.relativeSize.y * num2;
				}
			}
			if (this.mTrans.localScale != localScale)
			{
				this.mTrans.localScale = localScale;
			}
		}
	}

	// Token: 0x04002BF4 RID: 11252
	public Camera uiCamera;

	// Token: 0x04002BF5 RID: 11253
	public global::UIStretch.Style style;

	// Token: 0x04002BF6 RID: 11254
	public Vector2 relativeSize = Vector2.one;

	// Token: 0x04002BF7 RID: 11255
	private Transform mTrans;

	// Token: 0x04002BF8 RID: 11256
	private global::UIRoot mRoot;

	// Token: 0x020008F6 RID: 2294
	public enum Style
	{
		// Token: 0x04002BFA RID: 11258
		None,
		// Token: 0x04002BFB RID: 11259
		Horizontal,
		// Token: 0x04002BFC RID: 11260
		Vertical,
		// Token: 0x04002BFD RID: 11261
		Both,
		// Token: 0x04002BFE RID: 11262
		BasedOnHeight
	}
}
