using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kütüphane_sistemi_son
{
    internal class yazar_işlemleri
    {
        public static string yazarAdi = "YAZARIM";
        public static string yazarSifre = "YAZAR1234";
        public static bool DogruGirisYapıldıMı = false;
        public static bool DogruEklemeYapıldıMı = false;

        public static void yazarGiris()
        {

            Console.WriteLine("Yazar kullanıcı adınız : ");
            string yazar_Adi = Console.ReadLine();
            Console.WriteLine("Yazar şifreniz : ");
            string yazar_Sifresi = Console.ReadLine();

            if (yazar_Adi == yazarAdi && yazar_Sifresi == yazarSifre)
            {
                Console.WriteLine("Giriş Başarılı!");
                DogruGirisYapıldıMı = true;
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("YANLIŞ GİRİŞ.BURAYA BULAŞMA.");
                Console.ReadLine(); 
            }
        }

        public static void yazarKitapEkleme()
        {
            string dosyaYolu = "C:\\Users\\ASUS\\Desktop\\yazar kitap ekleme talebi.txt";
            Console.WriteLine("Kitap eklemek ister misiniz : (E/H)");
            string kitap = Console.ReadLine();
            if (kitap == "E" || kitap == "e")
            {
                Console.Write("Kitap ismi giriniz : ");
                string kitapIsmi = Console.ReadLine();
                Console.Write("Yazar ismi giriniz : ");
                string yazarIsmi = Console.ReadLine();
                Console.Write("Yayınevi giriniz : ");
                string yayınEvi = Console.ReadLine();
                Console.WriteLine("Kitabınız kaç sayfa olacak : ");
                int sayfaSayısı = Convert.ToInt32(Console.ReadLine());
                string[] yazarDosyaYolları = new string[sayfaSayısı];
                for (int i = 0; i < sayfaSayısı; i++)
                {
                    Console.WriteLine($"{i + 1}. sayfa dosya yolunu giriniz : ");
                    yazarDosyaYolları[i] = Console.ReadLine();
                }
                string kitapBilgileri = $"{kitapIsmi}*{yazarIsmi}*{yayınEvi}*{string.Join("*", yazarDosyaYolları)}";

                using (StreamWriter yazmaN = new StreamWriter(dosyaYolu, true))
                {
                    yazmaN.WriteLine(kitapBilgileri);
                }

                Console.WriteLine("Kitap bilgileri başarıyla kaydedildi.");
                DogruEklemeYapıldıMı = true;
                Console.ReadLine();



            }
        }
    }
}

