using System;
using System.Diagnostics;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;

namespace RustProto.Proto
{
	// Token: 0x02000265 RID: 613
	[DebuggerNonUserCode]
	public static class ItemMod
	{
		// Token: 0x060015DD RID: 5597 RVA: 0x00049518 File Offset: 0x00047718
		static ItemMod()
		{
			byte[] array = Convert.FromBase64String("ChNydXN0L2l0ZW1fbW9kLnByb3RvEglSdXN0UHJvdG8iIwoHSXRlbU1vZBIKCgJpZBgBIAIoBRIMCgRuYW1lGAIgASgJQgJIAQ==");
			FileDescriptor.InternalDescriptorAssigner internalDescriptorAssigner = delegate(FileDescriptor root)
			{
				ItemMod.descriptor = root;
				ItemMod.internal__static_RustProto_ItemMod__Descriptor = ItemMod.Descriptor.MessageTypes[0];
				ItemMod.internal__static_RustProto_ItemMod__FieldAccessorTable = new FieldAccessorTable<ItemMod, ItemMod.Builder>(ItemMod.internal__static_RustProto_ItemMod__Descriptor, new string[]
				{
					"Id",
					"Name"
				});
				return null;
			};
			FileDescriptor.InternalBuildGeneratedFileFrom(array, new FileDescriptor[0], internalDescriptorAssigner);
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x0004955C File Offset: 0x0004775C
		public static void RegisterAllExtensions(ExtensionRegistry registry)
		{
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x060015DF RID: 5599 RVA: 0x00049560 File Offset: 0x00047760
		public static FileDescriptor Descriptor
		{
			get
			{
				return ItemMod.descriptor;
			}
		}

		// Token: 0x04000B60 RID: 2912
		internal static MessageDescriptor internal__static_RustProto_ItemMod__Descriptor;

		// Token: 0x04000B61 RID: 2913
		internal static FieldAccessorTable<ItemMod, ItemMod.Builder> internal__static_RustProto_ItemMod__FieldAccessorTable;

		// Token: 0x04000B62 RID: 2914
		private static FileDescriptor descriptor;
	}
}
