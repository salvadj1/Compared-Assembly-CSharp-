using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Facepunch;
using uLink;
using UnityEngine;

// Token: 0x020002BE RID: 702
[AddComponentMenu("")]
public sealed class NGC : MonoBehaviour
{
	// Token: 0x060018FE RID: 6398 RVA: 0x000626E4 File Offset: 0x000608E4
	private void uLink_OnNetworkInstantiate(NetworkMessageInfo info)
	{
		NGC ngc;
		if (NGC.Global.byGroup.TryGetValue(this.groupNumber, out ngc))
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
		NGC.Global.all.Add(this);
		this.groupNumber = (ushort)this.networkView.group.id;
		NGC.Global.byGroup[this.groupNumber] = this;
		this.added = true;
		this.creation = info;
	}

	// Token: 0x060018FF RID: 6399 RVA: 0x0006276C File Offset: 0x0006096C
	private void Release()
	{
		if (this.added)
		{
			if (NGC.Global.all.Remove(this))
			{
				NGC.Global.byGroup.Remove(this.groupNumber);
			}
			this.added = false;
		}
	}

	// Token: 0x06001900 RID: 6400 RVA: 0x000627A4 File Offset: 0x000609A4
	private void DestroyView(NGCView view, bool andGameObject, bool skipPreDestroy)
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

	// Token: 0x06001901 RID: 6401 RVA: 0x00062820 File Offset: 0x00060A20
	private void PreDestroy()
	{
		List<NGCView> list = new List<NGCView>(this.views.Values);
		foreach (NGCView view in list)
		{
			this.DestroyView(view, false, false);
		}
		foreach (NGCView view2 in list)
		{
			this.DestroyView(view2, true, true);
		}
	}

	// Token: 0x06001902 RID: 6402 RVA: 0x000628E8 File Offset: 0x00060AE8
	private void OnDestroy()
	{
		this.Release();
	}

	// Token: 0x06001903 RID: 6403 RVA: 0x000628F0 File Offset: 0x00060AF0
	internal Dictionary<ushort, NGCView>.ValueCollection GetViews()
	{
		return this.views.Values;
	}

	// Token: 0x06001904 RID: 6404 RVA: 0x00062900 File Offset: 0x00060B00
	private NGCView Add(byte[] data, int offset, int length, NetworkMessageInfo info)
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
		NGC.Prefab prefab;
		NGC.Prefab.Register.Find(index, out prefab);
		NGCView ngcview = (NGCView)Object.Instantiate(prefab.prefab, vector, quaternion);
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
		ngcview.install = new NGC.Prefab.Installation.Instance(prefab.installation);
		return ngcview;
	}

	// Token: 0x06001905 RID: 6405 RVA: 0x00062A54 File Offset: 0x00060C54
	private NGCView Delete(ushort id, NetworkMessageInfo info)
	{
		NGCView ngcview = this.views[id];
		this.DestroyView(ngcview, false, false);
		this.views.Remove(id);
		return ngcview;
	}

	// Token: 0x06001906 RID: 6406 RVA: 0x00062A88 File Offset: 0x00060C88
	private NGC.Procedure Message(int id, int msg, byte[] args, int argByteSize, NetworkMessageInfo info)
	{
		return new NGC.Procedure
		{
			outer = this,
			target = id,
			message = msg,
			data = args,
			dataLength = argByteSize,
			info = info
		};
	}

	// Token: 0x06001907 RID: 6407 RVA: 0x00062AC8 File Offset: 0x00060CC8
	private NGC.Procedure Message(int id_msg, byte[] args, int argByteSize, NetworkMessageInfo info)
	{
		return this.Message(id_msg >> 16 & 65535, id_msg & 65535, args, argByteSize, info);
	}

	// Token: 0x06001908 RID: 6408 RVA: 0x00062AE8 File Offset: 0x00060CE8
	private NGC.Procedure Message(byte[] data, int offset, int length, NetworkMessageInfo info)
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

	// Token: 0x06001909 RID: 6409 RVA: 0x00062B4C File Offset: 0x00060D4C
	[RPC]
	internal void A(byte[] data, NetworkMessageInfo info)
	{
		NGCView ngcview = this.Add(data, 0, data.Length, info);
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

	// Token: 0x0600190A RID: 6410 RVA: 0x00062BAC File Offset: 0x00060DAC
	[RPC]
	internal void D(ushort id, NetworkMessageInfo info)
	{
		NGCView view = this.Delete(id, info);
		this.DestroyView(view, true, true);
	}

	// Token: 0x0600190B RID: 6411 RVA: 0x00062BCC File Offset: 0x00060DCC
	[RPC]
	internal void C(byte[] data, NetworkMessageInfo info)
	{
		NGC.Procedure procedure = this.Message(data, 0, data.Length, info);
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
			else if (NGC.log_nonexistant_ngc_errors)
			{
				Debug.LogWarning(string.Format("Did not call rpc to non existant view# {0}. ( message id was {1} )", procedure.target, procedure.message), this);
			}
		}
	}

	// Token: 0x0600190C RID: 6412 RVA: 0x00062CA8 File Offset: 0x00060EA8
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

	// Token: 0x0600190D RID: 6413 RVA: 0x00062D20 File Offset: 0x00060F20
	private void ShootRPC(NGC.RPCName rpc, RPCMode mode, byte[] data)
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

	// Token: 0x0600190E RID: 6414 RVA: 0x00062D70 File Offset: 0x00060F70
	private void ShootRPC(NGC.RPCName rpc, NetworkPlayer target, byte[] data)
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

	// Token: 0x0600190F RID: 6415 RVA: 0x00062DC0 File Offset: 0x00060FC0
	private void ShootRPC(NGC.RPCName rpc, IEnumerable<NetworkPlayer> targets, byte[] data)
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

	// Token: 0x06001910 RID: 6416 RVA: 0x00062E10 File Offset: 0x00061010
	private static NGC.RPCName CallRPCName(NetworkFlags? flags, NGCView view, int messageID)
	{
		return new NGC.RPCName(view, messageID, "C", (flags == null) ? view.prefab.DefaultNetworkFlags(messageID) : flags.Value);
	}

	// Token: 0x06001911 RID: 6417 RVA: 0x00062E50 File Offset: 0x00061050
	private static NGC.RPCName CallRPCName(NetworkFlags? flags, NGCView view, int messageID, ref NetworkPlayer target)
	{
		return NGC.CallRPCName(flags, view, messageID);
	}

	// Token: 0x06001912 RID: 6418 RVA: 0x00062E5C File Offset: 0x0006105C
	private static NGC.RPCName CallRPCName(NetworkFlags? flags, NGCView view, int messageID, ref IEnumerable<NetworkPlayer> targets)
	{
		return NGC.CallRPCName(flags, view, messageID);
	}

	// Token: 0x06001913 RID: 6419 RVA: 0x00062E68 File Offset: 0x00061068
	private static NGC.RPCName CallRPCName(NetworkFlags? flags, NGCView view, int messageID, ref RPCMode mode)
	{
		return NGC.CallRPCName(flags, view, messageID);
	}

	// Token: 0x06001914 RID: 6420 RVA: 0x00062E74 File Offset: 0x00061074
	internal void NGCViewRPC(NetworkFlags flags, RPCMode mode, NGCView view, int messageID, byte[] arguments, int argumentsOffset, int argumentsLength)
	{
		this.ShootRPC(NGC.CallRPCName(new NetworkFlags?(flags), view, messageID, ref mode), mode, this.RPCData((int)view.innerID, messageID, arguments, argumentsOffset, argumentsLength));
	}

	// Token: 0x06001915 RID: 6421 RVA: 0x00062EAC File Offset: 0x000610AC
	internal void NGCViewRPC(NetworkFlags flags, NetworkPlayer target, NGCView view, int messageID, byte[] arguments, int argumentsOffset, int argumentsLength)
	{
		this.ShootRPC(NGC.CallRPCName(new NetworkFlags?(flags), view, messageID, ref target), target, this.RPCData((int)view.innerID, messageID, arguments, argumentsOffset, argumentsLength));
	}

	// Token: 0x06001916 RID: 6422 RVA: 0x00062EE4 File Offset: 0x000610E4
	internal void NGCViewRPC(NetworkFlags flags, IEnumerable<NetworkPlayer> targets, NGCView view, int messageID, byte[] arguments, int argumentsOffset, int argumentsLength)
	{
		this.ShootRPC(NGC.CallRPCName(new NetworkFlags?(flags), view, messageID, ref targets), targets, this.RPCData((int)view.innerID, messageID, arguments, argumentsOffset, argumentsLength));
	}

	// Token: 0x06001917 RID: 6423 RVA: 0x00062F1C File Offset: 0x0006111C
	internal void NGCViewRPC(RPCMode mode, NGCView view, int messageID, byte[] arguments, int argumentsOffset, int argumentsLength)
	{
		this.ShootRPC(NGC.CallRPCName(null, view, messageID, ref mode), mode, this.RPCData((int)view.innerID, messageID, arguments, argumentsOffset, argumentsLength));
	}

	// Token: 0x06001918 RID: 6424 RVA: 0x00062F58 File Offset: 0x00061158
	internal void NGCViewRPC(NetworkPlayer target, NGCView view, int messageID, byte[] arguments, int argumentsOffset, int argumentsLength)
	{
		this.ShootRPC(NGC.CallRPCName(null, view, messageID, ref target), target, this.RPCData((int)view.innerID, messageID, arguments, argumentsOffset, argumentsLength));
	}

	// Token: 0x06001919 RID: 6425 RVA: 0x00062F94 File Offset: 0x00061194
	internal void NGCViewRPC(IEnumerable<NetworkPlayer> targets, NGCView view, int messageID, byte[] arguments, int argumentsOffset, int argumentsLength)
	{
		this.ShootRPC(NGC.CallRPCName(null, view, messageID, ref targets), targets, this.RPCData((int)view.innerID, messageID, arguments, argumentsOffset, argumentsLength));
	}

	// Token: 0x0600191A RID: 6426 RVA: 0x00062FD0 File Offset: 0x000611D0
	[Obsolete("NO, Use net cull making sure the prefab name string you must use starts with ;", true)]
	public static Object Instantiate(Object obj)
	{
		return Object.Instantiate(obj);
	}

	// Token: 0x0600191B RID: 6427 RVA: 0x00062FD8 File Offset: 0x000611D8
	[Obsolete("NO, Use net cull making sure the prefab name string you must use starts with ;", true)]
	public static Object Instantiate(Object obj, Vector3 position, Quaternion rotation)
	{
		return Object.Instantiate(obj, position, rotation);
	}

	// Token: 0x0600191C RID: 6428 RVA: 0x00062FE4 File Offset: 0x000611E4
	private static NetworkView SpawnNGC_NetworkView(string prefabName, NetworkInstantiateArgs args, NetworkMessageInfo info)
	{
		NetworkInstantiatorUtility.AutoSetupNetworkViewOnAwake(args);
		GameObject gameObject = new GameObject(string.Format("__NGC-{0:X}", args.group), new Type[]
		{
			typeof(NGC),
			typeof(NGCInternalView)
		})
		{
			hideFlags = 1
		};
		NetworkInstantiatorUtility.ClearAutoSetupNetworkViewOnAwake();
		uLinkNetworkView component = gameObject.GetComponent<uLinkNetworkView>();
		NGC component2 = gameObject.GetComponent<NGC>();
		component.observed = component2;
		component.rpcReceiver = 1;
		component.stateSynchronization = 0;
		NetworkMessageInfo info2 = new NetworkMessageInfo(info, component);
		component2.uLink_OnNetworkInstantiate(info2);
		return component;
	}

	// Token: 0x0600191D RID: 6429 RVA: 0x00063078 File Offset: 0x00061278
	private static void DestroyNGC_NetworkView(NetworkView view)
	{
		NGC component = view.GetComponent<NGC>();
		component.PreDestroy();
		Object.Destroy(component);
		NetworkInstantiator.defaultDestroyer.Invoke(view);
	}

	// Token: 0x0600191E RID: 6430 RVA: 0x000630A4 File Offset: 0x000612A4
	public static void Register(NGCConfiguration configuration)
	{
		NetworkInstantiator.Add("!Ng", new NetworkInstantiator.Creator(NGC.SpawnNGC_NetworkView), new NetworkInstantiator.Destroyer(NGC.DestroyNGC_NetworkView));
		configuration.Install();
	}

	// Token: 0x0600191F RID: 6431 RVA: 0x000630DC File Offset: 0x000612DC
	internal static int PackID(int groupNumber, int innerID)
	{
		if (groupNumber < 0 || innerID <= 0)
		{
			return 0;
		}
		return (groupNumber & 65535) << 16 | innerID;
	}

	// Token: 0x06001920 RID: 6432 RVA: 0x000630FC File Offset: 0x000612FC
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

	// Token: 0x06001921 RID: 6433 RVA: 0x00063130 File Offset: 0x00061330
	public static NGCView Find(int id)
	{
		ushort key;
		ushort key2;
		if (!NGC.UnpackID(id, out key, out key2))
		{
			return null;
		}
		NGC ngc;
		if (!NGC.Global.byGroup.TryGetValue(key, out ngc))
		{
			return null;
		}
		NGCView result;
		ngc.views.TryGetValue(key2, out result);
		return result;
	}

	// Token: 0x06001922 RID: 6434 RVA: 0x00063174 File Offset: 0x00061374
	public static NGC.callf<P0>.Block BlockArgs<P0>(P0 p0)
	{
		NGC.callf<P0>.Block result;
		result.p0 = p0;
		return result;
	}

	// Token: 0x06001923 RID: 6435 RVA: 0x0006318C File Offset: 0x0006138C
	public static NGC.callf<P0, P1>.Block BlockArgs<P0, P1>(P0 p0, P1 p1)
	{
		NGC.callf<P0, P1>.Block result;
		result.p0 = p0;
		result.p1 = p1;
		return result;
	}

	// Token: 0x06001924 RID: 6436 RVA: 0x000631AC File Offset: 0x000613AC
	public static NGC.callf<P0, P1, P2>.Block BlockArgs<P0, P1, P2>(P0 p0, P1 p1, P2 p2)
	{
		NGC.callf<P0, P1, P2>.Block result;
		result.p0 = p0;
		result.p1 = p1;
		result.p2 = p2;
		return result;
	}

	// Token: 0x06001925 RID: 6437 RVA: 0x000631D4 File Offset: 0x000613D4
	public static NGC.callf<P0, P1, P2, P3>.Block BlockArgs<P0, P1, P2, P3>(P0 p0, P1 p1, P2 p2, P3 p3)
	{
		NGC.callf<P0, P1, P2, P3>.Block result;
		result.p0 = p0;
		result.p1 = p1;
		result.p2 = p2;
		result.p3 = p3;
		return result;
	}

	// Token: 0x06001926 RID: 6438 RVA: 0x00063204 File Offset: 0x00061404
	public static NGC.callf<P0, P1, P2, P3, P4>.Block BlockArgs<P0, P1, P2, P3, P4>(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4)
	{
		NGC.callf<P0, P1, P2, P3, P4>.Block result;
		result.p0 = p0;
		result.p1 = p1;
		result.p2 = p2;
		result.p3 = p3;
		result.p4 = p4;
		return result;
	}

	// Token: 0x06001927 RID: 6439 RVA: 0x0006323C File Offset: 0x0006143C
	public static NGC.callf<P0, P1, P2, P3, P4, P5>.Block BlockArgs<P0, P1, P2, P3, P4, P5>(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
	{
		NGC.callf<P0, P1, P2, P3, P4, P5>.Block result;
		result.p0 = p0;
		result.p1 = p1;
		result.p2 = p2;
		result.p3 = p3;
		result.p4 = p4;
		result.p5 = p5;
		return result;
	}

	// Token: 0x06001928 RID: 6440 RVA: 0x0006327C File Offset: 0x0006147C
	public static NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Block BlockArgs<P0, P1, P2, P3, P4, P5, P6>(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
	{
		NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Block result;
		result.p0 = p0;
		result.p1 = p1;
		result.p2 = p2;
		result.p3 = p3;
		result.p4 = p4;
		result.p5 = p5;
		result.p6 = p6;
		return result;
	}

	// Token: 0x06001929 RID: 6441 RVA: 0x000632C8 File Offset: 0x000614C8
	public static NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Block BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7>(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
	{
		NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Block result;
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

	// Token: 0x0600192A RID: 6442 RVA: 0x0006331C File Offset: 0x0006151C
	public static NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Block BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8>(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8)
	{
		NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Block result;
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

	// Token: 0x0600192B RID: 6443 RVA: 0x00063378 File Offset: 0x00061578
	public static NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Block BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9)
	{
		NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Block result;
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

	// Token: 0x0600192C RID: 6444 RVA: 0x000633DC File Offset: 0x000615DC
	public static NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Block BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10)
	{
		NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Block result;
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

	// Token: 0x0600192D RID: 6445 RVA: 0x0006344C File Offset: 0x0006164C
	public static NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Block BlockArgs<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11)
	{
		NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Block result;
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

	// Token: 0x0600192E RID: 6446 RVA: 0x000634C4 File Offset: 0x000616C4
	private static NGC.IExecuter FindExecuter(Type[] argumentTypes)
	{
		switch (argumentTypes.Length)
		{
		case 0:
			return typeof(NGC.callf).GetProperty("Exec", BindingFlags.Static | BindingFlags.Public).GetValue(null, null) as NGC.IExecuter;
		case 1:
			return typeof(NGC.callf<>).MakeGenericType(argumentTypes).GetProperty("Exec", BindingFlags.Static | BindingFlags.Public).GetValue(null, null) as NGC.IExecuter;
		case 2:
			return typeof(NGC.callf<, >).MakeGenericType(argumentTypes).GetProperty("Exec", BindingFlags.Static | BindingFlags.Public).GetValue(null, null) as NGC.IExecuter;
		case 3:
			return typeof(NGC.callf<, , >).MakeGenericType(argumentTypes).GetProperty("Exec", BindingFlags.Static | BindingFlags.Public).GetValue(null, null) as NGC.IExecuter;
		case 4:
			return typeof(NGC.callf<, , , >).MakeGenericType(argumentTypes).GetProperty("Exec", BindingFlags.Static | BindingFlags.Public).GetValue(null, null) as NGC.IExecuter;
		case 5:
			return typeof(NGC.callf<, , , , >).MakeGenericType(argumentTypes).GetProperty("Exec", BindingFlags.Static | BindingFlags.Public).GetValue(null, null) as NGC.IExecuter;
		case 6:
			return typeof(NGC.callf<, , , , , >).MakeGenericType(argumentTypes).GetProperty("Exec", BindingFlags.Static | BindingFlags.Public).GetValue(null, null) as NGC.IExecuter;
		case 7:
			return typeof(NGC.callf<, , , , , , >).MakeGenericType(argumentTypes).GetProperty("Exec", BindingFlags.Static | BindingFlags.Public).GetValue(null, null) as NGC.IExecuter;
		case 8:
			return typeof(NGC.callf<, , , , , , , >).MakeGenericType(argumentTypes).GetProperty("Exec", BindingFlags.Static | BindingFlags.Public).GetValue(null, null) as NGC.IExecuter;
		case 9:
			return typeof(NGC.callf<, , , , , , , , >).MakeGenericType(argumentTypes).GetProperty("Exec", BindingFlags.Static | BindingFlags.Public).GetValue(null, null) as NGC.IExecuter;
		case 10:
			return typeof(NGC.callf<, , , , , , , , , >).MakeGenericType(argumentTypes).GetProperty("Exec", BindingFlags.Static | BindingFlags.Public).GetValue(null, null) as NGC.IExecuter;
		case 11:
			return typeof(NGC.callf<, , , , , , , , , , >).MakeGenericType(argumentTypes).GetProperty("Exec", BindingFlags.Static | BindingFlags.Public).GetValue(null, null) as NGC.IExecuter;
		case 12:
			return typeof(NGC.callf<, , , , , , , , , , , >).MakeGenericType(argumentTypes).GetProperty("Exec", BindingFlags.Static | BindingFlags.Public).GetValue(null, null) as NGC.IExecuter;
		default:
			throw new ArgumentOutOfRangeException("argumentTypes.Length > {0}");
		}
	}

	// Token: 0x04000D52 RID: 3410
	private const string kAddRPC = "A";

	// Token: 0x04000D53 RID: 3411
	private const string kDeleteRPC = "D";

	// Token: 0x04000D54 RID: 3412
	private const string kCallRPC = "C";

	// Token: 0x04000D55 RID: 3413
	private const string kPrefabIdentifier = "!Ng";

	// Token: 0x04000D56 RID: 3414
	[NonSerialized]
	private bool added;

	// Token: 0x04000D57 RID: 3415
	[NonSerialized]
	internal ushort groupNumber;

	// Token: 0x04000D58 RID: 3416
	[NonSerialized]
	private NetworkMessageInfo creation;

	// Token: 0x04000D59 RID: 3417
	[NonSerialized]
	internal NGCInternalView networkView;

	// Token: 0x04000D5A RID: 3418
	[NonSerialized]
	internal NetworkViewID networkViewID;

	// Token: 0x04000D5B RID: 3419
	[NonSerialized]
	private readonly Dictionary<ushort, NGCView> views = new Dictionary<ushort, NGCView>();

	// Token: 0x04000D5C RID: 3420
	private static bool log_nonexistant_ngc_errors;

	// Token: 0x020002BF RID: 703
	private static class Global
	{
		// Token: 0x04000D5D RID: 3421
		public static readonly Dictionary<ushort, NGC> byGroup = new Dictionary<ushort, NGC>();

		// Token: 0x04000D5E RID: 3422
		public static readonly List<NGC> all = new List<NGC>();
	}

	// Token: 0x020002C0 RID: 704
	public interface IExecuter
	{
		// Token: 0x06001930 RID: 6448
		void ExecuteCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance);

		// Token: 0x06001931 RID: 6449
		IEnumerator ExecuteRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance);

		// Token: 0x06001932 RID: 6450
		void ExecuteInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info);

		// Token: 0x06001933 RID: 6451
		IEnumerator ExecuteInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info);

		// Token: 0x06001934 RID: 6452
		void ExecuteStreamCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance);

		// Token: 0x06001935 RID: 6453
		IEnumerator ExecuteStreamRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance);

		// Token: 0x06001936 RID: 6454
		void ExecuteStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info);

		// Token: 0x06001937 RID: 6455
		IEnumerator ExecuteStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info);
	}

	// Token: 0x020002C1 RID: 705
	public sealed class Prefab
	{
		// Token: 0x06001938 RID: 6456 RVA: 0x00063748 File Offset: 0x00061948
		private Prefab(string contentPath, int key, string handle)
		{
			this.contentPath = contentPath;
			this.key = key;
			this.handle = handle;
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x00063768 File Offset: 0x00061968
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

		// Token: 0x0600193A RID: 6458 RVA: 0x000637D4 File Offset: 0x000619D4
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

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x0600193B RID: 6459 RVA: 0x000638C0 File Offset: 0x00061AC0
		public NGC.Prefab.Installation installation
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

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x0600193C RID: 6460 RVA: 0x000638FC File Offset: 0x00061AFC
		public NGCView prefab
		{
			get
			{
				NGCView ngcview;
				if (this.weakReference == null || !(ngcview = (NGCView)this.weakReference.Target) || !this.weakReference.IsAlive)
				{
					if (!Bundling.Load<NGCView>(this.contentPath, typeof(NGCView), out ngcview))
					{
						throw new MissingReferenceException("Could not load NGCView at " + this.contentPath);
					}
					if (this._installation == null)
					{
						this._installation = NGC.Prefab.Installation.Create(ngcview);
					}
					this.weakReference = new WeakReference(ngcview);
				}
				return ngcview;
			}
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x00063998 File Offset: 0x00061B98
		internal NetworkFlags DefaultNetworkFlags(int messageIndex)
		{
			return this.installation.methods[messageIndex].defaultNetworkFlags;
		}

		// Token: 0x04000D5F RID: 3423
		[NonSerialized]
		public readonly string contentPath;

		// Token: 0x04000D60 RID: 3424
		[NonSerialized]
		public readonly int key;

		// Token: 0x04000D61 RID: 3425
		[NonSerialized]
		public readonly string handle;

		// Token: 0x04000D62 RID: 3426
		[NonSerialized]
		private NGC.Prefab.Installation _installation;

		// Token: 0x04000D63 RID: 3427
		private Dictionary<string, int> cachedMessageIndices;

		// Token: 0x04000D64 RID: 3428
		private WeakReference weakReference;

		// Token: 0x020002C2 RID: 706
		public sealed class Installation
		{
			// Token: 0x0600193E RID: 6462 RVA: 0x000639B0 File Offset: 0x00061BB0
			private Installation(NGC.Prefab.Installation.Method[] methods, ushort[] methodScriptIndices)
			{
				this.methods = methods;
				this.methodScriptIndices = methodScriptIndices;
			}

			// Token: 0x06001940 RID: 6464 RVA: 0x000639D4 File Offset: 0x00061BD4
			public static NGC.Prefab.Installation Create(NGCView view)
			{
				int num = 0;
				List<NGC.Prefab.Installation.Method[]> list = new List<NGC.Prefab.Installation.Method[]>();
				foreach (MonoBehaviour monoBehaviour in view.scripts)
				{
					Type type = monoBehaviour.GetType();
					NGC.Prefab.Installation.Method[] array;
					if (!NGC.Prefab.Installation.methodsForType.TryGetValue(type, out array))
					{
						List<NGC.Prefab.Installation.Method> list2 = new List<NGC.Prefab.Installation.Method>();
						MethodInfo[] array2 = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
						foreach (MethodInfo methodInfo in array2)
						{
							bool flag = false;
							if (methodInfo.IsDefined(typeof(RPC), true))
							{
								if (!methodInfo.IsDefined(typeof(NGCRPCSkipAttribute), false) || methodInfo.IsDefined(typeof(NGCRPCAttribute), true))
								{
									flag = true;
								}
							}
							else if (methodInfo.IsDefined(typeof(NGCRPCAttribute), true))
							{
								flag = true;
							}
							if (flag)
							{
								list2.Add(NGC.Prefab.Installation.Method.Create(methodInfo));
							}
						}
						list2.Sort((NGC.Prefab.Installation.Method x, NGC.Prefab.Installation.Method y) => x.method.Name.CompareTo(y.method.Name));
						array = list2.ToArray();
						NGC.Prefab.Installation.methodsForType[type] = array;
					}
					num += array.Length;
					list.Add(array);
				}
				NGC.Prefab.Installation.Method[] array4 = new NGC.Prefab.Installation.Method[num];
				ushort[] array5 = new ushort[num];
				int num2 = 0;
				ushort num3 = 0;
				foreach (NGC.Prefab.Installation.Method[] array6 in list)
				{
					foreach (NGC.Prefab.Installation.Method method in array6)
					{
						array4[num2] = method;
						array5[num2] = num3;
						num2++;
					}
					num3 += 1;
				}
				return new NGC.Prefab.Installation(array4, array5);
			}

			// Token: 0x04000D65 RID: 3429
			public readonly NGC.Prefab.Installation.Method[] methods;

			// Token: 0x04000D66 RID: 3430
			public readonly ushort[] methodScriptIndices;

			// Token: 0x04000D67 RID: 3431
			private static readonly Dictionary<Type, NGC.Prefab.Installation.Method[]> methodsForType = new Dictionary<Type, NGC.Prefab.Installation.Method[]>();

			// Token: 0x020002C3 RID: 707
			public sealed class Instance
			{
				// Token: 0x06001942 RID: 6466 RVA: 0x00063C08 File Offset: 0x00061E08
				public Instance(NGC.Prefab.Installation installation)
				{
					this.delegates = new Delegate[installation.methods.Length];
				}

				// Token: 0x06001943 RID: 6467 RVA: 0x00063C24 File Offset: 0x00061E24
				public void Invoke(NGC.Procedure procedure)
				{
					procedure.view.prefab.installation.methods[procedure.message].Invoke(ref this.delegates[procedure.message], procedure, procedure.view.prefab.installation.methodScriptIndices[procedure.message]);
				}

				// Token: 0x04000D69 RID: 3433
				public readonly Delegate[] delegates;
			}

			// Token: 0x020002C4 RID: 708
			public struct Method
			{
				// Token: 0x06001944 RID: 6468 RVA: 0x00063C84 File Offset: 0x00061E84
				private Method(MethodInfo method, byte flags, NGC.IExecuter executer)
				{
					this.method = method;
					this.flags = flags;
					this.executer = executer;
				}

				// Token: 0x17000740 RID: 1856
				// (get) Token: 0x06001945 RID: 6469 RVA: 0x00063C9C File Offset: 0x00061E9C
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

				// Token: 0x06001946 RID: 6470 RVA: 0x00063D20 File Offset: 0x00061F20
				public void Invoke(ref Delegate d, NGC.Procedure procedure, ushort scriptIndex)
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

				// Token: 0x06001947 RID: 6471 RVA: 0x00063EC0 File Offset: 0x000620C0
				public static NGC.Prefab.Installation.Method Create(MethodInfo info)
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
						if ((parameterType = parameters[parameters.Length - 1].ParameterType) == typeof(NetworkMessageInfo))
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
					NGC.IExecuter executer = NGC.FindExecuter(array);
					if (executer != null)
					{
						return new NGC.Prefab.Installation.Method(info, b, executer);
					}
					throw new InvalidProgramException();
				}

				// Token: 0x04000D6A RID: 3434
				private const byte FLAG_INFO = 1;

				// Token: 0x04000D6B RID: 3435
				private const byte FLAG_STREAM = 2;

				// Token: 0x04000D6C RID: 3436
				private const byte FLAG_ENUMERATOR = 4;

				// Token: 0x04000D6D RID: 3437
				private const byte FLAG_FORCE_UNBUFFERED = 8;

				// Token: 0x04000D6E RID: 3438
				private const byte FLAG_FORCE_INSECURE = 16;

				// Token: 0x04000D6F RID: 3439
				private const byte FLAG_FORCE_NO_TIMESTAMP = 32;

				// Token: 0x04000D70 RID: 3440
				private const byte FLAG_FORCE_UNRELIABLE = 64;

				// Token: 0x04000D71 RID: 3441
				private const byte FLAG_FORCE_TYPE_UNSAFE = 128;

				// Token: 0x04000D72 RID: 3442
				private const byte INVOKE_FLAGS = 7;

				// Token: 0x04000D73 RID: 3443
				public readonly MethodInfo method;

				// Token: 0x04000D74 RID: 3444
				public readonly byte flags;

				// Token: 0x04000D75 RID: 3445
				private readonly NGC.IExecuter executer;
			}
		}

		// Token: 0x020002C5 RID: 709
		public static class Register
		{
			// Token: 0x06001949 RID: 6473 RVA: 0x00064058 File Offset: 0x00062258
			public static bool Find(int index, out NGC.Prefab prefab)
			{
				return NGC.Prefab.Register.PrefabByIndex.TryGetValue(index, out prefab);
			}

			// Token: 0x0600194A RID: 6474 RVA: 0x00064068 File Offset: 0x00062268
			public static string FindName(int iIndex)
			{
				return NGC.Prefab.Register.PrefabByIndex[iIndex].handle;
			}

			// Token: 0x0600194B RID: 6475 RVA: 0x0006407C File Offset: 0x0006227C
			public static bool Find(string handle, out NGC.Prefab prefab)
			{
				return NGC.Prefab.Register.PrefabByName.TryGetValue(handle, out prefab);
			}

			// Token: 0x0600194C RID: 6476 RVA: 0x0006408C File Offset: 0x0006228C
			public static bool Add(string contentPath, int index, string handle)
			{
				bool result;
				try
				{
					NGC.Prefab prefab = new NGC.Prefab(contentPath, index, handle);
					NGC.Prefab.Register.PrefabByIndex.Add(index, prefab);
					try
					{
						NGC.Prefab.Register.PrefabByName.Add(handle, prefab);
					}
					catch
					{
						NGC.Prefab.Register.PrefabByIndex.Remove(index);
						throw;
					}
					NGC.Prefab.Register.Prefabs.Add(prefab);
					result = true;
				}
				catch
				{
					result = false;
				}
				return result;
			}

			// Token: 0x04000D76 RID: 3446
			private static readonly Dictionary<int, NGC.Prefab> PrefabByIndex = new Dictionary<int, NGC.Prefab>();

			// Token: 0x04000D77 RID: 3447
			private static readonly Dictionary<string, NGC.Prefab> PrefabByName = new Dictionary<string, NGC.Prefab>();

			// Token: 0x04000D78 RID: 3448
			private static readonly List<NGC.Prefab> Prefabs = new List<NGC.Prefab>();
		}
	}

	// Token: 0x020002C6 RID: 710
	public sealed class Procedure
	{
		// Token: 0x0600194E RID: 6478 RVA: 0x00064134 File Offset: 0x00062334
		public BitStream CreateBitStream()
		{
			if (this.dataLength == 0)
			{
				return new BitStream(false);
			}
			return new BitStream(this.data, false);
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x00064154 File Offset: 0x00062354
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

		// Token: 0x04000D79 RID: 3449
		public NGC outer;

		// Token: 0x04000D7A RID: 3450
		public int target;

		// Token: 0x04000D7B RID: 3451
		public int message;

		// Token: 0x04000D7C RID: 3452
		public byte[] data;

		// Token: 0x04000D7D RID: 3453
		public int dataLength;

		// Token: 0x04000D7E RID: 3454
		public NetworkMessageInfo info;

		// Token: 0x04000D7F RID: 3455
		public NGCView view;
	}

	// Token: 0x020002C7 RID: 711
	private struct RPCName
	{
		// Token: 0x06001950 RID: 6480 RVA: 0x00064200 File Offset: 0x00062400
		public RPCName(NGCView view, int message, string name, NetworkFlags flags)
		{
			this.name = name;
			this.flags = flags;
		}

		// Token: 0x04000D80 RID: 3456
		public readonly string name;

		// Token: 0x04000D81 RID: 3457
		public readonly NetworkFlags flags;
	}

	// Token: 0x020002C8 RID: 712
	public static class callf
	{
		// Token: 0x06001951 RID: 6481 RVA: 0x00064214 File Offset: 0x00062414
		public static void InvokeCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf.Call), instance, method, true);
			}
			((NGC.callf.Call)d)();
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x00064240 File Offset: 0x00062440
		public static IEnumerator InvokeRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf.Routine), instance, method, true);
			}
			return ((NGC.callf.Routine)d)();
		}

		// Token: 0x06001953 RID: 6483 RVA: 0x0006426C File Offset: 0x0006246C
		public static void InvokeInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
		{
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf.InfoCall), instance, method, true);
			}
			((NGC.callf.InfoCall)d)(info);
		}

		// Token: 0x06001954 RID: 6484 RVA: 0x00064298 File Offset: 0x00062498
		public static IEnumerator InvokeInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
		{
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf.InfoRoutine), instance, method, true);
			}
			return ((NGC.callf.InfoRoutine)d)(info);
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x000642C4 File Offset: 0x000624C4
		public static void InvokeStreamCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf.StreamCall), instance, method, true);
			}
			((NGC.callf.StreamCall)d)(stream);
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x000642FC File Offset: 0x000624FC
		public static IEnumerator InvokeStreamRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf.StreamRoutine), instance, method, true);
			}
			return ((NGC.callf.StreamRoutine)d)(stream);
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x00064334 File Offset: 0x00062534
		public static void InvokeStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
		{
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf.StreamInfoCall), instance, method, true);
			}
			((NGC.callf.StreamInfoCall)d)(info, stream);
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x0006436C File Offset: 0x0006256C
		public static IEnumerator InvokeStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
		{
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf.StreamInfoRoutine), instance, method, true);
			}
			return ((NGC.callf.StreamInfoRoutine)d)(info, stream);
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06001959 RID: 6489 RVA: 0x000643A4 File Offset: 0x000625A4
		public static NGC.IExecuter Exec
		{
			get
			{
				return NGC.callf.Executer.Singleton;
			}
		}

		// Token: 0x020002C9 RID: 713
		private sealed class Executer : NGC.IExecuter
		{
			// Token: 0x0600195C RID: 6492 RVA: 0x000643C0 File Offset: 0x000625C0
			public void ExecuteCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				NGC.callf.InvokeCall(stream, ref d, method, instance);
			}

			// Token: 0x0600195D RID: 6493 RVA: 0x000643CC File Offset: 0x000625CC
			public IEnumerator ExecuteRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return NGC.callf.InvokeRoutine(stream, ref d, method, instance);
			}

			// Token: 0x0600195E RID: 6494 RVA: 0x000643D8 File Offset: 0x000625D8
			public void ExecuteInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				NGC.callf.InvokeInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x0600195F RID: 6495 RVA: 0x000643E8 File Offset: 0x000625E8
			public IEnumerator ExecuteInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				return NGC.callf.InvokeInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x06001960 RID: 6496 RVA: 0x000643F8 File Offset: 0x000625F8
			public void ExecuteStreamCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				NGC.callf.InvokeStreamCall(stream, ref d, method, instance);
			}

			// Token: 0x06001961 RID: 6497 RVA: 0x00064404 File Offset: 0x00062604
			public IEnumerator ExecuteStreamRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return NGC.callf.InvokeStreamRoutine(stream, ref d, method, instance);
			}

			// Token: 0x06001962 RID: 6498 RVA: 0x00064410 File Offset: 0x00062610
			public void ExecuteStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				NGC.callf.InvokeStreamInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x06001963 RID: 6499 RVA: 0x00064420 File Offset: 0x00062620
			public IEnumerator ExecuteStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				return NGC.callf.InvokeStreamInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x04000D82 RID: 3458
			public static readonly NGC.IExecuter Singleton = new NGC.callf.Executer();
		}

		// Token: 0x0200086C RID: 2156
		// (Invoke) Token: 0x06004B88 RID: 19336
		public delegate void Call();

		// Token: 0x0200086D RID: 2157
		// (Invoke) Token: 0x06004B8C RID: 19340
		public delegate IEnumerator Routine();

		// Token: 0x0200086E RID: 2158
		// (Invoke) Token: 0x06004B90 RID: 19344
		public delegate void InfoCall(NetworkMessageInfo info);

		// Token: 0x0200086F RID: 2159
		// (Invoke) Token: 0x06004B94 RID: 19348
		public delegate IEnumerator InfoRoutine(NetworkMessageInfo info);

		// Token: 0x02000870 RID: 2160
		// (Invoke) Token: 0x06004B98 RID: 19352
		public delegate void StreamCall(BitStream stream);

		// Token: 0x02000871 RID: 2161
		// (Invoke) Token: 0x06004B9C RID: 19356
		public delegate IEnumerator StreamRoutine(BitStream stream);

		// Token: 0x02000872 RID: 2162
		// (Invoke) Token: 0x06004BA0 RID: 19360
		public delegate void StreamInfoCall(NetworkMessageInfo info, BitStream stream);

		// Token: 0x02000873 RID: 2163
		// (Invoke) Token: 0x06004BA4 RID: 19364
		public delegate IEnumerator StreamInfoRoutine(NetworkMessageInfo info, BitStream stream);
	}

	// Token: 0x020002CA RID: 714
	public static class callf<P0>
	{
		// Token: 0x06001964 RID: 6500 RVA: 0x00064430 File Offset: 0x00062630
		static callf()
		{
			BitStreamCodec.Add<NGC.callf<P0>.Block>(new BitStreamCodec.Deserializer(NGC.callf<P0>.Deserializer), new BitStreamCodec.Serializer(NGC.callf<P0>.Serializer));
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x00064450 File Offset: 0x00062650
		private static object Deserializer(BitStream stream, params object[] codecOptions)
		{
			NGC.callf<P0>.Block block;
			block.p0 = stream.Read<P0>(codecOptions);
			return block;
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x00064474 File Offset: 0x00062674
		private static void Serializer(BitStream stream, object value, params object[] codecOptions)
		{
			stream.Write<P0>(((NGC.callf<P0>.Block)value).p0, codecOptions);
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x00064498 File Offset: 0x00062698
		public static void InvokeCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0>.Call), instance, method, true);
			}
			((NGC.callf<P0>.Call)d)(p);
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x000644DC File Offset: 0x000626DC
		public static IEnumerator InvokeRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0>.Routine), instance, method, true);
			}
			return ((NGC.callf<P0>.Routine)d)(p);
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x00064520 File Offset: 0x00062720
		public static void InvokeInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0>.InfoCall), instance, method, true);
			}
			((NGC.callf<P0>.InfoCall)d)(p, info);
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x00064564 File Offset: 0x00062764
		public static IEnumerator InvokeInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0>.InfoRoutine), instance, method, true);
			}
			return ((NGC.callf<P0>.InfoRoutine)d)(p, info);
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x000645A8 File Offset: 0x000627A8
		public static void InvokeStreamCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0>.StreamCall), instance, method, true);
			}
			((NGC.callf<P0>.StreamCall)d)(p, stream);
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x000645EC File Offset: 0x000627EC
		public static IEnumerator InvokeStreamRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0>.StreamRoutine), instance, method, true);
			}
			return ((NGC.callf<P0>.StreamRoutine)d)(p, stream);
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x00064630 File Offset: 0x00062830
		public static void InvokeStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0>.StreamInfoCall), instance, method, true);
			}
			((NGC.callf<P0>.StreamInfoCall)d)(p, info, stream);
		}

		// Token: 0x0600196E RID: 6510 RVA: 0x00064678 File Offset: 0x00062878
		public static IEnumerator InvokeStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0>.StreamInfoRoutine), instance, method, true);
			}
			return ((NGC.callf<P0>.StreamInfoRoutine)d)(p, info, stream);
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x0600196F RID: 6511 RVA: 0x000646C0 File Offset: 0x000628C0
		public static NGC.IExecuter Exec
		{
			get
			{
				return NGC.callf<P0>.Executer.Singleton;
			}
		}

		// Token: 0x020002CB RID: 715
		public struct Block
		{
			// Token: 0x04000D83 RID: 3459
			public P0 p0;
		}

		// Token: 0x020002CC RID: 716
		private sealed class Executer : NGC.IExecuter
		{
			// Token: 0x06001972 RID: 6514 RVA: 0x000646DC File Offset: 0x000628DC
			public void ExecuteCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				NGC.callf<P0>.InvokeCall(stream, ref d, method, instance);
			}

			// Token: 0x06001973 RID: 6515 RVA: 0x000646E8 File Offset: 0x000628E8
			public IEnumerator ExecuteRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return NGC.callf<P0>.InvokeRoutine(stream, ref d, method, instance);
			}

			// Token: 0x06001974 RID: 6516 RVA: 0x000646F4 File Offset: 0x000628F4
			public void ExecuteInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				NGC.callf<P0>.InvokeInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x06001975 RID: 6517 RVA: 0x00064704 File Offset: 0x00062904
			public IEnumerator ExecuteInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				return NGC.callf<P0>.InvokeInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x06001976 RID: 6518 RVA: 0x00064714 File Offset: 0x00062914
			public void ExecuteStreamCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				NGC.callf<P0>.InvokeStreamCall(stream, ref d, method, instance);
			}

			// Token: 0x06001977 RID: 6519 RVA: 0x00064720 File Offset: 0x00062920
			public IEnumerator ExecuteStreamRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return NGC.callf<P0>.InvokeStreamRoutine(stream, ref d, method, instance);
			}

			// Token: 0x06001978 RID: 6520 RVA: 0x0006472C File Offset: 0x0006292C
			public void ExecuteStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				NGC.callf<P0>.InvokeStreamInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x06001979 RID: 6521 RVA: 0x0006473C File Offset: 0x0006293C
			public IEnumerator ExecuteStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				return NGC.callf<P0>.InvokeStreamInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x04000D84 RID: 3460
			public static readonly NGC.IExecuter Singleton = new NGC.callf<P0>.Executer();
		}

		// Token: 0x02000874 RID: 2164
		// (Invoke) Token: 0x06004BA8 RID: 19368
		public delegate void Call(P0 p0);

		// Token: 0x02000875 RID: 2165
		// (Invoke) Token: 0x06004BAC RID: 19372
		public delegate IEnumerator Routine(P0 p0);

		// Token: 0x02000876 RID: 2166
		// (Invoke) Token: 0x06004BB0 RID: 19376
		public delegate void InfoCall(P0 p0, NetworkMessageInfo info);

		// Token: 0x02000877 RID: 2167
		// (Invoke) Token: 0x06004BB4 RID: 19380
		public delegate IEnumerator InfoRoutine(P0 p0, NetworkMessageInfo info);

		// Token: 0x02000878 RID: 2168
		// (Invoke) Token: 0x06004BB8 RID: 19384
		public delegate void StreamCall(P0 p0, BitStream stream);

		// Token: 0x02000879 RID: 2169
		// (Invoke) Token: 0x06004BBC RID: 19388
		public delegate IEnumerator StreamRoutine(P0 p0, BitStream stream);

		// Token: 0x0200087A RID: 2170
		// (Invoke) Token: 0x06004BC0 RID: 19392
		public delegate void StreamInfoCall(P0 p0, NetworkMessageInfo info, BitStream stream);

		// Token: 0x0200087B RID: 2171
		// (Invoke) Token: 0x06004BC4 RID: 19396
		public delegate IEnumerator StreamInfoRoutine(P0 p0, NetworkMessageInfo info, BitStream stream);
	}

	// Token: 0x020002CD RID: 717
	public static class callf<P0, P1>
	{
		// Token: 0x0600197A RID: 6522 RVA: 0x0006474C File Offset: 0x0006294C
		static callf()
		{
			BitStreamCodec.Add<NGC.callf<P0, P1>.Block>(new BitStreamCodec.Deserializer(NGC.callf<P0, P1>.Deserializer), new BitStreamCodec.Serializer(NGC.callf<P0, P1>.Serializer));
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x0006476C File Offset: 0x0006296C
		private static object Deserializer(BitStream stream, params object[] codecOptions)
		{
			NGC.callf<P0, P1>.Block block;
			block.p0 = stream.Read<P0>(codecOptions);
			block.p1 = stream.Read<P1>(codecOptions);
			return block;
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x0006479C File Offset: 0x0006299C
		private static void Serializer(BitStream stream, object value, params object[] codecOptions)
		{
			NGC.callf<P0, P1>.Block block = (NGC.callf<P0, P1>.Block)value;
			stream.Write<P0>(block.p0, codecOptions);
			stream.Write<P1>(block.p1, codecOptions);
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x000647CC File Offset: 0x000629CC
		public static void InvokeCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1>.Call), instance, method, true);
			}
			((NGC.callf<P0, P1>.Call)d)(p, p2);
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x0006481C File Offset: 0x00062A1C
		public static IEnumerator InvokeRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1>.Routine), instance, method, true);
			}
			return ((NGC.callf<P0, P1>.Routine)d)(p, p2);
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x0006486C File Offset: 0x00062A6C
		public static void InvokeInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1>.InfoCall), instance, method, true);
			}
			((NGC.callf<P0, P1>.InfoCall)d)(p, p2, info);
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x000648C0 File Offset: 0x00062AC0
		public static IEnumerator InvokeInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1>.InfoRoutine), instance, method, true);
			}
			return ((NGC.callf<P0, P1>.InfoRoutine)d)(p, p2, info);
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x00064914 File Offset: 0x00062B14
		public static void InvokeStreamCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1>.StreamCall), instance, method, true);
			}
			((NGC.callf<P0, P1>.StreamCall)d)(p, p2, stream);
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x00064968 File Offset: 0x00062B68
		public static IEnumerator InvokeStreamRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1>.StreamRoutine), instance, method, true);
			}
			return ((NGC.callf<P0, P1>.StreamRoutine)d)(p, p2, stream);
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x000649BC File Offset: 0x00062BBC
		public static void InvokeStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1>.StreamInfoCall), instance, method, true);
			}
			((NGC.callf<P0, P1>.StreamInfoCall)d)(p, p2, info, stream);
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x00064A10 File Offset: 0x00062C10
		public static IEnumerator InvokeStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1>.StreamInfoRoutine), instance, method, true);
			}
			return ((NGC.callf<P0, P1>.StreamInfoRoutine)d)(p, p2, info, stream);
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06001985 RID: 6533 RVA: 0x00064A64 File Offset: 0x00062C64
		public static NGC.IExecuter Exec
		{
			get
			{
				return NGC.callf<P0, P1>.Executer.Singleton;
			}
		}

		// Token: 0x020002CE RID: 718
		public struct Block
		{
			// Token: 0x04000D85 RID: 3461
			public P0 p0;

			// Token: 0x04000D86 RID: 3462
			public P1 p1;
		}

		// Token: 0x020002CF RID: 719
		private sealed class Executer : NGC.IExecuter
		{
			// Token: 0x06001988 RID: 6536 RVA: 0x00064A80 File Offset: 0x00062C80
			public void ExecuteCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				NGC.callf<P0, P1>.InvokeCall(stream, ref d, method, instance);
			}

			// Token: 0x06001989 RID: 6537 RVA: 0x00064A8C File Offset: 0x00062C8C
			public IEnumerator ExecuteRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return NGC.callf<P0, P1>.InvokeRoutine(stream, ref d, method, instance);
			}

			// Token: 0x0600198A RID: 6538 RVA: 0x00064A98 File Offset: 0x00062C98
			public void ExecuteInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				NGC.callf<P0, P1>.InvokeInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x0600198B RID: 6539 RVA: 0x00064AA8 File Offset: 0x00062CA8
			public IEnumerator ExecuteInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				return NGC.callf<P0, P1>.InvokeInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x0600198C RID: 6540 RVA: 0x00064AB8 File Offset: 0x00062CB8
			public void ExecuteStreamCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				NGC.callf<P0, P1>.InvokeStreamCall(stream, ref d, method, instance);
			}

			// Token: 0x0600198D RID: 6541 RVA: 0x00064AC4 File Offset: 0x00062CC4
			public IEnumerator ExecuteStreamRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return NGC.callf<P0, P1>.InvokeStreamRoutine(stream, ref d, method, instance);
			}

			// Token: 0x0600198E RID: 6542 RVA: 0x00064AD0 File Offset: 0x00062CD0
			public void ExecuteStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				NGC.callf<P0, P1>.InvokeStreamInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x0600198F RID: 6543 RVA: 0x00064AE0 File Offset: 0x00062CE0
			public IEnumerator ExecuteStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				return NGC.callf<P0, P1>.InvokeStreamInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x04000D87 RID: 3463
			public static readonly NGC.IExecuter Singleton = new NGC.callf<P0, P1>.Executer();
		}

		// Token: 0x0200087C RID: 2172
		// (Invoke) Token: 0x06004BC8 RID: 19400
		public delegate void Call(P0 p0, P1 p1);

		// Token: 0x0200087D RID: 2173
		// (Invoke) Token: 0x06004BCC RID: 19404
		public delegate IEnumerator Routine(P0 p0, P1 p1);

		// Token: 0x0200087E RID: 2174
		// (Invoke) Token: 0x06004BD0 RID: 19408
		public delegate void InfoCall(P0 p0, P1 p1, NetworkMessageInfo info);

		// Token: 0x0200087F RID: 2175
		// (Invoke) Token: 0x06004BD4 RID: 19412
		public delegate IEnumerator InfoRoutine(P0 p0, P1 p1, NetworkMessageInfo info);

		// Token: 0x02000880 RID: 2176
		// (Invoke) Token: 0x06004BD8 RID: 19416
		public delegate void StreamCall(P0 p0, P1 p1, BitStream stream);

		// Token: 0x02000881 RID: 2177
		// (Invoke) Token: 0x06004BDC RID: 19420
		public delegate IEnumerator StreamRoutine(P0 p0, P1 p1, BitStream stream);

		// Token: 0x02000882 RID: 2178
		// (Invoke) Token: 0x06004BE0 RID: 19424
		public delegate void StreamInfoCall(P0 p0, P1 p1, NetworkMessageInfo info, BitStream stream);

		// Token: 0x02000883 RID: 2179
		// (Invoke) Token: 0x06004BE4 RID: 19428
		public delegate IEnumerator StreamInfoRoutine(P0 p0, P1 p1, NetworkMessageInfo info, BitStream stream);
	}

	// Token: 0x020002D0 RID: 720
	public static class callf<P0, P1, P2>
	{
		// Token: 0x06001990 RID: 6544 RVA: 0x00064AF0 File Offset: 0x00062CF0
		static callf()
		{
			BitStreamCodec.Add<NGC.callf<P0, P1, P2>.Block>(new BitStreamCodec.Deserializer(NGC.callf<P0, P1, P2>.Deserializer), new BitStreamCodec.Serializer(NGC.callf<P0, P1, P2>.Serializer));
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x00064B10 File Offset: 0x00062D10
		private static object Deserializer(BitStream stream, params object[] codecOptions)
		{
			NGC.callf<P0, P1, P2>.Block block;
			block.p0 = stream.Read<P0>(codecOptions);
			block.p1 = stream.Read<P1>(codecOptions);
			block.p2 = stream.Read<P2>(codecOptions);
			return block;
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x00064B50 File Offset: 0x00062D50
		private static void Serializer(BitStream stream, object value, params object[] codecOptions)
		{
			NGC.callf<P0, P1, P2>.Block block = (NGC.callf<P0, P1, P2>.Block)value;
			stream.Write<P0>(block.p0, codecOptions);
			stream.Write<P1>(block.p1, codecOptions);
			stream.Write<P2>(block.p2, codecOptions);
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x00064B90 File Offset: 0x00062D90
		public static void InvokeCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2>.Call), instance, method, true);
			}
			((NGC.callf<P0, P1, P2>.Call)d)(p, p2, p3);
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x00064BF0 File Offset: 0x00062DF0
		public static IEnumerator InvokeRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2>.Routine), instance, method, true);
			}
			return ((NGC.callf<P0, P1, P2>.Routine)d)(p, p2, p3);
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x00064C50 File Offset: 0x00062E50
		public static void InvokeInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2>.InfoCall), instance, method, true);
			}
			((NGC.callf<P0, P1, P2>.InfoCall)d)(p, p2, p3, info);
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x00064CB0 File Offset: 0x00062EB0
		public static IEnumerator InvokeInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2>.InfoRoutine), instance, method, true);
			}
			return ((NGC.callf<P0, P1, P2>.InfoRoutine)d)(p, p2, p3, info);
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x00064D10 File Offset: 0x00062F10
		public static void InvokeStreamCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2>.StreamCall), instance, method, true);
			}
			((NGC.callf<P0, P1, P2>.StreamCall)d)(p, p2, p3, stream);
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x00064D70 File Offset: 0x00062F70
		public static IEnumerator InvokeStreamRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2>.StreamRoutine), instance, method, true);
			}
			return ((NGC.callf<P0, P1, P2>.StreamRoutine)d)(p, p2, p3, stream);
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x00064DD0 File Offset: 0x00062FD0
		public static void InvokeStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2>.StreamInfoCall), instance, method, true);
			}
			((NGC.callf<P0, P1, P2>.StreamInfoCall)d)(p, p2, p3, info, stream);
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x00064E34 File Offset: 0x00063034
		public static IEnumerator InvokeStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2>.StreamInfoRoutine), instance, method, true);
			}
			return ((NGC.callf<P0, P1, P2>.StreamInfoRoutine)d)(p, p2, p3, info, stream);
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x0600199B RID: 6555 RVA: 0x00064E98 File Offset: 0x00063098
		public static NGC.IExecuter Exec
		{
			get
			{
				return NGC.callf<P0, P1, P2>.Executer.Singleton;
			}
		}

		// Token: 0x020002D1 RID: 721
		public struct Block
		{
			// Token: 0x04000D88 RID: 3464
			public P0 p0;

			// Token: 0x04000D89 RID: 3465
			public P1 p1;

			// Token: 0x04000D8A RID: 3466
			public P2 p2;
		}

		// Token: 0x020002D2 RID: 722
		private sealed class Executer : NGC.IExecuter
		{
			// Token: 0x0600199E RID: 6558 RVA: 0x00064EB4 File Offset: 0x000630B4
			public void ExecuteCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				NGC.callf<P0, P1, P2>.InvokeCall(stream, ref d, method, instance);
			}

			// Token: 0x0600199F RID: 6559 RVA: 0x00064EC0 File Offset: 0x000630C0
			public IEnumerator ExecuteRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return NGC.callf<P0, P1, P2>.InvokeRoutine(stream, ref d, method, instance);
			}

			// Token: 0x060019A0 RID: 6560 RVA: 0x00064ECC File Offset: 0x000630CC
			public void ExecuteInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				NGC.callf<P0, P1, P2>.InvokeInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x060019A1 RID: 6561 RVA: 0x00064EDC File Offset: 0x000630DC
			public IEnumerator ExecuteInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				return NGC.callf<P0, P1, P2>.InvokeInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x060019A2 RID: 6562 RVA: 0x00064EEC File Offset: 0x000630EC
			public void ExecuteStreamCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				NGC.callf<P0, P1, P2>.InvokeStreamCall(stream, ref d, method, instance);
			}

			// Token: 0x060019A3 RID: 6563 RVA: 0x00064EF8 File Offset: 0x000630F8
			public IEnumerator ExecuteStreamRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return NGC.callf<P0, P1, P2>.InvokeStreamRoutine(stream, ref d, method, instance);
			}

			// Token: 0x060019A4 RID: 6564 RVA: 0x00064F04 File Offset: 0x00063104
			public void ExecuteStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				NGC.callf<P0, P1, P2>.InvokeStreamInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x060019A5 RID: 6565 RVA: 0x00064F14 File Offset: 0x00063114
			public IEnumerator ExecuteStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				return NGC.callf<P0, P1, P2>.InvokeStreamInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x04000D8B RID: 3467
			public static readonly NGC.IExecuter Singleton = new NGC.callf<P0, P1, P2>.Executer();
		}

		// Token: 0x02000884 RID: 2180
		// (Invoke) Token: 0x06004BE8 RID: 19432
		public delegate void Call(P0 p0, P1 p1, P2 p2);

		// Token: 0x02000885 RID: 2181
		// (Invoke) Token: 0x06004BEC RID: 19436
		public delegate IEnumerator Routine(P0 p0, P1 p1, P2 p2);

		// Token: 0x02000886 RID: 2182
		// (Invoke) Token: 0x06004BF0 RID: 19440
		public delegate void InfoCall(P0 p0, P1 p1, P2 p2, NetworkMessageInfo info);

		// Token: 0x02000887 RID: 2183
		// (Invoke) Token: 0x06004BF4 RID: 19444
		public delegate IEnumerator InfoRoutine(P0 p0, P1 p1, P2 p2, NetworkMessageInfo info);

		// Token: 0x02000888 RID: 2184
		// (Invoke) Token: 0x06004BF8 RID: 19448
		public delegate void StreamCall(P0 p0, P1 p1, P2 p2, BitStream stream);

		// Token: 0x02000889 RID: 2185
		// (Invoke) Token: 0x06004BFC RID: 19452
		public delegate IEnumerator StreamRoutine(P0 p0, P1 p1, P2 p2, BitStream stream);

		// Token: 0x0200088A RID: 2186
		// (Invoke) Token: 0x06004C00 RID: 19456
		public delegate void StreamInfoCall(P0 p0, P1 p1, P2 p2, NetworkMessageInfo info, BitStream stream);

		// Token: 0x0200088B RID: 2187
		// (Invoke) Token: 0x06004C04 RID: 19460
		public delegate IEnumerator StreamInfoRoutine(P0 p0, P1 p1, P2 p2, NetworkMessageInfo info, BitStream stream);
	}

	// Token: 0x020002D3 RID: 723
	public static class callf<P0, P1, P2, P3>
	{
		// Token: 0x060019A6 RID: 6566 RVA: 0x00064F24 File Offset: 0x00063124
		static callf()
		{
			BitStreamCodec.Add<NGC.callf<P0, P1, P2, P3>.Block>(new BitStreamCodec.Deserializer(NGC.callf<P0, P1, P2, P3>.Deserializer), new BitStreamCodec.Serializer(NGC.callf<P0, P1, P2, P3>.Serializer));
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x00064F44 File Offset: 0x00063144
		private static object Deserializer(BitStream stream, params object[] codecOptions)
		{
			NGC.callf<P0, P1, P2, P3>.Block block;
			block.p0 = stream.Read<P0>(codecOptions);
			block.p1 = stream.Read<P1>(codecOptions);
			block.p2 = stream.Read<P2>(codecOptions);
			block.p3 = stream.Read<P3>(codecOptions);
			return block;
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x00064F90 File Offset: 0x00063190
		private static void Serializer(BitStream stream, object value, params object[] codecOptions)
		{
			NGC.callf<P0, P1, P2, P3>.Block block = (NGC.callf<P0, P1, P2, P3>.Block)value;
			stream.Write<P0>(block.p0, codecOptions);
			stream.Write<P1>(block.p1, codecOptions);
			stream.Write<P2>(block.p2, codecOptions);
			stream.Write<P3>(block.p3, codecOptions);
		}

		// Token: 0x060019A9 RID: 6569 RVA: 0x00064FDC File Offset: 0x000631DC
		public static void InvokeCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3>.Call), instance, method, true);
			}
			((NGC.callf<P0, P1, P2, P3>.Call)d)(p, p2, p3, p4);
		}

		// Token: 0x060019AA RID: 6570 RVA: 0x00065048 File Offset: 0x00063248
		public static IEnumerator InvokeRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3>.Routine), instance, method, true);
			}
			return ((NGC.callf<P0, P1, P2, P3>.Routine)d)(p, p2, p3, p4);
		}

		// Token: 0x060019AB RID: 6571 RVA: 0x000650B4 File Offset: 0x000632B4
		public static void InvokeInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3>.InfoCall), instance, method, true);
			}
			((NGC.callf<P0, P1, P2, P3>.InfoCall)d)(p, p2, p3, p4, info);
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x00065124 File Offset: 0x00063324
		public static IEnumerator InvokeInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3>.InfoRoutine), instance, method, true);
			}
			return ((NGC.callf<P0, P1, P2, P3>.InfoRoutine)d)(p, p2, p3, p4, info);
		}

		// Token: 0x060019AD RID: 6573 RVA: 0x00065194 File Offset: 0x00063394
		public static void InvokeStreamCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3>.StreamCall), instance, method, true);
			}
			((NGC.callf<P0, P1, P2, P3>.StreamCall)d)(p, p2, p3, p4, stream);
		}

		// Token: 0x060019AE RID: 6574 RVA: 0x00065204 File Offset: 0x00063404
		public static IEnumerator InvokeStreamRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3>.StreamRoutine), instance, method, true);
			}
			return ((NGC.callf<P0, P1, P2, P3>.StreamRoutine)d)(p, p2, p3, p4, stream);
		}

		// Token: 0x060019AF RID: 6575 RVA: 0x00065274 File Offset: 0x00063474
		public static void InvokeStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3>.StreamInfoCall), instance, method, true);
			}
			((NGC.callf<P0, P1, P2, P3>.StreamInfoCall)d)(p, p2, p3, p4, info, stream);
		}

		// Token: 0x060019B0 RID: 6576 RVA: 0x000652E4 File Offset: 0x000634E4
		public static IEnumerator InvokeStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3>.StreamInfoRoutine), instance, method, true);
			}
			return ((NGC.callf<P0, P1, P2, P3>.StreamInfoRoutine)d)(p, p2, p3, p4, info, stream);
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x060019B1 RID: 6577 RVA: 0x00065354 File Offset: 0x00063554
		public static NGC.IExecuter Exec
		{
			get
			{
				return NGC.callf<P0, P1, P2, P3>.Executer.Singleton;
			}
		}

		// Token: 0x020002D4 RID: 724
		public struct Block
		{
			// Token: 0x04000D8C RID: 3468
			public P0 p0;

			// Token: 0x04000D8D RID: 3469
			public P1 p1;

			// Token: 0x04000D8E RID: 3470
			public P2 p2;

			// Token: 0x04000D8F RID: 3471
			public P3 p3;
		}

		// Token: 0x020002D5 RID: 725
		private sealed class Executer : NGC.IExecuter
		{
			// Token: 0x060019B4 RID: 6580 RVA: 0x00065370 File Offset: 0x00063570
			public void ExecuteCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				NGC.callf<P0, P1, P2, P3>.InvokeCall(stream, ref d, method, instance);
			}

			// Token: 0x060019B5 RID: 6581 RVA: 0x0006537C File Offset: 0x0006357C
			public IEnumerator ExecuteRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return NGC.callf<P0, P1, P2, P3>.InvokeRoutine(stream, ref d, method, instance);
			}

			// Token: 0x060019B6 RID: 6582 RVA: 0x00065388 File Offset: 0x00063588
			public void ExecuteInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				NGC.callf<P0, P1, P2, P3>.InvokeInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x060019B7 RID: 6583 RVA: 0x00065398 File Offset: 0x00063598
			public IEnumerator ExecuteInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				return NGC.callf<P0, P1, P2, P3>.InvokeInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x060019B8 RID: 6584 RVA: 0x000653A8 File Offset: 0x000635A8
			public void ExecuteStreamCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				NGC.callf<P0, P1, P2, P3>.InvokeStreamCall(stream, ref d, method, instance);
			}

			// Token: 0x060019B9 RID: 6585 RVA: 0x000653B4 File Offset: 0x000635B4
			public IEnumerator ExecuteStreamRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return NGC.callf<P0, P1, P2, P3>.InvokeStreamRoutine(stream, ref d, method, instance);
			}

			// Token: 0x060019BA RID: 6586 RVA: 0x000653C0 File Offset: 0x000635C0
			public void ExecuteStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				NGC.callf<P0, P1, P2, P3>.InvokeStreamInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x060019BB RID: 6587 RVA: 0x000653D0 File Offset: 0x000635D0
			public IEnumerator ExecuteStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				return NGC.callf<P0, P1, P2, P3>.InvokeStreamInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x04000D90 RID: 3472
			public static readonly NGC.IExecuter Singleton = new NGC.callf<P0, P1, P2, P3>.Executer();
		}

		// Token: 0x0200088C RID: 2188
		// (Invoke) Token: 0x06004C08 RID: 19464
		public delegate void Call(P0 p0, P1 p1, P2 p2, P3 p3);

		// Token: 0x0200088D RID: 2189
		// (Invoke) Token: 0x06004C0C RID: 19468
		public delegate IEnumerator Routine(P0 p0, P1 p1, P2 p2, P3 p3);

		// Token: 0x0200088E RID: 2190
		// (Invoke) Token: 0x06004C10 RID: 19472
		public delegate void InfoCall(P0 p0, P1 p1, P2 p2, P3 p3, NetworkMessageInfo info);

		// Token: 0x0200088F RID: 2191
		// (Invoke) Token: 0x06004C14 RID: 19476
		public delegate IEnumerator InfoRoutine(P0 p0, P1 p1, P2 p2, P3 p3, NetworkMessageInfo info);

		// Token: 0x02000890 RID: 2192
		// (Invoke) Token: 0x06004C18 RID: 19480
		public delegate void StreamCall(P0 p0, P1 p1, P2 p2, P3 p3, BitStream stream);

		// Token: 0x02000891 RID: 2193
		// (Invoke) Token: 0x06004C1C RID: 19484
		public delegate IEnumerator StreamRoutine(P0 p0, P1 p1, P2 p2, P3 p3, BitStream stream);

		// Token: 0x02000892 RID: 2194
		// (Invoke) Token: 0x06004C20 RID: 19488
		public delegate void StreamInfoCall(P0 p0, P1 p1, P2 p2, P3 p3, NetworkMessageInfo info, BitStream stream);

		// Token: 0x02000893 RID: 2195
		// (Invoke) Token: 0x06004C24 RID: 19492
		public delegate IEnumerator StreamInfoRoutine(P0 p0, P1 p1, P2 p2, P3 p3, NetworkMessageInfo info, BitStream stream);
	}

	// Token: 0x020002D6 RID: 726
	public static class callf<P0, P1, P2, P3, P4>
	{
		// Token: 0x060019BC RID: 6588 RVA: 0x000653E0 File Offset: 0x000635E0
		static callf()
		{
			BitStreamCodec.Add<NGC.callf<P0, P1, P2, P3, P4>.Block>(new BitStreamCodec.Deserializer(NGC.callf<P0, P1, P2, P3, P4>.Deserializer), new BitStreamCodec.Serializer(NGC.callf<P0, P1, P2, P3, P4>.Serializer));
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x00065400 File Offset: 0x00063600
		private static object Deserializer(BitStream stream, params object[] codecOptions)
		{
			NGC.callf<P0, P1, P2, P3, P4>.Block block;
			block.p0 = stream.Read<P0>(codecOptions);
			block.p1 = stream.Read<P1>(codecOptions);
			block.p2 = stream.Read<P2>(codecOptions);
			block.p3 = stream.Read<P3>(codecOptions);
			block.p4 = stream.Read<P4>(codecOptions);
			return block;
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x0006545C File Offset: 0x0006365C
		private static void Serializer(BitStream stream, object value, params object[] codecOptions)
		{
			NGC.callf<P0, P1, P2, P3, P4>.Block block = (NGC.callf<P0, P1, P2, P3, P4>.Block)value;
			stream.Write<P0>(block.p0, codecOptions);
			stream.Write<P1>(block.p1, codecOptions);
			stream.Write<P2>(block.p2, codecOptions);
			stream.Write<P3>(block.p3, codecOptions);
			stream.Write<P4>(block.p4, codecOptions);
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x000654B8 File Offset: 0x000636B8
		public static void InvokeCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4>.Call), instance, method, true);
			}
			((NGC.callf<P0, P1, P2, P3, P4>.Call)d)(p, p2, p3, p4, p5);
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x00065534 File Offset: 0x00063734
		public static IEnumerator InvokeRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4>.Routine), instance, method, true);
			}
			return ((NGC.callf<P0, P1, P2, P3, P4>.Routine)d)(p, p2, p3, p4, p5);
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x000655B0 File Offset: 0x000637B0
		public static void InvokeInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4>.InfoCall), instance, method, true);
			}
			((NGC.callf<P0, P1, P2, P3, P4>.InfoCall)d)(p, p2, p3, p4, p5, info);
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x00065630 File Offset: 0x00063830
		public static IEnumerator InvokeInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4>.InfoRoutine), instance, method, true);
			}
			return ((NGC.callf<P0, P1, P2, P3, P4>.InfoRoutine)d)(p, p2, p3, p4, p5, info);
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x000656B0 File Offset: 0x000638B0
		public static void InvokeStreamCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4>.StreamCall), instance, method, true);
			}
			((NGC.callf<P0, P1, P2, P3, P4>.StreamCall)d)(p, p2, p3, p4, p5, stream);
		}

		// Token: 0x060019C4 RID: 6596 RVA: 0x00065730 File Offset: 0x00063930
		public static IEnumerator InvokeStreamRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4>.StreamRoutine), instance, method, true);
			}
			return ((NGC.callf<P0, P1, P2, P3, P4>.StreamRoutine)d)(p, p2, p3, p4, p5, stream);
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x000657B0 File Offset: 0x000639B0
		public static void InvokeStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4>.StreamInfoCall), instance, method, true);
			}
			((NGC.callf<P0, P1, P2, P3, P4>.StreamInfoCall)d)(p, p2, p3, p4, p5, info, stream);
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x00065830 File Offset: 0x00063A30
		public static IEnumerator InvokeStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4>.StreamInfoRoutine), instance, method, true);
			}
			return ((NGC.callf<P0, P1, P2, P3, P4>.StreamInfoRoutine)d)(p, p2, p3, p4, p5, info, stream);
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x060019C7 RID: 6599 RVA: 0x000658B0 File Offset: 0x00063AB0
		public static NGC.IExecuter Exec
		{
			get
			{
				return NGC.callf<P0, P1, P2, P3, P4>.Executer.Singleton;
			}
		}

		// Token: 0x020002D7 RID: 727
		public struct Block
		{
			// Token: 0x04000D91 RID: 3473
			public P0 p0;

			// Token: 0x04000D92 RID: 3474
			public P1 p1;

			// Token: 0x04000D93 RID: 3475
			public P2 p2;

			// Token: 0x04000D94 RID: 3476
			public P3 p3;

			// Token: 0x04000D95 RID: 3477
			public P4 p4;
		}

		// Token: 0x020002D8 RID: 728
		private sealed class Executer : NGC.IExecuter
		{
			// Token: 0x060019CA RID: 6602 RVA: 0x000658CC File Offset: 0x00063ACC
			public void ExecuteCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				NGC.callf<P0, P1, P2, P3, P4>.InvokeCall(stream, ref d, method, instance);
			}

			// Token: 0x060019CB RID: 6603 RVA: 0x000658D8 File Offset: 0x00063AD8
			public IEnumerator ExecuteRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return NGC.callf<P0, P1, P2, P3, P4>.InvokeRoutine(stream, ref d, method, instance);
			}

			// Token: 0x060019CC RID: 6604 RVA: 0x000658E4 File Offset: 0x00063AE4
			public void ExecuteInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				NGC.callf<P0, P1, P2, P3, P4>.InvokeInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x060019CD RID: 6605 RVA: 0x000658F4 File Offset: 0x00063AF4
			public IEnumerator ExecuteInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				return NGC.callf<P0, P1, P2, P3, P4>.InvokeInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x060019CE RID: 6606 RVA: 0x00065904 File Offset: 0x00063B04
			public void ExecuteStreamCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				NGC.callf<P0, P1, P2, P3, P4>.InvokeStreamCall(stream, ref d, method, instance);
			}

			// Token: 0x060019CF RID: 6607 RVA: 0x00065910 File Offset: 0x00063B10
			public IEnumerator ExecuteStreamRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return NGC.callf<P0, P1, P2, P3, P4>.InvokeStreamRoutine(stream, ref d, method, instance);
			}

			// Token: 0x060019D0 RID: 6608 RVA: 0x0006591C File Offset: 0x00063B1C
			public void ExecuteStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				NGC.callf<P0, P1, P2, P3, P4>.InvokeStreamInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x060019D1 RID: 6609 RVA: 0x0006592C File Offset: 0x00063B2C
			public IEnumerator ExecuteStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				return NGC.callf<P0, P1, P2, P3, P4>.InvokeStreamInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x04000D96 RID: 3478
			public static readonly NGC.IExecuter Singleton = new NGC.callf<P0, P1, P2, P3, P4>.Executer();
		}

		// Token: 0x02000894 RID: 2196
		// (Invoke) Token: 0x06004C28 RID: 19496
		public delegate void Call(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4);

		// Token: 0x02000895 RID: 2197
		// (Invoke) Token: 0x06004C2C RID: 19500
		public delegate IEnumerator Routine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4);

		// Token: 0x02000896 RID: 2198
		// (Invoke) Token: 0x06004C30 RID: 19504
		public delegate void InfoCall(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, NetworkMessageInfo info);

		// Token: 0x02000897 RID: 2199
		// (Invoke) Token: 0x06004C34 RID: 19508
		public delegate IEnumerator InfoRoutine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, NetworkMessageInfo info);

		// Token: 0x02000898 RID: 2200
		// (Invoke) Token: 0x06004C38 RID: 19512
		public delegate void StreamCall(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, BitStream stream);

		// Token: 0x02000899 RID: 2201
		// (Invoke) Token: 0x06004C3C RID: 19516
		public delegate IEnumerator StreamRoutine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, BitStream stream);

		// Token: 0x0200089A RID: 2202
		// (Invoke) Token: 0x06004C40 RID: 19520
		public delegate void StreamInfoCall(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, NetworkMessageInfo info, BitStream stream);

		// Token: 0x0200089B RID: 2203
		// (Invoke) Token: 0x06004C44 RID: 19524
		public delegate IEnumerator StreamInfoRoutine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, NetworkMessageInfo info, BitStream stream);
	}

	// Token: 0x020002D9 RID: 729
	public static class callf<P0, P1, P2, P3, P4, P5>
	{
		// Token: 0x060019D2 RID: 6610 RVA: 0x0006593C File Offset: 0x00063B3C
		static callf()
		{
			BitStreamCodec.Add<NGC.callf<P0, P1, P2, P3, P4, P5>.Block>(new BitStreamCodec.Deserializer(NGC.callf<P0, P1, P2, P3, P4, P5>.Deserializer), new BitStreamCodec.Serializer(NGC.callf<P0, P1, P2, P3, P4, P5>.Serializer));
		}

		// Token: 0x060019D3 RID: 6611 RVA: 0x0006595C File Offset: 0x00063B5C
		private static object Deserializer(BitStream stream, params object[] codecOptions)
		{
			NGC.callf<P0, P1, P2, P3, P4, P5>.Block block;
			block.p0 = stream.Read<P0>(codecOptions);
			block.p1 = stream.Read<P1>(codecOptions);
			block.p2 = stream.Read<P2>(codecOptions);
			block.p3 = stream.Read<P3>(codecOptions);
			block.p4 = stream.Read<P4>(codecOptions);
			block.p5 = stream.Read<P5>(codecOptions);
			return block;
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x000659C4 File Offset: 0x00063BC4
		private static void Serializer(BitStream stream, object value, params object[] codecOptions)
		{
			NGC.callf<P0, P1, P2, P3, P4, P5>.Block block = (NGC.callf<P0, P1, P2, P3, P4, P5>.Block)value;
			stream.Write<P0>(block.p0, codecOptions);
			stream.Write<P1>(block.p1, codecOptions);
			stream.Write<P2>(block.p2, codecOptions);
			stream.Write<P3>(block.p3, codecOptions);
			stream.Write<P4>(block.p4, codecOptions);
			stream.Write<P5>(block.p5, codecOptions);
		}

		// Token: 0x060019D5 RID: 6613 RVA: 0x00065A2C File Offset: 0x00063C2C
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5>.Call), instance, method, true);
			}
			((NGC.callf<P0, P1, P2, P3, P4, P5>.Call)d)(p, p2, p3, p4, p5, p6);
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x00065AB8 File Offset: 0x00063CB8
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5>.Routine), instance, method, true);
			}
			return ((NGC.callf<P0, P1, P2, P3, P4, P5>.Routine)d)(p, p2, p3, p4, p5, p6);
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x00065B44 File Offset: 0x00063D44
		public static void InvokeInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5>.InfoCall), instance, method, true);
			}
			((NGC.callf<P0, P1, P2, P3, P4, P5>.InfoCall)d)(p, p2, p3, p4, p5, p6, info);
		}

		// Token: 0x060019D8 RID: 6616 RVA: 0x00065BD4 File Offset: 0x00063DD4
		public static IEnumerator InvokeInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5>.InfoRoutine), instance, method, true);
			}
			return ((NGC.callf<P0, P1, P2, P3, P4, P5>.InfoRoutine)d)(p, p2, p3, p4, p5, p6, info);
		}

		// Token: 0x060019D9 RID: 6617 RVA: 0x00065C64 File Offset: 0x00063E64
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5>.StreamCall), instance, method, true);
			}
			((NGC.callf<P0, P1, P2, P3, P4, P5>.StreamCall)d)(p, p2, p3, p4, p5, p6, stream);
		}

		// Token: 0x060019DA RID: 6618 RVA: 0x00065CF4 File Offset: 0x00063EF4
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5>.StreamRoutine), instance, method, true);
			}
			return ((NGC.callf<P0, P1, P2, P3, P4, P5>.StreamRoutine)d)(p, p2, p3, p4, p5, p6, stream);
		}

		// Token: 0x060019DB RID: 6619 RVA: 0x00065D84 File Offset: 0x00063F84
		public static void InvokeStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5>.StreamInfoCall), instance, method, true);
			}
			((NGC.callf<P0, P1, P2, P3, P4, P5>.StreamInfoCall)d)(p, p2, p3, p4, p5, p6, info, stream);
		}

		// Token: 0x060019DC RID: 6620 RVA: 0x00065E14 File Offset: 0x00064014
		public static IEnumerator InvokeStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
		{
			P0 p = stream.Read<P0>(new object[0]);
			P1 p2 = stream.Read<P1>(new object[0]);
			P2 p3 = stream.Read<P2>(new object[0]);
			P3 p4 = stream.Read<P3>(new object[0]);
			P4 p5 = stream.Read<P4>(new object[0]);
			P5 p6 = stream.Read<P5>(new object[0]);
			if (d == null)
			{
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5>.StreamInfoRoutine), instance, method, true);
			}
			return ((NGC.callf<P0, P1, P2, P3, P4, P5>.StreamInfoRoutine)d)(p, p2, p3, p4, p5, p6, info, stream);
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x060019DD RID: 6621 RVA: 0x00065EA4 File Offset: 0x000640A4
		public static NGC.IExecuter Exec
		{
			get
			{
				return NGC.callf<P0, P1, P2, P3, P4, P5>.Executer.Singleton;
			}
		}

		// Token: 0x020002DA RID: 730
		public struct Block
		{
			// Token: 0x04000D97 RID: 3479
			public P0 p0;

			// Token: 0x04000D98 RID: 3480
			public P1 p1;

			// Token: 0x04000D99 RID: 3481
			public P2 p2;

			// Token: 0x04000D9A RID: 3482
			public P3 p3;

			// Token: 0x04000D9B RID: 3483
			public P4 p4;

			// Token: 0x04000D9C RID: 3484
			public P5 p5;
		}

		// Token: 0x020002DB RID: 731
		private sealed class Executer : NGC.IExecuter
		{
			// Token: 0x060019E0 RID: 6624 RVA: 0x00065EC0 File Offset: 0x000640C0
			public void ExecuteCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				NGC.callf<P0, P1, P2, P3, P4, P5>.InvokeCall(stream, ref d, method, instance);
			}

			// Token: 0x060019E1 RID: 6625 RVA: 0x00065ECC File Offset: 0x000640CC
			public IEnumerator ExecuteRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return NGC.callf<P0, P1, P2, P3, P4, P5>.InvokeRoutine(stream, ref d, method, instance);
			}

			// Token: 0x060019E2 RID: 6626 RVA: 0x00065ED8 File Offset: 0x000640D8
			public void ExecuteInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				NGC.callf<P0, P1, P2, P3, P4, P5>.InvokeInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x060019E3 RID: 6627 RVA: 0x00065EE8 File Offset: 0x000640E8
			public IEnumerator ExecuteInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				return NGC.callf<P0, P1, P2, P3, P4, P5>.InvokeInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x060019E4 RID: 6628 RVA: 0x00065EF8 File Offset: 0x000640F8
			public void ExecuteStreamCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				NGC.callf<P0, P1, P2, P3, P4, P5>.InvokeStreamCall(stream, ref d, method, instance);
			}

			// Token: 0x060019E5 RID: 6629 RVA: 0x00065F04 File Offset: 0x00064104
			public IEnumerator ExecuteStreamRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return NGC.callf<P0, P1, P2, P3, P4, P5>.InvokeStreamRoutine(stream, ref d, method, instance);
			}

			// Token: 0x060019E6 RID: 6630 RVA: 0x00065F10 File Offset: 0x00064110
			public void ExecuteStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				NGC.callf<P0, P1, P2, P3, P4, P5>.InvokeStreamInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x060019E7 RID: 6631 RVA: 0x00065F20 File Offset: 0x00064120
			public IEnumerator ExecuteStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				return NGC.callf<P0, P1, P2, P3, P4, P5>.InvokeStreamInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x04000D9D RID: 3485
			public static readonly NGC.IExecuter Singleton = new NGC.callf<P0, P1, P2, P3, P4, P5>.Executer();
		}

		// Token: 0x0200089C RID: 2204
		// (Invoke) Token: 0x06004C48 RID: 19528
		public delegate void Call(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5);

		// Token: 0x0200089D RID: 2205
		// (Invoke) Token: 0x06004C4C RID: 19532
		public delegate IEnumerator Routine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5);

		// Token: 0x0200089E RID: 2206
		// (Invoke) Token: 0x06004C50 RID: 19536
		public delegate void InfoCall(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, NetworkMessageInfo info);

		// Token: 0x0200089F RID: 2207
		// (Invoke) Token: 0x06004C54 RID: 19540
		public delegate IEnumerator InfoRoutine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, NetworkMessageInfo info);

		// Token: 0x020008A0 RID: 2208
		// (Invoke) Token: 0x06004C58 RID: 19544
		public delegate void StreamCall(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, BitStream stream);

		// Token: 0x020008A1 RID: 2209
		// (Invoke) Token: 0x06004C5C RID: 19548
		public delegate IEnumerator StreamRoutine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, BitStream stream);

		// Token: 0x020008A2 RID: 2210
		// (Invoke) Token: 0x06004C60 RID: 19552
		public delegate void StreamInfoCall(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, NetworkMessageInfo info, BitStream stream);

		// Token: 0x020008A3 RID: 2211
		// (Invoke) Token: 0x06004C64 RID: 19556
		public delegate IEnumerator StreamInfoRoutine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, NetworkMessageInfo info, BitStream stream);
	}

	// Token: 0x020002DC RID: 732
	public static class callf<P0, P1, P2, P3, P4, P5, P6>
	{
		// Token: 0x060019E8 RID: 6632 RVA: 0x00065F30 File Offset: 0x00064130
		static callf()
		{
			BitStreamCodec.Add<NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Block>(new BitStreamCodec.Deserializer(NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Deserializer), new BitStreamCodec.Serializer(NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Serializer));
		}

		// Token: 0x060019E9 RID: 6633 RVA: 0x00065F50 File Offset: 0x00064150
		private static object Deserializer(BitStream stream, params object[] codecOptions)
		{
			NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Block block;
			block.p0 = stream.Read<P0>(codecOptions);
			block.p1 = stream.Read<P1>(codecOptions);
			block.p2 = stream.Read<P2>(codecOptions);
			block.p3 = stream.Read<P3>(codecOptions);
			block.p4 = stream.Read<P4>(codecOptions);
			block.p5 = stream.Read<P5>(codecOptions);
			block.p6 = stream.Read<P6>(codecOptions);
			return block;
		}

		// Token: 0x060019EA RID: 6634 RVA: 0x00065FC8 File Offset: 0x000641C8
		private static void Serializer(BitStream stream, object value, params object[] codecOptions)
		{
			NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Block block = (NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Block)value;
			stream.Write<P0>(block.p0, codecOptions);
			stream.Write<P1>(block.p1, codecOptions);
			stream.Write<P2>(block.p2, codecOptions);
			stream.Write<P3>(block.p3, codecOptions);
			stream.Write<P4>(block.p4, codecOptions);
			stream.Write<P5>(block.p5, codecOptions);
			stream.Write<P6>(block.p6, codecOptions);
		}

		// Token: 0x060019EB RID: 6635 RVA: 0x00066040 File Offset: 0x00064240
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Call), instance, method, true);
			}
			((NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Call)d)(p, p2, p3, p4, p5, p6, p7);
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x000660DC File Offset: 0x000642DC
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Routine), instance, method, true);
			}
			return ((NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Routine)d)(p, p2, p3, p4, p5, p6, p7);
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x00066178 File Offset: 0x00064378
		public static void InvokeInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5, P6>.InfoCall), instance, method, true);
			}
			((NGC.callf<P0, P1, P2, P3, P4, P5, P6>.InfoCall)d)(p, p2, p3, p4, p5, p6, p7, info);
		}

		// Token: 0x060019EE RID: 6638 RVA: 0x00066218 File Offset: 0x00064418
		public static IEnumerator InvokeInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5, P6>.InfoRoutine), instance, method, true);
			}
			return ((NGC.callf<P0, P1, P2, P3, P4, P5, P6>.InfoRoutine)d)(p, p2, p3, p4, p5, p6, p7, info);
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x000662B8 File Offset: 0x000644B8
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5, P6>.StreamCall), instance, method, true);
			}
			((NGC.callf<P0, P1, P2, P3, P4, P5, P6>.StreamCall)d)(p, p2, p3, p4, p5, p6, p7, stream);
		}

		// Token: 0x060019F0 RID: 6640 RVA: 0x00066358 File Offset: 0x00064558
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5, P6>.StreamRoutine), instance, method, true);
			}
			return ((NGC.callf<P0, P1, P2, P3, P4, P5, P6>.StreamRoutine)d)(p, p2, p3, p4, p5, p6, p7, stream);
		}

		// Token: 0x060019F1 RID: 6641 RVA: 0x000663F8 File Offset: 0x000645F8
		public static void InvokeStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5, P6>.StreamInfoCall), instance, method, true);
			}
			((NGC.callf<P0, P1, P2, P3, P4, P5, P6>.StreamInfoCall)d)(p, p2, p3, p4, p5, p6, p7, info, stream);
		}

		// Token: 0x060019F2 RID: 6642 RVA: 0x00066498 File Offset: 0x00064698
		public static IEnumerator InvokeStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5, P6>.StreamInfoRoutine), instance, method, true);
			}
			return ((NGC.callf<P0, P1, P2, P3, P4, P5, P6>.StreamInfoRoutine)d)(p, p2, p3, p4, p5, p6, p7, info, stream);
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x060019F3 RID: 6643 RVA: 0x00066538 File Offset: 0x00064738
		public static NGC.IExecuter Exec
		{
			get
			{
				return NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Executer.Singleton;
			}
		}

		// Token: 0x020002DD RID: 733
		public struct Block
		{
			// Token: 0x04000D9E RID: 3486
			public P0 p0;

			// Token: 0x04000D9F RID: 3487
			public P1 p1;

			// Token: 0x04000DA0 RID: 3488
			public P2 p2;

			// Token: 0x04000DA1 RID: 3489
			public P3 p3;

			// Token: 0x04000DA2 RID: 3490
			public P4 p4;

			// Token: 0x04000DA3 RID: 3491
			public P5 p5;

			// Token: 0x04000DA4 RID: 3492
			public P6 p6;
		}

		// Token: 0x020002DE RID: 734
		private sealed class Executer : NGC.IExecuter
		{
			// Token: 0x060019F6 RID: 6646 RVA: 0x00066554 File Offset: 0x00064754
			public void ExecuteCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				NGC.callf<P0, P1, P2, P3, P4, P5, P6>.InvokeCall(stream, ref d, method, instance);
			}

			// Token: 0x060019F7 RID: 6647 RVA: 0x00066560 File Offset: 0x00064760
			public IEnumerator ExecuteRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return NGC.callf<P0, P1, P2, P3, P4, P5, P6>.InvokeRoutine(stream, ref d, method, instance);
			}

			// Token: 0x060019F8 RID: 6648 RVA: 0x0006656C File Offset: 0x0006476C
			public void ExecuteInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				NGC.callf<P0, P1, P2, P3, P4, P5, P6>.InvokeInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x060019F9 RID: 6649 RVA: 0x0006657C File Offset: 0x0006477C
			public IEnumerator ExecuteInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				return NGC.callf<P0, P1, P2, P3, P4, P5, P6>.InvokeInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x060019FA RID: 6650 RVA: 0x0006658C File Offset: 0x0006478C
			public void ExecuteStreamCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				NGC.callf<P0, P1, P2, P3, P4, P5, P6>.InvokeStreamCall(stream, ref d, method, instance);
			}

			// Token: 0x060019FB RID: 6651 RVA: 0x00066598 File Offset: 0x00064798
			public IEnumerator ExecuteStreamRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return NGC.callf<P0, P1, P2, P3, P4, P5, P6>.InvokeStreamRoutine(stream, ref d, method, instance);
			}

			// Token: 0x060019FC RID: 6652 RVA: 0x000665A4 File Offset: 0x000647A4
			public void ExecuteStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				NGC.callf<P0, P1, P2, P3, P4, P5, P6>.InvokeStreamInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x060019FD RID: 6653 RVA: 0x000665B4 File Offset: 0x000647B4
			public IEnumerator ExecuteStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				return NGC.callf<P0, P1, P2, P3, P4, P5, P6>.InvokeStreamInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x04000DA5 RID: 3493
			public static readonly NGC.IExecuter Singleton = new NGC.callf<P0, P1, P2, P3, P4, P5, P6>.Executer();
		}

		// Token: 0x020008A4 RID: 2212
		// (Invoke) Token: 0x06004C68 RID: 19560
		public delegate void Call(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6);

		// Token: 0x020008A5 RID: 2213
		// (Invoke) Token: 0x06004C6C RID: 19564
		public delegate IEnumerator Routine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6);

		// Token: 0x020008A6 RID: 2214
		// (Invoke) Token: 0x06004C70 RID: 19568
		public delegate void InfoCall(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, NetworkMessageInfo info);

		// Token: 0x020008A7 RID: 2215
		// (Invoke) Token: 0x06004C74 RID: 19572
		public delegate IEnumerator InfoRoutine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, NetworkMessageInfo info);

		// Token: 0x020008A8 RID: 2216
		// (Invoke) Token: 0x06004C78 RID: 19576
		public delegate void StreamCall(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, BitStream stream);

		// Token: 0x020008A9 RID: 2217
		// (Invoke) Token: 0x06004C7C RID: 19580
		public delegate IEnumerator StreamRoutine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, BitStream stream);

		// Token: 0x020008AA RID: 2218
		// (Invoke) Token: 0x06004C80 RID: 19584
		public delegate void StreamInfoCall(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, NetworkMessageInfo info, BitStream stream);

		// Token: 0x020008AB RID: 2219
		// (Invoke) Token: 0x06004C84 RID: 19588
		public delegate IEnumerator StreamInfoRoutine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, NetworkMessageInfo info, BitStream stream);
	}

	// Token: 0x020002DF RID: 735
	public static class callf<P0, P1, P2, P3, P4, P5, P6, P7>
	{
		// Token: 0x060019FE RID: 6654 RVA: 0x000665C4 File Offset: 0x000647C4
		static callf()
		{
			BitStreamCodec.Add<NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Block>(new BitStreamCodec.Deserializer(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Deserializer), new BitStreamCodec.Serializer(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Serializer));
		}

		// Token: 0x060019FF RID: 6655 RVA: 0x000665E4 File Offset: 0x000647E4
		private static object Deserializer(BitStream stream, params object[] codecOptions)
		{
			NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Block block;
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

		// Token: 0x06001A00 RID: 6656 RVA: 0x00066668 File Offset: 0x00064868
		private static void Serializer(BitStream stream, object value, params object[] codecOptions)
		{
			NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Block block = (NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Block)value;
			stream.Write<P0>(block.p0, codecOptions);
			stream.Write<P1>(block.p1, codecOptions);
			stream.Write<P2>(block.p2, codecOptions);
			stream.Write<P3>(block.p3, codecOptions);
			stream.Write<P4>(block.p4, codecOptions);
			stream.Write<P5>(block.p5, codecOptions);
			stream.Write<P6>(block.p6, codecOptions);
			stream.Write<P7>(block.p7, codecOptions);
		}

		// Token: 0x06001A01 RID: 6657 RVA: 0x000666EC File Offset: 0x000648EC
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Call), instance, method, true);
			}
			((NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Call)d)(p, p2, p3, p4, p5, p6, p7, p8);
		}

		// Token: 0x06001A02 RID: 6658 RVA: 0x00066798 File Offset: 0x00064998
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Routine), instance, method, true);
			}
			return ((NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Routine)d)(p, p2, p3, p4, p5, p6, p7, p8);
		}

		// Token: 0x06001A03 RID: 6659 RVA: 0x00066844 File Offset: 0x00064A44
		public static void InvokeInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.InfoCall), instance, method, true);
			}
			((NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.InfoCall)d)(p, p2, p3, p4, p5, p6, p7, p8, info);
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x000668F4 File Offset: 0x00064AF4
		public static IEnumerator InvokeInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.InfoRoutine), instance, method, true);
			}
			return ((NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.InfoRoutine)d)(p, p2, p3, p4, p5, p6, p7, p8, info);
		}

		// Token: 0x06001A05 RID: 6661 RVA: 0x000669A4 File Offset: 0x00064BA4
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.StreamCall), instance, method, true);
			}
			((NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.StreamCall)d)(p, p2, p3, p4, p5, p6, p7, p8, stream);
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x00066A54 File Offset: 0x00064C54
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.StreamRoutine), instance, method, true);
			}
			return ((NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.StreamRoutine)d)(p, p2, p3, p4, p5, p6, p7, p8, stream);
		}

		// Token: 0x06001A07 RID: 6663 RVA: 0x00066B04 File Offset: 0x00064D04
		public static void InvokeStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.StreamInfoCall), instance, method, true);
			}
			((NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.StreamInfoCall)d)(p, p2, p3, p4, p5, p6, p7, p8, info, stream);
		}

		// Token: 0x06001A08 RID: 6664 RVA: 0x00066BB4 File Offset: 0x00064DB4
		public static IEnumerator InvokeStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.StreamInfoRoutine), instance, method, true);
			}
			return ((NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.StreamInfoRoutine)d)(p, p2, p3, p4, p5, p6, p7, p8, info, stream);
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06001A09 RID: 6665 RVA: 0x00066C64 File Offset: 0x00064E64
		public static NGC.IExecuter Exec
		{
			get
			{
				return NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Executer.Singleton;
			}
		}

		// Token: 0x020002E0 RID: 736
		public struct Block
		{
			// Token: 0x04000DA6 RID: 3494
			public P0 p0;

			// Token: 0x04000DA7 RID: 3495
			public P1 p1;

			// Token: 0x04000DA8 RID: 3496
			public P2 p2;

			// Token: 0x04000DA9 RID: 3497
			public P3 p3;

			// Token: 0x04000DAA RID: 3498
			public P4 p4;

			// Token: 0x04000DAB RID: 3499
			public P5 p5;

			// Token: 0x04000DAC RID: 3500
			public P6 p6;

			// Token: 0x04000DAD RID: 3501
			public P7 p7;
		}

		// Token: 0x020002E1 RID: 737
		private sealed class Executer : NGC.IExecuter
		{
			// Token: 0x06001A0C RID: 6668 RVA: 0x00066C80 File Offset: 0x00064E80
			public void ExecuteCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.InvokeCall(stream, ref d, method, instance);
			}

			// Token: 0x06001A0D RID: 6669 RVA: 0x00066C8C File Offset: 0x00064E8C
			public IEnumerator ExecuteRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.InvokeRoutine(stream, ref d, method, instance);
			}

			// Token: 0x06001A0E RID: 6670 RVA: 0x00066C98 File Offset: 0x00064E98
			public void ExecuteInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.InvokeInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x06001A0F RID: 6671 RVA: 0x00066CA8 File Offset: 0x00064EA8
			public IEnumerator ExecuteInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				return NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.InvokeInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x06001A10 RID: 6672 RVA: 0x00066CB8 File Offset: 0x00064EB8
			public void ExecuteStreamCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.InvokeStreamCall(stream, ref d, method, instance);
			}

			// Token: 0x06001A11 RID: 6673 RVA: 0x00066CC4 File Offset: 0x00064EC4
			public IEnumerator ExecuteStreamRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.InvokeStreamRoutine(stream, ref d, method, instance);
			}

			// Token: 0x06001A12 RID: 6674 RVA: 0x00066CD0 File Offset: 0x00064ED0
			public void ExecuteStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.InvokeStreamInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x06001A13 RID: 6675 RVA: 0x00066CE0 File Offset: 0x00064EE0
			public IEnumerator ExecuteStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				return NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.InvokeStreamInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x04000DAE RID: 3502
			public static readonly NGC.IExecuter Singleton = new NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7>.Executer();
		}

		// Token: 0x020008AC RID: 2220
		// (Invoke) Token: 0x06004C88 RID: 19592
		public delegate void Call(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7);

		// Token: 0x020008AD RID: 2221
		// (Invoke) Token: 0x06004C8C RID: 19596
		public delegate IEnumerator Routine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7);

		// Token: 0x020008AE RID: 2222
		// (Invoke) Token: 0x06004C90 RID: 19600
		public delegate void InfoCall(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, NetworkMessageInfo info);

		// Token: 0x020008AF RID: 2223
		// (Invoke) Token: 0x06004C94 RID: 19604
		public delegate IEnumerator InfoRoutine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, NetworkMessageInfo info);

		// Token: 0x020008B0 RID: 2224
		// (Invoke) Token: 0x06004C98 RID: 19608
		public delegate void StreamCall(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, BitStream stream);

		// Token: 0x020008B1 RID: 2225
		// (Invoke) Token: 0x06004C9C RID: 19612
		public delegate IEnumerator StreamRoutine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, BitStream stream);

		// Token: 0x020008B2 RID: 2226
		// (Invoke) Token: 0x06004CA0 RID: 19616
		public delegate void StreamInfoCall(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, NetworkMessageInfo info, BitStream stream);

		// Token: 0x020008B3 RID: 2227
		// (Invoke) Token: 0x06004CA4 RID: 19620
		public delegate IEnumerator StreamInfoRoutine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, NetworkMessageInfo info, BitStream stream);
	}

	// Token: 0x020002E2 RID: 738
	public static class callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>
	{
		// Token: 0x06001A14 RID: 6676 RVA: 0x00066CF0 File Offset: 0x00064EF0
		static callf()
		{
			BitStreamCodec.Add<NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Block>(new BitStreamCodec.Deserializer(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Deserializer), new BitStreamCodec.Serializer(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Serializer));
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x00066D10 File Offset: 0x00064F10
		private static object Deserializer(BitStream stream, params object[] codecOptions)
		{
			NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Block block;
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

		// Token: 0x06001A16 RID: 6678 RVA: 0x00066DA4 File Offset: 0x00064FA4
		private static void Serializer(BitStream stream, object value, params object[] codecOptions)
		{
			NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Block block = (NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Block)value;
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

		// Token: 0x06001A17 RID: 6679 RVA: 0x00066E38 File Offset: 0x00065038
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Call), instance, method, true);
			}
			((NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Call)d)(p, p2, p3, p4, p5, p6, p7, p8, p9);
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x00066EF4 File Offset: 0x000650F4
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Routine), instance, method, true);
			}
			return ((NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Routine)d)(p, p2, p3, p4, p5, p6, p7, p8, p9);
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x00066FB0 File Offset: 0x000651B0
		public static void InvokeInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.InfoCall), instance, method, true);
			}
			((NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.InfoCall)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, info);
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x00067070 File Offset: 0x00065270
		public static IEnumerator InvokeInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.InfoRoutine), instance, method, true);
			}
			return ((NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.InfoRoutine)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, info);
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x00067130 File Offset: 0x00065330
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.StreamCall), instance, method, true);
			}
			((NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.StreamCall)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, stream);
		}

		// Token: 0x06001A1C RID: 6684 RVA: 0x000671F0 File Offset: 0x000653F0
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.StreamRoutine), instance, method, true);
			}
			return ((NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.StreamRoutine)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, stream);
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x000672B0 File Offset: 0x000654B0
		public static void InvokeStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.StreamInfoCall), instance, method, true);
			}
			((NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.StreamInfoCall)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, info, stream);
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x00067370 File Offset: 0x00065570
		public static IEnumerator InvokeStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.StreamInfoRoutine), instance, method, true);
			}
			return ((NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.StreamInfoRoutine)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, info, stream);
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06001A1F RID: 6687 RVA: 0x00067430 File Offset: 0x00065630
		public static NGC.IExecuter Exec
		{
			get
			{
				return NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Executer.Singleton;
			}
		}

		// Token: 0x020002E3 RID: 739
		public struct Block
		{
			// Token: 0x04000DAF RID: 3503
			public P0 p0;

			// Token: 0x04000DB0 RID: 3504
			public P1 p1;

			// Token: 0x04000DB1 RID: 3505
			public P2 p2;

			// Token: 0x04000DB2 RID: 3506
			public P3 p3;

			// Token: 0x04000DB3 RID: 3507
			public P4 p4;

			// Token: 0x04000DB4 RID: 3508
			public P5 p5;

			// Token: 0x04000DB5 RID: 3509
			public P6 p6;

			// Token: 0x04000DB6 RID: 3510
			public P7 p7;

			// Token: 0x04000DB7 RID: 3511
			public P8 p8;
		}

		// Token: 0x020002E4 RID: 740
		private sealed class Executer : NGC.IExecuter
		{
			// Token: 0x06001A22 RID: 6690 RVA: 0x0006744C File Offset: 0x0006564C
			public void ExecuteCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.InvokeCall(stream, ref d, method, instance);
			}

			// Token: 0x06001A23 RID: 6691 RVA: 0x00067458 File Offset: 0x00065658
			public IEnumerator ExecuteRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.InvokeRoutine(stream, ref d, method, instance);
			}

			// Token: 0x06001A24 RID: 6692 RVA: 0x00067464 File Offset: 0x00065664
			public void ExecuteInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.InvokeInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x06001A25 RID: 6693 RVA: 0x00067474 File Offset: 0x00065674
			public IEnumerator ExecuteInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				return NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.InvokeInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x06001A26 RID: 6694 RVA: 0x00067484 File Offset: 0x00065684
			public void ExecuteStreamCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.InvokeStreamCall(stream, ref d, method, instance);
			}

			// Token: 0x06001A27 RID: 6695 RVA: 0x00067490 File Offset: 0x00065690
			public IEnumerator ExecuteStreamRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.InvokeStreamRoutine(stream, ref d, method, instance);
			}

			// Token: 0x06001A28 RID: 6696 RVA: 0x0006749C File Offset: 0x0006569C
			public void ExecuteStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.InvokeStreamInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x06001A29 RID: 6697 RVA: 0x000674AC File Offset: 0x000656AC
			public IEnumerator ExecuteStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				return NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.InvokeStreamInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x04000DB8 RID: 3512
			public static readonly NGC.IExecuter Singleton = new NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8>.Executer();
		}

		// Token: 0x020008B4 RID: 2228
		// (Invoke) Token: 0x06004CA8 RID: 19624
		public delegate void Call(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8);

		// Token: 0x020008B5 RID: 2229
		// (Invoke) Token: 0x06004CAC RID: 19628
		public delegate IEnumerator Routine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8);

		// Token: 0x020008B6 RID: 2230
		// (Invoke) Token: 0x06004CB0 RID: 19632
		public delegate void InfoCall(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, NetworkMessageInfo info);

		// Token: 0x020008B7 RID: 2231
		// (Invoke) Token: 0x06004CB4 RID: 19636
		public delegate IEnumerator InfoRoutine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, NetworkMessageInfo info);

		// Token: 0x020008B8 RID: 2232
		// (Invoke) Token: 0x06004CB8 RID: 19640
		public delegate void StreamCall(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, BitStream stream);

		// Token: 0x020008B9 RID: 2233
		// (Invoke) Token: 0x06004CBC RID: 19644
		public delegate IEnumerator StreamRoutine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, BitStream stream);

		// Token: 0x020008BA RID: 2234
		// (Invoke) Token: 0x06004CC0 RID: 19648
		public delegate void StreamInfoCall(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, NetworkMessageInfo info, BitStream stream);

		// Token: 0x020008BB RID: 2235
		// (Invoke) Token: 0x06004CC4 RID: 19652
		public delegate IEnumerator StreamInfoRoutine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, NetworkMessageInfo info, BitStream stream);
	}

	// Token: 0x020002E5 RID: 741
	public static class callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>
	{
		// Token: 0x06001A2A RID: 6698 RVA: 0x000674BC File Offset: 0x000656BC
		static callf()
		{
			BitStreamCodec.Add<NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Block>(new BitStreamCodec.Deserializer(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Deserializer), new BitStreamCodec.Serializer(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Serializer));
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x000674DC File Offset: 0x000656DC
		private static object Deserializer(BitStream stream, params object[] codecOptions)
		{
			NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Block block;
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

		// Token: 0x06001A2C RID: 6700 RVA: 0x0006757C File Offset: 0x0006577C
		private static void Serializer(BitStream stream, object value, params object[] codecOptions)
		{
			NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Block block = (NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Block)value;
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

		// Token: 0x06001A2D RID: 6701 RVA: 0x0006761C File Offset: 0x0006581C
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Call), instance, method, true);
			}
			((NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Call)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, p10);
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x000676E8 File Offset: 0x000658E8
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Routine), instance, method, true);
			}
			return ((NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Routine)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, p10);
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x000677B4 File Offset: 0x000659B4
		public static void InvokeInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.InfoCall), instance, method, true);
			}
			((NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.InfoCall)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, p10, info);
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x00067884 File Offset: 0x00065A84
		public static IEnumerator InvokeInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.InfoRoutine), instance, method, true);
			}
			return ((NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.InfoRoutine)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, p10, info);
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x00067954 File Offset: 0x00065B54
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.StreamCall), instance, method, true);
			}
			((NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.StreamCall)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, p10, stream);
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x00067A24 File Offset: 0x00065C24
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.StreamRoutine), instance, method, true);
			}
			return ((NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.StreamRoutine)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, p10, stream);
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x00067AF4 File Offset: 0x00065CF4
		public static void InvokeStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.StreamInfoCall), instance, method, true);
			}
			((NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.StreamInfoCall)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, p10, info, stream);
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x00067BC4 File Offset: 0x00065DC4
		public static IEnumerator InvokeStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.StreamInfoRoutine), instance, method, true);
			}
			return ((NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.StreamInfoRoutine)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, p10, info, stream);
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06001A35 RID: 6709 RVA: 0x00067C94 File Offset: 0x00065E94
		public static NGC.IExecuter Exec
		{
			get
			{
				return NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Executer.Singleton;
			}
		}

		// Token: 0x020002E6 RID: 742
		public struct Block
		{
			// Token: 0x04000DB9 RID: 3513
			public P0 p0;

			// Token: 0x04000DBA RID: 3514
			public P1 p1;

			// Token: 0x04000DBB RID: 3515
			public P2 p2;

			// Token: 0x04000DBC RID: 3516
			public P3 p3;

			// Token: 0x04000DBD RID: 3517
			public P4 p4;

			// Token: 0x04000DBE RID: 3518
			public P5 p5;

			// Token: 0x04000DBF RID: 3519
			public P6 p6;

			// Token: 0x04000DC0 RID: 3520
			public P7 p7;

			// Token: 0x04000DC1 RID: 3521
			public P8 p8;

			// Token: 0x04000DC2 RID: 3522
			public P9 p9;
		}

		// Token: 0x020002E7 RID: 743
		private sealed class Executer : NGC.IExecuter
		{
			// Token: 0x06001A38 RID: 6712 RVA: 0x00067CB0 File Offset: 0x00065EB0
			public void ExecuteCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.InvokeCall(stream, ref d, method, instance);
			}

			// Token: 0x06001A39 RID: 6713 RVA: 0x00067CBC File Offset: 0x00065EBC
			public IEnumerator ExecuteRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.InvokeRoutine(stream, ref d, method, instance);
			}

			// Token: 0x06001A3A RID: 6714 RVA: 0x00067CC8 File Offset: 0x00065EC8
			public void ExecuteInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.InvokeInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x06001A3B RID: 6715 RVA: 0x00067CD8 File Offset: 0x00065ED8
			public IEnumerator ExecuteInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				return NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.InvokeInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x06001A3C RID: 6716 RVA: 0x00067CE8 File Offset: 0x00065EE8
			public void ExecuteStreamCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.InvokeStreamCall(stream, ref d, method, instance);
			}

			// Token: 0x06001A3D RID: 6717 RVA: 0x00067CF4 File Offset: 0x00065EF4
			public IEnumerator ExecuteStreamRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.InvokeStreamRoutine(stream, ref d, method, instance);
			}

			// Token: 0x06001A3E RID: 6718 RVA: 0x00067D00 File Offset: 0x00065F00
			public void ExecuteStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.InvokeStreamInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x06001A3F RID: 6719 RVA: 0x00067D10 File Offset: 0x00065F10
			public IEnumerator ExecuteStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				return NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.InvokeStreamInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x04000DC3 RID: 3523
			public static readonly NGC.IExecuter Singleton = new NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9>.Executer();
		}

		// Token: 0x020008BC RID: 2236
		// (Invoke) Token: 0x06004CC8 RID: 19656
		public delegate void Call(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9);

		// Token: 0x020008BD RID: 2237
		// (Invoke) Token: 0x06004CCC RID: 19660
		public delegate IEnumerator Routine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9);

		// Token: 0x020008BE RID: 2238
		// (Invoke) Token: 0x06004CD0 RID: 19664
		public delegate void InfoCall(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, NetworkMessageInfo info);

		// Token: 0x020008BF RID: 2239
		// (Invoke) Token: 0x06004CD4 RID: 19668
		public delegate IEnumerator InfoRoutine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, NetworkMessageInfo info);

		// Token: 0x020008C0 RID: 2240
		// (Invoke) Token: 0x06004CD8 RID: 19672
		public delegate void StreamCall(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, BitStream stream);

		// Token: 0x020008C1 RID: 2241
		// (Invoke) Token: 0x06004CDC RID: 19676
		public delegate IEnumerator StreamRoutine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, BitStream stream);

		// Token: 0x020008C2 RID: 2242
		// (Invoke) Token: 0x06004CE0 RID: 19680
		public delegate void StreamInfoCall(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, NetworkMessageInfo info, BitStream stream);

		// Token: 0x020008C3 RID: 2243
		// (Invoke) Token: 0x06004CE4 RID: 19684
		public delegate IEnumerator StreamInfoRoutine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, NetworkMessageInfo info, BitStream stream);
	}

	// Token: 0x020002E8 RID: 744
	public static class callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>
	{
		// Token: 0x06001A40 RID: 6720 RVA: 0x00067D20 File Offset: 0x00065F20
		static callf()
		{
			BitStreamCodec.Add<NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Block>(new BitStreamCodec.Deserializer(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Deserializer), new BitStreamCodec.Serializer(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Serializer));
		}

		// Token: 0x06001A41 RID: 6721 RVA: 0x00067D40 File Offset: 0x00065F40
		private static object Deserializer(BitStream stream, params object[] codecOptions)
		{
			NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Block block;
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

		// Token: 0x06001A42 RID: 6722 RVA: 0x00067DF0 File Offset: 0x00065FF0
		private static void Serializer(BitStream stream, object value, params object[] codecOptions)
		{
			NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Block block = (NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Block)value;
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

		// Token: 0x06001A43 RID: 6723 RVA: 0x00067EA0 File Offset: 0x000660A0
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Call), instance, method, true);
			}
			((NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Call)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x00067F7C File Offset: 0x0006617C
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Routine), instance, method, true);
			}
			return ((NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Routine)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11);
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x00068058 File Offset: 0x00066258
		public static void InvokeInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.InfoCall), instance, method, true);
			}
			((NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.InfoCall)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, info);
		}

		// Token: 0x06001A46 RID: 6726 RVA: 0x00068138 File Offset: 0x00066338
		public static IEnumerator InvokeInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.InfoRoutine), instance, method, true);
			}
			return ((NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.InfoRoutine)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, info);
		}

		// Token: 0x06001A47 RID: 6727 RVA: 0x00068218 File Offset: 0x00066418
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.StreamCall), instance, method, true);
			}
			((NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.StreamCall)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, stream);
		}

		// Token: 0x06001A48 RID: 6728 RVA: 0x000682F8 File Offset: 0x000664F8
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.StreamRoutine), instance, method, true);
			}
			return ((NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.StreamRoutine)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, stream);
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x000683D8 File Offset: 0x000665D8
		public static void InvokeStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.StreamInfoCall), instance, method, true);
			}
			((NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.StreamInfoCall)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, info, stream);
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x000684B8 File Offset: 0x000666B8
		public static IEnumerator InvokeStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.StreamInfoRoutine), instance, method, true);
			}
			return ((NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.StreamInfoRoutine)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, info, stream);
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06001A4B RID: 6731 RVA: 0x00068598 File Offset: 0x00066798
		public static NGC.IExecuter Exec
		{
			get
			{
				return NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Executer.Singleton;
			}
		}

		// Token: 0x020002E9 RID: 745
		public struct Block
		{
			// Token: 0x04000DC4 RID: 3524
			public P0 p0;

			// Token: 0x04000DC5 RID: 3525
			public P1 p1;

			// Token: 0x04000DC6 RID: 3526
			public P2 p2;

			// Token: 0x04000DC7 RID: 3527
			public P3 p3;

			// Token: 0x04000DC8 RID: 3528
			public P4 p4;

			// Token: 0x04000DC9 RID: 3529
			public P5 p5;

			// Token: 0x04000DCA RID: 3530
			public P6 p6;

			// Token: 0x04000DCB RID: 3531
			public P7 p7;

			// Token: 0x04000DCC RID: 3532
			public P8 p8;

			// Token: 0x04000DCD RID: 3533
			public P9 p9;

			// Token: 0x04000DCE RID: 3534
			public P10 p10;
		}

		// Token: 0x020002EA RID: 746
		private sealed class Executer : NGC.IExecuter
		{
			// Token: 0x06001A4E RID: 6734 RVA: 0x000685B4 File Offset: 0x000667B4
			public void ExecuteCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.InvokeCall(stream, ref d, method, instance);
			}

			// Token: 0x06001A4F RID: 6735 RVA: 0x000685C0 File Offset: 0x000667C0
			public IEnumerator ExecuteRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.InvokeRoutine(stream, ref d, method, instance);
			}

			// Token: 0x06001A50 RID: 6736 RVA: 0x000685CC File Offset: 0x000667CC
			public void ExecuteInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.InvokeInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x06001A51 RID: 6737 RVA: 0x000685DC File Offset: 0x000667DC
			public IEnumerator ExecuteInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				return NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.InvokeInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x06001A52 RID: 6738 RVA: 0x000685EC File Offset: 0x000667EC
			public void ExecuteStreamCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.InvokeStreamCall(stream, ref d, method, instance);
			}

			// Token: 0x06001A53 RID: 6739 RVA: 0x000685F8 File Offset: 0x000667F8
			public IEnumerator ExecuteStreamRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.InvokeStreamRoutine(stream, ref d, method, instance);
			}

			// Token: 0x06001A54 RID: 6740 RVA: 0x00068604 File Offset: 0x00066804
			public void ExecuteStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.InvokeStreamInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x06001A55 RID: 6741 RVA: 0x00068614 File Offset: 0x00066814
			public IEnumerator ExecuteStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				return NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.InvokeStreamInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x04000DCF RID: 3535
			public static readonly NGC.IExecuter Singleton = new NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10>.Executer();
		}

		// Token: 0x020008C4 RID: 2244
		// (Invoke) Token: 0x06004CE8 RID: 19688
		public delegate void Call(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10);

		// Token: 0x020008C5 RID: 2245
		// (Invoke) Token: 0x06004CEC RID: 19692
		public delegate IEnumerator Routine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10);

		// Token: 0x020008C6 RID: 2246
		// (Invoke) Token: 0x06004CF0 RID: 19696
		public delegate void InfoCall(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, NetworkMessageInfo info);

		// Token: 0x020008C7 RID: 2247
		// (Invoke) Token: 0x06004CF4 RID: 19700
		public delegate IEnumerator InfoRoutine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, NetworkMessageInfo info);

		// Token: 0x020008C8 RID: 2248
		// (Invoke) Token: 0x06004CF8 RID: 19704
		public delegate void StreamCall(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, BitStream stream);

		// Token: 0x020008C9 RID: 2249
		// (Invoke) Token: 0x06004CFC RID: 19708
		public delegate IEnumerator StreamRoutine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, BitStream stream);

		// Token: 0x020008CA RID: 2250
		// (Invoke) Token: 0x06004D00 RID: 19712
		public delegate void StreamInfoCall(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, NetworkMessageInfo info, BitStream stream);

		// Token: 0x020008CB RID: 2251
		// (Invoke) Token: 0x06004D04 RID: 19716
		public delegate IEnumerator StreamInfoRoutine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, NetworkMessageInfo info, BitStream stream);
	}

	// Token: 0x020002EB RID: 747
	public static class callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>
	{
		// Token: 0x06001A56 RID: 6742 RVA: 0x00068624 File Offset: 0x00066824
		static callf()
		{
			BitStreamCodec.Add<NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Block>(new BitStreamCodec.Deserializer(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Deserializer), new BitStreamCodec.Serializer(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Serializer));
		}

		// Token: 0x06001A57 RID: 6743 RVA: 0x00068644 File Offset: 0x00066844
		private static object Deserializer(BitStream stream, params object[] codecOptions)
		{
			NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Block block;
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

		// Token: 0x06001A58 RID: 6744 RVA: 0x00068700 File Offset: 0x00066900
		private static void Serializer(BitStream stream, object value, params object[] codecOptions)
		{
			NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Block block = (NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Block)value;
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

		// Token: 0x06001A59 RID: 6745 RVA: 0x000687BC File Offset: 0x000669BC
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Call), instance, method, true);
			}
			((NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Call)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12);
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x000688A8 File Offset: 0x00066AA8
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Routine), instance, method, true);
			}
			return ((NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Routine)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12);
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x00068994 File Offset: 0x00066B94
		public static void InvokeInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.InfoCall), instance, method, true);
			}
			((NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.InfoCall)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, info);
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x00068A84 File Offset: 0x00066C84
		public static IEnumerator InvokeInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.InfoRoutine), instance, method, true);
			}
			return ((NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.InfoRoutine)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, info);
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x00068B74 File Offset: 0x00066D74
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.StreamCall), instance, method, true);
			}
			((NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.StreamCall)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, stream);
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x00068C64 File Offset: 0x00066E64
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.StreamRoutine), instance, method, true);
			}
			return ((NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.StreamRoutine)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, stream);
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x00068D54 File Offset: 0x00066F54
		public static void InvokeStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.StreamInfoCall), instance, method, true);
			}
			((NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.StreamInfoCall)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, info, stream);
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x00068E44 File Offset: 0x00067044
		public static IEnumerator InvokeStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
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
				d = Delegate.CreateDelegate(typeof(NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.StreamInfoRoutine), instance, method, true);
			}
			return ((NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.StreamInfoRoutine)d)(p, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, info, stream);
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06001A61 RID: 6753 RVA: 0x00068F34 File Offset: 0x00067134
		public static NGC.IExecuter Exec
		{
			get
			{
				return NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Executer.Singleton;
			}
		}

		// Token: 0x020002EC RID: 748
		public struct Block
		{
			// Token: 0x04000DD0 RID: 3536
			public P0 p0;

			// Token: 0x04000DD1 RID: 3537
			public P1 p1;

			// Token: 0x04000DD2 RID: 3538
			public P2 p2;

			// Token: 0x04000DD3 RID: 3539
			public P3 p3;

			// Token: 0x04000DD4 RID: 3540
			public P4 p4;

			// Token: 0x04000DD5 RID: 3541
			public P5 p5;

			// Token: 0x04000DD6 RID: 3542
			public P6 p6;

			// Token: 0x04000DD7 RID: 3543
			public P7 p7;

			// Token: 0x04000DD8 RID: 3544
			public P8 p8;

			// Token: 0x04000DD9 RID: 3545
			public P9 p9;

			// Token: 0x04000DDA RID: 3546
			public P10 p10;

			// Token: 0x04000DDB RID: 3547
			public P11 p11;
		}

		// Token: 0x020002ED RID: 749
		private sealed class Executer : NGC.IExecuter
		{
			// Token: 0x06001A64 RID: 6756 RVA: 0x00068F50 File Offset: 0x00067150
			public void ExecuteCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.InvokeCall(stream, ref d, method, instance);
			}

			// Token: 0x06001A65 RID: 6757 RVA: 0x00068F5C File Offset: 0x0006715C
			public IEnumerator ExecuteRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.InvokeRoutine(stream, ref d, method, instance);
			}

			// Token: 0x06001A66 RID: 6758 RVA: 0x00068F68 File Offset: 0x00067168
			public void ExecuteInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.InvokeInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x06001A67 RID: 6759 RVA: 0x00068F78 File Offset: 0x00067178
			public IEnumerator ExecuteInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				return NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.InvokeInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x06001A68 RID: 6760 RVA: 0x00068F88 File Offset: 0x00067188
			public void ExecuteStreamCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.InvokeStreamCall(stream, ref d, method, instance);
			}

			// Token: 0x06001A69 RID: 6761 RVA: 0x00068F94 File Offset: 0x00067194
			public IEnumerator ExecuteStreamRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance)
			{
				return NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.InvokeStreamRoutine(stream, ref d, method, instance);
			}

			// Token: 0x06001A6A RID: 6762 RVA: 0x00068FA0 File Offset: 0x000671A0
			public void ExecuteStreamInfoCall(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.InvokeStreamInfoCall(stream, ref d, method, instance, info);
			}

			// Token: 0x06001A6B RID: 6763 RVA: 0x00068FB0 File Offset: 0x000671B0
			public IEnumerator ExecuteStreamInfoRoutine(BitStream stream, ref Delegate d, MethodInfo method, MonoBehaviour instance, NetworkMessageInfo info)
			{
				return NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.InvokeStreamInfoRoutine(stream, ref d, method, instance, info);
			}

			// Token: 0x04000DDC RID: 3548
			public static readonly NGC.IExecuter Singleton = new NGC.callf<P0, P1, P2, P3, P4, P5, P6, P7, P8, P9, P10, P11>.Executer();
		}

		// Token: 0x020008CC RID: 2252
		// (Invoke) Token: 0x06004D08 RID: 19720
		public delegate void Call(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11);

		// Token: 0x020008CD RID: 2253
		// (Invoke) Token: 0x06004D0C RID: 19724
		public delegate IEnumerator Routine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11);

		// Token: 0x020008CE RID: 2254
		// (Invoke) Token: 0x06004D10 RID: 19728
		public delegate void InfoCall(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11, NetworkMessageInfo info);

		// Token: 0x020008CF RID: 2255
		// (Invoke) Token: 0x06004D14 RID: 19732
		public delegate IEnumerator InfoRoutine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11, NetworkMessageInfo info);

		// Token: 0x020008D0 RID: 2256
		// (Invoke) Token: 0x06004D18 RID: 19736
		public delegate void StreamCall(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11, BitStream stream);

		// Token: 0x020008D1 RID: 2257
		// (Invoke) Token: 0x06004D1C RID: 19740
		public delegate IEnumerator StreamRoutine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11, BitStream stream);

		// Token: 0x020008D2 RID: 2258
		// (Invoke) Token: 0x06004D20 RID: 19744
		public delegate void StreamInfoCall(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11, NetworkMessageInfo info, BitStream stream);

		// Token: 0x020008D3 RID: 2259
		// (Invoke) Token: 0x06004D24 RID: 19748
		public delegate IEnumerator StreamInfoRoutine(P0 p0, P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7, P8 p8, P9 p9, P10 p10, P11 p11, NetworkMessageInfo info, BitStream stream);
	}

	// Token: 0x020008D4 RID: 2260
	// (Invoke) Token: 0x06004D28 RID: 19752
	public delegate void EventCallback(NGCView view);
}
