using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000EE RID: 238
public static class SoundPool
{
	// Token: 0x170000CC RID: 204
	// (get) Token: 0x06000502 RID: 1282 RVA: 0x00018D9C File Offset: 0x00016F9C
	// (set) Token: 0x06000503 RID: 1283 RVA: 0x00018DA4 File Offset: 0x00016FA4
	internal static bool enabled
	{
		get
		{
			return global::SoundPool._enabled;
		}
		set
		{
			if (value)
			{
				global::SoundPool._enabled = !global::SoundPool._quitting;
			}
			else
			{
				global::SoundPool._enabled = false;
			}
		}
	}

	// Token: 0x170000CD RID: 205
	// (set) Token: 0x06000504 RID: 1284 RVA: 0x00018DC4 File Offset: 0x00016FC4
	internal static bool quitting
	{
		set
		{
			if (!global::SoundPool._quitting && value)
			{
				global::SoundPool._quitting = true;
				global::SoundPool._enabled = false;
				global::SoundPool.Drain();
			}
		}
	}

	// Token: 0x06000505 RID: 1285 RVA: 0x00018DE8 File Offset: 0x00016FE8
	private static global::SoundPool.Node NewNode()
	{
		global::SoundPool.Node node = new global::SoundPool.Node();
		GameObject gameObject = new GameObject("zzz-soundpoolnode", global::SoundPool.goTypes);
		gameObject.hideFlags = 8;
		Object.DontDestroyOnLoad(gameObject);
		node.audio = gameObject.audio;
		node.transform = gameObject.transform;
		node.audio.playOnAwake = false;
		node.audio.enabled = false;
		return node;
	}

	// Token: 0x06000506 RID: 1286 RVA: 0x00018E4C File Offset: 0x0001704C
	private static global::SoundPool.Node CreateNode()
	{
		if (global::SoundPool.reserved.first.has)
		{
			global::SoundPool.Node node = global::SoundPool.reserved.first.node;
			node.EnterLimbo();
			return node;
		}
		return global::SoundPool.NewNode();
	}

	// Token: 0x06000507 RID: 1287 RVA: 0x00018E90 File Offset: 0x00017090
	private static Object TARG(ref global::SoundPool.Settings settings)
	{
		return (!settings.parent) ? settings.clip : settings.parent;
	}

	// Token: 0x06000508 RID: 1288 RVA: 0x00018EB4 File Offset: 0x000170B4
	private static void Play(ref global::SoundPool.Settings settings)
	{
		if (!global::SoundPool._enabled || settings.volume <= 0f || settings.pitch == 0f || !settings.clip)
		{
			return;
		}
		Transform transform = null;
		global::SoundPool.Root root;
		global::SoundPool.RootID rootID;
		bool flag;
		switch (settings.SelectRoot)
		{
		default:
			root = global::SoundPool.playing;
			rootID = global::SoundPool.RootID.PLAYING;
			flag = false;
			break;
		case 1:
			if (!Camera.main)
			{
				return;
			}
			transform = Camera.main.transform;
			root = global::SoundPool.playingCamera;
			rootID = global::SoundPool.RootID.PLAYING_CAMERA;
			flag = false;
			break;
		case 2:
			if (!settings.parent)
			{
				return;
			}
			root = global::SoundPool.playingAttached;
			rootID = global::SoundPool.RootID.PLAYING_ATTACHED;
			flag = false;
			break;
		case 5:
			if (!Camera.main)
			{
				return;
			}
			transform = Camera.main.transform;
			root = global::SoundPool.playingCamera;
			rootID = global::SoundPool.RootID.PLAYING_CAMERA;
			flag = true;
			break;
		case 6:
			if (!settings.parent)
			{
				return;
			}
			root = global::SoundPool.playingAttached;
			rootID = global::SoundPool.RootID.PLAYING_ATTACHED;
			flag = true;
			break;
		}
		Vector3 vector;
		Quaternion quaternion;
		Vector3 vector2;
		Quaternion rotation;
		if (flag)
		{
			global::SoundPool.RootID rootID2 = rootID;
			if (rootID2 != global::SoundPool.RootID.PLAYING_ATTACHED)
			{
				if (rootID2 != global::SoundPool.RootID.PLAYING_CAMERA)
				{
					return;
				}
				vector = transform.InverseTransformPoint(settings.localPosition);
				quaternion = settings.localRotation * Quaternion.Inverse(transform.rotation);
			}
			else
			{
				vector = settings.parent.InverseTransformPoint(settings.localPosition);
				quaternion = settings.localRotation * Quaternion.Inverse(settings.parent.rotation);
			}
			vector2 = settings.localPosition;
			rotation = settings.localRotation;
		}
		else
		{
			vector = settings.localPosition;
			quaternion = settings.localRotation;
			global::SoundPool.RootID rootID2 = rootID;
			switch (rootID2 + 3)
			{
			case global::SoundPool.RootID.LIMBO:
				vector2 = settings.parent.TransformPoint(vector);
				rotation = settings.parent.rotation * quaternion;
				break;
			case global::SoundPool.RootID.RESERVED:
				vector2 = transform.TransformPoint(vector);
				rotation = transform.rotation * quaternion;
				break;
			case global::SoundPool.RootID.DISPOSED:
				vector2 = vector;
				rotation = quaternion;
				break;
			default:
				return;
			}
		}
		if (!transform)
		{
			Camera main = Camera.main;
			if (!main)
			{
				return;
			}
			transform = main.transform;
			float num = Vector3.Distance(vector2, transform.position);
			switch (settings.mode)
			{
			case 0:
				if (num > settings.max * 2f)
				{
					return;
				}
				break;
			case 1:
			case 2:
				if (num > settings.max)
				{
					return;
				}
				break;
			}
		}
		global::SoundPool.Node node = global::SoundPool.CreateNode();
		if ((int)node.rootID != 0)
		{
			Debug.LogWarning("Wasn't Limbo " + node.rootID);
		}
		node.root = root;
		node.rootID = rootID;
		node.audio.pan = settings.pan;
		node.audio.panLevel = settings.panLevel;
		node.audio.volume = settings.volume;
		node.audio.dopplerLevel = settings.doppler;
		node.audio.pitch = settings.pitch;
		node.audio.rolloffMode = settings.mode;
		node.audio.minDistance = settings.min;
		node.audio.maxDistance = settings.max;
		node.audio.spread = settings.spread;
		node.audio.bypassEffects = settings.bypassEffects;
		node.audio.priority = settings.priority;
		node.parent = settings.parent;
		node.transform.position = vector2;
		node.transform.rotation = rotation;
		node.translation = vector;
		node.rotation = quaternion;
		node.audio.clip = settings.clip;
		node.Bind();
		node.audio.enabled = true;
		node.audio.Play();
	}

	// Token: 0x06000509 RID: 1289 RVA: 0x000192E0 File Offset: 0x000174E0
	public static void Pump()
	{
		if (global::SoundPool.firstLeak)
		{
			if (!global::SoundPool.hadFirstLeak)
			{
				Debug.LogWarning("SoundPool node leaked for the first time. Though performance should still be good, from now on until application exit there will be extra processing in Pump to clean up game objects of leaked/gc'd nodes. [ie. a mutex is now being locked and unlocked]");
				global::SoundPool.hadFirstLeak = true;
			}
			global::SoundPool.NodeGC.JOIN();
		}
		global::SoundPool.Dir dir = global::SoundPool.playingCamera.first;
		if (dir.has)
		{
			Camera main = Camera.main;
			if (main)
			{
				Transform transform = main.transform;
				Quaternion rotation = transform.rotation;
				do
				{
					global::SoundPool.Node node = dir.node;
					dir = dir.node.way.next;
					if (node.audio.isPlaying)
					{
						node.transform.position = transform.TransformPoint(node.translation);
						node.transform.rotation = rotation * node.rotation;
					}
					else
					{
						node.Reserve();
					}
				}
				while (dir.has);
			}
			else
			{
				do
				{
					global::SoundPool.Node node2 = dir.node;
					dir = dir.node.way.next;
					node2.Reserve();
				}
				while (dir.has);
			}
		}
		dir = global::SoundPool.playingAttached.first;
		while (dir.has)
		{
			global::SoundPool.Node node3 = dir.node;
			dir = dir.node.way.next;
			if (node3.audio.isPlaying && node3.parent)
			{
				node3.transform.position = node3.parent.TransformPoint(node3.translation);
				node3.transform.rotation = node3.parent.rotation * node3.rotation;
			}
			else
			{
				node3.Reserve();
			}
		}
		dir = global::SoundPool.playing.first;
		while (dir.has)
		{
			global::SoundPool.Node node4 = dir.node;
			dir = dir.node.way.next;
			if (!node4.audio.isPlaying)
			{
				node4.Reserve();
			}
		}
	}

	// Token: 0x0600050A RID: 1290 RVA: 0x000194EC File Offset: 0x000176EC
	public static void DrainReserves()
	{
		global::SoundPool.Dir dir = global::SoundPool.reserved.first;
		while (dir.has)
		{
			global::SoundPool.Node node = dir.node;
			dir = dir.node.way.next;
			node.Dispose();
		}
	}

	// Token: 0x0600050B RID: 1291 RVA: 0x00019538 File Offset: 0x00017738
	public static void Stop()
	{
		global::SoundPool.Dir dir = global::SoundPool.playing.first;
		while (dir.has)
		{
			global::SoundPool.Node node = dir.node;
			dir = dir.node.way.next;
			node.Reserve();
		}
		dir = global::SoundPool.playingAttached.first;
		while (dir.has)
		{
			global::SoundPool.Node node = dir.node;
			dir = dir.node.way.next;
			node.Reserve();
		}
		dir = global::SoundPool.playingCamera.first;
		while (dir.has)
		{
			global::SoundPool.Node node = dir.node;
			dir = dir.node.way.next;
			node.Reserve();
		}
	}

	// Token: 0x0600050C RID: 1292 RVA: 0x000195FC File Offset: 0x000177FC
	public static void Drain()
	{
		global::SoundPool.Dir dir = global::SoundPool.playing.first;
		while (dir.has)
		{
			global::SoundPool.Node node = dir.node;
			dir = dir.node.way.next;
			node.Dispose();
		}
		dir = global::SoundPool.playingAttached.first;
		while (dir.has)
		{
			global::SoundPool.Node node = dir.node;
			dir = dir.node.way.next;
			node.Dispose();
		}
		dir = global::SoundPool.playingCamera.first;
		while (dir.has)
		{
			global::SoundPool.Node node = dir.node;
			dir = dir.node.way.next;
			node.Dispose();
		}
		dir = global::SoundPool.reserved.first;
		while (dir.has)
		{
			global::SoundPool.Node node = dir.node;
			dir = dir.node.way.next;
			node.Dispose();
		}
	}

	// Token: 0x170000CE RID: 206
	// (get) Token: 0x0600050D RID: 1293 RVA: 0x000196FC File Offset: 0x000178FC
	public static int reserveCount
	{
		get
		{
			return global::SoundPool.reserved.count;
		}
	}

	// Token: 0x170000CF RID: 207
	// (get) Token: 0x0600050E RID: 1294 RVA: 0x00019708 File Offset: 0x00017908
	public static int playingCount
	{
		get
		{
			return global::SoundPool.playingCamera.count + global::SoundPool.playingAttached.count + global::SoundPool.playing.count;
		}
	}

	// Token: 0x170000D0 RID: 208
	// (get) Token: 0x0600050F RID: 1295 RVA: 0x00019738 File Offset: 0x00017938
	public static int totalCount
	{
		get
		{
			return global::SoundPool.playingCamera.count + global::SoundPool.playingAttached.count + global::SoundPool.playing.count + global::SoundPool.reserved.count;
		}
	}

	// Token: 0x06000510 RID: 1296 RVA: 0x00019768 File Offset: 0x00017968
	public static void Play(this AudioClip clip, Vector3 position, Quaternion rotation)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000511 RID: 1297 RVA: 0x0001979C File Offset: 0x0001799C
	public static void Play(this AudioClip clip, Vector3 position, Quaternion rotation, float volume)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000512 RID: 1298 RVA: 0x000197D8 File Offset: 0x000179D8
	public static void Play(this AudioClip clip, Vector3 position, Quaternion rotation, float volume, float pitch)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000513 RID: 1299 RVA: 0x0001981C File Offset: 0x00017A1C
	public static void Play(this AudioClip clip, Vector3 position, Quaternion rotation, float volume, float minDistance, float maxDistance)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		def.min = minDistance;
		def.max = maxDistance;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000514 RID: 1300 RVA: 0x00019868 File Offset: 0x00017A68
	public static void Play(this AudioClip clip, Vector3 position, Quaternion rotation, float volume, AudioRolloffMode rolloffMode, float minDistance, float maxDistance)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		def.min = minDistance;
		def.max = maxDistance;
		def.mode = rolloffMode;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000515 RID: 1301 RVA: 0x000198C0 File Offset: 0x00017AC0
	public static void Play(this AudioClip clip, Vector3 position, Quaternion rotation, float volume, float pitch, float minDistance, float maxDistance)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.min = minDistance;
		def.max = maxDistance;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000516 RID: 1302 RVA: 0x00019918 File Offset: 0x00017B18
	public static void Play(this AudioClip clip, Vector3 position, Quaternion rotation, float volume, float pitch, float minDistance, float maxDistance, float dopplerLevel)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.doppler = dopplerLevel;
		def.min = minDistance;
		def.max = maxDistance;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000517 RID: 1303 RVA: 0x00019978 File Offset: 0x00017B78
	public static void Play(this AudioClip clip, Vector3 position, Quaternion rotation, float volume, float pitch, float minDistance, float maxDistance, float dopplerLevel, float spread)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.doppler = dopplerLevel;
		def.min = minDistance;
		def.max = maxDistance;
		def.spread = spread;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000518 RID: 1304 RVA: 0x000199E0 File Offset: 0x00017BE0
	public static void Play(this AudioClip clip, Vector3 position, Quaternion rotation, float volume, float pitch, float minDistance, float maxDistance, float dopplerLevel, float spread, bool bypassEffects)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.doppler = dopplerLevel;
		def.min = minDistance;
		def.max = maxDistance;
		def.spread = spread;
		def.bypassEffects = bypassEffects;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000519 RID: 1305 RVA: 0x00019A50 File Offset: 0x00017C50
	public static void Play(this AudioClip clip, Vector3 position, Quaternion rotation, float volume, float pitch, AudioRolloffMode rolloffMode, float minDistance, float maxDistance, float dopplerLevel)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.doppler = dopplerLevel;
		def.min = minDistance;
		def.max = maxDistance;
		def.mode = rolloffMode;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x0600051A RID: 1306 RVA: 0x00019AB8 File Offset: 0x00017CB8
	public static void Play(this AudioClip clip, Vector3 position, Quaternion rotation, float volume, float pitch, AudioRolloffMode rolloffMode, float minDistance, float maxDistance, float dopplerLevel, float spread)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.doppler = dopplerLevel;
		def.min = minDistance;
		def.max = maxDistance;
		def.mode = rolloffMode;
		def.spread = spread;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x0600051B RID: 1307 RVA: 0x00019B28 File Offset: 0x00017D28
	public static void Play(this AudioClip clip, Vector3 position, Quaternion rotation, float volume, float pitch, AudioRolloffMode rolloffMode, float minDistance, float maxDistance, float dopplerLevel, float spread, bool bypassEffects)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.doppler = dopplerLevel;
		def.min = minDistance;
		def.max = maxDistance;
		def.spread = spread;
		def.bypassEffects = bypassEffects;
		def.mode = rolloffMode;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x0600051C RID: 1308 RVA: 0x00019BA4 File Offset: 0x00017DA4
	public static void Play(this AudioClip clip, Vector3 position)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.localPosition = position;
		def.clip = clip;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x0600051D RID: 1309 RVA: 0x00019BD0 File Offset: 0x00017DD0
	public static void Play(this AudioClip clip, Vector3 position, float volume)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x0600051E RID: 1310 RVA: 0x00019C04 File Offset: 0x00017E04
	public static void Play(this AudioClip clip, Vector3 position, float volume, float pitch)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x0600051F RID: 1311 RVA: 0x00019C40 File Offset: 0x00017E40
	public static void Play(this AudioClip clip, Vector3 position, float volume, float minDistance, float maxDistance)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.min = minDistance;
		def.max = maxDistance;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000520 RID: 1312 RVA: 0x00019C84 File Offset: 0x00017E84
	public static void Play(this AudioClip clip, Vector3 position, float volume, float minDistance, float maxDistance, int priority)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.min = minDistance;
		def.max = maxDistance;
		def.priority = priority;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000521 RID: 1313 RVA: 0x00019CD0 File Offset: 0x00017ED0
	public static void Play(this AudioClip clip, Vector3 position, float volume, float pitch, float minDistance, float maxDistance, int priority)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.min = minDistance;
		def.max = maxDistance;
		def.priority = priority;
		def.pitch = pitch;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000522 RID: 1314 RVA: 0x00019D28 File Offset: 0x00017F28
	public static void Play(this AudioClip clip, Vector3 position, float volume, AudioRolloffMode rolloffMode, float minDistance, float maxDistance)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.min = minDistance;
		def.max = maxDistance;
		def.mode = rolloffMode;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000523 RID: 1315 RVA: 0x00019D74 File Offset: 0x00017F74
	public static void Play(this AudioClip clip, Vector3 position, float volume, float pitch, float minDistance, float maxDistance)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.min = minDistance;
		def.max = maxDistance;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000524 RID: 1316 RVA: 0x00019DC0 File Offset: 0x00017FC0
	public static void Play(this AudioClip clip, Vector3 position, float volume, float pitch, float minDistance, float maxDistance, float dopplerLevel)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.doppler = dopplerLevel;
		def.min = minDistance;
		def.max = maxDistance;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000525 RID: 1317 RVA: 0x00019E18 File Offset: 0x00018018
	public static void Play(this AudioClip clip, Vector3 position, float volume, float pitch, float minDistance, float maxDistance, float dopplerLevel, float spread)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.doppler = dopplerLevel;
		def.min = minDistance;
		def.max = maxDistance;
		def.spread = spread;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000526 RID: 1318 RVA: 0x00019E78 File Offset: 0x00018078
	public static void Play(this AudioClip clip, Vector3 position, float volume, float pitch, float minDistance, float maxDistance, float dopplerLevel, float spread, bool bypassEffects)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.doppler = dopplerLevel;
		def.min = minDistance;
		def.max = maxDistance;
		def.spread = spread;
		def.bypassEffects = bypassEffects;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000527 RID: 1319 RVA: 0x00019EE0 File Offset: 0x000180E0
	public static void Play(this AudioClip clip, Vector3 position, float volume, float pitch, AudioRolloffMode rolloffMode, float minDistance, float maxDistance, float dopplerLevel)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.doppler = dopplerLevel;
		def.min = minDistance;
		def.max = maxDistance;
		def.mode = rolloffMode;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000528 RID: 1320 RVA: 0x00019F40 File Offset: 0x00018140
	public static void Play(this AudioClip clip, Vector3 position, float volume, float pitch, AudioRolloffMode rolloffMode, float minDistance, float maxDistance, float dopplerLevel, float spread)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.doppler = dopplerLevel;
		def.min = minDistance;
		def.max = maxDistance;
		def.mode = rolloffMode;
		def.spread = spread;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000529 RID: 1321 RVA: 0x00019FA8 File Offset: 0x000181A8
	public static void Play(this AudioClip clip, Vector3 position, float volume, float pitch, AudioRolloffMode rolloffMode, float minDistance, float maxDistance, float dopplerLevel, float spread, bool bypassEffects)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.doppler = dopplerLevel;
		def.min = minDistance;
		def.max = maxDistance;
		def.spread = spread;
		def.bypassEffects = bypassEffects;
		def.mode = rolloffMode;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x0600052A RID: 1322 RVA: 0x0001A018 File Offset: 0x00018218
	public static void Play(this AudioClip clip)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 1;
		def.clip = clip;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x0600052B RID: 1323 RVA: 0x0001A044 File Offset: 0x00018244
	public static void Play(this AudioClip clip, float volume)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 1;
		def.clip = clip;
		def.volume = volume;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x0600052C RID: 1324 RVA: 0x0001A078 File Offset: 0x00018278
	public static void Play(this AudioClip clip, float volume, float pan)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 1;
		def.clip = clip;
		def.volume = volume;
		def.pan = pan;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x0600052D RID: 1325 RVA: 0x0001A0B4 File Offset: 0x000182B4
	public static void Play(this AudioClip clip, float volume, float pan, float pitch)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 1;
		def.clip = clip;
		def.volume = volume;
		def.pan = pan;
		def.pitch = pitch;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x0600052E RID: 1326 RVA: 0x0001A0F8 File Offset: 0x000182F8
	public static void Play(this AudioClip clip, float volume, float pan, Vector3 worldPosition)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 5;
		def.clip = clip;
		def.volume = volume;
		def.pan = pan;
		def.localPosition = worldPosition;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x0600052F RID: 1327 RVA: 0x0001A13C File Offset: 0x0001833C
	public static void Play(this AudioClip clip, float volume, float pan, Vector3 worldPosition, Quaternion worldRotation)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 5;
		def.clip = clip;
		def.volume = volume;
		def.pan = pan;
		def.localPosition = worldPosition;
		def.localRotation = worldRotation;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000530 RID: 1328 RVA: 0x0001A188 File Offset: 0x00018388
	public static void PlayLocal(this AudioClip clip, float volume, float pan, Vector3 worldPosition)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 1;
		def.clip = clip;
		def.volume = volume;
		def.pan = pan;
		def.localPosition = worldPosition;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000531 RID: 1329 RVA: 0x0001A1CC File Offset: 0x000183CC
	public static void PlayLocal(this AudioClip clip, float volume, float pan, Vector3 worldPosition, Quaternion worldRotation)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 1;
		def.clip = clip;
		def.volume = volume;
		def.pan = pan;
		def.localPosition = worldPosition;
		def.localRotation = worldRotation;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000532 RID: 1330 RVA: 0x0001A218 File Offset: 0x00018418
	public static void Play(this AudioClip clip, Transform on)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 2;
		def.parent = on;
		def.clip = clip;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000533 RID: 1331 RVA: 0x0001A24C File Offset: 0x0001844C
	public static void Play(this AudioClip clip, Transform on, float volume)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 2;
		def.parent = on;
		def.clip = clip;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000534 RID: 1332 RVA: 0x0001A280 File Offset: 0x00018480
	public static void Play(this AudioClip clip, Transform on, Vector3 position, Quaternion rotation)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 6;
		def.parent = on;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000535 RID: 1333 RVA: 0x0001A2C4 File Offset: 0x000184C4
	public static void Play(this AudioClip clip, Transform on, Vector3 position, Quaternion rotation, float volume)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 6;
		def.parent = on;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000536 RID: 1334 RVA: 0x0001A310 File Offset: 0x00018510
	public static void Play(this AudioClip clip, Transform on, Vector3 position, Quaternion rotation, float volume, float pitch)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 6;
		def.parent = on;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000537 RID: 1335 RVA: 0x0001A364 File Offset: 0x00018564
	public static void Play(this AudioClip clip, Transform on, Vector3 position, Quaternion rotation, float volume, float minDistance, float maxDistance)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 6;
		def.parent = on;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		def.min = minDistance;
		def.max = maxDistance;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000538 RID: 1336 RVA: 0x0001A3C4 File Offset: 0x000185C4
	public static void Play(this AudioClip clip, Transform on, Vector3 position, Quaternion rotation, float volume, AudioRolloffMode rolloffMode, float minDistance, float maxDistance)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 6;
		def.parent = on;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		def.min = minDistance;
		def.max = maxDistance;
		def.mode = rolloffMode;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000539 RID: 1337 RVA: 0x0001A42C File Offset: 0x0001862C
	public static void Play(this AudioClip clip, Transform on, Vector3 position, Quaternion rotation, float volume, float pitch, float minDistance, float maxDistance)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 6;
		def.parent = on;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.min = minDistance;
		def.max = maxDistance;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x0600053A RID: 1338 RVA: 0x0001A494 File Offset: 0x00018694
	public static void Play(this AudioClip clip, Transform on, Vector3 position, Quaternion rotation, float volume, float pitch, float minDistance, float maxDistance, float dopplerLevel)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 6;
		def.parent = on;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.doppler = dopplerLevel;
		def.min = minDistance;
		def.max = maxDistance;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x0600053B RID: 1339 RVA: 0x0001A504 File Offset: 0x00018704
	public static void Play(this AudioClip clip, Transform on, Vector3 position, Quaternion rotation, float volume, float pitch, float minDistance, float maxDistance, float dopplerLevel, float spread)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 6;
		def.parent = on;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.doppler = dopplerLevel;
		def.min = minDistance;
		def.max = maxDistance;
		def.spread = spread;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x0600053C RID: 1340 RVA: 0x0001A57C File Offset: 0x0001877C
	public static void Play(this AudioClip clip, Transform on, Vector3 position, Quaternion rotation, float volume, float pitch, float minDistance, float maxDistance, float dopplerLevel, float spread, bool bypassEffects)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 6;
		def.parent = on;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.doppler = dopplerLevel;
		def.min = minDistance;
		def.max = maxDistance;
		def.spread = spread;
		def.bypassEffects = bypassEffects;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x0600053D RID: 1341 RVA: 0x0001A600 File Offset: 0x00018800
	public static void Play(this AudioClip clip, Transform on, Vector3 position, Quaternion rotation, float volume, float pitch, AudioRolloffMode rolloffMode, float minDistance, float maxDistance, float dopplerLevel)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 6;
		def.parent = on;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.doppler = dopplerLevel;
		def.min = minDistance;
		def.max = maxDistance;
		def.mode = rolloffMode;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x0600053E RID: 1342 RVA: 0x0001A678 File Offset: 0x00018878
	public static void Play(this AudioClip clip, Transform on, Vector3 position, Quaternion rotation, float volume, float pitch, AudioRolloffMode rolloffMode, float minDistance, float maxDistance, float dopplerLevel, float spread)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 6;
		def.parent = on;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.doppler = dopplerLevel;
		def.min = minDistance;
		def.max = maxDistance;
		def.mode = rolloffMode;
		def.spread = spread;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x0600053F RID: 1343 RVA: 0x0001A6FC File Offset: 0x000188FC
	public static void Play(this AudioClip clip, Transform on, Vector3 position, Quaternion rotation, float volume, float pitch, AudioRolloffMode rolloffMode, float minDistance, float maxDistance, float dopplerLevel, float spread, bool bypassEffects)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 6;
		def.parent = on;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.doppler = dopplerLevel;
		def.min = minDistance;
		def.max = maxDistance;
		def.spread = spread;
		def.bypassEffects = bypassEffects;
		def.mode = rolloffMode;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000540 RID: 1344 RVA: 0x0001A788 File Offset: 0x00018988
	public static void Play(this AudioClip clip, Transform on, Vector3 position)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 6;
		def.parent = on;
		def.localPosition = position;
		def.clip = clip;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000541 RID: 1345 RVA: 0x0001A7C4 File Offset: 0x000189C4
	public static void Play(this AudioClip clip, Transform on, Vector3 position, float volume)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 6;
		def.parent = on;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000542 RID: 1346 RVA: 0x0001A808 File Offset: 0x00018A08
	public static void Play(this AudioClip clip, Transform on, Vector3 position, float volume, float pitch)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 6;
		def.parent = on;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000543 RID: 1347 RVA: 0x0001A854 File Offset: 0x00018A54
	public static void Play(this AudioClip clip, Transform on, Vector3 position, float volume, float minDistance, float maxDistance)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 6;
		def.parent = on;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.min = minDistance;
		def.max = maxDistance;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000544 RID: 1348 RVA: 0x0001A8A8 File Offset: 0x00018AA8
	public static void Play(this AudioClip clip, Transform on, Vector3 position, float volume, AudioRolloffMode rolloffMode, float minDistance, float maxDistance)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 6;
		def.parent = on;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.min = minDistance;
		def.max = maxDistance;
		def.mode = rolloffMode;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000545 RID: 1349 RVA: 0x0001A908 File Offset: 0x00018B08
	public static void Play(this AudioClip clip, Transform on, Vector3 position, float volume, float pitch, float minDistance, float maxDistance)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 6;
		def.parent = on;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.min = minDistance;
		def.max = maxDistance;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000546 RID: 1350 RVA: 0x0001A968 File Offset: 0x00018B68
	public static void Play(this AudioClip clip, Transform on, Vector3 position, float volume, float pitch, float minDistance, float maxDistance, float dopplerLevel)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 6;
		def.parent = on;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.doppler = dopplerLevel;
		def.min = minDistance;
		def.max = maxDistance;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000547 RID: 1351 RVA: 0x0001A9D0 File Offset: 0x00018BD0
	public static void Play(this AudioClip clip, Transform on, Vector3 position, float volume, float pitch, float minDistance, float maxDistance, float dopplerLevel, float spread)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 6;
		def.parent = on;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.doppler = dopplerLevel;
		def.min = minDistance;
		def.max = maxDistance;
		def.spread = spread;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000548 RID: 1352 RVA: 0x0001AA40 File Offset: 0x00018C40
	public static void Play(this AudioClip clip, Transform on, Vector3 position, float volume, float pitch, float minDistance, float maxDistance, float dopplerLevel, float spread, bool bypassEffects)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 6;
		def.parent = on;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.doppler = dopplerLevel;
		def.min = minDistance;
		def.max = maxDistance;
		def.spread = spread;
		def.bypassEffects = bypassEffects;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000549 RID: 1353 RVA: 0x0001AAB8 File Offset: 0x00018CB8
	public static void Play(this AudioClip clip, Transform on, Vector3 position, float volume, float pitch, AudioRolloffMode rolloffMode, float minDistance, float maxDistance, float dopplerLevel)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 6;
		def.parent = on;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.doppler = dopplerLevel;
		def.min = minDistance;
		def.max = maxDistance;
		def.mode = rolloffMode;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x0600054A RID: 1354 RVA: 0x0001AB28 File Offset: 0x00018D28
	public static void Play(this AudioClip clip, Transform on, Vector3 position, float volume, float pitch, AudioRolloffMode rolloffMode, float minDistance, float maxDistance, float dopplerLevel, float spread)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 6;
		def.parent = on;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.doppler = dopplerLevel;
		def.min = minDistance;
		def.max = maxDistance;
		def.mode = rolloffMode;
		def.spread = spread;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x0600054B RID: 1355 RVA: 0x0001ABA0 File Offset: 0x00018DA0
	public static void Play(this AudioClip clip, Transform on, Vector3 position, float volume, float pitch, AudioRolloffMode rolloffMode, float minDistance, float maxDistance, float dopplerLevel, float spread, bool bypassEffects)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 6;
		def.parent = on;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.doppler = dopplerLevel;
		def.min = minDistance;
		def.max = maxDistance;
		def.spread = spread;
		def.bypassEffects = bypassEffects;
		def.mode = rolloffMode;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x0600054C RID: 1356 RVA: 0x0001AC24 File Offset: 0x00018E24
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, Quaternion rotation)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 2;
		def.parent = on;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x0600054D RID: 1357 RVA: 0x0001AC68 File Offset: 0x00018E68
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, Quaternion rotation, float volume)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 2;
		def.parent = on;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x0600054E RID: 1358 RVA: 0x0001ACB4 File Offset: 0x00018EB4
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, Quaternion rotation, float volume, float pitch)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 2;
		def.parent = on;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x0600054F RID: 1359 RVA: 0x0001AD08 File Offset: 0x00018F08
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, Quaternion rotation, float volume, float minDistance, float maxDistance)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 2;
		def.parent = on;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		def.min = minDistance;
		def.max = maxDistance;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000550 RID: 1360 RVA: 0x0001AD68 File Offset: 0x00018F68
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, Quaternion rotation, float volume, AudioRolloffMode rolloffMode, float minDistance, float maxDistance)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 2;
		def.parent = on;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		def.min = minDistance;
		def.max = maxDistance;
		def.mode = rolloffMode;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000551 RID: 1361 RVA: 0x0001ADD0 File Offset: 0x00018FD0
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, Quaternion rotation, float volume, float pitch, float minDistance, float maxDistance)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 2;
		def.parent = on;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.min = minDistance;
		def.max = maxDistance;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000552 RID: 1362 RVA: 0x0001AE38 File Offset: 0x00019038
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, Quaternion rotation, float volume, float pitch, float minDistance, float maxDistance, float dopplerLevel)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 2;
		def.parent = on;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.doppler = dopplerLevel;
		def.min = minDistance;
		def.max = maxDistance;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000553 RID: 1363 RVA: 0x0001AEA8 File Offset: 0x000190A8
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, Quaternion rotation, float volume, float pitch, float minDistance, float maxDistance, float dopplerLevel, float spread)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 2;
		def.parent = on;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.doppler = dopplerLevel;
		def.min = minDistance;
		def.max = maxDistance;
		def.spread = spread;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000554 RID: 1364 RVA: 0x0001AF20 File Offset: 0x00019120
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, Quaternion rotation, float volume, float pitch, float minDistance, float maxDistance, float dopplerLevel, float spread, bool bypassEffects)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 2;
		def.parent = on;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.doppler = dopplerLevel;
		def.min = minDistance;
		def.max = maxDistance;
		def.spread = spread;
		def.bypassEffects = bypassEffects;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000555 RID: 1365 RVA: 0x0001AFA4 File Offset: 0x000191A4
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, Quaternion rotation, float volume, float pitch, AudioRolloffMode rolloffMode, float minDistance, float maxDistance, float dopplerLevel)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 2;
		def.parent = on;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.doppler = dopplerLevel;
		def.min = minDistance;
		def.max = maxDistance;
		def.mode = rolloffMode;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000556 RID: 1366 RVA: 0x0001B01C File Offset: 0x0001921C
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, Quaternion rotation, float volume, float pitch, AudioRolloffMode rolloffMode, float minDistance, float maxDistance, float dopplerLevel, float spread)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 2;
		def.parent = on;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.doppler = dopplerLevel;
		def.min = minDistance;
		def.max = maxDistance;
		def.mode = rolloffMode;
		def.spread = spread;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000557 RID: 1367 RVA: 0x0001B0A0 File Offset: 0x000192A0
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, Quaternion rotation, float volume, float pitch, AudioRolloffMode rolloffMode, float minDistance, float maxDistance, float dopplerLevel, float spread, bool bypassEffects)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 2;
		def.parent = on;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.doppler = dopplerLevel;
		def.min = minDistance;
		def.max = maxDistance;
		def.spread = spread;
		def.bypassEffects = bypassEffects;
		def.mode = rolloffMode;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000558 RID: 1368 RVA: 0x0001B12C File Offset: 0x0001932C
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 2;
		def.parent = on;
		def.localPosition = position;
		def.clip = clip;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000559 RID: 1369 RVA: 0x0001B168 File Offset: 0x00019368
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, float volume)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 2;
		def.parent = on;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x0600055A RID: 1370 RVA: 0x0001B1AC File Offset: 0x000193AC
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, float volume, int priority)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 2;
		def.parent = on;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.priority = priority;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x0600055B RID: 1371 RVA: 0x0001B1F8 File Offset: 0x000193F8
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, float volume, float pitch)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 2;
		def.parent = on;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x0600055C RID: 1372 RVA: 0x0001B244 File Offset: 0x00019444
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, float volume, float minDistance, float maxDistance)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 2;
		def.parent = on;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.min = minDistance;
		def.max = maxDistance;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x0600055D RID: 1373 RVA: 0x0001B298 File Offset: 0x00019498
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, float volume, AudioRolloffMode rolloffMode, float minDistance, float maxDistance)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 2;
		def.parent = on;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.min = minDistance;
		def.max = maxDistance;
		def.mode = rolloffMode;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x0600055E RID: 1374 RVA: 0x0001B2F8 File Offset: 0x000194F8
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, float volume, float pitch, float minDistance, float maxDistance)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 2;
		def.parent = on;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.min = minDistance;
		def.max = maxDistance;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x0600055F RID: 1375 RVA: 0x0001B358 File Offset: 0x00019558
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, float volume, float pitch, float minDistance, float maxDistance, int priority)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 2;
		def.parent = on;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.min = minDistance;
		def.max = maxDistance;
		def.priority = priority;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000560 RID: 1376 RVA: 0x0001B3C0 File Offset: 0x000195C0
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, float volume, float pitch, float minDistance, float maxDistance, float dopplerLevel)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 2;
		def.parent = on;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.doppler = dopplerLevel;
		def.min = minDistance;
		def.max = maxDistance;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000561 RID: 1377 RVA: 0x0001B428 File Offset: 0x00019628
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, float volume, float pitch, float minDistance, float maxDistance, float dopplerLevel, float spread)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 2;
		def.parent = on;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.doppler = dopplerLevel;
		def.min = minDistance;
		def.max = maxDistance;
		def.spread = spread;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000562 RID: 1378 RVA: 0x0001B498 File Offset: 0x00019698
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, float volume, float pitch, float minDistance, float maxDistance, float dopplerLevel, float spread, bool bypassEffects)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 2;
		def.parent = on;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.doppler = dopplerLevel;
		def.min = minDistance;
		def.max = maxDistance;
		def.spread = spread;
		def.bypassEffects = bypassEffects;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000563 RID: 1379 RVA: 0x0001B510 File Offset: 0x00019710
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, float volume, float pitch, AudioRolloffMode rolloffMode, float minDistance, float maxDistance, float dopplerLevel)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 2;
		def.parent = on;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.doppler = dopplerLevel;
		def.min = minDistance;
		def.max = maxDistance;
		def.mode = rolloffMode;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000564 RID: 1380 RVA: 0x0001B580 File Offset: 0x00019780
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, float volume, float pitch, AudioRolloffMode rolloffMode, float minDistance, float maxDistance, float dopplerLevel, float spread)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 2;
		def.parent = on;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.doppler = dopplerLevel;
		def.min = minDistance;
		def.max = maxDistance;
		def.mode = rolloffMode;
		def.spread = spread;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x06000565 RID: 1381 RVA: 0x0001B5F8 File Offset: 0x000197F8
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, float volume, float pitch, AudioRolloffMode rolloffMode, float minDistance, float maxDistance, float dopplerLevel, float spread, bool bypassEffects)
	{
		global::SoundPool.Settings def = global::SoundPool.DEF;
		def.SelectRoot = 2;
		def.parent = on;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.doppler = dopplerLevel;
		def.min = minDistance;
		def.max = maxDistance;
		def.spread = spread;
		def.bypassEffects = bypassEffects;
		def.mode = rolloffMode;
		global::SoundPool.Play(ref def);
	}

	// Token: 0x040004A3 RID: 1187
	private const sbyte SelectRoot_Attach = 2;

	// Token: 0x040004A4 RID: 1188
	private const sbyte SelectRoot_Camera = 1;

	// Token: 0x040004A5 RID: 1189
	private const sbyte SelectRoot_Default = 0;

	// Token: 0x040004A6 RID: 1190
	private const sbyte SelectRoot_Camera_WorldOffset = 5;

	// Token: 0x040004A7 RID: 1191
	private const sbyte SelectRoot_Attach_WorldOffset = 6;

	// Token: 0x040004A8 RID: 1192
	private const string goName = "zzz-soundpoolnode";

	// Token: 0x040004A9 RID: 1193
	private const float logarithmicMaxScale = 2f;

	// Token: 0x040004AA RID: 1194
	private static bool _enabled;

	// Token: 0x040004AB RID: 1195
	private static bool _quitting;

	// Token: 0x040004AC RID: 1196
	private static readonly global::SoundPool.Root playingAttached = new global::SoundPool.Root(global::SoundPool.RootID.PLAYING_ATTACHED);

	// Token: 0x040004AD RID: 1197
	private static readonly global::SoundPool.Root playingCamera = new global::SoundPool.Root(global::SoundPool.RootID.PLAYING_CAMERA);

	// Token: 0x040004AE RID: 1198
	private static readonly global::SoundPool.Root playing = new global::SoundPool.Root(global::SoundPool.RootID.PLAYING);

	// Token: 0x040004AF RID: 1199
	private static readonly global::SoundPool.Root reserved = new global::SoundPool.Root(global::SoundPool.RootID.RESERVED);

	// Token: 0x040004B0 RID: 1200
	private static bool firstLeak = false;

	// Token: 0x040004B1 RID: 1201
	private static bool hadFirstLeak;

	// Token: 0x040004B2 RID: 1202
	private static readonly Type[] goTypes = new Type[]
	{
		typeof(AudioSource)
	};

	// Token: 0x040004B3 RID: 1203
	private static readonly global::SoundPool.Settings DEF = new global::SoundPool.Settings
	{
		volume = 1f,
		pitch = 1f,
		mode = 1,
		min = 1f,
		max = 500f,
		panLevel = 1f,
		doppler = 1f,
		priority = 128,
		localRotation = Quaternion.identity
	};

	// Token: 0x020000EF RID: 239
	private struct Settings
	{
		// Token: 0x06000566 RID: 1382 RVA: 0x0001B67C File Offset: 0x0001987C
		public static explicit operator global::SoundPool.Settings(global::SoundPool.PlayerShared player)
		{
			global::SoundPool.Settings def = global::SoundPool.DEF;
			def.clip = player.clip;
			def.volume = player.volume;
			def.pitch = player.pitch;
			def.priority = player.priority;
			return def;
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0001B6C8 File Offset: 0x000198C8
		public static explicit operator global::SoundPool.Settings(global::SoundPool.Player3D player)
		{
			global::SoundPool.Settings result = (global::SoundPool.Settings)player.super;
			result.doppler = player.dopplerLevel;
			result.min = player.minDistance;
			result.max = player.maxDistance;
			result.panLevel = player.panLevel;
			result.spread = player.spread;
			result.mode = player.rolloffMode;
			result.bypassEffects = player.bypassEffects;
			result.SelectRoot = ((!player.cameraSticky) ? 0 : 5);
			return result;
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0001B760 File Offset: 0x00019960
		public static explicit operator global::SoundPool.Settings(global::SoundPool.PlayerLocal player)
		{
			global::SoundPool.Settings result = (global::SoundPool.Settings)player.super;
			result.localPosition = player.localPosition;
			result.localRotation = player.localRotation;
			return result;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0001B798 File Offset: 0x00019998
		public static explicit operator global::SoundPool.Settings(global::SoundPool.Player2D player)
		{
			global::SoundPool.Settings result = (global::SoundPool.Settings)player.super;
			result.pan = player.pan;
			return result;
		}

		// Token: 0x040004B4 RID: 1204
		public AudioClip clip;

		// Token: 0x040004B5 RID: 1205
		public Transform parent;

		// Token: 0x040004B6 RID: 1206
		public Quaternion localRotation;

		// Token: 0x040004B7 RID: 1207
		public Vector3 localPosition;

		// Token: 0x040004B8 RID: 1208
		public float volume;

		// Token: 0x040004B9 RID: 1209
		public float pitch;

		// Token: 0x040004BA RID: 1210
		public float pan;

		// Token: 0x040004BB RID: 1211
		public float panLevel;

		// Token: 0x040004BC RID: 1212
		public float min;

		// Token: 0x040004BD RID: 1213
		public float max;

		// Token: 0x040004BE RID: 1214
		public float doppler;

		// Token: 0x040004BF RID: 1215
		public float spread;

		// Token: 0x040004C0 RID: 1216
		public int priority;

		// Token: 0x040004C1 RID: 1217
		public AudioRolloffMode mode;

		// Token: 0x040004C2 RID: 1218
		public sbyte SelectRoot;

		// Token: 0x040004C3 RID: 1219
		public bool bypassEffects;
	}

	// Token: 0x020000F0 RID: 240
	public struct PlayerShared
	{
		// Token: 0x0600056A RID: 1386 RVA: 0x0001B7C4 File Offset: 0x000199C4
		public PlayerShared(AudioClip clip)
		{
			this.clip = clip;
			this.volume = global::SoundPool.DEF.volume;
			this.pitch = global::SoundPool.DEF.pitch;
			this.priority = global::SoundPool.DEF.priority;
		}

		// Token: 0x040004C4 RID: 1220
		public static readonly global::SoundPool.PlayerShared Default = new global::SoundPool.PlayerShared
		{
			volume = global::SoundPool.DEF.volume,
			pitch = global::SoundPool.DEF.pitch,
			priority = global::SoundPool.DEF.priority
		};

		// Token: 0x040004C5 RID: 1221
		public AudioClip clip;

		// Token: 0x040004C6 RID: 1222
		public float volume;

		// Token: 0x040004C7 RID: 1223
		public float pitch;

		// Token: 0x040004C8 RID: 1224
		public int priority;
	}

	// Token: 0x020000F1 RID: 241
	public struct Player3D
	{
		// Token: 0x0600056C RID: 1388 RVA: 0x0001B864 File Offset: 0x00019A64
		public Player3D(AudioClip clip)
		{
			this.super = new global::SoundPool.PlayerShared(clip);
			this.minDistance = global::SoundPool.DEF.min;
			this.maxDistance = global::SoundPool.DEF.max;
			this.spread = global::SoundPool.DEF.spread;
			this.dopplerLevel = global::SoundPool.DEF.doppler;
			this.panLevel = global::SoundPool.DEF.panLevel;
			this.rolloffMode = global::SoundPool.DEF.mode;
			this.bypassEffects = global::SoundPool.DEF.bypassEffects;
			this.cameraSticky = false;
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x0001B9AC File Offset: 0x00019BAC
		// (set) Token: 0x0600056F RID: 1391 RVA: 0x0001B9BC File Offset: 0x00019BBC
		public float volume
		{
			get
			{
				return this.super.volume;
			}
			set
			{
				this.super.volume = value;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x0001B9CC File Offset: 0x00019BCC
		// (set) Token: 0x06000571 RID: 1393 RVA: 0x0001B9DC File Offset: 0x00019BDC
		public float pitch
		{
			get
			{
				return this.super.pitch;
			}
			set
			{
				this.super.pitch = value;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x0001B9EC File Offset: 0x00019BEC
		// (set) Token: 0x06000573 RID: 1395 RVA: 0x0001B9FC File Offset: 0x00019BFC
		public int priority
		{
			get
			{
				return this.super.priority;
			}
			set
			{
				this.super.priority = value;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x0001BA0C File Offset: 0x00019C0C
		// (set) Token: 0x06000575 RID: 1397 RVA: 0x0001BA1C File Offset: 0x00019C1C
		public AudioClip clip
		{
			get
			{
				return this.super.clip;
			}
			set
			{
				this.super.clip = value;
			}
		}

		// Token: 0x040004C9 RID: 1225
		public static readonly global::SoundPool.Player3D Default = new global::SoundPool.Player3D
		{
			super = global::SoundPool.PlayerShared.Default,
			minDistance = global::SoundPool.DEF.min,
			maxDistance = global::SoundPool.DEF.max,
			rolloffMode = global::SoundPool.DEF.mode,
			spread = global::SoundPool.DEF.spread,
			dopplerLevel = global::SoundPool.DEF.doppler,
			bypassEffects = global::SoundPool.DEF.bypassEffects,
			panLevel = global::SoundPool.DEF.panLevel
		};

		// Token: 0x040004CA RID: 1226
		public global::SoundPool.PlayerShared super;

		// Token: 0x040004CB RID: 1227
		public float minDistance;

		// Token: 0x040004CC RID: 1228
		public float maxDistance;

		// Token: 0x040004CD RID: 1229
		public float spread;

		// Token: 0x040004CE RID: 1230
		public float dopplerLevel;

		// Token: 0x040004CF RID: 1231
		public float panLevel;

		// Token: 0x040004D0 RID: 1232
		public AudioRolloffMode rolloffMode;

		// Token: 0x040004D1 RID: 1233
		public bool cameraSticky;

		// Token: 0x040004D2 RID: 1234
		public bool bypassEffects;
	}

	// Token: 0x020000F2 RID: 242
	public struct PlayerWorld
	{
		// Token: 0x06000576 RID: 1398 RVA: 0x0001BA2C File Offset: 0x00019C2C
		public PlayerWorld(AudioClip clip)
		{
			this.super = new global::SoundPool.Player3D(clip);
			this.position = default(Vector3);
			this.rotation = Quaternion.identity;
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x0001BAA4 File Offset: 0x00019CA4
		// (set) Token: 0x06000579 RID: 1401 RVA: 0x0001BAB4 File Offset: 0x00019CB4
		public float volume
		{
			get
			{
				return this.super.volume;
			}
			set
			{
				this.super.volume = value;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x0001BAC4 File Offset: 0x00019CC4
		// (set) Token: 0x0600057B RID: 1403 RVA: 0x0001BAD4 File Offset: 0x00019CD4
		public float pitch
		{
			get
			{
				return this.super.pitch;
			}
			set
			{
				this.super.pitch = value;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x0001BAE4 File Offset: 0x00019CE4
		// (set) Token: 0x0600057D RID: 1405 RVA: 0x0001BAF4 File Offset: 0x00019CF4
		public int priority
		{
			get
			{
				return this.super.priority;
			}
			set
			{
				this.super.priority = value;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x0001BB04 File Offset: 0x00019D04
		// (set) Token: 0x0600057F RID: 1407 RVA: 0x0001BB14 File Offset: 0x00019D14
		public AudioClip clip
		{
			get
			{
				return this.super.clip;
			}
			set
			{
				this.super.clip = value;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x0001BB24 File Offset: 0x00019D24
		// (set) Token: 0x06000581 RID: 1409 RVA: 0x0001BB34 File Offset: 0x00019D34
		public float minDistance
		{
			get
			{
				return this.super.minDistance;
			}
			set
			{
				this.super.minDistance = value;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x0001BB44 File Offset: 0x00019D44
		// (set) Token: 0x06000583 RID: 1411 RVA: 0x0001BB54 File Offset: 0x00019D54
		public float maxDistance
		{
			get
			{
				return this.super.maxDistance;
			}
			set
			{
				this.super.maxDistance = value;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x0001BB64 File Offset: 0x00019D64
		// (set) Token: 0x06000585 RID: 1413 RVA: 0x0001BB74 File Offset: 0x00019D74
		public float spread
		{
			get
			{
				return this.super.spread;
			}
			set
			{
				this.super.spread = value;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x0001BB84 File Offset: 0x00019D84
		// (set) Token: 0x06000587 RID: 1415 RVA: 0x0001BB94 File Offset: 0x00019D94
		public float dopplerLevel
		{
			get
			{
				return this.super.dopplerLevel;
			}
			set
			{
				this.super.dopplerLevel = value;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x0001BBA4 File Offset: 0x00019DA4
		// (set) Token: 0x06000589 RID: 1417 RVA: 0x0001BBB4 File Offset: 0x00019DB4
		public float panLevel
		{
			get
			{
				return this.super.panLevel;
			}
			set
			{
				this.super.panLevel = value;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x0001BBC4 File Offset: 0x00019DC4
		// (set) Token: 0x0600058B RID: 1419 RVA: 0x0001BBD4 File Offset: 0x00019DD4
		public AudioRolloffMode rolloffMode
		{
			get
			{
				return this.super.rolloffMode;
			}
			set
			{
				this.super.rolloffMode = value;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x0001BBE4 File Offset: 0x00019DE4
		// (set) Token: 0x0600058D RID: 1421 RVA: 0x0001BBF4 File Offset: 0x00019DF4
		public bool bypassEffects
		{
			get
			{
				return this.super.bypassEffects;
			}
			set
			{
				this.super.bypassEffects = value;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x0001BC04 File Offset: 0x00019E04
		// (set) Token: 0x0600058F RID: 1423 RVA: 0x0001BC14 File Offset: 0x00019E14
		public bool cameraSticky
		{
			get
			{
				return this.super.cameraSticky;
			}
			set
			{
				this.super.cameraSticky = value;
			}
		}

		// Token: 0x040004D3 RID: 1235
		public static readonly global::SoundPool.PlayerWorld Default = new global::SoundPool.PlayerWorld
		{
			super = global::SoundPool.Player3D.Default,
			position = default(Vector3),
			rotation = Quaternion.identity
		};

		// Token: 0x040004D4 RID: 1236
		public global::SoundPool.Player3D super;

		// Token: 0x040004D5 RID: 1237
		public Vector3 position;

		// Token: 0x040004D6 RID: 1238
		public Quaternion rotation;
	}

	// Token: 0x020000F3 RID: 243
	public struct PlayerLocal
	{
		// Token: 0x06000590 RID: 1424 RVA: 0x0001BC24 File Offset: 0x00019E24
		public PlayerLocal(AudioClip clip)
		{
			this.super = new global::SoundPool.Player3D(clip);
			this.localPosition = default(Vector3);
			this.localRotation = Quaternion.identity;
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x0001BC9C File Offset: 0x00019E9C
		// (set) Token: 0x06000593 RID: 1427 RVA: 0x0001BCAC File Offset: 0x00019EAC
		public float volume
		{
			get
			{
				return this.super.volume;
			}
			set
			{
				this.super.volume = value;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x0001BCBC File Offset: 0x00019EBC
		// (set) Token: 0x06000595 RID: 1429 RVA: 0x0001BCCC File Offset: 0x00019ECC
		public float pitch
		{
			get
			{
				return this.super.pitch;
			}
			set
			{
				this.super.pitch = value;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x0001BCDC File Offset: 0x00019EDC
		// (set) Token: 0x06000597 RID: 1431 RVA: 0x0001BCEC File Offset: 0x00019EEC
		public int priority
		{
			get
			{
				return this.super.priority;
			}
			set
			{
				this.super.priority = value;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x0001BCFC File Offset: 0x00019EFC
		// (set) Token: 0x06000599 RID: 1433 RVA: 0x0001BD0C File Offset: 0x00019F0C
		public AudioClip clip
		{
			get
			{
				return this.super.clip;
			}
			set
			{
				this.super.clip = value;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x0001BD1C File Offset: 0x00019F1C
		// (set) Token: 0x0600059B RID: 1435 RVA: 0x0001BD2C File Offset: 0x00019F2C
		public float minDistance
		{
			get
			{
				return this.super.minDistance;
			}
			set
			{
				this.super.minDistance = value;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x0001BD3C File Offset: 0x00019F3C
		// (set) Token: 0x0600059D RID: 1437 RVA: 0x0001BD4C File Offset: 0x00019F4C
		public float maxDistance
		{
			get
			{
				return this.super.maxDistance;
			}
			set
			{
				this.super.maxDistance = value;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x0001BD5C File Offset: 0x00019F5C
		// (set) Token: 0x0600059F RID: 1439 RVA: 0x0001BD6C File Offset: 0x00019F6C
		public float spread
		{
			get
			{
				return this.super.spread;
			}
			set
			{
				this.super.spread = value;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x0001BD7C File Offset: 0x00019F7C
		// (set) Token: 0x060005A1 RID: 1441 RVA: 0x0001BD8C File Offset: 0x00019F8C
		public float dopplerLevel
		{
			get
			{
				return this.super.dopplerLevel;
			}
			set
			{
				this.super.dopplerLevel = value;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x0001BD9C File Offset: 0x00019F9C
		// (set) Token: 0x060005A3 RID: 1443 RVA: 0x0001BDAC File Offset: 0x00019FAC
		public float panLevel
		{
			get
			{
				return this.super.panLevel;
			}
			set
			{
				this.super.panLevel = value;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x0001BDBC File Offset: 0x00019FBC
		// (set) Token: 0x060005A5 RID: 1445 RVA: 0x0001BDCC File Offset: 0x00019FCC
		public AudioRolloffMode rolloffMode
		{
			get
			{
				return this.super.rolloffMode;
			}
			set
			{
				this.super.rolloffMode = value;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x0001BDDC File Offset: 0x00019FDC
		// (set) Token: 0x060005A7 RID: 1447 RVA: 0x0001BDEC File Offset: 0x00019FEC
		public bool bypassEffects
		{
			get
			{
				return this.super.bypassEffects;
			}
			set
			{
				this.super.bypassEffects = value;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x0001BDFC File Offset: 0x00019FFC
		// (set) Token: 0x060005A9 RID: 1449 RVA: 0x0001BE0C File Offset: 0x0001A00C
		public bool cameraSticky
		{
			get
			{
				return this.super.cameraSticky;
			}
			set
			{
				this.super.cameraSticky = value;
			}
		}

		// Token: 0x040004D7 RID: 1239
		public static readonly global::SoundPool.PlayerLocal Default = new global::SoundPool.PlayerLocal
		{
			super = global::SoundPool.Player3D.Default,
			localPosition = default(Vector3),
			localRotation = Quaternion.identity
		};

		// Token: 0x040004D8 RID: 1240
		public global::SoundPool.Player3D super;

		// Token: 0x040004D9 RID: 1241
		public Vector3 localPosition;

		// Token: 0x040004DA RID: 1242
		public Quaternion localRotation;
	}

	// Token: 0x020000F4 RID: 244
	public struct PlayerChild
	{
		// Token: 0x060005AA RID: 1450 RVA: 0x0001BE1C File Offset: 0x0001A01C
		public PlayerChild(AudioClip clip)
		{
			this.super = new global::SoundPool.PlayerLocal(clip);
			this.parent = null;
			this.unglue = false;
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x0001BE60 File Offset: 0x0001A060
		// (set) Token: 0x060005AD RID: 1453 RVA: 0x0001BE70 File Offset: 0x0001A070
		public float volume
		{
			get
			{
				return this.super.volume;
			}
			set
			{
				this.super.volume = value;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x0001BE80 File Offset: 0x0001A080
		// (set) Token: 0x060005AF RID: 1455 RVA: 0x0001BE90 File Offset: 0x0001A090
		public float pitch
		{
			get
			{
				return this.super.pitch;
			}
			set
			{
				this.super.pitch = value;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x0001BEA0 File Offset: 0x0001A0A0
		// (set) Token: 0x060005B1 RID: 1457 RVA: 0x0001BEB0 File Offset: 0x0001A0B0
		public int priority
		{
			get
			{
				return this.super.priority;
			}
			set
			{
				this.super.priority = value;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x0001BEC0 File Offset: 0x0001A0C0
		// (set) Token: 0x060005B3 RID: 1459 RVA: 0x0001BED0 File Offset: 0x0001A0D0
		public AudioClip clip
		{
			get
			{
				return this.super.clip;
			}
			set
			{
				this.super.clip = value;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x0001BEE0 File Offset: 0x0001A0E0
		// (set) Token: 0x060005B5 RID: 1461 RVA: 0x0001BEF0 File Offset: 0x0001A0F0
		public float minDistance
		{
			get
			{
				return this.super.minDistance;
			}
			set
			{
				this.super.minDistance = value;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x0001BF00 File Offset: 0x0001A100
		// (set) Token: 0x060005B7 RID: 1463 RVA: 0x0001BF10 File Offset: 0x0001A110
		public float maxDistance
		{
			get
			{
				return this.super.maxDistance;
			}
			set
			{
				this.super.maxDistance = value;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x0001BF20 File Offset: 0x0001A120
		// (set) Token: 0x060005B9 RID: 1465 RVA: 0x0001BF30 File Offset: 0x0001A130
		public float spread
		{
			get
			{
				return this.super.spread;
			}
			set
			{
				this.super.spread = value;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x0001BF40 File Offset: 0x0001A140
		// (set) Token: 0x060005BB RID: 1467 RVA: 0x0001BF50 File Offset: 0x0001A150
		public float dopplerLevel
		{
			get
			{
				return this.super.dopplerLevel;
			}
			set
			{
				this.super.dopplerLevel = value;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x0001BF60 File Offset: 0x0001A160
		// (set) Token: 0x060005BD RID: 1469 RVA: 0x0001BF70 File Offset: 0x0001A170
		public AudioRolloffMode rolloffMode
		{
			get
			{
				return this.super.rolloffMode;
			}
			set
			{
				this.super.rolloffMode = value;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x0001BF80 File Offset: 0x0001A180
		// (set) Token: 0x060005BF RID: 1471 RVA: 0x0001BF90 File Offset: 0x0001A190
		public bool bypassEffects
		{
			get
			{
				return this.super.bypassEffects;
			}
			set
			{
				this.super.bypassEffects = value;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x0001BFA0 File Offset: 0x0001A1A0
		// (set) Token: 0x060005C1 RID: 1473 RVA: 0x0001BFB0 File Offset: 0x0001A1B0
		public bool cameraSticky
		{
			get
			{
				return this.super.cameraSticky;
			}
			set
			{
				this.super.cameraSticky = value;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x0001BFC0 File Offset: 0x0001A1C0
		// (set) Token: 0x060005C3 RID: 1475 RVA: 0x0001BFD0 File Offset: 0x0001A1D0
		public Vector3 localPosition
		{
			get
			{
				return this.super.localPosition;
			}
			set
			{
				this.super.localPosition = value;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x0001BFE0 File Offset: 0x0001A1E0
		// (set) Token: 0x060005C5 RID: 1477 RVA: 0x0001BFF0 File Offset: 0x0001A1F0
		public Quaternion localRotation
		{
			get
			{
				return this.super.localRotation;
			}
			set
			{
				this.super.localRotation = value;
			}
		}

		// Token: 0x040004DB RID: 1243
		public static readonly global::SoundPool.PlayerChild Default = new global::SoundPool.PlayerChild
		{
			super = global::SoundPool.PlayerLocal.Default
		};

		// Token: 0x040004DC RID: 1244
		public global::SoundPool.PlayerLocal super;

		// Token: 0x040004DD RID: 1245
		public bool unglue;

		// Token: 0x040004DE RID: 1246
		public Transform parent;
	}

	// Token: 0x020000F5 RID: 245
	public struct Player2D
	{
		// Token: 0x060005C6 RID: 1478 RVA: 0x0001C000 File Offset: 0x0001A200
		public Player2D(AudioClip clip)
		{
			this.super = new global::SoundPool.PlayerShared(clip);
			this.pan = global::SoundPool.DEF.pan;
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x0001C064 File Offset: 0x0001A264
		// (set) Token: 0x060005C9 RID: 1481 RVA: 0x0001C074 File Offset: 0x0001A274
		public float volume
		{
			get
			{
				return this.super.volume;
			}
			set
			{
				this.super.volume = value;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060005CA RID: 1482 RVA: 0x0001C084 File Offset: 0x0001A284
		// (set) Token: 0x060005CB RID: 1483 RVA: 0x0001C094 File Offset: 0x0001A294
		public float pitch
		{
			get
			{
				return this.super.pitch;
			}
			set
			{
				this.super.pitch = value;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x0001C0A4 File Offset: 0x0001A2A4
		// (set) Token: 0x060005CD RID: 1485 RVA: 0x0001C0B4 File Offset: 0x0001A2B4
		public int priority
		{
			get
			{
				return this.super.priority;
			}
			set
			{
				this.super.priority = value;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x0001C0C4 File Offset: 0x0001A2C4
		// (set) Token: 0x060005CF RID: 1487 RVA: 0x0001C0D4 File Offset: 0x0001A2D4
		public AudioClip clip
		{
			get
			{
				return this.super.clip;
			}
			set
			{
				this.super.clip = value;
			}
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0001C0E4 File Offset: 0x0001A2E4
		public void Play()
		{
			this.Play(this.clip);
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x0001C0F4 File Offset: 0x0001A2F4
		public void Play(AudioClip clip)
		{
			if (!clip)
			{
				return;
			}
		}

		// Token: 0x040004DF RID: 1247
		public static readonly global::SoundPool.Player2D Default = new global::SoundPool.Player2D
		{
			super = global::SoundPool.PlayerShared.Default,
			pan = global::SoundPool.DEF.pan
		};

		// Token: 0x040004E0 RID: 1248
		public global::SoundPool.PlayerShared super;

		// Token: 0x040004E1 RID: 1249
		public float pan;
	}

	// Token: 0x020000F6 RID: 246
	private struct Dir
	{
		// Token: 0x040004E2 RID: 1250
		public global::SoundPool.Node node;

		// Token: 0x040004E3 RID: 1251
		public bool has;
	}

	// Token: 0x020000F7 RID: 247
	private struct Way
	{
		// Token: 0x040004E4 RID: 1252
		public global::SoundPool.Dir prev;

		// Token: 0x040004E5 RID: 1253
		public global::SoundPool.Dir next;
	}

	// Token: 0x020000F8 RID: 248
	private enum RootID : sbyte
	{
		// Token: 0x040004E7 RID: 1255
		LIMBO,
		// Token: 0x040004E8 RID: 1256
		PLAYING_ATTACHED = -3,
		// Token: 0x040004E9 RID: 1257
		PLAYING_CAMERA,
		// Token: 0x040004EA RID: 1258
		PLAYING,
		// Token: 0x040004EB RID: 1259
		RESERVED = 1,
		// Token: 0x040004EC RID: 1260
		DISPOSED
	}

	// Token: 0x020000F9 RID: 249
	private class Root
	{
		// Token: 0x060005D2 RID: 1490 RVA: 0x0001C104 File Offset: 0x0001A304
		public Root(global::SoundPool.RootID id)
		{
			this.id = id;
		}

		// Token: 0x040004ED RID: 1261
		public int count;

		// Token: 0x040004EE RID: 1262
		public global::SoundPool.Dir first;

		// Token: 0x040004EF RID: 1263
		public readonly global::SoundPool.RootID id;
	}

	// Token: 0x020000FA RID: 250
	private static class NodeGC
	{
		// Token: 0x060005D3 RID: 1491 RVA: 0x0001C114 File Offset: 0x0001A314
		public static void JOIN()
		{
			Transform[] array = null;
			bool flag = false;
			object destroyNextPumpLock = global::SoundPool.NodeGC.GCDAT.destroyNextPumpLock;
			lock (destroyNextPumpLock)
			{
				if (global::SoundPool.NodeGC.GCDAT.destroyNextQueued)
				{
					flag = true;
					array = global::SoundPool.NodeGC.GCDAT.destroyTheseNextPump.ToArray();
					global::SoundPool.NodeGC.GCDAT.destroyTheseNextPump.Clear();
					global::SoundPool.NodeGC.GCDAT.destroyNextQueued = false;
				}
			}
			if (flag)
			{
				foreach (Transform transform in array)
				{
					if (transform)
					{
						Object.Destroy(transform.gameObject);
					}
				}
				Debug.LogWarning("There were " + array.Length + " SoundPool nodes leaked!. Cleaned them up.");
			}
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0001C1DC File Offset: 0x0001A3DC
		public static void LEAK(Transform transform)
		{
			object destroyNextPumpLock = global::SoundPool.NodeGC.GCDAT.destroyNextPumpLock;
			lock (destroyNextPumpLock)
			{
				global::SoundPool.NodeGC.GCDAT.destroyNextQueued = true;
				global::SoundPool.NodeGC.GCDAT.destroyTheseNextPump.Add(transform);
			}
		}

		// Token: 0x020000FB RID: 251
		private static class GCDAT
		{
			// Token: 0x060005D5 RID: 1493 RVA: 0x0001C230 File Offset: 0x0001A430
			static GCDAT()
			{
				global::SoundPool.firstLeak = true;
			}

			// Token: 0x040004F0 RID: 1264
			public static readonly List<Transform> destroyTheseNextPump = new List<Transform>();

			// Token: 0x040004F1 RID: 1265
			public static readonly object destroyNextPumpLock = new object();

			// Token: 0x040004F2 RID: 1266
			public static bool destroyNextQueued;
		}
	}

	// Token: 0x020000FC RID: 252
	private sealed class Node : IDisposable
	{
		// Token: 0x060005D7 RID: 1495 RVA: 0x0001C254 File Offset: 0x0001A454
		public void Reserve()
		{
			switch (this.rootID)
			{
			case global::SoundPool.RootID.LIMBO:
				break;
			case global::SoundPool.RootID.RESERVED:
			case global::SoundPool.RootID.DISPOSED:
				return;
			default:
				this.audio.Stop();
				this.audio.enabled = false;
				this.audio.clip = null;
				this.parent = null;
				if (this.way.next.has)
				{
					this.way.next.node.way.prev = this.way.prev;
				}
				if (this.way.prev.has)
				{
					this.way.prev.node.way.next = this.way.next;
				}
				else
				{
					this.root.first = this.way.next;
				}
				this.root.count--;
				this.way = default(global::SoundPool.Way);
				break;
			}
			this.root = global::SoundPool.reserved;
			this.rootID = global::SoundPool.RootID.RESERVED;
			this.way.next = global::SoundPool.reserved.first;
			if (this.way.next.has)
			{
				this.way.next.node.way.prev.has = true;
				this.way.next.node.way.prev.node = this;
			}
			global::SoundPool.reserved.first.has = true;
			global::SoundPool.reserved.first.node = this;
			global::SoundPool.reserved.count++;
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0001C414 File Offset: 0x0001A614
		public void EnterLimbo()
		{
			switch (this.rootID)
			{
			case global::SoundPool.RootID.LIMBO:
			case global::SoundPool.RootID.DISPOSED:
				return;
			case global::SoundPool.RootID.RESERVED:
				break;
			default:
				this.audio.Stop();
				this.audio.enabled = false;
				this.audio.clip = null;
				this.parent = null;
				break;
			}
			if (this.way.prev.has)
			{
				this.way.prev.node.way.next = this.way.next;
			}
			else
			{
				this.root.first = this.way.next;
			}
			if (this.way.next.has)
			{
				this.way.next.node.way.prev = this.way.prev;
			}
			this.root.count--;
			this.way = default(global::SoundPool.Way);
			this.root = null;
			this.rootID = global::SoundPool.RootID.LIMBO;
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x0001C534 File Offset: 0x0001A734
		public void Bind()
		{
			this.way.prev = default(global::SoundPool.Dir);
			this.way.next = this.root.first;
			this.root.first.has = true;
			this.root.first.node = this;
			if (this.way.next.has)
			{
				this.way.next.node.way.prev.has = true;
				this.way.next.node.way.prev.node = this;
			}
			this.root.count++;
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0001C5F8 File Offset: 0x0001A7F8
		public void Dispose()
		{
			switch (this.rootID)
			{
			case global::SoundPool.RootID.LIMBO:
				goto IL_2F;
			case global::SoundPool.RootID.DISPOSED:
				return;
			}
			this.EnterLimbo();
			IL_2F:
			Object.Destroy(this.transform.gameObject);
			this.transform = null;
			this.audio = null;
			this.rootID = global::SoundPool.RootID.DISPOSED;
			GC.SuppressFinalize(this);
			GC.KeepAlive(this);
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0001C668 File Offset: 0x0001A868
		~Node()
		{
			if ((int)this.rootID != 2)
			{
				global::SoundPool.NodeGC.LEAK(this.transform);
			}
			this.transform = null;
			this.audio = null;
		}

		// Token: 0x040004F3 RID: 1267
		public AudioSource audio;

		// Token: 0x040004F4 RID: 1268
		public Transform transform;

		// Token: 0x040004F5 RID: 1269
		public global::SoundPool.Way way;

		// Token: 0x040004F6 RID: 1270
		public global::SoundPool.RootID rootID;

		// Token: 0x040004F7 RID: 1271
		public global::SoundPool.Root root;

		// Token: 0x040004F8 RID: 1272
		public Vector3 translation;

		// Token: 0x040004F9 RID: 1273
		public Quaternion rotation;

		// Token: 0x040004FA RID: 1274
		public Transform parent;
	}
}
