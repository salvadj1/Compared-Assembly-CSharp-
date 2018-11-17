using System;
using UnityEngine;

// Token: 0x02000803 RID: 2051
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Stretch")]
public class UIStretch : MonoBehaviour
{
	// Token: 0x060049B9 RID: 18873 RVA: 0x001397D4 File Offset: 0x001379D4
	private void OnEnable()
	{
		if (this.uiCamera == null)
		{
			this.uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		this.mRoot = NGUITools.FindInParents<UIRoot>(base.gameObject);
	}

	// Token: 0x060049BA RID: 18874 RVA: 0x0013981C File Offset: 0x00137A1C
	private void Update()
	{
		if (this.uiCamera != null && this.style != UIStretch.Style.None)
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
			if (this.style == UIStretch.Style.BasedOnHeight)
			{
				localScale.x = this.relativeSize.x * num2;
				localScale.y = this.relativeSize.y * num2;
			}
			else
			{
				if (this.style == UIStretch.Style.Both || this.style == UIStretch.Style.Horizontal)
				{
					localScale.x = this.relativeSize.x * num;
				}
				if (this.style == UIStretch.Style.Both || this.style == UIStretch.Style.Vertical)
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

	// Token: 0x040029A6 RID: 10662
	public Camera uiCamera;

	// Token: 0x040029A7 RID: 10663
	public UIStretch.Style style;

	// Token: 0x040029A8 RID: 10664
	public Vector2 relativeSize = Vector2.one;

	// Token: 0x040029A9 RID: 10665
	private Transform mTrans;

	// Token: 0x040029AA RID: 10666
	private UIRoot mRoot;

	// Token: 0x02000804 RID: 2052
	public enum Style
	{
		// Token: 0x040029AC RID: 10668
		None,
		// Token: 0x040029AD RID: 10669
		Horizontal,
		// Token: 0x040029AE RID: 10670
		Vertical,
		// Token: 0x040029AF RID: 10671
		Both,
		// Token: 0x040029B0 RID: 10672
		BasedOnHeight
	}
}
