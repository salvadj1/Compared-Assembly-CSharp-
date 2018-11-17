using System;
using UnityEngine;

// Token: 0x020008FE RID: 2302
[AddComponentMenu("Game/UI/Tooltip")]
public class UITooltip : MonoBehaviour
{
	// Token: 0x06004E81 RID: 20097 RVA: 0x00144BD8 File Offset: 0x00142DD8
	private void Awake()
	{
		global::UITooltip.mInstance = this;
	}

	// Token: 0x06004E82 RID: 20098 RVA: 0x00144BE0 File Offset: 0x00142DE0
	private void OnDestroy()
	{
		global::UITooltip.mInstance = null;
	}

	// Token: 0x06004E83 RID: 20099 RVA: 0x00144BE8 File Offset: 0x00142DE8
	private void Start()
	{
		this.mTrans = base.transform;
		this.mWidgets = base.GetComponentsInChildren<global::UIWidget>();
		this.mPos = this.mTrans.localPosition;
		this.mSize = this.mTrans.localScale;
		if (this.uiCamera == null)
		{
			this.uiCamera = global::NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		this.SetAlpha(0f);
	}

	// Token: 0x06004E84 RID: 20100 RVA: 0x00144C64 File Offset: 0x00142E64
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

	// Token: 0x06004E85 RID: 20101 RVA: 0x00144D60 File Offset: 0x00142F60
	private void SetAlpha(float val)
	{
		int i = 0;
		int num = this.mWidgets.Length;
		while (i < num)
		{
			global::UIWidget uiwidget = this.mWidgets[i];
			Color color = uiwidget.color;
			color.a = val;
			uiwidget.color = color;
			i++;
		}
	}

	// Token: 0x06004E86 RID: 20102 RVA: 0x00144DA8 File Offset: 0x00142FA8
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

	// Token: 0x06004E87 RID: 20103 RVA: 0x001451A0 File Offset: 0x001433A0
	public static void ShowText(string tooltipText)
	{
		if (global::UITooltip.mInstance != null)
		{
			global::UITooltip.mInstance.SetText(tooltipText);
		}
	}

	// Token: 0x04002C12 RID: 11282
	private static global::UITooltip mInstance;

	// Token: 0x04002C13 RID: 11283
	public Camera uiCamera;

	// Token: 0x04002C14 RID: 11284
	public global::UILabel text;

	// Token: 0x04002C15 RID: 11285
	public global::UISlicedSprite background;

	// Token: 0x04002C16 RID: 11286
	public float appearSpeed = 10f;

	// Token: 0x04002C17 RID: 11287
	public bool scalingTransitions = true;

	// Token: 0x04002C18 RID: 11288
	private Transform mTrans;

	// Token: 0x04002C19 RID: 11289
	private float mTarget;

	// Token: 0x04002C1A RID: 11290
	private float mCurrent;

	// Token: 0x04002C1B RID: 11291
	private Vector3 mPos;

	// Token: 0x04002C1C RID: 11292
	private Vector3 mSize;

	// Token: 0x04002C1D RID: 11293
	private global::UIWidget[] mWidgets;
}
