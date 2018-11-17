using System;
using System.Collections.Generic;
using uLink;
using UnityEngine;

// Token: 0x02000399 RID: 921
public sealed class NGCView : MonoBehaviour
{
	// Token: 0x1400000F RID: 15
	// (add) Token: 0x06001DB6 RID: 7606 RVA: 0x0006DE44 File Offset: 0x0006C044
	// (remove) Token: 0x06001DB7 RID: 7607 RVA: 0x0006DE80 File Offset: 0x0006C080
	public event global::NGC.EventCallback OnPreDestroy
	{
		add
		{
			if (this.preDestroying)
			{
				value(this);
			}
			else
			{
				this.onPreDestroy = (global::NGC.EventCallback)Delegate.Combine(this.onPreDestroy, value);
			}
		}
		remove
		{
			this.onPreDestroy = (global::NGC.EventCallback)Delegate.Remove(this.onPreDestroy, value);
		}
	}

	// Token: 0x06001DB8 RID: 7608 RVA: 0x0006DE9C File Offset: 0x0006C09C
	public void RPC<P0, P1>(NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1)
	{
		this.RPC<P0, P1>(flags, messageName, rpcMode, global::NGC.BlockArgs<P0, P1>(p0, p1));
	}

	// Token: 0x06001DB9 RID: 7609 RVA: 0x0006DEB0 File Offset: 0x0006C0B0
	public void RPC<P0, P1>(NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, global::NGC.callf<P0, P1>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, rpcMode, bitStream);
	}

	// Token: 0x06001DBA RID: 7610 RVA: 0x0006DEDC File Offset: 0x0006C0DC
	public void RPC<P0, P1>(NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1)
	{
		this.RPC<P0, P1>(flags, messageName, target, global::NGC.BlockArgs<P0, P1>(p0, p1));
	}

	// Token: 0x06001DBB RID: 7611 RVA: 0x0006DEF0 File Offset: 0x0006C0F0
	public void RPC<P0, P1>(NetworkFlags flags, string messageName, uLink.NetworkPlayer target, global::NGC.callf<P0, P1>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, target, bitStream);
	}

	// Token: 0x06001DBC RID: 7612 RVA: 0x0006DF1C File Offset: 0x0006C11C
	public void RPC<P0, P1>(NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1)
	{
		this.RPC<P0, P1>(flags, messageName, targets, global::NGC.BlockArgs<P0, P1>(p0, p1));
	}

	// Token: 0x06001DBD RID: 7613 RVA: 0x0006DF30 File Offset: 0x0006C130
	public void RPC<P0, P1>(NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, global::NGC.callf<P0, P1>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, targets, bitStream);
	}

	// Token: 0x06001DBE RID: 7614 RVA: 0x0006DF5C File Offset: 0x0006C15C
	public void RPC<P0, P1>(string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1)
	{
		this.RPC<P0, P1>(messageName, rpcMode, global::NGC.BlockArgs<P0, P1>(p0, p1));
	}

	// Token: 0x06001DBF RID: 7615 RVA: 0x0006DF70 File Offset: 0x0006C170
	public void RPC<P0, P1>(string messageName, uLink.RPCMode rpcMode, global::NGC.callf<P0, P1>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, rpcMode, bitStream);
	}

	// Token: 0x06001DC0 RID: 7616 RVA: 0x0006DF9C File Offset: 0x0006C19C
	public void RPC<P0, P1>(string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1)
	{
		this.RPC<P0, P1>(messageName, target, global::NGC.BlockArgs<P0, P1>(p0, p1));
	}

	// Token: 0x06001DC1 RID: 7617 RVA: 0x0006DFB0 File Offset: 0x0006C1B0
	public void RPC<P0, P1>(string messageName, uLink.NetworkPlayer target, global::NGC.callf<P0, P1>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, target, bitStream);
	}

	// Token: 0x06001DC2 RID: 7618 RVA: 0x0006DFDC File Offset: 0x0006C1DC
	public void RPC<P0, P1>(string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1)
	{
		this.RPC<P0, P1>(messageName, targets, global::NGC.BlockArgs<P0, P1>(p0, p1));
	}

	// Token: 0x06001DC3 RID: 7619 RVA: 0x0006DFF0 File Offset: 0x0006C1F0
	public void RPC<P0, P1>(string messageName, IEnumerable<uLink.NetworkPlayer> targets, global::NGC.callf<P0, P1>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, targets, bitStream);
	}

	// Token: 0x06001DC4 RID: 7620 RVA: 0x0006E01C File Offset: 0x0006C21C
	public void RPC<P0, P1, P2>(NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2)
	{
		this.RPC<P0, P1, P2>(flags, messageName, rpcMode, global::NGC.BlockArgs<P0, P1, P2>(p0, p1, p2));
	}

	// Token: 0x06001DC5 RID: 7621 RVA: 0x0006E034 File Offset: 0x0006C234
	public void RPC<P0, P1, P2>(NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, global::NGC.callf<P0, P1, P2>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, rpcMode, bitStream);
	}

	// Token: 0x06001DC6 RID: 7622 RVA: 0x0006E060 File Offset: 0x0006C260
	public void RPC<P0, P1, P2>(NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2)
	{
		this.RPC<P0, P1, P2>(flags, messageName, target, global::NGC.BlockArgs<P0, P1, P2>(p0, p1, p2));
	}

	// Token: 0x06001DC7 RID: 7623 RVA: 0x0006E078 File Offset: 0x0006C278
	public void RPC<P0, P1, P2>(NetworkFlags flags, string messageName, uLink.NetworkPlayer target, global::NGC.callf<P0, P1, P2>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, target, bitStream);
	}

	// Token: 0x06001DC8 RID: 7624 RVA: 0x0006E0A4 File Offset: 0x0006C2A4
	public void RPC<P0, P1, P2>(NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2)
	{
		this.RPC<P0, P1, P2>(flags, messageName, targets, global::NGC.BlockArgs<P0, P1, P2>(p0, p1, p2));
	}

	// Token: 0x06001DC9 RID: 7625 RVA: 0x0006E0BC File Offset: 0x0006C2BC
	public void RPC<P0, P1, P2>(NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, global::NGC.callf<P0, P1, P2>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, targets, bitStream);
	}

	// Token: 0x06001DCA RID: 7626 RVA: 0x0006E0E8 File Offset: 0x0006C2E8
	public void RPC<P0, P1, P2>(string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2)
	{
		this.RPC<P0, P1, P2>(messageName, rpcMode, global::NGC.BlockArgs<P0, P1, P2>(p0, p1, p2));
	}

	// Token: 0x06001DCB RID: 7627 RVA: 0x0006E0FC File Offset: 0x0006C2FC
	public void RPC<P0, P1, P2>(string messageName, uLink.RPCMode rpcMode, global::NGC.callf<P0, P1, P2>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, rpcMode, bitStream);
	}

	// Token: 0x06001DCC RID: 7628 RVA: 0x0006E128 File Offset: 0x0006C328
	public void RPC<P0, P1, P2>(string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2)
	{
		this.RPC<P0, P1, P2>(messageName, target, global::NGC.BlockArgs<P0, P1, P2>(p0, p1, p2));
	}

	// Token: 0x06001DCD RID: 7629 RVA: 0x0006E13C File Offset: 0x0006C33C
	public void RPC<P0, P1, P2>(string messageName, uLink.NetworkPlayer target, global::NGC.callf<P0, P1, P2>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, target, bitStream);
	}

	// Token: 0x06001DCE RID: 7630 RVA: 0x0006E168 File Offset: 0x0006C368
	public void RPC<P0, P1, P2>(string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2)
	{
		this.RPC<P0, P1, P2>(messageName, targets, global::NGC.BlockArgs<P0, P1, P2>(p0, p1, p2));
	}

	// Token: 0x06001DCF RID: 7631 RVA: 0x0006E17C File Offset: 0x0006C37C
	public void RPC<P0, P1, P2>(string messageName, IEnumerable<uLink.NetworkPlayer> targets, global::NGC.callf<P0, P1, P2>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, targets, bitStream);
	}

	// Token: 0x06001DD0 RID: 7632 RVA: 0x0006E1A8 File Offset: 0x0006C3A8
	public void RPC<P0, P1, P2, P3>(NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		this.RPC<P0, P1, P2, P3>(flags, messageName, rpcMode, global::NGC.BlockArgs<P0, P1, P2, P3>(p0, p1, p2, p3));
	}

	// Token: 0x06001DD1 RID: 7633 RVA: 0x0006E1CC File Offset: 0x0006C3CC
	public void RPC<P0, P1, P2, P3>(NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, global::NGC.callf<P0, P1, P2, P3>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, rpcMode, bitStream);
	}

	// Token: 0x06001DD2 RID: 7634 RVA: 0x0006E1F8 File Offset: 0x0006C3F8
	public void RPC<P0, P1, P2, P3>(NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		this.RPC<P0, P1, P2, P3>(flags, messageName, target, global::NGC.BlockArgs<P0, P1, P2, P3>(p0, p1, p2, p3));
	}

	// Token: 0x06001DD3 RID: 7635 RVA: 0x0006E21C File Offset: 0x0006C41C
	public void RPC<P0, P1, P2, P3>(NetworkFlags flags, string messageName, uLink.NetworkPlayer target, global::NGC.callf<P0, P1, P2, P3>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, target, bitStream);
	}

	// Token: 0x06001DD4 RID: 7636 RVA: 0x0006E248 File Offset: 0x0006C448
	public void RPC<P0, P1, P2, P3>(NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		this.RPC<P0, P1, P2, P3>(flags, messageName, targets, global::NGC.BlockArgs<P0, P1, P2, P3>(p0, p1, p2, p3));
	}

	// Token: 0x06001DD5 RID: 7637 RVA: 0x0006E26C File Offset: 0x0006C46C
	public void RPC<P0, P1, P2, P3>(NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, global::NGC.callf<P0, P1, P2, P3>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, targets, bitStream);
	}

	// Token: 0x06001DD6 RID: 7638 RVA: 0x0006E298 File Offset: 0x0006C498
	public void RPC<P0, P1, P2, P3>(string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		this.RPC<P0, P1, P2, P3>(messageName, rpcMode, global::NGC.BlockArgs<P0, P1, P2, P3>(p0, p1, p2, p3));
	}

	// Token: 0x06001DD7 RID: 7639 RVA: 0x0006E2B0 File Offset: 0x0006C4B0
	public void RPC<P0, P1, P2, P3>(string messageName, uLink.RPCMode rpcMode, global::NGC.callf<P0, P1, P2, P3>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, rpcMode, bitStream);
	}

	// Token: 0x06001DD8 RID: 7640 RVA: 0x0006E2DC File Offset: 0x0006C4DC
	public void RPC<P0, P1, P2, P3>(string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		this.RPC<P0, P1, P2, P3>(messageName, target, global::NGC.BlockArgs<P0, P1, P2, P3>(p0, p1, p2, p3));
	}

	// Token: 0x06001DD9 RID: 7641 RVA: 0x0006E2F4 File Offset: 0x0006C4F4
	public void RPC<P0, P1, P2, P3>(string messageName, uLink.NetworkPlayer target, global::NGC.callf<P0, P1, P2, P3>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, target, bitStream);
	}

	// Token: 0x06001DDA RID: 7642 RVA: 0x0006E320 File Offset: 0x0006C520
	public void RPC<P0, P1, P2, P3>(string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		this.RPC<P0, P1, P2, P3>(messageName, targets, global::NGC.BlockArgs<P0, P1, P2, P3>(p0, p1, p2, p3));
	}

	// Token: 0x06001DDB RID: 7643 RVA: 0x0006E338 File Offset: 0x0006C538
	public void RPC<P0, P1, P2, P3>(string messageName, IEnumerable<uLink.NetworkPlayer> targets, global::NGC.callf<P0, P1, P2, P3>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, targets, bitStream);
	}

	// Token: 0x06001DDC RID: 7644 RVA: 0x0006E364 File Offset: 0x0006C564
	public void RPC<P0, P1, P2, P3, P4>(NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		this.RPC<P0, P1, P2, P3, P4>(flags, messageName, rpcMode, global::NGC.BlockArgs<P0, P1, P2, P3, P4>(p0, p1, p2, p3, p4));
	}

	// Token: 0x06001DDD RID: 7645 RVA: 0x0006E38C File Offset: 0x0006C58C
	public void RPC<P0, P1, P2, P3, P4>(NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, global::NGC.callf<P0, P1, P2, P3, P4>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3, P4>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, rpcMode, bitStream);
	}

	// Token: 0x06001DDE RID: 7646 RVA: 0x0006E3B8 File Offset: 0x0006C5B8
	public void RPC<P0, P1, P2, P3, P4>(NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		this.RPC<P0, P1, P2, P3, P4>(flags, messageName, target, global::NGC.BlockArgs<P0, P1, P2, P3, P4>(p0, p1, p2, p3, p4));
	}

	// Token: 0x06001DDF RID: 7647 RVA: 0x0006E3E0 File Offset: 0x0006C5E0
	public void RPC<P0, P1, P2, P3, P4>(NetworkFlags flags, string messageName, uLink.NetworkPlayer target, global::NGC.callf<P0, P1, P2, P3, P4>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3, P4>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, target, bitStream);
	}

	// Token: 0x06001DE0 RID: 7648 RVA: 0x0006E40C File Offset: 0x0006C60C
	public void RPC<P0, P1, P2, P3, P4>(NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		this.RPC<P0, P1, P2, P3, P4>(flags, messageName, targets, global::NGC.BlockArgs<P0, P1, P2, P3, P4>(p0, p1, p2, p3, p4));
	}

	// Token: 0x06001DE1 RID: 7649 RVA: 0x0006E434 File Offset: 0x0006C634
	public void RPC<P0, P1, P2, P3, P4>(NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, global::NGC.callf<P0, P1, P2, P3, P4>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3, P4>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, targets, bitStream);
	}

	// Token: 0x06001DE2 RID: 7650 RVA: 0x0006E460 File Offset: 0x0006C660
	public void RPC<P0, P1, P2, P3, P4>(string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		this.RPC<P0, P1, P2, P3, P4>(messageName, rpcMode, global::NGC.BlockArgs<P0, P1, P2, P3, P4>(p0, p1, p2, p3, p4));
	}

	// Token: 0x06001DE3 RID: 7651 RVA: 0x0006E484 File Offset: 0x0006C684
	public void RPC<P0, P1, P2, P3, P4>(string messageName, uLink.RPCMode rpcMode, global::NGC.callf<P0, P1, P2, P3, P4>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3, P4>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, rpcMode, bitStream);
	}

	// Token: 0x06001DE4 RID: 7652 RVA: 0x0006E4B0 File Offset: 0x0006C6B0
	public void RPC<P0, P1, P2, P3, P4>(string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		this.RPC<P0, P1, P2, P3, P4>(messageName, target, global::NGC.BlockArgs<P0, P1, P2, P3, P4>(p0, p1, p2, p3, p4));
	}

	// Token: 0x06001DE5 RID: 7653 RVA: 0x0006E4D4 File Offset: 0x0006C6D4
	public void RPC<P0, P1, P2, P3, P4>(string messageName, uLink.NetworkPlayer target, global::NGC.callf<P0, P1, P2, P3, P4>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3, P4>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, target, bitStream);
	}

	// Token: 0x06001DE6 RID: 7654 RVA: 0x0006E500 File Offset: 0x0006C700
	public void RPC<P0, P1, P2, P3, P4>(string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		this.RPC<P0, P1, P2, P3, P4>(messageName, targets, global::NGC.BlockArgs<P0, P1, P2, P3, P4>(p0, p1, p2, p3, p4));
	}

	// Token: 0x06001DE7 RID: 7655 RVA: 0x0006E524 File Offset: 0x0006C724
	public void RPC<P0, P1, P2, P3, P4>(string messageName, IEnumerable<uLink.NetworkPlayer> targets, global::NGC.callf<P0, P1, P2, P3, P4>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3, P4>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, targets, bitStream);
	}

	// Token: 0x06001DE8 RID: 7656 RVA: 0x0006E550 File Offset: 0x0006C750
	public void RPC<P0, P1, P2, P3, P4, P5>(NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		this.RPC<P0, P1, P2, P3, P4, P5>(flags, messageName, rpcMode, global::NGC.BlockArgs<P0, P1, P2, P3, P4, P5>(p0, p1, p2, p3, p4, p5));
	}

	// Token: 0x06001DE9 RID: 7657 RVA: 0x0006E578 File Offset: 0x0006C778
	public void RPC<P0, P1, P2, P3, P4, P5>(NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, global::NGC.callf<P0, P1, P2, P3, P4, P5>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3, P4, P5>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, rpcMode, bitStream);
	}

	// Token: 0x06001DEA RID: 7658 RVA: 0x0006E5A4 File Offset: 0x0006C7A4
	public void RPC<P0, P1, P2, P3, P4, P5>(NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		this.RPC<P0, P1, P2, P3, P4, P5>(flags, messageName, target, global::NGC.BlockArgs<P0, P1, P2, P3, P4, P5>(p0, p1, p2, p3, p4, p5));
	}

	// Token: 0x06001DEB RID: 7659 RVA: 0x0006E5CC File Offset: 0x0006C7CC
	public void RPC<P0, P1, P2, P3, P4, P5>(NetworkFlags flags, string messageName, uLink.NetworkPlayer target, global::NGC.callf<P0, P1, P2, P3, P4, P5>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3, P4, P5>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, target, bitStream);
	}

	// Token: 0x06001DEC RID: 7660 RVA: 0x0006E5F8 File Offset: 0x0006C7F8
	public void RPC<P0, P1, P2, P3, P4, P5>(NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		this.RPC<P0, P1, P2, P3, P4, P5>(flags, messageName, targets, global::NGC.BlockArgs<P0, P1, P2, P3, P4, P5>(p0, p1, p2, p3, p4, p5));
	}

	// Token: 0x06001DED RID: 7661 RVA: 0x0006E620 File Offset: 0x0006C820
	public void RPC<P0, P1, P2, P3, P4, P5>(NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, global::NGC.callf<P0, P1, P2, P3, P4, P5>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3, P4, P5>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, targets, bitStream);
	}

	// Token: 0x06001DEE RID: 7662 RVA: 0x0006E64C File Offset: 0x0006C84C
	public void RPC<P0, P1, P2, P3, P4, P5>(string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		this.RPC<P0, P1, P2, P3, P4, P5>(messageName, rpcMode, global::NGC.BlockArgs<P0, P1, P2, P3, P4, P5>(p0, p1, p2, p3, p4, p5));
	}

	// Token: 0x06001DEF RID: 7663 RVA: 0x0006E674 File Offset: 0x0006C874
	public void RPC<P0, P1, P2, P3, P4, P5>(string messageName, uLink.RPCMode rpcMode, global::NGC.callf<P0, P1, P2, P3, P4, P5>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3, P4, P5>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, rpcMode, bitStream);
	}

	// Token: 0x06001DF0 RID: 7664 RVA: 0x0006E6A0 File Offset: 0x0006C8A0
	public void RPC<P0, P1, P2, P3, P4, P5>(string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		this.RPC<P0, P1, P2, P3, P4, P5>(messageName, target, global::NGC.BlockArgs<P0, P1, P2, P3, P4, P5>(p0, p1, p2, p3, p4, p5));
	}

	// Token: 0x06001DF1 RID: 7665 RVA: 0x0006E6C8 File Offset: 0x0006C8C8
	public void RPC<P0, P1, P2, P3, P4, P5>(string messageName, uLink.NetworkPlayer target, global::NGC.callf<P0, P1, P2, P3, P4, P5>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3, P4, P5>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, target, bitStream);
	}

	// Token: 0x06001DF2 RID: 7666 RVA: 0x0006E6F4 File Offset: 0x0006C8F4
	public void RPC<P0, P1, P2, P3, P4, P5>(string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		this.RPC<P0, P1, P2, P3, P4, P5>(messageName, targets, global::NGC.BlockArgs<P0, P1, P2, P3, P4, P5>(p0, p1, p2, p3, p4, p5));
	}

	// Token: 0x06001DF3 RID: 7667 RVA: 0x0006E71C File Offset: 0x0006C91C
	public void RPC<P0, P1, P2, P3, P4, P5>(string messageName, IEnumerable<uLink.NetworkPlayer> targets, global::NGC.callf<P0, P1, P2, P3, P4, P5>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3, P4, P5>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, targets, bitStream);
	}

	// Token: 0x06001DF4 RID: 7668 RVA: 0x0006E748 File Offset: 0x0006C948
	public void RPC<P0, P1, P2, P3, P4, P5, P6>(NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6>(flags, messageName, rpcMode, global::NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6>(p0, p1, p2, p3, p4, p5, p6));
	}

	// Token: 0x06001DF5 RID: 7669 RVA: 0x0006E774 File Offset: 0x0006C974
	public void RPC<P0, P1, P2, P3, P4, P5, P6>(NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, global::NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, rpcMode, bitStream);
	}

	// Token: 0x06001DF6 RID: 7670 RVA: 0x0006E7A0 File Offset: 0x0006C9A0
	public void RPC<P0, P1, P2, P3, P4, P5, P6>(NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6>(flags, messageName, target, global::NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6>(p0, p1, p2, p3, p4, p5, p6));
	}

	// Token: 0x06001DF7 RID: 7671 RVA: 0x0006E7CC File Offset: 0x0006C9CC
	public void RPC<P0, P1, P2, P3, P4, P5, P6>(NetworkFlags flags, string messageName, uLink.NetworkPlayer target, global::NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, target, bitStream);
	}

	// Token: 0x06001DF8 RID: 7672 RVA: 0x0006E7F8 File Offset: 0x0006C9F8
	public void RPC<P0, P1, P2, P3, P4, P5, P6>(NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6>(flags, messageName, targets, global::NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6>(p0, p1, p2, p3, p4, p5, p6));
	}

	// Token: 0x06001DF9 RID: 7673 RVA: 0x0006E824 File Offset: 0x0006CA24
	public void RPC<P0, P1, P2, P3, P4, P5, P6>(NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, global::NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, targets, bitStream);
	}

	// Token: 0x06001DFA RID: 7674 RVA: 0x0006E850 File Offset: 0x0006CA50
	public void RPC<P0, P1, P2, P3, P4, P5, P6>(string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6>(messageName, rpcMode, global::NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6>(p0, p1, p2, p3, p4, p5, p6));
	}

	// Token: 0x06001DFB RID: 7675 RVA: 0x0006E878 File Offset: 0x0006CA78
	public void RPC<P0, P1, P2, P3, P4, P5, P6>(string messageName, uLink.RPCMode rpcMode, global::NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, rpcMode, bitStream);
	}

	// Token: 0x06001DFC RID: 7676 RVA: 0x0006E8A4 File Offset: 0x0006CAA4
	public void RPC<P0, P1, P2, P3, P4, P5, P6>(string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6>(messageName, target, global::NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6>(p0, p1, p2, p3, p4, p5, p6));
	}

	// Token: 0x06001DFD RID: 7677 RVA: 0x0006E8CC File Offset: 0x0006CACC
	public void RPC<P0, P1, P2, P3, P4, P5, P6>(string messageName, uLink.NetworkPlayer target, global::NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, target, bitStream);
	}

	// Token: 0x06001DFE RID: 7678 RVA: 0x0006E8F8 File Offset: 0x0006CAF8
	public void RPC<P0, P1, P2, P3, P4, P5, P6>(string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6>(messageName, targets, global::NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6>(p0, p1, p2, p3, p4, p5, p6));
	}

	// Token: 0x06001DFF RID: 7679 RVA: 0x0006E920 File Offset: 0x0006CB20
	public void RPC<P0, P1, P2, P3, P4, P5, P6>(string messageName, IEnumerable<uLink.NetworkPlayer> targets, global::NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, targets, bitStream);
	}

	// Token: 0x06001E00 RID: 7680 RVA: 0x0006E94C File Offset: 0x0006CB4C
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7>(NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(flags, messageName, rpcMode, global::NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7>(p0, p1, p2, p3, p4, p5, p6, p7));
	}

	// Token: 0x06001E01 RID: 7681 RVA: 0x0006E978 File Offset: 0x0006CB78
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7>(NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, rpcMode, bitStream);
	}

	// Token: 0x06001E02 RID: 7682 RVA: 0x0006E9A4 File Offset: 0x0006CBA4
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7>(NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(flags, messageName, target, global::NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7>(p0, p1, p2, p3, p4, p5, p6, p7));
	}

	// Token: 0x06001E03 RID: 7683 RVA: 0x0006E9D0 File Offset: 0x0006CBD0
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7>(NetworkFlags flags, string messageName, uLink.NetworkPlayer target, global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, target, bitStream);
	}

	// Token: 0x06001E04 RID: 7684 RVA: 0x0006E9FC File Offset: 0x0006CBFC
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7>(NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(flags, messageName, targets, global::NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7>(p0, p1, p2, p3, p4, p5, p6, p7));
	}

	// Token: 0x06001E05 RID: 7685 RVA: 0x0006EA28 File Offset: 0x0006CC28
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7>(NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, targets, bitStream);
	}

	// Token: 0x06001E06 RID: 7686 RVA: 0x0006EA54 File Offset: 0x0006CC54
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7>(string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(messageName, rpcMode, global::NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7>(p0, p1, p2, p3, p4, p5, p6, p7));
	}

	// Token: 0x06001E07 RID: 7687 RVA: 0x0006EA80 File Offset: 0x0006CC80
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7>(string messageName, uLink.RPCMode rpcMode, global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, rpcMode, bitStream);
	}

	// Token: 0x06001E08 RID: 7688 RVA: 0x0006EAAC File Offset: 0x0006CCAC
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7>(string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(messageName, target, global::NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7>(p0, p1, p2, p3, p4, p5, p6, p7));
	}

	// Token: 0x06001E09 RID: 7689 RVA: 0x0006EAD8 File Offset: 0x0006CCD8
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7>(string messageName, uLink.NetworkPlayer target, global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, target, bitStream);
	}

	// Token: 0x06001E0A RID: 7690 RVA: 0x0006EB04 File Offset: 0x0006CD04
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7>(string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(messageName, targets, global::NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7>(p0, p1, p2, p3, p4, p5, p6, p7));
	}

	// Token: 0x06001E0B RID: 7691 RVA: 0x0006EB30 File Offset: 0x0006CD30
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7>(string messageName, IEnumerable<uLink.NetworkPlayer> targets, global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, targets, bitStream);
	}

	// Token: 0x06001E0C RID: 7692 RVA: 0x0006EB5C File Offset: 0x0006CD5C
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(flags, messageName, rpcMode, global::NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8>(p0, p1, p2, p3, p4, p5, p6, p7, p8));
	}

	// Token: 0x06001E0D RID: 7693 RVA: 0x0006EB8C File Offset: 0x0006CD8C
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, rpcMode, bitStream);
	}

	// Token: 0x06001E0E RID: 7694 RVA: 0x0006EBB8 File Offset: 0x0006CDB8
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(flags, messageName, target, global::NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8>(p0, p1, p2, p3, p4, p5, p6, p7, p8));
	}

	// Token: 0x06001E0F RID: 7695 RVA: 0x0006EBE8 File Offset: 0x0006CDE8
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(NetworkFlags flags, string messageName, uLink.NetworkPlayer target, global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, target, bitStream);
	}

	// Token: 0x06001E10 RID: 7696 RVA: 0x0006EC14 File Offset: 0x0006CE14
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(flags, messageName, targets, global::NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8>(p0, p1, p2, p3, p4, p5, p6, p7, p8));
	}

	// Token: 0x06001E11 RID: 7697 RVA: 0x0006EC44 File Offset: 0x0006CE44
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, targets, bitStream);
	}

	// Token: 0x06001E12 RID: 7698 RVA: 0x0006EC70 File Offset: 0x0006CE70
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(messageName, rpcMode, global::NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8>(p0, p1, p2, p3, p4, p5, p6, p7, p8));
	}

	// Token: 0x06001E13 RID: 7699 RVA: 0x0006EC9C File Offset: 0x0006CE9C
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(string messageName, uLink.RPCMode rpcMode, global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, rpcMode, bitStream);
	}

	// Token: 0x06001E14 RID: 7700 RVA: 0x0006ECC8 File Offset: 0x0006CEC8
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(messageName, target, global::NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8>(p0, p1, p2, p3, p4, p5, p6, p7, p8));
	}

	// Token: 0x06001E15 RID: 7701 RVA: 0x0006ECF4 File Offset: 0x0006CEF4
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(string messageName, uLink.NetworkPlayer target, global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, target, bitStream);
	}

	// Token: 0x06001E16 RID: 7702 RVA: 0x0006ED20 File Offset: 0x0006CF20
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(messageName, targets, global::NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8>(p0, p1, p2, p3, p4, p5, p6, p7, p8));
	}

	// Token: 0x06001E17 RID: 7703 RVA: 0x0006ED4C File Offset: 0x0006CF4C
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(string messageName, IEnumerable<uLink.NetworkPlayer> targets, global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, targets, bitStream);
	}

	// Token: 0x06001E18 RID: 7704 RVA: 0x0006ED78 File Offset: 0x0006CF78
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(flags, messageName, rpcMode, global::NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(p0, p1, p2, p3, p4, p5, p6, p7, p8, p9));
	}

	// Token: 0x06001E19 RID: 7705 RVA: 0x0006EDA8 File Offset: 0x0006CFA8
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, rpcMode, bitStream);
	}

	// Token: 0x06001E1A RID: 7706 RVA: 0x0006EDD4 File Offset: 0x0006CFD4
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(flags, messageName, target, global::NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(p0, p1, p2, p3, p4, p5, p6, p7, p8, p9));
	}

	// Token: 0x06001E1B RID: 7707 RVA: 0x0006EE04 File Offset: 0x0006D004
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(NetworkFlags flags, string messageName, uLink.NetworkPlayer target, global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, target, bitStream);
	}

	// Token: 0x06001E1C RID: 7708 RVA: 0x0006EE30 File Offset: 0x0006D030
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(flags, messageName, targets, global::NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(p0, p1, p2, p3, p4, p5, p6, p7, p8, p9));
	}

	// Token: 0x06001E1D RID: 7709 RVA: 0x0006EE60 File Offset: 0x0006D060
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, targets, bitStream);
	}

	// Token: 0x06001E1E RID: 7710 RVA: 0x0006EE8C File Offset: 0x0006D08C
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(messageName, rpcMode, global::NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(p0, p1, p2, p3, p4, p5, p6, p7, p8, p9));
	}

	// Token: 0x06001E1F RID: 7711 RVA: 0x0006EEBC File Offset: 0x0006D0BC
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(string messageName, uLink.RPCMode rpcMode, global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, rpcMode, bitStream);
	}

	// Token: 0x06001E20 RID: 7712 RVA: 0x0006EEE8 File Offset: 0x0006D0E8
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(messageName, target, global::NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(p0, p1, p2, p3, p4, p5, p6, p7, p8, p9));
	}

	// Token: 0x06001E21 RID: 7713 RVA: 0x0006EF18 File Offset: 0x0006D118
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(string messageName, uLink.NetworkPlayer target, global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, target, bitStream);
	}

	// Token: 0x06001E22 RID: 7714 RVA: 0x0006EF44 File Offset: 0x0006D144
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(messageName, targets, global::NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(p0, p1, p2, p3, p4, p5, p6, p7, p8, p9));
	}

	// Token: 0x06001E23 RID: 7715 RVA: 0x0006EF74 File Offset: 0x0006D174
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(string messageName, IEnumerable<uLink.NetworkPlayer> targets, global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, targets, bitStream);
	}

	// Token: 0x06001E24 RID: 7716 RVA: 0x0006EFA0 File Offset: 0x0006D1A0
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(flags, messageName, rpcMode, global::NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10));
	}

	// Token: 0x06001E25 RID: 7717 RVA: 0x0006EFD4 File Offset: 0x0006D1D4
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, rpcMode, bitStream);
	}

	// Token: 0x06001E26 RID: 7718 RVA: 0x0006F000 File Offset: 0x0006D200
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(flags, messageName, target, global::NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10));
	}

	// Token: 0x06001E27 RID: 7719 RVA: 0x0006F034 File Offset: 0x0006D234
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(NetworkFlags flags, string messageName, uLink.NetworkPlayer target, global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, target, bitStream);
	}

	// Token: 0x06001E28 RID: 7720 RVA: 0x0006F060 File Offset: 0x0006D260
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(flags, messageName, targets, global::NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10));
	}

	// Token: 0x06001E29 RID: 7721 RVA: 0x0006F094 File Offset: 0x0006D294
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, targets, bitStream);
	}

	// Token: 0x06001E2A RID: 7722 RVA: 0x0006F0C0 File Offset: 0x0006D2C0
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(messageName, rpcMode, global::NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10));
	}

	// Token: 0x06001E2B RID: 7723 RVA: 0x0006F0F0 File Offset: 0x0006D2F0
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(string messageName, uLink.RPCMode rpcMode, global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, rpcMode, bitStream);
	}

	// Token: 0x06001E2C RID: 7724 RVA: 0x0006F11C File Offset: 0x0006D31C
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(messageName, target, global::NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10));
	}

	// Token: 0x06001E2D RID: 7725 RVA: 0x0006F14C File Offset: 0x0006D34C
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(string messageName, uLink.NetworkPlayer target, global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, target, bitStream);
	}

	// Token: 0x06001E2E RID: 7726 RVA: 0x0006F178 File Offset: 0x0006D378
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(messageName, targets, global::NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10));
	}

	// Token: 0x06001E2F RID: 7727 RVA: 0x0006F1A8 File Offset: 0x0006D3A8
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(string messageName, IEnumerable<uLink.NetworkPlayer> targets, global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, targets, bitStream);
	}

	// Token: 0x06001E30 RID: 7728 RVA: 0x0006F1D4 File Offset: 0x0006D3D4
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(flags, messageName, rpcMode, global::NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11));
	}

	// Token: 0x06001E31 RID: 7729 RVA: 0x0006F208 File Offset: 0x0006D408
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(NetworkFlags flags, string messageName, uLink.RPCMode rpcMode, global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, rpcMode, bitStream);
	}

	// Token: 0x06001E32 RID: 7730 RVA: 0x0006F234 File Offset: 0x0006D434
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(NetworkFlags flags, string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(flags, messageName, target, global::NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11));
	}

	// Token: 0x06001E33 RID: 7731 RVA: 0x0006F268 File Offset: 0x0006D468
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(NetworkFlags flags, string messageName, uLink.NetworkPlayer target, global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, target, bitStream);
	}

	// Token: 0x06001E34 RID: 7732 RVA: 0x0006F294 File Offset: 0x0006D494
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(flags, messageName, targets, global::NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11));
	}

	// Token: 0x06001E35 RID: 7733 RVA: 0x0006F2C8 File Offset: 0x0006D4C8
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(NetworkFlags flags, string messageName, IEnumerable<uLink.NetworkPlayer> targets, global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, targets, bitStream);
	}

	// Token: 0x06001E36 RID: 7734 RVA: 0x0006F2F4 File Offset: 0x0006D4F4
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(string messageName, uLink.RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(messageName, rpcMode, global::NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11));
	}

	// Token: 0x06001E37 RID: 7735 RVA: 0x0006F328 File Offset: 0x0006D528
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(string messageName, uLink.RPCMode rpcMode, global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, rpcMode, bitStream);
	}

	// Token: 0x06001E38 RID: 7736 RVA: 0x0006F354 File Offset: 0x0006D554
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(string messageName, uLink.NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(messageName, target, global::NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11));
	}

	// Token: 0x06001E39 RID: 7737 RVA: 0x0006F388 File Offset: 0x0006D588
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(string messageName, uLink.NetworkPlayer target, global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, target, bitStream);
	}

	// Token: 0x06001E3A RID: 7738 RVA: 0x0006F3B4 File Offset: 0x0006D5B4
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(string messageName, IEnumerable<uLink.NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(messageName, targets, global::NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11));
	}

	// Token: 0x06001E3B RID: 7739 RVA: 0x0006F3E8 File Offset: 0x0006D5E8
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(string messageName, IEnumerable<uLink.NetworkPlayer> targets, global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, targets, bitStream);
	}

	// Token: 0x170007A8 RID: 1960
	// (get) Token: 0x06001E3C RID: 7740 RVA: 0x0006F414 File Offset: 0x0006D614
	internal int id
	{
		get
		{
			if (this.innerID <= 0 || !this.outer)
			{
				return 0;
			}
			return global::NGC.PackID((int)this.outer.groupNumber, (int)this.innerID);
		}
	}

	// Token: 0x170007A9 RID: 1961
	// (get) Token: 0x06001E3D RID: 7741 RVA: 0x0006F458 File Offset: 0x0006D658
	public global::NetEntityID entityID
	{
		get
		{
			return new global::NetEntityID(this);
		}
	}

	// Token: 0x170007AA RID: 1962
	// (get) Token: 0x06001E3E RID: 7742 RVA: 0x0006F460 File Offset: 0x0006D660
	public Vector3 creationPosition
	{
		get
		{
			return this.spawnPosition;
		}
	}

	// Token: 0x170007AB RID: 1963
	// (get) Token: 0x06001E3F RID: 7743 RVA: 0x0006F468 File Offset: 0x0006D668
	public Quaternion creationRotation
	{
		get
		{
			return this.spawnRotation;
		}
	}

	// Token: 0x06001E40 RID: 7744 RVA: 0x0006F470 File Offset: 0x0006D670
	internal void PostInstantiate()
	{
		base.BroadcastMessage("NGC_OnInstantiate", this, 1);
	}

	// Token: 0x06001E41 RID: 7745 RVA: 0x0006F480 File Offset: 0x0006D680
	internal void PreDestroy()
	{
		if (!this.preDestroying)
		{
			this.preDestroying = true;
			if (this.onPreDestroy != null)
			{
				global::NGC.EventCallback eventCallback = this.onPreDestroy;
				this.onPreDestroy = null;
				try
				{
					eventCallback(this);
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
				}
			}
		}
	}

	// Token: 0x06001E42 RID: 7746 RVA: 0x0006F4EC File Offset: 0x0006D6EC
	private global::NGC EnsureCall()
	{
		return this.outer;
	}

	// Token: 0x06001E43 RID: 7747 RVA: 0x0006F4F4 File Offset: 0x0006D6F4
	public void RPC(NetworkFlags flags, string message, uLink.RPCMode mode)
	{
		this.EnsureCall().NGCViewRPC(flags, mode, this, this.prefab.MessageIndex(message), null, 0, 0);
	}

	// Token: 0x06001E44 RID: 7748 RVA: 0x0006F520 File Offset: 0x0006D720
	public void RPC(NetworkFlags flags, string message, uLink.NetworkPlayer target)
	{
		this.EnsureCall().NGCViewRPC(flags, target, this, this.prefab.MessageIndex(message), null, 0, 0);
	}

	// Token: 0x06001E45 RID: 7749 RVA: 0x0006F54C File Offset: 0x0006D74C
	public void RPC(NetworkFlags flags, string message, IEnumerable<uLink.NetworkPlayer> target)
	{
		this.EnsureCall().NGCViewRPC(flags, target, this, this.prefab.MessageIndex(message), null, 0, 0);
	}

	// Token: 0x06001E46 RID: 7750 RVA: 0x0006F578 File Offset: 0x0006D778
	public void RPC(string message, uLink.RPCMode mode)
	{
		this.EnsureCall().NGCViewRPC(mode, this, this.prefab.MessageIndex(message), null, 0, 0);
	}

	// Token: 0x06001E47 RID: 7751 RVA: 0x0006F5A4 File Offset: 0x0006D7A4
	public void RPC(string message, uLink.NetworkPlayer target)
	{
		this.EnsureCall().NGCViewRPC(target, this, this.prefab.MessageIndex(message), null, 0, 0);
	}

	// Token: 0x06001E48 RID: 7752 RVA: 0x0006F5D0 File Offset: 0x0006D7D0
	public void RPC(string message, IEnumerable<uLink.NetworkPlayer> target)
	{
		this.EnsureCall().NGCViewRPC(target, this, this.prefab.MessageIndex(message), null, 0, 0);
	}

	// Token: 0x06001E49 RID: 7753 RVA: 0x0006F5FC File Offset: 0x0006D7FC
	public void RPC_Bytes(NetworkFlags flags, string message, uLink.RPCMode mode, byte[] data)
	{
		this.EnsureCall().NGCViewRPC(flags, mode, this, this.prefab.MessageIndex(message), data, 0, (data != null) ? data.Length : 0);
	}

	// Token: 0x06001E4A RID: 7754 RVA: 0x0006F638 File Offset: 0x0006D838
	public void RPC_Bytes(NetworkFlags flags, string message, uLink.NetworkPlayer target, byte[] data)
	{
		this.EnsureCall().NGCViewRPC(flags, target, this, this.prefab.MessageIndex(message), data, 0, (data != null) ? data.Length : 0);
	}

	// Token: 0x06001E4B RID: 7755 RVA: 0x0006F674 File Offset: 0x0006D874
	public void RPC_Bytes(NetworkFlags flags, string message, IEnumerable<uLink.NetworkPlayer> target, byte[] data)
	{
		this.EnsureCall().NGCViewRPC(flags, target, this, this.prefab.MessageIndex(message), data, 0, (data != null) ? data.Length : 0);
	}

	// Token: 0x06001E4C RID: 7756 RVA: 0x0006F6B0 File Offset: 0x0006D8B0
	public void RPC_Bytes(string message, uLink.RPCMode mode, byte[] data)
	{
		this.EnsureCall().NGCViewRPC(mode, this, this.prefab.MessageIndex(message), data, 0, (data != null) ? data.Length : 0);
	}

	// Token: 0x06001E4D RID: 7757 RVA: 0x0006F6E8 File Offset: 0x0006D8E8
	public void RPC_Bytes(string message, uLink.NetworkPlayer target, byte[] data)
	{
		this.EnsureCall().NGCViewRPC(target, this, this.prefab.MessageIndex(message), data, 0, (data != null) ? data.Length : 0);
	}

	// Token: 0x06001E4E RID: 7758 RVA: 0x0006F720 File Offset: 0x0006D920
	public void RPC_Bytes(string message, IEnumerable<uLink.NetworkPlayer> target, byte[] data)
	{
		this.EnsureCall().NGCViewRPC(target, this, this.prefab.MessageIndex(message), data, 0, (data != null) ? data.Length : 0);
	}

	// Token: 0x06001E4F RID: 7759 RVA: 0x0006F758 File Offset: 0x0006D958
	public void RPC_Stream(NetworkFlags flags, string message, uLink.RPCMode mode, BitStream data)
	{
		this.RPC_Bytes(flags, message, mode, data.GetDataByteArray());
	}

	// Token: 0x06001E50 RID: 7760 RVA: 0x0006F76C File Offset: 0x0006D96C
	public void RPC_Stream(NetworkFlags flags, string message, uLink.NetworkPlayer target, BitStream data)
	{
		this.RPC_Bytes(flags, message, target, data.GetDataByteArray());
	}

	// Token: 0x06001E51 RID: 7761 RVA: 0x0006F780 File Offset: 0x0006D980
	public void RPC_Stream(NetworkFlags flags, string message, IEnumerable<uLink.NetworkPlayer> target, BitStream data)
	{
		this.RPC_Bytes(flags, message, target, data.GetDataByteArray());
	}

	// Token: 0x06001E52 RID: 7762 RVA: 0x0006F794 File Offset: 0x0006D994
	public void RPC_Stream(string message, uLink.RPCMode mode, BitStream data)
	{
		this.RPC_Bytes(message, mode, data.GetDataByteArray());
	}

	// Token: 0x06001E53 RID: 7763 RVA: 0x0006F7A4 File Offset: 0x0006D9A4
	public void RPC_Stream(string message, uLink.NetworkPlayer target, BitStream data)
	{
		this.RPC_Bytes(message, target, data.GetDataByteArray());
	}

	// Token: 0x06001E54 RID: 7764 RVA: 0x0006F7B4 File Offset: 0x0006D9B4
	public void RPC_Stream(string message, IEnumerable<uLink.NetworkPlayer> target, BitStream data)
	{
		this.RPC_Bytes(message, target, data.GetDataByteArray());
	}

	// Token: 0x06001E55 RID: 7765 RVA: 0x0006F7C4 File Offset: 0x0006D9C4
	private static BitStream ToStream<T>(T arg)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<T>(arg, new object[0]);
		return bitStream;
	}

	// Token: 0x06001E56 RID: 7766 RVA: 0x0006F7E8 File Offset: 0x0006D9E8
	public void RPC<T>(NetworkFlags flags, string message, uLink.RPCMode mode, T arg)
	{
		this.RPC_Stream(flags, message, mode, global::NGCView.ToStream<T>(arg));
	}

	// Token: 0x06001E57 RID: 7767 RVA: 0x0006F7FC File Offset: 0x0006D9FC
	public void RPC<T>(NetworkFlags flags, string message, uLink.NetworkPlayer target, T arg)
	{
		this.RPC_Stream(flags, message, target, global::NGCView.ToStream<T>(arg));
	}

	// Token: 0x06001E58 RID: 7768 RVA: 0x0006F810 File Offset: 0x0006DA10
	public void RPC<T>(NetworkFlags flags, string message, IEnumerable<uLink.NetworkPlayer> target, T arg)
	{
		this.RPC_Stream(flags, message, target, global::NGCView.ToStream<T>(arg));
	}

	// Token: 0x06001E59 RID: 7769 RVA: 0x0006F824 File Offset: 0x0006DA24
	public void RPC<T>(string message, uLink.RPCMode mode, T arg)
	{
		this.RPC_Stream(message, mode, global::NGCView.ToStream<T>(arg));
	}

	// Token: 0x06001E5A RID: 7770 RVA: 0x0006F834 File Offset: 0x0006DA34
	public void RPC<T>(string message, uLink.NetworkPlayer target, T arg)
	{
		this.RPC_Stream(message, target, global::NGCView.ToStream<T>(arg));
	}

	// Token: 0x06001E5B RID: 7771 RVA: 0x0006F844 File Offset: 0x0006DA44
	public void RPC<T>(string message, IEnumerable<uLink.NetworkPlayer> target, T arg)
	{
		this.RPC_Stream(message, target, global::NGCView.ToStream<T>(arg));
	}

	// Token: 0x06001E5C RID: 7772 RVA: 0x0006F854 File Offset: 0x0006DA54
	public static global::NGCView Find(int id)
	{
		return global::NGC.Find(id);
	}

	// Token: 0x04000F26 RID: 3878
	[NonSerialized]
	public global::NGC.Prefab prefab;

	// Token: 0x04000F27 RID: 3879
	[NonSerialized]
	public global::NGC outer;

	// Token: 0x04000F28 RID: 3880
	[NonSerialized]
	public ushort innerID;

	// Token: 0x04000F29 RID: 3881
	[NonSerialized]
	public uLink.NetworkMessageInfo creation;

	// Token: 0x04000F2A RID: 3882
	[NonSerialized]
	public BitStream initialData;

	// Token: 0x04000F2B RID: 3883
	[SerializeField]
	internal MonoBehaviour[] scripts;

	// Token: 0x04000F2C RID: 3884
	[NonSerialized]
	internal global::NGC.Prefab.Installation.Instance install;

	// Token: 0x04000F2D RID: 3885
	[NonSerialized]
	internal Vector3 spawnPosition;

	// Token: 0x04000F2E RID: 3886
	[NonSerialized]
	internal Quaternion spawnRotation;

	// Token: 0x04000F2F RID: 3887
	[NonSerialized]
	private global::NGC.EventCallback onPreDestroy;

	// Token: 0x04000F30 RID: 3888
	[NonSerialized]
	private bool preDestroying;
}
