using System;
using System.Collections.Generic;
using uLink;
using UnityEngine;

// Token: 0x020002F2 RID: 754
public sealed class NGCView : MonoBehaviour
{
	// Token: 0x1400000F RID: 15
	// (add) Token: 0x06001A7C RID: 6780 RVA: 0x000693C4 File Offset: 0x000675C4
	// (remove) Token: 0x06001A7D RID: 6781 RVA: 0x00069400 File Offset: 0x00067600
	public event NGC.EventCallback OnPreDestroy
	{
		add
		{
			if (this.preDestroying)
			{
				value(this);
			}
			else
			{
				this.onPreDestroy = (NGC.EventCallback)Delegate.Combine(this.onPreDestroy, value);
			}
		}
		remove
		{
			this.onPreDestroy = (NGC.EventCallback)Delegate.Remove(this.onPreDestroy, value);
		}
	}

	// Token: 0x06001A7E RID: 6782 RVA: 0x0006941C File Offset: 0x0006761C
	public void RPC<P0, P1>(NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1)
	{
		this.RPC<P0, P1>(flags, messageName, rpcMode, NGC.BlockArgs<P0, P1>(p0, p1));
	}

	// Token: 0x06001A7F RID: 6783 RVA: 0x00069430 File Offset: 0x00067630
	public void RPC<P0, P1>(NetworkFlags flags, string messageName, RPCMode rpcMode, NGC.callf<P0, P1>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, rpcMode, bitStream);
	}

	// Token: 0x06001A80 RID: 6784 RVA: 0x0006945C File Offset: 0x0006765C
	public void RPC<P0, P1>(NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1)
	{
		this.RPC<P0, P1>(flags, messageName, target, NGC.BlockArgs<P0, P1>(p0, p1));
	}

	// Token: 0x06001A81 RID: 6785 RVA: 0x00069470 File Offset: 0x00067670
	public void RPC<P0, P1>(NetworkFlags flags, string messageName, NetworkPlayer target, NGC.callf<P0, P1>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, target, bitStream);
	}

	// Token: 0x06001A82 RID: 6786 RVA: 0x0006949C File Offset: 0x0006769C
	public void RPC<P0, P1>(NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1)
	{
		this.RPC<P0, P1>(flags, messageName, targets, NGC.BlockArgs<P0, P1>(p0, p1));
	}

	// Token: 0x06001A83 RID: 6787 RVA: 0x000694B0 File Offset: 0x000676B0
	public void RPC<P0, P1>(NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, NGC.callf<P0, P1>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, targets, bitStream);
	}

	// Token: 0x06001A84 RID: 6788 RVA: 0x000694DC File Offset: 0x000676DC
	public void RPC<P0, P1>(string messageName, RPCMode rpcMode, P0 p0, P1 p1)
	{
		this.RPC<P0, P1>(messageName, rpcMode, NGC.BlockArgs<P0, P1>(p0, p1));
	}

	// Token: 0x06001A85 RID: 6789 RVA: 0x000694F0 File Offset: 0x000676F0
	public void RPC<P0, P1>(string messageName, RPCMode rpcMode, NGC.callf<P0, P1>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, rpcMode, bitStream);
	}

	// Token: 0x06001A86 RID: 6790 RVA: 0x0006951C File Offset: 0x0006771C
	public void RPC<P0, P1>(string messageName, NetworkPlayer target, P0 p0, P1 p1)
	{
		this.RPC<P0, P1>(messageName, target, NGC.BlockArgs<P0, P1>(p0, p1));
	}

	// Token: 0x06001A87 RID: 6791 RVA: 0x00069530 File Offset: 0x00067730
	public void RPC<P0, P1>(string messageName, NetworkPlayer target, NGC.callf<P0, P1>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, target, bitStream);
	}

	// Token: 0x06001A88 RID: 6792 RVA: 0x0006955C File Offset: 0x0006775C
	public void RPC<P0, P1>(string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1)
	{
		this.RPC<P0, P1>(messageName, targets, NGC.BlockArgs<P0, P1>(p0, p1));
	}

	// Token: 0x06001A89 RID: 6793 RVA: 0x00069570 File Offset: 0x00067770
	public void RPC<P0, P1>(string messageName, IEnumerable<NetworkPlayer> targets, NGC.callf<P0, P1>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, targets, bitStream);
	}

	// Token: 0x06001A8A RID: 6794 RVA: 0x0006959C File Offset: 0x0006779C
	public void RPC<P0, P1, P2>(NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2)
	{
		this.RPC<P0, P1, P2>(flags, messageName, rpcMode, NGC.BlockArgs<P0, P1, P2>(p0, p1, p2));
	}

	// Token: 0x06001A8B RID: 6795 RVA: 0x000695B4 File Offset: 0x000677B4
	public void RPC<P0, P1, P2>(NetworkFlags flags, string messageName, RPCMode rpcMode, NGC.callf<P0, P1, P2>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, rpcMode, bitStream);
	}

	// Token: 0x06001A8C RID: 6796 RVA: 0x000695E0 File Offset: 0x000677E0
	public void RPC<P0, P1, P2>(NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2)
	{
		this.RPC<P0, P1, P2>(flags, messageName, target, NGC.BlockArgs<P0, P1, P2>(p0, p1, p2));
	}

	// Token: 0x06001A8D RID: 6797 RVA: 0x000695F8 File Offset: 0x000677F8
	public void RPC<P0, P1, P2>(NetworkFlags flags, string messageName, NetworkPlayer target, NGC.callf<P0, P1, P2>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, target, bitStream);
	}

	// Token: 0x06001A8E RID: 6798 RVA: 0x00069624 File Offset: 0x00067824
	public void RPC<P0, P1, P2>(NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2)
	{
		this.RPC<P0, P1, P2>(flags, messageName, targets, NGC.BlockArgs<P0, P1, P2>(p0, p1, p2));
	}

	// Token: 0x06001A8F RID: 6799 RVA: 0x0006963C File Offset: 0x0006783C
	public void RPC<P0, P1, P2>(NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, NGC.callf<P0, P1, P2>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, targets, bitStream);
	}

	// Token: 0x06001A90 RID: 6800 RVA: 0x00069668 File Offset: 0x00067868
	public void RPC<P0, P1, P2>(string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2)
	{
		this.RPC<P0, P1, P2>(messageName, rpcMode, NGC.BlockArgs<P0, P1, P2>(p0, p1, p2));
	}

	// Token: 0x06001A91 RID: 6801 RVA: 0x0006967C File Offset: 0x0006787C
	public void RPC<P0, P1, P2>(string messageName, RPCMode rpcMode, NGC.callf<P0, P1, P2>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, rpcMode, bitStream);
	}

	// Token: 0x06001A92 RID: 6802 RVA: 0x000696A8 File Offset: 0x000678A8
	public void RPC<P0, P1, P2>(string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2)
	{
		this.RPC<P0, P1, P2>(messageName, target, NGC.BlockArgs<P0, P1, P2>(p0, p1, p2));
	}

	// Token: 0x06001A93 RID: 6803 RVA: 0x000696BC File Offset: 0x000678BC
	public void RPC<P0, P1, P2>(string messageName, NetworkPlayer target, NGC.callf<P0, P1, P2>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, target, bitStream);
	}

	// Token: 0x06001A94 RID: 6804 RVA: 0x000696E8 File Offset: 0x000678E8
	public void RPC<P0, P1, P2>(string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2)
	{
		this.RPC<P0, P1, P2>(messageName, targets, NGC.BlockArgs<P0, P1, P2>(p0, p1, p2));
	}

	// Token: 0x06001A95 RID: 6805 RVA: 0x000696FC File Offset: 0x000678FC
	public void RPC<P0, P1, P2>(string messageName, IEnumerable<NetworkPlayer> targets, NGC.callf<P0, P1, P2>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, targets, bitStream);
	}

	// Token: 0x06001A96 RID: 6806 RVA: 0x00069728 File Offset: 0x00067928
	public void RPC<P0, P1, P2, P3>(NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		this.RPC<P0, P1, P2, P3>(flags, messageName, rpcMode, NGC.BlockArgs<P0, P1, P2, P3>(p0, p1, p2, p3));
	}

	// Token: 0x06001A97 RID: 6807 RVA: 0x0006974C File Offset: 0x0006794C
	public void RPC<P0, P1, P2, P3>(NetworkFlags flags, string messageName, RPCMode rpcMode, NGC.callf<P0, P1, P2, P3>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, rpcMode, bitStream);
	}

	// Token: 0x06001A98 RID: 6808 RVA: 0x00069778 File Offset: 0x00067978
	public void RPC<P0, P1, P2, P3>(NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		this.RPC<P0, P1, P2, P3>(flags, messageName, target, NGC.BlockArgs<P0, P1, P2, P3>(p0, p1, p2, p3));
	}

	// Token: 0x06001A99 RID: 6809 RVA: 0x0006979C File Offset: 0x0006799C
	public void RPC<P0, P1, P2, P3>(NetworkFlags flags, string messageName, NetworkPlayer target, NGC.callf<P0, P1, P2, P3>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, target, bitStream);
	}

	// Token: 0x06001A9A RID: 6810 RVA: 0x000697C8 File Offset: 0x000679C8
	public void RPC<P0, P1, P2, P3>(NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		this.RPC<P0, P1, P2, P3>(flags, messageName, targets, NGC.BlockArgs<P0, P1, P2, P3>(p0, p1, p2, p3));
	}

	// Token: 0x06001A9B RID: 6811 RVA: 0x000697EC File Offset: 0x000679EC
	public void RPC<P0, P1, P2, P3>(NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, NGC.callf<P0, P1, P2, P3>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, targets, bitStream);
	}

	// Token: 0x06001A9C RID: 6812 RVA: 0x00069818 File Offset: 0x00067A18
	public void RPC<P0, P1, P2, P3>(string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		this.RPC<P0, P1, P2, P3>(messageName, rpcMode, NGC.BlockArgs<P0, P1, P2, P3>(p0, p1, p2, p3));
	}

	// Token: 0x06001A9D RID: 6813 RVA: 0x00069830 File Offset: 0x00067A30
	public void RPC<P0, P1, P2, P3>(string messageName, RPCMode rpcMode, NGC.callf<P0, P1, P2, P3>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, rpcMode, bitStream);
	}

	// Token: 0x06001A9E RID: 6814 RVA: 0x0006985C File Offset: 0x00067A5C
	public void RPC<P0, P1, P2, P3>(string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		this.RPC<P0, P1, P2, P3>(messageName, target, NGC.BlockArgs<P0, P1, P2, P3>(p0, p1, p2, p3));
	}

	// Token: 0x06001A9F RID: 6815 RVA: 0x00069874 File Offset: 0x00067A74
	public void RPC<P0, P1, P2, P3>(string messageName, NetworkPlayer target, NGC.callf<P0, P1, P2, P3>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, target, bitStream);
	}

	// Token: 0x06001AA0 RID: 6816 RVA: 0x000698A0 File Offset: 0x00067AA0
	public void RPC<P0, P1, P2, P3>(string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3)
	{
		this.RPC<P0, P1, P2, P3>(messageName, targets, NGC.BlockArgs<P0, P1, P2, P3>(p0, p1, p2, p3));
	}

	// Token: 0x06001AA1 RID: 6817 RVA: 0x000698B8 File Offset: 0x00067AB8
	public void RPC<P0, P1, P2, P3>(string messageName, IEnumerable<NetworkPlayer> targets, NGC.callf<P0, P1, P2, P3>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, targets, bitStream);
	}

	// Token: 0x06001AA2 RID: 6818 RVA: 0x000698E4 File Offset: 0x00067AE4
	public void RPC<P0, P1, P2, P3, P4>(NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		this.RPC<P0, P1, P2, P3, P4>(flags, messageName, rpcMode, NGC.BlockArgs<P0, P1, P2, P3, P4>(p0, p1, p2, p3, p4));
	}

	// Token: 0x06001AA3 RID: 6819 RVA: 0x0006990C File Offset: 0x00067B0C
	public void RPC<P0, P1, P2, P3, P4>(NetworkFlags flags, string messageName, RPCMode rpcMode, NGC.callf<P0, P1, P2, P3, P4>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3, P4>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, rpcMode, bitStream);
	}

	// Token: 0x06001AA4 RID: 6820 RVA: 0x00069938 File Offset: 0x00067B38
	public void RPC<P0, P1, P2, P3, P4>(NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		this.RPC<P0, P1, P2, P3, P4>(flags, messageName, target, NGC.BlockArgs<P0, P1, P2, P3, P4>(p0, p1, p2, p3, p4));
	}

	// Token: 0x06001AA5 RID: 6821 RVA: 0x00069960 File Offset: 0x00067B60
	public void RPC<P0, P1, P2, P3, P4>(NetworkFlags flags, string messageName, NetworkPlayer target, NGC.callf<P0, P1, P2, P3, P4>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3, P4>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, target, bitStream);
	}

	// Token: 0x06001AA6 RID: 6822 RVA: 0x0006998C File Offset: 0x00067B8C
	public void RPC<P0, P1, P2, P3, P4>(NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		this.RPC<P0, P1, P2, P3, P4>(flags, messageName, targets, NGC.BlockArgs<P0, P1, P2, P3, P4>(p0, p1, p2, p3, p4));
	}

	// Token: 0x06001AA7 RID: 6823 RVA: 0x000699B4 File Offset: 0x00067BB4
	public void RPC<P0, P1, P2, P3, P4>(NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, NGC.callf<P0, P1, P2, P3, P4>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3, P4>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, targets, bitStream);
	}

	// Token: 0x06001AA8 RID: 6824 RVA: 0x000699E0 File Offset: 0x00067BE0
	public void RPC<P0, P1, P2, P3, P4>(string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		this.RPC<P0, P1, P2, P3, P4>(messageName, rpcMode, NGC.BlockArgs<P0, P1, P2, P3, P4>(p0, p1, p2, p3, p4));
	}

	// Token: 0x06001AA9 RID: 6825 RVA: 0x00069A04 File Offset: 0x00067C04
	public void RPC<P0, P1, P2, P3, P4>(string messageName, RPCMode rpcMode, NGC.callf<P0, P1, P2, P3, P4>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3, P4>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, rpcMode, bitStream);
	}

	// Token: 0x06001AAA RID: 6826 RVA: 0x00069A30 File Offset: 0x00067C30
	public void RPC<P0, P1, P2, P3, P4>(string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		this.RPC<P0, P1, P2, P3, P4>(messageName, target, NGC.BlockArgs<P0, P1, P2, P3, P4>(p0, p1, p2, p3, p4));
	}

	// Token: 0x06001AAB RID: 6827 RVA: 0x00069A54 File Offset: 0x00067C54
	public void RPC<P0, P1, P2, P3, P4>(string messageName, NetworkPlayer target, NGC.callf<P0, P1, P2, P3, P4>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3, P4>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, target, bitStream);
	}

	// Token: 0x06001AAC RID: 6828 RVA: 0x00069A80 File Offset: 0x00067C80
	public void RPC<P0, P1, P2, P3, P4>(string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		this.RPC<P0, P1, P2, P3, P4>(messageName, targets, NGC.BlockArgs<P0, P1, P2, P3, P4>(p0, p1, p2, p3, p4));
	}

	// Token: 0x06001AAD RID: 6829 RVA: 0x00069AA4 File Offset: 0x00067CA4
	public void RPC<P0, P1, P2, P3, P4>(string messageName, IEnumerable<NetworkPlayer> targets, NGC.callf<P0, P1, P2, P3, P4>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3, P4>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, targets, bitStream);
	}

	// Token: 0x06001AAE RID: 6830 RVA: 0x00069AD0 File Offset: 0x00067CD0
	public void RPC<P0, P1, P2, P3, P4, P5>(NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		this.RPC<P0, P1, P2, P3, P4, P5>(flags, messageName, rpcMode, NGC.BlockArgs<P0, P1, P2, P3, P4, P5>(p0, p1, p2, p3, p4, p5));
	}

	// Token: 0x06001AAF RID: 6831 RVA: 0x00069AF8 File Offset: 0x00067CF8
	public void RPC<P0, P1, P2, P3, P4, P5>(NetworkFlags flags, string messageName, RPCMode rpcMode, NGC.callf<P0, P1, P2, P3, P4, P5>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3, P4, P5>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, rpcMode, bitStream);
	}

	// Token: 0x06001AB0 RID: 6832 RVA: 0x00069B24 File Offset: 0x00067D24
	public void RPC<P0, P1, P2, P3, P4, P5>(NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		this.RPC<P0, P1, P2, P3, P4, P5>(flags, messageName, target, NGC.BlockArgs<P0, P1, P2, P3, P4, P5>(p0, p1, p2, p3, p4, p5));
	}

	// Token: 0x06001AB1 RID: 6833 RVA: 0x00069B4C File Offset: 0x00067D4C
	public void RPC<P0, P1, P2, P3, P4, P5>(NetworkFlags flags, string messageName, NetworkPlayer target, NGC.callf<P0, P1, P2, P3, P4, P5>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3, P4, P5>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, target, bitStream);
	}

	// Token: 0x06001AB2 RID: 6834 RVA: 0x00069B78 File Offset: 0x00067D78
	public void RPC<P0, P1, P2, P3, P4, P5>(NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		this.RPC<P0, P1, P2, P3, P4, P5>(flags, messageName, targets, NGC.BlockArgs<P0, P1, P2, P3, P4, P5>(p0, p1, p2, p3, p4, p5));
	}

	// Token: 0x06001AB3 RID: 6835 RVA: 0x00069BA0 File Offset: 0x00067DA0
	public void RPC<P0, P1, P2, P3, P4, P5>(NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, NGC.callf<P0, P1, P2, P3, P4, P5>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3, P4, P5>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, targets, bitStream);
	}

	// Token: 0x06001AB4 RID: 6836 RVA: 0x00069BCC File Offset: 0x00067DCC
	public void RPC<P0, P1, P2, P3, P4, P5>(string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		this.RPC<P0, P1, P2, P3, P4, P5>(messageName, rpcMode, NGC.BlockArgs<P0, P1, P2, P3, P4, P5>(p0, p1, p2, p3, p4, p5));
	}

	// Token: 0x06001AB5 RID: 6837 RVA: 0x00069BF4 File Offset: 0x00067DF4
	public void RPC<P0, P1, P2, P3, P4, P5>(string messageName, RPCMode rpcMode, NGC.callf<P0, P1, P2, P3, P4, P5>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3, P4, P5>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, rpcMode, bitStream);
	}

	// Token: 0x06001AB6 RID: 6838 RVA: 0x00069C20 File Offset: 0x00067E20
	public void RPC<P0, P1, P2, P3, P4, P5>(string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		this.RPC<P0, P1, P2, P3, P4, P5>(messageName, target, NGC.BlockArgs<P0, P1, P2, P3, P4, P5>(p0, p1, p2, p3, p4, p5));
	}

	// Token: 0x06001AB7 RID: 6839 RVA: 0x00069C48 File Offset: 0x00067E48
	public void RPC<P0, P1, P2, P3, P4, P5>(string messageName, NetworkPlayer target, NGC.callf<P0, P1, P2, P3, P4, P5>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3, P4, P5>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, target, bitStream);
	}

	// Token: 0x06001AB8 RID: 6840 RVA: 0x00069C74 File Offset: 0x00067E74
	public void RPC<P0, P1, P2, P3, P4, P5>(string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		this.RPC<P0, P1, P2, P3, P4, P5>(messageName, targets, NGC.BlockArgs<P0, P1, P2, P3, P4, P5>(p0, p1, p2, p3, p4, p5));
	}

	// Token: 0x06001AB9 RID: 6841 RVA: 0x00069C9C File Offset: 0x00067E9C
	public void RPC<P0, P1, P2, P3, P4, P5>(string messageName, IEnumerable<NetworkPlayer> targets, NGC.callf<P0, P1, P2, P3, P4, P5>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3, P4, P5>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, targets, bitStream);
	}

	// Token: 0x06001ABA RID: 6842 RVA: 0x00069CC8 File Offset: 0x00067EC8
	public void RPC<P0, P1, P2, P3, P4, P5, P6>(NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6>(flags, messageName, rpcMode, NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6>(p0, p1, p2, p3, p4, p5, p6));
	}

	// Token: 0x06001ABB RID: 6843 RVA: 0x00069CF4 File Offset: 0x00067EF4
	public void RPC<P0, P1, P2, P3, P4, P5, P6>(NetworkFlags flags, string messageName, RPCMode rpcMode, NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, rpcMode, bitStream);
	}

	// Token: 0x06001ABC RID: 6844 RVA: 0x00069D20 File Offset: 0x00067F20
	public void RPC<P0, P1, P2, P3, P4, P5, P6>(NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6>(flags, messageName, target, NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6>(p0, p1, p2, p3, p4, p5, p6));
	}

	// Token: 0x06001ABD RID: 6845 RVA: 0x00069D4C File Offset: 0x00067F4C
	public void RPC<P0, P1, P2, P3, P4, P5, P6>(NetworkFlags flags, string messageName, NetworkPlayer target, NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, target, bitStream);
	}

	// Token: 0x06001ABE RID: 6846 RVA: 0x00069D78 File Offset: 0x00067F78
	public void RPC<P0, P1, P2, P3, P4, P5, P6>(NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6>(flags, messageName, targets, NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6>(p0, p1, p2, p3, p4, p5, p6));
	}

	// Token: 0x06001ABF RID: 6847 RVA: 0x00069DA4 File Offset: 0x00067FA4
	public void RPC<P0, P1, P2, P3, P4, P5, P6>(NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, targets, bitStream);
	}

	// Token: 0x06001AC0 RID: 6848 RVA: 0x00069DD0 File Offset: 0x00067FD0
	public void RPC<P0, P1, P2, P3, P4, P5, P6>(string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6>(messageName, rpcMode, NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6>(p0, p1, p2, p3, p4, p5, p6));
	}

	// Token: 0x06001AC1 RID: 6849 RVA: 0x00069DF8 File Offset: 0x00067FF8
	public void RPC<P0, P1, P2, P3, P4, P5, P6>(string messageName, RPCMode rpcMode, NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, rpcMode, bitStream);
	}

	// Token: 0x06001AC2 RID: 6850 RVA: 0x00069E24 File Offset: 0x00068024
	public void RPC<P0, P1, P2, P3, P4, P5, P6>(string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6>(messageName, target, NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6>(p0, p1, p2, p3, p4, p5, p6));
	}

	// Token: 0x06001AC3 RID: 6851 RVA: 0x00069E4C File Offset: 0x0006804C
	public void RPC<P0, P1, P2, P3, P4, P5, P6>(string messageName, NetworkPlayer target, NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, target, bitStream);
	}

	// Token: 0x06001AC4 RID: 6852 RVA: 0x00069E78 File Offset: 0x00068078
	public void RPC<P0, P1, P2, P3, P4, P5, P6>(string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6>(messageName, targets, NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6>(p0, p1, p2, p3, p4, p5, p6));
	}

	// Token: 0x06001AC5 RID: 6853 RVA: 0x00069EA0 File Offset: 0x000680A0
	public void RPC<P0, P1, P2, P3, P4, P5, P6>(string messageName, IEnumerable<NetworkPlayer> targets, NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, targets, bitStream);
	}

	// Token: 0x06001AC6 RID: 6854 RVA: 0x00069ECC File Offset: 0x000680CC
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7>(NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(flags, messageName, rpcMode, NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7>(p0, p1, p2, p3, p4, p5, p6, p7));
	}

	// Token: 0x06001AC7 RID: 6855 RVA: 0x00069EF8 File Offset: 0x000680F8
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7>(NetworkFlags flags, string messageName, RPCMode rpcMode, NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, rpcMode, bitStream);
	}

	// Token: 0x06001AC8 RID: 6856 RVA: 0x00069F24 File Offset: 0x00068124
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7>(NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(flags, messageName, target, NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7>(p0, p1, p2, p3, p4, p5, p6, p7));
	}

	// Token: 0x06001AC9 RID: 6857 RVA: 0x00069F50 File Offset: 0x00068150
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7>(NetworkFlags flags, string messageName, NetworkPlayer target, NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, target, bitStream);
	}

	// Token: 0x06001ACA RID: 6858 RVA: 0x00069F7C File Offset: 0x0006817C
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7>(NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(flags, messageName, targets, NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7>(p0, p1, p2, p3, p4, p5, p6, p7));
	}

	// Token: 0x06001ACB RID: 6859 RVA: 0x00069FA8 File Offset: 0x000681A8
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7>(NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, targets, bitStream);
	}

	// Token: 0x06001ACC RID: 6860 RVA: 0x00069FD4 File Offset: 0x000681D4
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7>(string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(messageName, rpcMode, NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7>(p0, p1, p2, p3, p4, p5, p6, p7));
	}

	// Token: 0x06001ACD RID: 6861 RVA: 0x0006A000 File Offset: 0x00068200
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7>(string messageName, RPCMode rpcMode, NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, rpcMode, bitStream);
	}

	// Token: 0x06001ACE RID: 6862 RVA: 0x0006A02C File Offset: 0x0006822C
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7>(string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(messageName, target, NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7>(p0, p1, p2, p3, p4, p5, p6, p7));
	}

	// Token: 0x06001ACF RID: 6863 RVA: 0x0006A058 File Offset: 0x00068258
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7>(string messageName, NetworkPlayer target, NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, target, bitStream);
	}

	// Token: 0x06001AD0 RID: 6864 RVA: 0x0006A084 File Offset: 0x00068284
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7>(string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7>(messageName, targets, NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7>(p0, p1, p2, p3, p4, p5, p6, p7));
	}

	// Token: 0x06001AD1 RID: 6865 RVA: 0x0006A0B0 File Offset: 0x000682B0
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7>(string messageName, IEnumerable<NetworkPlayer> targets, NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, targets, bitStream);
	}

	// Token: 0x06001AD2 RID: 6866 RVA: 0x0006A0DC File Offset: 0x000682DC
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(flags, messageName, rpcMode, NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8>(p0, p1, p2, p3, p4, p5, p6, p7, p8));
	}

	// Token: 0x06001AD3 RID: 6867 RVA: 0x0006A10C File Offset: 0x0006830C
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(NetworkFlags flags, string messageName, RPCMode rpcMode, NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, rpcMode, bitStream);
	}

	// Token: 0x06001AD4 RID: 6868 RVA: 0x0006A138 File Offset: 0x00068338
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(flags, messageName, target, NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8>(p0, p1, p2, p3, p4, p5, p6, p7, p8));
	}

	// Token: 0x06001AD5 RID: 6869 RVA: 0x0006A168 File Offset: 0x00068368
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(NetworkFlags flags, string messageName, NetworkPlayer target, NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, target, bitStream);
	}

	// Token: 0x06001AD6 RID: 6870 RVA: 0x0006A194 File Offset: 0x00068394
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(flags, messageName, targets, NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8>(p0, p1, p2, p3, p4, p5, p6, p7, p8));
	}

	// Token: 0x06001AD7 RID: 6871 RVA: 0x0006A1C4 File Offset: 0x000683C4
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, targets, bitStream);
	}

	// Token: 0x06001AD8 RID: 6872 RVA: 0x0006A1F0 File Offset: 0x000683F0
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(messageName, rpcMode, NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8>(p0, p1, p2, p3, p4, p5, p6, p7, p8));
	}

	// Token: 0x06001AD9 RID: 6873 RVA: 0x0006A21C File Offset: 0x0006841C
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(string messageName, RPCMode rpcMode, NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, rpcMode, bitStream);
	}

	// Token: 0x06001ADA RID: 6874 RVA: 0x0006A248 File Offset: 0x00068448
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(messageName, target, NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8>(p0, p1, p2, p3, p4, p5, p6, p7, p8));
	}

	// Token: 0x06001ADB RID: 6875 RVA: 0x0006A274 File Offset: 0x00068474
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(string messageName, NetworkPlayer target, NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, target, bitStream);
	}

	// Token: 0x06001ADC RID: 6876 RVA: 0x0006A2A0 File Offset: 0x000684A0
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(messageName, targets, NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8>(p0, p1, p2, p3, p4, p5, p6, p7, p8));
	}

	// Token: 0x06001ADD RID: 6877 RVA: 0x0006A2CC File Offset: 0x000684CC
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8>(string messageName, IEnumerable<NetworkPlayer> targets, NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, targets, bitStream);
	}

	// Token: 0x06001ADE RID: 6878 RVA: 0x0006A2F8 File Offset: 0x000684F8
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(flags, messageName, rpcMode, NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(p0, p1, p2, p3, p4, p5, p6, p7, p8, p9));
	}

	// Token: 0x06001ADF RID: 6879 RVA: 0x0006A328 File Offset: 0x00068528
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(NetworkFlags flags, string messageName, RPCMode rpcMode, NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, rpcMode, bitStream);
	}

	// Token: 0x06001AE0 RID: 6880 RVA: 0x0006A354 File Offset: 0x00068554
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(flags, messageName, target, NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(p0, p1, p2, p3, p4, p5, p6, p7, p8, p9));
	}

	// Token: 0x06001AE1 RID: 6881 RVA: 0x0006A384 File Offset: 0x00068584
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(NetworkFlags flags, string messageName, NetworkPlayer target, NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, target, bitStream);
	}

	// Token: 0x06001AE2 RID: 6882 RVA: 0x0006A3B0 File Offset: 0x000685B0
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(flags, messageName, targets, NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(p0, p1, p2, p3, p4, p5, p6, p7, p8, p9));
	}

	// Token: 0x06001AE3 RID: 6883 RVA: 0x0006A3E0 File Offset: 0x000685E0
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, targets, bitStream);
	}

	// Token: 0x06001AE4 RID: 6884 RVA: 0x0006A40C File Offset: 0x0006860C
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(messageName, rpcMode, NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(p0, p1, p2, p3, p4, p5, p6, p7, p8, p9));
	}

	// Token: 0x06001AE5 RID: 6885 RVA: 0x0006A43C File Offset: 0x0006863C
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(string messageName, RPCMode rpcMode, NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, rpcMode, bitStream);
	}

	// Token: 0x06001AE6 RID: 6886 RVA: 0x0006A468 File Offset: 0x00068668
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(messageName, target, NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(p0, p1, p2, p3, p4, p5, p6, p7, p8, p9));
	}

	// Token: 0x06001AE7 RID: 6887 RVA: 0x0006A498 File Offset: 0x00068698
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(string messageName, NetworkPlayer target, NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, target, bitStream);
	}

	// Token: 0x06001AE8 RID: 6888 RVA: 0x0006A4C4 File Offset: 0x000686C4
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(messageName, targets, NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(p0, p1, p2, p3, p4, p5, p6, p7, p8, p9));
	}

	// Token: 0x06001AE9 RID: 6889 RVA: 0x0006A4F4 File Offset: 0x000686F4
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(string messageName, IEnumerable<NetworkPlayer> targets, NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, targets, bitStream);
	}

	// Token: 0x06001AEA RID: 6890 RVA: 0x0006A520 File Offset: 0x00068720
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(flags, messageName, rpcMode, NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10));
	}

	// Token: 0x06001AEB RID: 6891 RVA: 0x0006A554 File Offset: 0x00068754
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(NetworkFlags flags, string messageName, RPCMode rpcMode, NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, rpcMode, bitStream);
	}

	// Token: 0x06001AEC RID: 6892 RVA: 0x0006A580 File Offset: 0x00068780
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(flags, messageName, target, NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10));
	}

	// Token: 0x06001AED RID: 6893 RVA: 0x0006A5B4 File Offset: 0x000687B4
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(NetworkFlags flags, string messageName, NetworkPlayer target, NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, target, bitStream);
	}

	// Token: 0x06001AEE RID: 6894 RVA: 0x0006A5E0 File Offset: 0x000687E0
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(flags, messageName, targets, NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10));
	}

	// Token: 0x06001AEF RID: 6895 RVA: 0x0006A614 File Offset: 0x00068814
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, targets, bitStream);
	}

	// Token: 0x06001AF0 RID: 6896 RVA: 0x0006A640 File Offset: 0x00068840
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(messageName, rpcMode, NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10));
	}

	// Token: 0x06001AF1 RID: 6897 RVA: 0x0006A670 File Offset: 0x00068870
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(string messageName, RPCMode rpcMode, NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, rpcMode, bitStream);
	}

	// Token: 0x06001AF2 RID: 6898 RVA: 0x0006A69C File Offset: 0x0006889C
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(messageName, target, NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10));
	}

	// Token: 0x06001AF3 RID: 6899 RVA: 0x0006A6CC File Offset: 0x000688CC
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(string messageName, NetworkPlayer target, NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, target, bitStream);
	}

	// Token: 0x06001AF4 RID: 6900 RVA: 0x0006A6F8 File Offset: 0x000688F8
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(messageName, targets, NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10));
	}

	// Token: 0x06001AF5 RID: 6901 RVA: 0x0006A728 File Offset: 0x00068928
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(string messageName, IEnumerable<NetworkPlayer> targets, NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, targets, bitStream);
	}

	// Token: 0x06001AF6 RID: 6902 RVA: 0x0006A754 File Offset: 0x00068954
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(NetworkFlags flags, string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(flags, messageName, rpcMode, NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11));
	}

	// Token: 0x06001AF7 RID: 6903 RVA: 0x0006A788 File Offset: 0x00068988
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(NetworkFlags flags, string messageName, RPCMode rpcMode, NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, rpcMode, bitStream);
	}

	// Token: 0x06001AF8 RID: 6904 RVA: 0x0006A7B4 File Offset: 0x000689B4
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(NetworkFlags flags, string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(flags, messageName, target, NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11));
	}

	// Token: 0x06001AF9 RID: 6905 RVA: 0x0006A7E8 File Offset: 0x000689E8
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(NetworkFlags flags, string messageName, NetworkPlayer target, NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, target, bitStream);
	}

	// Token: 0x06001AFA RID: 6906 RVA: 0x0006A814 File Offset: 0x00068A14
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(flags, messageName, targets, NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11));
	}

	// Token: 0x06001AFB RID: 6907 RVA: 0x0006A848 File Offset: 0x00068A48
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(NetworkFlags flags, string messageName, IEnumerable<NetworkPlayer> targets, NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Block>(block, new object[0]);
		this.RPC_Stream(flags, messageName, targets, bitStream);
	}

	// Token: 0x06001AFC RID: 6908 RVA: 0x0006A874 File Offset: 0x00068A74
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(string messageName, RPCMode rpcMode, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(messageName, rpcMode, NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11));
	}

	// Token: 0x06001AFD RID: 6909 RVA: 0x0006A8A8 File Offset: 0x00068AA8
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(string messageName, RPCMode rpcMode, NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, rpcMode, bitStream);
	}

	// Token: 0x06001AFE RID: 6910 RVA: 0x0006A8D4 File Offset: 0x00068AD4
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(string messageName, NetworkPlayer target, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(messageName, target, NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11));
	}

	// Token: 0x06001AFF RID: 6911 RVA: 0x0006A908 File Offset: 0x00068B08
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(string messageName, NetworkPlayer target, NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, target, bitStream);
	}

	// Token: 0x06001B00 RID: 6912 RVA: 0x0006A934 File Offset: 0x00068B34
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(string messageName, IEnumerable<NetworkPlayer> targets, P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		this.RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(messageName, targets, NGC.BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11));
	}

	// Token: 0x06001B01 RID: 6913 RVA: 0x0006A968 File Offset: 0x00068B68
	public void RPC<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(string messageName, IEnumerable<NetworkPlayer> targets, NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Block block)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Block>(block, new object[0]);
		this.RPC_Stream(messageName, targets, bitStream);
	}

	// Token: 0x17000752 RID: 1874
	// (get) Token: 0x06001B02 RID: 6914 RVA: 0x0006A994 File Offset: 0x00068B94
	internal int id
	{
		get
		{
			if (this.innerID <= 0 || !this.outer)
			{
				return 0;
			}
			return NGC.PackID((int)this.outer.groupNumber, (int)this.innerID);
		}
	}

	// Token: 0x17000753 RID: 1875
	// (get) Token: 0x06001B03 RID: 6915 RVA: 0x0006A9D8 File Offset: 0x00068BD8
	public NetEntityID entityID
	{
		get
		{
			return new NetEntityID(this);
		}
	}

	// Token: 0x17000754 RID: 1876
	// (get) Token: 0x06001B04 RID: 6916 RVA: 0x0006A9E0 File Offset: 0x00068BE0
	public Vector3 creationPosition
	{
		get
		{
			return this.spawnPosition;
		}
	}

	// Token: 0x17000755 RID: 1877
	// (get) Token: 0x06001B05 RID: 6917 RVA: 0x0006A9E8 File Offset: 0x00068BE8
	public Quaternion creationRotation
	{
		get
		{
			return this.spawnRotation;
		}
	}

	// Token: 0x06001B06 RID: 6918 RVA: 0x0006A9F0 File Offset: 0x00068BF0
	internal void PostInstantiate()
	{
		base.BroadcastMessage("NGC_OnInstantiate", this, 1);
	}

	// Token: 0x06001B07 RID: 6919 RVA: 0x0006AA00 File Offset: 0x00068C00
	internal void PreDestroy()
	{
		if (!this.preDestroying)
		{
			this.preDestroying = true;
			if (this.onPreDestroy != null)
			{
				NGC.EventCallback eventCallback = this.onPreDestroy;
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

	// Token: 0x06001B08 RID: 6920 RVA: 0x0006AA6C File Offset: 0x00068C6C
	private NGC EnsureCall()
	{
		return this.outer;
	}

	// Token: 0x06001B09 RID: 6921 RVA: 0x0006AA74 File Offset: 0x00068C74
	public void RPC(NetworkFlags flags, string message, RPCMode mode)
	{
		this.EnsureCall().NGCViewRPC(flags, mode, this, this.prefab.MessageIndex(message), null, 0, 0);
	}

	// Token: 0x06001B0A RID: 6922 RVA: 0x0006AAA0 File Offset: 0x00068CA0
	public void RPC(NetworkFlags flags, string message, NetworkPlayer target)
	{
		this.EnsureCall().NGCViewRPC(flags, target, this, this.prefab.MessageIndex(message), null, 0, 0);
	}

	// Token: 0x06001B0B RID: 6923 RVA: 0x0006AACC File Offset: 0x00068CCC
	public void RPC(NetworkFlags flags, string message, IEnumerable<NetworkPlayer> target)
	{
		this.EnsureCall().NGCViewRPC(flags, target, this, this.prefab.MessageIndex(message), null, 0, 0);
	}

	// Token: 0x06001B0C RID: 6924 RVA: 0x0006AAF8 File Offset: 0x00068CF8
	public void RPC(string message, RPCMode mode)
	{
		this.EnsureCall().NGCViewRPC(mode, this, this.prefab.MessageIndex(message), null, 0, 0);
	}

	// Token: 0x06001B0D RID: 6925 RVA: 0x0006AB24 File Offset: 0x00068D24
	public void RPC(string message, NetworkPlayer target)
	{
		this.EnsureCall().NGCViewRPC(target, this, this.prefab.MessageIndex(message), null, 0, 0);
	}

	// Token: 0x06001B0E RID: 6926 RVA: 0x0006AB50 File Offset: 0x00068D50
	public void RPC(string message, IEnumerable<NetworkPlayer> target)
	{
		this.EnsureCall().NGCViewRPC(target, this, this.prefab.MessageIndex(message), null, 0, 0);
	}

	// Token: 0x06001B0F RID: 6927 RVA: 0x0006AB7C File Offset: 0x00068D7C
	public void RPC_Bytes(NetworkFlags flags, string message, RPCMode mode, byte[] data)
	{
		this.EnsureCall().NGCViewRPC(flags, mode, this, this.prefab.MessageIndex(message), data, 0, (data != null) ? data.Length : 0);
	}

	// Token: 0x06001B10 RID: 6928 RVA: 0x0006ABB8 File Offset: 0x00068DB8
	public void RPC_Bytes(NetworkFlags flags, string message, NetworkPlayer target, byte[] data)
	{
		this.EnsureCall().NGCViewRPC(flags, target, this, this.prefab.MessageIndex(message), data, 0, (data != null) ? data.Length : 0);
	}

	// Token: 0x06001B11 RID: 6929 RVA: 0x0006ABF4 File Offset: 0x00068DF4
	public void RPC_Bytes(NetworkFlags flags, string message, IEnumerable<NetworkPlayer> target, byte[] data)
	{
		this.EnsureCall().NGCViewRPC(flags, target, this, this.prefab.MessageIndex(message), data, 0, (data != null) ? data.Length : 0);
	}

	// Token: 0x06001B12 RID: 6930 RVA: 0x0006AC30 File Offset: 0x00068E30
	public void RPC_Bytes(string message, RPCMode mode, byte[] data)
	{
		this.EnsureCall().NGCViewRPC(mode, this, this.prefab.MessageIndex(message), data, 0, (data != null) ? data.Length : 0);
	}

	// Token: 0x06001B13 RID: 6931 RVA: 0x0006AC68 File Offset: 0x00068E68
	public void RPC_Bytes(string message, NetworkPlayer target, byte[] data)
	{
		this.EnsureCall().NGCViewRPC(target, this, this.prefab.MessageIndex(message), data, 0, (data != null) ? data.Length : 0);
	}

	// Token: 0x06001B14 RID: 6932 RVA: 0x0006ACA0 File Offset: 0x00068EA0
	public void RPC_Bytes(string message, IEnumerable<NetworkPlayer> target, byte[] data)
	{
		this.EnsureCall().NGCViewRPC(target, this, this.prefab.MessageIndex(message), data, 0, (data != null) ? data.Length : 0);
	}

	// Token: 0x06001B15 RID: 6933 RVA: 0x0006ACD8 File Offset: 0x00068ED8
	public void RPC_Stream(NetworkFlags flags, string message, RPCMode mode, BitStream data)
	{
		this.RPC_Bytes(flags, message, mode, data.GetDataByteArray());
	}

	// Token: 0x06001B16 RID: 6934 RVA: 0x0006ACEC File Offset: 0x00068EEC
	public void RPC_Stream(NetworkFlags flags, string message, NetworkPlayer target, BitStream data)
	{
		this.RPC_Bytes(flags, message, target, data.GetDataByteArray());
	}

	// Token: 0x06001B17 RID: 6935 RVA: 0x0006AD00 File Offset: 0x00068F00
	public void RPC_Stream(NetworkFlags flags, string message, IEnumerable<NetworkPlayer> target, BitStream data)
	{
		this.RPC_Bytes(flags, message, target, data.GetDataByteArray());
	}

	// Token: 0x06001B18 RID: 6936 RVA: 0x0006AD14 File Offset: 0x00068F14
	public void RPC_Stream(string message, RPCMode mode, BitStream data)
	{
		this.RPC_Bytes(message, mode, data.GetDataByteArray());
	}

	// Token: 0x06001B19 RID: 6937 RVA: 0x0006AD24 File Offset: 0x00068F24
	public void RPC_Stream(string message, NetworkPlayer target, BitStream data)
	{
		this.RPC_Bytes(message, target, data.GetDataByteArray());
	}

	// Token: 0x06001B1A RID: 6938 RVA: 0x0006AD34 File Offset: 0x00068F34
	public void RPC_Stream(string message, IEnumerable<NetworkPlayer> target, BitStream data)
	{
		this.RPC_Bytes(message, target, data.GetDataByteArray());
	}

	// Token: 0x06001B1B RID: 6939 RVA: 0x0006AD44 File Offset: 0x00068F44
	private static BitStream ToStream<T>(T arg)
	{
		BitStream bitStream = new BitStream(false);
		bitStream.Write<T>(arg, new object[0]);
		return bitStream;
	}

	// Token: 0x06001B1C RID: 6940 RVA: 0x0006AD68 File Offset: 0x00068F68
	public void RPC<T>(NetworkFlags flags, string message, RPCMode mode, T arg)
	{
		this.RPC_Stream(flags, message, mode, NGCView.ToStream<T>(arg));
	}

	// Token: 0x06001B1D RID: 6941 RVA: 0x0006AD7C File Offset: 0x00068F7C
	public void RPC<T>(NetworkFlags flags, string message, NetworkPlayer target, T arg)
	{
		this.RPC_Stream(flags, message, target, NGCView.ToStream<T>(arg));
	}

	// Token: 0x06001B1E RID: 6942 RVA: 0x0006AD90 File Offset: 0x00068F90
	public void RPC<T>(NetworkFlags flags, string message, IEnumerable<NetworkPlayer> target, T arg)
	{
		this.RPC_Stream(flags, message, target, NGCView.ToStream<T>(arg));
	}

	// Token: 0x06001B1F RID: 6943 RVA: 0x0006ADA4 File Offset: 0x00068FA4
	public void RPC<T>(string message, RPCMode mode, T arg)
	{
		this.RPC_Stream(message, mode, NGCView.ToStream<T>(arg));
	}

	// Token: 0x06001B20 RID: 6944 RVA: 0x0006ADB4 File Offset: 0x00068FB4
	public void RPC<T>(string message, NetworkPlayer target, T arg)
	{
		this.RPC_Stream(message, target, NGCView.ToStream<T>(arg));
	}

	// Token: 0x06001B21 RID: 6945 RVA: 0x0006ADC4 File Offset: 0x00068FC4
	public void RPC<T>(string message, IEnumerable<NetworkPlayer> target, T arg)
	{
		this.RPC_Stream(message, target, NGCView.ToStream<T>(arg));
	}

	// Token: 0x06001B22 RID: 6946 RVA: 0x0006ADD4 File Offset: 0x00068FD4
	public static NGCView Find(int id)
	{
		return NGC.Find(id);
	}

	// Token: 0x04000DE6 RID: 3558
	[NonSerialized]
	public NGC.Prefab prefab;

	// Token: 0x04000DE7 RID: 3559
	[NonSerialized]
	public NGC outer;

	// Token: 0x04000DE8 RID: 3560
	[NonSerialized]
	public ushort innerID;

	// Token: 0x04000DE9 RID: 3561
	[NonSerialized]
	public NetworkMessageInfo creation;

	// Token: 0x04000DEA RID: 3562
	[NonSerialized]
	public BitStream initialData;

	// Token: 0x04000DEB RID: 3563
	[SerializeField]
	internal MonoBehaviour[] scripts;

	// Token: 0x04000DEC RID: 3564
	[NonSerialized]
	internal NGC.Prefab.Installation.Instance install;

	// Token: 0x04000DED RID: 3565
	[NonSerialized]
	internal Vector3 spawnPosition;

	// Token: 0x04000DEE RID: 3566
	[NonSerialized]
	internal Quaternion spawnRotation;

	// Token: 0x04000DEF RID: 3567
	[NonSerialized]
	private NGC.EventCallback onPreDestroy;

	// Token: 0x04000DF0 RID: 3568
	[NonSerialized]
	private bool preDestroying;
}
