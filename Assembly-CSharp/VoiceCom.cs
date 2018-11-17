using System;
using MoPhoGames.USpeak.Interface;
using uLink;
using UnityEngine;

// Token: 0x020000C3 RID: 195
public sealed class VoiceCom : global::IDLocalCharacter, IVoiceCom, MoPhoGames.USpeak.Interface.ISpeechDataHandler, MoPhoGames.USpeak.Interface.IUSpeakTalkController
{
	// Token: 0x06000432 RID: 1074 RVA: 0x00014F7C File Offset: 0x0001317C
	void MoPhoGames.USpeak.Interface.IUSpeakTalkController.OnInspectorGUI()
	{
	}

	// Token: 0x170000A8 RID: 168
	// (get) Token: 0x06000433 RID: 1075 RVA: 0x00014F80 File Offset: 0x00013180
	public global::USpeaker uspeaker
	{
		get
		{
			if (!this._uspeakerChecked)
			{
				this._uspeaker = global::USpeaker.Get(this);
				this._uspeakerChecked = true;
			}
			return this._uspeaker;
		}
	}

	// Token: 0x06000434 RID: 1076 RVA: 0x00014FB4 File Offset: 0x000131B4
	private void uLink_OnNetworkInstantiate(uLink.NetworkMessageInfo info)
	{
		if (!base.networkView.isMine)
		{
			this.uspeaker.SpeakerMode = global::SpeakerMode.Remote;
		}
		else
		{
			this.uspeaker.SpeakerMode = global::SpeakerMode.Local;
		}
	}

	// Token: 0x06000435 RID: 1077 RVA: 0x00014FF0 File Offset: 0x000131F0
	public void USpeakOnSerializeAudio(byte[] data)
	{
		base.networkView.RPC("clientspeak", 0, new object[]
		{
			this.setupData,
			data
		});
	}

	// Token: 0x06000436 RID: 1078 RVA: 0x00015028 File Offset: 0x00013228
	public void USpeakInitializeSettings(int data)
	{
		this.setupData = data;
	}

	// Token: 0x06000437 RID: 1079 RVA: 0x00015034 File Offset: 0x00013234
	[RPC]
	private void clientspeak(int setupData, byte[] data)
	{
	}

	// Token: 0x06000438 RID: 1080 RVA: 0x00015038 File Offset: 0x00013238
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
		global::USpeaker uspeaker = this.uspeaker;
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

	// Token: 0x06000439 RID: 1081 RVA: 0x00015128 File Offset: 0x00013328
	[RPC]
	private void init(int data)
	{
		this.uspeaker.InitializeSettings(data);
	}

	// Token: 0x0600043A RID: 1082 RVA: 0x00015138 File Offset: 0x00013338
	public bool ShouldSend()
	{
		global::Character idMain = base.idMain;
		return idMain && idMain.alive && global::GameInput.GetButton("Voice").IsDown();
	}

	// Token: 0x0600043B RID: 1083 RVA: 0x00015174 File Offset: 0x00013374
	public static float GetVolume()
	{
		return 0f;
	}

	// Token: 0x040003B7 RID: 951
	private int setupData;

	// Token: 0x040003B8 RID: 952
	[NonSerialized]
	private global::USpeaker _uspeaker;

	// Token: 0x040003B9 RID: 953
	[NonSerialized]
	private bool _uspeakerChecked;
}
