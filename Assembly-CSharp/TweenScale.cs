using System;
using UnityEngine;

// Token: 0x020007B6 RID: 1974
[AddComponentMenu("NGUI/Tween/Scale")]
public class TweenScale : UITweener
{
	// Token: 0x17000DC0 RID: 3520
	// (get) Token: 0x06004744 RID: 18244 RVA: 0x0011E89C File Offset: 0x0011CA9C
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

	// Token: 0x17000DC1 RID: 3521
	// (get) Token: 0x06004745 RID: 18245 RVA: 0x0011E8C4 File Offset: 0x0011CAC4
	// (set) Token: 0x06004746 RID: 18246 RVA: 0x0011E8D4 File Offset: 0x0011CAD4
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

	// Token: 0x06004747 RID: 18247 RVA: 0x0011E8E4 File Offset: 0x0011CAE4
	protected override void OnUpdate(float factor)
	{
		this.cachedTransform.localScale = this.from * (1f - factor) + this.to * factor;
		if (this.updateTable)
		{
			if (this.mTable == null)
			{
				this.mTable = NGUITools.FindInParents<UITable>(base.gameObject);
				if (this.mTable == null)
				{
					this.updateTable = false;
					return;
				}
			}
			this.mTable.repositionNow = true;
		}
	}

	// Token: 0x06004748 RID: 18248 RVA: 0x0011E974 File Offset: 0x0011CB74
	public static TweenScale Begin(GameObject go, float duration, Vector3 scale)
	{
		TweenScale tweenScale = UITweener.Begin<TweenScale>(go, duration);
		tweenScale.from = tweenScale.scale;
		tweenScale.to = scale;
		return tweenScale;
	}

	// Token: 0x04002745 RID: 10053
	public Vector3 from = Vector3.one;

	// Token: 0x04002746 RID: 10054
	public Vector3 to = Vector3.one;

	// Token: 0x04002747 RID: 10055
	public bool updateTable;

	// Token: 0x04002748 RID: 10056
	private Transform mTrans;

	// Token: 0x04002749 RID: 10057
	private UITable mTable;
}
