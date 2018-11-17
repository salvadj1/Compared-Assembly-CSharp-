using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x020002FB RID: 763
[AddComponentMenu("")]
public sealed class NGC : MonoBehaviour
{
	// Token: 0x06001A8E RID: 6798 RVA: 0x00067058 File Offset: 0x00065258
	private void uLink_OnNetworkInstantiate(uLink.NetworkMessageInfo info)
	{
		global::NGC ngc;
		if (global::NGC.Global.byGroup.TryGetValue(this.groupNumber, out ngc))
		{
			if (ngc == this)
			{
				return;
			}
			if (ngc)
			{
				ngc.Release();
			}
		}
		global::NGC.Global.all.Add(this);
		this.groupNumber = (ushort)this.networkView.group.id;
		global::NGC.Global.byGroup[this.groupNumber] = this;
		this.added = true;
		this.creation = info;
	}

	// Token: 0x06001A8F RID: 6799 RVA: 0x000670E0 File Offset: 0x000652E0
	private void Release()
	{
		if (this.added)
		{
			if (global::NGC.Global.all.Remove(this))
			{
				global::NGC.Global.byGroup.Remove(this.groupNumber);
			}
			this.added = false;
		}
	}

	// Token: 0x06001A90 RID: 6800 RVA: 0x00067118 File Offset: 0x00065318
	private void DestroyView(global::NGCView view, bool andGameObject, bool skipPreDestroy)
	{
		if (!view)
		{
			return;
		}
		if (andGameObject)
		{
			GameObject gameObject = view.gameObject;
			if (!skipPreDestroy)
			{
				this.DestroyView(view, false, false);
			}
			Object.Destroy(gameObject);
		}
		else if (!skipPreDestroy)
		{
			try
			{
				view.PreDestroy();
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
			}
		}
	}

	// Token: 0x06001A91 RID: 6801 RVA: 0x00067194 File Offset: 0x00065394
	private void PreDestroy()
	{
		List<global::NGCView> list = new List<global::NGCView>(this.views.Values);
		foreach (global::NGCView view in list)
		{
			this.DestroyView(view, false, false);
		}
		foreach (global::NGCView view2 in list)
		{
			this.DestroyView(view2, true, true);
		}
	}

	// Token: 0x06001A92 RID: 6802 RVA: 0x0006725C File Offset: 0x0006545C
	private void OnDestroy()
	{
		this.Release();
	}

	// Token: 0x06001A93 RID: 6803 RVA: 0x00067264 File Offset: 0x00065464
	internal Dictionary<ushort, global::NGCView>.ValueCollection GetViews()
	{
		return this.views.Values;
	}

	// Token: 0x06001A94 RID: 6804 RVA: 0x00067274 File Offset: 0x00065474
	private global::NGCView Add(byte[] data, int offset, int length, uLink.NetworkMessageInfo info)
	{
		int index = BitConverter.ToInt32(data, offset);
		int num = offset + 4;
		ushort innerID = BitConverter.ToUInt16(data, num);
		num += 2;
		Vector3 vector;
		vector.x = BitConverter.ToSingle(data, num);
		num += 4;
		vector.y = BitConverter.ToSingle(data, num);
		num += 4;
		vector.z = BitConverter.ToSingle(data, num);
		num += 4;
		Vector3 vector2;
		vector2.x = BitConverter.ToSingle(data, num);
		num += 4;
		vector2.y = BitConverter.ToSingle(data, num);
		num += 4;
		vector2.z = BitConverter.ToSingle(data, num);
		num += 4;
		Quaternion quaternion = Quaternion.Euler(vector2);
		global::NGC.Prefab prefab;
		global::NGC.Prefab.Register.Find(index, out prefab);
		global::NGCView ngcview = (global::NGCView)Object.Instantiate(prefab.prefab, vector, quaternion);
		ngcview.creation = info;
		ngcview.innerID = innerID;
		ngcview.prefab = prefab;
		ngcview.outer = this;
		ngcview.spawnPosition = vector;
		ngcview.spawnRotation = quaternion;
		int num2 = offset + length;
		if (num2 == num)
		{
			ngcview.initialData = null;
		}
		else
		{
			byte[] array = new byte[num2 - num];
			int num3 = 0;
			do
			{
				array[num3++] = data[num++];
			}
			while (num < num2);
			ngcview.initialData = new BitStream(array, false);
		}
		ngcview.install = new global::NGC.Prefab.Installation.Instance(prefab.installation);
		return ngcview;
	}

	// Token: 0x06001A95 RID: 6805 RVA: 0x000673C8 File Offset: 0x000655C8
	private global::NGCView Delete(ushort id, uLink.NetworkMessageInfo info)
	{
		global::NGCView ngcview = this.views[id];
		this.DestroyView(ngcview, false, false);
		this.views.Remove(id);
		return ngcview;
	}

	// Token: 0x06001A96 RID: 6806 RVA: 0x000673FC File Offset: 0x000655FC
	private global::NGC.Procedure Message(int id, int msg, byte[] args, int argByteSize, uLink.NetworkMessageInfo info)
	{
		return new global::NGC.Procedure
		{
			outer = this,
			target = id,
			message = msg,
			data = args,
			dataLength = argByteSize,
			info = info
		};
	}

	// Token: 0x06001A97 RID: 6807 RVA: 0x0006743C File Offset: 0x0006563C
	private global::NGC.Procedure Message(int id_msg, byte[] args, int argByteSize, uLink.NetworkMessageInfo info)
	{
		return this.Message(id_msg >> 16 & 65535, id_msg & 65535, args, argByteSize, info);
	}

	// Token: 0x06001A98 RID: 6808 RVA: 0x0006745C File Offset: 0x0006565C
	private global::NGC.Procedure Message(byte[] data, int offset, int length, uLink.NetworkMessageInfo info)
	{
		int id_msg = BitConverter.ToInt32(data, offset);
		int num = offset + 4;
		int num2 = offset + length;
		byte[] array;
		int num3;
		if (num == num2)
		{
			array = null;
			num3 = 0;
		}
		else
		{
			num3 = num2 - num;
			array = new byte[num3];
			int num4 = 0;
			do
			{
				array[num4++] = data[num++];
			}
			while (num < num2);
		}
		return this.Message(id_msg, array, num3, info);
	}

	// Token: 0x06001A99 RID: 6809 RVA: 0x000674C0 File Offset: 0x000656C0
	[RPC]
	internal void A(byte[] data, uLink.NetworkMessageInfo info)
	{
		global::NGCView ngcview = this.Add(data, 0, data.Length, info);
		this.views[ngcview.innerID] = ngcview;
		try
		{
			ngcview.PostInstantiate();
		}
		catch (Exception ex)
		{
			Debug.LogException(ex);
		}
	}

	// Token: 0x06001A9A RID: 6810 RVA: 0x00067520 File Offset: 0x00065720
	[RPC]
	internal void D(ushort id, uLink.NetworkMessageInfo info)
	{
		global::NGCView view = this.Delete(id, info);
		this.DestroyView(view, true, true);
	}

	// Token: 0x06001A9B RID: 6811 RVA: 0x00067540 File Offset: 0x00065740
	[RPC]
	internal void C(byte[] data, uLink.NetworkMessageInfo info)
	{
		global::NGC.Procedure procedure = this.Message(data, 0, data.Length, info);
		if (!procedure.Call())
		{
			if (procedure.view)
			{
				Debug.LogWarning(string.Format("Did not call rpc \"{0}\" for view \"{1}\" (entid:{2},msg:{3})", new object[]
				{
					procedure.view.prefab.installation.methods[procedure.message].method.Name,
					procedure.view.name,
					procedure.view.id,
					procedure.message
				}), this);
			}
			else if (global::NGC.log_nonexistant_ngc_errors)
			{
				Debug.LogWarning(string.Format("Did not call rpc to non existant view# {0}. ( message id was {1} )", procedure.target, procedure.message), this);
			}
		}
	}

	// Token: 0x06001A9C RID: 6812 RVA: 0x0006761C File Offset: 0x0006581C
	private byte[] RPCData(int viewID, int messageID, byte[] arguments, int argumentsOffset, int argumentsLength)
	{
		byte[] bytes = BitConverter.GetBytes(viewID << 16 | (messageID & 65535));
		byte[] array = new byte[bytes.Length + argumentsLength];
		int num = 0;
		for (int i = 0; i < bytes.Length; i++)
		{
			array[num++] = bytes[i];
		}
		int num2 = argumentsOffset;
		int j = 0;
		while (j < argumentsLength)
		{
			array[num++] = arguments[num2];
			j++;
			num2++;
		}
		return array;
	}

	// Token: 0x06001A9D RID: 6813 RVA: 0x00067694 File Offset: 0x00065894
	private void ShootRPC(global::NGC.RPCName rpc, uLink.RPCMode mode, byte[] data)
	{
		if (rpc.flags == null)
		{
			this.networkView.RPC<byte[]>(rpc.name, mode, data);
		}
		else
		{
			this.networkView.RPC<byte[]>(rpc.flags, rpc.name, mode, data);
		}
	}

	// Token: 0x06001A9E RID: 6814 RVA: 0x000676E4 File Offset: 0x000658E4
	private void ShootRPC(global::NGC.RPCName rpc, uLink.NetworkPlayer target, byte[] data)
	{
		if (rpc.flags == null)
		{
			this.networkView.RPC<byte[]>(rpc.name, target, data);
		}
		else
		{
			this.networkView.RPC<byte[]>(rpc.flags, rpc.name, target, data);
		}
	}

	// Token: 0x06001A9F RID: 6815 RVA: 0x00067734 File Offset: 0x00065934
	private void ShootRPC(global::NGC.RPCName rpc, IEnumerable<uLink.NetworkPlayer> targets, byte[] data)
	{
		if (rpc.flags == null)
		{
			this.networkView.RPC<byte[]>(rpc.name, targets, data);
		}
		else
		{
			this.networkView.RPC<byte[]>(rpc.flags, rpc.name, targets, data);
		}
	}

	// Token: 0x06001AA0 RID: 6816 RVA: 0x00067784 File Offset: 0x00065984
	private static global::NGC.RPCName CallRPCName(NetworkFlags? flags, global::NGCView view, int messageID)
	{
		return new global::NGC.RPCName(view, messageID, "C", (flags == null) ? view.prefab.DefaultNetworkFlags(messageID) : flags.Value);
	}

	// Token: 0x06001AA1 RID: 6817 RVA: 0x000677C4 File Offset: 0x000659C4
	private static global::NGC.RPCName CallRPCName(NetworkFlags? flags, global::NGCView view, int messageID, ref uLink.NetworkPlayer target)
	{
		return global::NGC.CallRPCName(flags, view, messageID);
	}

	// Token: 0x06001AA2 RID: 6818 RVA: 0x000677D0 File Offset: 0x000659D0
	private static global::NGC.RPCName CallRPCName(NetworkFlags? flags, global::NGCView view, int messageID, ref IEnumerable<uLink.NetworkPlayer> targets)
	{
		return global::NGC.CallRPCName(flags, view, messageID);
	}

	// Token: 0x06001AA3 RID: 6819 RVA: 0x000677DC File Offset: 0x000659DC
	private static global::NGC.RPCName CallRPCName(NetworkFlags? flags, global::NGCView view, int messageID, ref uLink.RPCMode mode)
	{
		return global::NGC.CallRPCName(flags, view, messageID);
	}

	// Token: 0x06001AA4 RID: 6820 RVA: 0x000677E8 File Offset: 0x000659E8
	internal void NGCViewRPC(NetworkFlags flags, uLink.RPCMode mode, global::NGCView view, int messageID, byte[] arguments, int argumentsOffset, int argumentsLength)
	{
		this.ShootRPC(global::NGC.CallRPCName(new NetworkFlags?(flags), view, messageID, ref mode), mode, this.RPCData((int)view.innerID, messageID, arguments, argumentsOffset, argumentsLength));
	}

	// Token: 0x06001AA5 RID: 6821 RVA: 0x00067820 File Offset: 0x00065A20
	internal void NGCViewRPC(NetworkFlags flags, uLink.NetworkPlayer target, global::NGCView view, int messageID, byte[] arguments, int argumentsOffset, int argumentsLength)
	{
		this.ShootRPC(global::NGC.CallRPCName(new NetworkFlags?(flags), view, messageID, ref target), target, this.RPCData((int)view.innerID, messageID, arguments, argumentsOffset, argumentsLength));
	}

	// Token: 0x06001AA6 RID: 6822 RVA: 0x00067858 File Offset: 0x00065A58
	internal void NGCViewRPC(NetworkFlags flags, IEnumerable<uLink.NetworkPlayer> targets, global::NGCView view, int messageID, byte[] arguments, int argumentsOffset, int argumentsLength)
	{
		this.ShootRPC(global::NGC.CallRPCName(new NetworkFlags?(flags), view, messageID, ref targets), targets, this.RPCData((int)view.innerID, messageID, arguments, argumentsOffset, argumentsLength));
	}

	// Token: 0x06001AA7 RID: 6823 RVA: 0x00067890 File Offset: 0x00065A90
	internal void NGCViewRPC(uLink.RPCMode mode, global::NGCView view, int messageID, byte[] arguments, int argumentsOffset, int argumentsLength)
	{
		this.ShootRPC(global::NGC.CallRPCName(null, view, messageID, ref mode), mode, this.RPCData((int)view.innerID, messageID, arguments, argumentsOffset, argumentsLength));
	}

	// Token: 0x06001AA8 RID: 6824 RVA: 0x000678CC File Offset: 0x00065ACC
	internal void NGCViewRPC(uLink.NetworkPlayer target, global::NGCView view, int messageID, byte[] arguments, int argumentsOffset, int argumentsLength)
	{
		this.ShootRPC(global::NGC.CallRPCName(null, view, messageID, ref target), target, this.RPCData((int)view.innerID, messageID, arguments, argumentsOffset, argumentsLength));
	}

	// Token: 0x06001AA9 RID: 6825 RVA: 0x00067908 File Offset: 0x00065B08
	internal void NGCViewRPC(IEnumerable<uLink.NetworkPlayer> targets, global::NGCView view, int messageID, byte[] arguments, int argumentsOffset, int argumentsLength)
	{
		this.ShootRPC(global::NGC.CallRPCName(null, view, messageID, ref targets), targets, this.RPCData((int)view.innerID, messageID, arguments, argumentsOffset, argumentsLength));
	}

	// Token: 0x06001AAA RID: 6826 RVA: 0x00067944 File Offset: 0x00065B44
	[Obsolete("NO, Use net cull making sure the prefab name string you must use starts with ;", true)]
	public static Object Instantiate(Object obj)
	{
		return Object.Instantiate(obj);
	}

	// Token: 0x06001AAB RID: 6827 RVA: 0x0006794C File Offset: 0x00065B4C
	[Obsolete("NO, Use net cull making sure the prefab name string you must use starts with ;", true)]
	public static Object Instantiate(Object obj, Vector3 position, Quaternion rotation)
	{
		return Object.Instantiate(obj, position, rotation);
	}

	// Token: 0x06001AAC RID: 6828 RVA: 0x00067958 File Offset: 0x00065B58
	private static uLink.NetworkView SpawnNGC_NetworkView(string prefabName, NetworkInstantiateArgs args, uLink.NetworkMessageInfo info)
	{
		NetworkInstantiatorUtility.AutoSetupNetworkViewOnAwake(args);
		GameObject gameObject = new GameObject(string.Format("__NGC-{0:X}", args.group), new Type[]
		{
			typeof(global::NGC),
			typeof(NGCInternalView)
		})
		{
			hideFlags = 1
		};
		NetworkInstantiatorUtility.ClearAutoSetupNetworkViewOnAwake();
		uLinkNetworkView component = gameObject.GetComponent<uLinkNetworkView>();
		global::NGC component2 = gameObject.GetComponent<global::NGC>();
		component.observed = component2;
		component.rpcReceiver = 1;
		component.stateSynchronization = 0;
		uLink.NetworkMessageInfo info2 = new uLink.NetworkMessageInfo(info, component);
		component2.uLink_OnNetworkInstantiate(info2);
		return component;
	}

	// Token: 0x06001AAD RID: 6829 RVA: 0x000679EC File Offset: 0x00065BEC
	private static void DestroyNGC_NetworkView(uLink.NetworkView view)
	{
		global::NGC component = view.GetComponent<global::NGC>();
		component.PreDestroy();
		Object.Destroy(component);
		NetworkInstantiator.defaultDestroyer.Invoke(view);
	}

	// Token: 0x06001AAE RID: 6830 RVA: 0x00067A18 File Offset: 0x00065C18
	public static void Register(global::NGCConfiguration configuration)
	{
		NetworkInstantiator.Add("!Ng", new NetworkInstantiator.Creator(global::NGC.SpawnNGC_NetworkView), new NetworkInstantiator.Destroyer(global::NGC.DestroyNGC_NetworkView));
		configuration.Install();
	}

	// Token: 0x06001AAF RID: 6831 RVA: 0x00067A50 File Offset: 0x00065C50
	internal static int PackID(int groupNumber, int innerID)
	{
		if (groupNumber < 0 || innerID <= 0)
		{
			return 0;
		}
		return (groupNumber & 65535) << 16 | innerID;
	}

	// Token: 0x06001AB0 RID: 6832 RVA: 0x00067A70 File Offset: 0x00065C70
	internal static bool UnpackID(int packed, out ushort groupNumber, out ushort innerID)
	{
		if (packed == 0)
		{
			groupNumber = 0;
			innerID = 0;
			return false;
		}
		groupNumber = (ushort)(packed >> 16 & 65535);
		innerID = (ushort)(packed & 65535);
		return true;
	}

	// Token: 0x06001AB1 RID: 6833 RVA: 0x00067AA4 File Offset: 0x00065CA4
	public static global::NGCView Find(int id)
	{
		ushort key;
		ushort key2;
		if (!global::NGC.UnpackID(id, out key, out key2))
		{
			return null;
		}
		global::NGC ngc;
		if (!global::NGC.Global.byGroup.TryGetValue(key, out ngc))
		{
			return null;
		}
		global::NGCView result;
		ngc.views.TryGetValue(key2, out result);
		return result;
	}

	// Token: 0x06001AB2 RID: 6834 RVA: 0x00067AE8 File Offset: 0x00065CE8
	public static global::NGC.callf<P0>.Block BlockArgs<P0>(P0 p0)
	{
		global::NGC.callf<P0>.Block result;
		result.p0 = p0;
		return result;
	}

	// Token: 0x06001AB3 RID: 6835 RVA: 0x00067B00 File Offset: 0x00065D00
	public static global::NGC.callf<P0, P1>.Block BlockArgs<P0, P1>(P0 p0, P1 p1)
	{
		global::NGC.callf<P0, P1>.Block result;
		result.p0 = p0;
		result.p1 = p1;
		return result;
	}

	// Token: 0x06001AB4 RID: 6836 RVA: 0x00067B20 File Offset: 0x00065D20
	public static global::NGC.callf<P0, P1, P2>.Block BlockArgs<P0, P1, P2>(P0 p0, P1 p1, P2 p2)
	{
		global::NGC.callf<P0, P1, P2>.Block result;
		result.p0 = p0;
		result.p1 = p1;
		result.p2 = p2;
		return result;
	}

	// Token: 0x06001AB5 RID: 6837 RVA: 0x00067B48 File Offset: 0x00065D48
	public static global::NGC.callf<P0, P1, P2, P3>.Block BlockArgs<P0, P1, P2, P3>(P0 p0, P1 p1, P2 p2, P3 p3)
	{
		global::NGC.callf<P0, P1, P2, P3>.Block result;
		result.p0 = p0;
		result.p1 = p1;
		result.p2 = p2;
		result.p3 = p3;
		return result;
	}

	// Token: 0x06001AB6 RID: 6838 RVA: 0x00067B78 File Offset: 0x00065D78
	public static global::NGC.callf<P0, P1, P2, P3, P4>.Block BlockArgs<P0, P1, P2, P3, P4>(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		global::NGC.callf<P0, P1, P2, P3, P4>.Block result;
		result.p0 = p0;
		result.p1 = p1;
		result.p2 = p2;
		result.p3 = p3;
		result.p4 = p4;
		return result;
	}

	// Token: 0x06001AB7 RID: 6839 RVA: 0x00067BB0 File Offset: 0x00065DB0
	public static global::NGC.callf<P0, P1, P2, P3, P4, P5>.Block BlockArgs<P0, P1, P2, P3, P4, P5>(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		global::NGC.callf<P0, P1, P2, P3, P4, P5>.Block result;
		result.p0 = p0;
		result.p1 = p1;
		result.p2 = p2;
		result.p3 = p3;
		result.p4 = p4;
		result.p5 = p5;
		return result;
	}

	// Token: 0x06001AB8 RID: 6840 RVA: 0x00067BF0 File Offset: 0x00065DF0
	public static global::NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Block BlockArgs<P0, P1, P2, P3, P4, P5, P6>(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		global::NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Block result;
		result.p0 = p0;
		result.p1 = p1;
		result.p2 = p2;
		result.p3 = p3;
		result.p4 = p4;
		result.p5 = p5;
		result.p6 = p6;
		return result;
	}

	// Token: 0x06001AB9 RID: 6841 RVA: 0x00067C3C File Offset: 0x00065E3C
	public static global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Block BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7>(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Block result;
		result.p0 = p0;
		result.p1 = p1;
		result.p2 = p2;
		result.p3 = p3;
		result.p4 = p4;
		result.p5 = p5;
		result.p6 = p6;
		result.p7 = p7;
		return result;
	}

	// Token: 0x06001ABA RID: 6842 RVA: 0x00067C90 File Offset: 0x00065E90
	public static global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Block BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8>(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Block result;
		result.p0 = p0;
		result.p1 = p1;
		result.p2 = p2;
		result.p3 = p3;
		result.p4 = p4;
		result.p5 = p5;
		result.p6 = p6;
		result.p7 = p7;
		result.p8 = p8;
		return result;
	}

	// Token: 0x06001ABB RID: 6843 RVA: 0x00067CEC File Offset: 0x00065EEC
	public static global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Block BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Block result;
		result.p0 = p0;
		result.p1 = p1;
		result.p2 = p2;
		result.p3 = p3;
		result.p4 = p4;
		result.p5 = p5;
		result.p6 = p6;
		result.p7 = p7;
		result.p8 = p8;
		result.p9 = p9;
		return result;
	}

	// Token: 0x06001ABC RID: 6844 RVA: 0x00067D50 File Offset: 0x00065F50
	public static global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Block BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Block result;
		result.p0 = p0;
		result.p1 = p1;
		result.p2 = p2;
		result.p3 = p3;
		result.p4 = p4;
		result.p5 = p5;
		result.p6 = p6;
		result.p7 = p7;
		result.p8 = p8;
		result.p9 = p9;
		result.p10 = p10;
		return result;
	}

	// Token: 0x06001ABD RID: 6845 RVA: 0x00067DC0 File Offset: 0x00065FC0
	public static global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Block BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Block result;
		result.p0 = p0;
		result.p1 = p1;
		result.p2 = p2;
		result.p3 = p3;
		result.p4 = p4;
		result.p5 = p5;
		result.p6 = p6;
		result.p7 = p7;
		result.p8 = p8;
		result.p9 = p9;
		result.p10 = p10;
		result.p11 = p11;
		return result;
	}

	// Token: 0x06001ABE RID: 6846 RVA: 0x00067E38 File Offset: 0x00066038
	private static global::NGC.IExecuter FindExecuter(Type[] argumentTypes)
	{
		switch (argumentTypes.Length)
		{
		case 0:
			return typeof(global::NGC.callf).GetProperty("Exec", BindingFlags.Static | BindingFlags.Public).GetValue(null, null) as global::NGC.IExecuter;
		case 1:
			return typeof(global::NGC.callf<>).MakeGenericType(argumentTypes).GetProperty("Exec", BindingFlags.Static | BindingFlags.Public).GetValue(null, null) as global::NGC.IExecuter;
		case 2:
			return typeof(global::NGC.callf<, >).MakeGenericType(argumentTypes).GetProperty("Exec", BindingFlags.Static | BindingFlags.Public).GetValue(null, null) as global::NGC.IExecuter;
		case 3:
			return typeof(global::NGC.callf<, , >).MakeGenericType(argumentTypes).GetProperty("Exec", BindingFlags.Static | BindingFlags.Public).GetValue(null, null) as global::NGC.IExecuter;
		case 4:
			return typeof(global::NGC.callf<, , , >).MakeGenericType(argumentTypes).GetProperty("Exec", BindingFlags.Static | BindingFlags.Public).GetValue(null, null) as global::NGC.IExecuter;
		case 5:
			return typeof(global::NGC.callf<, , , , >).MakeGenericType(argumentTypes).GetProperty("Exec", BindingFlags.Static | BindingFlags.Public).GetValue(null, null) as global::NGC.IExecuter;
		case 6:
			return typeof(global::NGC.callf<, , , , , >).MakeGenericType(argumentTypes).GetProperty("Exec", BindingFlags.Static | BindingFlags.Public).GetValue(null, null) as global::NGC.IExecuter;
		case 7:
			return typeof(global::NGC.callf<, , , , , , >).MakeGenericType(argumentTypes).GetProperty("Exec", BindingFlags.Static | BindingFlags.Public).GetValue(null, null) as global::NGC.IExecuter;
		case 8:
			return typeof(global::NGC.callf<, , , , , , , >).MakeGenericType(argumentTypes).GetProperty("Exec", BindingFlags.Static | BindingFlags.Public).GetValue(null, null) as global::NGC.IExecuter;
		case 9:
			return typeof(global::NGC.callf<, , , , , , , , >).MakeGenericType(argumentTypes).GetProperty("Exec", BindingFlags.Static | BindingFlags.Public).GetValue(null, null) as global::NGC.IExecuter;
		case 10:
			return typeof(global::NGC.callf<, , , , , , , , , >).MakeGenericType(argumentTypes).GetProperty("Exec", BindingFlags.Static | BindingFlags.Public).GetValue(null, null) as global::NGC.IExecuter;
		case 11:
			return typeof(global::NGC.callf<, , , , , , , , , , >).MakeGenericType(argumentTypes).GetProperty("Exec", BindingFlags.Static | BindingFlags.Public).GetValue(null, null) as global::NGC.IExecuter;
		case 12:
			return typeof(global::NGC.callf<, , , , , , , , , , , >).MakeGenericType(argumentTypes).GetProperty("Exec", BindingFlags.Static | BindingFlags.Public).GetValue(null, null) as global::NGC.IExecuter;
		default:
			throw new ArgumentOutOfRangeException("argumentTypes.Length > {0}");
		}
	}

	// Token: 0x04000E8D RID: 3725
	private const string kAddRPC = "A";

	// Token: 0x04000E8E RID: 3726
	private const string kDeleteRPC = "D";

	// Token: 0x04000E8F RID: 3727
	private const string kCallRPC = "C";

	// Token: 0x04000E90 RID: 3728
	private const string kPrefabIdentifier = "!Ng";

	// Token: 0x04000E91 RID: 3729
	[NonSerialized]
	private bool added;

	// Token: 0x04000E92 RID: 3730
	[NonSerialized]
	internal ushort groupNumber;

	// Token: 0x04000E93 RID: 3731
	[NonSerialized]
	private uLink.NetworkMessageInfo creation;

	// Token: 0x04000E94 RID: 3732
	[NonSerialized]
	internal NGCInternalView networkView;

	// Token: 0x04000E95 RID: 3733
	[NonSerialized]
	internal uLink.NetworkViewID networkViewID;

	// Token: 0x04000E96 RID: 3734
	[NonSerialized]
	private readonly Dictionary<ushort, global::NGCView> views = new Dictionary<ushort, global::NGCView>();

	// Token: 0x04000E97 RID: 3735
	private static bool log_nonexistant_ngc_errors;

	// Token: 0x020002FC RID: 764
	private static class Global
	{
		// Token: 0x04000E98 RID: 3736
		public static readonly Dictionary<ushort, global::NGC> byGroup = new Dictionary<ushort, global::NGC>();

		// Token: 0x04000E99 RID: 3737
		public static readonly List<global::NGC> all = new List<global::NGC>();
	}

	// Token: 0x020002FD RID: 765
	public interface IExecuter
	{
		// Token: 0x06001AC0 RID: 6848
		void ExecuteCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance);

		// Token: 0x06001AC1 RID: 6849
		IEnumerator ExecuteRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance);

		// Token: 0x06001AC2 RID: 6850
		void ExecuteInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info);

		// Token: 0x06001AC3 RID: 6851
		IEnumerator ExecuteInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info);

		// Token: 0x06001AC4 RID: 6852
		void ExecuteStreamCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance);

		// Token: 0x06001AC5 RID: 6853
		IEnumerator ExecuteStreamRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance);

		// Token: 0x06001AC6 RID: 6854
		void ExecuteStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info);

		// Token: 0x06001AC7 RID: 6855
		IEnumerator ExecuteStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info);
	}

	// Token: 0x020002FE RID: 766
	public sealed class Prefab
	{
		// Token: 0x06001AC8 RID: 6856 RVA: 0x000680BC File Offset: 0x000662BC
		private Prefab(string contentPath, int key, string handle)
		{
			this.contentPath = contentPath;
			this.key = key;
			this.handle = handle;
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x000680DC File Offset: 0x000662DC
		public int MessageIndex(string message)
		{
			int result;
			if (this.cachedMessageIndices != null && this.cachedMessageIndices.TryGetValue(message, out result))
			{
				return result;
			}
			int num = this.MessageIndexFind(message);
			if (num == -1)
			{
				throw new ArgumentException(message, "message");
			}
			if (this.cachedMessageIndices == null)
			{
				this.cachedMessageIndices = new Dictionary<string, int>();
			}
			this.cachedMessageIndices[message] = num;
			return num;
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x00068148 File Offset: 0x00066348
		private int MessageIndexFind(string message)
		{
			int num = message.LastIndexOf(':');
			if (num == -1)
			{
				for (int i = 0; i < this._installation.methods.Length; i++)
				{
					if (this._installation.methods[i].method.Name == message)
					{
						return i;
					}
				}
			}
			else
			{
				for (int j = 0; j < this._installation.methods.Length; j++)
				{
					if (string.Compare(this._installation.methods[j].method.Name, 0, message, num + 1, message.Length - (num + 1)) == 0 && string.Compare(this._installation.methods[j].method.DeclaringType.FullName, 0, message, 0, num) == 0)
					{
						return j;
					}
				}
			}
			return -1;
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06001ACB RID: 6859 RVA: 0x00068234 File Offset: 0x00066434
		public global::NGC.Prefab.Installation installation
		{
			get
			{
				if (this._installation == null && !this.prefab)
				{
					throw new InvalidOperationException("Could not get installation because prefab could not load");
				}
				return this._installation;
			}
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06001ACC RID: 6860 RVA: 0x00068270 File Offset: 0x00066470
		public global::NGCView prefab
		{
			get
			{
				global::NGCView ngcview;
				if (this.weakReference == null || !(ngcview = (global::NGCView)this.weakReference.Target) || !this.weakReference.IsAlive)
				{
					if (!Facepunch.Bundling.Load<global::NGCView>(this.contentPath, typeof(global::NGCView), out ngcview))
					{
						throw new MissingReferenceException("Could not load NGCView at " + this.contentPath);
					}
					if (this._installation == null)
					{
						this._installation = global::NGC.Prefab.Installation.Create(ngcview);
					}
					this.weakReference = new WeakReference(ngcview);
				}
				return ngcview;
			}
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x0006830C File Offset: 0x0006650C
		internal NetworkFlags DefaultNetworkFlags(int messageIndex)
		{
			return this.installation.methods[messageIndex].defaultNetworkFlags;
		}

		// Token: 0x04000E9A RID: 3738
		[NonSerialized]
		public readonly string contentPath;

		// Token: 0x04000E9B RID: 3739
		[NonSerialized]
		public readonly int key;

		// Token: 0x04000E9C RID: 3740
		[NonSerialized]
		public readonly string handle;

		// Token: 0x04000E9D RID: 3741
		[NonSerialized]
		private global::NGC.Prefab.Installation _installation;

		// Token: 0x04000E9E RID: 3742
		private Dictionary<string, int> cachedMessageIndices;

		// Token: 0x04000E9F RID: 3743
		private WeakReference weakReference;

		// Token: 0x020002FF RID: 767
		public sealed class Installation
		{
			// Token: 0x06001ACE RID: 6862 RVA: 0x00068324 File Offset: 0x00066524
			private Installation(global::NGC.Prefab.Installation.Method[] methods, ushort[] methodScriptIndices)
			{
				this.methods = methods;
				this.methodScriptIndices = methodScriptIndices;
			}

			// Token: 0x06001AD0 RID: 6864 RVA: 0x00068348 File Offset: 0x00066548
			public static global::NGC.Prefab.Installation Create(global::NGCView view)
			{
				int num = 0;
				List<global::NGC.Prefab.Installation.Method[]> list = new List<global::NGC.Prefab.Installation.Method[]>();
				foreach (MonoBehaviour monoBehaviour in view.scripts)
				{
					Type type = monoBehaviour.GetType();
					global::NGC.Prefab.Installation.Method[] array;
					if (!global::NGC.Prefab.Installation.methodsForType.TryGetValue(type, out array))
					{
						List<global::NGC.Prefab.Installation.Method> list2 = new List<global::NGC.Prefab.Installation.Method>();
						MethodInfo[] array2 = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
						foreach (MethodInfo methodInfo in array2)
						{
							bool flag = false;
							if (methodInfo.IsDefined(typeof(RPC), true))
							{
								if (!methodInfo.IsDefined(typeof(global::NGCRPCSkipAttribute), false) || methodInfo.IsDefined(typeof(global::NGCRPCAttribute), true))
								{
									flag = true;
								}
							}
							else if (methodInfo.IsDefined(typeof(global::NGCRPCAttribute), true))
							{
								flag = true;
							}
							if (flag)
							{
								list2.Add(global::NGC.Prefab.Installation.Method.Create(methodInfo));
							}
						}
						list2.Sort((global::NGC.Prefab.Installation.Method x, global::NGC.Prefab.Installation.Method y) => x.method.Name.CompareTo(y.method.Name));
						array = list2.ToArray();
						global::NGC.Prefab.Installation.methodsForType[type] = array;
					}
					num += array.Length;
					list.Add(array);
				}
				global::NGC.Prefab.Installation.Method[] array4 = new global::NGC.Prefab.Installation.Method[num];
				ushort[] array5 = new ushort[num];
				int num2 = 0;
				ushort num3 = 0;
				foreach (global::NGC.Prefab.Installation.Method[] array6 in list)
				{
					foreach (global::NGC.Prefab.Installation.Method method in array6)
					{
						array4[num2] = method;
						array5[num2] = num3;
						num2++;
					}
					num3 += 1;
				}
				return new global::NGC.Prefab.Installation(array4, array5);
			}

			// Token: 0x04000EA0 RID: 3744
			public readonly global::NGC.Prefab.Installation.Method[] methods;

			// Token: 0x04000EA1 RID: 3745
			public readonly ushort[] methodScriptIndices;

			// Token: 0x04000EA2 RID: 3746
			private static readonly Dictionary<Type, global::NGC.Prefab.Installation.Method[]> methodsForType = new Dictionary<Type, global::NGC.Prefab.Installation.Method[]>();

			// Token: 0x02000300 RID: 768
			public sealed class Instance
			{
				// Token: 0x06001AD2 RID: 6866 RVA: 0x0006857C File Offset: 0x0006677C
				public Instance(global::NGC.Prefab.Installation installation)
				{
					this.delegates = new Delegate[installation.methods.Length];
				}

				// Token: 0x06001AD3 RID: 6867 RVA: 0x00068598 File Offset: 0x00066798
				public void Invoke(global::NGC.Procedure procedure)
				{
					procedure.view.prefab.installation.methods[procedure.message].Invoke(ref this.delegates[procedure.message], procedure, procedure.view.prefab.installation.methodScriptIndices[procedure.message]);
				}

				// Token: 0x04000EA4 RID: 3748
				public readonly Delegate[] delegates;
			}

			// Token: 0x02000301 RID: 769
			public struct Method
			{
				// Token: 0x06001AD4 RID: 6868 RVA: 0x000685F8 File Offset: 0x000667F8
				private Method(MethodInfo method, byte flags, global::NGC.IExecuter executer)
				{
					this.method = method;
					this.flags = flags;
					this.executer = executer;
				}

				// Token: 0x17000794 RID: 1940
				// (get) Token: 0x06001AD5 RID: 6869 RVA: 0x00068610 File Offset: 0x00066810
				public NetworkFlags defaultNetworkFlags
				{
					get
					{
						NetworkFlags networkFlags = 0;
						if ((this.flags & 33) != 1)
						{
							networkFlags |= 8;
						}
						if ((this.flags & 128) == 128)
						{
							networkFlags |= 16;
						}
						if ((this.flags & 64) == 16)
						{
							networkFlags |= 3;
						}
						else if ((this.flags & 8) == 8)
						{
							networkFlags |= 2;
						}
						if ((this.flags & 16) == 16)
						{
							networkFlags |= 4;
						}
						return networkFlags;
					}
				}

				// Token: 0x06001AD6 RID: 6870 RVA: 0x00068694 File Offset: 0x00066894
				public void Invoke(ref Delegate d, global::NGC.Procedure procedure, ushort scriptIndex)
				{
					MonoBehaviour monoBehaviour = procedure.view.scripts[(int)scriptIndex];
					IEnumerator enumerator;
					switch (this.flags & 7)
					{
					case 0:
						this.executer.ExecuteCall(procedure.CreateBitStream(), ref d, this.method, monoBehaviour);
						return;
					case 1:
						this.executer.ExecuteInfoCall(procedure.CreateBitStream(), ref d, this.method, monoBehaviour, procedure.info);
						return;
					case 2:
						this.executer.ExecuteStreamCall(procedure.CreateBitStream(), ref d, this.method, monoBehaviour);
						return;
					case 3:
						this.executer.ExecuteStreamInfoCall(procedure.CreateBitStream(), ref d, this.method, monoBehaviour, procedure.info);
						return;
					case 4:
						enumerator = this.executer.ExecuteRoutine(procedure.CreateBitStream(), ref d, this.method, monoBehaviour);
						break;
					case 5:
						enumerator = this.executer.ExecuteInfoRoutine(procedure.CreateBitStream(), ref d, this.method, monoBehaviour, procedure.info);
						break;
					case 6:
						enumerator = this.executer.ExecuteStreamRoutine(procedure.CreateBitStream(), ref d, this.method, monoBehaviour);
						break;
					case 7:
						enumerator = this.executer.ExecuteStreamInfoRoutine(procedure.CreateBitStream(), ref d, this.method, monoBehaviour, procedure.info);
						break;
					default:
						throw new NotImplementedException(((int)(this.flags & 7)).ToString());
					}
					if (enumerator == null)
					{
						return;
					}
					try
					{
						monoBehaviour.StartCoroutine(enumerator);
					}
					catch (Exception ex)
					{
						Debug.LogException(ex, monoBehaviour);
					}
				}

				// Token: 0x06001AD7 RID: 6871 RVA: 0x00068834 File Offset: 0x00066A34
				public static global::NGC.Prefab.Installation.Method Create(MethodInfo info)
				{
					Type returnType = info.ReturnType;
					byte b;
					if (returnType == typeof(void))
					{
						b = 0;
					}
					else
					{
						if (returnType != typeof(IEnumerator))
						{
							throw new InvalidOperationException(string.Format("RPC {0} returns a unhandled type: {1}", info, returnType));
						}
						b = 4;
					}
					ParameterInfo[] parameters = info.GetParameters();
					for (int i = 0; i < parameters.Length; i++)
					{
						if (parameters[i].IsOut || parameters[i].IsIn)
						{
							throw new InvalidOperationException(string.Format("RPC method {0} has a illegal parameter {1}", info, parameters[i]));
						}
					}
					int num = parameters.Length;
					if (num > 0)
					{
						Type parameterType;
						if ((parameterType = parameters[parameters.Length - 1].ParameterType) == typeof(uLink.NetworkMessageInfo))
						{
							num--;
							if (num > 0 && parameters[num - 1].ParameterType == typeof(BitStream))
							{
								num--;
								b |= 3;
							}
							else
							{
								b |= 1;
							}
						}
						else if (parameterType == typeof(BitStream))
						{
							num--;
							b |= 2;
						}
					}
					Type[] array = new Type[num];
					for (int j = 0; j < num; j++)
					{
						array[j] = parameters[j].ParameterType;
					}
					global::NGC.IExecuter executer = global::NGC.FindExecuter(array);
					if (executer != null)
					{
						return new global::NGC.Prefab.Installation.Method(info, b, executer);
					}
					throw new InvalidProgramException();
				}

				// Token: 0x04000EA5 RID: 3749
				private const byte FLAG_INFO = 1;

				// Token: 0x04000EA6 RID: 3750
				private const byte FLAG_STREAM = 2;

				// Token: 0x04000EA7 RID: 3751
				private const byte FLAG_ENUMERATOR = 4;

				// Token: 0x04000EA8 RID: 3752
				private const byte FLAG_FORCE_UNBUFFERED = 8;

				// Token: 0x04000EA9 RID: 3753
				private const byte FLAG_FORCE_INSECURE = 16;

				// Token: 0x04000EAA RID: 3754
				private const byte FLAG_FORCE_NO_TIMESTAMP = 32;

				// Token: 0x04000EAB RID: 3755
				private const byte FLAG_FORCE_UNRELIABLE = 64;

				// Token: 0x04000EAC RID: 3756
				private const byte FLAG_FORCE_TYPE_UNSAFE = 128;

				// Token: 0x04000EAD RID: 3757
				private const byte INVOKE_FLAGS = 7;

				// Token: 0x04000EAE RID: 3758
				public readonly MethodInfo method;

				// Token: 0x04000EAF RID: 3759
				public readonly byte flags;

				// Token: 0x04000EB0 RID: 3760
				private readonly global::NGC.IExecuter executer;
			}
		}

		// Token: 0x02000302 RID: 770
		public static class Register
		{
			// Token: 0x06001AD9 RID: 6873 RVA: 0x000689CC File Offset: 0x00066BCC
			public static bool Find(int index, out global::NGC.Prefab prefab)
			{
				return global::NGC.Prefab.Register.PrefabByIndex.TryGetValue(index, out prefab);
			}

			// Token: 0x06001ADA RID: 6874 RVA: 0x000689DC File Offset: 0x00066BDC
			public static string FindName(int iIndex)
			{
				return global::NGC.Prefab.Register.PrefabByIndex[iIndex].handle;
			}

			// Token: 0x06001ADB RID: 6875 RVA: 0x000689F0 File Offset: 0x00066BF0
			public static bool Find(string handle, out global::NGC.Prefab prefab)
			{
				return global::NGC.Prefab.Register.PrefabByName.TryGetValue(handle, out prefab);
			}

			// Token: 0x06001ADC RID: 6876 RVA: 0x00068A00 File Offset: 0x00066C00
			public static bool Add(string contentPath, int index, string handle)
			{
				bool result;
				try
				{
					global::NGC.Prefab prefab = new global::NGC.Prefab(contentPath, index, handle);
					global::NGC.Prefab.Register.PrefabByIndex.Add(index, prefab);
					try
					{
						global::NGC.Prefab.Register.PrefabByName.Add(handle, prefab);
					}
					catch
					{
						global::NGC.Prefab.Register.PrefabByIndex.Remove(index);
						throw;
					}
					global::NGC.Prefab.Register.Prefabs.Add(prefab);
					result = true;
				}
				catch
				{
					result = false;
				}
				return result;
			}

			// Token: 0x04000EB1 RID: 3761
			private static readonly Dictionary<int, global::NGC.Prefab> PrefabByIndex = new Dictionary<int, global::NGC.Prefab>();

			// Token: 0x04000EB2 RID: 3762
			private static readonly Dictionary<string, global::NGC.Prefab> PrefabByName = new Dictionary<string, global::NGC.Prefab>();

			// Token: 0x04000EB3 RID: 3763
			private static readonly List<global::NGC.Prefab> Prefabs = new List<global::NGC.Prefab>();
		}
	}

	// Token: 0x02000303 RID: 771
	public sealed class Procedure
	{
		// Token: 0x06001ADE RID: 6878 RVA: 0x00068AA8 File Offset: 0x00066CA8
		public BitStream CreateBitStream()
		{
			if (this.dataLength == 0)
			{
				return new BitStream(false);
			}
			return new BitStream(this.data, false);
		}

		// Token: 0x06001ADF RID: 6879 RVA: 0x00068AC8 File Offset: 0x00066CC8
		public bool Call()
		{
			if (!this.view)
			{
				try
				{
					this.view = this.outer.views[(ushort)this.target];
				}
				catch (KeyNotFoundException)
				{
					return false;
				}
			}
			try
			{
				this.view.install.Invoke(this);
			}
			catch (Exception ex)
			{
				Debug.LogException(ex, this.view);
			}
			return true;
		}

		// Token: 0x04000EB4 RID: 3764
		public global::NGC outer;

		// Token: 0x04000EB5 RID: 3765
		public int target;

		// Token: 0x04000EB6 RID: 3766
		public int message;

		// Token: 0x04000EB7 RID: 3767
		public byte[] data;

		// Token: 0x04000EB8 RID: 3768
		public int dataLength;

		// Token: 0x04000EB9 RID: 3769
		public uLink.NetworkMessageInfo info;

		// Token: 0x04000EBA RID: 3770
		public global::NGCView view;
	}

	// Token: 0x02000304 RID: 772
	private struct RPCName
	{
		// Token: 0x06001AE0 RID: 6880 RVA: 0x00068B74 File Offset: 0x00066D74
		public RPCName(global::NGCView view, int message, string name, NetworkFlags flags)
		{
			this.name = name;
			this.flags = flags;
		}

		// Token: 0x04000EBB RID: 3771
		public readonly string name;

		// Token: 0x04000EBC RID: 3772
		public readonly NetworkFlags flags;
	}

	// Token: 0x02000305 RID: 773
	public static class callf
	{
		// Token: 0x06001AE1 RID: 6881 RVA: 0x00068B88 File Offset: 0x00066D88
		public static void InvokeCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf.Call), instance, method, true);
			}
			((global::NGC.callf.Call)d)();
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x00068BB4 File Offset: 0x00066DB4
		public static IEnumerator InvokeRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf.Routine), instance, method, true);
			}
			return ((global::NGC.callf.Routine)d)();
		}

		// Token: 0x06001AE3 RID: 6883 RVA: 0x00068BE0 File Offset: 0x00066DE0
		public static void InvokeInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf.InfoCall), instance, method, true);
			}
			((global::NGC.callf.InfoCall)d)(info);
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x00068C0C File Offset: 0x00066E0C
		public static IEnumerator InvokeInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf.InfoRoutine), instance, method, true);
			}
			return ((global::NGC.callf.InfoRoutine)d)(info);
		}

		// Token: 0x06001AE5 RID: 6885 RVA: 0x00068C38 File Offset: 0x00066E38
		public static void InvokeStreamCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf.StreamCall), instance, method, true);
			}
			((global::NGC.callf.StreamCall)d)(stream);
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x00068C70 File Offset: 0x00066E70
		public static IEnumerator InvokeStreamRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf.StreamRoutine), instance, method, true);
			}
			return ((global::NGC.callf.StreamRoutine)d)(stream);
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x00068CA8 File Offset: 0x00066EA8
		public static void InvokeStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf.StreamInfoCall), instance, method, true);
			}
			((global::NGC.callf.StreamInfoCall)d)(info, stream);
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x00068CE0 File Offset: 0x00066EE0
		public static IEnumerator InvokeStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf.StreamInfoRoutine), instance, method, true);
			}
			return ((global::NGC.callf.StreamInfoRoutine)d)(info, stream);
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06001AE9 RID: 6889 RVA: 0x00068D18 File Offset: 0x00066F18
		public static global::NGC.IExecuter Exec
		{
			get
			{
				return global::NGC.callf.Executer.Singleton;
			}
		}

		// Token: 0x02000306 RID: 774
		private sealed class Executer : global::NGC.IExecuter
		{
			// Token: 0x06001AEC RID: 6892 RVA: 0x00068D34 File Offset: 0x00066F34
			public void ExecuteCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				global::NGC.callf.InvokeCall(stream, ref d, method, instance);
			}

			// Token: 0x06001AED RID: 6893 RVA: 0x00068D40 File Offset: 0x00066F40
			public IEnumerator ExecuteRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return global::NGC.callf.InvokeRoutine(stream, ref d, method, instance);
			}

			// Token: 0x06001AEE RID: 6894 RVA: 0x00068D4C File Offset: 0x00066F4C
			public void ExecuteInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				global::NGC.callf.InvokeInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x06001AEF RID: 6895 RVA: 0x00068D5C File Offset: 0x00066F5C
			public IEnumerator ExecuteInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				return global::NGC.callf.InvokeInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x06001AF0 RID: 6896 RVA: 0x00068D6C File Offset: 0x00066F6C
			public void ExecuteStreamCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				global::NGC.callf.InvokeStreamCall(stream, ref d, method, instance);
			}

			// Token: 0x06001AF1 RID: 6897 RVA: 0x00068D78 File Offset: 0x00066F78
			public IEnumerator ExecuteStreamRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return global::NGC.callf.InvokeStreamRoutine(stream, ref d, method, instance);
			}

			// Token: 0x06001AF2 RID: 6898 RVA: 0x00068D84 File Offset: 0x00066F84
			public void ExecuteStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				global::NGC.callf.InvokeStreamInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x06001AF3 RID: 6899 RVA: 0x00068D94 File Offset: 0x00066F94
			public IEnumerator ExecuteStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				return global::NGC.callf.InvokeStreamInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x04000EBD RID: 3773
			public static readonly global::NGC.IExecuter Singleton = new global::NGC.callf.Executer();
		}

		// Token: 0x02000307 RID: 775
		// (Invoke) Token: 0x06001AF5 RID: 6901
		public delegate void Call();

		// Token: 0x02000308 RID: 776
		// (Invoke) Token: 0x06001AF9 RID: 6905
		public delegate IEnumerator Routine();

		// Token: 0x02000309 RID: 777
		// (Invoke) Token: 0x06001AFD RID: 6909
		public delegate void InfoCall(uLink.NetworkMessageInfo info);

		// Token: 0x0200030A RID: 778
		// (Invoke) Token: 0x06001B01 RID: 6913
		public delegate IEnumerator InfoRoutine(uLink.NetworkMessageInfo info);

		// Token: 0x0200030B RID: 779
		// (Invoke) Token: 0x06001B05 RID: 6917
		public delegate void StreamCall(BitStream stream);

		// Token: 0x0200030C RID: 780
		// (Invoke) Token: 0x06001B09 RID: 6921
		public delegate IEnumerator StreamRoutine(BitStream stream);

		// Token: 0x0200030D RID: 781
		// (Invoke) Token: 0x06001B0D RID: 6925
		public delegate void StreamInfoCall(uLink.NetworkMessageInfo info, BitStream stream);

		// Token: 0x0200030E RID: 782
		// (Invoke) Token: 0x06001B11 RID: 6929
		public delegate IEnumerator StreamInfoRoutine(uLink.NetworkMessageInfo info, BitStream stream);
	}

	// Token: 0x0200030F RID: 783
	public static class callf<P0>
	{
		// Token: 0x06001B14 RID: 6932 RVA: 0x00068DA4 File Offset: 0x00066FA4
		static callf()
		{
			BitStreamCodec.Add<global::NGC.callf<P0>.Block>(new BitStreamCodec.Deserializer(global::NGC.callf<P0>.Deserializer), new BitStreamCodec.Serializer(global::NGC.callf<P0>.Serializer));
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x00068DC4 File Offset: 0x00066FC4
		private static object Deserializer(BitStream stream, params object[] codecOptions)
		{
			global::NGC.callf<P0>.Block block;
			block.p0 = stream.Read<P0>(codecOptions);
			return block;
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x00068DE8 File Offset: 0x00066FE8
		private static void Serializer(BitStream stream, object value, params object[] codecOptions)
		{
			stream.Write<P0>(((global::NGC.callf<P0>.Block)value).p0, codecOptions);
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x00068E0C File Offset: 0x0006700C
		public static void InvokeCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0>.Call), instance, method, true);
			}
			((global::NGC.callf<P0>.Call)d)(p);
		}

		// Token: 0x06001B18 RID: 6936 RVA: 0x00068E50 File Offset: 0x00067050
		public static IEnumerator InvokeRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0>.Routine), instance, method, true);
			}
			return ((global::NGC.callf<P0>.Routine)d)(p);
		}

		// Token: 0x06001B19 RID: 6937 RVA: 0x00068E94 File Offset: 0x00067094
		public static void InvokeInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0>.InfoCall), instance, method, true);
			}
			((global::NGC.callf<P0>.InfoCall)d)(p, info);
		}

		// Token: 0x06001B1A RID: 6938 RVA: 0x00068ED8 File Offset: 0x000670D8
		public static IEnumerator InvokeInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0>.InfoRoutine), instance, method, true);
			}
			return ((global::NGC.callf<P0>.InfoRoutine)d)(p, info);
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x00068F1C File Offset: 0x0006711C
		public static void InvokeStreamCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0>.StreamCall), instance, method, true);
			}
			((global::NGC.callf<P0>.StreamCall)d)(p, stream);
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x00068F60 File Offset: 0x00067160
		public static IEnumerator InvokeStreamRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0>.StreamRoutine), instance, method, true);
			}
			return ((global::NGC.callf<P0>.StreamRoutine)d)(p, stream);
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x00068FA4 File Offset: 0x000671A4
		public static void InvokeStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0>.StreamInfoCall), instance, method, true);
			}
			((global::NGC.callf<P0>.StreamInfoCall)d)(p, info, stream);
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x00068FEC File Offset: 0x000671EC
		public static IEnumerator InvokeStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0>.StreamInfoRoutine), instance, method, true);
			}
			return ((global::NGC.callf<P0>.StreamInfoRoutine)d)(p, info, stream);
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06001B1F RID: 6943 RVA: 0x00069034 File Offset: 0x00067234
		public static global::NGC.IExecuter Exec
		{
			get
			{
				return global::NGC.callf<P0>.Executer.Singleton;
			}
		}

		// Token: 0x02000310 RID: 784
		public struct Block
		{
			// Token: 0x04000EBE RID: 3774
			public P0 p0;
		}

		// Token: 0x02000311 RID: 785
		private sealed class Executer : global::NGC.IExecuter
		{
			// Token: 0x06001B22 RID: 6946 RVA: 0x00069050 File Offset: 0x00067250
			public void ExecuteCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				global::NGC.callf<P0>.InvokeCall(stream, ref d, method, instance);
			}

			// Token: 0x06001B23 RID: 6947 RVA: 0x0006905C File Offset: 0x0006725C
			public IEnumerator ExecuteRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return global::NGC.callf<P0>.InvokeRoutine(stream, ref d, method, instance);
			}

			// Token: 0x06001B24 RID: 6948 RVA: 0x00069068 File Offset: 0x00067268
			public void ExecuteInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				global::NGC.callf<P0>.InvokeInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x06001B25 RID: 6949 RVA: 0x00069078 File Offset: 0x00067278
			public IEnumerator ExecuteInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				return global::NGC.callf<P0>.InvokeInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x06001B26 RID: 6950 RVA: 0x00069088 File Offset: 0x00067288
			public void ExecuteStreamCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				global::NGC.callf<P0>.InvokeStreamCall(stream, ref d, method, instance);
			}

			// Token: 0x06001B27 RID: 6951 RVA: 0x00069094 File Offset: 0x00067294
			public IEnumerator ExecuteStreamRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return global::NGC.callf<P0>.InvokeStreamRoutine(stream, ref d, method, instance);
			}

			// Token: 0x06001B28 RID: 6952 RVA: 0x000690A0 File Offset: 0x000672A0
			public void ExecuteStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				global::NGC.callf<P0>.InvokeStreamInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x06001B29 RID: 6953 RVA: 0x000690B0 File Offset: 0x000672B0
			public IEnumerator ExecuteStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				return global::NGC.callf<P0>.InvokeStreamInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x04000EBF RID: 3775
			public static readonly global::NGC.IExecuter Singleton = new global::NGC.callf<P0>.Executer();
		}

		// Token: 0x02000312 RID: 786
		// (Invoke) Token: 0x06001B2B RID: 6955
		public delegate void Call(P0 p0);

		// Token: 0x02000313 RID: 787
		// (Invoke) Token: 0x06001B2F RID: 6959
		public delegate IEnumerator Routine(P0 p0);

		// Token: 0x02000314 RID: 788
		// (Invoke) Token: 0x06001B33 RID: 6963
		public delegate void InfoCall(P0 p0, uLink.NetworkMessageInfo info);

		// Token: 0x02000315 RID: 789
		// (Invoke) Token: 0x06001B37 RID: 6967
		public delegate IEnumerator InfoRoutine(P0 p0, uLink.NetworkMessageInfo info);

		// Token: 0x02000316 RID: 790
		// (Invoke) Token: 0x06001B3B RID: 6971
		public delegate void StreamCall(P0 p0, BitStream stream);

		// Token: 0x02000317 RID: 791
		// (Invoke) Token: 0x06001B3F RID: 6975
		public delegate IEnumerator StreamRoutine(P0 p0, BitStream stream);

		// Token: 0x02000318 RID: 792
		// (Invoke) Token: 0x06001B43 RID: 6979
		public delegate void StreamInfoCall(P0 p0, uLink.NetworkMessageInfo info, BitStream stream);

		// Token: 0x02000319 RID: 793
		// (Invoke) Token: 0x06001B47 RID: 6983
		public delegate IEnumerator StreamInfoRoutine(P0 p0, uLink.NetworkMessageInfo info, BitStream stream);
	}

	// Token: 0x0200031A RID: 794
	public static class callf<P0, P1>
	{
		// Token: 0x06001B4A RID: 6986 RVA: 0x000690C0 File Offset: 0x000672C0
		static callf()
		{
			BitStreamCodec.Add<global::NGC.callf<P0, P1>.Block>(new BitStreamCodec.Deserializer(global::NGC.callf<P0, P1>.Deserializer), new BitStreamCodec.Serializer(global::NGC.callf<P0, P1>.Serializer));
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x000690E0 File Offset: 0x000672E0
		private static object Deserializer(BitStream stream, params object[] codecOptions)
		{
			global::NGC.callf<P0, P1>.Block block;
			block.p0 = stream.Read<P0>(codecOptions);
			block.p1 = stream.Read<P1>(codecOptions);
			return block;
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x00069110 File Offset: 0x00067310
		private static void Serializer(BitStream stream, object value, params object[] codecOptions)
		{
			global::NGC.callf<P0, P1>.Block block = (global::NGC.callf<P0, P1>.Block)value;
			stream.Write<P0>(block.p0, codecOptions);
			stream.Write<P1>(block.p1, codecOptions);
		}

		// Token: 0x06001B4D RID: 6989 RVA: 0x00069140 File Offset: 0x00067340
		public static void InvokeCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1>.Call), instance, method, true);
			}
			((global::NGC.callf<P0, P1>.Call)d)(p, p2);
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x00069190 File Offset: 0x00067390
		public static IEnumerator InvokeRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1>.Routine), instance, method, true);
			}
			return ((global::NGC.callf<P0, P1>.Routine)d)(p, p2);
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x000691E0 File Offset: 0x000673E0
		public static void InvokeInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1>.InfoCall), instance, method, true);
			}
			((global::NGC.callf<P0, P1>.InfoCall)d)(p, p2, info);
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x00069234 File Offset: 0x00067434
		public static IEnumerator InvokeInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1>.InfoRoutine), instance, method, true);
			}
			return ((global::NGC.callf<P0, P1>.InfoRoutine)d)(p, p2, info);
		}

		// Token: 0x06001B51 RID: 6993 RVA: 0x00069288 File Offset: 0x00067488
		public static void InvokeStreamCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1>.StreamCall), instance, method, true);
			}
			((global::NGC.callf<P0, P1>.StreamCall)d)(p, p2, stream);
		}

		// Token: 0x06001B52 RID: 6994 RVA: 0x000692DC File Offset: 0x000674DC
		public static IEnumerator InvokeStreamRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1>.StreamRoutine), instance, method, true);
			}
			return ((global::NGC.callf<P0, P1>.StreamRoutine)d)(p, p2, stream);
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x00069330 File Offset: 0x00067530
		public static void InvokeStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1>.StreamInfoCall), instance, method, true);
			}
			((global::NGC.callf<P0, P1>.StreamInfoCall)d)(p, p2, info, stream);
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x00069384 File Offset: 0x00067584
		public static IEnumerator InvokeStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1>.StreamInfoRoutine), instance, method, true);
			}
			return ((global::NGC.callf<P0, P1>.StreamInfoRoutine)d)(p, p2, info, stream);
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06001B55 RID: 6997 RVA: 0x000693D8 File Offset: 0x000675D8
		public static global::NGC.IExecuter Exec
		{
			get
			{
				return global::NGC.callf<P0, P1>.Executer.Singleton;
			}
		}

		// Token: 0x0200031B RID: 795
		public struct Block
		{
			// Token: 0x04000EC0 RID: 3776
			public P0 p0;

			// Token: 0x04000EC1 RID: 3777
			public P1 p1;
		}

		// Token: 0x0200031C RID: 796
		private sealed class Executer : global::NGC.IExecuter
		{
			// Token: 0x06001B58 RID: 7000 RVA: 0x000693F4 File Offset: 0x000675F4
			public void ExecuteCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				global::NGC.callf<P0, P1>.InvokeCall(stream, ref d, method, instance);
			}

			// Token: 0x06001B59 RID: 7001 RVA: 0x00069400 File Offset: 0x00067600
			public IEnumerator ExecuteRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return global::NGC.callf<P0, P1>.InvokeRoutine(stream, ref d, method, instance);
			}

			// Token: 0x06001B5A RID: 7002 RVA: 0x0006940C File Offset: 0x0006760C
			public void ExecuteInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				global::NGC.callf<P0, P1>.InvokeInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x06001B5B RID: 7003 RVA: 0x0006941C File Offset: 0x0006761C
			public IEnumerator ExecuteInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				return global::NGC.callf<P0, P1>.InvokeInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x06001B5C RID: 7004 RVA: 0x0006942C File Offset: 0x0006762C
			public void ExecuteStreamCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				global::NGC.callf<P0, P1>.InvokeStreamCall(stream, ref d, method, instance);
			}

			// Token: 0x06001B5D RID: 7005 RVA: 0x00069438 File Offset: 0x00067638
			public IEnumerator ExecuteStreamRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return global::NGC.callf<P0, P1>.InvokeStreamRoutine(stream, ref d, method, instance);
			}

			// Token: 0x06001B5E RID: 7006 RVA: 0x00069444 File Offset: 0x00067644
			public void ExecuteStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				global::NGC.callf<P0, P1>.InvokeStreamInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x06001B5F RID: 7007 RVA: 0x00069454 File Offset: 0x00067654
			public IEnumerator ExecuteStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				return global::NGC.callf<P0, P1>.InvokeStreamInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x04000EC2 RID: 3778
			public static readonly global::NGC.IExecuter Singleton = new global::NGC.callf<P0, P1>.Executer();
		}

		// Token: 0x0200031D RID: 797
		// (Invoke) Token: 0x06001B61 RID: 7009
		public delegate void Call(P0 p0, P1 p1);

		// Token: 0x0200031E RID: 798
		// (Invoke) Token: 0x06001B65 RID: 7013
		public delegate IEnumerator Routine(P0 p0, P1 p1);

		// Token: 0x0200031F RID: 799
		// (Invoke) Token: 0x06001B69 RID: 7017
		public delegate void InfoCall(P0 p0, P1 p1, uLink.NetworkMessageInfo info);

		// Token: 0x02000320 RID: 800
		// (Invoke) Token: 0x06001B6D RID: 7021
		public delegate IEnumerator InfoRoutine(P0 p0, P1 p1, uLink.NetworkMessageInfo info);

		// Token: 0x02000321 RID: 801
		// (Invoke) Token: 0x06001B71 RID: 7025
		public delegate void StreamCall(P0 p0, P1 p1, BitStream stream);

		// Token: 0x02000322 RID: 802
		// (Invoke) Token: 0x06001B75 RID: 7029
		public delegate IEnumerator StreamRoutine(P0 p0, P1 p1, BitStream stream);

		// Token: 0x02000323 RID: 803
		// (Invoke) Token: 0x06001B79 RID: 7033
		public delegate void StreamInfoCall(P0 p0, P1 p1, uLink.NetworkMessageInfo info, BitStream stream);

		// Token: 0x02000324 RID: 804
		// (Invoke) Token: 0x06001B7D RID: 7037
		public delegate IEnumerator StreamInfoRoutine(P0 p0, P1 p1, uLink.NetworkMessageInfo info, BitStream stream);
	}

	// Token: 0x02000325 RID: 805
	public static class callf<P0, P1, P2>
	{
		// Token: 0x06001B80 RID: 7040 RVA: 0x00069464 File Offset: 0x00067664
		static callf()
		{
			BitStreamCodec.Add<global::NGC.callf<P0, P1, P2>.Block>(new BitStreamCodec.Deserializer(global::NGC.callf<P0, P1, P2>.Deserializer), new BitStreamCodec.Serializer(global::NGC.callf<P0, P1, P2>.Serializer));
		}

		// Token: 0x06001B81 RID: 7041 RVA: 0x00069484 File Offset: 0x00067684
		private static object Deserializer(BitStream stream, params object[] codecOptions)
		{
			global::NGC.callf<P0, P1, P2>.Block block;
			block.p0 = stream.Read<P0>(codecOptions);
			block.p1 = stream.Read<P1>(codecOptions);
			block.p2 = stream.Read<P2>(codecOptions);
			return block;
		}

		// Token: 0x06001B82 RID: 7042 RVA: 0x000694C4 File Offset: 0x000676C4
		private static void Serializer(BitStream stream, object value, params object[] codecOptions)
		{
			global::NGC.callf<P0, P1, P2>.Block block = (global::NGC.callf<P0, P1, P2>.Block)value;
			stream.Write<P0>(block.p0, codecOptions);
			stream.Write<P1>(block.p1, codecOptions);
			stream.Write<P2>(block.p2, codecOptions);
		}

		// Token: 0x06001B83 RID: 7043 RVA: 0x00069504 File Offset: 0x00067704
		public static void InvokeCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2>.Call), instance, method, true);
			}
			((global::NGC.callf<P0, P1, P2>.Call)d)(p, p2, p3);
		}

		// Token: 0x06001B84 RID: 7044 RVA: 0x00069564 File Offset: 0x00067764
		public static IEnumerator InvokeRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2>.Routine), instance, method, true);
			}
			return ((global::NGC.callf<P0, P1, P2>.Routine)d)(p, p2, p3);
		}

		// Token: 0x06001B85 RID: 7045 RVA: 0x000695C4 File Offset: 0x000677C4
		public static void InvokeInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2>.InfoCall), instance, method, true);
			}
			((global::NGC.callf<P0, P1, P2>.InfoCall)d)(p, p2, p3, info);
		}

		// Token: 0x06001B86 RID: 7046 RVA: 0x00069624 File Offset: 0x00067824
		public static IEnumerator InvokeInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2>.InfoRoutine), instance, method, true);
			}
			return ((global::NGC.callf<P0, P1, P2>.InfoRoutine)d)(p, p2, p3, info);
		}

		// Token: 0x06001B87 RID: 7047 RVA: 0x00069684 File Offset: 0x00067884
		public static void InvokeStreamCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2>.StreamCall), instance, method, true);
			}
			((global::NGC.callf<P0, P1, P2>.StreamCall)d)(p, p2, p3, stream);
		}

		// Token: 0x06001B88 RID: 7048 RVA: 0x000696E4 File Offset: 0x000678E4
		public static IEnumerator InvokeStreamRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2>.StreamRoutine), instance, method, true);
			}
			return ((global::NGC.callf<P0, P1, P2>.StreamRoutine)d)(p, p2, p3, stream);
		}

		// Token: 0x06001B89 RID: 7049 RVA: 0x00069744 File Offset: 0x00067944
		public static void InvokeStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2>.StreamInfoCall), instance, method, true);
			}
			((global::NGC.callf<P0, P1, P2>.StreamInfoCall)d)(p, p2, p3, info, stream);
		}

		// Token: 0x06001B8A RID: 7050 RVA: 0x000697A8 File Offset: 0x000679A8
		public static IEnumerator InvokeStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2>.StreamInfoRoutine), instance, method, true);
			}
			return ((global::NGC.callf<P0, P1, P2>.StreamInfoRoutine)d)(p, p2, p3, info, stream);
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06001B8B RID: 7051 RVA: 0x0006980C File Offset: 0x00067A0C
		public static global::NGC.IExecuter Exec
		{
			get
			{
				return global::NGC.callf<P0, P1, P2>.Executer.Singleton;
			}
		}

		// Token: 0x02000326 RID: 806
		public struct Block
		{
			// Token: 0x04000EC3 RID: 3779
			public P0 p0;

			// Token: 0x04000EC4 RID: 3780
			public P1 p1;

			// Token: 0x04000EC5 RID: 3781
			public P2 p2;
		}

		// Token: 0x02000327 RID: 807
		private sealed class Executer : global::NGC.IExecuter
		{
			// Token: 0x06001B8E RID: 7054 RVA: 0x00069828 File Offset: 0x00067A28
			public void ExecuteCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				global::NGC.callf<P0, P1, P2>.InvokeCall(stream, ref d, method, instance);
			}

			// Token: 0x06001B8F RID: 7055 RVA: 0x00069834 File Offset: 0x00067A34
			public IEnumerator ExecuteRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return global::NGC.callf<P0, P1, P2>.InvokeRoutine(stream, ref d, method, instance);
			}

			// Token: 0x06001B90 RID: 7056 RVA: 0x00069840 File Offset: 0x00067A40
			public void ExecuteInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				global::NGC.callf<P0, P1, P2>.InvokeInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x06001B91 RID: 7057 RVA: 0x00069850 File Offset: 0x00067A50
			public IEnumerator ExecuteInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				return global::NGC.callf<P0, P1, P2>.InvokeInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x06001B92 RID: 7058 RVA: 0x00069860 File Offset: 0x00067A60
			public void ExecuteStreamCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				global::NGC.callf<P0, P1, P2>.InvokeStreamCall(stream, ref d, method, instance);
			}

			// Token: 0x06001B93 RID: 7059 RVA: 0x0006986C File Offset: 0x00067A6C
			public IEnumerator ExecuteStreamRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return global::NGC.callf<P0, P1, P2>.InvokeStreamRoutine(stream, ref d, method, instance);
			}

			// Token: 0x06001B94 RID: 7060 RVA: 0x00069878 File Offset: 0x00067A78
			public void ExecuteStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				global::NGC.callf<P0, P1, P2>.InvokeStreamInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x06001B95 RID: 7061 RVA: 0x00069888 File Offset: 0x00067A88
			public IEnumerator ExecuteStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				return global::NGC.callf<P0, P1, P2>.InvokeStreamInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x04000EC6 RID: 3782
			public static readonly global::NGC.IExecuter Singleton = new global::NGC.callf<P0, P1, P2>.Executer();
		}

		// Token: 0x02000328 RID: 808
		// (Invoke) Token: 0x06001B97 RID: 7063
		public delegate void Call(P0 p0, P1 p1, P2 p2);

		// Token: 0x02000329 RID: 809
		// (Invoke) Token: 0x06001B9B RID: 7067
		public delegate IEnumerator Routine(P0 p0, P1 p1, P2 p2);

		// Token: 0x0200032A RID: 810
		// (Invoke) Token: 0x06001B9F RID: 7071
		public delegate void InfoCall(P0 p0, P1 p1, P2 p2, uLink.NetworkMessageInfo info);

		// Token: 0x0200032B RID: 811
		// (Invoke) Token: 0x06001BA3 RID: 7075
		public delegate IEnumerator InfoRoutine(P0 p0, P1 p1, P2 p2, uLink.NetworkMessageInfo info);

		// Token: 0x0200032C RID: 812
		// (Invoke) Token: 0x06001BA7 RID: 7079
		public delegate void StreamCall(P0 p0, P1 p1, P2 p2, BitStream stream);

		// Token: 0x0200032D RID: 813
		// (Invoke) Token: 0x06001BAB RID: 7083
		public delegate IEnumerator StreamRoutine(P0 p0, P1 p1, P2 p2, BitStream stream);

		// Token: 0x0200032E RID: 814
		// (Invoke) Token: 0x06001BAF RID: 7087
		public delegate void StreamInfoCall(P0 p0, P1 p1, P2 p2, uLink.NetworkMessageInfo info, BitStream stream);

		// Token: 0x0200032F RID: 815
		// (Invoke) Token: 0x06001BB3 RID: 7091
		public delegate IEnumerator StreamInfoRoutine(P0 p0, P1 p1, P2 p2, uLink.NetworkMessageInfo info, BitStream stream);
	}

	// Token: 0x02000330 RID: 816
	public static class callf<P0, P1, P2, P3>
	{
		// Token: 0x06001BB6 RID: 7094 RVA: 0x00069898 File Offset: 0x00067A98
		static callf()
		{
			BitStreamCodec.Add<global::NGC.callf<P0, P1, P2, P3>.Block>(new BitStreamCodec.Deserializer(global::NGC.callf<P0, P1, P2, P3>.Deserializer), new BitStreamCodec.Serializer(global::NGC.callf<P0, P1, P2, P3>.Serializer));
		}

		// Token: 0x06001BB7 RID: 7095 RVA: 0x000698B8 File Offset: 0x00067AB8
		private static object Deserializer(BitStream stream, params object[] codecOptions)
		{
			global::NGC.callf<P0, P1, P2, P3>.Block block;
			block.p0 = stream.Read<P0>(codecOptions);
			block.p1 = stream.Read<P1>(codecOptions);
			block.p2 = stream.Read<P2>(codecOptions);
			block.p3 = stream.Read<P3>(codecOptions);
			return block;
		}

		// Token: 0x06001BB8 RID: 7096 RVA: 0x00069904 File Offset: 0x00067B04
		private static void Serializer(BitStream stream, object value, params object[] codecOptions)
		{
			global::NGC.callf<P0, P1, P2, P3>.Block block = (global::NGC.callf<P0, P1, P2, P3>.Block)value;
			stream.Write<P0>(block.p0, codecOptions);
			stream.Write<P1>(block.p1, codecOptions);
			stream.Write<P2>(block.p2, codecOptions);
			stream.Write<P3>(block.p3, codecOptions);
		}

		// Token: 0x06001BB9 RID: 7097 RVA: 0x00069950 File Offset: 0x00067B50
		public static void InvokeCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3>.Call), instance, method, true);
			}
			((global::NGC.callf<P0, P1, P2, P3>.Call)d)(p, p2, p3, p4);
		}

		// Token: 0x06001BBA RID: 7098 RVA: 0x000699BC File Offset: 0x00067BBC
		public static IEnumerator InvokeRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3>.Routine), instance, method, true);
			}
			return ((global::NGC.callf<P0, P1, P2, P3>.Routine)d)(p, p2, p3, p4);
		}

		// Token: 0x06001BBB RID: 7099 RVA: 0x00069A28 File Offset: 0x00067C28
		public static void InvokeInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3>.InfoCall), instance, method, true);
			}
			((global::NGC.callf<P0, P1, P2, P3>.InfoCall)d)(p, p2, p3, p4, info);
		}

		// Token: 0x06001BBC RID: 7100 RVA: 0x00069A98 File Offset: 0x00067C98
		public static IEnumerator InvokeInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3>.InfoRoutine), instance, method, true);
			}
			return ((global::NGC.callf<P0, P1, P2, P3>.InfoRoutine)d)(p, p2, p3, p4, info);
		}

		// Token: 0x06001BBD RID: 7101 RVA: 0x00069B08 File Offset: 0x00067D08
		public static void InvokeStreamCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3>.StreamCall), instance, method, true);
			}
			((global::NGC.callf<P0, P1, P2, P3>.StreamCall)d)(p, p2, p3, p4, stream);
		}

		// Token: 0x06001BBE RID: 7102 RVA: 0x00069B78 File Offset: 0x00067D78
		public static IEnumerator InvokeStreamRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3>.StreamRoutine), instance, method, true);
			}
			return ((global::NGC.callf<P0, P1, P2, P3>.StreamRoutine)d)(p, p2, p3, p4, stream);
		}

		// Token: 0x06001BBF RID: 7103 RVA: 0x00069BE8 File Offset: 0x00067DE8
		public static void InvokeStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3>.StreamInfoCall), instance, method, true);
			}
			((global::NGC.callf<P0, P1, P2, P3>.StreamInfoCall)d)(p, p2, p3, p4, info, stream);
		}

		// Token: 0x06001BC0 RID: 7104 RVA: 0x00069C58 File Offset: 0x00067E58
		public static IEnumerator InvokeStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3>.StreamInfoRoutine), instance, method, true);
			}
			return ((global::NGC.callf<P0, P1, P2, P3>.StreamInfoRoutine)d)(p, p2, p3, p4, info, stream);
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06001BC1 RID: 7105 RVA: 0x00069CC8 File Offset: 0x00067EC8
		public static global::NGC.IExecuter Exec
		{
			get
			{
				return global::NGC.callf<P0, P1, P2, P3>.Executer.Singleton;
			}
		}

		// Token: 0x02000331 RID: 817
		public struct Block
		{
			// Token: 0x04000EC7 RID: 3783
			public P0 p0;

			// Token: 0x04000EC8 RID: 3784
			public P1 p1;

			// Token: 0x04000EC9 RID: 3785
			public P2 p2;

			// Token: 0x04000ECA RID: 3786
			public P3 p3;
		}

		// Token: 0x02000332 RID: 818
		private sealed class Executer : global::NGC.IExecuter
		{
			// Token: 0x06001BC4 RID: 7108 RVA: 0x00069CE4 File Offset: 0x00067EE4
			public void ExecuteCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				global::NGC.callf<P0, P1, P2, P3>.InvokeCall(stream, ref d, method, instance);
			}

			// Token: 0x06001BC5 RID: 7109 RVA: 0x00069CF0 File Offset: 0x00067EF0
			public IEnumerator ExecuteRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return global::NGC.callf<P0, P1, P2, P3>.InvokeRoutine(stream, ref d, method, instance);
			}

			// Token: 0x06001BC6 RID: 7110 RVA: 0x00069CFC File Offset: 0x00067EFC
			public void ExecuteInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				global::NGC.callf<P0, P1, P2, P3>.InvokeInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x06001BC7 RID: 7111 RVA: 0x00069D0C File Offset: 0x00067F0C
			public IEnumerator ExecuteInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				return global::NGC.callf<P0, P1, P2, P3>.InvokeInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x06001BC8 RID: 7112 RVA: 0x00069D1C File Offset: 0x00067F1C
			public void ExecuteStreamCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				global::NGC.callf<P0, P1, P2, P3>.InvokeStreamCall(stream, ref d, method, instance);
			}

			// Token: 0x06001BC9 RID: 7113 RVA: 0x00069D28 File Offset: 0x00067F28
			public IEnumerator ExecuteStreamRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return global::NGC.callf<P0, P1, P2, P3>.InvokeStreamRoutine(stream, ref d, method, instance);
			}

			// Token: 0x06001BCA RID: 7114 RVA: 0x00069D34 File Offset: 0x00067F34
			public void ExecuteStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				global::NGC.callf<P0, P1, P2, P3>.InvokeStreamInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x06001BCB RID: 7115 RVA: 0x00069D44 File Offset: 0x00067F44
			public IEnumerator ExecuteStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				return global::NGC.callf<P0, P1, P2, P3>.InvokeStreamInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x04000ECB RID: 3787
			public static readonly global::NGC.IExecuter Singleton = new global::NGC.callf<P0, P1, P2, P3>.Executer();
		}

		// Token: 0x02000333 RID: 819
		// (Invoke) Token: 0x06001BCD RID: 7117
		public delegate void Call(P0 p0, P1 p1, P2 p2, P3 p3);

		// Token: 0x02000334 RID: 820
		// (Invoke) Token: 0x06001BD1 RID: 7121
		public delegate IEnumerator Routine(P0 p0, P1 p1, P2 p2, P3 p3);

		// Token: 0x02000335 RID: 821
		// (Invoke) Token: 0x06001BD5 RID: 7125
		public delegate void InfoCall(P0 p0, P1 p1, P2 p2, P3 p3, uLink.NetworkMessageInfo info);

		// Token: 0x02000336 RID: 822
		// (Invoke) Token: 0x06001BD9 RID: 7129
		public delegate IEnumerator InfoRoutine(P0 p0, P1 p1, P2 p2, P3 p3, uLink.NetworkMessageInfo info);

		// Token: 0x02000337 RID: 823
		// (Invoke) Token: 0x06001BDD RID: 7133
		public delegate void StreamCall(P0 p0, P1 p1, P2 p2, P3 p3, BitStream stream);

		// Token: 0x02000338 RID: 824
		// (Invoke) Token: 0x06001BE1 RID: 7137
		public delegate IEnumerator StreamRoutine(P0 p0, P1 p1, P2 p2, P3 p3, BitStream stream);

		// Token: 0x02000339 RID: 825
		// (Invoke) Token: 0x06001BE5 RID: 7141
		public delegate void StreamInfoCall(P0 p0, P1 p1, P2 p2, P3 p3, uLink.NetworkMessageInfo info, BitStream stream);

		// Token: 0x0200033A RID: 826
		// (Invoke) Token: 0x06001BE9 RID: 7145
		public delegate IEnumerator StreamInfoRoutine(P0 p0, P1 p1, P2 p2, P3 p3, uLink.NetworkMessageInfo info, BitStream stream);
	}

	// Token: 0x0200033B RID: 827
	public static class callf<P0, P1, P2, P3, P4>
	{
		// Token: 0x06001BEC RID: 7148 RVA: 0x00069D54 File Offset: 0x00067F54
		static callf()
		{
			BitStreamCodec.Add<global::NGC.callf<P0, P1, P2, P3, P4>.Block>(new BitStreamCodec.Deserializer(global::NGC.callf<P0, P1, P2, P3, P4>.Deserializer), new BitStreamCodec.Serializer(global::NGC.callf<P0, P1, P2, P3, P4>.Serializer));
		}

		// Token: 0x06001BED RID: 7149 RVA: 0x00069D74 File Offset: 0x00067F74
		private static object Deserializer(BitStream stream, params object[] codecOptions)
		{
			global::NGC.callf<P0, P1, P2, P3, P4>.Block block;
			block.p0 = stream.Read<P0>(codecOptions);
			block.p1 = stream.Read<P1>(codecOptions);
			block.p2 = stream.Read<P2>(codecOptions);
			block.p3 = stream.Read<P3>(codecOptions);
			block.p4 = stream.Read<P4>(codecOptions);
			return block;
		}

		// Token: 0x06001BEE RID: 7150 RVA: 0x00069DD0 File Offset: 0x00067FD0
		private static void Serializer(BitStream stream, object value, params object[] codecOptions)
		{
			global::NGC.callf<P0, P1, P2, P3, P4>.Block block = (global::NGC.callf<P0, P1, P2, P3, P4>.Block)value;
			stream.Write<P0>(block.p0, codecOptions);
			stream.Write<P1>(block.p1, codecOptions);
			stream.Write<P2>(block.p2, codecOptions);
			stream.Write<P3>(block.p3, codecOptions);
			stream.Write<P4>(block.p4, codecOptions);
		}

		// Token: 0x06001BEF RID: 7151 RVA: 0x00069E2C File Offset: 0x0006802C
		public static void InvokeCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4>.Call), instance, method, true);
			}
			((global::NGC.callf<P0, P1, P2, P3, P4>.Call)d)(p, p2, p3, p4, p5);
		}

		// Token: 0x06001BF0 RID: 7152 RVA: 0x00069EA8 File Offset: 0x000680A8
		public static IEnumerator InvokeRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4>.Routine), instance, method, true);
			}
			return ((global::NGC.callf<P0, P1, P2, P3, P4>.Routine)d)(p, p2, p3, p4, p5);
		}

		// Token: 0x06001BF1 RID: 7153 RVA: 0x00069F24 File Offset: 0x00068124
		public static void InvokeInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4>.InfoCall), instance, method, true);
			}
			((global::NGC.callf<P0, P1, P2, P3, P4>.InfoCall)d)(p, p2, p3, p4, p5, info);
		}

		// Token: 0x06001BF2 RID: 7154 RVA: 0x00069FA4 File Offset: 0x000681A4
		public static IEnumerator InvokeInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4>.InfoRoutine), instance, method, true);
			}
			return ((global::NGC.callf<P0, P1, P2, P3, P4>.InfoRoutine)d)(p, p2, p3, p4, p5, info);
		}

		// Token: 0x06001BF3 RID: 7155 RVA: 0x0006A024 File Offset: 0x00068224
		public static void InvokeStreamCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4>.StreamCall), instance, method, true);
			}
			((global::NGC.callf<P0, P1, P2, P3, P4>.StreamCall)d)(p, p2, p3, p4, p5, stream);
		}

		// Token: 0x06001BF4 RID: 7156 RVA: 0x0006A0A4 File Offset: 0x000682A4
		public static IEnumerator InvokeStreamRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4>.StreamRoutine), instance, method, true);
			}
			return ((global::NGC.callf<P0, P1, P2, P3, P4>.StreamRoutine)d)(p, p2, p3, p4, p5, stream);
		}

		// Token: 0x06001BF5 RID: 7157 RVA: 0x0006A124 File Offset: 0x00068324
		public static void InvokeStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4>.StreamInfoCall), instance, method, true);
			}
			((global::NGC.callf<P0, P1, P2, P3, P4>.StreamInfoCall)d)(p, p2, p3, p4, p5, info, stream);
		}

		// Token: 0x06001BF6 RID: 7158 RVA: 0x0006A1A4 File Offset: 0x000683A4
		public static IEnumerator InvokeStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4>.StreamInfoRoutine), instance, method, true);
			}
			return ((global::NGC.callf<P0, P1, P2, P3, P4>.StreamInfoRoutine)d)(p, p2, p3, p4, p5, info, stream);
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06001BF7 RID: 7159 RVA: 0x0006A224 File Offset: 0x00068424
		public static global::NGC.IExecuter Exec
		{
			get
			{
				return global::NGC.callf<P0, P1, P2, P3, P4>.Executer.Singleton;
			}
		}

		// Token: 0x0200033C RID: 828
		public struct Block
		{
			// Token: 0x04000ECC RID: 3788
			public P0 p0;

			// Token: 0x04000ECD RID: 3789
			public P1 p1;

			// Token: 0x04000ECE RID: 3790
			public P2 p2;

			// Token: 0x04000ECF RID: 3791
			public P3 p3;

			// Token: 0x04000ED0 RID: 3792
			public P4 p4;
		}

		// Token: 0x0200033D RID: 829
		private sealed class Executer : global::NGC.IExecuter
		{
			// Token: 0x06001BFA RID: 7162 RVA: 0x0006A240 File Offset: 0x00068440
			public void ExecuteCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				global::NGC.callf<P0, P1, P2, P3, P4>.InvokeCall(stream, ref d, method, instance);
			}

			// Token: 0x06001BFB RID: 7163 RVA: 0x0006A24C File Offset: 0x0006844C
			public IEnumerator ExecuteRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return global::NGC.callf<P0, P1, P2, P3, P4>.InvokeRoutine(stream, ref d, method, instance);
			}

			// Token: 0x06001BFC RID: 7164 RVA: 0x0006A258 File Offset: 0x00068458
			public void ExecuteInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				global::NGC.callf<P0, P1, P2, P3, P4>.InvokeInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x06001BFD RID: 7165 RVA: 0x0006A268 File Offset: 0x00068468
			public IEnumerator ExecuteInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				return global::NGC.callf<P0, P1, P2, P3, P4>.InvokeInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x06001BFE RID: 7166 RVA: 0x0006A278 File Offset: 0x00068478
			public void ExecuteStreamCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				global::NGC.callf<P0, P1, P2, P3, P4>.InvokeStreamCall(stream, ref d, method, instance);
			}

			// Token: 0x06001BFF RID: 7167 RVA: 0x0006A284 File Offset: 0x00068484
			public IEnumerator ExecuteStreamRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return global::NGC.callf<P0, P1, P2, P3, P4>.InvokeStreamRoutine(stream, ref d, method, instance);
			}

			// Token: 0x06001C00 RID: 7168 RVA: 0x0006A290 File Offset: 0x00068490
			public void ExecuteStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				global::NGC.callf<P0, P1, P2, P3, P4>.InvokeStreamInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x06001C01 RID: 7169 RVA: 0x0006A2A0 File Offset: 0x000684A0
			public IEnumerator ExecuteStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				return global::NGC.callf<P0, P1, P2, P3, P4>.InvokeStreamInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x04000ED1 RID: 3793
			public static readonly global::NGC.IExecuter Singleton = new global::NGC.callf<P0, P1, P2, P3, P4>.Executer();
		}

		// Token: 0x0200033E RID: 830
		// (Invoke) Token: 0x06001C03 RID: 7171
		public delegate void Call(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4);

		// Token: 0x0200033F RID: 831
		// (Invoke) Token: 0x06001C07 RID: 7175
		public delegate IEnumerator Routine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4);

		// Token: 0x02000340 RID: 832
		// (Invoke) Token: 0x06001C0B RID: 7179
		public delegate void InfoCall(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, uLink.NetworkMessageInfo info);

		// Token: 0x02000341 RID: 833
		// (Invoke) Token: 0x06001C0F RID: 7183
		public delegate IEnumerator InfoRoutine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, uLink.NetworkMessageInfo info);

		// Token: 0x02000342 RID: 834
		// (Invoke) Token: 0x06001C13 RID: 7187
		public delegate void StreamCall(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, BitStream stream);

		// Token: 0x02000343 RID: 835
		// (Invoke) Token: 0x06001C17 RID: 7191
		public delegate IEnumerator StreamRoutine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, BitStream stream);

		// Token: 0x02000344 RID: 836
		// (Invoke) Token: 0x06001C1B RID: 7195
		public delegate void StreamInfoCall(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, uLink.NetworkMessageInfo info, BitStream stream);

		// Token: 0x02000345 RID: 837
		// (Invoke) Token: 0x06001C1F RID: 7199
		public delegate IEnumerator StreamInfoRoutine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, uLink.NetworkMessageInfo info, BitStream stream);
	}

	// Token: 0x02000346 RID: 838
	public static class callf<P0, P1, P2, P3, P4, P5>
	{
		// Token: 0x06001C22 RID: 7202 RVA: 0x0006A2B0 File Offset: 0x000684B0
		static callf()
		{
			BitStreamCodec.Add<global::NGC.callf<P0, P1, P2, P3, P4, P5>.Block>(new BitStreamCodec.Deserializer(global::NGC.callf<P0, P1, P2, P3, P4, P5>.Deserializer), new BitStreamCodec.Serializer(global::NGC.callf<P0, P1, P2, P3, P4, P5>.Serializer));
		}

		// Token: 0x06001C23 RID: 7203 RVA: 0x0006A2D0 File Offset: 0x000684D0
		private static object Deserializer(BitStream stream, params object[] codecOptions)
		{
			global::NGC.callf<P0, P1, P2, P3, P4, P5>.Block block;
			block.p0 = stream.Read<P0>(codecOptions);
			block.p1 = stream.Read<P1>(codecOptions);
			block.p2 = stream.Read<P2>(codecOptions);
			block.p3 = stream.Read<P3>(codecOptions);
			block.p4 = stream.Read<P4>(codecOptions);
			block.p5 = stream.Read<P5>(codecOptions);
			return block;
		}

		// Token: 0x06001C24 RID: 7204 RVA: 0x0006A338 File Offset: 0x00068538
		private static void Serializer(BitStream stream, object value, params object[] codecOptions)
		{
			global::NGC.callf<P0, P1, P2, P3, P4, P5>.Block block = (global::NGC.callf<P0, P1, P2, P3, P4, P5>.Block)value;
			stream.Write<P0>(block.p0, codecOptions);
			stream.Write<P1>(block.p1, codecOptions);
			stream.Write<P2>(block.p2, codecOptions);
			stream.Write<P3>(block.p3, codecOptions);
			stream.Write<P4>(block.p4, codecOptions);
			stream.Write<P5>(block.p5, codecOptions);
		}

		// Token: 0x06001C25 RID: 7205 RVA: 0x0006A3A0 File Offset: 0x000685A0
		public static void InvokeCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5>.Call), instance, method, true);
			}
			((global::NGC.callf<P0, P1, P2, P3, P4, P5>.Call)d)(p, p2, p3, p4, p5, p6);
		}

		// Token: 0x06001C26 RID: 7206 RVA: 0x0006A42C File Offset: 0x0006862C
		public static IEnumerator InvokeRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5>.Routine), instance, method, true);
			}
			return ((global::NGC.callf<P0, P1, P2, P3, P4, P5>.Routine)d)(p, p2, p3, p4, p5, p6);
		}

		// Token: 0x06001C27 RID: 7207 RVA: 0x0006A4B8 File Offset: 0x000686B8
		public static void InvokeInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5>.InfoCall), instance, method, true);
			}
			((global::NGC.callf<P0, P1, P2, P3, P4, P5>.InfoCall)d)(p, p2, p3, p4, p5, p6, info);
		}

		// Token: 0x06001C28 RID: 7208 RVA: 0x0006A548 File Offset: 0x00068748
		public static IEnumerator InvokeInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5>.InfoRoutine), instance, method, true);
			}
			return ((global::NGC.callf<P0, P1, P2, P3, P4, P5>.InfoRoutine)d)(p, p2, p3, p4, p5, p6, info);
		}

		// Token: 0x06001C29 RID: 7209 RVA: 0x0006A5D8 File Offset: 0x000687D8
		public static void InvokeStreamCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5>.StreamCall), instance, method, true);
			}
			((global::NGC.callf<P0, P1, P2, P3, P4, P5>.StreamCall)d)(p, p2, p3, p4, p5, p6, stream);
		}

		// Token: 0x06001C2A RID: 7210 RVA: 0x0006A668 File Offset: 0x00068868
		public static IEnumerator InvokeStreamRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5>.StreamRoutine), instance, method, true);
			}
			return ((global::NGC.callf<P0, P1, P2, P3, P4, P5>.StreamRoutine)d)(p, p2, p3, p4, p5, p6, stream);
		}

		// Token: 0x06001C2B RID: 7211 RVA: 0x0006A6F8 File Offset: 0x000688F8
		public static void InvokeStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5>.StreamInfoCall), instance, method, true);
			}
			((global::NGC.callf<P0, P1, P2, P3, P4, P5>.StreamInfoCall)d)(p, p2, p3, p4, p5, p6, info, stream);
		}

		// Token: 0x06001C2C RID: 7212 RVA: 0x0006A788 File Offset: 0x00068988
		public static IEnumerator InvokeStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5>.StreamInfoRoutine), instance, method, true);
			}
			return ((global::NGC.callf<P0, P1, P2, P3, P4, P5>.StreamInfoRoutine)d)(p, p2, p3, p4, p5, p6, info, stream);
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06001C2D RID: 7213 RVA: 0x0006A818 File Offset: 0x00068A18
		public static global::NGC.IExecuter Exec
		{
			get
			{
				return global::NGC.callf<P0, P1, P2, P3, P4, P5>.Executer.Singleton;
			}
		}

		// Token: 0x02000347 RID: 839
		public struct Block
		{
			// Token: 0x04000ED2 RID: 3794
			public P0 p0;

			// Token: 0x04000ED3 RID: 3795
			public P1 p1;

			// Token: 0x04000ED4 RID: 3796
			public P2 p2;

			// Token: 0x04000ED5 RID: 3797
			public P3 p3;

			// Token: 0x04000ED6 RID: 3798
			public P4 p4;

			// Token: 0x04000ED7 RID: 3799
			public P5 p5;
		}

		// Token: 0x02000348 RID: 840
		private sealed class Executer : global::NGC.IExecuter
		{
			// Token: 0x06001C30 RID: 7216 RVA: 0x0006A834 File Offset: 0x00068A34
			public void ExecuteCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				global::NGC.callf<P0, P1, P2, P3, P4, P5>.InvokeCall(stream, ref d, method, instance);
			}

			// Token: 0x06001C31 RID: 7217 RVA: 0x0006A840 File Offset: 0x00068A40
			public IEnumerator ExecuteRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return global::NGC.callf<P0, P1, P2, P3, P4, P5>.InvokeRoutine(stream, ref d, method, instance);
			}

			// Token: 0x06001C32 RID: 7218 RVA: 0x0006A84C File Offset: 0x00068A4C
			public void ExecuteInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				global::NGC.callf<P0, P1, P2, P3, P4, P5>.InvokeInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x06001C33 RID: 7219 RVA: 0x0006A85C File Offset: 0x00068A5C
			public IEnumerator ExecuteInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				return global::NGC.callf<P0, P1, P2, P3, P4, P5>.InvokeInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x06001C34 RID: 7220 RVA: 0x0006A86C File Offset: 0x00068A6C
			public void ExecuteStreamCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				global::NGC.callf<P0, P1, P2, P3, P4, P5>.InvokeStreamCall(stream, ref d, method, instance);
			}

			// Token: 0x06001C35 RID: 7221 RVA: 0x0006A878 File Offset: 0x00068A78
			public IEnumerator ExecuteStreamRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return global::NGC.callf<P0, P1, P2, P3, P4, P5>.InvokeStreamRoutine(stream, ref d, method, instance);
			}

			// Token: 0x06001C36 RID: 7222 RVA: 0x0006A884 File Offset: 0x00068A84
			public void ExecuteStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				global::NGC.callf<P0, P1, P2, P3, P4, P5>.InvokeStreamInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x06001C37 RID: 7223 RVA: 0x0006A894 File Offset: 0x00068A94
			public IEnumerator ExecuteStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				return global::NGC.callf<P0, P1, P2, P3, P4, P5>.InvokeStreamInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x04000ED8 RID: 3800
			public static readonly global::NGC.IExecuter Singleton = new global::NGC.callf<P0, P1, P2, P3, P4, P5>.Executer();
		}

		// Token: 0x02000349 RID: 841
		// (Invoke) Token: 0x06001C39 RID: 7225
		public delegate void Call(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5);

		// Token: 0x0200034A RID: 842
		// (Invoke) Token: 0x06001C3D RID: 7229
		public delegate IEnumerator Routine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5);

		// Token: 0x0200034B RID: 843
		// (Invoke) Token: 0x06001C41 RID: 7233
		public delegate void InfoCall(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, uLink.NetworkMessageInfo info);

		// Token: 0x0200034C RID: 844
		// (Invoke) Token: 0x06001C45 RID: 7237
		public delegate IEnumerator InfoRoutine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, uLink.NetworkMessageInfo info);

		// Token: 0x0200034D RID: 845
		// (Invoke) Token: 0x06001C49 RID: 7241
		public delegate void StreamCall(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, BitStream stream);

		// Token: 0x0200034E RID: 846
		// (Invoke) Token: 0x06001C4D RID: 7245
		public delegate IEnumerator StreamRoutine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, BitStream stream);

		// Token: 0x0200034F RID: 847
		// (Invoke) Token: 0x06001C51 RID: 7249
		public delegate void StreamInfoCall(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, uLink.NetworkMessageInfo info, BitStream stream);

		// Token: 0x02000350 RID: 848
		// (Invoke) Token: 0x06001C55 RID: 7253
		public delegate IEnumerator StreamInfoRoutine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, uLink.NetworkMessageInfo info, BitStream stream);
	}

	// Token: 0x02000351 RID: 849
	public static class callf<P0, P1, P2, P3, P4, P5, P6>
	{
		// Token: 0x06001C58 RID: 7256 RVA: 0x0006A8A4 File Offset: 0x00068AA4
		static callf()
		{
			BitStreamCodec.Add<global::NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Block>(new BitStreamCodec.Deserializer(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Deserializer), new BitStreamCodec.Serializer(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Serializer));
		}

		// Token: 0x06001C59 RID: 7257 RVA: 0x0006A8C4 File Offset: 0x00068AC4
		private static object Deserializer(BitStream stream, params object[] codecOptions)
		{
			global::NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Block block;
			block.p0 = stream.Read<P0>(codecOptions);
			block.p1 = stream.Read<P1>(codecOptions);
			block.p2 = stream.Read<P2>(codecOptions);
			block.p3 = stream.Read<P3>(codecOptions);
			block.p4 = stream.Read<P4>(codecOptions);
			block.p5 = stream.Read<P5>(codecOptions);
			block.p6 = stream.Read<P6>(codecOptions);
			return block;
		}

		// Token: 0x06001C5A RID: 7258 RVA: 0x0006A93C File Offset: 0x00068B3C
		private static void Serializer(BitStream stream, object value, params object[] codecOptions)
		{
			global::NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Block block = (global::NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Block)value;
			stream.Write<P0>(block.p0, codecOptions);
			stream.Write<P1>(block.p1, codecOptions);
			stream.Write<P2>(block.p2, codecOptions);
			stream.Write<P3>(block.p3, codecOptions);
			stream.Write<P4>(block.p4, codecOptions);
			stream.Write<P5>(block.p5, codecOptions);
			stream.Write<P6>(block.p6, codecOptions);
		}

		// Token: 0x06001C5B RID: 7259 RVA: 0x0006A9B4 File Offset: 0x00068BB4
		public static void InvokeCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			P6 p7 = stream.Read<P6>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Call), instance, method, true);
			}
			((global::NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Call)d)(p, p2, p3, p4, p5, p6, p7);
		}

		// Token: 0x06001C5C RID: 7260 RVA: 0x0006AA50 File Offset: 0x00068C50
		public static IEnumerator InvokeRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			P6 p7 = stream.Read<P6>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Routine), instance, method, true);
			}
			return ((global::NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Routine)d)(p, p2, p3, p4, p5, p6, p7);
		}

		// Token: 0x06001C5D RID: 7261 RVA: 0x0006AAEC File Offset: 0x00068CEC
		public static void InvokeInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			P6 p7 = stream.Read<P6>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6>.InfoCall), instance, method, true);
			}
			((global::NGC.callf<P0, P1, P2, P3, P4, P5, P6>.InfoCall)d)(p, p2, p3, p4, p5, p6, p7, info);
		}

		// Token: 0x06001C5E RID: 7262 RVA: 0x0006AB8C File Offset: 0x00068D8C
		public static IEnumerator InvokeInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			P6 p7 = stream.Read<P6>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6>.InfoRoutine), instance, method, true);
			}
			return ((global::NGC.callf<P0, P1, P2, P3, P4, P5, P6>.InfoRoutine)d)(p, p2, p3, p4, p5, p6, p7, info);
		}

		// Token: 0x06001C5F RID: 7263 RVA: 0x0006AC2C File Offset: 0x00068E2C
		public static void InvokeStreamCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			P6 p7 = stream.Read<P6>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6>.StreamCall), instance, method, true);
			}
			((global::NGC.callf<P0, P1, P2, P3, P4, P5, P6>.StreamCall)d)(p, p2, p3, p4, p5, p6, p7, stream);
		}

		// Token: 0x06001C60 RID: 7264 RVA: 0x0006ACCC File Offset: 0x00068ECC
		public static IEnumerator InvokeStreamRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			P6 p7 = stream.Read<P6>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6>.StreamRoutine), instance, method, true);
			}
			return ((global::NGC.callf<P0, P1, P2, P3, P4, P5, P6>.StreamRoutine)d)(p, p2, p3, p4, p5, p6, p7, stream);
		}

		// Token: 0x06001C61 RID: 7265 RVA: 0x0006AD6C File Offset: 0x00068F6C
		public static void InvokeStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			P6 p7 = stream.Read<P6>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6>.StreamInfoCall), instance, method, true);
			}
			((global::NGC.callf<P0, P1, P2, P3, P4, P5, P6>.StreamInfoCall)d)(p, p2, p3, p4, p5, p6, p7, info, stream);
		}

		// Token: 0x06001C62 RID: 7266 RVA: 0x0006AE0C File Offset: 0x0006900C
		public static IEnumerator InvokeStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			P6 p7 = stream.Read<P6>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6>.StreamInfoRoutine), instance, method, true);
			}
			return ((global::NGC.callf<P0, P1, P2, P3, P4, P5, P6>.StreamInfoRoutine)d)(p, p2, p3, p4, p5, p6, p7, info, stream);
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06001C63 RID: 7267 RVA: 0x0006AEAC File Offset: 0x000690AC
		public static global::NGC.IExecuter Exec
		{
			get
			{
				return global::NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Executer.Singleton;
			}
		}

		// Token: 0x02000352 RID: 850
		public struct Block
		{
			// Token: 0x04000ED9 RID: 3801
			public P0 p0;

			// Token: 0x04000EDA RID: 3802
			public P1 p1;

			// Token: 0x04000EDB RID: 3803
			public P2 p2;

			// Token: 0x04000EDC RID: 3804
			public P3 p3;

			// Token: 0x04000EDD RID: 3805
			public P4 p4;

			// Token: 0x04000EDE RID: 3806
			public P5 p5;

			// Token: 0x04000EDF RID: 3807
			public P6 p6;
		}

		// Token: 0x02000353 RID: 851
		private sealed class Executer : global::NGC.IExecuter
		{
			// Token: 0x06001C66 RID: 7270 RVA: 0x0006AEC8 File Offset: 0x000690C8
			public void ExecuteCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				global::NGC.callf<P0, P1, P2, P3, P4, P5, P6>.InvokeCall(stream, ref d, method, instance);
			}

			// Token: 0x06001C67 RID: 7271 RVA: 0x0006AED4 File Offset: 0x000690D4
			public IEnumerator ExecuteRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return global::NGC.callf<P0, P1, P2, P3, P4, P5, P6>.InvokeRoutine(stream, ref d, method, instance);
			}

			// Token: 0x06001C68 RID: 7272 RVA: 0x0006AEE0 File Offset: 0x000690E0
			public void ExecuteInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				global::NGC.callf<P0, P1, P2, P3, P4, P5, P6>.InvokeInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x06001C69 RID: 7273 RVA: 0x0006AEF0 File Offset: 0x000690F0
			public IEnumerator ExecuteInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				return global::NGC.callf<P0, P1, P2, P3, P4, P5, P6>.InvokeInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x06001C6A RID: 7274 RVA: 0x0006AF00 File Offset: 0x00069100
			public void ExecuteStreamCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				global::NGC.callf<P0, P1, P2, P3, P4, P5, P6>.InvokeStreamCall(stream, ref d, method, instance);
			}

			// Token: 0x06001C6B RID: 7275 RVA: 0x0006AF0C File Offset: 0x0006910C
			public IEnumerator ExecuteStreamRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return global::NGC.callf<P0, P1, P2, P3, P4, P5, P6>.InvokeStreamRoutine(stream, ref d, method, instance);
			}

			// Token: 0x06001C6C RID: 7276 RVA: 0x0006AF18 File Offset: 0x00069118
			public void ExecuteStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				global::NGC.callf<P0, P1, P2, P3, P4, P5, P6>.InvokeStreamInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x06001C6D RID: 7277 RVA: 0x0006AF28 File Offset: 0x00069128
			public IEnumerator ExecuteStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				return global::NGC.callf<P0, P1, P2, P3, P4, P5, P6>.InvokeStreamInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x04000EE0 RID: 3808
			public static readonly global::NGC.IExecuter Singleton = new global::NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Executer();
		}

		// Token: 0x02000354 RID: 852
		// (Invoke) Token: 0x06001C6F RID: 7279
		public delegate void Call(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6);

		// Token: 0x02000355 RID: 853
		// (Invoke) Token: 0x06001C73 RID: 7283
		public delegate IEnumerator Routine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6);

		// Token: 0x02000356 RID: 854
		// (Invoke) Token: 0x06001C77 RID: 7287
		public delegate void InfoCall(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, uLink.NetworkMessageInfo info);

		// Token: 0x02000357 RID: 855
		// (Invoke) Token: 0x06001C7B RID: 7291
		public delegate IEnumerator InfoRoutine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, uLink.NetworkMessageInfo info);

		// Token: 0x02000358 RID: 856
		// (Invoke) Token: 0x06001C7F RID: 7295
		public delegate void StreamCall(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, BitStream stream);

		// Token: 0x02000359 RID: 857
		// (Invoke) Token: 0x06001C83 RID: 7299
		public delegate IEnumerator StreamRoutine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, BitStream stream);

		// Token: 0x0200035A RID: 858
		// (Invoke) Token: 0x06001C87 RID: 7303
		public delegate void StreamInfoCall(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, uLink.NetworkMessageInfo info, BitStream stream);

		// Token: 0x0200035B RID: 859
		// (Invoke) Token: 0x06001C8B RID: 7307
		public delegate IEnumerator StreamInfoRoutine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, uLink.NetworkMessageInfo info, BitStream stream);
	}

	// Token: 0x0200035C RID: 860
	public static class callf<P0, P1, P2, P3, P4, P5, P6, P7>
	{
		// Token: 0x06001C8E RID: 7310 RVA: 0x0006AF38 File Offset: 0x00069138
		static callf()
		{
			BitStreamCodec.Add<global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Block>(new BitStreamCodec.Deserializer(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Deserializer), new BitStreamCodec.Serializer(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Serializer));
		}

		// Token: 0x06001C8F RID: 7311 RVA: 0x0006AF58 File Offset: 0x00069158
		private static object Deserializer(BitStream stream, params object[] codecOptions)
		{
			global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Block block;
			block.p0 = stream.Read<P0>(codecOptions);
			block.p1 = stream.Read<P1>(codecOptions);
			block.p2 = stream.Read<P2>(codecOptions);
			block.p3 = stream.Read<P3>(codecOptions);
			block.p4 = stream.Read<P4>(codecOptions);
			block.p5 = stream.Read<P5>(codecOptions);
			block.p6 = stream.Read<P6>(codecOptions);
			block.p7 = stream.Read<P7>(codecOptions);
			return block;
		}

		// Token: 0x06001C90 RID: 7312 RVA: 0x0006AFDC File Offset: 0x000691DC
		private static void Serializer(BitStream stream, object value, params object[] codecOptions)
		{
			global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Block block = (global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Block)value;
			stream.Write<P0>(block.p0, codecOptions);
			stream.Write<P1>(block.p1, codecOptions);
			stream.Write<P2>(block.p2, codecOptions);
			stream.Write<P3>(block.p3, codecOptions);
			stream.Write<P4>(block.p4, codecOptions);
			stream.Write<P5>(block.p5, codecOptions);
			stream.Write<P6>(block.p6, codecOptions);
			stream.Write<P7>(block.p7, codecOptions);
		}

		// Token: 0x06001C91 RID: 7313 RVA: 0x0006B060 File Offset: 0x00069260
		public static void InvokeCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			P6 p7 = stream.Read<P6>(new object[0]);
			P7 p8 = stream.Read<P7>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Call), instance, method, true);
			}
			((global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Call)d)(p, p2, p3, p4, p5, p6, p7, p8);
		}

		// Token: 0x06001C92 RID: 7314 RVA: 0x0006B10C File Offset: 0x0006930C
		public static IEnumerator InvokeRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			P6 p7 = stream.Read<P6>(new object[0]);
			P7 p8 = stream.Read<P7>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Routine), instance, method, true);
			}
			return ((global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Routine)d)(p, p2, p3, p4, p5, p6, p7, p8);
		}

		// Token: 0x06001C93 RID: 7315 RVA: 0x0006B1B8 File Offset: 0x000693B8
		public static void InvokeInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			P6 p7 = stream.Read<P6>(new object[0]);
			P7 p8 = stream.Read<P7>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.InfoCall), instance, method, true);
			}
			((global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.InfoCall)d)(p, p2, p3, p4, p5, p6, p7, p8, info);
		}

		// Token: 0x06001C94 RID: 7316 RVA: 0x0006B268 File Offset: 0x00069468
		public static IEnumerator InvokeInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			P6 p7 = stream.Read<P6>(new object[0]);
			P7 p8 = stream.Read<P7>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.InfoRoutine), instance, method, true);
			}
			return ((global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.InfoRoutine)d)(p, p2, p3, p4, p5, p6, p7, p8, info);
		}

		// Token: 0x06001C95 RID: 7317 RVA: 0x0006B318 File Offset: 0x00069518
		public static void InvokeStreamCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			P6 p7 = stream.Read<P6>(new object[0]);
			P7 p8 = stream.Read<P7>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.StreamCall), instance, method, true);
			}
			((global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.StreamCall)d)(p, p2, p3, p4, p5, p6, p7, p8, stream);
		}

		// Token: 0x06001C96 RID: 7318 RVA: 0x0006B3C8 File Offset: 0x000695C8
		public static IEnumerator InvokeStreamRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			P6 p7 = stream.Read<P6>(new object[0]);
			P7 p8 = stream.Read<P7>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.StreamRoutine), instance, method, true);
			}
			return ((global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.StreamRoutine)d)(p, p2, p3, p4, p5, p6, p7, p8, stream);
		}

		// Token: 0x06001C97 RID: 7319 RVA: 0x0006B478 File Offset: 0x00069678
		public static void InvokeStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			P6 p7 = stream.Read<P6>(new object[0]);
			P7 p8 = stream.Read<P7>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.StreamInfoCall), instance, method, true);
			}
			((global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.StreamInfoCall)d)(p, p2, p3, p4, p5, p6, p7, p8, info, stream);
		}

		// Token: 0x06001C98 RID: 7320 RVA: 0x0006B528 File Offset: 0x00069728
		public static IEnumerator InvokeStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			P6 p7 = stream.Read<P6>(new object[0]);
			P7 p8 = stream.Read<P7>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.StreamInfoRoutine), instance, method, true);
			}
			return ((global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.StreamInfoRoutine)d)(p, p2, p3, p4, p5, p6, p7, p8, info, stream);
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06001C99 RID: 7321 RVA: 0x0006B5D8 File Offset: 0x000697D8
		public static global::NGC.IExecuter Exec
		{
			get
			{
				return global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Executer.Singleton;
			}
		}

		// Token: 0x0200035D RID: 861
		public struct Block
		{
			// Token: 0x04000EE1 RID: 3809
			public P0 p0;

			// Token: 0x04000EE2 RID: 3810
			public P1 p1;

			// Token: 0x04000EE3 RID: 3811
			public P2 p2;

			// Token: 0x04000EE4 RID: 3812
			public P3 p3;

			// Token: 0x04000EE5 RID: 3813
			public P4 p4;

			// Token: 0x04000EE6 RID: 3814
			public P5 p5;

			// Token: 0x04000EE7 RID: 3815
			public P6 p6;

			// Token: 0x04000EE8 RID: 3816
			public P7 p7;
		}

		// Token: 0x0200035E RID: 862
		private sealed class Executer : global::NGC.IExecuter
		{
			// Token: 0x06001C9C RID: 7324 RVA: 0x0006B5F4 File Offset: 0x000697F4
			public void ExecuteCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.InvokeCall(stream, ref d, method, instance);
			}

			// Token: 0x06001C9D RID: 7325 RVA: 0x0006B600 File Offset: 0x00069800
			public IEnumerator ExecuteRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.InvokeRoutine(stream, ref d, method, instance);
			}

			// Token: 0x06001C9E RID: 7326 RVA: 0x0006B60C File Offset: 0x0006980C
			public void ExecuteInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.InvokeInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x06001C9F RID: 7327 RVA: 0x0006B61C File Offset: 0x0006981C
			public IEnumerator ExecuteInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				return global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.InvokeInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x06001CA0 RID: 7328 RVA: 0x0006B62C File Offset: 0x0006982C
			public void ExecuteStreamCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.InvokeStreamCall(stream, ref d, method, instance);
			}

			// Token: 0x06001CA1 RID: 7329 RVA: 0x0006B638 File Offset: 0x00069838
			public IEnumerator ExecuteStreamRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.InvokeStreamRoutine(stream, ref d, method, instance);
			}

			// Token: 0x06001CA2 RID: 7330 RVA: 0x0006B644 File Offset: 0x00069844
			public void ExecuteStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.InvokeStreamInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x06001CA3 RID: 7331 RVA: 0x0006B654 File Offset: 0x00069854
			public IEnumerator ExecuteStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				return global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.InvokeStreamInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x04000EE9 RID: 3817
			public static readonly global::NGC.IExecuter Singleton = new global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Executer();
		}

		// Token: 0x0200035F RID: 863
		// (Invoke) Token: 0x06001CA5 RID: 7333
		public delegate void Call(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7);

		// Token: 0x02000360 RID: 864
		// (Invoke) Token: 0x06001CA9 RID: 7337
		public delegate IEnumerator Routine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7);

		// Token: 0x02000361 RID: 865
		// (Invoke) Token: 0x06001CAD RID: 7341
		public delegate void InfoCall(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, uLink.NetworkMessageInfo info);

		// Token: 0x02000362 RID: 866
		// (Invoke) Token: 0x06001CB1 RID: 7345
		public delegate IEnumerator InfoRoutine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, uLink.NetworkMessageInfo info);

		// Token: 0x02000363 RID: 867
		// (Invoke) Token: 0x06001CB5 RID: 7349
		public delegate void StreamCall(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, BitStream stream);

		// Token: 0x02000364 RID: 868
		// (Invoke) Token: 0x06001CB9 RID: 7353
		public delegate IEnumerator StreamRoutine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, BitStream stream);

		// Token: 0x02000365 RID: 869
		// (Invoke) Token: 0x06001CBD RID: 7357
		public delegate void StreamInfoCall(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, uLink.NetworkMessageInfo info, BitStream stream);

		// Token: 0x02000366 RID: 870
		// (Invoke) Token: 0x06001CC1 RID: 7361
		public delegate IEnumerator StreamInfoRoutine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, uLink.NetworkMessageInfo info, BitStream stream);
	}

	// Token: 0x02000367 RID: 871
	public static class callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>
	{
		// Token: 0x06001CC4 RID: 7364 RVA: 0x0006B664 File Offset: 0x00069864
		static callf()
		{
			BitStreamCodec.Add<global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Block>(new BitStreamCodec.Deserializer(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Deserializer), new BitStreamCodec.Serializer(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Serializer));
		}

		// Token: 0x06001CC5 RID: 7365 RVA: 0x0006B684 File Offset: 0x00069884
		private static object Deserializer(BitStream stream, params object[] codecOptions)
		{
			global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Block block;
			block.p0 = stream.Read<P0>(codecOptions);
			block.p1 = stream.Read<P1>(codecOptions);
			block.p2 = stream.Read<P2>(codecOptions);
			block.p3 = stream.Read<P3>(codecOptions);
			block.p4 = stream.Read<P4>(codecOptions);
			block.p5 = stream.Read<P5>(codecOptions);
			block.p6 = stream.Read<P6>(codecOptions);
			block.p7 = stream.Read<P7>(codecOptions);
			block.p8 = stream.Read<P8>(codecOptions);
			return block;
		}

		// Token: 0x06001CC6 RID: 7366 RVA: 0x0006B718 File Offset: 0x00069918
		private static void Serializer(BitStream stream, object value, params object[] codecOptions)
		{
			global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Block block = (global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Block)value;
			stream.Write<P0>(block.p0, codecOptions);
			stream.Write<P1>(block.p1, codecOptions);
			stream.Write<P2>(block.p2, codecOptions);
			stream.Write<P3>(block.p3, codecOptions);
			stream.Write<P4>(block.p4, codecOptions);
			stream.Write<P5>(block.p5, codecOptions);
			stream.Write<P6>(block.p6, codecOptions);
			stream.Write<P7>(block.p7, codecOptions);
			stream.Write<P8>(block.p8, codecOptions);
		}

		// Token: 0x06001CC7 RID: 7367 RVA: 0x0006B7AC File Offset: 0x000699AC
		public static void InvokeCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			P6 p7 = stream.Read<P6>(new object[0]);
			P7 p8 = stream.Read<P7>(new object[0]);
			P8 p9 = stream.Read<P8>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Call), instance, method, true);
			}
			((global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Call)d)(p, p2, p3, p4, p5, p6, p7, p8, p9);
		}

		// Token: 0x06001CC8 RID: 7368 RVA: 0x0006B868 File Offset: 0x00069A68
		public static IEnumerator InvokeRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			P6 p7 = stream.Read<P6>(new object[0]);
			P7 p8 = stream.Read<P7>(new object[0]);
			P8 p9 = stream.Read<P8>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Routine), instance, method, true);
			}
			return ((global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Routine)d)(p, p2, p3, p4, p5, p6, p7, p8, p9);
		}

		// Token: 0x06001CC9 RID: 7369 RVA: 0x0006B924 File Offset: 0x00069B24
		public static void InvokeInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			P6 p7 = stream.Read<P6>(new object[0]);
			P7 p8 = stream.Read<P7>(new object[0]);
			P8 p9 = stream.Read<P8>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.InfoCall), instance, method, true);
			}
			((global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.InfoCall)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, info);
		}

		// Token: 0x06001CCA RID: 7370 RVA: 0x0006B9E4 File Offset: 0x00069BE4
		public static IEnumerator InvokeInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			P6 p7 = stream.Read<P6>(new object[0]);
			P7 p8 = stream.Read<P7>(new object[0]);
			P8 p9 = stream.Read<P8>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.InfoRoutine), instance, method, true);
			}
			return ((global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.InfoRoutine)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, info);
		}

		// Token: 0x06001CCB RID: 7371 RVA: 0x0006BAA4 File Offset: 0x00069CA4
		public static void InvokeStreamCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			P6 p7 = stream.Read<P6>(new object[0]);
			P7 p8 = stream.Read<P7>(new object[0]);
			P8 p9 = stream.Read<P8>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.StreamCall), instance, method, true);
			}
			((global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.StreamCall)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, stream);
		}

		// Token: 0x06001CCC RID: 7372 RVA: 0x0006BB64 File Offset: 0x00069D64
		public static IEnumerator InvokeStreamRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			P6 p7 = stream.Read<P6>(new object[0]);
			P7 p8 = stream.Read<P7>(new object[0]);
			P8 p9 = stream.Read<P8>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.StreamRoutine), instance, method, true);
			}
			return ((global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.StreamRoutine)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, stream);
		}

		// Token: 0x06001CCD RID: 7373 RVA: 0x0006BC24 File Offset: 0x00069E24
		public static void InvokeStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			P6 p7 = stream.Read<P6>(new object[0]);
			P7 p8 = stream.Read<P7>(new object[0]);
			P8 p9 = stream.Read<P8>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.StreamInfoCall), instance, method, true);
			}
			((global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.StreamInfoCall)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, info, stream);
		}

		// Token: 0x06001CCE RID: 7374 RVA: 0x0006BCE4 File Offset: 0x00069EE4
		public static IEnumerator InvokeStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			P6 p7 = stream.Read<P6>(new object[0]);
			P7 p8 = stream.Read<P7>(new object[0]);
			P8 p9 = stream.Read<P8>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.StreamInfoRoutine), instance, method, true);
			}
			return ((global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.StreamInfoRoutine)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, info, stream);
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06001CCF RID: 7375 RVA: 0x0006BDA4 File Offset: 0x00069FA4
		public static global::NGC.IExecuter Exec
		{
			get
			{
				return global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Executer.Singleton;
			}
		}

		// Token: 0x02000368 RID: 872
		public struct Block
		{
			// Token: 0x04000EEA RID: 3818
			public P0 p0;

			// Token: 0x04000EEB RID: 3819
			public P1 p1;

			// Token: 0x04000EEC RID: 3820
			public P2 p2;

			// Token: 0x04000EED RID: 3821
			public P3 p3;

			// Token: 0x04000EEE RID: 3822
			public P4 p4;

			// Token: 0x04000EEF RID: 3823
			public P5 p5;

			// Token: 0x04000EF0 RID: 3824
			public P6 p6;

			// Token: 0x04000EF1 RID: 3825
			public P7 p7;

			// Token: 0x04000EF2 RID: 3826
			public P8 p8;
		}

		// Token: 0x02000369 RID: 873
		private sealed class Executer : global::NGC.IExecuter
		{
			// Token: 0x06001CD2 RID: 7378 RVA: 0x0006BDC0 File Offset: 0x00069FC0
			public void ExecuteCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.InvokeCall(stream, ref d, method, instance);
			}

			// Token: 0x06001CD3 RID: 7379 RVA: 0x0006BDCC File Offset: 0x00069FCC
			public IEnumerator ExecuteRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.InvokeRoutine(stream, ref d, method, instance);
			}

			// Token: 0x06001CD4 RID: 7380 RVA: 0x0006BDD8 File Offset: 0x00069FD8
			public void ExecuteInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.InvokeInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x06001CD5 RID: 7381 RVA: 0x0006BDE8 File Offset: 0x00069FE8
			public IEnumerator ExecuteInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				return global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.InvokeInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x06001CD6 RID: 7382 RVA: 0x0006BDF8 File Offset: 0x00069FF8
			public void ExecuteStreamCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.InvokeStreamCall(stream, ref d, method, instance);
			}

			// Token: 0x06001CD7 RID: 7383 RVA: 0x0006BE04 File Offset: 0x0006A004
			public IEnumerator ExecuteStreamRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.InvokeStreamRoutine(stream, ref d, method, instance);
			}

			// Token: 0x06001CD8 RID: 7384 RVA: 0x0006BE10 File Offset: 0x0006A010
			public void ExecuteStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.InvokeStreamInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x06001CD9 RID: 7385 RVA: 0x0006BE20 File Offset: 0x0006A020
			public IEnumerator ExecuteStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				return global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.InvokeStreamInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x04000EF3 RID: 3827
			public static readonly global::NGC.IExecuter Singleton = new global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Executer();
		}

		// Token: 0x0200036A RID: 874
		// (Invoke) Token: 0x06001CDB RID: 7387
		public delegate void Call(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8);

		// Token: 0x0200036B RID: 875
		// (Invoke) Token: 0x06001CDF RID: 7391
		public delegate IEnumerator Routine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8);

		// Token: 0x0200036C RID: 876
		// (Invoke) Token: 0x06001CE3 RID: 7395
		public delegate void InfoCall(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, uLink.NetworkMessageInfo info);

		// Token: 0x0200036D RID: 877
		// (Invoke) Token: 0x06001CE7 RID: 7399
		public delegate IEnumerator InfoRoutine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, uLink.NetworkMessageInfo info);

		// Token: 0x0200036E RID: 878
		// (Invoke) Token: 0x06001CEB RID: 7403
		public delegate void StreamCall(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, BitStream stream);

		// Token: 0x0200036F RID: 879
		// (Invoke) Token: 0x06001CEF RID: 7407
		public delegate IEnumerator StreamRoutine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, BitStream stream);

		// Token: 0x02000370 RID: 880
		// (Invoke) Token: 0x06001CF3 RID: 7411
		public delegate void StreamInfoCall(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, uLink.NetworkMessageInfo info, BitStream stream);

		// Token: 0x02000371 RID: 881
		// (Invoke) Token: 0x06001CF7 RID: 7415
		public delegate IEnumerator StreamInfoRoutine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, uLink.NetworkMessageInfo info, BitStream stream);
	}

	// Token: 0x02000372 RID: 882
	public static class callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>
	{
		// Token: 0x06001CFA RID: 7418 RVA: 0x0006BE30 File Offset: 0x0006A030
		static callf()
		{
			BitStreamCodec.Add<global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Block>(new BitStreamCodec.Deserializer(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Deserializer), new BitStreamCodec.Serializer(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Serializer));
		}

		// Token: 0x06001CFB RID: 7419 RVA: 0x0006BE50 File Offset: 0x0006A050
		private static object Deserializer(BitStream stream, params object[] codecOptions)
		{
			global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Block block;
			block.p0 = stream.Read<P0>(codecOptions);
			block.p1 = stream.Read<P1>(codecOptions);
			block.p2 = stream.Read<P2>(codecOptions);
			block.p3 = stream.Read<P3>(codecOptions);
			block.p4 = stream.Read<P4>(codecOptions);
			block.p5 = stream.Read<P5>(codecOptions);
			block.p6 = stream.Read<P6>(codecOptions);
			block.p7 = stream.Read<P7>(codecOptions);
			block.p8 = stream.Read<P8>(codecOptions);
			block.p9 = stream.Read<P9>(codecOptions);
			return block;
		}

		// Token: 0x06001CFC RID: 7420 RVA: 0x0006BEF0 File Offset: 0x0006A0F0
		private static void Serializer(BitStream stream, object value, params object[] codecOptions)
		{
			global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Block block = (global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Block)value;
			stream.Write<P0>(block.p0, codecOptions);
			stream.Write<P1>(block.p1, codecOptions);
			stream.Write<P2>(block.p2, codecOptions);
			stream.Write<P3>(block.p3, codecOptions);
			stream.Write<P4>(block.p4, codecOptions);
			stream.Write<P5>(block.p5, codecOptions);
			stream.Write<P6>(block.p6, codecOptions);
			stream.Write<P7>(block.p7, codecOptions);
			stream.Write<P8>(block.p8, codecOptions);
			stream.Write<P9>(block.p9, codecOptions);
		}

		// Token: 0x06001CFD RID: 7421 RVA: 0x0006BF90 File Offset: 0x0006A190
		public static void InvokeCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			P6 p7 = stream.Read<P6>(new object[0]);
			P7 p8 = stream.Read<P7>(new object[0]);
			P8 p9 = stream.Read<P8>(new object[0]);
			P9 p10 = stream.Read<P9>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Call), instance, method, true);
			}
			((global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Call)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, p10);
		}

		// Token: 0x06001CFE RID: 7422 RVA: 0x0006C05C File Offset: 0x0006A25C
		public static IEnumerator InvokeRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			P6 p7 = stream.Read<P6>(new object[0]);
			P7 p8 = stream.Read<P7>(new object[0]);
			P8 p9 = stream.Read<P8>(new object[0]);
			P9 p10 = stream.Read<P9>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Routine), instance, method, true);
			}
			return ((global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Routine)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, p10);
		}

		// Token: 0x06001CFF RID: 7423 RVA: 0x0006C128 File Offset: 0x0006A328
		public static void InvokeInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			P6 p7 = stream.Read<P6>(new object[0]);
			P7 p8 = stream.Read<P7>(new object[0]);
			P8 p9 = stream.Read<P8>(new object[0]);
			P9 p10 = stream.Read<P9>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.InfoCall), instance, method, true);
			}
			((global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.InfoCall)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, p10, info);
		}

		// Token: 0x06001D00 RID: 7424 RVA: 0x0006C1F8 File Offset: 0x0006A3F8
		public static IEnumerator InvokeInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			P6 p7 = stream.Read<P6>(new object[0]);
			P7 p8 = stream.Read<P7>(new object[0]);
			P8 p9 = stream.Read<P8>(new object[0]);
			P9 p10 = stream.Read<P9>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.InfoRoutine), instance, method, true);
			}
			return ((global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.InfoRoutine)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, p10, info);
		}

		// Token: 0x06001D01 RID: 7425 RVA: 0x0006C2C8 File Offset: 0x0006A4C8
		public static void InvokeStreamCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			P6 p7 = stream.Read<P6>(new object[0]);
			P7 p8 = stream.Read<P7>(new object[0]);
			P8 p9 = stream.Read<P8>(new object[0]);
			P9 p10 = stream.Read<P9>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.StreamCall), instance, method, true);
			}
			((global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.StreamCall)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, p10, stream);
		}

		// Token: 0x06001D02 RID: 7426 RVA: 0x0006C398 File Offset: 0x0006A598
		public static IEnumerator InvokeStreamRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			P6 p7 = stream.Read<P6>(new object[0]);
			P7 p8 = stream.Read<P7>(new object[0]);
			P8 p9 = stream.Read<P8>(new object[0]);
			P9 p10 = stream.Read<P9>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.StreamRoutine), instance, method, true);
			}
			return ((global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.StreamRoutine)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, p10, stream);
		}

		// Token: 0x06001D03 RID: 7427 RVA: 0x0006C468 File Offset: 0x0006A668
		public static void InvokeStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			P6 p7 = stream.Read<P6>(new object[0]);
			P7 p8 = stream.Read<P7>(new object[0]);
			P8 p9 = stream.Read<P8>(new object[0]);
			P9 p10 = stream.Read<P9>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.StreamInfoCall), instance, method, true);
			}
			((global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.StreamInfoCall)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, p10, info, stream);
		}

		// Token: 0x06001D04 RID: 7428 RVA: 0x0006C538 File Offset: 0x0006A738
		public static IEnumerator InvokeStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			P6 p7 = stream.Read<P6>(new object[0]);
			P7 p8 = stream.Read<P7>(new object[0]);
			P8 p9 = stream.Read<P8>(new object[0]);
			P9 p10 = stream.Read<P9>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.StreamInfoRoutine), instance, method, true);
			}
			return ((global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.StreamInfoRoutine)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, p10, info, stream);
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06001D05 RID: 7429 RVA: 0x0006C608 File Offset: 0x0006A808
		public static global::NGC.IExecuter Exec
		{
			get
			{
				return global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Executer.Singleton;
			}
		}

		// Token: 0x02000373 RID: 883
		public struct Block
		{
			// Token: 0x04000EF4 RID: 3828
			public P0 p0;

			// Token: 0x04000EF5 RID: 3829
			public P1 p1;

			// Token: 0x04000EF6 RID: 3830
			public P2 p2;

			// Token: 0x04000EF7 RID: 3831
			public P3 p3;

			// Token: 0x04000EF8 RID: 3832
			public P4 p4;

			// Token: 0x04000EF9 RID: 3833
			public P5 p5;

			// Token: 0x04000EFA RID: 3834
			public P6 p6;

			// Token: 0x04000EFB RID: 3835
			public P7 p7;

			// Token: 0x04000EFC RID: 3836
			public P8 p8;

			// Token: 0x04000EFD RID: 3837
			public P9 p9;
		}

		// Token: 0x02000374 RID: 884
		private sealed class Executer : global::NGC.IExecuter
		{
			// Token: 0x06001D08 RID: 7432 RVA: 0x0006C624 File Offset: 0x0006A824
			public void ExecuteCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.InvokeCall(stream, ref d, method, instance);
			}

			// Token: 0x06001D09 RID: 7433 RVA: 0x0006C630 File Offset: 0x0006A830
			public IEnumerator ExecuteRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.InvokeRoutine(stream, ref d, method, instance);
			}

			// Token: 0x06001D0A RID: 7434 RVA: 0x0006C63C File Offset: 0x0006A83C
			public void ExecuteInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.InvokeInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x06001D0B RID: 7435 RVA: 0x0006C64C File Offset: 0x0006A84C
			public IEnumerator ExecuteInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				return global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.InvokeInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x06001D0C RID: 7436 RVA: 0x0006C65C File Offset: 0x0006A85C
			public void ExecuteStreamCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.InvokeStreamCall(stream, ref d, method, instance);
			}

			// Token: 0x06001D0D RID: 7437 RVA: 0x0006C668 File Offset: 0x0006A868
			public IEnumerator ExecuteStreamRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.InvokeStreamRoutine(stream, ref d, method, instance);
			}

			// Token: 0x06001D0E RID: 7438 RVA: 0x0006C674 File Offset: 0x0006A874
			public void ExecuteStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.InvokeStreamInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x06001D0F RID: 7439 RVA: 0x0006C684 File Offset: 0x0006A884
			public IEnumerator ExecuteStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				return global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.InvokeStreamInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x04000EFE RID: 3838
			public static readonly global::NGC.IExecuter Singleton = new global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Executer();
		}

		// Token: 0x02000375 RID: 885
		// (Invoke) Token: 0x06001D11 RID: 7441
		public delegate void Call(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9);

		// Token: 0x02000376 RID: 886
		// (Invoke) Token: 0x06001D15 RID: 7445
		public delegate IEnumerator Routine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9);

		// Token: 0x02000377 RID: 887
		// (Invoke) Token: 0x06001D19 RID: 7449
		public delegate void InfoCall(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, uLink.NetworkMessageInfo info);

		// Token: 0x02000378 RID: 888
		// (Invoke) Token: 0x06001D1D RID: 7453
		public delegate IEnumerator InfoRoutine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, uLink.NetworkMessageInfo info);

		// Token: 0x02000379 RID: 889
		// (Invoke) Token: 0x06001D21 RID: 7457
		public delegate void StreamCall(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, BitStream stream);

		// Token: 0x0200037A RID: 890
		// (Invoke) Token: 0x06001D25 RID: 7461
		public delegate IEnumerator StreamRoutine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, BitStream stream);

		// Token: 0x0200037B RID: 891
		// (Invoke) Token: 0x06001D29 RID: 7465
		public delegate void StreamInfoCall(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, uLink.NetworkMessageInfo info, BitStream stream);

		// Token: 0x0200037C RID: 892
		// (Invoke) Token: 0x06001D2D RID: 7469
		public delegate IEnumerator StreamInfoRoutine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, uLink.NetworkMessageInfo info, BitStream stream);
	}

	// Token: 0x0200037D RID: 893
	public static class callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>
	{
		// Token: 0x06001D30 RID: 7472 RVA: 0x0006C694 File Offset: 0x0006A894
		static callf()
		{
			BitStreamCodec.Add<global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Block>(new BitStreamCodec.Deserializer(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Deserializer), new BitStreamCodec.Serializer(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Serializer));
		}

		// Token: 0x06001D31 RID: 7473 RVA: 0x0006C6B4 File Offset: 0x0006A8B4
		private static object Deserializer(BitStream stream, params object[] codecOptions)
		{
			global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Block block;
			block.p0 = stream.Read<P0>(codecOptions);
			block.p1 = stream.Read<P1>(codecOptions);
			block.p2 = stream.Read<P2>(codecOptions);
			block.p3 = stream.Read<P3>(codecOptions);
			block.p4 = stream.Read<P4>(codecOptions);
			block.p5 = stream.Read<P5>(codecOptions);
			block.p6 = stream.Read<P6>(codecOptions);
			block.p7 = stream.Read<P7>(codecOptions);
			block.p8 = stream.Read<P8>(codecOptions);
			block.p9 = stream.Read<P9>(codecOptions);
			block.p10 = stream.Read<P10>(codecOptions);
			return block;
		}

		// Token: 0x06001D32 RID: 7474 RVA: 0x0006C764 File Offset: 0x0006A964
		private static void Serializer(BitStream stream, object value, params object[] codecOptions)
		{
			global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Block block = (global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Block)value;
			stream.Write<P0>(block.p0, codecOptions);
			stream.Write<P1>(block.p1, codecOptions);
			stream.Write<P2>(block.p2, codecOptions);
			stream.Write<P3>(block.p3, codecOptions);
			stream.Write<P4>(block.p4, codecOptions);
			stream.Write<P5>(block.p5, codecOptions);
			stream.Write<P6>(block.p6, codecOptions);
			stream.Write<P7>(block.p7, codecOptions);
			stream.Write<P8>(block.p8, codecOptions);
			stream.Write<P9>(block.p9, codecOptions);
			stream.Write<P10>(block.p10, codecOptions);
		}

		// Token: 0x06001D33 RID: 7475 RVA: 0x0006C814 File Offset: 0x0006AA14
		public static void InvokeCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			P6 p7 = stream.Read<P6>(new object[0]);
			P7 p8 = stream.Read<P7>(new object[0]);
			P8 p9 = stream.Read<P8>(new object[0]);
			P9 p10 = stream.Read<P9>(new object[0]);
			P10 p11 = stream.Read<P10>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Call), instance, method, true);
			}
			((global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Call)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
		}

		// Token: 0x06001D34 RID: 7476 RVA: 0x0006C8F0 File Offset: 0x0006AAF0
		public static IEnumerator InvokeRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			P6 p7 = stream.Read<P6>(new object[0]);
			P7 p8 = stream.Read<P7>(new object[0]);
			P8 p9 = stream.Read<P8>(new object[0]);
			P9 p10 = stream.Read<P9>(new object[0]);
			P10 p11 = stream.Read<P10>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Routine), instance, method, true);
			}
			return ((global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Routine)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
		}

		// Token: 0x06001D35 RID: 7477 RVA: 0x0006C9CC File Offset: 0x0006ABCC
		public static void InvokeInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			P6 p7 = stream.Read<P6>(new object[0]);
			P7 p8 = stream.Read<P7>(new object[0]);
			P8 p9 = stream.Read<P8>(new object[0]);
			P9 p10 = stream.Read<P9>(new object[0]);
			P10 p11 = stream.Read<P10>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.InfoCall), instance, method, true);
			}
			((global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.InfoCall)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, info);
		}

		// Token: 0x06001D36 RID: 7478 RVA: 0x0006CAAC File Offset: 0x0006ACAC
		public static IEnumerator InvokeInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			P6 p7 = stream.Read<P6>(new object[0]);
			P7 p8 = stream.Read<P7>(new object[0]);
			P8 p9 = stream.Read<P8>(new object[0]);
			P9 p10 = stream.Read<P9>(new object[0]);
			P10 p11 = stream.Read<P10>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.InfoRoutine), instance, method, true);
			}
			return ((global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.InfoRoutine)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, info);
		}

		// Token: 0x06001D37 RID: 7479 RVA: 0x0006CB8C File Offset: 0x0006AD8C
		public static void InvokeStreamCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			P6 p7 = stream.Read<P6>(new object[0]);
			P7 p8 = stream.Read<P7>(new object[0]);
			P8 p9 = stream.Read<P8>(new object[0]);
			P9 p10 = stream.Read<P9>(new object[0]);
			P10 p11 = stream.Read<P10>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.StreamCall), instance, method, true);
			}
			((global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.StreamCall)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, stream);
		}

		// Token: 0x06001D38 RID: 7480 RVA: 0x0006CC6C File Offset: 0x0006AE6C
		public static IEnumerator InvokeStreamRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			P6 p7 = stream.Read<P6>(new object[0]);
			P7 p8 = stream.Read<P7>(new object[0]);
			P8 p9 = stream.Read<P8>(new object[0]);
			P9 p10 = stream.Read<P9>(new object[0]);
			P10 p11 = stream.Read<P10>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.StreamRoutine), instance, method, true);
			}
			return ((global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.StreamRoutine)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, stream);
		}

		// Token: 0x06001D39 RID: 7481 RVA: 0x0006CD4C File Offset: 0x0006AF4C
		public static void InvokeStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			P6 p7 = stream.Read<P6>(new object[0]);
			P7 p8 = stream.Read<P7>(new object[0]);
			P8 p9 = stream.Read<P8>(new object[0]);
			P9 p10 = stream.Read<P9>(new object[0]);
			P10 p11 = stream.Read<P10>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.StreamInfoCall), instance, method, true);
			}
			((global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.StreamInfoCall)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, info, stream);
		}

		// Token: 0x06001D3A RID: 7482 RVA: 0x0006CE2C File Offset: 0x0006B02C
		public static IEnumerator InvokeStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			P6 p7 = stream.Read<P6>(new object[0]);
			P7 p8 = stream.Read<P7>(new object[0]);
			P8 p9 = stream.Read<P8>(new object[0]);
			P9 p10 = stream.Read<P9>(new object[0]);
			P10 p11 = stream.Read<P10>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.StreamInfoRoutine), instance, method, true);
			}
			return ((global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.StreamInfoRoutine)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, info, stream);
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x06001D3B RID: 7483 RVA: 0x0006CF0C File Offset: 0x0006B10C
		public static global::NGC.IExecuter Exec
		{
			get
			{
				return global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Executer.Singleton;
			}
		}

		// Token: 0x0200037E RID: 894
		public struct Block
		{
			// Token: 0x04000EFF RID: 3839
			public P0 p0;

			// Token: 0x04000F00 RID: 3840
			public P1 p1;

			// Token: 0x04000F01 RID: 3841
			public P2 p2;

			// Token: 0x04000F02 RID: 3842
			public P3 p3;

			// Token: 0x04000F03 RID: 3843
			public P4 p4;

			// Token: 0x04000F04 RID: 3844
			public P5 p5;

			// Token: 0x04000F05 RID: 3845
			public P6 p6;

			// Token: 0x04000F06 RID: 3846
			public P7 p7;

			// Token: 0x04000F07 RID: 3847
			public P8 p8;

			// Token: 0x04000F08 RID: 3848
			public P9 p9;

			// Token: 0x04000F09 RID: 3849
			public P10 p10;
		}

		// Token: 0x0200037F RID: 895
		private sealed class Executer : global::NGC.IExecuter
		{
			// Token: 0x06001D3E RID: 7486 RVA: 0x0006CF28 File Offset: 0x0006B128
			public void ExecuteCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.InvokeCall(stream, ref d, method, instance);
			}

			// Token: 0x06001D3F RID: 7487 RVA: 0x0006CF34 File Offset: 0x0006B134
			public IEnumerator ExecuteRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.InvokeRoutine(stream, ref d, method, instance);
			}

			// Token: 0x06001D40 RID: 7488 RVA: 0x0006CF40 File Offset: 0x0006B140
			public void ExecuteInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.InvokeInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x06001D41 RID: 7489 RVA: 0x0006CF50 File Offset: 0x0006B150
			public IEnumerator ExecuteInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				return global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.InvokeInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x06001D42 RID: 7490 RVA: 0x0006CF60 File Offset: 0x0006B160
			public void ExecuteStreamCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.InvokeStreamCall(stream, ref d, method, instance);
			}

			// Token: 0x06001D43 RID: 7491 RVA: 0x0006CF6C File Offset: 0x0006B16C
			public IEnumerator ExecuteStreamRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.InvokeStreamRoutine(stream, ref d, method, instance);
			}

			// Token: 0x06001D44 RID: 7492 RVA: 0x0006CF78 File Offset: 0x0006B178
			public void ExecuteStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.InvokeStreamInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x06001D45 RID: 7493 RVA: 0x0006CF88 File Offset: 0x0006B188
			public IEnumerator ExecuteStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				return global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.InvokeStreamInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x04000F0A RID: 3850
			public static readonly global::NGC.IExecuter Singleton = new global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Executer();
		}

		// Token: 0x02000380 RID: 896
		// (Invoke) Token: 0x06001D47 RID: 7495
		public delegate void Call(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10);

		// Token: 0x02000381 RID: 897
		// (Invoke) Token: 0x06001D4B RID: 7499
		public delegate IEnumerator Routine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10);

		// Token: 0x02000382 RID: 898
		// (Invoke) Token: 0x06001D4F RID: 7503
		public delegate void InfoCall(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, uLink.NetworkMessageInfo info);

		// Token: 0x02000383 RID: 899
		// (Invoke) Token: 0x06001D53 RID: 7507
		public delegate IEnumerator InfoRoutine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, uLink.NetworkMessageInfo info);

		// Token: 0x02000384 RID: 900
		// (Invoke) Token: 0x06001D57 RID: 7511
		public delegate void StreamCall(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, BitStream stream);

		// Token: 0x02000385 RID: 901
		// (Invoke) Token: 0x06001D5B RID: 7515
		public delegate IEnumerator StreamRoutine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, BitStream stream);

		// Token: 0x02000386 RID: 902
		// (Invoke) Token: 0x06001D5F RID: 7519
		public delegate void StreamInfoCall(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, uLink.NetworkMessageInfo info, BitStream stream);

		// Token: 0x02000387 RID: 903
		// (Invoke) Token: 0x06001D63 RID: 7523
		public delegate IEnumerator StreamInfoRoutine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, uLink.NetworkMessageInfo info, BitStream stream);
	}

	// Token: 0x02000388 RID: 904
	public static class callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>
	{
		// Token: 0x06001D66 RID: 7526 RVA: 0x0006CF98 File Offset: 0x0006B198
		static callf()
		{
			BitStreamCodec.Add<global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Block>(new BitStreamCodec.Deserializer(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Deserializer), new BitStreamCodec.Serializer(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Serializer));
		}

		// Token: 0x06001D67 RID: 7527 RVA: 0x0006CFB8 File Offset: 0x0006B1B8
		private static object Deserializer(BitStream stream, params object[] codecOptions)
		{
			global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Block block;
			block.p0 = stream.Read<P0>(codecOptions);
			block.p1 = stream.Read<P1>(codecOptions);
			block.p2 = stream.Read<P2>(codecOptions);
			block.p3 = stream.Read<P3>(codecOptions);
			block.p4 = stream.Read<P4>(codecOptions);
			block.p5 = stream.Read<P5>(codecOptions);
			block.p6 = stream.Read<P6>(codecOptions);
			block.p7 = stream.Read<P7>(codecOptions);
			block.p8 = stream.Read<P8>(codecOptions);
			block.p9 = stream.Read<P9>(codecOptions);
			block.p10 = stream.Read<P10>(codecOptions);
			block.p11 = stream.Read<P11>(codecOptions);
			return block;
		}

		// Token: 0x06001D68 RID: 7528 RVA: 0x0006D074 File Offset: 0x0006B274
		private static void Serializer(BitStream stream, object value, params object[] codecOptions)
		{
			global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Block block = (global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Block)value;
			stream.Write<P0>(block.p0, codecOptions);
			stream.Write<P1>(block.p1, codecOptions);
			stream.Write<P2>(block.p2, codecOptions);
			stream.Write<P3>(block.p3, codecOptions);
			stream.Write<P4>(block.p4, codecOptions);
			stream.Write<P5>(block.p5, codecOptions);
			stream.Write<P6>(block.p6, codecOptions);
			stream.Write<P7>(block.p7, codecOptions);
			stream.Write<P8>(block.p8, codecOptions);
			stream.Write<P9>(block.p9, codecOptions);
			stream.Write<P10>(block.p10, codecOptions);
			stream.Write<P11>(block.p11, codecOptions);
		}

		// Token: 0x06001D69 RID: 7529 RVA: 0x0006D130 File Offset: 0x0006B330
		public static void InvokeCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			P6 p7 = stream.Read<P6>(new object[0]);
			P7 p8 = stream.Read<P7>(new object[0]);
			P8 p9 = stream.Read<P8>(new object[0]);
			P9 p10 = stream.Read<P9>(new object[0]);
			P10 p11 = stream.Read<P10>(new object[0]);
			P11 p12 = stream.Read<P11>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Call), instance, method, true);
			}
			((global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Call)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12);
		}

		// Token: 0x06001D6A RID: 7530 RVA: 0x0006D21C File Offset: 0x0006B41C
		public static IEnumerator InvokeRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			P6 p7 = stream.Read<P6>(new object[0]);
			P7 p8 = stream.Read<P7>(new object[0]);
			P8 p9 = stream.Read<P8>(new object[0]);
			P9 p10 = stream.Read<P9>(new object[0]);
			P10 p11 = stream.Read<P10>(new object[0]);
			P11 p12 = stream.Read<P11>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Routine), instance, method, true);
			}
			return ((global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Routine)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12);
		}

		// Token: 0x06001D6B RID: 7531 RVA: 0x0006D308 File Offset: 0x0006B508
		public static void InvokeInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			P6 p7 = stream.Read<P6>(new object[0]);
			P7 p8 = stream.Read<P7>(new object[0]);
			P8 p9 = stream.Read<P8>(new object[0]);
			P9 p10 = stream.Read<P9>(new object[0]);
			P10 p11 = stream.Read<P10>(new object[0]);
			P11 p12 = stream.Read<P11>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.InfoCall), instance, method, true);
			}
			((global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.InfoCall)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, info);
		}

		// Token: 0x06001D6C RID: 7532 RVA: 0x0006D3F8 File Offset: 0x0006B5F8
		public static IEnumerator InvokeInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			P6 p7 = stream.Read<P6>(new object[0]);
			P7 p8 = stream.Read<P7>(new object[0]);
			P8 p9 = stream.Read<P8>(new object[0]);
			P9 p10 = stream.Read<P9>(new object[0]);
			P10 p11 = stream.Read<P10>(new object[0]);
			P11 p12 = stream.Read<P11>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.InfoRoutine), instance, method, true);
			}
			return ((global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.InfoRoutine)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, info);
		}

		// Token: 0x06001D6D RID: 7533 RVA: 0x0006D4E8 File Offset: 0x0006B6E8
		public static void InvokeStreamCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			P6 p7 = stream.Read<P6>(new object[0]);
			P7 p8 = stream.Read<P7>(new object[0]);
			P8 p9 = stream.Read<P8>(new object[0]);
			P9 p10 = stream.Read<P9>(new object[0]);
			P10 p11 = stream.Read<P10>(new object[0]);
			P11 p12 = stream.Read<P11>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.StreamCall), instance, method, true);
			}
			((global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.StreamCall)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, stream);
		}

		// Token: 0x06001D6E RID: 7534 RVA: 0x0006D5D8 File Offset: 0x0006B7D8
		public static IEnumerator InvokeStreamRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			P6 p7 = stream.Read<P6>(new object[0]);
			P7 p8 = stream.Read<P7>(new object[0]);
			P8 p9 = stream.Read<P8>(new object[0]);
			P9 p10 = stream.Read<P9>(new object[0]);
			P10 p11 = stream.Read<P10>(new object[0]);
			P11 p12 = stream.Read<P11>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.StreamRoutine), instance, method, true);
			}
			return ((global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.StreamRoutine)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, stream);
		}

		// Token: 0x06001D6F RID: 7535 RVA: 0x0006D6C8 File Offset: 0x0006B8C8
		public static void InvokeStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			P6 p7 = stream.Read<P6>(new object[0]);
			P7 p8 = stream.Read<P7>(new object[0]);
			P8 p9 = stream.Read<P8>(new object[0]);
			P9 p10 = stream.Read<P9>(new object[0]);
			P10 p11 = stream.Read<P10>(new object[0]);
			P11 p12 = stream.Read<P11>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.StreamInfoCall), instance, method, true);
			}
			((global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.StreamInfoCall)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, info, stream);
		}

		// Token: 0x06001D70 RID: 7536 RVA: 0x0006D7B8 File Offset: 0x0006B9B8
		public static IEnumerator InvokeStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			P6 p7 = stream.Read<P6>(new object[0]);
			P7 p8 = stream.Read<P7>(new object[0]);
			P8 p9 = stream.Read<P8>(new object[0]);
			P9 p10 = stream.Read<P9>(new object[0]);
			P10 p11 = stream.Read<P10>(new object[0]);
			P11 p12 = stream.Read<P11>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.StreamInfoRoutine), instance, method, true);
			}
			return ((global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.StreamInfoRoutine)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, info, stream);
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06001D71 RID: 7537 RVA: 0x0006D8A8 File Offset: 0x0006BAA8
		public static global::NGC.IExecuter Exec
		{
			get
			{
				return global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Executer.Singleton;
			}
		}

		// Token: 0x02000389 RID: 905
		public struct Block
		{
			// Token: 0x04000F0B RID: 3851
			public P0 p0;

			// Token: 0x04000F0C RID: 3852
			public P1 p1;

			// Token: 0x04000F0D RID: 3853
			public P2 p2;

			// Token: 0x04000F0E RID: 3854
			public P3 p3;

			// Token: 0x04000F0F RID: 3855
			public P4 p4;

			// Token: 0x04000F10 RID: 3856
			public P5 p5;

			// Token: 0x04000F11 RID: 3857
			public P6 p6;

			// Token: 0x04000F12 RID: 3858
			public P7 p7;

			// Token: 0x04000F13 RID: 3859
			public P8 p8;

			// Token: 0x04000F14 RID: 3860
			public P9 p9;

			// Token: 0x04000F15 RID: 3861
			public P10 p10;

			// Token: 0x04000F16 RID: 3862
			public P11 p11;
		}

		// Token: 0x0200038A RID: 906
		private sealed class Executer : global::NGC.IExecuter
		{
			// Token: 0x06001D74 RID: 7540 RVA: 0x0006D8C4 File Offset: 0x0006BAC4
			public void ExecuteCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.InvokeCall(stream, ref d, method, instance);
			}

			// Token: 0x06001D75 RID: 7541 RVA: 0x0006D8D0 File Offset: 0x0006BAD0
			public IEnumerator ExecuteRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.InvokeRoutine(stream, ref d, method, instance);
			}

			// Token: 0x06001D76 RID: 7542 RVA: 0x0006D8DC File Offset: 0x0006BADC
			public void ExecuteInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.InvokeInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x06001D77 RID: 7543 RVA: 0x0006D8EC File Offset: 0x0006BAEC
			public IEnumerator ExecuteInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				return global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.InvokeInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x06001D78 RID: 7544 RVA: 0x0006D8FC File Offset: 0x0006BAFC
			public void ExecuteStreamCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.InvokeStreamCall(stream, ref d, method, instance);
			}

			// Token: 0x06001D79 RID: 7545 RVA: 0x0006D908 File Offset: 0x0006BB08
			public IEnumerator ExecuteStreamRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.InvokeStreamRoutine(stream, ref d, method, instance);
			}

			// Token: 0x06001D7A RID: 7546 RVA: 0x0006D914 File Offset: 0x0006BB14
			public void ExecuteStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.InvokeStreamInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x06001D7B RID: 7547 RVA: 0x0006D924 File Offset: 0x0006BB24
			public IEnumerator ExecuteStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, uLink.NetworkMessageInfo info)
			{
				return global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.InvokeStreamInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x04000F17 RID: 3863
			public static readonly global::NGC.IExecuter Singleton = new global::NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Executer();
		}

		// Token: 0x0200038B RID: 907
		// (Invoke) Token: 0x06001D7D RID: 7549
		public delegate void Call(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11);

		// Token: 0x0200038C RID: 908
		// (Invoke) Token: 0x06001D81 RID: 7553
		public delegate IEnumerator Routine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11);

		// Token: 0x0200038D RID: 909
		// (Invoke) Token: 0x06001D85 RID: 7557
		public delegate void InfoCall(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11, uLink.NetworkMessageInfo info);

		// Token: 0x0200038E RID: 910
		// (Invoke) Token: 0x06001D89 RID: 7561
		public delegate IEnumerator InfoRoutine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11, uLink.NetworkMessageInfo info);

		// Token: 0x0200038F RID: 911
		// (Invoke) Token: 0x06001D8D RID: 7565
		public delegate void StreamCall(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11, BitStream stream);

		// Token: 0x02000390 RID: 912
		// (Invoke) Token: 0x06001D91 RID: 7569
		public delegate IEnumerator StreamRoutine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11, BitStream stream);

		// Token: 0x02000391 RID: 913
		// (Invoke) Token: 0x06001D95 RID: 7573
		public delegate void StreamInfoCall(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11, uLink.NetworkMessageInfo info, BitStream stream);

		// Token: 0x02000392 RID: 914
		// (Invoke) Token: 0x06001D99 RID: 7577
		public delegate IEnumerator StreamInfoRoutine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11, uLink.NetworkMessageInfo info, BitStream stream);
	}

	// Token: 0x02000393 RID: 915
	// (Invoke) Token: 0x06001D9D RID: 7581
	public delegate void EventCallback(global::NGCView view);
}
