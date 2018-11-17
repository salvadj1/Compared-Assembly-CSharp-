using System;
using System.Collections;
using JSON;
using UnityEngine;

// Token: 0x02000851 RID: 2129
public class GameConfig : MonoBehaviour
{
	// Token: 0x06004AF1 RID: 19185 RVA: 0x00147F0C File Offset: 0x0014610C
	private void Start()
	{
		if (PlayerPrefs.HasKey(this.storeKey))
		{
			this.LoadFrom(PlayerPrefs.GetString(this.storeKey));
		}
		base.StartCoroutine(this.LoadFromURL());
	}

	// Token: 0x06004AF2 RID: 19186 RVA: 0x00147F48 File Offset: 0x00146148
	private IEnumerator LoadFromURL()
	{
		for (;;)
		{
			WWW www = new WWW(this.jsonURL);
			yield return www;
			if (string.IsNullOrEmpty(www.error))
			{
				this.LoadFrom(www.text);
				yield return new WaitForSeconds(this.updateMinutes * 60f);
			}
			else
			{
				Debug.LogWarning("GameConfig: " + www.error);
				yield return new WaitForSeconds(30f);
			}
		}
		yield break;
	}

	// Token: 0x06004AF3 RID: 19187 RVA: 0x00147F64 File Offset: 0x00146164
	private void LoadFrom(string strData)
	{
		JSON.Object @object = null;
		try
		{
			@object = JSON.Object.Parse(strData);
		}
		catch (Exception)
		{
			return;
		}
		if (@object == null)
		{
			return;
		}
		try
		{
			PlayerPrefs.SetString(this.storeKey, strData);
		}
		catch (Exception)
		{
		}
		this.json = @object;
		this.isLoaded = true;
		if (this.onUpdated != null)
		{
			this.onUpdated(this.json);
		}
	}

	// Token: 0x04002C05 RID: 11269
	public string jsonURL;

	// Token: 0x04002C06 RID: 11270
	public string storeKey;

	// Token: 0x04002C07 RID: 11271
	public float updateMinutes;

	// Token: 0x04002C08 RID: 11272
	public Action<JSON.Object> onUpdated;

	// Token: 0x04002C09 RID: 11273
	[NonSerialized]
	public bool isLoaded;

	// Token: 0x04002C0A RID: 11274
	[NonSerialized]
	public JSON.Object json;
}
