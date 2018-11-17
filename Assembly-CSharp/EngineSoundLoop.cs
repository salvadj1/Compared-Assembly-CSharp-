using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000CE RID: 206
public class EngineSoundLoop : ScriptableObject
{
	// Token: 0x1700009F RID: 159
	// (get) Token: 0x06000444 RID: 1092 RVA: 0x00015954 File Offset: 0x00013B54
	private float volumeD
	{
		get
		{
			return this._dUpper.volume * 0.4f;
		}
	}

	// Token: 0x170000A0 RID: 160
	// (get) Token: 0x06000445 RID: 1093 RVA: 0x00015968 File Offset: 0x00013B68
	private float volumeF
	{
		get
		{
			return this._fMidHigh.volume * 0.4f;
		}
	}

	// Token: 0x170000A1 RID: 161
	// (get) Token: 0x06000446 RID: 1094 RVA: 0x0001597C File Offset: 0x00013B7C
	private float volumeE
	{
		get
		{
			return this._eMidLow.volume * 0.4f;
		}
	}

	// Token: 0x170000A2 RID: 162
	// (get) Token: 0x06000447 RID: 1095 RVA: 0x00015990 File Offset: 0x00013B90
	private float volumeK
	{
		get
		{
			return this._kPassing.volume * 0.7f;
		}
	}

	// Token: 0x170000A3 RID: 163
	// (get) Token: 0x06000448 RID: 1096 RVA: 0x000159A4 File Offset: 0x00013BA4
	private float volumeL
	{
		get
		{
			return this._lLower.volume * 0.4f;
		}
	}

	// Token: 0x06000449 RID: 1097 RVA: 0x000159B8 File Offset: 0x00013BB8
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

	// Token: 0x0600044A RID: 1098 RVA: 0x00015A04 File Offset: 0x00013C04
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

	// Token: 0x0600044B RID: 1099 RVA: 0x00015A80 File Offset: 0x00013C80
	public EngineSoundLoop.Instance Create(Transform attachTo, Vector3 localPosition)
	{
		if (!attachTo)
		{
			throw new MissingReferenceException("attachTo must not be null or destroyed");
		}
		EngineSoundLoop.Instance instance;
		if (this.instances == null)
		{
			this.instances = new Dictionary<Transform, EngineSoundLoop.Instance>();
		}
		else if (this.instances.TryGetValue(attachTo, out instance))
		{
			instance.localPosition = localPosition;
			return instance;
		}
		instance = new EngineSoundLoop.Instance(attachTo, localPosition, this);
		this.instances[attachTo] = instance;
		return instance;
	}

	// Token: 0x0600044C RID: 1100 RVA: 0x00015AF4 File Offset: 0x00013CF4
	public EngineSoundLoop.Instance Create(Transform attachTo)
	{
		return this.Create(attachTo, Vector3.zero);
	}

	// Token: 0x0600044D RID: 1101 RVA: 0x00015B04 File Offset: 0x00013D04
	public EngineSoundLoop.Instance CreateWorld(Transform attachTo, Vector3 worldPosition)
	{
		return this.Create(attachTo, attachTo.InverseTransformPoint(worldPosition));
	}

	// Token: 0x0600044E RID: 1102 RVA: 0x00015B14 File Offset: 0x00013D14
	private static float Sinerp(float start, float end, float value)
	{
		float num = Mathf.Sin(value * 1.57079637f);
		return (num > 0f) ? ((num < 1f) ? (end * num + start * (1f - num)) : end) : start;
	}

	// Token: 0x0600044F RID: 1103 RVA: 0x00015B60 File Offset: 0x00013D60
	private static float Coserp(float start, float end, float value)
	{
		float num = Mathf.Cos(value * 1.57079637f);
		return (num < 1f) ? ((num > 0f) ? (start * num + end * (1f - num)) : end) : start;
	}

	// Token: 0x040003B6 RID: 950
	private const float kPitchDefault_Idle = 0.7f;

	// Token: 0x040003B7 RID: 951
	private const float kPitchDefault_Start = 0.85f;

	// Token: 0x040003B8 RID: 952
	private const float kPitchDefault_Low = 1.17f;

	// Token: 0x040003B9 RID: 953
	private const float kPitchDefault_Medium = 1.25f;

	// Token: 0x040003BA RID: 954
	private const float kPitchDefault_High1 = 1.65f;

	// Token: 0x040003BB RID: 955
	private const float kPitchDefault_High2 = 1.76f;

	// Token: 0x040003BC RID: 956
	private const float kPitchDefault_High3 = 1.8f;

	// Token: 0x040003BD RID: 957
	private const float kPitchDefault_High4 = 1.86f;

	// Token: 0x040003BE RID: 958
	private const float kPitchDefault_Shift = 1.44f;

	// Token: 0x040003BF RID: 959
	private const float F_PITCH = 0.8f;

	// Token: 0x040003C0 RID: 960
	private const float F_THROTTLE = 0.7f;

	// Token: 0x040003C1 RID: 961
	private const float E_PITCH = 0.89f;

	// Token: 0x040003C2 RID: 962
	private const float E_THROTTLE = 0.8f;

	// Token: 0x040003C3 RID: 963
	private const float sD = 0.4f;

	// Token: 0x040003C4 RID: 964
	private const float sF = 0.4f;

	// Token: 0x040003C5 RID: 965
	private const float sE = 0.4f;

	// Token: 0x040003C6 RID: 966
	private const float sK = 0.7f;

	// Token: 0x040003C7 RID: 967
	private const float sL = 0.4f;

	// Token: 0x040003C8 RID: 968
	private const float F_PITCH_DELTA = 0.199999988f;

	// Token: 0x040003C9 RID: 969
	private const float F_THROTTLE_DELTA = 0.3f;

	// Token: 0x040003CA RID: 970
	private const float E_PITCH_DELTA = 0.110000014f;

	// Token: 0x040003CB RID: 971
	private const float E_THROTTLE_DELTA = 0.199999988f;

	// Token: 0x040003CC RID: 972
	[SerializeField]
	private EngineSoundLoop.Phrase _dUpper = new EngineSoundLoop.Phrase(0.565f);

	// Token: 0x040003CD RID: 973
	[SerializeField]
	private EngineSoundLoop.Phrase _fMidHigh = new EngineSoundLoop.Phrase(0.78f);

	// Token: 0x040003CE RID: 974
	[SerializeField]
	private EngineSoundLoop.Phrase _eMidLow = new EngineSoundLoop.Phrase(0.8f);

	// Token: 0x040003CF RID: 975
	[SerializeField]
	private EngineSoundLoop.Phrase _lLower = new EngineSoundLoop.Phrase(0.61f);

	// Token: 0x040003D0 RID: 976
	[SerializeField]
	private EngineSoundLoop.Phrase _kPassing = new EngineSoundLoop.Phrase(0.565f);

	// Token: 0x040003D1 RID: 977
	[SerializeField]
	private EngineSoundLoop.Gear _idleShiftUp = new EngineSoundLoop.Gear(1.17f, 1.65f);

	// Token: 0x040003D2 RID: 978
	[SerializeField]
	private EngineSoundLoop.Gear _shiftUp = new EngineSoundLoop.Gear(1.17f, 1.76f);

	// Token: 0x040003D3 RID: 979
	[SerializeField]
	private EngineSoundLoop.Gear[] _gears = new EngineSoundLoop.Gear[]
	{
		new EngineSoundLoop.Gear(0.7f, 1.65f),
		new EngineSoundLoop.Gear(0.85f, 1.76f),
		new EngineSoundLoop.Gear(1.17f, 1.8f),
		new EngineSoundLoop.Gear(1.25f, 1.86f)
	};

	// Token: 0x040003D4 RID: 980
	[SerializeField]
	private EngineSoundLoop.Gear _shiftDown = new EngineSoundLoop.Gear(1.44f, 1.17f);

	// Token: 0x040003D5 RID: 981
	[SerializeField]
	private float _shiftDuration = 0.1f;

	// Token: 0x040003D6 RID: 982
	[SerializeField]
	private float _volumeFromPitchBase = 0.85f;

	// Token: 0x040003D7 RID: 983
	[SerializeField]
	private float _volumeFromPitchRange = 0.909999967f;

	// Token: 0x040003D8 RID: 984
	[SerializeField]
	private int _topGear = 4;

	// Token: 0x040003D9 RID: 985
	[NonSerialized]
	private Dictionary<Transform, EngineSoundLoop.Instance> instances;

	// Token: 0x020000CF RID: 207
	[Serializable]
	private class Phrase
	{
		// Token: 0x06000450 RID: 1104 RVA: 0x00015BAC File Offset: 0x00013DAC
		public Phrase()
		{
			this.volume = 1f;
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00015BC0 File Offset: 0x00013DC0
		public Phrase(float volume)
		{
			this.volume = volume;
		}

		// Token: 0x040003DA RID: 986
		public AudioClip clip;

		// Token: 0x040003DB RID: 987
		public float volume;
	}

	// Token: 0x020000D0 RID: 208
	[Serializable]
	private class Gear
	{
		// Token: 0x06000452 RID: 1106 RVA: 0x00015BD0 File Offset: 0x00013DD0
		public Gear() : this(0.7f, 1.65f)
		{
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00015BE4 File Offset: 0x00013DE4
		public Gear(float lower, float upper)
		{
			this.lowVolume = lower;
			this.lowPitch = lower;
			this.highVolume = upper;
			this.highPitch = upper;
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00015C18 File Offset: 0x00013E18
		public Gear(float lowerPitch, float lowerVolume, float upperPitch, float upperVolume)
		{
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00015C20 File Offset: 0x00013E20
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

		// Token: 0x06000456 RID: 1110 RVA: 0x00015C9C File Offset: 0x00013E9C
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

		// Token: 0x040003DC RID: 988
		public float lowPitch;

		// Token: 0x040003DD RID: 989
		public float lowVolume;

		// Token: 0x040003DE RID: 990
		public float highPitch;

		// Token: 0x040003DF RID: 991
		public float highVolume;
	}

	// Token: 0x020000D1 RID: 209
	public class Instance : IDisposable
	{
		// Token: 0x06000457 RID: 1111 RVA: 0x00015D7C File Offset: 0x00013F7C
		internal Instance(Transform parent, Vector3 offset, EngineSoundLoop loop)
		{
			this.parent = parent;
			this.loop = loop;
			GameObject gameObject = new GameObject("_EnginePlayer", new Type[]
			{
				typeof(EngineSoundLoopPlayer)
			});
			this.player = gameObject.GetComponent<EngineSoundLoopPlayer>();
			this.player.instance = this;
			EngineSoundLoop.Instance.Setup(gameObject, ref this.D, ref this.flags, 1, loop._dUpper, 1f);
			EngineSoundLoop.Instance.Setup(gameObject, ref this.F, ref this.flags, 2, loop._fMidHigh, 1f);
			EngineSoundLoop.Instance.Setup(gameObject, ref this.E, ref this.flags, 4, loop._eMidLow, 1f);
			EngineSoundLoop.Instance.Setup(gameObject, ref this.L, ref this.flags, 8, loop._lLower, 1f);
			EngineSoundLoop.Instance.Setup(gameObject, ref this.K, ref this.flags, 16, loop._kPassing, 0f);
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

		// Token: 0x06000458 RID: 1112 RVA: 0x00015F20 File Offset: 0x00014120
		private static void Setup(GameObject go, ref AudioSource source, ref ushort flags, ushort flag, EngineSoundLoop.Phrase phrase, float volumeScalar)
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

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x00015F90 File Offset: 0x00014190
		// (set) Token: 0x0600045A RID: 1114 RVA: 0x00015FA0 File Offset: 0x000141A0
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

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x00015FD8 File Offset: 0x000141D8
		// (set) Token: 0x0600045C RID: 1116 RVA: 0x00015FF0 File Offset: 0x000141F0
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

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x00016040 File Offset: 0x00014240
		public bool playingOrPaused
		{
			get
			{
				return (this.flags & 96) == 64 || (this.flags & 160) == 128;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x00016074 File Offset: 0x00014274
		public bool stopped
		{
			get
			{
				return (this.flags & 192) == 0;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x00016088 File Offset: 0x00014288
		// (set) Token: 0x06000460 RID: 1120 RVA: 0x00016090 File Offset: 0x00014290
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

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x000160CC File Offset: 0x000142CC
		public bool hasUpdated
		{
			get
			{
				return (this.flags & 1024) == 1024;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x000160E4 File Offset: 0x000142E4
		public bool disposed
		{
			get
			{
				return (this.flags & 32) == 32;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x000160F4 File Offset: 0x000142F4
		public bool anySounds
		{
			get
			{
				return (this.flags & 31) != 0 && (this.flags & 32) == 0;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x00016114 File Offset: 0x00014314
		public float speedFactor
		{
			get
			{
				return this._speedFactor;
			}
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0001611C File Offset: 0x0001431C
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

		// Token: 0x06000466 RID: 1126 RVA: 0x000161A8 File Offset: 0x000143A8
		public void Play()
		{
			if ((this.flags & 96) == 0)
			{
				this.PLAY();
			}
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x000161C0 File Offset: 0x000143C0
		public void Stop()
		{
			if ((this.flags & 32) == 0 && (this.flags & 192) != 0)
			{
				this.STOP();
			}
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x000161E8 File Offset: 0x000143E8
		public void Pause()
		{
			if ((this.flags & 224) == 64)
			{
				this.PAUSE();
			}
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00016204 File Offset: 0x00014404
		public void Dispose()
		{
			this.Dispose(false);
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00016210 File Offset: 0x00014410
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

		// Token: 0x170000AD RID: 173
		// (set) Token: 0x0600046B RID: 1131 RVA: 0x00016304 File Offset: 0x00014504
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

		// Token: 0x0600046C RID: 1132 RVA: 0x00016334 File Offset: 0x00014534
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

		// Token: 0x0600046D RID: 1133 RVA: 0x000163FC File Offset: 0x000145FC
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

		// Token: 0x0600046E RID: 1134 RVA: 0x0001649C File Offset: 0x0001469C
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

		// Token: 0x0600046F RID: 1135 RVA: 0x00016550 File Offset: 0x00014750
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

		// Token: 0x06000470 RID: 1136 RVA: 0x0001682C File Offset: 0x00014A2C
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

		// Token: 0x06000471 RID: 1137 RVA: 0x00016A44 File Offset: 0x00014C44
		private void UPDATE_PASSING_VOLUME()
		{
			if ((this.flags & 16) == 16)
			{
				this.K.volume = (this._kVol = this.loop.volumeK * this._speedFactor) * this._masterVolume;
			}
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00016A90 File Offset: 0x00014C90
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

		// Token: 0x06000473 RID: 1139 RVA: 0x00016B64 File Offset: 0x00014D64
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

		// Token: 0x06000474 RID: 1140 RVA: 0x00016BF0 File Offset: 0x00014DF0
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
			EngineSoundLoop.Gear gear;
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

		// Token: 0x06000475 RID: 1141 RVA: 0x00016CF4 File Offset: 0x00014EF4
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
						this._lastSinerp = EngineSoundLoop.Sinerp(0f, (float)topGear, speedFactor);
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

		// Token: 0x040003E0 RID: 992
		private const ushort kD = 1;

		// Token: 0x040003E1 RID: 993
		private const ushort kF = 2;

		// Token: 0x040003E2 RID: 994
		private const ushort kE = 4;

		// Token: 0x040003E3 RID: 995
		private const ushort kL = 8;

		// Token: 0x040003E4 RID: 996
		private const ushort kK = 16;

		// Token: 0x040003E5 RID: 997
		private const ushort kDisposed = 32;

		// Token: 0x040003E6 RID: 998
		private const ushort kPlaying = 64;

		// Token: 0x040003E7 RID: 999
		private const ushort kPaused = 128;

		// Token: 0x040003E8 RID: 1000
		private const ushort kShifting = 256;

		// Token: 0x040003E9 RID: 1001
		private const ushort kShiftingDown = 256;

		// Token: 0x040003EA RID: 1002
		private const ushort kShiftingUp = 768;

		// Token: 0x040003EB RID: 1003
		private const ushort kFlagOnceUpdate = 1024;

		// Token: 0x040003EC RID: 1004
		private const ushort FLAGS_MASK = 65535;

		// Token: 0x040003ED RID: 1005
		private const ushort nD = 65534;

		// Token: 0x040003EE RID: 1006
		private const ushort nF = 65533;

		// Token: 0x040003EF RID: 1007
		private const ushort nE = 65531;

		// Token: 0x040003F0 RID: 1008
		private const ushort nL = 65527;

		// Token: 0x040003F1 RID: 1009
		private const ushort nK = 65519;

		// Token: 0x040003F2 RID: 1010
		private const ushort nDisposed = 65503;

		// Token: 0x040003F3 RID: 1011
		private const ushort nPlaying = 65471;

		// Token: 0x040003F4 RID: 1012
		private const ushort nPaused = 65407;

		// Token: 0x040003F5 RID: 1013
		private const ushort nShifting = 65279;

		// Token: 0x040003F6 RID: 1014
		private const ushort nShiftingDown = 65279;

		// Token: 0x040003F7 RID: 1015
		private const ushort nShiftingUp = 64767;

		// Token: 0x040003F8 RID: 1016
		private const ushort kPlayingOrPaused = 192;

		// Token: 0x040003F9 RID: 1017
		private const ushort kShiftingUpOrDown = 768;

		// Token: 0x040003FA RID: 1018
		private const ushort nPlayingOrPaused = 65343;

		// Token: 0x040003FB RID: 1019
		private const ushort nShiftingUpOrDown = 64767;

		// Token: 0x040003FC RID: 1020
		[NonSerialized]
		private EngineSoundLoop loop;

		// Token: 0x040003FD RID: 1021
		[NonSerialized]
		private EngineSoundLoopPlayer player;

		// Token: 0x040003FE RID: 1022
		[NonSerialized]
		private Transform parent;

		// Token: 0x040003FF RID: 1023
		[NonSerialized]
		private AudioSource D;

		// Token: 0x04000400 RID: 1024
		[NonSerialized]
		private AudioSource E;

		// Token: 0x04000401 RID: 1025
		[NonSerialized]
		private AudioSource F;

		// Token: 0x04000402 RID: 1026
		[NonSerialized]
		private AudioSource L;

		// Token: 0x04000403 RID: 1027
		[NonSerialized]
		private AudioSource K;

		// Token: 0x04000404 RID: 1028
		[NonSerialized]
		private float _volume;

		// Token: 0x04000405 RID: 1029
		[NonSerialized]
		private float _pitch;

		// Token: 0x04000406 RID: 1030
		[NonSerialized]
		private float _masterVolume;

		// Token: 0x04000407 RID: 1031
		[NonSerialized]
		private float _speedFactor;

		// Token: 0x04000408 RID: 1032
		[NonSerialized]
		private float _shiftTime;

		// Token: 0x04000409 RID: 1033
		[NonSerialized]
		private float _throttle;

		// Token: 0x0400040A RID: 1034
		[NonSerialized]
		private float _lastPitchFactor;

		// Token: 0x0400040B RID: 1035
		[NonSerialized]
		private float _lastSinerp;

		// Token: 0x0400040C RID: 1036
		[NonSerialized]
		private float _lastVolumeFactor;

		// Token: 0x0400040D RID: 1037
		[NonSerialized]
		private float _lastClampedThrottle;

		// Token: 0x0400040E RID: 1038
		[NonSerialized]
		private float _dVol;

		// Token: 0x0400040F RID: 1039
		[NonSerialized]
		private float _fVol;

		// Token: 0x04000410 RID: 1040
		[NonSerialized]
		private float _eVol;

		// Token: 0x04000411 RID: 1041
		[NonSerialized]
		private float _kVol;

		// Token: 0x04000412 RID: 1042
		[NonSerialized]
		private ushort flags;

		// Token: 0x04000413 RID: 1043
		[NonSerialized]
		private byte _gear;

		// Token: 0x04000414 RID: 1044
		[NonSerialized]
		private sbyte _lastVolumeFactorClamp;
	}
}
