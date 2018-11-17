using System;
using UnityEngine;

// Token: 0x020003EB RID: 1003
public static class SocketUtility
{
	// Token: 0x060022F9 RID: 8953 RVA: 0x00081A30 File Offset: 0x0007FC30
	public static void Play(this AudioClip clip, global::Socket socket, bool parent, float volume, float pitch, AudioRolloffMode rolloffMode, float minDistance, float maxDistance, float doppler, float spread, bool bypassEffects)
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

	// Token: 0x060022FA RID: 8954 RVA: 0x00081AA0 File Offset: 0x0007FCA0
	public static bool FindSocket<TSocket>(this global::Socket.Map map, string name, out TSocket socket) where TSocket : global::Socket, new()
	{
		return map.Socket<TSocket>(name, out socket);
	}

	// Token: 0x060022FB RID: 8955 RVA: 0x00081AC0 File Offset: 0x0007FCC0
	public static bool FindSocket<TSocket>(this global::Socket.Map map, int index, out TSocket socket) where TSocket : global::Socket, new()
	{
		return map.Socket<TSocket>(index, out socket);
	}

	// Token: 0x060022FC RID: 8956 RVA: 0x00081AE0 File Offset: 0x0007FCE0
	public static bool FindSocket(this global::Socket.Map map, string name, out global::Socket socket)
	{
		return map.Socket(name, out socket);
	}

	// Token: 0x060022FD RID: 8957 RVA: 0x00081B00 File Offset: 0x0007FD00
	public static bool FindSocket(this global::Socket.Map map, int index, out global::Socket socket)
	{
		return map.Socket(index, out socket);
	}

	// Token: 0x060022FE RID: 8958 RVA: 0x00081B20 File Offset: 0x0007FD20
	public static bool ContainsSocket<TSocket>(this global::Socket.Map map, string name) where TSocket : global::Socket, new()
	{
		TSocket tsocket;
		return map.FindSocket(name, out tsocket);
	}

	// Token: 0x060022FF RID: 8959 RVA: 0x00081B38 File Offset: 0x0007FD38
	public static bool ContainsSocket<TSocket>(this global::Socket.Map map, int index) where TSocket : global::Socket, new()
	{
		TSocket tsocket;
		return map.FindSocket(index, out tsocket);
	}

	// Token: 0x06002300 RID: 8960 RVA: 0x00081B50 File Offset: 0x0007FD50
	public static bool ContainsSocket(this global::Socket.Map map, string name)
	{
		global::Socket socket;
		return map.FindSocket(name, out socket);
	}

	// Token: 0x06002301 RID: 8961 RVA: 0x00081B68 File Offset: 0x0007FD68
	public static bool ContainsSocket(this global::Socket.Map map, int index)
	{
		global::Socket socket;
		return map.FindSocket(index, out socket);
	}

	// Token: 0x06002302 RID: 8962 RVA: 0x00081B80 File Offset: 0x0007FD80
	public static int SocketIndex(this global::Socket.Map map, string name)
	{
		int result;
		map.SocketIndex(name, out result);
		return result;
	}

	// Token: 0x06002303 RID: 8963 RVA: 0x00081BA0 File Offset: 0x0007FDA0
	public static global::Socket.Map GetSocketMapOrNull(this global::Socket.Mapped mapped)
	{
		return (!object.ReferenceEquals(mapped, null) && mapped as Object) ? mapped.socketMap : null;
	}

	// Token: 0x06002304 RID: 8964 RVA: 0x00081BD8 File Offset: 0x0007FDD8
	public static bool GetSocketMapOrNull(this global::Socket.Mapped mapped, out global::Socket.Map map)
	{
		if (object.ReferenceEquals(mapped, null) || !(mapped as Object))
		{
			map = null;
			return false;
		}
		map = mapped.socketMap;
		return !object.ReferenceEquals(map, null);
	}

	// Token: 0x06002305 RID: 8965 RVA: 0x00081C1C File Offset: 0x0007FE1C
	public static bool FindSocket<TSocket>(this global::Socket.Mapped mapped, string name, out TSocket socket) where TSocket : global::Socket, new()
	{
		return mapped.GetSocketMapOrNull().Socket<TSocket>(name, out socket);
	}

	// Token: 0x06002306 RID: 8966 RVA: 0x00081C40 File Offset: 0x0007FE40
	public static bool FindSocket<TSocket>(this global::Socket.Mapped mapped, int index, out TSocket socket) where TSocket : global::Socket, new()
	{
		return mapped.GetSocketMapOrNull().Socket<TSocket>(index, out socket);
	}

	// Token: 0x06002307 RID: 8967 RVA: 0x00081C64 File Offset: 0x0007FE64
	public static bool FindSocket(this global::Socket.Mapped mapped, string name, out global::Socket socket)
	{
		return mapped.GetSocketMapOrNull().Socket(name, out socket);
	}

	// Token: 0x06002308 RID: 8968 RVA: 0x00081C88 File Offset: 0x0007FE88
	public static bool FindSocket(this global::Socket.Mapped mapped, int index, out global::Socket socket)
	{
		return mapped.GetSocketMapOrNull().Socket(index, out socket);
	}

	// Token: 0x06002309 RID: 8969 RVA: 0x00081CAC File Offset: 0x0007FEAC
	public static bool ContainsSocket<TSocket>(this global::Socket.Mapped mapped, string name) where TSocket : global::Socket, new()
	{
		TSocket tsocket;
		return mapped.GetSocketMapOrNull().FindSocket(name, out tsocket);
	}

	// Token: 0x0600230A RID: 8970 RVA: 0x00081CC8 File Offset: 0x0007FEC8
	public static bool ContainsSocket<TSocket>(this global::Socket.Mapped mapped, int index) where TSocket : global::Socket, new()
	{
		TSocket tsocket;
		return mapped.GetSocketMapOrNull().FindSocket(index, out tsocket);
	}

	// Token: 0x0600230B RID: 8971 RVA: 0x00081CE4 File Offset: 0x0007FEE4
	public static bool ContainsSocket(this global::Socket.Mapped mapped, string name)
	{
		global::Socket socket;
		return mapped.GetSocketMapOrNull().FindSocket(name, out socket);
	}

	// Token: 0x0600230C RID: 8972 RVA: 0x00081D00 File Offset: 0x0007FF00
	public static bool ContainsSocket(this global::Socket.Mapped mapped, int index)
	{
		global::Socket socket;
		return mapped.GetSocketMapOrNull().FindSocket(index, out socket);
	}

	// Token: 0x0600230D RID: 8973 RVA: 0x00081D1C File Offset: 0x0007FF1C
	public static int SocketIndex(this global::Socket.Mapped mapped, string name)
	{
		int result;
		mapped.GetSocketMapOrNull().SocketIndex(name, out result);
		return result;
	}
}
