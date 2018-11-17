using System;
using System.Diagnostics;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;

namespace RustProto.Proto
{
	// Token: 0x0200022A RID: 554
	[DebuggerNonUserCode]
	public static class Blueprint
	{
		// Token: 0x0600140D RID: 5133 RVA: 0x000440F0 File Offset: 0x000422F0
		static Blueprint()
		{
			byte[] array = Convert.FromBase64String("ChRydXN0L2JsdWVwcmludC5wcm90bxIJUnVzdFByb3RvIhcKCUJsdWVwcmludBIKCgJpZBgBIAIoBUICSAE=");
			FileDescriptor.InternalDescriptorAssigner internalDescriptorAssigner = delegate(FileDescriptor root)
			{
				Blueprint.descriptor = root;
				Blueprint.internal__static_RustProto_Blueprint__Descriptor = Blueprint.Descriptor.MessageTypes[0];
				Blueprint.internal__static_RustProto_Blueprint__FieldAccessorTable = new FieldAccessorTable<Blueprint, Blueprint.Builder>(Blueprint.internal__static_RustProto_Blueprint__Descriptor, new string[]
				{
					"Id"
				});
				return null;
			};
			FileDescriptor.InternalBuildGeneratedFileFrom(array, new FileDescriptor[0], internalDescriptorAssigner);
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x00044134 File Offset: 0x00042334
		public static void RegisterAllExtensions(ExtensionRegistry registry)
		{
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x0600140F RID: 5135 RVA: 0x00044138 File Offset: 0x00042338
		public static FileDescriptor Descriptor
		{
			get
			{
				return Blueprint.descriptor;
			}
		}

		// Token: 0x04000A11 RID: 2577
		internal static MessageDescriptor internal__static_RustProto_Blueprint__Descriptor;

		// Token: 0x04000A12 RID: 2578
		internal static FieldAccessorTable<Blueprint, Blueprint.Builder> internal__static_RustProto_Blueprint__FieldAccessorTable;

		// Token: 0x04000A13 RID: 2579
		private static FileDescriptor descriptor;
	}
}
