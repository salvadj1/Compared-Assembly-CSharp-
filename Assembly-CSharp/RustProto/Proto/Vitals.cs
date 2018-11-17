using System;
using System.Diagnostics;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;

namespace RustProto.Proto
{
	// Token: 0x02000238 RID: 568
	[DebuggerNonUserCode]
	public static class Vitals
	{
		// Token: 0x060014CE RID: 5326 RVA: 0x00045AB8 File Offset: 0x00043CB8
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

		// Token: 0x060014CF RID: 5327 RVA: 0x00045AFC File Offset: 0x00043CFC
		public static void RegisterAllExtensions(ExtensionRegistry registry)
		{
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x060014D0 RID: 5328 RVA: 0x00045B00 File Offset: 0x00043D00
		public static FileDescriptor Descriptor
		{
			get
			{
				return Vitals.descriptor;
			}
		}

		// Token: 0x04000A58 RID: 2648
		internal static MessageDescriptor internal__static_RustProto_Vitals__Descriptor;

		// Token: 0x04000A59 RID: 2649
		internal static FieldAccessorTable<Vitals, Vitals.Builder> internal__static_RustProto_Vitals__FieldAccessorTable;

		// Token: 0x04000A5A RID: 2650
		private static FileDescriptor descriptor;
	}
}
