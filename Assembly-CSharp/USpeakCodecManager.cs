using System;
using MoPhoGames.USpeak.Codec;
using UnityEngine;

// Token: 0x020000D1 RID: 209
public class USpeakCodecManager : ScriptableObject
{
	// Token: 0x170000AF RID: 175
	// (get) Token: 0x06000485 RID: 1157 RVA: 0x00016B00 File Offset: 0x00014D00
	public static global::USpeakCodecManager Instance
	{
		get
		{
			if (global::USpeakCodecManager.instance == null)
			{
				global::USpeakCodecManager.instance = (global::USpeakCodecManager)global::Resources.Load("CodecManager");
				if (global::USpeakCodecManager.instance == null)
				{
					Debug.LogError("Couldn't load Resources/CodecManager!");
				}
				if (Application.isPlaying)
				{
					global::USpeakCodecManager.instance.Codecs = new MoPhoGames.USpeak.Codec.ICodec[global::USpeakCodecManager.instance.CodecNames.Length];
					for (int i = 0; i < global::USpeakCodecManager.instance.Codecs.Length; i++)
					{
						global::USpeakCodecManager.instance.Codecs[i] = (MoPhoGames.USpeak.Codec.ICodec)Activator.CreateInstance(Type.GetType(global::USpeakCodecManager.instance.CodecNames[i]));
					}
				}
			}
			return global::USpeakCodecManager.instance;
		}
	}

	// Token: 0x04000406 RID: 1030
	private static global::USpeakCodecManager instance;

	// Token: 0x04000407 RID: 1031
	public MoPhoGames.USpeak.Codec.ICodec[] Codecs;

	// Token: 0x04000408 RID: 1032
	public string[] CodecNames = new string[0];

	// Token: 0x04000409 RID: 1033
	public string[] FriendlyNames = new string[0];
}
