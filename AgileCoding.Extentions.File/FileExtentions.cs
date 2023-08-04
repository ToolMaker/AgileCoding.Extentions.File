namespace AgileCoding.Extentions.Files
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;

    public static class FileExtentions
    {
        public static void ThrowIfFileDoesntExist<TException>(this string filePath, string exceptionMessage)
            where TException : Exception
        {
            if (!File.Exists(filePath))
            {
                var exception = Activator.CreateInstance(typeof(TException), exceptionMessage) as TException;
                if (exception != null)
                {
                    throw exception;
                }
                else
                {
                    throw new InvalidOperationException("Could not create exception of type " + typeof(TException).FullName);
                }
            }
        }

        public static bool FileIsInUse(this FileInfo self)
        {
            if (!self.Exists)
            {
                throw new InvalidOperationException($"Cant check if file is in use because file {self.FullName} does not exist");
            }

            try
            {
                using (FileStream fs = new FileStream(self.FullName, FileMode.OpenOrCreate))
                { }

                return false;
            }
            catch (Exception ex)
            {
                var loweredMessage = ex.Message.ToLower();
                if (loweredMessage.Contains("the process cannot access the file") &&
                    loweredMessage.Contains("because it is being used by another process."))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static void FileIsInUse<TException>(this FileInfo self, string? exceptionMessage = null)
        where TException : Exception
        {
            if (exceptionMessage == null)
            {
                exceptionMessage = $"File given {self.FullName} is in use.";
            }

            if (FileIsInUse(self))
            {
                var exception = Activator.CreateInstance(typeof(TException), exceptionMessage) as TException;
                if (exception != null)
                {
                    throw exception;
                }
                else
                {
                    throw new InvalidOperationException("Could not create exception of type " + typeof(TException).FullName);
                }
            }
        }

        public static bool FileExist(this string filePath)
        {
            return File.Exists(filePath);
        }

        public static string ShiftDirectotyBack(this FileInfo file, int directoriesBack, string aditionalPath = "")
        {
            return string.Format("{0}\\{1}{2}\\{3}", file.Directory?.FullName, string.Concat(Enumerable.Repeat("..\\", directoriesBack)), aditionalPath, file.Name);
        }

        public static byte[] GetFileHash(this FileInfo file)
        {
            byte[]? result = null;
            using (var stream = file.OpenRead())
            {
                using (SHA1 sha = SHA1.Create())
                {
                    result = sha.ComputeHash(stream);
                }
            }

            return result;
        }

        public static string? CreateFileNameWithExtention(this string fileName, string extention)
        {
            if (fileName != null)
            {
                if (!fileName.Contains('.') ||
                    !(fileName.ToLower().EndsWith($".{extention.ToLower()}"))
                   )
                {
                    return $"{fileName}.{extention.ToLower()}";
                }
                else
                {
                    return fileName;
                }
            }

            return fileName;
        }

        public static FileInfo CreateFileAndDelteIfExist(this string fullFileName)
        {
            var zipFileName = fullFileName;
            if (File.Exists(zipFileName))
            {
                File.Delete(zipFileName);
            }

            return new FileInfo(zipFileName);
        }

        public static IEnumerable<string?> AllLines(this FileInfo file)
        {
            using (StreamReader streamReader = new StreamReader(file.OpenRead()))
            {
                while (!streamReader.EndOfStream)
                {
                    yield return streamReader.ReadLine();
                }
            }
        }
    }
}
