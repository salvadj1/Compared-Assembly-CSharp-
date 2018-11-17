using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MoPhoGames.USpeak.Core;
using MoPhoGames.USpeak.Core.Utils;
using MoPhoGames.USpeak.Interface;
using UnityEngine;

// Token: 0x020000CD RID: 205
[AddComponentMenu("USpeak/USpeaker")]
public class USpeaker : MonoBehaviour
{
	// Token: 0x170000AA RID: 170
	// (get) Token: 0x0600045A RID: 1114 RVA: 0x000158C4 File Offset: 0x00013AC4
	// (set) Token: 0x0600045B RID: 1115 RVA: 0x000158D0 File Offset: 0x00013AD0
	[Obsolete("Use USpeaker._3DMode instead")]
	public bool Is3D
	{
		get
		{
			return this._3DMode == global::ThreeDMode.SpeakerPan;
		}
		set
		{
			if (value)
			{
				this._3DMode = global::ThreeDMode.SpeakerPan;
			}
			else
			{
				this._3DMode = global::ThreeDMode.None;
			}
		}
	}

	// Token: 0x170000AB RID: 171
	// (get) Token: 0x0600045C RID: 1116 RVA: 0x000158EC File Offset: 0x00013AEC
	public bool IsTalking
	{
		get
		{
			return this.talkTimer > 0f;
		}
	}

	// Token: 0x0600045D RID: 1117 RVA: 0x000158FC File Offset: 0x00013AFC
	public bool HasSettings()
	{
		return this.settings != null;
	}

	// Token: 0x170000AC RID: 172
	// (get) Token: 0x0600045E RID: 1118 RVA: 0x0001590C File Offset: 0x00013B0C
	private int audioFrequency
	{
		get
		{
			if (this.recFreq == 0)
			{
				switch (this.BandwidthMode)
				{
				case global::BandMode.Narrow:
					this.recFreq = 8000;
					break;
				case global::BandMode.Wide:
					this.recFreq = 16000;
					break;
				case global::BandMode.UltraWide:
					this.recFreq = 32000;
					break;
				default:
					this.recFreq = 8000;
					break;
				}
			}
			return this.recFreq;
		}
	}

	// Token: 0x0600045F RID: 1119 RVA: 0x00015988 File Offset: 0x00013B88
	public void SetInputDevice(int deviceID)
	{
		global::USpeaker.InputDeviceID = deviceID;
	}

	// Token: 0x06000460 RID: 1120 RVA: 0x00015990 File Offset: 0x00013B90
	public static global::USpeaker Get(Object source)
	{
		if (source is GameObject)
		{
			return (source as GameObject).GetComponent<global::USpeaker>();
		}
		if (source is Transform)
		{
			return (source as Transform).GetComponent<global::USpeaker>();
		}
		if (source is Component)
		{
			return (source as Component).GetComponent<global::USpeaker>();
		}
		return null;
	}

	// Token: 0x06000461 RID: 1121 RVA: 0x000159E4 File Offset: 0x00013BE4
	public void GetInputHandler()
	{
		this.talkController = (MoPhoGames.USpeak.Interface.IUSpeakTalkController)this.FindInputHandler();
	}

	// Token: 0x06000462 RID: 1122 RVA: 0x000159F8 File Offset: 0x00013BF8
	public void DrawTalkControllerUI()
	{
		if (this.talkController != null)
		{
			this.talkController.OnInspectorGUI();
		}
		else
		{
			GUILayout.Label("No component available which implements IUSpeakTalkController\nReverting to default behavior - data is always sent", new GUILayoutOption[0]);
		}
	}

	// Token: 0x06000463 RID: 1123 RVA: 0x00015A28 File Offset: 0x00013C28
	public void ReceiveAudio(byte[] data)
	{
		if (this.settings == null)
		{
			Debug.LogWarning("Trying to receive remote audio data without calling InitializeSettings!\nIncoming packet will be ignored");
			return;
		}
		if (global::USpeaker.MuteAll || this.Mute || (this.SpeakerMode == global::SpeakerMode.Local && !this.DebugPlayback))
		{
			return;
		}
		if (this.SpeakerMode == global::SpeakerMode.Remote)
		{
			this.talkTimer = 1f;
		}
		byte[] @byte;
		for (int i = 0; i < data.Length; i += @byte.Length)
		{
			int num = BitConverter.ToInt32(data, i);
			@byte = MoPhoGames.USpeak.Core.Utils.USpeakPoolUtils.GetByte(num + 6);
			Array.Copy(data, i, @byte, 0, @byte.Length);
			MoPhoGames.USpeak.Core.USpeakFrameContainer uspeakFrameContainer = default(MoPhoGames.USpeak.Core.USpeakFrameContainer);
			uspeakFrameContainer.LoadFrom(@byte);
			MoPhoGames.USpeak.Core.Utils.USpeakPoolUtils.Return(@byte);
			float[] array = MoPhoGames.USpeak.Core.USpeakAudioClipCompressor.DecompressAudio(uspeakFrameContainer.encodedData, (int)uspeakFrameContainer.Samples, 1, false, this.settings.bandMode, this.codecMgr.Codecs[this.Codec], global::USpeaker.RemoteGain);
			float num2 = (float)array.Length / (float)this.audioFrequency;
			this.received += (double)num2;
			Array.Copy(array, 0, this.receivedData, this.index, array.Length);
			MoPhoGames.USpeak.Core.Utils.USpeakPoolUtils.Return(array);
			this.index += array.Length;
			if (this.index >= base.audio.clip.samples)
			{
				this.index = 0;
			}
			base.audio.clip.SetData(this.receivedData, 0);
			if (!base.audio.isPlaying)
			{
				this.shouldPlay = true;
				if (this.playDelay <= 0f)
				{
					this.playDelay = num2 * 2f;
				}
			}
		}
	}

	// Token: 0x06000464 RID: 1124 RVA: 0x00015BD0 File Offset: 0x00013DD0
	public void InitializeSettings(int data)
	{
		MonoBehaviour.print("Settings changed");
		this.settings = new MoPhoGames.USpeak.Core.USpeakSettingsData((byte)data);
		this.Codec = this.settings.Codec;
	}

	// Token: 0x06000465 RID: 1125 RVA: 0x00015C08 File Offset: 0x00013E08
	private void Awake()
	{
		global::USpeaker.USpeakerList.Add(this);
		if (base.audio == null)
		{
			base.gameObject.AddComponent<AudioSource>();
		}
		base.audio.clip = AudioClip.Create("vc", this.audioFrequency * 10, 1, this.audioFrequency, this._3DMode == global::ThreeDMode.Full3D, false);
		base.audio.loop = true;
		this.receivedData = new float[this.audioFrequency * 10];
		this.codecMgr = global::USpeakCodecManager.Instance;
		this.lastBandMode = this.BandwidthMode;
		this.lastCodec = this.Codec;
		this.last3DMode = this._3DMode;
	}

	// Token: 0x06000466 RID: 1126 RVA: 0x00015CBC File Offset: 0x00013EBC
	private void OnDestroy()
	{
		global::USpeaker.USpeakerList.Remove(this);
	}

	// Token: 0x06000467 RID: 1127 RVA: 0x00015CCC File Offset: 0x00013ECC
	private IEnumerator Start()
	{
		yield return null;
		this.audioHandler = (MoPhoGames.USpeak.Interface.ISpeechDataHandler)this.FindSpeechHandler();
		this.talkController = (MoPhoGames.USpeak.Interface.IUSpeakTalkController)this.FindInputHandler();
		if (this.audioHandler == null)
		{
			Debug.LogError("USpeaker requires a component which implements the ISpeechDataHandler interface");
			yield break;
		}
		if (this.SpeakerMode == global::SpeakerMode.Remote)
		{
			yield break;
		}
		if (this.AskPermission && !Application.HasUserAuthorization(2))
		{
			yield return Application.RequestUserAuthorization(2);
		}
		if (!Application.HasUserAuthorization(2))
		{
			Debug.LogError("Failed to start recording - user has denied microphone access");
			yield break;
		}
		string[] devices = Microphone.devices;
		if (devices.Length == 0)
		{
			Debug.LogWarning("Failed to find a recording device");
			yield break;
		}
		this.UpdateSettings();
		this.sendt = 1f / this.SendRate;
		this.recording = Microphone.Start(this.currentDeviceName, false, 21, this.audioFrequency);
		MonoBehaviour.print(devices[global::USpeaker.InputDeviceID]);
		this.currentDeviceName = devices[global::USpeaker.InputDeviceID];
		yield break;
	}

	// Token: 0x06000468 RID: 1128 RVA: 0x00015CE8 File Offset: 0x00013EE8
	private void Update()
	{
		this.talkTimer -= Time.deltaTime;
		base.audio.volume = this.SpeakerVolume;
		if (this.last3DMode != this._3DMode)
		{
			this.last3DMode = this._3DMode;
			this.StopPlaying();
			base.audio.clip = AudioClip.Create("vc", this.audioFrequency * 10, 1, this.audioFrequency, this._3DMode == global::ThreeDMode.Full3D, false);
			base.audio.loop = true;
		}
		if (this._3DMode == global::ThreeDMode.SpeakerPan)
		{
			Transform transform = Camera.main.transform;
			Vector3 vector = Vector3.Cross(transform.up, transform.forward);
			vector.Normalize();
			float num = Vector3.Dot(base.transform.position - transform.position, vector);
			float num2 = Vector3.Dot(base.transform.position - transform.position, transform.forward);
			float num3 = Mathf.Atan2(num, num2);
			float pan = Mathf.Sin(num3);
			base.audio.pan = pan;
		}
		if (base.audio.isPlaying)
		{
			if (this.lastTime > base.audio.time)
			{
				this.played += (double)base.audio.clip.length;
			}
			this.lastTime = base.audio.time;
			if (this.played + (double)base.audio.time >= this.received)
			{
				this.StopPlaying();
				this.shouldPlay = false;
			}
		}
		else if (this.shouldPlay)
		{
			this.playDelay -= Time.deltaTime;
			if (this.playDelay <= 0f)
			{
				base.audio.Play();
			}
		}
		if (this.SpeakerMode == global::SpeakerMode.Remote)
		{
			return;
		}
		if (this.audioHandler == null)
		{
			return;
		}
		if (this.devicesCached == null)
		{
			this.devicesCached = Microphone.devices;
			base.InvokeRepeating("RefreshDevices", 4.2f, 4.2f);
		}
		string[] array = this.devicesCached;
		if (array.Length == 0)
		{
			return;
		}
		if (array[Mathf.Min(global::USpeaker.InputDeviceID, array.Length - 1)] != this.currentDeviceName)
		{
			this.currentDeviceName = array[Mathf.Min(global::USpeaker.InputDeviceID, array.Length - 1)];
			MonoBehaviour.print("Using input device: " + this.currentDeviceName);
			this.recording = Microphone.Start(this.currentDeviceName, false, 21, this.audioFrequency);
			this.lastReadPos = 0;
		}
		if (this.lastBandMode != this.BandwidthMode || this.lastCodec != this.Codec)
		{
			this.UpdateSettings();
			this.lastBandMode = this.BandwidthMode;
			this.lastCodec = this.Codec;
		}
		int num4 = Microphone.GetPosition(null);
		if (num4 >= this.audioFrequency * 20)
		{
			num4 = 0;
			this.lastReadPos = 0;
			Object.DestroyImmediate(this.recording);
			Microphone.End(null);
			this.recording = Microphone.Start(this.currentDeviceName, false, 21, this.audioFrequency);
		}
		if (num4 <= this.overlap)
		{
			return;
		}
		bool? flag = null;
		try
		{
			int num5 = num4 - this.lastReadPos;
			int num6 = this.codecMgr.Codecs[this.Codec].GetSampleSize(this.audioFrequency);
			if (num6 == 0)
			{
				num6 = 100;
			}
			if (num6 == 0)
			{
				if (num5 > num6)
				{
					float[] array2 = new float[num5 - 1];
					this.recording.GetData(array2, this.lastReadPos);
					if (this.talkController == null || this.talkController.ShouldSend())
					{
						this.talkTimer = 1f;
						this.OnAudioAvailable(array2);
					}
				}
				this.lastReadPos = num4;
			}
			else
			{
				int num7 = this.lastReadPos;
				int num8 = Mathf.FloorToInt((float)(num5 / num6));
				for (int i = 0; i < num8; i++)
				{
					float[] @float = MoPhoGames.USpeak.Core.Utils.USpeakPoolUtils.GetFloat(num6);
					this.recording.GetData(@float, num7);
					bool value;
					if (flag != null)
					{
						value = flag.Value;
					}
					else
					{
						bool? flag2;
						flag = (flag2 = new bool?(this.talkController != null && this.talkController.ShouldSend()));
						value = flag2.Value;
					}
					if (value)
					{
						this.talkTimer = 1f;
						this.OnAudioAvailable(@float);
					}
					MoPhoGames.USpeak.Core.Utils.USpeakPoolUtils.Return(@float);
					num7 += num6;
				}
				this.lastReadPos = num7;
			}
		}
		catch (Exception)
		{
		}
		this.ProcessPendingEncodeBuffer();
		bool flag3 = true;
		if (this.SendingMode == global::SendBehavior.RecordThenSend && this.talkController != null)
		{
			int value2;
			if (flag != null)
			{
				value2 = (flag.Value ? 1 : 0);
			}
			else
			{
				bool? flag4;
				flag = (flag4 = new bool?(this.talkController.ShouldSend()));
				value2 = (flag4.Value ? 1 : 0);
			}
			flag3 = (value2 == 0);
		}
		this.sendTimer += Time.deltaTime;
		if (this.sendTimer >= this.sendt && flag3)
		{
			this.sendTimer = 0f;
			this.tempSendBytes.Clear();
			foreach (MoPhoGames.USpeak.Core.USpeakFrameContainer uspeakFrameContainer in this.sendBuffer)
			{
				this.tempSendBytes.AddRange(uspeakFrameContainer.ToByteArray());
			}
			this.sendBuffer.Clear();
			if (this.tempSendBytes.Count > 0)
			{
				this.audioHandler.USpeakOnSerializeAudio(this.tempSendBytes.ToArray());
			}
		}
	}

	// Token: 0x06000469 RID: 1129 RVA: 0x000162D8 File Offset: 0x000144D8
	private void RefreshDevices()
	{
		if (this.SpeakerMode != global::SpeakerMode.Local)
		{
			base.CancelInvoke("RefreshDevices");
		}
		else
		{
			this.devicesCached = Microphone.devices;
		}
	}

	// Token: 0x0600046A RID: 1130 RVA: 0x0001630C File Offset: 0x0001450C
	private void StopPlaying()
	{
		base.audio.Stop();
		base.audio.time = 0f;
		this.index = 0;
		this.played = 0.0;
		this.received = 0.0;
		this.lastTime = 0f;
	}

	// Token: 0x0600046B RID: 1131 RVA: 0x00016364 File Offset: 0x00014564
	private void UpdateSettings()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		this.settings = new MoPhoGames.USpeak.Core.USpeakSettingsData();
		this.settings.bandMode = this.BandwidthMode;
		this.settings.Codec = this.Codec;
		this.audioHandler.USpeakInitializeSettings((int)this.settings.ToByte());
	}

	// Token: 0x0600046C RID: 1132 RVA: 0x000163C0 File Offset: 0x000145C0
	private Component FindSpeechHandler()
	{
		Component[] components = base.GetComponents<Component>();
		foreach (Component component in components)
		{
			if (component is MoPhoGames.USpeak.Interface.ISpeechDataHandler)
			{
				return component;
			}
		}
		return null;
	}

	// Token: 0x0600046D RID: 1133 RVA: 0x000163FC File Offset: 0x000145FC
	private Component FindInputHandler()
	{
		Component[] components = base.GetComponents<Component>();
		foreach (Component component in components)
		{
			if (component is MoPhoGames.USpeak.Interface.IUSpeakTalkController)
			{
				return component;
			}
		}
		return null;
	}

	// Token: 0x0600046E RID: 1134 RVA: 0x00016438 File Offset: 0x00014638
	private void OnAudioAvailable(float[] pcmData)
	{
		if (this.UseVAD && !this.CheckVAD(pcmData))
		{
			return;
		}
		global::USpeaker.CurrentVolume = 0f;
		if (pcmData.Length > 0)
		{
			foreach (float num in pcmData)
			{
				global::USpeaker.CurrentVolume += Mathf.Abs(num);
			}
			global::USpeaker.CurrentVolume /= (float)pcmData.Length;
		}
		int size = 1280;
		List<float[]> list = this.SplitArray(pcmData, size);
		foreach (float[] item in list)
		{
			this.pendingEncode.Add(item);
		}
	}

	// Token: 0x0600046F RID: 1135 RVA: 0x0001651C File Offset: 0x0001471C
	private List<float[]> SplitArray(float[] array, int size)
	{
		List<float[]> list = new List<float[]>();
		float[] array2;
		for (int i = 0; i < array.Length; i += array2.Length)
		{
			array2 = array.Skip(i).Take(size).ToArray<float>();
			list.Add(array2);
		}
		return list;
	}

	// Token: 0x06000470 RID: 1136 RVA: 0x00016560 File Offset: 0x00014760
	private void ProcessPendingEncodeBuffer()
	{
		int num = 10;
		float num2 = (float)num / 1000f;
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		while (Time.realtimeSinceStartup <= realtimeSinceStartup + num2 && this.pendingEncode.Count > 0)
		{
			float[] pcm = this.pendingEncode[0];
			this.pendingEncode.RemoveAt(0);
			this.ProcessPendingEncode(pcm);
		}
	}

	// Token: 0x06000471 RID: 1137 RVA: 0x000165C4 File Offset: 0x000147C4
	private void ProcessPendingEncode(float[] pcm)
	{
		int num;
		byte[] encodedData = MoPhoGames.USpeak.Core.USpeakAudioClipCompressor.CompressAudioData(pcm, 1, out num, this.lastBandMode, this.codecMgr.Codecs[this.lastCodec], global::USpeaker.LocalGain);
		MoPhoGames.USpeak.Core.USpeakFrameContainer item = default(MoPhoGames.USpeak.Core.USpeakFrameContainer);
		item.Samples = (ushort)num;
		item.encodedData = encodedData;
		this.sendBuffer.Add(item);
	}

	// Token: 0x06000472 RID: 1138 RVA: 0x00016620 File Offset: 0x00014820
	private int CalculateSamplesRead(int readPos)
	{
		if (readPos >= this.lastReadPos)
		{
			return readPos - this.lastReadPos;
		}
		return this.audioFrequency * 10 - this.lastReadPos + readPos;
	}

	// Token: 0x06000473 RID: 1139 RVA: 0x0001664C File Offset: 0x0001484C
	private float[] normalize(float[] samples, float magnitude)
	{
		float[] array = new float[samples.Length];
		for (int i = 0; i < samples.Length; i++)
		{
			array[i] = samples[i] / magnitude;
		}
		return array;
	}

	// Token: 0x06000474 RID: 1140 RVA: 0x00016680 File Offset: 0x00014880
	private float amplitude(float[] x)
	{
		float num = 0f;
		for (int i = 0; i < x.Length; i++)
		{
			num = Mathf.Max(num, Mathf.Abs(x[i]));
		}
		return num;
	}

	// Token: 0x06000475 RID: 1141 RVA: 0x000166B8 File Offset: 0x000148B8
	private bool CheckVAD(float[] samples)
	{
		if (Time.realtimeSinceStartup < this.lastVTime + this.vadHangover)
		{
			return true;
		}
		float num = 0f;
		foreach (float num2 in samples)
		{
			num = Mathf.Max(num, Mathf.Abs(num2));
		}
		bool flag = num >= this.VolumeThreshold;
		if (flag)
		{
			this.lastVTime = Time.realtimeSinceStartup;
		}
		return flag;
	}

	// Token: 0x040003D2 RID: 978
	public static float CurrentVolume = 0f;

	// Token: 0x040003D3 RID: 979
	public static float RemoteGain = 1f;

	// Token: 0x040003D4 RID: 980
	public static float LocalGain = 1f;

	// Token: 0x040003D5 RID: 981
	public static bool MuteAll = false;

	// Token: 0x040003D6 RID: 982
	public static List<global::USpeaker> USpeakerList = new List<global::USpeaker>();

	// Token: 0x040003D7 RID: 983
	private static int InputDeviceID = 0;

	// Token: 0x040003D8 RID: 984
	public global::SpeakerMode SpeakerMode;

	// Token: 0x040003D9 RID: 985
	public global::BandMode BandwidthMode;

	// Token: 0x040003DA RID: 986
	public float SendRate = 16f;

	// Token: 0x040003DB RID: 987
	public global::SendBehavior SendingMode;

	// Token: 0x040003DC RID: 988
	public bool UseVAD;

	// Token: 0x040003DD RID: 989
	public global::ThreeDMode _3DMode;

	// Token: 0x040003DE RID: 990
	public bool DebugPlayback;

	// Token: 0x040003DF RID: 991
	public bool AskPermission = true;

	// Token: 0x040003E0 RID: 992
	public bool Mute;

	// Token: 0x040003E1 RID: 993
	public float SpeakerVolume = 1f;

	// Token: 0x040003E2 RID: 994
	public float VolumeThreshold = 0.01f;

	// Token: 0x040003E3 RID: 995
	public int Codec;

	// Token: 0x040003E4 RID: 996
	private global::USpeakCodecManager codecMgr;

	// Token: 0x040003E5 RID: 997
	private AudioClip recording;

	// Token: 0x040003E6 RID: 998
	private int recFreq;

	// Token: 0x040003E7 RID: 999
	private int lastReadPos;

	// Token: 0x040003E8 RID: 1000
	private float sendTimer;

	// Token: 0x040003E9 RID: 1001
	private float sendt = 1f;

	// Token: 0x040003EA RID: 1002
	private List<MoPhoGames.USpeak.Core.USpeakFrameContainer> sendBuffer = new List<MoPhoGames.USpeak.Core.USpeakFrameContainer>();

	// Token: 0x040003EB RID: 1003
	private List<byte> tempSendBytes = new List<byte>();

	// Token: 0x040003EC RID: 1004
	private MoPhoGames.USpeak.Interface.ISpeechDataHandler audioHandler;

	// Token: 0x040003ED RID: 1005
	private MoPhoGames.USpeak.Interface.IUSpeakTalkController talkController;

	// Token: 0x040003EE RID: 1006
	private int overlap;

	// Token: 0x040003EF RID: 1007
	private MoPhoGames.USpeak.Core.USpeakSettingsData settings;

	// Token: 0x040003F0 RID: 1008
	private string currentDeviceName = string.Empty;

	// Token: 0x040003F1 RID: 1009
	private float talkTimer;

	// Token: 0x040003F2 RID: 1010
	private float vadHangover = 0.5f;

	// Token: 0x040003F3 RID: 1011
	private float lastVTime;

	// Token: 0x040003F4 RID: 1012
	private List<float[]> pendingEncode = new List<float[]>();

	// Token: 0x040003F5 RID: 1013
	private double played;

	// Token: 0x040003F6 RID: 1014
	private int index;

	// Token: 0x040003F7 RID: 1015
	private double received;

	// Token: 0x040003F8 RID: 1016
	private float[] receivedData;

	// Token: 0x040003F9 RID: 1017
	private float playDelay;

	// Token: 0x040003FA RID: 1018
	private bool shouldPlay;

	// Token: 0x040003FB RID: 1019
	private float lastTime;

	// Token: 0x040003FC RID: 1020
	private global::BandMode lastBandMode;

	// Token: 0x040003FD RID: 1021
	private int lastCodec;

	// Token: 0x040003FE RID: 1022
	private global::ThreeDMode last3DMode;

	// Token: 0x040003FF RID: 1023
	private string[] devicesCached;
}
