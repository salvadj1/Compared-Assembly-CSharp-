using System;
using UnityEngine;

// Token: 0x020007B2 RID: 1970
[AddComponentMenu("NGUI/Tween/Spring Position")]
public class SpringPosition : IgnoreTimeScale
{
	// Token: 0x0600472E RID: 18222 RVA: 0x0011E330 File Offset: 0x0011C530
	private void Start()
	{
		this.mTrans = base.transform;
	}

	// Token: 0x0600472F RID: 18223 RVA: 0x0011E340 File Offset: 0x0011C540
	private void Update()
	{
		float deltaTime = (!this.ignoreTimeScale) ? Time.deltaTime : base.UpdateRealTimeDelta();
		if (this.worldSpace)
		{
			if (this.mThreshold == 0f)
			{
				this.mThreshold = (this.target - this.mTrans.position).magnitude * 0.001f;
			}
			this.mTrans.position = NGUIMath.SpringLerp(this.mTrans.position, this.target, this.strength, deltaTime);
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
			this.mTrans.localPosition = NGUIMath.SpringLerp(this.mTrans.localPosition, this.target, this.strength, deltaTime);
			if (this.mThreshold >= (this.target - this.mTrans.localPosition).magnitude)
			{
				this.mTrans.localPosition = this.target;
				base.enabled = false;
			}
		}
	}

	// Token: 0x06004730 RID: 18224 RVA: 0x0011E4C0 File Offset: 0x0011C6C0
	public static SpringPosition Begin(GameObject go, Vector3 pos, float strength)
	{
		SpringPosition springPosition = go.GetComponent<SpringPosition>();
		if (springPosition == null)
		{
			springPosition = go.AddComponent<SpringPosition>();
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

	// Token: 0x04002732 RID: 10034
	public Vector3 target = Vector3.zero;

	// Token: 0x04002733 RID: 10035
	public float strength = 10f;

	// Token: 0x04002734 RID: 10036
	public bool worldSpace;

	// Token: 0x04002735 RID: 10037
	public bool ignoreTimeScale;

	// Token: 0x04002736 RID: 10038
	private Transform mTrans;

	// Token: 0x04002737 RID: 10039
	private float mThreshold;
}
