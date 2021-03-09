﻿using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MongoDownloader
{
    internal static class Program
    {
        private static async Task<int> Main()
        {
            try
            {
                var cancellationTokenSource = new CancellationTokenSource();
                Console.CancelKeyPress += (_, eventArgs) =>
                {
                    // Try to cancel gracefully the first time, then abort the process the second time Ctrl+C is pressed
                    eventArgs.Cancel = !cancellationTokenSource.IsCancellationRequested;
                    cancellationTokenSource.Cancel();
                };
                var archiveExtractor = new ArchiveExtractor("mongod", "mongoexport", "mongoimport");
                var downloader = new MongoDbDownloader(archiveExtractor, new Options());
                var toolsDirectory = GetToolsDirectory();
                await downloader.RunAsync(toolsDirectory, cancellationTokenSource.Token);
                Console.WriteLine();
                Console.WriteLine($"✅ Downloaded and extracted MongoDB archives into {toolsDirectory.FullName}");
                return 0;
            }
            catch (Exception exception)
            {
                if (!(exception is OperationCanceledException))
                {
                    Console.Error.WriteLine(exception);
                }
                return 1;
            }
        }

        private static DirectoryInfo GetToolsDirectory()
        {
            for (var directory = new DirectoryInfo("."); directory != null; directory = directory.Parent)
            {
                var toolsDirectory = directory.GetDirectories("tools", SearchOption.TopDirectoryOnly).SingleOrDefault();
                if (toolsDirectory?.Exists ?? false)
                {
                    return toolsDirectory;
                }
            }
            throw new InvalidOperationException("The tools directory was not found");
        }
    }
}
