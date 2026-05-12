using System.Text.Json;
using TaskHub.Models;

namespace TaskHub.Services
{
    public class FileService : IDisposable
    {
        private readonly string _path;

        public FileService(string path)
        {
            _path = path;
        }

        public async Task SaveAsync(List<TaskItem> tasks)
        {
            try
            {
                using FileStream stream = new FileStream(
                    _path,
                    FileMode.Create,
                    FileAccess.Write,
                    FileShare.None);

                await JsonSerializer.SerializeAsync(
                    stream,
                    tasks,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    });
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Save error: {ex.Message}");
            }
        }

        public async Task<List<TaskItem>> LoadAsync()
        {
            try
            {
                if (!File.Exists(_path))
                {
                    return new List<TaskItem>();
                }

                using FileStream stream = new FileStream(
                    _path,
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.Read);

                var tasks =
                    await JsonSerializer.DeserializeAsync
                    <List<TaskItem>>(stream);

                return tasks ?? new List<TaskItem>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Load error: {ex.Message}");

                return new List<TaskItem>();
            }
        }

        public void Dispose()
        {
            Console.WriteLine("FileService disposed");
        }
    }
}
