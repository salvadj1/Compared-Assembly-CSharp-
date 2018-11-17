using System;
using UnityEngine;

// Token: 0x0200089F RID: 2207
[AddComponentMenu("NGUI/Tween/Spring Position")]
public class SpringPosition : global::IgnoreTimeScale
{
	// Token: 0x06004BBD RID: 19389 RVA: 0x00127D54 File Offset: 0x00125F54
	private void Start()
	{
		this.mTrans = base.transform;
	}

	// Token: 0x06004BBE RID: 19390 RVA: 0x00127D64 File Offset: 0x00125F64
	private void Update()
	{
		float deltaTime = (!this.ignoreTimeScale) ? Time.deltaTime : base.UpdateRealTimeDelta();
		if (this.worldSpace)
		{
			if (this.mThreshold == 0f)
			{
				this.mThreshold = (this.target - this.mTrans.position).magnitude * 0.001f;
			}
			this.mTrans.position = global::NGUIMath.SpringLerp(this.mTrans.position, this.target, this.strength, deltaTime);
			if (this.mThreshold >= (this.target - this.mTrans.position).magnitude)
			{
				this.mTrans.position = this.target;
				base.enabled = false;
			}
		}
		else
		{
			if (this.mThreshold == 0f)
			{
				this.mThreshold = (this.target - this.mTrans.localPosition).magnitude * 0.001f;
			}
			this.mTrans.localPosition = global::NGUIMath.SpringLerp(this.mTrans.localPosition, this.target, this.strength, deltaTime);
			if (this.mThreshold >= (this.target - this.mTrans.localPosition).magnitude)
			{
				this.mTrans.localPosition = this.target;
				base.enabled = false;
			}
		}
	}

	// Token: 0x06004BBF RID: 19391 RVA: 0x00127EE4 File Offset: 0x001260E4
	public static global::SpringPosition Begin(GameObject go, Vector3 pos, float strength)
	{
		global::SpringPosition springPosition = go.GetComponent<global::SpringPosition>();
		if (springPosition == null)
		{
			springPosition = go.AddComponent<global::SpringPosition>();
		}
		springPosition.target = pos;
		springPosition.strength = strength;
		if (!springPosition.enabled)
		{
			springPosition.mThreshold = 0f;
			springPosition.enabled = true;
		}
		return springPosition;
	}

	// Token: 0x0400296C RID: 10604
	public Vector3 target = Vector3.zero;

	// Token: 0x0400296D RID: 10605
	public float strength = 10f;

	// Token: 0x0400296E RID: 10606
	public bool worldSpace;

	// Token: 0x0400296F RID: 10607
	public bool ignoreTimeScale;

	// Token: 0x04002970 RID: 10608
	private Transform mTrans;

	// Token: 0x04002971 RID: 10609
	private float mThreshold;
}
