using System;
using UnityEngine;

// Token: 0x020002AF RID: 687
[RequireComponent(typeof(global::CCDesc))]
public sealed class CCHitDispatch : MonoBehaviour
{
	// Token: 0x1400000C RID: 12
	// (add) Token: 0x06001869 RID: 6249 RVA: 0x0005BDFC File Offset: 0x00059FFC
	// (remove) Token: 0x0600186A RID: 6250 RVA: 0x0005BE24 File Offset: 0x0005A024
	public event global::CCDesc.HitFilter OnHit
	{
		add
		{
			global::CCDesc.HitManager hits = this.Hits;
			if (!object.ReferenceEquals(hits, null))
			{
				hits.OnHit += value;
			}
		}
		remove
		{
			global::CCDesc.HitManager hits = this.Hits;
			if (!object.ReferenceEquals(hits, null))
			{
				hits.OnHit -= value;
			}
		}
	}

	// Token: 0x170006FB RID: 1787
	// (get) Token: 0x0600186B RID: 6251 RVA: 0x0005BE4C File Offset: 0x0005A04C
	public global::CCDesc.HitManager Hits
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

	// Token: 0x170006FC RID: 1788
	// (get) Token: 0x0600186C RID: 6252 RVA: 0x0005BE68 File Offset: 0x0005A068
	public global::CCDesc CCDesc
	{
		get
		{
			if (!Application.isPlaying)
			{
				return (!this.ccdesc) ? base.GetComponent<global::CCDesc>() : this.ccdesc;
			}
			if (!this.didSetup)
			{
				this.DoSetup();
			}
			return this.ccdesc;
		}
	}

	// Token: 0x0600186D RID: 6253 RVA: 0x0005BEB8 File Offset: 0x0005A0B8
	private void DoSetup()
	{
		if (!this.didSetup)
		{
			if (!Application.isPlaying)
			{
				return;
			}
			this.didSetup = true;
			(this.ccdesc = base.GetComponent<global::CCDesc>()).AssignedHitManager = (this.hitManager = new global::CCDesc.HitManager());
		}
	}

	// Token: 0x0600186E RID: 6254 RVA: 0x0005BF04 File Offset: 0x0005A104
	private void OnDestroy()
	{
		if (this.didSetup && !object.ReferenceEquals(this.hitManager, null))
		{
			global::CCDesc.HitManager hitManager = this.hitManager;
			this.hitManager = null;
			if (this.ccdesc)
			{
				this.ccdesc.AssignedHitManager = null;
			}
			hitManager.Dispose();
		}
	}

	// Token: 0x0600186F RID: 6255 RVA: 0x0005BF60 File Offset: 0x0005A160
	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		global::CCDesc.HitManager hits = this.Hits;
		if (!object.ReferenceEquals(hits, null))
		{
			hits.Push(hit);
		}
	}

	// Token: 0x06001870 RID: 6256 RVA: 0x0005BF88 File Offset: 0x0005A188
	public static global::CCHitDispatch GetHitDispatch(global::CCDesc CCDesc)
	{
		if (!CCDesc)
		{
			return null;
		}
		if (!object.ReferenceEquals(CCDesc.AssignedHitManager, null))
		{
			return CCDesc.GetComponent<global::CCHitDispatch>();
		}
		global::CCHitDispatch component = CCDesc.GetComponent<global::CCHitDispatch>();
		if (component)
		{
			return component;
		}
		return CCDesc.gameObject.AddComponent<global::CCHitDispatch>();
	}

	// Token: 0x04000CED RID: 3309
	[NonSerialized]
	private global::CCDesc ccdesc;

	// Token: 0x04000CEE RID: 3310
	[NonSerialized]
	private global::CCDesc.HitManager hitManager;

	// Token: 0x04000CEF RID: 3311
	[NonSerialized]
	private bool didSetup;
}
