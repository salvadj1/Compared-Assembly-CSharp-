using System;

// Token: 0x0200039F RID: 927
public sealed class VisMessageInfo : IDisposable
{
	// Token: 0x06002328 RID: 9000 RVA: 0x000876C0 File Offset: 0x000858C0
	private VisMessageInfo()
	{
	}

	// Token: 0x06002329 RID: 9001 RVA: 0x000876C8 File Offset: 0x000858C8
	void IDisposable.Dispose()
	{
		if (this._kind != (VisMessageInfo.Kind)0)
		{
			this._kind = (VisMessageInfo.Kind)0;
			this.next = VisMessageInfo.dump;
			VisMessageInfo.dump = this;
			this._other = null;
			this._self = null;
		}
	}

	// Token: 0x17000890 RID: 2192
	// (get) Token: 0x0600232A RID: 9002 RVA: 0x000876FC File Offset: 0x000858FC
	public bool isSpectatingEvent
	{
		get
		{
			return (this._kind - VisMessageInfo.Kind.SeeEnter & 1) == 1;
		}
	}

	// Token: 0x17000891 RID: 2193
	// (get) Token: 0x0600232B RID: 9003 RVA: 0x0008770C File Offset: 0x0008590C
	public bool isSeeEvent
	{
		get
		{
			return (this._kind & VisMessageInfo.Kind.SeeEnter) == VisMessageInfo.Kind.SeeEnter;
		}
	}

	// Token: 0x17000892 RID: 2194
	// (get) Token: 0x0600232C RID: 9004 RVA: 0x0008771C File Offset: 0x0008591C
	public VisMessageInfo.Kind kind
	{
		get
		{
			return this._kind;
		}
	}

	// Token: 0x17000893 RID: 2195
	// (get) Token: 0x0600232D RID: 9005 RVA: 0x00087724 File Offset: 0x00085924
	public VisNode sender
	{
		get
		{
			return this._self.node;
		}
	}

	// Token: 0x17000894 RID: 2196
	// (get) Token: 0x0600232E RID: 9006 RVA: 0x00087734 File Offset: 0x00085934
	public VisNode self
	{
		get
		{
			return this._self.node;
		}
	}

	// Token: 0x17000895 RID: 2197
	// (get) Token: 0x0600232F RID: 9007 RVA: 0x00087744 File Offset: 0x00085944
	public VisReactor issuer
	{
		get
		{
			return this._self;
		}
	}

	// Token: 0x17000896 RID: 2198
	// (get) Token: 0x06002330 RID: 9008 RVA: 0x0008774C File Offset: 0x0008594C
	public VisNode target
	{
		get
		{
			return this._other;
		}
	}

	// Token: 0x17000897 RID: 2199
	// (get) Token: 0x06002331 RID: 9009 RVA: 0x00087754 File Offset: 0x00085954
	public VisNode other
	{
		get
		{
			return this._other;
		}
	}

	// Token: 0x17000898 RID: 2200
	// (get) Token: 0x06002332 RID: 9010 RVA: 0x0008775C File Offset: 0x0008595C
	public bool isTwoNodeEvent
	{
		get
		{
			return this._kind > VisMessageInfo.Kind.SpectatorExit;
		}
	}

	// Token: 0x17000899 RID: 2201
	// (get) Token: 0x06002333 RID: 9011 RVA: 0x00087768 File Offset: 0x00085968
	public VisNode spectator
	{
		get
		{
			return (!this.isSpectatingEvent) ? this.self : this._other;
		}
	}

	// Token: 0x1700089A RID: 2202
	// (get) Token: 0x06002334 RID: 9012 RVA: 0x00087788 File Offset: 0x00085988
	public VisNode spectated
	{
		get
		{
			return (!this.isSeeEvent) ? this.self : this._other;
		}
	}

	// Token: 0x1700089B RID: 2203
	// (get) Token: 0x06002335 RID: 9013 RVA: 0x000877A8 File Offset: 0x000859A8
	public VisNode seenNode
	{
		get
		{
			return this.spectated;
		}
	}

	// Token: 0x1700089C RID: 2204
	// (get) Token: 0x06002336 RID: 9014 RVA: 0x000877B0 File Offset: 0x000859B0
	public VisNode seeer
	{
		get
		{
			return this.spectator;
		}
	}

	// Token: 0x06002337 RID: 9015 RVA: 0x000877B8 File Offset: 0x000859B8
	public static VisMessageInfo Create(VisReactor issuer, VisNode other, VisMessageInfo.Kind kind)
	{
		VisMessageInfo visMessageInfo;
		if (VisMessageInfo.dump != null)
		{
			visMessageInfo = VisMessageInfo.dump;
			VisMessageInfo.dump = visMessageInfo.next;
			visMessageInfo.next = null;
		}
		else
		{
			visMessageInfo = new VisMessageInfo();
		}
		visMessageInfo._self = issuer;
		visMessageInfo._other = other;
		visMessageInfo._kind = kind;
		return visMessageInfo;
	}

	// Token: 0x06002338 RID: 9016 RVA: 0x00087808 File Offset: 0x00085A08
	public static VisMessageInfo Create(VisReactor issuer, VisMessageInfo.Kind kind)
	{
		VisMessageInfo visMessageInfo;
		if (VisMessageInfo.dump != null)
		{
			visMessageInfo = VisMessageInfo.dump;
			VisMessageInfo.dump = visMessageInfo.next;
			visMessageInfo.next = null;
		}
		else
		{
			visMessageInfo = new VisMessageInfo();
		}
		visMessageInfo._self = issuer;
		visMessageInfo._other = null;
		visMessageInfo._kind = kind;
		return visMessageInfo;
	}

	// Token: 0x040010B5 RID: 4277
	private VisNode _other;

	// Token: 0x040010B6 RID: 4278
	private VisReactor _self;

	// Token: 0x040010B7 RID: 4279
	private VisMessageInfo next;

	// Token: 0x040010B8 RID: 4280
	private VisMessageInfo.Kind _kind;

	// Token: 0x040010B9 RID: 4281
	private static VisMessageInfo dump;

	// Token: 0x020003A0 RID: 928
	public enum Kind : byte
	{
		// Token: 0x040010BB RID: 4283
		SeeEnter = 1,
		// Token: 0x040010BC RID: 4284
		SeeExit = 3,
		// Token: 0x040010BD RID: 4285
		SeeAdd = 5,
		// Token: 0x040010BE RID: 4286
		SeeRemove = 7,
		// Token: 0x040010BF RID: 4287
		SpectatedEnter = 2,
		// Token: 0x040010C0 RID: 4288
		SpectatorExit = 4,
		// Token: 0x040010C1 RID: 4289
		SpectatorAdd = 8,
		// Token: 0x040010C2 RID: 4290
		SpectatorRemove = 10
	}
}
