using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MoPhoGames.USpeak.Core;
using MoPhoGames.USpeak.Core.Utils;
using MoPhoGames.USpeak.Interface;
using UnityEngine;

// Token: 0x020000BA RID: 186
[AddComponentMenu("USpeak/USpeaker")]
public class USpeaker : MonoBehaviour
{
	// Token: 0x17000092 RID: 146
	// (get) Token: 0x060003E2 RID: 994 RVA: 0x000140D4 File Offset: 0x000122D4
	// (set) Token: 0x060003E3 RID: 995 RVA: 0x000140E0 File Offset: 0x000122E0
	[Obsolete("Use USpeaker._3DMode instead")]
	public bool Is3D
	{
		get
		{
			return this._3DMode == ThreeDMode.SpeakerPan;
		}
		set
		{
			if (value)
			{
				this._3DMode = ThreeDMode.SpeakerPan;
			}
			else
			{
				this._3DMode = ThreeDMode.None;
			}
		}
	}

	// Token: 0x17000093 RID: 147
	// (get) Token: 0x060003E4 RID: 996 RVA: 0x000140FC File Offset: 0x000122FC
	public bool IsTalking
	{
		get
		{
			return this.talkTimer > 0f;
		}
	}

	// Token: 0x060003E5 RID: 997 RVA: 0x0001410C File Offset: 0x0001230C
	public bool HasSettings()
	{
		return this.settings != null;
	}

	// Token: 0x17000094 RID: 148
	// (get) Token: 0x060003E6 RID: 998 RVA: 0x0001411C File Offset: 0x0001231C
	private int audioFrequency
	{
		get
		{
			if (this.recFreq == 0)
			{
				switch (this.BandwidthMode)
				{
				case BandMode.Narrow:
					this.recFreq = 8000;
					break;
				case BandMode.Wide:
					this.recFreq = 16000;
					break;
				case BandMode.UltraWide:
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

	// Token: 0x060003E7 RID: 999 RVA: 0x00014198 File Offset: 0x00012398
	public void SetInputDevice(int deviceID)
	{
		USpeaker.InputDeviceID = deviceID;
	}

	// Token: 0x060003E8 RID: 1000 RVA: 0x000141A0 File Offset: 0x000123A0
	public static USpeaker Get(Object source)
	{
		if (source is GameObject)
		{
			return (source as GameObject).GetComponent<USpeaker>();
		}
		if (source is Transform)
		{
			return (source as Transform).GetComponent<USpeaker>();
		}
		if (source is Component)
		{
			return (source as Component).GetComponent<USpeaker>();
		}
		return null;
	}

	// Token: 0x060003E9 RID: 1001 RVA: 0x000141F4 File Offset: 0x000123F4
	public void GetInputHandler()
	{
		this.talkController = (IUSpeakTalkController)this.FindInputHandler();
	}

	// Token: 0x060003EA RID: 1002 RVA: 0x00014208 File Offset: 0x00012408
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

	// Token: 0x060003EB RID: 1003 RVA: 0x00014238 File Offset: 0x00012438
	public void ReceiveAudio(byte[] data)
	{
		if (this.settings == null)
		{
			Debug.LogWarning("Trying to receive remote audio data without calling InitializeSettings!\nIncoming packet will be ignored");
			return;
		}
		if (USpeaker.MuteAll || this.Mute || (this.SpeakerMode == SpeakerMode.Local && !this.DebugPlayback))
		{
			return;
		}
		if (this.SpeakerMode == SpeakerMode.Remote)
		{
			this.talkTimer = 1f;
		}
		byte[] @byte;
		for (int i = 0; i < data.Length; i += @byte.Length)
		{
			int num = BitConverter.ToInt32(data, i);
			@byte = USpeakPoolUtils.GetByte(num + 6);
			Array.Copy(data, i, @byte, 0, @byte.Length);
			USpeakFrameContainer uspeakFrameContainer = default(USpeakFrameContainer);
			uspeakFrameContainer.LoadFrom(@byte);
			USpeakPoolUtils.Return(@byte);
			float[] array = USpeakAudioClipCompressor.DecompressAudio(uspeakFrameContainer.encodedData, (int)uspeakFrameContainer.Samples, 1, false, this.settings.bandMode, this.codecMgr.Codecs[this.Codec], USpeaker.RemoteGain);
			float num2 = (float)array.Length / (float)this.audioFrequency;
			this.received += (double)num2;
			Array.Copy(array, 0, this.receivedData, this.index, array.Length);
			USpeakPoolUtils.Return(array);
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

	// Token: 0x060003EC RID: 1004 RVA: 0x000143E0 File Offset: 0x000125E0
	public void InitializeSettings(int data)
	{
		MonoBehaviour.print("Settings changed");
		this.settings = new USpeakSettingsData((byte)data);
		this.Codec = this.settings.Codec;
	}

	// Token: 0x060003ED RID: 1005 RVA: 0x00014418 File Offset: 0x00012618
	private void Awake()
	{
		USpeaker.USpeakerList.Add(this);
		if (base.audio == null)
		{
			base.gameObject.AddComponent<AudioSource>();
		}
		base.audio.clip = AudioClip.Create("vc", this.audioFrequency * 10, 1, this.audioFrequency, this._3DMode == ThreeDMode.Full3D, false);
		base.audio.loop = true;
		this.receivedData = new float[this.audioFrequency * 10];
		this.codecMgr = USpeakCodecManager.Instance;
		this.lastBandMode = this.BandwidthMode;
		this.lastCodec = this.Codec;
		this.last3DMode = this._3DMode;
	}

	// Token: 0x060003EE RID: 1006 RVA: 0x000144CC File Offset: 0x000126CC
	private void OnDestroy()
	{
		USpeaker.USpeakerList.Remove(this);
	}

	// Token: 0x060003EF RID: 1007 RVA: 0x000144DC File Offset: 0x000126DC
	private IEnumerator Start()
	{
		yield return null;
		this.audioHandler = (ISpeechDataHandler)this.FindSpeechHandler();
		this.talkController = (IUSpeakTalkController)this.FindInputHandler();
		if (this.audioHandler == null)
		{
			Debug.LogError("USpeaker requires a component which implements the ISpeechDataHandler interface");
			yield break;
		}
		if (this.SpeakerMode == SpeakerMode.Remote)
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
		MonoBehaviour.print(devices[USpeaker.InputDeviceID]);
		this.currentDeviceName = devices[USpeaker.InputDeviceID];
		yield break;
	}

	// Token: 0x060003F0 RID: 1008 RVA: 0x000144F8 File Offset: 0x000126F8
	private void Update()
	{
		this.talkTimer -= Time.deltaTime;
		base.audio.volume = this.SpeakerVolume;
		if (this.last3DMode != this._3DMode)
		{
			this.last3DMode = this._3DMode;
			this.StopPlaying();
			base.audio.clip = AudioClip.Create("vc", this.audioFrequency * 10, 1, this.audioFrequency, this._3DMode == ThreeDMode.Full3D, false);
			base.audio.loop = true;
		}
		if (this._3DMode == ThreeDMode.SpeakerPan)
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
		if (this.SpeakerMode == SpeakerMode.Remote)
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
		if (array[Mathf.Min(USpeaker.InputDeviceID, array.Length - 1)] != this.currentDeviceName)
		{
			this.currentDeviceName = array[Mathf.Min(USpeaker.InputDeviceID, array.Length - 1)];
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
					float[] @float = USpeakPoolUtils.GetFloat(num6);
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
					USpeakPoolUtils.Return(@float);
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
		if (this.SendingMode == SendBehavior.RecordThenSend && this.talkController != null)
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
			foreach (USpeakFrameContainer uspeakFrameContainer in this.sendBuffer)
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

	// Token: 0x060003F1 RID: 1009 RVA: 0x00014AE8 File Offset: 0x00012CE8
	private void RefreshDevices()
	{
		if (this.SpeakerMode != SpeakerMode.Local)
		{
			base.CancelInvoke("RefreshDevices");
		}
		else
		{
			this.devicesCached = Microphone.devices;
		}
	}

	// Token: 0x060003F2 RID: 1010 RVA: 0x00014B1C File Offset: 0x00012D1C
	private void StopPlaying()
	{
		base.audio.Stop();
		base.audio.time = 0f;
		this.index = 0;
		this.played = 0.0;
		this.received = 0.0;
		this.lastTime = 0f;
	}

	// Token: 0x060003F3 RID: 1011 RVA: 0x00014B74 File Offset: 0x00012D74
	private void UpdateSettings()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		this.settings = new USpeakSettingsData();
		this.settings.bandMode = this.BandwidthMode;
		this.settings.Codec = this.Codec;
		this.audioHandler.USpeakInitializeSettings((int)this.settings.ToByte());
	}

	// Token: 0x060003F4 RID: 1012 RVA: 0x00014BD0 File Offset: 0x00012DD0
	private Component FindSpeechHandler()
	{
		Component[] components = base.GetComponents<Component>();
		foreach (Component component in components)
		{
			if (component is ISpeechDataHandler)
			{
				return component;
			}
		}
		return null;
	}

	// Token: 0x060003F5 RID: 1013 RVA: 0x00014C0C File Offset: 0x00012E0C
	private Component FindInputHandler()
	{
		Component[] components = base.GetComponents<Component>();
		foreach (Component component in components)
		{
			if (component is IUSpeakTalkController)
			{
				return component;
			}
		}
		return null;
	}

	// Token: 0x060003F6 RID: 1014 RVA: 0x00014C48 File Offset: 0x00012E48
	private void OnAudioAvailable(float[] pcmData)
	{
		if (this.UseVAD && !this.CheckVAD(pcmData))
		{
			return;
		}
		USpeaker.CurrentVolume = 0f;
		if (pcmData.Length > 0)
		{
			foreach (float num in pcmData)
			{
				USpeaker.CurrentVolume += Mathf.Abs(num);
			}
			USpeaker.CurrentVolume /= (float)pcmData.Length;
		}
		int size = 1280;
		List<float[]> list = this.SplitArray(pcmData, size);
		foreach (float[] item in list)
		{
			this.pendingEncode.Add(item);
		}
	}

	// Token: 0x060003F7 RID: 1015 RVA: 0x00014D2C File Offset: 0x00012F2C
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

	// Token: 0x060003F8 RID: 1016 RVA: 0x00014D70 File Offset: 0x00012F70
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

	// Token: 0x060003F9 RID: 1017 RVA: 0x00014DD4 File Offset: 0x00012FD4
	private void ProcessPendingEncode(float[] pcm)
	{
		int num;
		byte[] encodedData = USpeakAudioClipCompressor.CompressAudioData(pcm, 1, out num, this.lastBandMode, this.codecMgr.Codecs[this.lastCodec], USpeaker.LocalGain);
		USpeakFrameContainer item = default(USpeakFrameContainer);
		item.Samples = (ushort)num;
		item.encodedData = encodedData;
		this.sendBuffer.Add(item);
	}

	// Token: 0x060003FA RID: 1018 RVA: 0x00014E30 File Offset: 0x00013030
	private int CalculateSamplesRead(int readPos)
	{
		if (readPos >= this.lastReadPos)
		{
			return readPos - this.lastReadPos;
		}
		return this.audioFrequency * 10 - this.lastReadPos + readPos;
	}

	// Token: 0x060003FB RID: 1019 RVA: 0x00014E5C File Offset: 0x0001305C
	private float[] normalize(float[] samples, float magnitude)
	{
		float[] array = new float[samples.Length];
		for (int i = 0; i < samples.Length; i++)
		{
			array[i] = samples[i] / magnitude;
		}
		return array;
	}

	// Token: 0x060003FC RID: 1020 RVA: 0x00014E90 File Offset: 0x00013090
	private float amplitude(float[] x)
	{
		float num = 0f;
		for (int i = 0; i < x.Length; i++)
		{
			num = Mathf.Max(num, Mathf.Abs(x[i]));
		}
		return num;
	}

	// Token: 0x060003FD RID: 1021 RVA: 0x00014EC8 File Offset: 0x000130C8
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

	// Token: 0x04000367 RID: 871
	public static float CurrentVolume = 0f;

	// Token: 0x04000368 RID: 872
	public static float RemoteGain = 1f;

	// Token: 0x04000369 RID: 873
	public static float LocalGain = 1f;

	// Token: 0x0400036A RID: 874
	public static bool MuteAll = false;

	// Token: 0x0400036B RID: 875
	public static List<USpeaker> USpeakerList = new List<USpeaker>();

	// Token: 0x0400036C RID: 876
	private static int InputDeviceID = 0;

	// Token: 0x0400036D RID: 877
	public SpeakerMode SpeakerMode;

	// Token: 0x0400036E RID: 878
	public BandMode BandwidthMode;

	// Token: 0x0400036F RID: 879
	public float SendRate = 16f;

	// Token: 0x04000370 RID: 880
	public SendBehavior SendingMode;

	// Token: 0x04000371 RID: 881
	public bool UseVAD;

	// Token: 0x04000372 RID: 882
	public ThreeDMode _3DMode;

	// Token: 0x04000373 RID: 883
	public bool DebugPlayback;

	// Token: 0x04000374 RID: 884
	public bool AskPermission = true;

	// Token: 0x04000375 RID: 885
	public bool Mute;

	// Token: 0x04000376 RID: 886
	public float SpeakerVolume = 1f;

	// Token: 0x04000377 RID: 887
	public float VolumeThreshold = 0.01f;

	// Token: 0x04000378 RID: 888
	public int Codec;

	// Token: 0x04000379 RID: 889
	private USpeakCodecManager codecMgr;

	// Token: 0x0400037A RID: 890
	private AudioClip recording;

	// Token: 0x0400037B RID: 891
	private int recFreq;

	// Token: 0x0400037C RID: 892
	private int lastReadPos;

	// Token: 0x0400037D RID: 893
	private float sendTimer;

	// Token: 0x0400037E RID: 894
	private float sendt = 1f;

	// Token: 0x0400037F RID: 895
	private List<USpeakFrameContainer> sendBuffer = new List<USpeakFrameContainer>();

	// Token: 0x04000380 RID: 896
	private List<byte> tempSendBytes = new List<byte>();

	// Token: 0x04000381 RID: 897
	private ISpeechDataHandler audioHandler;

	// Token: 0x04000382 RID: 898
	private IUSpeakTalkController talkController;

	// Token: 0x04000383 RID: 899
	private int overlap;

	// Token: 0x04000384 RID: 900
	private USpeakSettingsData settings;

	// Token: 0x04000385 RID: 901
	private string currentDeviceName = string.Empty;

	// Token: 0x04000386 RID: 902
	private float talkTimer;

	// Token: 0x04000387 RID: 903
	private float vadHangover = 0.5f;

	// Token: 0x04000388 RID: 904
	private float lastVTime;

	// Token: 0x04000389 RID: 905
	private List<float[]> pendingEncode = new List<float[]>();

	// Token: 0x0400038A RID: 906
	private double played;

	// Token: 0x0400038B RID: 907
	private int index;

	// Token: 0x0400038C RID: 908
	private double received;

	// Token: 0x0400038D RID: 909
	private float[] receivedData;

	// Token: 0x0400038E RID: 910
	private float playDelay;

	// Token: 0x0400038F RID: 911
	private bool shouldPlay;

	// Token: 0x04000390 RID: 912
	private float lastTime;

	// Token: 0x04000391 RID: 913
	private BandMode lastBandMode;

	// Token: 0x04000392 RID: 914
	private int lastCodec;

	// Token: 0x04000393 RID: 915
	private ThreeDMode last3DMode;

	// Token: 0x04000394 RID: 916
	private string[] devicesCached;
}
