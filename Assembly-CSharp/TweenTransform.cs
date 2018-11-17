using System;
using UnityEngine;

// Token: 0x020008A4 RID: 2212
[AddComponentMenu("NGUI/Tween/Transform")]
public class TweenTransform : global::UITweener
{
	// Token: 0x06004BD9 RID: 19417 RVA: 0x001283CC File Offset: 0x001265CC
	protected override void OnUpdate(float factor)
	{
		if (this.from != null && this.to != null)
		{
			if (this.mTrans == null)
			{
				this.mTrans = base.transform;
			}
			this.mTrans.position = this.from.position * (1f - factor) + this.to.position * factor;
			this.mTrans.localScale = this.from.localScale * (1f - factor) + this.to.localScale * factor;
			this.mTrans.rotation = Quaternion.Slerp(this.from.rotation, this.to.rotation, factor);
		}
	}

	// Token: 0x06004BDA RID: 19418 RVA: 0x001284B0 File Offset: 0x001266B0
	public static global::TweenTransform Begin(GameObject go, float duration, Transform from, Transform to)
	{
		global::TweenTransform tweenTransform = global::UITweener.Begin<global::TweenTransform>(go, duration);
		tweenTransform.from = from;
		tweenTransform.to = to;
		return tweenTransform;
	}

	// Token: 0x04002984 RID: 10628
	public Transform from;

	// Token: 0x04002985 RID: 10629
	public Transform to;

	// Token: 0x04002986 RID: 10630
	private Transform mTrans;
}
