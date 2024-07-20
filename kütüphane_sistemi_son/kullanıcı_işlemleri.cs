using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kütüphane_sistemi_son
{
    internal class kullanıcı_işlemleri
    {
        public static string uyeDosyaYolu = @"C:\Users\ASUS\Desktop\üyeler dosyası.txt";
        public static bool DogruGirisYapıldıMı = false;
        public static bool UyeVarMi = false;
        public static string aktifUyeTC = "";

        public static void UyeKayit()
        {
            string sifre = "";
            string satir = "";
            Console.Write("TC KİMLİK NUMARANIZ : ");
            string tc = Console.ReadLine();
            Console.Write("ADINIZ : ");
            string isim = Console.ReadLine();
            Console.Write("SOYADINIZ : ");
            string soyisim = Console.ReadLine();
            Console.Write("TELEFON NUMARANIZ : ");
            string telno = Console.ReadLine();
            Console.Write("ŞİFRE BELİRLEYİNİZ : ");
            string sifre1 = Console.ReadLine();
            Console.Write("YENİ ŞİFRENİZİ TEKRAR GİRİNİZ : ");
            string sifre2 = Console.ReadLine();
            if (sifre1 == sifre2)
            {
                sifre = sifre1;
                satir = tc + "*" + isim + "*" + soyisim + "*" + telno + "*" + sifre;
            }
            else
            {
                Console.WriteLine("Girdiğiniz iki şifre hatalıdır.Tekrar deneyiniz.");
                Console.WriteLine("ŞİFRE BELİRLEYİNİZ : ");
                string sifre3 = Console.ReadLine();
                Console.WriteLine("YENİ ŞİFRENİZİ TEKRAR GİRİNİZ : ");
                string sifre4 = Console.ReadLine();
                if (sifre3 == sifre4)
                {
                    sifre = sifre3;
                    satir = tc + "*" + isim + "*" + soyisim + "*" + telno + "*" + sifre;
                }
                else
                {
                    Console.WriteLine("Tekrar hatalı giriş yapıldı.");
                    Console.WriteLine("Güvenliğiniz için program kapatılıyor...");
                    Environment.Exit(0);
                }
            }
            string satir2;
            StreamReader okumaNesnesi = new StreamReader(uyeDosyaYolu);

            while ((satir2 = okumaNesnesi.ReadLine()) != null)
            {
                string[] parcalar = satir2.Split('*');
                if (parcalar[0] == tc )
                {
                    Console.WriteLine("Zaten bir üyeliğiniz bulunmakta.");
                    Console.WriteLine("Giriş yapmak ister misiniz ? (E/H)");
                    string girisYapmak = Console.ReadLine();
                    if (girisYapmak == "e" && girisYapmak == "E")
                    {
                        UyeGiris();
                        UyeVarMi = true;
                        break;
                    }
                }
            }
            okumaNesnesi.Close();
            if (UyeVarMi == false)
            {
                StreamWriter yazmaNesnesi = new StreamWriter(uyeDosyaYolu, true);
                yazmaNesnesi.WriteLine(satir);
                yazmaNesnesi.Close();
                Console.WriteLine("Kaydınız başarıyla oluşturuldu.");
                Console.WriteLine("----Giriş----");
                UyeGiris();
            }
        }



        public static void UyeGiris()
        {
            Console.Write("TC KİMLİK NO : ");
            string tc = Console.ReadLine();
            Console.Write("ŞİFRENİZ : ");
            string sifre = Console.ReadLine();

            string satir;
            StreamReader okumaNesnesi2 = new StreamReader(uyeDosyaYolu);

            while ((satir = okumaNesnesi2.ReadLine()) != null)
            {
                string[] parcalar = satir.Split('*');
                if (parcalar[0] == tc && parcalar[4] == sifre)
                {
                    Console.WriteLine("HOŞGELDİNİZ ! SAYIN " + parcalar[1]);
                    DogruGirisYapıldıMı = true;
                    aktifUyeTC = tc;
                    break;
                }
            }
            Console.ReadLine();
            okumaNesnesi2.Close();
            if (DogruGirisYapıldıMı == false)
            {
                Console.WriteLine("Kayıt bulunamadı.Kayıt oluşturmak ister misiniz ? (E/H) : ");
                string tuş = Console.ReadLine();
                if (tuş == "E" && tuş == "e")
                {
                    UyeKayit();
                }
            }
        }
    }
}
