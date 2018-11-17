using System;
using System.Diagnostics;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;

namespace RustProto.Proto
{
	// Token: 0x02000233 RID: 563
	[DebuggerNonUserCode]
	public static class User
	{
		// Token: 0x0600148D RID: 5261 RVA: 0x00045210 File Offset: 0x00043410
		static User()
		{
			byte[] array = Convert.FromBase64String("Cg9ydXN0L3VzZXIucHJvdG8SCVJ1c3RQcm90byKKAQoEVXNlchIOCgZ1c2VyaWQYASACKAQSEwoLZGlzcGxheW5hbWUYAiACKAkSLAoJdXNlcmdyb3VwGAMgAigOMhkuUnVzdFByb3RvLlVzZXIuVXNlckdyb3VwIi8KCVVzZXJHcm91cBILCgdSRUdVTEFSEAASCgoGQkFOTkVEEAESCQoFQURNSU4QAkICSAE=");
			FileDescriptor.InternalDescriptorAssigner internalDescriptorAssigner = delegate(FileDescriptor root)
			{
				User.descriptor = root;
				User.internal__static_RustProto_User__Descriptor = User.Descriptor.MessageTypes[0];
				User.internal__static_RustProto_User__FieldAccessorTable = new FieldAccessorTable<User, User.Builder>(User.internal__static_RustProto_User__Descriptor, new string[]
				{
					"Userid",
					"Displayname",
					"Usergroup"
				});
				return null;
			};
			FileDescriptor.InternalBuildGeneratedFileFrom(array, new FileDescriptor[0], internalDescriptorAssigner);
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x00045254 File Offset: 0x00043454
		public static void RegisterAllExtensions(ExtensionRegistry registry)
		{
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x0600148F RID: 5263 RVA: 0x00045258 File Offset: 0x00043458
		public static FileDescriptor Descriptor
		{
			get
			{
				return User.descriptor;
			}
		}

		// Token: 0x04000A41 RID: 2625
		internal static MessageDescriptor internal__static_RustProto_User__Descriptor;

		// Token: 0x04000A42 RID: 2626
		internal static FieldAccessorTable<User, User.Builder> internal__static_RustProto_User__FieldAccessorTable;

		// Token: 0x04000A43 RID: 2627
		private static FileDescriptor descriptor;
	}
}
