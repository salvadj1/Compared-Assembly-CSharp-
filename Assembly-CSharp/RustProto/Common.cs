using System;
using System.Diagnostics;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;

namespace RustProto
{
	// Token: 0x0200022B RID: 555
	[DebuggerNonUserCode]
	public static class Common
	{
		// Token: 0x06001411 RID: 5137 RVA: 0x00044188 File Offset: 0x00042388
		static Common()
		{
			byte[] array = Convert.FromBase64String("ChFydXN0L2NvbW1vbi5wcm90bxIJUnVzdFByb3RvIjIKBlZlY3RvchIMCgF4GAEgASgCOgEwEgwKAXkYAiABKAI6ATASDAoBehgDIAEoAjoBMCJECgpRdWF0ZXJuaW9uEgwKAXgYASABKAI6ATASDAoBeRgCIAEoAjoBMBIMCgF6GAMgASgCOgEwEgwKAXcYBCABKAI6ATBCAkgB");
			FileDescriptor.InternalDescriptorAssigner internalDescriptorAssigner = delegate(FileDescriptor root)
			{
				Common.descriptor = root;
				Common.internal__static_RustProto_Vector__Descriptor = Common.Descriptor.MessageTypes[0];
				Common.internal__static_RustProto_Vector__FieldAccessorTable = new FieldAccessorTable<Vector, Vector.Builder>(Common.internal__static_RustProto_Vector__Descriptor, new string[]
				{
					"X",
					"Y",
					"Z"
				});
				Common.internal__static_RustProto_Quaternion__Descriptor = Common.Descriptor.MessageTypes[1];
				Common.internal__static_RustProto_Quaternion__FieldAccessorTable = new FieldAccessorTable<Quaternion, Quaternion.Builder>(Common.internal__static_RustProto_Quaternion__Descriptor, new string[]
				{
					"X",
					"Y",
					"Z",
					"W"
				});
				return null;
			};
			FileDescriptor.InternalBuildGeneratedFileFrom(array, new FileDescriptor[0], internalDescriptorAssigner);
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x000441CC File Offset: 0x000423CC
		public static void RegisterAllExtensions(ExtensionRegistry registry)
		{
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06001413 RID: 5139 RVA: 0x000441D0 File Offset: 0x000423D0
		public static FileDescriptor Descriptor
		{
			get
			{
				return Common.descriptor;
			}
		}

		// Token: 0x04000A15 RID: 2581
		internal static MessageDescriptor internal__static_RustProto_Vector__Descriptor;

		// Token: 0x04000A16 RID: 2582
		internal static FieldAccessorTable<Vector, Vector.Builder> internal__static_RustProto_Vector__FieldAccessorTable;

		// Token: 0x04000A17 RID: 2583
		internal static MessageDescriptor internal__static_RustProto_Quaternion__Descriptor;

		// Token: 0x04000A18 RID: 2584
		internal static FieldAccessorTable<Quaternion, Quaternion.Builder> internal__static_RustProto_Quaternion__FieldAccessorTable;

		// Token: 0x04000A19 RID: 2585
		private static FileDescriptor descriptor;
	}
}
