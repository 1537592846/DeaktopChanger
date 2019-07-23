using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;

namespace DesktopChanger
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory() + "\\Images";
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System",true);
            var images = Directory.GetFiles(path);
            if (images.Where(x => !x.Contains("Used-")).Count() == 0)
            {
                foreach(var image in images)
                {
                    Directory.Move(image, image.Replace("Used-", ""));
                }
                images = Directory.GetFiles(path);
            }
            var rand = new Random();
            images = images.Where(x => !x.Contains("Used-")).ToArray();
            string imageChosen = images[rand.Next()%images.Count()];
            Directory.Move(imageChosen, imageChosen.Insert(imageChosen.LastIndexOf("\\") + 1, "Used-"));
            imageChosen = imageChosen.Insert(imageChosen.LastIndexOf("\\") + 1, "Used-");
            key.SetValue("Wallpaper", imageChosen);
        }
    }
}
