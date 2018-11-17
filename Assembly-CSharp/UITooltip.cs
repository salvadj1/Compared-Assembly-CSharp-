using System;
using UnityEngine;

// Token: 0x0200080C RID: 2060
[AddComponentMenu("Game/UI/Tooltip")]
public class UITooltip : MonoBehaviour
{
	// Token: 0x060049D2 RID: 18898 RVA: 0x0013AC74 File Offset: 0x00138E74
	private void Awake()
	{
		UITooltip.mInstance = this;
	}

	// Token: 0x060049D3 RID: 18899 RVA: 0x0013AC7C File Offset: 0x00138E7C
	private void OnDestroy()
	{
		UITooltip.mInstance = null;
	}

	// Token: 0x060049D4 RID: 18900 RVA: 0x0013AC84 File Offset: 0x00138E84
	private void Start()
	{
		this.mTrans = base.transform;
		this.mWidgets = base.GetComponentsInChildren<UIWidget>();
		this.mPos = this.mTrans.localPosition;
		this.mSize = this.mTrans.localScale;
		if (this.uiCamera == null)
		{
			this.uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		this.SetAlpha(0f);
	}

	// Token: 0x060049D5 RID: 18901 RVA: 0x0013AD00 File Offset: 0x00138F00
	private void Update()
	{
		if (this.mCurrent != this.mTarget)
		{
			this.mCurrent = Mathf.Lerp(this.mCurrent, this.mTarget, Time.deltaTime * this.appearSpeed);
			if (Mathf.Abs(this.mCurrent - this.mTarget) < 0.001f)
			{
				this.mCurrent = this.mTarget;
			}
			this.SetAlpha(this.mCurrent * this.mCurrent);
			if (this.scalingTransitions)
			{
				Vector3 vector = this.mSize * 0.25f;
				vector.y = -vector.y;
				Vector3 localScale = Vector3.one * (1.5f - this.mCurrent * 0.5f);
				Vector3 localPosition = Vector3.Lerp(this.mPos - vector, this.mPos, this.mCurrent);
				this.mTrans.localPosition = localPosition;
				this.mTrans.localScale = localScale;
			}
		}
	}

	// Token: 0x060049D6 RID: 18902 RVA: 0x0013ADFC File Offset: 0x00138FFC
	private void SetAlpha(float val)
	{
		int i = 0;
		int num = this.mWidgets.Length;
		while (i < num)
		{
			UIWidget uiwidget = this.mWidgets[i];
			Color color = uiwidget.color;
			color.a = val;
			uiwidget.color = color;
			i++;
		}
	}

	// Token: 0x060049D7 RID: 18903 RVA: 0x0013AE44 File Offset: 0x00139044
	private void SetText(string tooltipText)
	{
		if (this.text != null && !string.IsNullOrEmpty(tooltipText))
		{
			this.mTarget = 1f;
			if (this.text != null)
			{
				this.text.text = tooltipText;
			}
			this.mPos = Input.mousePosition;
			if (this.background != null)
			{
				Transform transform = this.background.transform;
				Transform transform2 = this.text.transform;
				Vector3 localPosition = transform2.localPosition;
				Vector3 localScale = transform2.localScale;
				this.mSize = this.text.relativeSize;
				this.mSize.x = this.mSize.x * localScale.x;
				this.mSize.y = this.mSize.y * localScale.y;
				this.mSize.x = this.mSize.x + (this.background.border.x + this.background.border.z + (localPosition.x - this.background.border.x) * 2f);
				this.mSize.y = this.mSize.y + (this.background.border.y + this.background.border.w + (-localPosition.y - this.background.border.y) * 2f);
				this.mSize.z = 1f;
				transform.localScale = this.mSize;
			}
			if (this.uiCamera != null)
			{
				this.mPos.x = Mathf.Clamp01(this.mPos.x / (float)Screen.width);
				this.mPos.y = Mathf.Clamp01(this.mPos.y / (float)Screen.height);
				float num = this.uiCamera.orthographicSize / this.mTrans.parent.lossyScale.y;
				float num2 = (float)Screen.height * 0.5f / num;
				Vector2 vector;
				vector..ctor(num2 * this.mSize.x / (float)Screen.width, num2 * this.mSize.y / (float)Screen.height);
				this.mPos.x = Mathf.Min(this.mPos.x, 1f - vector.x);
				this.mPos.y = Mathf.Max(this.mPos.y, vector.y);
				this.mTrans.position = this.uiCamera.ViewportToWorldPoint(this.mPos);
				this.mPos = this.mTrans.localPosition;
				this.mPos.x = Mathf.Round(this.mPos.x);
				this.mPos.y = Mathf.Round(this.mPos.y);
				this.mTrans.localPosition = this.mPos;
			}
			else
			{
				if (this.mPos.x + this.mSize.x > (float)Screen.width)
				{
					this.mPos.x = (float)Screen.width - this.mSize.x;
				}
				if (this.mPos.y - this.mSize.y < 0f)
				{
					this.mPos.y = this.mSize.y;
				}
				this.mPos.x = this.mPos.x - (float)Screen.width * 0.5f;
				this.mPos.y = this.mPos.y - (float)Screen.height * 0.5f;
			}
		}
		else
		{
			this.mTarget = 0f;
		}
	}

	// Token: 0x060049D8 RID: 18904 RVA: 0x0013B23C File Offset: 0x0013943C
	public static void ShowText(string tooltipText)
	{
		if (UITooltip.mInstance != null)
		{
			UITooltip.mInstance.SetText(tooltipText);
		}
	}

	// Token: 0x040029C4 RID: 10692
	private static UITooltip mInstance;

	// Token: 0x040029C5 RID: 10693
	public Camera uiCamera;

	// Token: 0x040029C6 RID: 10694
	public UILabel text;

	// Token: 0x040029C7 RID: 10695
	public UISlicedSprite background;

	// Token: 0x040029C8 RID: 10696
	public float appearSpeed = 10f;

	// Token: 0x040029C9 RID: 10697
	public bool scalingTransitions = true;

	// Token: 0x040029CA RID: 10698
	private Transform mTrans;

	// Token: 0x040029CB RID: 10699
	private float mTarget;

	// Token: 0x040029CC RID: 10700
	private float mCurrent;

	// Token: 0x040029CD RID: 10701
	private Vector3 mPos;

	// Token: 0x040029CE RID: 10702
	private Vector3 mSize;

	// Token: 0x040029CF RID: 10703
	private UIWidget[] mWidgets;
}
