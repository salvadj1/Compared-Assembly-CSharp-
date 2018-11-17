using System;

namespace Facepunch.Build
{
	// Token: 0x020000EC RID: 236
	public static class Constants
	{
		// Token: 0x0400048F RID: 1167
		public const string EditorBundleFileName = "editorbundle.txt";

		// Token: 0x04000490 RID: 1168
		public const string ManifestFileName = "manifest.txt";

		// Token: 0x04000491 RID: 1169
		public const string AssetBundleExtension = ".unity3d";

		// Token: 0x04000492 RID: 1170
		public const string SharedSceneBundleFileName = "scene.shared.unity3d";

		// Token: 0x04000493 RID: 1171
		public const string UniqueSceneBundleFileName = "scene.specific.unity3d";

		// Token: 0x04000494 RID: 1172
		public const string BunchedSceneBundleFileName = "scenes.unity3d";

		// Token: 0x04000495 RID: 1173
		public const int ExitCode_NoError = 0;

		// Token: 0x04000496 RID: 1174
		public const int ExitCode_MissingArguments = 300;

		// Token: 0x04000497 RID: 1175
		public const int ExitCode_BuildFailureException = 500;

		// Token: 0x04000498 RID: 1176
		public const int ExitCode_BuildProjectFormattingException = 502;

		// Token: 0x04000499 RID: 1177
		public const int ExitCode_OtherException = 503;

		// Token: 0x0400049A RID: 1178
		public const int ExitCode_FileNotFound = 404;

		// Token: 0x0400049B RID: 1179
		public const string Key_PathToBuiltServer = "FACEPUNCH_BUILD_PATH_TO_BUILT_SERVER";

		// Token: 0x0400049C RID: 1180
		public const string Key_PathToBuiltWebplayer = "FACEPUNCH_BUILD_PATH_TO_BUILT_WEBPLAYER";

		// Token: 0x0400049D RID: 1181
		public const string Key_ConnectCommand = "FACEPUNCH_BUILD_CONNECT_COMMAND";
	}
}
