using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Calabonga.Microservices.Core.Exceptions;

namespace Calabonga.Microservices.Core
{
    /// <summary>
    /// System utilities
    /// </summary>
    public static class Utilities
    {
        /// <summary>
        /// Compute Hash
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static byte[] ComputeHash(string filePath)
        {
            var runCount = 1;
            while (runCount < 4)
            {
                try
                {
                    if (!File.Exists(filePath))
                    {
                        throw new FileNotFoundException();
                    }

                    using (var fs = File.OpenRead(filePath))
                    {
                        return SHA1.Create().ComputeHash(fs);
                    }
                }
                catch (IOException ex)
                {
                    if (runCount == 3 || ex.HResult != -2147024864)
                    {
                        throw;
                    }
                    else
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(Math.Pow(2, runCount)));
                        runCount++;
                    }
                }
            }

            return new byte[20];
        }

        /// <summary>
        /// Return file content
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static async Task<string> GetFileContent(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException();
                }

                return await File.ReadAllTextAsync(filePath);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Remove file from directory (physical deleting)
        /// </summary>
        /// <param name="filePath"></param>
        public static void DeleteFile(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException();
                }

                File.Delete(filePath);
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        /// Save content to the text file
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static async Task SetFileContent(string filePath, string content)
        {
            try
            {
                var folder = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                if (File.Exists(filePath))
                {
                    throw new MicroserviceFileAlreadyExistsException();
                }

                using (var fs = File.Create(filePath))
                {
                    var info = new UTF8Encoding(true).GetBytes(content);
                    await fs.WriteAsync(info, 0, info.Length);
                }
            }
            catch
            {
                // ignored
                
            }
        }

        /// <summary>
        /// Generate ETag for content bytes
        /// </summary>
        /// <param name="key"></param>
        /// <param name="contentBytes"></param>
        /// <returns></returns>
        public static string GetETag(string key, byte[] contentBytes)
        {
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var combinedBytes = Combine(keyBytes, contentBytes);
            return GenerateETag(combinedBytes);
        }

        /// <summary>
        /// Returns Working folder path
        /// </summary>
        /// <returns></returns>
        public static string GetWorkingFolder()
        {
            var location = System.Reflection.Assembly.GetEntryAssembly().Location;
            return Path.GetDirectoryName(location);
        }

        private static string GenerateETag(byte[] data)
        {
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(data);
                var hex = BitConverter.ToString(hash);
                return hex.Replace("-", "");
            }
        }

        private static byte[] Combine(byte[] a, byte[] b)
        {
            var c = new byte[a.Length + b.Length];
            Buffer.BlockCopy(a, 0, c, 0, a.Length);
            Buffer.BlockCopy(b, 0, c, a.Length, b.Length);
            return c;
        }
    }
}
