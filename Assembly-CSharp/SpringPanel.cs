using System;
using UnityEngine;

// Token: 0x02000795 RID: 1941
[AddComponentMenu("NGUI/Internal/Spring Panel")]
[RequireComponent(typeof(UIPanel))]
public class SpringPanel : IgnoreTimeScale
{
	// Token: 0x0600463E RID: 17982 RVA: 0x00117608 File Offset: 0x00115808
	private void Start()
	{
		this.mPanel = base.GetComponent<UIPanel>();
		this.mDrag = base.GetComponent<UIDraggablePanel>();
		this.mTrans = base.transform;
	}

	// Token: 0x0600463F RID: 17983 RVA: 0x0011763C File Offset: 0x0011583C
	private void Update()
	{
		float deltaTime = base.UpdateRealTimeDelta();
		if (this.mThreshold == 0f)
		{
			this.mThreshold = (this.target - this.mTrans.localPosition).magnitude * 0.005f;
		}
		Vector3 localPosition = this.mTrans.localPosition;
		this.mTrans.localPosition = NGUIMath.SpringLerp(this.mTrans.localPosition, this.target, this.strength, deltaTime);
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

	// Token: 0x06004640 RID: 17984 RVA: 0x00117764 File Offset: 0x00115964
	public static SpringPanel Begin(GameObject go, Vector3 pos, float strength)
	{
		SpringPanel springPanel = go.GetComponent<SpringPanel>();
		if (springPanel == null)
		{
			springPanel = go.AddComponent<SpringPanel>();
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

	// Token: 0x0400266D RID: 9837
	public Vector3 target = Vector3.zero;

	// Token: 0x0400266E RID: 9838
	public float strength = 10f;

	// Token: 0x0400266F RID: 9839
	private UIPanel mPanel;

	// Token: 0x04002670 RID: 9840
	private Transform mTrans;

	// Token: 0x04002671 RID: 9841
	private float mThreshold;

	// Token: 0x04002672 RID: 9842
	private UIDraggablePanel mDrag;
}
