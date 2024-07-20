using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace kütüphane_sistemi_son
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string kitaplıkDosyaYolu = "C:\\Users\\ASUS\\Desktop\\kitaplık.txt";
            List<Kitap> kitaplar = new List<Kitap>();

            // Kitaplik dosyasini okuyup Kitap nesneleri oluşturma
            using (StreamReader okumaNesnesi = new StreamReader(kitaplıkDosyaYolu))
            {
                string satir;
                while ((satir = okumaNesnesi.ReadLine()) != null)
                {
                    string[] parcalar = satir.Split('*');
                    if (parcalar.Length >= 3)
                    {
                        string isim = parcalar[0];
                        string yazar = parcalar[1];
                        string yayinevi = parcalar[2];

                        List<string> sayfalar = new List<string>();
                        for (int i = 3; i < parcalar.Length; i++)
                        {
                            sayfalar.Add(parcalar[i]);
                        }

                        Kitap kitap = new Kitap(isim, yazar, yayinevi, sayfalar);
                        kitaplar.Add(kitap);
                    }
                }
            }
            

            
        Console.WriteLine("--------------KÜTÜPHANEMİZE HOŞ GELDİNİZ--------------");
            Console.WriteLine("Kullanıcı girişi için ----> 1");
            Console.WriteLine("Yetkili girişi için ----> 2");
            Console.WriteLine("Yazar girişi için ----> 3");
            int basilan = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("------------------------------");

            if (basilan == 1)
            {
                Console.WriteLine("Kayıt olmak için ----> 1");
                Console.WriteLine("Gİirş yapmak için ----> 2");
                int basilan2 = Convert.ToInt32(Console.ReadLine());
                if (basilan2 == 1)
                {
                    kullanıcı_işlemleri.UyeKayit();
                    if (kullanıcı_işlemleri.DogruGirisYapıldıMı)
                    {
                        Console.WriteLine("Kitap ödünç almak içi ----> 1");
                        Console.WriteLine("Kitap iade etmek için ----> 2");
                        int odunc = Convert.ToInt32(Console.ReadLine());
                        if (odunc == 1)
                        {
                            var siraliKitaplar = kitaplar.OrderBy(k => k.isim).ToList();

                            // Sıralanmış kitapları ekrana yazdırma
                            for (int i = 0; i < siraliKitaplar.Count; i++)
                            {
                                var kitap = siraliKitaplar[i];
                                Console.WriteLine($"{i + 1}-{kitap.isim} - {kitap.yazar} - {kitap.yayinevi}");
                            }
                            Console.Write("Ödünç almak istediğiniz kitabın numarasını giriniz: ");
                            int secilenNumara = Convert.ToInt32(Console.ReadLine()) - 1;
                            if (secilenNumara >= 0 && secilenNumara < siraliKitaplar.Count)
                            {
                                Kitap secilenKitap = siraliKitaplar[secilenNumara];
                                Console.WriteLine($"Seçilen Kitap: {secilenKitap.isim} - {secilenKitap.yazar}");

                                ödünç_alma_talep.KitapTalepEkle(secilenKitap.isim);
                                Console.WriteLine("Yetkili onayı için bekleniyor...");
                            }
                            else
                            {
                                Console.WriteLine("Yanlış kitap numarası.");
                            }
                        }
                        else if (odunc == 2)
                        {
                        }
                    }
                }
                else if (basilan2 == 2)
                {
                    kullanıcı_işlemleri.UyeGiris();
                    if (kullanıcı_işlemleri.DogruGirisYapıldıMı == true)
                    {
                        Console.WriteLine("------------------------------");
                        Console.WriteLine("Kitap ödünç almak için ----> 1");
                        Console.WriteLine("Kitap iade etmek için ----> 2");
                        int odunc = Convert.ToInt32(Console.ReadLine());

                        if (odunc == 1)
                        {
                            var siraliKitaplar = kitaplar.OrderBy(k => k.isim).ToList();

                            // Sıralanmış kitapları ekrana yazdırma
                            for (int i = 0; i < siraliKitaplar.Count; i++)
                            {
                                var kitap = siraliKitaplar[i];
                                Console.WriteLine($"{i + 1}-{kitap.isim} - {kitap.yazar} - {kitap.yayinevi}");
                            }
                            Console.Write("Ödünç almak istediğiniz kitabın numarasını giriniz: ");
                            int secilenNumara = Convert.ToInt32(Console.ReadLine()) - 1;
                            if (secilenNumara >= 0 && secilenNumara < siraliKitaplar.Count)
                            {
                                Kitap secilenKitap = siraliKitaplar[secilenNumara];
                                Console.WriteLine("------------------------------");
                                Console.WriteLine($"Seçilen Kitap: {secilenKitap.isim} - {secilenKitap.yazar}");

                                ödünç_alma_talep.KitapTalepEkle(secilenKitap.isim);
                            }
                            else
                            {
                                Console.WriteLine("------------------------------");
                                Console.WriteLine("Yanlış kitap numarası.");
                                Console.ReadLine();
                            }
                        }
                        else if (odunc == 2)
                        {
                            ödünç_alma_talep.KitapIadeEt(kullanıcı_işlemleri.aktifUyeTC);
                        }
                        else
                        {
                            Console.WriteLine("Yanlış tuşlama yapıldı.");
                        }
                    }
                }
            }
            else if (basilan == 2)
            {
                yetkili_işlemleri.YetkiliGiris();
               if (yetkili_işlemleri.dogruGiris)
               {
                    Console.WriteLine("------------------------------");
                    Console.WriteLine("Kitap taleplerini görüntülemek için ----> 1");
                    Console.WriteLine("Kitapların sayfalarını görüntülemek için ----> 2");
                    Console.WriteLine("Ödünç kitap canlı takip için ----> 3");
                    Console.WriteLine("Yazar kitap talebi için ----> 4");
                    int secim = Convert.ToInt32(Console.ReadLine());
                    if (secim == 1)
                    {
                        ödünç_alma_talep.TalepOnayla();
                    }
                    else if (secim == 2)
                    {
                        yetkili_işlemleri.KitapSayfalariniGoruntule(kitaplar);
                    }
                    else if(secim == 3)
                    {
                        yetkili_işlemleri.CanliTakip();
                    }
                    else if(secim == 4)
                    {
                        yetkili_işlemleri.YazarOnayı();
                    }
                }
            }
            else if (basilan == 3)
            {
                yazar_işlemleri.yazarGiris();
                if(yazar_işlemleri.DogruGirisYapıldıMı)
                {
                    yazar_işlemleri.yazarKitapEkleme();
                }
                if(yazar_işlemleri.DogruEklemeYapıldıMı)
                {
                    Console.WriteLine("Yetkiliye talebiniz gönderilmiştir.Eğer yetkili onaylarsa kitabınızı kütüphanemizin kitap listesinde görebilirsiniz.");
                    Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("Yanlış tuşlama yapıldı.");
            }
        }
    }
}