using FileWatchApi.Utils;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FileWatch.WatchDirectory
{
    public class DirectoryWatch
    {
        private readonly IConfiguration _configuration;
        private readonly HttpUtils _httpUtils;
        public DirectoryWatch(IConfiguration configuration, HttpUtils httpUtils)
        {
            _configuration = configuration;
            _httpUtils = httpUtils;
        }
        public void Run()
        {
            var path = _configuration["WatchPath"];
            using (FileSystemWatcher watcher = new FileSystemWatcher())
            {
                watcher.Path = path;
                watcher.IncludeSubdirectories = false;
                watcher.NotifyFilter = NotifyFilters.LastWrite;
                watcher.Changed += OnChanged;
                watcher.EnableRaisingEvents = true;
                Console.WriteLine("Press 'q' to quit the sample.");
                while (Console.Read() != 'q') ;
            }
        }
        private void OnChanged(object source, FileSystemEventArgs e) => ReadFile(source, e);
        public async Task ReadFile(object source, FileSystemEventArgs e)
        {
            try
            {
                Console.WriteLine(e.FullPath);
                using (var fileStream = new FileStream(e.FullPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    byte[] bytes = new byte[fileStream.Length];
                    fileStream.Read(bytes, 0, bytes.Length);
                    fileStream.Close();
                    using (Stream stream = new MemoryStream(bytes))
                    {
                        await _httpUtils.UploadFile(stream, e.Name);
                        Console.WriteLine(e.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
