using System.Diagnostics;
using System.IO;
using System.Net;

internal class Program
{
    private static async Task Main(string[] args)
    {

        bool ok;
        HttpClient client = new HttpClient();
        Console.WriteLine("Введите (вставьте) ссылку на изображение");
        var httpathimage = Console.ReadLine();
        Console.WriteLine("Введите (вставьте) ссылку на скачивание песни");
        var httpathmusic = Console.ReadLine();
        if (httpathimage != "" & httpathmusic != "")
        {
            string imagepath = "D:/VSWorks/DownloadImages/image.jpg";
            string musicpath = "D:/VSWorks/DownloadImages/music.mp3";
            ok = false;
            try
            {
                try
                {
                    Console.WriteLine("Введите путь для сохранения изображения");
                    imagepath = Console.ReadLine();
                    Console.WriteLine("Введите путь для сохранения песни");
                    musicpath = Console.ReadLine();
                }
                catch (Exception f)
                {
                    f = new Exception("Неверный путь для сохранения");
                    Console.WriteLine(f.Message);
                }
                File.Delete(imagepath);
                File.Delete(musicpath);
                HttpResponseMessage response = await client.GetAsync(httpathmusic);
                byte[] datamusic = await response.Content.ReadAsByteArrayAsync();
                await File.WriteAllBytesAsync(musicpath, datamusic);
                WebClient wclient = new WebClient();
                wclient.DownloadFile(new Uri(httpathimage), imagepath);
                ok = true;
                Console.WriteLine("Изображение успешно загружено");
                Console.WriteLine("Песня успешно загружена");
            }
            catch (Exception e)
            {
                e = new Exception("Что-то пошло не так :/ ");
                Console.WriteLine(e.Message);
            }
            if (ok == true)
            {
                ProcessStartInfo play, open;
                play = new ProcessStartInfo();
                open = new ProcessStartInfo();
                play.FileName = "cmd";
                open.FileName = "cmd";
                open.Arguments = @$"/c {imagepath}";
                play.Arguments = $@"/c {musicpath}";
                Process.Start(play);
                Process.Start(open);
            }
        }
        else
        {
            await Console.Out.WriteLineAsync("Ссылки на скачивание не были введены");
        }
    }
}