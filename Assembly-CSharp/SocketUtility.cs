using System;
using UnityEngine;

// Token: 0x0200033E RID: 830
public static class SocketUtility
{
	// Token: 0x06001F97 RID: 8087 RVA: 0x0007C634 File Offset: 0x0007A834
	public static void Play(this AudioClip clip, Socket socket, bool parent, float volume, float pitch, AudioRolloffMode rolloffMode, float minDistance, float maxDistance, float doppler, float spread, bool bypassEffects)
	{
		if (socket != null)
		{
			return;
		}
		if (parent)
		{
			socket.Snap();
			clip.Play(socket.attachParent, socket.position, socket.rotation, volume, pitch, rolloffMode, minDistance, maxDistance, doppler, spread, bypassEffects);
		}
		else
		{
			clip.Play(socket.position, socket.rotation, volume, pitch, rolloffMode, minDistance, maxDistance, doppler, spread, bypassEffects);
		}
	}

	// Token: 0x06001F98 RID: 8088 RVA: 0x0007C6A4 File Offset: 0x0007A8A4
	public static bool FindSocket<TSocket>(this Socket.Map map, string name, out TSocket socket) where TSocket : Socket, new()
	{
		return map.Socket<TSocket>(name, out socket);
	}

	// Token: 0x06001F99 RID: 8089 RVA: 0x0007C6C4 File Offset: 0x0007A8C4
	public static bool FindSocket<TSocket>(this Socket.Map map, int index, out TSocket socket) where TSocket : Socket, new()
	{
		return map.Socket<TSocket>(index, out socket);
	}

	// Token: 0x06001F9A RID: 8090 RVA: 0x0007C6E4 File Offset: 0x0007A8E4
	public static bool FindSocket(this Socket.Map map, string name, out Socket socket)
	{
		return map.Socket(name, out socket);
	}

	// Token: 0x06001F9B RID: 8091 RVA: 0x0007C704 File Offset: 0x0007A904
	public static bool FindSocket(this Socket.Map map, int index, out Socket socket)
	{
		return map.Socket(index, out socket);
	}

	// Token: 0x06001F9C RID: 8092 RVA: 0x0007C724 File Offset: 0x0007A924
	public static bool ContainsSocket<TSocket>(this Socket.Map map, string name) where TSocket : Socket, new()
	{
		TSocket tsocket;
		return map.FindSocket(name, out tsocket);
	}

	// Token: 0x06001F9D RID: 8093 RVA: 0x0007C73C File Offset: 0x0007A93C
	public static bool ContainsSocket<TSocket>(this Socket.Map map, int index) where TSocket : Socket, new()
	{
		TSocket tsocket;
		return map.FindSocket(index, out tsocket);
	}

	// Token: 0x06001F9E RID: 8094 RVA: 0x0007C754 File Offset: 0x0007A954
	public static bool ContainsSocket(this Socket.Map map, string name)
	{
		Socket socket;
		return map.FindSocket(name, out socket);
	}

	// Token: 0x06001F9F RID: 8095 RVA: 0x0007C76C File Offset: 0x0007A96C
	public static bool ContainsSocket(this Socket.Map map, int index)
	{
		Socket socket;
		return map.FindSocket(index, out socket);
	}

	// Token: 0x06001FA0 RID: 8096 RVA: 0x0007C784 File Offset: 0x0007A984
	public static int SocketIndex(this Socket.Map map, string name)
	{
		int result;
		map.SocketIndex(name, out result);
		return result;
	}

	// Token: 0x06001FA1 RID: 8097 RVA: 0x0007C7A4 File Offset: 0x0007A9A4
	public static Socket.Map GetSocketMapOrNull(this Socket.Mapped mapped)
	{
		return (!object.ReferenceEquals(mapped, null) && mapped as Object) ? mapped.socketMap : null;
	}

	// Token: 0x06001FA2 RID: 8098 RVA: 0x0007C7DC File Offset: 0x0007A9DC
	public static bool GetSocketMapOrNull(this Socket.Mapped mapped, out Socket.Map map)
	{
		if (object.ReferenceEquals(mapped, null) || !(mapped as Object))
		{
			map = null;
			return false;
		}
		map = mapped.socketMap;
		return !object.ReferenceEquals(map, null);
	}

	// Token: 0x06001FA3 RID: 8099 RVA: 0x0007C820 File Offset: 0x0007AA20
	public static bool FindSocket<TSocket>(this Socket.Mapped mapped, string name, out TSocket socket) where TSocket : Socket, new()
	{
		return mapped.GetSocketMapOrNull().Socket<TSocket>(name, out socket);
	}

	// Token: 0x06001FA4 RID: 8100 RVA: 0x0007C844 File Offset: 0x0007AA44
	public static bool FindSocket<TSocket>(this Socket.Mapped mapped, int index, out TSocket socket) where TSocket : Socket, new()
	{
		return mapped.GetSocketMapOrNull().Socket<TSocket>(index, out socket);
	}

	// Token: 0x06001FA5 RID: 8101 RVA: 0x0007C868 File Offset: 0x0007AA68
	public static bool FindSocket(this Socket.Mapped mapped, string name, out Socket socket)
	{
		return mapped.GetSocketMapOrNull().Socket(name, out socket);
	}

	// Token: 0x06001FA6 RID: 8102 RVA: 0x0007C88C File Offset: 0x0007AA8C
	public static bool FindSocket(this Socket.Mapped mapped, int index, out Socket socket)
	{
		return mapped.GetSocketMapOrNull().Socket(index, out socket);
	}

	// Token: 0x06001FA7 RID: 8103 RVA: 0x0007C8B0 File Offset: 0x0007AAB0
	public static bool ContainsSocket<TSocket>(this Socket.Mapped mapped, string name) where TSocket : Socket, new()
	{
		TSocket tsocket;
		return mapped.GetSocketMapOrNull().FindSocket(name, out tsocket);
	}

	// Token: 0x06001FA8 RID: 8104 RVA: 0x0007C8CC File Offset: 0x0007AACC
	public static bool ContainsSocket<TSocket>(this Socket.Mapped mapped, int index) where TSocket : Socket, new()
	{
		TSocket tsocket;
		return mapped.GetSocketMapOrNull().FindSocket(index, out tsocket);
	}

	// Token: 0x06001FA9 RID: 8105 RVA: 0x0007C8E8 File Offset: 0x0007AAE8
	public static bool ContainsSocket(this Socket.Mapped mapped, string name)
	{
		Socket socket;
		return mapped.GetSocketMapOrNull().FindSocket(name, out socket);
	}

	// Token: 0x06001FAA RID: 8106 RVA: 0x0007C904 File Offset: 0x0007AB04
	public static bool ContainsSocket(this Socket.Mapped mapped, int index)
	{
		Socket socket;
		return mapped.GetSocketMapOrNull().FindSocket(index, out socket);
	}

	// Token: 0x06001FAB RID: 8107 RVA: 0x0007C920 File Offset: 0x0007AB20
	public static int SocketIndex(this Socket.Mapped mapped, string name)
	{
		int result;
		mapped.GetSocketMapOrNull().SocketIndex(name, out result);
		return result;
	}
}
