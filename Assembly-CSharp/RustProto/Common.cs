using System;
using System.Diagnostics;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;

namespace RustProto
{
	// Token: 0x0200025E RID: 606
	[DebuggerNonUserCode]
	public static class Common
	{
		// Token: 0x06001565 RID: 5477 RVA: 0x00048530 File Offset: 0x00046730
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

		// Token: 0x06001566 RID: 5478 RVA: 0x00048574 File Offset: 0x00046774
		public static void RegisterAllExtensions(ExtensionRegistry registry)
		{
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06001567 RID: 5479 RVA: 0x00048578 File Offset: 0x00046778
		public static FileDescriptor Descriptor
		{
			get
			{
				return Common.descriptor;
			}
		}

		// Token: 0x04000B38 RID: 2872
		internal static MessageDescriptor internal__static_RustProto_Vector__Descriptor;

		// Token: 0x04000B39 RID: 2873
		internal static FieldAccessorTable<Vector, Vector.Builder> internal__static_RustProto_Vector__FieldAccessorTable;

		// Token: 0x04000B3A RID: 2874
		internal static MessageDescriptor internal__static_RustProto_Quaternion__Descriptor;

		// Token: 0x04000B3B RID: 2875
		internal static FieldAccessorTable<Quaternion, Quaternion.Builder> internal__static_RustProto_Quaternion__FieldAccessorTable;

		// Token: 0x04000B3C RID: 2876
		private static FileDescriptor descriptor;
	}
}
