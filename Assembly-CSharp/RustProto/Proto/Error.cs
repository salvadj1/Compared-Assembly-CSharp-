using System;
using System.Diagnostics;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;

namespace RustProto.Proto
{
	// Token: 0x0200025F RID: 607
	[DebuggerNonUserCode]
	public static class Error
	{
		// Token: 0x06001569 RID: 5481 RVA: 0x00048620 File Offset: 0x00046820
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

		// Token: 0x0600156A RID: 5482 RVA: 0x00048664 File Offset: 0x00046864
		public static void RegisterAllExtensions(ExtensionRegistry registry)
		{
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x0600156B RID: 5483 RVA: 0x00048668 File Offset: 0x00046868
		public static FileDescriptor Descriptor
		{
			get
			{
				return Error.descriptor;
			}
		}

		// Token: 0x04000B3E RID: 2878
		internal static MessageDescriptor internal__static_RustProto_Error__Descriptor;

		// Token: 0x04000B3F RID: 2879
		internal static FieldAccessorTable<Error, Error.Builder> internal__static_RustProto_Error__FieldAccessorTable;

		// Token: 0x04000B40 RID: 2880
		internal static MessageDescriptor internal__static_RustProto_GameError__Descriptor;

		// Token: 0x04000B41 RID: 2881
		internal static FieldAccessorTable<GameError, GameError.Builder> internal__static_RustProto_GameError__FieldAccessorTable;

		// Token: 0x04000B42 RID: 2882
		private static FileDescriptor descriptor;
	}
}
