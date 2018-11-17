using System;
using UnityEngine;

// Token: 0x0200087A RID: 2170
[RequireComponent(typeof(global::UIPanel))]
[AddComponentMenu("NGUI/Internal/Spring Panel")]
public class SpringPanel : global::IgnoreTimeScale
{
	// Token: 0x06004AAB RID: 19115 RVA: 0x00120F88 File Offset: 0x0011F188
	private void Start()
	{
		this.mPanel = base.GetComponent<global::UIPanel>();
		this.mDrag = base.GetComponent<global::UIDraggablePanel>();
		this.mTrans = base.transform;
	}

	// Token: 0x06004AAC RID: 19116 RVA: 0x00120FBC File Offset: 0x0011F1BC
	private void Update()
	{
		float deltaTime = base.UpdateRealTimeDelta();
		if (this.mThreshold == 0f)
		{
			this.mThreshold = (this.target - this.mTrans.localPosition).magnitude * 0.005f;
		}
		Vector3 localPosition = this.mTrans.localPosition;
		this.mTrans.localPosition = global::NGUIMath.SpringLerp(this.mTrans.localPosition, this.target, this.strength, deltaTime);
		Vector3 vector = this.mTrans.localPosition - localPosition;
		Vector4 clipRange = this.mPanel.clipRange;
		clipRange.x -= vector.x;
		clipRange.y -= vector.y;
		this.mPanel.clipRange = clipRange;
		if (this.mDrag != null)
		{
			this.mDrag.UpdateScrollbars(false);
		}
		if (this.mThreshold >= (this.target - this.mTrans.localPosition).magnitude)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06004AAD RID: 19117 RVA: 0x001210E4 File Offset: 0x0011F2E4
	public static global::SpringPanel Begin(GameObject go, Vector3 pos, float strength)
	{
		global::SpringPanel springPanel = go.GetComponent<global::SpringPanel>();
		if (springPanel == null)
		{
			springPanel = go.AddComponent<global::SpringPanel>();
		}
		springPanel.target = pos;
		springPanel.strength = strength;
		if (!springPanel.enabled)
		{
			springPanel.mThreshold = 0f;
			springPanel.enabled = true;
		}
		return springPanel;
	}

	// Token: 0x040028A4 RID: 10404
	public Vector3 target = Vector3.zero;

	// Token: 0x040028A5 RID: 10405
	public float strength = 10f;

	// Token: 0x040028A6 RID: 10406
	private global::UIPanel mPanel;

	// Token: 0x040028A7 RID: 10407
	private Transform mTrans;

	// Token: 0x040028A8 RID: 10408
	private float mThreshold;

	// Token: 0x040028A9 RID: 10409
	private global::UIDraggablePanel mDrag;
}
