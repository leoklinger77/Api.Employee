using Domain.Interfaces;
using Domain.Notifications;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading;

namespace Api.Extension
{
    public class FileService
    {
        private readonly INotifier _notifier;

        public FileService(INotifier notifier)
        {
            _notifier = notifier;
        }

        public bool UpdateFile(IFormFile file, string imgName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", imgName + file.FileName);
            if (File.Exists(filePath))
            {
                _notifier.Handle(new Notification("Essa imagem já existe."));
                return false;
            }

            new Thread(() =>
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyToAsync(stream);
                }
            }).Start();

            return true;
        }
        public void DeleteFile(string pathImage)
        {
            FileInfo fi = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", pathImage));
            try
            {
                fi.Delete();
            }
            catch (IOException)
            {
                _notifier.Handle(new Notification("Ocorreu um erro ao Deletar o arquivo: " + pathImage));
            }
        }
    }
}
