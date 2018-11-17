using System;

namespace MoPhoGames.USpeak.Interface
{
	// Token: 0x020000D6 RID: 214
	public interface ISpeechDataHandler
	{
		// Token: 0x06000496 RID: 1174
		void USpeakOnSerializeAudio(byte[] data);

		// Token: 0x06000497 RID: 1175
		void USpeakInitializeSettings(int data);
	}
}
