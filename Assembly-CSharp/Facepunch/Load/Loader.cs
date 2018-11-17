using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;

namespace Facepunch.Load
{
	// Token: 0x0200029B RID: 667
	public sealed class Loader : IDisposable, IDownloadTask
	{
		// Token: 0x060017C7 RID: 6087 RVA: 0x000592D8 File Offset: 0x000574D8
		private Loader(Group masterGroup, Job[] allJobs, Group[] allGroups, IDownloaderDispatch dispatch)
		{
			this.MasterGroup = masterGroup;
			this.Jobs = allJobs;
			this.Groups = allGroups;
			this.Dispatch = dispatch;
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060017C8 RID: 6088 RVA: 0x00059310 File Offset: 0x00057510
		// (remove) Token: 0x060017C9 RID: 6089 RVA: 0x0005932C File Offset: 0x0005752C
		public event AssetBundleLoadedEventHandler OnAssetBundleLoaded;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060017CA RID: 6090 RVA: 0x00059348 File Offset: 0x00057548
		// (remove) Token: 0x060017CB RID: 6091 RVA: 0x00059364 File Offset: 0x00057564
		public event MultipleAssetBundlesLoadedEventHandler OnAllAssetBundlesLoaded;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060017CC RID: 6092 RVA: 0x00059380 File Offset: 0x00057580
		// (remove) Token: 0x060017CD RID: 6093 RVA: 0x0005939C File Offset: 0x0005759C
		public event MultipleAssetBundlesLoadedEventHandler OnGroupedAssetBundlesLoaded;

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x060017CE RID: 6094 RVA: 0x000593B8 File Offset: 0x000575B8
		string IDownloadTask.Name
		{
			get
			{
				return "Loading all bundles";
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x060017CF RID: 6095 RVA: 0x000593C0 File Offset: 0x000575C0
		string IDownloadTask.ContextualDescription
		{
			get
			{
				return this.MasterGroup.ContextualDescription;
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x060017D0 RID: 6096 RVA: 0x000593D0 File Offset: 0x000575D0
		public int ByteLength
		{
			get
			{
				return this.MasterGroup.ByteLength;
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x060017D1 RID: 6097 RVA: 0x000593E0 File Offset: 0x000575E0
		public int ByteLengthDownloaded
		{
			get
			{
				return this.MasterGroup.ByteLengthDownloaded;
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x060017D2 RID: 6098 RVA: 0x000593F0 File Offset: 0x000575F0
		public float PercentDone
		{
			get
			{
				return this.MasterGroup.PercentDone;
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x060017D3 RID: 6099 RVA: 0x00059400 File Offset: 0x00057600
		public TaskStatus TaskStatus
		{
			get
			{
				return this.MasterGroup.TaskStatus;
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x060017D4 RID: 6100 RVA: 0x00059410 File Offset: 0x00057610
		public int Count
		{
			get
			{
				return this.MasterGroup.Count;
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x060017D5 RID: 6101 RVA: 0x00059420 File Offset: 0x00057620
		public int Done
		{
			get
			{
				return this.MasterGroup.Done;
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x060017D6 RID: 6102 RVA: 0x00059430 File Offset: 0x00057630
		public TaskStatusCount TaskStatusCount
		{
			get
			{
				return this.MasterGroup.TaskStatusCount;
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x060017D7 RID: 6103 RVA: 0x00059440 File Offset: 0x00057640
		public Group CurrentGroup
		{
			get
			{
				return (this.currentGroup < 0 || this.currentGroup >= this.Groups.Length) ? null : this.Groups[this.currentGroup];
			}
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x00059480 File Offset: 0x00057680
		private static Loader Deserialize(Reader reader, IDownloaderDispatch dispatch)
		{
			List<Item[]> list = new List<Item[]>();
			List<Item> list2 = new List<Item>();
			while (reader.Read())
			{
				switch (reader.Token)
				{
				case Token.RandomLoadOrderAreaBegin:
					list2.Clear();
					break;
				case Token.BundleListing:
					list2.Add(reader.Item);
					break;
				case Token.RandomLoadOrderAreaEnd:
					list.Add(list2.ToArray());
					break;
				case Token.DownloadQueueEnd:
				{
					Operation operation = new Operation();
					int num = 0;
					foreach (Item[] array in list)
					{
						num += array.Length;
					}
					Job[] array2 = new Job[num];
					int num2 = 0;
					foreach (Item[] array3 in list)
					{
						foreach (Item item in array3)
						{
							array2[num2++] = new Job
							{
								_op = operation,
								Item = item
							};
						}
					}
					Group group = new Group();
					group._op = operation;
					group.Jobs = array2;
					group.Initialize();
					Group[] array5 = new Group[list.Count];
					int num3 = 0;
					int num4 = 0;
					foreach (Item[] array6 in list)
					{
						int num5 = array6.Length;
						Job[] array7 = new Job[num5];
						for (int j = 0; j < num5; j++)
						{
							array7[j] = array2[num3++];
						}
						array5[num4] = new Group();
						array5[num4]._op = operation;
						array5[num4].Jobs = array7;
						array5[num4].Initialize();
						for (int k = 0; k < num5; k++)
						{
							array7[k].Group = array5[num4];
						}
						num4++;
					}
					return operation.Loader = new Loader(group, array2, array5, dispatch);
				}
				}
			}
			throw new InvalidProgramException();
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x00059734 File Offset: 0x00057934
		public static Loader CreateFromText(string downloadListJson, string bundlePath, IDownloaderDispatch dispatch)
		{
			Loader result;
			using (Reader reader = Reader.CreateFromText(downloadListJson, bundlePath))
			{
				result = Loader.Deserialize(reader, dispatch);
			}
			return result;
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x00059788 File Offset: 0x00057988
		public static Loader CreateFromFile(string downloadListPath, string bundlePath, IDownloaderDispatch dispatch)
		{
			Loader result;
			using (Reader reader = Reader.CreateFromFile(downloadListPath, bundlePath))
			{
				result = Loader.Deserialize(reader, dispatch);
			}
			return result;
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x000597DC File Offset: 0x000579DC
		public static Loader CreateFromFile(string downloadListPath, IDownloaderDispatch dispatch)
		{
			Loader result;
			using (Reader reader = Reader.CreateFromFile(downloadListPath))
			{
				result = Loader.Deserialize(reader, dispatch);
			}
			return result;
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x0005982C File Offset: 0x00057A2C
		public static Loader CreateFromReader(TextReader textReader, string bundlePath, IDownloaderDispatch dispatch)
		{
			Loader result;
			using (Reader reader = Reader.CreateFromReader(textReader, bundlePath))
			{
				result = Loader.Deserialize(reader, dispatch);
			}
			return result;
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x00059880 File Offset: 0x00057A80
		public static Loader CreateFromReader(JsonReader jsonReader, string bundlePath, IDownloaderDispatch dispatch)
		{
			Loader result;
			using (Reader reader = Reader.CreateFromReader(jsonReader, bundlePath))
			{
				result = Loader.Deserialize(reader, dispatch);
			}
			return result;
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x000598D4 File Offset: 0x00057AD4
		public static Loader Create(Reader reader, IDownloaderDispatch dispatch)
		{
			return Loader.Deserialize(reader, dispatch);
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x000598E0 File Offset: 0x00057AE0
		internal void OnJobCompleted(Job job, IDownloader downloader)
		{
			job.AssetBundle = downloader.GetLoadedAssetBundle(job);
			if (this.OnAssetBundleLoaded != null)
			{
				try
				{
					this.OnAssetBundleLoaded(job.AssetBundle, job.Item);
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
				}
			}
			downloader.OnJobCompleted(job);
			this.Dispatch.DeleteDownloader(job, downloader);
			if (++this.jobsCompleted == this.MasterGroup.Count)
			{
				if (this.OnAllAssetBundlesLoaded != null)
				{
					AssetBundle[] assetBundle;
					Item[] item;
					this.MasterGroup.GetArrays(out assetBundle, out item);
					try
					{
						this.OnAllAssetBundlesLoaded(assetBundle, item);
					}
					catch (Exception ex2)
					{
						Debug.LogException(ex2);
					}
				}
				this.DisposeDispatch();
			}
			else if (++this.jobsCompletedInGroup == this.Groups[this.currentGroup].Jobs.Length)
			{
				if (this.OnGroupedAssetBundlesLoaded != null)
				{
					AssetBundle[] assetBundle2;
					Item[] item2;
					this.Groups[this.currentGroup].GetArrays(out assetBundle2, out item2);
					try
					{
						this.OnGroupedAssetBundlesLoaded(assetBundle2, item2);
					}
					catch (Exception ex3)
					{
						Debug.LogException(ex3);
					}
				}
				this.StartNextGroup();
			}
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x00059A68 File Offset: 0x00057C68
		private void DisposeDispatch()
		{
			if (this.Dispatch != null)
			{
				IDownloaderDispatch dispatch = this.Dispatch;
				this.Dispatch = null;
				dispatch.UnbindLoader(this);
			}
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x00059A98 File Offset: 0x00057C98
		public void Dispose()
		{
			if (!this.disposed)
			{
				this.disposed = true;
				this.DisposeDispatch();
			}
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x00059AB4 File Offset: 0x00057CB4
		private void StartJob(Job job)
		{
			IDownloader downloader = this.Dispatch.CreateDownloaderForJob(job);
			downloader.BeginJob(job);
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x00059AD8 File Offset: 0x00057CD8
		private void StartNextGroup()
		{
			this.jobsCompletedInGroup = 0;
			foreach (Job job in this.Groups[++this.currentGroup].Jobs)
			{
				this.StartJob(job);
			}
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x00059B2C File Offset: 0x00057D2C
		public void StartLoading()
		{
			if (this.currentGroup == -1)
			{
				this.Dispatch.BindLoader(this);
				if (this.Groups.Length > 0)
				{
					this.StartNextGroup();
				}
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x060017E5 RID: 6117 RVA: 0x00059B68 File Offset: 0x00057D68
		public IEnumerator WaitEnumerator
		{
			get
			{
				while (this.jobsCompleted < this.MasterGroup.Jobs.Length)
				{
					yield return null;
				}
				yield break;
			}
		}

		// Token: 0x04000C8B RID: 3211
		[NonSerialized]
		private readonly Group MasterGroup;

		// Token: 0x04000C8C RID: 3212
		[NonSerialized]
		public readonly Group[] Groups;

		// Token: 0x04000C8D RID: 3213
		[NonSerialized]
		public readonly Job[] Jobs;

		// Token: 0x04000C8E RID: 3214
		[NonSerialized]
		private int jobsCompleted;

		// Token: 0x04000C8F RID: 3215
		[NonSerialized]
		private int currentGroup = -1;

		// Token: 0x04000C90 RID: 3216
		[NonSerialized]
		private int jobsCompletedInGroup;

		// Token: 0x04000C91 RID: 3217
		[NonSerialized]
		private bool disposed;

		// Token: 0x04000C92 RID: 3218
		[NonSerialized]
		private IDownloaderDispatch Dispatch;
	}
}
