using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000E2 RID: 226
public class EngineSoundLoop : ScriptableObject
{
	// Token: 0x170000B9 RID: 185
	// (get) Token: 0x060004C2 RID: 1218 RVA: 0x0001731C File Offset: 0x0001551C
	private float volumeD
	{
		get
		{
			return this._dUpper.volume * 0.4f;
		}
	}

	// Token: 0x170000BA RID: 186
	// (get) Token: 0x060004C3 RID: 1219 RVA: 0x00017330 File Offset: 0x00015530
	private float volumeF
	{
		get
		{
			return this._fMidHigh.volume * 0.4f;
		}
	}

	// Token: 0x170000BB RID: 187
	// (get) Token: 0x060004C4 RID: 1220 RVA: 0x00017344 File Offset: 0x00015544
	private float volumeE
	{
		get
		{
			return this._eMidLow.volume * 0.4f;
		}
	}

	// Token: 0x170000BC RID: 188
	// (get) Token: 0x060004C5 RID: 1221 RVA: 0x00017358 File Offset: 0x00015558
	private float volumeK
	{
		get
		{
			return this._kPassing.volume * 0.7f;
		}
	}

	// Token: 0x170000BD RID: 189
	// (get) Token: 0x060004C6 RID: 1222 RVA: 0x0001736C File Offset: 0x0001556C
	private float volumeL
	{
		get
		{
			return this._lLower.volume * 0.4f;
		}
	}

	// Token: 0x060004C7 RID: 1223 RVA: 0x00017380 File Offset: 0x00015580
	private sbyte VolumeFactor(float pitch, out float between)
	{
		between = (pitch - this._volumeFromPitchBase) / this._volumeFromPitchRange;
		if (between >= 1f)
		{
			between = 1f;
			return 1;
		}
		if (between <= 0f)
		{
			between = 0f;
			return -1;
		}
		return 0;
	}

	// Token: 0x060004C8 RID: 1224 RVA: 0x000173CC File Offset: 0x000155CC
	private void GearLerp(byte gear, float factor, ref float pitch, ref bool pitchChanged, ref float volume, ref bool volumeChanged)
	{
		int num;
		if (this._gears == null || (num = this._gears.Length) == 0)
		{
			return;
		}
		int num2;
		if (this._topGear < num)
		{
			num2 = this._topGear;
		}
		else
		{
			num2 = num - 1;
		}
		if ((int)gear > num2)
		{
			this._gears[num2].CompareLerp(factor, ref pitch, ref pitchChanged, ref volume, ref volumeChanged);
		}
		else
		{
			this._gears[(int)gear].CompareLerp(factor, ref pitch, ref pitchChanged, ref volume, ref volumeChanged);
		}
	}

	// Token: 0x060004C9 RID: 1225 RVA: 0x00017448 File Offset: 0x00015648
	public global::EngineSoundLoop.Instance Create(Transform attachTo, Vector3 localPosition)
	{
		if (!attachTo)
		{
			throw new MissingReferenceException("attachTo must not be null or destroyed");
		}
		global::EngineSoundLoop.Instance instance;
		if (this.instances == null)
		{
			this.instances = new Dictionary<Transform, global::EngineSoundLoop.Instance>();
		}
		else if (this.instances.TryGetValue(attachTo, out instance))
		{
			instance.localPosition = localPosition;
			return instance;
		}
		instance = new global::EngineSoundLoop.Instance(attachTo, localPosition, this);
		this.instances[attachTo] = instance;
		return instance;
	}

	// Token: 0x060004CA RID: 1226 RVA: 0x000174BC File Offset: 0x000156BC
	public global::EngineSoundLoop.Instance Create(Transform attachTo)
	{
		return this.Create(attachTo, Vector3.zero);
	}

	// Token: 0x060004CB RID: 1227 RVA: 0x000174CC File Offset: 0x000156CC
	public global::EngineSoundLoop.Instance CreateWorld(Transform attachTo, Vector3 worldPosition)
	{
		return this.Create(attachTo, attachTo.InverseTransformPoint(worldPosition));
	}

	// Token: 0x060004CC RID: 1228 RVA: 0x000174DC File Offset: 0x000156DC
	private static float Sinerp(float start, float end, float value)
	{
		float num = Mathf.Sin(value * 1.57079637f);
		return (num > 0f) ? ((num < 1f) ? (end * num + start * (1f - num)) : end) : start;
	}

	// Token: 0x060004CD RID: 1229 RVA: 0x00017528 File Offset: 0x00015728
	private static float Coserp(float start, float end, float value)
	{
		float num = Mathf.Cos(value * 1.57079637f);
		return (num < 1f) ? ((num > 0f) ? (start * num + end * (1f - num)) : end) : start;
	}

	// Token: 0x04000425 RID: 1061
	private const float kPitchDefault_Idle = 0.7f;

	// Token: 0x04000426 RID: 1062
	private const float kPitchDefault_Start = 0.85f;

	// Token: 0x04000427 RID: 1063
	private const float kPitchDefault_Low = 1.17f;

	// Token: 0x04000428 RID: 1064
	private const float kPitchDefault_Medium = 1.25f;

	// Token: 0x04000429 RID: 1065
	private const float kPitchDefault_High1 = 1.65f;

	// Token: 0x0400042A RID: 1066
	private const float kPitchDefault_High2 = 1.76f;

	// Token: 0x0400042B RID: 1067
	private const float kPitchDefault_High3 = 1.8f;

	// Token: 0x0400042C RID: 1068
	private const float kPitchDefault_High4 = 1.86f;

	// Token: 0x0400042D RID: 1069
	private const float kPitchDefault_Shift = 1.44f;

	// Token: 0x0400042E RID: 1070
	private const float F_PITCH = 0.8f;

	// Token: 0x0400042F RID: 1071
	private const float F_THROTTLE = 0.7f;

	// Token: 0x04000430 RID: 1072
	private const float E_PITCH = 0.89f;

	// Token: 0x04000431 RID: 1073
	private const float E_THROTTLE = 0.8f;

	// Token: 0x04000432 RID: 1074
	private const float sD = 0.4f;

	// Token: 0x04000433 RID: 1075
	private const float sF = 0.4f;

	// Token: 0x04000434 RID: 1076
	private const float sE = 0.4f;

	// Token: 0x04000435 RID: 1077
	private const float sK = 0.7f;

	// Token: 0x04000436 RID: 1078
	private const float sL = 0.4f;

	// Token: 0x04000437 RID: 1079
	private const float F_PITCH_DELTA = 0.199999988f;

	// Token: 0x04000438 RID: 1080
	private const float F_THROTTLE_DELTA = 0.3f;

	// Token: 0x04000439 RID: 1081
	private const float E_PITCH_DELTA = 0.110000014f;

	// Token: 0x0400043A RID: 1082
	private const float E_THROTTLE_DELTA = 0.199999988f;

	// Token: 0x0400043B RID: 1083
	[SerializeField]
	private global::EngineSoundLoop.Phrase _dUpper = new global::EngineSoundLoop.Phrase(0.565f);

	// Token: 0x0400043C RID: 1084
	[SerializeField]
	private global::EngineSoundLoop.Phrase _fMidHigh = new global::EngineSoundLoop.Phrase(0.78f);

	// Token: 0x0400043D RID: 1085
	[SerializeField]
	private global::EngineSoundLoop.Phrase _eMidLow = new global::EngineSoundLoop.Phrase(0.8f);

	// Token: 0x0400043E RID: 1086
	[SerializeField]
	private global::EngineSoundLoop.Phrase _lLower = new global::EngineSoundLoop.Phrase(0.61f);

	// Token: 0x0400043F RID: 1087
	[SerializeField]
	private global::EngineSoundLoop.Phrase _kPassing = new global::EngineSoundLoop.Phrase(0.565f);

	// Token: 0x04000440 RID: 1088
	[SerializeField]
	private global::EngineSoundLoop.Gear _idleShiftUp = new global::EngineSoundLoop.Gear(1.17f, 1.65f);

	// Token: 0x04000441 RID: 1089
	[SerializeField]
	private global::EngineSoundLoop.Gear _shiftUp = new global::EngineSoundLoop.Gear(1.17f, 1.76f);

	// Token: 0x04000442 RID: 1090
	[SerializeField]
	private global::EngineSoundLoop.Gear[] _gears = new global::EngineSoundLoop.Gear[]
	{
		new global::EngineSoundLoop.Gear(0.7f, 1.65f),
		new global::EngineSoundLoop.Gear(0.85f, 1.76f),
		new global::EngineSoundLoop.Gear(1.17f, 1.8f),
		new global::EngineSoundLoop.Gear(1.25f, 1.86f)
	};

	// Token: 0x04000443 RID: 1091
	[SerializeField]
	private global::EngineSoundLoop.Gear _shiftDown = new global::EngineSoundLoop.Gear(1.44f, 1.17f);

	// Token: 0x04000444 RID: 1092
	[SerializeField]
	private float _shiftDuration = 0.1f;

	// Token: 0x04000445 RID: 1093
	[SerializeField]
	private float _volumeFromPitchBase = 0.85f;

	// Token: 0x04000446 RID: 1094
	[SerializeField]
	private float _volumeFromPitchRange = 0.909999967f;

	// Token: 0x04000447 RID: 1095
	[SerializeField]
	private int _topGear = 4;

	// Token: 0x04000448 RID: 1096
	[NonSerialized]
	private Dictionary<Transform, global::EngineSoundLoop.Instance> instances;

	// Token: 0x020000E3 RID: 227
	[Serializable]
	private class Phrase
	{
		// Token: 0x060004CE RID: 1230 RVA: 0x00017574 File Offset: 0x00015774
		public Phrase()
		{
			this.volume = 1f;
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00017588 File Offset: 0x00015788
		public Phrase(float volume)
		{
			this.volume = volume;
		}

		// Token: 0x04000449 RID: 1097
		public AudioClip clip;

		// Token: 0x0400044A RID: 1098
		public float volume;
	}

	// Token: 0x020000E4 RID: 228
	[Serializable]
	private class Gear
	{
		// Token: 0x060004D0 RID: 1232 RVA: 0x00017598 File Offset: 0x00015798
		public Gear() : this(0.7f, 1.65f)
		{
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x000175AC File Offset: 0x000157AC
		public Gear(float lower, float upper)
		{
			this.lowVolume = lower;
			this.lowPitch = lower;
			this.highVolume = upper;
			this.highPitch = upper;
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x000175E0 File Offset: 0x000157E0
		public Gear(float lowerPitch, float lowerVolume, float upperPitch, float upperVolume)
		{
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x000175E8 File Offset: 0x000157E8
		public void Lerp(float t, out float pitch, out float volume)
		{
			if (t <= 0f)
			{
				pitch = this.lowPitch;
				volume = this.lowVolume;
			}
			else if (t >= 1f)
			{
				pitch = this.highPitch;
				volume = this.highVolume;
			}
			else
			{
				float num = 1f - t;
				pitch = this.lowPitch * num + this.highPitch * t;
				volume = this.lowVolume * num + this.highVolume * t;
			}
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00017664 File Offset: 0x00015864
		public void CompareLerp(float t, ref float pitch, ref bool pitchChanged, ref float volume, ref bool volumeChanged)
		{
			if (t <= 0f)
			{
				if (pitch != this.lowPitch)
				{
					pitchChanged = true;
					pitch = this.lowPitch;
				}
				if (volume != this.lowVolume)
				{
					volumeChanged = true;
					volume = this.lowVolume;
				}
			}
			else if (t >= 1f)
			{
				if (pitch != this.highPitch)
				{
					pitchChanged = true;
					pitch = this.highPitch;
				}
				if (volume != this.highVolume)
				{
					volumeChanged = true;
					volume = this.highVolume;
				}
			}
			else
			{
				float num = 1f - t;
				float num2 = this.lowPitch * num + this.highPitch * t;
				float num3 = this.lowVolume * num + this.highVolume * t;
				if (pitch != num2)
				{
					pitchChanged = true;
					pitch = num2;
				}
				if (volume != num3)
				{
					volumeChanged = true;
					volume = num3;
				}
			}
		}

		// Token: 0x0400044B RID: 1099
		public float lowPitch;

		// Token: 0x0400044C RID: 1100
		public float lowVolume;

		// Token: 0x0400044D RID: 1101
		public float highPitch;

		// Token: 0x0400044E RID: 1102
		public float highVolume;
	}

	// Token: 0x020000E5 RID: 229
	public class Instance : IDisposable
	{
		// Token: 0x060004D5 RID: 1237 RVA: 0x00017744 File Offset: 0x00015944
		internal Instance(Transform parent, Vector3 offset, global::EngineSoundLoop loop)
		{
			this.parent = parent;
			this.loop = loop;
			GameObject gameObject = new GameObject("_EnginePlayer", new Type[]
			{
				typeof(EngineSoundLoopPlayer)
			});
			this.player = gameObject.GetComponent<EngineSoundLoopPlayer>();
			this.player.instance = this;
			global::EngineSoundLoop.Instance.Setup(gameObject, ref this.D, ref this.flags, 1, loop._dUpper, 1f);
			global::EngineSoundLoop.Instance.Setup(gameObject, ref this.F, ref this.flags, 2, loop._fMidHigh, 1f);
			global::EngineSoundLoop.Instance.Setup(gameObject, ref this.E, ref this.flags, 4, loop._eMidLow, 1f);
			global::EngineSoundLoop.Instance.Setup(gameObject, ref this.L, ref this.flags, 8, loop._lLower, 1f);
			global::EngineSoundLoop.Instance.Setup(gameObject, ref this.K, ref this.flags, 16, loop._kPassing, 0f);
			this._lastVolumeFactor = (this._lastClampedThrottle = (this._lastSinerp = (this._lastPitchFactor = float.NegativeInfinity)));
			this._lastVolumeFactorClamp = sbyte.MinValue;
			this._masterVolume = 1f;
			this._pitch = loop._idleShiftUp.lowVolume;
			this._shiftTime = -3000f;
			this._speedFactor = (this._dVol = (this._fVol = (this._eVol = (this._kVol = (this._throttle = 0f)))));
			this._gear = 0;
			Transform transform = gameObject.transform;
			transform.parent = parent;
			transform.localPosition = offset;
			transform.localRotation = Quaternion.identity;
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x000178E8 File Offset: 0x00015AE8
		private static void Setup(GameObject go, ref AudioSource source, ref ushort flags, ushort flag, global::EngineSoundLoop.Phrase phrase, float volumeScalar)
		{
			if (phrase == null || !phrase.clip)
			{
				return;
			}
			source = go.AddComponent<AudioSource>();
			source.playOnAwake = false;
			source.loop = true;
			source.clip = phrase.clip;
			source.volume = phrase.volume * volumeScalar;
			source.dopplerLevel = 0f;
			flags |= flag;
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x00017958 File Offset: 0x00015B58
		// (set) Token: 0x060004D8 RID: 1240 RVA: 0x00017968 File Offset: 0x00015B68
		public bool playing
		{
			get
			{
				return (this.flags & 96) == 64;
			}
			set
			{
				if (value)
				{
					if ((this.flags & 96) == 0)
					{
						this.PLAY();
					}
				}
				else if ((this.flags & 96) == 64)
				{
					this.PAUSE();
				}
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x000179A0 File Offset: 0x00015BA0
		// (set) Token: 0x060004DA RID: 1242 RVA: 0x000179B8 File Offset: 0x00015BB8
		public bool paused
		{
			get
			{
				return (this.flags & 160) == 128;
			}
			set
			{
				if (value)
				{
					if ((this.flags & 224) == 64)
					{
						this.PAUSE();
					}
				}
				else if ((this.flags & 224) == 128)
				{
					this.PLAY();
				}
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x00017A08 File Offset: 0x00015C08
		public bool playingOrPaused
		{
			get
			{
				return (this.flags & 96) == 64 || (this.flags & 160) == 128;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x00017A3C File Offset: 0x00015C3C
		public bool stopped
		{
			get
			{
				return (this.flags & 192) == 0;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x00017A50 File Offset: 0x00015C50
		// (set) Token: 0x060004DE RID: 1246 RVA: 0x00017A58 File Offset: 0x00015C58
		public float volume
		{
			get
			{
				return this._masterVolume;
			}
			set
			{
				if (value < 0f)
				{
					value = 0f;
				}
				if (this._masterVolume != value)
				{
					this._masterVolume = value;
					if ((this.flags & 32) == 0)
					{
						this.UPDATE_MASTER_VOLUME();
					}
				}
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x00017A94 File Offset: 0x00015C94
		public bool hasUpdated
		{
			get
			{
				return (this.flags & 1024) == 1024;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x00017AAC File Offset: 0x00015CAC
		public bool disposed
		{
			get
			{
				return (this.flags & 32) == 32;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x00017ABC File Offset: 0x00015CBC
		public bool anySounds
		{
			get
			{
				return (this.flags & 31) != 0 && (this.flags & 32) == 0;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x00017ADC File Offset: 0x00015CDC
		public float speedFactor
		{
			get
			{
				return this._speedFactor;
			}
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00017AE4 File Offset: 0x00015CE4
		public void Update(float speedFactor, float throttle)
		{
			int num = (int)(this.flags & 1056);
			if (num != 32)
			{
				if (num != 1024)
				{
					this.flags |= 1024;
					this.UPDATE(speedFactor, throttle);
					if ((this.flags & 192) == 64)
					{
						this.PLAY();
					}
				}
				else
				{
					this.UPDATE(speedFactor, throttle);
				}
			}
			else
			{
				this._speedFactor = speedFactor;
				this._throttle = throttle;
			}
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00017B70 File Offset: 0x00015D70
		public void Play()
		{
			if ((this.flags & 96) == 0)
			{
				this.PLAY();
			}
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00017B88 File Offset: 0x00015D88
		public void Stop()
		{
			if ((this.flags & 32) == 0 && (this.flags & 192) != 0)
			{
				this.STOP();
			}
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00017BB0 File Offset: 0x00015DB0
		public void Pause()
		{
			if ((this.flags & 224) == 64)
			{
				this.PAUSE();
			}
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00017BCC File Offset: 0x00015DCC
		public void Dispose()
		{
			this.Dispose(false);
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00017BD8 File Offset: 0x00015DD8
		internal void Dispose(bool fromPlayer)
		{
			if ((this.flags & 32) == 32)
			{
				return;
			}
			if (this.loop && this.loop.instances != null)
			{
				this.loop.instances.Remove(this.parent);
			}
			this.D = (this.E = (this.F = (this.L = (this.K = null))));
			if (!fromPlayer && this.player)
			{
				try
				{
					this.player.instance = null;
					Object.Destroy(this.player.gameObject);
				}
				catch (Exception ex)
				{
					Debug.LogError(ex, this.player);
				}
			}
			this.player = null;
			this.flags = 32;
		}

		// Token: 0x170000C7 RID: 199
		// (set) Token: 0x060004E9 RID: 1257 RVA: 0x00017CCC File Offset: 0x00015ECC
		internal Vector3 localPosition
		{
			set
			{
				if ((this.flags & 32) == 32)
				{
					return;
				}
				this.player.transform.localPosition = value;
			}
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00017CFC File Offset: 0x00015EFC
		private void PLAY()
		{
			if ((this.flags & 1024) == 1024)
			{
				if ((this.flags & 1) == 1)
				{
					this.D.Play();
				}
				if ((this.flags & 2) == 2)
				{
					this.F.Play();
				}
				if ((this.flags & 4) == 4)
				{
					this.E.Play();
				}
				if ((this.flags & 8) == 8)
				{
					this.L.Play();
				}
				if ((this.flags & 16) == 16)
				{
					this.K.Play();
				}
			}
			this.flags |= 64;
			this.flags &= 65407;
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00017DC4 File Offset: 0x00015FC4
		private void STOP()
		{
			if ((this.flags & 1) == 1)
			{
				this.D.Stop();
			}
			if ((this.flags & 2) == 2)
			{
				this.F.Stop();
			}
			if ((this.flags & 4) == 4)
			{
				this.E.Stop();
			}
			if ((this.flags & 8) == 8)
			{
				this.L.Stop();
			}
			if ((this.flags & 16) == 16)
			{
				this.K.Stop();
			}
			this.flags &= 65343;
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00017E64 File Offset: 0x00016064
		private void PAUSE()
		{
			if ((this.flags & 1) == 1)
			{
				this.D.Pause();
			}
			if ((this.flags & 2) == 2)
			{
				this.F.Pause();
			}
			if ((this.flags & 4) == 4)
			{
				this.E.Pause();
			}
			if ((this.flags & 8) == 8)
			{
				this.L.Pause();
			}
			if ((this.flags & 16) == 16)
			{
				this.K.Pause();
			}
			this.flags |= 128;
			this.flags &= 65471;
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00017F18 File Offset: 0x00016118
		private void UPDATE_PITCH_AND_OR_THROTTLE_VOLUME()
		{
			ushort num = this.flags;
			num &= 7;
			if (num != 0)
			{
				float num2;
				sbyte b = this.loop.VolumeFactor(this._volume, out num2);
				bool flag;
				if ((int)this._lastVolumeFactorClamp != (int)b || this._lastVolumeFactor != num2)
				{
					flag = true;
					this._lastVolumeFactor = num2;
					this._lastVolumeFactorClamp = b;
					if ((num & 1) == 1)
					{
						this.D.volume = (((int)b != -1) ? (this._masterVolume * (this._dVol = (((int)b != 1) ? (this.loop.volumeD * num2) : this.loop.volumeD))) : (this._dVol = 0f));
					}
				}
				else
				{
					flag = false;
				}
				num &= 65534;
				if (num != 0)
				{
					float num3 = Mathf.Clamp01(this._throttle);
					if (num3 != this._lastClampedThrottle)
					{
						this._lastClampedThrottle = num3;
						flag = true;
					}
					if (flag)
					{
						sbyte b2 = b;
						switch (b2 + 1)
						{
						case 0:
							if ((num & 2) == 2)
							{
								this.F.volume = (this._fVol = this.loop.volumeF * 0.8f * (0.7f + 0.3f * num3)) * this._masterVolume;
							}
							if ((num & 4) == 4)
							{
								this.E.volume = (this._eVol = this.loop.volumeE * 0.89f * (0.8f + 0.199999988f * num3)) * this._masterVolume;
							}
							return;
						case 2:
							if ((num & 2) == 2)
							{
								this.F.volume = (this._fVol = this.loop.volumeF * (0.7f + 0.3f * num3)) * this._masterVolume;
							}
							if ((num & 4) == 4)
							{
								this.E.volume = (this._eVol = this.loop.volumeE * (0.8f + 0.199999988f * num3)) * this._masterVolume;
							}
							return;
						}
						if ((num & 2) == 2)
						{
							this.F.volume = (this._fVol = this.loop.volumeF * (0.8f + 0.199999988f * num2) * (0.7f + 0.3f * num3)) * this._masterVolume;
						}
						if ((num & 4) == 4)
						{
							this.E.volume = (this._eVol = this.loop.volumeE * (0.89f + 0.110000014f * num2) * (0.8f + 0.199999988f * num3)) * this._masterVolume;
						}
					}
				}
			}
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x000181F4 File Offset: 0x000163F4
		private void UPDATE_THROTTLE_VOLUME()
		{
			ushort num = this.flags;
			num &= 6;
			if (num != 0)
			{
				float num2 = Mathf.Clamp01(this._throttle);
				if (num2 != this._lastClampedThrottle)
				{
					float lastVolumeFactor = this._lastVolumeFactor;
					this._lastClampedThrottle = num2;
					sbyte lastVolumeFactorClamp = this._lastVolumeFactorClamp;
					switch (lastVolumeFactorClamp + 1)
					{
					case 0:
						if ((num & 2) == 2)
						{
							this.F.volume = (this._fVol = this.loop.volumeF * 0.8f * (0.7f + 0.3f * num2)) * this._masterVolume;
						}
						if ((num & 4) == 4)
						{
							this.E.volume = (this._eVol = this.loop.volumeE * 0.89f * (0.8f + 0.199999988f * num2)) * this._masterVolume;
						}
						return;
					case 2:
						if ((num & 2) == 2)
						{
							this.F.volume = (this._fVol = this.loop.volumeF * (0.7f + 0.3f * num2)) * this._masterVolume;
						}
						if ((num & 4) == 4)
						{
							this.E.volume = (this._eVol = this.loop.volumeE * (0.8f + 0.199999988f * num2)) * this._masterVolume;
						}
						return;
					}
					if ((num & 2) == 2)
					{
						this.F.volume = (this._fVol = this.loop.volumeF * (0.8f + 0.199999988f * lastVolumeFactor) * (0.7f + 0.3f * num2)) * this._masterVolume;
					}
					if ((num & 4) == 4)
					{
						this.E.volume = (this._eVol = this.loop.volumeE * (0.89f + 0.110000014f * lastVolumeFactor) * (0.8f + 0.199999988f * num2)) * this._masterVolume;
					}
				}
			}
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0001840C File Offset: 0x0001660C
		private void UPDATE_PASSING_VOLUME()
		{
			if ((this.flags & 16) == 16)
			{
				this.K.volume = (this._kVol = this.loop.volumeK * this._speedFactor) * this._masterVolume;
			}
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00018458 File Offset: 0x00016658
		private void UPDATE_MASTER_VOLUME()
		{
			if ((this.flags & 1) == 1)
			{
				this.D.volume = this._dVol * this._masterVolume;
			}
			if ((this.flags & 2) == 2)
			{
				this.F.volume = this._fVol * this._masterVolume;
			}
			if ((this.flags & 4) == 4)
			{
				this.E.volume = this._eVol * this._masterVolume;
			}
			if ((this.flags & 8) == 8)
			{
				this.L.volume = this.loop.volumeL * this._masterVolume;
			}
			if ((this.flags & 16) == 16)
			{
				this.K.volume = this._kVol * this._masterVolume;
			}
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0001852C File Offset: 0x0001672C
		private void UPDATE_RATES()
		{
			if ((this.flags & 1) == 1)
			{
				this.D.pitch = this._pitch;
			}
			if ((this.flags & 2) == 2)
			{
				this.F.pitch = this._pitch;
			}
			if ((this.flags & 4) == 4)
			{
				this.E.pitch = this._pitch;
			}
			if ((this.flags & 8) == 8)
			{
				this.L.pitch = this._pitch;
			}
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x000185B8 File Offset: 0x000167B8
		private bool UPDATE_SHIFTING(ref bool doPitchAdjust, ref bool doVolumeAdjust)
		{
			float num = Time.time - this._shiftTime;
			if (num >= this.loop._shiftDuration)
			{
				if ((this.flags & 768) == 768)
				{
					this._gear += 1;
				}
				else if (this._gear > 0)
				{
					this._gear -= 1;
				}
				this.flags &= 64767;
				return true;
			}
			float num2 = num / this.loop._shiftDuration;
			float t;
			global::EngineSoundLoop.Gear gear;
			if ((this.flags & 768) == 768)
			{
				t = this._lastPitchFactor * num2;
				if (this._gear == 0)
				{
					gear = this.loop._idleShiftUp;
				}
				else
				{
					gear = this.loop._shiftUp;
				}
			}
			else
			{
				t = num2;
				gear = this.loop._shiftDown;
			}
			gear.CompareLerp(t, ref this._pitch, ref doPitchAdjust, ref this._volume, ref doVolumeAdjust);
			return false;
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x000186BC File Offset: 0x000168BC
		private void UPDATE(float speedFactor, float throttle)
		{
			bool flag;
			if (throttle != this._throttle)
			{
				this._throttle = throttle;
				flag = true;
			}
			else
			{
				flag = false;
			}
			float pitch = this._pitch;
			float volume = this._volume;
			float speedFactor2 = this._speedFactor;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = speedFactor != speedFactor2;
			if (flag4)
			{
				this._speedFactor = speedFactor;
			}
			bool flag6;
			bool flag5;
			if ((this.flags & 256) == 256)
			{
				flag5 = (flag6 = this.UPDATE_SHIFTING(ref flag2, ref flag3));
			}
			else
			{
				flag6 = true;
				flag5 = false;
			}
			if (flag6)
			{
				float num3;
				do
				{
					bool flag7;
					byte b;
					if (flag4 || flag5)
					{
						int topGear = this.loop._topGear;
						this._lastSinerp = global::EngineSoundLoop.Sinerp(0f, (float)topGear, speedFactor);
						int num = (int)this._lastSinerp;
						if (num == (int)this._gear)
						{
							flag7 = false;
							b = ((num != topGear) ? this._gear : ((byte)(topGear - 1)));
						}
						else if (num < (int)this._gear)
						{
							if (this._gear > 0)
							{
								if ((int)this._gear == topGear)
								{
									this._gear -= 1;
									flag7 = false;
									b = this._gear;
								}
								else
								{
									flag7 = true;
									b = this._gear - 1;
								}
							}
							else
							{
								flag7 = false;
								b = this._gear;
							}
						}
						else if (this._gear < 255 && (int)this._gear < topGear)
						{
							if ((int)this._gear < topGear - 1)
							{
								flag7 = true;
								b = this._gear + 1;
							}
							else
							{
								flag7 = false;
								b = this._gear;
								this._gear += 1;
							}
						}
						else
						{
							flag7 = false;
							b = this._gear;
						}
					}
					else
					{
						flag7 = false;
						b = (((int)this._gear != this.loop._topGear) ? this._gear : (this._gear - 1));
					}
					float num2 = this._lastSinerp - (float)b;
					if (num2 == 0f)
					{
						num3 = 0f;
					}
					else if (throttle >= 0.5f)
					{
						num3 = num2;
					}
					else if (throttle <= 0f)
					{
						num3 = num2 * 0.3f;
					}
					else
					{
						num3 = num2 * (0.3f + throttle * 0.7f);
					}
					if (!flag7)
					{
						goto IL_2C4;
					}
					if (b > this._gear)
					{
						this.flags |= 768;
					}
					else
					{
						this.flags |= 256;
					}
					this._lastPitchFactor = num3;
					this._shiftTime = Time.time;
				}
				while (flag5 = this.UPDATE_SHIFTING(ref flag2, ref flag3));
				goto IL_2FF;
				IL_2C4:
				if (num3 != this._lastPitchFactor || flag5)
				{
					this._lastPitchFactor = num3;
					byte b;
					this.loop.GearLerp(b, num3, ref this._pitch, ref flag2, ref this._volume, ref flag3);
				}
			}
			IL_2FF:
			if (flag3 && this._volume != volume)
			{
				this.UPDATE_PITCH_AND_OR_THROTTLE_VOLUME();
			}
			else if (flag)
			{
				this.UPDATE_THROTTLE_VOLUME();
			}
			if (flag4)
			{
				this.UPDATE_PASSING_VOLUME();
			}
			if (flag2 && this._pitch != pitch)
			{
				this.UPDATE_RATES();
			}
		}

		// Token: 0x0400044F RID: 1103
		private const ushort kD = 1;

		// Token: 0x04000450 RID: 1104
		private const ushort kF = 2;

		// Token: 0x04000451 RID: 1105
		private const ushort kE = 4;

		// Token: 0x04000452 RID: 1106
		private const ushort kL = 8;

		// Token: 0x04000453 RID: 1107
		private const ushort kK = 16;

		// Token: 0x04000454 RID: 1108
		private const ushort kDisposed = 32;

		// Token: 0x04000455 RID: 1109
		private const ushort kPlaying = 64;

		// Token: 0x04000456 RID: 1110
		private const ushort kPaused = 128;

		// Token: 0x04000457 RID: 1111
		private const ushort kShifting = 256;

		// Token: 0x04000458 RID: 1112
		private const ushort kShiftingDown = 256;

		// Token: 0x04000459 RID: 1113
		private const ushort kShiftingUp = 768;

		// Token: 0x0400045A RID: 1114
		private const ushort kFlagOnceUpdate = 1024;

		// Token: 0x0400045B RID: 1115
		private const ushort FLAGS_MASK = 65535;

		// Token: 0x0400045C RID: 1116
		private const ushort nD = 65534;

		// Token: 0x0400045D RID: 1117
		private const ushort nF = 65533;

		// Token: 0x0400045E RID: 1118
		private const ushort nE = 65531;

		// Token: 0x0400045F RID: 1119
		private const ushort nL = 65527;

		// Token: 0x04000460 RID: 1120
		private const ushort nK = 65519;

		// Token: 0x04000461 RID: 1121
		private const ushort nDisposed = 65503;

		// Token: 0x04000462 RID: 1122
		private const ushort nPlaying = 65471;

		// Token: 0x04000463 RID: 1123
		private const ushort nPaused = 65407;

		// Token: 0x04000464 RID: 1124
		private const ushort nShifting = 65279;

		// Token: 0x04000465 RID: 1125
		private const ushort nShiftingDown = 65279;

		// Token: 0x04000466 RID: 1126
		private const ushort nShiftingUp = 64767;

		// Token: 0x04000467 RID: 1127
		private const ushort kPlayingOrPaused = 192;

		// Token: 0x04000468 RID: 1128
		private const ushort kShiftingUpOrDown = 768;

		// Token: 0x04000469 RID: 1129
		private const ushort nPlayingOrPaused = 65343;

		// Token: 0x0400046A RID: 1130
		private const ushort nShiftingUpOrDown = 64767;

		// Token: 0x0400046B RID: 1131
		[NonSerialized]
		private global::EngineSoundLoop loop;

		// Token: 0x0400046C RID: 1132
		[NonSerialized]
		private EngineSoundLoopPlayer player;

		// Token: 0x0400046D RID: 1133
		[NonSerialized]
		private Transform parent;

		// Token: 0x0400046E RID: 1134
		[NonSerialized]
		private AudioSource D;

		// Token: 0x0400046F RID: 1135
		[NonSerialized]
		private AudioSource E;

		// Token: 0x04000470 RID: 1136
		[NonSerialized]
		private AudioSource F;

		// Token: 0x04000471 RID: 1137
		[NonSerialized]
		private AudioSource L;

		// Token: 0x04000472 RID: 1138
		[NonSerialized]
		private AudioSource K;

		// Token: 0x04000473 RID: 1139
		[NonSerialized]
		private float _volume;

		// Token: 0x04000474 RID: 1140
		[NonSerialized]
		private float _pitch;

		// Token: 0x04000475 RID: 1141
		[NonSerialized]
		private float _masterVolume;

		// Token: 0x04000476 RID: 1142
		[NonSerialized]
		private float _speedFactor;

		// Token: 0x04000477 RID: 1143
		[NonSerialized]
		private float _shiftTime;

		// Token: 0x04000478 RID: 1144
		[NonSerialized]
		private float _throttle;

		// Token: 0x04000479 RID: 1145
		[NonSerialized]
		private float _lastPitchFactor;

		// Token: 0x0400047A RID: 1146
		[NonSerialized]
		private float _lastSinerp;

		// Token: 0x0400047B RID: 1147
		[NonSerialized]
		private float _lastVolumeFactor;

		// Token: 0x0400047C RID: 1148
		[NonSerialized]
		private float _lastClampedThrottle;

		// Token: 0x0400047D RID: 1149
		[NonSerialized]
		private float _dVol;

		// Token: 0x0400047E RID: 1150
		[NonSerialized]
		private float _fVol;

		// Token: 0x0400047F RID: 1151
		[NonSerialized]
		private float _eVol;

		// Token: 0x04000480 RID: 1152
		[NonSerialized]
		private float _kVol;

		// Token: 0x04000481 RID: 1153
		[NonSerialized]
		private ushort flags;

		// Token: 0x04000482 RID: 1154
		[NonSerialized]
		private byte _gear;

		// Token: 0x04000483 RID: 1155
		[NonSerialized]
		private sbyte _lastVolumeFactorClamp;
	}
}
