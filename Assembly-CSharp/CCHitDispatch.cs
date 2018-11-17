using System;
using UnityEngine;

// Token: 0x02000279 RID: 633
[RequireComponent(typeof(CCDesc))]
public sealed class CCHitDispatch : MonoBehaviour
{
	// Token: 0x1400000C RID: 12
	// (add) Token: 0x06001707 RID: 5895 RVA: 0x000579B4 File Offset: 0x00055BB4
	// (remove) Token: 0x06001708 RID: 5896 RVA: 0x000579DC File Offset: 0x00055BDC
	public event CCDesc.HitFilter OnHit
	{
		add
		{
			CCDesc.HitManager hits = this.Hits;
			if (!object.ReferenceEquals(hits, null))
			{
				hits.OnHit += value;
			}
		}
		remove
		{
			CCDesc.HitManager hits = this.Hits;
			if (!object.ReferenceEquals(hits, null))
			{
				hits.OnHit -= value;
			}
		}
	}

	// Token: 0x170006B1 RID: 1713
	// (get) Token: 0x06001709 RID: 5897 RVA: 0x00057A04 File Offset: 0x00055C04
	public CCDesc.HitManager Hits
	{
		get
		{
			if (!this.didSetup)
			{
				this.DoSetup();
			}
			return this.hitManager;
		}
	}

	// Token: 0x170006B2 RID: 1714
	// (get) Token: 0x0600170A RID: 5898 RVA: 0x00057A20 File Offset: 0x00055C20
	public CCDesc CCDesc
	{
		get
		{
			if (!Application.isPlaying)
			{
				return (!this.ccdesc) ? base.GetComponent<CCDesc>() : this.ccdesc;
			}
			if (!this.didSetup)
			{
				this.DoSetup();
			}
			return this.ccdesc;
		}
	}

	// Token: 0x0600170B RID: 5899 RVA: 0x00057A70 File Offset: 0x00055C70
	private void DoSetup()
	{
		if (!this.didSetup)
		{
			if (!Application.isPlaying)
			{
				return;
			}
			this.didSetup = true;
			(this.ccdesc = base.GetComponent<CCDesc>()).AssignedHitManager = (this.hitManager = new CCDesc.HitManager());
		}
	}

	// Token: 0x0600170C RID: 5900 RVA: 0x00057ABC File Offset: 0x00055CBC
	private void OnDestroy()
	{
		if (this.didSetup && !object.ReferenceEquals(this.hitManager, null))
		{
			CCDesc.HitManager hitManager = this.hitManager;
			this.hitManager = null;
			if (this.ccdesc)
			{
				this.ccdesc.AssignedHitManager = null;
			}
			hitManager.Dispose();
		}
	}

	// Token: 0x0600170D RID: 5901 RVA: 0x00057B18 File Offset: 0x00055D18
	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		CCDesc.HitManager hits = this.Hits;
		if (!object.ReferenceEquals(hits, null))
		{
			hits.Push(hit);
		}
	}

	// Token: 0x0600170E RID: 5902 RVA: 0x00057B40 File Offset: 0x00055D40
	public static CCHitDispatch GetHitDispatch(CCDesc CCDesc)
	{
		if (!CCDesc)
		{
			return null;
		}
		if (!object.ReferenceEquals(CCDesc.AssignedHitManager, null))
		{
			return CCDesc.GetComponent<CCHitDispatch>();
		}
		CCHitDispatch component = CCDesc.GetComponent<CCHitDispatch>();
		if (component)
		{
			return component;
		}
		return CCDesc.gameObject.AddComponent<CCHitDispatch>();
	}

	// Token: 0x04000BC7 RID: 3015
	[NonSerialized]
	private CCDesc ccdesc;

	// Token: 0x04000BC8 RID: 3016
	[NonSerialized]
	private CCDesc.HitManager hitManager;

	// Token: 0x04000BC9 RID: 3017
	[NonSerialized]
	private bool didSetup;
}
