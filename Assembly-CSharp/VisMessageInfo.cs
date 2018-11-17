using System;

// Token: 0x0200044C RID: 1100
public sealed class VisMessageInfo : IDisposable
{
	// Token: 0x0600268A RID: 9866 RVA: 0x0008CABC File Offset: 0x0008ACBC
	private VisMessageInfo()
	{
	}

	// Token: 0x0600268B RID: 9867 RVA: 0x0008CAC4 File Offset: 0x0008ACC4
	void IDisposable.Dispose()
	{
		if (this._kind != (global::VisMessageInfo.Kind)0)
		{
			this._kind = (global::VisMessageInfo.Kind)0;
			this.next = global::VisMessageInfo.dump;
			global::VisMessageInfo.dump = this;
			this._other = null;
			this._self = null;
		}
	}

	// Token: 0x170008EE RID: 2286
	// (get) Token: 0x0600268C RID: 9868 RVA: 0x0008CAF8 File Offset: 0x0008ACF8
	public bool isSpectatingEvent
	{
		get
		{
			return (this._kind - global::VisMessageInfo.Kind.SeeEnter & 1) == 1;
		}
	}

	// Token: 0x170008EF RID: 2287
	// (get) Token: 0x0600268D RID: 9869 RVA: 0x0008CB08 File Offset: 0x0008AD08
	public bool isSeeEvent
	{
		get
		{
			return (this._kind & global::VisMessageInfo.Kind.SeeEnter) == global::VisMessageInfo.Kind.SeeEnter;
		}
	}

	// Token: 0x170008F0 RID: 2288
	// (get) Token: 0x0600268E RID: 9870 RVA: 0x0008CB18 File Offset: 0x0008AD18
	public global::VisMessageInfo.Kind kind
	{
		get
		{
			return this._kind;
		}
	}

	// Token: 0x170008F1 RID: 2289
	// (get) Token: 0x0600268F RID: 9871 RVA: 0x0008CB20 File Offset: 0x0008AD20
	public global::VisNode sender
	{
		get
		{
			return this._self.node;
		}
	}

	// Token: 0x170008F2 RID: 2290
	// (get) Token: 0x06002690 RID: 9872 RVA: 0x0008CB30 File Offset: 0x0008AD30
	public global::VisNode self
	{
		get
		{
			return this._self.node;
		}
	}

	// Token: 0x170008F3 RID: 2291
	// (get) Token: 0x06002691 RID: 9873 RVA: 0x0008CB40 File Offset: 0x0008AD40
	public global::VisReactor issuer
	{
		get
		{
			return this._self;
		}
	}

	// Token: 0x170008F4 RID: 2292
	// (get) Token: 0x06002692 RID: 9874 RVA: 0x0008CB48 File Offset: 0x0008AD48
	public global::VisNode target
	{
		get
		{
			return this._other;
		}
	}

	// Token: 0x170008F5 RID: 2293
	// (get) Token: 0x06002693 RID: 9875 RVA: 0x0008CB50 File Offset: 0x0008AD50
	public global::VisNode other
	{
		get
		{
			return this._other;
		}
	}

	// Token: 0x170008F6 RID: 2294
	// (get) Token: 0x06002694 RID: 9876 RVA: 0x0008CB58 File Offset: 0x0008AD58
	public bool isTwoNodeEvent
	{
		get
		{
			return this._kind > global::VisMessageInfo.Kind.SpectatorExit;
		}
	}

	// Token: 0x170008F7 RID: 2295
	// (get) Token: 0x06002695 RID: 9877 RVA: 0x0008CB64 File Offset: 0x0008AD64
	public global::VisNode spectator
	{
		get
		{
			return (!this.isSpectatingEvent) ? this.self : this._other;
		}
	}

	// Token: 0x170008F8 RID: 2296
	// (get) Token: 0x06002696 RID: 9878 RVA: 0x0008CB84 File Offset: 0x0008AD84
	public global::VisNode spectated
	{
		get
		{
			return (!this.isSeeEvent) ? this.self : this._other;
		}
	}

	// Token: 0x170008F9 RID: 2297
	// (get) Token: 0x06002697 RID: 9879 RVA: 0x0008CBA4 File Offset: 0x0008ADA4
	public global::VisNode seenNode
	{
		get
		{
			return this.spectated;
		}
	}

	// Token: 0x170008FA RID: 2298
	// (get) Token: 0x06002698 RID: 9880 RVA: 0x0008CBAC File Offset: 0x0008ADAC
	public global::VisNode seeer
	{
		get
		{
			return this.spectator;
		}
	}

	// Token: 0x06002699 RID: 9881 RVA: 0x0008CBB4 File Offset: 0x0008ADB4
	public static global::VisMessageInfo Create(global::VisReactor issuer, global::VisNode other, global::VisMessageInfo.Kind kind)
	{
		global::VisMessageInfo visMessageInfo;
		if (global::VisMessageInfo.dump != null)
		{
			visMessageInfo = global::VisMessageInfo.dump;
			global::VisMessageInfo.dump = visMessageInfo.next;
			visMessageInfo.next = null;
		}
		else
		{
			visMessageInfo = new global::VisMessageInfo();
		}
		visMessageInfo._self = issuer;
		visMessageInfo._other = other;
		visMessageInfo._kind = kind;
		return visMessageInfo;
	}

	// Token: 0x0600269A RID: 9882 RVA: 0x0008CC04 File Offset: 0x0008AE04
	public static global::VisMessageInfo Create(global::VisReactor issuer, global::VisMessageInfo.Kind kind)
	{
		global::VisMessageInfo visMessageInfo;
		if (global::VisMessageInfo.dump != null)
		{
			visMessageInfo = global::VisMessageInfo.dump;
			global::VisMessageInfo.dump = visMessageInfo.next;
			visMessageInfo.next = null;
		}
		else
		{
			visMessageInfo = new global::VisMessageInfo();
		}
		visMessageInfo._self = issuer;
		visMessageInfo._other = null;
		visMessageInfo._kind = kind;
		return visMessageInfo;
	}

	// Token: 0x0400121B RID: 4635
	private global::VisNode _other;

	// Token: 0x0400121C RID: 4636
	private global::VisReactor _self;

	// Token: 0x0400121D RID: 4637
	private global::VisMessageInfo next;

	// Token: 0x0400121E RID: 4638
	private global::VisMessageInfo.Kind _kind;

	// Token: 0x0400121F RID: 4639
	private static global::VisMessageInfo dump;

	// Token: 0x0200044D RID: 1101
	public enum Kind : byte
	{
		// Token: 0x04001221 RID: 4641
		SeeEnter = 1,
		// Token: 0x04001222 RID: 4642
		SeeExit = 3,
		// Token: 0x04001223 RID: 4643
		SeeAdd = 5,
		// Token: 0x04001224 RID: 4644
		SeeRemove = 7,
		// Token: 0x04001225 RID: 4645
		SpectatedEnter = 2,
		// Token: 0x04001226 RID: 4646
		SpectatorExit = 4,
		// Token: 0x04001227 RID: 4647
		SpectatorAdd = 8,
		// Token: 0x04001228 RID: 4648
		SpectatorRemove = 10
	}
}
