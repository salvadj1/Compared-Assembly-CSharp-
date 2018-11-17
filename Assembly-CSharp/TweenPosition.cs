using System;
using UnityEngine;

// Token: 0x020008A1 RID: 2209
[AddComponentMenu("NGUI/Tween/Position")]
public class TweenPosition : global::UITweener
{
	// Token: 0x17000E4E RID: 3662
	// (get) Token: 0x06004BC7 RID: 19399 RVA: 0x00128134 File Offset: 0x00126334
	public Transform cachedTransform
	{
		get
		{
			if (this.mTrans == null)
			{
				this.mTrans = base.transform;
			}
			return this.mTrans;
		}
	}

	// Token: 0x17000E4F RID: 3663
	// (get) Token: 0x06004BC8 RID: 19400 RVA: 0x0012815C File Offset: 0x0012635C
	// (set) Token: 0x06004BC9 RID: 19401 RVA: 0x0012816C File Offset: 0x0012636C
	public Vector3 position
	{
		get
		{
			return this.cachedTransform.localPosition;
		}
		set
		{
			this.cachedTransform.localPosition = value;
		}
	}

	// Token: 0x06004BCA RID: 19402 RVA: 0x0012817C File Offset: 0x0012637C
	protected override void OnUpdate(float factor)
	{
		this.cachedTransform.localPosition = this.from * (1f - factor) + this.to * factor;
	}

	// Token: 0x06004BCB RID: 19403 RVA: 0x001281B8 File Offset: 0x001263B8
	public static global::TweenPosition Begin(GameObject go, float duration, Vector3 pos)
	{
		global::TweenPosition tweenPosition = global::UITweener.Begin<global::TweenPosition>(go, duration);
		tweenPosition.from = tweenPosition.position;
		tweenPosition.to = pos;
		return tweenPosition;
	}

	// Token: 0x04002979 RID: 10617
	public Vector3 from;

	// Token: 0x0400297A RID: 10618
	public Vector3 to;

	// Token: 0x0400297B RID: 10619
	private Transform mTrans;
}
