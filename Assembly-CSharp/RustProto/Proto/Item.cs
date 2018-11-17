using System;
using System.Diagnostics;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;

namespace RustProto.Proto
{
	// Token: 0x02000264 RID: 612
	[DebuggerNonUserCode]
	public static class Item
	{
		// Token: 0x060015D9 RID: 5593 RVA: 0x00049440 File Offset: 0x00047640
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

		// Token: 0x060015DA RID: 5594 RVA: 0x0004948C File Offset: 0x0004768C
		public static void RegisterAllExtensions(ExtensionRegistry registry)
		{
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x060015DB RID: 5595 RVA: 0x00049490 File Offset: 0x00047690
		public static FileDescriptor Descriptor
		{
			get
			{
				return Item.descriptor;
			}
		}

		// Token: 0x04000B5C RID: 2908
		internal static MessageDescriptor internal__static_RustProto_Item__Descriptor;

		// Token: 0x04000B5D RID: 2909
		internal static FieldAccessorTable<Item, Item.Builder> internal__static_RustProto_Item__FieldAccessorTable;

		// Token: 0x04000B5E RID: 2910
		private static FileDescriptor descriptor;
	}
}
