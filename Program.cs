namespace ATM
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<kullanici> kullanicilar = new List<kullanici>();
            int sayac = -1;
            int sayacKullanici = 0;
            List<string> basariligirisler = new List<string>();
            List<string> fraudgirisler = new List<string>();
            YetkiliKullanici yk = new YetkiliKullanici("görevli1", 1,"123", basariligirisler,fraudgirisler);
                     
            void YeniMusteriEkle()
            {
                sayacKullanici = sayacKullanici + 1;
                Console.Write("isim:");
                string Yisim = Console.ReadLine();
                Console.Write("parola:");
                string Yparola = Console.ReadLine();
                kullanicilar.Add(new kullanici(Yisim, Yparola,sayacKullanici));
                
            }

            void ATMislemleri(kullanici kl)
            {
               basariligirisler.Add(kl.isim + " " + DateTime.Now.ToString() + " tarihinde giriş yaptı.\n");
                while (true)
                {
                    Console.WriteLine("\nİŞLEMLER\n*********************");
                    Console.Write("1-para çekme\n2-para yatırma\n3-bilgileri görme\nq-çıkış\nseçim:");
                    string secimATM = Console.ReadLine();
                    if (secimATM == "1")
                    {
                        Console.Write("hesaptan çekilecek bakiye tutarı:");
                        int bakiyeÇek = Convert.ToInt32(Console.ReadLine());
                        if (kl.bakiye > bakiyeÇek)
                        {
                            kl.bakiye = kl.bakiye - bakiyeÇek;
                            Console.WriteLine("işlem başarılı.");
                            basariligirisler.Add(kl.isim + " " + DateTime.Now.ToString() + " tarihinde para çekti.\n");
                        }
                        else
                        {
                            Console.WriteLine("hesapta yeterli bakiye yok.");
                            fraudgirisler.Add(kl.isim + "  " + DateTime.Now.ToString() + " tarihinde para çekme işlemi reddedildi.\n");
                        }
                    }
                    if (secimATM == "2")
                    {
                        Console.Write("hesabınıza yüklenecek bakiye tutarı:");
                        int bakiyeYükle = Convert.ToInt32(Console.ReadLine());
                        kl.bakiye = kl.bakiye + bakiyeYükle;
                        Console.WriteLine("işlem başarılı.");
                        basariligirisler.Add(kl.isim + " " + DateTime.Now.ToString() + " tarihinde para yatırdı.\n");
                    }
                    if (secimATM == "3")
                    {
                        Console.WriteLine("\nisim:"+kl.isim);
                        Console.WriteLine("kart no:"+kl.kartNo);
                        Console.WriteLine("bakiye:"+kl.bakiye);
                        basariligirisler.Add(kl.isim + " " + DateTime.Now.ToString() + " tarihinde bilgilerini görüntüledi.\n");

                    }
                    if (secimATM == "q")
                    {
                        basariligirisler.Add(kl.isim + " " + DateTime.Now.ToString() + " tarihinde çıkış yaptı.\n");
                        sayac = -1;
                        
                        break;
                        
                    }
                }

            }

            while (true)
            {
                int sayac2 = -1;
                if (sayac==-1)
                {
                    Console.WriteLine("\nATM Uygulamasına Hoşgeldiniz.");
                    Console.Write("1-uygulamaya giriş\n2-yeni müşteri olma\n3-uygulama sonlandırma\nseçim:");
                    string secim = Console.ReadLine();
                    if (secim == "1")
                    {
                        Console.Write("isim:");
                        string isim = Console.ReadLine();
                        Console.Write("kart numarası:");
                        int kartno =Convert.ToInt32( Console.ReadLine());
                        Console.Write("parola:");
                        string parola = Console.ReadLine();
                        if (isim == yk.isim && kartno == yk.kartNo && parola == yk.parola)
                        {

                            yk.GunSonuRapor();
                            break;
                        }
                        else
                        {
                            foreach (var temp in kullanicilar)
                            {
                                if (temp.isim == isim && temp.kartNo == kartno && temp.parola == parola)
                                {
                                    ATMislemleri(temp);
                                    sayac2 = 1;
                                }
                            }
                            if (sayac2 == -1)
                            {
                                fraudgirisler.Add(isim + " kişisi " + DateTime.Now.ToString() + " tarihinde şüpheli giriş yaptı\n");
                                sayac = 1;
                            }
                        }
                    }
                    if (secim == "2")
                    {
                        YeniMusteriEkle();
                        ATMislemleri(kullanicilar[kullanicilar.Count - 1]);
                    }
                    if (secim == "3")
                    {   
                       
                        break;
                    }
                }

                if (sayac == 1)
                {   
                    
                    Console.WriteLine("\ngörünüşe göre müşterimiz değilsiniz.");
                    Console.Write("müşterimiz olmak ister misiniz(y/n):");
                    string Ymusteri = Console.ReadLine();
                    if (Ymusteri == "y")
                    {
                        YeniMusteriEkle();
                        ATMislemleri(kullanicilar[kullanicilar.Count - 1]);
                    }
                    else if (Ymusteri == "n")
                    {
                        Console.WriteLine("tekrar bekleriz.");
                        sayac = -1;
                    }
   
                }
                
            }

        }
       
    }
    class kullanici
    {
        private string Isim;
        private string Parola;
        private int KartNo=0;
        private int Bakiye = 0;
        public kullanici(string isimm, string parolaa,int kartnoo)
        {
            Isim = isimm;
            Parola = parolaa;
            KartNo = kartnoo;
        }
        public String isim
        {
            get { return Isim; }
            set { Isim = value; }
        }
        public String parola
        {
            get { return Parola; }
            set { Parola = value; }
        }

        public int kartNo
        {
            get { return KartNo; }
            set { KartNo = value; }
        }

        public int bakiye
        {
            get { return Bakiye; }
            set { Bakiye = value; }
        }
    }

    class YetkiliKullanici : kullanici
    {
        private List<string> basariliGiris = new List<string>();
        private List<string> fraudGiris = new List<string>();
        public YetkiliKullanici(string isimm, int kartnoo, string parolaa,List<string> basariligiriss,List<string> fraudgiriss) : base(isimm, parolaa,kartnoo)
        {
            basariliGiris = basariligiriss;
            fraudGiris = fraudgiriss;
            kartNo = kartnoo;
        }

        public List<string> BasariliGiris { get => basariliGiris; set => basariliGiris = value; }
        public List<string> FraudiGiris { get => basariliGiris; set => basariliGiris = value; }

        public void GunSonuRapor()
        {
            string basariligirisfile = "BAŞARILI GİRİŞLER*************\n"; ;
            string fraudgirisfile = "FRAUD GİRİŞLER*************\n"; ;
            if (basariliGiris.Count == 0)
            {
                basariligirisfile =basariligirisfile+ "bugün başarılı giriş tespit edilemedi.\n";
            }
            else
            {
                
                foreach(var item in basariliGiris)
                {
                    basariligirisfile =basariligirisfile+ item;
                }
               
            }

            if (fraudGiris.Count==0)
            {

                fraudgirisfile =fraudgirisfile+ "bugün fraud giriş tespit edilemedi.\n";
            }
            else
            {
                
                foreach (var item in fraudGiris)
                {
                    fraudgirisfile = fraudgirisfile +item;
                }
                
            }
            Console.WriteLine("dosya oluşturuldu.");
            string file = @"C:\Users\ELİF\Desktop\EOD_" + DateTime.Now.ToString("ddMMyyyy") + ".txt";
            File.WriteAllText(file, basariligirisfile+fraudgirisfile);
        }
    }
}
