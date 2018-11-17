using System;
using System.Diagnostics;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;

namespace RustProto.Proto
{
	// Token: 0x0200025D RID: 605
	[DebuggerNonUserCode]
	public static class Blueprint
	{
		// Token: 0x06001561 RID: 5473 RVA: 0x00048498 File Offset: 0x00046698
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

		// Token: 0x06001562 RID: 5474 RVA: 0x000484DC File Offset: 0x000466DC
		public static void RegisterAllExtensions(ExtensionRegistry registry)
		{
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06001563 RID: 5475 RVA: 0x000484E0 File Offset: 0x000466E0
		public static FileDescriptor Descriptor
		{
			get
			{
				return Blueprint.descriptor;
			}
		}

		// Token: 0x04000B34 RID: 2868
		internal static MessageDescriptor internal__static_RustProto_Blueprint__Descriptor;

		// Token: 0x04000B35 RID: 2869
		internal static FieldAccessorTable<Blueprint, Blueprint.Builder> internal__static_RustProto_Blueprint__FieldAccessorTable;

		// Token: 0x04000B36 RID: 2870
		private static FileDescriptor descriptor;
	}
}
