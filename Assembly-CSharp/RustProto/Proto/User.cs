using System;
using System.Diagnostics;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;

namespace RustProto.Proto
{
	// Token: 0x02000266 RID: 614
	[DebuggerNonUserCode]
	public static class User
	{
		// Token: 0x060015E1 RID: 5601 RVA: 0x000495B8 File Offset: 0x000477B8
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

		// Token: 0x060015E2 RID: 5602 RVA: 0x000495FC File Offset: 0x000477FC
		public static void RegisterAllExtensions(ExtensionRegistry registry)
		{
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x060015E3 RID: 5603 RVA: 0x00049600 File Offset: 0x00047800
		public static FileDescriptor Descriptor
		{
			get
			{
				return User.descriptor;
			}
		}

		// Token: 0x04000B64 RID: 2916
		internal static MessageDescriptor internal__static_RustProto_User__Descriptor;

		// Token: 0x04000B65 RID: 2917
		internal static FieldAccessorTable<User, User.Builder> internal__static_RustProto_User__FieldAccessorTable;

		// Token: 0x04000B66 RID: 2918
		private static FileDescriptor descriptor;
	}
}
