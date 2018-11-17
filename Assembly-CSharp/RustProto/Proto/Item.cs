using System;
using System.Diagnostics;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;

namespace RustProto.Proto
{
	// Token: 0x02000231 RID: 561
	[DebuggerNonUserCode]
	public static class Item
	{
		// Token: 0x06001485 RID: 5253 RVA: 0x00045098 File Offset: 0x00043298
		static Item()
		{
			byte[] array = Convert.FromBase64String("Cg9ydXN0L2l0ZW0ucHJvdG8SCVJ1c3RQcm90bxoTcnVzdC9pdGVtX21vZC5wcm90byKaAQoESXRlbRIKCgJpZBgBIAIoBRIMCgRuYW1lGAIgASgJEgwKBHNsb3QYAyABKAUSDQoFY291bnQYBCABKAUSEAoIc3Vic2xvdHMYBiABKAUSEQoJY29uZGl0aW9uGAcgASgCEhQKDG1heGNvbmRpdGlvbhgIIAEoAhIgCgdzdWJpdGVtGAUgAygLMg8uUnVzdFByb3RvLkl0ZW1CAkgB");
			FileDescriptor.InternalDescriptorAssigner internalDescriptorAssigner = delegate(FileDescriptor root)
			{
				Item.descriptor = root;
				Item.internal__static_RustProto_Item__Descriptor = Item.Descriptor.MessageTypes[0];
				Item.internal__static_RustProto_Item__FieldAccessorTable = new FieldAccessorTable<Item, Item.Builder>(Item.internal__static_RustProto_Item__Descriptor, new string[]
				{
					"Id",
					"Name",
					"Slot",
					"Count",
					"Subslots",
					"Condition",
					"Maxcondition",
					"Subitem"
				});
				return null;
			};
			FileDescriptor.InternalBuildGeneratedFileFrom(array, new FileDescriptor[]
			{
				ItemMod.Descriptor
			}, internalDescriptorAssigner);
		}

		// Token: 0x06001486 RID: 5254 RVA: 0x000450E4 File Offset: 0x000432E4
		public static void RegisterAllExtensions(ExtensionRegistry registry)
		{
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06001487 RID: 5255 RVA: 0x000450E8 File Offset: 0x000432E8
		public static FileDescriptor Descriptor
		{
			get
			{
				return Item.descriptor;
			}
		}

		// Token: 0x04000A39 RID: 2617
		internal static MessageDescriptor internal__static_RustProto_Item__Descriptor;

		// Token: 0x04000A3A RID: 2618
		internal static FieldAccessorTable<Item, Item.Builder> internal__static_RustProto_Item__FieldAccessorTable;

		// Token: 0x04000A3B RID: 2619
		private static FileDescriptor descriptor;
	}
}
