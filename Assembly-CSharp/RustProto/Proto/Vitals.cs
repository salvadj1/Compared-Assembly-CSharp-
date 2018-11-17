using System;
using System.Diagnostics;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;

namespace RustProto.Proto
{
	// Token: 0x0200026B RID: 619
	[DebuggerNonUserCode]
	public static class Vitals
	{
		// Token: 0x06001622 RID: 5666 RVA: 0x00049E60 File Offset: 0x00048060
		static Vitals()
		{
			byte[] array = Convert.FromBase64String("ChFydXN0L3ZpdGFscy5wcm90bxIJUnVzdFByb3RvIu8BCgZWaXRhbHMSEwoGaGVhbHRoGAEgASgCOgMxMDASFQoJaHlkcmF0aW9uGAIgASgCOgIzMBIWCghjYWxvcmllcxgDIAEoAjoEMTAwMBIUCglyYWRpYXRpb24YBCABKAI6ATASGQoOcmFkaWF0aW9uX2FudGkYBSABKAI6ATASFgoLYmxlZWRfc3BlZWQYBiABKAI6ATASFAoJYmxlZWRfbWF4GAcgASgCOgEwEhUKCmhlYWxfc3BlZWQYCCABKAI6ATASEwoIaGVhbF9tYXgYCSABKAI6ATASFgoLdGVtcGVyYXR1cmUYCiABKAI6ATBCAkgB");
			FileDescriptor.InternalDescriptorAssigner internalDescriptorAssigner = delegate(FileDescriptor root)
			{
				Vitals.descriptor = root;
				Vitals.internal__static_RustProto_Vitals__Descriptor = Vitals.Descriptor.MessageTypes[0];
				Vitals.internal__static_RustProto_Vitals__FieldAccessorTable = new FieldAccessorTable<Vitals, Vitals.Builder>(Vitals.internal__static_RustProto_Vitals__Descriptor, new string[]
				{
					"Health",
					"Hydration",
					"Calories",
					"Radiation",
					"RadiationAnti",
					"BleedSpeed",
					"BleedMax",
					"HealSpeed",
					"HealMax",
					"Temperature"
				});
				return null;
			};
			FileDescriptor.InternalBuildGeneratedFileFrom(array, new FileDescriptor[0], internalDescriptorAssigner);
		}

		// Token: 0x06001623 RID: 5667 RVA: 0x00049EA4 File Offset: 0x000480A4
		public static void RegisterAllExtensions(ExtensionRegistry registry)
		{
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001624 RID: 5668 RVA: 0x00049EA8 File Offset: 0x000480A8
		public static FileDescriptor Descriptor
		{
			get
			{
				return Vitals.descriptor;
			}
		}

		// Token: 0x04000B7B RID: 2939
		internal static MessageDescriptor internal__static_RustProto_Vitals__Descriptor;

		// Token: 0x04000B7C RID: 2940
		internal static FieldAccessorTable<Vitals, Vitals.Builder> internal__static_RustProto_Vitals__FieldAccessorTable;

		// Token: 0x04000B7D RID: 2941
		private static FileDescriptor descriptor;
	}
}
