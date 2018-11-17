using System;
using System.Diagnostics;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;

namespace RustProto.Proto
{
	// Token: 0x02000232 RID: 562
	[DebuggerNonUserCode]
	public static class ItemMod
	{
		// Token: 0x06001489 RID: 5257 RVA: 0x00045170 File Offset: 0x00043370
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

		// Token: 0x0600148A RID: 5258 RVA: 0x000451B4 File Offset: 0x000433B4
		public static void RegisterAllExtensions(ExtensionRegistry registry)
		{
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x0600148B RID: 5259 RVA: 0x000451B8 File Offset: 0x000433B8
		public static FileDescriptor Descriptor
		{
			get
			{
				return ItemMod.descriptor;
			}
		}

		// Token: 0x04000A3D RID: 2621
		internal static MessageDescriptor internal__static_RustProto_ItemMod__Descriptor;

		// Token: 0x04000A3E RID: 2622
		internal static FieldAccessorTable<ItemMod, ItemMod.Builder> internal__static_RustProto_ItemMod__FieldAccessorTable;

		// Token: 0x04000A3F RID: 2623
		private static FileDescriptor descriptor;
	}
}
