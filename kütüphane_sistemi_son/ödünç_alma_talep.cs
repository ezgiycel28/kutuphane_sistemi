using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kütüphane_sistemi_son
{
    internal class ödünç_alma_talep
    {
        public static Queue<(string kitapIsmi, string uyeTC)> kitapTalepleri = new Queue<(string kitapIsmi, string uyeTC)>();
        public static string alinanKitaplar = "C:\\Users\\ASUS\\Desktop\\alınan kiaplar.txt";

        public static void KitapTalepEkle(string kitapIsmi)
        { 
            string uyeTC = kullanıcı_işlemleri.aktifUyeTC; // Giriş yapan kullanıcının TC numarasını al
            if (string.IsNullOrEmpty(uyeTC))
            {
                Console.WriteLine("Kullanıcı giriş yapmamış.");
                Console.ReadLine();
                return;
            }
            kitapTalepleri.Enqueue((kitapIsmi, uyeTC));

            using (StreamWriter yazmaNesnesi = new StreamWriter("C:\\Users\\ASUS\\Desktop\\kitap talepleri listesi.txt", true))
            {
                yazmaNesnesi.WriteLine($"{kitapIsmi}*{uyeTC}");
            }

            Console.WriteLine($"{kitapIsmi} için talep oluşturuldu. Yetkili onayı bekleniyor.");
            Console.ReadLine();
        }

        public static void TalepOnayla()
        {
            using (StreamReader okumaNesnesi = new StreamReader("C:\\Users\\ASUS\\Desktop\\kitap talepleri listesi.txt"))
            {
                string satir;
                while ((satir = okumaNesnesi.ReadLine()) != null)
                {
                    string[] parcalar = satir.Split('*');
                    if (parcalar.Length == 2)
                    {
                        string kitapIsmi = parcalar[0];
                        string uyeTC = parcalar[1];
                        kitapTalepleri.Enqueue((kitapIsmi, uyeTC));
                    }
                }
            }

            if (kitapTalepleri.Count == 0)
            {
                Console.WriteLine("Onaylanacak talep bulunmuyor.");
                Console.ReadLine();
            }
            else
            {
                while (kitapTalepleri.Count > 0)
                {
                    var (talepEdilenKitap, uyeTC) = kitapTalepleri.Dequeue();
                    Console.WriteLine($"{talepEdilenKitap} için onay veriyor musunuz? (E/H)");
                    string onay = Console.ReadLine();
                    if (onay == "E" || onay == "e")
                    {
                        Console.WriteLine($"{talepEdilenKitap} talebi onaylandı.");
                        string tarih = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        // Kitap alındı olarak işaretleme
                        using (StreamWriter yazmaNesnesi = new StreamWriter(alinanKitaplar, true))
                        {
                            yazmaNesnesi.WriteLine($"{talepEdilenKitap}*{uyeTC}*{tarih}");
                        }
                    }
                    else if(onay == "H" || onay=="h")
                    {
                        Console.WriteLine($"{talepEdilenKitap} talebi reddedildi.");
                    }
                    else
                    {
                        Console.WriteLine("İşlem yapılamadı.");
                    }
                }
                File.WriteAllText("C:\\Users\\ASUS\\Desktop\\kitap talepleri listesi.txt", string.Empty);
                Console.WriteLine("Tüm talepler işlendi.");
                Console.ReadLine();
            }
        }

        public static void KitapIadeEt(string uyeTC)
        {
            List<string> iadeEdebilecegimKitaplar = new List<string>();

            // Kullanıcının iade edebileceği kitapları listeleme
            using (StreamReader okumaNesnesi = new StreamReader(alinanKitaplar))
            {
                string satir;
                while ((satir = okumaNesnesi.ReadLine()) != null)
                {
                    string[] parcalar = satir.Split('-');
                    if (parcalar.Length >= 3 && parcalar[1].Trim() == uyeTC)
                    {
                        iadeEdebilecegimKitaplar.Add(parcalar[0].Trim());
                    }
                }
            }

            if (iadeEdebilecegimKitaplar.Count == 0)
            {
                Console.WriteLine("İade edilecek kitap bulunmuyor.");
                return;
            }

            Console.WriteLine("İade edebileceğiniz kitaplar:");
            for (int i = 0; i < iadeEdebilecegimKitaplar.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {iadeEdebilecegimKitaplar[i]}");
            }

            Console.Write("İade etmek istediğiniz kitabın numarasını giriniz: ");
            int secilenNumara = Convert.ToInt32(Console.ReadLine()) - 1;

            if (secilenNumara >= 0 && secilenNumara < iadeEdebilecegimKitaplar.Count)
            {
                string iadeEdilenKitap = iadeEdebilecegimKitaplar[secilenNumara];
                List<string> yeniSatirlar = new List<string>();

                // Kitap iadesi sonrasında dosyayı güncelleme
                using (StreamReader okumaNesnesi = new StreamReader(alinanKitaplar))
                {
                    string satir;
                    while ((satir = okumaNesnesi.ReadLine()) != null)
                    {
                        string[] parcalar = satir.Split('-');
                        if (parcalar.Length >= 3 && !(parcalar[0].Trim() == iadeEdilenKitap && parcalar[1].Trim() == uyeTC))
                        {
                            yeniSatirlar.Add(satir);
                        }
                    }
                }

                // Yeni satırları dosyaya yazma
                using (StreamWriter yazmaNesnesi = new StreamWriter(alinanKitaplar))
                {
                    foreach (string yeniSatir in yeniSatirlar)
                    {
                        yazmaNesnesi.WriteLine(yeniSatir);
                    }
                }

                Console.WriteLine($"{iadeEdilenKitap} kitabı başarıyla iade edildi.");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Geçersiz kitap numarası.");
            }
        }
    }
}

