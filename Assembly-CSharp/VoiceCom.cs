using System;
using MoPhoGames.USpeak.Interface;
using uLink;
using UnityEngine;

// Token: 0x020000B0 RID: 176
public sealed class VoiceCom : IDLocalCharacter, IVoiceCom, ISpeechDataHandler, IUSpeakTalkController
{
	// Token: 0x060003BA RID: 954 RVA: 0x0001378C File Offset: 0x0001198C
	void IUSpeakTalkController.OnInspectorGUI()
	{
	}

	// Token: 0x17000090 RID: 144
	// (get) Token: 0x060003BB RID: 955 RVA: 0x00013790 File Offset: 0x00011990
	public USpeaker uspeaker
	{
		get
		{
			if (!this._uspeakerChecked)
			{
				this._uspeaker = USpeaker.Get(this);
				this._uspeakerChecked = true;
			}
			return this._uspeaker;
		}
	}

	// Token: 0x060003BC RID: 956 RVA: 0x000137C4 File Offset: 0x000119C4
	private void uLink_OnNetworkInstantiate(NetworkMessageInfo info)
	{
		if (!base.networkView.isMine)
		{
			this.uspeaker.SpeakerMode = SpeakerMode.Remote;
		}
		else
		{
			this.uspeaker.SpeakerMode = SpeakerMode.Local;
		}
	}

	// Token: 0x060003BD RID: 957 RVA: 0x00013800 File Offset: 0x00011A00
	public void USpeakOnSerializeAudio(byte[] data)
	{
		base.networkView.RPC("clientspeak", 0, new object[]
		{
			this.setupData,
			data
		});
	}

	// Token: 0x060003BE RID: 958 RVA: 0x00013838 File Offset: 0x00011A38
	public void USpeakInitializeSettings(int data)
	{
		this.setupData = data;
	}

	// Token: 0x060003BF RID: 959 RVA: 0x00013844 File Offset: 0x00011A44
	[RPC]
	private void clientspeak(int setupData, byte[] data)
	{
	}

	// Token: 0x060003C0 RID: 960 RVA: 0x00013848 File Offset: 0x00011A48
	[RPC]
	private void voiceplay(float hearDistance, int setupData, byte[] data)
	{
		Camera main = Camera.main;
		if (!main)
		{
			return;
		}
		if (hearDistance <= 0f)
		{
			return;
		}
		USpeaker uspeaker = this.uspeaker;
		if (!uspeaker)
		{
			Debug.LogWarning("voiceplayback:" + base.gameObject + " didn't have a USpeaker!?");
		}
		if (!uspeaker.HasSettings())
		{
			uspeaker.InitializeSettings(setupData);
		}
		if (data == null)
		{
			Debug.LogWarning("voiceplayback: data was null!");
		}
		Vector3 vector = main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0001f));
		float num = Vector3.Distance(vector, base.eyesOrigin);
		float speakerVolume = Mathf.Clamp01(1f - num / hearDistance);
		uspeaker.SpeakerVolume = speakerVolume;
		uspeaker.ReceiveAudio(data);
		AudioSource audio = uspeaker.audio;
		if (audio)
		{
			audio.rolloffMode = 1;
			audio.maxDistance = hearDistance;
			audio.minDistance = 1f;
		}
	}

	// Token: 0x060003C1 RID: 961 RVA: 0x00013938 File Offset: 0x00011B38
	[RPC]
	private void init(int data)
	{
		this.uspeaker.InitializeSettings(data);
	}

	// Token: 0x060003C2 RID: 962 RVA: 0x00013948 File Offset: 0x00011B48
	public bool ShouldSend()
	{
		Character idMain = base.idMain;
		return idMain && idMain.alive && GameInput.GetButton("Voice").IsDown();
	}

	// Token: 0x060003C3 RID: 963 RVA: 0x00013984 File Offset: 0x00011B84
	public static float GetVolume()
	{
		return 0f;
	}

	// Token: 0x0400034C RID: 844
	private int setupData;

	// Token: 0x0400034D RID: 845
	[NonSerialized]
	private USpeaker _uspeaker;

	// Token: 0x0400034E RID: 846
	[NonSerialized]
	private bool _uspeakerChecked;
}
