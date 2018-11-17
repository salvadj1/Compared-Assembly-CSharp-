using System;
using System.Diagnostics;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;

namespace RustProto.Proto
{
	// Token: 0x02000229 RID: 553
	[DebuggerNonUserCode]
	public static class Avatar
	{
		// Token: 0x06001409 RID: 5129 RVA: 0x00043FC0 File Offset: 0x000421C0
		static Avatar()
		{
			byte[] array = Convert.FromBase64String("ChFydXN0L2F2YXRhci5wcm90bxIJUnVzdFByb3RvGhRydXN0L2JsdWVwcmludC5wcm90bxoPcnVzdC9pdGVtLnByb3RvGhFydXN0L2NvbW1vbi5wcm90bxoRcnVzdC92aXRhbHMucHJvdG8iqAIKBkF2YXRhchIeCgNwb3MYASABKAsyES5SdXN0UHJvdG8uVmVjdG9yEiIKA2FuZxgCIAEoCzIVLlJ1c3RQcm90by5RdWF0ZXJuaW9uEiEKBnZpdGFscxgDIAEoCzIRLlJ1c3RQcm90by5WaXRhbHMSKAoKYmx1ZXByaW50cxgEIAMoCzIULlJ1c3RQcm90by5CbHVlcHJpbnQSIgoJaW52ZW50b3J5GAUgAygLMg8uUnVzdFByb3RvLkl0ZW0SIQoId2VhcmFibGUYBiADKAsyDy5SdXN0UHJvdG8uSXRlbRIdCgRiZWx0GAcgAygLMg8uUnVzdFByb3RvLkl0ZW0SJwoJYXdheUV2ZW50GAggASgLMhQuUnVzdFByb3RvLkF3YXlFdmVudCKZAQoJQXdheUV2ZW50EjAKBHR5cGUYASACKA4yIi5SdXN0UHJvdG8uQXdheUV2ZW50LkF3YXlFdmVudFR5cGUSEQoJdGltZXN0YW1wGAIgAigFEhIKCmluc3RpZ2F0b3IYAyABKAQiMwoNQXdheUV2ZW50VHlwZRILCgdVTktOT1dOEAASCwoHU0xVTUJFUhABEggKBERJRUQQAkICSAE=");
			FileDescriptor.InternalDescriptorAssigner internalDescriptorAssigner = delegate(FileDescriptor root)
			{
				Avatar.descriptor = root;
				Avatar.internal__static_RustProto_Avatar__Descriptor = Avatar.Descriptor.MessageTypes[0];
				Avatar.internal__static_RustProto_Avatar__FieldAccessorTable = new FieldAccessorTable<Avatar, Avatar.Builder>(Avatar.internal__static_RustProto_Avatar__Descriptor, new string[]
				{
					"Pos",
					"Ang",
					"Vitals",
					"Blueprints",
					"Inventory",
					"Wearable",
					"Belt",
					"AwayEvent"
				});
				Avatar.internal__static_RustProto_AwayEvent__Descriptor = Avatar.Descriptor.MessageTypes[1];
				Avatar.internal__static_RustProto_AwayEvent__FieldAccessorTable = new FieldAccessorTable<AwayEvent, AwayEvent.Builder>(Avatar.internal__static_RustProto_AwayEvent__Descriptor, new string[]
				{
					"Type",
					"Timestamp",
					"Instigator"
				});
				return null;
			};
			FileDescriptor.InternalBuildGeneratedFileFrom(array, new FileDescriptor[]
			{
				Blueprint.Descriptor,
				Item.Descriptor,
				Common.Descriptor,
				Vitals.Descriptor
			}, internalDescriptorAssigner);
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x00044024 File Offset: 0x00042224
		public static void RegisterAllExtensions(ExtensionRegistry registry)
		{
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x0600140B RID: 5131 RVA: 0x00044028 File Offset: 0x00042228
		public static FileDescriptor Descriptor
		{
			get
			{
				return Avatar.descriptor;
			}
		}

		// Token: 0x04000A0B RID: 2571
		internal static MessageDescriptor internal__static_RustProto_Avatar__Descriptor;

		// Token: 0x04000A0C RID: 2572
		internal static FieldAccessorTable<Avatar, Avatar.Builder> internal__static_RustProto_Avatar__FieldAccessorTable;

		// Token: 0x04000A0D RID: 2573
		internal static MessageDescriptor internal__static_RustProto_AwayEvent__Descriptor;

		// Token: 0x04000A0E RID: 2574
		internal static FieldAccessorTable<AwayEvent, AwayEvent.Builder> internal__static_RustProto_AwayEvent__FieldAccessorTable;

		// Token: 0x04000A0F RID: 2575
		private static FileDescriptor descriptor;
	}
}
