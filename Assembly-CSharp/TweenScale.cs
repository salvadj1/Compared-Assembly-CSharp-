using System;
using UnityEngine;

// Token: 0x020008A3 RID: 2211
[AddComponentMenu("NGUI/Tween/Scale")]
public class TweenScale : global::UITweener
{
	// Token: 0x17000E52 RID: 3666
	// (get) Token: 0x06004BD3 RID: 19411 RVA: 0x001282C0 File Offset: 0x001264C0
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

	// Token: 0x17000E53 RID: 3667
	// (get) Token: 0x06004BD4 RID: 19412 RVA: 0x001282E8 File Offset: 0x001264E8
	// (set) Token: 0x06004BD5 RID: 19413 RVA: 0x001282F8 File Offset: 0x001264F8
	public Vector3 scale
	{
		get
		{
			return this.cachedTransform.localScale;
		}
		set
		{
			this.cachedTransform.localScale = value;
		}
	}

	// Token: 0x06004BD6 RID: 19414 RVA: 0x00128308 File Offset: 0x00126508
	protected override void OnUpdate(float factor)
	{
		this.cachedTransform.localScale = this.from * (1f - factor) + this.to * factor;
		if (this.updateTable)
		{
			if (this.mTable == null)
			{
				this.mTable = global::NGUITools.FindInParents<global::UITable>(base.gameObject);
				if (this.mTable == null)
				{
					this.updateTable = false;
					return;
				}
			}
			this.mTable.repositionNow = true;
		}
	}

	// Token: 0x06004BD7 RID: 19415 RVA: 0x00128398 File Offset: 0x00126598
	public static global::TweenScale Begin(GameObject go, float duration, Vector3 scale)
	{
		global::TweenScale tweenScale = global::UITweener.Begin<global::TweenScale>(go, duration);
		tweenScale.from = tweenScale.scale;
		tweenScale.to = scale;
		return tweenScale;
	}

	// Token: 0x0400297F RID: 10623
	public Vector3 from = Vector3.one;

	// Token: 0x04002980 RID: 10624
	public Vector3 to = Vector3.one;

	// Token: 0x04002981 RID: 10625
	public bool updateTable;

	// Token: 0x04002982 RID: 10626
	private Transform mTrans;

	// Token: 0x04002983 RID: 10627
	private global::UITable mTable;
}
