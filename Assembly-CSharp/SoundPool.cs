using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000DA RID: 218
public static class SoundPool
{
	// Token: 0x170000B2 RID: 178
	// (get) Token: 0x06000484 RID: 1156 RVA: 0x000173D4 File Offset: 0x000155D4
	// (set) Token: 0x06000485 RID: 1157 RVA: 0x000173DC File Offset: 0x000155DC
	internal static bool enabled
	{
		get
		{
			return SoundPool._enabled;
		}
		set
		{
			if (value)
			{
				SoundPool._enabled = !SoundPool._quitting;
			}
			else
			{
				SoundPool._enabled = false;
			}
		}
	}

	// Token: 0x170000B3 RID: 179
	// (set) Token: 0x06000486 RID: 1158 RVA: 0x000173FC File Offset: 0x000155FC
	internal static bool quitting
	{
		set
		{
			if (!SoundPool._quitting && value)
			{
				SoundPool._quitting = true;
				SoundPool._enabled = false;
				SoundPool.Drain();
			}
		}
	}

	// Token: 0x06000487 RID: 1159 RVA: 0x00017420 File Offset: 0x00015620
	private static SoundPool.Node NewNode()
	{
		SoundPool.Node node = new SoundPool.Node();
		GameObject gameObject = new GameObject("zzz-soundpoolnode", SoundPool.goTypes);
		gameObject.hideFlags = 8;
		Object.DontDestroyOnLoad(gameObject);
		node.audio = gameObject.audio;
		node.transform = gameObject.transform;
		node.audio.playOnAwake = false;
		node.audio.enabled = false;
		return node;
	}

	// Token: 0x06000488 RID: 1160 RVA: 0x00017484 File Offset: 0x00015684
	private static SoundPool.Node CreateNode()
	{
		if (SoundPool.reserved.first.has)
		{
			SoundPool.Node node = SoundPool.reserved.first.node;
			node.EnterLimbo();
			return node;
		}
		return SoundPool.NewNode();
	}

	// Token: 0x06000489 RID: 1161 RVA: 0x000174C8 File Offset: 0x000156C8
	private static Object TARG(ref SoundPool.Settings settings)
	{
		return (!settings.parent) ? settings.clip : settings.parent;
	}

	// Token: 0x0600048A RID: 1162 RVA: 0x000174EC File Offset: 0x000156EC
	private static void Play(ref SoundPool.Settings settings)
	{
		if (!SoundPool._enabled || settings.volume <= 0f || settings.pitch == 0f || !settings.clip)
		{
			return;
		}
		Transform transform = null;
		SoundPool.Root root;
		SoundPool.RootID rootID;
		bool flag;
		switch (settings.SelectRoot)
		{
		default:
			root = SoundPool.playing;
			rootID = SoundPool.RootID.PLAYING;
			flag = false;
			break;
		case 1:
			if (!Camera.main)
			{
				return;
			}
			transform = Camera.main.transform;
			root = SoundPool.playingCamera;
			rootID = SoundPool.RootID.PLAYING_CAMERA;
			flag = false;
			break;
		case 2:
			if (!settings.parent)
			{
				return;
			}
			root = SoundPool.playingAttached;
			rootID = SoundPool.RootID.PLAYING_ATTACHED;
			flag = false;
			break;
		case 5:
			if (!Camera.main)
			{
				return;
			}
			transform = Camera.main.transform;
			root = SoundPool.playingCamera;
			rootID = SoundPool.RootID.PLAYING_CAMERA;
			flag = true;
			break;
		case 6:
			if (!settings.parent)
			{
				return;
			}
			root = SoundPool.playingAttached;
			rootID = SoundPool.RootID.PLAYING_ATTACHED;
			flag = true;
			break;
		}
		Vector3 vector;
		Quaternion quaternion;
		Vector3 vector2;
		Quaternion rotation;
		if (flag)
		{
			SoundPool.RootID rootID2 = rootID;
			if (rootID2 != SoundPool.RootID.PLAYING_ATTACHED)
			{
				if (rootID2 != SoundPool.RootID.PLAYING_CAMERA)
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
			SoundPool.RootID rootID2 = rootID;
			switch (rootID2 + 3)
			{
			case SoundPool.RootID.LIMBO:
				vector2 = settings.parent.TransformPoint(vector);
				rotation = settings.parent.rotation * quaternion;
				break;
			case SoundPool.RootID.RESERVED:
				vector2 = transform.TransformPoint(vector);
				rotation = transform.rotation * quaternion;
				break;
			case SoundPool.RootID.DISPOSED:
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
		SoundPool.Node node = SoundPool.CreateNode();
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

	// Token: 0x0600048B RID: 1163 RVA: 0x00017918 File Offset: 0x00015B18
	public static void Pump()
	{
		if (SoundPool.firstLeak)
		{
			if (!SoundPool.hadFirstLeak)
			{
				Debug.LogWarning("SoundPool node leaked for the first time. Though performance should still be good, from now on until application exit there will be extra processing in Pump to clean up game objects of leaked/gc'd nodes. [ie. a mutex is now being locked and unlocked]");
				SoundPool.hadFirstLeak = true;
			}
			SoundPool.NodeGC.JOIN();
		}
		SoundPool.Dir dir = SoundPool.playingCamera.first;
		if (dir.has)
		{
			Camera main = Camera.main;
			if (main)
			{
				Transform transform = main.transform;
				Quaternion rotation = transform.rotation;
				do
				{
					SoundPool.Node node = dir.node;
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
					SoundPool.Node node2 = dir.node;
					dir = dir.node.way.next;
					node2.Reserve();
				}
				while (dir.has);
			}
		}
		dir = SoundPool.playingAttached.first;
		while (dir.has)
		{
			SoundPool.Node node3 = dir.node;
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
		dir = SoundPool.playing.first;
		while (dir.has)
		{
			SoundPool.Node node4 = dir.node;
			dir = dir.node.way.next;
			if (!node4.audio.isPlaying)
			{
				node4.Reserve();
			}
		}
	}

	// Token: 0x0600048C RID: 1164 RVA: 0x00017B24 File Offset: 0x00015D24
	public static void DrainReserves()
	{
		SoundPool.Dir dir = SoundPool.reserved.first;
		while (dir.has)
		{
			SoundPool.Node node = dir.node;
			dir = dir.node.way.next;
			node.Dispose();
		}
	}

	// Token: 0x0600048D RID: 1165 RVA: 0x00017B70 File Offset: 0x00015D70
	public static void Stop()
	{
		SoundPool.Dir dir = SoundPool.playing.first;
		while (dir.has)
		{
			SoundPool.Node node = dir.node;
			dir = dir.node.way.next;
			node.Reserve();
		}
		dir = SoundPool.playingAttached.first;
		while (dir.has)
		{
			SoundPool.Node node = dir.node;
			dir = dir.node.way.next;
			node.Reserve();
		}
		dir = SoundPool.playingCamera.first;
		while (dir.has)
		{
			SoundPool.Node node = dir.node;
			dir = dir.node.way.next;
			node.Reserve();
		}
	}

	// Token: 0x0600048E RID: 1166 RVA: 0x00017C34 File Offset: 0x00015E34
	public static void Drain()
	{
		SoundPool.Dir dir = SoundPool.playing.first;
		while (dir.has)
		{
			SoundPool.Node node = dir.node;
			dir = dir.node.way.next;
			node.Dispose();
		}
		dir = SoundPool.playingAttached.first;
		while (dir.has)
		{
			SoundPool.Node node = dir.node;
			dir = dir.node.way.next;
			node.Dispose();
		}
		dir = SoundPool.playingCamera.first;
		while (dir.has)
		{
			SoundPool.Node node = dir.node;
			dir = dir.node.way.next;
			node.Dispose();
		}
		dir = SoundPool.reserved.first;
		while (dir.has)
		{
			SoundPool.Node node = dir.node;
			dir = dir.node.way.next;
			node.Dispose();
		}
	}

	// Token: 0x170000B4 RID: 180
	// (get) Token: 0x0600048F RID: 1167 RVA: 0x00017D34 File Offset: 0x00015F34
	public static int reserveCount
	{
		get
		{
			return SoundPool.reserved.count;
		}
	}

	// Token: 0x170000B5 RID: 181
	// (get) Token: 0x06000490 RID: 1168 RVA: 0x00017D40 File Offset: 0x00015F40
	public static int playingCount
	{
		get
		{
			return SoundPool.playingCamera.count + SoundPool.playingAttached.count + SoundPool.playing.count;
		}
	}

	// Token: 0x170000B6 RID: 182
	// (get) Token: 0x06000491 RID: 1169 RVA: 0x00017D70 File Offset: 0x00015F70
	public static int totalCount
	{
		get
		{
			return SoundPool.playingCamera.count + SoundPool.playingAttached.count + SoundPool.playing.count + SoundPool.reserved.count;
		}
	}

	// Token: 0x06000492 RID: 1170 RVA: 0x00017DA0 File Offset: 0x00015FA0
	public static void Play(this AudioClip clip, Vector3 position, Quaternion rotation)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		SoundPool.Play(ref def);
	}

	// Token: 0x06000493 RID: 1171 RVA: 0x00017DD4 File Offset: 0x00015FD4
	public static void Play(this AudioClip clip, Vector3 position, Quaternion rotation, float volume)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		SoundPool.Play(ref def);
	}

	// Token: 0x06000494 RID: 1172 RVA: 0x00017E10 File Offset: 0x00016010
	public static void Play(this AudioClip clip, Vector3 position, Quaternion rotation, float volume, float pitch)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		SoundPool.Play(ref def);
	}

	// Token: 0x06000495 RID: 1173 RVA: 0x00017E54 File Offset: 0x00016054
	public static void Play(this AudioClip clip, Vector3 position, Quaternion rotation, float volume, float minDistance, float maxDistance)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		def.min = minDistance;
		def.max = maxDistance;
		SoundPool.Play(ref def);
	}

	// Token: 0x06000496 RID: 1174 RVA: 0x00017EA0 File Offset: 0x000160A0
	public static void Play(this AudioClip clip, Vector3 position, Quaternion rotation, float volume, AudioRolloffMode rolloffMode, float minDistance, float maxDistance)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		def.min = minDistance;
		def.max = maxDistance;
		def.mode = rolloffMode;
		SoundPool.Play(ref def);
	}

	// Token: 0x06000497 RID: 1175 RVA: 0x00017EF8 File Offset: 0x000160F8
	public static void Play(this AudioClip clip, Vector3 position, Quaternion rotation, float volume, float pitch, float minDistance, float maxDistance)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.min = minDistance;
		def.max = maxDistance;
		SoundPool.Play(ref def);
	}

	// Token: 0x06000498 RID: 1176 RVA: 0x00017F50 File Offset: 0x00016150
	public static void Play(this AudioClip clip, Vector3 position, Quaternion rotation, float volume, float pitch, float minDistance, float maxDistance, float dopplerLevel)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.doppler = dopplerLevel;
		def.min = minDistance;
		def.max = maxDistance;
		SoundPool.Play(ref def);
	}

	// Token: 0x06000499 RID: 1177 RVA: 0x00017FB0 File Offset: 0x000161B0
	public static void Play(this AudioClip clip, Vector3 position, Quaternion rotation, float volume, float pitch, float minDistance, float maxDistance, float dopplerLevel, float spread)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.doppler = dopplerLevel;
		def.min = minDistance;
		def.max = maxDistance;
		def.spread = spread;
		SoundPool.Play(ref def);
	}

	// Token: 0x0600049A RID: 1178 RVA: 0x00018018 File Offset: 0x00016218
	public static void Play(this AudioClip clip, Vector3 position, Quaternion rotation, float volume, float pitch, float minDistance, float maxDistance, float dopplerLevel, float spread, bool bypassEffects)
	{
		SoundPool.Settings def = SoundPool.DEF;
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
		SoundPool.Play(ref def);
	}

	// Token: 0x0600049B RID: 1179 RVA: 0x00018088 File Offset: 0x00016288
	public static void Play(this AudioClip clip, Vector3 position, Quaternion rotation, float volume, float pitch, AudioRolloffMode rolloffMode, float minDistance, float maxDistance, float dopplerLevel)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.doppler = dopplerLevel;
		def.min = minDistance;
		def.max = maxDistance;
		def.mode = rolloffMode;
		SoundPool.Play(ref def);
	}

	// Token: 0x0600049C RID: 1180 RVA: 0x000180F0 File Offset: 0x000162F0
	public static void Play(this AudioClip clip, Vector3 position, Quaternion rotation, float volume, float pitch, AudioRolloffMode rolloffMode, float minDistance, float maxDistance, float dopplerLevel, float spread)
	{
		SoundPool.Settings def = SoundPool.DEF;
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
		SoundPool.Play(ref def);
	}

	// Token: 0x0600049D RID: 1181 RVA: 0x00018160 File Offset: 0x00016360
	public static void Play(this AudioClip clip, Vector3 position, Quaternion rotation, float volume, float pitch, AudioRolloffMode rolloffMode, float minDistance, float maxDistance, float dopplerLevel, float spread, bool bypassEffects)
	{
		SoundPool.Settings def = SoundPool.DEF;
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
		SoundPool.Play(ref def);
	}

	// Token: 0x0600049E RID: 1182 RVA: 0x000181DC File Offset: 0x000163DC
	public static void Play(this AudioClip clip, Vector3 position)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.localPosition = position;
		def.clip = clip;
		SoundPool.Play(ref def);
	}

	// Token: 0x0600049F RID: 1183 RVA: 0x00018208 File Offset: 0x00016408
	public static void Play(this AudioClip clip, Vector3 position, float volume)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004A0 RID: 1184 RVA: 0x0001823C File Offset: 0x0001643C
	public static void Play(this AudioClip clip, Vector3 position, float volume, float pitch)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004A1 RID: 1185 RVA: 0x00018278 File Offset: 0x00016478
	public static void Play(this AudioClip clip, Vector3 position, float volume, float minDistance, float maxDistance)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.min = minDistance;
		def.max = maxDistance;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004A2 RID: 1186 RVA: 0x000182BC File Offset: 0x000164BC
	public static void Play(this AudioClip clip, Vector3 position, float volume, float minDistance, float maxDistance, int priority)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.min = minDistance;
		def.max = maxDistance;
		def.priority = priority;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004A3 RID: 1187 RVA: 0x00018308 File Offset: 0x00016508
	public static void Play(this AudioClip clip, Vector3 position, float volume, float pitch, float minDistance, float maxDistance, int priority)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.min = minDistance;
		def.max = maxDistance;
		def.priority = priority;
		def.pitch = pitch;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004A4 RID: 1188 RVA: 0x00018360 File Offset: 0x00016560
	public static void Play(this AudioClip clip, Vector3 position, float volume, AudioRolloffMode rolloffMode, float minDistance, float maxDistance)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.min = minDistance;
		def.max = maxDistance;
		def.mode = rolloffMode;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004A5 RID: 1189 RVA: 0x000183AC File Offset: 0x000165AC
	public static void Play(this AudioClip clip, Vector3 position, float volume, float pitch, float minDistance, float maxDistance)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.min = minDistance;
		def.max = maxDistance;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004A6 RID: 1190 RVA: 0x000183F8 File Offset: 0x000165F8
	public static void Play(this AudioClip clip, Vector3 position, float volume, float pitch, float minDistance, float maxDistance, float dopplerLevel)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.doppler = dopplerLevel;
		def.min = minDistance;
		def.max = maxDistance;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004A7 RID: 1191 RVA: 0x00018450 File Offset: 0x00016650
	public static void Play(this AudioClip clip, Vector3 position, float volume, float pitch, float minDistance, float maxDistance, float dopplerLevel, float spread)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.doppler = dopplerLevel;
		def.min = minDistance;
		def.max = maxDistance;
		def.spread = spread;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004A8 RID: 1192 RVA: 0x000184B0 File Offset: 0x000166B0
	public static void Play(this AudioClip clip, Vector3 position, float volume, float pitch, float minDistance, float maxDistance, float dopplerLevel, float spread, bool bypassEffects)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.doppler = dopplerLevel;
		def.min = minDistance;
		def.max = maxDistance;
		def.spread = spread;
		def.bypassEffects = bypassEffects;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004A9 RID: 1193 RVA: 0x00018518 File Offset: 0x00016718
	public static void Play(this AudioClip clip, Vector3 position, float volume, float pitch, AudioRolloffMode rolloffMode, float minDistance, float maxDistance, float dopplerLevel)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.doppler = dopplerLevel;
		def.min = minDistance;
		def.max = maxDistance;
		def.mode = rolloffMode;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004AA RID: 1194 RVA: 0x00018578 File Offset: 0x00016778
	public static void Play(this AudioClip clip, Vector3 position, float volume, float pitch, AudioRolloffMode rolloffMode, float minDistance, float maxDistance, float dopplerLevel, float spread)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.doppler = dopplerLevel;
		def.min = minDistance;
		def.max = maxDistance;
		def.mode = rolloffMode;
		def.spread = spread;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004AB RID: 1195 RVA: 0x000185E0 File Offset: 0x000167E0
	public static void Play(this AudioClip clip, Vector3 position, float volume, float pitch, AudioRolloffMode rolloffMode, float minDistance, float maxDistance, float dopplerLevel, float spread, bool bypassEffects)
	{
		SoundPool.Settings def = SoundPool.DEF;
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
		SoundPool.Play(ref def);
	}

	// Token: 0x060004AC RID: 1196 RVA: 0x00018650 File Offset: 0x00016850
	public static void Play(this AudioClip clip)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.SelectRoot = 1;
		def.clip = clip;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004AD RID: 1197 RVA: 0x0001867C File Offset: 0x0001687C
	public static void Play(this AudioClip clip, float volume)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.SelectRoot = 1;
		def.clip = clip;
		def.volume = volume;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004AE RID: 1198 RVA: 0x000186B0 File Offset: 0x000168B0
	public static void Play(this AudioClip clip, float volume, float pan)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.SelectRoot = 1;
		def.clip = clip;
		def.volume = volume;
		def.pan = pan;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004AF RID: 1199 RVA: 0x000186EC File Offset: 0x000168EC
	public static void Play(this AudioClip clip, float volume, float pan, float pitch)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.SelectRoot = 1;
		def.clip = clip;
		def.volume = volume;
		def.pan = pan;
		def.pitch = pitch;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004B0 RID: 1200 RVA: 0x00018730 File Offset: 0x00016930
	public static void Play(this AudioClip clip, float volume, float pan, Vector3 worldPosition)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.SelectRoot = 5;
		def.clip = clip;
		def.volume = volume;
		def.pan = pan;
		def.localPosition = worldPosition;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004B1 RID: 1201 RVA: 0x00018774 File Offset: 0x00016974
	public static void Play(this AudioClip clip, float volume, float pan, Vector3 worldPosition, Quaternion worldRotation)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.SelectRoot = 5;
		def.clip = clip;
		def.volume = volume;
		def.pan = pan;
		def.localPosition = worldPosition;
		def.localRotation = worldRotation;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004B2 RID: 1202 RVA: 0x000187C0 File Offset: 0x000169C0
	public static void PlayLocal(this AudioClip clip, float volume, float pan, Vector3 worldPosition)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.SelectRoot = 1;
		def.clip = clip;
		def.volume = volume;
		def.pan = pan;
		def.localPosition = worldPosition;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004B3 RID: 1203 RVA: 0x00018804 File Offset: 0x00016A04
	public static void PlayLocal(this AudioClip clip, float volume, float pan, Vector3 worldPosition, Quaternion worldRotation)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.SelectRoot = 1;
		def.clip = clip;
		def.volume = volume;
		def.pan = pan;
		def.localPosition = worldPosition;
		def.localRotation = worldRotation;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004B4 RID: 1204 RVA: 0x00018850 File Offset: 0x00016A50
	public static void Play(this AudioClip clip, Transform on)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.SelectRoot = 2;
		def.parent = on;
		def.clip = clip;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004B5 RID: 1205 RVA: 0x00018884 File Offset: 0x00016A84
	public static void Play(this AudioClip clip, Transform on, float volume)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.SelectRoot = 2;
		def.parent = on;
		def.clip = clip;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004B6 RID: 1206 RVA: 0x000188B8 File Offset: 0x00016AB8
	public static void Play(this AudioClip clip, Transform on, Vector3 position, Quaternion rotation)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.SelectRoot = 6;
		def.parent = on;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004B7 RID: 1207 RVA: 0x000188FC File Offset: 0x00016AFC
	public static void Play(this AudioClip clip, Transform on, Vector3 position, Quaternion rotation, float volume)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.SelectRoot = 6;
		def.parent = on;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004B8 RID: 1208 RVA: 0x00018948 File Offset: 0x00016B48
	public static void Play(this AudioClip clip, Transform on, Vector3 position, Quaternion rotation, float volume, float pitch)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.SelectRoot = 6;
		def.parent = on;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004B9 RID: 1209 RVA: 0x0001899C File Offset: 0x00016B9C
	public static void Play(this AudioClip clip, Transform on, Vector3 position, Quaternion rotation, float volume, float minDistance, float maxDistance)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.SelectRoot = 6;
		def.parent = on;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		def.min = minDistance;
		def.max = maxDistance;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004BA RID: 1210 RVA: 0x000189FC File Offset: 0x00016BFC
	public static void Play(this AudioClip clip, Transform on, Vector3 position, Quaternion rotation, float volume, AudioRolloffMode rolloffMode, float minDistance, float maxDistance)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.SelectRoot = 6;
		def.parent = on;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		def.min = minDistance;
		def.max = maxDistance;
		def.mode = rolloffMode;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004BB RID: 1211 RVA: 0x00018A64 File Offset: 0x00016C64
	public static void Play(this AudioClip clip, Transform on, Vector3 position, Quaternion rotation, float volume, float pitch, float minDistance, float maxDistance)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.SelectRoot = 6;
		def.parent = on;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.min = minDistance;
		def.max = maxDistance;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004BC RID: 1212 RVA: 0x00018ACC File Offset: 0x00016CCC
	public static void Play(this AudioClip clip, Transform on, Vector3 position, Quaternion rotation, float volume, float pitch, float minDistance, float maxDistance, float dopplerLevel)
	{
		SoundPool.Settings def = SoundPool.DEF;
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
		SoundPool.Play(ref def);
	}

	// Token: 0x060004BD RID: 1213 RVA: 0x00018B3C File Offset: 0x00016D3C
	public static void Play(this AudioClip clip, Transform on, Vector3 position, Quaternion rotation, float volume, float pitch, float minDistance, float maxDistance, float dopplerLevel, float spread)
	{
		SoundPool.Settings def = SoundPool.DEF;
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
		SoundPool.Play(ref def);
	}

	// Token: 0x060004BE RID: 1214 RVA: 0x00018BB4 File Offset: 0x00016DB4
	public static void Play(this AudioClip clip, Transform on, Vector3 position, Quaternion rotation, float volume, float pitch, float minDistance, float maxDistance, float dopplerLevel, float spread, bool bypassEffects)
	{
		SoundPool.Settings def = SoundPool.DEF;
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
		SoundPool.Play(ref def);
	}

	// Token: 0x060004BF RID: 1215 RVA: 0x00018C38 File Offset: 0x00016E38
	public static void Play(this AudioClip clip, Transform on, Vector3 position, Quaternion rotation, float volume, float pitch, AudioRolloffMode rolloffMode, float minDistance, float maxDistance, float dopplerLevel)
	{
		SoundPool.Settings def = SoundPool.DEF;
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
		SoundPool.Play(ref def);
	}

	// Token: 0x060004C0 RID: 1216 RVA: 0x00018CB0 File Offset: 0x00016EB0
	public static void Play(this AudioClip clip, Transform on, Vector3 position, Quaternion rotation, float volume, float pitch, AudioRolloffMode rolloffMode, float minDistance, float maxDistance, float dopplerLevel, float spread)
	{
		SoundPool.Settings def = SoundPool.DEF;
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
		SoundPool.Play(ref def);
	}

	// Token: 0x060004C1 RID: 1217 RVA: 0x00018D34 File Offset: 0x00016F34
	public static void Play(this AudioClip clip, Transform on, Vector3 position, Quaternion rotation, float volume, float pitch, AudioRolloffMode rolloffMode, float minDistance, float maxDistance, float dopplerLevel, float spread, bool bypassEffects)
	{
		SoundPool.Settings def = SoundPool.DEF;
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
		SoundPool.Play(ref def);
	}

	// Token: 0x060004C2 RID: 1218 RVA: 0x00018DC0 File Offset: 0x00016FC0
	public static void Play(this AudioClip clip, Transform on, Vector3 position)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.SelectRoot = 6;
		def.parent = on;
		def.localPosition = position;
		def.clip = clip;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004C3 RID: 1219 RVA: 0x00018DFC File Offset: 0x00016FFC
	public static void Play(this AudioClip clip, Transform on, Vector3 position, float volume)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.SelectRoot = 6;
		def.parent = on;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004C4 RID: 1220 RVA: 0x00018E40 File Offset: 0x00017040
	public static void Play(this AudioClip clip, Transform on, Vector3 position, float volume, float pitch)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.SelectRoot = 6;
		def.parent = on;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004C5 RID: 1221 RVA: 0x00018E8C File Offset: 0x0001708C
	public static void Play(this AudioClip clip, Transform on, Vector3 position, float volume, float minDistance, float maxDistance)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.SelectRoot = 6;
		def.parent = on;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.min = minDistance;
		def.max = maxDistance;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004C6 RID: 1222 RVA: 0x00018EE0 File Offset: 0x000170E0
	public static void Play(this AudioClip clip, Transform on, Vector3 position, float volume, AudioRolloffMode rolloffMode, float minDistance, float maxDistance)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.SelectRoot = 6;
		def.parent = on;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.min = minDistance;
		def.max = maxDistance;
		def.mode = rolloffMode;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004C7 RID: 1223 RVA: 0x00018F40 File Offset: 0x00017140
	public static void Play(this AudioClip clip, Transform on, Vector3 position, float volume, float pitch, float minDistance, float maxDistance)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.SelectRoot = 6;
		def.parent = on;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.min = minDistance;
		def.max = maxDistance;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004C8 RID: 1224 RVA: 0x00018FA0 File Offset: 0x000171A0
	public static void Play(this AudioClip clip, Transform on, Vector3 position, float volume, float pitch, float minDistance, float maxDistance, float dopplerLevel)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.SelectRoot = 6;
		def.parent = on;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.doppler = dopplerLevel;
		def.min = minDistance;
		def.max = maxDistance;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004C9 RID: 1225 RVA: 0x00019008 File Offset: 0x00017208
	public static void Play(this AudioClip clip, Transform on, Vector3 position, float volume, float pitch, float minDistance, float maxDistance, float dopplerLevel, float spread)
	{
		SoundPool.Settings def = SoundPool.DEF;
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
		SoundPool.Play(ref def);
	}

	// Token: 0x060004CA RID: 1226 RVA: 0x00019078 File Offset: 0x00017278
	public static void Play(this AudioClip clip, Transform on, Vector3 position, float volume, float pitch, float minDistance, float maxDistance, float dopplerLevel, float spread, bool bypassEffects)
	{
		SoundPool.Settings def = SoundPool.DEF;
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
		SoundPool.Play(ref def);
	}

	// Token: 0x060004CB RID: 1227 RVA: 0x000190F0 File Offset: 0x000172F0
	public static void Play(this AudioClip clip, Transform on, Vector3 position, float volume, float pitch, AudioRolloffMode rolloffMode, float minDistance, float maxDistance, float dopplerLevel)
	{
		SoundPool.Settings def = SoundPool.DEF;
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
		SoundPool.Play(ref def);
	}

	// Token: 0x060004CC RID: 1228 RVA: 0x00019160 File Offset: 0x00017360
	public static void Play(this AudioClip clip, Transform on, Vector3 position, float volume, float pitch, AudioRolloffMode rolloffMode, float minDistance, float maxDistance, float dopplerLevel, float spread)
	{
		SoundPool.Settings def = SoundPool.DEF;
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
		SoundPool.Play(ref def);
	}

	// Token: 0x060004CD RID: 1229 RVA: 0x000191D8 File Offset: 0x000173D8
	public static void Play(this AudioClip clip, Transform on, Vector3 position, float volume, float pitch, AudioRolloffMode rolloffMode, float minDistance, float maxDistance, float dopplerLevel, float spread, bool bypassEffects)
	{
		SoundPool.Settings def = SoundPool.DEF;
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
		SoundPool.Play(ref def);
	}

	// Token: 0x060004CE RID: 1230 RVA: 0x0001925C File Offset: 0x0001745C
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, Quaternion rotation)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.SelectRoot = 2;
		def.parent = on;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004CF RID: 1231 RVA: 0x000192A0 File Offset: 0x000174A0
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, Quaternion rotation, float volume)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.SelectRoot = 2;
		def.parent = on;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004D0 RID: 1232 RVA: 0x000192EC File Offset: 0x000174EC
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, Quaternion rotation, float volume, float pitch)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.SelectRoot = 2;
		def.parent = on;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004D1 RID: 1233 RVA: 0x00019340 File Offset: 0x00017540
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, Quaternion rotation, float volume, float minDistance, float maxDistance)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.SelectRoot = 2;
		def.parent = on;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		def.min = minDistance;
		def.max = maxDistance;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004D2 RID: 1234 RVA: 0x000193A0 File Offset: 0x000175A0
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, Quaternion rotation, float volume, AudioRolloffMode rolloffMode, float minDistance, float maxDistance)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.SelectRoot = 2;
		def.parent = on;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		def.min = minDistance;
		def.max = maxDistance;
		def.mode = rolloffMode;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004D3 RID: 1235 RVA: 0x00019408 File Offset: 0x00017608
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, Quaternion rotation, float volume, float pitch, float minDistance, float maxDistance)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.SelectRoot = 2;
		def.parent = on;
		def.localPosition = position;
		def.localRotation = rotation;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.min = minDistance;
		def.max = maxDistance;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004D4 RID: 1236 RVA: 0x00019470 File Offset: 0x00017670
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, Quaternion rotation, float volume, float pitch, float minDistance, float maxDistance, float dopplerLevel)
	{
		SoundPool.Settings def = SoundPool.DEF;
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
		SoundPool.Play(ref def);
	}

	// Token: 0x060004D5 RID: 1237 RVA: 0x000194E0 File Offset: 0x000176E0
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, Quaternion rotation, float volume, float pitch, float minDistance, float maxDistance, float dopplerLevel, float spread)
	{
		SoundPool.Settings def = SoundPool.DEF;
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
		SoundPool.Play(ref def);
	}

	// Token: 0x060004D6 RID: 1238 RVA: 0x00019558 File Offset: 0x00017758
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, Quaternion rotation, float volume, float pitch, float minDistance, float maxDistance, float dopplerLevel, float spread, bool bypassEffects)
	{
		SoundPool.Settings def = SoundPool.DEF;
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
		SoundPool.Play(ref def);
	}

	// Token: 0x060004D7 RID: 1239 RVA: 0x000195DC File Offset: 0x000177DC
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, Quaternion rotation, float volume, float pitch, AudioRolloffMode rolloffMode, float minDistance, float maxDistance, float dopplerLevel)
	{
		SoundPool.Settings def = SoundPool.DEF;
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
		SoundPool.Play(ref def);
	}

	// Token: 0x060004D8 RID: 1240 RVA: 0x00019654 File Offset: 0x00017854
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, Quaternion rotation, float volume, float pitch, AudioRolloffMode rolloffMode, float minDistance, float maxDistance, float dopplerLevel, float spread)
	{
		SoundPool.Settings def = SoundPool.DEF;
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
		SoundPool.Play(ref def);
	}

	// Token: 0x060004D9 RID: 1241 RVA: 0x000196D8 File Offset: 0x000178D8
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, Quaternion rotation, float volume, float pitch, AudioRolloffMode rolloffMode, float minDistance, float maxDistance, float dopplerLevel, float spread, bool bypassEffects)
	{
		SoundPool.Settings def = SoundPool.DEF;
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
		SoundPool.Play(ref def);
	}

	// Token: 0x060004DA RID: 1242 RVA: 0x00019764 File Offset: 0x00017964
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.SelectRoot = 2;
		def.parent = on;
		def.localPosition = position;
		def.clip = clip;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004DB RID: 1243 RVA: 0x000197A0 File Offset: 0x000179A0
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, float volume)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.SelectRoot = 2;
		def.parent = on;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004DC RID: 1244 RVA: 0x000197E4 File Offset: 0x000179E4
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, float volume, int priority)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.SelectRoot = 2;
		def.parent = on;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.priority = priority;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004DD RID: 1245 RVA: 0x00019830 File Offset: 0x00017A30
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, float volume, float pitch)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.SelectRoot = 2;
		def.parent = on;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004DE RID: 1246 RVA: 0x0001987C File Offset: 0x00017A7C
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, float volume, float minDistance, float maxDistance)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.SelectRoot = 2;
		def.parent = on;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.min = minDistance;
		def.max = maxDistance;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004DF RID: 1247 RVA: 0x000198D0 File Offset: 0x00017AD0
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, float volume, AudioRolloffMode rolloffMode, float minDistance, float maxDistance)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.SelectRoot = 2;
		def.parent = on;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.min = minDistance;
		def.max = maxDistance;
		def.mode = rolloffMode;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004E0 RID: 1248 RVA: 0x00019930 File Offset: 0x00017B30
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, float volume, float pitch, float minDistance, float maxDistance)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.SelectRoot = 2;
		def.parent = on;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.min = minDistance;
		def.max = maxDistance;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004E1 RID: 1249 RVA: 0x00019990 File Offset: 0x00017B90
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, float volume, float pitch, float minDistance, float maxDistance, int priority)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.SelectRoot = 2;
		def.parent = on;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.min = minDistance;
		def.max = maxDistance;
		def.priority = priority;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004E2 RID: 1250 RVA: 0x000199F8 File Offset: 0x00017BF8
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, float volume, float pitch, float minDistance, float maxDistance, float dopplerLevel)
	{
		SoundPool.Settings def = SoundPool.DEF;
		def.SelectRoot = 2;
		def.parent = on;
		def.localPosition = position;
		def.clip = clip;
		def.volume = volume;
		def.pitch = pitch;
		def.doppler = dopplerLevel;
		def.min = minDistance;
		def.max = maxDistance;
		SoundPool.Play(ref def);
	}

	// Token: 0x060004E3 RID: 1251 RVA: 0x00019A60 File Offset: 0x00017C60
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, float volume, float pitch, float minDistance, float maxDistance, float dopplerLevel, float spread)
	{
		SoundPool.Settings def = SoundPool.DEF;
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
		SoundPool.Play(ref def);
	}

	// Token: 0x060004E4 RID: 1252 RVA: 0x00019AD0 File Offset: 0x00017CD0
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, float volume, float pitch, float minDistance, float maxDistance, float dopplerLevel, float spread, bool bypassEffects)
	{
		SoundPool.Settings def = SoundPool.DEF;
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
		SoundPool.Play(ref def);
	}

	// Token: 0x060004E5 RID: 1253 RVA: 0x00019B48 File Offset: 0x00017D48
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, float volume, float pitch, AudioRolloffMode rolloffMode, float minDistance, float maxDistance, float dopplerLevel)
	{
		SoundPool.Settings def = SoundPool.DEF;
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
		SoundPool.Play(ref def);
	}

	// Token: 0x060004E6 RID: 1254 RVA: 0x00019BB8 File Offset: 0x00017DB8
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, float volume, float pitch, AudioRolloffMode rolloffMode, float minDistance, float maxDistance, float dopplerLevel, float spread)
	{
		SoundPool.Settings def = SoundPool.DEF;
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
		SoundPool.Play(ref def);
	}

	// Token: 0x060004E7 RID: 1255 RVA: 0x00019C30 File Offset: 0x00017E30
	public static void PlayLocal(this AudioClip clip, Transform on, Vector3 position, float volume, float pitch, AudioRolloffMode rolloffMode, float minDistance, float maxDistance, float dopplerLevel, float spread, bool bypassEffects)
	{
		SoundPool.Settings def = SoundPool.DEF;
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
		SoundPool.Play(ref def);
	}

	// Token: 0x04000434 RID: 1076
	private const sbyte SelectRoot_Attach = 2;

	// Token: 0x04000435 RID: 1077
	private const sbyte SelectRoot_Camera = 1;

	// Token: 0x04000436 RID: 1078
	private const sbyte SelectRoot_Default = 0;

	// Token: 0x04000437 RID: 1079
	private const sbyte SelectRoot_Camera_WorldOffset = 5;

	// Token: 0x04000438 RID: 1080
	private const sbyte SelectRoot_Attach_WorldOffset = 6;

	// Token: 0x04000439 RID: 1081
	private const string goName = "zzz-soundpoolnode";

	// Token: 0x0400043A RID: 1082
	private const float logarithmicMaxScale = 2f;

	// Token: 0x0400043B RID: 1083
	private static bool _enabled;

	// Token: 0x0400043C RID: 1084
	private static bool _quitting;

	// Token: 0x0400043D RID: 1085
	private static readonly SoundPool.Root playingAttached = new SoundPool.Root(SoundPool.RootID.PLAYING_ATTACHED);

	// Token: 0x0400043E RID: 1086
	private static readonly SoundPool.Root playingCamera = new SoundPool.Root(SoundPool.RootID.PLAYING_CAMERA);

	// Token: 0x0400043F RID: 1087
	private static readonly SoundPool.Root playing = new SoundPool.Root(SoundPool.RootID.PLAYING);

	// Token: 0x04000440 RID: 1088
	private static readonly SoundPool.Root reserved = new SoundPool.Root(SoundPool.RootID.RESERVED);

	// Token: 0x04000441 RID: 1089
	private static bool firstLeak = false;

	// Token: 0x04000442 RID: 1090
	private static bool hadFirstLeak;

	// Token: 0x04000443 RID: 1091
	private static readonly Type[] goTypes = new Type[]
	{
		typeof(AudioSource)
	};

	// Token: 0x04000444 RID: 1092
	private static readonly SoundPool.Settings DEF = new SoundPool.Settings
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

	// Token: 0x020000DB RID: 219
	private struct Settings
	{
		// Token: 0x060004E8 RID: 1256 RVA: 0x00019CB4 File Offset: 0x00017EB4
		public static explicit operator SoundPool.Settings(SoundPool.PlayerShared player)
		{
			SoundPool.Settings def = SoundPool.DEF;
			def.clip = player.clip;
			def.volume = player.volume;
			def.pitch = player.pitch;
			def.priority = player.priority;
			return def;
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00019D00 File Offset: 0x00017F00
		public static explicit operator SoundPool.Settings(SoundPool.Player3D player)
		{
			SoundPool.Settings result = (SoundPool.Settings)player.super;
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

		// Token: 0x060004EA RID: 1258 RVA: 0x00019D98 File Offset: 0x00017F98
		public static explicit operator SoundPool.Settings(SoundPool.PlayerLocal player)
		{
			SoundPool.Settings result = (SoundPool.Settings)player.super;
			result.localPosition = player.localPosition;
			result.localRotation = player.localRotation;
			return result;
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00019DD0 File Offset: 0x00017FD0
		public static explicit operator SoundPool.Settings(SoundPool.Player2D player)
		{
			SoundPool.Settings result = (SoundPool.Settings)player.super;
			result.pan = player.pan;
			return result;
		}

		// Token: 0x04000445 RID: 1093
		public AudioClip clip;

		// Token: 0x04000446 RID: 1094
		public Transform parent;

		// Token: 0x04000447 RID: 1095
		public Quaternion localRotation;

		// Token: 0x04000448 RID: 1096
		public Vector3 localPosition;

		// Token: 0x04000449 RID: 1097
		public float volume;

		// Token: 0x0400044A RID: 1098
		public float pitch;

		// Token: 0x0400044B RID: 1099
		public float pan;

		// Token: 0x0400044C RID: 1100
		public float panLevel;

		// Token: 0x0400044D RID: 1101
		public float min;

		// Token: 0x0400044E RID: 1102
		public float max;

		// Token: 0x0400044F RID: 1103
		public float doppler;

		// Token: 0x04000450 RID: 1104
		public float spread;

		// Token: 0x04000451 RID: 1105
		public int priority;

		// Token: 0x04000452 RID: 1106
		public AudioRolloffMode mode;

		// Token: 0x04000453 RID: 1107
		public sbyte SelectRoot;

		// Token: 0x04000454 RID: 1108
		public bool bypassEffects;
	}

	// Token: 0x020000DC RID: 220
	public struct PlayerShared
	{
		// Token: 0x060004EC RID: 1260 RVA: 0x00019DFC File Offset: 0x00017FFC
		public PlayerShared(AudioClip clip)
		{
			this.clip = clip;
			this.volume = SoundPool.DEF.volume;
			this.pitch = SoundPool.DEF.pitch;
			this.priority = SoundPool.DEF.priority;
		}

		// Token: 0x04000455 RID: 1109
		public static readonly SoundPool.PlayerShared Default = new SoundPool.PlayerShared
		{
			volume = SoundPool.DEF.volume,
			pitch = SoundPool.DEF.pitch,
			priority = SoundPool.DEF.priority
		};

		// Token: 0x04000456 RID: 1110
		public AudioClip clip;

		// Token: 0x04000457 RID: 1111
		public float volume;

		// Token: 0x04000458 RID: 1112
		public float pitch;

		// Token: 0x04000459 RID: 1113
		public int priority;
	}

	// Token: 0x020000DD RID: 221
	public struct Player3D
	{
		// Token: 0x060004EE RID: 1262 RVA: 0x00019E9C File Offset: 0x0001809C
		public Player3D(AudioClip clip)
		{
			this.super = new SoundPool.PlayerShared(clip);
			this.minDistance = SoundPool.DEF.min;
			this.maxDistance = SoundPool.DEF.max;
			this.spread = SoundPool.DEF.spread;
			this.dopplerLevel = SoundPool.DEF.doppler;
			this.panLevel = SoundPool.DEF.panLevel;
			this.rolloffMode = SoundPool.DEF.mode;
			this.bypassEffects = SoundPool.DEF.bypassEffects;
			this.cameraSticky = false;
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x00019FE4 File Offset: 0x000181E4
		// (set) Token: 0x060004F1 RID: 1265 RVA: 0x00019FF4 File Offset: 0x000181F4
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

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x0001A004 File Offset: 0x00018204
		// (set) Token: 0x060004F3 RID: 1267 RVA: 0x0001A014 File Offset: 0x00018214
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

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x0001A024 File Offset: 0x00018224
		// (set) Token: 0x060004F5 RID: 1269 RVA: 0x0001A034 File Offset: 0x00018234
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

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x0001A044 File Offset: 0x00018244
		// (set) Token: 0x060004F7 RID: 1271 RVA: 0x0001A054 File Offset: 0x00018254
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

		// Token: 0x0400045A RID: 1114
		public static readonly SoundPool.Player3D Default = new SoundPool.Player3D
		{
			super = SoundPool.PlayerShared.Default,
			minDistance = SoundPool.DEF.min,
			maxDistance = SoundPool.DEF.max,
			rolloffMode = SoundPool.DEF.mode,
			spread = SoundPool.DEF.spread,
			dopplerLevel = SoundPool.DEF.doppler,
			bypassEffects = SoundPool.DEF.bypassEffects,
			panLevel = SoundPool.DEF.panLevel
		};

		// Token: 0x0400045B RID: 1115
		public SoundPool.PlayerShared super;

		// Token: 0x0400045C RID: 1116
		public float minDistance;

		// Token: 0x0400045D RID: 1117
		public float maxDistance;

		// Token: 0x0400045E RID: 1118
		public float spread;

		// Token: 0x0400045F RID: 1119
		public float dopplerLevel;

		// Token: 0x04000460 RID: 1120
		public float panLevel;

		// Token: 0x04000461 RID: 1121
		public AudioRolloffMode rolloffMode;

		// Token: 0x04000462 RID: 1122
		public bool cameraSticky;

		// Token: 0x04000463 RID: 1123
		public bool bypassEffects;
	}

	// Token: 0x020000DE RID: 222
	public struct PlayerWorld
	{
		// Token: 0x060004F8 RID: 1272 RVA: 0x0001A064 File Offset: 0x00018264
		public PlayerWorld(AudioClip clip)
		{
			this.super = new SoundPool.Player3D(clip);
			this.position = default(Vector3);
			this.rotation = Quaternion.identity;
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x0001A0DC File Offset: 0x000182DC
		// (set) Token: 0x060004FB RID: 1275 RVA: 0x0001A0EC File Offset: 0x000182EC
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

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x0001A0FC File Offset: 0x000182FC
		// (set) Token: 0x060004FD RID: 1277 RVA: 0x0001A10C File Offset: 0x0001830C
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

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x0001A11C File Offset: 0x0001831C
		// (set) Token: 0x060004FF RID: 1279 RVA: 0x0001A12C File Offset: 0x0001832C
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

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x0001A13C File Offset: 0x0001833C
		// (set) Token: 0x06000501 RID: 1281 RVA: 0x0001A14C File Offset: 0x0001834C
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

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x0001A15C File Offset: 0x0001835C
		// (set) Token: 0x06000503 RID: 1283 RVA: 0x0001A16C File Offset: 0x0001836C
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

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x0001A17C File Offset: 0x0001837C
		// (set) Token: 0x06000505 RID: 1285 RVA: 0x0001A18C File Offset: 0x0001838C
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

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x0001A19C File Offset: 0x0001839C
		// (set) Token: 0x06000507 RID: 1287 RVA: 0x0001A1AC File Offset: 0x000183AC
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

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x0001A1BC File Offset: 0x000183BC
		// (set) Token: 0x06000509 RID: 1289 RVA: 0x0001A1CC File Offset: 0x000183CC
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

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x0001A1DC File Offset: 0x000183DC
		// (set) Token: 0x0600050B RID: 1291 RVA: 0x0001A1EC File Offset: 0x000183EC
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

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x0001A1FC File Offset: 0x000183FC
		// (set) Token: 0x0600050D RID: 1293 RVA: 0x0001A20C File Offset: 0x0001840C
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

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x0001A21C File Offset: 0x0001841C
		// (set) Token: 0x0600050F RID: 1295 RVA: 0x0001A22C File Offset: 0x0001842C
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

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x0001A23C File Offset: 0x0001843C
		// (set) Token: 0x06000511 RID: 1297 RVA: 0x0001A24C File Offset: 0x0001844C
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

		// Token: 0x04000464 RID: 1124
		public static readonly SoundPool.PlayerWorld Default = new SoundPool.PlayerWorld
		{
			super = SoundPool.Player3D.Default,
			position = default(Vector3),
			rotation = Quaternion.identity
		};

		// Token: 0x04000465 RID: 1125
		public SoundPool.Player3D super;

		// Token: 0x04000466 RID: 1126
		public Vector3 position;

		// Token: 0x04000467 RID: 1127
		public Quaternion rotation;
	}

	// Token: 0x020000DF RID: 223
	public struct PlayerLocal
	{
		// Token: 0x06000512 RID: 1298 RVA: 0x0001A25C File Offset: 0x0001845C
		public PlayerLocal(AudioClip clip)
		{
			this.super = new SoundPool.Player3D(clip);
			this.localPosition = default(Vector3);
			this.localRotation = Quaternion.identity;
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x0001A2D4 File Offset: 0x000184D4
		// (set) Token: 0x06000515 RID: 1301 RVA: 0x0001A2E4 File Offset: 0x000184E4
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

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x0001A2F4 File Offset: 0x000184F4
		// (set) Token: 0x06000517 RID: 1303 RVA: 0x0001A304 File Offset: 0x00018504
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

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x0001A314 File Offset: 0x00018514
		// (set) Token: 0x06000519 RID: 1305 RVA: 0x0001A324 File Offset: 0x00018524
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

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x0001A334 File Offset: 0x00018534
		// (set) Token: 0x0600051B RID: 1307 RVA: 0x0001A344 File Offset: 0x00018544
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

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x0001A354 File Offset: 0x00018554
		// (set) Token: 0x0600051D RID: 1309 RVA: 0x0001A364 File Offset: 0x00018564
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

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x0001A374 File Offset: 0x00018574
		// (set) Token: 0x0600051F RID: 1311 RVA: 0x0001A384 File Offset: 0x00018584
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

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x0001A394 File Offset: 0x00018594
		// (set) Token: 0x06000521 RID: 1313 RVA: 0x0001A3A4 File Offset: 0x000185A4
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

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x0001A3B4 File Offset: 0x000185B4
		// (set) Token: 0x06000523 RID: 1315 RVA: 0x0001A3C4 File Offset: 0x000185C4
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

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x0001A3D4 File Offset: 0x000185D4
		// (set) Token: 0x06000525 RID: 1317 RVA: 0x0001A3E4 File Offset: 0x000185E4
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

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x0001A3F4 File Offset: 0x000185F4
		// (set) Token: 0x06000527 RID: 1319 RVA: 0x0001A404 File Offset: 0x00018604
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

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x0001A414 File Offset: 0x00018614
		// (set) Token: 0x06000529 RID: 1321 RVA: 0x0001A424 File Offset: 0x00018624
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

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x0001A434 File Offset: 0x00018634
		// (set) Token: 0x0600052B RID: 1323 RVA: 0x0001A444 File Offset: 0x00018644
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

		// Token: 0x04000468 RID: 1128
		public static readonly SoundPool.PlayerLocal Default = new SoundPool.PlayerLocal
		{
			super = SoundPool.Player3D.Default,
			localPosition = default(Vector3),
			localRotation = Quaternion.identity
		};

		// Token: 0x04000469 RID: 1129
		public SoundPool.Player3D super;

		// Token: 0x0400046A RID: 1130
		public Vector3 localPosition;

		// Token: 0x0400046B RID: 1131
		public Quaternion localRotation;
	}

	// Token: 0x020000E0 RID: 224
	public struct PlayerChild
	{
		// Token: 0x0600052C RID: 1324 RVA: 0x0001A454 File Offset: 0x00018654
		public PlayerChild(AudioClip clip)
		{
			this.super = new SoundPool.PlayerLocal(clip);
			this.parent = null;
			this.unglue = false;
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x0001A498 File Offset: 0x00018698
		// (set) Token: 0x0600052F RID: 1327 RVA: 0x0001A4A8 File Offset: 0x000186A8
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

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x0001A4B8 File Offset: 0x000186B8
		// (set) Token: 0x06000531 RID: 1329 RVA: 0x0001A4C8 File Offset: 0x000186C8
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

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x0001A4D8 File Offset: 0x000186D8
		// (set) Token: 0x06000533 RID: 1331 RVA: 0x0001A4E8 File Offset: 0x000186E8
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

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x0001A4F8 File Offset: 0x000186F8
		// (set) Token: 0x06000535 RID: 1333 RVA: 0x0001A508 File Offset: 0x00018708
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

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x0001A518 File Offset: 0x00018718
		// (set) Token: 0x06000537 RID: 1335 RVA: 0x0001A528 File Offset: 0x00018728
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

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x0001A538 File Offset: 0x00018738
		// (set) Token: 0x06000539 RID: 1337 RVA: 0x0001A548 File Offset: 0x00018748
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

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x0001A558 File Offset: 0x00018758
		// (set) Token: 0x0600053B RID: 1339 RVA: 0x0001A568 File Offset: 0x00018768
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

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x0001A578 File Offset: 0x00018778
		// (set) Token: 0x0600053D RID: 1341 RVA: 0x0001A588 File Offset: 0x00018788
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

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x0001A598 File Offset: 0x00018798
		// (set) Token: 0x0600053F RID: 1343 RVA: 0x0001A5A8 File Offset: 0x000187A8
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

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x0001A5B8 File Offset: 0x000187B8
		// (set) Token: 0x06000541 RID: 1345 RVA: 0x0001A5C8 File Offset: 0x000187C8
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

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x0001A5D8 File Offset: 0x000187D8
		// (set) Token: 0x06000543 RID: 1347 RVA: 0x0001A5E8 File Offset: 0x000187E8
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

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x0001A5F8 File Offset: 0x000187F8
		// (set) Token: 0x06000545 RID: 1349 RVA: 0x0001A608 File Offset: 0x00018808
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

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x0001A618 File Offset: 0x00018818
		// (set) Token: 0x06000547 RID: 1351 RVA: 0x0001A628 File Offset: 0x00018828
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

		// Token: 0x0400046C RID: 1132
		public static readonly SoundPool.PlayerChild Default = new SoundPool.PlayerChild
		{
			super = SoundPool.PlayerLocal.Default
		};

		// Token: 0x0400046D RID: 1133
		public SoundPool.PlayerLocal super;

		// Token: 0x0400046E RID: 1134
		public bool unglue;

		// Token: 0x0400046F RID: 1135
		public Transform parent;
	}

	// Token: 0x020000E1 RID: 225
	public struct Player2D
	{
		// Token: 0x06000548 RID: 1352 RVA: 0x0001A638 File Offset: 0x00018838
		public Player2D(AudioClip clip)
		{
			this.super = new SoundPool.PlayerShared(clip);
			this.pan = SoundPool.DEF.pan;
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x0001A69C File Offset: 0x0001889C
		// (set) Token: 0x0600054B RID: 1355 RVA: 0x0001A6AC File Offset: 0x000188AC
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

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x0001A6BC File Offset: 0x000188BC
		// (set) Token: 0x0600054D RID: 1357 RVA: 0x0001A6CC File Offset: 0x000188CC
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

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x0001A6DC File Offset: 0x000188DC
		// (set) Token: 0x0600054F RID: 1359 RVA: 0x0001A6EC File Offset: 0x000188EC
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

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x0001A6FC File Offset: 0x000188FC
		// (set) Token: 0x06000551 RID: 1361 RVA: 0x0001A70C File Offset: 0x0001890C
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

		// Token: 0x06000552 RID: 1362 RVA: 0x0001A71C File Offset: 0x0001891C
		public void Play()
		{
			this.Play(this.clip);
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0001A72C File Offset: 0x0001892C
		public void Play(AudioClip clip)
		{
			if (!clip)
			{
				return;
			}
		}

		// Token: 0x04000470 RID: 1136
		public static readonly SoundPool.Player2D Default = new SoundPool.Player2D
		{
			super = SoundPool.PlayerShared.Default,
			pan = SoundPool.DEF.pan
		};

		// Token: 0x04000471 RID: 1137
		public SoundPool.PlayerShared super;

		// Token: 0x04000472 RID: 1138
		public float pan;
	}

	// Token: 0x020000E2 RID: 226
	private struct Dir
	{
		// Token: 0x04000473 RID: 1139
		public SoundPool.Node node;

		// Token: 0x04000474 RID: 1140
		public bool has;
	}

	// Token: 0x020000E3 RID: 227
	private struct Way
	{
		// Token: 0x04000475 RID: 1141
		public SoundPool.Dir prev;

		// Token: 0x04000476 RID: 1142
		public SoundPool.Dir next;
	}

	// Token: 0x020000E4 RID: 228
	private enum RootID : sbyte
	{
		// Token: 0x04000478 RID: 1144
		LIMBO,
		// Token: 0x04000479 RID: 1145
		PLAYING_ATTACHED = -3,
		// Token: 0x0400047A RID: 1146
		PLAYING_CAMERA,
		// Token: 0x0400047B RID: 1147
		PLAYING,
		// Token: 0x0400047C RID: 1148
		RESERVED = 1,
		// Token: 0x0400047D RID: 1149
		DISPOSED
	}

	// Token: 0x020000E5 RID: 229
	private class Root
	{
		// Token: 0x06000554 RID: 1364 RVA: 0x0001A73C File Offset: 0x0001893C
		public Root(SoundPool.RootID id)
		{
			this.id = id;
		}

		// Token: 0x0400047E RID: 1150
		public int count;

		// Token: 0x0400047F RID: 1151
		public SoundPool.Dir first;

		// Token: 0x04000480 RID: 1152
		public readonly SoundPool.RootID id;
	}

	// Token: 0x020000E6 RID: 230
	private static class NodeGC
	{
		// Token: 0x06000555 RID: 1365 RVA: 0x0001A74C File Offset: 0x0001894C
		public static void JOIN()
		{
			Transform[] array = null;
			bool flag = false;
			object destroyNextPumpLock = SoundPool.NodeGC.GCDAT.destroyNextPumpLock;
			lock (destroyNextPumpLock)
			{
				if (SoundPool.NodeGC.GCDAT.destroyNextQueued)
				{
					flag = true;
					array = SoundPool.NodeGC.GCDAT.destroyTheseNextPump.ToArray();
					SoundPool.NodeGC.GCDAT.destroyTheseNextPump.Clear();
					SoundPool.NodeGC.GCDAT.destroyNextQueued = false;
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

		// Token: 0x06000556 RID: 1366 RVA: 0x0001A814 File Offset: 0x00018A14
		public static void LEAK(Transform transform)
		{
			object destroyNextPumpLock = SoundPool.NodeGC.GCDAT.destroyNextPumpLock;
			lock (destroyNextPumpLock)
			{
				SoundPool.NodeGC.GCDAT.destroyNextQueued = true;
				SoundPool.NodeGC.GCDAT.destroyTheseNextPump.Add(transform);
			}
		}

		// Token: 0x020000E7 RID: 231
		private static class GCDAT
		{
			// Token: 0x06000557 RID: 1367 RVA: 0x0001A868 File Offset: 0x00018A68
			static GCDAT()
			{
				SoundPool.firstLeak = true;
			}

			// Token: 0x04000481 RID: 1153
			public static readonly List<Transform> destroyTheseNextPump = new List<Transform>();

			// Token: 0x04000482 RID: 1154
			public static readonly object destroyNextPumpLock = new object();

			// Token: 0x04000483 RID: 1155
			public static bool destroyNextQueued;
		}
	}

	// Token: 0x020000E8 RID: 232
	private sealed class Node : IDisposable
	{
		// Token: 0x06000559 RID: 1369 RVA: 0x0001A88C File Offset: 0x00018A8C
		public void Reserve()
		{
			switch (this.rootID)
			{
			case SoundPool.RootID.LIMBO:
				break;
			case SoundPool.RootID.RESERVED:
			case SoundPool.RootID.DISPOSED:
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
				this.way = default(SoundPool.Way);
				break;
			}
			this.root = SoundPool.reserved;
			this.rootID = SoundPool.RootID.RESERVED;
			this.way.next = SoundPool.reserved.first;
			if (this.way.next.has)
			{
				this.way.next.node.way.prev.has = true;
				this.way.next.node.way.prev.node = this;
			}
			SoundPool.reserved.first.has = true;
			SoundPool.reserved.first.node = this;
			SoundPool.reserved.count++;
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0001AA4C File Offset: 0x00018C4C
		public void EnterLimbo()
		{
			switch (this.rootID)
			{
			case SoundPool.RootID.LIMBO:
			case SoundPool.RootID.DISPOSED:
				return;
			case SoundPool.RootID.RESERVED:
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
			this.way = default(SoundPool.Way);
			this.root = null;
			this.rootID = SoundPool.RootID.LIMBO;
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0001AB6C File Offset: 0x00018D6C
		public void Bind()
		{
			this.way.prev = default(SoundPool.Dir);
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

		// Token: 0x0600055C RID: 1372 RVA: 0x0001AC30 File Offset: 0x00018E30
		public void Dispose()
		{
			switch (this.rootID)
			{
			case SoundPool.RootID.LIMBO:
				goto IL_2F;
			case SoundPool.RootID.DISPOSED:
				return;
			}
			this.EnterLimbo();
			IL_2F:
			Object.Destroy(this.transform.gameObject);
			this.transform = null;
			this.audio = null;
			this.rootID = SoundPool.RootID.DISPOSED;
			GC.SuppressFinalize(this);
			GC.KeepAlive(this);
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0001ACA0 File Offset: 0x00018EA0
		~Node()
		{
			if ((int)this.rootID != 2)
			{
				SoundPool.NodeGC.LEAK(this.transform);
			}
			this.transform = null;
			this.audio = null;
		}

		// Token: 0x04000484 RID: 1156
		public AudioSource audio;

		// Token: 0x04000485 RID: 1157
		public Transform transform;

		// Token: 0x04000486 RID: 1158
		public SoundPool.Way way;

		// Token: 0x04000487 RID: 1159
		public SoundPool.RootID rootID;

		// Token: 0x04000488 RID: 1160
		public SoundPool.Root root;

		// Token: 0x04000489 RID: 1161
		public Vector3 translation;

		// Token: 0x0400048A RID: 1162
		public Quaternion rotation;

		// Token: 0x0400048B RID: 1163
		public Transform parent;
	}
}
