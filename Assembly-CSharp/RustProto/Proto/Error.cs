using System;
using System.Diagnostics;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;

namespace RustProto.Proto
{
	// Token: 0x0200022C RID: 556
	[DebuggerNonUserCode]
	public static class Error
	{
		// Token: 0x06001415 RID: 5141 RVA: 0x00044278 File Offset: 0x00042478
		static Error()
		{
			byte[] array = Convert.FromBase64String("ChBydXN0L2Vycm9yLnByb3RvEglSdXN0UHJvdG8iKAoFRXJyb3ISDgoGc3RhdHVzGAEgAigJEg8KB21lc3NhZ2UYAiACKAkiKQoJR2FtZUVycm9yEg0KBWVycm9yGAEgAigJEg0KBXRyYWNlGAIgAigJQgJIAQ==");
			FileDescriptor.InternalDescriptorAssigner internalDescriptorAssigner = delegate(FileDescriptor root)
			{
				Error.descriptor = root;
				Error.internal__static_RustProto_Error__Descriptor = Error.Descriptor.MessageTypes[0];
				Error.internal__static_RustProto_Error__FieldAccessorTable = new FieldAccessorTable<Error, Error.Builder>(Error.internal__static_RustProto_Error__Descriptor, new string[]
				{
					"Status",
					"Message"
				});
				Error.internal__static_RustProto_GameError__Descriptor = Error.Descriptor.MessageTypes[1];
				Error.internal__static_RustProto_GameError__FieldAccessorTable = new FieldAccessorTable<GameError, GameError.Builder>(Error.internal__static_RustProto_GameError__Descriptor, new string[]
				{
					"Error",
					"Trace"
				});
				return null;
			};
			FileDescriptor.InternalBuildGeneratedFileFrom(array, new FileDescriptor[0], internalDescriptorAssigner);
		}

		// Token: 0x06001416 RID: 5142 RVA: 0x000442BC File Offset: 0x000424BC
		public static void RegisterAllExtensions(ExtensionRegistry registry)
		{
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06001417 RID: 5143 RVA: 0x000442C0 File Offset: 0x000424C0
		public static FileDescriptor Descriptor
		{
			get
			{
				return Error.descriptor;
			}
		}

		// Token: 0x04000A1B RID: 2587
		internal static MessageDescriptor internal__static_RustProto_Error__Descriptor;

		// Token: 0x04000A1C RID: 2588
		internal static FieldAccessorTable<Error, Error.Builder> internal__static_RustProto_Error__FieldAccessorTable;

		// Token: 0x04000A1D RID: 2589
		internal static MessageDescriptor internal__static_RustProto_GameError__Descriptor;

		// Token: 0x04000A1E RID: 2590
		internal static FieldAccessorTable<GameError, GameError.Builder> internal__static_RustProto_GameError__FieldAccessorTable;

		// Token: 0x04000A1F RID: 2591
		private static FileDescriptor descriptor;
	}
}
