using System;
using System.IO;
using System.Reflection;
using UnityEngine;

// Token: 0x02000409 RID: 1033
public class BrandingVersion : MonoBehaviour
{
	// Token: 0x060025DC RID: 9692 RVA: 0x00091FA8 File Offset: 0x000901A8
	private void Start()
	{
		DateTime dateTime = this.RetrieveLinkerTimestamp();
		this.textVersion.Text = dateTime.ToString("d MMM yyyy\\, h:mmtt");
	}

	// Token: 0x060025DD RID: 9693 RVA: 0x00091FD4 File Offset: 0x000901D4
	private DateTime RetrieveLinkerTimestamp()
	{
		string location = Assembly.GetCallingAssembly().Location;
		byte[] array = new byte[2048];
		Stream stream = null;
		try
		{
			stream = new FileStream(location, FileMode.Open, FileAccess.Read);
			stream.Read(array, 0, 2048);
		}
		finally
		{
			if (stream != null)
			{
				stream.Close();
			}
		}
		int num = BitConverter.ToInt32(array, 60);
		int num2 = BitConverter.ToInt32(array, num + 8);
		DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0);
		dateTime = dateTime.AddSeconds((double)num2);
		dateTime = dateTime.AddHours((double)TimeZone.CurrentTimeZone.GetUtcOffset(dateTime).Hours);
		return dateTime;
	}

	// Token: 0x04001275 RID: 4725
	public dfRichTextLabel textVersion;
}
