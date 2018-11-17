using System;
using System.Diagnostics;
using UnityEngine;

// Token: 0x0200074E RID: 1870
[ExecuteInEditMode]
public class TestTransformFind : MonoBehaviour
{
	// Token: 0x06003DF3 RID: 15859 RVA: 0x000DFFAC File Offset: 0x000DE1AC
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
			this.foundIter = global::FindChildHelper.FindChildByName(this.find, this);
			this.iterSW.Stop();
			this.iterTime = (float)this.iterSW.Elapsed.TotalMilliseconds;
		}
	}

	// Token: 0x04002000 RID: 8192
	public string find;

	// Token: 0x04002001 RID: 8193
	public Transform foundFind;

	// Token: 0x04002002 RID: 8194
	public Transform foundIter;

	// Token: 0x04002003 RID: 8195
	public float findTime;

	// Token: 0x04002004 RID: 8196
	public float iterTime;

	// Token: 0x04002005 RID: 8197
	private Stopwatch findSW;

	// Token: 0x04002006 RID: 8198
	private Stopwatch iterSW;
}
