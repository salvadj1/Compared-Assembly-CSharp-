using System;

namespace MoPhoGames.USpeak.Interface
{
	// Token: 0x020000C2 RID: 194
	public interface ISpeechDataHandler
	{
		// Token: 0x06000418 RID: 1048
		void USpeakOnSerializeAudio(byte[] data);

		// Token: 0x06000419 RID: 1049
		void USpeakInitializeSettings(int data);
	}
}
