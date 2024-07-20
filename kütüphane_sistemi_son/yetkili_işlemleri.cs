using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kütüphane_sistemi_son
{
    internal class yetkili_işlemleri
    {
        public static string yetkiliAdi = "YETKİLİYİM";
        public static string yetkiliSifre = "YETKİLİ1234";
        public static bool dogruGiris = false;

        public static void YetkiliGiris()
        {
            Console.Write("Yetkili kullanıcı adınız : ");
            string yetkili_Adi = Console.ReadLine();
            Console.Write("Yetkili şifreniz : ");
            string yetkili_Sifresi = Console.ReadLine();

            if (yetkiliAdi == yetkili_Adi && yetkiliSifre == yetkili_Sifresi)
            {
                Console.WriteLine("Giriş Başarılı!");
                dogruGiris = true;
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("YANLIŞ GİRİŞ.BURAYA BULAŞMA.");
                Console.ReadLine();
            }
        }

        public static void KitapSayfalariniGoruntule(List<Kitap> kitaplar)
        {
            var siraliKitaplar = kitaplar.OrderBy(k => k.isim).ToList();

            // Sıralanmış kitapları ekrana yazdırma
            for (int i = 0; i < siraliKitaplar.Count; i++)
            {
                var kitap = siraliKitaplar[i];
                Console.WriteLine($"{i + 1}-{kitap.isim} - {kitap.yazar} - {kitap.yayinevi}");
            }

            Console.Write("İçeriğini görüntülemek istediğiniz kitabın numarasını giriniz: ");
            int secilenNumara = Convert.ToInt32(Console.ReadLine()) - 1;

            if (secilenNumara >= 0 && secilenNumara < siraliKitaplar.Count)
            {
                Kitap secilenKitap = siraliKitaplar[secilenNumara];
                Console.WriteLine($"Seçilen Kitap: {secilenKitap.isim} - {secilenKitap.yazar}");
                Console.ReadLine();

                foreach (var sayfaYolu in secilenKitap.sayfalar)
                {
                    StreamReader sr = new StreamReader($@"{sayfaYolu}");
                    string icerik = sr.ReadToEnd();
                    Console.WriteLine(icerik);
                    sr.Close();
                    Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("Yanlış kitap numarası.");
                Console.ReadLine();
            }
        }


        public static void CanliTakip()
        {
            string alınanKitaplar = "C:\\Users\\ASUS\\Desktop\\alınan kiaplar.txt";
            if (File.Exists(alınanKitaplar))
            {
                
                string[] satirlar = File.ReadAllLines(alınanKitaplar);
                foreach (string satir in satirlar)
                {
                    string[] parcalar = satir.Split('*');
                    if (parcalar.Length == 3)
                    {
                        string kitapAdi = parcalar[0];
                        string uyeTC = parcalar[1];
                        DateTime alisTarihi = DateTime.Parse(parcalar[2]);
                        TimeSpan geçenSüre = DateTime.Now - alisTarihi;
                        int kalanGün = 15 - geçenSüre.Days;

                        if (kalanGün < 0)
                        {
                            Console.WriteLine($"ÖDÜNÇ : {kitapAdi},  {uyeTC} - Süre dolmuş! Gecikme: {-kalanGün} gün.");
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine($"ÖDÜNÇ : {kitapAdi}, {uyeTC} - Kalan süre: {kalanGün} gün.");
                            Console.ReadLine();
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Ödünç alınan kitap bulunmuyor.");
                Console.ReadLine();
            }
        }


        public static void YazarOnayı()
        {
            string dosyaYolu = "C:\\Users\\ASUS\\Desktop\\yazar kitap ekleme talebi.txt";
            string dosyaYoluKitaplık = "C:\\Users\\ASUS\\Desktop\\kitaplık.txt";
            using (StreamReader okumaN = new StreamReader(dosyaYolu))
            {
                string satir = "";
                while ((satir = okumaN.ReadLine()) != null)
                {
                    string[] parcalar = satir.Split('*');
                    if (parcalar.Length != 0)
                    {
                        Console.WriteLine("Yazarın onay için gönderdiği kitap bilgileri : ");
                        Console.WriteLine($"Kitap ismi : {parcalar[0]}");
                        Console.WriteLine($"Yazar ismi : {parcalar[1]}");
                        Console.WriteLine($"Yayınevi : {parcalar[2]}");
                        Console.WriteLine("Sayfaalrını okumak istersen ----> 1 : ");
                        Console.WriteLine("Sayfalarını okumadan direkt reddetmek istersen ----> 2 : ");
                        int istek = Convert.ToInt32(Console.ReadLine());

                        if(istek==1)
                        {
                            for (int i = 3; i < parcalar.Length; i++)
                            {
                                StreamReader okumaN2 = new StreamReader($@"{parcalar[i]}");
                                string icerik = okumaN2.ReadToEnd();
                                Console.WriteLine($"{i - 2}.sayfa : {icerik}");
                            }

                            Console.WriteLine("Bu kitabın kitaplığa eklenmesine onay veriyor musun : (E/H)");
                            string onay = Console.ReadLine();
                            if(onay == "E" || onay== "e")
                            {
                                StreamWriter yazmaN = new StreamWriter(dosyaYoluKitaplık, true);
                                yazmaN.WriteLine(satir);
                                yazmaN.Close();
                                Console.WriteLine("Başarıyla kitaplığa eklendi.");
                                Console.ReadLine();
                            }
                            else if(onay == "H" || onay == "h")
                            {
                                Console.WriteLine("Talep reddedildi.");
                                Console.ReadLine();
                            }
                            else
                            {
                                Console.WriteLine("Yanlış bir tuşlama yaptınız. ");
                                Console.ReadLine();
                            }
                        }
                        else if(istek==2)
                        {
                            Console.WriteLine("Talep reddedildi.");
                            Console.ReadLine();
                        }

                    }
                    else
                    {
                        Console.WriteLine("Herhangi bir yazar talebi bulunamadı.");
                        Console.ReadLine();
                    }
                }
            }
            File.WriteAllText(dosyaYolu, string.Empty);
        }
    }
}
