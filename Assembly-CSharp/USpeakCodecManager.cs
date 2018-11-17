using System;
using MoPhoGames.USpeak.Codec;
using UnityEngine;

// Token: 0x020000BD RID: 189
public class USpeakCodecManager : ScriptableObject
{
	// Token: 0x17000095 RID: 149
	// (get) Token: 0x06000407 RID: 1031 RVA: 0x00015138 File Offset: 0x00013338
	public static USpeakCodecManager Instance
	{
		get
		{
			if (USpeakCodecManager.instance == null)
			{
				USpeakCodecManager.instance = (USpeakCodecManager)Resources.Load("CodecManager");
				if (USpeakCodecManager.instance == null)
				{
					Debug.LogError("Couldn't load Resources/CodecManager!");
				}
				if (Application.isPlaying)
				{
					USpeakCodecManager.instance.Codecs = new ICodec[USpeakCodecManager.instance.CodecNames.Length];
					for (int i = 0; i < USpeakCodecManager.instance.Codecs.Length; i++)
					{
						USpeakCodecManager.instance.Codecs[i] = (ICodec)Activator.CreateInstance(Type.GetType(USpeakCodecManager.instance.CodecNames[i]));
					}
				}
			}
			return USpeakCodecManager.instance;
		}
	}

	// Token: 0x04000397 RID: 919
	private static USpeakCodecManager instance;

	// Token: 0x04000398 RID: 920
	public ICodec[] Codecs;

	// Token: 0x04000399 RID: 921
	public string[] CodecNames = new string[0];

	// Token: 0x0400039A RID: 922
	public string[] FriendlyNames = new string[0];
}
