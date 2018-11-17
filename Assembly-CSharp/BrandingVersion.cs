using System;
using System.IO;
using System.Reflection;
using UnityEngine;

// Token: 0x020004BD RID: 1213
public class BrandingVersion : MonoBehaviour
{
	// Token: 0x06002962 RID: 10594 RVA: 0x00097E6C File Offset: 0x0009606C
	private void Start()
	{
		DateTime dateTime = this.RetrieveLinkerTimestamp();
		this.textVersion.Text = dateTime.ToString("d MMM yyyy\\, h:mmtt");
	}

	// Token: 0x06002963 RID: 10595 RVA: 0x00097E98 File Offset: 0x00096098
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

	// Token: 0x040013F5 RID: 5109
	public global::dfRichTextLabel textVersion;
}
