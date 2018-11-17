using System;
using System.Diagnostics;
using UnityEngine;

// Token: 0x02000689 RID: 1673
[ExecuteInEditMode]
public class TestTransformFind : MonoBehaviour
{
	// Token: 0x060039FB RID: 14843 RVA: 0x000D75CC File Offset: 0x000D57CC
	private void Update()
	{
		if (string.IsNullOrEmpty(this.find))
		{
			this.foundFind = null;
			this.foundIter = null;
		}
		else
		{
			if (this.findSW == null)
			{
				this.findSW = new Stopwatch();
			}
			this.findSW.Reset();
			this.findSW.Start();
			this.foundFind = base.transform.Find(this.find);
			this.findSW.Stop();
			this.findTime = (float)this.findSW.Elapsed.TotalMilliseconds;
			if (this.iterSW == null)
			{
				this.iterSW = new Stopwatch();
			}
			this.iterSW.Reset();
			this.iterSW.Start();
			this.foundIter = FindChildHelper.FindChildByName(this.find, this);
			this.iterSW.Stop();
			this.iterTime = (float)this.iterSW.Elapsed.TotalMilliseconds;
		}
	}

	// Token: 0x04001E08 RID: 7688
	public string find;

	// Token: 0x04001E09 RID: 7689
	public Transform foundFind;

	// Token: 0x04001E0A RID: 7690
	public Transform foundIter;

	// Token: 0x04001E0B RID: 7691
	public float findTime;

	// Token: 0x04001E0C RID: 7692
	public float iterTime;

	// Token: 0x04001E0D RID: 7693
	private Stopwatch findSW;

	// Token: 0x04001E0E RID: 7694
	private Stopwatch iterSW;
}
