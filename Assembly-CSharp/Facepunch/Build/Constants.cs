using System;

namespace Facepunch.Build
{
	// Token: 0x02000100 RID: 256
	public static class Constants
	{
		// Token: 0x040004FE RID: 1278
		public const string EditorBundleFileName = "editorbundle.txt";

		// Token: 0x040004FF RID: 1279
		public const string ManifestFileName = "manifest.txt";

		// Token: 0x04000500 RID: 1280
		public const string AssetBundleExtension = ".unity3d";

		// Token: 0x04000501 RID: 1281
		public const string SharedSceneBundleFileName = "scene.shared.unity3d";

		// Token: 0x04000502 RID: 1282
		public const string UniqueSceneBundleFileName = "scene.specific.unity3d";

		// Token: 0x04000503 RID: 1283
		public const string BunchedSceneBundleFileName = "scenes.unity3d";

		// Token: 0x04000504 RID: 1284
		public const int ExitCode_NoError = 0;

		// Token: 0x04000505 RID: 1285
		public const int ExitCode_MissingArguments = 300;

		// Token: 0x04000506 RID: 1286
		public const int ExitCode_BuildFailureException = 500;

		// Token: 0x04000507 RID: 1287
		public const int ExitCode_BuildProjectFormattingException = 502;

		// Token: 0x04000508 RID: 1288
		public const int ExitCode_OtherException = 503;

		// Token: 0x04000509 RID: 1289
		public const int ExitCode_FileNotFound = 404;

		// Token: 0x0400050A RID: 1290
		public const string Key_PathToBuiltServer = "FACEPUNCH_BUILD_PATH_TO_BUILT_SERVER";

		// Token: 0x0400050B RID: 1291
		public const string Key_PathToBuiltWebplayer = "FACEPUNCH_BUILD_PATH_TO_BUILT_WEBPLAYER";

		// Token: 0x0400050C RID: 1292
		public const string Key_ConnectCommand = "FACEPUNCH_BUILD_CONNECT_COMMAND";
	}
}
